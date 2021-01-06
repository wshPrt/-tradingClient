using Newtonsoft.Json;
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
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UserManageUnitView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageUnitView : UserControl
    {
        public UserManageUnitView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static ObservableCollection<UserInfoModel> eee;
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
           
                
        }
      
    }
}
