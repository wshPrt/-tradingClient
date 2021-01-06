using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class QueryCapitalFlowModel:ValidateModelBase
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
        /// 发生日期
        /// </summary>
        private string happenDateTime;
        public string HappenDateTime 
        {
            get { return happenDateTime; }
            set { happenDateTime = value; RaisePropertyChanged(() => HappenDateTime); }
        }

        /// <summary>
        /// 发生时间
        /// </summary>
        private string happenTime;
        public string HappenTime  
        {
            get { return happenTime; }
            set { happenTime = value;RaisePropertyChanged(() => HappenTime); }
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        private int businessName;
        public int BusinessName
        {
            get { return businessName; }
            set { businessName = value; BusinessStr = GetBusinessType(value); RaisePropertyChanged(() => BusinessName); }
        }
        public string BusinessStr { get; set; }
        
        private static string GetBusinessType(int businessType)
        {
            var str = "";
            if (businessType == 1)
            {
                str += "转入资金";
            }
            if (businessType == 0)
            {
                str += "转出资金";
            }
            return str;
        }



        /// <summary>
        /// 发生金额
        /// </summary>
        private decimal? happenAmount;
        public decimal? HappenAmount 
        {
            get { return happenAmount; }
            set { happenAmount = value;RaisePropertyChanged(() => HappenTime);}
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
        /// 股票资金账号
        /// </summary>
        private bool stockCapitalAccount;
        public bool StockCapitalAccount 
        {
            get { return stockCapitalAccount; }
            set { stockCapitalAccount = value; RaisePropertyChanged(() => StockCapitalAccount);}
        }

        /// <summary>
        /// 保证金账号
        /// </summary>
        private bool marginAccount;
        public bool MarginAccount
        {
            get { return marginAccount; }
            set { marginAccount = value; RaisePropertyChanged(() => MarginAccount); }
        }

        public class Root 
        {
            public int code { get; set; }
            public string Message { get; set; }
            public List<Data> Data { get; set; }
        }
        
        public class Data 
        {
            /// <summary>
            /// 
            /// </summary>
            public int action { get; set; }
            /// <summary>
            /// 发生金额
            /// </summary>
            public decimal? amount { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string remark  { get; set; }
            /// <summary>
            /// 发生时间
            /// </summary>
            public DateTime time_dt { get; set; }
            /// <summary>
            /// 类型
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 单元Id
            /// </summary>
            public int unit_id { get; set; }
        }
        public class UnitRoot 
        {
            public int code { get; set; }
            public string Message { get; set; }
            public List<UnitData> Data { get; set; }
        }
        public class UnitData
        {
            public string code { get; set; }
            public string name { get; set; }
            public int id { get; set; } 
        }

    }
}
