using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class DealSellModel: ValidateModelBase
    {

        /// <summary>
        /// 卖进度比例
        /// </summary>
        private double sellProgressRatio;
        public double SellProgressRatio
        {
            get { return sellProgressRatio; }
            set { sellProgressRatio = value; RaisePropertyChanged(() => SellProgressRatio); }
        }
        /// <summary>
        ///  最大可买数量
        /// </summary>
        private int maxSellNumber;
        public int MaxSellNumber
        {
            get { return maxSellNumber; }
            set { maxSellNumber = value; RaisePropertyChanged(() => MaxSellNumber); }
        }
        /// <summary>
        /// 单元Id
        /// </summary>
        private int unitId;
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; RaisePropertyChanged(() => UnitId); }
        }
        /// <summary>
        /// 单元名称
        /// </summary>
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; RaisePropertyChanged(() => UnitName); }
        }

        /// <summary>
        /// 证券代码
        /// </summary>
        private string securitiesCode;
        public string SecuritiesCode
        {
            get { return securitiesCode; }
            set { securitiesCode = value; RaisePropertyChanged(() => SecuritiesCode); }
        }

        /// <summary>
        /// 证券名称
        /// </summary>
        private string securitiesName;
        public string SecuritiesName
        {
            get { return securitiesName; }
            set { securitiesName = value; RaisePropertyChanged(() => SecuritiesName); }
        }

        /// <summary>
        /// 证券数量
        /// </summary>
        private int securitiesAmount;
        public int SecuritiesAmount
        {
            get { return securitiesAmount; }
            set { securitiesAmount = value; RaisePropertyChanged(() => SecuritiesAmount); }
        }

        /// <summary>
        /// 规模
        /// </summary>
        private decimal scale;
        public decimal Scale
        {
            get { return scale; }
            set { scale = value; RaisePropertyChanged(() => Scale); }
        }

        /// <summary>
        /// 可用
        /// </summary>
        private decimal available;
        public decimal Available
        {
            get { return available; }
            set { available = value; RaisePropertyChanged(() => Available); }
        }

        /// <summary>
        /// 资产
        /// </summary>
        private decimal assets;
        public decimal Assets
        {
            get { return assets; }
            set { assets = value; RaisePropertyChanged(() => Assets); }
        }

        /// <summary>
        /// 余额
        /// </summary>
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; RaisePropertyChanged(() => Balance); }
        }
        /// <summary>
        /// 可卖数量
        /// </summary>
        private int marketableAmount;
        public int MarketableAmount
        {
            get { return marketableAmount; }
            set { marketableAmount = value; RaisePropertyChanged(() => MarketableAmount); }
        }

        /// <summary>
        /// 成本价
        /// </summary>
        private decimal costPrice;
        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; RaisePropertyChanged(() => CostPrice); }
        }

        /// <summary>
        /// 当前价
        /// </summary>
        private decimal currentPrice;
        public decimal CurrentPrice
        {
            get { return currentPrice; }
            set { currentPrice = value; RaisePropertyChanged(() => CurrentPrice); }
        }

        /// <summary>
        /// 盈亏比例
        /// </summary>
        private string profitLossRatio;
        public string ProfitLossRatio
        {
            get { return profitLossRatio; }
            set { profitLossRatio = value; RaisePropertyChanged(() => ProfitLossRatio); }
        }

        /// <summary>
        /// 浮动盈亏
        /// </summary>
        private decimal floatProfitLoss;
        public decimal FloatProfitLoss
        {
            get { return floatProfitLoss; }
            set { floatProfitLoss = value; RaisePropertyChanged(() => FloatProfitLoss); }
        }

        /// <summary>
        /// 最新市值
        /// </summary>
        private decimal newestMarketValue;
        public decimal NewestMarketValue
        {
            get { return newestMarketValue; }
            set { newestMarketValue = value; RaisePropertyChanged(() => NewestMarketValue); }
        }

        /// <summary>
        /// 今买数量
        /// </summary>
        private int buyAmount;
        public int BuyAmount
        {
            get { return buyAmount; }
            set { buyAmount = value; RaisePropertyChanged(() => BuyAmount); }
        }

        /// <summary>
        /// 今买金额
        /// </summary>
        private decimal buyMoneyAmount;
        public decimal BuyMoneyAmount
        {
            get { return buyMoneyAmount; }
            set { buyMoneyAmount = value; RaisePropertyChanged(() => BuyMoneyAmount); }
        }

        /// <summary>
        /// 今买均价
        /// </summary>
        private decimal buyAveragePrice;
        public decimal BuyAveragePrice
        {
            get { return buyAveragePrice; }
            set { buyAveragePrice = value; RaisePropertyChanged(() => BuyMoneyAmount); }
        }

        /// <summary>
        /// 今卖数量
        /// </summary>
        private int? sellAmount;
        public int? SellAmount
        {
            get { return sellAmount; }
            set { sellAmount = value; RaisePropertyChanged(() => SellAmount); }
        }

        /// <summary>
        /// 今卖金额
        /// </summary>
        private decimal sellMoneyAmount;
        public decimal SellMoneyAmount
        {
            get { return sellMoneyAmount; }
            set { sellMoneyAmount = value; RaisePropertyChanged(() => SellMoneyAmount); }
        }

        /// <summary>
        /// 今卖均价
        /// </summary>
        private decimal? sellAveragePrice ;
        public decimal? SellAveragePrice
        {
            get { return sellAveragePrice; }
            set { sellAveragePrice = value; RaisePropertyChanged(() => SellAveragePrice); }
        }

        /// <summary>
        /// 主账户
        /// </summary>
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(() => Account); }
        }

        /// <summary>
        /// 买入价格
        /// </summary>
        private decimal buyingPrice;
        public decimal BuyingPrice
        {
            get { return buyingPrice; }
            set { buyingPrice = value; RaisePropertyChanged(() => BuyingPrice); }
        }

        /// <summary>
        /// 买入卖出类型,0表示买入;1表示卖出
        /// </summary>
        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public class UnitRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<Data> Data { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 证券ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
        }

        public class capitalRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public capitalData Data { get; set; }
        }
        public class capitalData
        {
            /// <summary>
            /// 资产
            /// </summary>
            public decimal assets { get; set; }
            /// <summary>
            /// 可用
            /// </summary>
            public decimal available { get; set; }
            /// <summary>
            /// 余额
            /// </summary>
            public decimal balance { get; set; }
            /// <summary>
            /// 盈亏
            /// </summary>
            public decimal profit { get; set; }
            /// <summary>
            /// 资金规模
            /// </summary>
            public decimal scale { get; set; }
            /// <summary>
            /// 市值
            /// </summary>
            public decimal value { get; set; }
        }

        public class PositionRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<PositionData> Data { get; set; }
        }
        public class PositionData
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 主账户ID
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 主账户名称
            /// </summary>
            public string account_name { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 可卖数量
            /// </summary>
            public int count_sellable { get; set; }
            /// <summary>
            /// 今天数量
            /// </summary>
            public int count_today_buy { get; set; }
            /// <summary>
            /// 今卖数量
            /// </summary>
            public int count_today_sell { get; set; }
            /// <summary>
            /// 成本价
            /// </summary>
            public decimal price_cost { get; set; }
            /// <summary>
            /// 今买均价
            /// </summary>
            public decimal price_cost_today_buy { get; set; }
            /// <summary>
            /// 今卖均价
            /// </summary>
            public decimal price_cost_today_sell { get; set; }
            /// <summary>
            /// 单元ID
            /// </summary>
            public int unit_id { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }
    }
}
