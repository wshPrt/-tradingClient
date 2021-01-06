using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UnitManage;
using Xh.FastTrading.Wpf.Untils;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.UnitManage
{
    public class EntrustSummaryVM : ViewModelBase
    {
        public EntrustSummaryVM()
        {
            EntrustSummary = new UnitManageEntrustSummaryModel();
            ValidateUI = new UnitManageEntrustSummaryModel();
            List = new ObservableCollection<UnitManageEntrustSummaryModel>();
            InitDataGird();
        }

        #region DataGrid
        private UnitManageEntrustSummaryModel entrustSummary;
        public UnitManageEntrustSummaryModel EntrustSummary
        {
            get { return entrustSummary; }
            set { entrustSummary = value; RaisePropertyChanged(() => EntrustSummary); }
        }

        private UnitManageEntrustSummaryModel validateUI;
        public UnitManageEntrustSummaryModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<UnitManageEntrustSummaryModel> _list;
        public ObservableCollection<UnitManageEntrustSummaryModel> List
        {
            get { return _list; }
            set { _list = value;RaisePropertyChanged(() => List);}
        }
        public List<UnitManageEntrustSummaryModel> ListAll = new List<UnitManageEntrustSummaryModel>();
        /// <summary>
        /// 选中行
        /// </summary>
        private UnitManageEntrustSummaryModel _selectedRow;
        public UnitManageEntrustSummaryModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

      
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
                return refreshCommand; }
            set { refreshCommand = value; }
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
        /// 导出
        /// </summary>
        private RelayCommand exportCommand;
        public RelayCommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand(() => ExportData());
                }
                return exportCommand; }
            set { exportCommand = value; }
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
        /// 导出Excel
        /// </summary>
        private RelayCommand excelCommand;
        public RelayCommand ExcelCommand
        {
            get
            {
                if (excelCommand == null)
                {
                    excelCommand = new RelayCommand(() => ExportData());
                }
                return excelCommand;
            }
            set { excelCommand = value; }
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

        /// <summary>
        /// 加载委托汇总列表
        /// </summary>
        private void InitDataGird()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                //1表示正常委托;2表示废单;3表示异常委托
                int status = 1;
                string FormDateTime = ValidateUI.FromTime;
                string ToDatetime = ValidateUI.ToDatetime;
                IEntrustAbnormalScrapListInterface entrustAbnormal = new IEntrustAbnormalScrapListInterface();
                var result = await Task.Run(() => entrustAbnormal.EntrustAbnormalScrapList(status, FormDateTime, ToDatetime, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    UnitManageEntrustSummaryModel.Root data = JsonConvert.DeserializeObject<UnitManageEntrustSummaryModel.Root>(jsonData);
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        var m = new UnitManageEntrustSummaryModel()
                        {
                            UnitName = data.Data[i].unit_name,
                            EntrustTime = data.Data[i].time.ToString("yyyy/MM/dd"),
                            SecuritiesCode = data.Data[i].code,
                            SercuritiesName = data.Data[i].name,
                            EntrustType = data.Data[i].type,
                            EntrustPrice = double.Parse(data.Data[i].price.ToString()),
                            EntrustAmount = data.Data[i].count,
                            DealPrice = data.Data[i].deal_average_price,
                            DelAmount = data.Data[i].trade_count,
                            CancelOrderAmount = data.Data[i].cancel_count,
                            StatusExplain = data.Data[i].state,
                            Account = data.Data[i].account_name,
                            Remarks = data.Data[i].remark,
                            PlaceOrdepUser = data.Data[i].user_name,
                            Id = data.Data[i].id,
                            EntrustNumber = data.Data[i].trade_no
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
            });
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            List.Clear();
            InitDataGird();
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
                InitDataGird();
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
               InitDataGird();
           });
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportData()
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
                    irow.CreateCell(0).SetCellValue("单元名称");
                    irow.CreateCell(1).SetCellValue("委托时间");
                    irow.CreateCell(2).SetCellValue("证券代码");
                    irow.CreateCell(3).SetCellValue("证券名称");
                    irow.CreateCell(4).SetCellValue("委托类型");
                    irow.CreateCell(5).SetCellValue("委托价格");
                    irow.CreateCell(6).SetCellValue("委托数量");
                    irow.CreateCell(7).SetCellValue("成交均价");
                    irow.CreateCell(8).SetCellValue("成交数量");
                    irow.CreateCell(9).SetCellValue("撤单数量");
                    irow.CreateCell(10).SetCellValue("状态说明");
                    irow.CreateCell(11).SetCellValue("主账号");
                    irow.CreateCell(12).SetCellValue("备注");
                    irow.CreateCell(13).SetCellValue("下单用户");
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].UnitName);
                        row.CreateCell(1).SetCellValue(List[i].EntrustTime);
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesCode);
                        row.CreateCell(3).SetCellValue(List[i].SercuritiesName);
                        row.CreateCell(4).SetCellValue(List[i].EntrustType);
                        row.CreateCell(5).SetCellValue(List[i].EntrustPrice.ToString());
                        row.CreateCell(6).SetCellValue(List[i].EntrustAmount.ToString());
                        row.CreateCell(7).SetCellValue(List[i].DealPrice.ToString());
                        row.CreateCell(8).SetCellValue(List[i].DelAmount.ToString());
                        row.CreateCell(9).SetCellValue(List[i].CancelOrderAmount.ToString());
                        //row.CreateCell(10).SetCellValue(List[i].StatusExplain.ToString());
                        //row.CreateCell(11).SetCellValue(List[i].Account.ToString());
                        // row.CreateCell(12).SetCellValue(List[i].Remarks.ToString());
                        row.CreateCell(13).SetCellValue(List[i].PlaceOrdepUser.ToString());

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
                        MessageDialogManager.ShowDialogAsync("废单汇总导出成功!");
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