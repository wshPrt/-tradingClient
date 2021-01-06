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
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UnitManage;
using Xh.FastTrading.Wpf.Untils;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Xh.FastTrading.Wpf.Views.UnitManage
{
    /// <summary>
    /// ModifyTrategyUnitView.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyTrategyUnitView : Window
    {
        public static MidStrategyUnitManageModel MidStrategyUnitManage;
        public ModifyTrategyUnitView(Model.MidStrategyUnitManageModel midStrategyUnit)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            MidStrategyUnitManage = midStrategyUnit;
           // AccountPool();
            txtData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
        private void txtData() 
        {
            txtUnitCode.Text = MidStrategyUnitManage.UnitCode;
            txtUnitName.Text = MidStrategyUnitManage.UnitName;
            txtUnitRegion.Text = MidStrategyUnitManage.UnitRegion;
            txtUnitAgent.Text = MidStrategyUnitManage.UnitAgent;
            txtUnitRisk.Text = MidStrategyUnitManage.UnitRisk;
            txtAccount.Text = MidStrategyUnitManage.Account;
            cmbAccountPool.SelectedIndex = MidStrategyUnitManage.AccountPool.Value;
            txtUnitleverage.Text = MidStrategyUnitManage.Unitleverage.ToString();
            txtManageRatio.Text = MidStrategyUnitManage.ManageRatio.ToString();
            txtUnitSoftwareRate.Text = MidStrategyUnitManage.UnitSoftwareRate.ToString();
            txtCommissionRate.Text = MidStrategyUnitManage.CommissionRate.ToString();
            txtIndividualRestrictionStock.Text = MidStrategyUnitManage.IndividualRestrictionStock.ToString();
            txtProportionBoardStocks.Text = MidStrategyUnitManage.ProportionBoardStocks.ToString();
            txtProportionGemStocks.Text = MidStrategyUnitManage.ProportionGemStocks.ToString();
            txtTotalProportionGem.Text = MidStrategyUnitManage.TotalProportionGem.ToString();
            txtProportionSmallMediumBoardStocks.Text = MidStrategyUnitManage.ProportionSmallMediumBoardStocks.ToString();
            txtTotalProportionSmallMediumBoards.Text = MidStrategyUnitManage.TotalProportionSmallMediumBoards.ToString();
            txtMiddleSmallTotalProportion.Text = MidStrategyUnitManage.MiddleSmallTotalProportion.ToString();
            txtScienceInnovationBoardRatio.Text = MidStrategyUnitManage.ScienceInnovationBoardRatio.ToString();
            txtCordonRatio.Text = MidStrategyUnitManage.CommissionRate.ToString();
            txtLevelingLineRatio.Text = MidStrategyUnitManage.LevelingLineRatio.ToString();
            txtNoBuyingShares.Text = MidStrategyUnitManage.NoBuyingShares;
            cmbPriceLimit.Text = MidStrategyUnitManage.PriceLimit.ToString();
            
        }

        
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Modify();
        }

        /// <summary>
        ///  修改中期策略单元
        /// </summary>
        private void Modify()
        {
                    if (string.IsNullOrWhiteSpace(txtUnitCode.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("代码不能为空!");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtUnitName.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("名称不能为空!");

                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtUnitRegion.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("地区不为空!");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtUnitAgent.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("经纪人不为空!");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtUnitAgent.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("风控员不为空!");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtAccount.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("账号池不为空!");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtUnitleverage.Text))

                    {
                        MessageDialogManager.ShowDialogAsync("杠杆不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtManageRatio.Text)))
                    {
                        if (!(decimal.Parse(txtManageRatio.Text) >= 0 && decimal.Parse(txtManageRatio.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("管理费率为小于1的小数");
                            return;
                        }
                    }
                    if(string.IsNullOrWhiteSpace(txtManageRatio.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("管理费率不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtCommissionRate.Text)))
                    {
                        if (!(decimal.Parse(txtCommissionRate.Text) >= 0 && decimal.Parse(txtCommissionRate.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("佣金率为小于1的小数");
                            return;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(txtCommissionRate.Text))
                    {
                        MessageDialogManager.ShowDialogAsync("佣金率不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtIndividualRestrictionStock.Text )))
                    {
                        if (!(int.Parse(txtIndividualRestrictionStock.Text) % 1 == 0))
                        {
                            MessageDialogManager.ShowDialogAsync("股票个数限制为整数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("股票个人限制不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtProportionGemStocks.Text)))
                    {
                        if (!(decimal.Parse(txtProportionGemStocks.Text) >= 0 && decimal.Parse(txtProportionGemStocks.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板个股比例不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtTotalProportionGem.Text)))
                    {
                        if (!(decimal.Parse(txtTotalProportionGem.Text) >= 0 && decimal.Parse(txtTotalProportionGem.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板总比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板总比例不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtScienceInnovationBoardRatio.Text)))
                    {
                        if (!(decimal.Parse(txtScienceInnovationBoardRatio.Text) >= 0 && decimal.Parse(txtScienceInnovationBoardRatio.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("科创板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("科创板个股比例不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtTotalProportionSmallMediumBoards.Text)))
                    {
                        if (!(decimal.Parse(txtTotalProportionSmallMediumBoards.Text) >= 0 && decimal.Parse(txtTotalProportionSmallMediumBoards.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小板总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小板总比例不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtMiddleSmallTotalProportion.Text)))
                    {
                        if (!(decimal.Parse(txtMiddleSmallTotalProportion.Text) >= 0 && decimal.Parse(txtMiddleSmallTotalProportion.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小创总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小创总比例不为空!");
                        return;
                    }

                    if (!(string.IsNullOrWhiteSpace(txtLevelingLineRatio.Text )))
                    {
                        if (!(decimal.Parse(txtLevelingLineRatio.Text) >= 0 && decimal.Parse(txtLevelingLineRatio.Text) < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("平仓线比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("平仓线比例不为空!");
                        return;
                    }
                    string token = UserToken.token;
                    IModifyMidStrategyInterface modifyMidStrategy = new IModifyMidStrategyInterface();
                        var result = modifyMidStrategy.ModifyMidStrategy
                        (MidStrategyUnitManage.Id,
                        txtUnitCode.Text,
                        txtUnitName.Text,
                        txtUnitRegion.Text,
                        txtUnitAgent.Text,
                        txtUnitRisk.Text,
                        Convert.ToInt32(cmbAccountPool.Text),
                        Convert.ToDecimal(txtUnitleverage.Text),
                        Convert.ToDecimal(txtCommissionRate.Text),
                        Convert.ToDecimal(txtManageRatio.Text), 
                        Convert.ToDecimal(txtUnitSoftwareRate.Text),
                        Convert.ToInt32(txtIndividualRestrictionStock.Text),
                        Convert.ToDecimal(txtProportionBoardStocks.Text),
                        Convert.ToDecimal(txtProportionGemStocks.Text),
                        Convert.ToDecimal(txtTotalProportionGem.Text),
                        Convert.ToDecimal(txtProportionSmallMediumBoardStocks.Text),
                        Convert.ToDecimal(txtTotalProportionSmallMediumBoards.Text),
                        Convert.ToDecimal(txtMiddleSmallTotalProportion.Text),
                        Convert.ToDecimal(txtScienceInnovationBoardRatio.Text),
                        Convert.ToDecimal(txtTotalProportionSmallMediumBoards.Text),
                        Convert.ToDecimal(txtCordonRatio.Text),
                        Convert.ToDecimal(txtLevelingLineRatio.Text),
                        Convert.ToString(txtNoBuyingShares.Text),
                        0, token);

                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("修改单元成功!");
                        this.Close();
                        return;
                    }
                    else
                    {


                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }

    }
}
