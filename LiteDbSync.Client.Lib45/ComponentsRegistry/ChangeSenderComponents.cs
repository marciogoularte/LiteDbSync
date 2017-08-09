using Autofac;
using CommonTools.Lib.fx45.DependencyInjection;
using CommonTools.Lib.fx45.ExceptionTools;
using CommonTools.Lib.fx45.FileSystemTools;
using CommonTools.Lib.fx45.ViewModelTools;
using CommonTools.Lib.ns11.FileSystemTools;
using LiteDbSync.Client.Lib45.ChangeSenders;
using LiteDbSync.Client.Lib45.DatabaseReaders;
using LiteDbSync.Client.Lib45.ViewModels;
using LiteDbSync.Client.Lib45.ViewModels.SoloFileWatcher;
using LiteDbSync.Common.API.Configuration;
using LiteDbSync.Common.API.ServiceContracts;
using System;
using System.IO;
using System.Windows;

namespace LiteDbSync.Client.Lib45.ComponentsRegistry
{
    public static class ChangeSenderComponents
    {
        const string SETTINGS_CFG = "settings.cfg";

        private static ILifetimeScope BuildAndBeginScope(Application app)
        {
            var b   = new ContainerBuilder();
            var cfg = JsonFile.Read<ChangeSenderSettings>(SETTINGS_CFG);

            b.RegisterInstance<ChangeSenderSettings>(cfg);

            b.Solo  <MainSenderWindowVM>();
            b.Multi <SoloFileWatcherVM>();

            b.Multi<IThrottledFileWatcher, ThrottledFileWatcher1>();
            b.Multi<ILocalDbReader, LocalDbReader1>();
            b.Multi<IChangeSender, ChangeSender1>();

            SetDataTemplates(app);

            return b.Build().BeginLifetimeScope();
        }


        private static void SetDataTemplates(Application app)
        {
            app.SetTemplate<SoloFileWatcherVM, SoloFileWatcherUI>();
        }


        public static ILifetimeScope Build(Application app)
        {
            try
            {
                return BuildAndBeginScope(app);
            }
            catch (FileNotFoundException)
            {
                WriteDefaultSettingsFile();
                return BuildAndBeginScope(app);
            }
            catch (Exception ex)
            {
                ex.ShowAlert(true, true);
                return null;
            }
        }


        private static void WriteDefaultSettingsFile()
        {
            var cfg = ChangeSenderSettings.CreateDefault();
            JsonFile.Write(cfg, SETTINGS_CFG);
        }
    }
}
