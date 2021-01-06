using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class PositionTransferModel: ValidateModelBase
    {

        /// <summary>
        /// Id
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 源单元名称
        /// </summary>
        private string sourceUnitName;
        public string SourceUnitName
        {
            get { return sourceUnitName; }
            set { sourceUnitName = value; RaisePropertyChanged(() => SourceUnitName); }
        }

        /// <summary>
        /// 目的单元
        /// </summary>
        private string destinationUnitName;
        public string DestinationUnitName
        {
            get { return destinationUnitName; }
            set { destinationUnitName = value; RaisePropertyChanged(() => DestinationUnitName); }
        }

        /// <summary>
        /// 证券代码
        /// </summary>
        [Required]
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "证券代码只能为数字.")]
        private int securitiesCode;
        public int SecuritiesCode
        {
            get { return securitiesCode; }
            set { securitiesCode = value; RaisePropertyChanged(() => SecuritiesCode); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "数量只能为数值.")]
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value;RaisePropertyChanged(() => Number); }
        }

        /// <summary>
        /// 金额
        /// </summary>
        [Required]
        [RegularExpression(@"^([1-9]\d{0,9}|0)([.]?|(\.\d{1,2})?)$",ErrorMessage ="金额只能为数字")]
        private double amountMoney;
        public double AmountMoney
        {
            get { return amountMoney; }
            set { amountMoney = value; RaisePropertyChanged(() => AmountMoney); }
        }

        /// <summary>
        /// 主账号名称(可选)
        /// </summary>
        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; RaisePropertyChanged(() => AccountName); }
        }

        public class Data
        {
            /// <summary>
            /// 源单元名称
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 目标单元名称
            /// </summary>
            public string name { get; set; }
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
            /// dataList
            /// </summary>
            public List<Data> Data { get; set; }
        }
    }
}
