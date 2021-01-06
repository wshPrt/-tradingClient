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
using Xh.FastTrading.Wpf.Views.UnitManage;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// RuleDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class RuleDetailView : UserControl
    {
        public RuleDetailView()
        {
            InitializeComponent();
        }

        private void AddLimitRuleInfo_Click(object sender, RoutedEventArgs e)
        {
            AddLimitRuleInfoView addLimit = new AddLimitRuleInfoView();
            addLimit.Width = Window.GetWindow(this).Width;
            addLimit.Height = Window.GetWindow(this).Height;
            addLimit.Left = Window.GetWindow(this).Left;
            addLimit.Top = Window.GetWindow(this).Top;
            addLimit.ShowDialog();

        }

        private void ModifyLimitRuleInfo_Click(object sender, RoutedEventArgs e)
        {
            AddLimitRuleInfoView modifyLimit = new AddLimitRuleInfoView();
            modifyLimit.Width = Window.GetWindow(this).Width;
            modifyLimit.Height = Window.GetWindow(this).Height;
            modifyLimit.Left = Window.GetWindow(this).Left;
            modifyLimit.Top = Window.GetWindow(this).Top;
            modifyLimit.ShowDialog();
        }
    }
}
