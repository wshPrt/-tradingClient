using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
  public class UnitManageEntrustSummaryModel:ValidateModelBase
    {

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
        /// 单元名称
        /// </summary>
        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; RaisePropertyChanged (()=> UnitName); }
        }

        /// <summary>
        /// 委托时间
        /// </summary>
        private string entrustTime;
        public string EntrustTime
        {
            get { return entrustTime; }
            set { entrustTime = value; RaisePropertyChanged(() => EntrustTime); }
        }

        /// <summary>
        ///证券代码
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
        private string sercuritiesName;
        public string SercuritiesName
        {
            get { return sercuritiesName; }
            set { sercuritiesName = value;RaisePropertyChanged(() => SercuritiesName); }
        }

        /// <summary>
        /// 委托类型
        /// </summary>
        private int entrustType;
        public int EntrustType 
        {
            get { return entrustType; }
            set { entrustType = value; EntrustStr = GetEntrustType(value); RaisePropertyChanged(() => EntrustType); }
        }
        public string EntrustStr { get; set; }
        private static string GetEntrustType(int entrustType)
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
        /// 委托价格
        /// </summary>
        private double? entrustPrice;
        public double? EntrustPrice
        {
            get { return entrustPrice; }
            set { entrustPrice = value; RaisePropertyChanged(() => entrustPrice); }
        }

        /// <summary>
        /// 委托数量
        /// </summary>
        private int? entrustAmount;
        public int? EntrustAmount
        {
            get { return entrustAmount; }
            set { entrustAmount = value; RaisePropertyChanged(() => EntrustAmount); }
        }

        /// <summary>
        /// 成交均价
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
        private int? delAmount;
        public int? DelAmount
        {
            get { return delAmount; }
            set { delAmount = value; RaisePropertyChanged(() => DelAmount); }
        }

        /// <summary>
        ///撤单数量
        /// </summary>
        private int? cancelOrderAmount;
        public int? CancelOrderAmount 

        {
            get { return cancelOrderAmount; }
            set { cancelOrderAmount = value; RaisePropertyChanged(() => CancelOrderAmount); }
        }

        /// <summary>
        /// 状态说明
        /// </summary>
        private string statusExplain;
        public string StatusExplain
        {
            get { return statusExplain; }
            set { statusExplain = value; RaisePropertyChanged(() => StatusExplain); }
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
        /// 备注
        /// </summary>
        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; RaisePropertyChanged(() => Remarks); }
        }

        /// <summary>
        /// 下单用户
        /// </summary>
        private string placeOrdepUser;
        public string PlaceOrdepUser
        {
            get { return placeOrdepUser; }
            set { placeOrdepUser = value; RaisePropertyChanged(() => PlaceOrdepUser); }
        }

        /// <summary>
        /// 从时间
        /// </summary>
        private string formDateTime;
        public string FormDateTime
        {
            get { return formDateTime; }
            set { formDateTime = value; RaisePropertyChanged(() => FormDateTime); }
        }

        /// <summary>
        /// 至时间
        /// </summary>
        private string toDatetime;
        public string ToDatetime
        {
            get { return toDatetime; }
            set { toDatetime = value; RaisePropertyChanged(() => ToDatetime); }
        }

        public class Data
        {
            /// <summary>
            /// 代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// ID编号
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name  { get; set; }
            /// <summary>
            /// 主账号Id
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 主账户
            /// </summary>
            public string account_name { get; set; }
            /// <summary>
            /// 撤单数量
            /// </summary>
            public int cancel_count { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 成交均价
            /// </summary>
            public decimal deal_average_price  { get; set; }
            /// <summary>
            /// 成交数量
            /// </summary>
            public decimal deal_count { get; set; }
            /// <summary>
            /// 委托编号
            /// </summary>
            public string order_no { get; set; }
            /// <summary>
            /// 平台
            /// </summary>
            public int platform  { get; set; }
            /// <summary>
            /// 委托价格
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// 状态说明
            /// </summary>
            public string state { get; set; }
            /// <summary>
            /// 委托时间
            /// </summary>
            public DateTime time { get; set; }
            /// <summary>
            /// 成交数量
            /// </summary>
            public int trade_count { get; set; }
            /// <summary>
            /// 成交编号
            /// </summary>
            public string trade_no { get; set; }
            /// <summary>
            /// 委托类型
            /// </summary>

            public int type { get; set; }
            /// <summary>
            /// 下单用户
            /// </summary>
            public string user_name { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 代码
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 成功
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 集合
            /// </summary>
            public List<Data> Data { get; set; }
        }
    }
}
