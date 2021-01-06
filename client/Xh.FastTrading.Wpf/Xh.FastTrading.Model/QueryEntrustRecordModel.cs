using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
  public  class QueryEntrustRecordModel :ValidateModelBase
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
        /// 单元Id
        /// </summary>
        private int unitid;
        public int UnitId
        {
            get { return unitid; }
            set { unitid = value; RaisePropertyChanged(() => UnitId); }
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
        /// 委托日期
        /// </summary>
        private string entrustTime;
        public string EntrustTime
        {
            get { return entrustTime; }
            set { entrustTime = value; RaisePropertyChanged(() => EntrustTime); }
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
        /// 委托名称
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
        private int securitiesType;
        public int SecuritiesType
        {
            get { return securitiesType; }
            set { securitiesType = value; SecuritiesStr = GetSecuritiesType(value); RaisePropertyChanged(() => SecuritiesType); }
        }
        public string SecuritiesStr { get; set; }
        private static string GetSecuritiesType(int securities)
        {
               var str = "";
                if (securities == 0)
                {
                    str += "买入";
                
                }
                if (securities == 1)
                {
                    str += "卖出";
                   
                }
            return str;
        }

        /// <summary>
        /// 委托价格
        /// </summary>
        private decimal? securitiesPrice;
        public decimal? SecuritiesPrice
        {
            get { return securitiesPrice; }
            set { securitiesPrice = value; RaisePropertyChanged(() => SecuritiesPrice); }
        }

        /// <summary>
        /// 委托数量
        /// </summary>
        private int? securitiesAmount;
        public int? SecuritiesAmount
        {
            get { return securitiesAmount; }
            set { securitiesAmount = value; RaisePropertyChanged(() => SecuritiesAmount); }
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
        private int? dealAmount;
        public int? DealAmount
        {
            get { return dealAmount; }
            set { dealAmount = value; RaisePropertyChanged(() => DealAmount); }
        }

        /// <summary>
        /// 撤单数量
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
        private Int64 tradeNumber;
        public Int64 TradeNumber
        {
            get { return tradeNumber; }
            set { tradeNumber = value; RaisePropertyChanged(() => TradeNumber); }
        }

        /// <summary>
        /// 成交编号
        /// </summary>
        private string tradeNo;
        public string TradeNo
        {
            get { return tradeNo; }
            set { tradeNo = value;RaisePropertyChanged(() => TradeNo);}
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
        private string placeOrderUser;
        public string PlaceOrderUser
        {
            get { return placeOrderUser; }
            set { placeOrderUser = value; RaisePropertyChanged(() => PlaceOrderUser); }
        }

        /// <summary>
        /// 下单渠道
        /// </summary>
        private int? placeOrderChannel;
        public int? PlaceOrderChannel
        {
            get { return placeOrderChannel; }
            set { placeOrderChannel = value; PlaceOrderChannelStr = GetPlaceOrderChannelype(value); RaisePropertyChanged(() => PlaceOrderChannel); }
        }
        
        public string PlaceOrderChannelStr { get; set; }
        private static string GetPlaceOrderChannelype(int? securities)
        {
            var str = "";
            if (securities == 0)
            {
                str += "App端";

            }
            if (securities == 1)
            {
                str += "PC端";

            }
            return str;
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
            /// 委托ID
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
            /// 撤单数量
            /// </summary>
            public int cancel_count { get; set; }
            /// <summary>
            /// 委托数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 成交数量
            /// </summary>
            public int deal_count { get; set; }
            /// <summary>
            /// 委托编号
            /// </summary>
            public string order_no { get; set; }
            /// <summary>
            /// 平台;1表示PC,2表示移动端
            /// </summary>
            public int platform { get; set; }
            /// <summary>
            /// 委托价格
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 成功交均价
            /// </summary>
            public decimal deal_average_price { get; set; }
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
            public Int64 trade_no  { get; set; }
            /// <summary>
            /// 委托类型
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 单元Id
            /// </summary>
            public int unit_id { get; set; }
            /// <summary>
            /// 下单用户
            /// </summary>
            public string user_name { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string unit_name { get; set; }
        }

    }

}
