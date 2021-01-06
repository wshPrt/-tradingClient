using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Xh.FastTrading.Wpf.Common.Converters.ValueConverter;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
   public  class UserManageUnitFunctionPowerVM
    {
        public UserManageUnitFunctionPowerVM() 
        {
            //InitData();
        }
        #region 旧的
        //private authority_modules _authority_modules;
        //public authority_modules Authority_modules
        //{
        //    get
        //    { return _authority_modules; }
        //    set
        //    {
        //        _authority_modules = value;
        //        RaisePropertyChanged("Authority_modules");
        //    }

        //}

        //public ICommand Authority_modulesCommand { get { return new RelayCommand(Authority_modulesOK, CanPreess); } }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private void InitData()
        //{
        //    Authority_modules = new authority_modules();
        //    //传入设定的初始值，比如开始设置有三种状态，那么界面就勾选了这三个状态对应的CheckBox，界面在一开始已经给出了
        //    Authority_modules.State = TypeEnum.交易 | TypeEnum.交易 | TypeEnum.交易;
        //    Authority_modules.State = TypeEnum.PC端 | TypeEnum.PC端 | TypeEnum.PC端;
        //}
        //private bool CanPreess()
        //{
        //    return true;
        //}
        //private void Authority_modulesOK()
        //{
        //    //点击确定后，界面勾选的返回的当前编辑的状态
        //    TypeEnum getValue = Authority_modules.State;

        //}
        #endregion





    }
}
