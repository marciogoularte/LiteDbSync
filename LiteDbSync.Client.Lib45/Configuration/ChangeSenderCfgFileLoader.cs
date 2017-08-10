using CommonTools.Lib.fx45.FileSystemTools;
using LiteDbSync.Common.API.Configuration;
using System.IO;

namespace LiteDbSync.Client.Lib45.Configuration
{
    public static class ChangeSenderCfgFile
    {
        const string SETTINGS_CFG = "ChangeSender.cfg";


        public static ChangeSenderSettings LoadOrDefault()
        {
            try
            {
                return JsonFile.Read<ChangeSenderSettings>(SETTINGS_CFG);
            }
            catch (FileNotFoundException)
            {
                return WriteDefaultSettingsFile();
            }
        }


        private static ChangeSenderSettings WriteDefaultSettingsFile()
        {
            var cfg = ChangeSenderSettings.CreateDefault();
            JsonFile.Write(cfg, SETTINGS_CFG);
            return cfg;
        }
    }
}
