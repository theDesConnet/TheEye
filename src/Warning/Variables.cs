using System;

namespace Win32.TheEye
{
  internal class Variables
  {
    public static string[] theeyedir = new string[7]
    {
      Environment.GetEnvironmentVariable("windir") + "\\migwiz\\dlmanifests\\Microsoft-Windows-migrate-tool",
      Environment.GetEnvironmentVariable("windir") + "\\inf\\ASP.NET\\7895\\0148\\4875\\4758\\4545\\3215\\4613\\4862",
      Environment.GetEnvironmentVariable("windir") + "\\Microsoft.NET\\Framework\\v4.0.30319\\ASP.NETWebAdminFiles\\Security\\Users\\App_LocalResources\\Resources",
      Environment.GetEnvironmentVariable("windir") + "\\SoftwareDistribution\\Download\\msil_aspnet_regsql.resources_b87daw84e7f11d50a3a_6.1.7600.16385_ru-ru_b7c8cd48561af09a",
      Environment.GetEnvironmentVariable("windir") + "\\LogFiles\\SQM\\mdmxsi",
      Environment.GetEnvironmentVariable("windir") + "\\migration\\WSMT\\rras\\dlmanifests\\Microsoft-Windows-RasServer-MigHost",
      Environment.GetEnvironmentVariable("windir") + "\\drivers\\UMDF\\ru-RU\\UMDF"
    };
    public static string[] windefdir = new string[9]
    {
      Environment.GetEnvironmentVariable("windir") + "\\Defender",
      Environment.GetEnvironmentVariable("windir") + "\\SoftwareDistribution\\AuthCabs\\Defender",
      Environment.GetEnvironmentVariable("windir") + "\\security\\ApplicationId\\PolicyManagement\\Defender",
      Environment.GetEnvironmentVariable("windir") + "\\schemas\\WCN\\windef",
      Environment.GetEnvironmentVariable("windir") + "\\tracing\\windows\\hostlogs",
      Environment.GetEnvironmentVariable("windir") + "\\Vss\\Writers\\Application\\Defender",
      Environment.GetEnvironmentVariable("windir") + "\\manifeststore\\RU-ru\\manifesthost",
      Environment.GetEnvironmentVariable("windir") + "\\oobe\\UK-uk\\msoobe",
      Environment.GetEnvironmentVariable("windir") + "\\Microsoft\\Protect\\Defender"
    };
  }
}
