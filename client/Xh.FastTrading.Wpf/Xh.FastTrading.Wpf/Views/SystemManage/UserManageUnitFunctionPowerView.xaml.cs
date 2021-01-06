using GalaSoft.MvvmLight.Threading;
using Microsoft.Office.Interop.Excel;
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
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Untils;
using static Xh.FastTrading.Wpf.Model.UserInfoModel;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UserManageUnitFunctionPowerView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageUnitFunctionPowerView : System.Windows.Window
    {
        private UserInfoModel UserInfo;
        public UserManageUnitFunctionPowerView(UserInfoModel UserInfoModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            UserManageDetailView.fff = new ObservableCollection<UserInfoModel>();
            List = new ObservableCollection<UserInfoModel>();

            UserInfo = UserInfoModel;
            PlatformData();
            PowerData();

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置平台状态
        /// </summary>
        private void PlatformData()
        {
            var count = 0;
            foreach (var item in UserInfo.FunctionPower)
            {
                count++;
                if (item == 1)
                {
                    chbPC.IsChecked = true;
                    continue;
                }
                if (item == 2)
                {
                    chbApp.IsChecked = true;
                    continue;
                }
            }
        }

        private int strApp;
        private int strPC;
        private void ConfirmPlatform() 
        {
            if (chbPC.IsChecked == true)//PC
            {
                strPC = 1;
            }
            if (chbApp.IsChecked == true)//App
            {
                strApp = 2;
            }
        }

        /// <summary>
        /// 设置功能状态
        /// </summary>
        private void PowerData()
        {
            var count = 0;
            foreach (var item in UserInfo.Power)
            {
                count++;
                if (item == 100)//交易
                {
                    chbTrade.IsChecked = true;
                    continue;
                }
                if (item == 200)//自动交易
                {
                    chbSeniorTrade.IsChecked = true;
                    continue;
                }
                if (item == 300)//指定交易
                {
                    chbAppoint.IsChecked = true;
                    continue;
                }
                if (item == 400)//查询 查看
                {
                    chbLook.IsChecked = true;
                    continue;
                }
                if (item == 500)//单元管理
                {
                    chbUnit.IsChecked = true;
                    continue;
                }
               
                if (item == 700)//系统管理
                {
                    chbSystem.IsChecked = true;
                    continue;
                }
            }
        }

        private int strTrade;
        private int strSeniorTrade;
        private int strAppoint;
        private int strLook;
        private int strUnit;
        private int strSystem;
        private void ConfirmPower()
        {
                if (chbTrade.IsChecked == true)//交易
                {
                    strTrade = 100;
                }
                if (chbSeniorTrade.IsChecked == true)//自动交易
                {
                     strSeniorTrade = 200;
                }
                if (chbAppoint.IsChecked == true)//指定交易
                {
                    strAppoint = 300;
                }
                if (chbLook.IsChecked == true)//查询 查看
                {
                    strLook = 400;
                }
                if (chbUnit.IsChecked == true)//单元管理
                {
                    strUnit = 500;
                }
                if (chbSystem.IsChecked == true)//系统管理
                {
                    strSystem = 700;
                }
        }
        
        /// <summary>
        /// Token
        /// </summary>
        public static string token;
        public static string Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPower();
            ConfirmPlatform();
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                Token = UserToken.token;
                int PowerId = UserInfo.Id;
                //功能权限
                var PowerItems  = new List<tModules>();
                PowerItems.Add(new tModules()
                {
                    trade = strTrade,
                    seniorTrade = strSeniorTrade,
                    appoint = strAppoint,
                    look = strLook,
                    unit= strUnit,
                    system = strSystem
                });
                List<int> PlatPowerList = new List<int>();
                for (int i = 0; i < PowerItems.Count; i++)
                {
                    if (PowerItems[i].trade!=0)
                    {
                        PlatPowerList.Add(PowerItems[i].trade);
                    }
                    if (PowerItems[i].seniorTrade!=0)
                    {
                        PlatPowerList.Add(PowerItems[i].seniorTrade);
                    }
                    if (PowerItems[i].appoint != 0)
                    {
                        PlatPowerList.Add(PowerItems[i].appoint);
                    }
                    if (PowerItems[i].look != 0)
                    {
                        PlatPowerList.Add(PowerItems[i].look);
                    }
                    if (PowerItems[i].unit != 0)
                    {
                        PlatPowerList.Add(PowerItems[i].unit);
                    }
                    if (PowerItems[i].system != 0)
                    {
                        PlatPowerList.Add(PowerItems[i].system);
                    }
                }


                //平台
                var PlatformItems = new List<tplatforms>();
                PlatformItems.Add(new tplatforms()
                {
                    App = strApp,
                    PC =strPC
                });
                List<int> QuotaItems = new List<int>();
                for (int i = 0; i < PlatformItems.Count; i++)
                {
                    if (PlatformItems[i].App!=0)
                    {
                        QuotaItems.Add(PlatformItems[i].App);
                    }
                    if (PlatformItems[i].PC!=0)
                    {
                        QuotaItems.Add(PlatformItems[i].PC);
                    }
                }
               
                IFunctionPowerInterface PowerSetting = new IFunctionPowerInterface();
                var result = await Task.Run(() => PowerSetting.SettingPower(PowerId, PlatPowerList, QuotaItems,  Token));
                string success = result["Message"]["Message"].ToString();
                if (success == "成功")
                {                    
                    InitDataGrid();
                    UserManageDetailView.fff = List;
                    this.Close();
                    MessageDialogManager.ShowDialogAsync("选中账户权限设置成功!");
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success.ToString());
                } 
            });
        }


        /// <summary>
        /// dataGrid集合
        /// </summary>
        private ObservableCollection<UserInfoModel> _list;
        public ObservableCollection<UserInfoModel> List
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        ///  用户列表加载
        /// </summary>
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
    }
}
