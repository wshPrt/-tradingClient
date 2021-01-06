using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class MasterAccountPositionModel : ValidateModelBase
    {

        /// <summary>
        /// Id
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value;RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        /// 主账户
        /// </summary>
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value;RaisePropertyChanged(() => Account);}
        }

        /// <summary>
        /// 证券代码
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "证券代码只能为数字")]
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
            set { securitiesName = value;RaisePropertyChanged(() => SecuritiesName); }
        }

        /// <summary>
        /// 主账户数量
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "主账户数量只能为数字")]
        private int accountNumber;
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; RaisePropertyChanged(() => AccountNumber); }
        }

        /// <summary>
        /// 系统数量
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "系统数量只能为数字")]
        private int systemNumber;
        public int SystemNumber
        {
            get { return systemNumber; }
            set { systemNumber = value; RaisePropertyChanged(() => SystemNumber); }
        }

        /// <summary>
        /// 差额数量
        /// </summary>
        private int differenceNumber;
        public int DifferenceNumber
        {
            get { return differenceNumber; }
            set { differenceNumber = value; RaisePropertyChanged(() => DifferenceNumber); }
        }


        public class CommonBox 
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 证券Id
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
            /// 主账号数量
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 系统数量
            /// </summary>
            public int count_in { get; set; }

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
            /// DataList
            /// </summary>
            public List<Data> Data { get; set; }
        }
    }
}
