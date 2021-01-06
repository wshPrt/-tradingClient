using System;
using System.Diagnostics;

namespace Xh.FastTrading.Wpf.Common.Untils
{
    public class Uninstall
    {
        /// <summary>
        /// 启动卸载程序
        /// </summary>
        public static void Start()
        {
            Process m_Process = new Process();
            m_Process.StartInfo.FileName = "Uninstall.exe";
            m_Process.StartInfo.UseShellExecute = false;
            m_Process.Start();
        }
    }
}