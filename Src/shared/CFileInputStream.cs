// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CFileInputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class CFileInputStream : CInputStream
  {
    private ICFile m_pFile;

    public CFileInputStream() => this.m_pFile = (ICFile) null;

    public bool Open(string wcsFileName)
    {
      this.m_fail = true;
      if (wcsFileName != null)
      {
        this.m_size = ICFileMgr.GetInstance().Size(wcsFileName);
        if (this.m_size > 0U)
        {
          this.m_pFile = ICFileMgr.GetInstance().Open(wcsFileName, 0);
          this.m_fail = this.m_pFile == null;
        }
      }
      return !this.m_fail;
    }

    public bool Open(ICFile pFile)
    {
      this.m_fail = true;
      if (pFile != null)
      {
        int offset = (int) pFile.Tell();
        bool flag = pFile.Seek(0, 2);
        if (flag)
        {
          this.m_size = pFile.Tell();
          flag = pFile.Seek(offset, 0);
        }
        this.m_pFile = pFile;
        this.m_fail = !flag;
      }
      return !this.m_fail;
    }

    public override sealed void Close()
    {
      base.Close();
      if (this.m_pFile == null)
        return;
      this.m_pFile.m_pFile.Dispose();
      this.m_pFile = (ICFile) null;
    }

    protected override bool IsOpenInternal() => this.m_pFile != null;

    protected override uint AvailableInternal()
    {
      uint num = 0;
      if (this.m_pFile != null && !this.m_fail)
        num = this.m_size - this.m_pFile.Tell();
      return num;
    }

    protected override void SkipInternal(uint n)
    {
      if (n <= 0U)
        return;
      if (this.Available() >= n)
        this.m_fail = !this.m_pFile.Seek((int) n, 1);
      else
        this.m_fail = true;
    }

    protected override void ReadInternal(byte[] pBuf, uint n)
    {
      if (pBuf == null || n <= 0U)
        return;
      if (this.Available() >= n)
        this.m_fail = (int) n != (int) this.m_pFile.Read(pBuf, n);
      else
        this.m_fail = true;
      if (!this.m_fail)
        return;
      Array.Clear((Array) pBuf, 0, (int) n);
    }
  }
}
