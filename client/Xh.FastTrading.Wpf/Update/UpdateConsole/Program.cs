using AutoUpdate;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UpdateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filepath = "";//文件得下载地址

                Process p = new Process();
                p.StartInfo.FileName = "AutoUpdate.exe";
                //filepaht = 下载文件的地址,  空格隔开的是 启动程序的完整路径
                p.StartInfo.Arguments = filepath + " " + System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                p.Start();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
