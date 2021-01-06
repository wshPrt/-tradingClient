using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
   public class BoMenumodel
    {
        public string Name { get; set; }
        public string Header { get; set; }

        public BoMenumodel[] Children { get; set; }
        public object Model { get; set; }
    }

    public class BuyModel
    {
        public string Title { get; set; }
    }
    public class SellModel
    {
        public string Title { get; set; }
    }
    public class BackModel
    {
        public string Title { get; set; }
    }
    public class AutoMaticModel
    {
        public string Title { get; set; }
    }
    public class SpecifyModel
    {
        public string Title { get; set; }
    }
    public class CancelModel
    {
        public string Title { get; set; }
    }
    public class RiskStrategyModel
    {
        public string Title { get; set; }
    }
    public class AssetsModel
    {
        public string Title { get; set; }
    }
    public class EntrustModel
    {
        public string Title { get; set; }
    }
    public class DealModel
    {
        public string Title { get; set; }
    }
    public class CapitalModel
    {
        public string Title { get; set; }
    }
    public class UnitTrategyModel
    {
        public string Title { get; set; }
    }
    public class RuleModel
    {
        public string Title { get; set; }
    }
    public class PositionModel
    {
        public string Title { get; set; }
    }
    public class AbnormalEntrustModel
    {
        public string Title { get; set; }
    }
    public class EntrustSummaryModel
    {
        public string Title { get; set; }
    }

    public class AbnormalCancelModel
    {
        public string Title { get; set; }
    }
    public class VoidModel
    {
        public string Title { get; set; }
    }
    public class SystemDealModel
    {
        public string Title { get; set; }
    }
    public class SystemDealOutModel
    {
        public string Title { get; set; }
    }
    public class BanModel
    {
        public string Title { get; set; }
    }
    public class LiquidationModel
    {
        public string Title { get; set; }
    }
    public class UserManagemodel
    {
        public string Title { get; set; }
    }
    public class OpenCloseModel
    {
        public string Title { get; set; }
    }
    public class DividendsModel
    {
        public string Title { get; set; }
    }
    public class TransferModel
    {
        public string Title { get; set; }
    }
    public class AccountModel
    {
        public string Title { get; set; }
    }
    public class AccountManageModel
    {
        public string Title { get; set; }
    }
    public class AccountPositionModel
    {
        public string Title { get; set; }
    }
}
