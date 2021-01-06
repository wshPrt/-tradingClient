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
            //Title = "UTOPͶ�����ϵͳ3.0(�� �� ��) ";
            ChangeTitleCommand = new RelayCommand(ChangeTitle);

            var mainMenu = new[] {
                KeyBindBoMenu(new BoMenu{ Header = "���� (F1)", Name="BUY", Model = new BuyModel() { Title = "����" } }, Key.F1),
                KeyBindBoMenu(new BoMenu{ Header = "���� (F2)", Name="SELL",Model = new SellModel() { Title = "����" } }, Key.F2),
                new BoMenu{ Header = "��ծ��ع�",Name="BACK",Model = new BackModel() { Title = "�ع�" } },
                new BoMenu{ Header = "�Զ�����",Name="AUTOMATIC",Model = new AutoMaticModel() { Title = "�Զ�����" } },
                new BoMenu{ Header = "ָ������",Name="SPECIFY",Model = new SpecifyModel() { Title = "ָ������" } },
                KeyBindBoMenu(new BoMenu{ Header = "���� (F3)",Name="CANCEL(F3)",Model = new CancelModel() { Title = "���� (F3)" } },Key.F3),
                new BoMenu{
                    Header = "��ع���",
                    Children=new[]{
                        new BoMenu { Header= "���ڲ���",Name="RISKSTRATEGY",Model = new RiskStrategyModel() { Title = "���ڲ���" }}
                    }
                },
                new BoMenu{
                    Header = "��ѯ",
                    Children=new[]{
                        KeyBindBoMenu(new BoMenu { Header= "�ʲ� (F4)",Name="ASSETS",Model = new AssetsModel() { Title = "�ʲ� (F4)" }},Key.F4),
                        new BoMenu { Header= "ί�м�¼",Name="ENTRUST",Model = new EntrustModel() { Title = "ί�м�¼" }},
                        new BoMenu { Header= "�ɽ���¼",Name="DEAL" ,Model = new DealModel() { Title = "�ɽ���¼" }},
                        new BoMenu { Header= "�ʽ��¼",Name="CAPITAL",Model = new CapitalModel() { Title = "�ʽ���ˮ" }}
                    }
                },
                 new BoMenu{
                    Header = "��Ԫ����",
                    Children=new[]{
                        new BoMenu { Header= "���ڲ���",Name="UNITTRATEGY",Model = new UnitTrategyModel() { Title = "���ڲ���" }},
                        new BoMenu { Header= "�������ƹ���",Name="RULE",Model = new RuleModel() { Title = "�������ƹ���" }},
                        new BoMenu { Header= "�ֲֻ���",Name="POSITION",Model = new PositionModel() { Title = "�ֲֻ���" }},
                        new BoMenu { Header= "ί�л���" ,Name="ENTRUSTSUMMARY",Model = new EntrustSummaryModel() { Title = "ί�л���" }},
                        new BoMenu { Header= "�쳣ί��",Name="ABNORMALENTRUST",Model = new AbnormalEntrustModel() { Title = "�쳣ί��" }},
                        new BoMenu { Header= "�쳣����",Name="ABNORMALCANCEL",Model = new AbnormalCancelModel() { Title = "�쳣����" }},
                        new BoMenu { Header= "�ϵ�����",Name="VOID",Model = new VoidModel() { Title = "�ϵ�����" }},
                        new BoMenu { Header= "ϵͳ�ڳɽ�����",Name="SYSTEMDEAL",Model = new SystemDealModel() { Title = "ϵͳ�ڳɽ�����" }},
                        new BoMenu { Header= "ϵͳ��ɽ�����",Name="SYSTEMDEALOUT",Model = new SystemDealOutModel() { Title = "ϵͳ��ɽ�����" }},
                        new BoMenu { Header= "�����Ʊ����������",Name="BAN",Model = new BanModel() { Title = "�����Ʊ����������" }},
                        new BoMenu { Header= "�Զ�ƽ������",Name="LIQUIDATION",Model = new LiquidationModel() { Title = "�Զ�ƽ������" }}
                    }
                },
                  new BoMenu{
                    Header = "ϵͳ����",
                    Children=new[]{
                        new BoMenu { Header= "�û�����",Name="USERMANAGE",Model = new UserManagemodel() { Title = "�û�����" }},
                        new BoMenu { Header= "��������",Name="OPENCLOSE",Model = new OpenCloseModel() { Title = "��������" }},
                        new BoMenu { Header= "�ֺ��͹�",Name="DIVIDENDS",Model = new DividendsModel() { Title = "�ֺ��͹�" }},
                        new BoMenu { Header= "�ֲֻ�ת",Name="TRANSFER",Model = new TransferModel() { Title = "�ֲֻ�ת" }},
                        new BoMenu { Header= "���˻���",Name="ACCOUNT",Model = new AccountModel() { Title = "���˻���" }},
                        new BoMenu { Header= "���˻�����",Name="ACCOUNTMANAGE",Model = new AccountManageModel() { Title = "���˻�����" }},
                        new BoMenu { Header= "���˻��ֲ�",Name="ACCOUNTPOSITION",Model = new AccountPositionModel() { Title = "���˻��ֲ�"}}
                    }
                }
            };
            model.MainMenu = mainMenu;
        }
        private void ChangeTitle()
        {
           // Title = "UTOPͶ�����ϵͳ3.0(�� �� ��)";
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