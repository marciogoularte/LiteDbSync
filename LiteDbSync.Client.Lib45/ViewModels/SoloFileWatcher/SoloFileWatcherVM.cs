﻿using CommonTools.Lib.fx45.InputTools;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.FileSystemTools;
using CommonTools.Lib.ns11.InputTools;
using CommonTools.Lib.ns11.ExceptionTools;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.ComponentModel;

namespace LiteDbSync.Client.Lib45.ViewModels.SoloFileWatcher
{
    public class SoloFileWatcherVM : ViewModelBase
    {
        private IThrottledFileWatcher _watchr;
        private IChangeSender         _sendr;
        private ILocalDbReader        _db;
        private FileWatcherSettings   _cfg;


        public SoloFileWatcherVM(IThrottledFileWatcher throttledFileWatcher,
                                 ILocalDbReader localDbReader,
                                 IChangeSender changeSender)
        {
            _db     = localDbReader;
            _sendr  = changeSender;
            _watchr = throttledFileWatcher;
            _watchr.FileChanged += _watchr_FileChanged;

            StartWatchingCmd = R2Command.Relay(StartWatchingLDB);
            StopWatchingCmd  = R2Command.Relay(StopWatchingLDB);
        }


        public IR2Command  StartWatchingCmd  { get; }
        public IR2Command  StopWatchingCmd   { get; }
        public long        LatestId          { get; private set; }


        private void StartWatchingLDB()
        {
            _watchr.IntervalMS = _cfg.IntervalMS;
            _watchr.StartWatching(_cfg.DbFilePath);
        }


        private void StopWatchingLDB()
        {
            _watchr.StopWatching();
        }


        private void _watchr_FileChanged(object sender, EventArgs e)
        {
            if (!TryGetLatestId()) return;
            try
            {
                _sendr.SendLatestId(LatestId);
            }
            catch (Exception ex)
            {
                SetStatus(ex.Info(true, true));
            }
        }


        private bool TryGetLatestId()
        {
            try
            {
                LatestId = _db.GetLatestId(_cfg.DbFilePath, 
                                           _cfg.CollectionName);
                return true;
            }
            catch (Exception ex)
            {
                SetStatus(ex.Info(true, true));
                return false;
            }
        }


        internal void SetTarget(FileWatcherSettings fileWatcherSettings)
        {
            _cfg = fileWatcherSettings;
            var nme = Path.GetFileName(_cfg.DbFilePath);
            var typ = _cfg.CollectionName;
            var ims = $"every {_cfg.IntervalMS:n0} ms";
            UpdateTitle($"Watching “{nme}” ‹{typ}› [{ims}]");
        }
    }
}