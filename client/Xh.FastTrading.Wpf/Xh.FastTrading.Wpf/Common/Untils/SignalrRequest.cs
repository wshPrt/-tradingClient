using GalaSoft.MvvmLight.Threading;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.Common.Untils
{
   public class SignalrRequest
    {
        public HubConnection Connection;
        private static readonly string SIGNAL_URL = ConfigurationManager.ConnectionStrings["SIGNAL_URL"].ConnectionString;
        public static string platform = "1";
        IHubProxy proxy;
       public void Signalr() 
        {
            //创建连接
            Connection = new HubConnection(SIGNAL_URL);

            //创建代理 
            proxy = Connection.CreateHubProxy("MessageHub");
            
            //注册接收事
            proxy.On<string>("Message", Receive);

            //启动连接
            try
            {
                Connection.Start().Wait(2000);
            }
            catch (HttpRequestException ex)
            {
                MessageDialogManager.ShowDialogAsync("无法连接到服务器：请在连接客户端之前启动服务器.");
                return;
            }

            //发送订阅请求
            var a =  proxy?.Invoke("Subscribe", UserToken.token, platform);
            Console.WriteLine();
        }
        private void Receive(string message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if(message == "1007")
                {
                    Uninstall.Start();
                    Environment.Exit(Environment.ExitCode);
                }
                //if (message == "1001")
                //{
                //    MessageDialogManager.ShowDialogAsync("消息订阅成功!");
                //}

            });
            
        }
    }
}
