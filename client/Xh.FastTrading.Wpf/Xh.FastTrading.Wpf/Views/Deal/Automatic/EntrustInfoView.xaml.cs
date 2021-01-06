using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using Xh.FastTrading.Wpf.ViewModel.Deal;
using static Xh.FastTrading.Wpf.Model.DealAutoMaticModel;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// EntrustInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class EntrustInfoView : Window
    {
        public EntrustInfoView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            txtClean();

        }

        private void txtClean()
        {
            txtSecuritiesCode.Text = "";
            cmbBuySell.SelectedIndex = 0;
            txtMinSpace.Text = "";
            txtMaxSpace.Text = "";
            txtMinNumber.Text = "";
            txtMaxNumber.Text = "";
            cmbType.SelectedIndex = 0;
            txtLowPrice.Text = "";
            txtHighPrice.Text = "";
            txtAccount.Text = "";
            txtNumberlimit.Text = "";
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
