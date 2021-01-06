using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using HQ;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.ViewModel.Deal
{
    public class BuyingVM : ViewModelBase
    {

        public BuyingVM()
        {
            ValidateUI = new DealBuyingModel();
            buyHQ = new BuyHQModel();
            Lists = new ObservableCollection<DealBuyingModel>();
            InitUnitData();

            UnitPosition();
        }

        #region DataGrid
        private DealBuyingModel buying;
        public DealBuyingModel Buy
        {
            get { return buying; }
            set { buying = value; RaisePropertyChanged(() => Buy); }
        }

        private ObservableCollection<DealBuyingModel> _updateList;
        public ObservableCollection<DealBuyingModel> UpdateList
        {
            get { return _updateList; }
            set { _updateList = value; RaisePropertyChanged(() => UpdateList); }
        }
        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<DealBuyingModel> _list;
        public ObservableCollection<DealBuyingModel> Lists
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => Lists); }
        }

        /// <summary>
        /// 单元资金
        /// </summary>
        private DealBuyingModel _capital;
        public DealBuyingModel Capital
        {
            get { return _capital; }
            set { _capital = value; }
        }

        /// <summary>
        /// 单元资金
        /// </summary>
        private RelayCommand unitCapitalCommand;
        public RelayCommand UnitCapitalCommand
        {
            get
            {
                if (unitCapitalCommand == null)
                {
                    unitCapitalCommand = new RelayCommand(() => UnitCapital());
                }
                return unitCapitalCommand;
            }
            set { unitCapitalCommand = value; }
        }

        //选中行
        private DealBuyingModel _selectedRow;
        public DealBuyingModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        /// <summary>
        /// 点击某行给买入数量赋值
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
        /// 买入行情Model
        /// </summary>
        private BuyHQModel buyHQ;
        public BuyHQModel BuyHQ
        {
            get { return buyHQ; }
            set { buyHQ = value; RaisePropertyChanged(() => BuyHQ); }
        }

        /// <summary>
        /// 持仓买入行情Model
        /// </summary>
        private BuyHQModel buyHQPosition;
        public BuyHQModel BuyHQPosition
        {
            get { return buyHQPosition; }
            set { buyHQPosition = value; RaisePropertyChanged(() => BuyHQPosition); }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private DealBuyingModel validateUI;
        public DealBuyingModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private DealBuyingModel cmbItem;
        public DealBuyingModel CmbItem
        {
            get { return cmbItem; }
            set
            {
                cmbItem = value;
                RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                    Lists.Clear();
                    UnitCapital();

                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<DealBuyingModel> cmbList;
        public ObservableCollection<DealBuyingModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        #region 买入指令
        /// <summary>
        /// 买入
        /// </summary>
        private RelayCommand buyCommand;
        public RelayCommand BuyCommand
        {
            get
            {
                if (buyCommand == null)
                {
                    buyCommand = new RelayCommand(() => Buying(), CanExcute);
                }
                return buyCommand;
            }
            set { buyCommand = value; }
        }
        #endregion

        /// <summary>
        /// 证券代码
        /// </summary>
        private RelayCommand securityCommand;
        public RelayCommand SecurityCommand
        {
            get
            {
                if (securityCommand == null)
                {
                    securityCommand = new RelayCommand(() => Security());
                }
                return securityCommand;
            }
            set { securityCommand = value; }
        }

        /// <summary>
        /// 最大可买数量
        /// </summary>
        private RelayCommand maxBuyNumberCommnand;
        public RelayCommand MaxBuyNumberCommand
        {
            get
            {
                if (maxBuyNumberCommnand == null)
                {
                    maxBuyNumberCommnand = new RelayCommand(() => MaxBuyNumber());
                }
                return maxBuyNumberCommnand;
            }
            set { maxBuyNumberCommnand = value; }
        }

        #region 点击五档行情值自动填充买入价
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
                ValidateUI.CurrentPrice = BuyHQ.Sell_5;
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
                ValidateUI.CurrentPrice = BuyHQ.Sell_4;
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
                ValidateUI.CurrentPrice = BuyHQ.Sell_3;
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
                ValidateUI.CurrentPrice = BuyHQ.Sell_2;
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
                ValidateUI.CurrentPrice = BuyHQ.Sell_1;
            }
        }
        /// <summary>
        ///买一行情
        /// </summary>
        private RelayCommand mouseDownBuyOneCommand;
        public RelayCommand MouseDownBuyOneCommand
        {
            get
            {
                if (mouseDownBuyOneCommand == null)
                    mouseDownBuyOneCommand = new RelayCommand(() => MouseDownBuyOne());
                return mouseDownBuyOneCommand;
            }
            set { mouseDownBuyOneCommand = value; }
        }
        private void MouseDownBuyOne()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Buy_1;
            }
        }
        /// <summary>
        ///买二行情
        /// </summary>
        private RelayCommand mouseDownBuyTwoCommand;
        public RelayCommand MouseDownBuyTwoCommand
        {
            get
            {
                if (mouseDownBuyTwoCommand == null)
                    mouseDownBuyTwoCommand = new RelayCommand(() => MouseDownBuyTwo());
                return mouseDownBuyTwoCommand;
            }
            set { mouseDownBuyTwoCommand = value; }
        }
        private void MouseDownBuyTwo()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Buy_2;
            }
        }
        /// <summary>
        ///买三行情
        /// </summary>
        private RelayCommand mouseDownBuyThreeCommand;
        public RelayCommand MouseDownBuyThreeCommand
        {
            get
            {
                if (mouseDownBuyThreeCommand == null)
                    mouseDownBuyThreeCommand = new RelayCommand(() => MouseDownBuyThree());
                return mouseDownBuyThreeCommand;
            }
            set { mouseDownBuyThreeCommand = value; }
        }
        private void MouseDownBuyThree()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Buy_3;
            }
        }
        /// <summary>
        ///买四行情
        /// </summary>
        private RelayCommand mouseDownBuyFourCommand;
        public RelayCommand MouseDownBuyFourCommand
        {
            get
            {
                if (mouseDownBuyFourCommand == null)
                    mouseDownBuyFourCommand = new RelayCommand(() => MouseDownBuyFour());
                return mouseDownBuyFourCommand;
            }
            set { mouseDownBuyFourCommand = value; }
        }
        private void MouseDownBuyFour()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Buy_4;
            }
        }
        /// <summary>
        ///买五行情
        /// </summary>
        private RelayCommand mouseDownBuyFiveCommand;
        public RelayCommand MouseDownBuyFiveCommand
        {
            get
            {
                if (mouseDownBuyFiveCommand == null)
                    mouseDownBuyFiveCommand = new RelayCommand(() => MouseDownBuyFive());
                return mouseDownBuyFiveCommand;
            }
            set { mouseDownBuyFiveCommand = value; }
        }
        private void MouseDownBuyFive()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Buy_5;
            }
        }

        /// <summary>
        /// 涨停
        /// </summary>
        private RelayCommand buyRiseStopCommand;
        public RelayCommand BuyRiseStopCommand
        {
            get
            {
                if (buyRiseStopCommand == null)
                {
                    buyRiseStopCommand = new RelayCommand(() => BuyRiseStop());
                }

                return buyRiseStopCommand;
            }
            set { buyRiseStopCommand = value; }
        }
        private void BuyRiseStop()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.LimitHigh;
            }
        }

        /// <summary>
        /// 输入百分比
        /// </summary>
        private RelayCommand percentageCommand;
        public RelayCommand PercentageCommand
        {
            get
            {
                if (percentageCommand == null)
                {
                    percentageCommand = new RelayCommand(() => Percentage());
                }
                return percentageCommand;
            }
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
        /// 跌停
        /// </summary>
        private RelayCommand buyFallStopCommand;
        public RelayCommand BuyFallStopCommand
        {
            get
            {
                if (buyFallStopCommand == null)
                {
                    buyFallStopCommand = new RelayCommand(() => BuyFallStop());
                }

                return buyFallStopCommand;
            }
            set { buyFallStopCommand = value; }
        }
        private void BuyFallStop()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.LimitLow;
            }
        }

        /// <summary>
        /// 昨收
        /// </summary>
        private RelayCommand buyYesterdayCollectCommand;
        public RelayCommand BuyYesterdayCollectCommand
        {
            get
            {
                if (buyYesterdayCollectCommand == null)
                {
                    buyYesterdayCollectCommand = new RelayCommand(() => BuyYesterdayCollect());
                }

                return buyYesterdayCollectCommand;
            }
            set { buyYesterdayCollectCommand = value; }
        }
        private void BuyYesterdayCollect()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Close_Prev;
            }
        }

        /// <summary>
        /// 现价
        /// </summary>
        private RelayCommand buyCurrentPriceCommand;
        public RelayCommand BuyCurrentPriceCommand
        {
            get
            {
                if (buyCurrentPriceCommand == null)
                {
                    buyCurrentPriceCommand = new RelayCommand(() => BuyCurrentPrice());
                }

                return buyCurrentPriceCommand;
            }
            set { buyCurrentPriceCommand = value; }
        }
        private void BuyCurrentPrice()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = BuyHQ.Last;
            }
        }

        /// <summary>
        /// 涨幅
        /// </summary>
        private RelayCommand buyIncreaseCommand;
        public RelayCommand BuyIncreaseCommand
        {
            get
            {
                if (buyIncreaseCommand == null)
                {
                    buyIncreaseCommand = new RelayCommand(() => BuyIncrease());
                }

                return buyIncreaseCommand;
            }
            set { buyIncreaseCommand = value; }
        }
        private void BuyIncrease()
        {
            if (ValidateUI != null)
            {
                ValidateUI.CurrentPrice = decimal.Parse(BuyHQ.Increase.Replace("%", "")) / 100;
            }
        }

        /// <summary>
        /// 加载单元combox数据
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
          {
              string token = UserToken.token;
              int unitId = 0;

              IDealBuyInterface dealBuy = new IDealBuyInterface();
              var result = dealBuy.UnitList(unitId, token);
              string success = result["Message"]["Message"].ToString();
              string jsonData = result["Message"].ToString();
              if (success == "成功")
              {
                  DealBuyingModel.UnitRoot data = JsonConvert.DeserializeObject<DealBuyingModel.UnitRoot>(jsonData);
                  CmbList = new ObservableCollection<DealBuyingModel>();
                  for (int i = 0; i < data.Data.Count; i++)
                  {
                      CmbList.Add(new DealBuyingModel
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
        /// 买入
        /// </summary>
        private void Buying()
        {
            if (ValidateUI.SecuritiesCode == null)
            {
                MessageDialogManager.ShowDialogAsync("证券代码不能为空!");
                return;
            }
            if (ValidateUI.CurrentPrice == null)
            {
                MessageDialogManager.ShowDialogAsync("买入价格(元)不能为空!");
                return;
            }
            if (ValidateUI.BuyAmount == null)
            {
                MessageDialogManager.ShowDialogAsync("买入数量(股)不能为空!");
                return;
            }
            if (!(ValidateUI.BuyAmount % 100 == 0))
            {
                MessageDialogManager.ShowDialogAsync("买入数量(股)为一百的倍数!");
                return;
            }

            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                //0是买，1是卖
                int type = 0;
                IDealBuyInterface dealBuy = new IDealBuyInterface();

                var result = await Task.Run(() => dealBuy.DealBuy(CmbItem.UnitId, validateUI.SecuritiesCode, validateUI.CurrentPrice, type, ValidateUI.BuyAmount, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    Lists.Clear();
                    UnitPosition();
                    validateUI.SecuritiesCode = "";
                    validateUI.CurrentPrice = 0;
                    ValidateUI.BuyAmount = 0;
                    MessageDialogManager.ShowDialogAsync("买入下单成功!");
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }

        /// <summary>
        /// 证券Enter
        /// </summary>
        DispatcherTimer CodeTimer = null;
        private void Security()
        {
            if (ValidateUI.SecuritiesCode.Length == 6)
            {
                //定时查询 - 定时器
                if (CodeTimer is null)
                {
                    CodeTimer = new DispatcherTimer();
                    CodeTimer.Tick += (s, e) =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                        {
                   if (ValidateUI.SecuritiesCode != null)
                   {
                       HQItem hq = HQService.Get(ValidateUI.SecuritiesCode);
                       if (hq != null && hq.Close_Prev > 0)
                       {

                           BuyHQ = new BuyHQModel()
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
                               Increase = ((hq.Last - hq.Close_Prev) / hq.Close_Prev).ToString("P2")
                           };
                       }
                   }
               });
                    };
                    CodeTimer.Interval = new TimeSpan(0, 0, 0, 3);
                }
                if (!CodeTimer.IsEnabled)
                {
                    CodeTimer.Start();
                }
            }
            else
            {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
               {
                   BuyHQ = new BuyHQModel()
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
               });
            }

        }

        /// <summary>
        /// 单元资金
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
                   DealBuyingModel.Root data = JsonConvert.DeserializeObject<DealBuyingModel.Root>(jsonData);
                   CmbItem.ProfitLossRatio = data.Data.profit.ToString();
                   CmbItem.NewestMarketValue = data.Data.value;
                   CmbItem.Assets = data.Data.assets;
                   CmbItem.Balance = data.Data.balance;
                   CmbItem.Available = data.Data.available;
                   CmbItem.Scale = data.Data.scale;

               }
                //}
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
                             DealBuyingModel.PositionRoot data = JsonConvert.DeserializeObject<DealBuyingModel.PositionRoot>(jsonData);
                             for (int i = 0; i < data.Data.Count; i++)
                             {
                                 HQItem hq = HQService.Get(data.Data[i].code);
                                 if (!(hq == null))
                                 {
                                     BuyHQPosition = new BuyHQModel()
                                     {
                                         Last = hq.Last,
                                     };
                                 }
                                 DealBuyingModel model = new DealBuyingModel()
                                 {
                                     Id = data.Data[i].id,
                                     SecuritiesCode = data.Data[i].code,
                                     SecuritiesName = data.Data[i].name,
                                     SecuritiesAmount = data.Data[i].count,
                                     MarketableAmount = data.Data[i].count_sellable,
                                     CurrentPrice = BuyHQPosition.Last,
                                     CostPrice = data.Data[i].price_cost,
                                     ProfitLossRatio = ((BuyHQPosition.Last - data.Data[i].price_cost) / data.Data[i].price_cost).ToString("P2"),
                                     FloatProfitLoss = Math.Round(((BuyHQPosition.Last - data.Data[i].price_cost) * data.Data[i].count), 3),
                                     NewestMarketValue = BuyHQPosition.Last * data.Data[i].count,
                                     BuyAmount = data.Data[i].count_today_buy,
                                     BuyAveragePrice = data.Data[i].price_cost_today_buy,
                                     BuyMoneyAmount = data.Data[i].price_cost_today_buy * data.Data[i].count_today_buy,
                                     SellAmount = data.Data[i].count_today_sell,
                                     SellAveragePrice = data.Data[i].price_cost_today_sell,
                                     SellMoneyAmount = data.Data[i].price_cost_today_sell * data.Data[i].count_today_sell,
                                     UnitId = data.Data[i].unit_id
                                 };
                                
                                 var obj = Lists.ToList().Find(target => target.SecuritiesCode.Equals(data.Data[i].code));
                                 if (obj != null)
                                 {
                                     obj = model;
                                 }
                                 else
                                     Lists.Add(model);
                             }
                         }
                     }
                     
                 });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);

                if (!dispatcherTimer.IsEnabled)
                {
                    dispatcherTimer.Start();
                }
            }
        }
           

        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate (object f)
                {
                    ((DispatcherFrame)f).Continue = false;
                    return null;
                }
            ), frame);
            Dispatcher.PushFrame(frame);
        }
        /// <summary>
        /// 最大可买数量
        /// </summary>
        private void MaxBuyNumber()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               string token = UserToken.token;
               int unitId = CmbItem.UnitId;
               int type = 0;
               IMaxBuyNumberInterface maxBuyNumber = new IMaxBuyNumberInterface();
               var result = await Task.Run(() => maxBuyNumber.MaxBuyNumber(unitId, ValidateUI.SecuritiesCode, type, ValidateUI.CurrentPrice, 0, token));
               string sucess = result["Message"]["Message"].ToString();
               string jsonData = result["Message"].ToString();
               if (sucess == "成功")
               {
                   DealBuyingModel.MaxBuyNumRoot data = JsonConvert.DeserializeObject<DealBuyingModel.MaxBuyNumRoot>(jsonData);
                   ValidateUI.MaxBuyNumber = data.Data;
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
                ValidateUI.BuyAmount = SelectedRow.MarketableAmount;
                ValidateUI.SecuritiesCode = SelectedRow.SecuritiesCode;
                ValidateUI.CurrentPrice = SelectedRow.CurrentPrice;
            }
        }

        /// <summary>
        /// 是否可执行（这边用表单是否验证通过来判断命令是否执行）
        /// </summary>
        /// <returns></returns>
        private bool CanExcute()
        {
            return ValidateUI.IsValidated;
        }

        /// <summary>
        /// 1/3百分比
        /// </summary>
        private void OneThird()
        {
            double OneThird = 0.33;
            int proportion = 3;
            int multiply = int.Parse(ValidateUI.MaxBuyNumber.ToString()) / proportion;
            ValidateUI.Percentage = OneThird.ToString("P2");
            ValidateUI.BuyAmount = multiply / 100 * 100;
            ValidateUI.ProgressRatio = 33;
        }

        /// <summary>
        /// 2/3百分比
        /// </summary>
        private void TwoThird()
        {
            double TwoThird = 0.66;
            int TwoThirdRatio = 3;
            int TwoThirdMultiply = (int.Parse(ValidateUI.MaxBuyNumber.ToString()) / TwoThirdRatio) * 2;
            ValidateUI.Percentage = TwoThird.ToString("P2");
            ValidateUI.BuyAmount = TwoThirdMultiply / 100 * 100;
            ValidateUI.ProgressRatio = 66;
        }

        /// <summary>
        /// 1/2 百分比
        /// </summary>
        private void OneTwo()
        {
            double OneTwo = 0.50;
            int OneTwoRatio = 2;
            int OneTwoMultiply = int.Parse(ValidateUI.MaxBuyNumber.ToString()) / OneTwoRatio;
            ValidateUI.Percentage = OneTwo.ToString("P2");
            ValidateUI.BuyAmount = OneTwoMultiply / 100 * 100;
            ValidateUI.ProgressRatio = 50;
        }

        /// <summary>
        /// 全部
        /// </summary>
        private void Whole()
        {
            double Whole = 1.0;
            int WholeRatio = 10;
            ValidateUI.Percentage = Whole.ToString("P2");
            int WholeMultiply = int.Parse(ValidateUI.MaxBuyNumber.ToString()) / WholeRatio;
            ValidateUI.BuyAmount = WholeMultiply * WholeRatio;
            ValidateUI.ProgressRatio = 100;
        }

        /// <summary>
        /// 进度百分比
        /// </summary>
        private void ProgressRatio()
        {
            if (ValidateUI.ProgressRatio != 0)
            {
                double a = Math.Round(ValidateUI.ProgressRatio);
                if (a != 0 && a >= 1)
                {
                    ValidateUI.Percentage = (a / 100).ToString("P2");
                }
                double Percentage = Convert.ToDouble(ValidateUI.Percentage.Replace("%", "")) / 100;
                var Amount = ValidateUI.MaxBuyNumber * Percentage;
                ValidateUI.BuyAmount = int.Parse(Amount.ToString())/100 * 100;

            }
        }

        /// <summary>
        /// 输入百分比
        /// </summary>
        private void Percentage()
        {
            if (ValidateUI.Percentage != null)
            {
                double proportion = double.Parse(ValidateUI.Percentage.TrimEnd('%')) / 100;
                ValidateUI.ProgressRatio = double.Parse(proportion.ToString()) * 100;
                ValidateUI.BuyAmount = int.Parse((proportion * ValidateUI.MaxBuyNumber).ToString());
            }

        }
    }
}
