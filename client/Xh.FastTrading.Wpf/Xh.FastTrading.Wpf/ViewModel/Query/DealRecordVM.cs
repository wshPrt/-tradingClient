using GalaSoft.MvvmLight;
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
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.InterFace.Query;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Query
{
   public class DealRecordVM:ViewModelBase
    {
        public DealRecordVM()
        {
            QueryCapitalFlow = new QueryDealRecordModel();
            List = new ObservableCollection<QueryDealRecordModel>();
            ValidateUI = new QueryDealRecordModel();
            InitUnitData();
        }

        #region DataGrid
        private QueryDealRecordModel queryCapitalFlow;
        public QueryDealRecordModel QueryCapitalFlow
        {
            get { return queryCapitalFlow; }
            set { queryCapitalFlow = value; RaisePropertyChanged(() => QueryCapitalFlow); }
        }
        /// <summary>
        /// 验证用户界面
        /// </summary>
        private QueryDealRecordModel validateUI;
        public QueryDealRecordModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<QueryDealRecordModel> _list;
        public ObservableCollection<QueryDealRecordModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        //选中行
        private QueryDealRecordModel _selectedRow;
        public QueryDealRecordModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

        #region 下拉框
        private QueryDealRecordModel cmbItem;
        public QueryDealRecordModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem );
                if (value != null && value.Id > 0)
                {
                     List.Clear();
                     InitDataGrid();
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<QueryDealRecordModel> cmbList;
        public ObservableCollection<QueryDealRecordModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        private RelayCommand refreshCommmand ;
        public RelayCommand RefreshCommmand 
        {
            get {
                if (refreshCommmand == null)
                {
                    refreshCommmand = new RelayCommand(() => Refresh());
                }
                return refreshCommmand; }
            set { refreshCommmand = value; }
        }

        /// <summary>
        /// 开始结束时间
        /// </summary>
        private RelayCommand timeChangedCommand;
        public RelayCommand TimeChangedCommand 
        {
            get {
                if (timeChangedCommand==null)
                {
                    timeChangedCommand = new RelayCommand(() => InitDataGrid());
                }
                return timeChangedCommand; }
            set { timeChangedCommand = value; }
        }

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
        /// 加载成交记录别表数据
        /// </summary>
        DispatcherTimer dispatcherTimer = null;
        private void InitDataGrid()
        {
            //定时查询 - 定时器
            if (dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                {
                    string token = UserToken.token;
                    //单元Id
                    int unitId = CmbItem.Id;
                    string fromTime = ValidateUI.FromTime;
                    string toTime = ValidateUI.ToTime;
                    IDealRecordInterface dealRecord = new IDealRecordInterface();
                    var result = await Task.Run(() => dealRecord.DealRecord(unitId, fromTime, toTime, token));
                    string succecss = result["Message"]["Message"].ToString();
                    string jsonData = result["Message"].ToString();
                    if (succecss == "成功")
                    {
                        QueryDealRecordModel.Root data = JsonConvert.DeserializeObject<QueryDealRecordModel.Root>(jsonData);
                        for (int i = 0; i < data.Data.Count; i++)
                        {
                            QueryDealRecordModel model = new QueryDealRecordModel()
                            {
                                DealDateTime = data.Data[i].time.ToString("yyyy/MM/dd"),
                                DealTime = data.Data[i].time.ToString("T"),
                                SecuritiesCode = data.Data[i].code,
                                SecuritiesName = data.Data[i].name,
                                EntrustType = data.Data[i].type,
                                DealPrice = data.Data[i].price,
                                DealAmount = data.Data[i].count,
                                DealAmountMoney = data.Data[i].money,
                                Commission = data.Data[i].commission,
                                StampDuty = data.Data[i].price,
                                TransferFee = data.Data[i].management_fee,
                                DealNumber = data.Data[i].deal_no,
                                EntrustNumber = data.Data[i].order_no,
                                Account = data.Data[i].account_name,
                                IsNoInto = data.Data[i].transferred
                            };
    
                            var obj = List.ToList().Find(target => target.DealNumber.Equals(data.Data[i].deal_no));
                            if (obj != null)
                            {
                                obj = model;
                            }
                            else
                                List.Add(model);
                        }
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(succecss);
                    }
                });
                };
            }
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }

        /// <summary>
        /// 加载单元combox数据
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                string token = UserToken.token;
                int userId = 0;
                IAutomaticInterface automatic = new IAutomaticInterface();
                var result = await Task.Run(() => automatic.UnitList(userId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    QueryDealRecordModel.Root data = JsonConvert.DeserializeObject<QueryDealRecordModel.Root>(jsonData);
                    CmbList = new ObservableCollection<QueryDealRecordModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new QueryDealRecordModel
                        {
                            SecuritiesCode = data.Data[i].code,
                            SecuritiesName = data.Data[i].name,
                            Id =data.Data[i].id
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
        /// 刷新
        /// </summary>
        private void Refresh() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
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
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
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
    
                    irow.CreateCell(0).SetCellValue("成交日期");
                    irow.CreateCell(1).SetCellValue("证券代码");
                    irow.CreateCell(2).SetCellValue("证券名称");
                    irow.CreateCell(3).SetCellValue("委托类型");
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
                        row.CreateCell(3).SetCellValue(List[i].EntrustStr.ToString());
                        row.CreateCell(4).SetCellValue(List[i].DealPrice.ToString());
                        row.CreateCell(5).SetCellValue(List[i].DealAmount.ToString());
                        row.CreateCell(6).SetCellValue(List[i].DealAmountMoney.ToString());
                        row.CreateCell(7).SetCellValue(List[i].Commission.ToString());
                        row.CreateCell(8).SetCellValue(List[i].StampDuty.ToString());
                        row.CreateCell(9).SetCellValue(List[i].TransferFee.ToString());
                        row.CreateCell(10).SetCellValue(List[i].DealNumber.ToString());
                        row.CreateCell(11).SetCellValue(List[i].EntrustNumber.ToString());
                        row.CreateCell(12).SetCellValue(List[i].Account.ToString());
                        row.CreateCell(13).SetCellValue(List[i].IsNoIntoStr.ToString());

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
                        MessageDialogManager.ShowDialogAsync("成交记录导出成功!");
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
    }
}
