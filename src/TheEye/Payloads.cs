// Decompiled with JetBrains decompiler
// Type: TheEye.Payloads
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheEye
{
  internal class Payloads
  {
    public void RandomFormMove(Form form)
    {
      Random random = new Random();
      form.Location = new Point(random.Next(0, Screen.PrimaryScreen.Bounds.Width - form.Width), random.Next(0, Screen.PrimaryScreen.Bounds.Height - form.Height));
    }
  }
}
