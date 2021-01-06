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
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.InterFace.Query;
using Xh.FastTrading.Wpf.Untils;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Query
{
   public class CapitalFlowVM:ViewModelBase
    {
        public CapitalFlowVM() 
        {
            QueryCapitalFlow = new QueryCapitalFlowModel();
            List = new ObservableCollection<QueryCapitalFlowModel>();
            ValidateUI = new QueryCapitalFlowModel();
            InitUnitData();
        }
        /// <summary>
        /// 验证用户界面
        /// </summary>
        private QueryCapitalFlowModel validateUI;
        public QueryCapitalFlowModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #region DataGrid
        private QueryCapitalFlowModel queryCapitalFlow;
        public QueryCapitalFlowModel QueryCapitalFlow
        {
            get { return queryCapitalFlow; }
            set { queryCapitalFlow = value; RaisePropertyChanged(() => QueryCapitalFlow); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<QueryCapitalFlowModel> _list;
        public ObservableCollection<QueryCapitalFlowModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        //选中行
        private QueryCapitalFlowModel _selectedRow;
        public QueryCapitalFlowModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        private QueryCapitalFlowModel cmbItem;
        public QueryCapitalFlowModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                    List.Clear();
                    InitDataGrid();
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<QueryCapitalFlowModel> cmbList;
        public ObservableCollection<QueryCapitalFlowModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }
        #endregion

        #region 指令
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
                return refreshCommand; }
            set { refreshCommand = value; }
        }

        /// <summary>
        /// 当月
        /// </summary>
        private RelayCommand whenMonthCommand;
        public RelayCommand WhenMonthCommand
        {
            get
            {
                if (whenMonthCommand == null)
                {
                    whenMonthCommand = new RelayCommand(() => WhenMonth());
                }
                return whenMonthCommand;
            }
            set { whenMonthCommand = value; }
        }

        /// <summary>
        /// 当日
        /// </summary>
        private RelayCommand whenDayCommand;
        public RelayCommand WhenDayCommand
        {
            get
            {
                if (whenDayCommand == null)
                {
                    whenDayCommand = new RelayCommand(() => WhenDay());
                }
                return whenDayCommand;
            }
            set { whenDayCommand = value; }
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
        /// 加载查询资金流水数据
        /// </summary>
        DispatcherTimer dispatcherTimer = null;
        private void InitDataGrid()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
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
                      string fromTime = ValidateUI.FromTime;
                      string toTime = ValidateUI.ToTime;
                      int type = cmbItem.UnitId;
                      ICapitalFlowInterface capitalFlow = new ICapitalFlowInterface();
                      var result = capitalFlow.CapitalFlow(type, fromTime, toTime, type, token);
                      string success = result["Message"]["Message"].ToString();
                      string jsonData = result["Message"].ToString();
                      if (success == "成功")
                      {
                          QueryCapitalFlowModel.Root data = JsonConvert.DeserializeObject<QueryCapitalFlowModel.Root>(jsonData);
                          for (int i = 0; i < data.Data.Count; i++)
                          {
                              QueryCapitalFlowModel model = new QueryCapitalFlowModel()
                              {
                                  UnitId = data.Data[i].unit_id,
                                  HappenDateTime = data.Data[i].time_dt.ToString("d"),
                                  HappenTime = data.Data[i].time_dt.ToString("T"),
                                  BusinessName = data.Data[i].type,
                                  HappenAmount = data.Data[i].amount,
                                  Remarks = data.Data[i].remark
                              };

                              var obj = List.ToList().Find(target => target.UnitId.Equals(data.Data[i].unit_id));
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
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);//每隔二秒刷新
                }
                if (!dispatcherTimer.IsEnabled)
                {
                    dispatcherTimer.Start();
                }
            });
        }

        /// <summary>
        /// 加载单元combox数据
        /// </summary>
        private void InitUnitData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int userId = 0;
                IAutomaticInterface automatic = new IAutomaticInterface();
                var result = await Task.Run(() => automatic.UnitList(userId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    QueryCapitalFlowModel.UnitRoot data = JsonConvert.DeserializeObject<QueryCapitalFlowModel.UnitRoot>(jsonData);
                    CmbList = new ObservableCollection<QueryCapitalFlowModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new QueryCapitalFlowModel
                        {
                            UnitCode = data.Data[i].code,
                            UnitId = data.Data[i].id
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
        /// 当月
        /// </summary>
        private void WhenMonth()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                List.Clear();
                ValidateUI.FromTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date).ToString();
                ValidateUI.ToTime = (DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)).ToString(); ;
                InitDataGrid();
            });
        }

        /// <summary>
        /// 当日
        /// </summary>
        private void WhenDay()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                List.Clear();
                ValidateUI.FromTime = DateTime.Now.ToString();
                ValidateUI.ToTime = DateTime.Now.ToString();
                InitDataGrid();
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
                    irow.CreateCell(0).SetCellValue("发生时间");
                    irow.CreateCell(1).SetCellValue("业务名称");
                    irow.CreateCell(2).SetCellValue("发生金额");
                    irow.CreateCell(3).SetCellValue("备注");

                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].HappenTime.ToString());
                        row.CreateCell(1).SetCellValue(List[i].BusinessName);
                        row.CreateCell(2).SetCellValue(List[i].HappenAmount.ToString());
                       // row.CreateCell(3).SetCellValue(List[i].Remarks.ToString());

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
                        MessageDialogManager.ShowDialogAsync("资金流水导出成功!");
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
    }
}
