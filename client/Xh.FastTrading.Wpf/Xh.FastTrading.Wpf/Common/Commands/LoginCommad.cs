using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xh.FastTrading.Wpf.ViewModel;

namespace Xh.FastTrading.Wpf.Commands
{
    class LoginCommad : ICommand
    {
        public SignInViewModel userViewModel;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            //userViewModel.QueryData();
        }
        public LoginCommad(SignInViewModel userViewModel)
        {
            this.userViewModel = userViewModel;
        }
    }
}
