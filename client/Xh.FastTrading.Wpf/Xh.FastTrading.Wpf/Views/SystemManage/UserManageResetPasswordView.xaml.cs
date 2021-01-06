using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UserManageResetPasswordView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageResetPasswordView : Window
    {
        public UserManageResetPasswordView()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            InitializeComponent();

            txtPassword.Text = "";
            txtPwd.Text = "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
