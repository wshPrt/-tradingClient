using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using HQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.ViewModel.Deal
{
   public class SellVM:ViewModelBase
    {
      
        public SellVM() 
        {
            List = new ObservableCollection<DealSellModel>();
            CmbUnitData();
            ValidateUI = new DealSellModel();
            sellHQ = new SellHQModel();
        }


        #region DataGrid
        private DealSellModel sell;
        public DealSellModel Sell
        {
            get { return sell; }
            set { sell = value; RaisePropertyChanged(() => Sell); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<DealSellModel> _list;
        public ObservableCollection<DealSellModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        /// <summary>
        /// 单元资金
        /// </summary>
        private DealSellModel _capital;
        public DealSellModel Capital 
        {
            get { return _capital; }
            set { _capital = value; }
        }
        //选中行
        private DealSellModel _selectedRow;
        public DealSellModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        /// <summary>
        /// 点击某行给卖出数量赋值
        /// </summary>
        private RelayCommand selectedCommand;
        public RelayCommand SelectedCommand
        {
            get
            {
                if (selectedCommand == null)
                {
                    selectedCommand = new RelayCommand(() => SelectedDown());
                }
                return selectedCommand;
            }
            set { selectedCommand = value; }
        }
        /// <summary>
        /// 验证用户界面
        /// </summary>
        private DealSellModel validateUI;
        public DealSellModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private DealSellModel cmbItem;
        public DealSellModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; 
                RaisePropertyChanged(() => CmbItem);
                if (value.UnitId > 0)
                {
                    UnitCapital();
                    List.Clear();
                    UnitPosition();
                } 
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<DealSellModel> cmbList;
        public ObservableCollection<DealSellModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }

        private SellHQModel sellHQ;
        public SellHQModel SellHQ 
        {
            get { return sellHQ; }
            set { sellHQ = value; RaisePropertyChanged(() => SellHQ); }
        }

        private SellHQModel sellHQPosition;
        public SellHQModel SellHQPosition
        {
            get { return sellHQPosition; }
            set { sellHQPosition = value; RaisePropertyChanged(() => SellHQPosition); }
        }
        
        /// <summary>
        /// 卖行情指令
        /// </summary>
        private RelayCommand sellHQCommand;
        public RelayCommand  SellHQCommand
        {
            get {
                if (sellHQCommand == null)
                {
                    sellHQCommand = new RelayCommand(() => Security());
                }
                return sellHQCommand; }
            set { sellHQCommand = value;}
        }
        #endregion
        /// <summary>
        /// 最大可卖数量
        /// </summary>
        private RelayCommand maxSellNumberCommnand;
        public RelayCommand MaxSellNumberCommand
        {
            get
            {
                if (maxSellNumberCommnand == null)
                {
                    maxSellNumberCommnand = new RelayCommand(() => MaxSellNumber());
                }
                return maxSellNumberCommnand;
            }
            set { maxSellNumberCommnand = value; }
        }
        #region 卖出指令
        /// <summary>
        /// 卖出
        /// </summary>
        private RelayCommand sellCommand;
        public RelayCommand SellCommand
        {
            get
            {
                if (sellCommand == null)
                {
                    sellCommand = new RelayCommand(() => SELL());
                }
                return sellCommand;
            }
            set { sellCommand = value; }
        }
        #endregion

        #region 点击五档行情值自动填充卖出价
        /// <summary>
        /// 卖五行情
        /// </summary>
        private RelayCommand mouseDownFiveCommand;
        public RelayCommand MouseDownFiveCommand
        {
            get
            {
                if (mouseDownFiveCommand == null)
                    mouseDownFiveCommand = new RelayCommand(() => MouseDownFive());
                return mouseDownFiveCommand;
            }
            set { mouseDownFiveCommand = value; }
        }
        private void MouseDownFive()
        {
            if (ValidateUI != null)
            {
                 ValidateUI.SellAveragePrice = SellHQ.Sell_5;
            }
        }
        /// <summary>
        ///卖四行情
        /// </summary>
        private RelayCommand mouseDownFourCommand;
        public RelayCommand MouseDownFourCommand
        {
            get
            {
                if (mouseDownFourCommand == null)
                    mouseDownFourCommand = new RelayCommand(() => MouseDownFour());
                return mouseDownFourCommand;
            }
            set { mouseDownFiveCommand = value; }
        }
        private void MouseDownFour()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Sell_4;
            }
        }
        /// <summary>
        ///卖三行情
        /// </summary>
        private RelayCommand mouseDownThreeCommand;
        public RelayCommand MouseDownThreeCommand
        {
            get
            {
                if (mouseDownThreeCommand == null)
                    mouseDownThreeCommand = new RelayCommand(() => MouseDownThree());
                return mouseDownThreeCommand;
            }
            set { mouseDownThreeCommand = value; }
        }
        private void MouseDownThree()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Sell_3;
            }
        }
        /// <summary>
        ///卖二行情
        /// </summary>
        private RelayCommand mouseDownTwoCommand;
        public RelayCommand MouseDownTwoCommand
        {
            get
            {
                if (mouseDownTwoCommand == null)
                    mouseDownTwoCommand = new RelayCommand(() => MouseDownTwo());
                return mouseDownTwoCommand;
            }
            set { mouseDownTwoCommand = value; }
        }
        private void MouseDownTwo()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Sell_2;
            }
        }
        /// <summary>
        ///卖一行情
        /// </summary>
        private RelayCommand mouseDownOneCommand;
        public RelayCommand MouseDownOneCommand
        {
            get
            {
                if (mouseDownOneCommand == null)
                    mouseDownOneCommand = new RelayCommand(() => MouseDownOne());
                return mouseDownOneCommand;
            }
            set { mouseDownOneCommand = value; }
        }
        private void MouseDownOne()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Sell_1;
            }
        }
        /// <summary>
        ///买一行情
        /// </summary>
        private RelayCommand mouseDownSellOneCommand;
        public RelayCommand MouseDownSellOneCommand
        {
            get
            {
                if (mouseDownSellOneCommand == null)
                    mouseDownSellOneCommand = new RelayCommand(() => MouseDownSellOne());
                return mouseDownSellOneCommand;
            }
            set { mouseDownSellOneCommand = value; }
        }
        private void MouseDownSellOne()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Buy_1;
            }
        }
        /// <summary>
        ///买二行情
        /// </summary>
        private RelayCommand mouseDownSellTwoCommand;
        public RelayCommand MouseDownSellTwoCommand
        {
            get
            {
                if (mouseDownSellTwoCommand == null)
                    mouseDownSellTwoCommand = new RelayCommand(() => MouseDownSellTwo());
                return mouseDownSellTwoCommand;
            }
            set { mouseDownSellTwoCommand = value; }
        }
        private void MouseDownSellTwo()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Buy_2;
            }
        }
        /// <summary>
        ///买三行情
        /// </summary>
        private RelayCommand mouseDownSellThreeCommand;
        public RelayCommand MouseDownSellThreeCommand
        {
            get
            {
                if (mouseDownSellThreeCommand == null)
                    mouseDownSellThreeCommand = new RelayCommand(() => MouseDownSellThree());
                return mouseDownSellThreeCommand;
            }
            set { mouseDownSellThreeCommand = value; }
        }
        private void MouseDownSellThree()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Buy_3;
            }
        }
        /// <summary>
        ///买四行情
        /// </summary>
        private RelayCommand mouseDownSellFourCommand;
        public RelayCommand MouseDownSellFourCommand
        {
            get
            {
                if (mouseDownSellFourCommand == null)
                    mouseDownSellFourCommand = new RelayCommand(() => MouseDownSellFour());
                return mouseDownSellFourCommand;
            }
            set { mouseDownSellFourCommand = value; }
        }
        private void MouseDownSellFour()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Buy_4;
            }
        }
        /// <summary>
        ///买五行情
        /// </summary>
        private RelayCommand mouseDownSellFiveCommand;
        public RelayCommand MouseDownSellFiveCommand
        {
            get
            {
                if (mouseDownSellFiveCommand == null)
                    mouseDownSellFiveCommand = new RelayCommand(() => MouseDownSellFive());
                return mouseDownSellFiveCommand;
            }
            set { mouseDownSellFiveCommand = value; }
        }
        private void MouseDownSellFive()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Buy_5;
            }
        }

        /// <summary>
        ///涨停 
        /// </summary>
        private RelayCommand dailyLimitCommand;
        public RelayCommand DailyLimitCommand
        {
            get
            {
                if (dailyLimitCommand == null)
                    dailyLimitCommand = new RelayCommand(() => DailyLimit());
                return dailyLimitCommand;
            }
            set { dailyLimitCommand = value; }
        }
        private void DailyLimit()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.LimitHigh;
            }
        }

        /// <summary>
        ///跌停 
        /// </summary>
        private RelayCommand fallStopCommand;
        public RelayCommand FallStopCommand
        {
            get
            {
                if (fallStopCommand == null)
                    fallStopCommand = new RelayCommand(() => FallStop());
                return fallStopCommand;
            }
            set { fallStopCommand = value; }
        }
        private void FallStop()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.LimitLow;
            }
        }

        /// <summary>
        ///昨收 
        /// </summary>
        private RelayCommand yesterdayCollectCommand;
        public RelayCommand YesterdayCollectCommand
        {
            get
            {
                if (yesterdayCollectCommand == null)
                    yesterdayCollectCommand = new RelayCommand(() => YesterdayCollect());
                return yesterdayCollectCommand;
            }
            set { yesterdayCollectCommand = value; }
        }
        private void YesterdayCollect()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Close_Prev;
            }
        }
        /// <summary>
        ///现价 
        /// </summary>
        private RelayCommand currentPriceCommand;
        public RelayCommand CurrentPriceCommand
        {
            get
            {
                if (currentPriceCommand == null)
                    currentPriceCommand = new RelayCommand(() => CurrentPrice());
                return currentPriceCommand;
            }
            set { currentPriceCommand = value; }
        }
        private void CurrentPrice()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Last;
            }
        }

        /// <summary>
        ///涨幅 
        /// </summary>
        private RelayCommand increaseCommand;
        public RelayCommand IncreaseCommand
        {
            get
            {
                if (increaseCommand == null)
                    increaseCommand = new RelayCommand(() => Increase());
                return increaseCommand;
            }
            set { increaseCommand = value; }
        }
        private void Increase()
        {
            if (ValidateUI != null)
            {
                ValidateUI.SellAveragePrice = SellHQ.Increase;
            }
        }

        /// <summary>
        /// 百分比
        /// </summary>
        private RelayCommand percentageCommand;
        public RelayCommand PercentageCommand 
        {
            get {
                if (percentageCommand == null)
                {
                    percentageCommand = new RelayCommand(() => Percentage());
                }
                return percentageCommand; }
            set { percentageCommand = value; }
        }

        /// <summary>
        /// 1/3
        /// </summary>
        private RelayCommand oneThirdCommand;
        public RelayCommand OneThirdCommand
        {
            get
            {
                if (oneThirdCommand == null)
                {
                    oneThirdCommand = new RelayCommand(() => OneThird());
                }
                return oneThirdCommand;
            }
            set { oneThirdCommand = value; }
        }

        /// <summary>
        /// 2/3
        /// </summary>
        private RelayCommand twoThirdCommand;
        public RelayCommand TwoThirdCommand
        {
            get
            {
                if (twoThirdCommand == null)
                {
                    twoThirdCommand = new RelayCommand(() => TwoThird());
                }
                return twoThirdCommand;
            }
            set { twoThirdCommand = value; }
        }

        /// <summary>
        /// 1/2
        /// </summary>
        private RelayCommand oneTwoCommand;
        public RelayCommand OneTwoCommand
        {
            get
            {
                if (oneTwoCommand == null)
                {
                    oneTwoCommand = new RelayCommand(() => OneTwo());
                }
                return oneTwoCommand;
            }
            set { oneTwoCommand = value; }
        }

        /// <summary>
        /// 进度百分比
        /// </summary>
        private RelayCommand progressRatioCommand;
        public RelayCommand ProgressRatioCommand
        {
            get
            {
                if (progressRatioCommand == null)
                {
                    progressRatioCommand = new RelayCommand(() => ProgressRatio());
                }
                return progressRatioCommand;
            }
            set { progressRatioCommand = value; }
        }

        /// <summary>
        /// 全部
        /// </summary>
        private RelayCommand wholeCommand;
        public RelayCommand WholeCommand
        {
            get
            {
                if (wholeCommand == null)
                {
                    wholeCommand = new RelayCommand(() => Whole());
                }
                return wholeCommand;
            }
            set { wholeCommand = value; }
        }
        #endregion

        /// <summary>
        /// 加载单元数据
        /// </summary>
        private void CmbUnitData() 
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
                    DealSellModel.UnitRoot data = JsonConvert.DeserializeObject<DealSellModel.UnitRoot>(jsonData);
                    CmbList = new ObservableCollection<DealSellModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new DealSellModel
                        {
                            SecuritiesCode = data.Data[i].Code,
                            UnitId = data.Data[i].id,
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

        /// <summary>
        /// 卖出
        /// </summary>
        private void SELL()
        {
            if (ValidateUI.SecuritiesCode == null)
            {
                MessageDialogManager.ShowDialogAsync("证券代码不能为空!");
                return;
            }
            if (ValidateUI.SellAveragePrice == null)
            {
                MessageDialogManager.ShowDialogAsync("卖出价格(元)不能为空!");
                return;
            }
            if (ValidateUI.SellAmount == null)
            {
                MessageDialogManager.ShowDialogAsync("入数量(股)不能为空!");
                return;
            }
            if (!(ValidateUI.SellAmount % 100 == 0))
            {
                MessageDialogManager.ShowDialogAsync("卖出数量(股)为一百的倍数!");
                return;
            }
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                //0是买，1是卖
                int type = 1;
                IDealSellInterface dealSell = new IDealSellInterface();
                var result = await Task.Run(() => dealSell.DealSell(CmbItem.UnitId, ValidateUI.SecuritiesCode, ValidateUI.SellAveragePrice, type, ValidateUI.SellAmount, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    List.Clear();
                    UnitPosition();
                    ValidateUI.SecuritiesCode = "";
                    MessageDialogManager.ShowDialogAsync("卖出下单成功!");
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }

        /// <summary>
        /// 卖行情
        /// </summary>
        DispatcherTimer sellTimer  = null;
        private void Security()
        {
              DispatcherHelper.CheckBeginInvokeOnUI( async () =>
                {
                    if (!(ValidateUI.SecuritiesCode == null))
                    {
                        if (ValidateUI.SecuritiesCode.Length == 6)
                        {
                            //定时查询 - 定时器
                            if (sellTimer is null)
                            {
                                sellTimer = new DispatcherTimer();
                                sellTimer.Tick += (s, e) =>
                                {
                                    MaxSellNumber();
                                    HQItem hq = HQService.Get(ValidateUI.SecuritiesCode);
                                    if (hq != null && hq.Close_Prev > 0)
                                    {
                                        SellHQ = new SellHQModel()
                                        {
                                            Code = hq.Code,
                                            Name = hq.Name,
                                            High = hq.High,
                                            Open = hq.Open,
                                            Low = hq.Low,
                                            Close = hq.Close,
                                            Close_Prev = hq.Close_Prev,
                                            Last = hq.Last,
                                            Volume = hq.Volume,
                                            Amount = hq.Amount,
                                            Buy_1 = hq.Buy_1,
                                            Buy_2 = hq.Buy_2,
                                            Buy_3 = hq.Buy_3,
                                            Buy_4 = hq.Buy_4,
                                            Buy_5 = hq.Buy_5,
                                            Buy_Volume_1 = hq.Buy_Volume_1,
                                            Buy_Volume_2 = hq.Buy_Volume_2,
                                            Buy_Volume_3 = hq.Buy_Volume_3,
                                            Buy_Volume_4 = hq.Buy_Volume_4,
                                            Buy_Volume_5 = hq.Buy_Volume_5,
                                            Sell_1 = hq.Sell_1,
                                            Sell_2 = hq.Sell_2,
                                            Sell_3 = hq.Sell_3,
                                            Sell_4 = hq.Sell_4,
                                            Sell_5 = hq.Sell_5,
                                            Sell_Volume_1 = hq.Sell_Volume_1,
                                            Sell_Volume_2 = hq.Sell_Volume_2,
                                            Sell_Volume_3 = hq.Sell_Volume_3,
                                            Sell_Volume_4 = hq.Sell_Volume_4,
                                            Sell_Volume_5 = hq.Sell_Volume_5,
                                            Time = hq.Time,
                                            Date = hq.Date,
                                            LimitHigh = hq.Limit_High,
                                            LimitLow = hq.Limit_Low,
                                            Increase = Math.Round(((hq.Last - hq.Close_Prev) / hq.Close_Prev), 3)
                                        };
                                    }
                                };
                                sellTimer.Interval = new TimeSpan(0, 0, 0, 3);
                            }
                            if (!sellTimer.IsEnabled)
                            {
                                sellTimer.Start();
                            }
                        }
                        else
                        {
                            SellHQ = new SellHQModel()
                            {
                                Code = null,
                                Name = null,
                                High = 0,
                                Open = 0,
                                Low = 0,
                                Close = 0,
                                Close_Prev = 0,
                                Last = 0,
                                Volume = 0,
                                Amount = 0,
                                Buy_1 = 0,
                                Buy_2 = 0,
                                Buy_3 = 0,
                                Buy_4 = 0,
                                Buy_5 = 0,
                                Buy_Volume_1 = 0,
                                Buy_Volume_2 = 0,
                                Buy_Volume_3 = 0,
                                Buy_Volume_4 = 0,
                                Buy_Volume_5 = 0,
                                Sell_1 = 0,
                                Sell_2 = 0,
                                Sell_3 = 0,
                                Sell_4 = 0,
                                Sell_5 = 0,
                                Sell_Volume_1 = 0,
                                Sell_Volume_2 = 0,
                                Sell_Volume_3 = 0,
                                Sell_Volume_4 = 0,
                                Sell_Volume_5 = 0,
                                Time = null,
                                Date = null
                            };
                        }
                    }
                });
      

        }

        /// <summary>
        /// 卖单元资金
        /// </summary>
        private void UnitCapital()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IUnitCapitalInterface unitCapital = new IUnitCapitalInterface();
                int unitId = CmbItem.UnitId;
                var result = await Task.Run(() => unitCapital.UnitCapital(unitId, token));
                string sucess = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (sucess == "成功")
                {
                    DealSellModel.capitalRoot data = JsonConvert.DeserializeObject<DealSellModel.capitalRoot>(jsonData);

                    CmbItem.ProfitLossRatio = data.Data.profit.ToString();
                    CmbItem.NewestMarketValue = data.Data.value;
                    CmbItem.Assets = data.Data.assets;
                    CmbItem.Balance = data.Data.balance;
                    CmbItem.Available = data.Data.available;
                    CmbItem.Scale = data.Data.scale;

                }

            });
        }

        DispatcherTimer dispatcherTimer = null;
        /// <summary>
        /// 单元持仓列表
        /// </summary>
        private void UnitPosition()
        {
            //定时查询 - 定时器
            if (dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                    {
                        if(CmbItem != null)
                        {
                            string token = UserToken.token;
                            int unitId = CmbItem.UnitId;
                            IUnitPositionInterface unitPosition = new IUnitPositionInterface();
                            var result = await Task.Run(() => unitPosition.UnitPosition(unitId, token));
                            string sucess = result["Message"]["Message"].ToString();
                            string jsonData = result["Message"].ToString();
                            if (sucess == "成功")
                            {
                                DealSellModel.PositionRoot data = JsonConvert.DeserializeObject<DealSellModel.PositionRoot>(jsonData);
                                for (int i = 0; i < data.Data.Count; i++)
                                {
                                    HQItem hq = HQService.Get(data.Data[i].code);
                                    if (!(hq == null))
                                    {
                                        SellHQPosition = new SellHQModel()
                                        {
                                            Last = hq.Last
                                        };
                                    }
                                    DealSellModel model = new DealSellModel()
                                    {
                                        SecuritiesCode = data.Data[i].code,
                                        SecuritiesName = data.Data[i].name,
                                        SecuritiesAmount = data.Data[i].count,
                                        MarketableAmount = data.Data[i].count_sellable,
                                        CurrentPrice = SellHQPosition.Last,
                                        ProfitLossRatio = ((SellHQPosition.Last - data.Data[i].price_cost) / data.Data[i].price_cost).ToString("P2"),
                                        FloatProfitLoss = Math.Round(((SellHQPosition.Last - data.Data[i].price_cost) * data.Data[i].count), 3),
                                        NewestMarketValue = SellHQPosition.Last * data.Data[i].count,
                                        BuyAmount = data.Data[i].count_today_buy,
                                        CostPrice = data.Data[i].price_cost,
                                        SellAmount = data.Data[i].count_today_sell,
                                        BuyAveragePrice = data.Data[i].price_cost_today_buy,
                                        BuyMoneyAmount = data.Data[i].price_cost_today_buy * data.Data[i].count_today_buy,
                                        SellAveragePrice = data.Data[i].price_cost_today_sell,
                                        SellMoneyAmount = data.Data[i].price_cost_today_sell * data.Data[i].count_today_sell,
                                        Account = data.Data[i].account_name,
                                        UnitId = data.Data[i].unit_id
                                    };

                                    var obj = List.ToList().Find(target => target.SecuritiesCode.Equals(data.Data[i].code));
                                    if (obj != null)
                                    {
                                        obj = model;
                                    }
                                    else
                                        List.Add(model);
                                }
                            }
                        }

                    });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            }
            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }

        /// <summary>
        /// 最大可卖值
        /// </summary>
        private void MaxSellNumber()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int unitId = CmbItem.UnitId;
                int account_id = 0;//主账户池ID
                int type = 1;
                IMaxSellNumberInterface maxSellNumber = new IMaxSellNumberInterface();
                var result = await Task.Run(() => maxSellNumber.MaxSellNumber(unitId, ValidateUI.SecuritiesCode, type, account_id, token));
                string sucess = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (sucess == "成功")
                {
                    DealBuyingModel.MaxBuyNumRoot data = JsonConvert.DeserializeObject<DealBuyingModel.MaxBuyNumRoot>(jsonData);
                    ValidateUI.MaxSellNumber = data.Data;
                }
            });
        }

        /// <summary>
        /// 自动填充买入数量
        /// </summary>
        private void SelectedDown()
        {
            if (SelectedRow != null)
            {
                ValidateUI.SecuritiesCode = SelectedRow.SecuritiesCode;
                ValidateUI.SellAveragePrice = SelectedRow.CurrentPrice;
                ValidateUI.SellAmount = SelectedRow.MarketableAmount;
            }
        }

        /// <summary>
        /// 输入百分比
        /// </summary>
        private void Percentage() 
        {
            if (ValidateUI.ProfitLossRatio != null)
            {
                double proportion = double.Parse(ValidateUI.ProfitLossRatio.TrimEnd('%')) / 100;
                ValidateUI.SellProgressRatio = double.Parse(proportion.ToString()) * 100;
                ValidateUI.BuyAmount = int.Parse((proportion * ValidateUI.MaxSellNumber).ToString());
            }
        }

        /// <summary>
        /// 1/3百分比
        /// </summary>
        private void OneThird()
        {
            double OneThird = 0.33;
            int proportion = 3; 
            int multiply = int.Parse(ValidateUI.MaxSellNumber.ToString()) / proportion;
            ValidateUI.ProfitLossRatio = OneThird.ToString("P2");
            ValidateUI.SellAmount = multiply / 100  * 100;
            ValidateUI.SellProgressRatio = 33;
        }

        /// <summary>
        /// 2/3百分比
        /// </summary>
        private void TwoThird()
        {
            double TwoThird = 0.66;
            int TwoThirdRatio = 3;
            int TwoThirdMultiply = (int.Parse(ValidateUI.MaxSellNumber.ToString()) / TwoThirdRatio) * 2;
            ValidateUI.ProfitLossRatio = TwoThird.ToString("P2");
            ValidateUI.SellAmount = TwoThirdMultiply / 100 * 100;
            ValidateUI.SellProgressRatio = 66;
        }

        /// <summary>
        /// 1/2 百分比
        /// </summary>
        private void OneTwo()
        {
            double OneTwo = 0.50;
            int OneTwoRatio = 2;
            int OneTwoMultiply = int.Parse(ValidateUI.MaxSellNumber.ToString()) / OneTwoRatio;
            ValidateUI.ProfitLossRatio = OneTwo.ToString("P2");
            ValidateUI.SellAmount = OneTwoMultiply / 100  * 100;
            ValidateUI.SellProgressRatio = 50;
        }

        /// <summary>
        /// 全部
        /// </summary>
        private void Whole()
        {
            double Whole = 1.0;
            int WholeRatio = 10;
            ValidateUI.ProfitLossRatio = Whole.ToString("P2");
            int WholeMultiply = int.Parse(ValidateUI.MaxSellNumber.ToString()) / WholeRatio;
            ValidateUI.SellAmount = WholeMultiply * WholeRatio;
            ValidateUI.SellProgressRatio = 100;
        }

        /// <summary>
        /// 进度百分比
        /// </summary>
        private void ProgressRatio()
        {
            if (ValidateUI.SellProgressRatio != 0)
            {
                double a = Math.Round(ValidateUI.SellProgressRatio);
                if (a != 0 && a >= 1)
                {
                    ValidateUI.ProfitLossRatio = (a / 100).ToString("P2");
                    double Percentage = Convert.ToDouble(ValidateUI.ProfitLossRatio.Replace("%", "")) / 100;
                    var Amount = ValidateUI.MaxSellNumber * Percentage;
                    ValidateUI.SellAmount = int.Parse(Amount.ToString())/100 * 100;
                }
            }
        }
    }
}
