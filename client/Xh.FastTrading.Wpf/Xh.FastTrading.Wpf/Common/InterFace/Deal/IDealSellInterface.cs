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
   public class IDealSellInterface
    {
        public JObject UnitList(int Request, string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.ACCOUNT_UNIT_LIST, Request, token);
            Console.Write("主账户所在的单元列表 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        public JObject DealSell(int unit_id, string code, decimal? price, int type, int? count, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("unit_id", unit_id);
            map.Add("code", code);
            map.Add("price", price);
            map.Add("type", type);
            map.Add("count", count);
            string resultJson = HttpHelper.postJson(Urls.BUY_SELL_ORDER, map, token);
            Console.Write("卖出下单 返回结果：" + resultJson);
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
