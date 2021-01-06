using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;
using static Xh.FastTrading.Wpf.Model.UserInfoModel;

namespace Xh.FastTrading.Wpf.Common.InterFace.UserManage
{
  public  class IFunctionPowerInterface
    {
        public JObject SettingPower(int id, List<int> authority_modules, List<int> authority_platforms, string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("id", id);
            map.Add("authority_modules", authority_modules);
            map.Add("authority_platforms", authority_platforms);
            string resultJson = HttpHelper.postJson(Urls.POWER_SETTINGS, map, token);
            Console.Write("设置用户权限返回结果：" + resultJson);
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
