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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// SellDealView.xaml 的交互逻辑
    /// </summary>
    public partial class SellDealView : UserControl
    {
        public SellDealView()
        {
            InitializeComponent();
        }


        //private void SellInfo_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Thread thread = new Thread(new ThreadStart(Run));
        //    thread.IsBackground = true;
        //    thread.Start();
        //}
       

        //public void Run()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        this.Dispatcher.BeginInvoke((Action)delegate ()
        //        {
        //            string passedSns = i + Environment.NewLine + this.txtSell5.Text;
        //            this.txtSell5.Text = passedSns;
        //        });

        //        //给界面更新TextBox的时间
        //        Thread.Sleep(2000);
        //    }
        //}

       
    }
}
