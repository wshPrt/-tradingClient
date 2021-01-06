using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public  class ObserAccountPoolModel
    {
        public class Data
        {
            /// <summary>
            /// 代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// List
            /// </summary>
            public List<items> itemsList { get; set; }
            /// <summary>
            /// 优先策略
            /// </summary>
            public string PriorityStrategy { get; set; }

        }

        public class items
        {
            /// <summary>
            /// 主账号池代码
            /// </summary>
            public int accountCode { get; set; }
            /// <summary>
            /// 主账号池Id
            /// </summary>
            public int accountId { get; set; }
            /// <summary>
            /// 主账号池名称
            /// </summary>
            public string accountName { get; set; }
            /// <summary>
            /// 账号池可用
            /// </summary>
            public long capitalAvailable { get; set; }
            /// <summary>
            /// 分类买入
            /// </summary>
            public int sortBuy { get; set; }
            /// <summary>
            /// 分类卖出
            /// </summary>
            public int sortSell { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 代码
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 成功消息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Data> Data { get; set; }

        }
    }
}
