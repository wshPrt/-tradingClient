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
   public class EntrustRecordVM: ViewModelBase
    {
        public EntrustRecordVM() 
        {
            EntrustRecord = new QueryEntrustRecordModel();
            List = new ObservableCollection<QueryEntrustRecordModel>();
            ValidateUI = new QueryEntrustRecordModel();
            InitUnitData();
        }

        #region DataGrid
        private QueryEntrustRecordModel entrustRecord;
        public QueryEntrustRecordModel EntrustRecord
        {
            get { return entrustRecord; }
            set { entrustRecord = value; RaisePropertyChanged(() => EntrustRecord); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<QueryEntrustRecordModel> _list;
        public ObservableCollection<QueryEntrustRecordModel> List
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        /// 验证用户界面
        /// </summary>
        private QueryEntrustRecordModel validateUI;
        public QueryEntrustRecordModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        //选中行
        private QueryEntrustRecordModel _selectedRow;
        public QueryEntrustRecordModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

        #region 下拉框列表
        private ObservableCollection<QueryEntrustRecordModel> cmbList;
        public ObservableCollection<QueryEntrustRecordModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }

        private QueryEntrustRecordModel cmbItem;
        public QueryEntrustRecordModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                    List.Clear();
                    InitDataGrid();
                }
            }
        }

        /// <summary>
        /// 刷新指令
        /// </summary>
        private RelayCommand refreshCommmand;
        public RelayCommand RefreshCommand 
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

        #endregion
        /// <summary>
        /// 当月
        /// </summary>
        private RelayCommand whenMonthCommand;
        public RelayCommand WhenMonthCommand
        {
            get {
                if (whenMonthCommand == null)
                {
                    whenMonthCommand = new RelayCommand(() => WhenMonth());
                }
                return whenMonthCommand; }
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
        /// 加载委托记录数据
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
                    string fromTime = ValidateUI.FromTime;
                    string toTime = ValidateUI.ToTime;
                    IEntrustRecordInterface entrustRecord = new IEntrustRecordInterface();
                    var result = await Task.Run(() => entrustRecord.EntrustRecord(CmbItem.UnitId, fromTime, toTime, token));
                    string success = result["Message"]["Message"].ToString();
                    string jsonData = result["Message"].ToString();
                    if (success == "成功")
                    {
                        QueryEntrustRecordModel.Root data = JsonConvert.DeserializeObject<QueryEntrustRecordModel.Root>(jsonData);
                        for (int i = 0; i < data.Data.Count; i++)
                        {
                            QueryEntrustRecordModel model = new QueryEntrustRecordModel()
                            {
                                EntrustTime = data.Data[i].time.ToString("yyy/MM/dd"),
                                DealTime = data.Data[i].time.ToString("T"),
                                SecuritiesCode = data.Data[i].code,
                                SecuritiesName = data.Data[i].name,
                                SecuritiesType = data.Data[i].type,
                                SecuritiesPrice = data.Data[i].price,
                                SecuritiesAmount = data.Data[i].count,
                                DealPrice = data.Data[i].deal_average_price,
                                DealAmount = data.Data[i].deal_count,
                                CancelOrderAmount = data.Data[i].cancel_count,
                                StatusExplain = data.Data[i].state,
                                TradeNumber = data.Data[i].trade_no,
                                Account = data.Data[i].account_name,
                                Remarks = data.Data[i].remark,
                                PlaceOrderUser = data.Data[i].user_name,
                                PlaceOrderChannel = data.Data[i].platform
                            };
     
                            var obj = List.ToList().Find(target => target.TradeNumber.Equals(data.Data[i].trade_no));

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
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                });
                };
            }
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);//每隔二秒刷新
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
                var result =  automatic.UnitList(userId, token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    QueryEntrustRecordModel.Root data = JsonConvert.DeserializeObject<QueryEntrustRecordModel.Root>(jsonData);
                    CmbList = new ObservableCollection<QueryEntrustRecordModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new QueryEntrustRecordModel
                        {
                            SecuritiesCode = data.Data[i].code,
                            SecuritiesName = data.Data[i].name,
                            UnitId= data.Data[i].id
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
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                List.Clear();
                ValidateUI.FromTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date).ToString();
                ValidateUI.ToTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)).ToString();
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
                    irow.CreateCell(0).SetCellValue("委托日期");
                    irow.CreateCell(1).SetCellValue("证券代码");
                    irow.CreateCell(2).SetCellValue("证券名称");
                    irow.CreateCell(3).SetCellValue("委托类型");
                    irow.CreateCell(4).SetCellValue("委托价格");
                    irow.CreateCell(5).SetCellValue("委托数量");
                    irow.CreateCell(6).SetCellValue("成交均价");
                    irow.CreateCell(7).SetCellValue("成交数量");
                    irow.CreateCell(8).SetCellValue("撤单数量");
                    irow.CreateCell(9).SetCellValue("状态说明");
                    irow.CreateCell(10).SetCellValue("委托编码");
                    irow.CreateCell(11).SetCellValue("主账号");
                    irow.CreateCell(12).SetCellValue("备注");
                    irow.CreateCell(13).SetCellValue("下单用户");
                    irow.CreateCell(14).SetCellValue("下单渠道");
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].EntrustTime);
                        row.CreateCell(1).SetCellValue(List[i].SecuritiesCode);
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesName.ToString());
                        row.CreateCell(3).SetCellValue(List[i].SecuritiesStr.ToString());
                        row.CreateCell(4).SetCellValue(List[i].SecuritiesPrice.ToString());
                        row.CreateCell(5).SetCellValue(List[i].SecuritiesAmount.ToString());
                        row.CreateCell(6).SetCellValue(List[i].DealPrice.ToString());
                        row.CreateCell(7).SetCellValue(List[i].DealAmount.ToString());
                        row.CreateCell(8).SetCellValue(List[i].CancelOrderAmount.ToString());
                        row.CreateCell(9).SetCellValue(List[i].StatusExplain.ToString());
                        row.CreateCell(10).SetCellValue(List[i].TradeNumber.ToString());
                        //row.CreateCell(11).SetCellValue(List[i].Account.ToString());
                        //row.CreateCell(12).SetCellValue(List[i].Remarks.ToString());
                        row.CreateCell(13).SetCellValue(List[i].PlaceOrderUser.ToString());
                        row.CreateCell(14).SetCellValue(List[i].PlaceOrderChannelStr.ToString());

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
                        //将工作薄写入文件流  
                        book.Write(BookStream);
                        //输出之前调用Seek（偏移量，游标位置）方法：获取文件流的长度
                        BookStream.Seek(0, SeekOrigin.Begin);
                        BookStream.Close();
                        MessageDialogManager.ShowDialogAsync("委托记录导出成功!");
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
