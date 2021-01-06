using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Forms;
using Xh.FastTrading.Wpf.Common.Commands;
using Xh.FastTrading.Wpf.Common.Converters;
using Xh.FastTrading.Wpf.ViewModel.Deal;
using Xh.FastTrading.Wpf.ViewModel.Query;
using Xh.FastTrading.Wpf.ViewModel.SystemManage;
using Xh.FastTrading.Wpf.ViewModel.UnitManage;
using Xh.FastTrading.Wpf.Views.UnitManage;

namespace Xh.FastTrading.Wpf.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<SignInViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserManageDetailVM>();
            SimpleIoc.Default.Register<ValidateExceptionVM>();
            SimpleIoc.Default.Register<UserManageUnitFunctionPowerVM>();
            SimpleIoc.Default.Register<AccountDetailVM>();
            SimpleIoc.Default.Register<AccountDetailPoolVM>();
            SimpleIoc.Default.Register<AccountPositionVM>();
            SimpleIoc.Default.Register<OpenClosingVM>();
            SimpleIoc.Default.Register<PositionTransferVM>();
            SimpleIoc.Default.Register<UnitListVM>();
            SimpleIoc.Default.Register<EntrustSummaryVM>();
            SimpleIoc.Default.Register<SysWithinDealSummaryVM>();
            SimpleIoc.Default.Register<SysOutDealSummaryVM>();
            SimpleIoc.Default.Register<PositionSummaryVM>();
            SimpleIoc.Default.Register<CapitalFlowVM>();
            SimpleIoc.Default.Register<DealRecordVM>();
            SimpleIoc.Default.Register<EntrustRecordVM>();
            SimpleIoc.Default.Register<AsetsVM>();
            SimpleIoc.Default.Register<BuyingVM>();
            SimpleIoc.Default.Register<SellVM>();
            SimpleIoc.Default.Register<AutoMaticVM>();
            SimpleIoc.Default.Register<AppointVM>();
            SimpleIoc.Default.Register<CancelOrderVM>();

            #region Validate
            //SimpleIoc.Default.Register<BindDataAnnotationsVM>();
            //SimpleIoc.Default.Register<PackagedValidateVM>();
            //SimpleIoc.Default.Register<ValidateModelBase>();
            #endregion
        }
        public T GetViewModel<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public SignInViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SignInViewModel>();
            }
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public UserManageDetailVM UserManage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserManageDetailVM>();
            }
        }
        public ValidateExceptionVM ValidateException
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ValidateExceptionVM>();
            }
        }
        public UserManageUnitFunctionPowerVM FunctionPower
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<UserManageUnitFunctionPowerVM>();
            }
        }
        public AccountDetailVM Account 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AccountDetailVM>();
            }
        }
        public AccountDetailPoolVM AccountPool 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AccountDetailPoolVM>();
            }
        }
        public AccountPositionVM AccountPosition 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AccountPositionVM>();
            }
        }
        public OpenClosingVM OpenClosing 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<OpenClosingVM>();
            }
        }
        public PositionTransferVM PositionTransfer 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<PositionTransferVM>();
            }
        }
        public UnitListVM UnitManage 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<UnitListVM>();
            }
        }
        public EntrustSummaryVM EntrustSummary 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<EntrustSummaryVM>();
            }
        }
        public SysWithinDealSummaryVM WithinDealSummary  
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<SysWithinDealSummaryVM>();
            }
        }
        public SysOutDealSummaryVM OutDealSummary 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<SysOutDealSummaryVM>();
            }
        }
        public PositionSummaryVM PositionSummary 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<PositionSummaryVM>();
            }
        }
        public CapitalFlowVM CapitalFlow 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<CapitalFlowVM>();
            }
        }
        public DealRecordVM DealRecord 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<DealRecordVM>();
            }
        }
        public EntrustRecordVM EntrustRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EntrustRecordVM>();
            }
        }
        public AsetsVM Asets 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AsetsVM>();
            }
        }
        public BuyingVM Buying 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<BuyingVM>();
            }
        }
        public SellVM Sell
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SellVM>();
            }
        }
        public AutoMaticVM autoMatic 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AutoMaticVM>();
            }
        }
        public AppointVM Appoint 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<AppointVM>();
            }
        }
        public CancelOrderVM CancelOrder
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CancelOrderVM>();
            }
        }
        //public BindDataAnnotationsVM BindDataAnnotations 
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<BindDataAnnotationsVM>();
        //    }
        //}
        //public PackagedValidateVM PackagedValidate 
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<PackagedValidateVM>();
        //    }
        //}
        //public ValidateModelBase ValidateModelBase 
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ValidateModelBase>();
        //    }
        //}
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}