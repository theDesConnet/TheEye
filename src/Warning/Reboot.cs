using System;
using System.Runtime.InteropServices;

namespace Win32.TheEye
{
  internal class Reboot
  {
    internal const int SE_PRIVILEGE_ENABLED = 2;
    internal const int TOKEN_QUERY = 8;
    internal const int TOKEN_ADJUST_PRIVILEGES = 32;
    internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

    [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
    private static extern int InitiateSystemShutdown(
      string lpMachineName,
      string lpMessage,
      int dwTimeout,
      bool bForceAppsClosed,
      bool bRebootAfterShutdown);

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool AdjustTokenPrivileges(
      IntPtr htok,
      bool disall,
      ref Reboot.TokPriv1Luid newst,
      int len,
      IntPtr prev,
      IntPtr relen);

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetCurrentProcess();

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

    [DllImport("advapi32.dll", SetLastError = true)]
    internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

    private void SetPriv()
    {
      IntPtr zero = IntPtr.Zero;
      if (!Reboot.OpenProcessToken(Reboot.GetCurrentProcess(), 40, ref zero))
        return;
      Reboot.TokPriv1Luid newst;
      newst.Count = 1;
      newst.Attr = 2;
      newst.Luid = 0L;
      Reboot.LookupPrivilegeValue((string) null, "SeShutdownPrivilege", ref newst.Luid);
      Reboot.AdjustTokenPrivileges(zero, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero);
    }

    public int halt(bool RSh, bool Force)
    {
      this.SetPriv();
      return Reboot.InitiateSystemShutdown((string) null, (string) null, 0, Force, RSh);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokPriv1Luid
    {
      public int Count;
      public long Luid;
      public int Attr;
    }
  }
}
