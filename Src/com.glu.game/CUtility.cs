// Decompiled with JetBrains decompiler
// Type: com.glu.game.CUtility
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  internal class CUtility
  {
    private string kszAppTimestampFormat = "\n\n\n$Name: GHERO5_20110124_1721_W $\n%s %s";
    private string kszVersionFormat = "\nv1.%s.%s";
    private string kszCustomVersionPrefix = "\nv";
    private string kszAppBuildDate = "__DATE__";
    private string kszAppBuildTime = "__TIME__";

    public static void GetVersionString(out string output) => output = "v1.0.11";

    public static void GetTimestampString(out string output) => output = "TimestampString";

    public static void GetString(out string output, string id)
    {
      output = "";
      CUtility.AppendString(ref output, id);
    }

    public static string RemoveUnwantedEscapes(string input)
    {
      if (input == null)
        return (string) null;
      for (int index = 0; index < input.Length; ++index)
      {
        if (input[index] == '\\')
        {
          if (input[index + 1] == 'n')
            input = input.Substring(0, index) + (object) '\n' + input.Substring(index + 2);
          else if (input[index + 1] == '\\')
            input = input.Substring(0, index) + (object) '\\' + input.Substring(index + 2);
        }
      }
      return input;
    }

    public static string ChangePercentSigns(string input)
    {
      int num = 0;
      string str = "";
      for (int index = 0; index < input.Length; ++index)
      {
        if (input[index] == '%')
        {
          if (input[index + 1] == '%')
          {
            str += "%";
            index += 2;
          }
          else
          {
            str = str + "{" + (object) num++ + "}";
            ++index;
          }
        }
        else
          str += input[index];
      }
      return str;
    }

    public static void AppendString(ref string output, string id)
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource(id, out resource1);
      if (resource1 != null)
      {
        string str = ((CStrChar) resource1.GetData()).ToString();
        if (str != null)
          output += str;
        resourceManager.ReleaseResource(id);
      }
      output = CUtility.ChangePercentSigns(output);
      output = CUtility.RemoveUnwantedEscapes(output);
    }

    public static bool FileChecksumRead(string filepath, out byte[] dest, ref uint outSize)
    {
      bool flag1 = true;
      bool flag2 = true;
      ICFileMgr instance = ICFileMgr.GetInstance();
      dest = (byte[]) null;
      uint num = instance.Size(filepath);
      if (num > 4U)
        outSize = num - 4U;
      else
        flag1 = false;
      dest = new byte[(int) outSize];
      byte[] pBuf = dest;
      bool flag3 = pBuf != null;
      CFileInputStream cfileInputStream = new CFileInputStream();
      if (flag3)
        flag3 &= cfileInputStream.Open(filepath);
      if (flag3)
        cfileInputStream.Read(pBuf, outSize);
      if (!flag3 || !flag2)
        flag2 = false;
      cfileInputStream.Close();
      return flag2;
    }

    public static void RegisterUIKeys()
    {
    }

    public static void RegisterUISoftkeys()
    {
    }

    public static void RegisterGameKeys()
    {
    }

    public static void UnregisterUIKeys()
    {
    }

    public static void UnregisterUISoftkeys()
    {
    }

    public static void UnregisterGameKeys()
    {
    }

    public static string UIntTostring(uint convert, uint Base)
    {
      return Convert.ToString((long) convert, 16);
    }
  }
}
