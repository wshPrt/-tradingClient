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
    public class IWithinOutDealSummaryInterface
    {
        /// <summary>
        /// 系统内成交汇总 列表
        /// </summary>
        public JObject WithinDealSummary(int status,string fromTime,string toTime,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("status", status);
            map.Add("from", fromTime);
            map.Add("to", toTime);
            string resultJson = HttpHelper.postJson(Urls.DEAL_SUMMARY_LIST, map, token);
            Console.Write("系统内/系统外成交汇总返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        /// <summary>
        /// 系统外成交汇总列表
        /// </summary>
        public JObject WithinOutDealSummary( int status, string fromTime, string toTime, int account_id, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("status", status);
            map.Add("from", fromTime);
            map.Add("to", toTime);
            map.Add("account_id", account_id);
            string resultJson = HttpHelper.postJson(Urls.OUT_DEAL_SUMMARY_LIST, map, token);
            Console.Write("系统内/系统外成交汇总返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }
        /// <summary>
        /// 选择交易账号
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject AccountFilterList(string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJson(Urls.ACCOUNT_FILTER_LIST, null, token);
            Console.Write("主账户过滤列表返回结果：" + resultJson);
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
