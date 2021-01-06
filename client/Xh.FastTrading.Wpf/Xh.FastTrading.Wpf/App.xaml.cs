using Lierda.WPFHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.ViewModel;
using Xh.FastTrading.Wpf.Views;

namespace Xh.FastTrading.Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        LierdaCracker cracker = new LierdaCracker();
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (!File.Exists("Uninstall.exe"))
                    Environment.Exit(Environment.ExitCode);

                cracker.Cracker();
                base.OnStartup(e);
                SignInViewModel singInVM = new SignInViewModel();
                singInVM.Version();

                MainWindow mv = new MainWindow();                
                singInVM.ReadConfigInfo();//读写配置参数
                SignInView login = new SignInView();
                var a = login.ShowDialog();
                if (a.HasValue && a.Value)
                {
                    mv.Init();
                    mv.ShowDialog();                    
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
