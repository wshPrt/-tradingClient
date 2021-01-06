using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Commands.IniFile;

namespace Xh.FastTrading.Wpf.Common.Untils
{
  public  class Configuration
    {
        public class SerivceFiguration
        {
            /// <summary>
            /// 配置文件
            /// </summary>
            public const string INI_CFG = "config\\user.ini";

            /// <summary>
            /// 保存记录
            /// </summary>
            public const string Save_Records  = "data\\userSettings.ini";

            /// <summary>
            /// 版本文件
            /// </summary>
            public const string INI_VERSION = "config\\version.ini";

            /// <summary>
            /// 获取本地样式参数
            /// </summary>
            /// <returns></returns>
            //public static string GetSkin()
            //{
            //    string cfgINI = AppDomain.CurrentDomain.BaseDirectory + INI_CFG;
            //    if (File.Exists(cfgINI))
            //    {
            //        IniFiles ini = new IniFiles(cfgINI);
            //        string SkinName = ini.IniReadValue("Skin", "Skin");
            //        return SkinName;
            //    }
            //    else
            //        return string.Empty;
            //}

            /// <summary>
            /// 设置样式
            /// </summary>
            /// <param name="SkinName"></param>
            //public static void SetKin(string SkinName)
            //{
            //    string cfgINI = AppDomain.CurrentDomain.BaseDirectory + INI_CFG;
            //    IniFiles ini = new IniFiles(cfgINI);
            //    ini.IniWriteValue("Skin", "Skin", SkinName);
            //}
        }
    }
}
