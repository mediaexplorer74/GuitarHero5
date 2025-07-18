// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CFileUtil
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public static class CFileUtil
  {
    public static string kwcsTempFileName = "temp.dat";

    public static void GetFilePath(out string outValue, string wcsFilePath)
    {
      outValue = "";
      if (wcsFilePath == null)
        return;
      int num = wcsFilePath.LastIndexOf(ICFileMgr.GetInstance().GetPathSeparator());
      if (num == -1)
        return;
      outValue = wcsFilePath;
      outValue = outValue.Substring(0, num - 1);
    }

    public static void GetFileName(out string outValue, string wcsFilePath)
    {
      outValue = "";
      if (wcsFilePath == null)
        return;
      outValue = wcsFilePath;
      int num = wcsFilePath.LastIndexOf(ICFileMgr.GetInstance().GetPathSeparator());
      if (num == -1)
        return;
      outValue = outValue.Substring(num + 1, wcsFilePath.Length - 1);
    }

    public static void GetApplicationPathForFile(out string outValue, string wcsFileName)
    {
      outValue = ICFileMgr.GetInstance().GetApplicationPath();
      if (outValue.Length > 0)
        outValue += ICFileMgr.GetInstance().GetPathSeparator();
      outValue += wcsFileName;
    }

    public static void GetApplicationDataPathForFile(out string outValue, string wcsFileName)
    {
      outValue = ICFileMgr.GetInstance().GetApplicationDataPath();
      if (outValue.Length > 0)
        outValue += ICFileMgr.GetInstance().GetPathSeparator();
      outValue += wcsFileName;
    }

    public static bool ReadFile(string wcsFileName, byte[] pData, uint size)
    {
      bool flag = false;
      if (pData != null && size > 0U)
      {
        ICFile pFile = ICFileMgr.GetInstance().Open(wcsFileName, 0);
        if (pFile != null)
        {
          flag = (int) size == (int) pFile.Read(pData, size);
          ICFileMgr.GetInstance().Close(pFile);
        }
      }
      else
        flag = true;
      return flag;
    }

    public static bool WriteFile(string wcsFileName, byte[] pData, uint size)
    {
      bool flag = false;
      if (pData != null && size > 0U)
      {
        ICFile pFile = ICFileMgr.GetInstance().Open(wcsFileName, 1);
        if (pFile != null)
        {
          flag = (int) size == (int) pFile.Write(pData, size);
          ICFileMgr.GetInstance().Close(pFile);
        }
      }
      return flag;
    }

    public static bool SafeWriteFile(string wcsFileName, byte[] pData, uint size)
    {
      bool flag = false;
      if (pData != null && size > 0U)
      {
        string kwcsTempFileName = CFileUtil.kwcsTempFileName;
        ICFile pFile = ICFileMgr.GetInstance().Open(kwcsTempFileName, 1);
        if (pFile != null)
        {
          flag = (int) size == (int) pFile.Write(pData, size);
          ICFileMgr.GetInstance().Close(pFile);
          if (flag)
          {
            ICFileMgr.GetInstance().Delete(wcsFileName);
            flag = ICFileMgr.GetInstance().Rename(kwcsTempFileName, wcsFileName);
          }
        }
        ICFileMgr.GetInstance().Delete(kwcsTempFileName);
      }
      else
      {
        ICFileMgr.GetInstance().Delete(wcsFileName);
        flag = true;
      }
      return flag;
    }

    public static bool ReadApplicationFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationPathForFile(out outValue, wcsFileName);
      return CFileUtil.ReadFile(outValue, pData, size);
    }

    public static bool ReadApplicationDataFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.ReadFile(outValue, pData, size);
    }

    public static bool ReadApplicationSaveGameFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.ReadFile(outValue, pData, size);
    }

    public static bool WriteApplicationDataFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.WriteFile(outValue, pData, size);
    }

    public static bool SafeWriteApplicationDataFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.SafeWriteFile(outValue, pData, size);
    }

    public static bool WriteApplicationSaveGameFile(string wcsFileName, byte[] pData, uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.WriteFile(outValue, pData, size);
    }

    public static bool SafeWriteApplicationSaveGameFile(
      string wcsFileName,
      byte[] pData,
      uint size)
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, wcsFileName);
      return CFileUtil.SafeWriteFile(outValue, pData, size);
    }
  }
}
