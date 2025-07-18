// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CVector
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public class CVector : CClass
  {
    public const uint ClassId = 906183298;
    protected int[] m_pBuf;
    protected int m_size;
    protected int m_capacity;
    public static readonly int VECTOR_INITIAL_CAPACITY = 10;

    public CVector()
    {
      this.m_classId = 906183298U;
      this.m_pBuf = (int[]) null;
      this.m_size = 0;
      this.m_capacity = 0;
    }

    public void Clear() => this.m_size = 0;

    public bool IsEmpty() => this.m_size == 0;

    public int Size() => this.m_size;

    public int Capacity() => this.m_capacity;

    public int[] GetArrayPtr() => this.m_pBuf;

    public uint Add(int val) => this.Add(this.m_size, val);

    public uint Add(int idx, int val)
    {
      uint num;
      if (idx >= 0 && idx <= this.m_size)
      {
        num = this.AdjustVectorCapacity(this.m_size + 1, false);
        if (num == 0U)
        {
          ++this.m_size;
          for (int index = this.m_size - 1; index > idx; --index)
            this.m_pBuf[index] = this.m_pBuf[index - 1];
          this.m_pBuf[idx] = val;
        }
      }
      else
        num = 5U;
      return num;
    }

    public uint Remove(int val)
    {
      int idx = this.IndexOf(val);
      return idx < 0 ? 6U : this.Remove(idx, ref val);
    }

    public uint Remove(int idx, ref int val)
    {
      uint num;
      if (idx >= 0 && idx < this.m_size)
      {
        val = this.m_pBuf[idx];
        for (int index = idx; index < this.m_size - 1; ++index)
          this.m_pBuf[index] = this.m_pBuf[index + 1];
        num = this.AdjustVectorCapacity(this.m_size - 1, false);
        if (num == 0U)
          --this.m_size;
      }
      else
        num = 5U;
      return num;
    }

    public uint Set(int idx, int val)
    {
      uint num;
      if (idx >= 0 && idx < this.m_size)
      {
        this.m_pBuf[idx] = val;
        num = 0U;
      }
      else
        num = 5U;
      return num;
    }

    public uint Get(int idx, ref int val)
    {
      uint num;
      if (idx >= 0 && idx < this.m_size)
      {
        val = this.m_pBuf[idx];
        num = 0U;
      }
      else
        num = 5U;
      return num;
    }

    public int IndexOf(int val) => this.IndexOf(0, val);

    public int IndexOf(int startIdx, int val)
    {
      int num = -1;
      for (int index = startIdx; index < this.m_size; ++index)
      {
        if (this.m_pBuf[index] == val)
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public int LastIndexOf(int val) => this.LastIndexOf(this.m_size - 1, val);

    public int LastIndexOf(int startIdx, int val)
    {
      int num = -1;
      for (int index = startIdx; index >= 0; --index)
      {
        if (this.m_pBuf[index] == val)
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public uint SetSize(int size)
    {
      uint num = this.AdjustVectorCapacity(size, false);
      if (num == 0U)
        this.m_size = size;
      return num;
    }

    public uint TrimToSize() => this.AdjustVectorCapacity(this.m_size, true);

    public uint EnsureCapacity(int capacity)
    {
      return capacity < 0 ? 3U : this.AdjustVectorCapacity(capacity, false);
    }

    public uint AdjustVectorCapacity(int capacity, bool exact)
    {
      capacity = exact ? capacity : Math.Max(CVector.VECTOR_INITIAL_CAPACITY, capacity << 1);
      uint num;
      if (exact && capacity == 0)
      {
        this.m_pBuf = (int[]) null;
        this.m_capacity = 0;
        num = 0U;
      }
      else if (exact || capacity > this.m_capacity)
      {
        int[] pBuf = this.m_pBuf;
        this.m_pBuf = new int[capacity];
        if (this.m_pBuf != null)
        {
          if (pBuf != null)
          {
            int length = Math.Min(this.m_size, capacity);
            if (length > 0)
              Array.Copy((Array) pBuf, (Array) this.m_pBuf, length);
          }
          this.m_capacity = capacity;
          num = 0U;
        }
        else
          num = 2U;
      }
      else
        num = 0U;
      return num;
    }
  }
}
