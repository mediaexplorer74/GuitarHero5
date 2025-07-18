// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CInputStream
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public class CInputStream
  {
    protected CInputStream m_pReflexiveStream;
    protected bool m_bigEndian;
    protected bool m_fail;
    protected uint m_size;
    protected uint m_cursor;
    protected byte[] m_byteArray = new byte[8];

    public CInputStream()
    {
      this.m_pReflexiveStream = (CInputStream) null;
      this.m_bigEndian = false;
      this.m_fail = false;
      this.m_size = 0U;
      this.m_cursor = 0U;
    }

    public void SetEndian(bool bigEndian) => this.m_bigEndian = bigEndian;

    public bool GetEndian() => this.m_bigEndian;

    public bool GetFail() => this.m_fail;

    public uint GetSize() => this.m_size;

    public bool Open(CInputStream pStream, uint size)
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
        this.m_pReflexiveStream = (CInputStream) null;
      }
      this.m_bigEndian = false;
      this.m_fail = false;
      this.m_size = 0U;
      this.m_cursor = 0U;
    }

    public byte ReadUInt8()
    {
      this.Read(this.m_byteArray, 1U);
      return this.m_byteArray[0];
    }

    public ushort ReadUInt16()
    {
      this.Read(this.m_byteArray, 2U);
      this.FixByteOrderToHost(this.m_byteArray, 2U);
      return BitConverter.ToUInt16(this.m_byteArray, 0);
    }

    public uint ReadUInt32()
    {
      this.Read(this.m_byteArray, 4U);
      this.FixByteOrderToHost(this.m_byteArray, 4U);
      return BitConverter.ToUInt32(this.m_byteArray, 0);
    }

    public ulong ReadUInt64()
    {
      this.Read(this.m_byteArray, 8U);
      this.FixByteOrderToHost(this.m_byteArray, 8U);
      return BitConverter.ToUInt64(this.m_byteArray, 0);
    }

    public sbyte ReadInt8()
    {
      this.Read(this.m_byteArray, 1U);
      return (sbyte) this.m_byteArray[0];
    }

    public short ReadInt16()
    {
      this.Read(this.m_byteArray, 2U);
      this.FixByteOrderToHost(this.m_byteArray, 2U);
      return BitConverter.ToInt16(this.m_byteArray, 0);
    }

    public int ReadInt32()
    {
      this.Read(this.m_byteArray, 4U);
      this.FixByteOrderToHost(this.m_byteArray, 4U);
      return BitConverter.ToInt32(this.m_byteArray, 0);
    }

    public long ReadInt64()
    {
      this.Read(this.m_byteArray, 8U);
      this.FixByteOrderToHost(this.m_byteArray, 8U);
      return BitConverter.ToInt64(this.m_byteArray, 0);
    }

    public bool IsOpen()
    {
      return this.m_pReflexiveStream == null ? this.IsOpenInternal() : this.m_pReflexiveStream.IsOpen();
    }

    public uint Available()
    {
      uint num = 0;
      if (this.m_pReflexiveStream != null)
      {
        if (!this.m_fail)
          num = Math.Min(this.m_size - this.m_cursor, this.m_pReflexiveStream.Available());
      }
      else
        num = this.AvailableInternal();
      return num;
    }

    public void Skip(uint n)
    {
      if (this.m_pReflexiveStream != null)
      {
        if (n <= 0U)
          return;
        if (this.Available() >= n)
        {
          this.m_pReflexiveStream.Skip(n);
          this.m_fail = this.m_pReflexiveStream.GetFail();
          this.m_cursor += n;
        }
        else
          this.m_fail = true;
      }
      else
        this.SkipInternal(n);
    }

    public void Read(byte[] pBuf, uint n)
    {
      if (this.m_pReflexiveStream != null)
      {
        if (n <= 0U)
          return;
        if (this.Available() >= n)
        {
          this.m_pReflexiveStream.Read(pBuf, n);
          this.m_fail = this.m_pReflexiveStream.GetFail();
          this.m_cursor += n;
        }
        else
          this.m_fail = true;
        if (!this.m_fail)
          return;
        Array.Clear((Array) pBuf, 0, (int) n);
      }
      else
        this.ReadInternal(pBuf, n);
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

    protected virtual bool IsOpenInternal() => false;

    protected virtual uint AvailableInternal() => 0;

    protected virtual void SkipInternal(uint n)
    {
      if (n <= 0U)
        return;
      this.m_fail = true;
    }

    protected virtual void ReadInternal(byte[] bytes, uint n)
    {
      if (bytes == null || n <= 0U)
        return;
      Array.Clear((Array) bytes, 0, (int) n);
      this.m_fail = true;
    }
  }
}
