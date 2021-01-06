using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;

namespace Xh.FastTrading.Wpf.Common.InterFace.UserManage
{
   public class IAddAccountInterface
    {
        public JObject AddAccount(string? server_ip,int? server_port, string code,string name,
            string full_name, string remarks,string limit_no_buying, double ratio_commission,
            double limit_ratio_single,double limit_ratio_gem_single,double limit_ratio_gem_total,double ratio_capital_warning,
            decimal capital_initial,decimal capital_inferior,long capital_priority,double capital_raobiao,
            decimal capital_raobiao_rate,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("server_ip", server_ip);
            map.Add("server_port", server_port);
            map.Add("code", code);
            map.Add("name", name);

            map.Add("full_name", full_name);
            map.Add("remarks", remarks);
            map.Add("limit_no_buying", limit_no_buying);
            map.Add("ratio_commission", ratio_commission);
            
            map.Add("limit_ratio_single", limit_ratio_single);
            map.Add("limit_ratio_gem_single", limit_ratio_gem_single);
            map.Add("limit_ratio_gem_total", limit_ratio_gem_total);
            map.Add("ratio_capital_warning", ratio_capital_warning);

            map.Add("capital_initial", capital_initial);
            map.Add("capital_inferior", capital_inferior);
            map.Add("capital_priority", capital_priority);
            map.Add("capital_raobiao", capital_raobiao);

            map.Add("capital_raobiao_rate", capital_raobiao_rate);
            string resultJson = HttpHelper.postJson(Urls.ADD_ACCOUNT, map, token);
            Console.Write("新增主账户列表返回结果：" + resultJson);
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
