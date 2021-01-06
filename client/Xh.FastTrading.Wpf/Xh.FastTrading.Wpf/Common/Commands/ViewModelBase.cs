using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Xh.FastTrading.Wpf.Commands
{
        public class ViewModelBase : INotifyPropertyChanged
        {
            #region Window-窗口管理
            //打开窗口
            public void ShowDialog(string key, object vm)
            {
                WindowManager.ShowDialog(key, vm);
            }

            //打开窗口并设置所属窗口
            public void ShowDialog(string key, object args, Window owner)
            {
              //  WindowManager.Show(key, args, owner);
            }

            //消息提示框
            public void ShowMessage(string mes, string title = "", MessageBoxButton buttons = MessageBoxButton.OK)
            {
            System.Windows.MessageBox.Show(mes, title, buttons);
            }

            //带返回结果的消息提示框
            public bool ShowMessageWithResult(string content, string title = "", MessageBoxButton button = MessageBoxButton.OKCancel)
            {
                if (System.Windows.MessageBox.Show(content, title, button) == MessageBoxResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //文件选择对话框
            //public string ShowFileDialog(string title, string filter = "docx|*.docx", string folder = "C:\\Users\\Administrator\\Desktop")
            //{
            //    string filePath = "";
            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.Title = title;
            //    dialog.Filter = filter;
            //    dialog.InitialDirectory = folder;
            //    if (dialog.ShowDialog() == true)
            //    {
            //        filePath = dialog.FileName;
            //    }
            //    return filePath;

            //}

            //文件夹选择对话框
            public string ShowFolderDialog(string title)
            {
                string folderPath = "";
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
                dialog.ShowNewFolderButton = true;
                dialog.Description = title;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = dialog.SelectedPath;
                };

                return folderPath;
            }

            #endregion

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            protected internal virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
