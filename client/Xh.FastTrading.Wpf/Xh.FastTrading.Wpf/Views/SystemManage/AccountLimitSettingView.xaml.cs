using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
using Xh.FastTrading.Wpf.ViewModel.SystemManage;
using Xh.FastTrading.Wpf.Commands.IniFile;
using static Xh.FastTrading.Wpf.Common.Commands.IniFile.Configuration;

namespace Xh.FastTrading.Wpf.Views.SystemManage
{

    /// <summary>
    /// AccountLimitSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountLimitSettingView : Window
    {
        private static List<ListDataModel> myDataList = null;
        private List<BuySellModel> myDataListPool = null;
        AccountDetailVM account = new AccountDetailVM();
        AccountDetailPoolVM accountPool = new AccountDetailPoolVM();
        private MasterAccountModel ValidateUI;
        private AccountPoolModel AccountPool;
        public static List<ListDataModel> PoolModel { get; set; } = new List<ListDataModel>();
        public static List<ListDataModel> PoolModelYc { get; set; } = new List<ListDataModel>();
        public static List<ListDataModel> PoolModelSellYc { get; set; } = new List<ListDataModel>();
        public AccountLimitSettingView(AccountPoolModel accountPool)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            InitializeComponent();
            AccountPool = accountPool;
            ValidateUI = new MasterAccountModel();
            this.DataContext = account;
            //this.DataContext = accountPool;

            PoolModelYc = accountPool.Items.OrderBy(i => i.sort_buy).Select(i => new ListDataModel() { Id = i.account_id, Code = i.account_code, Name = i.account_name }).ToList();
            PoolModelSellYc = accountPool.Items.OrderBy(i => i.sort_sell).Select(i => new ListDataModel() { Id = i.account_id, Code = i.account_code, Name = i.account_name }).ToList();
            PoolModel = account.List.Select(p => new ListDataModel { Indexes = p.Id, Name = p.Name, Id = p.Id, Cash = p.Cash, PriorityStrategy = p.Status, Code = p.Code }).Where(i => !PoolModelYc.Select(o => o.Id).Contains(i.Id)).ToList();
            // myDataListPool = accountPool.List.Select(p => new BuySellModel { id = p.Id, accountId = p.Id, accountName = p.accountName, accountCode = p.AccountCode, capitalAllow = p.CapitalAllow, code = p.Code }).ToList<BuySellModel>();
            //if (account.List != null && account.List.Count > 0)
            //{
            //    Random rd = new Random();

            //    ValidateUI.Sortbuy = rd.Next(0, account.List.Count);
            //    ValidateUI.SortSell = rd.Next(0, account.List.Count);
            //}
            //EsCanSelectList.ItemsSource = myDataList;
            //绑定用户池list
            //ReadDataInfo();
            //if (PoolModel == null || PoolModel.Count == 0) PoolModel = myDataList;
            //if ((myDataList != null && myDataList.Count > 0) && (PoolModelYc != null && PoolModelYc.Count > 0))
            //{

            //}
            ReLoadData();

        }


        private void ReLoadData()
        {
            EsCanSelectList.ItemsSource = null;
            EsCanSelectList.ItemsSource = PoolModel;
            txtBUY.ItemsSource = null;
            txtBUY.ItemsSource = PoolModelYc;
            txtSell.ItemsSource = null;
            txtSell.ItemsSource = PoolModelSellYc;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //private Dictionary<string, int> SelectedItems;
        private void EsCanSelectList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取当前选择项的值
            var currentItemText = EsCanSelectList.SelectedValue.ToString();
            //获取当前选择项的索引
            var currentItemIndex = EsCanSelectList.SelectedIndex;
            //this.Close();
            //添加对象到目标控件
            var sItem = (EsCanSelectList.SelectedItems[0] as ListDataModel);
            ValidateUI.Id = sItem.Id;
            //没有优先策略字段;暂时赋值状态
            ValidateUI.Status = sItem.PriorityStrategy;
            //txtBUY.Items.Add(sItem.Name);
            //txtSell.Items.Add(sItem.Name);


            PoolModelYc.Add(sItem);
            PoolModelSellYc.Add(sItem);
            if (PoolModel.Count > 0) PoolModel.Remove(sItem);
            else
            {
                //移除当前控件的数据源对象中的当前选择项
                myDataList.RemoveAt(currentItemIndex);
                PoolModel = myDataList;
            }
            //Dictionary<string, int> itemsList = new Dictionary<string, int>();
            //itemsList.Add("account_id", sItem.Id);
            //itemsList.Add("capitalAllow", sItem.Cash);
            //itemsList.Add("sort_buy", ValidateUI.Sortbuy);
            //itemsList.Add("sort_sell", ValidateUI.SortSell);
            // MasterAccountModel.SelectedQuotaItems = PoolModelYc.Select(i=>new tClass {  account_id=i.Id, capital_allow=i.PriorityStrategy}).ToList();
            MasterAccountModel.MasterId = sItem.Id;
            MasterAccountModel.PriorityStrategy = ValidateUI.Status;

            //ReadDataInfo();
            ReLoadData();
        }

        private void Clean()
        {
            PoolModelYc.Clear();
            PoolModelSellYc.Clear();
            PoolModel.AddRange(PoolModelYc);
            PoolModel.AddRange(PoolModelSellYc);
            myDataList = account.List.Select(p => new ListDataModel { Name = p.Name, Id = p.Id, Cash = p.Cash, PriorityStrategy = p.Status, Code = p.Code }).ToList<ListDataModel>();
            ReLoadData();
        }

        private void SelectAll()
        {
            PoolModelYc.AddRange(PoolModel.Select(i => i).ToList());
            PoolModelSellYc.AddRange(PoolModel.Select(i => i).ToList());
            PoolModel = new List<ListDataModel>();
            ReLoadData();
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        //private void SaveDataInfo()
        //{
        //    string srINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.Save_Records;
        //    string srYcINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.SaveYc_Records;

        //    if (File.Exists(srINI)) { File.Delete(srINI); File.Create(srINI).Close(); }
        //    if (File.Exists(srYcINI)) { File.Delete(srYcINI); File.Create(srYcINI).Close(); }
        //    if (PoolModel == null) ReadDataInfo();
        //    using (StreamWriter sw = new StreamWriter(srINI))
        //    {
        //        sw.WriteLine(JsonConvert.SerializeObject(PoolModel));
        //    }
        //    using (StreamWriter sw = new StreamWriter(srYcINI))
        //    {
        //        sw.WriteLine(JsonConvert.SerializeObject(PoolModelYc));
        //    }
        //}
        /// <summary>
        /// 读取保存数据
        /// </summary>
        //public void ReadDataInfo()
        //{
        //    string srINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.Save_Records;
        //    string srYcINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.SaveYc_Records;
        //    if (File.Exists(srINI))
        //    {
        //        var json = "";
        //        using (StreamReader sr = new StreamReader(srINI))
        //        {
        //            json = sr.ReadToEnd();
        //        }
        //        PoolModel = JsonConvert.DeserializeObject<List<ListDataModel>>(json) ?? new List<ListDataModel>();
        //       //PoolModel = PoolModel.OrderBy(o => o.Id).ToList();
        //    }
        //    if (File.Exists(srINI))
        //    {
        //        var json = "";
        //        using (StreamReader sr = new StreamReader(srYcINI))
        //        {
        //            json = sr.ReadToEnd();
        //        }
        //        PoolModelYc = JsonConvert.DeserializeObject<List<ListDataModel>>(json) ?? new List<ListDataModel>();
        //        //PoolModelYc=PoolModelYc.OrderBy(o => o.Id).ToList();
        //    }

        //}
        //买入向上移
        private void menUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtBUY.SelectedItems.Count == 0)
            {
                return;
            }
            var item = (ListDataModel)this.txtBUY.SelectedItem;//listbox选择的索引  
            var items = (IList<ListDataModel>)txtBUY.ItemsSource;
            var index = items.IndexOf(item);

            items.Remove(item);
            items.Insert(Math.Max(index - 1, 0), item);
            txtBUY.Items.Refresh();
        }

        //买入向下移
        private void menDown_CLick(object sender, RoutedEventArgs e)
        {
            if (this.txtBUY.SelectedItems.Count == 0)
            {
                return;
            }
            int lbxLength = this.txtBUY.Items.Count;//listbox的长度  
            var item = (ListDataModel)this.txtBUY.SelectedItem;//listbox选择的索引  
            var items = (IList<ListDataModel>)txtBUY.ItemsSource;
            var index = items.IndexOf(item);
            if (lbxLength > index && index < lbxLength -1)
            {
                items.Remove(item);
                items.Insert(Math.Max(index + 1, 0), item);
                txtBUY.Items.Refresh();
            }
        
            }

        /// <summary>
        /// 卖出上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtSell.SelectedItems.Count == 0)
            {
                return;
            }
            var item = (ListDataModel)this.txtSell.SelectedItem;//listbox选择的索引  
            var items = (IList<ListDataModel>)txtSell.ItemsSource;
           var index = items.IndexOf(item);

            items.Remove(item);
            items.Insert(Math.Max(index - 1, 0), item);
            txtSell.Items.Refresh();
        }

        /// <summary>
        /// 卖出下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtSell.SelectedItems.Count == 0)
            {
                return;
            }
            int lbxLength = this.txtBUY.Items.Count;//listbox的长度  
            var item = (ListDataModel)this.txtSell.SelectedItem;//listbox选择的索引  
            var items = (IList<ListDataModel>)txtSell.ItemsSource;
            var index = items.IndexOf(item);
            if (lbxLength > index && index < lbxLength - 1)
            {
                items.Remove(item);
                items.Insert(Math.Max(index + 1, 0), item);
                txtSell.Items.Refresh();
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }

        /// <summary>
        /// 买入双击左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBUY_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取当前选择项的值
            var currentItemText = txtBUY.SelectedValue.ToString();
            //获取当前选择项的索引
            var currentItemIndex = txtBUY.SelectedIndex;
            //this.Close();
            //添加对象到目标控件
            var sItem = (txtBUY.SelectedItems[0] as ListDataModel);
            ValidateUI.Id = sItem.Id;
            //没有优先策略字段;暂时赋值状态
            ValidateUI.Status = sItem.PriorityStrategy;
            PoolModel.Add(sItem);
            PoolModelYc.RemoveAt(PoolModelYc.FindIndex(p => p.Id == sItem.Id));
            PoolModelSellYc.RemoveAt(PoolModelSellYc.FindIndex(p => p.Id == sItem.Id));
            //SaveDataInfo();
            ReLoadData();

        }

        /// <summary>
        /// 卖出双击左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //获取当前选择项的值
            var currentItemText = txtSell.SelectedValue.ToString();
            //获取当前选择项的索引
            var currentItemIndex = txtSell.SelectedIndex;
            //this.Close();
            //添加对象到目标控件
            var sItem = (txtSell.SelectedItems[0] as ListDataModel);
            ValidateUI.Id = sItem.Id;
            //没有优先策略字段;暂时赋值状态
            ValidateUI.Status = sItem.PriorityStrategy;

            PoolModel.Add(sItem);
            PoolModelYc.RemoveAt(PoolModelYc.FindIndex(p => p.Id == sItem.Id));
            PoolModelSellYc.RemoveAt(PoolModelSellYc.FindIndex(p => p.Id == sItem.Id));
            //SaveDataInfo();
            ReLoadData();
        }

        /// <summary>
        /// 限额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quota_Click(object sender, RoutedEventArgs e)
        {
            AccountLimitView accuntLimit = new AccountLimitView();
            accuntLimit.ShowDialog();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountLimitSetting_Click(object sender, RoutedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                AccountDetailPoolVM objAccount = new AccountDetailPoolVM();
                if (!(objAccount.List.Count <= 0))
                {
                    string token = UserToken.token;
                    var QuotaItems = new List<tClass>();
                    for (int i = 0; i < PoolModelYc.Count; i++)
                    {
                        QuotaItems.Add(new tClass()
                        {
                            account_id = PoolModelYc[i].Id,
                            capital_allow = PoolModelYc[i].Cash,
                            sort_buy = i,
                            sort_sell = PoolModelSellYc.FindIndex(m => m.Id == PoolModelYc[i].Id)
                        });
                    }
                    var MasterId = MasterAccountModel.MasterId;
                    var PriorityStrategy = MasterAccountModel.PriorityStrategy;

                    IAccountPoolLimitSettingInterface accountPoolLimitSetting = new IAccountPoolLimitSettingInterface();
                    var result = await Task.Run(() => accountPoolLimitSetting.AccountPoolLimitSetting(AccountPool.Id, PriorityStrategy, QuotaItems, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        this.Close();
                        MessageDialogManager.ShowDialogAsync("用户和限额设置成功!");
                        return;
                    }
                    else
                    {
                        this.Close();
                        txtBUY.ItemsSource = null;
                        txtSell.ItemsSource = null;
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }
        public class ListDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Cash { get; set; }
            public int PriorityStrategy { get; set; }
            public string Code { get; set; }
            public int Indexes  { get; set; }
        }

        public class BuySellModel
        {
            public string code { get; set; }
            public int id { get; set; }
            public string accountCode { get; set; }
            public int accountId { get; set; }
            public string accountName { get; set; }
            public decimal capitalAllow { get; set; }
        }
    }
}
