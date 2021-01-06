using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.InterFace.Query;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using HQ;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Query
{
   public class AsetsVM:ViewModelBase
    {
        public AsetsVM() 
        {
            QueryAsets = new QueryAsetsModel();
            List = new ObservableCollection<QueryAsetsModel>();
            ValidateUI = new QueryAsetsModel();
            InitUnitData();

        }

        #region DataGrid
      
        private QueryAsetsModel validateUI;
        public QueryAsetsModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<QueryAsetsModel> _list;
        public ObservableCollection<QueryAsetsModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }
        /// <summary>
        /// 标签List
        /// </summary>
        private ObservableCollection<QueryAsetsModel> _labelList;
        public ObservableCollection<QueryAsetsModel> LabelList
        {
            get { return _labelList; }
            set { _labelList = value;RaisePropertyChanged(() => LabelList);}
        }
        private QueryAsetsModel queryAsets;
        public QueryAsetsModel QueryAsets
        {
            get { return queryAsets; }
            set { queryAsets = value; RaisePropertyChanged(() => QueryAsets); }
        }
        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<QueryAsetsModel> cmbList;
        public ObservableCollection<QueryAsetsModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }

        private QueryAsetsModel cmbItem;
        public QueryAsetsModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                    List.Clear();
                    ProfitLoss();
                    InitDataGrid();
                }
            }
        }
        //选中行
        private QueryAsetsModel _selectedRow;
        public QueryAsetsModel SelectedRow
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
            get {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(() => Refresh());
                }
                return refreshCommand; }
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
        /// 加载查询资产数据
        /// </summary>
        private void InitDataGrid() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               string token = UserToken.token;
               int unitId = CmbItem.UnitId;
               IAssetsInterface assets = new IAssetsInterface();
               var result = await Task.Run(() => assets.AssetsUnitCapital(unitId, token));
               string succes = result["Message"]["Message"].ToString();
               string jsonData = result["Message"].ToString();
               if (succes == "成功")
               {
                   QueryAsetsModel.capitalRoot data = JsonConvert.DeserializeObject<QueryAsetsModel.capitalRoot>(jsonData);
                   ValidateUI.Balance = data.Data.balance;
                   ValidateUI.Available = data.Data.available;
                   ValidateUI.Market = data.Data.value;
                   ValidateUI.Assets = data.Data.assets;
                   ValidateUI.ProfitLoss = data.Data.profit;
                   ValidateUI.Scale = data.Data.scale;
               }
               else 
               {
                   MessageDialogManager.ShowDialogAsync(succes);
               }
           });
        }
        private BuyHQModel buyHQ;
        public BuyHQModel BuyHQ
        {
            get { return buyHQ; }
            set { buyHQ = value; RaisePropertyChanged(() => BuyHQ); }
        }

        DispatcherTimer dispatcherTimer = null;
        private void ProfitLoss()
        {
            //定时查询 - 定时器
            if (dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                  {
                //添加DataGrid数据
                //盈亏比例 =  (市价-成本价）/成本价 
                string token = UserToken.token;
                int unitId = CmbItem.UnitId;
                IAssetsInterface assets = new IAssetsInterface();
                var resultPosition = await Task.Run(() => assets.AssetsUnitPositionList(unitId, token));
                string succesPosition = resultPosition["Message"]["Message"].ToString();
                string jsonPosition = resultPosition["Message"].ToString();
                if (succesPosition == "成功")
                {
                    QueryAsetsModel.positionRoot data = JsonConvert.DeserializeObject<QueryAsetsModel.positionRoot>(jsonPosition);

                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        if (data.Data[i].code != null)
                        {
                            HQItem hq = HQService.Get(data.Data[i].code);
                            if (hq != null)
                            {
                                BuyHQ = new BuyHQModel()
                                {
                                    Last = hq.Last
                                };
                            }
                        }

                       QueryAsetsModel model = new QueryAsetsModel()
                       {
                           SecuritiesCode = data.Data[i].code,
                           ScuritiesName = data.Data[i].name,
                           SecuritiesAmount = data.Data[i].count,
                           MarketableAmount = data.Data[i].count_sellable,
                           CostPrice = data.Data[i].price_cost,
                           CurrentPrice = BuyHQ.Last,
                           BuyAmountMoney = data.Data[i].price_cost_today_sell * data.Data[i].price_cost_today_buy,
                           ProfitLossRatio = data.Data[i].count <= 0 ? 0 : decimal.Round((BuyHQ.Last - data.Data[i].price_cost) / data.Data[i].price_cost, 4),
                           MarketValue = decimal.Round(BuyHQ.Last * data.Data[i].count, 2),
                           BuyAmount = data.Data[i].count_today_buy,
                           AveragePrice = data.Data[i].price_cost_today_buy,
                           SellAmount = data.Data[i].count_today_sell,
                           SellAmountMoney = data.Data[i].price_cost_today_sell * data.Data[i].price_cost_today_buy,
                           SellAveragePrice = data.Data[i].price_cost_today_sell,
                           Account = data.Data[i].account_name
                       };
  
                            var obj = List.ToList().Find(target => target.SecuritiesCode.Equals(data.Data[i].code));
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
                     MessageDialogManager.ShowDialogAsync(succesPosition);
                }
                });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            }
            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }

        /// <summary>
        /// 加载cmbox单元
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int UserId = 0;
                IDealBuyInterface dealBuy = new IDealBuyInterface();
                var result = await Task.Run(() => dealBuy.UnitList(UserId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    QueryAsetsModel.positionRoot data = JsonConvert.DeserializeObject<QueryAsetsModel.positionRoot>(jsonData);
                    CmbList = new ObservableCollection<QueryAsetsModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new QueryAsetsModel
                        {
                            SecuritiesCode = data.Data[i].code,
                            ScuritiesName = data.Data[i].name,
                            UnitId = data.Data[i].id
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
                ProfitLoss();
                InitDataGrid();
            });
          }


        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel()
        {
            if (List == null|| List.Count ==0)
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
                            row1.CreateCell(0).SetCellValue("证券代码");
                            row1.CreateCell(1).SetCellValue("证券名称");
                            row1.CreateCell(2).SetCellValue("证券数量");
                            row1.CreateCell(3).SetCellValue("可卖数量");
                            row1.CreateCell(4).SetCellValue("成本");
                            row1.CreateCell(5).SetCellValue("当前价");
                            row1.CreateCell(6).SetCellValue("亏盈比例");
                            row1.CreateCell(7).SetCellValue("最新市值");
                            row1.CreateCell(8).SetCellValue("今买数量");
                            row1.CreateCell(9).SetCellValue("今买金额");
                            row1.CreateCell(10).SetCellValue("今买均价");
                            row1.CreateCell(11).SetCellValue("今卖数量");
                            row1.CreateCell(12).SetCellValue("今卖金额");
                            row1.CreateCell(13).SetCellValue("今卖均价");
                            row1.CreateCell(14).SetCellValue("主账号");
                            //第四步：for循环给sheet的每行添加数据
                            for (int i = 0; i < List.Count; i++)
                            {
                                NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                                row.CreateCell(0).SetCellValue(List[i].SecuritiesCode);
                                row.CreateCell(1).SetCellValue(List[i].ScuritiesName);
                                row.CreateCell(2).SetCellValue(double.Parse(List[i].SecuritiesAmount.ToString()));
                                row.CreateCell(3).SetCellValue(double.Parse(List[i].MarketableAmount.ToString()));
                                row.CreateCell(4).SetCellValue(double.Parse(List[i].CostPrice.ToString()));
                                row.CreateCell(5).SetCellValue(double.Parse(List[i].CurrentPrice.ToString()));
                                row.CreateCell(6).SetCellValue(double.Parse(List[i].ProfitLossRatio.ToString()));
                                row.CreateCell(7).SetCellValue(double.Parse(List[i].MarketValue.ToString()));
                                row.CreateCell(8).SetCellValue(double.Parse(List[i].BuyAmount.ToString()));
                                //row.CreateCell(9).SetCellValue(double.Parse(List[i].BuyAmountMoney.ToString()));
                                row.CreateCell(10).SetCellValue(double.Parse(List[i].AveragePrice.ToString()));
                                row.CreateCell(11).SetCellValue(double.Parse(List[i].SellAmount.ToString()));
                                row.CreateCell(12).SetCellValue(double.Parse(List[i].SellAmountMoney.ToString()));
                                row.CreateCell(13).SetCellValue(double.Parse(List[i].SellAveragePrice.ToString()));
                                row.CreateCell(14).SetCellValue(double.Parse(List[i].Account.ToString()));
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
                                MessageDialogManager.ShowDialogAsync("资产记录导出成功!");
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
