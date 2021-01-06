using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Deal
{
   public class CancelOrderVM:ViewModelBase
    {
        public CancelOrderVM() 
        {
            CancelOrder = new DealCancelOrderModel();
            ValidateUI = new DealCancelOrderModel();
            List = new ObservableCollection<DealCancelOrderModel>();
            InitUnitData();
        }
       
        #region DataGrid
        private DealCancelOrderModel cancelOrder;
        public DealCancelOrderModel CancelOrder
        {
            get { return cancelOrder; }
            set { cancelOrder = value; RaisePropertyChanged(() => CancelOrder); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<DealCancelOrderModel> _list;
        public ObservableCollection<DealCancelOrderModel> List
        {
            get { return _list; }
            set { _list = value;RaisePropertyChanged(() => List);}
        }

        //选中行
        private DealCancelOrderModel _selectedRow;
        public DealCancelOrderModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private DealCancelOrderModel validateUI;
        public DealCancelOrderModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #endregion

        #region 选择
        /// <summary>
        /// 选中全部
        /// </summary>
        private RelayCommand selectAllCommand;
        public RelayCommand SelectAllCommand
        {
            get
            {
                return selectAllCommand ?? (selectAllCommand = new RelayCommand(ExecuteSelectAllCommand, CanExecuteSelectAllCommand));
            }
            set { selectAllCommand = value; }
        }
        private void ExecuteSelectAllCommand()
        {
            if (List.Count < 1) return;
           // List.ToList().FindAll(p => p.IsSelected = true);
        }
        private bool CanExecuteSelectAllCommand()
        {
            if (List != null)
            {
                return List.Count > 0;
            }
            else
                return false;
        }

        /// <summary>
        /// 取消全部选中
        /// </summary>
        private RelayCommand unSelectAllCommand;
        public RelayCommand UnSelectAllCommand
        {
            get { return unSelectAllCommand ?? (unSelectAllCommand = new RelayCommand(ExecuteUnSelectAllCommand, CanExecuteUnSelectAllCommand)); }
            set { unSelectAllCommand = value; }
        }
        private void ExecuteUnSelectAllCommand()
        {
            if (List.Count < 1)
                return;
            if (List.ToList().FindAll(p => p.IsSelected == false).Count != 0)
               SelectedRow.IsSelected = false;
           // else
               // List.ToList().FindAll(p => p.IsSelected = false);
        }
        private bool CanExecuteUnSelectAllCommand()
        {
            if (List != null)
            {
                return List.Count > 0;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private DealCancelOrderModel cmbItem;
        public DealCancelOrderModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                    List.Clear();
                    InitCancelOrderData();
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<DealCancelOrderModel> cmbList;
        public ObservableCollection<DealCancelOrderModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList);}
        }
        #endregion

        #region 指定
        /// <summary>
        /// 批量撤单
        /// </summary>
        private RelayCommand batchCancelCommand;
        public RelayCommand BatchCancelCommand
        {
            get 
            {
                if (batchCancelCommand == null)
                {
                    batchCancelCommand = new RelayCommand(() => BatchCancel());
                }
                return batchCancelCommand; }
            set { batchCancelCommand = value; }
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

        #endregion
        /// <summary>
        /// 加载单元combox数据
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int unitId = 0;
                IDealCancelOrderInterface appoint = new IDealCancelOrderInterface();
                 var result = await Task.Run(() => appoint.UnitList(unitId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    DealCancelOrderModel.UnitRoot data = JsonConvert.DeserializeObject<DealCancelOrderModel.UnitRoot>(jsonData);
                    CmbList = new ObservableCollection<DealCancelOrderModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new DealCancelOrderModel
                        {
                            UnitCode = data.Data[i].Code,
                            UnitId = data.Data[i].Id,
                            UnitName = data.Data[i].Name
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
        /// 可撤委托列表
        /// </summary>
        DispatcherTimer dispatcherTimer = null;
        private void InitCancelOrderData() 
        {
            //定时查询 - 定时器
            if (dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
             {
                 string token = UserToken.token;
                 int UnitId = CmbItem.UnitId;
                 IDealCancelOrderInterface cancelOrder = new IDealCancelOrderInterface();
                 var result = await Task.Run(() => cancelOrder.DealCancelOrder(UnitId, token));
                 string success = result["Message"]["Message"].ToString();
                 string jsonData = result["Message"].ToString();
                 if (success == "成功")
                 {
                     DealCancelOrderModel.EntrustRoot data = JsonConvert.DeserializeObject<DealCancelOrderModel.EntrustRoot>(jsonData);
                     for (int i = 0; i < data.Data.Count; i++)
                     {
                         DealCancelOrderModel model = new DealCancelOrderModel()
                         {
                             EntrustDateTime = data.Data[i].time.ToString("yyyy/MM/dd"),
                             EntrustTime = data.Data[i].time.ToString("T"),
                             SecuritiesCode = data.Data[i].code,
                             SecuritiesName = data.Data[i].name,
                             EntrustType = data.Data[i].type,
                             EntrustPrice = data.Data[i].price,
                             EntrustAmount = data.Data[i].count,
                             DealPrice = data.Data[i].price,
                             DealAmount = data.Data[i].trade_count,
                             CancelOrderAmount = data.Data[i].cancel_count,
                             StatusExplain = data.Data[i].state,
                             EntrustNumber = Int64.Parse(data.Data[i].trade_no),
                             Account = data.Data[i].account_name,
                             Remarks = data.Data[i].remark,
                             PlaceOrderUser = data.Data[i].user_name
                         };
       
                         var obj = List.ToList().Find(target => target.SecuritiesCode.Equals(data.Data[i].code));
                         if (obj != null)
                         {
                             obj = model;
                         }
                         else
                             List.Add(model);
                     }
                     return;
                 }
                 else
                 {
                     MessageDialogManager.ShowDialogAsync(success);
                 }
             });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            }
            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }


        /// <summary>
        /// 批量撤单
        /// </summary>
        private void BatchCancel() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                foreach (var v in List)
                {
                    if (v.ChbChoose == false) 
                    {
                        MessageDialogManager.ShowDialogAsync("未勾选选撤单记录！");
                        return;
                    }
                    if (v.ChbChoose == true)
                    {
                        List<Int64> trade_nos = new List<Int64>();
                        for (int i = 0; i < List.Count; i++)
                        {
                            trade_nos.Add(List[i].EntrustNumber);
                        }
                        string token = UserToken.token;

                        IDealCancelOrderInterface cancelOrder = new IDealCancelOrderInterface();
                        var result = await Task.Run(() => cancelOrder.BatchCancelOrder(CmbItem.UnitId, trade_nos, token));
                        string success = result["Message"]["Message"].ToString();
                        if (success == "成功")
                        {
                            MessageDialogManager.ShowDialogAsync("批量撤单成功!");
                            return;
                        }
                        else
                        {
                            MessageDialogManager.ShowDialogAsync(success);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel()
        {
            if (List == null)
            {
                MessageDialogManager.ShowDialogAsync("没记录无法导出!");
                return;
            }
            if (List.Count > 0)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                {
                    //Excel表格的创建步骤
                    //第一步：创建Excel对象
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    //第二步：创建Excel对象的工作簿
                    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();
                    //第三步：Excel表头设置
                    //给sheet添加第一行的头部标题
                    NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);//创建行
                    row1.CreateCell(0).SetCellValue("委托时间");
                    row1.CreateCell(1).SetCellValue("证券代码");
                    row1.CreateCell(2).SetCellValue("证券名称"); 
                    row1.CreateCell(3).SetCellValue("委托类型");
                    row1.CreateCell(4).SetCellValue("委托价格");
                    row1.CreateCell(5).SetCellValue("委托数量");
                    row1.CreateCell(6).SetCellValue("成交均价");
                    row1.CreateCell(7).SetCellValue("成交数量");
                    row1.CreateCell(8).SetCellValue("撤单数量");
                    row1.CreateCell(9).SetCellValue("状态说明");
                    row1.CreateCell(10).SetCellValue("委托编号"); 
                    row1.CreateCell(11).SetCellValue("主账户");
                    row1.CreateCell(12).SetCellValue("备注");
                    row1.CreateCell(13).SetCellValue("下单用户");

                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].EntrustTime);
                        row.CreateCell(1).SetCellValue(List[i].SecuritiesCode);
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesName);
                        row.CreateCell(3).SetCellValue(List[i].EntrustTypeStr);
                        row.CreateCell(4).SetCellValue(double.Parse(List[i].EntrustPrice.ToString()));
                        row.CreateCell(5).SetCellValue(double.Parse(List[i].EntrustAmount.ToString()));
                        row.CreateCell(6).SetCellValue(double.Parse(List[i].DealPrice.ToString()));
                        row.CreateCell(7).SetCellValue(double.Parse(List[i].DealAmount.ToString()));
                        row.CreateCell(8).SetCellValue(double.Parse(List[i].CancelOrderAmount.ToString()));
                        row.CreateCell(9).SetCellValue(List[i].StatusExplain.ToString());
                        row.CreateCell(10).SetCellValue(List[i].EntrustNumber);
                        //row.CreateCell(11).SetCellValue(List[i].Account.ToString()??"");
                       // row.CreateCell(12).SetCellValue(List[i].Remarks.ToString());
                        row.CreateCell(13).SetCellValue(List[i].PlaceOrderUser.ToString());

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
                        MessageDialogManager.ShowDialogAsync("指定交易记录导出成功!");
                        return;
                    }
                    else
                    {
                        MessageDialogManager.ShowDialogAsync("导出保存失败！");
                        return;
                    }

                });
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void Refresh() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
           {
               List.Clear();
               InitCancelOrderData();
           });
        }

       
    }
}
