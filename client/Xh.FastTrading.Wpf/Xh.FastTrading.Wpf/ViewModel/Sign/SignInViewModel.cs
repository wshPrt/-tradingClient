using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xh.FastTrading.Wpf.ViewModel;
using Xh.FastTrading.Wpf.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.IO;
using static Xh.FastTrading.Wpf.Common.Untils.Configuration;
using Xh.FastTrading.Wpf.Commands.MD5Poxy;
using Xh.FastTrading.Wpf.Common.InterFace;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views;
using System.Runtime.Remoting.Contexts;
using GalaSoft.MvvmLight.Threading;
using System.Data.SqlClient;
using Xh.FastTrading.Wpf.Views.SystemManage;
using Newtonsoft.Json;
using System.Diagnostics;
using Xh.FastTrading.Wpf.Commands.Base;
using Xh.FastTrading.Wpf.Common;
using Xh.FastTrading.Wpf.Common.Untils;
using HQ;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Commands.IniFile;
using static Xh.FastTrading.Wpf.Model.UserInfoModel;

namespace Xh.FastTrading.Wpf.ViewModel
{
    public class SignInViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        SignalrRequest signalrRequest = new SignalrRequest();
        public SignInViewModel()
        {
            DispatcherHelper.Initialize();
            LoginInfo = new LoginModel();
            //List = new ObservableCollection<LoginModel>();
            //List.Add(LoginInfo);
        }

        private LoginModel loginInfo;
        public LoginModel LoginInfo
        {
            get { return loginInfo; }
            set { loginInfo = value; RaisePropertyChanged(() => LoginInfo); }
        }
        /// <summary>
        /// dataGrid集合
        /// </summary>
        private ObservableCollection<LoginModel> _list;
        public ObservableCollection<LoginModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }

        private string userName;
        public string UserName 
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(() => UserName); }
        }


        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                if (toClose != value)
                {
                    toClose = value;
                    this.RaisePropertyChanged("ToClose");
                }
            }

        }
        #region 命令(Binding Command)

        /// <summary>
        /// 登录
        /// </summary>
        private RelayCommand _signCommand;
        public RelayCommand SignCommand
        {
            get
            {
                if (_signCommand == null)
                {
                    _signCommand = new RelayCommand(() => Login());
                }
                return _signCommand;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(() => ApplicationShutdown());
                }
                return _exitCommand;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(() => ApplicationShutdown());
                }
                return _cancelCommand;
            }
            set { _cancelCommand = value; }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        private RelayCommand _modifyPwdCommand;
        public RelayCommand ModifyPwdCommand
        {
            get
            {
                if (_modifyPwdCommand == null)
                {
                    _modifyPwdCommand = new RelayCommand(() => ModifyPassword());
                }
                return _modifyPwdCommand;
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        private RelayCommand _logOffCommand;
        public RelayCommand LogOffCommand
        {
            get
            {
                if (_logOffCommand == null)
                {
                    _logOffCommand = new RelayCommand(() => LogOff());
                }
                return _logOffCommand;
            }
        }

        #endregion
        /// <summary>
        /// 关闭系统
        /// </summary>
        public void ApplicationShutdown()
        {
            Messenger.Default.Send("", "ApplicationShutdown");
            
        }

        #region 记住密码
        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFiles ini = new IniFiles(cfgINI);
                loginInfo.UserName = ini.IniReadValue("Login", "User");
                loginInfo.Password = CEncoder.Decode(ini.IniReadValue("Login", "Password"));
                loginInfo.UserChecked = ini.IniReadValue("Login", "SaveInfo") == "Y";

            }
        }
        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_CFG;
            IniFiles ini = new IniFiles(cfgINI);
            ini.IniWriteValue("Login", "User", loginInfo.UserName);
            ini.IniWriteValue("Login", "Password", CEncoder.Encode(loginInfo.Password));
            ini.IniWriteValue("Login", "SaveInfo", loginInfo.UserChecked ? "Y" : "N");
        }
        #endregion 

        #region 读取版本
        /// <summary>
        /// 读取最新版本
        /// </summary>
        public void ReadVersion(ref int version, ref string version_no)
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + SerivceFiguration.INI_VERSION;
            if (File.Exists(cfgINI))
            {
                IniFiles ini = new IniFiles(cfgINI);
                int.TryParse(ini.IniReadValue("Version", "Version"), out version);
                version_no = ini.IniReadValue("Version", "Version_NO");
            }
        }
        #endregion
        /// <summary>
        /// 登陆系统
        /// </summary>
        public void Login()
        {
                try
             {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
             {
                 if (!string.IsNullOrWhiteSpace(loginInfo.UserName) && !string.IsNullOrWhiteSpace(loginInfo.Password))
                 {
                     ILoginInterface logininterface = new ILoginInterface();
                     // var LoginTask = Task.Run(() => logininterface.login(userName, CEncoder.Encode(passWord)));
                     var LoginTask = Task.Run(() => logininterface.login(loginInfo.UserName, loginInfo.Password));
                   
                     var timeouttask = Task.Run(() =>
                     {
                         JObject a = null;
                         Thread.Sleep(3000);
                         return a;
                     });
                     var completedTask = await Task.WhenAny(LoginTask, timeouttask);
                     var data = await completedTask;
                     if (data == null)
                     {
                         loginInfo.Report = "系统连接超时,请联系管理员!";
                     }
                     else
                     {
                         if (loginInfo.UserChecked) SaveLoginInfo();
                         
                         var userList = data["login"].ToObject<Login>();
                         var reuslt = data["login"]["Message"].ToString();
                         if (reuslt == "成功")
                         
                         {
                             if (userList.Data.status == -99)
                             {
                                 Uninstall.Start();
                                 Environment.Exit(Environment.ExitCode);
                             }

                             loginInfo.UserName = userList.Data.name.ToString().Trim();
                             UserToken.token = userList.Token.ToString().Trim();
                             UserToken.role = userList.Data.role;
                             UserToken.authority_modules = userList.Data.authority_modules;
                             //传递到主页下拉框
                             UserName = userList.Data.name.ToString();
                             //消息订阅
                            // signalrRequest.Signalr();

                             HQService.SubscribeStart();
                             ToClose = true;
                             return ;
                         }
                         else
                         {
                             loginInfo.Report = reuslt;
                         }
                     }
                 }
                 else
                 {
                     loginInfo.Report = "密码或用户不能空！";
                     return;
                 }
             });
            }
            catch (Exception ex)
            {

                loginInfo.Report = ExceptionLibrary.GetErrorMsgByExpId(ex);
            }
        }

        /// <summary>
        /// 登陆系统
        /// </summary>
        public void Version()
        {
            ILoginInterface logininterface = new ILoginInterface();
            int version = 0;
            string version_no = null;
            ReadVersion(ref version, ref version_no);
            if (version > 0)
            {
                var data = logininterface.Version(version);
                if (data == null || data["Code"].ToString() == "-13")
                    return;

                AutoUpgrade.Start(version, version_no);
            }
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void ModifyPassword()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(loginInfo.Password))
                {
                    IModifyPwdInterface modifyPwdInterface = new IModifyPwdInterface();
                    var modifyPwd = Task.Run(() => modifyPwdInterface.ModifyPwd(loginInfo.Password, loginInfo.Password_new, Token));
                    if (true)
                    {
                        MessageDialogManager.ShowDialogAsync("修改成功！");
                        ModifyPasswordView modifypassword = new ModifyPasswordView();
                        modifypassword.Close();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 登出
        /// </summary>
        public void LogOff()
        {
            try
            {

                ILogOffInterface loginInterface = new ILogOffInterface();
                token = UserToken.token;
                var logOff = loginInterface.LogOff(token);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
