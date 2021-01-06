using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Commands.IniFile;

namespace Xh.FastTrading.Wpf.Common.Commands.IniFile
{
    public class Configuration
    {
        /// <summary>
        /// 配置user.Ini文件
        /// </summary>
        public class SerivceFiguration
        {
            /// <summary>
            /// 配置文件
            /// </summary>
            public const string INI_CFG = "config\\user.ini";
            /// <summary>
            ///
            /// </summary>
            public const string Save_Records = "data\\userSetting.ini";
            public const string SaveYc_Records = "data\\userYcSetting.ini";
            /// <summary>
            /// 获取本地样式参数
            /// </summary>
            /// <returns></returns>
            public static string GetSkin()
            {
                string cfgINI = AppDomain.CurrentDomain.BaseDirectory + INI_CFG;
                if (File.Exists(cfgINI))
                {
                    IniFiles ini = new IniFiles(cfgINI);
                    string SkinName = ini.IniReadValue("Skin", "Skin");
                    return SkinName;
                }
                else
                    return string.Empty;
            }
        }
    }
}