using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xh.FastTrading.Wpf.Commands
{
   public class WindowManager
    {
        private static Hashtable _RegisterWindow = new Hashtable();

        //注册窗口
        public static void Regiter<T>(string key)
        {
            _RegisterWindow.Add(key, typeof(T));
        }

        //注册窗口
        public static void Regiter(string key, Type t)
        {
            if (!_RegisterWindow.ContainsKey(key))
                _RegisterWindow.Add(key, t);
        }

        //移除窗口
        public static void Remove(string key)
        {
            if (_RegisterWindow.ContainsKey(key))
                _RegisterWindow.Remove(key);
        }

        //打开窗口
        public static void ShowDialog(string key, object VM)
        {
            if (!_RegisterWindow.ContainsKey(key))
            {
                throw (new Exception("没有注册此键！"));
            }

            var win = (Window)Activator.CreateInstance((Type)_RegisterWindow[key]);
            win.DataContext = VM;
            win.ShowDialog();
        }

        //打开窗口
        public static void ShowDialog(string key, object model, Window window)
        {
            if (!_RegisterWindow.ContainsKey(key))
            {
                throw (new Exception("没有注册此键！"));
            }

            var win = (Window)Activator.CreateInstance((Type)_RegisterWindow[key]);
            win.DataContext = model;
            win.Owner = window;
            win.ShowDialog();
        }

    }
}
