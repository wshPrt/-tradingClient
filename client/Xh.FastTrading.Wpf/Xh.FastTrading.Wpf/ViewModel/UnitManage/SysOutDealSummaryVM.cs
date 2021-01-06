using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UnitManage;
using Xh.FastTrading.Wpf.Untils;
using static Xh.FastTrading.Wpf.Model.DealSummaryModel;
using System.IO;
using System.Windows.Forms;

namespace Xh.FastTrading.Wpf.ViewModel.UnitManage
{
   public class SysOutDealSummaryVM: ViewModelBase,INotifyPropertyChanged
    {

        public SysOutDealSummaryVM() 
        {
            DealSummary = new DealSummaryModel();
            ValidateUI = new DealSummaryModel();
            List = new BindingList<DealSummaryModel>();

            AccountUnit();
            TradeAccount();
        }

        #region DataGrid
        private DealSummaryModel dealSummary;
        public DealSummaryModel DealSummary
        {
            get { return dealSummary; }
            set { dealSummary = value; RaisePropertyChanged(() => DealSummary); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private BindingList<DealSummaryModel> _list;
        public BindingList<DealSummaryModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }

        public List<DealSummaryModel> ListAll = new List<DealSummaryModel>();

        private DealSummaryModel validateUI;
        public DealSummaryModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }

        /// <summary>
        /// 选中行
        /// </summary>
        private DealSummaryModel _selectedRow;
        public DealSummaryModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        #endregion


        /// <summary>
        /// 当月
        /// </summary>
        private RelayCommand whenMonthCommand;
        public RelayCommand WhenMonthCommand
        {
            get
            {
                if (whenMonthCommand == null)
                {
                    whenMonthCommand = new RelayCommand(() => WhenMonth());
                }
                return whenMonthCommand;
            }
            set { whenMonthCommand = value; }
        }

        /// <summary>
        /// 当日
        /// </summary>
        private RelayCommand whenDayCommand;
        public RelayCommand WhenDayCommand
        {
            get
            {
                if (whenDayCommand == null)
                {
                    whenDayCommand = new RelayCommand(() => WhenDay());
                }
                return whenDayCommand;
            }
            set { whenDayCommand = value; }
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

        /// <summary>
        /// 转入单元
        /// </summary>
        private RelayCommand intoCommand;
        public RelayCommand IntoCommand
        {
            get 
            {
                if (intoCommand == null)
                {
                    intoCommand = new RelayCommand(() => Into());
                }
                return intoCommand; }
            set { intoCommand = value;}
        }

        #region 单元下拉框
        /// <summary>
        /// 单元下拉框选中信息
        /// </summary>
        private DealSummaryModel cmbItem;
        public DealSummaryModel CmbItem
        {
            get { return cmbItem; }
            set{cmbItem = value; RaisePropertyChanged(() => CmbItem);}
        }

        /// <summary>
        /// 单元下拉框列表
        /// </summary>
        private ObservableCollection<DealSummaryModel> cmbList;
        public ObservableCollection<DealSummaryModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        #region 交易账号下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private DealSummaryModel cmbTradeItem;
        public DealSummaryModel CmbTradeItem
        {
            get { return cmbTradeItem; }
            set
            {
                cmbTradeItem = value; RaisePropertyChanged(() => CmbTradeItem);
                if (value != null && value.TradeId > 0)
                {
                    List.Clear();
                    InitDataGrid();
                }
            }
        }

        /// <summary>
        ///  交易账号下拉框列表
        /// </summary>
        private ObservableCollection<DealSummaryModel> cmbTradeList;
        public ObservableCollection<DealSummaryModel> CmbTradeList
        {
            get { return cmbTradeList; }
            set { cmbTradeList = value; RaisePropertyChanged(() => CmbTradeList); }
        }
        #endregion

        /// <summary>
        /// 选择交易账号
        /// </summary>
        private void TradeAccount() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IWithinOutDealSummaryInterface unitListAccount = new IWithinOutDealSummaryInterface();
                var result = await Task.Run(() => unitListAccount.AccountFilterList(token));
                string success = result["Message"]["Message"].ToString();
                string jsonDate = result["Message"].ToString();
                if (success == "成功")
                {
                    DealSummaryModel.TradeRoot data = JsonConvert.DeserializeObject<DealSummaryModel.TradeRoot>(jsonDate);
                    CmbTradeList = new ObservableCollection<DealSummaryModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbTradeList.Add(new DealSummaryModel()
                        {
                            TradeId = data.Data[i].id,
                            TradeCode = data.Data[i].code,
                            TradeName = data.Data[i].name
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
        /// 系统外成交汇总列表
        /// </summary>
        private void InitDataGrid() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                //1表示系统内;2表示系统外
                int status = 2;
                if (CmbTradeItem != null)
                {
                    int tradeId = CmbTradeItem.Id;
                    string FormDateTime = ValidateUI.FromTime;
                    string ToDatetime = ValidateUI.ToTime;
                    IWithinOutDealSummaryInterface withinOutDelSummary = new IWithinOutDealSummaryInterface();
                    var result = await Task.Run(() => withinOutDelSummary.WithinOutDealSummary(status, FormDateTime, ToDatetime, tradeId, token));
                    string success = result["Message"]["Message"].ToString();
                    string jsonData = result["Message"].ToString();
                    if (success == "成功")
                    {
                        DealSummaryModel.Root data = JsonConvert.DeserializeObject<DealSummaryModel.Root>(jsonData);
                        for (int i = 0; i < data.Data.Count; i++)
                        {
                            var m = new DealSummaryModel()
                            {
                                Id = data.Data[i].id,
                                DealTime = data.Data[i].time.ToString("yyyy/MM/dd"),
                                SecuritiesCode = data.Data[i].code,
                                SecuritiesName = data.Data[i].name,
                                EntrustType = data.Data[i].type,
                                DealPrice = data.Data[i].price,
                                DealAmount = data.Data[i].count,
                                DealAmountMoney = data.Data[i].money,
                                Commission = data.Data[i].commission,
                                StampDuty = data.Data[i].management_fee,
                                TransferFee = data.Data[i].transferred,
                                DealNumber = data.Data[i].deal_no,
                                EntrustNumber = data.Data[i].order_no,
                                Account = data.Data[i].account_name,
                                IsNoInto = 1
                            };
                            ListAll.Add(m);
                            List.Add(m);
                        }
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
        /// 主账户所在的单元列表
        /// </summary>
        private void AccountUnit() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               string token = UserToken.token;
               IUnitListAccountInterface unitListAccount = new IUnitListAccountInterface();
               var result = await Task.Run(() => unitListAccount.UnitListAccount(token));
               string success = result["Message"]["Message"].ToString();
               string jsonDate = result["Message"].ToString();
               if (success == "成功")
               {
                   DealSummaryModel.UnitRoot data = JsonConvert.DeserializeObject<DealSummaryModel.UnitRoot>(jsonDate);
                   CmbList = new ObservableCollection<DealSummaryModel>();
                   for (int i = 0; i < data.Data.Count ; i++)
                   {
                       CmbList.Add( new DealSummaryModel()
                       {
                           UnitId = data.Data[i].id,
                           UnitCode = data.Data[i].code,
                           UnitNameTwo = data.Data[i].name
                       });
                   }
                   return ;
               }
               else 
               {
                   MessageDialogManager.ShowDialogAsync(success);
               }
           });
        }
      
        /// <summary>
        /// 转入单元
        /// </summary>
        private void Into() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (String.IsNullOrEmpty(CmbItem.UnitId.ToString()))
                {
                    MessageDialogManager.ShowTipsTrategyUnitView();
                    return;
                }
                if (SelectedRow == null)
                {
                    MessageDialogManager.ShowDialogAsync("未选中记录!");
                    return;
                }
                 if (!(String.IsNullOrEmpty(CmbItem.UnitId.ToString()))&& SelectedRow.DealNumber != null)
                {
                    string token = UserToken.token;
                    int unitId = CmbItem.UnitId;
                    string dealNo = SelectedRow.DealNumber;
                    ISysOutDealIntoUnitInterface outDealIntoUnit = new ISysOutDealIntoUnitInterface();
                    var result = await Task.Run(() => outDealIntoUnit.OutDealIntoUnit(unitId, dealNo, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("转入单元成功！");
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
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                List.Clear();
                InitDataGrid();
            });
        }

        /// <summary>
        /// 当月
        /// </summary>
        private void WhenMonth()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                List.Clear();
                ValidateUI.FromTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date).ToString();
                ValidateUI.ToTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)).ToString(); ;
                InitDataGrid();
            });
        }

        /// <summary>
        /// 当日
        /// </summary>
        private void WhenDay()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                List.Clear();
                ValidateUI.FromTime = DateTime.Now.ToString();
                ValidateUI.ToTime = DateTime.Now.ToString();
                InitDataGrid();
            });
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

                    irow.CreateCell(0).SetCellValue("成交时间");
                    irow.CreateCell(1).SetCellValue("证券代码");
                    irow.CreateCell(2).SetCellValue("委托名称");
                    irow.CreateCell(3).SetCellValue("证券类型");
                    irow.CreateCell(4).SetCellValue("成本价格");
                    irow.CreateCell(5).SetCellValue("成交数量");
                    irow.CreateCell(6).SetCellValue("成交金额");
                    irow.CreateCell(7).SetCellValue("佣金");
                    irow.CreateCell(8).SetCellValue("印花税");
                    irow.CreateCell(9).SetCellValue("过户费");
                    irow.CreateCell(10).SetCellValue("成交编号");
                    irow.CreateCell(11).SetCellValue("委托编号");
                    irow.CreateCell(12).SetCellValue("主账号");
                    irow.CreateCell(13).SetCellValue("是否转入");

                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].DealTime);
                        row.CreateCell(1).SetCellValue(List[i].SecuritiesCode);
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesName.ToString());
                        row.CreateCell(3).SetCellValue(List[i].EntrustType.ToString());
                        row.CreateCell(4).SetCellValue(List[i].DealPrice.ToString());
                        row.CreateCell(5).SetCellValue(List[i].DealAmount.ToString());
                        row.CreateCell(6).SetCellValue(List[i].DealAmountMoney.ToString());
                        row.CreateCell(7).SetCellValue(List[i].Commission.ToString());
                        row.CreateCell(8).SetCellValue(List[i].StampDuty.ToString());
                        row.CreateCell(9).SetCellValue(List[i].TransferFee.ToString());
                        row.CreateCell(10).SetCellValue(List[i].DealNumber.ToString());
                        row.CreateCell(11).SetCellValue(List[i].EntrustNumber.ToString());
                        row.CreateCell(12).SetCellValue(List[i].Account.ToString());
                        row.CreateCell(13).SetCellValue(List[i].IsNoInto.ToString());

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
                        MessageDialogManager.ShowDialogAsync("系统外成交汇总导出成功!");
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
            ListAll.Where(i => i.SecuritiesCode.Contains(ValidateUI.SecuritiesCode)).ToList().ForEach(i =>
            {
                List.Add(i);
            });

        }
    }

}
