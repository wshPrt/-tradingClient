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
   public class IDealAppointInterface
    {
        /// <summary>
        /// 指定交易 下单接口
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject DealAppoint(int? unit_id,int account_id,string code,decimal? price,int type, int? count,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("unit_id", unit_id);
            map.Add("account_id", account_id);
            map.Add("code", code);
            map.Add("price", price);
            map.Add("type", type);
            map.Add("count", count);
            string resultJson = HttpHelper.postJson(Urls.APPOINT_TRANSACTION_ORDER, map, token);
            Console.Write("指定交易下单 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        /// <summary>
        /// 单元下拉框 接口
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject UnitList(int userId , string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.CURRENT_USER_UNIT_LIST, userId, token);
            Console.Write("主账户所在的单元列表 返回结果：" + resultJson);
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
