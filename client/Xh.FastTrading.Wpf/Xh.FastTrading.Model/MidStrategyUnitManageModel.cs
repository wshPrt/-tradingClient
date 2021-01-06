using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{

    public class MidStrategyUnitManageModel: ValidateModelBase,ICloneable
    {

        /// <summary>
        /// 单元List代码
        /// </summary>
        [Required]
        [RegularExpression(@"/^[A-Za-z0-9\u4e00-\u9fa5]+$/", ErrorMessage = "单元代码不能为特殊符号")]
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value;RaisePropertyChanged(() => Code); }
        }

        /// <summary>
        /// 单元List 名称
        /// </summary>
        [Required]
        [RegularExpression(@"/^[a-z]{4,12}$", ErrorMessage = "单元名称不符合输入范围")]
        private string name;
        public string Name 
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }

        /// <summary>
        ///单元List Id
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]+$",ErrorMessage ="单元Id只能为数字")]
        private int ? unitId;
        public int ? UnitId
        {
            get { return unitId; }
            set { unitId = value; RaisePropertyChanged(() => UnitId);}
        }


        /// <summary>
        /// 单元List
        /// </summary>
        private List<string> unit;  
        public List<string> Unit 
        {
            get { return unit; }
            set { unit = value; RaisePropertyChanged(() => Unit);}
        }

        /// <summary>
        /// 备注
        /// </summary>
        private string remarks;
        public string  Remarks
        {
            get { return remarks; }
            set { remarks = value; RaisePropertyChanged(() => Remarks); }
        }

        /// <summary>
        /// id
        /// </summary>

        private int  id;
        public int  Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        /// 账户组ID
        /// </summary>

        private int ? accountGroupId;
        public int ? AccountGroupId
        {
            get { return accountGroupId; }
            set { accountGroupId = value; RaisePropertyChanged(() => AccountGroupId); }
        }

        /// <summary>
        /// 代码
        /// </summary>

        private string unitCode;
        public string UnitCode
        {
            get { return unitCode; }
            set { unitCode = value; RaisePropertyChanged(() => UnitCode); }
        }

        /// <summary>
        /// 名称
        /// </summary>

        private string unitName;
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; RaisePropertyChanged(() => UnitName); }
        }

        /// <summary>
        /// 地区
        /// </summary>

        private string unitRegion;
        public string UnitRegion
        {
            get { return unitRegion; }
            set { unitRegion = value; RaisePropertyChanged(() => UnitRegion); }
        }

        /// <summary>
        /// 经纪人
        /// </summary>

        private string unitAgent;
        public string UnitAgent
        {
            get { return unitAgent; }
            set { unitAgent = value;RaisePropertyChanged(() => UnitRegion); }
        }

        /// <summary>
        /// 风控员
        /// </summary>

        private string unitRisk;
        public string UnitRisk
        {
            get { return unitRisk; }
            set { unitRisk = value;RaisePropertyChanged(() => UnitRisk); }
        }

        /// <summary>
        /// 实际账户
        /// </summary>

        private string actualAccount;
        public string  ActualAccount
        {
            get { return actualAccount; }
            set { actualAccount = value; RaisePropertyChanged(() => ActualAccount); }
        }

        /// <summary>
        /// 开户时间
        /// </summary>
 
        private string openingTime;
        public string OpeningTime
        {
            get { return openingTime; }
            set { openingTime = value;RaisePropertyChanged(() => OpeningTime);}
        }

        /// <summary>
        /// 主账号
        /// </summary>

        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(() => Account); }
        }

        /// <summary>
        /// 账号池
        /// </summary>

        private int? accountPool;
        public int? AccountPool
        {
            get { return accountPool; }
            set { accountPool = value; RaisePropertyChanged(() => AccountPool); }
        }

        /// <summary>
        /// 保证金
        /// </summary>
     
        private decimal midBond;
        public decimal MidBond
        {
         get { return midBond; }
         set { midBond = value; RaisePropertyChanged(() => MidBond); }
        }


/// <summary>
/// 杠杆
/// </summary>

private decimal ? unitleverage;
        public decimal ? Unitleverage
        {
            get { return unitleverage; }
            set { unitleverage = value; RaisePropertyChanged(() => Unitleverage); }
        }

        /// <summary>
        /// 资金规模
        /// </summary>

        private decimal ? unitFunds;
        public decimal ? UnitFunds
        {
            get { return unitFunds; }
            set { unitFunds = value; RaisePropertyChanged(() => UnitFunds); }
        }

        /// <summary>
        /// 总资产
        /// </summary>

        private decimal ? totalAssets;
        public decimal ? TotalAssets
        {
            get { return totalAssets; }
            set { totalAssets = value; RaisePropertyChanged(() => TotalAssets); }
        }

        /// <summary>
        /// 冻结比例
        /// </summary>

        private decimal ? freezingRatio;
        public decimal ? FreezingRatio
        {
            get { return freezingRatio; }
            set { freezingRatio = value; RaisePropertyChanged(() => FreezingRatio); }
        }

        /// <summary>
        /// 管理费率
        /// </summary>

        private decimal ? manageRatio;
        public decimal ? ManageRatio
        {
            get { return manageRatio; }
            set { manageRatio = value;RaisePropertyChanged(() => manageRatio); }
        }

        /// <summary>
        /// 佣金率
        /// </summary>

        private decimal ? commissionRate;
        public decimal ? CommissionRate
        {
            get { return  commissionRate; }
            set {  commissionRate = value; RaisePropertyChanged(() => CommissionRate); }
        }

        /// <summary>
        /// 软件费率
        /// </summary>

        private decimal ? unitSoftwareRate;
        public decimal ? UnitSoftwareRate
        {
            get { return unitSoftwareRate; }
            set { unitSoftwareRate = value; RaisePropertyChanged(() => UnitSoftwareRate); }
        }

        /// <summary>
        /// 股票个人限制
        /// </summary>

        private int ? individualRestrictionStock;
        public int ? IndividualRestrictionStock
        {
            get { return individualRestrictionStock; }
            set { individualRestrictionStock = value; RaisePropertyChanged(() => IndividualRestrictionStock); }
        }

        /// <summary>
        /// 主板个股比例
        /// </summary>

        private decimal ? proportionBoardStocks;
        public decimal ? ProportionBoardStocks
        {
            get { return proportionBoardStocks; }
            set { proportionBoardStocks = value; RaisePropertyChanged(() => ProportionBoardStocks); }
        }

        /// <summary>
        /// 创业板个股比例
        /// </summary>

        private decimal ? proportionGemStocks;
        public decimal ? ProportionGemStocks
        {
            get { return proportionGemStocks; }
            set{ proportionGemStocks = value;RaisePropertyChanged(() => ProportionGemStocks); }
        }

        /// <summary>
        /// 创业板总比例
        /// </summary>

        private decimal ? totalProportionGem;
        public decimal ? TotalProportionGem
        {
            get { return totalProportionGem; }
            set { totalProportionGem = value; RaisePropertyChanged(() => totalProportionGem); }
        }

        /// <summary>
        /// 中小板个股比例
        /// </summary>

        private decimal ? proportionSmallMediumBoardStocks;
        public decimal ? ProportionSmallMediumBoardStocks
        {
            get { return proportionSmallMediumBoardStocks; }
            set { proportionSmallMediumBoardStocks = value; RaisePropertyChanged(() => ProportionSmallMediumBoardStocks); }
        }

        /// <summary>
        /// 中小板总比例
        /// </summary>

        private decimal ? totalProportionSmallMediumBoards;
        public decimal ? TotalProportionSmallMediumBoards
        {
            get { return totalProportionSmallMediumBoards; }
            set { totalProportionSmallMediumBoards = value;RaisePropertyChanged(() => totalProportionSmallMediumBoards); }
        }

        /// <summary>
        /// 中小创总比例
        /// </summary>

        private decimal ? middleSmallTotalProportion;
        public decimal ? MiddleSmallTotalProportion
        {
            get { return middleSmallTotalProportion; }
            set { middleSmallTotalProportion = value;RaisePropertyChanged(() => MiddleSmallTotalProportion); }
        }

        /// <summary>
        /// 科创板个股比例
        /// </summary>

        private decimal ? scienceInnovationBoardRatio;
        public decimal ? ScienceInnovationBoardRatio
        {
            get { return scienceInnovationBoardRatio; }
            set { scienceInnovationBoardRatio = value;  RaisePropertyChanged(() => ScienceInnovationBoardRatio); }
        }

        /// <summary>
        /// 科创板总比例
        /// </summary>

        private decimal ? scienceInnovationTotalRatio;
        public decimal ? ScienceInnovationTotalRatio
        {
            get { return scienceInnovationTotalRatio; }
            set { scienceInnovationTotalRatio = value;RaisePropertyChanged(() => ScienceInnovationTotalRatio);}
        }

        /// <summary>
        /// 委托价格范围限制
        /// </summary>

        private int priceLimit; 
        public int PriceLimit
        {
            get { return priceLimit; }
            set { priceLimit = value;  RaisePropertyChanged(() => PriceLimit); }
        }

        /// <summary>
        /// 警戒线比例
        /// </summary>

        private decimal ? cordonRatio;
        public decimal ? CordonRatio
        {
            get { return cordonRatio; }
            set { cordonRatio = value;RaisePropertyChanged(() => CordonRatio); }
        }

        /// <summary>
        ///  平仓线比例
        /// </summary>
 
        private decimal ? levelingLineRatio;
        public decimal ? LevelingLineRatio 
        {
            get { return levelingLineRatio; }
            set { levelingLineRatio = value; }
        }

        /// <summary>
        /// 禁买股票
        /// </summary>

        private string noBuyingShares;
        public string NoBuyingShares
        {
            get { return noBuyingShares; }
            set { noBuyingShares = value;RaisePropertyChanged(() => NoBuyingShares); }
        }


        /// <summary>
        /// 是否全局验证
        /// </summary>
        private bool isFormValid;
        public bool IsFormValid
        {
            get { return isFormValid; }
            set { isFormValid = value; RaisePropertyChanged(() => IsFormValid); }
        }

        public class Data
        {
            /// <summary>
            /// 单元代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 单元ID
            /// </summary>
            public int  id { get; set; }
            /// <summary>
            /// 单元名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 主账户池ID
            /// </summary>
            public int ? account_group_id { get; set; }
            /// <summary>
            /// 地区
            /// </summary>
            public string area { get; set; }
            /// <summary>
            /// 经纪人
            /// </summary>
            public string broker { get; set; }
            /// <summary>
            /// 保证金
            /// </summary>
            public decimal bond { get; set; }
            /// <summary>
            /// 资金规模
            /// </summary>
            public decimal ? capital_scale  { get; set; }
            /// <summary>
            /// 总资产
            /// </summary>
            public decimal capital_Total { get; set; }
            /// <summary>
            /// 存款
            /// </summary>
            public int ? deposit { get; set; }
            /// <summary>
            /// 杠杆
            /// </summary>
            public decimal ? lever { get; set; }
            /// <summary>
            /// 禁买股票
            /// </summary>
            public string limit_no_buying { get; set; }
            /// <summary>
            ///  委托价格范围限制，1不限制，0五档内报价
            /// </summary>
            public int ? limit_order_price { get; set; }
            /// <summary>
            /// 创业板个股比例
            /// </summary>
            public decimal ? limit_ratio_gem_single { get; set; }
            /// <summary>
            /// 创业板总比例
            /// </summary>
            public decimal ? limit_ratio_gem_total { get; set; }
            /// <summary>
            /// 主板个股比例
            /// </summary>
            public decimal ? limit_ratio_mbm_single { get; set; }
            /// <summary>
            /// 中小板个股比例
            /// </summary>
            public decimal ? limit_ratio_sme_single { get; set; }
            /// <summary>
            /// 中小板总比例
            /// </summary>
            public decimal ? limit_ratio_sme_total { get; set; }
            /// <summary>
            /// 中小创总比例
            /// </summary>
            public decimal ? limit_ratio_smg_total { get; set; }
            /// <summary>
            /// 科创板个股比例
            /// </summary>
            public decimal ? limit_ratio_star_single { get; set; }
            /// <summary>
            /// 科创板总比例
            /// </summary>
            public decimal ? limit_ratio_star_total { get; set; }
            /// <summary>
            /// 股票个数限制
            /// </summary>
            public int ? limit_stock_count { get; set; }
            /// <summary>
            /// 开户时间
            /// </summary>
            public DateTime ? opened_time { get; set; }
            /// <summary>
            /// 平仓线比例
            /// </summary>
            public decimal ? ratio_close_position { get; set; }
            /// <summary>
            /// 佣金率
            /// </summary>
            public decimal ? ratio_commission { get; set; }
            /// <summary>
            /// 冷结比例
            /// </summary>
            public decimal ? ratio_freezing { get; set; }
            /// <summary>
            /// 管理费率
            /// </summary>
            public decimal ? ratio_management_fee { get; set; }
            /// <summary>
            /// 软件费率
            /// </summary>
            public decimal ? ratio_software_fee { get; set; }
            /// <summary>
            /// 警告比例
            /// </summary>
            public decimal ? ratio_warning { get; set; }
            /// <summary>
            /// 风险控制
            /// </summary>
            public string risk_controller { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public int ? status { get; set; }
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
            /// Data
            /// </summary>
            public List<Data> Data { get; set; }
        }

        public class UnitRoot 
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<UnitData> Data { get; set; }
        }
        public class UnitData 
        {
            public string code { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public class PoolRoot
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public List<UnitData> Data { get; set; }
        }
        public class PoolData 
        {
            public string code { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
    }
}
