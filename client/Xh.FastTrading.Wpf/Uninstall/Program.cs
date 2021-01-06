using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Uninstall
{
    class Program
    {
        private static string ProgressName = "Sup-Trade";

        static void Main(string[] args)
        {
            KillProgress();

            List<Tuple<string, int>> files = new List<Tuple<string, int>>();
            GetFiles(ref files);

            files.ForEach(file =>
            {
                try
                {
                    if (file.Item2 == 1)
                        Directory.Delete(file.Item1, true);
                    else
                        File.Delete(file.Item1);
                }
                catch { }
            });
        }

        private static void KillProgress()
        {
            bool flag = true;
            while (flag)
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList)
                {
                    if (process.ProcessName.Contains(ProgressName))
                    {
                        process.Kill();
                        Thread.Sleep(1000);
                    }
                    else
                        flag = false;
                }
            }
        }

        public static void GetFiles(ref List<Tuple<string, int>> files)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileSystemInfo[] fsinfos = di.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                files.Add(new Tuple<string, int>(fsinfo.FullName, (fsinfo is DirectoryInfo ? 1 : 2)));
            }
        }
    }
}