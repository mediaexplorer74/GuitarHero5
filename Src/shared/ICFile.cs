// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICFile
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System.IO;
using System.IO.IsolatedStorage;

#nullable disable
namespace com.glu.shared
{
  public sealed class ICFile
  {
    public const int FILE_SEEK_START = 0;
    public const int FILE_SEEK_CURRENT = 1;
    public const int FILE_SEEK_END = 2;
    public FileStream /*IsolatedStorageFileStream*/ m_pFile;

    public ICFile() => this.m_pFile = null;//(IsolatedStorageFileStream) null;

    public SeekOrigin GetFileSeekType(int origin)
    {
      SeekOrigin fileSeekType = SeekOrigin.Begin;
      switch (origin)
      {
        case 0:
          fileSeekType = SeekOrigin.Begin;
          break;
        case 1:
          fileSeekType = SeekOrigin.Current;
          break;
        case 2:
          fileSeekType = SeekOrigin.End;
          break;
      }
      return fileSeekType;
    }

    public bool IsOpen() => this.m_pFile != null;

    public bool Seek(int offset, int origin)
    {
      bool flag = false;
      if (this.m_pFile != null)
      {
        this.m_pFile.Seek((long) offset, this.GetFileSeekType(origin));
        flag = true;
      }
      return flag;
    }

    public uint Tell() => this.m_pFile != null ? (uint) this.m_pFile.Position : 0U;

    public uint Read(byte[] pOut, uint n)
    {
      uint num = 0;
      if (this.m_pFile != null)
        num = (uint) this.m_pFile.Read(pOut, 0, (int) n);
      return num;
    }

    public uint Write(byte[] pData, uint n)
    {
      uint num = 0;
      if (this.m_pFile != null)
      {
        this.m_pFile.Write(pData, 0, (int) n);
        num = n;
      }
      return num;
    }

    public static ICFile CreateInstance() => new ICFile();
  }
}
