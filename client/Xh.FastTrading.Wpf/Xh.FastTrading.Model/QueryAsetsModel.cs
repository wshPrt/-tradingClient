using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class QueryAsetsModel:ValidateModelBase
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
        /// 余额
        /// </summary>
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; RaisePropertyChanged(() => Balance); }
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
        /// 市值
        /// </summary>
        private decimal market;
        public decimal Market
        {
            get { return market; }
            set { market = value; RaisePropertyChanged(() => Market);}
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
        /// 盈亏
        /// </summary>
        private decimal profitLoss;
        public decimal ProfitLoss
        {
            get { return  profitLoss; }
            set {  profitLoss = value; RaisePropertyChanged(() => ProfitLoss);}
        }

        /// <summary>
        /// 规模
        /// </summary>
        private decimal scale;
        public decimal Scale 
        {
            get { return scale; }
            set { scale = value;RaisePropertyChanged(() => Scale); }
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
        public string ScuritiesName
        {
            get { return securitiesName; }
            set { securitiesName = value; RaisePropertyChanged(() => ScuritiesName); }
        }

        /// <summary>
        /// 证券数量
        /// </summary>
        private int? securitiesAmount;
        public int? SecuritiesAmount
        {
            get { return securitiesAmount; }
            set { securitiesAmount = value; RaisePropertyChanged(() => securitiesAmount); }
        }

        /// <summary>
        /// 可卖数量
        /// </summary>
        private int? marketableAmount;
        public int? MarketableAmount
        {
            get { return marketableAmount; }
            set { marketableAmount = value; RaisePropertyChanged(() => MarketableAmount);}
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
        /// 盈亏比例
        /// </summary>
        private decimal? profitLossRatio;
        public decimal? ProfitLossRatio
        {
            get { return profitLossRatio; }
            set { profitLossRatio = value; RaisePropertyChanged(() => ProfitLossRatio); }
        }

        /// <summary>
        /// 最新市值
        /// </summary>
        private decimal? marketValue;
        public decimal? MarketValue 
        {
            get { return marketValue; }
            set { marketValue = value; RaisePropertyChanged(() => MarketValue); }
        }

        /// <summary>
        /// 今买数量
        /// </summary>
        private int? buyAmout;
        public int? BuyAmount
        {
            get { return buyAmout; }
            set { buyAmout = value; RaisePropertyChanged(() => BuyAmount); }
        }

        /// <summary>
        /// 今买金额
        /// </summary>
        private decimal? buyAmountMoney;
        public decimal? BuyAmountMoney
        {
            get { return buyAmountMoney; }
            set { buyAmountMoney = value; RaisePropertyChanged(() => BuyAmountMoney); }
        }


        /// <summary>
        /// 今买均价
        /// </summary>
        private decimal? averagePrice;
        public decimal? AveragePrice
        {
            get { return averagePrice; }
            set { averagePrice = value; RaisePropertyChanged(() => AveragePrice); }
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
        private decimal? sellAmountMoney;
        public decimal? SellAmountMoney 
        {
            get { return sellAmountMoney; }
            set { sellAmountMoney = value; RaisePropertyChanged(() => SellAmountMoney); }
        }

        /// <summary>
        /// 今卖均价
        /// </summary>
        private decimal? sellAveragePrice;
        public decimal? SellAveragePrice
        {
            get { return sellAveragePrice; }
            set { sellAveragePrice = value;RaisePropertyChanged(() => SellAveragePrice); }
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
        /// 单元资金 root
        /// </summary>
        public class capitalRoot 
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public  capitalData Data { get; set; }
        }
        
        /// <summary>
        /// 单元资金data
        /// </summary>
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
            ///盈亏
            /// </summary>
            public decimal profit { get; set; }
            /// <summary>
            /// 规模
            /// </summary>
            public decimal scale { get; set; }
            /// <summary>
            /// 市值
            /// </summary>
            public decimal value { get; set; }
        }

        /// <summary>
        /// 单元持仓 root
        /// </summary>
        public class positionRoot 
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<positionData> Data { get; set; } 
        }

        /// <summary>
        /// 单元持仓列表 data
        /// </summary>
        public class positionData 
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 证券Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 主账户
            /// </summary>
            public string account_name { get; set; }
            /// <summary>
            /// 证券数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 可卖数量
            /// </summary>
            public int count_sellable { get; set; }
            /// <summary>
            /// 今买数量
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
            /// 最新市值
            /// </summary>
            public decimal price_latest { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }
    }
}
