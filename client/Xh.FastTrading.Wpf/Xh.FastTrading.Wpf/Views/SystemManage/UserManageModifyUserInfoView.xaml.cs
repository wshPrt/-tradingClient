using GalaSoft.MvvmLight.Threading;
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
using System.Windows.Threading;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.ViewModel;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UserManageModifyUserInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageModifyUserInfoView : Window
    { 
        public UserManageModifyUserInfoView(UserInfoModel userInfoModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true; 
            txtLoginName.Text = userInfoModel.LoginName;
            txtName.Text = userInfoModel.Name;
            txtPhoneNumber.Text = userInfoModel.PhoneNumber;
            txtId.Content = userInfoModel.Id;
            UserManageDetailView.eee = new ObservableCollection<UserInfoModel>();
            List = new ObservableCollection<UserInfoModel>();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private ObservableCollection<UserInfoModel> _list;
        public ObservableCollection<UserInfoModel> List
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLoginName.Text) && !string.IsNullOrWhiteSpace(txtName.Text))
            {
                IModifyUserInterface modifyUser = new IModifyUserInterface();
                var Token = UserToken.token;
                var confirmModify = modifyUser.ModifyUser(int.Parse(txtId.Content.ToString()), txtLoginName.Text, txtName.Text, int.Parse(txtPhoneNumber.Text.ToString()), Token);
                string success = confirmModify["Message"]["Message"].ToString();
                if (success == "成功")
                {
                    List.Clear();
                    InitDataGrid();
                    
                    UserManageDetailView.eee = List;
                    this.Close();
                    MessageDialogManager.ShowDialogAsync("用户信息修改成功!");
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            }

        }

        public void InitDataGrid()
        {
            string token = UserToken.token;
            IUserlistInterface userList = new IUserlistInterface();
            var userListData = userList.UserList(token);
            string success = userListData["Message"]["Message"].ToString();
            string json = userListData["Message"].ToString();
            if (success == "成功")
            {
                UserInfoModel.RootData data = JsonConvert.DeserializeObject<UserInfoModel.RootData>(json);
                for (int i = 0; i < data.Data.Count; i++)
                {

                    List.Add(new UserInfoModel()
                    {
                        Id = data.Data[i].id,
                        LoginName = data.Data[i].code.ToString(),
                        Name = data.Data[i].name.ToString(),
                        PhoneNumber = data.Data[i].mobile,
                        DynPasswordNumber = data.Data[i].id,
                        FunctionPower = data.Data[i].authority_platforms,
                        Power = data.Data[i].authority_modules,
                        UnitNumber = data.Data[i].unit_count,
                        Limit = data.Data[i].status_order,
                        Status = data.Data[i].status
                    });
                }
            }
            else
            {
                MessageDialogManager.ShowDialogAsync("加载用户信息失败！");
            }
        }

        private void Refresh()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate (object f)
                {
                    ((DispatcherFrame)f).Continue = false;
                    return null;
                }
            ), frame);

            Dispatcher.PushFrame(frame);

        }


    }
}
