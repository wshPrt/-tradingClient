using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xh.FastTrading.Wpf.Common.Commands
{
    public class DelegateCommandStart : ICommand
    {
        private Action<object> executeAction;
        private Func<object, bool> canExecuteFunc;
        public event EventHandler CanExecuteChanged;

        public DelegateCommandStart(Action<object> execute)
            : this(execute, null)
        { }

        public DelegateCommandStart(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                return;
            }
            executeAction = execute;
            canExecuteFunc = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteFunc == null)
            {
                return true;
            }
            return canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            if (executeAction == null)
            {
                return;
            }
            executeAction(parameter);
        }
    }

    // DelegateCommand的泛型版本
    public class DelegateCommandStart<T> : ICommand
    {
        private Action<T> executeAction;
        private Func<T, bool> canExecuteFunc;
        public event EventHandler CanExecuteChanged;

        public DelegateCommandStart(Action<T> execute)
            : this(execute, null)
        { }

        public DelegateCommandStart(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                return;
            }
            executeAction = execute;
            canExecuteFunc = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteFunc == null)
            {
                return true;
            }
            return canExecuteFunc((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (executeAction == null)
            {
                return;
            }
            executeAction((T)parameter);
        }
    }
}
