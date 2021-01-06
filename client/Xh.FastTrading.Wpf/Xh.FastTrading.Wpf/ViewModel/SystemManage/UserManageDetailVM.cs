using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Commands;
using Xh.FastTrading.Wpf.Common;
using Xh.FastTrading.Wpf.Common.Commands;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views;
using static Xh.FastTrading.Wpf.Model.UserInfoModel;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Xh.FastTrading.Wpf.ViewModel
{
    public class UserManageDetailVM: GalaSoft.MvvmLight.ViewModelBase
    { 
        public UserManageDetailVM()
        {
            UserInfo = new UserInfoModel();
            ValidateUI = new UserInfoModel();
            List = new ObservableCollection<UserInfoModel>();
            InitDataGrid();
        }

        private UserInfoModel userInfo;
        public UserInfoModel UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }
        /// <summary>
        /// 验证用户界面
        /// </summary>
        private UserInfoModel validateUI;
        public UserInfoModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        /// <summary>
        /// dataGrid集合
        /// </summary>
        private ObservableCollection<UserInfoModel> _list;
        public ObservableCollection<UserInfoModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }
        public List<UserInfoModel> ListAll = new List<UserInfoModel>();
        /// <summary>
        /// 选中行
        /// </summary>
        private UserInfoModel _selectedRow;
        public UserInfoModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }


        private UserInfoModel selectedItem;
        public UserInfoModel SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
        }

        /// <summary>
        /// 搜索指令
        /// </summary>
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(() => Search());
                }
                return searchCommand;
            }
            set { searchCommand = value; }
        }

        /// <summary>
        /// 选中文本数据
        /// </summary>
        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                    selectCommand = new RelayCommand(() => Select());
                return selectCommand;
            }
            set { selectCommand = value; }
        }

        /// <summary>
        /// 选中lstPower
        /// </summary>
        private void Select()
        {

        }
        #region 指令
        /// <summary>
        /// 关闭窗口
        /// </summary>
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

        /// <summary>
        /// 刷新
        /// </summary>
        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(() => Refresh());
                }
                return refreshCommand; }
            set { refreshCommand = value; }
        }

        /// <summary>
        /// 功能权限指令
        /// </summary>
        private RelayCommand functionPopupCommand;
        public RelayCommand FunctionPopupCommand
        {
            get
            {
                if (functionPopupCommand == null)
                {
                    functionPopupCommand = new RelayCommand(() => FunctionPopup(SelectedRow));
                }

                return functionPopupCommand; }
            set { functionPopupCommand = value; }
        }


        /// <summary>
        /// 新增确定
        /// </summary>
        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get 
            {
                if (confirmCommand == null)
                    confirmCommand = new RelayCommand(() => ExcuteConfirmCommand());
                    return confirmCommand;
            }
            set { confirmCommand = value; }
           
        }
        /// <summary>
        /// 单元权限弹窗
        /// </summary>
        private RelayCommand unitAuthorityPopupCommand;
        public RelayCommand UnitAuthorityPopupCommand
        {
            get {
                if (unitAuthorityPopupCommand == null)  
                {
                    unitAuthorityPopupCommand = new RelayCommand(() => unitAuthorityPopup(SelectedRow));
                }
                return unitAuthorityPopupCommand; }
            set { unitAuthorityPopupCommand = value; }
        }

        /// <summary>
        /// 弹窗重置密码指令
        /// </summary>
        private RelayCommand resetPasswordCommand;
        public RelayCommand ResetPasswordCommand
        {
            get
            {
                if (resetPasswordCommand == null)
                    resetPasswordCommand = new RelayCommand(() => ExcuteResetPasswordCommand());
                return resetPasswordCommand;
            }
            set { resetPasswordCommand = value; }
        }

        /// <summary>
        /// 修改弹窗
        /// </summary>
        private RelayCommand modifyPopupCommand;
        public RelayCommand ModifyPopupCommand 
        {
            get
            {
                if (modifyPopupCommand == null)
                {
                    modifyCommand = new RelayCommand(() => ModifyPopup(SelectedRow));
                } 
                return modifyPopupCommand; }
            set { modifyPopupCommand = value; }
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        private RelayCommand modifyCommand;
        public RelayCommand ExcModifyCommand
        {
            get
            {
                if (modifyCommand == null)
                    modifyCommand = new RelayCommand(() => ExcConfirmModifyCommand());
                return modifyCommand;
            }
            set { modifyCommand = value; }
        }

        /// <summary>
        /// 权限设置
        /// </summary>
        private RelayCommand powerSettingCommand;
        public RelayCommand PowerSettingCommand
        {
            get
            {
                if (powerSettingCommand == null)
                {
                    powerSettingCommand = new RelayCommand(() => PowerSetting());
                }
                return powerSettingCommand; 
            }
            set { powerSettingCommand = value; }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        private RelayCommand resetPwdCommand;
        public RelayCommand ResetPwdCommand
        {
            get 
            {
                if (resetPwdCommand == null)
                {
                    resetPwdCommand = new RelayCommand(() => ResetPwd());
                }
                return resetPwdCommand; 
            }
            set { resetPwdCommand = value; }
        }

        /// <summary>
        ///启停用 
        /// </summary>
        private RelayCommand startStopCommmand;
        public RelayCommand StartStopCommmand
        {
            get 
            {
                if (startStopCommmand == null)
                {
                    startStopCommmand = new RelayCommand(() => StartStop());
                }
                return startStopCommmand; 
            }
            set { startStopCommmand = value; }
        }

        /// <summary>
        /// 限制交易
        /// </summary>
        private RelayCommand limitCommand;
        public RelayCommand LimitCommand
        {
            get {
                if (limitCommand == null)
                {
                    limitCommand = new RelayCommand(() => Limit());
                }
                
                return limitCommand; 
            }
            set { limitCommand = value; }
        }

        /// <summary>
        /// 取消限制
        /// </summary>
        private RelayCommand cancelLimitCommand;
        public RelayCommand CancelLimitCommand
        {
            get {
                if (cancelLimitCommand == null)
                {
                    cancelLimitCommand = new RelayCommand(() => CancelLimit());
                }
                return cancelLimitCommand; }
            set { cancelLimitCommand = value;}
        }

        /// <summary>
        /// 单元权限
        /// </summary>
        private RelayCommand unitPowerCommand;
        public RelayCommand UnitPowerCommand
        {
            get 
            {
                if (unitPowerCommand == null)
                {
                    unitPowerCommand = new RelayCommand(() => UnitPower());
                }
                return unitPowerCommand; }
            set { unitPowerCommand = value; }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private RelayCommand delCommand;
        public RelayCommand DelCommand
        {
            get 
            {
                if (delCommand == null)
                {
                    delCommand = new RelayCommand(() => Del());
                }
                return delCommand; }
            set { delCommand = value; }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private RelayCommand excelCommand;
        public RelayCommand ExcelCommand
        {
            get
            {
                if (excelCommand == null)
                {
                    excelCommand = new RelayCommand(() => ExportExcel());
                }
                return excelCommand;
            }
            set { excelCommand = value; }
        }

        #endregion


        /// <summary>
        /// 新增用户弹窗
        /// </summary>
        /// <param name="parameter"></param>
        private void ExcuteConfirmCommand()
        {
            MessageDialogManager.ShowWidwonAddUserInfo();
        }

        /// <summary>
        /// 功能权限弹窗
        /// </summary>
        private void FunctionPopup(Model.UserInfoModel SelectedRow) 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else 
            {
                MessageDialogManager.ShowWindwonFunctionPower(SelectedRow);
            }

        }

        /// <summary>
        /// 单元权限
        /// </summary>
        private void unitAuthorityPopup( Model.UserInfoModel SelectedRow)
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else 
            {
                MessageDialogManager.UnitAuthorityPopupCommand(SelectedRow);
            }
        }

        /// <summary>
        /// 修改弹窗
        /// </summary>
        /// <param name="parameter"></param>
        private void ModifyPopup(Model.UserInfoModel SelectedRow)
        {
            //if(SelectedRow == null)
            //{
            //    MessageDialogManager.ShowDialogAsync("未选中记录");
            //    return;
            //}
            //else 
            //{
                MessageDialogManager.ShowWindonModifyUserInfo(UserInfo );
            //    return;
            //}      
        }

        /// <summary>
        /// 重置密码弹窗
        /// </summary>
        private void ExcuteResetPasswordCommand() 
        {
            if (SelectedRow ==null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else
            {
                MessageDialogManager.ShowUserManageResetPassword();
            }
        }

        /// <summary>
        /// 权限设置弹窗
        /// </summary>
        /// <param name="parameter"></param>
        private void FunctionPower(object parameter) 
        {
            if (SelectedRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中记录");
                return;
            }
            else 
            {

                MessageDialogManager.ShowWindwonFunctionPower(SelectedRow);
                return;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh() 
        {
            List.Clear();
            InitDataGrid();

        }

        /// <summary>
        ///  用户列表加载
        /// </summary>
        public void InitDataGrid()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IUserlistInterface userList = new IUserlistInterface();
                var userListData = userList.UserList(token);
                string success = userListData["Message"]["Message"].ToString();
                string json = userListData["Message"].ToString();
                if (success == "成功")
                {
                    UserInfoModel.RootData data = JsonConvert.DeserializeObject<UserInfoModel.RootData>(json);
                    for (int i = 0; i < data.Data.Count; i++)
                    {

                        var m = new UserInfoModel()
                        {
                            Id = data.Data[i].id,
                            LoginName = data.Data[i].code.ToString(),
                            Name = data.Data[i].name.ToString(),
                            PhoneNumber = data.Data[i].mobile,
                            DynPasswordNumber = data.Data[i].id,
                            FunctionPower = data.Data[i].authority_platforms,
                            Power = data.Data[i].authority_modules,
                            UnitNumber = data.Data[i].unit_count,
                            Limit = data.Data[i].status_order,
                            Status = data.Data[i].status
                        };
                        ListAll.Add(m);
                        List.Add(m);
                    }
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync("加载用户信息失败！");
                }
            });
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        private void ExcConfirmModifyCommand()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
            if (!string.IsNullOrWhiteSpace(modifyLoginName) && !string.IsNullOrWhiteSpace(modifyName))
            {

                IModifyUserInterface modifyUser = new IModifyUserInterface();
                    Token = UserToken.token;
                    var confirmModify = await Task.Run(() => modifyUser.ModifyUser(modifyId, modifyLoginName, modifyName, modifyPhoneNumber, Token));
                    string success  = confirmModify["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        List.Clear();
                        InitDataGrid();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("用户信息修改成功!");                        
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                        this.Cleanup();
                    }
                }
            });
        }

        /// <summary>
        /// 权限设置
        /// </summary>
        private void PowerSetting()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            { 
                IPowerSettingUserInterface powerSetting = new IPowerSettingUserInterface();
                token = UserToken.token;
                var result = await Task.Run(() => powerSetting.SettingPower(SelectedRow.Id, SelectedRow.FunctionPower, SelectedRow.Power, token));
                string success = result["Message"]["Message"].ToString();
                if (success == " 成功"){ Refresh();MessageDialogManager.ShowDialogAsync("选中用户权限设置成功!");}
                else { MessageDialogManager.ShowDialogAsync(success);}
            });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        private void ResetPwd() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                IResetPwdInterface resetPwd = new IResetPwdInterface();
                token = UserToken.token;
                var result = await Task.Run(() => resetPwd.ResetPwd(SelectedRow.Id, userInfo.Password.ToString().Trim(),Token));
                string success = result["Message"]["Message"].ToString();
                ToClose = true;
                if (success == "成功")
                {
                    List.Clear();
                    InitDataGrid();
                    ToClose = true;
                    MessageDialogManager.ShowDialogAsync("选中用户密码重置成功!"); 
                }
                else { MessageDialogManager.ShowDialogAsync(success); }
            });
        }

        /// <summary>
        /// 启停用
        /// </summary>
        private void StartStop() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (SelectedRow == null) 
                {
                    MessageDialogManager.ShowDialogAsync("未选中!"); 
                    return;
                }
                else
                {
                    if (SelectedRow.Status == 1)
                    {
                        int Status = 0;
                        IStartStopInterface startStop = new IStartStopInterface();
                        token = UserToken.token;
                        var result = await Task.Run(() => startStop.StartStop(SelectedRow.Id, Status, Token));
                        string success = result["Message"]["Message"].ToString();
                        if (success == "成功")
                        {
                            MessageDialogManager.ShowSatrtStopDialogAsync("账号已停用!");
                            Refresh();
                        }
                        return;
                    }
                    else
                    {
                        if (SelectedRow.Status == 0)
                        {
                            int Status = 1;
                               IStartStopInterface startStop = new IStartStopInterface();
                            token = UserToken.token;
                            var result = await Task.Run(() => startStop.StartStop(SelectedRow.Id, Status, Token));
                            string success = result["Message"]["Message"].ToString();
                            if (success == "成功")
                            {
                                MessageDialogManager.ShowSatrtStopDialogAsync("账号已启用!");
                                Refresh();
                            }
                            return;
                        }
                      
                    }
                  
                }
             
            });
        }
       
        /// <summary>
        /// 限制交易
        /// </summary>
        private void Limit() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (SelectedRow == null)
                {
                    MessageDialogManager.ShowDialogAsync("未选中!");
                    return;
                }
                token = UserToken.token;
                ILimitInterface limit = new ILimitInterface();
                int status = 0;
                var result = await Task.Run(() => limit.ILimit(SelectedRow.Id, status, Token));
                string success = result["Message"]["Message"].ToString();
                if (success =="成功")
                {
                    List.Clear();
                    InitDataGrid();
                    MessageDialogManager.ShowDialogAsync("选中账号已限制交易成功!");
                }
                else 
                {
                    MessageDialogManager.ShowDialogAsync(success.ToString());
                }
            });
        }

        /// <summary>
        /// 取消限制
        /// </summary>
        private void CancelLimit() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (SelectedRow == null)
                {
                    MessageDialogManager.ShowDialogAsync("未选中!");
                    return;
                }
                else
                {
                    token = UserToken.token;
                    ICancelTradeInterface CancelTrade = new ICancelTradeInterface();
                    int status = 1;
                    var result = CancelTrade.CancelTrade(SelectedRow.Id, status, Token);
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        List.Clear();
                        InitDataGrid();
                        MessageDialogManager.ShowDialogAsync("选中账号已取消限制!");
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(success.ToString());
                    }
                }
            });
        }

        /// <summary>
        /// 单元权限
        /// </summary>
        private void UnitPower() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                token = UserToken.token;
                List<int> unit_ids = null;
                IUnitPowerInterface unitPower = new IUnitPowerInterface();
                var result = await Task.Run(() => unitPower.UnitPower(SelectedRow.Id, unit_ids,token));
                string success = result["Message"]["Message"].ToString();
                if (success == "成功")
                {
                    List.Clear();
                    InitDataGrid();
                    MessageDialogManager.ShowDialogAsync("单元权限设置成功!");
                }
                else 
                {
                    MessageDialogManager.ShowDialogAsync(success.ToString());
                }
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Del() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (SelectedRow == null)
                {
                    MessageDialogManager.ShowDialogAsync("未选中!");
                    return;
                }
                token = UserToken.token;
                IDelUserInterface del = new IDelUserInterface();
                var result = await Task.Run(()=> del.Del(SelectedRow.Id, SelectedRow.LoginName, Token));
                string success = result["Message"]["Message"].ToString();
                if (success == "成功")
                {
                    List.Clear();
                    InitDataGrid();
                    MessageDialogManager.ShowDialogAsync("选中账号删除成功!");
                }
                else 
                {
                    MessageDialogManager.ShowDialogAsync(success.ToString());
                }
            });
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel()
        { 
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (List == null || List.Count == 0)
                {
                    MessageDialogManager.ShowDialogAsync("没记录无法导出!");
                    return;
                }
                if (List.Count > 0)
                {
                    //Excel表格的创建步骤
                    //第一步：创建Excel对象
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    //第二步：创建Excel对象的工作簿
                    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();
                    //第三步：Excel表头设置
                    //给sheet添加第一行的头部标题
                    NPOI.SS.UserModel.IRow irow = sheet.CreateRow(0);//创建行
                    irow.CreateCell(0).SetCellValue("登录名");
                    irow.CreateCell(1).SetCellValue("姓名");
                    irow.CreateCell(2).SetCellValue("手机");
                    irow.CreateCell(3).SetCellValue("动态密码编码");
                    irow.CreateCell(4).SetCellValue("登录权限");
                    irow.CreateCell(5).SetCellValue("功能权限");
                    irow.CreateCell(6).SetCellValue("单元数量");
                    irow.CreateCell(7).SetCellValue("限制交易");
                    irow.CreateCell(8).SetCellValue("状态");


                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].LoginName);
                        row.CreateCell(1).SetCellValue(List[i].Name);
                        row.CreateCell(2).SetCellValue(List[i].PhoneNumber);
                        row.CreateCell(3).SetCellValue(List[i].DynPasswordNumber);
                        row.CreateCell(4).SetCellValue(List[i].FunctionStr);
                        row.CreateCell(5).SetCellValue(List[i].PowerStr);
                        row.CreateCell(6).SetCellValue(List[i].UnitNumber);
                        row.CreateCell(7).SetCellValue(List[i].Limit);
                        row.CreateCell(8).SetCellValue(List[i].Status);
                    }
                    //把Excel转化为文件流，输出
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "选择要保存的路径";
                    saveFileDialog.Filter = "Excel文件|*.xls|所有文件|*.*";
                    saveFileDialog.FileName = string.Empty;
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.DefaultExt = "xls";
                    saveFileDialog.CreatePrompt = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileStream BookStream = new FileStream(saveFileDialog.FileName.ToString(), FileMode.Create, FileAccess.Write);//定义文件流
                        book.Write(BookStream);//将工作薄写入文件流                  
                        BookStream.Seek(0, SeekOrigin.Begin); //输出之前调用Seek（偏移量，游标位置）方法：获取文件流的长度
                        BookStream.Close();
                        MessageDialogManager.ShowDialogAsync("中期策略单元管理导出成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("导出保存失败！");
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void Search()
        {
            List.Clear();
            ListAll.Where(i => i.Name.Contains(ValidateUI.Name)).ToList().ForEach(i =>
            {
                List.Add(i);
            });

        }

            public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
    }
}
