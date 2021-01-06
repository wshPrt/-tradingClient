using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xh.FastTrading.Wpf.Common.Commands
{
    public static class CommandTool
    {
        static WindowState _windowState;
        public static WindowState windowState
        {
            get
            {
                return _windowState;
            }
            set
            {
                _windowState=value;
            }
        }
    }
}
