using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class LoginModel: ValidateModelBase
    {
        /// <summary>
        /// 进度报告
        /// </summary>
        private string _Report;
        public string Report
        {
            get { return _Report; }
            set { _Report = value; RaisePropertyChanged(() => Report); }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        private bool _UserChecked;
        public bool UserChecked
        {
            get { return _UserChecked; }
            set { _UserChecked = value; RaisePropertyChanged(() => UserChecked); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string passWord;
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 新密码
        /// </summary>
        private string password_new;
        public string Password_new
        {
            get { return password_new; }
            set { password_new = value; RaisePropertyChanged(() => Password_new); }
        }

        /// <summary>
        /// Token
        /// </summary>
        public static string token;
        public static string Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            { userName = value; RaisePropertyChanged(() => UserName); }
        }

    }
}
