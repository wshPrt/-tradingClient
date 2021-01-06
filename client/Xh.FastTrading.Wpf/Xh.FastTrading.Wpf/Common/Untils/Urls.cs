using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Common.Untils
{
   public class Urls
    {
        /// <summary>
        /// 请求IP地址
        /// </summary>
        private static readonly string SERVER_URL = ConfigurationManager.ConnectionStrings["SERVER_URL"].ConnectionString;

        /// <summary>
        /// 登录
        /// </summary>
        public static string LOGIN_URL = SERVER_URL + "/Service/Utility.svc/Login";
        /// <summary>
        /// 获取最新版本
        /// </summary>
        public static string VERSION_URL = SERVER_URL + "/Service/Utility.svc/Version";
        /// <summary>
        /// 修改密码
        /// </summary>
        public static string MODIFY_PASSWORD_URL = SERVER_URL + "/Service/User.svc/UpdatePassword";
        /// <summary>
        /// 登出
        /// </summary>
        public static string LOG_OUT_URL = SERVER_URL + "/Service/User.svc/Logout";
        /// <summary>
        /// 中期策略单元列表
        /// </summary>
        public static string STRATEGY_USER_UNIT_LIST = SERVER_URL + "/Service/Unit.svc/List";
        /// <summary>
        /// 当前用户单元列表
        /// </summary>
        public static string CURRENT_USER_UNIT_LIST= SERVER_URL + "/Service/Unit.svc/List4User";
        /// <summary>
        /// 未指向用户的单元列表
        /// </summary>
        public static string NOT_POINT_USER_UNIT_LIST = SERVER_URL + "/Service/Unit.svc/List4Undirected";
        /// <summary>
        /// 指定用户的单元列表
        /// </summary>
        public static string APPOINT_USER_UNIT_LIST = SERVER_URL + "/Service/Unit.svc/List4User";
        /// <summary>
        /// 单元包含的主账户列表
        /// </summary>
        public static string MASTER_ACCOUNT_LIST = SERVER_URL + "/Service/Unit.svc/List4Filter";
        /// <summary>
        /// 单元资金
        /// </summary>
        public static string UNIT_FUNDS = SERVER_URL + "/Service/Query.svc/UnitCapital";
        /// <summary>
        /// 最大可买数量
        /// </summary>
        public static string MAX_BUY_NUMBER  = SERVER_URL + "/Service/Trade.svc/Count";
        /// <summary>
        /// 用户列表
        /// </summary>
        public static string USER_LIST = SERVER_URL + "/Service/User.svc/List";
        /// <summary>
        /// 新增用户
        /// </summary>
        public static string ADD_USER = SERVER_URL + "/Service/User.svc/Add";
        /// <summary>
        /// 修改用户
        /// </summary>
        public static string MODIFY_USER = SERVER_URL + "/Service/User.svc/Update";
        /// <summary>
        /// 权限设置
        /// </summary>
        public static string POWER_SETTINGS = SERVER_URL + "/Service/User.svc/Authority";
        /// <summary>
        /// 密码重置
        /// </summary>
        public static string RESET_PASSWORD = SERVER_URL + "/Service/User.svc/ResetPassword";
        /// <summary>
        /// 启停用
        /// </summary>
        public static string START_STOP = SERVER_URL + "/Service/User.svc/UpdateStatus";
        /// <summary>
        /// 限制交易
        /// </summary>
        public static string LIMIT_TRANSACTION = SERVER_URL + "/Service/User.svc/UpdateStatusOrder";
        /// <summary>
        /// 单元权限
        /// </summary>
        public static string UNIT_POWER = SERVER_URL + "/Service/User.svc/UpdateUnits";
        /// <summary>
        /// 删除用户
        /// </summary>
        public static string DEL_USER = SERVER_URL + "/Service/User.svc/Delete";
        /// <summary>
        /// 主账户列表
        /// </summary>
        public static string ACCOUNT_LIST = SERVER_URL + "/Service/Account.svc/List";
        /// <summary>
        /// 新增主账户
        /// </summary>
        public static string ADD_ACCOUNT = SERVER_URL + "/Service/Account.svc/Add";
        /// <summary>
        /// 修改主账户
        /// </summary>
        public static string MODIFY_ACCOUNT = SERVER_URL + "/Service/AccountGroup.svc/Update";
        /// <summary>
        /// 启停用-主账户列表
        /// </summary>
        public static string ACCOUNT_START_STOP = SERVER_URL + "/Service/Account.svc/UpdateStatus";
        /// <summary>
        /// 限制交易-主账户列表
        /// </summary>
        public static string ACCOUNT_LIMIT_TRANSACTION = SERVER_URL + "/Service/Account.svc/UpdateStatusOrder";
        /// <summary>
        /// 同步交易服务器IP
        /// </summary>
        public static string SYNCHRONOUS_TRANSACTION_SERVER_IP = SERVER_URL + "/Service/Account.svc/Synchronize";
        /// <summary>
        /// 删除主账户
        /// </summary>
        public static string DEL_ACCOUNT = SERVER_URL + "/Service/Account.svc/Delete";
        /// <summary>
        /// 账户池列表
        /// </summary>
        public static string ACCOUNT_POOL_LIST = SERVER_URL + "/Service/AccountGroup.svc/List";
        /// <summary>
        /// 新增主账户池
        /// </summary>
        public static string ADD_ACCOUNT_POOL = SERVER_URL + "/Service/AccountGroup.svc/Add";
        /// <summary>
        /// 修改主账户池
        /// </summary>
        public static string MODIFY_ACCOUNT_POOL = SERVER_URL + "/Service/AccountGroup.svc/Update";
        /// <summary>
        /// 账户和限额设置
        /// </summary>
        public static string ACCOUNT_LIMIT_SETTING = SERVER_URL + "/Service/AccountGroup.svc/UpdateItems";
        /// <summary>
        /// 删除主账户池
        /// </summary>
        public static string DEL_ACCOUNT_POOL = SERVER_URL + "/Service/AccountGroup.svc/Delete";
        /// <summary>
        /// 主账户过滤列表
        /// </summary>
        public static string ACCOUNT_FILTER_LIST = SERVER_URL + "/Service/Account.svc/List4Filter";
        /// <summary>
        /// 主账户持仓列表
        /// </summary>
        public static string ACCOUNT_POSITION_LIST = SERVER_URL + "/Service/System.svc/ListAccountPosition";
        /// <summary>
        /// 开盘收盘列表
        /// </summary>
        public static string OPEN_CLOSE_LIST = SERVER_URL + "/Service/System.svc/ListLogTrade";
        /// <summary>
        /// 开盘收盘暂停恢复
        /// </summary>
        public static string PAUSE_RESUME = SERVER_URL + "/Service/System.svc/UpdateStatusTrade";
        /// <summary>
        /// 单元过滤列表
        /// </summary>
        public static string UNIT_FILTER_LIST = SERVER_URL + "/Service/Unit.svc/List4Filter";
        /// <summary>
        /// 持仓划转
        /// </summary>
        public static string POSITION_TRANSFER = SERVER_URL + "/Service/System.svc/PositionTransfer";
        /// <summary>
        /// 单元列表
        /// </summary>
        public static string UNIT_LIST = SERVER_URL + "/Service/Unit.svc/List";
        /// <summary>
        /// 新增单元
        /// </summary>
        public static string ADD_UNIT = SERVER_URL + "/Service/Unit.svc/Add";
        /// <summary>
        /// 修改单元
        /// </summary>
        public static string MODIFY_UNIT = SERVER_URL + "/Service/Unit.svc/Update";
        /// <summary>
        /// 启停用-单元
        /// </summary>
        public static string START_STOP_UNIT = SERVER_URL + "/Service/Unit.svc/UpdateStatus";
        /// <summary>
        /// 设置冻结比例
        /// </summary>
        public static string SET_FREEZING_SCALE = SERVER_URL + "/Service/Unit.svc/UpdateRatioFreezing";
        /// <summary>
        /// 增加/减少资金/保证金
        /// </summary>
        public static string ADD_REDUCE_FUNDS_BOND = SERVER_URL + "/Service/Unit.svc/CapitalInOut";
        /// <summary>
        /// 删除单元
        /// </summary>
        public static string DEL_UNIT = SERVER_URL + "/Service/Unit.svc/Deletee";
        /// <summary>
        /// 委托汇总列表/异常委托/废单汇总
        /// </summary>
        public static string ENTRUST_SUMMARY_LIST = SERVER_URL + "/Service/System.svc/ListOrder";
        /// <summary>
        /// 系统内成交汇总列表
        /// </summary>
        public static string DEAL_SUMMARY_LIST = SERVER_URL + "/Service/System.svc/ListDeal";
        /// <summary>
        /// 系统外成交汇总列表
        /// </summary>
        public static string OUT_DEAL_SUMMARY_LIST = SERVER_URL + "/Service/System.svc/ListDeal";
        /// <summary>
        /// 主账户所在的单元列表
        /// </summary>
        public static string ACCOUNT_UNIT_LIST = SERVER_URL + "/Service/Unit.svc/List4Account";
        /// <summary> 
        /// 转入
        /// </summary>
        public static string ACCOUNT_INTO = SERVER_URL + "/Service/System.svc/Transfer";
        /// <summary>
        /// 持仓汇总
        /// </summary>
        public static string POSITION_SUMMARY = SERVER_URL + "/Service/System.svc/ListPosition";
        /// <summary>
        /// 资金流水列表
        /// </summary>
        public static string CAPITAL_FLOW_LIST = SERVER_URL + "/Service/Trade.svc/ListLogCapital";
        /// <summary>
        /// 委托记录列表
        /// </summary>
        public static string ENTRUST_RECORD_LIST = SERVER_URL + "/Service/Trade.svc/ListOrder";
        /// <summary>
        /// 成交记录列表
        /// </summary>
        public static string DEAL_RECORD_LIST = SERVER_URL + "/Service/Trade.svc/ListDeal";
        /// <summary>
        /// 持仓列表
        /// </summary>
        public static string POSITION_LIST = SERVER_URL + "/Service/Trade.svc/ListPosition";
        /// <summary>
        /// 查询资产(单元资金、单元持仓列表)
        /// </summary>
        public static string ASSETS_UNIT_CAPITAL = SERVER_URL + "/Service/Trade.svc/GetUnitCapital";
        /// <summary>
        /// 单元持仓列表
        /// </summary>
        public static string ASSETS_UNIT_POSITION = SERVER_URL + "/Service/Trade.svc/ListPosition";
        /// <summary>
        /// 买入/卖出 下单
        /// </summary>
        public static string BUY_SELL_ORDER = SERVER_URL + "/Service/Trade.svc/Order";
        /// <summary>
        /// 指定交易 下单
        /// </summary>
        public static string APPOINT_TRANSACTION_ORDER = SERVER_URL + "/Service/Trade.svc/OrderLimit";
        /// <summary>
        /// 指定交易 指定账户列表
        /// </summary>
        public static string APPOINT_ACCOUNT = SERVER_URL + "/Service/Account.svc/List4Unit";
        /// <summary>
        /// 自动交易  新增
        /// </summary>
        public static string Auto_transaction_ADD = SERVER_URL + "/Service/Trade.svc/OrderAutoAdd";
        /// <summary>
        /// 自动交易  修改 
        /// </summary>
        public static string Auto_transaction_Modify = SERVER_URL + "/Service/Trade.svc/OrderAutoUpdate";
        /// <summary>
        /// 自动交易 启用停用
        /// </summary>
        public static string AUTO_TRANSACTION_ENABLEDISABLE = SERVER_URL + "/Service/Trade.svc/OrderAutoUpdateStatus";
        /// <summary>
        /// 自动交易 删除
        /// </summary>
        public static string AUTO_TRANSACTION_DEL = SERVER_URL + "/Service/Trade.svc/OrderAutoDelete";
        /// <summary>
        ///自动交易 列表
        /// </summary>
        public static string AUTO_TRANSACTION_LIST = SERVER_URL + "/Service/Trade.svc/ListOrderAuto";
        /// <summary>
        // 撤单 可撤委托列表
        /// </summary>
        public static string CANCELLATIONS_REVOCABLE_DELEGATE_LIST= SERVER_URL + "/Service/Trade.svc/ListOrderCancellable";
        /// <summary>
        /// 批量撤单
        /// </summary>
        public static string BATCH_CANCELLATION = SERVER_URL + "/Service/Trade.svc/Cancel";
        /// <summary>
        /// 账户代码
        /// </summary>
        public static string ACCOUNT_CODE = SERVER_URL + "/Service/Trade.svc/ListPositionDetailed";
        /// <summary>
        /// 主账户池过滤列表
        /// </summary>
        public static string ACCOUNT_POOL_FILTER_LIST = SERVER_URL + "/Service/AccountGroup.svc/List4Filter";
        
    }

}
