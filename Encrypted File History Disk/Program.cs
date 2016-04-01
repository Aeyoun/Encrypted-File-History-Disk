using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Management.Automation;
using System.Collections.ObjectModel;

[assembly: CLSCompliant(true)]

namespace EncryptedFileHistoryDiskUtility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form());
        }

        public static void OpenFileHistorySettings() {
            Process.Start("control.exe", "/name Microsoft.FileHistory /page SelectDisk");
        }

        public static void ShellHWServiceController(Boolean toggle) {
            /// Prevent dialogs from system about unformatted drives
            ServiceController service = new ServiceController("ShellHWDetection");

            if (toggle) {
                try { service.Start(); }
                catch { }
            } else {
                try { service.Stop(); }
                catch { }
            }
            service.Dispose();
        }

        public static Boolean TestHyperV() {
            PowerShell ps = PowerShell.Create();
            /// Looks silly, but works around a bug in PowerShell
            ps.AddScript("if (Get-Module -ListAvailable | Where-Object {$_.Name -eq 'Hyper-V'}) { $true } else { $false }");
            Collection<PSObject> PSOutput = ps.Invoke();
            Console.WriteLine(PSOutput.ToString());
            Boolean result = false;
            foreach (PSObject item in PSOutput) {

                if (item == null)
                    continue;
                else if ((Boolean) item.BaseObject == true) {
                    result = true;
                    break;
                }
                else if ((Boolean) item.BaseObject == false) {
                    result = false;
                    break;
                }
            }

            ps.Dispose();
            return result;
        }

        public static void InstallHyperV()
        {
            System.Diagnostics.Process.Start("powershell.exe",
                @"-NoExit -Command ""Enable-WindowsOptionalFeature –FeatureName 'Microsoft-Hyper-V' -Online -All -NoRestart; Restart-Computer""");
        }

        public static void CreateVhd(String volPath, String volSize, String volLetter)
        {
            /// Stop hardware detection service, restarted in TestVhd()
            ShellHWServiceController(false);

            PowerShell ps = PowerShell.Create();
            
            ps.AddScript(String.Format("New-VHD -Fixed -Path '{0}' -SizeBytes {1} |Mount-VHD -Passthru |"
                                      +"Initialize-Disk -Passthru -PartitionStyle GPT |"
                                      +"New-Partition -DriveLetter '{2}' -UseMaximumSize |"
                                      +"Format-Volume -FileSystem NTFS -Confirm:$false -Force",
                                       volPath,
                                       volSize,
                                       volLetter));
            ps.Invoke();
            ps.Dispose();
        }

        public static Boolean VhdTest(String volPath, String volLetter)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddScript(String.Format("Test-VHD -Path '{0}'; Test-Path '{1}:'",
                                       volPath,
                                       volLetter));
            Collection<PSObject> PSOutput = ps.Invoke();

            Boolean result = false;
            foreach (PSObject item in PSOutput) {
                if (item == null) continue;
                else if ((Boolean) item.BaseObject == false) {
                    result = false;
                    break;
                }
                result = true;
            }

            ps.Dispose();

            /// Restart hardware detection service
            ShellHWServiceController(true);

            return result;
        }

        public static void EncryptVhd(String volPath, String volLetter, String volPassword)
        {
            /// Encrypt the VHD
            PowerShell ps = PowerShell.Create();
            ps.AddScript(String.Format("Enable-BitLocker -MountPoint '{0}:' -EncryptionMethod Aes256 -Password (ConvertTo-SecureString '{1}' -AsPlainText -Force) -PasswordProtector -UsedSpaceOnly",
                                       volLetter,
                                       volPassword));
            ps.Invoke();
            ps.Dispose();
        }

        public static Boolean EncryptedVhdTestrun(String volPath, String volLetter, String volPassword)
        {
            PowerShell ps = PowerShell.Create();
            /// Make Windows realize the volume has been encrypted
            ps.AddScript(String.Format("Dismount-VHD -Path '{0}'; Mount-VHD -Path '{0}';  Unlock-BitLocker -MountPoint '{1}:' -Password (ConvertTo-SecureString '{2}' -AsPlainText -Force); Get-BitLockerVolume -MountPoint '{1}:'",
                                       volPath,
                                       volLetter,
                                       volPassword));
            Collection<PSObject> PSOutput = ps.Invoke();

            Boolean result = false;
            foreach (PSObject item in PSOutput) {
                if (item == null) continue;
                if (item.Members["EncryptionMethod"].Value.ToString() == "None" ) {
                    result = false;
                    break;
                }
                result = true;
            }
            ps.Dispose();

            return result;
        }

        public static void RegisterMountTask(String volPath, String volLetter, String volPassword)
        {
            PowerShell ps = PowerShell.Create();
            
            /// Commands to run when triggered.
            /// Check if partition is mounted, if not check if the disk is available and mount it
            ps.AddScript(String.Format("$ss = ConvertTo-SecureString '{0}' -AsPlainText -Force |ConvertFrom-SecureString",
                                       volPassword));
            ps.AddScript(string.Format("$taskcmdarg = \"if ((! (Test-Path '{1}:')) -and (Test-Path '{0}')) {{Mount-DiskImage -ImagePath '{0}';"
                                      + "Unlock-BitLocker -MountPoint '{1}:' -Password (ConvertTo-SecureString ${{ss}})}}\"",
                                       volPath,
                                       volLetter));

            ps.AddScript(string.Format("$taskname = \"Auto-mount and unlock {0}\"",
                                       volLetter));
            ps.AddScript(@"$taskuser = ([environment]::UserDomainName + '\' + [environment]::UserName)");

            /// Register a task that runs at the most once per 45-minute but only when user has been idle for more than 15 minutes
            ps.AddScript(@"$schsrv = New-Object -ComObject('Schedule.Service'); $schsrv.Connect()");
            ps.AddScript(@"$taskdef = $schsrv.NewTask(0); $taskset = $taskdef.Settings");
            ps.AddScript(@"$taskset.Compatibility = 2");
            ps.AddScript(@"$taskset.UseUnifiedSchedulingEngine = $true; $taskset.DisallowStartOnRemoteAppSession = $false");
            ps.AddScript(@"$taskset.Enabled = $true; $taskset.AllowDemandStart = $true; $taskset.RunOnlyIfIdle = $true");
            ps.AddScript(@"$taskset.DisallowStartOnRemoteAppSession = $false; $taskset.UseUnifiedSchedulingEngine = $true");
            ps.AddScript(@"$taskset.IdleSettings.IdleDuration = 'PT15M'; $taskset.IdleSettings.WaitTimeout = 'PT45M'; $taskset.IdleSettings.StopOnIdleEnd = $true");
            ps.AddScript(@"$tasktrg = $taskdef.Triggers.Create(2)");
            ps.AddScript(@"$tasktrg.StartBoundary = ([datetime]::Now).ToString(""yyyy-MM-dd'T'HH:mm:ss"")");
            ps.AddScript(@"$tasktrg.Enabled = $true; $tasktrg.DaysInterval = 1; $tasktrg.Repetition.StopAtDurationEnd = $true");
            ps.AddScript(@"$tasktrg.Repetition.Duration = 'P1D'; $tasktrg.Repetition.Interval = 'PT45M'");
            ps.AddScript(@"$taskcmd = $taskdef.Actions.Create(0); $taskcmd.Path = 'powershell.exe'; $taskcmd.Arguments = ""-NoLogo -WindowStyle Hidden -NonInteractive -Command """"$taskcmdarg""""""");
            ps.AddScript(@"$taskdef.Principal.RunLevel = 1; $taskdef.Principal.GroupId = 'BUILTIN\Administrators'");
            ps.AddScript(@"$schsroot = $schsrv.GetFolder('\')");

            ps.AddScript(@"$schsroot.RegisterTaskDefinition($taskname + ' periodically', $taskdef, 6, $taskuser, $null, 3)");

            ps.Invoke();
            ps.Dispose();
        }
    }
}
