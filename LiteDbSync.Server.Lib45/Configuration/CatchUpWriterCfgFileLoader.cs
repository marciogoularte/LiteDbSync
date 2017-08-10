using CommonTools.Lib.fx45.FileSystemTools;
using LiteDbSync.Common.API.Configuration;
using System.IO;

namespace LiteDbSync.Server.Lib45.Configuration
{
    public static class CatchUpWriterCfgFile
    {
        const string SETTINGS_CFG = "CatchUpWriter.cfg";


        public static CatchUpWriterSettings LoadOrDefault()
        {
            try
            {
                return JsonFile.Read<CatchUpWriterSettings>(SETTINGS_CFG);
            }
            catch (FileNotFoundException)
            {
                return WriteDefaultSettingsFile();
            }
        }


        private static CatchUpWriterSettings WriteDefaultSettingsFile()
        {
            var cfg = CatchUpWriterSettings.CreateDefault();
            JsonFile.Write(cfg, SETTINGS_CFG);
            return cfg;
        }
    }
}
