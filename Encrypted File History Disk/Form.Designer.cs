namespace EncryptedFileHistoryDiskUtility
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.labelPwd = new System.Windows.Forms.Label();
            this.inputPwd = new System.Windows.Forms.TextBox();
            this.volumeMaxSize = new System.Windows.Forms.Label();
            this.buttonCreateVhd = new System.Windows.Forms.Button();
            this.labelVolumePath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.GB = new System.Windows.Forms.Label();
            this.buttonUpgradeWindows = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonBrowsePath = new System.Windows.Forms.Button();
            this.comboLetters = new System.Windows.Forms.ComboBox();
            this.labelForumMountPoint = new System.Windows.Forms.Label();
            this.inputSize = new System.Windows.Forms.NumericUpDown();
            this.pathLabel = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Location = new System.Drawing.Point(12, 172);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(108, 13);
            this.labelPwd.TabIndex = 0;
            this.labelPwd.Text = "Encryption password:";
            // 
            // inputPwd
            // 
            this.inputPwd.Location = new System.Drawing.Point(126, 169);
            this.inputPwd.Name = "inputPwd";
            this.inputPwd.Size = new System.Drawing.Size(193, 20);
            this.inputPwd.TabIndex = 4;
            this.inputPwd.TextChanged += new System.EventHandler(this.inputPwd_TextChanged);
            // 
            // volumeMaxSize
            // 
            this.volumeMaxSize.AutoSize = true;
            this.volumeMaxSize.Location = new System.Drawing.Point(54, 146);
            this.volumeMaxSize.Name = "volumeMaxSize";
            this.volumeMaxSize.Size = new System.Drawing.Size(66, 13);
            this.volumeMaxSize.TabIndex = 2;
            this.volumeMaxSize.Text = "Volume size:";
            // 
            // buttonCreateVhd
            // 
            this.buttonCreateVhd.Enabled = false;
            this.buttonCreateVhd.Location = new System.Drawing.Point(236, 200);
            this.buttonCreateVhd.Name = "buttonCreateVhd";
            this.buttonCreateVhd.Size = new System.Drawing.Size(83, 23);
            this.buttonCreateVhd.TabIndex = 5;
            this.buttonCreateVhd.Text = "Create Disk";
            this.buttonCreateVhd.UseVisualStyleBackColor = true;
            this.buttonCreateVhd.Click += new System.EventHandler(this.createVhdButton_Click);
            // 
            // labelVolumePath
            // 
            this.labelVolumePath.AutoSize = true;
            this.labelVolumePath.Location = new System.Drawing.Point(35, 92);
            this.labelVolumePath.Name = "labelVolumePath";
            this.labelVolumePath.Size = new System.Drawing.Size(85, 13);
            this.labelVolumePath.TabIndex = 6;
            this.labelVolumePath.Text = "Volume file path:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 65);
            this.label4.TabIndex = 7;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 205);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(79, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Documentation";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_Documentation_Click);
            // 
            // GB
            // 
            this.GB.AutoSize = true;
            this.GB.Location = new System.Drawing.Point(225, 146);
            this.GB.Name = "GB";
            this.GB.Size = new System.Drawing.Size(22, 13);
            this.GB.TabIndex = 8;
            this.GB.Text = "GB";
            // 
            // buttonUpgradeWindows
            // 
            this.buttonUpgradeWindows.Enabled = false;
            this.buttonUpgradeWindows.Location = new System.Drawing.Point(125, 87);
            this.buttonUpgradeWindows.Name = "buttonUpgradeWindows";
            this.buttonUpgradeWindows.Size = new System.Drawing.Size(194, 102);
            this.buttonUpgradeWindows.TabIndex = 0;
            this.buttonUpgradeWindows.UseVisualStyleBackColor = true;
            this.buttonUpgradeWindows.Visible = false;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "vhdx";
            this.saveFileDialog.FileName = "Backup disk.vhdx";
            this.saveFileDialog.Filter = "Virtual Hard Disk File|*.vhdx";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // buttonBrowsePath
            // 
            this.buttonBrowsePath.Location = new System.Drawing.Point(125, 87);
            this.buttonBrowsePath.Name = "buttonBrowsePath";
            this.buttonBrowsePath.Size = new System.Drawing.Size(95, 23);
            this.buttonBrowsePath.TabIndex = 1;
            this.buttonBrowsePath.Text = "Browse";
            this.buttonBrowsePath.UseVisualStyleBackColor = true;
            this.buttonBrowsePath.Click += new System.EventHandler(this.button_ChoosePath_Click);
            // 
            // comboLetters
            // 
            this.comboLetters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLetters.FormattingEnabled = true;
            this.comboLetters.Location = new System.Drawing.Point(126, 116);
            this.comboLetters.Name = "comboLetters";
            this.comboLetters.Size = new System.Drawing.Size(93, 21);
            this.comboLetters.TabIndex = 2;
            this.comboLetters.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelForumMountPoint
            // 
            this.labelForumMountPoint.AutoSize = true;
            this.labelForumMountPoint.Location = new System.Drawing.Point(17, 119);
            this.labelForumMountPoint.Name = "labelForumMountPoint";
            this.labelForumMountPoint.Size = new System.Drawing.Size(103, 13);
            this.labelForumMountPoint.TabIndex = 12;
            this.labelForumMountPoint.Text = "Volume mount point:";
            // 
            // inputSize
            // 
            this.inputSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.inputSize.Location = new System.Drawing.Point(126, 144);
            this.inputSize.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.inputSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.inputSize.Name = "inputSize";
            this.inputSize.Size = new System.Drawing.Size(93, 20);
            this.inputSize.TabIndex = 3;
            this.inputSize.ThousandsSeparator = true;
            this.inputSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.inputSize.ValueChanged += new System.EventHandler(this.inputSize_ValueChanged);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoEllipsis = true;
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(229, 92);
            this.pathLabel.MaximumSize = new System.Drawing.Size(98, 13);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(0, 13);
            this.pathLabel.TabIndex = 13;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(130, 205);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 14;
            this.labelProgress.Visible = false;
            // 
            // Form
            // 
            this.AcceptButton = this.buttonCreateVhd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 241);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.buttonUpgradeWindows);
            this.Controls.Add(this.inputSize);
            this.Controls.Add(this.labelForumMountPoint);
            this.Controls.Add(this.comboLetters);
            this.Controls.Add(this.buttonBrowsePath);
            this.Controls.Add(this.GB);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelVolumePath);
            this.Controls.Add(this.buttonCreateVhd);
            this.Controls.Add(this.volumeMaxSize);
            this.Controls.Add(this.inputPwd);
            this.Controls.Add(this.labelPwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Encrypted File History Disk";
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.TextBox inputPwd;
        private System.Windows.Forms.Label volumeMaxSize;
        private System.Windows.Forms.Button buttonCreateVhd;
        private System.Windows.Forms.Label labelVolumePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label GB;
        private System.Windows.Forms.Button buttonUpgradeWindows;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonBrowsePath;
        private System.Windows.Forms.ComboBox comboLetters;
        private System.Windows.Forms.Label labelForumMountPoint;
        private System.Windows.Forms.NumericUpDown inputSize;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label labelProgress;
    }
}

