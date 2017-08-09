using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CommonTools.Lib.fx45.FileSystemTools
{
    public class CurrentExeTools
    {
    }


    public static class CurrentExe
    {
        public static string GetFullPath()
        {
            return Assembly.GetEntryAssembly().Location;
        }


        public static string GetDirectory()
        {
            var exe = GetFullPath();
            return Directory.GetParent(exe).FullName;
        }


        public static string GetVersion()
        {
            var exe = GetFullPath();
            return FileVersionInfo.GetVersionInfo(exe).FileVersion;
        }
    }
}
