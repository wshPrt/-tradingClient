
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;


namespace Xh.FastTrading.Wpf.Common.InterFace
{
    public class ILoginInterface
    {
        public JObject login(string code, string password)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("code", code);
            map.Add("password", password);
            string resultJson = HttpHelper.postJson(Urls.LOGIN_URL, map, null);
            Console.Write("登录验证返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["login"] = json;
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

        public JObject Version(int version_current)
        {
            string resultJson = HttpHelper.postJsonOne(Urls.VERSION_URL, version_current, null);
            return (JObject)Json_Check(resultJson);
        }
    }
}