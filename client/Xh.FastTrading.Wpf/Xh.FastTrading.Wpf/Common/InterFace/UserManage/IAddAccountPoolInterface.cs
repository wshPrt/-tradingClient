using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;

namespace Xh.FastTrading.Wpf.Common.InterFace.UserManage
{
   public class IAddAccountPoolInterface
    {
            public JObject AddAccountPool(string code, string name,string token)
            {
                JObject jobject = new JObject();
                Dictionary<string, Object> map = new Dictionary<string, object>();
                map.Add("code", code);
                map.Add("name", name);
                string resultJson = HttpHelper.postJson(Urls.ADD_ACCOUNT_POOL, map, token);
                Console.Write("新增主账户池返回结果：" + resultJson);
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
