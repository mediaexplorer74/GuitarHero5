// Decompiled with JetBrains decompiler
// Type: com.glu.shared.TCStack`1
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class TCStack<T>
  {
    public T[] m_ele;
    public uint m_depth;
    public uint m_ptr;

    public TCStack()
    {
      this.m_ele = (T[]) null;
      this.m_ptr = 0U;
      this.m_depth = 0U;
    }

    public void Initialize(uint depth)
    {
      this.m_ele = new T[(int) depth];
      this.m_depth = depth;
      this.m_ptr = 1U;
    }

    public void Destroy()
    {
      this.m_ele = (T[]) null;
      this.m_depth = 0U;
      this.m_ptr = 0U;
    }

    public T Top() => this.m_ele[(int) (this.m_ptr - 1U)];

    public void Top(T t) => this.m_ele[(int) (this.m_ptr - 1U)] = t;

    public void Push()
    {
      this.m_ele[(int) this.m_ptr] = this.m_ele[(int) (this.m_ptr - 1U)];
      ++this.m_ptr;
    }

    public void Pop() => --this.m_ptr;

    public bool IsEmpty() => this.m_ptr < 1U;

    public void Set(int idx, T t) => this.m_ele[idx] = t;

    public T this[int idx]
    {
      get => this.m_ele[idx];
      set => this.m_ele[idx] = value;
    }

    public uint Depth => this.m_depth;
  }
}
