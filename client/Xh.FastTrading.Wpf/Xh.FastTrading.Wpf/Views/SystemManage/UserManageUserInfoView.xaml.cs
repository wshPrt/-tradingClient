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
using Xh.FastTrading.Wpf.ViewModel;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UserManageUserInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageUserInfoView : Window
    {
        public UserManageUserInfoView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            txtLogin.Text = "";
            txtUser.Text = "";
            txtPhone.Text = "";
            txtPwd2.Text = "";
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }       
    }
}
