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
   public class IEntrustAbnormalScrapListInterface
    {
        public JObject EntrustAbnormalScrapList(int status, string from, string to,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("status", status);
            map.Add("from", from);
            map.Add("to", to);
            string resultJson = HttpHelper.postJson(Urls.ENTRUST_SUMMARY_LIST, map, token);
            Console.Write("委托汇总/异常委托/废单汇总返回结果：" + resultJson);
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
