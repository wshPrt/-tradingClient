using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.Common.InterFace.UserManage;
using Xh.FastTrading.Wpf.Untils;
using static Xh.FastTrading.Wpf.Model.MasterAccountPositionModel;
using GalaSoft.MvvmLight.Threading;
using System.Windows.Forms;
using System.IO;

namespace Xh.FastTrading.Wpf.ViewModel.SystemManage
{
   public class AccountPositionVM:ViewModelBase
    {
        public AccountPositionVM() 
        {
            MasterAccountPosition = new MasterAccountPositionModel();
            List = new ObservableCollection<MasterAccountPositionModel>();
            InitAccountData();
            
        }

        #region DataGird
        private MasterAccountPositionModel masterAccountPosition;
        public MasterAccountPositionModel MasterAccountPosition
        {
            get { return masterAccountPosition; }
            set { masterAccountPosition = value;RaisePropertyChanged(() => MasterAccountPosition);}
        }
        #endregion

        /// <summary>
        /// DataGrid集合
        /// </summary>
        private ObservableCollection<MasterAccountPositionModel> _list;
        public ObservableCollection<MasterAccountPositionModel> List
        {
            get { return _list; }
            set { _list = value; RaisePropertyChanged(() => List);}
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

        #region 下拉框列表
        private ObservableCollection<MasterAccountPositionModel> cmbList;
        public ObservableCollection<MasterAccountPositionModel> CmbList
        {
            get { return cmbList; }
            set { cmbList = value; RaisePropertyChanged(() => CmbList); }
        }

        private MasterAccountPositionModel cmbItem;
        public MasterAccountPositionModel CmbItem
        {
            get { return cmbItem; }
            set { cmbItem = value; RaisePropertyChanged(() => CmbItem);
                if (value != null && value.Id > 0)
                {
                    InitDataGrid();
                }  
            }
        }
        #endregion

        /// <summary>
        /// 加载cmbox主账户
        /// </summary>
        private void InitAccountData() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                IAccountFilterListInterface accountFilterList = new IAccountFilterListInterface();
                var result = accountFilterList.AccountFilterList(token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MasterAccountPositionModel.Root data = JsonConvert.DeserializeObject<MasterAccountPositionModel.Root>(jsonData);
                    CmbList = new ObservableCollection<MasterAccountPositionModel>();
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        CmbList.Add(new MasterAccountPositionModel()
                        {
                            Id = data.Data[i].id,
                            Account = data.Data[i].name,
                            SecuritiesCode = data.Data[i].code,
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
        /// 主账户列表加载
        /// </summary>
        private void InitDataGrid() 
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                string token = UserToken.token;
                int requestId = CmbItem.Id;
                // int requestId = 1;
                IAccountPositionListInterface accountPostionList = new IAccountPositionListInterface();
                var result = accountPostionList.AccountPositionList(requestId, token);
                string success = result["Message"]["Message"].ToString();
                string jsonData = result["Message"].ToString();
                if (success == "成功")
                {
                    MasterAccountPositionModel.Root data = JsonConvert.DeserializeObject<MasterAccountPositionModel.Root>(jsonData);
                    for (int i = 0; i < data.Data.Count; i++)
                    {
                        List.Add(new MasterAccountPositionModel()
                        {
                            Id = data.Data[i].id,
                            Account = data.Data[i].account_name,
                            SecuritiesCode = data.Data[i].code,
                            SecuritiesName = data.Data[i].name,
                            AccountNumber = data.Data[i].count,
                            SystemNumber = data.Data[i].count_in,
                            DifferenceNumber = data.Data[i].id
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


        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ExportExcel()
        {
            if (List == null || List.Count == 0)
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
                    row1.CreateCell(0).SetCellValue("主账号");
                    row1.CreateCell(1).SetCellValue("证券代码");
                    row1.CreateCell(2).SetCellValue("证券名称");
                    row1.CreateCell(3).SetCellValue("主账号数量");
                    row1.CreateCell(4).SetCellValue("系统数量");
                    row1.CreateCell(5).SetCellValue("差额数量");
       
                    //第四步：for循环给sheet的每行添加数据
                    for (int i = 0; i < List.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(List[i].Account);
                        row.CreateCell(1).SetCellValue(List[i].SecuritiesCode);
                        row.CreateCell(2).SetCellValue(List[i].SecuritiesName.ToString());
                        row.CreateCell(3).SetCellValue(List[i].AccountNumber.ToString());
                        row.CreateCell(4).SetCellValue(List[i].SystemNumber.ToString());
                        row.CreateCell(5).SetCellValue(List[i].DifferenceNumber.ToString());
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
                        MessageDialogManager.ShowDialogAsync("主账户持仓导出成功!");
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
    }

}
