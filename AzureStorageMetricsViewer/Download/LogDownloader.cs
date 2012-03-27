using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using System.Threading;
using System.ComponentModel;

namespace AzureStorageMetricsViewer.Download
{
    public class LogDownloader
    {
        public void func()
        {
            BackgroundWorker bw=new BackgroundWorker();
            bw.DoWork+=new DoWorkEventHandler(bw_DoWork);
            //bw.dow
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.r
        }

        public event EventHandler<DownloadEventArgs> DownloadProgressChanged;
        public event EventHandler<DownloadEventArgs> DownloadCompleted;
        public void DownloadAsync(List<CloudBlob> blobs, string downloadfolder)
        {
            ThreadPool.QueueUserWorkItem(
                new WaitCallback((o) =>
                    {
                        try
                        {
                            for(int i=0;i<blobs.Count;i++)
                            {
                                var blob = blobs[i];
                                var filename = blob.Name.Replace('/', '_');
                                blob.DownloadToFile(downloadfolder + "/" + filename);
                                OnProgressChanged(i * 100 / blobs.Count);
                            }

                            OnDownloadCompleted(false);
                        }
                        catch
                        {
                            OnDownloadCompleted( true);
                        }
                    }), null);
        }

        void OnDownloadCompleted(bool haserror)
        {
            if (DownloadCompleted != null)
                DownloadCompleted.BeginInvoke(this, new DownloadEventArgs
                {
                    HasError = haserror
                },null,null);
        }

        void OnProgressChanged(int percent)
        {
            if (DownloadProgressChanged != null)
                DownloadProgressChanged.Invoke(this, new DownloadEventArgs
                {
                    Percentage=percent
                });
        }

        //void Download(object list)
        //{
        //    var blist = list as List<CloudBlob>;
        //    foreach(
        //}
    }


    public class DownloadEventArgs : EventArgs
    {
        public int Percentage { set; get; }
        public bool HasError { set; get; }
    }
}
