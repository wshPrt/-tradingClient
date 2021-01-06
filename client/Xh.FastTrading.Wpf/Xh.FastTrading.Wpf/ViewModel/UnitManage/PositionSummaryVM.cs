using GalaSoft.MvvmLight;
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
using GalaSoft.MvvmLight.Command;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;
using HQ;

namespace Xh.FastTrading.Wpf.ViewModel.UnitManage
{
    public class PositionSummaryVM : ViewModelBase
    {
        public PositionSummaryVM()
        {
            PositionSummary = new PositionSummaryModel();
            ValidateUI = new PositionSummaryModel();
            List = new ObservableCollection<PositionSummaryModel>();
            InitDataGrid();
        }

        
        private PositionSummaryModel validateUI;
        public PositionSummaryModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }

        #region DataGrid
        private SellHQModel sellHQ;
        public SellHQModel SellHQ
        {
            get { return sellHQ; }
            set { sellHQ = value; RaisePropertyChanged(() => SellHQ); }
        }

        private PositionSummaryModel positionSummary;
        public PositionSummaryModel PositionSummary
        {
            get { return positionSummary; }
            set { positionSummary = value; RaisePropertyChanged(() => PositionSummary); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<PositionSummaryModel> _list;
        public ObservableCollection<PositionSummaryModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        public List<PositionSummaryModel> ListAll = new List<PositionSummaryModel>();

        //选中行
        private PositionSummaryModel _selectedRow;
        public PositionSummaryModel SelectedRow
        {
            get { return _selectedRow; }

            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
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
        private void Refresh()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
           {
               List.Clear();
               InitDataGrid();
           });
        }
        #endregion

        /// <summary>
        /// 持仓汇总数据加载
        /// </summary>
        private void InitDataGrid()
        {
           DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               string token = UserToken.token;
               IPositionSummaryInterface positionSummary = new IPositionSummaryInterface();
               var result = await Task.Run(() => positionSummary.PositionSummary(token));
               string success = result["Message"]["Message"].ToString();
               string jsonData = result["Message"].ToString();
               if (success == "成功")
               {
                   PositionSummaryModel.Root data = JsonConvert.DeserializeObject<PositionSummaryModel.Root>(jsonData);
                   for (int i = 0; i < data.Data.Count; i++)
                   {
                       //
                       //HQItem hq = HQService.Get(ValidateUI.SecuritiesCode);
                       //if (hq != null && hq.Close_Prev > 0)
                       //{
                       //    SellHQ = new SellHQModel()
                       //    {
                       //        Last = hq.Last,
                       //    };
                       //}
                  
                   var m = new PositionSummaryModel()
                       {
                           // UnitName = data.Data[i].unit_name.HasValue? data.Data[i].unit_name:0,
                           UnitName = data.Data[i].unit_name,
                           SecuritiesCode = data.Data[i].code.ToString(),
                           SecuritiesName = data.Data[i].name,
                           SercuritesAmount = data.Data[i].count,
                           ToSell = data.Data[i].count_sellable,
                           CostPrice = data.Data[i].price_cost,
                           CurrentPrice = data.Data[i].price_cost_today_buy,
                           // ProfitLossProportion = data.Data[i].price_cost_today_buy == 0 ? 0:(data.Data[i].price_cost / data.Data[i].price_cost_today_buy).ToString(),
                           //ProfitLossProportion = (data.Data[i].price_cost_today_buy  - data.Data[i].price_cost_today_sell)/ data.Data[i].price_cost_today_buy,
                           LatestMarketValue = data.Data[i].price_latest,
                           BuyAmount = data.Data[i].count_today_buy,
                           SellAmount = data.Data[i].count_today_sell,
                           Account = data.Data[i].account_name
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
                    irow.CreateCell(0).SetCellValue("单元名称");
                    irow.CreateCell(1).SetCellValue("证券代码");
                    irow.CreateCell(2).SetCellValue("证券名称");
                    irow.CreateCell(3).SetCellValue("证券数量");
                    irow.CreateCell(4).SetCellValue("可卖数量");
                    irow.CreateCell(5).SetCellValue("成本价");
                    irow.CreateCell(6).SetCellValue("当前价");
                    irow.CreateCell(7).SetCellValue("盈亏比例");
                    irow.CreateCell(8).SetCellValue("浮动盈亏");
                    irow.CreateCell(9).SetCellValue("最新市值");
                    irow.CreateCell(10).SetCellValue("今买数量");
                    irow.CreateCell(11).SetCellValue("今卖数量");
                    irow.CreateCell(12).SetCellValue("主账号");
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].UnitName);
                        row.CreateCell(1).SetCellValue(List[i].SecuritiesCode.ToString());
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesName.ToString());
                        row.CreateCell(3).SetCellValue(List[i].SercuritesAmount.ToString());
                        row.CreateCell(4).SetCellValue(List[i].ToSell.ToString());
                        row.CreateCell(5).SetCellValue(List[i].CostPrice.ToString());
                        row.CreateCell(6).SetCellValue(List[i].CurrentPrice.ToString());
                        row.CreateCell(7).SetCellValue(List[i].ProfitLossProportion.ToString());
                       // row.CreateCell(8).SetCellValue(List[i].FloatingProfitLoss.ToString());
                        row.CreateCell(9).SetCellValue(List[i].LatestMarketValue.ToString());
                        row.CreateCell(10).SetCellValue(List[i].BuyAmount.ToString());
                        row.CreateCell(11).SetCellValue(List[i].SellAmount.ToString());
                        row.CreateCell(12).SetCellValue(List[i].Account.ToString());

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
      