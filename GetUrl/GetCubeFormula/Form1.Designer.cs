namespace GetCubeFormula
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextUrl = new System.Windows.Forms.TextBox();
            this.TextPath = new System.Windows.Forms.TextBox();
            this.FolderButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.rtbList = new System.Windows.Forms.RichTextBox();
            this.labLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PATH：";
            // 
            // TextUrl
            // 
            this.TextUrl.Location = new System.Drawing.Point(55, 13);
            this.TextUrl.Name = "TextUrl";
            this.TextUrl.Size = new System.Drawing.Size(701, 21);
            this.TextUrl.TabIndex = 2;
            // 
            // TextPath
            // 
            this.TextPath.Location = new System.Drawing.Point(55, 46);
            this.TextPath.Name = "TextPath";
            this.TextPath.Size = new System.Drawing.Size(701, 21);
            this.TextPath.TabIndex = 3;
            // 
            // FolderButton
            // 
            this.FolderButton.Location = new System.Drawing.Point(778, 43);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(75, 23);
            this.FolderButton.TabIndex = 4;
            this.FolderButton.Text = "选择路径";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(55, 389);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(193, 389);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "暂停";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // rtbList
            // 
            this.rtbList.Location = new System.Drawing.Point(55, 74);
            this.rtbList.Name = "rtbList";
            this.rtbList.Size = new System.Drawing.Size(701, 309);
            this.rtbList.TabIndex = 7;
            this.rtbList.Text = "";
            // 
            // labLog
            // 
            this.labLog.AutoSize = true;
            this.labLog.Location = new System.Drawing.Point(778, 74);
            this.labLog.Name = "labLog";
            this.labLog.Size = new System.Drawing.Size(41, 12);
            this.labLog.TabIndex = 8;
            this.labLog.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 424);
            this.Controls.Add(this.labLog);
            this.Controls.Add(this.rtbList);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.FolderButton);
            this.Controls.Add(this.TextPath);
            this.Controls.Add(this.TextUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextUrl;
        private System.Windows.Forms.TextBox TextPath;
        private System.Windows.Forms.Button FolderButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox rtbList;
        private System.Windows.Forms.Label labLog;
    }
}

