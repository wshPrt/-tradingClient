using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class PositionSummaryModel: ValidateModelBase
    {
        /// <summary>
        /// 单元名称
        /// </summary>
        private string unitName;
        public string UnitName 
        {
            get { return unitName; }
            set { unitName = value;RaisePropertyChanged(() => unitName); }
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
        private int? securitiesAmount;
        public int? SercuritesAmount 
        {
            get { return securitiesAmount; }
            set { securitiesAmount = value; RaisePropertyChanged(() => SercuritesAmount); }
        }

        /// <summary>
        /// 可卖数量
        /// </summary>
        private int? toSell;
        public int? ToSell 
        {
            get { return toSell; }
            set { toSell = value; RaisePropertyChanged(() => ToSell); }
        }

       /// <summary>
       /// 成本价
       /// </summary>
        private decimal? costPrice;
        public decimal? CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; RaisePropertyChanged(() => CostPrice); }
        }

        /// <summary>
        /// 当前价
        /// </summary>
        private decimal? currentPrice;
        public decimal? CurrentPrice
        {
            get { return currentPrice; }
            set { currentPrice = value; RaisePropertyChanged(() => CurrentPrice); }
        }

        /// <summary>
        /// 浮动盈亏
        /// </summary>
        private string floatingProfitLoss;
        public string FloatingProfitLoss 
        {
            get { return floatingProfitLoss; }
            set { floatingProfitLoss = value; RaisePropertyChanged(() => FloatingProfitLoss); }
        }

        /// <summary>
        /// 盈亏比例
        /// </summary>
        private decimal? profitLossProportion;
        public decimal? ProfitLossProportion
        {
            get { return profitLossProportion; }
            set { profitLossProportion = value; RaisePropertyChanged(() => ProfitLossProportion); }
        }

        /// <summary>
        /// 最新市值
        /// </summary>
        private decimal? latestMarketValue;
        public decimal? LatestMarketValue
        {
            get { return latestMarketValue; }
            set { latestMarketValue = value; RaisePropertyChanged(() => LatestMarketValue); }
        }

        /// <summary>
        /// 今买数量
        /// </summary>
        private int? buyAmount;
        public int? BuyAmount
        {
            get { return buyAmount;}
            set { buyAmount = value; RaisePropertyChanged(() => BuyAmount); }
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
        /// 主账户
        /// </summary>
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(() => Account); }
        }
        
        public class Root 
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<Data> Data  { get; set; }
        }
        public class Data 
        {
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
            /// <summary>
            /// 证券代码
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 证券数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 可卖数量
            /// </summary>
            public int count_sellable { get; set; }

            /// <summary>
            /// 成本价
            /// </summary>
            public decimal price_cost { get; set; }
            /// <summary>
            /// 当前价
            /// </summary>
            public decimal price_cost_today_buy { get; set; }
            /// <summary>
            /// 当前出售价
            /// </summary>
            public decimal price_cost_today_sell  { get; set; }
            /// <summary>
            /// 最新市值
            /// </summary>
            public decimal price_latest  { get; set; }
            /// <summary>
            /// 今买数量
            /// </summary>
            public int count_today_buy { get; set; }
            /// <summary>
            /// 今卖数量
            /// </summary>
            public int count_today_sell { get; set; }

            /// <summary>
            /// 主账号
            /// </summary>
            public string account_name { get; set; }

            /// <summary>
            /// 持仓Id
            /// </summary>
            public int id { get; set; }


     
        }
    }
}
