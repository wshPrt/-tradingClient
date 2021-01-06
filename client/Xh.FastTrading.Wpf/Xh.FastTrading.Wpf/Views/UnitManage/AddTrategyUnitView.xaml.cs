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
using Xh.FastTrading.Wpf.Commands;

namespace Xh.FastTrading.Wpf.Views.UnitManage
{
    /// <summary> 
    /// AddTrategyUnitView.xaml 的交互逻辑
    /// </summary>
    public partial class AddTrategyUnitView : Window
    {
        public AddTrategyUnitView()
        {
            InitializeComponent();
            Clean();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }

        private void Clean() 
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtRegion.Text = "";
            txtAgent.Text = "";
            txtRisk.Text = "";
            txtAccount.Text = "";
            txtPool.Text = "";
            txtleve.Text = "";
            txtRatio.Text = "";
            txtSoftwareRate.Text = "";
            txtCommissionRate.Text = "";
            txtRestrictionStock.Text = "";
            txtProportionBoardStocks.Text = "";
            txtProportionGemStocks.Text = "";
            txtTotalProportionGem.Text = "";
            txtScienceInnovationBoardRatio.Text = "";
            txtTotalProportionSmallMediumBoards.Text = "";
            txtMiddleSmallTotalProportion.Text = "";
            txtScienceInnovationTotalRatio.Text = "";
            txtCordonRatio.Text = "";
            txtLevelingLineRatio.Text = "";
            txtIndividualRestrictionStock.Text = "";
            txtFreezingRatio.SelectedIndex = 0;
            txtuser.Text = "";
            txtContractNumber.Text = "";

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
