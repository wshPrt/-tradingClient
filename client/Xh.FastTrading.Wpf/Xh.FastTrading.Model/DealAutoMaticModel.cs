using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
  public class DealAutoMaticModel:ValidateModelBase, ICloneable
    {

        /// <summary>
        /// 指定账户ID
        /// </summary>
        private int maticId;
        public int MaticId
        {
            get { return maticId; }
            set { maticId = value; RaisePropertyChanged(() => MaticId); }
        }

        /// <summary>
        /// 指定账户代码
        /// </summary>
        private string maticCode;
        public string MaticCode
        {
            get { return maticCode; }
            set { maticCode = value; RaisePropertyChanged(() => MaticCode); }
        }

        /// <summary>
        ///  指定账户名称
        /// </summary>
        private string maticName;
        public string MaticName
        {
            get { return maticName; }
            set { maticName = value; RaisePropertyChanged(() => MaticName); }
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
        /// 状态
        /// </summary>
        private int? status;
        public int? Status
        {
            get { return status; }
            set { status = value; StartStr = StartStopStr(value); RaisePropertyChanged(() => Status); }
        }
        public string StartStr { get; set; }
        private static string StartStopStr(int? direction)
        {
            var str = "";
            if (direction == 0)
            {
                str += "停用";
            }
            if (direction == 1)
            {
                str += "启用";
            }
            if (direction == 2)
            {
                str += "已完成";
            }
            return str;
        }
        /// <summary>
        /// ID
        /// </summary>
        private Int64 id;
        public Int64 Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        /// 证券代码
        /// </summary>
        private string securitiesCode;
        public string SecuritiesCode
        {
            get { return securitiesCode; }
            set { securitiesCode = value; RaisePropertyChanged(() => SecuritiesCode);}
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
        /// 买卖方向
        /// </summary>
        private int buySellDirection;
        public int BuySellDirection
        {
            get { return buySellDirection; }
            set { buySellDirection = value; DirectionStr = BuySellDirectionStr(value); RaisePropertyChanged(() => BuySellDirection); }
        }
        public string DirectionStr { get; set; }
        private static string BuySellDirectionStr(int direction)
        {
            var str = "";
            if (direction == 0)
            {
                str += "买入";
            }
            if (direction == 1)
            {
                str += "卖出";
            }
            return str;
        }

        /// <summary>
        /// 最新价
        /// </summary>
        //private double newPrice =13.01;
        private double newPrice;
        public double NewPrice
        {
            get { return newPrice; }
            set { newPrice = value; }
        }

        /// <summary>
        /// 最小间隔
        /// </summary>
        private int? minInterval;
        public int? MinInterval 
        {
            get { return minInterval; }
            set { minInterval = value; RaisePropertyChanged(() => MinInterval); }
        }

        /// <summary>
        /// 最大间隔
        /// </summary>
        private int? maxInterval;
        public int? MaxInterval
        {
            get { return maxInterval; }
            set { maxInterval = value; RaisePropertyChanged(() => MaxInterval); }
        }

        /// <summary>
        /// 最小数量
        /// </summary>
        private int? minAmount;
        public int? MinAmount
        {
            get { return minAmount; }
            set { minAmount = value; RaisePropertyChanged(() => MinAmount);}
        }

        /// <summary>
        /// 最大数量
        /// </summary>
        private int? maxAmount;
        public int? MaxAmount
        {
            get { return maxAmount; }
            set { maxAmount = value; RaisePropertyChanged(() => MaxAmount); }
        }

        /// <summary>
        /// 价格类型
        /// </summary>
        private int? priceType;
        public int? PriceType
        {
            get { return priceType; }
            set { priceType = value; TypeStr = PriceTypeStr(value); RaisePropertyChanged(() => PriceType); }
        }
        public string TypeStr { get; set; }
        private static string PriceTypeStr(int? direction)
        {
            var str = "";
            if (direction == 0)
            {
                str += "最新价";
            }
            if (direction == 1)
            {
                str += "买一价";
            }
            if (direction == 2)
            {
                str += "买二价";
            }
            if (direction == 3)
            {
                str += "买三价";
            }
            if (direction == 4)
            {
                str += "买四价";
            }
            if (direction == 5)
            {
                str += "买五价";
            }
            if (direction == -1)
            {
                str += "卖一价";
            }
            if (direction == -2)
            {
                str += "卖二价";
            }
            if (direction == -3)
            {
                str += "卖三价";
            }
            if (direction == -4)
            {
                str += "卖四价";
            }
            if (direction == -5)
            {
                str += "卖五价";
            }
            return str;
        }

        /// <summary>
        /// 最低限价
        /// </summary>
        private decimal? minPrice;
        public decimal? MinPrice 
        {
            get { return minPrice; }
            set { minPrice = value;RaisePropertyChanged(() => MinPrice); }
        }

        /// <summary>
        /// 最高限价
        /// </summary>
        private decimal? highestPrice;
        public decimal? HighestPrice
        {
            get { return highestPrice; }
            set { highestPrice = value; RaisePropertyChanged(() => HighestPrice); }
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
        /// 已执行次数
        /// </summary>
        private int? executeNumber;
        public int? ExecuteNumber
        {
            get { return executeNumber; }
            set { executeNumber = value; RaisePropertyChanged(() => ExecuteNumber); }
        }

        /// <summary>
        /// 已委托数量
        /// </summary>
        private int? entrustAmount;
        public int? EntrustAmount
        {
            get { return entrustAmount; }
            set { entrustAmount = value; RaisePropertyChanged(() => EntrustAmount); }
        }

        /// <summary>
        /// 数量限制
        /// </summary>
        private int? amountLimit;
        public int? AmountLimit
        {
            get { return amountLimit; }
            set { amountLimit = value; RaisePropertyChanged(() => AmountLimit); }
        }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        private string nextExecuteTime;
        public string NextExecuteTime
        {
            get { return nextExecuteTime; }
            set { nextExecuteTime = value; RaisePropertyChanged(() => NextExecuteTime);}
        }

        /// <summary>
        /// 上次执行时间
        /// </summary>
        private string lastExecuteTime;
        public string LastExecuteTime
        {
            get { return lastExecuteTime; }
            set { lastExecuteTime = value; RaisePropertyChanged(() => LastExecuteTime); }
        }

        /// <summary>
        /// 上次执行结果
        /// </summary>
        private string lastExecuteResult;
        public string LastExecuteResult
        {
            get { return lastExecuteResult; }
            set { lastExecuteResult = value; RaisePropertyChanged(() => LastExecuteResult); }
        }

        public class AccountRoot  
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<AccountData> Data { get; set; }
        }

        public class AccountData 
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

        public class AutoMaticRoot 
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<AutoMaticData> Data { get; set; }
        }

        public class AutoMaticData 
        {

            /// <summary>
            /// 最大数量
            /// </summary>
            public int count_max { get; set; }
            /// <summary>
            /// 最小数量
            /// </summary>
            public int count_min { get; set; }
            /// <summary>
            /// Id
            /// </summary>
            public Int64 id { get; set; }
            /// <summary>
            /// 最高限价
            /// </summary>
            public decimal price_max { get; set; }
            /// <summary>
            /// 最低限价
            /// </summary>
            public decimal price_min  { get; set; }
            /// <summary>
            /// 价格类型
            /// </summary>
            public int price_type { get; set; }
            /// <summary>
            /// 最大间隔
            /// </summary>
            public int time_max { get; set; }
            /// <summary>
            /// 最小间隔
            /// </summary>
            public int time_min  { get; set; }
            /// <summary>
            /// 单元Id
            /// </summary>
            public int unit_id  { get; set; }
            /// <summary>
            /// 主账户ID
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 主账户名称
            /// </summary>
            public string account_name { get; set; }
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 数量限制
            /// </summary>
            public int count_total { get; set; }
            /// <summary>
            /// 证券名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 价格类型
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 启用
            /// </summary>
            public string operator_start { get; set; }
            /// <summary>
            /// 停止
            /// </summary>
            public string operator_stop { get; set; }
            /// <summary>
            /// 已委托数量
            /// </summary>
            public int order_count { get; set; }
            /// <summary>
            /// 已执行次数
            /// </summary>
            public int order_times { get; set; }
            /// <summary>
            /// 上次执行结果
            /// </summary>
            public string result_prev { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public int status  { get; set; }
            /// <summary>
            /// 下次执行时间
            /// </summary>
            public DateTime? time_next { get; set; }
            /// <summary>
            /// 上次执行时间
            /// </summary>
            public DateTime? time_prev  { get; set; }
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

            public class PriceTypeModel 
            {
                /// <summary>
            /// 价格ID
            /// </summary>
                 private int _priceId;
                 public int PriceId
            {
                get { return _priceId; }
                set { _priceId = value; }
            }
                 /// <summary>
                /// 价格名称
                /// </summary>
                 private string _priceName;
                 public string PriceName 
            {
                get { return _priceName; }
                set { _priceName = value; }
            }
            }

        public class MaticAccountRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<MaticAccountData> Data { get; set; }
        }

        public class MaticAccountData
        {
            /// <summary>
            /// 账户代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 账户Id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 账户名称
            /// </summary>
            public string name { get; set; }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
    }
}
