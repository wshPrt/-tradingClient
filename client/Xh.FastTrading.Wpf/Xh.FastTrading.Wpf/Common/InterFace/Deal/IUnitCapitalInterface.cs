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
  public class IUnitCapitalInterface
    {
        /// <summary>
        /// 买入卖出单元资金
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject UnitCapital(int id, string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.ASSETS_UNIT_CAPITAL, id, token);
            Console.Write("买入卖出单元资金 返回结果：" + resultJson);
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
