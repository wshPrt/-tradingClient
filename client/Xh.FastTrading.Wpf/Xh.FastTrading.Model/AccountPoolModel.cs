using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class AccountPoolModel : ValidateModelBase
    {



        /// <summary>
        ///  ID
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        /// 代码
        /// </summary>
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(() => Code); }
        }


        /// <summary>
        /// 名称
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }

        /// <summary>
        /// 主账号池代码
        /// </summary>
        private string accountCode;
        public string AccountCode
        {
            get { return accountCode; }
            set { accountCode = value; RaisePropertyChanged(() => AccountCode); }
        }

        private int accountId; 

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; RaisePropertyChanged(() => AccountId); }
        }

        /// <summary>
        /// 账号池可用
        /// </summary>
        private List<item> items;
        public List<item> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChanged(() => items); }
        }


        /// <summary>
        /// 主账号
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "主账户只能为数字")]
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(() => Account); }
        }

        /// <summary>
        ///优先策略 
        /// </summary>
        private string priorityStrategy;
        public string PriorityStrategy
        {
            get { return priorityStrategy; }
            set { priorityStrategy = value; RaisePropertyChanged(() => PriorityStrategy); }
        }

        /// <summary>
        /// 账号池可用
        /// </summary>
        private long accountPoolAvailable;
        public long AccountPoolAvailable
        {
            get { return accountPoolAvailable; }
            set { accountPoolAvailable = value; RaisePropertyChanged(() => AccountPoolAvailable); }
        }

        /// <summary>
        /// 关联单元
        /// </summary>
        private int relationUnit;
        public int RelationUnit
        {
            get { return relationUnit; }
            set { relationUnit = value; RaisePropertyChanged(() => RelationUnit); }
        }

        /// <summary>
        /// 单元可用
        /// </summary>
        private long unitAvailable;
        public long UnitAvailable
        {
            get { return unitAvailable; }
            set { unitAvailable = value; RaisePropertyChanged(() => UnitAvailable); }
        }


        /// <summary>
        /// ObservableCollection的属性
        /// </summary>
        private ObservableCollection<ObserAccountPoolModel> a;
        public ObservableCollection<ObserAccountPoolModel> A
        {
            get { return a; }
            set { a = value; RaisePropertyChanged(() => A); }
        }

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
            /// items
            /// </summary>
            public List<item> items { get; set; }
            /// <summary>
            /// 优先策略
            /// </summary>
            public string PriorityStrategy { get; set; }

        }

        public class item
        {
            /// <summary>
            /// 主账号池代码
            /// </summary>
            public string account_code { get; set; }
            /// <summary>
            /// 主账号池Id
            /// </summary>
            public int account_id { get; set; }
            /// <summary>
            /// 主账号池名称
            /// </summary>
            public string account_name { get; set; }
            public decimal capital_allow { get; set; }

            /// <summary>
            /// 买入顺序
            /// </summary>
            public int sort_buy { get; set; }
            /// <summary>
            /// 卖出顺序
            /// </summary>
            public int sort_sell { get; set; }
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
