
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.Common
{
    class HttpHelper
    {

        private static string ContentType = "application/x-www-form-urlencoded";

        private static string ContentTypeJson = "application/json";

        public static string post(string url, Dictionary<string, Object> param,string token)
        {
            string json = JsonConvert.SerializeObject(param);
            Console.Write("Http Request : " + json);
            return postWebRequest(url, json, ContentType, Encoding.UTF8,token);
        }

        public static string postJson(string url, Dictionary<string, Object> param,string token)
        {
            string json = JsonConvert.SerializeObject(param);
            Console.Write("Http Request : " + json);
            return postWebRequest(url, json, ContentTypeJson, Encoding.UTF8,token);
        }
        public static string postJsonOne(string url, int param, string token)
        {
            string json = JsonConvert.SerializeObject(param);
            Console.Write("Http Request : " + json);
            return postWebRequest(url, json, ContentTypeJson, Encoding.UTF8, token);
        }

        public static string postReqeust(string url, string param,string token)
        {
            return postWebRequest(url, param, ContentType, Encoding.UTF8,token);
        }

        //数据上传时使用
        public static string postReqeust(string url, string param, string contentType,string token)
        {
            return postWebRequest(url, param, contentType, Encoding.UTF8,token);
        }

        public static string postWebRequest(string postUrl, string paramData, string contentType, Encoding dataEncode,string Token)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = contentType;
                webReq.ContentLength = byteArray.Length;
                webReq.Headers.Add("platform", "1");
                webReq.Headers.Add("token", Token);
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                DispatcherHelper.CheckBeginInvokeOnUI( async () =>
                {
                    MessageDialogManager.ShowDialogAsync(ex.Message);
                });
            }
            return ret;
        }
    }
}

