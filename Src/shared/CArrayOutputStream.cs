// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CArrayOutputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.IO;

#nullable disable
namespace com.glu.shared
{
  public sealed class CArrayOutputStream : COutputStream
  {
    private byte[] m_pBuf;
    private MemoryStream stream;

    public CArrayOutputStream() => this.m_pBuf = (byte[]) null;

    public bool Open(byte[] pBuf, uint size)
    {
      this.m_fail = true;
      if (pBuf != null && size > 0U)
      {
        this.m_pBuf = pBuf;
        this.m_size = size;
        this.stream = new MemoryStream((int) this.m_size);
        this.m_fail = false;
      }
      return !this.m_fail;
    }

    public override void Close()
    {
      base.Close();
      this.stream.Dispose();
      this.m_pBuf = (byte[]) null;
    }

    public override bool IsOpenInternal()
    {
      return this.m_pBuf != null && this.m_size > 0U && this.stream.CanWrite;
    }

    public override void WriteInternal(byte[] pBuf, uint n)
    {
      this.m_fail = true;
      if (this.m_pBuf == null || pBuf == null)
        return;
      this.m_fail = n > (uint) ((int) this.m_size - (int) this.m_cursor + 1);
      for (uint index = 0; index < n && this.m_cursor < this.m_size; ++index)
        this.m_pBuf[(int) this.m_cursor++] = (byte) (int) pBuf[(int) index];
    }
  }
}
