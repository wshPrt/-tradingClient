using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Untils;
using static Xh.FastTrading.Wpf.Model.DealAutoMaticModel;

namespace Xh.FastTrading.Wpf.Views.Deal.Automatic
{
    /// <summary>
    /// ModifyEntrustInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyEntrustInfoView : Window
    {
        public static DealAutoMaticModel DealAutoMaticModel;
        public ModifyEntrustInfoView(Model.DealAutoMaticModel DealAutoMatic)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;

            DealAutoMaticModel = DealAutoMatic;
            txtData();

            BuySellType = new ObservableCollection<BuySellTypModel>()
            {
                new BuySellTypModel(){Id=0,Name="买入"},
                new BuySellTypModel(){Id=1,Name="卖出"}
            };
            PriceType = new ObservableCollection<PriceTypeModel>()
            {
                new PriceTypeModel(){PriceId=0,PriceName="最新价"},
                new PriceTypeModel(){PriceId=1,PriceName="买一价"},
                new PriceTypeModel(){PriceId=2,PriceName="买二价"},
                new PriceTypeModel(){PriceId=3,PriceName="买三价"},
                new PriceTypeModel(){PriceId=4,PriceName="买四价"},
                new PriceTypeModel(){PriceId=5,PriceName="买五价"},

                new PriceTypeModel(){PriceId=-1,PriceName="卖一价"},
                new PriceTypeModel(){PriceId=-2,PriceName="卖二价"},
                new PriceTypeModel(){PriceId=-3,PriceName="卖三价"},
                new PriceTypeModel(){PriceId=-4,PriceName="卖四价"},
                new PriceTypeModel(){PriceId=-5,PriceName="卖五价"}
            };
        }


        #region 买卖方向cmbox
        private ObservableCollection<DealAutoMaticModel.BuySellTypModel> _buySellType;
        public ObservableCollection<DealAutoMaticModel.BuySellTypModel> BuySellType
        {
            get { return _buySellType; }
            set { _buySellType = value; }
        }

        private DealAutoMaticModel.BuySellTypModel _sbuySellType;
        public DealAutoMaticModel.BuySellTypModel SbuySellType
        {
            get { return _sbuySellType; }
            set { _sbuySellType = value; }
        }
        #endregion

        #region 价格类型cmbox
        private ObservableCollection<DealAutoMaticModel.PriceTypeModel> _priceType;
        public ObservableCollection<DealAutoMaticModel.PriceTypeModel> PriceType
        {
            get { return _priceType; }
            set { _priceType = value; }
        }

        private DealAutoMaticModel.PriceTypeModel _spriceType;
        public DealAutoMaticModel.PriceTypeModel SPriceType
        {
            get { return _spriceType; }
            set { _spriceType = value; }
        }
        #endregion
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtData()
        {
            txtSecuritiesCode.Text = DealAutoMaticModel.SecuritiesCode;
            cmbDirection.SelectedIndex = int.Parse(DealAutoMaticModel.BuySellDirection.ToString());
            txtMinSpace.Text = DealAutoMaticModel.MinInterval.ToString();
            txtMaxSpace.Text = DealAutoMaticModel.MaxInterval.ToString();
            txtMinNumber.Text = DealAutoMaticModel.MinAmount.ToString();
            txtMaxNumber.Text = DealAutoMaticModel.MaxAmount.ToString();
           
            cmbType.SelectedIndex = int.Parse(DealAutoMaticModel.PriceType.ToString());
            txtLowPrice.Text = DealAutoMaticModel.MinPrice.ToString();
            txtHighPrice.Text = DealAutoMaticModel.HighestPrice.ToString();
            //            txtAccount.Text = DealAutoMaticModel.Account.ToString()??0;
            txtAccount.Text = "";
            txtNumberlimit.Text = DealAutoMaticModel.AmountLimit.ToString();
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecuritiesCode.Text))
            {
                MessageDialogManager.ShowDialogAsync("证券代码为空!");
                return;
            }
            if (!(txtSecuritiesCode.Text.Length == 6))
            {
                MessageDialogManager.ShowDialogAsync("请输入6位证券代码!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtMinSpace.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最小间隔为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtMaxSpace.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最大间隔为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtMinNumber.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最小数量为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtMaxNumber.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最大数量为空!");
                return; 
            }
            if (cmbDirection.SelectedItem == null)
            {
                MessageDialogManager.ShowDialogAsync("买入方向未选!");
                return;
            }
            if (cmbType.SelectedItem == null)
            {
                MessageDialogManager.ShowDialogAsync("价格类型未选!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtLowPrice.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最低限价为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace((txtHighPrice.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("最高限价为空!");
                return;
            }
            //if (string.IsNullOrWhiteSpace(txtAccount.Text))
            //{
            //    MessageDialogManager.ShowDialogAsync("主账号为空!");
            //    return;
            //}
            if (string.IsNullOrWhiteSpace((txtNumberlimit.Text).ToString()))
            {
                MessageDialogManager.ShowDialogAsync("数量限制为空!");
                return;
            }
            if (!(int.Parse(txtMinSpace.Text) <= int.Parse(txtMaxSpace.Text)))
            {
                MessageDialogManager.ShowDialogAsync("最小间隔大于最大间隔!");
                return;
            }
            if (!(int.Parse(txtMinNumber.Text) <= int.Parse(txtMaxNumber.Text)))
            {
                MessageDialogManager.ShowDialogAsync("最小数量大于最大数量!");
                return;
            }
            if (!(decimal.Parse(txtLowPrice.Text) <= decimal.Parse(txtHighPrice.Text)))
            {
                MessageDialogManager.ShowDialogAsync("最低限价大于最高限价!");
                return;
            }
                    string token = UserToken.token;
                    IAutomaticModifyInterface automaticModify = new IAutomaticModifyInterface();
                    var result = automaticModify.AutomaticModify
                    (DealAutoMaticModel.Id,decimal.Parse(txtLowPrice.Text), decimal.Parse(txtHighPrice.Text),
                    cmbDirection.SelectedIndex,int.Parse(txtMinNumber.Text), int.Parse(txtMaxNumber.Text),int.Parse(txtMinSpace.Text),
                    int.Parse(txtMaxSpace.Text),token);
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("选中委托计划修改成功！");
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
