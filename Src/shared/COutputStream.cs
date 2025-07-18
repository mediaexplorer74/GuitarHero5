// Decompiled with JetBrains decompiler
// Type: com.glu.shared.COutputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public class COutputStream
  {
    protected COutputStream m_pReflexiveStream;
    protected bool m_bigEndian;
    protected bool m_fail;
    protected uint m_size;
    protected uint m_cursor;
    protected byte[] m_oneByteArray = new byte[1];

    public COutputStream()
    {
      this.m_pReflexiveStream = (COutputStream) null;
      this.m_bigEndian = false;
      this.m_fail = false;
      this.m_size = 0U;
      this.m_cursor = 0U;
    }

    public void SetEndian(bool bigEndian) => this.m_bigEndian = bigEndian;

    public bool GetEndian() => this.m_bigEndian;

    public bool GetFail() => this.m_fail;

    public bool Open(COutputStream pStream, uint size)
    {
      bool flag = false;
      if (pStream != null && size > 0U)
      {
        this.m_pReflexiveStream = pStream;
        this.m_size = size;
        flag = true;
      }
      return flag;
    }

    public virtual void Close()
    {
      if (this.m_pReflexiveStream != null)
      {
        this.m_pReflexiveStream.Close();
        this.m_pReflexiveStream = (COutputStream) null;
      }
      this.m_bigEndian = false;
      this.m_fail = false;
      this.m_size = 0U;
      this.m_cursor = 0U;
    }

    public void WriteUInt8(byte val)
    {
      this.m_oneByteArray[0] = val;
      this.Write(this.m_oneByteArray, 1U);
    }

    public void WriteUInt16(ushort val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 2U);
      this.Write(bytes, 2U);
    }

    public void WriteUInt32(uint val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 4U);
      this.Write(bytes, 4U);
    }

    public void WriteUInt64(ulong val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 8U);
      this.Write(bytes, 8U);
    }

    public void WriteInt8(sbyte val)
    {
      this.m_oneByteArray[0] = (byte) val;
      this.Write(this.m_oneByteArray, 1U);
    }

    public void WriteInt16(short val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 2U);
      this.Write(bytes, 2U);
    }

    public void WriteInt32(int val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 4U);
      this.Write(bytes, 4U);
    }

    public void WriteInt64(long val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.FixByteOrderToHost(bytes, 8U);
      this.Write(bytes, 8U);
    }

    public bool IsOpen()
    {
      return this.m_pReflexiveStream == null ? this.IsOpenInternal() : this.m_pReflexiveStream.IsOpen();
    }

    public void Write(byte[] pBuf, uint n)
    {
      if (this.m_pReflexiveStream != null)
      {
        if (n <= 0U)
          return;
        if ((uint) ((int) this.m_size - (int) this.m_cursor + 1) >= n)
        {
          this.m_pReflexiveStream.Write(pBuf, n);
          this.m_fail = this.m_pReflexiveStream.GetFail();
          this.m_cursor += n;
        }
        else
          this.m_fail = true;
      }
      else
        this.WriteInternal(pBuf, n);
    }

    protected void FixByteOrderToHost(byte[] bytes, uint n)
    {
      if (!this.m_bigEndian)
        return;
      for (uint index = 0; index < n >> 1; ++index)
      {
        bytes[(int) index] ^= bytes[(int) (uint) ((int) n - (int) index - 1)];
        bytes[(int) (uint) ((int) n - (int) index - 1)] ^= bytes[(int) index];
        bytes[(int) index] ^= bytes[(int) (uint) ((int) n - (int) index - 1)];
      }
    }

    public virtual bool IsOpenInternal() => false;

    public virtual void WriteInternal(byte[] pBuf, uint n)
    {
      if (pBuf == null || n <= 0U)
        return;
      this.m_fail = true;
    }
  }
}
