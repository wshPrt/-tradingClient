using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xh.FastTrading.Wpf.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action esCanSelectList_MouseDoubleClick;

        public DelegateCommand(Action esCanSelectList_MouseDoubleClick)
        {
            this.esCanSelectList_MouseDoubleClick = esCanSelectList_MouseDoubleClick;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteFunc != null)
            {
                return CanExecuteFunc(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteAction != null)
            {
                ExecuteAction(parameter);
            }
        }
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }
    }

    //public class DelegateCommand : ICommand
    //{
    //    public Action<object> ExecuteCommand = null;
    //    public Func<object, bool> CanExecuteCommand = null;

    //    public DelegateCommand(Action<object> executeCommand)
    //    {
    //        this.ExecuteCommand = executeCommand;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        if (CanExecuteCommand != null)
    //        {
    //            return this.CanExecuteCommand(parameter);
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        this.ExecuteCommand?.Invoke(parameter);
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    //    }
    //}

}
