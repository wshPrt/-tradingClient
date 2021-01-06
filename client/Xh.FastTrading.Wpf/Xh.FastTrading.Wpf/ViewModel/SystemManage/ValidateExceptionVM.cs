using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Views.SystemManage;
using Xh.FastTrading.Wpf.Untils;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Xh.FastTrading.Wpf.ViewModel
{
    
    public class ValidateExceptionVM:ViewModelBase
    {
        public ValidateExceptionVM() 
        {
            ValidateUI = new Model.ValidateUserInfo();
        }

        #region 全局属性
        private ValidateUserInfo validateUI;
         /// <summary>
         /// 用户信息
         /// </summary>
         public ValidateUserInfo ValidateUI
        {
             get
             {
                 return validateUI;
             }
 
             set
             {
                validateUI = value;
                 RaisePropertyChanged(()=> ValidateUI);
             }
         }
        /// <summary>
        /// Message 成功
        /// </summary>
        public string success { get; set; }
        #endregion

        #region 全局指令
        /// <summary>
        /// 新增
        /// </summary>
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get 
            {
                if (addCommand == null)
                    return new RelayCommand(() => ExcuteValid());
                return AddCommand;
            }
            set { addCommand = value; }
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
        #endregion

        private ObservableCollection<UserInfoModel> _list;
        public ObservableCollection<UserInfoModel> List
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        /// 验证及新增用户
        /// </summary>
        private void ExcuteValid()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    IAddUserlistInterface adduserlist = new IAddUserlistInterface();
                    var jsonData = await Task.Run(() => adduserlist.AddUser(validateUI.UserName, validateUI.Name, validateUI.Phone, validateUI.Password, UserToken.token));
                    success = jsonData["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        List = null;
                        List = new ObservableCollection<UserInfoModel>();
                        
                        InitDataGrid();
                         ToClose = true;
                        MessageDialogManager.ShowDialogAsync("新增成功!");
                    }
                    else { MessageDialogManager.ShowDialogAsync(success); }
                }
            });
        }

        public void InitDataGrid()
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

                    List.Add(new UserInfoModel()
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
                    });
                }
            }
            else
            {
                MessageDialogManager.ShowDialogAsync("加载用户信息失败！");
            }
        }
    }
}
