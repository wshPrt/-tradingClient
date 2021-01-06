using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public  class BuyingSellModel:ValidateModelBase
    {
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
        /// 证券代码
        /// </summary>
        private int securitiesCode;
        public int SecuritiesCode
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
        private decimal profitLossRatio;
        public decimal ProfitLossRatio 
        {
            get { return profitLossRatio; }
            set { profitLossRatio = value;RaisePropertyChanged(() => ProfitLossRatio); }
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
        private decimal buyMoneyAmount ;
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
            get { return buyAveragePrice ; }
            set { buyAveragePrice = value;RaisePropertyChanged(() => BuyMoneyAmount);}
        }

        /// <summary>
        /// 今卖数量
        /// </summary>
        private int sellAmount;
        public int SellAmount 
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
        private decimal sellAveragePrice; 
        public decimal SellAveragePrice 
        {
            get { return sellAveragePrice; }
            set { sellAveragePrice = value; RaisePropertyChanged(() => SellMoneyAmount); }
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
            public int Code { get; set; }
            /// <summary>
            /// 证券ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
        }
    }
}
