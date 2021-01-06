using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class OpenClosingModel: ValidateModelBase
    {
        /// <summary>
        /// 交易日
        /// </summary>
        private string trading;
        public string Trading
        {
            get { return trading; }
            set { trading = value; RaisePropertyChanged(() => Trading); }
        }

        /// <summary>
        /// 开盘时间
        /// </summary>
        private string openTime;
        public string OpenTime
        {
            get { return openTime; }
            set { openTime = value; RaisePropertyChanged(() => OpenTime); }
        }

        /// <summary>
        /// 开盘用户
        /// </summary>
        private string openUser;
        public string OpenUser
        {
            get { return openUser; }
            set { openUser = value; RaisePropertyChanged(() => OpenUser); }
        }

        /// <summary>
        /// 收盘时间
        /// </summary>
        private string closingTime;
        public string ClosingTime
        {
            get { return closingTime; }
            set { closingTime = value; RaisePropertyChanged(() => ClosingTime); }
        }

        /// <summary>
        /// 收盘用户
        /// </summary>
        private string closingUser;
        public string ClosingUser
        {
            get { return closingUser; }
            set { closingUser = value;RaisePropertyChanged(() => ClosingUser); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(() => Status); }
        }


        public class Data
        {
            /// <summary>
            /// 交易日
            /// </summary>
            public DateTime date { get; set; }
            /// <summary>
            /// 收盘用户
            /// </summary>
            public string operator_close { get; set; }
            /// <summary>
            /// 开盘用户
            /// </summary>
            public string operator_open { get; set; }
            /// <summary>
            /// 状态(已收盘)
            /// </summary>
            public string state { get; set; }
            /// <summary>
            /// 收盘时间
            /// </summary>
            public string time_close { get; set; }
            /// <summary>
            /// 开盘时间
            /// </summary>
            public DateTime time_open { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 成功
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Data> Data { get; set; }
        }

    }
}
