using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;


namespace GetCubeFormula
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _spider = new Spider();
            _spider.MaxDepth = 3;
            _spider.MaxConnection = 4;
            _spider.ContentsSaved += new Spider.ContentsSavedHandler(Spider_ContentsSaved);
            _spider.DownloadFinish += new Spider.DownloadFinishHandler(Spider_DownloadFinish);
            this.Closed += new EventHandler(MainWindow_Closed);
        }


        private Spider _spider = null;
        private delegate void CSHandler(string arg0, string arg1);
        private delegate void DFHandler(int arg1);

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TextUrl.Text = @"http://algdb.net/";
        }

        void Spider_DownloadFinish(int count)
        {
            DFHandler h = c =>
            {
                _spider.Abort();
                btnDownload.Enabled = true;

                btnStop.Enabled = false;
                MessageBox.Show("Finished " + c.ToString());
            };

            Invoke(h, count);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            _spider.Abort();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            _spider.RootUrl = TextUrl.Text;
            this.rtbList.Text = string.Empty;
            Thread thread = new Thread(new ParameterizedThreadStart(Download));
            thread.Start(TextPath.Text);
            btnDownload.Enabled = false;
            btnDownload.Text = "正在下载...";
            btnStop.Enabled = true;
        }

        private void Download(object param)
        {
            _spider.Download((string)param);
        }

        void Spider_ContentsSaved(string path, string url)
        {
            CSHandler h = (p, u) =>
            {
                //ListDownload.Items.Add(new { Url = u, File = p });
                rtbList.Text += u +"    "+ p+"\r\n"; 
            };
            Invoke(h, path, url);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _spider.Abort();
            btnDownload.Enabled = true;
            btnDownload.Text = "下载";
            btnStop.Enabled = false;
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fdlg = new System.Windows.Forms.FolderBrowserDialog();
            fdlg.RootFolder = Environment.SpecialFolder.Desktop;
            fdlg.Description = "Contents Root Folder";
            var result = fdlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = fdlg.SelectedPath;
                TextPath.Text = path;
            }
        }






    }
}
