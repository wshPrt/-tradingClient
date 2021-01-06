using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Views;
using Xh.FastTrading.Wpf.Views.Deal.Automatic;
using Xh.FastTrading.Wpf.Views.SystemManage;
using Xh.FastTrading.Wpf.Views.UnitManage;

namespace Xh.FastTrading.Wpf.Untils
{
    public class MessageDialogManager: BaseWindow
    {
        
        /// <summary>
        /// 自动交易修改
        /// </summary>
       public static void ShowModifyAutomaticView(Model.DealAutoMaticModel DealAutoMatic)
        {
            Model.DealAutoMaticModel tmpmodule = DealAutoMatic.Clone() as Model.DealAutoMaticModel;
            ModifyEntrustInfoView modifyInfo = new ModifyEntrustInfoView(tmpmodule);
            modifyInfo.ShowDialog();
        }
        /// <summary>
        /// 自动交易新增
        /// </summary>
        public static void ShowAutomaticAdd() 
        {
            EntrustInfoView entrustInfo = new EntrustInfoView();
            entrustInfo.ShowDialog();
        }
        /// <summary>
        /// 转入单元提示
        /// </summary>
        public static void ShowTipsTrategyUnitView()
        {
            TipsTrategyUnitView tipsTrategy = new TipsTrategyUnitView();
            tipsTrategy.ShowDialog();
        }

        /// <summary>
        /// 资产冻结比例
        /// </summary>
        public static void ShowFreezingRatioTrategyUnitView()
        {
            FreezingRatioTrategyUnitView freezingRatio = new FreezingRatioTrategyUnitView();
            freezingRatio.ShowDialog();
        }

        /// <summary>
        /// 转出资金 
        /// </summary>
        public static void ShowTurnOutCapitalView()
        {
            TurnOutCapitalView turnOutCapital = new TurnOutCapitalView();
            turnOutCapital.ShowDialog();
        }

        /// <summary>
        /// 转入资金
        /// </summary>
        public static void ShowTurnIntoCapitalView()
        {
            TurnIntoCapitalView turnIntoCapital = new TurnIntoCapitalView();
            turnIntoCapital.ShowDialog();
        }

        /// <summary>
        /// 复制单元
        /// </summary>
        public static void ShowCopyTrategyUnitView()
        {
            CopyTrategyUnitView copyTrategy = new CopyTrategyUnitView();
            copyTrategy.ShowDialog();
        }

        /// <summary>
        /// 减少保证金
        ///// </summary>
        public static void ShowReduceBondTrategyUnitView()
        {
            ReduceBondTrategyUnitView reduceBond = new ReduceBondTrategyUnitView();
            reduceBond.ShowDialog();
        }
        /// <summary>
        /// 添加保证
        /// </summary>
        public static void ShowAddMarginView()
        {
            AddBondTrategyUnitView addBondTrag = new AddBondTrategyUnitView();
            addBondTrag.ShowDialog();
        }

        /// <summary>
        /// 修改中期策略单元
        /// </summary>
        public static void ShowModifyTrategyUnitView(Model.MidStrategyUnitManageModel MidStrategyUnit )
        {
            Model.MidStrategyUnitManageModel tmpmodule = MidStrategyUnit.Clone() as Model.MidStrategyUnitManageModel;
            ModifyTrategyUnitView modigyTrategyUnit = new ModifyTrategyUnitView(tmpmodule);
            modigyTrategyUnit.ShowDialog();
        }
        /// <summary>
        /// 新增中期策略单元
        /// </summary>
        public static void ShowAddTrategyUnitView()
        {

            AddTrategyUnitView AddTrategyUnitDialog = new AddTrategyUnitView();
            AddTrategyUnitDialog.ShowDialog();
        }
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="owner">父级窗体</param>
        //public static void ShowDialog(Window owner)
        //{
        //    //蒙板
        //    Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };
        //    //父级窗体原来的内容
        //    UIElement original = owner.Content as UIElement;
        //    owner.Content = null;
        //    //容器Grid
        //    Grid container = new Grid();
        //    container.Children.Add(original);//放入原来的内容
        //    container.Children.Add(layer);//在上面放一层蒙板
        //                                  //将装有原来内容和蒙板的容器赋给父级窗体
        //    owner.Content = container;

        //    //弹出消息框
        //     AddTrategyUnitView box = new AddTrategyUnitView(){ Owner = owner };           
        //    box.ShowDialog();
        //}
        /// <summary>
        /// 限额
        /// </summary>
        public static void ShowAccountLimitView()
        {
            AccountLimitView AccountLimitViewDialog = new AccountLimitView();
            AccountLimitViewDialog.ShowDialog();
        }

        /// <summary>
        /// 账号和限额设置
        /// </summary>
        public static void ShowAccountLimitSettingView(Model.AccountPoolModel accountPool)
        {
            AccountLimitSettingView AccountLimitSettingDialog = new AccountLimitSettingView(accountPool);
            AccountLimitSettingDialog.ShowDialog();
        }
        /// <summary>
        /// 修改主账户池
        /// </summary>
        public static void ShowAccountModifyInfoView()
        {
            AccountModifyInfoView AccountAddInfoDialog = new AccountModifyInfoView();
            AccountAddInfoDialog.ShowDialog();
        }
        /// <summary>
        /// 新增主账户池
        /// </summary>
        public static void ShowAccountAddInfoView()
        {
            AccountAddInfoView AccountAddInfoDialog = new AccountAddInfoView();
            AccountAddInfoDialog.ShowDialog();
        }
        /// <summary>
        /// 新增主账户信息
        /// </summary>
        public static void ShowAddAccountInfo()
        {
            AccountInfoView AddShowAccountInfoDialog = new AccountInfoView();
            AddShowAccountInfoDialog.ShowDialog();
        }
        /// <summary>
        /// 修改主账户信息
        /// </summary>
        public static void ShowAccountInfo()
        {
            AccountInfoModifyView AddShowAccountInfoDialog = new AccountInfoModifyView();
            AddShowAccountInfoDialog.ShowDialog();
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        public static void ShowWidwonAddUserInfo()
        {
            UserManageUserInfoView AddUserInfoDialog = new UserManageUserInfoView();
            AddUserInfoDialog.ShowDialog();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        public static void ShowWindonModifyUserInfo(Model.UserInfoModel UserInfoModel)
        {
            Model.UserInfoModel tmpmodule = UserInfoModel.Clone() as Model.UserInfoModel;
            UserManageModifyUserInfoView modifyUserInfoDialog = new UserManageModifyUserInfoView(tmpmodule);
            modifyUserInfoDialog.ShowDialog();
        }
        /// <summary>
        /// 单元权限
        /// </summary>
        public static void UnitAuthorityPopupCommand(Model.UserInfoModel UserInfoModel)
        {
            Model.UserInfoModel tmpmodule = UserInfoModel.Clone() as Model.UserInfoModel;
            UserManageUnitPowerView userManageUnitPowerView = new UserManageUnitPowerView(tmpmodule);
            //userManageUnitPowerView.Window_Loaded();
            //userManageUnitPowerView.Right_Loaded();
            userManageUnitPowerView.ShowDialog();
        }
        /// <summary>
        /// 功能权限
        /// </summary>
        public static void ShowWindwonFunctionPower(Model.UserInfoModel UserInfoModel)
        {
            Model.UserInfoModel tmpmodule = UserInfoModel.Clone() as Model.UserInfoModel;
            UserManageUnitFunctionPowerView functionPower = new UserManageUnitFunctionPowerView(tmpmodule);
            functionPower.ShowDialog();
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        public static void ShowUserManageResetPassword()
        {
            UserManageResetPasswordView resetPassord = new UserManageResetPasswordView();
            resetPassord.ShowDialog();
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public static void ShowWidwonAsync()
        {
            UserManageUserInfoView UserManageUserDialog = new UserManageUserInfoView();
            UserManageUserDialog.Show();
        }
        /// <summary>
        /// 主页
        /// </summary>
        public static void ShowMainWindow()
        {
            MainWindow mian = new MainWindow();
            mian.Show();
        }
       
        public static void CloseLogin() 
        {
            SignInView signIn = new SignInView();
            signIn.Close();
        }
        /// <summary>
        /// 错误提示框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isModeDialog"></param>
        public static void ShowDialogAsync(string msg, bool isModeDialog = false)
        {

            MsgBoxView customMessageDialog = new MsgBoxView()
            {
                msg = { Text = msg },
            };


            if (isModeDialog)
            {
                customMessageDialog.ShowDialog();
            }
            else
            {
                customMessageDialog.Show();
            }
        }

        /// <summary>
        /// 启停用消息窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isModeDialog"></param>
        public static void ShowSatrtStopDialogAsync(string msg, bool isModeDialog = false)
        {
            StartStopMsgBoxView customMessageDialog = new StartStopMsgBoxView()
            {
                msg = { Text = msg },
            };


            if (isModeDialog)
            {
                customMessageDialog.ShowDialog();
            }
            else
            {
                customMessageDialog.Show();
            }
        }
    }
}
