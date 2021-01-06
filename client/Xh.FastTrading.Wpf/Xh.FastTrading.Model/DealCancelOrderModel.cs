using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class DealCancelOrderModel: ValidateModelBase,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 选择
        /// </summary>
        private bool chbChoose;
        public bool ChbChoose
        {
            get { return chbChoose; }
            set { chbChoose = value;RaisePropertyChanged(() => ChbChoose);}
        }

        /// <summary>
        /// 委托日期
        /// </summary>
        private string entrustdateTime;
        public string EntrustDateTime 
        {
            get { return entrustdateTime; }
            set { entrustdateTime = value;RaisePropertyChanged(() => EntrustDateTime); }
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
            set { entrustType = value; EntrustTypeStr = GetEntrustType(value); RaisePropertyChanged(() => EntrustType); }
        }
        public string EntrustTypeStr { get; set; }
        private static string GetEntrustType(int? securities)
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
        private decimal? entrustPrice;
        public decimal? EntrustPrice 
        {
            get { return entrustPrice; }
            set { entrustPrice = value; RaisePropertyChanged(() => EntrustPrice); }
        }

        /// <summary>
        /// 委托数量
        /// </summary>
        private decimal? entrustAmount;
        public decimal? EntrustAmount
        {
            get { return entrustAmount; }
            set { entrustAmount = value; RaisePropertyChanged(() => EntrustPrice); }
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
        private Int64 entrustNumber;
        public Int64 EntrustNumber
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
            set { account = value; RaisePropertyChanged(() => Account);}
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; RaisePropertyChanged(() => remarks); }
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
            set { unitName = value; RaisePropertyChanged(() => UnitName); }
        }

        private bool _isSelected  ;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
               // OnPropertyChanged("IsSelected");
            }
        }


        public class EntrustRoot 
        {
            public int code { get; set; }
            public string Message { get; set; }
            public List<EntrustData> Data { get; set; }
        }

        public class EntrustData 
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 主账户名称
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
            /// 订单编号
            /// </summary>
            public string order_no { get; set; }
            /// <summary>
            /// 下单平台
            /// </summary>
            public int platform { get; set; }
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
            /// 单元Id
            /// </summary>
            public int unit_id  { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public string user_name { get; set; }
            /// <summary>
            /// 单元名
            /// </summary>
            public string unit_name { get; set; }
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
            public int Id { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string Name { get; set; }
        }
    }
}
