using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;

namespace Xh.FastTrading.Wpf.Common.InterFace.UnitManage
{
   public class IModifyMidStrategyInterface
    {
        /// <summary>
        /// 修改中期单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="broker"></param>
        /// <param name="risk_controller"></param>
        /// <param name="account_group_id"></param>
        /// <param name="lever"></param>
        /// <param name="ratio_management_fee"></param>
        /// <param name="ratio_commission"></param>
        /// <param name="ratio_software_fee"></param>
        /// <param name="limit_stock_count"></param>
        /// <param name="limit_ratio_mbm_single"></param>
        /// <param name="limit_ratio_gem_single"></param>
        /// <param name="limit_ratio_gem_total"></param>
        /// <param name="limit_ratio_sme_single"></param>
        /// <param name="limit_ratio_sme_total"></param>
        /// <param name="limit_ratio_smg_total"></param>
        /// <param name="limit_ratio_star_single"></param>
        /// <param name="limit_ratio_star_total"></param>
        /// <param name="ratio_warning"></param>
        /// <param name="ratio_close_position"></param>
        /// <param name="limit_no_buying"></param>
        /// <param name="limit_order_price"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject ModifyMidStrategy(int? id,string code, string name, string area, string broker,
        string risk_controller, int? account_group_id, decimal? lever, decimal? ratio_management_fee,
        decimal? ratio_commission, decimal? ratio_software_fee, int? limit_stock_count, decimal? limit_ratio_mbm_single,
        decimal? limit_ratio_gem_single, decimal? limit_ratio_gem_total, decimal? limit_ratio_sme_single, decimal? limit_ratio_sme_total,
        decimal? limit_ratio_smg_total, decimal? limit_ratio_star_single, decimal? limit_ratio_star_total, decimal? ratio_warning,
        decimal? ratio_close_position, string? limit_no_buying, int? limit_order_price, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("id", id);
            map.Add("code", code);
            map.Add("name", name);
            map.Add("area", area);
            map.Add("broker", broker);

            map.Add("risk_controller", risk_controller);
            map.Add("account_group_id", account_group_id);
            map.Add("lever", lever);
            map.Add("ratio_management_fee", ratio_management_fee);

            map.Add("ratio_commission", ratio_commission);
            map.Add("ratio_software_fee", ratio_software_fee);
            map.Add("limit_stock_count", limit_stock_count);
            map.Add("limit_ratio_mbm_single", limit_ratio_mbm_single);

            map.Add("limit_ratio_gem_single", limit_ratio_gem_single);
            map.Add("limit_ratio_gem_total", limit_ratio_gem_total);
            map.Add("limit_ratio_sme_single", limit_ratio_sme_single);
            map.Add("limit_ratio_sme_total", limit_ratio_sme_total);

            map.Add("limit_ratio_smg_total", limit_ratio_smg_total);
            map.Add("limit_ratio_star_single", limit_ratio_star_single);
            map.Add("limit_ratio_star_total", limit_ratio_star_total);
            map.Add("ratio_warning", ratio_warning);

            map.Add("ratio_close_position", ratio_close_position);
            map.Add("limit_no_buying", limit_no_buying);
            map.Add("limit_order_price", limit_order_price);
            string resultJson = HttpHelper.postJson(Urls.MODIFY_UNIT, map, token);
            Console.Write("修改中期策略单元表返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        public JObject AccountPoolFilter(string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJson(Urls.ACCOUNT_POOL_FILTER_LIST, null, token);
            Console.Write("主账户池过滤列表 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }
        private static object Json_Check(string s)
        {
            try
            {
                string a = Regex.Match(s, @"\((?<json>.+?)\)").Groups["json"].Value;
                if (a == string.Empty)
                {
                    return JsonConvert.DeserializeObject(s.TrimStart(new char[] { '(' }).TrimEnd(new char[] { ')' }).Trim());
                }
                else
                {
                    return JsonConvert.DeserializeObject(a);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
