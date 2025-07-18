// Decompiled with JetBrains decompiler
// Type: com.glu.game.BitVector
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System;

#nullable disable
namespace com.glu.game
{
  public class BitVector
  {
    private uint m_numBytes;
    private byte[] m_pBytes;
    private uint m_logicalCapacity;
    private uint m_onCount;

    public BitVector()
    {
      this.m_numBytes = 0U;
      this.m_pBytes = (byte[]) null;
      this.m_logicalCapacity = 0U;
      this.m_onCount = 0U;
    }

    public BitVector(uint logicalCapacity)
    {
      this.m_numBytes = 0U;
      this.m_pBytes = (byte[]) null;
      this.Init(logicalCapacity);
      this.m_onCount = 0U;
    }

    public void Init(uint logicalCapacity)
    {
      this.m_numBytes = (logicalCapacity + 7U) / 8U;
      this.m_pBytes = new byte[(int) this.m_numBytes];
      Array.Clear((Array) this.m_pBytes, 0, (int) this.m_numBytes);
      this.m_logicalCapacity = logicalCapacity;
    }

    public void Init(uint logicalCapacity, DataInputStream dis)
    {
      this.Init(logicalCapacity);
      dis.Read(this.m_pBytes, this.m_numBytes);
      for (int index = 0; (long) index < (long) logicalCapacity; ++index)
      {
        if (this.IsMember(index))
          ++this.m_onCount;
      }
    }

    public void SetMember(int index, bool member)
    {
      int index1 = index / 8;
      if (member)
      {
        if (this.IsMember(index))
          return;
        ++this.m_onCount;
        this.m_pBytes[index1] |= (byte) (1 << index % 8);
      }
      else
      {
        if (!this.IsMember(index))
          return;
        --this.m_onCount;
        this.m_pBytes[index1] &= (byte) ~(1 << index % 8);
      }
    }

    public bool IsMember(int index) => ((int) this.m_pBytes[index / 8] & 1 << index % 8) != 0;

    public uint GetLogicalCapacity() => this.m_logicalCapacity;

    public uint GetOnCount() => this.m_onCount;

    public uint GetOffCount() => this.m_logicalCapacity - this.m_onCount;
  }
}
