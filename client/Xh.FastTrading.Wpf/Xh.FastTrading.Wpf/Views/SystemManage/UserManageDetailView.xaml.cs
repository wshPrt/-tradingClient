using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
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
using Xh.FastTrading.Wpf.ViewModel;
using Xh.FastTrading.Wpf.Untils;
using GalaSoft.MvvmLight.Messaging;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{

    public partial class UserManageDetailView : UserControl
    {

        public UserManageDetailView()
        {
            InitializeComponent();

        }

        public static ObservableCollection<UserInfoModel> eee;
        private void ModifyUserInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridFert.SelectedItem == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else
            {

                var userInfoModel = dataGridFert.SelectedItem as Model.UserInfoModel;
                UserManageModifyUserInfoView userManageModify = new UserManageModifyUserInfoView(userInfoModel);

                userManageModify.ShowDialog();
                if (eee != null&& eee.Count>0)
                {
                    dataGridFert.ItemsSource = null;
                    dataGridFert.ItemsSource = eee;
                }
            }
        }
        public static ObservableCollection<UserInfoModel> fff ;
        private void FunctionPower_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridFert.SelectedItem == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else
            {

                var userInfoModel = dataGridFert.SelectedItem as Model.UserInfoModel;
                UserManageUnitFunctionPowerView functionPower = new UserManageUnitFunctionPowerView(userInfoModel);

                functionPower.ShowDialog();
                if (fff != null && fff.Count > 0)
                {
                    dataGridFert.ItemsSource = null;
                    dataGridFert.ItemsSource = fff;
                }
            }
        }
    }
}
