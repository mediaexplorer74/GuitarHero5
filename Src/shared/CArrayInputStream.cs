// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CArrayInputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public class CArrayInputStream : CInputStream
  {
    private byte[] m_pBuf;
    private MemoryStream stream;

    public CArrayInputStream() => this.m_pBuf = (byte[]) null;

    public bool Open(byte[] pBuf, uint size)
    {
      this.m_fail = true;
      if (pBuf != null && size > 0U)
      {
        this.m_pBuf = pBuf;
        this.stream = new MemoryStream(this.m_pBuf);
        this.m_size = size;
        this.m_fail = false;
      }
      return !this.m_fail;
    }

        public override void Close()
        {
            base.Close();
            this.stream.Dispose();
            this.m_pBuf = (byte[])null;
        }

    public uint ReadUtf8(uint utflen, char[] szOut, uint buflen)
    {
      uint length = 0;
      if (szOut != null && buflen > 0U)
      {
        if (this.Available() >= utflen)
        {
          byte[] numArray = new byte[(int)utflen];
          this.Read(numArray, utflen);
          char[] charArray = Encoding.UTF8.GetString(numArray, 0, (int) utflen).ToCharArray();
          length = Math.Min((uint) charArray.Length, buflen);
          Array.Copy((Array) charArray, (Array) szOut, (int) length);
        }
        else
          this.m_fail = true;
      }
      return length;
    }

    public uint ReadJMUtf(char[] szOut, uint buflen)
    {
      bool bigEndian = this.m_bigEndian;
      this.m_bigEndian = true;
      uint utflen = (uint) this.ReadUInt16();
      this.m_bigEndian = bigEndian;
      return this.ReadUtf8(utflen, szOut, buflen);
    }

    protected override bool IsOpenInternal()
    {
      return this.m_pBuf != null && this.m_size > 0U && this.stream.CanRead;
    }

    protected override uint AvailableInternal()
    {
      uint num = 0;
      if (this.m_pBuf != null && !this.m_fail)
        num = this.m_size - this.m_cursor;
      return num;
    }

    protected override void SkipInternal(uint n)
    {
      if (n <= 0U)
        return;
      if (this.Available() >= n)
      {
        this.stream.Seek((long) n, SeekOrigin.Current);
        this.m_cursor += n;
      }
      else
        this.m_fail = true;
    }

    protected override void ReadInternal(byte[] pBuf, uint n)
    {
      if (pBuf == null || n <= 0U)
        return;
      if (this.Available() >= n)
      {
        this.stream.Read(pBuf, 0, (int) n);
        this.m_cursor += n;
      }
      else
        this.m_fail = true;
      if (!this.m_fail)
        return;
      Array.Clear((Array) pBuf, 0, (int) n);
    }
  }
}
