using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xh.FastTrading.Wpf.Commands
{
   public static class WindowExtension
    {
        public static void Register(this Window win, string key)
        {
            WindowManager.Regiter(key, win.GetType());
        }

        public static void Register(this Window win, string key, Type t)
        {
            WindowManager.Regiter(key, t);
        }

        public static void Register<T>(this Window win, string key)
        {
            WindowManager.Regiter<T>(key);
        }
    }
}
