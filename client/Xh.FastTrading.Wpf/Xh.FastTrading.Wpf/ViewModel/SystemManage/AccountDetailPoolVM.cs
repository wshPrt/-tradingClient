using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using System.IO;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
    public class AccountDetailPoolVM : ViewModelBase
    {
        public AccountDetailPoolVM()
        {
            //AccountPool = new AccountPoolModel();
            List = new ObservableCollection<AccountPoolModel>();
            InitDataGrid();
            ValidateUI = new Model.AccountPoolModel();

        }

        #region DataGird
        //private AccountPoolModel  accountPool;
        //public AccountPoolModel AccountPool
        //{
        //    get { return accountPool; }
        //    set { accountPool = value; RaisePropertyChanged(() => AccountPool); }
        //}

        /// <summary>
        /// DataGrid集合
        /// </summary>
        private ObservableCollection<AccountPoolModel> _list;
        public ObservableCollection<AccountPoolModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }
        public List<AccountPoolModel> ListAll = new List<AccountPoolModel>();
        /// <summary>
        /// 选中行
        /// </summary>
        private AccountPoolModel _selectedRow;
        public AccountPoolModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private AccountPoolModel cmbItem;
        public AccountPoolModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem); }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<AccountPoolModel> cmbList;
        public ObservableCollection<AccountPoolModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        #region 指令
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
        /// <summary>
        /// 用户界面验证
        /// </summary>
        private AccountPoolModel validateUI;
        public AccountPoolModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
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
                return refreshCommand;
            }
            set { refreshCommand = value; }
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
        /// 新增弹窗
        /// </summary>
        private RelayCommand addPopupCommand;
        public RelayCommand AddPopupCommand
        {
            get
            {
                if (addPopupCommand == null)
                {
                    addPopupCommand = new RelayCommand(() => AddPopup());
                }
                return addPopupCommand; }
            set { addPopupCommand = value; }
        }

        /// <summary>
        /// 修改弹窗
        /// </summary>
        private RelayCommand modifyAccountPopupCommand;
        public RelayCommand ModifyAccountPopupCommand
        {
            get
            {
                if (modifyAccountPopupCommand == null)
                {
                    modifyAccountPopupCommand = new RelayCommand(() => ModifyAccountPopup());
                }
                return modifyAccountPopupCommand; }
            set { modifyAccountPopupCommand = value; }
        }

        /// <summary>
        /// 账号和限额设置-弹窗
        /// </summary>
        private RelayCommand accountLimitSettingPopupCommand;
        public RelayCommand AccountLimitSettingPopupCommand
        {
            get
            {
                if (accountLimitSettingPopupCommand == null)
                {
                    accountLimitSettingPopupCommand = new RelayCommand(() => AccountLimitSettingPopup(SelectedRow));
                }
                return accountLimitSettingPopupCommand; }
            set { accountLimitSettingPopupCommand = value; }
        }

        /// <summary>
        /// 新增主账户池
        /// </summary>
        private RelayCommand addAccountPoolCommand;
        public RelayCommand AddAccountPoolCommand
        {
            get
            {
                if (addAccountPoolCommand == null)
                {
                    addAccountPoolCommand = new RelayCommand(() => AddAccountPool());
                }
                return addAccountPoolCommand; }
            set { addAccountPoolCommand = value; }
        }


        /// <summary>
        /// 账户和限额设置
        /// </summary>
        private RelayCommand accountLimitSettingCommand;
        public RelayCommand AccountLimitSettingCommand
        {
            get
            {
                if (accountLimitSettingCommand == null)
                {
                    AccountLimitSettingCommand = new RelayCommand(() => AccountLimitSetting());
                }
                return accountLimitSettingCommand; }
            set { accountLimitSettingCommand = value; }
        }


        /// <summary>
        /// 修改
        /// </summary>
        private RelayCommand modifyAccountCommand;
        public RelayCommand ModifyAccountCommand
        {
            get
            {
                if (modifyAccountCommand == null)
                {
                    modifyAccountCommand = new RelayCommand(() => ModifyAccount());
                }
                return modifyAccountCommand; }
            set { modifyAccountCommand = value; }
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
            set { delCommand = value; }
        }

        /// <summary>
        /// 限额
        /// </summary>
        private RelayCommand accountLimitCommmand;
        public RelayCommand AccountLimitCommand
        {
            get
            {
                if (accountLimitCommmand == null)
                {
                    accountLimitCommmand = new RelayCommand(() => AccountLimit());
                }
                return accountLimitCommmand; }
            set { accountLimitCommmand = value; }
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
                    searchCommand = new RelayCommand(() => Search());
                }
                return searchCommand;
            }
            set { searchCommand = value; }
        }
        #endregion

        /// <summary>
        /// 新增弹窗
        /// </summary>
        private void AddPopup()
        {
            MessageDialogManager.ShowAccountAddInfoView();
        }

        /// <summary>
        /// 修改弹窗
        /// </summary>
        private void ModifyAccountPopup()
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelectedRow.Code == null))
            {
                MessageDialogManager.ShowAccountModifyInfoView();
            }
        }

        /// <summary>
        /// 用户和限额设置-弹窗
        /// </summary>
        private void AccountLimitSettingPopup(AccountPoolModel accountPool)
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else
            {
                MessageDialogManager.ShowAccountLimitSettingView(accountPool);
            }

        }

        /// <summary>
        /// 加载主账户池数据
        /// </summary>
        private void InitDataGrid()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
          {
              string token = UserToken.token;
              IAccountPoolInterface accountPool = new IAccountPoolInterface();
              var reuslt = accountPool.AccountPool(token);
              string success = reuslt["Message"]["Message"].ToString();
              string jsonData = reuslt["Message"].ToString();
              if (success == "成功")
              {
                  AccountPoolModel.Root data = JsonConvert.DeserializeObject<AccountPoolModel.Root>(jsonData);
                  for (int i = 0; i < data.Data.Count; i++)
                  {

                      var m = new AccountPoolModel()
                      {
                          Id = data.Data[i].id,
                          Code = data.Data[i].code,

                          Name = data.Data[i].name,
                           // AccountId = data.Data[i].items.Select(i =>i.account_id),
                           AccountCode = string.Join(",", data.Data[i].items.Select(i => i.account_code)),
                          Items = data.Data[i].items,
                           // PriorityStrategy = data.Data[i].PriorityStrategy//优先策略

                           //AccountPoolAvailable = data.Data[i].itemsList[i].capitalAvailable,
                           //RelationUnit = data.Data[i].itemsList[i].sortBuy,
                           //UnitAvailable = data.Data[i].itemsList[i].sortSell
                       };
                      ListAll.Add(m);
                      List.Add(m);
                  }
              }
          });

        }

        /// <summary>
        /// 新增主账户池
        /// </summary>
        private void AddAccountPool()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               if (validateUI.IsValidated)
               {
                   string token = UserToken.token;
                   IAddAccountPoolInterface addAccountPool = new IAddAccountPoolInterface();
                   var reuslt = await Task.Run(() => addAccountPool.AddAccountPool(validateUI.Code, validateUI.Name, token));
                   string success = reuslt["Message"]["Message"].ToString();
                   if (success == "成功")
                   {
                       Refresh();
                       ToClose = true;
                       MessageDialogManager.ShowDialogAsync("主账户池新增成功!");
                       return;
                   }
                   else
                   {
                       MessageDialogManager.ShowDialogAsync(success);
                   }
               }
           });

        }

        /// <summary>
        /// 修改主账户池
        /// </summary>
        private void ModifyAccount()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    string token = UserToken.token;
                    IModifyAccountPoolInterface modifyAccountPool = new IModifyAccountPoolInterface();
                    var reuslt = await Task.Run(() => modifyAccountPool.ModifyAccountPool(SelectedRow.Id, SelectedRow.Code, SelectedRow.Name, token));
                    string success = reuslt["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("选中主账户池已修改成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 用户和限额设置
        /// </summary>
        private void AccountLimitSetting()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (!(List.Count <= 0))
                {
                    string token = UserToken.token;
                    var QuotaItems = MasterAccountModel.items;
                    var MasterId = MasterAccountModel.MasterId;
                    var PriorityStrategy = MasterAccountModel.PriorityStrategy;

                    IAccountPoolLimitSettingInterface accountPoolLimitSetting = new IAccountPoolLimitSettingInterface();
                    var result = await Task.Run(() => accountPoolLimitSetting.AccountPoolLimitSetting(MasterId, PriorityStrategy, QuotaItems, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("用户和限额设置成功!");
                        return;
                    }
                    else
                    {

                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 删除
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
                   IDelAccountPoolInterface accountPool = new IDelAccountPoolInterface();
                   var result = await Task.Run(() => accountPool.Del(SelectedRow.Id, SelectedRow.Code, token));
                   string success = result["Message"]["Message"].ToString();
                   if (success == "成功")
                   {
                       Refresh();
                       MessageDialogManager.ShowDialogAsync("选中主账号池已删除成功!");
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
        /// 设置限额
        /// </summary>
        private void AccountLimit()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                AccountDetailPoolVM objAccount = new AccountDetailPoolVM();
                if (!(objAccount.List.Count <= 0))
                {
                    string token = UserToken.token;
                    var QuotaItems = MasterAccountModel.items;
                    var MasterId = MasterAccountModel.MasterId;
                    var PriorityStrategy = MasterAccountModel.PriorityStrategy;

                    IAccountPoolLimitSettingInterface accountPoolLimitSetting = new IAccountPoolLimitSettingInterface();
                    var result = await Task.Run(() => accountPoolLimitSetting.AccountPoolLimitSetting(MasterId, PriorityStrategy, QuotaItems, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("限额设置成功!");

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
                    irow.CreateCell(2).SetCellValue("主账号");
                    irow.CreateCell(3).SetCellValue("优先策略");
                    irow.CreateCell(4).SetCellValue("账号池可用");
                    irow.CreateCell(5).SetCellValue("关联单元");
                    irow.CreateCell(6).SetCellValue("单元可用");
                         //第四步：for循环给sheet的每行添加数据
                         for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].Code);
                        row.CreateCell(1).SetCellValue(List[i].Name);
                        row.CreateCell(2).SetCellValue(List[i].AccountCode.ToString());
                             // row.CreateCell(3).SetCellValue(List[i].PriorityStrategy.ToString());
                             row.CreateCell(4).SetCellValue(List[i].AccountPoolAvailable.ToString());
                        row.CreateCell(5).SetCellValue(List[i].RelationUnit.ToString());
                        row.CreateCell(6).SetCellValue(List[i].UnitAvailable.ToString());

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
                        FileStream BookStream = new FileStream(saveFileDialog.FileName.ToString() + ".xls", FileMode.Create, FileAccess.Write);//定义文件流
                             book.Write(BookStream);//将工作薄写入文件流                  
                             BookStream.Seek(0, SeekOrigin.Begin); //输出之前调用Seek（偏移量，游标位置）方法：获取文件流的长度
                             BookStream.Close();
                        MessageDialogManager.ShowDialogAsync("主账户池导出成功!");
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


        /// <summary>
        /// 搜索
        /// </summary>
        private void Search()
        {
            List.Clear();
            ListAll.Where(i => i.Code.Contains(ValidateUI.Code)).ToList().ForEach(i =>
            {
                List.Add(i);
            });
        }
  
        }
    }
