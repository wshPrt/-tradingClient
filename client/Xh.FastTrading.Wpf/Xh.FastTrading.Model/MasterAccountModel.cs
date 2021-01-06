using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class tClass
    {
        public int account_id { get; set; }
        public int sort_buy { get; set; }
        public int sort_sell { get; set; }
        public int capital_allow { get; set; }
    }
    public class item {
        public List<tClass> items_buy { get; set; } 
    }
   //[MetadataType(typeof(BindDataAnnotationsViewModel))]
   public class MasterAccountModel : ValidateModelBase
    {
        public static Dictionary<string, int> SelectedQuotaItems { get; set; }

        public static int MasterId { get; set; }
        public static int PriorityStrategy { get; set; }
        /// <summary>
        /// 账户和限额设置list集合
        /// </summary>
        public static List<tClass> items { get; set; } 
        private string info;
        public string Info
        {
            get { return info; }
            set { info = value; RaisePropertyChanged(() => Info); }
        }

        /// <summary>
        /// 买入顺序
        /// </summary>
        //private int sortBuy;
        //public int Sortbuy
        //{
        //    get { return sortBuy; }
        //    set { sortBuy = value; RaisePropertyChanged(() => Sortbuy); }
        //}

        /// <summary>
        /// 卖出顺序
        /// </summary>
        //private int sortSell;
        //public int SortSell
        //{
        //    get { return sortSell; }
        //    set { sortSell = value;RaisePropertyChanged(() => SortSell);}
        //}


        /// <summary>
        /// 文本选中集合
        /// </summary>
        private string selectInfo; 
        public string SelectInfo
        {
            get { return selectInfo; }
            set { selectInfo = value;RaisePropertyChanged(() => SelectInfo); }
        } 



        /// <summary>
        /// IP
        /// </summary>
        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; RaisePropertyChanged(() => IP); }
        }

        /// <summary>
        /// 端口
        /// </summary>
        private int? prot;
        public int? Prot
        {
            get { return prot; }
            set { prot = value; RaisePropertyChanged(() => Prot); }
        }


        /// <summary>
        /// ID编号
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "ID只能为数字")]
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => id); }
        }

        /// <summary>
        /// 代码
        /// </summary>
        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value; 
                RaisePropertyChanged(() => Code); 
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new ApplicationException("该字段不能为空");
                //} 
            }
        }
    
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;RaisePropertyChanged(() => Name); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; RaisePropertyChanged(() => Remarks); }
        }
        /// <summary>
        /// 限买
        /// </summary>
        [Required]
        private string limitBuy; 
        public string LimitBuy
        {
            get { return limitBuy; }
            set { limitBuy = value;RaisePropertyChanged(() => LimitBuy); }
        }

        /// <summary>
        /// 限买股票
        /// </summary>
        [Required]
        private string limitBuyShare;
        public string LimitBuyShare
        {
            get { return limitBuyShare; }
            set { limitBuyShare = value;RaisePropertyChanged(() => LimitBuyShare); }
        }

        /// <summary>
        /// 佣金率
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]+(.[0-9]{2})?$", ErrorMessage = "用户必须为两位小数的正实数.")]
        private double commissionRate;
        public double CommissionRate
        {
            get { return commissionRate; }
            set { commissionRate = value; RaisePropertyChanged(() => CommissionRate); }
        }

        /// <summary>
        /// 单票限制
        /// </summary>
        [Required]
        private double singleTicketLimit;
        public double SingleTicketLimit
        {
            get { return singleTicketLimit; }
            set { singleTicketLimit = value; RaisePropertyChanged(() => SingleTicketLimit); }
        }

        /// <summary>
        /// 创业板单票限制
        /// </summary>
        [Required]
        private double gemSingleTicketLimit;
        public double GemSingleTicketLimit
        {
            get { return gemSingleTicketLimit; }
            set { gemSingleTicketLimit = value;RaisePropertyChanged(() => GemSingleTicketLimit); }
        }

        /// <summary>
        /// 创业板限制
        /// </summary>
        [Required]
        private double gemLimit;
        public double  GemLimit
        {
            get { return gemLimit; }
            set { gemLimit = value; RaisePropertyChanged(() => GemLimit); }
        }

        /// <summary>
        /// 资产预警线
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]+(.[0-9]{2})?$", ErrorMessage = "资产预警线必须为两位小数的正实数.")]
        private double assetWarningLine;
        public double AssetWarningLine
        {
            get { return assetWarningLine; }
            set { assetWarningLine = value; RaisePropertyChanged(() => AssetWarningLine); }
        }

        /// <summary>
        /// 当前资产
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "当前资产值只能为数字")]
        private double currentAssets;
        public double CurrentAssets
        {
            get { return currentAssets; }
            set { currentAssets = value; RaisePropertyChanged(() => CurrentAssets); }
        }

        /// <summary>
        /// 现金
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "现金只能为数字")]
        private int cash;
        public int Cash
        {
            get { return cash; }
            set { cash = value; RaisePropertyChanged(() => Cash); }
        }

        /// <summary>
        /// 可用
        /// </summary>
        [Required]
        private  decimal available;
        public decimal Available
        {
            get { return available; }
            set { available = value;RaisePropertyChanged(() => Available); }
        }

        /// <summary>
        /// 账户市值
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "账户市值只能为数字")]
        private double accountMarketValue;
        public double AccountMarketValue
        {
            get { return accountMarketValue; }
            set { accountMarketValue = value;RaisePropertyChanged(() => AccountMarketValue);}
        }

        /// <summary>
        /// 系统账户
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "系统账户只能输入由数字和26个英文字母组成的字符串")]
        private string systemAccount;
        public string SystemAccount
        {
            get { return systemAccount; }
            set { systemAccount = value; RaisePropertyChanged(() => SystemAccount); }
        }

        /// <summary>
        /// Token
        /// </summary>
        public  string token;
        public  string Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// 本地IP
        /// </summary>
        [Required]
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// 饶标资金
        /// </summary>
        [Required]
        private double raoBiaoCapital;
        public double RaoBiaoCapital
        {
            get { return raoBiaoCapital; }
            set { raoBiaoCapital = value; RaisePropertyChanged(() => RaoBiaoCapital); }
        }

        /// <summary>
        /// 饶标利率
        /// </summary>
        [Required]
        private decimal raoBiaoRates;
        public decimal RaoBiaoRates
        {
            get { return raoBiaoRates; }
            set { raoBiaoRates = value; RaisePropertyChanged(() => RaoBiaoRates); }
        }

        /// <summary>
        /// 优先资金
        /// </summary>
        [Required]
        private long firstCapital;
        public long FirstCapital
        {
            get { return firstCapital; }
            set { firstCapital = value; RaisePropertyChanged(() => FirstCapital); }
        }
        /// <summary>
        /// 劣后资金
        /// </summary>
        [Required]
        private decimal badCapital;
        public decimal BadCapital
        {
            get { return badCapital; }
            set { badCapital = value; RaisePropertyChanged(() => BadCapital); }
        }


        /// <summary>
        /// 初始资金
        /// </summary>
        [Required]
        private decimal initialCapital;
        public decimal InitialCapital
        {
            get { return initialCapital; }
            set { initialCapital = value; RaisePropertyChanged(() => InitialCapital); }
        }

        /// <summary>
        /// 启停状态
        /// </summary>
        [Required]
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(() => Status); }
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
            /// 名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 可用
            /// </summary>
            public decimal capital_available { get; set; }
            /// <summary>
            /// 现金
            /// </summary>
            public int capital_cash { get; set; }
            /// <summary>
            /// 劣后资金
            /// </summary>
            public decimal capital_inferior { get; set; }
            /// <summary>
            /// 初始资金
            /// </summary>
            public decimal capital_initial { get; set; }
            /// <summary>
            /// 优先资金
            /// </summary>
            public Double capital_priority { get; set; }
            /// <summary>
            /// 饶标资金
            /// </summary>
            public double capital_raobiao { get; set; }
            /// <summary>
            /// 饶标利率
            /// </summary>
            public decimal capital_raobiao_rate { get; set; }
            /// <summary>
            /// 资产预警线
            /// </summary>
            public double capital_stock_value { get; set; }
            /// <summary>
            /// 股本价值
            /// </summary>
            public int capital_stock_value_in { get; set; }
            /// <summary>
            /// 账户市值
            /// </summary>
            public decimal capital_total { get; set; }
            /// <summary>
            /// 平安证券654120036普通账户
            /// </summary>
            public string full_name { get; set; }
            /// <summary>
            /// 限买
            /// </summary>
            public string limit_no_buying { get; set; }
            /// <summary>
            /// 创业板单票限制
            /// </summary>
            public double limit_ratio_gem_single { get; set; }
            /// <summary>
            /// 创业板限制
            /// </summary>
            public double limit_ratio_gem_total { get; set; }
            /// <summary>
            /// 单票限制
            /// </summary>
            public double limit_ratio_single { get; set; }
            /// <summary>
            /// 资产预警线
            /// </summary>
            public double ratio_capital_warning { get; set; }
            /// <summary>
            /// 佣金率
            /// </summary>
            public double ratio_commission { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string remarks { get; set; }
            /// <summary>
            /// ip地址
            /// </summary>
            public string server_ip { get; set; }
            /// <summary>
            /// ip端口
            /// </summary>
            public int server_port { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 交易状态
            /// </summary>
            public int status_trade { get; set; }
            /// <summary>
            /// 同步时间
            /// </summary>
            public string synchronized_time { get; set; }
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
            /// 主账户List集合
            /// </summary>
            public List<Data> Data { get; set; }
        }
    }
}
