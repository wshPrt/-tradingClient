using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{
    public static class UserToken
    {
        /// <summary>
        /// Token
        /// </summary>
        public static string token { get; set; }

        /// <summary>
        /// 用户功能权限
        /// </summary>
        public static List<int> authority_modules { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public static int role { get; set; }
    }
}
