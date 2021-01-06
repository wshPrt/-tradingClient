using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Commands;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Common.NetWork;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views.UnitManage;
using System.Windows.Forms;
using System.IO;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
   public class AccountDetailVM:GalaSoft.MvvmLight.ViewModelBase
    {
        public ICommand EsCanSelectListMouseDoubleClickCommand { get; set; }
        public AccountDetailVM() 
        {
            MasterAccount = new MasterAccountModel();
            List = new ObservableCollection<MasterAccountModel>();
            List.Add(masterAccount);
            InitDataGrid();
            ValidateUI = new Model.MasterAccountModel();

            //MasterAccountModel master = new MasterAccountModel();

           // ValidateUIPool = new Model.AccountPoolModel();

        }


        #region DataGrid

        private MasterAccountModel masterAccount;
        public MasterAccountModel MasterAccount
        {
            get { return masterAccount; }
            set { masterAccount = value; RaisePropertyChanged(() => MasterAccount); }
        }

        /// <summary>
        /// 上下移动集合
        /// </summary>
        private ObservableCollection<MasterAccountModel> _listLua; 
        public ObservableCollection<MasterAccountModel> ListLua 
        {
            get { return _listLua; }
            set { _listLua = value; RaisePropertyChanged(); }
        }
        public delegate void ScrollToEnd();
        public ScrollToEnd FocusLastItem = null;


        public List<MasterAccountModel> ListAll = new List<MasterAccountModel>();
        /// <summary>
        /// DataGrid集合
        /// </summary>
        private ObservableCollection<MasterAccountModel> _list;
        public ObservableCollection<MasterAccountModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }

        /// <summary>
        /// 选中行
        /// </summary>
        private MasterAccountModel _selectedRow;
        public MasterAccountModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        /// <summary>
        /// 搜索内容集合
        /// </summary>
        private List<MasterAccountModel> _SearchContent ;
        public List<MasterAccountModel> SearchContent  
        {

            get {
                if (string.IsNullOrEmpty(SearchText)) return new List<MasterAccountModel>();
                return _SearchContent.Where(t => t.Code.Contains(SearchText)).ToList();
                }
                   
            set { _SearchContent = value; }
        }
   
         

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SearchText { get; set; }

        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                if (toClose != value)
                {
                    toClose = value;
                    this.RaisePropertyChanged("ToClose"); 
                }
            }

        }
        #endregion

      
        private MasterAccountModel selectedItem;
        public MasterAccountModel SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(() => SearchCmd());
                }
                return searchCommand;
            }
            set { searchCommand = value; }
        }

        /// <summary>
        /// 选中文本数据
        /// </summary>
        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                    selectCommand = new RelayCommand(() => ExecuteSelect());
                return selectCommand;
            }
            set { selectCommand = value; }
        }
        private void ExecuteSelect()
        {

            //if (List != null && List.Count > 0)
            //{
            //    Random rd = new Random();
            //    ValidateUI.Info = SelectedItem.Name;
            //    ValidateUI.Sortbuy = rd.Next(0, List.Count);
            //    ValidateUI.SortSell = rd.Next(0, List.Count);

            //    //List.SelectedItem = ((Button)sender).DataContext;
            //    ////在数据集合中删除此元素
            //    //List.RemoveAt(List.SelectedIndex);
            //    //List.Refresh();//刷
            //    //List.Remove(List.FirstOrDefault(i => i.Name == SelectedItem.Name));
            //    // InitDataGrid();
            //    //List.Move(selectedItem.Id, List.Count);
            //}

          
            //Dictionary<string, int> itemsList = new Dictionary<string, int>();
            //itemsList.Add("account_id", ValidateUI.Id);
            //itemsList.Add("capitalAllow", 8888);
            //itemsList.Add("sort_buy", ValidateUI.Sortbuy);
            //itemsList.Add("sort_sell", ValidateUI.SortSell);
            //MasterAccountModel.QuotaIems = itemsList;


            //if (List != null && List.Count > 0) 
            //{
            //    for (int i = List.Count - 1; i >= 0; i--)
            //    {
            //        if (SelectedItem.Name == List[i].Name)
            //        {
            //            List.RemoveAt(i);
            //            //输出下一个参数
            //            Console.WriteLine(List[--i].NameInfo);
            //        }
            //    }
            //}
          

        }

       
        /// <summary>
        /// 导出Excel
        /// </summary>
        private RelayCommand excelCommand;
        public RelayCommand ExcelCommand
        {
            get
            {
                if (excelCommand == null)
                {
                    excelCommand = new RelayCommand(() => ExportExcel());
                }
                return excelCommand;
            }
            set { excelCommand = value; }
        }
            
        /// <summary>
        /// 刷新指令
        /// </summary>
        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get 
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(() => Refresh());
                }
                return refreshCommand; }
            set { refreshCommand = value; }
        }

        /// <summary>
        /// 搜索内容
        /// </summary>
        private RelayCommand<string> _QueryCommand;
        public RelayCommand<string> QueryCommand
        {
            get {
                if (_QueryCommand == null)
                {
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        _QueryCommand = new RelayCommand<string>((t) => Query(t));
                    }
                    
                }
                return _QueryCommand; }
            set { _QueryCommand = value;RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private MasterAccountModel validateUI;
        public MasterAccountModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        /// <summary>
        /// 搜索内容
        /// </summary>
        /// <param name="search"></param>
        public void Query(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                this.SearchText = search;
                SearchContent = _SearchContent.Where(t => t.Code.Contains(SearchText)).ToList();
            }

        }
        /// <summary>
        /// 新增弹窗
        /// </summary>
        private RelayCommand addPopupCommand;
        public RelayCommand AddPopupCommand
        {
            get 
            {
                if (addPopupCommand == null)
                {
                    addPopupCommand = new RelayCommand(()=> AddPopup());
                }
                return addPopupCommand; 
            }
            set { addPopupCommand = value; }
        }

        /// <summary>
        /// 新增
        /// </summary>
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get 
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(() => Add());
                }
                return addCommand; 
            }
            set { addCommand = value; }
        }

        /// <summary>
        /// 修改弹窗
        /// </summary>
        private RelayCommand modifyPopupCommand;
        public RelayCommand ModifyPopupCommand
        {
            get
            {
                if (modifyPopupCommand == null )
                {
                    modifyPopupCommand = new RelayCommand(() => ModifyPopup());
                }
                return modifyPopupCommand; }
            set { modifyPopupCommand = value;}
        }

        /// <summary>
        /// 修改
        /// </summary>
        private RelayCommand modifyCommand;
        public RelayCommand ModifyCommand
        {
            get
            {
                if (modifyCommand == null)
                {
                    modifyCommand = new RelayCommand(() => Modify());
                }
                return modifyCommand; 
            }
            set { modifyCommand = value; }
        }

        /// <summary>
        ///删除 
        /// </summary>
        private RelayCommand delCommand;
        public RelayCommand DelCommand
        {
            get 
            {
                if (delCommand == null)
                {
                    delCommand = new RelayCommand(() => Del());
                }
                return delCommand; }
            set { delCommand = value;}
        }

        /// <summary>
        /// 启停用
        /// </summary>
        private RelayCommand revStopCommand;
        public RelayCommand RevStopCommand
        {
            get 
            {
                if (revStopCommand == null)
                {
                    revStopCommand = new RelayCommand(() => RevStop());
                }
                return revStopCommand; 
            }
            set { revStopCommand = value; }
        }

        /// <summary>
        /// 同步
        /// </summary>
        private RelayCommand synchronousCommand;
        public RelayCommand SynchronousCommand
        {
            get
            {
                if (synchronousCommand == null)
                {
                    synchronousCommand = new RelayCommand(() => Syschronous());
                }
                return synchronousCommand; }
            set { synchronousCommand = value; }
        }

        /// <summary>
        ///限制交易 
        /// </summary>
        private RelayCommand limitTradingCommand;
        public RelayCommand LimitTradingCommand
        {
            get
            {
                if (limitTradingCommand == null)
                {
                    limitTradingCommand = new RelayCommand(() => LimitTrading());
                }
                return limitTradingCommand; 
            }
            set { limitTradingCommand = value; }
        }



        /// <summary>
        /// 新增弹窗
        /// </summary>
        private void AddPopup()
        {
            MessageDialogManager.ShowAddAccountInfo();
        }

        /// <summary>
        /// 修改弹窗主账户列表
        /// </summary>
        private void ModifyPopup() 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelectedRow.Code == null))
            {
                MessageDialogManager.ShowAccountInfo();
            }
        }     

        //public void add() 
        //{
        //    ListLua.Add(new MasterAccountModel() { Id = ListLua[ListLua.Count - 1].Id + 1 },Name = ListLua[ListLua.Count - 1].Name);
        //    Select(ListLua[ListLua.Count - 1]);
        //    Actorc = (ListLua.Cout - 1);
        //    FocusLastItem();
        //}

        /// <summary>
        /// 主账户列表加载
        /// </summary>
        private void InitDataGrid() 
        {
            string token = UserToken.token;
            IAccountDetailInterface accountDetail = new IAccountDetailInterface();
            var accountListData = accountDetail.AccountDetail(token);
            string success = accountListData["Message"]["Message"].ToString();
            string josnData = accountListData["Message"].ToString();
            if (success == "成功")
            {
                MasterAccountModel.Root data = JsonConvert.DeserializeObject<MasterAccountModel.Root>(josnData);
                for (int i = 0; i < data.Data.Count; i++)
                {
                    var m = new MasterAccountModel()
                    {
                        Id = data.Data[i].id,
                        Code = data.Data[i].code,
                        Name= data.Data[i].name,
                        Remarks = data.Data[i].remarks,
                        LimitBuy = data.Data[i].limit_no_buying,
                        LimitBuyShare= data.Data[i].limit_ratio_single.ToString(),
                        CommissionRate = data.Data[i].ratio_commission,
                        SingleTicketLimit = data.Data[i].limit_ratio_single,
                        GemSingleTicketLimit = data.Data[i].limit_ratio_gem_single,
                        GemLimit= data.Data[i].limit_ratio_gem_total,
                        AssetWarningLine = data.Data[i].ratio_capital_warning,
                        CurrentAssets = data.Data[i].capital_priority,
                        Cash = data.Data[i].capital_cash,
                        Available = data.Data[i].capital_available,
                        AccountMarketValue = data.Data[i].capital_stock_value,
                        SystemAccount = data.Data[i].full_name,
                        Status = data.Data[i].status
                    };
                    ListAll.Add(m);
                    List.Add(m);
                }
            }
            else  
            {
                MessageDialogManager.ShowDialogAsync(success);
            }
        }

        /// <summary>
        /// 主账户列表新增
        /// </summary>
        private void Add()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            { 
                if (validateUI.IsValidated)
                {
                 string token = UserToken.token;
                    // GetIP getip = new GetIP();
                   // var localIP = getip.GetLocalIP();
                   // var localPort = getip.GetFirstAvailablePort();
                  IAddAccountInterface addAccount = new IAddAccountInterface();

                    var jsonData = addAccount.AddAccount(ValidateUI.IP, ValidateUI.Prot, validateUI.Code, validateUI.Name,
                        validateUI.SystemAccount,validateUI.Remarks,validateUI.LimitBuy, validateUI.CommissionRate,validateUI.SingleTicketLimit,
                        validateUI.GemSingleTicketLimit,validateUI.GemLimit,validateUI.AssetWarningLine,validateUI.InitialCapital,
                        validateUI.InitialCapital, validateUI.FirstCapital, validateUI.RaoBiaoCapital,validateUI.RaoBiaoRates,
                        token);
                    string success = jsonData["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("主账户新增成功!");
                        InitDataGrid();//重新加载数据
                        ToClose = true;
                        return;
                       
                    }
                    else 
                    {
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 主账户列表修改
        /// </summary>
        private void Modify() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    string token = UserToken.token;
                    GetIP getip = new GetIP();
                    var localPort = getip.GetFirstAvailablePort();
                    IModifyAccountInterface modifyAccount = new IModifyAccountInterface();
                    var result = await Task.Run(()=> modifyAccount.ModifyAccount(SelectedRow.Id, SelectedRow.IP, localPort, SelectedRow.Code,
                        SelectedRow.Name,SelectedRow.SystemAccount,SelectedRow.Remarks,SelectedRow.LimitBuy,
                        SelectedRow.CommissionRate,SelectedRow.SingleTicketLimit,SelectedRow.GemSingleTicketLimit,SelectedRow.GemLimit,
                        SelectedRow.AssetWarningLine,SelectedRow.InitialCapital,SelectedRow.BadCapital,SelectedRow.FirstCapital,
                        SelectedRow.RaoBiaoCapital,SelectedRow.RaoBiaoRates,token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        ToClose = true;
                        InitDataGrid();//重新加载数据
                        MessageDialogManager.ShowDialogAsync("选中主账户已修改成功!");
                    }
                    else
                    {
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 删除选中主账户列表
        /// </summary>
        private void Del() 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelectedRow.Code == null))
            {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                {
                    string token = UserToken.token;
                    IDelAccountInterface delAccount = new IDelAccountInterface();
                    var result = await Task.Run(() => delAccount.Del(SelectedRow.Id, SelectedRow.Code, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success =="成功")
                    {
                        InitDataGrid();//重新加载数据
                        MessageDialogManager.ShowDialogAsync("选中主账户已删除成功!");
                        return;
                    }
                    else if(!string.IsNullOrEmpty(success))
                    {
                        InitDataGrid();
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                });
             
            }
        }

        /// <summary>
        /// 启停用
        /// </summary>
        private void RevStop() 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }else if(!(SelectedRow.Code == null)) 
            {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                {
                    string token = UserToken.token;
                    IRevStopInterface revStop = new IRevStopInterface();
                    var result = await Task.Run(()=> revStop.RevStop(SelectedRow.Id, SelectedRow.Status,token));
                    string success = result["Message"]["Message"].ToString();
                    if (SelectedRow.Status == 1 && success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("选中主账户已停用成功!");
                        return;
                    }
                    else if(SelectedRow.Status == 0 && success =="成功")
                    {
                        MessageDialogManager.ShowDialogAsync("选中主账号已启用成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }                   
                });
            }
        }

        /// <summary>
        /// 同步
        /// </summary>
        private void Syschronous() 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelectedRow.Code == null))
            {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                {
                    string token = UserToken.token;
                    ISyschronousAccountInterface sysAccount = new ISyschronousAccountInterface();
                    var result = sysAccount.SyschronousAccount(token);
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("选中账户IP同步成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                });
            }
        }

        /// <summary>
        ///限制交易 
        /// </summary>
        private void LimitTrading() 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }else if(!(SelectedRow.Code == null)) 
            {
               
                    string token = UserToken.token;
                    ILimitTradingAccountInterface limitTrading = new ILimitTradingAccountInterface();
                    var result =  limitTrading.LimitTrading(SelectedRow.Id, SelectedRow.Status, token);
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        InitDataGrid();//重新加载数据
                        MessageDialogManager.ShowDialogAsync("选中账号已设置限买成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }

            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh() 
        {
            List.Clear();
            InitDataGrid();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel() 
        {

            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (List == null || List.Count == 0)
                {
                    MessageDialogManager.ShowDialogAsync("没记录无法导出!");
                    return;
                }
                if (List.Count > 0)
                {
                    //Excel表格的创建步骤
                    //第一步：创建Excel对象
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    //第二步：创建Excel对象的工作簿
                    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();
                    //第三步：Excel表头设置
                    //给sheet添加第一行的头部标题
                    NPOI.SS.UserModel.IRow irow = sheet.CreateRow(0);//创建行
                    irow.CreateCell(0).SetCellValue("代码");
                    irow.CreateCell(1).SetCellValue("名称");
                    irow.CreateCell(2).SetCellValue("备注");
                    irow.CreateCell(3).SetCellValue("限买");
                    irow.CreateCell(4).SetCellValue("限买股票");
                    irow.CreateCell(5).SetCellValue("佣金率");
                    irow.CreateCell(6).SetCellValue("单票限制");
                    irow.CreateCell(7).SetCellValue("创业板单票限制");
                    irow.CreateCell(8).SetCellValue("创业板限制");
                    irow.CreateCell(9).SetCellValue("资产预警线");
                    irow.CreateCell(10).SetCellValue("当前资产");
                    irow.CreateCell(11).SetCellValue("现金");
                    irow.CreateCell(12).SetCellValue("可用");
                    irow.CreateCell(13).SetCellValue("账户市值");
                    irow.CreateCell(14).SetCellValue("系统账户");
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].Code);
                        row.CreateCell(1).SetCellValue(List[i].Name);
                        row.CreateCell(2).SetCellValue(List[i].Remarks.ToString());
                        row.CreateCell(3).SetCellValue(List[i].LimitBuy.ToString());
                        row.CreateCell(4).SetCellValue(List[i].LimitBuyShare.ToString());
                        row.CreateCell(5).SetCellValue(List[i].CommissionRate.ToString());
                        row.CreateCell(6).SetCellValue(List[i].SingleTicketLimit.ToString()); 
                        row.CreateCell(7).SetCellValue(List[i].GemSingleTicketLimit.ToString());
                        row.CreateCell(8).SetCellValue(List[i].GemLimit.ToString());
                        row.CreateCell(9).SetCellValue(List[i].AssetWarningLine.ToString());
                        row.CreateCell(10).SetCellValue(List[i].CurrentAssets.ToString());
                        row.CreateCell(11).SetCellValue(List[i].Cash.ToString());
                        row.CreateCell(12).SetCellValue(List[i].Available.ToString());
                        row.CreateCell(13).SetCellValue(List[i].AccountMarketValue.ToString());
                        row.CreateCell(14).SetCellValue(List[i].SystemAccount.ToString());

                    }
                    //把Excel转化为文件流，输出
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "选择要保存的路径";
                    saveFileDialog.Filter = "Excel文件|*.xls|所有文件|*.*";
                    saveFileDialog.FileName = string.Empty;
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.DefaultExt = "xls";
                    saveFileDialog.CreatePrompt = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileStream BookStream = new FileStream(saveFileDialog.FileName.ToString(), FileMode.Create, FileAccess.Write);//定义文件流
                        book.Write(BookStream);//将工作薄写入文件流                  
                        BookStream.Seek(0, SeekOrigin.Begin); //输出之前调用Seek（偏移量，游标位置）方法：获取文件流的长度
                        BookStream.Close();
                        MessageDialogManager.ShowDialogAsync("指定交易记录导出成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("导出保存失败！");
                        return;
                    }
                }
            });
        }

        //#region AccountDetailPoolVM下的
        ///// <summary>
        ///// 账户和限额设置
        ///// </summary>
        //private RelayCommand accountLimitSettingCommand;
        //public RelayCommand AccountLimitSettingCommand
        //{
        //    get
        //    {
        //        if (accountLimitSettingCommand == null)
        //        {
        //            AccountLimitSettingCommand = new RelayCommand(() => AccountLimitSetting());
        //        }
        //        return accountLimitSettingCommand;
        //    }
        //    set { accountLimitSettingCommand = value; }
        //}

        //private AccountPoolModel validateUIPool;
        //public AccountPoolModel ValidateUIPool
        //{
        //    get { return validateUIPool; }
        //    set { validateUIPool = value; RaisePropertyChanged(() => ValidateUIPool); }
        //}

        ///// <summary>
        ///// 用户和限额设置
        ///// </summary>
        //private void AccountLimitSetting()
        //{
        //    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
        //    {
        //        if (!(List.Count <= 0))
        //        {
        //            string token = UserToken.token;
        //            var QuotaItems = MasterAccountModel.QuotaItems;

        //            IAccountPoolLimitSettingInterface accountPoolLimitSetting = new IAccountPoolLimitSettingInterface();
        //            var result = await Task.Run(() => accountPoolLimitSetting.AccountPoolLimitSetting(ValidateUIPool.Id, ValidateUIPool.PriorityStrategy, QuotaItems, token));
        //            string success = result["Message"]["Message"].ToString();
        //            if (success == "成功")
        //            {
        //                ToClose = true;
        //                MessageDialogManager.ShowDialogAsync("用户和限额设置成功!");
        //                return;
        //            }
        //            else
        //            {
        //                ToClose = true;
        //                MessageDialogManager.ShowDialogAsync(success);
        //            }
        //        }
        //    });
        // #endregion


        /// <summary>
        /// 搜索
        /// </summary>
        private void SearchCmd()
        {
            List.Clear();
            ListAll.Where(i => i.Code.Contains(ValidateUI.Code)).ToList().ForEach(i =>
            {
                List.Add(i);
            });
        }


        }
    }

