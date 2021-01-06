using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace;
using Xh.FastTrading.Wpf.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Common.Commands;
using Xh.FastTrading.Wpf.Common.Untils;
using System.IO;
using static Xh.FastTrading.Wpf.Common.Commands.IniFile.Configuration;
using Xh.FastTrading.Wpf.Commands.IniFile;
using Xh.FastTrading.Wpf.Commands.MD5Poxy;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// SignInView.xaml 的交互逻辑
    /// </summary>
    public partial class SignInView : Window
    {
        public SignInView()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFiles ini = new IniFiles(cfgINI);
                txtUser.Text = ini.IniReadValue("Login", "User");
                txtPassWord.Password = CEncoder.Decode(ini.IniReadValue("Login", "Password"));
                chk.IsChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";

            }
        }

        private void txtPassWord_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SignInViewModel signinVM = new SignInViewModel();
            signinVM.Login(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
