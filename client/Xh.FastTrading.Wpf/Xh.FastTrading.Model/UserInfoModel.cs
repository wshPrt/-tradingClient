using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Runtime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public class UserInfoModel : ValidateModelBase, ICloneable
    {
        public static int modifyId { get; set; }
        public static string modifyName { get; set; }
        public static string modifyLoginName { get; set; }
        public static int modifyPhoneNumber { get; set; }

        public static UserInfoModel UserInfoView;
        public class userClass
        {
            public int account_id { get; set; }
            public int sort { get; set; }
            // public List<int> Power { get; set; }
        }
        public class itemUser
        {
            public List<userClass> items_lstPower { get; set; }
        }

        //功能权限
        public class tModules
        {
            public int trade { get; set; }
            public int seniorTrade { get; set; }
            public int appoint { get; set; } 
            public int look  { get; set; }
            public int unit { get; set; }
            public int system { get; set; }
        }
        public class itemPower 
        {
            public List<tModules> items_power { get; set; }
        }

        //平台
        public class tplatforms
        {
            public int PC { get; set; }
            public int App { get; set; }
        }
        public class itemplatforms
        {
            public List<tplatforms> items_platforms { get; set; }
        }
        /// <summary>
        /// 登录、功能权限
        /// </summary>
        private List<item> items;
        public List<item> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChanged(() => items); }
        }
        /// <summary>
        /// ID
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => Id); }
        }

        /// <summary>
        /// 登录名
        /// </summary>
        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; RaisePropertyChanged(() => LoginName); }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(() => Name); }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; RaisePropertyChanged(() => PhoneNumber); }
        }
        /// <summary>
        /// 动态密码编号
        /// </summary>
        private int dynPasswordNumber;
        public int DynPasswordNumber
        {
            get { return dynPasswordNumber; }
            set { dynPasswordNumber = value; RaisePropertyChanged(() => DynPasswordNumber); }
        }
        /// <summary>
        /// 登录权限
        /// </summary>
        private List<int> power;
        public List<int> Power
        {
            get { return power; }
            set { power = value; PowerStr = GetPowerString(value); RaisePropertyChanged(() => Power); }
        }
        private static string GetPowerString(List<int> powerList)
        {
            var str = "";
            var count = 0;
            foreach (var item in powerList)
            {
                count++;
                if (count > 1) str += ",";
                if (item == 100)
                {
                    str += "交易";
                    continue;
                }
                if (item == 200)
                {
                    str += "自动交易";
                    continue;
                }
                if (item == 300)
                {
                    str += "指定交易";
                    continue;
                }
                if (item == 400)
                {
                    str += "查询";
                    continue;
                }
                if (item == 500)
                {
                    str += "单元管理";
                    continue;
                }
                if (item == 600)
                {
                    str += "风控管理";
                    continue;
                }
                if (item == 700)
                {
                    str += "系统管理";
                    continue;
                }
            }
            return str;
        }

        public string PowerStr { get; set; }

        public string FunctionStr  { get; set;}
        /// <summary>
        /// 功能权限
        /// </summary>
        private List<int> functionPower;
        public List<int> FunctionPower
        {
            get { return functionPower; }
            set { functionPower = value; FunctionStr = GetFunctionPower(value); RaisePropertyChanged(() => FunctionPower); }
        }
        private static string GetFunctionPower(List<int> functionList)
        {
            var str = "";
            var count = 0;
            foreach (var item in functionList)
            {
                count++;
                if (count > 1) str += ",";
                if (item == 1)
                {
                    str += "PC端";
                    continue;
                }
                if (item == 2)
                {
                    str += "APP端";
                    continue;
                }
            }
            return str;
        }

        /// <summary>
        /// 单元数量
        /// </summary>
        private int unitNumber ;
        public int UnitNumber
        {
            get { return unitNumber; }
            set { unitNumber = value; RaisePropertyChanged(() => UnitNumber); }
        }

        /// <summary>
        /// 限制交易
        /// </summary>
        private int limit;
        public int Limit
        {
            get { return limit; }
            set { limit = value; LimitStr = GetLimit(value); RaisePropertyChanged(() => Limit); }
        }
        public string LimitStr { get; set; }
        private static string GetLimit(int? limitType)
        {
            var str = "";
            if (limitType == 1)
            {
                str += "无限制";
            }
            if (limitType == 0)
            {
                str += "禁止交易";
            }
            if (limitType == -1)
            {
                str += "禁止买入";
            }
            return str;
        }

        /// <summary>
        /// 状态
        /// </summary>
        private int status;
        public int Status
        {
            get { return status ; }
            set { status = value; StatusStr = GetStatus(value); RaisePropertyChanged(() => Status); }
        }
       
        public string StatusStr { get; set; }
        private static string GetStatus(int? statusType)
        {
            var str = "";
            if (statusType == 0)
            {
                str += "停用";

            }
            if (statusType == 1)
            {
                str += "启用";

            }
            return str;
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string passWord;
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(() => Password); }
        }
         
        /// <summary>
        /// 真实姓名
        /// </summary>
        private string realName;
        public string RealName
        {
            get { return realName; }
            set { realName = value; RaisePropertyChanged(() => RealName); }
        }

        /// <summary>
        /// 合约号
        /// </summary>
        private int contractNumber;
        public int ContractNumber
        {
            get { return contractNumber; }
            set { contractNumber = value;RaisePropertyChanged(() => ContractNumber);}
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

        public class Data
        {
            /// <summary>
            /// 登录名
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 操作权限
            /// </summary>
            public List<int> authority_modules { get; set; }
            /// <summary>
            /// 登录权限
            /// </summary>
            public List<int> authority_platforms { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string password { get; set; }
            /// <summary>
            /// 角色，99管理员，0普通用户
            /// </summary>
            public int role { get; set; }
            /// <summary>
            /// 账号状态，1正常，0停用，-99强制下线
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 交易状态，1正常，0禁止交易，-1禁止买入
            /// </summary>
            public int status_order { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int unit_count { get; set; }
        }

        public class RootData 
        {
            /// <summary>
            /// 登录名
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Data> Data { get; set; }
        }


        public class Login
        {
            /// <summary>
            /// 
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 成功
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Data Data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Token { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public Login login { get; set; }
        }

        public class item 
        {
            /// <summary>
            /// 登录名
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 操作权限
            /// </summary>
            public List<int> authority_modules { get; set; }
            /// <summary>
            /// 登录权限
            /// </summary>
            public List<int> authority_platforms { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string password { get; set; }
            /// <summary>
            /// 角色，99管理员，0普通用户
            /// </summary>
            public int role { get; set; }
            /// <summary>
            /// 账号状态，1正常，0停用
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 交易状态，1正常，0禁止交易，-1禁止买入
            /// </summary>
            public int status_order { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int unit_count { get; set; }

        }

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
    }
}
