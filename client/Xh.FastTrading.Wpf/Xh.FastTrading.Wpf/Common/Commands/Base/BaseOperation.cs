using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Commands.Base
{
    /// <summary>
    /// 主窗口基类
    /// </summary>
    public partial class BaseOperation<T> : ViewModelBase where T : class, new()
    {

        #region 基类属性  [搜索、功能按钮]

        private string searchText = string.Empty;
        private bool _DisplayGrid, _DisplayMetro = true;
        private ObservableCollection<T> _GridModelList;
       // private ObservableCollection<ContextMenuModel> _ContextMenuModel;
       // private ObservableCollection<ToolBarDefault<T>> _ButtonDefaults;

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value;  }
        }

        public bool DisplayGrid
        {
            get { return _DisplayGrid; }
            set { _DisplayGrid = value; }
        }

        public bool DisplayMetro
        {
            get { return _DisplayMetro; }
            set { _DisplayMetro = value; }
        }


        /// <summary>
        /// 功能集合
        /// </summary>
        //public ObservableCollection<ToolBarDefault<T>> ButtonDefaults
        //{
        //    get { return _ButtonDefaults; }
        //    set { _ButtonDefaults = value; }
        //}
        #endregion
    }

  

    /// <summary>
    /// 弹出式窗口基类-Host
    /// </summary>
    public class BaseHostDialogOperation : ViewModelBase
    {
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 窗口打开时处理的逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public virtual void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {

        }

        /// <summary>
        /// 窗口关闭前处理的逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public virtual void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;
        }
    }

    /// <summary>
    /// 弹出式窗口基类-Window
    /// </summary>
    public class BaseDialogOperation : ViewModelBase
    {
        public bool Result { get; set; }

        private RelayCommand _CancelCommand;
        private RelayCommand _SaveCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                    _CancelCommand = new RelayCommand(() => Cancel());
                return _CancelCommand;
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new RelayCommand(() => Save());
                return _SaveCommand;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            Result = false;
            Messenger.Default.Send("", "DialogClose");
        }

        /// <summary>
        /// 确定
        /// </summary>
        public void Save()
        {
            Result = true;
            Messenger.Default.Send("", "DialogClose");
        }


    }
}
