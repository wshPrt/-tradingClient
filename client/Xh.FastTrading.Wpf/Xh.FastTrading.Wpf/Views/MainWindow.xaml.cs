using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Xh.FastTrading.Wpf.Commands.IniFile;
using Xh.FastTrading.Wpf.Common.Commands;
using Xh.FastTrading.Wpf.Common.CommonHelper;
using Xh.FastTrading.Wpf.Converters;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.ViewModel;
using Xh.FastTrading.Wpf.Views.SystemManage;
using static Xh.FastTrading.Wpf.Common.Commands.IniFile.Configuration;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        BoModel model = new BoModel();
        System.Windows.Forms.NotifyIcon notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var mainMenu = new[] {
                KeyBindBoMenu(new BoMenu{Header = "买入 (F1)", Name="BUY",IsVisible = MenuVisible(100), Icon= @"/Sup-Trade;component/Common/Images/buyF1.png", Model = new BuyModel() {  } }, Key.F1),
                KeyBindBoMenu(new BoMenu{ Header = "卖出 (F2)", Name="SELL",IsVisible = MenuVisible(100), Icon= @"/Sup-Trade;component/Common/Images/sell.png",Model = new SellModel() { Title = "卖出" } }, Key.F2),
                //new BoMenu{ Header = "国债逆回购",Name="BACK",Model = new BackModel() { Title = "回购" } },
                new BoMenu{ Header = "自动交易",Name="AUTOMATIC",IsVisible = MenuVisible(200), Icon= @"/Sup-Trade;component/Common/Images/auto.png",Model = new AutoMaticModel() { Title = "自动交易" } },
                new BoMenu{ Header = "指定交易", Name="SPECIFY",IsVisible = MenuVisible(300), Icon= @"/Sup-Trade;component/Common/Images/appoint.png",Model = new SpecifyModel() { Title = "指定交易" } },
                KeyBindBoMenu(new BoMenu{ Header = "撤单 (F3)",Icon= @"/Sup-Trade;component/Common/Images/cancel.png",Name="CANCEL(F3)",Model = new CancelModel() { Title = "撤单 (F3)" } },Key.F3),
                //new BoMenu{
                //    Header = "风控管理",Icon= @"/Sup-Trade;component/Common/Images/risk.png",
                //    Children=new[]{
                //        new BoMenu { Header= "中期策略",Name="RISKSTRATEGY",Icon= @"/Sup-Trade;component/Common/Images/strategy.png",Model = new RiskStrategyModel() { Title = "中期策略" }}
                //    }
                //},
                new BoMenu{
                    Header = "查询",Icon= @"/Sup-Trade;component/Common/Images/query.png",
                    IsVisible = MenuVisible(400),
                    Children=new[]{
                        KeyBindBoMenu(new BoMenu { Header= "资产 (F4)",Name="ASSETS",Icon= @"/Sup-Trade;component/Common/Images/assets.png",Model = new AssetsModel() { Title = "资产 (F4)" }},Key.F4),
                        new BoMenu { Header= "委托记录",Name="ENTRUST",Icon= @"/Sup-Trade;component/Common/Images/entrust.png",Model = new EntrustModel() { Title = "委托记录" }},
                        new BoMenu { Header= "成交记录",Name="DEAL" ,Icon= @"/Sup-Trade;component/Common/Images/deal.png",Model = new DealModel() { Title = "成交记录" }},
                        new BoMenu { Header= "资金流水",Name="CAPITAL",Icon= @"/Sup-Trade;component/Common/Images/capital.png",Model = new CapitalModel() { Title = "资金流水" }}
                    }
                },
                new BoMenu{
                    Header = "单元管理",Icon= @"/Sup-Trade;component/Common/Images/auto.png",
                    IsVisible = MenuVisible(500),
                    Children=new[]{
                        new BoMenu { Header= "中期策略",Name="UNITTRATEGY",Icon= @"/Sup-Trade;component/Common/Images/strategy.png",Model = new UnitTrategyModel() { Title = "中期策略" }},
                        //new BoMenu { Header= "交易限制规则",Name="RULE",Icon= @"/Sup-Trade;component/Common/Images/limit.png",Model = new RuleModel() { Title = "交易限制规则" }},
                        new BoMenu { Header= "持仓汇总",Name="POSITION",Icon= @"/Sup-Trade;component/Common/Images/position.png",Model = new PositionModel() { Title = "持仓汇总" }},
                        new BoMenu { Header= "委托汇总" ,Name="ENTRUSTSUMMARY",Icon= @"/Sup-Trade;component/Common/Images/commission.png",Model = new EntrustSummaryModel() { Title = "委托汇总" }},
                        new BoMenu { Header= "异常委托",Name="ABNORMALENTRUST",Icon= @"/Sup-Trade;component/Common/Images/scrap.png",Model = new AbnormalEntrustModel() { Title = "异常委托" }},
                        //new BoMenu { Header= "异常撤单",Name="ABNORMALCANCEL",Icon= @"/Sup-Trade;component/Common/Images/abnormalCancel.png",Model = new AbnormalCancelModel() { Title = "异常撤单" }},
                        new BoMenu { Header= "废单汇总",Name="VOID",Icon= @"/Sup-Trade;component/Common/Images/abnormal.png",Model = new VoidModel() { Title = "废单汇总" }},
                        new BoMenu { Header= "系统内成交汇总",Name="SYSTEMDEAL",Icon= @"/Sup-Trade;component/Common/Images/sysOutDeal.png",Model = new SystemDealModel() { Title = "系统内成交汇总" }},
                        new BoMenu { Header= "系统外成交汇总",Name="SYSTEMDEALOUT",Icon= @"/Sup-Trade;component/Common/Images/abnormal.png",Model = new SystemDealOutModel() { Title = "系统外成交汇总" }},
                        //new BoMenu { Header= "禁买股票和例外设置",Name="BAN",Icon= @"/Sup-Trade;component/Common/Images/prohibitedStock.png",Model = new BanModel() { Title = "禁买股票和例外设置" }},
                        //new BoMenu { Header= "自动平仓设置",Name="LIQUIDATION",Icon= @"/Sup-Trade;component/Common/Images/automatic.png",Model = new LiquidationModel() { Title = "自动平仓设置" }}
                    }
                },
                new BoMenu{
                    Header = "系统管理",Icon= @"/Sup-Trade;component/Common/Images/settingItem.png",
                    IsVisible = MenuVisible(700),
                    Children=new[]{
                        new BoMenu { Header = "用户管理",Name="USERMANAGE",Icon= @"/Sup-Trade;component/Common/Images/user.png",Model = new UserManagemodel() { Title = "用户管理" }},
                        new BoMenu { Header= "开盘收盘",Name="OPENCLOSE",Icon= @"/Sup-Trade;component/Common/Images/openClose.png",Model = new OpenCloseModel() { Title = "开盘收盘" }},
                        //new BoMenu { Header= "分红送股",Name="DIVIDENDS",Icon= @"/Sup-Trade;component/Common/Images/bonus.png",Model = new DividendsModel() { Title = "分红送股" }},
                        new BoMenu { Header= "持仓划转",Name="TRANSFER",Icon= @"/Sup-Trade;component/Common/Images/positionPlan.png",Model = new TransferModel() { Title = "持仓划转" }},
                        new BoMenu { Header= "主账户池",Name="ACCOUNT",Icon= @"/Sup-Trade;component/Common/Images/accountPool.png",Model = new AccountModel() { Title = "主账户池" }},
                        new BoMenu { Header= "主账户管理",Name="ACCOUNTMANAGE",Icon= @"/Sup-Trade;component/Common/Images/accountManage.png",Model = new AccountManageModel() { Title = "主账户管理" }},
                        new BoMenu { Header= "主账户持仓",Name="ACCOUNTPOSITION",Icon= @"/Sup-Trade;component/Common/Images/accountPosition.png",Model = new AccountPositionModel() { Title = "主账户持仓"}}
                    }
                }
            };

            model.MainMenu = mainMenu;
            this.DataContext = model;

        }

        private bool MenuVisible(int module)
        {
            return UserToken.role == 99 || UserToken.authority_modules.Contains(module);
        }

        public BoMenu KeyBindBoMenu(BoMenu boMenu, Key key)
        {
            this.InputBindings.Add(new KeyBinding() { Key = key, Command = new UserCommand<BoMenu>(ExecuteBoMenu), CommandParameter = boMenu });
            return boMenu;
        }

        public void TreeView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            if (treeView.SelectedItem != null)
            {
                BoMenu boMenu = treeView.SelectedItem as BoMenu;
                if (boMenu.Children == null || boMenu.Children.Length == 0)
                {
                    ExecuteBoMenu(boMenu);
                }
            }
        }

        private void ExecuteBoMenu(BoMenu boMenu)
        {
            model.Model = boMenu.Model;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ModifyPasswordView modifyPwd = new ModifyPasswordView();
            modifyPwd.ShowDialog();
        }

        private void txtLock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UnlockView unlock = new UnlockView();
            unlock.ShowDialog();
        }

        private void txtSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SystemSettingView sysSetting = new SystemSettingView();
            sysSetting.ShowDialog();
        }
        private void SaveLoginInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            IniFiles ini = new IniFiles(cfgINI);
            ini.IniWriteValue("Login", "User", "");
            ini.IniWriteValue("Login", "Password", "");
            ini.IniWriteValue("Login", "SaveInfo", "N");
        }

        private void btnExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignInView signIn = new SignInView();
            // SaveLoginInfo();
            this.Close();
            signIn.ShowDialog();
        }

        private void txtLogOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignInView signin = new SignInView();
            this.Close();
            SignInViewModel signInVM = new SignInViewModel();
            signInVM.LogOff();
            signin.ShowDialog();


        }


    }

    public class UserCommand<T> : ICommand
    {
        public Func<T, bool> CanExecute { get; set; }
        public Action<T> Execute { get; set; }

        public UserCommand(Action<T> Execute, Func<T, bool> CanExecute = null)
        {
            this.Execute = Execute;
            this.CanExecute = CanExecute;
        }


        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if (this.CanExecute == null)
            {
                return true;
            }
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }
    }

    public class BoModel : INotifyPropertyChanged
    {
        public BoMenu[] MainMenu { get; set; }

        private object _Model;
        public object Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class BoMenu
    {
        public string Name { get; set; }
        public string Header { get; set; }

        public BoMenu[] Children { get; set; }
        public object Model { get; set; }
        public string Icon { get; set; }
        public bool IsVisible { get; set; } = true;
    }

    public class BuyModel
    {
        public string Title { get; set; }
    }

    public class SellModel
    {
        public string Title { get; set; }
    }
    public class BackModel
    {
        public string Title { get; set; }
    }
    public class AutoMaticModel
    {
        public string Title { get; set; }
    }
    public class SpecifyModel
    {
        public string Title { get; set; }
    }
    public class CancelModel
    {
        public string Title { get; set; }
    }
    public class RiskStrategyModel
    {
        public string Title { get; set; }
    }
    public class AssetsModel
    {
        public string Title { get; set; }
    }
    public class EntrustModel
    {
        public string Title { get; set; }
    }
    public class DealModel
    {
        public string Title { get; set; }
    }
    public class CapitalModel
    {
        public string Title { get; set; }
    }
    public class UnitTrategyModel
    {
        public string Title { get; set; }
    }
    public class RuleModel
    {
        public string Title { get; set; }
    }
    public class PositionModel
    {
        public string Title { get; set; }
    }
    public class AbnormalEntrustModel
    {
        public string Title { get; set; }
    }
    public class EntrustSummaryModel
    {
        public string Title { get; set; }
    }

    public class AbnormalCancelModel
    {
        public string Title { get; set; }
    }
    public class VoidModel
    {
        public string Title { get; set; }
    }
    public class SystemDealModel
    {
        public string Title { get; set; }
    }
    public class SystemDealOutModel
    {
        public string Title { get; set; }
    }
    public class BanModel
    {
        public string Title { get; set; }
    }
    public class LiquidationModel
    {
        public string Title { get; set; }
    }
    public class UserManagemodel
    {
        public string Title { get; set; }
    }
    public class OpenCloseModel
    {
        public string Title { get; set; }
    }
    public class DividendsModel
    {
        public string Title { get; set; }
    }
    public class TransferModel
    {
        public string Title { get; set; }
    }
    public class AccountModel
    {
        public string Title { get; set; }
    }
    public class AccountManageModel
    {
        public string Title { get; set; }
    }
    public class AccountPositionModel
    {
        public string Title { get; set; }
    }

}