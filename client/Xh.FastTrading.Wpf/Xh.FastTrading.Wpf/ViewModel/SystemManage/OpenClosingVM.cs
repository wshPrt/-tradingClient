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
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;
using Xh.FastTrading.Wpf.Views;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
     public class OpenClosingVM:ViewModelBase
     {
        public OpenClosingVM() 
        {
            OpenClosing = new OpenClosingModel();
            List = new ObservableCollection<OpenClosingModel>();
            List.Add(openClosing);
            InitDataGrid();
        }


        #region DataGrid
        private OpenClosingModel openClosing;
        public OpenClosingModel OpenClosing
        {
            get { return openClosing; }
            set { openClosing = value; RaisePropertyChanged(() => OpenClosing); }
        }

        /// <summary>
        /// DataGrid集合
        /// </summary>
        private ObservableCollection<OpenClosingModel> _list;
        public ObservableCollection<OpenClosingModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List); }
        }

        /// <summary>
        /// 选中行
        /// </summary>
        private OpenClosingModel _selectedRow;
        public OpenClosingModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

        #region 指令
        /// <summary>
        /// 开盘
        /// </summary>
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get 
            {
                if (openCommand == null)
                {
                    openCommand = new RelayCommand(() => OpenClosingPauseResume());
                }
                return openCommand; }
            set { openCommand = value; }
        }

        /// <summary>
        /// 收盘
        /// </summary>
        private RelayCommand closingCommand;
        public RelayCommand ClosingCommand
        {
            get
            {
                if (closingCommand == null)
                {
                    closingCommand = new RelayCommand(() => OpenClosingPauseResume());
                }
                return closingCommand; }
            set { closingCommand = value; }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        private RelayCommand pauseCommand;
        public RelayCommand PauseCommand
        {
            get 
            {
                if (pauseCommand == null)
                {
                    pauseCommand = new RelayCommand(() => OpenClosingPauseResume());
                }
                return pauseCommand; }
            set { pauseCommand = value; }
        }

        /// <summary>
        /// 恢复
        /// </summary>
        private RelayCommand resumeCommand;
        public RelayCommand ResumeCommand
        {
            get 
            {
                if (resumeCommand == null)
                {
                    resumeCommand = new RelayCommand(() => OpenClosingPauseResume());
                }
                return resumeCommand; }
            set { resumeCommand = value; }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                List.Clear();
                InitDataGrid();
                }); 
        }

            #endregion
            /// <summary>
            /// 开盘收盘列表数据加载
            /// </summary>
            private void InitDataGrid()
        { 
                string token = UserToken.token;
                IOpenClosingInterface openClosing = new IOpenClosingInterface();
                var result = openClosing.OpenClosing(token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    OpenClosingModel.Root data = JsonConvert.DeserializeObject<OpenClosingModel.Root>(jsonData);
                    List = new ObservableCollection<OpenClosingModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                         List.Add(new OpenClosingModel()
                        {
                             Trading = data.Data[i].date.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                             OpenTime = data.Data[i].time_open.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                             OpenUser = data.Data[i].operator_open,
                             ClosingTime = data.Data[i].time_close,
                             ClosingUser = data.Data[i].operator_close,
                             Status = data.Data[i].state
                        });
                    }
            }
            else 
            {
                MessageDialogManager.ShowDialogAsync(success);
            }
        }

        /// <summary>
        /// 开盘收盘暂停恢复
        /// </summary>
        private void OpenClosingPauseResume() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                //if (SelectedRow == null)
                //{
                //    MessageDialogManager.ShowDialogAsync("未选中!");
                //    return;
                //}
                 if (!(SelectedRow.OpenUser == null))
                {
                    string token = UserToken.token;
                    int request = 1;
                    IOpenClosePauseResumeInterface openClosePauseResume = new IOpenClosePauseResumeInterface();
                    var result = await Task.Run(() => openClosePauseResume.OpenClosePauseResume(request, token));
                    string succes = result["Message"]["Message"].ToString();
                    if (succes == "成功")
                    {
                        Refresh();
                        MessageDialogManager.ShowDialogAsync("设置成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync(succes);
                    }
                }
            });
            }
        }

    }
