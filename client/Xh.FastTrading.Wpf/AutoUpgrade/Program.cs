using System;
using System.Diagnostics;
using System.Threading;

namespace AutoUpgrade
{
    class Program
    {
        private static string ProgressName = "Sup-Trade";
        internal static string URL, URL_UPGRADE;
        internal static string VERSION, VERSION_NO;
        internal static string FTP_USER = "ftp_client", FTP_PASSWORD = "H7z@Xs16";

        [STAThread]
        public static void Main(string[] args)
        {
            KillProgress();

            if (args.Length == 4)
            {
                URL = args[0];
                URL_UPGRADE = args[1];
                VERSION = args[2];
                VERSION_NO = args[3];
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
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
    }
}