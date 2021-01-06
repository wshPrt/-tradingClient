using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class DealSummaryModel : ValidateModelBase
    {

        /// <summary>
        /// 开始时间(从)
        /// </summary>
        private string fromTime = DateTime.Now.ToString("yyyy/MM/dd");
        public string FromTime
        {
            get { return fromTime; }
            set { fromTime = value; RaisePropertyChanged(() => FromTime); }
        }
        /// <summary>
        /// 结束时间(到)
        /// </summary>
        private string toTime = DateTime.Now.ToString("yyyy/MM/dd");
        public string ToTime
        {
            get { return toTime; }
            set { toTime = value; RaisePropertyChanged(() => ToTime); }
        }

        /// <summary>
        /// ID
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        ///  单元名称
        /// </summary>
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; RaisePropertyChanged(() => UnitName); }
        }

        /// <summary>
        /// 成交时间
        /// </summary>
        private string dealTime;
        public string DealTime
        {
            get { return dealTime; }
            set { dealTime = value; RaisePropertyChanged(() => DealTime); }
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
        /// 委托类型
        /// </summary>
        private int? entrustType;
        public int? EntrustType
        {
            get { return entrustType; }
            set { entrustType = value; EntrustStr = GetEntrustType(value); RaisePropertyChanged(() => EntrustType); }
        }
        public string EntrustStr { get; set; }
        private static string GetEntrustType(int? entrustType)
        {
            var str = "";
            if (entrustType == 1)
            {
                str += "卖出";
            }
            if (entrustType == 0)
            {
                str += "买入";
            }
            return str;
        }


        /// <summary>
        /// 成交价格
        /// </summary>
        private decimal? dealPrice;
        public decimal? DealPrice
        {
            get { return dealPrice; }
            set { dealPrice = value; RaisePropertyChanged(() => DealPrice); }
        }

        /// <summary>
        /// 成交数量
        /// </summary>
        private int? dealAmount;
        public int? DealAmount
        {
            get { return dealAmount; }
            set { dealAmount = value; RaisePropertyChanged(() => DealAmount); }
        }

        /// <summary>
        /// 成交金额
        /// </summary>
        private decimal? dealAmountMoney;
        public decimal? DealAmountMoney
        {
            get { return dealAmountMoney; }
            set { dealAmountMoney = value; RaisePropertyChanged(() => DealAmountMoney); }
        }

        /// <summary>
        /// 佣金
        /// </summary>
        private decimal? commission;
        public decimal? Commission
        {
            get { return commission; }
            set { commission = value; RaisePropertyChanged(() => Commission); }
        }

        /// <summary>
        /// 印花税
        /// </summary>
        private decimal? stampDuty;
        public decimal? StampDuty
        {
            get { return stampDuty; }
            set { stampDuty = value; RaisePropertyChanged(() => StampDuty); }
        }

        /// <summary>
        /// 过户费
        /// </summary>
        private decimal? transferFee;
        public decimal? TransferFee
        {
            get { return transferFee; }
            set { transferFee = value; RaisePropertyChanged(() => TransferFee); }
        }

        /// <summary>
        /// 成交编号
        /// </summary>
        private string  dealNumber;
        public string DealNumber
        {
            get { return dealNumber; }
            set { dealNumber = value; RaisePropertyChanged(() => dealNumber); }
        }

        /// <summary>
        /// 委托编号
        /// </summary>
        private string entrustNumber;
        public string EntrustNumber
        {
            get { return entrustNumber; }
            set { entrustNumber = value; RaisePropertyChanged(() => EntrustNumber); }
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
        /// 是否转入
        /// </summary>
        private int? isNoInto;
        public int? IsNoInto
        {
            get { return isNoInto; }
            set { isNoInto = value; IsNoIntoStr = GeIsNoInto(value); RaisePropertyChanged(() => IsNoInto); }
        }
        public string IsNoIntoStr { get; set; }
        private static string GeIsNoInto(int? securities)
        {
            var str = "";
            if (securities == 0)
            {
                str += "否";

            }
            if (securities == 1)
            {
                str += "是";

            }
            return str;
        }

        /// <summary>
        /// 交易账号List
        /// </summary>
        public List<AccountArr> accountArr { get; set; }

        /// <summary>
        /// 交易账号代码
        /// </summary>
        private string tradeCode;
        public string TradeCode
        {
            get { return tradeCode; }
            set { tradeCode = value; RaisePropertyChanged(() => TradeCode); }
        }
        /// <summary>
        /// 交易账号Id
        /// </summary>
        private int tradeId;
        public int TradeId
        {
            get { return tradeId; }
            set { tradeId = value; RaisePropertyChanged(() => TradeId); }
        }
        /// <summary>
        /// 交易账号名称
        /// </summary>
        private string tradeName;
        public string TradeName
        {
            get { return tradeName; }
            set { tradeName = value; RaisePropertyChanged(() => TradeName); }
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
        private string unitNameTwo;
        public string UnitNameTwo
        {
            get { return unitNameTwo; }
            set { unitNameTwo = value; RaisePropertyChanged(() => UnitNameTwo); }
        }

        public class Root
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
            public string code { get; set; }
            /// <summary>
            /// 证券id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 主账户Id
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 主账户 
            /// </summary>
            public string account_name { get; set; }
            /// <summary>
            /// 佣金
            /// </summary>
            public decimal commission { get; set; }
            /// <summary>
            /// 成交数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 成交编号
            /// </summary>
            public string deal_no { get; set; }
            /// <summary>
            /// 成交金额
            /// </summary>
            public decimal money { get; set; }
            /// <summary>
            /// 委托编号
            /// </summary>
            public string order_no { get; set; }
            /// <summary>
            /// 成交价格
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 成交时间
            /// </summary>
            public DateTime time { get; set; }
            /// <summary>
            /// 印发税
            /// </summary>
            public decimal management_fee { get; set; }
            /// <summary>
            /// 过户费
            /// </summary>
            public int transferred { get; set; }
            /// <summary>
            /// 委托类型
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }

        public class TradeRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<TradeData> Data { get; set; }
        }

        public class TradeData
        {
            /// <summary>
            /// 交易账号代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 交易账号Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 交易账号名称
            /// </summary>
            public string name { get; set; }
        }
        public class AccountArr
        {
            /// <summary>
            /// 单元编号
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 单元ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string name { get; set; }
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
            /// 单元代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 单元账号Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 单元账号名称
            /// </summary>
            public string name { get; set; }
        }
    }
}
