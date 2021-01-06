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
   public class IDealCancelOrderInterface
    {
        /// <summary>
        /// 可撤委托列表
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject DealCancelOrder(int Request, string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.CANCELLATIONS_REVOCABLE_DELEGATE_LIST, Request, token);
            Console.Write("可撤委托列表 返回结果：" + resultJson);
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
        public JObject UnitList(int unitId, string token)
        {
            JObject jobject = new JObject();
            //string resultJson = HttpHelper.postJsonOne(Urls.ACCOUNT_UNIT_LIST, unitId, token);
            string resultJson = HttpHelper.postJsonOne(Urls.APPOINT_USER_UNIT_LIST, unitId, token);
            Console.Write("主账户所在的单元列表 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        /// <summary>
        /// 批量撤单
        /// </summary>
        /// <param name="unit_id"></param>
        /// <param name="items"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject BatchCancelOrder(int unit_id, List<Int64> trade_nos, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("unit_id", unit_id);
            map.Add("trade_nos", trade_nos);
            string resultJson = HttpHelper.postJson(Urls.BATCH_CANCELLATION, map, token);
            Console.Write("批量撤单 返回结果：" + resultJson);
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
