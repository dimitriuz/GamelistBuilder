using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GamelistBuilder.Infrastructure
{
    public class FolderRepository
    {

        public static IEnumerable<string> GetDrives()
        {
            IEnumerable<string> drives = new List<string>();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                drives = DriveInfo.GetDrives().Select(d => d.Name);
            };
            return drives;
        }

        public static IEnumerable<string> GetFiles(string path = "")
        {
            IEnumerable<string> files = Directory.GetFiles(path);
            return files;
        }

        public static IEnumerable<string> GetDirectories(string path = "")
        {
            IEnumerable<string> directories = Directory.GetDirectories(path);
            return directories;
        }

        public static string GetUpPath(string path)
        {
            return Directory.GetParent(path).FullName;
        }

    }
}
