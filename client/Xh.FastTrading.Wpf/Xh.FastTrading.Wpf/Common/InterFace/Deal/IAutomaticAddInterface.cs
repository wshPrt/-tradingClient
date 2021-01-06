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
   public class IAutomaticAddInterface
    {
        public JObject AutomaticAdd(string code,int? type, Int64 unit_id, Int64 account_id,
            decimal? price_min,decimal? price_max, int? price_type,int? count_total,
            int? count_min,int? count_max,int? time_min,int? time_max,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("code",code);
            map.Add("type",type);
            map.Add("unit_id",unit_id);
            map.Add("account_id",account_id);

            map.Add("price_min",price_min);
            map.Add("price_max",price_max);
            map.Add("price_type",price_type);
            map.Add("count_total",count_total);

            map.Add("count_min",count_min);
            map.Add("count_max",count_max);
            map.Add("time_min",time_min);
            map.Add("time_max",time_max);
            string resultJson = HttpHelper.postJson(Urls.Auto_transaction_ADD, map, token);
            Console.Write("自动交易新增 返回结果：" + resultJson);
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
