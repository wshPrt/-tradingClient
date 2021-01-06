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
   public class IMidStrategyInterface
    {
        public JObject UnitList(int Request, string token)
        {
            JObject jobject = new JObject(); 
            string resultJson = HttpHelper.postJsonOne(Urls.ACCOUNT_POOL_LIST, Request, token);
            Console.Write("中期策略的账号池列表 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }
        public JObject Strategy(string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJson(Urls.STRATEGY_USER_UNIT_LIST, null, token);
            Console.Write("中期策略单元列表返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        
        public JObject MidStrategy(string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJson(Urls.NOT_POINT_USER_UNIT_LIST, null, token);
            Console.Write("未指向用户的单元列表返回结果：" + resultJson);
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
