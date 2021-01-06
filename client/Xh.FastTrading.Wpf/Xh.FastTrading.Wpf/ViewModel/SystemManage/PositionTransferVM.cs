using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
   public class PositionTransferVM:ViewModelBase
    {
        public PositionTransferVM() 
        {
            PositionTransfer = new PositionTransferModel();
            List = new ObservableCollection<PositionTransferModel>();
            List.Add(PositionTransfer);
            UnitNameList();
            ValidateUI = new Model.PositionTransferModel();
        }

        #region DataGrid
        private PositionTransferModel positionTransfer;
        public PositionTransferModel PositionTransfer
        {
            get { return positionTransfer; }
            set { positionTransfer = value; RaisePropertyChanged(() => PositionTransfer); }
        }

        /// <summary>
        /// DataGrid集合
        /// </summary>
        private ObservableCollection<PositionTransferModel> _list;
        public ObservableCollection<PositionTransferModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }


        #endregion
        #region 指令
        /// <summary>
        ///用户界面验证 
        /// </summary>
        private PositionTransferModel validateUI;
        public PositionTransferModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }

        /// <summary>
        /// 执行(新增)
        /// </summary>
        private RelayCommand addExecuteCommand;
        public RelayCommand AddExecuteCommand
        {
            get 
            {
                if (addExecuteCommand == null)
                {
                    addExecuteCommand = new RelayCommand(() => AddExecute());
                }
                return addExecuteCommand; }
            set { addExecuteCommand = value; }
        }

        /// <summary>
        /// 单元名称
        /// </summary>
        private RelayCommand unitNameCommand;
        public RelayCommand UnitNameCommand
        {
            get 
            {
                if (unitNameCommand == null)
                {
                    unitNameCommand = new RelayCommand(() => UnitNameList());
                }
                return unitNameCommand; }
            set { unitNameCommand = value;}
        }
        #endregion

        /// <summary>
        /// 单元过滤列表
        /// </summary>
        private void UnitNameList()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IUnitNameFilterInterface unitNameFilter = new IUnitNameFilterInterface();
                var result = await Task.Run(() => unitNameFilter.UnitNameFilterList(token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MasterAccountPositionModel.Root accountPosition = JsonConvert.DeserializeObject<MasterAccountPositionModel.Root>(jsonData);
                    List = new ObservableCollection<PositionTransferModel>();
                    for (int i = 0; i < accountPosition.Data.Count; i++)
                    {
                        List.Add(new PositionTransferModel()
                        {
                            SourceUnitName = accountPosition.Data[i].name,
                            DestinationUnitName = accountPosition.Data[i].code,
                            Id = accountPosition.Data[i].id
                        });
                    }
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });

        }


        /// <summary>
        /// 执行
        /// </summary>
        private void AddExecute() 
        {
            if (string.IsNullOrEmpty(ValidateUI.SourceUnitName))
            {
                MessageDialogManager.ShowDialogAsync("源单元名称为空!");
                return;
            }
            if (string.IsNullOrEmpty(ValidateUI.DestinationUnitName))
            {
                MessageDialogManager.ShowDialogAsync("目的单元为空!");
                return;
            }
            if (string.IsNullOrEmpty(ValidateUI.SecuritiesCode.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("证券代码为空!");
                return;
            }
            if (string.IsNullOrEmpty(ValidateUI.AmountMoney.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("金额为空!");
                return;
            }
            if (string.IsNullOrEmpty(ValidateUI.Id.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("Id为空!");
                return;
            }
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (ValidateUI.IsValidated)
                {
                    string token = UserToken.token;
                    IPositionTransferInterface positionTransfer = new IPositionTransferInterface();
                    var result = await Task.Run(() => positionTransfer.PositionTransfer(
                    ValidateUI.SourceUnitName, ValidateUI.DestinationUnitName,
                    ValidateUI.SecuritiesCode, ValidateUI.AmountMoney, 
                    ValidateUI.Id, token)) ;
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        MessageDialogManager.ShowDialogAsync("持仓划转新增成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });

        }

    }
}
