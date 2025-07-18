// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICFileMgr
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.IO;
using System.IO.IsolatedStorage;

#nullable disable
namespace com.glu.shared
{
  public class ICFileMgr : CSingleton
  {
    public const int FILE_ACCESS_READ = 0;
    public const int FILE_ACCESS_WRITE_OVERWRITE = 1;
    public const int FILE_ACCESS_WRITE_APPEND = 2;
    public const string kwcsPathSeparator = "\\";
    protected new static CSingleton m_instance;
    protected IsolatedStorageFile m_isolatedStorageInstance;

    public string GetApplicationPath() => "";

    public string GetApplicationDataPath() => "";

    public string GetPathSeparator() => "\\";

    public ICFile Open(string wcsFileName, int mode)
    {
      ICFile icFile = (ICFile) null;
      if (wcsFileName != null)
      {
        icFile = new ICFile();
        if (icFile != null)
        {
          try
          {
           
            icFile.m_pFile = this.GetFileStream(wcsFileName, this.GetFileOpenMode(mode), 
                this.GetFileAccessMode(mode));       
          }
          catch (Exception ex)
          {
          }
          if (icFile.m_pFile == null)
            icFile = (ICFile) null;
        }
      }
      return icFile;
    }

    public IsolatedStorageFileStream GetFileStream(string wcsFileName, FileMode fileMode, FileAccess fileAccess)
    {
        return new IsolatedStorageFileStream(wcsFileName, fileMode, fileAccess, this.m_isolatedStorageInstance);
    }

    protected FileMode GetFileOpenMode(int mode)
    {
      FileMode fileOpenMode = FileMode.Open;
      switch (mode)
      {
        case 0:
          fileOpenMode = FileMode.Open;
          break;
        case 1:
          fileOpenMode = FileMode.Create;
          break;
        case 2:
          fileOpenMode = FileMode.Append;
          break;
      }
      return fileOpenMode;
    }

    protected FileAccess GetFileAccessMode(int mode)
    {
      FileAccess fileAccessMode = FileAccess.Read;
      switch (mode)
      {
        case 0:
          fileAccessMode = FileAccess.Read;
          break;
        case 1:
          fileAccessMode = FileAccess.Write;
          break;
        case 2:
          fileAccessMode = FileAccess.Write;
          break;
      }
      return fileAccessMode;
    }

    public void Close(ICFile pFile)
    {
      pFile.m_pFile.Dispose();
      pFile = (ICFile) null;
    }

    public bool Exists(string wcsFileName)
    {
      return wcsFileName != null && this.m_isolatedStorageInstance.FileExists(wcsFileName);
    }

    public uint Size(string wcsFileName)
    {
      uint num = 0;
      if (wcsFileName != null)
      {
        ICFile pFile = this.Open(wcsFileName, 0);
        if (pFile != null)
        {
          pFile.Seek(0, 2);
          num = pFile.Tell();
          this.Close(pFile);
        }
      }
      return num;
    }

    public bool Rename(string wcsSrcFileName, string wcsDestFileName)
    {
      bool flag = false;
      if (wcsSrcFileName != null)
      {
        if (wcsDestFileName != null)
        {
          try
          {
            ICFile pFile1 = this.Open(wcsSrcFileName, 0);
            ICFile pFile2 = this.Open(wcsDestFileName, 1);
            uint n = this.Size(wcsSrcFileName);
            byte[] numArray = new byte[(int) n];
            int num1 = (int) pFile1.Read(numArray, n);
            int num2 = (int) pFile2.Write(numArray, n);
            this.Close(pFile1);
            this.Close(pFile2);
            flag = this.Delete(wcsSrcFileName);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return flag;
    }

    public bool Delete(string wcsFileName)
    {
      bool flag = false;
      if (wcsFileName != null)
      {
        try
        {
          this.m_isolatedStorageInstance.DeleteFile(wcsFileName);
          flag = true;
        }
        catch (Exception ex)
        {
        }
      }
      return flag;
    }

    public bool MkDir(string wcsDirName)
    {
      bool flag = false;
      if (wcsDirName != null)
      {
        try
        {
          if (!this.m_isolatedStorageInstance.DirectoryExists(wcsDirName))
            this.m_isolatedStorageInstance.CreateDirectory(wcsDirName);
          flag = true;
        }
        catch (Exception ex)
        {
        }
      }
      return flag;
    }

    public bool RmDir(string wcsDirName)
    {
      bool flag = false;
      if (wcsDirName != null)
      {
        try
        {
          if (this.m_isolatedStorageInstance.DirectoryExists(wcsDirName))
            this.m_isolatedStorageInstance.DeleteDirectory(wcsDirName);
          flag = true;
        }
        catch (Exception ex)
        {
        }
      }
      return flag;
    }

    public bool DeleteFilesInDir(string wcsDirName)
    {
      bool flag = false;
      if (wcsDirName != null)
      {
        try
        {
          if (this.m_isolatedStorageInstance.DirectoryExists(wcsDirName))
          {
            string[] fileNames = this.m_isolatedStorageInstance.GetFileNames(Path.Combine(wcsDirName, "*.*"));
            if (fileNames.Length == 0)
              flag = true;
            foreach (string wcsFileName in fileNames)
            {
              flag = this.Delete(wcsFileName);
              if (!flag)
                break;
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      return flag;
    }

        public long GetFreeDiskSpace()
        {
            return 100000000L;//this.m_isolatedStorageInstance.AvailableFreeSpace;
        }

        public static ICFileMgr GetInstance()
    {
      if (ICFileMgr.m_instance == null)
      {
        ICFileMgr.m_instance = (CSingleton) new ICFileMgr();
        if (((ICFileMgr) ICFileMgr.m_instance).m_isolatedStorageInstance == null)
          ((ICFileMgr) ICFileMgr.m_instance).m_isolatedStorageInstance = IsolatedStorageFile.GetUserStoreForApplication();
      }
      return ICFileMgr.m_instance as ICFileMgr;
    }
  }
}
