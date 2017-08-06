using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Factorio.Launcher
{
    public class FactorioInstallation
    {
        public static string BinarySubpath = "bin\\x64\\factorio.exe";
        public static string ModDirFileSubpath = "mod-dirs.json";
        public readonly string FactorioPath;

        public List<string> ModDirectoryList { get; }


        public FactorioInstallation(string path)
        {
            if(!IsFactorioInstallation(path))
                throw  new ArgumentException("Path must be a valid factorio installation.");

            FactorioPath = path;
            ModDirectoryList = GetOrGenerateModDirectoryList();
        }

        private List<string> GetOrGenerateModDirectoryList()
        {
            var moddirsFile = Path.Combine(FactorioPath, ModDirFileSubpath);
            if (File.Exists(moddirsFile))
            {
                var fileContent = File.ReadAllText(moddirsFile);
                return JsonConvert.DeserializeObject<List<string>>(fileContent);
            }

            var dirs = Directory.EnumerateDirectories(FactorioPath);

            return (from dir in dirs let modListFile = Path.Combine(FactorioPath, dir, "mod-list.json") where File.Exists(modListFile) select dir).ToList();
        }

        public static bool IsFactorioInstallation(string path)
        {
            if (!Directory.Exists(path))
                return false;

            var bindir = Path.Combine(path, BinarySubpath);

            if (!File.Exists(bindir))
                return false;

            return true;
        }

        public void Launch(string modDir)
        {
            var exePath = Path.Combine(FactorioPath, BinarySubpath);
            var p = new Process();
            p.StartInfo.WorkingDirectory = FactorioPath;
            p.StartInfo.FileName = BinarySubpath;
            p.StartInfo.Arguments = $"--mod-directory {modDir}";
            p.Start();
        }
    }
}
