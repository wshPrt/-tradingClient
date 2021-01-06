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
   public class IDealBuyInterface
    {
        public JObject UnitList(int UserId, string token)
        {
            JObject jobject = new JObject(); 

            string resultJson = HttpHelper.postJsonOne(Urls.APPOINT_USER_UNIT_LIST, UserId, token);
            Console.Write("指定用户的单元列表 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        public JObject DealBuy(int unit_id, string code, decimal? price, int type,int? count, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("unit_id", unit_id);
            map.Add("code", code);
            map.Add("price", price);
            map.Add("type", type);
            map.Add("count", count);
            string resultJson = HttpHelper.postJson(Urls.BUY_SELL_ORDER, map, token);
            Console.Write("买入下单 返回结果：" + resultJson);
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
