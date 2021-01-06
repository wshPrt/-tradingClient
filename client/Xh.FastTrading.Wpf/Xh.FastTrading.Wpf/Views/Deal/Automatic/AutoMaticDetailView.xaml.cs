using GalaSoft.MvvmLight.Messaging;
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
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.ViewModel.Deal;
using Xh.FastTrading.Wpf.Views.Deal.Automatic;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// AutoMaticDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class AutoMaticDetailView : UserControl
    {
        public AutoMaticDetailView()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, "EntrustInfoView", ShowReceiveInfo);
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void ShowReceiveInfo(String msg)
        {
            EntrustInfoView entrust = new EntrustInfoView(); 
            entrust.ShowDialog();
        }


        public void ReleaseRegister()
        {
            Messenger.Default.Unregister(this);
        }

    }
}
