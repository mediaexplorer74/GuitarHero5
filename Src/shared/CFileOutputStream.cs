// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CFileOutputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CFileOutputStream : COutputStream
  {
    private ICFile m_pFile;

    public CFileOutputStream() => this.m_pFile = (ICFile) null;

    public bool Open(string wcsFileName)
    {
      this.m_pFile = ICFileMgr.GetInstance().Open(wcsFileName, 1);
      this.m_fail = this.m_pFile == null;
      return !this.m_fail;
    }

    public override void Close()
    {
      base.Close();
      if (this.m_pFile == null)
        return;
      this.m_pFile.m_pFile.Dispose();
      this.m_pFile = (ICFile) null;
    }

    public override bool IsOpenInternal() => this.m_pFile != null;

    public override void WriteInternal(byte[] pBuf, uint n)
    {
      this.m_fail = true;
      if (this.m_pFile == null)
        return;
      this.m_fail = (int) n != (int) this.m_pFile.Write(pBuf, n);
    }
  }
}
