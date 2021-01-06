using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class BuyHQModel
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 股票名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal High  { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal Open { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// 昨日收盘价
        /// </summary>
        public decimal Close_Prev { get; set; }
        /// <summary>
        /// 最新价
        /// </summary>
        public Decimal Last { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 买一价
        /// </summary>
        public decimal Buy_1 { get; set; }
        /// <summary>
        /// 买二价
        /// </summary>
        public decimal Buy_2 { get; set; }
        /// <summary>
        /// 买三价
        /// </summary>
        public decimal Buy_3 { get; set; }
        /// <summary>
        /// 买四价
        /// </summary>
        public decimal Buy_4 { get; set; }
        /// <summary>
        /// 买五价
        /// </summary>
        public decimal Buy_5 { get; set; }
        /// <summary>
        /// 买一量
        /// </summary>
        public int Buy_Volume_1 { get; set; }
        /// <summary>
        /// 买二量
        /// </summary>
        public int Buy_Volume_2 { get; set; }
        /// <summary>
        /// 买三量
        /// </summary>
        public int Buy_Volume_3 { get; set; }
        /// <summary>
        /// 买四量
        /// </summary>
        public int Buy_Volume_4 { get; set; }
        /// <summary>
        /// 买五量
        /// </summary>
        public int Buy_Volume_5 { get; set; }
        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal Sell_1 { get; set; }
        /// <summary>
        /// 卖二价
        /// </summary>
        public decimal Sell_2 { get; set; }
        /// <summary>
        /// 卖三价
        /// </summary>
        public decimal Sell_3 { get; set; }
        /// <summary>
        /// 卖四价
        /// </summary>
        public decimal Sell_4 { get; set; }
        /// <summary>
        /// 卖五价
        /// </summary>
        public decimal Sell_5 { get; set; }
        /// <summary>
        /// 卖一量
        /// </summary>
        public int Sell_Volume_1 { get; set; }
        /// <summary>
        /// 卖二量
        /// </summary>
        public int Sell_Volume_2 { get; set; }
        /// <summary>
        /// 卖三量
        /// </summary>
        public int Sell_Volume_3 { get; set; }
        /// <summary>
        /// 卖四量
        /// </summary>
        public int Sell_Volume_4 { get; set; }
        /// <summary>
        /// 卖五量
        /// </summary>
        public int Sell_Volume_5 { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 涨幅
        /// </summary>
        public string Increase { get; set; }
        /// <summary>
        /// 涨停
        /// </summary>
        public decimal LimitHigh { get; set; }
        /// <summary>
        /// 跌停
        /// </summary>
        public decimal LimitLow { get; set; }
    }
}
