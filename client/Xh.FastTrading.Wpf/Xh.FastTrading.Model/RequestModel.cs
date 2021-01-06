using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class Jobject1
    {
        public Request request { get; set; }
        public Response response { get; set; }
        public string respData { get; set; }
    }

    public class RequestResponse
    {
        public Request request { get; set; }
        public Dictionary<string, Object> response { get; set; }
    }

    public class QueryRequestResponse
    {
        public Request request { get; set; }
        public string response { get; set; }
        public string respData { get; set; }
    }

    public class AsynRequestResponse
    {
        public Request request { get; set; }
        public List<dynamic> Response { get; set; }
        public string respData { get; set; }
    }

    public class Request
    {
        public string code { get; set; }
        public string password { get; set; }
        public string password_old { get; set; }
        public string password_new { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public int id { get; set; }
        public int[] authority_modules { get; set; }
        public int[] authority_platforms { get; set; }
        public int status { get; set; }
        public int[] unit_ids { get; set; }
        public string server_ip { get; set; }
        public int server_port { get; set; }
        public string full_name { get; set; }
        public string remarks { get; set; }
        public string limit_no_buying { get; set; }
        public decimal ratio_commission { get; set; }
        public int limit_ratio_single { get; set; }
        public int limit_ratio_gem_single { get; set; }
        public int limit_ratio_gem_total { get; set; }
        public int ratio_capital_warning { get; set; }
        public int capital_initial { get; set; }
        public int capital_inferior { get; set; }
        public int capital_priority { get; set; }
        public int capital_raobiao { get; set; }
        public int capital_raobiao_rate { get; set; }
        public int priority_strategy { get; set; }
        public int[] items { get; set; }
        public int account_id { get; set; }
        public int capital_available { get; set; }
        public int sort_buy { get; set; }
        public int sort_sell { get; set; }
        public string area { get; set; }
        public string broker { get; set; }
        public string risk_controller { get; set; }
        public int account_group_id { get; set; }
        public int lever { get; set; }
        public decimal ratio_management_fee { get; set; }
        public decimal limit_ratio_mbm_single { get; set; }
        public decimal ratio_software_fee { get; set; }
        public int limit_stock_count { get; set; }
        public decimal limit_ratio_sme_single { get; set; }
        public decimal limit_ratio_sme_total { get; set; }
        public decimal limit_ratio_smg_total { get; set; }
        public int limit_ratio_star_single { get; set; }
        public int limit_ratio_star_total { get; set; }
        public decimal ratio_warning { get; set; }
        public decimal ratio_close_position { get; set; }
        public int limit_order_price { get; set; }
        public int ratio_freezing { get; set; }
        public int unit_id { get; set; }
        public int type { get; set; }
        public int action { get; set; }
        public int amount { get; set; }
        public string remark { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string Token { get; set; }
    }

    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string[] Data { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string account_name { get; set; }
        public int cancel_count { get; set; }
        public int count { get; set; }
        public decimal deal_average_price { get; set; }
        public int deal_count { get; set; }
        public int order_no { get; set; }
        public int platform { get; set; }
        public decimal price { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
        public string time { get; set; }
        public int type { get; set; }
        public string user_name { get; set; }
        public string unit_name { get; set; }
        public decimal commission { get; set; }
        public string deal_no { get; set; }
        public decimal money { get; set; }
        public decimal stamp_tax { get; set; }
        public decimal transfer_fee { get; set; }
        public int count_sellable { get; set; }
        public int count_today_buy { get; set; }
        public int count_today_sell { get; set; }
        public int price_cost { get; set; }
        public int price_cost_today_buy { get; set; }
        public int price_cost_today_sell { get; set; }
    }

}
