using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Common.Untils;

namespace Xh.FastTrading.Wpf.Common.InterFace.Query
{
   public  class IAssetsInterface
    {
        /// <summary>
        /// 单元资金接口
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject AssetsUnitCapital( int Request , string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.ASSETS_UNIT_CAPITAL, Request, token);
            Console.Write("单元资金 返回结果：" + resultJson);
            JObject json = (JObject)Json_Check(resultJson);
            jobject["Message"] = json;
            return jobject;
        }

        /// <summary>
        /// 单元持仓列表 接口
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject AssetsUnitPositionList(int Request, string token)
        {
            JObject jobject = new JObject();
            string resultJson = HttpHelper.postJsonOne(Urls.ASSETS_UNIT_POSITION, Request, token);
            Console.Write("单元持仓列表 返回结果：" + resultJson);
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
