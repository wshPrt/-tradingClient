using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;

namespace Xh.FastTrading.Wpf.Common.InterFace.Deal
{
   public class IAutomaticModifyInterface
    {
        public JObject AutomaticModify(Int64 id,decimal? price_min, decimal? price_max,int? price_type,
            int? count_min, int? count_max, int? time_min, int? time_max, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("id", id);
            map.Add("price_min", price_min);
            map.Add("price_max", price_max);
            map.Add("price_type", price_type);

            map.Add("count_min", count_min);
            map.Add("count_max", count_max);
            map.Add("time_min", time_min);
            map.Add("time_max", time_max);
            string resultJson = HttpHelper.postJson(Urls.Auto_transaction_Modify, map, token);
            Console.Write("自动交易修改 返回结果：" + resultJson);
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
