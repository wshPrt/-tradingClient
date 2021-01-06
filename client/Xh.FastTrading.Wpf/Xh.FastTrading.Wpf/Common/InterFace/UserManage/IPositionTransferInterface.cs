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
   public class IPositionTransferInterface
    {
        public JObject PositionTransfer(string from_unit_id,string to_unit_id,int code,double count,int to_account_id,string token)
        {
            JObject jobject = new JObject();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("from_unit_id", from_unit_id);
            map.Add("to_unit_id", to_unit_id);
            map.Add("code", code);
            map.Add("count", count);
            map.Add("to_account_id", to_account_id);
            string resultJson = HttpHelper.postJson(Urls.POSITION_TRANSFER, map, token);
            Console.Write("持仓划转返回结果：" + resultJson);
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
