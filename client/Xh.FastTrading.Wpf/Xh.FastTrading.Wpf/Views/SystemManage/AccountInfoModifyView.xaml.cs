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
using System.Windows.Shapes;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// MasterAccountInfoAddView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountInfoModifyView : Window
    {
        public AccountInfoModifyView()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            InitializeComponent();
          //  CleanVaule();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //public void CleanVaule()
        //{
        //    //txtCode.Text = string.Empty;
        //    //txtName.Text = " ";
        //    //txtSystemAccount.Text = " ";
        //    //txtRemark.Text = " ";
        //    //txtRobot.Text = " ";
        //    txtCommissionRate.Text = " ";
        //    txtStatus.Text = "";
        //    txtInitialCapital.Text = "";
        //    txtBadCapital.Text = "";
        //    txtFirstCapital.Text = "";
        //    txtRaoBiaoCapita.Text = "";
        //    txtRaoBiaoRates.Text = "";
        //    txtAssetWarningLine.Text = "";
        //    txtSingleTicketLimit.Text = "";
        //    txtGemSingleTicketLimit.Text = "";
        //    txtGemLimit.Text = "";
        //    txtLimitBuyShare.Text = "";
        //}
    }
}
