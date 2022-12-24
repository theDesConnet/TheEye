// Decompiled with JetBrains decompiler
// Type: TheEye.Properties.Resources
// Assembly: TheEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F112655-6C2B-4871-B1D6-7F6EA80D9DFB
// Assembly location: R:\WinBackup\Downloads\TheEye (Beta) [c0d9d by DesConnet](1)\src\Resources\theeye.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TheEye.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (TheEye.Properties.Resources.resourceMan == null)
          TheEye.Properties.Resources.resourceMan = new ResourceManager("TheEye.Properties.Resources", typeof (TheEye.Properties.Resources).Assembly);
        return TheEye.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => TheEye.Properties.Resources.resourceCulture;
      set => TheEye.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap _7d98d2290a25fdbadb1bfe263c9e40a4 => (Bitmap) TheEye.Properties.Resources.ResourceManager.GetObject("7d98d2290a25fdbadb1bfe263c9e40a4", TheEye.Properties.Resources.resourceCulture);

    internal static UnmanagedMemoryStream main => TheEye.Properties.Resources.ResourceManager.GetStream(nameof (main), TheEye.Properties.Resources.resourceCulture);
  }
}
