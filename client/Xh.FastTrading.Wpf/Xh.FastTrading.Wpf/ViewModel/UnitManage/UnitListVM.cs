using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UnitManage;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views.UnitManage;
using System.Windows.Forms;
using System.IO;

namespace Xh.FastTrading.Wpf.ViewModel.UnitManage
{
   public class UnitListVM:ViewModelBase, ICloneable
    {
 
        public UnitListVM()
        {
            AccountPool();
            InitDataGrid();
            ValidateUI = new MidStrategyUnitManageModel();
            List = new BindingList<MidStrategyUnitManageModel>();
            
        }
        #region DataGrid
        private MidStrategyUnitManageModel midStrategyUnitManage;
        public MidStrategyUnitManageModel MidStrategyUnitManage
        {
            get { return midStrategyUnitManage; }
            set { midStrategyUnitManage = value; RaisePropertyChanged(() => MidStrategyUnitManage); }
        }

        /// <summary>
        /// DataGrid集合
        /// </summary>
        private BindingList<MidStrategyUnitManageModel> _list;
        public BindingList<MidStrategyUnitManageModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        public List<MidStrategyUnitManageModel> ListAll = new List<MidStrategyUnitManageModel>();

        /// <summary>
        /// 选中行
        /// </summary>
        private MidStrategyUnitManageModel _selectedRow;
        public MidStrategyUnitManageModel SelecteRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelecteRow); }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private MidStrategyUnitManageModel validateUI;
        public MidStrategyUnitManageModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }


        #endregion

        #region  指令
        /// <summary>
        /// 新增弹窗指令
        /// </summary>
        private RelayCommand addPopupCommand;
        public RelayCommand AddPopupCommand
        {
            get
            {
                if (addPopupCommand == null)
                {
                    addPopupCommand = new RelayCommand(() => AddPopup());
                }
                return addPopupCommand; }
            set { addPopupCommand = value; }
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
                    modifyPopupCommand = new RelayCommand(() => ModifyPopup(SelecteRow));
                }
                return modifyPopupCommand; }
            set { modifyPopupCommand = value; }
        }

        /// <summary>
        /// 新增
        /// </summary>
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get 
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(() => Add());
                }
                return addCommand; }
            set { addCommand = value; }
        }

        /// <summary>
        /// 修改
        /// </summary>
        //private RelayCommand modifyCommand;
        //public RelayCommand ModifyCommand
        //{
        //    get 
        //    {
        //        if (modifyCommand == null)
        //        {
        //           // modifyCommand = new RelayCommand(() => Modify());
        //        }
        //        return modifyCommand; }
        //    set { modifyCommand = value; }
        //}

        /// <summary>
        /// 复制
        /// </summary>
        private RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                if (copyCommand == null)
                {
                    copyCommand = new RelayCommand(() => Copy());
                }
                return copyCommand;
            }
            set { copyCommand = value; }
        }

        /// <summary>
        /// 添加保证金 - 弹出窗
        /// </summary>
        private RelayCommand addMarginPopupCommand;
        public RelayCommand AddMarginPopupCommand
        {
            get 
            {
                if (addMarginPopupCommand == null)
                {
                    addMarginPopupCommand = new RelayCommand(() => AddMarginPopup());
                }
                return addMarginPopupCommand; }
            set { addMarginPopupCommand = value; }
        }

        /// <summary>
        /// 添加保证金
        /// </summary>
        private RelayCommand addMarginCommand;
        public RelayCommand AddMarginCommand 
        {
            get
            {
                if (addMarginCommand == null)
                {
                    addMarginCommand = new RelayCommand(() => AddMargin());
                }
                return addMarginCommand; }
            set { addMarginCommand = value; }
        }

        /// <summary>
        /// 复制 - 弹窗
        /// </summary>
        private RelayCommand copyTrategyPopupCommand;
        public RelayCommand CopyTrategyPopupCommand
        {
            get 
            {
                if (copyTrategyPopupCommand == null)
                {
                    copyTrategyPopupCommand = new RelayCommand(() => CopyTrategyPopup());
                }
                return copyTrategyPopupCommand; }
            set { copyTrategyPopupCommand = value; }
        }

        /// <summary>
        /// 减少保证金 - 弹窗
        /// </summary>
        private RelayCommand reduceBondTrategyPopupCommand;
        public RelayCommand ReduceBondTrategyPopupCommand
        {
            get 
            {
                if (reduceBondTrategyPopupCommand == null)
                {
                    reduceBondTrategyPopupCommand = new RelayCommand(() => ReduceBondTrategyPopup());
                }
                return reduceBondTrategyPopupCommand; }
            set { reduceBondTrategyPopupCommand = value; }
        }

        /// <summary>
        /// 减少保证金
        /// </summary>
        private RelayCommand reduceBondTrategyCommand;
        public RelayCommand ReduceBondTrategyCommand
        {
            get 
            {
                if (reduceBondTrategyCommand == null)
                {
                    reduceBondTrategyCommand = new RelayCommand(() => ReduceBondTrategy());
                }
                return reduceBondTrategyCommand; }
            set { reduceBondTrategyCommand = value; }
        }

        /// <summary>
        /// 转入资金 - 弹窗
        /// </summary>
        private RelayCommand turnIntoCapitalPopupCommand;
        public RelayCommand TurnIntoCapitalPopupCommand 
        {
            get
            {
                if (turnIntoCapitalPopupCommand == null)
                {
                    turnIntoCapitalPopupCommand = new RelayCommand(() => TurnIntoCapitalPopup());
                }
                return turnIntoCapitalPopupCommand; }
            set { turnIntoCapitalPopupCommand = value; }
        }

        /// <summary>
        /// 转入资金
        /// </summary>
        private RelayCommand turnIntoCapitalCommand;
        public RelayCommand TurnIntoCapitalCommand
        {
            get
            {
                if (turnIntoCapitalCommand == null)
                {
                    turnIntoCapitalCommand = new RelayCommand(() => TurnIntoCapital());
                }
                return turnIntoCapitalCommand; }
            set { turnIntoCapitalCommand = value; }
        }

        /// <summary>
        /// 转出资金 - 弹窗
        /// </summary>
        private RelayCommand turnOutCapitalPopupCommand;
        public RelayCommand TurnOutCapitalPopupCommand
        {
            get 
            {
                if (turnOutCapitalPopupCommand == null )
                {
                    turnOutCapitalPopupCommand = new RelayCommand(() => TurnOutCapitalPopup());
                }
                return turnOutCapitalPopupCommand; }
            set { turnOutCapitalPopupCommand = value; }
        }

        /// <summary>
        /// 转出资金
        /// </summary>
        private RelayCommand turnOutCommand;
        public RelayCommand TurnOutCommand
        {
            get 
            {
                if (turnOutCommand == null)
                {
                    turnOutCommand = new RelayCommand(() => TurnOutCapital());
                }
                return turnOutCommand; }
            set { turnOutCommand = value; }
        }

        /// <summary>
        /// 设置冷冻比例 - 弹窗
        /// </summary>
        private RelayCommand freezingRatioPopupCommand;
        public RelayCommand FreezingRatioPopupCommand
        {
            get
            {
                if (freezingRatioPopupCommand == null)
                {
                    freezingRatioPopupCommand = new RelayCommand(() => FreezingRatioPopup());
                }
                return freezingRatioPopupCommand; }
            set { freezingRatioPopupCommand = value; }
        }

        /// <summary>
        ///设置冷冻比例 
        /// </summary>
        private RelayCommand freezingRatioCommand;
        public RelayCommand FreezingRatioCommand
        {
            get 
            {
                if (freezingRatioCommand == null)
                {
                    freezingRatioCommand = new RelayCommand(() => FreezingRatio());
                }
                return freezingRatioCommand; }
            set { freezingRatioCommand = value; }
        }

        /// <summary>
        /// 删除单元
        /// </summary>
        private RelayCommand delUnitCommand;
        public RelayCommand DelUnitCommand
        {
            get
            {
                if (delUnitCommand == null)
                {
                    delUnitCommand = new RelayCommand(() => DelUnit());
                }
                return delUnitCommand;
            }
            set { delUnitCommand = value; }
        }

        /// <summary>
        /// 结算
        /// </summary>
        private RelayCommand settlementCommand;
        public RelayCommand SettlementCommand
        {
            get {

                if (settlementCommand == null)
                {
                    settlementCommand = new RelayCommand(() => Settlement());
                }
                return settlementCommand; }
            set { settlementCommand = value; }
        }

        /// <summary>
        /// 启用
        /// </summary>
        private RelayCommand activationCommand;
        public RelayCommand ActivationCommand
        {
            get
            {

                if (activationCommand == null)
                {
                    activationCommand = new RelayCommand(() => Activation());
                }
                return activationCommand;
            }
            set { activationCommand = value; }
        }

        /// <summary>
        /// 启用
        /// </summary>
        private RelayCommand enableCommand; 
        public RelayCommand EnableCommand 
        {
            get { return enableCommand; }
            set { enableCommand = value; }
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

        /// <summary>
        /// 刷新
        /// </summary>
        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(() => Refresh());
                }
                return refreshCommand;
            }
            set { refreshCommand = value; }
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

        /// <summary>
        /// 搜索
        /// </summary>
        //private RelayCommand<object> searchCommand;
        //public RelayCommand<object> SearchCommand
        //{
        //    get {
        //        if (searchCommand == null)
        //        {
        //            searchCommand = new RelayCommand<object>(Search);
        //        }
        //        return searchCommand; }
        //    set { searchCommand = value;}
        //}
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(()=>Search());
                }
                return searchCommand;
            }
            set { searchCommand = value; }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private MidStrategyUnitManageModel cmbItem;
        public MidStrategyUnitManageModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem); }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<MidStrategyUnitManageModel> cmbList;
        public ObservableCollection<MidStrategyUnitManageModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        /// <summary>
        /// 修改弹窗
        /// </summary>
        private void ModifyPopup(Model.MidStrategyUnitManageModel SelectedRow) 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else 
            {
                MessageDialogManager.ShowModifyTrategyUnitView(SelectedRow);
            }
        }

        /// <summary>
        /// 复制弹窗
        /// </summary>
        private void CopyPopup()
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else
            {
                MessageDialogManager.ShowCopyTrategyUnitView();
            }
        }

        /// <summary>
        /// 新增弹窗
        /// </summary>
        private void AddPopup() 
        {
            MessageDialogManager.ShowAddTrategyUnitView();
        }

        /// <summary>
        ///  添加保证金弹窗
        /// </summary>
        private void AddMarginPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null))
            {
                MessageDialogManager.ShowAddMarginView();
            }
        }

        /// <summary>
        ///复制 弹窗
        /// </summary>
        private void CopyTrategyPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null))
            {
                MessageDialogManager.ShowCopyTrategyUnitView();
            }
        }

        /// <summary>
        /// 转入资金 弹窗
        /// </summary>
        private void TurnIntoCapitalPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null))
            {
                MessageDialogManager.ShowTurnIntoCapitalView();
            }
        }

        /// <summary>
        ///减少保证金 弹窗 
        /// </summary>
        private void ReduceBondTrategyPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null))
            {
                MessageDialogManager.ShowReduceBondTrategyUnitView();
            }
        }

        /// <summary>
        /// 转出资金 - 弹窗
        /// </summary>
        private void TurnOutCapitalPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null ))
            {

                MessageDialogManager.ShowTurnOutCapitalView();
            }
        }

        /// <summary>
        /// 设置冷冻比例 - 弹窗
        /// </summary>
        private void FreezingRatioPopup() 
        {
            if (SelecteRow == null)
            {
                MessageDialogManager.ShowDialogAsync("未选中!");
                return;
            }
            else if (!(SelecteRow.UnitCode == null))
            {
                MessageDialogManager.ShowFreezingRatioTrategyUnitView();
            }
        }

        /// <summary>
        /// 单元列表加载
        /// </summary>
        private void InitDataGrid() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IMidStrategyInterface midStrategy = new IMidStrategyInterface();
                var result = await Task.Run(() => midStrategy.Strategy(token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MidStrategyUnitManageModel.Root data = JsonConvert.DeserializeObject<MidStrategyUnitManageModel.Root>(jsonData);
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        var m = new MidStrategyUnitManageModel()
                        {
                            UnitCode = data.Data[i].code,
                            Id = data.Data[i].id,
                            UnitName = data.Data[i].name,
                            UnitAgent = data.Data[i].broker,
                            AccountGroupId = data.Data[i].account_group_id,
                            UnitRegion = data.Data[i].area,
                            ScienceInnovationBoardRatio = data.Data[i].limit_ratio_star_single,
                            UnitRisk = data.Data[i].risk_controller,
                            OpeningTime = data.Data[i].opened_time.ToString(),
                            Account = data.Data[i].name,
                            AccountPool = data.Data[i].account_group_id,
                            MidBond = data.Data[i].bond,
                            Unitleverage = data.Data[i].lever,
                            UnitFunds = data.Data[i].capital_scale,
                            TotalAssets = data.Data[i].capital_Total,
                            FreezingRatio = data.Data[i].ratio_freezing,
                            ManageRatio = data.Data[i].ratio_management_fee,
                            CommissionRate = data.Data[i].ratio_commission,
                            UnitSoftwareRate = data.Data[i].ratio_software_fee,
                            IndividualRestrictionStock = data.Data[i].limit_stock_count,
                            NoBuyingShares = data.Data[i].limit_no_buying,
                            ProportionBoardStocks = data.Data[i].limit_ratio_mbm_single,
                            ProportionGemStocks = data.Data[i].limit_ratio_gem_single,
                            TotalProportionGem = data.Data[i].limit_ratio_gem_total,
                            ProportionSmallMediumBoardStocks = data.Data[i].limit_ratio_sme_single,
                            TotalProportionSmallMediumBoards = data.Data[i].limit_ratio_sme_total,
                            MiddleSmallTotalProportion = data.Data[i].limit_ratio_smg_total,
                            ScienceInnovationTotalRatio = data.Data[i].limit_ratio_star_total,
                            CordonRatio = data.Data[i].ratio_warning,
                            LevelingLineRatio = data.Data[i].ratio_close_position
                        };
                        ListAll.Add(m);
                        List.Add(m);
                    }
                   
                }
                else 
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }
    
        /// <summary>
        /// 新增中期策略单元
        /// </summary>
        private void Add()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async ()=>
             {
                 if (ValidateUI.IsValidated)
                    {
                    if (string.IsNullOrWhiteSpace(validateUI.UnitCode))
                    {
                        MessageDialogManager.ShowDialogAsync("代码不能为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(validateUI.UnitName))
                    {
                        
                        MessageDialogManager.ShowDialogAsync("名称不能为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(validateUI.UnitRegion))
                    {
                        MessageDialogManager.ShowDialogAsync("地区不为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(validateUI.UnitAgent))
                    {
                        MessageDialogManager.ShowDialogAsync("经纪人不为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(validateUI.UnitRisk))
                    {
                        MessageDialogManager.ShowDialogAsync("风控员不为空!");
                        return;
                    }

                    if (ValidateUI.AccountPool.HasValue)
                    {
                        if (ValidateUI.AccountPool % 1 > 0)
                        {
                            MessageDialogManager.ShowDialogAsync("账号池只能输入整数");
                            return;
                        }
                    }
                    else
                    {

                        MessageDialogManager.ShowDialogAsync("账号池不为空!");
                        return;
                    }


                    if (!ValidateUI.Unitleverage.HasValue)

                    {
                        MessageDialogManager.ShowDialogAsync("杠杆不为空!");
                        return;
                    }

                    if (ValidateUI.ManageRatio.HasValue)
                     {
                        if (!(ValidateUI.ManageRatio >= 0 && ValidateUI.ManageRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("管理费率为小于1的小数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("管理费率不为空!");
                        return;
                    }

                    if (ValidateUI.CommissionRate.HasValue)
                    {
                        if (!(ValidateUI.CommissionRate >= 0 && ValidateUI.CommissionRate < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("佣金率为小于1的小数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("佣金率不为空!");
                        return;
                    }

                    if (ValidateUI.IndividualRestrictionStock.HasValue)
                    {
                        if (ValidateUI.IndividualRestrictionStock % 1 > 0)
                        {
                            MessageDialogManager.ShowDialogAsync("股票个数限制为整数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("股票个人限制不为空!");
                        return;
                    }

                    if (ValidateUI.ProportionGemStocks.HasValue)
                    {
                        if (!(ValidateUI.ProportionGemStocks >= 0 && ValidateUI.ProportionGemStocks < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板个股比例不为空!");
                        return;
                    }

                    if (ValidateUI.TotalProportionGem.HasValue)
                    {
                        if (!(ValidateUI.TotalProportionGem >= 0 && ValidateUI.TotalProportionGem < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板总比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板总比例不为空!");
                        return;
                    }

                    if (ValidateUI.ScienceInnovationBoardRatio.HasValue)
                    {
                        if (!(ValidateUI.ScienceInnovationBoardRatio >= 0 && ValidateUI.ScienceInnovationBoardRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("科创板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("科创板个股比例不为空!");
                        return;
                    }

                    if (ValidateUI.TotalProportionSmallMediumBoards.HasValue)
                    {
                        if (!(ValidateUI.TotalProportionSmallMediumBoards >= 0 && ValidateUI.TotalProportionSmallMediumBoards < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小板总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小板总比例不为空!");
                        return;
                    }

                    if (ValidateUI.MiddleSmallTotalProportion.HasValue)
                    {
                        if (!(ValidateUI.MiddleSmallTotalProportion >= 0 && ValidateUI.MiddleSmallTotalProportion < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小创总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小创总比例不为空!");
                        return;
                    }



                    if (ValidateUI.ScienceInnovationTotalRatio.HasValue)
                    {
                        if (!(ValidateUI.ScienceInnovationTotalRatio >= 0 && ValidateUI.ScienceInnovationTotalRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("科创板总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("科创板总比例不为空!");
                        return;
                    }

                    if (ValidateUI.CordonRatio.HasValue)
                    {
                        if (!(ValidateUI.CordonRatio >= 0 && ValidateUI.CordonRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("警戒线比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("警戒线比例不为空!");
                        return;
                    }

                    if (ValidateUI.LevelingLineRatio.HasValue)
                    {
                        if (!(ValidateUI.LevelingLineRatio >= 0 && ValidateUI.LevelingLineRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("平仓线比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("平仓线比例不为空!");
                        return;
                    }



                    if (ValidateUI.PriceLimit.Equals(null))
                    {
                        MessageDialogManager.ShowDialogAsync("委托价格范围限制不为空!");
                        return;
                    }
                    string token = UserToken.token;
                    IAddMidStrategyInterface addMdiStrategy = new IAddMidStrategyInterface();
                     var result =  addMdiStrategy.AddMidStrategy
                             (validateUI.UnitCode, validateUI.UnitName,validateUI.UnitRegion, validateUI.UnitAgent, 
                              validateUI.UnitRisk, validateUI.AccountPool, validateUI.Unitleverage, validateUI.ManageRatio,
                              validateUI.CommissionRate, validateUI.UnitSoftwareRate, validateUI.IndividualRestrictionStock, validateUI.ProportionBoardStocks,
                              validateUI.ProportionGemStocks,validateUI.TotalProportionGem,validateUI.ScienceInnovationBoardRatio,
                              validateUI.TotalProportionSmallMediumBoards,validateUI.MiddleSmallTotalProportion,validateUI.ScienceInnovationBoardRatio,validateUI.ScienceInnovationTotalRatio,
                              validateUI.CordonRatio,validateUI.LevelingLineRatio,validateUI.NoBuyingShares, validateUI.PriceLimit,
                              token);
                    string success = result["Message"]["Message"].ToString(); 
                      if (success =="成功")
                      {
                        ToClose = true;
                        Refresh();
                        MessageDialogManager.ShowDialogAsync("新增单元成功!");
                        return;
                      }
                      else 
                     {
                        MessageDialogManager.ShowDialogAsync(success);
                     }       
                    }
                });
          
        }


        ///// <summary>
        /////  修改中期策略单元
        ///// </summary>
        //private void Modify() 
        //{
        //    DispatcherHelper.CheckBeginInvokeOnUI(async ()=>
        //    {
        //        if (MidStrategyUnitManage.IsValidated)
        //        {
        //            if (string.IsNullOrWhiteSpace(modifyMid.UnitCode))
        //            {
        //                MessageDialogManager.ShowDialogAsync("代码不能为空!");
        //                return;
        //            }
        //             if (string.IsNullOrWhiteSpace(MidStrategyUnitManage.UnitName))
        //            {
        //                MessageDialogManager.ShowDialogAsync("名称不能为空!");

        //                return;
        //            }
        //             if (string.IsNullOrWhiteSpace(MidStrategyUnitManage.UnitRegion))
        //            {
        //                MessageDialogManager.ShowDialogAsync("地区不为空!");
        //                return;
        //            }
        //             if (string.IsNullOrWhiteSpace(MidStrategyUnitManage.UnitAgent))
        //            {
        //                MessageDialogManager.ShowDialogAsync("经纪人不为空!");
        //                return;
        //            }
        //             if (string.IsNullOrWhiteSpace(MidStrategyUnitManage.UnitRisk))
        //            {
        //                MessageDialogManager.ShowDialogAsync("风控员不为空!");
        //                return;
        //            }

        //             if (!MidStrategyUnitManage.AccountPool.HasValue)
        //            {
        //                MessageDialogManager.ShowDialogAsync("账号池不为空!");
        //                return;
        //            }
        //             if(!MidStrategyUnitManage.Unitleverage.HasValue) 

        //            {
        //                MessageDialogManager.ShowDialogAsync("杠杆不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.ManageRatio.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.ManageRatio  >= 0 && MidStrategyUnitManage.ManageRatio < 1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("管理费率为小于1的小数");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("管理费率不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.CommissionRate.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.CommissionRate >= 0 && MidStrategyUnitManage.CommissionRate < 1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("佣金率为小于1的小数");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("佣金率不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.IndividualRestrictionStock.HasValue)
        //            {
        //                if (MidStrategyUnitManage.IndividualRestrictionStock % 1 > 0)
        //                {
        //                    MessageDialogManager.ShowDialogAsync("股票个数限制为整数");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("股票个人限制不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.ProportionGemStocks.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.ProportionGemStocks >=0 && MidStrategyUnitManage.ProportionGemStocks <1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("创业板个股比例为小于1的小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("创业板个股比例不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.TotalProportionGem.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.TotalProportionGem >= 0 && MidStrategyUnitManage.TotalProportionGem <1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("创业板总比例为小于1的小数值");
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                MessageDialogManager.ShowDialogAsync("创业板总比例不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.ScienceInnovationBoardRatio.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.ScienceInnovationBoardRatio >= 0 && MidStrategyUnitManage.ScienceInnovationBoardRatio<1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("科创板个股比例为小于1的小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("科创板个股比例不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.TotalProportionSmallMediumBoards.HasValue)
        //            {
        //               if (!(MidStrategyUnitManage.TotalProportionSmallMediumBoards  >= 0 && MidStrategyUnitManage.TotalProportionSmallMediumBoards<1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("中小板总比例为小于1小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("中小板总比例不为空!");
        //                return;
        //            }

        //             if (MidStrategyUnitManage.MiddleSmallTotalProportion.HasValue)
        //            {
        //                if (!(MidStrategyUnitManage.MiddleSmallTotalProportion  >= 0 && MidStrategyUnitManage.MiddleSmallTotalProportion <1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("中小创总比例为小于1小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("中小创总比例不为空!");
        //                return;
        //            }



        //             if (SelecteRow.ScienceInnovationTotalRatio.HasValue)
        //            {
        //                if (!(SelecteRow.ScienceInnovationTotalRatio  >= 0 && SelecteRow.ScienceInnovationTotalRatio < 1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("科创板总比例为小于1小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("科创板总比例不为空!");
        //                return;
        //            }

        //             if (SelecteRow.CordonRatio.HasValue)
        //            {
        //                if (!(SelecteRow.CordonRatio  >= 0 && SelecteRow.CordonRatio < 1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("警戒线比例为小于1小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("警戒线比例不为空!");
        //                return;
        //            }

        //             if (SelecteRow.LevelingLineRatio.HasValue)
        //            {
        //                if (!(SelecteRow.LevelingLineRatio >= 0 && SelecteRow.LevelingLineRatio < 1))
        //                {
        //                    MessageDialogManager.ShowDialogAsync("平仓线比例为小于1小数值");
        //                    return;
        //                }
        //            }
        //            else 
        //            {
        //                MessageDialogManager.ShowDialogAsync("平仓线比例不为空!");
        //                return;
        //            }



        //             if (SelecteRow.PriceLimit.Equals(null))
        //            {
        //                MessageDialogManager.ShowDialogAsync("委托价格范围限制不为空!");
        //                return;
        //            }

        //            string token = UserToken.token;
        //            IModifyMidStrategyInterface modifyMidStrategy = new IModifyMidStrategyInterface();
        //            var result = await Task.Run(() => modifyMidStrategy.ModifyMidStrategy
        //            (SelecteRow.Id, SelecteRow.UnitCode, SelecteRow.UnitName, SelecteRow.UnitRegion, SelecteRow.UnitAgent,
        //            SelecteRow.UnitRisk, SelecteRow.AccountPool, SelecteRow.Unitleverage, SelecteRow.CommissionRate,
        //            SelecteRow.ManageRatio, SelecteRow.UnitSoftwareRate, SelecteRow.IndividualRestrictionStock, SelecteRow.ProportionBoardStocks,
        //            SelecteRow.ProportionGemStocks, SelecteRow.TotalProportionGem, SelecteRow.ProportionSmallMediumBoardStocks, SelecteRow.TotalProportionSmallMediumBoards,
        //            SelecteRow.MiddleSmallTotalProportion, SelecteRow.ScienceInnovationBoardRatio, SelecteRow.ScienceInnovationTotalRatio, SelecteRow.CordonRatio,
        //            SelecteRow.LevelingLineRatio, SelecteRow.NoBuyingShares, SelecteRow.PriceLimit, token));

        //            string success = result["Message"]["Message"].ToString();
        //            if (success == "成功")
        //            {
        //                Refresh();
        //                ToClose = true;
        //                MessageDialogManager.ShowDialogAsync("修改单元成功!");
        //                return;
        //            }
        //            else 
        //            {


        //                MessageDialogManager.ShowDialogAsync(success);
        //            }
        //        }
        //    });
        //}


        #region 账号池下拉框列表
        private MidStrategyUnitManageModel cmbItemPool;
        public MidStrategyUnitManageModel CmbItemPool
        {
            get { return cmbItemPool; }
            set { cmbItemPool = value;  }
        }

        /// <summary>
        /// 账号池下拉框列表
        /// </summary>
        private ObservableCollection<MidStrategyUnitManageModel> cmbListPool;
        public ObservableCollection<MidStrategyUnitManageModel> CmbListPool
        {
            get { return cmbListPool; }
            set { cmbListPool = value;  }
        }

        private void AccountPool()
        {
            string token = UserToken.token;
            IModifyMidStrategyInterface modifyMidStrategy = new IModifyMidStrategyInterface();
            var resultPool = modifyMidStrategy.AccountPoolFilter(token);
            string successPool = resultPool["Message"]["Message"].ToString();
            string jsonDataPool = resultPool["Message"].ToString();
            if (successPool == "成功")
            {
                MidStrategyUnitManageModel.PoolRoot data = JsonConvert.DeserializeObject<MidStrategyUnitManageModel.PoolRoot>(jsonDataPool);
                CmbListPool = new ObservableCollection<MidStrategyUnitManageModel>();
                for (int i = 0; i < data.Data.Count; i++)
                {
                    CmbListPool.Add(new MidStrategyUnitManageModel
                    {
                        Code = data.Data[i].code,
                        Id = data.Data[i].id,
                        Name = data.Data[i].name
                    });
                }
                return;

            }
            else { MessageDialogManager.ShowDialogAsync(successPool); }
        }
        #endregion

        /// <summary>
        ///  复制中期策略单元
        /// </summary>
        private void Copy()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (SelecteRow.IsValidated)
                {
                    if (string.IsNullOrWhiteSpace(SelecteRow.UnitCode))
                    {
                        MessageDialogManager.ShowDialogAsync("代码不能为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(SelecteRow.UnitName))
                    {
                        MessageDialogManager.ShowDialogAsync("名称不能为空!");

                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(SelecteRow.UnitRegion))
                    {
                        MessageDialogManager.ShowDialogAsync("地区不为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(SelecteRow.UnitAgent))
                    {
                        MessageDialogManager.ShowDialogAsync("经纪人不为空!");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(SelecteRow.UnitRisk))
                    {
                        MessageDialogManager.ShowDialogAsync("风控员不为空!");
                        return;
                    }

                    if (SelecteRow.AccountPool.HasValue)
                    {
                        if (SelecteRow.AccountPool % 1 > 0)
                        {
                            MessageDialogManager.ShowDialogAsync("账号池只能输入整数");
                            return;
                        }
                    }
                    else
                    {

                        MessageDialogManager.ShowDialogAsync("账号池不为空!");
                        return;
                    }

                    if (!SelecteRow.Unitleverage.HasValue)

                    {
                        MessageDialogManager.ShowDialogAsync("杠杆不为空!");
                        return;
                    }

                    if (SelecteRow.ManageRatio.HasValue)
                    {
                        if (!(SelecteRow.ManageRatio >= 0 && SelecteRow.ManageRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("管理费率为小于1的小数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("管理费率不为空!");
                        return;
                    }

                    if (SelecteRow.CommissionRate.HasValue)
                    {
                        if (!(SelecteRow.CommissionRate >= 0 && SelecteRow.CommissionRate < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("佣金率为小于1的小数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("佣金率不为空!");
                        return;
                    }

                    if (SelecteRow.IndividualRestrictionStock.HasValue)
                    {
                        if (SelecteRow.IndividualRestrictionStock % 1 > 0)
                        {
                            MessageDialogManager.ShowDialogAsync("股票个数限制为整数");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("股票个人限制不为空!");
                        return;
                    }

                    if (SelecteRow.ProportionGemStocks.HasValue)
                    {
                        if (!(SelecteRow.ProportionGemStocks >= 0 && SelecteRow.ProportionGemStocks < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板个股比例不为空!");
                        return;
                    }

                    if (SelecteRow.TotalProportionGem.HasValue)
                    {
                        if (!(SelecteRow.TotalProportionGem >= 0 && SelecteRow.TotalProportionGem < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("创业板总比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("创业板总比例不为空!");
                        return;
                    }

                    if (SelecteRow.ScienceInnovationBoardRatio.HasValue)
                    {
                        if (!(SelecteRow.ScienceInnovationBoardRatio >= 0 && SelecteRow.ScienceInnovationBoardRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("科创板个股比例为小于1的小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("科创板个股比例不为空!");
                        return;
                    }

                    if (SelecteRow.TotalProportionSmallMediumBoards.HasValue)
                    {
                        if (!(SelecteRow.TotalProportionSmallMediumBoards >= 0 && SelecteRow.TotalProportionSmallMediumBoards < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小板总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小板总比例不为空!");
                        return;
                    }

                    if (SelecteRow.MiddleSmallTotalProportion.HasValue)
                    {
                        if (!(SelecteRow.MiddleSmallTotalProportion >= 0 && SelecteRow.MiddleSmallTotalProportion < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("中小创总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("中小创总比例不为空!");
                        return;
                    }



                    if (SelecteRow.ScienceInnovationTotalRatio.HasValue)
                    {
                        if (!(SelecteRow.ScienceInnovationTotalRatio >= 0 && SelecteRow.ScienceInnovationTotalRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("科创板总比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("科创板总比例不为空!");
                        return;
                    }

                    if (SelecteRow.CordonRatio.HasValue)
                    {
                        if (!(SelecteRow.CordonRatio >= 0 && SelecteRow.CordonRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("警戒线比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("警戒线比例不为空!");
                        return;
                    }

                    if (SelecteRow.LevelingLineRatio.HasValue)
                    {
                        if (!(SelecteRow.LevelingLineRatio >= 0 && SelecteRow.LevelingLineRatio < 1))
                        {
                            MessageDialogManager.ShowDialogAsync("平仓线比例为小于1小数值");
                            return;
                        }
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("平仓线比例不为空!");
                        return;
                    }



                    if (SelecteRow.PriceLimit.Equals(null))
                    {
                        MessageDialogManager.ShowDialogAsync("委托价格范围限制不为空!");
                        return;
                    }


                    string token = UserToken.token;
                    IModifyMidStrategyInterface modifyMidStrategy = new IModifyMidStrategyInterface();
                    var result = await Task.Run(() => modifyMidStrategy.ModifyMidStrategy
                    (SelecteRow.Id, SelecteRow.UnitCode, SelecteRow.UnitName, SelecteRow.UnitRegion, SelecteRow.UnitAgent,
                    SelecteRow.UnitRisk, SelecteRow.AccountPool, SelecteRow.Unitleverage, SelecteRow.CommissionRate,
                    SelecteRow.ManageRatio, SelecteRow.UnitSoftwareRate, SelecteRow.IndividualRestrictionStock, SelecteRow.ProportionBoardStocks,
                    SelecteRow.ProportionGemStocks, SelecteRow.TotalProportionGem, SelecteRow.ProportionSmallMediumBoardStocks, SelecteRow.TotalProportionSmallMediumBoards,
                    SelecteRow.MiddleSmallTotalProportion, SelecteRow.ScienceInnovationBoardRatio, SelecteRow.ScienceInnovationTotalRatio, SelecteRow.CordonRatio,
                    SelecteRow.LevelingLineRatio, SelecteRow.NoBuyingShares, SelecteRow.PriceLimit, token));

                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("复制单元成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
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
        /// 添加保证金
        /// </summary>
        private void AddMargin() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    string token = UserToken.token;
                    IAddMarginInterface addMargin = new IAddMarginInterface();
                    //1表示资金;0表示保证金
                    int Addmargintype = 0;
                    //1表示增加;0表示减少
                    int AddmargintAction = 1;
                   var result = await Task.Run(() => addMargin.AddMargin(SelecteRow.Id, Addmargintype, AddmargintAction,
                        ValidateUI.MidBond, ValidateUI.Remarks,token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        List.Clear();
                        List = new BindingList<MidStrategyUnitManageModel>();
                        InitDataGrid();
                       // ToClose = true;
                        MessageDialogManager.ShowDialogAsync("增加保证成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 减少保证金
        /// </summary>
        private void ReduceBondTrategy() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    string token = UserToken.token;
                    //1表示资金;0表示保证金
                    int reduceType = 0;
                    //1表示增加;0表示减少
                    int reduceAction = 1;
                    IAddMarginInterface reduceBond = new IAddMarginInterface();
                    var result = await Task.Run(() => reduceBond.AddMargin(SelecteRow.Id, reduceType, reduceAction,
                        ValidateUI.MidBond, ValidateUI.Remarks, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("减少保证金成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 转入资金
        /// </summary>
        private void TurnIntoCapital() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                if (validateUI.IsValidated)
                {
                    string token = UserToken.token;
                    //1表示资金;0表示保证金
                    int turnType = 1;
                    //1表示增加;0表示减少
                    int turnAction = 1;
                    IAddMarginInterface turnIntoCapital = new IAddMarginInterface();
                    var result = await Task.Run(() => turnIntoCapital.AddMargin(SelecteRow.Id, turnType, turnAction, ValidateUI.MidBond, ValidateUI.Remarks, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("转入资金成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        ///  转出资金
        /// </summary>
        private void TurnOutCapital() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                if (ValidateUI.IsValidated)
                {
                    string token = UserToken.token;
                    //1表示资金;0表示保证金
                    int turnOutType = 1;
                    //1表示增加;0表示减少
                    int turnOutAction = 1;
                    IAddMarginInterface turnOutCapital = new IAddMarginInterface();
                    var result = await Task.Run(() => turnOutCapital.AddMargin(SelecteRow.Id, turnOutType, turnOutAction, ValidateUI.MidBond, ValidateUI.Remarks, token));
                    string success = result["Message"]["Message"].ToString();
                    if (success == "成功")
                    {
                        Refresh();
                        ToClose = true;
                        MessageDialogManager.ShowDialogAsync("转出资金成功!");
                        return;
                    }
                    else 
                    {
                        MessageDialogManager.ShowDialogAsync(success);
                    }
                }
            });
        }

        /// <summary>
        /// 设置冷冻比例
        /// </summary>
        private void FreezingRatio() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
           {
               if (ValidateUI.IsValidated)
               {
                   string token = UserToken.token;
                   ISetFrozenRationInterface setFrozenRation = new ISetFrozenRationInterface();
                   var reuslt = await Task.Run(() => setFrozenRation.SetFrozenRation(SelecteRow.Id, SelecteRow.FreezingRatio, token));
                   string success = reuslt["Message"]["Message"].ToString();
                   if (success =="成功")
                   {
                       Refresh();
                       ToClose = true;
                       MessageDialogManager.ShowDialogAsync("设置冷冻比例成功!");
                       return;
                   }
                   else 
                   {
                       MessageDialogManager.ShowDialogAsync(success);
                   }
               }
           });
        }

        /// <summary>
        /// 删除单元
        /// </summary>
        private void DelUnit()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IDelUnitIterface delUnit = new IDelUnitIterface();
                var result = await Task.Run(() => delUnit.DelUnit(SelecteRow.Id, SelecteRow.UnitCode, token));
                string success = result["Message"]["Message"].ToString();
                if (success == "成功")
                {
                    Refresh();
                    ToClose = true;
                    MessageDialogManager.ShowDialogAsync("选中单元删除成功!");
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }

        /// <summary>
        /// 单元List
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int Request = 1;
                IMidStrategyInterface midStrategy = new IMidStrategyInterface();
                var result = await Task.Run(() => midStrategy.UnitList(Request, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MidStrategyUnitManageModel.UnitRoot data = JsonConvert.DeserializeObject<MidStrategyUnitManageModel.UnitRoot>(jsonData);
                    CmbList = new ObservableCollection<MidStrategyUnitManageModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new MidStrategyUnitManageModel
                        {
                            Code = data.Data[i].code,
                            Id = data.Data[i].id,
                            Name = data.Data[i].name
                        });
                    }
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }

        /// <summary>
        ///结算
        /// </summary>
        private void Settlement()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
            string token = UserToken.token;
            int Request = 0; //启用为1;停用0
            ISettlementInterface settlement = new ISettlementInterface();
            var result = await Task.Run(() => settlement.Settlement(SelecteRow.Id, Request, token));
            string success = result["Message"]["Message"].ToString();
            string jsonData = result["Message"].ToString();
            if (success == "成功")
            {
                    Refresh();
                    MessageDialogManager.ShowDialogAsync("选中记录结算成功!");
                return;
            }
            else
            {
                MessageDialogManager.ShowDialogAsync(success);
            }
            });
        }

        /// <summary>
        /// 启用
        /// </summary>
        private void Activation() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int Request = 1; //启用为1;停用0
                ISettlementInterface settlement = new ISettlementInterface();
                var result = await Task.Run(() => settlement.Settlement(SelecteRow.Id, Request, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    Refresh();
                    MessageDialogManager.ShowDialogAsync("选中记录启用成功!");
                    return;
                }
                else
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }
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
                    irow.CreateCell(0).SetCellValue("代码");
                    irow.CreateCell(1).SetCellValue("名称");
                    irow.CreateCell(2).SetCellValue("地区");
                    irow.CreateCell(3).SetCellValue("经纪人");
                    irow.CreateCell(4).SetCellValue("风控员");
                    irow.CreateCell(5).SetCellValue("开户时间");
                    irow.CreateCell(6).SetCellValue("主账号");
                    irow.CreateCell(7).SetCellValue("账号池");
                    irow.CreateCell(8).SetCellValue("保证金");
                    irow.CreateCell(9).SetCellValue("杆杆");
                    irow.CreateCell(10).SetCellValue("资金规模");
                    irow.CreateCell(11).SetCellValue("总资产");
                    irow.CreateCell(12).SetCellValue("冻结比例");
                    irow.CreateCell(13).SetCellValue("管理费率");
                    irow.CreateCell(14).SetCellValue("佣金率");
                    irow.CreateCell(15).SetCellValue("软件率");
                    irow.CreateCell(16).SetCellValue("股票个数限制");
                    irow.CreateCell(17).SetCellValue("主板个股比例");
                    irow.CreateCell(18).SetCellValue("创业板个股比例");
                    irow.CreateCell(19).SetCellValue("创业板总比例");
                    irow.CreateCell(20).SetCellValue("中小板个股比例");
                    irow.CreateCell(21).SetCellValue("中小板总比例");
                    irow.CreateCell(22).SetCellValue("中小创总比例");
                    irow.CreateCell(23).SetCellValue("科创业板个股比例");
                    irow.CreateCell(24).SetCellValue("科创板总比例");
                    irow.CreateCell(25).SetCellValue("警戒线比例");
                    irow.CreateCell(26).SetCellValue("平仓线比例");

                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].UnitCode);
                        row.CreateCell(1).SetCellValue(List[i].UnitName);
                        row.CreateCell(2).SetCellValue(List[i].UnitRegion);
                        row.CreateCell(3).SetCellValue(List[i].UnitAgent);
                        row.CreateCell(4).SetCellValue(List[i].UnitRisk);
                        row.CreateCell(5).SetCellValue(List[i].OpeningTime.ToString());
                        row.CreateCell(6).SetCellValue(List[i].Account);
                        row.CreateCell(7).SetCellValue(List[i].AccountPool.ToString());
                        row.CreateCell(8).SetCellValue(List[i].MidBond.ToString());
                        row.CreateCell(9).SetCellValue(List[i].Unitleverage.ToString());
                        row.CreateCell(10).SetCellValue(List[i].UnitFunds.ToString());
                        row.CreateCell(11).SetCellValue(List[i].TotalAssets.ToString());
                        row.CreateCell(12).SetCellValue(List[i].FreezingRatio.ToString());
                        row.CreateCell(13).SetCellValue(List[i].ManageRatio.ToString());
                        row.CreateCell(14).SetCellValue(List[i].CommissionRate.ToString());
                        row.CreateCell(15).SetCellValue(List[i].UnitSoftwareRate.ToString());
                        row.CreateCell(16).SetCellValue(List[i].IndividualRestrictionStock.ToString());
                        row.CreateCell(17).SetCellValue(List[i].ProportionBoardStocks.ToString());
                        row.CreateCell(18).SetCellValue(List[i].ProportionGemStocks.ToString());
                        row.CreateCell(19).SetCellValue(List[i].TotalProportionGem.ToString());
                        row.CreateCell(20).SetCellValue(List[i].ProportionSmallMediumBoardStocks.ToString());
                        row.CreateCell(21).SetCellValue(List[i].TotalProportionSmallMediumBoards.ToString());
                        row.CreateCell(22).SetCellValue(List[i].MiddleSmallTotalProportion.ToString());
                        row.CreateCell(23).SetCellValue(List[i].ScienceInnovationBoardRatio.ToString());
                        row.CreateCell(24).SetCellValue(List[i].ScienceInnovationTotalRatio.ToString());
                        row.CreateCell(25).SetCellValue(List[i].CordonRatio.ToString());
                        row.CreateCell(26).SetCellValue(List[i].LevelingLineRatio.ToString());

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
            ListAll.Where(i => i.UnitCode.Contains(ValidateUI.Code)).ToList().ForEach(i =>
            {
                List.Add(i);
            });//.Select(i => new ListDataModel()
            //{
            //    UnitCode = i.UnitCode,
            //    UnitName = i.UnitName,
            //    UnitRegion = i.UnitRegion,
            //    UnitAgent = i.UnitAgent,
            //    UnitRisk = i.UnitRisk,
            //    OpeningTime = i.OpeningTime,
            //    Account = i.Account,
            //    AccountPool = i.AccountPool,
            //    MidBond = i.MidBond,
            //    Unitleverage = i.Unitleverage,
            //    UnitFunds = i.UnitFunds,
            //    TotalAssets = i.TotalAssets,
            //    FreezingRatio = i.FreezingRatio,
            //    ManageRatio = i.ManageRatio,
            //    CommissionRate = i.CommissionRate,
            //    UnitSoftwareRate = i.UnitSoftwareRate,
            //    IndividualRestrictionStock = i.IndividualRestrictionStock,
            //    ProportionBoardStocks = i.ProportionBoardStocks,
            //    ProportionGemStocks = i.ProportionGemStocks,
            //    TotalProportionGem = i.TotalProportionGem,
            //    ProportionSmallMediumBoardStocks = i.ProportionSmallMediumBoardStocks,
            //    TotalProportionSmallMediumBoards = i.TotalProportionSmallMediumBoards,
            //    ScienceInnovationBoardRatio = i.ScienceInnovationBoardRatio,
            //    ScienceInnovationTotalRatio = i.ScienceInnovationTotalRatio,
            //    CordonRatio = i.CordonRatio,
            //    LevelingLineRatio = i.LevelingLineRatio
            //}).ToList();
            //List.Clear();
            //serchList.ForEach(i =>
            //{
            //    var t = new MidStrategyUnitManageModel();
            //    t.UnitCode = i.UnitCode;
            //    t.UnitName = i.UnitName;
            //    t.UnitAgent = i.UnitAgent;
            //    t.OpeningTime = i.OpeningTime;
            //    t.Account = i.Account;
            //    t.AccountPool = i.AccountPool;
            //    t.MidBond = i.MidBond;
            //    t.Unitleverage = i.Unitleverage;
            //    t.UnitFunds = i.UnitFunds;
            //    t.TotalAssets = i.TotalAssets;
            //    t.FreezingRatio = i.FreezingRatio;
            //    t.ManageRatio = i.ManageRatio;
            //    t.CommissionRate = i.CommissionRate;
            //    t.UnitSoftwareRate = i.UnitSoftwareRate;
            //    t.IndividualRestrictionStock = i.IndividualRestrictionStock;
            //    t.ProportionBoardStocks = i.ProportionBoardStocks;
            //    t.ProportionGemStocks = i.ProportionGemStocks;
            //    t.TotalProportionGem = i.TotalProportionGem;
            //    t.ProportionSmallMediumBoardStocks = i.ProportionSmallMediumBoardStocks;
            //    t.TotalProportionSmallMediumBoards = i.TotalProportionSmallMediumBoards;
            //    t.ScienceInnovationBoardRatio = i.ScienceInnovationBoardRatio;
            //    t.ScienceInnovationTotalRatio = i.ScienceInnovationTotalRatio;
            //    t.CordonRatio = i.CordonRatio;
            //    t.LevelingLineRatio = i.LevelingLineRatio;
            //    List.Add(t);
            //});

            //Console.WriteLine(List);
        }
        public object Clone()
        {
              return this.MemberwiseClone(); //浅复制
        }

        public class ListDataModel
        {
            public string UnitCode { get; set; }
            public string UnitName { get; set; }
            public string UnitRegion { get; set; }
            public string UnitAgent  { get; set; }
            public string UnitRisk  { get; set; }
            public string OpeningTime { get; set; }
            public string Account { get; set; }
            public int? AccountPool { get; set; }
            public decimal MidBond  { get; set; }
            public decimal? Unitleverage  { get; set; }
            public decimal? UnitFunds  { get; set; }
            public decimal? TotalAssets  { get; set; }
            public decimal? FreezingRatio  { get; set; }
            public decimal? ManageRatio  { get; set; }
            public decimal? CommissionRate  { get; set; }
            public decimal? UnitSoftwareRate  { get; set; }
            public int? IndividualRestrictionStock { get; set; }
            public decimal? ProportionBoardStocks  { get; set; }
            public decimal? ProportionGemStocks  { get; set; }
            public decimal? TotalProportionGem  { get; set; }
            public decimal? ProportionSmallMediumBoardStocks  { get; set; }
            public decimal? TotalProportionSmallMediumBoards  { get; set; }
            public decimal? ScienceInnovationBoardRatio { get; set; }
            public decimal? ScienceInnovationTotalRatio  { get; set; }
            public decimal? CordonRatio  { get; set; }
            public decimal? LevelingLineRatio  { get; set; }
        }
    }
}
