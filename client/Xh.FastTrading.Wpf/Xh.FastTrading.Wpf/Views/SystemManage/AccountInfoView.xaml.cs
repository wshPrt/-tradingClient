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
    /// AccountInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountInfoView : Window
    {
        public AccountInfoView()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            InitializeComponent();
            CleanVaule();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CleanVaule() 
        {
             txtIP.Text = string.Empty;
            textCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSystemAccount.Text = null;
            txtRemark.Text = null;
            txtRobot.Text = null;
            txtCommissionRate.Text = " ";
            txtStatus.Text = "";
            txtInitialCapital.Text = "";
            txtBadCapital.Text = "";
            txtFirstCapital.Text = "";
            txtRaoBiaoCapita.Text = "";
            txtRaoBiaoRates.Text = "";
            txtAssetWarningLine.Text = "";
            txtSingleTicketLimit.Text = "";
            txtGemSingleTicketLimit.Text = "";
            txtGemLimit.Text = "";
            txtLimitBuyShare.Text = "";
        }

       
    }
}
