using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpgrade
{
    public class FtpHelper
    {
        public EventHandler<int> DownloadSizeChanged;

        private int _downloadSize;
        public int DownloadSize
        {
            get
            {
                return _downloadSize;
            }
            set
            {
                _downloadSize = value;
                DownloadSizeChanged.Invoke(null, value);
            }
        }

        // FTP 下载
        public bool Download(string server, string savePath, string fileName, string ftpUserID, string ftpPassword)
        {
            string ftpServerPath = server + fileName;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            FtpWebRequest reqFTP;
            try
            {
                Directory.CreateDirectory(savePath + fileName.Substring(0, fileName.LastIndexOf('/')).Replace("/", "\\"));
                outputStream = new FileStream(savePath + fileName.Replace("/", "\\"), FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerPath));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Timeout = 3000;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    DownloadSize += readCount;
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch(Exception ex)
            {
                if (outputStream != null)
                {
                    outputStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                return false;
            }
        }
    }
}
