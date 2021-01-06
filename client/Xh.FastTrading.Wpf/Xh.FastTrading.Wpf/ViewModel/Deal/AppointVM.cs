using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.Deal;
using Xh.FastTrading.Wpf.Common.Untils;
using Xh.FastTrading.Wpf.Untils;
using static Xh.FastTrading.Wpf.Model.DealAppointModel;
using HQ;
using System.Windows.Threading;

namespace Xh.FastTrading.Wpf.ViewModel.Deal
{
   public class AppointVM:ViewModelBase
    {
        public AppointVM() 
        {
            Appoint = new DealAppointModel();
            List = new ObservableCollection<DealAppointModel>();
            InitUnitData();
            ValidateUI = new Model.DealAppointModel();
            BuySellType = new ObservableCollection<BuySellTypModel>()
            {
                new BuySellTypModel(){Id=0,Name="买入"},
                new BuySellTypModel(){Id=1,Name="卖出"}
            };
        }

        #region 买卖方向cmbox
        private ObservableCollection<DealAppointModel.BuySellTypModel> _buySellType;
        public ObservableCollection<DealAppointModel.BuySellTypModel> BuySellType
        {
            get { return _buySellType; }
            set { _buySellType = value; }
        }

        private DealAppointModel.BuySellTypModel _sbuySellType;
        public DealAppointModel.BuySellTypModel SbuySellType
        {
            get { return _sbuySellType; }
            set { _sbuySellType = value; }
        }
        #endregion

        #region DataGrid
        private DealAppointModel appoint;
        public DealAppointModel Appoint
        {
            get { return appoint; }
            set { appoint = value; RaisePropertyChanged(() => Appoint); }
        }

        /// <summary>
        /// DataGrid 集合
        /// </summary>
        private ObservableCollection<DealAppointModel> _list;
        public ObservableCollection<DealAppointModel> List
        {
            get { return _list; }
            set { _list = value; }
        }

        //选中行
        private DealAppointModel _selectedRow;
        public DealAppointModel SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; RaisePropertyChanged(() => SelectedRow); }
        }

        /// <summary>
        /// 验证用户界面
        /// </summary>
        private DealAppointModel validateUI;
        public DealAppointModel ValidateUI
        {
            get { return validateUI; }
            set { validateUI = value; RaisePropertyChanged(() => ValidateUI); }
        }
        #endregion

        #region 单元下拉框
        /// <summary>
        /// 单元下拉框选中信息
        /// </summary>
        private DealAppointModel cmbItem;
        public DealAppointModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.UnitId > 0)
                {
                        List.Clear();
                        AppointData();
                        AppointListData();
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        private ObservableCollection<DealAppointModel> cmbList;
        public ObservableCollection<DealAppointModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList);
            }
        }
        #endregion

        #region 指定账户下拉框
        /// <summary>
        /// 指定账户下拉框选中信息
        /// </summary>
        private DealAppointModel cmbItemAppoint;
        public DealAppointModel CmbItemAppoint
        {
            get { return cmbItemAppoint; }
            set { cmbItemAppoint = value; RaisePropertyChanged(() => CmbItemAppoint); }
        }

        /// <summary>
        /// 指定账户下拉框列表
        /// </summary>
        private ObservableCollection<DealAppointModel> cmbListAppoint;
        public ObservableCollection<DealAppointModel> CmbListAppoint
        {
            get { return cmbListAppoint; }
            set { cmbListAppoint = value; RaisePropertyChanged(() => CmbListAppoint); }
        }
        #endregion

        #region 指令
        /// <summary>
        /// 买入
        /// </summary>
        private RelayCommand buyCommand;
        public RelayCommand BuyCommand 
        {
            get
            {
                if (buyCommand == null)
                {
                    buyCommand = new RelayCommand(() => BUY());
                }
                return buyCommand; }
            set { buyCommand = value; }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private RelayCommand excelCommand;
        public RelayCommand ExcelCommand  
        {
            get 
            {
                if ( excelCommand == null)
                {
                    excelCommand = new RelayCommand(() => ExportExcel());
                }
                return excelCommand; }
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

        /// <summary>
        /// 买卖方向
        /// </summary>
        private RelayCommand directionCommand;
        public RelayCommand DirectionCommand
        {
            get {
                if (directionCommand == null)
                {
                    directionCommand = new RelayCommand(() => Direction());
                }
                return directionCommand; }
            set { directionCommand = value; }
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
                int userId = 0;
                IDealAppointInterface appoint = new IDealAppointInterface();
                var result = await Task.Run(() => appoint.UnitList(userId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    DealAppointModel.UnitRoot data = JsonConvert.DeserializeObject<DealAppointModel.UnitRoot>(jsonData);
                    CmbList = new ObservableCollection<DealAppointModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new DealAppointModel
                        {
                            UnitCode = data.Data[i].code,
                            UnitId = data.Data[i].id,
                            UnitName = data.Data[i].name
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
        /// 加载指定账户combox数据
        /// </summary>
        private void AppointData()
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                string token = UserToken.token;
                int unitId = CmbItem.UnitId;
                IDealAppointAccountInterface dealAppointAccount = new IDealAppointAccountInterface();
                var result = await Task.Run(() => dealAppointAccount.AppointAccountList(unitId, token));
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    DealAppointModel.AppointRoot data = JsonConvert.DeserializeObject<DealAppointModel.AppointRoot>(jsonData);
                    CmbListAppoint = new ObservableCollection<DealAppointModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbListAppoint.Add(new DealAppointModel
                        {
                            AppointCode= data.Data[i].code,
                            AppointId = data.Data[i].id,
                            AppointName = data.Data[i].name
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
        /// 买入 下单
        /// </summary>
        private void BUY() 
        {
            if (string.IsNullOrWhiteSpace(ValidateUI.PositionSecurities))
            {
                MessageDialogManager.ShowDialogAsync("持仓证券为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace(SbuySellType.Id.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("买卖方向位未选择!");
                return;
            }
            //if (string.IsNullOrWhiteSpace(CmbItemAppoint.UnitId.ToString()))
            //{
            //    MessageDialogManager.ShowDialogAsync("指定账户未选择!");
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(ValidateUI.SingleTicketMarketValue.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("委托价格为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ValidateUI.SingleTicketAmount.ToString()))
            {
                MessageDialogManager.ShowDialogAsync("委托数量为空!");
                return;
            }
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                //0表示买;1表示卖
                int type = SbuySellType.Id;
                IDealAppointInterface dealAppoint = new IDealAppointInterface();
                var result = await Task.Run(() => dealAppoint.DealAppoint(CmbItem.UnitId, CmbItemAppoint.AppointId,
                    ValidateUI.PositionSecurities, ValidateUI.SingleTicketMarketValue,
                    type, ValidateUI.SingleTicketAmount, token));
                string success = result["Message"]["Message"].ToString();
                if (success == "成功")
                {
                    Refresh();
                    MessageDialogManager.ShowDialogAsync("指定账户买入成功!");
                    ValidateUI.PositionSecurities = "";
                    ValidateUI.SingleTicketMarketValue = null;
                    ValidateUI.SingleTicketAmount = null;
                    return;
                }
                else 
                {
                    MessageDialogManager.ShowDialogAsync(success);
                }
            });
        }
        private BuyHQModel buyHQ;
        public BuyHQModel BuyHQ
        {
            get { return buyHQ; }
            set { buyHQ = value; RaisePropertyChanged(() => BuyHQ); }
        }

        DispatcherTimer dispatcherTimer = null;
        /// <summary>
        /// 加载指定交易列表
        /// </summary>
        private void AppointListData()
        {
            //定时查询 - 定时器
            if(dispatcherTimer is null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += (s, e) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                    {
                        string token = UserToken.token;
                        int unitId = CmbItem.UnitId;
                        //获取账户代码
                        IUnitPositionInterface unitPosition = new IUnitPositionInterface();
                        var resultAccount = unitPosition.AccountCode(unitId, token);
                        string success = resultAccount["Message"]["Message"].ToString();
                        string jsonDataAccount = resultAccount["Message"].ToString();
                        if (success == "成功")
                        {
                            DealAppointModel.AccountRoot data = JsonConvert.DeserializeObject<DealAppointModel.AccountRoot>(jsonDataAccount);
                            for (int i = 0; i < data.Data.Count; i++)
                            {
                                if (data.Data[i].code != null)
                                {
                                    HQItem hq = HQService.Get(data.Data[i].code);
                                    if (hq != null)
                                    {
                                        BuyHQ = new BuyHQModel()
                                        {
                                            Last = hq.Last
                                        };
                                    }
                                }
                                DealAppointModel model = new DealAppointModel()
                                {
                                    SecuritiesCode = data.Data[i].code,
                                    AccountCode = data.Data[i].account_name,
                                    SingleTicketAmount = data.Data[i].count,
                                    SingleTicketBuy = data.Data[i].count_today_buy,
                                    SingleTicketSell = data.Data[i].count_today_sell,
                                    SingleTicketMarketable = data.Data[i].count_sellable,
                                    //当前价*数量
                                    SingleTicketMarketValue = BuyHQ.Last * data.Data[i].count
                                };
                                 
                                var obj = List.ToList().Find(target => target.SecuritiesCode.Equals(data.Data[i].code));
                                if (obj != null)
                                {
                                    obj = model;
                                }
                                else
                                    List.Add(model);
                               
                            }
                        }
                        else
                        {
                            MessageDialogManager.ShowDialogAsync(success);
                        }
                    });
                };
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);//每隔二秒刷新
            }

            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
        }

        /// <summary>
        /// 买卖
        /// </summary>
        private void Direction() 
        {
           ValidateUI.BtnName = SbuySellType.Name;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                if (List == null)
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
                        NPOI.SS.UserModel.IRow irow  = sheet.CreateRow(0);//创建行
                        irow.CreateCell(0).SetCellValue("账号代码");
                        irow.CreateCell(1).SetCellValue("账号名称");
                        irow.CreateCell(2).SetCellValue("单票数量");
                        irow.CreateCell(3).SetCellValue("单票今买");
                        irow.CreateCell(4).SetCellValue("单票今卖");
                        irow.CreateCell(5).SetCellValue("单票可卖");
                        irow.CreateCell(6).SetCellValue("单票市值");
                        //第四步：for循环给sheet的每行添加数据
                        for (int i = 0; i < List.Count; i++)
                        {
                            NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                            row.CreateCell(0).SetCellValue(List[i].AccountCode);
                            row.CreateCell(1).SetCellValue(List[i].AccountName);
                            row.CreateCell(2).SetCellValue(int.Parse(List[i].SingleTicketAmount.ToString())); 
                            row.CreateCell(3).SetCellValue(double.Parse(List[i].SingleTicketBuy.ToString()));
                            row.CreateCell(4).SetCellValue(double.Parse(List[i].SingleTicketSell.ToString()));
                            row.CreateCell(5).SetCellValue(double.Parse(List[i].SingleTicketMarketable.ToString()));
                            row.CreateCell(6).SetCellValue(double.Parse(List[i].SingleTicketMarketValue.ToString()));
       
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
                            FileStream BookStream = new FileStream(saveFileDialog.FileName.ToString() + ".xls", FileMode.Create, FileAccess.Write);//定义文件流
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
                }
            });
        }

        /// <summary>
        /// 刷新
        /// </summary>
       private void Refresh() 
       {
            DispatcherHelper.CheckBeginInvokeOnUI( async () =>
            {
                List.Clear();
                AppointListData();
            });
        }
    }
}
