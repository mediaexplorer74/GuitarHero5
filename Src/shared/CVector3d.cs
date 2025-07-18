// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CVector3d
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public struct CVector3d
  {
    public int m_i;
    public int m_j;
    public int m_k;

    public CVector3d(int i, int j, int k)
    {
      this.m_i = i;
      this.m_j = j;
      this.m_k = k;
    }

    public CVector3d(CVector3d v)
    {
      this.m_i = v.m_i;
      this.m_j = v.m_j;
      this.m_k = v.m_k;
    }

    public int Length() => CMathFixed.Sqrt(this * this);

    public int LengthSq() => this * this;

    public CVector3d Clear()
    {
      this.m_i = 0;
      this.m_j = 0;
      this.m_k = 0;
      return this;
    }

    public CVector3d Set(int i, int j, int k)
    {
      this.m_i = i;
      this.m_j = j;
      this.m_k = k;
      return this;
    }

    public CVector3d Set(CVector3d v)
    {
      this.m_i = v.m_i;
      this.m_j = v.m_j;
      this.m_k = v.m_k;
      return this;
    }

    public CVector3d Normalize()
    {
      int denom = this.Length();
      if (denom != 0)
      {
        this.m_i = CMathFixed.Div(this.m_i, denom);
        this.m_j = CMathFixed.Div(this.m_j, denom);
        this.m_k = CMathFixed.Div(this.m_k, denom);
      }
      return this;
    }

    public int this[int idx]
    {
      get
      {
        if (idx == 0)
          return this.m_i;
        return idx != 1 ? this.m_k : this.m_j;
      }
      set
      {
        if (idx == 0)
          this.m_i = value;
        else if (idx == 1)
          this.m_j = value;
        else
          this.m_k = value;
      }
    }

    public static CVector3d operator +(CVector3d v1, CVector3d v2)
    {
      return new CVector3d(v1.m_i + v2.m_i, v1.m_j + v2.m_j, v1.m_k + v2.m_k);
    }

    public static CVector3d operator +(CVector3d v, int val)
    {
      return new CVector3d(v.m_i + val, v.m_j + val, v.m_k + val);
    }

    public static CVector3d operator +(int val, CVector3d v)
    {
      return new CVector3d(val + v.m_i, val + v.m_j, val + v.m_k);
    }

    public static CVector3d operator -(CVector3d v1, CVector3d v2)
    {
      return new CVector3d(v1.m_i - v2.m_i, v1.m_j - v2.m_j, v1.m_k - v2.m_k);
    }

    public static CVector3d operator -(CVector3d v, int val)
    {
      return new CVector3d(v.m_i - val, v.m_j - val, v.m_k - val);
    }

    public static CVector3d operator -(int val, CVector3d v)
    {
      return new CVector3d(val - v.m_i, val - v.m_j, val - v.m_k);
    }

    public static int operator *(CVector3d v1, CVector3d v2)
    {
      return CMathFixed.Mul(v1.m_i, v2.m_i) + CMathFixed.Mul(v1.m_j, v2.m_j) + CMathFixed.Mul(v1.m_k, v2.m_k);
    }

    public static CVector3d operator *(CVector3d v, int val)
    {
      return new CVector3d(CMathFixed.Mul(v.m_i, val), CMathFixed.Mul(v.m_j, val), CMathFixed.Mul(v.m_k, val));
    }

    public static CVector3d operator *(int val, CVector3d v)
    {
      return new CVector3d(CMathFixed.Mul(val, v.m_i), CMathFixed.Mul(val, v.m_j), CMathFixed.Mul(val, v.m_k));
    }

    public static CVector3d operator /(CVector3d v, int val)
    {
      return new CVector3d(CMathFixed.Div(v.m_i, val), CMathFixed.Div(v.m_j, val), CMathFixed.Div(v.m_k, val));
    }

    public static CVector3d operator /(int val, CVector3d v)
    {
      return new CVector3d(CMathFixed.Div(val, v.m_i), CMathFixed.Div(val, v.m_j), CMathFixed.Div(val, v.m_k));
    }
  }
}
