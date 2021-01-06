using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views.UnitManage;
using static Xh.FastTrading.Wpf.Model.DealAutoMaticModel;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Deal
{
    public class AutoMaticVM : ViewModelBase
    {
        public AutoMaticVM()
        {
            AutoMatic = new DealAutoMaticModel();
            List = new BindingList<DealAutoMaticModel>();
            ValidateUI = new DealAutoMaticModel();
            InitUnitData();
            BuySellType = new ObservableCollection<BuySellTypModel>()
            {
                new BuySellTypModel(){Id=0,Name="买入"},
                new BuySellTypModel(){Id=1,Name="卖出"}
            };
            PriceType = new ObservableCollection<PriceTypeModel>()
            {
                new PriceTypeModel(){PriceId=0,PriceName="最新价"},
                new PriceTypeModel(){PriceId=1,PriceName="买一价"},
                new PriceTypeModel(){PriceId=2,PriceName="买二价"},
                new PriceTypeModel(){PriceId=3,PriceName="买三价"},
                new PriceTypeModel(){PriceId=4,PriceName="买四价"},
                new PriceTypeModel(){PriceId=5,PriceName="买五价"},

                new PriceTypeModel(){PriceId=-1,PriceName="卖一价"},
                new PriceTypeModel(){PriceId=-2,PriceName="卖二价"},
                new PriceTypeModel(){PriceId=-3,PriceName="卖三价"},
                new PriceTypeModel(){PriceId=-4,PriceName="卖四价"},
                new PriceTypeModel(){PriceId=-5,PriceName="卖五价"}
            };
            CmbListAccount = new ObservableCollection<DealAutoMaticModel>();
        }

        #region 买卖方向cmbox
        private ObservableCollection<DealAutoMaticModel.BuySellTypModel> _buySellType;
        public ObservableCollection<DealAutoMaticModel.BuySellTypModel> BuySellType
        {
            get { return _buySellType; }
            set { _buySellType = value; }
        }

        private DealAutoMaticModel.BuySellTypModel _sbuySellType;
        public DealAutoMaticModel.BuySellTypModel SbuySellType
        {
            get { return _sbuySellType; }
            set { _sbuySellType = value; }
        }
        #endregion

        #region 价格类型cmbox
        private ObservableCollection<DealAutoMaticModel.PriceTypeModel> _priceType;
        public ObservableCollection<DealAutoMaticModel.PriceTypeModel> PriceType
        {
            get { return _priceType; }
            set { _priceType = value; }
        }

        private DealAutoMaticModel.PriceTypeModel _spriceType;
        public DealAutoMaticModel.PriceTypeModel SPriceType
        {
            get { return _spriceType; }
            set { _spriceType = value; }
        }
        #endregion

        #region DataGrid
        private DealAutoMaticModel autoMatic;
        public DealAutoMaticModel AutoMatic
        {
            get { return autoMatic; }
            set { autoMatic = value; RaisePropertyChanged(() => AutoMatic); }
        }
        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private BindingList<DealAutoMaticModel> _list;
        public BindingList<DealAutoMaticModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }

        //选中行
        private DealAutoMaticModel _selectedRow;
        public DealAutoMaticModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private DealAutoMaticModel validateUI;
        public DealAutoMaticModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private DealAutoMaticModel cmbItem;
        public DealAutoMaticModel CmbItem
        {
            get { return cmbItem; }
            set
            {
                cmbItem = value;
                RaisePropertyChanged(() => CmbItem);
                if (value != null && value.Id > 0)
                {
                    List.Clear();
                    DealAutoData();
                    InitAutoMaticData();
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<DealAutoMaticModel> cmbList;
        public ObservableCollection<DealAutoMaticModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }


        #endregion

        #region 指令
        /// <summary>
        /// 新增弹窗指令
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
                return addPopupCommand;
            }
            set { addPopupCommand = value; }
        }

        /// <summary>
        /// 自动交易新增
        /// </summary>
        private RelayCommand addCommand;
        public RelayCommand AddCommmand
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
                if (modifyPopupCommand == null)
                {
                    modifyPopupCommand = new RelayCommand(() => ModifyPopup(SelectedRow));
                }
                return modifyPopupCommand;
            }
            set { modifyPopupCommand = value; }
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
        /// 启用停用
        /// </summary>
        private RelayCommand enableDisableCommand;
        public RelayCommand EnableDisableCommand
        {
            get
            {
                if (enableDisableCommand == null)
                {
                    enableDisableCommand = new RelayCommand(() => EnableDisable());
                }
                return enableDisableCommand;
            }
            set { enableDisableCommand = value; }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private RelayCommand delCommand;
        public RelayCommand DelCommand
        {
            get
            {
                if (delCommand == null)
                {
                    delCommand = new RelayCommand(() => DEL());
                }
                return delCommand;
            }
            set { delCommand = value; }
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
        /// 刷新
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
        #endregion

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

        #region 主账号下拉框
        /// <summary>
        /// 主账号下拉框
        /// </summary>
        private DealAutoMaticModel cmbItemAccount;
        public DealAutoMaticModel CmbItemAccount
        {
            get { return cmbItemAccount; }
            set { cmbItemAccount = value; RaisePropertyChanged(() => CmbItemAccount); }
        }

        /// <summary>
        /// 主账号下拉框列表
        /// </summary>
        private ObservableCollection<DealAutoMaticModel> cmbListAccount;
        public ObservableCollection<DealAutoMaticModel> CmbListAccount
        {
            get { return cmbListAccount; }
            set { cmbListAccount = value; RaisePropertyChanged(() => CmbListAccount); }
        }
        #endregion

        /// <summary>
        /// 加载单元combox数据
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               string token = UserToken.token;
               int userId = 0;
               IAutomaticInterface automatic = new IAutomaticInterface();
               var result = await Task.Run(() => automatic.UnitList(userId, token));
               string success = result["Message"]["Message"].ToString();
               string jsonData = result["Message"].ToString();
               if (success == "成功")
               {
                   DealAutoMaticModel.AccountRoot data = JsonConvert.DeserializeObject<DealAutoMaticModel.AccountRoot>(jsonData);
                   CmbList = new ObservableCollection<DealAutoMaticModel>();
                   for (int i = 0; i < data.Data.Count; i++)
                   {
                       CmbList.Add(new DealAutoMaticModel
                       {
                           SecuritiesCode = data.Data[i].code,
                           Id = data.Data[i].id,
                           SecuritiesName = data.Data[i].name
                       });
                   }
                   return;
               }
               else
               {
                   MessageDialogManager.ShowDialogAsync(success);
               }
           });
        }

        DispatcherTimer dispatcherTimer = null;
        /// <summary>
        /// 自动交易列表
        /// </summary>
        private void InitAutoMaticData()
        {
            //定时查询 - 定时器
            if (dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                     {
                         string token = UserToken.token;
                         int unitId = int.Parse(CmbItem.Id.ToString());
                         IAutomaticInterface automatic = new IAutomaticInterface();
                         var result = automatic.Automatic(unitId, token);
                         string success = result["Message"]["Message"].ToString();
                         string jsonData = result["Message"].ToString();
                         if (success == "成功")
                         {
                             DealAutoMaticModel.AutoMaticRoot data = JsonConvert.DeserializeObject<DealAutoMaticModel.AutoMaticRoot>(jsonData);
                             for (int i = 0; i < data.Data.Count; i++)
                             {
                                 DealAutoMaticModel model = new DealAutoMaticModel()
                                 {
                                     SecuritiesCode = data.Data[i].code,
                                     SecuritiesName = data.Data[i].name,
                                     Id = data.Data[i].id,
                                     UnitId = data.Data[i].unit_id,
                                     Status = data.Data[i].status,
                                     BuySellDirection = data.Data[i].price_type,
                                     MinInterval = data.Data[i].time_min,
                                     MaxInterval = data.Data[i].time_max,
                                     MinAmount = data.Data[i].count_min,
                                     MaxAmount = data.Data[i].count_max,
                                     PriceType = data.Data[i].price_type,
                                     MinPrice = data.Data[i].price_min,
                                     HighestPrice = data.Data[i].price_max,
                                     Account = data.Data[i].account_name,
                                     ExecuteNumber = data.Data[i].order_times,
                                     EntrustAmount = data.Data[i].order_count,
                                     AmountLimit = data.Data[i].count_total,
                                     NextExecuteTime = data.Data[i].time_next.ToString(),
                                     LastExecuteTime = data.Data[i].time_prev.ToString(),
                                     LastExecuteResult = data.Data[i].result_prev
                                 };

                                 var obj = List.ToList().Find(target => target.Id.Equals(data.Data[i].id));
                                 if (obj != null)
                                 {
                                     obj = model;
                                 }
                                 else
                                     List.Add(model);
                             }

                         }
                     });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);//每隔二秒刷新
            }
            if (!dispatcherTimer.IsEnabled) 
            {
                dispatcherTimer.Start();
            }
        }
                
        /// <summary>
        /// 新增弹窗
        /// </summary>
        private void AddPopup()
{
    MessageDialogManager.ShowAutomaticAdd();
}

#region 新增弹窗 令牌 发送()
private void ShowReceiveInfo(string msg)
{
    ReceiveInfo += msg + "\n";
}

private String receiveInfo;
public String ReceiveInfo
{
    get { return receiveInfo; }
    set { receiveInfo = value; RaisePropertyChanged(() => ReceiveInfo); }
}

private String sendInfo;
public String SendInfo
{
    get { return sendInfo; }
    set { sendInfo = value; RaisePropertyChanged(() => SendInfo); }
}

private RelayCommand sendCommand;
public RelayCommand SendCommand
{
    get
    {
        if (sendCommand == null)
        {
            sendCommand = new RelayCommand(() => ExcuteSendCommand());
        }
        return sendCommand;
    }
    set { sendCommand = value; }
}
private void ExcuteSendCommand()
{
    //用AutoMaticDetailView令牌，发送SendInfo字段
    Messenger.Default.Send<String>(SendInfo, "EntrustInfoView");
}

#endregion

/// <summary>
/// 新增
/// </summary>
private void Add()
{
    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
   {
       if (string.IsNullOrWhiteSpace(ValidateUI.SecuritiesCode))
       {
           MessageDialogManager.ShowDialogAsync("证券代码为空!");
           return;
       }
       if (!(ValidateUI.SecuritiesCode.Length == 6))
       {
           MessageDialogManager.ShowDialogAsync("请输入6位证券代码!");
           return;
       }
       if (string.IsNullOrWhiteSpace((ValidateUI.MinInterval).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("最小间隔为空!");
           return;
       }
       if (string.IsNullOrWhiteSpace((ValidateUI.MaxInterval).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("最小数量为空!");
           return;
       }
       if (string.IsNullOrWhiteSpace((ValidateUI.MaxAmount).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("最大数量为空!");
           return;
       }
       if (string.IsNullOrWhiteSpace((ValidateUI.MinPrice).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("最低限价为空!");
           return;
       }
       if (string.IsNullOrWhiteSpace((ValidateUI.HighestPrice).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("最高限价为空!");
           return;
       }
                //if (string.IsNullOrWhiteSpace(ValidateUI.Account))
                //{
                //    MessageDialogManager.ShowDialogAsync("主账号为空!");
                //    return;
                //}
                if (string.IsNullOrWhiteSpace((ValidateUI.AmountLimit).ToString()))
       {
           MessageDialogManager.ShowDialogAsync("数量限制为空!");
           return;
       }
       if (!(ValidateUI.MinInterval <= ValidateUI.MaxInterval))
       {
           MessageDialogManager.ShowDialogAsync("最小间隔大于最大间隔!");
           return;
       }
       if (!(ValidateUI.MinAmount <= ValidateUI.MaxAmount))
       {
           MessageDialogManager.ShowDialogAsync("最小数量大于最大数量!");
           return;
       }
       if (!(ValidateUI.MinPrice <= ValidateUI.HighestPrice))
       {
           MessageDialogManager.ShowDialogAsync("最低限价大于于最高限价!");
           return;
       }
       if (ValidateUI.IsValidated)
       {
           string toke = UserToken.token;
           IAutomaticAddInterface automaticAdd = new IAutomaticAddInterface();
           var result = await Task.Run(() => automaticAdd.AutomaticAdd
                   (ValidateUI.SecuritiesCode, SbuySellType.Id, CmbItem.UnitId, CmbItemAccount.MaticId,
                   ValidateUI.MinPrice, ValidateUI.HighestPrice, SPriceType.PriceId, ValidateUI.AmountLimit,
                   ValidateUI.MinAmount, ValidateUI.MaxAmount, ValidateUI.MinInterval, ValidateUI.MaxInterval,
                   toke));
           string success = result["Message"]["Message"].ToString();
           string jsonData = result["Message"].ToString();
           if (success == "成功")
           {
               ToClose = true;
               MessageDialogManager.ShowDialogAsync("委托计划新增成功!");
               Refresh();
               return;
           }
           else
           {
               MessageDialogManager.ShowDialogAsync(success);
           }
       }
   });
}

//private void ModifyPopupSend()
//{
//    //用AutoMaticDetailView令牌，发送SendInfo字段
//    Messenger.Default.Send<object>(SendInfo, "ModifyEntrustInfoView");
//}

/// <summary>
/// 修改弹窗
/// </summary>

private void ModifyPopup(Model.DealAutoMaticModel SelectedRow)
{
    if (SelectedRow == null)
    {
        MessageDialogManager.ShowDialogAsync("未选中呢!");
        return;
    }
    else
    {
        MessageDialogManager.ShowModifyAutomaticView(SelectedRow);
        Refresh();
    }
}

/// <summary>
/// 修改
/// </summary>
private void Modify()
{
    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
   {
       if (ValidateUI.IsValidated)
       {
           string token = UserToken.token;
           IAutomaticModifyInterface automaticModify = new IAutomaticModifyInterface();
                    //var result = await Task.Run(() => automaticModify.AutomaticModify
                    //(SelectedRow.SecuritiesCode, SelectedRow.MinPrice, SelectedRow.HighestPrice,decimal.Parse(SelectedRow.MinPrice),
                    //SelectedRow.AmountLimit, SelectedRow.MinAmount, SelectedRow.MaxAmount,SelectedRow.MinInterval,
                    //SelectedRow.MaxInterval, token));
                    //string success = result["Message"]["Message"].ToString();
                    //if (success == "成功")
                    //{
                    //    ToClose = true;
                    //    MessageDialogManager.ShowDialogAsync("选中委托计划修改成功！");
                    //    Refresh();
                    //    return;
                    //}
                    //else 
                    //{
                    //    MessageDialogManager.ShowDialogAsync(success);
                    //}
                }
   });

}

/// <summary>
/// 启用
/// </summary>
private void EnableDisable()
{
    if (SelectedRow == null)
    {
        MessageDialogManager.ShowDialogAsync("未选中!");
        return;
    }
    else if (!(SelectedRow == null))
    {
        DispatcherHelper.CheckBeginInvokeOnUI(async () =>
        {
            string token = UserToken.token;
                    //1表示启用;0表示停用
                    IAutomaticEnableDisableInterface automaticEnable = new IAutomaticEnableDisableInterface();
            var result = await Task.Run(() => automaticEnable.EnableDisable(SelectedRow.Id, SelectedRow.Status == 0 ? 1 : 0, token));
            string success = result["Message"]["Message"].ToString();
            if (success == "成功")
            {
                MessageDialogManager.ShowDialogAsync("委托计划状态已变更成功!");
                Refresh();
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
/// 删除
/// </summary>
private void DEL()
{
    if (SelectedRow == null)
    {
        MessageDialogManager.ShowDialogAsync("未选中!");
        return;
    }
    else if (!(SelectedRow == null))
    {
        DispatcherHelper.CheckBeginInvokeOnUI(async () =>
        {
            string token = UserToken.token;
            IDealDeleteInterface dealDelete = new IDealDeleteInterface();
            var result = await Task.Run(() => dealDelete.Deal(SelectedRow.Id, SelectedRow.UnitId, token));
            string success = result["Message"]["Message"].ToString();
            if (success == "成功")
            {
                Refresh();
                ToClose = true;
                MessageDialogManager.ShowDialogAsync("选中委托计划已删除成功!");
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
/// 刷新
/// </summary>
private void Refresh()
{
    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
   {
       List.Clear();
       InitAutoMaticData();
   });
}

/// <summary>
/// 加载账户combox数据
/// </summary>
private void DealAutoData()
{
    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
    {
        string token = UserToken.token;
        int unitId = int.Parse(CmbItem.Id.ToString());
        IDealAppointAccountInterface dealAppointAccount = new IDealAppointAccountInterface();
        var result = await Task.Run(() => dealAppointAccount.AppointAccountList(unitId, token));
        string success = result["Message"]["Message"].ToString();
        string jsonData = result["Message"].ToString();
        if (success == "成功")
        {
            DealAutoMaticModel.MaticAccountRoot data = JsonConvert.DeserializeObject<DealAutoMaticModel.MaticAccountRoot>(jsonData);

            for (int i = 0; i < data.Data.Count; i++)
            {
                CmbListAccount.Add(new DealAutoMaticModel
                {
                    MaticCode = data.Data[i].code,
                    MaticId = data.Data[i].id,
                    MaticName = data.Data[i].name
                });
            }
            return;

        }
        else
        {
            MessageDialogManager.ShowDialogAsync(success);
        }
    });
}

/// <summary>
/// 导出Excel
/// </summary>
private void ExportExcel()
{
    if (List == null)
    {
        MessageDialogManager.ShowDialogAsync("没记录无法导出!");
        return;
    }
    if (List.Count > 0)
    {
        DispatcherHelper.CheckBeginInvokeOnUI(async () =>
       {
                    //Excel表格的创建步骤
                    //第一步：创建Excel对象
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    //第二步：创建Excel对象的工作簿
                    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();
                    //第三步：Excel表头设置
                    //给sheet添加第一行的头部标题
                    NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);//创建行
                    row1.CreateCell(0).SetCellValue("状态");
           row1.CreateCell(1).SetCellValue("证券代码");
           row1.CreateCell(2).SetCellValue("证券名称");
           row1.CreateCell(3).SetCellValue("卖卖方向");
           row1.CreateCell(4).SetCellValue("最小间隔");
           row1.CreateCell(5).SetCellValue("最大间隔");
           row1.CreateCell(6).SetCellValue("最小数量");
           row1.CreateCell(7).SetCellValue("最大数量");
           row1.CreateCell(8).SetCellValue("价格类型");
           row1.CreateCell(9).SetCellValue("最低限价");
           row1.CreateCell(10).SetCellValue("最高限价");
           row1.CreateCell(11).SetCellValue("主账户");
           row1.CreateCell(12).SetCellValue("已执行次数");
           row1.CreateCell(13).SetCellValue("已委托数量");
           row1.CreateCell(14).SetCellValue("数量限制");
           row1.CreateCell(15).SetCellValue("下次执行时间");
           row1.CreateCell(16).SetCellValue("上次执行时间");
           row1.CreateCell(17).SetCellValue("上次执行结果");
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
           {
               NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
               row.CreateCell(0).SetCellValue(List[i].StartStr);
               row.CreateCell(1).SetCellValue(List[i].SecuritiesCode);
               row.CreateCell(2).SetCellValue(List[i].SecuritiesName);
               row.CreateCell(3).SetCellValue(List[i].DirectionStr);
               row.CreateCell(4).SetCellValue(double.Parse(List[i].MinInterval.ToString()));
               row.CreateCell(5).SetCellValue(double.Parse(List[i].MaxInterval.ToString()));
               row.CreateCell(6).SetCellValue(double.Parse(List[i].MinAmount.ToString()));
               row.CreateCell(7).SetCellValue(double.Parse(List[i].MaxAmount.ToString()));
               row.CreateCell(8).SetCellValue(List[i].TypeStr);
               row.CreateCell(9).SetCellValue(double.Parse(List[i].MinPrice.ToString()));
               row.CreateCell(10).SetCellValue(double.Parse(List[i].HighestPrice.ToString()));
               row.CreateCell(11).SetCellValue(List[i].Account);
               row.CreateCell(12).SetCellValue(double.Parse(List[i].ExecuteNumber.ToString()));
               row.CreateCell(13).SetCellValue(double.Parse(List[i].EntrustAmount.ToString()));
               row.CreateCell(14).SetCellValue(double.Parse(List[i].AmountLimit.ToString()));
               row.CreateCell(15).SetCellValue(List[i].NextExecuteTime);
               row.CreateCell(16).SetCellValue(List[i].LastExecuteTime);
               row.CreateCell(17).SetCellValue(List[i].LastExecuteResult);
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

       });
    }
}
    }
}
