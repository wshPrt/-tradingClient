using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Xh.FastTrading.Wpf.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
        public ICommand ChangeTitleCommand { get; set; }
        BoModel model = new BoModel();
        public MainViewModel()
        {
            //Title = "UTOP投资理财系统3.0(易 富 版) ";
            ChangeTitleCommand = new RelayCommand(ChangeTitle);

            var mainMenu = new[] {
                KeyBindBoMenu(new BoMenu{ Header = "买入 (F1)", Name="BUY", Model = new BuyModel() { Title = "买入" } }, Key.F1),
                KeyBindBoMenu(new BoMenu{ Header = "卖出 (F2)", Name="SELL",Model = new SellModel() { Title = "卖出" } }, Key.F2),
                new BoMenu{ Header = "国债逆回购",Name="BACK",Model = new BackModel() { Title = "回购" } },
                new BoMenu{ Header = "自动交易",Name="AUTOMATIC",Model = new AutoMaticModel() { Title = "自动交易" } },
                new BoMenu{ Header = "指定交易",Name="SPECIFY",Model = new SpecifyModel() { Title = "指定交易" } },
                KeyBindBoMenu(new BoMenu{ Header = "撤单 (F3)",Name="CANCEL(F3)",Model = new CancelModel() { Title = "撤单 (F3)" } },Key.F3),
                new BoMenu{
                    Header = "风控管理",
                    Children=new[]{
                        new BoMenu { Header= "中期策略",Name="RISKSTRATEGY",Model = new RiskStrategyModel() { Title = "中期策略" }}
                    }
                },
                new BoMenu{
                    Header = "查询",
                    Children=new[]{
                        KeyBindBoMenu(new BoMenu { Header= "资产 (F4)",Name="ASSETS",Model = new AssetsModel() { Title = "资产 (F4)" }},Key.F4),
                        new BoMenu { Header= "委托记录",Name="ENTRUST",Model = new EntrustModel() { Title = "委托记录" }},
                        new BoMenu { Header= "成交记录",Name="DEAL" ,Model = new DealModel() { Title = "成交记录" }},
                        new BoMenu { Header= "资金记录",Name="CAPITAL",Model = new CapitalModel() { Title = "资金流水" }}
                    }
                },
                 new BoMenu{
                    Header = "单元管理",
                    Children=new[]{
                        new BoMenu { Header= "中期策略",Name="UNITTRATEGY",Model = new UnitTrategyModel() { Title = "中期策略" }},
                        new BoMenu { Header= "交易限制规则",Name="RULE",Model = new RuleModel() { Title = "交易限制规则" }},
                        new BoMenu { Header= "持仓汇总",Name="POSITION",Model = new PositionModel() { Title = "持仓汇总" }},
                        new BoMenu { Header= "委托汇总" ,Name="ENTRUSTSUMMARY",Model = new EntrustSummaryModel() { Title = "委托汇总" }},
                        new BoMenu { Header= "异常委托",Name="ABNORMALENTRUST",Model = new AbnormalEntrustModel() { Title = "异常委托" }},
                        new BoMenu { Header= "异常撤单",Name="ABNORMALCANCEL",Model = new AbnormalCancelModel() { Title = "异常撤单" }},
                        new BoMenu { Header= "废单汇总",Name="VOID",Model = new VoidModel() { Title = "废单汇总" }},
                        new BoMenu { Header= "系统内成交汇总",Name="SYSTEMDEAL",Model = new SystemDealModel() { Title = "系统内成交汇总" }},
                        new BoMenu { Header= "系统外成交汇总",Name="SYSTEMDEALOUT",Model = new SystemDealOutModel() { Title = "系统外成交汇总" }},
                        new BoMenu { Header= "禁买股票和例外设置",Name="BAN",Model = new BanModel() { Title = "禁买股票和例外设置" }},
                        new BoMenu { Header= "自动平仓设置",Name="LIQUIDATION",Model = new LiquidationModel() { Title = "自动平仓设置" }}
                    }
                },
                  new BoMenu{
                    Header = "系统管理",
                    Children=new[]{
                        new BoMenu { Header= "用户管理",Name="USERMANAGE",Model = new UserManagemodel() { Title = "用户管理" }},
                        new BoMenu { Header= "开盘收盘",Name="OPENCLOSE",Model = new OpenCloseModel() { Title = "开盘收盘" }},
                        new BoMenu { Header= "分红送股",Name="DIVIDENDS",Model = new DividendsModel() { Title = "分红送股" }},
                        new BoMenu { Header= "持仓划转",Name="TRANSFER",Model = new TransferModel() { Title = "持仓划转" }},
                        new BoMenu { Header= "主账户池",Name="ACCOUNT",Model = new AccountModel() { Title = "主账户池" }},
                        new BoMenu { Header= "主账户管理",Name="ACCOUNTMANAGE",Model = new AccountManageModel() { Title = "主账户管理" }},
                        new BoMenu { Header= "主账户持仓",Name="ACCOUNTPOSITION",Model = new AccountPositionModel() { Title = "主账户持仓"}}
                    }
                }
            };
            model.MainMenu = mainMenu;
        }
        private void ChangeTitle()
        {
           // Title = "UTOP投资理财系统3.0(易 富 版)";
        }
        private RelayCommand mouseUpCommand;
        public RelayCommand MouseUpCommand
        {
            get
            {
                if (mouseUpCommand == null)
                {
                    mouseUpCommand = new RelayCommand(() => MouseUp());
                }
                return mouseUpCommand;
            }
            set { mouseUpCommand = value; }
        }
        [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(TreeViewItem))]
        public class TreeView : ItemsControl
        {
            [Bindable(true)]
            [Category("Appearance")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            [ReadOnly(true)]
            public object SelectedItem { get; }
        }
        private void MouseUp()
        {
            TreeView treeView = new TreeView();
            if (!(treeView.SelectedItem == null))
            {
                BoMenu boMenu = treeView.SelectedItem as BoMenu;
                if (boMenu.Children == null || boMenu.Children.Length == 0)
                {
                    ExecuteBoMenu(boMenu);
                }
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InputBindingCollection InputBindings { get; }
        public BoMenu KeyBindBoMenu(BoMenu boMenu, Key key)
        {
            this.InputBindings.Add(new KeyBinding() { Key = key, Command = new UserCommand<BoMenu>(ExecuteBoMenu), CommandParameter = boMenu });
            return boMenu;
        }

        private void ExecuteBoMenu(BoMenu boMenu)
        {
            model.Model = boMenu.Model;
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
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        public class BoMenu
        {
            public string Name { get; set; }
            public string Header { get; set; }

            public BoMenu[] Children { get; set; }
            public object Model { get; set; }
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
}