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
  public class IUnitPositionInterface
    {
        
            public JObject UnitPosition(int Request, string token)
            {
                JObject jobject = new JObject();
                string resultJson = HttpHelper.postJsonOne(Urls.POSITION_LIST, Request, token);
                Console.Write("单元持仓列表 返回结果：" + resultJson);
                JObject json = (JObject)Json_Check(resultJson);
                jobject["Message"] = json;
                return jobject;
             }

        /// <summary>
        /// 账号代码
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject AccountCode(int unitId,string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.ACCOUNT_CODE, unitId, token);
            Console.Write("账号代码 返回结果：" + resultJson);
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
