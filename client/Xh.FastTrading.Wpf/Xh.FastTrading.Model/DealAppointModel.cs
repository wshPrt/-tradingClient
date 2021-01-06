using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class DealAppointModel:ValidateModelBase
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        private string btnName ="买入";
        public string BtnName 
        {
            get { return btnName; }
            set { btnName = value; RaisePropertyChanged(() => BtnName); }
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
        /// 单元代码
        /// </summary>
        private string unitCode;
        public string UnitCode
        {
            get { return unitCode; }
            set { unitCode = value; RaisePropertyChanged(() => UnitCode); }
        }

        /// <summary>
        /// 单元名称
        /// </summary>
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value;RaisePropertyChanged(() => UnitName); }
        }

        /// <summary>
        /// 指定账户Id
        /// </summary>
        private int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value;RaisePropertyChanged(() => accountId);}
        }

        /// <summary>
        /// 账户代码
        /// </summary>
        private string accountCode;
        public string AccountCode
        {
            get { return accountCode; }
            set { accountCode = value; RaisePropertyChanged(() => AccountCode); }
        }

        /// <summary>
        /// 账户名称
        /// </summary>
        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; RaisePropertyChanged(() => AccountName); }
        }

        /// <summary>
        /// 单票数量
        /// </summary>
        private int? singleTicketAmount;
        public int? SingleTicketAmount
        {
            get { return singleTicketAmount; }
            set { singleTicketAmount = value; RaisePropertyChanged(() => SingleTicketAmount); }
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
        /// 单票今买
        /// </summary>
        private decimal? singleTicketBuy;
        public decimal? SingleTicketBuy 
        {
            get { return singleTicketBuy; }
            set { singleTicketBuy = value; RaisePropertyChanged(() => SingleTicketAmount); }
        }


        /// <summary>
        /// 单票今卖
        /// </summary>
        private int? singleTicketSell ;
        public int? SingleTicketSell
        {
            get { return singleTicketSell; }
            set { singleTicketSell = value; RaisePropertyChanged(() => SingleTicketSell);}
        }

        /// <summary>
        /// 单票可卖
        /// </summary>
        private decimal? singleTicketMarketable;
        public decimal? SingleTicketMarketable
        {
            get { return singleTicketMarketable; }
            set { singleTicketMarketable = value; RaisePropertyChanged(() => SingleTicketMarketable);}
        }
        /// <summary>
        /// 单票市值
        /// </summary>
        private decimal? singleTicketMarketValue;
        public decimal? SingleTicketMarketValue
        {
            get { return singleTicketMarketValue; }
            set { singleTicketMarketValue = value; RaisePropertyChanged(() => SingleTicketMarketValue); }
        }

        /// <summary>
        /// 指定账户ID
        /// </summary>
        private int appointId;
        public int AppointId
        {
            get { return appointId; }
            set { appointId = value; RaisePropertyChanged(() => AppointId); }
        }

        /// <summary>
        /// 指定账户代码
        /// </summary>
        private string appointCode;
        public string AppointCode
        {
            get { return appointCode; }
            set { appointCode = value; RaisePropertyChanged(() => AppointCode); }
        }

        /// <summary>
        ///  指定账户名称
        /// </summary>
        private string appointName;
        public string AppointName
        {
            get { return appointName; }
            set { appointName = value; RaisePropertyChanged(() => AppointName);}
        }

        /// <summary>
        /// 持仓证券
        /// </summary>
        private string positionSecurities;
        public string PositionSecurities
        {
            get { return positionSecurities; }
            set { positionSecurities = value;RaisePropertyChanged(() => PositionSecurities);}
        }

        public class BuySellTypModel
        {
            /// <summary>
            /// 买卖ID
            /// </summary>
            private int _id;
            public int Id
            {
                get { return _id; }
                set { _id = value; }
            }

            /// <summary>
            /// 买卖名称
            /// </summary>
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }
        public class UnitRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<UnitData> Data { get; set; }
        }

        public class UnitData
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 证券ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
        }

        public class AppointRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<AppointData> Data { get; set; }
        }

        public class AppointData
        {
            /// <summary>
            /// 指定账户代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 指定账户Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 指定账户名称
            /// </summary>
            public string name { get; set; }
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
            /// 单元ID
            /// </summary>
            public int unit_id { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }

        public class AccountRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<AccoutData> Data { get; set; }
        }
        public class AccoutData
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
            /// 
            /// </summary>
            public string  name { get; set; }
            /// <summary>
            /// 主账户ID
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 账户代码
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
