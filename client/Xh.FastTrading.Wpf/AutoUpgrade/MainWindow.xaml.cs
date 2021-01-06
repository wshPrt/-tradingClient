using AutoUpgrade.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using Version = AutoUpgrade.Model.Version;

namespace AutoUpgrade
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count;
        private int size;
        private int last_version;
        private string last_version_no;
        private FtpHelper ftpHelper;

        public MainWindow()
        {
            InitializeComponent();
            ftpHelper = new FtpHelper();
            ftpHelper.DownloadSizeChanged += DownloadSizeChanged;

            txtCurrent.Text = Program.VERSION_NO;
            List<VersionFiles> files = GetVersion();

            if (files != null)
            {
                Download(files);
            }
            else
            {
                RefreshInfo("已经是最新版本！");
            }
        }

        private async void Download(List<VersionFiles> files)
        {
            await Task.Run(() =>
            {
                if (Directory.Exists(Environment.CurrentDirectory + "\\upgrade"))
                    Directory.Delete(Environment.CurrentDirectory + "\\upgrade", true);

                for (int i = 0; i < files.Count; i++)
                {
                    bool result = ftpHelper.Download(Program.URL_UPGRADE, Environment.CurrentDirectory + "\\upgrade", files[i].name, Program.FTP_USER, Program.FTP_PASSWORD);
                    if (result)
                    {
                        RefreshCount(i + 1);
                    }
                    else
                    {
                        RefreshInfo("抱歉，升级失败，请稍后重试！");
                        return;
                    }
                }

                string[] fnames = Directory.GetFiles(Environment.CurrentDirectory + "\\upgrade");
                for (int i = 0; i < fnames.Length; i++)
                {
                    File.Copy(fnames[i], fnames[i].Replace("\\upgrade\\", "\\"), true);
                }
                SaveLoginInfo();
                RefreshInfo("升级成功！");
            });
        }

        /// <summary>
        /// 保存版本信息
        /// </summary>
        private void SaveLoginInfo()
        {
            string cfgINI = Environment.CurrentDirectory + "\\config\\version.ini";
            IniFiles ini = new IniFiles(cfgINI);
            ini.IniWriteValue("Version", "Version", last_version.ToString());
            ini.IniWriteValue("Version", "Version_NO", last_version_no);
        }

        private List<VersionFiles> GetVersion()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Headers.Add("platform", "1");
            string url = Program.URL + "/Service/Utility.svc/VersionFiles";
            string result = webClient.UploadString(url, Program.VERSION);
            var version = jsSerializer.Deserialize<Result<Version>>(result);
            last_version = version.Data.version;
            txtLast.Text = last_version_no = version.Data.version_no;
            count = version.Data.total_file_count;
            psbProgress.Maximum = size = version.Data.total_size / 1024;
            RefreshCount(0);
            RefreshSize(0);
            return version.Data.files;
        }

        private void RefreshCount(int _count)
        {
            Dispatcher.Invoke(() =>
            {
                txtCount.Text = _count + " / " + count;
            });
        }

        private void RefreshSize(int _size)
        {
            Dispatcher.Invoke(() =>
            {
                _size /= 1024;
                txtSize.Text = _size + "KB / " + size + "KB";
                psbProgress.Value = _size;
                RefreshInfo("已完成：" + (_size / psbProgress.Maximum).ToString("P0"));
            });
        }

        private void RefreshInfo(string _info)
        {
            Dispatcher.Invoke(() =>
            {
                txtInfo.Text = _info;
            });
        }

        private void DownloadSizeChanged(object sender, int downloadSize)
        {
            RefreshSize(downloadSize);
        }
    }
}
