using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xh.FastTrading.Wpf.Common.Commands;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.InterFace.UnitManage;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.ViewModel;
using Xh.FastTrading.Wpf.ViewModel.UnitManage;
using static Xh.FastTrading.Wpf.Model.UserInfoModel;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{
    /// <summary>
    /// UnitPowerView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageUnitPowerView : Window
    {
        
        private BindingList<MidStrategyUnitManageModel> _Bindlist;
        public BindingList<MidStrategyUnitManageModel> Bindlist
        {
            get { return _Bindlist; }
            set { _Bindlist = value; }
        }
        private static List<ListDataModel> myDataList = null;
        private UserInfoModel UserInfo ;
        public  List<ListDataModel> LeftPowerModel { get; set; }
        public  List<ListDataModel> RightPowerModel { get; set; } = new List<ListDataModel>();
        public UserManageUnitPowerView(UserInfoModel UserInfoModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;

            Bindlist = new BindingList<MidStrategyUnitManageModel>();
            UserInfo = UserInfoModel;
            Window_Loaded();
            Right_Loaded();
           
            LeftPowerModel = Bindlist.Select(p => new ListDataModel { Id = p.Id, Code = p.UnitCode}).Where(i => !RightPowerModel.Select(o => o.Code).Contains(i.Code)).ToList();

            ReLoadData();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ReLoadData()
        {
            lstPower.ItemsSource = null; 
            lstPower.ItemsSource = LeftPowerModel;
            lstUnitPower.ItemsSource = null;
            lstUnitPower.ItemsSource = RightPowerModel;
        }
        private void lstPower_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取当前选择项的值
            var currentItemText = lstPower.SelectedValue.ToString();
            //获取当前选择项的索引
            var currentItemIndex = lstPower.SelectedIndex;
            //this.Close();
            //添加对象到目标控件
            var sItem = (lstPower.SelectedItems[0] as ListDataModel);
            //没有优先策略字段;暂时赋值状态
            UserInfo.Name = sItem.Code;
            RightPowerModel.Add(sItem);
            if (LeftPowerModel.Count > 0) LeftPowerModel.Remove(sItem);
            else 
            {
                //移除当前控件的数据源对象中的当前选择项
                myDataList.RemoveAt(currentItemIndex);
                LeftPowerModel = myDataList;
            }
            ReLoadData();
        }

        /// <summary>
        /// 全清
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            LeftPowerModel.AddRange(RightPowerModel);
            RightPowerModel.Clear();
            ReLoadData();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            RightPowerModel.AddRange(LeftPowerModel);
            LeftPowerModel.Clear();
            ReLoadData();
        }

        //双击右list左移
        private void lstUnitPower_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取当前选择项的值
            var currentItemText = lstUnitPower.SelectedValue.ToString();
            //获取当前选择项的索引
            var currentItemIndex = lstUnitPower.SelectedIndex;
            //this.Close();
            //添加对象到目标控件
            var sItem = (lstUnitPower.SelectedItems[0] as ListDataModel);
            UserInfo.Name = sItem.Code;
            LeftPowerModel.Add(sItem);
            RightPowerModel.RemoveAt(RightPowerModel.FindIndex(p => p.Id == sItem.Id));
            ReLoadData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                UserManageDetailVM objUserManage = new UserManageDetailVM();
                if (!(objUserManage.List.Count <= 0))
                {
                    string token = UserToken.token;
                    List<int> QuotaItems = new List<int>();
                    for (int i = 0; i < RightPowerModel.Count; i++)
                    {
                        QuotaItems.Add(RightPowerModel[i].Id);
                    }

                    IUnitPowerInterface unitPower = new IUnitPowerInterface();

                    var result = await Task.Run(() => unitPower.UnitPower(UserInfo.Id, QuotaItems, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        this.Close();
                        MessageDialogManager.ShowDialogAsync("单元权限保存成功!");
                        return;
                    }
                    else
                    {
                        this.Close();
                        
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

      
        public void Window_Loaded()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IMidStrategyInterface midStrategy = new IMidStrategyInterface();
                var result = midStrategy.MidStrategy(token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MidStrategyUnitManageModel.Root data = JsonConvert.DeserializeObject<MidStrategyUnitManageModel.Root>(jsonData);
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        Bindlist.Add(new MidStrategyUnitManageModel()
                        {
                            UnitCode = data.Data[i].code,
                            Id = data.Data[i].id
                        });
                    }

                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }
        public void Right_Loaded() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                string token = UserToken.token;
                IDealBuyInterface unitList = new IDealBuyInterface();
                int Requst = UserInfo.Id;
                var result = unitList.UnitList(Requst,token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MidStrategyUnitManageModel.Root data = JsonConvert.DeserializeObject<MidStrategyUnitManageModel.Root>(jsonData);
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        RightPowerModel.Add(new ListDataModel()
                        {
                            Code  = data.Data[i].code,
                            Id = data.Data[i].id
                        });
                    }

                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }
    }


    public class ListDataModel 
    {
        public int Id { get; set; }
        public string Code { get; set; } 
    }
}
