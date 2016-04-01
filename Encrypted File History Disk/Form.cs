using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EncryptedFileHistoryDiskUtility
{
    public partial class Form : System.Windows.Forms.Form
    {
        private String vhdPath;

        public Form()
        {
            InitializeComponent();

            /// Windows Upgrade button
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            int winVersion = (int)reg.GetValue("CurrentMajorVersionNumber");
            if (winVersion < 10)
            {
                disableAllControls();
                buttonUpgradeWindows.Text = "Upgrade To Windows 10 Pro\nTo Use This Program";
                buttonUpgradeWindows.Visible = true;
                buttonUpgradeWindows.Enabled = true;
                buttonUpgradeWindows.Click += button_UpgradeWindowsVersion_Click;
                return;
            }
            else {
                string winEdition = (string)reg.GetValue("EditionID");
                if (winEdition.Contains("Home") || winEdition.Contains("Education"))
                {
                    disableAllControls();
                    buttonUpgradeWindows.Text = "Upgrade To Windows Pro\nTo Use This Program";
                    buttonUpgradeWindows.Visible = true;
                    buttonUpgradeWindows.Enabled = true;
                    buttonUpgradeWindows.Click += button_UpgradeWindowsEdition_Click;
                    return;
                }
                else {
                    if (!Program.TestHyperV()) {
                        disableAllControls();
                        buttonUpgradeWindows.Text = "Enable Hyper-V and Restart PC\nTo Use This Program";
                        buttonUpgradeWindows.Visible = true;
                        buttonUpgradeWindows.Enabled = true;
                        buttonUpgradeWindows.Click += button_InstallHyperV_Click; ;
                    }
                }
            }

            /// A: to Z:
            List<string> driveLetters = new List<string>();
            for (int i = 65; i < 91; i++) {
                driveLetters.Add(Convert.ToChar(i) + ":\\");
            }

            driveLetters = driveLetters
                .Except(System.IO.Directory.GetLogicalDrives().ToList())  /// Remove in-use drives
                .Select(s => s.Substring(0, 2))                           /// Remove the \ (e.g. A:\ to A:)
                .ToList<string>();
            comboLetters.Items.AddRange(driveLetters.ToArray());          /// Add all of them to the combobox 

            comboLetters.SelectedIndex = (new Random()).Next(0, driveLetters.Count);
            
        }

        private void disableAllControls() {
            buttonBrowsePath.Enabled = false;
            inputSize.Enabled = false;
            comboLetters.Enabled = false;
            inputPwd.Enabled = false;
            buttonCreateVhd.Enabled = false;
        }

        private void createVhdButton_Click(object sender, EventArgs e)
        {
            disableAllControls();
            labelProgress.Visible = true;

            labelProgress.Text = "Creating new disk…";

            /// Replace ' with '' in user-input for safe parsing as per PowerShell syntax in 'single quoted strings'
            String volPath   = (this.vhdPath).Replace(@"'", @"''");
            String volSize   = (String.Format("{0}GB", inputSize.Text)).Replace(@"'", @"''");
            String volLetter = (comboLetters.Text.Split(':')[0]).Replace(@"'", @"''");
            String volPwd    = (inputPwd.Text).Replace(@"'", @"''");

            /// TODO: Move this out of the main process
            labelProgress.Text = "Creating new disk…";
            Program.CreateVhd(volPath, volSize, volLetter);
            labelProgress.Text = "Checking disk…";
            if (Program.VhdTest(volPath, volLetter)) {
                labelProgress.Text = "Encrypting disk…";
                Program.EncryptVhd(volPath, volLetter, volPwd);
                labelProgress.Text = "Checking crypto…";
                if (Program.EncryptedVhdTestrun(volPath, volLetter, volPwd)) {
                    labelProgress.Text = "Scheduling mounts…";
                    Program.RegisterMountTask(volPath, volLetter, volPwd);
                    labelProgress.Text = "Done.";
                    MessageBox.Show("Successfully created a new encrypted virtual disk image. You may now choose the new disk as a backup destination, or use it for any other purpose.");
                    labelProgress.Text = "Run File History…";
                    Program.OpenFileHistorySettings();
                }
                else MessageBox.Show("Failed to encrypt the new virtual disk file. Please restart the computer before reattempting to create another virtual disk.");
            } else MessageBox.Show("Failed to create a new virtual disk file. The file couldn’t be written because of a unknown problem. Please restart the program and try again.");

            Application.Exit();
        }

        private void link_Documentation_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.geeky.software/utils/encrypted-file-history-disk/documentation");
        }

        private void button_UpgradeWindowsEdition_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("ms-windows-store://WindowsUpgrade");
        }
        private void button_UpgradeWindowsVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("ms-windows-store://WindowsUpgrade");
        }
        private void button_InstallHyperV_Click(object sender, EventArgs e)
        {
            Program.InstallHyperV();
            Application.Exit();
        }

        private void button_ChoosePath_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

            SaveFileDialog saveDialog = (SaveFileDialog)sender;

            if (System.IO.Path.GetExtension(saveDialog.FileName) == ".vhdx") {
                this.vhdPath = saveDialog.FileName;

                Uri test2 = new Uri(saveDialog.FileName);
                if (test2.IsUnc) {
                    pathLabel.Text = String.Format("\\\\{0}", test2.Host);
                }
                else {
                    pathLabel.Text = System.IO.Path.GetPathRoot(saveDialog.FileName).Substring(0, 2);
                }
                createVhdButton_Enabled_State();
        }   }



        private void createVhdButton_Enabled_State()
        {
            buttonCreateVhd.Enabled = (this.vhdPath != null &&
                                       comboLetters.SelectedItem.ToString().EndsWith(":") &&
                                       (inputSize.Value >= inputSize.Minimum && inputSize.Value <= inputSize.Maximum) &&
                                       inputPwd.TextLength >= 6
                                       );
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            createVhdButton_Enabled_State();
        }

        private void inputSize_ValueChanged(object sender, EventArgs e)
        {
            createVhdButton_Enabled_State();
        }

        private void inputPwd_TextChanged(object sender, EventArgs e)
        {
            createVhdButton_Enabled_State();
        }
    }
}
