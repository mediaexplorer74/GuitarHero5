// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CVector2d
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public struct CVector2d
  {
    public int m_i;
    public int m_j;

    public CVector2d(int degrees)
    {
      this.m_i = CMathFixed.Cos(degrees);
      this.m_j = CMathFixed.Sin(degrees);
    }

    public CVector2d(int[] ij)
    {
      this.m_i = ij[0];
      this.m_j = ij[1];
    }

    public CVector2d(int i, int j)
    {
      this.m_i = i;
      this.m_j = j;
    }

    public CVector2d(CVector2d v)
    {
      this.m_i = v.m_i;
      this.m_j = v.m_j;
    }

    public int Length() => CMathFixed.Sqrt(this.m_i * this.m_i + this.m_j * this.m_j);

    public int LengthSq() => this.m_i * this.m_i + this.m_j * this.m_j;

    public CVector2d Clear()
    {
      this.m_i = 0;
      this.m_j = 0;
      return this;
    }

    public CVector2d Set(int i, int j)
    {
      this.m_i = i;
      this.m_j = j;
      return this;
    }

    public CVector2d Set(CVector2d v)
    {
      this.m_i = v.m_i;
      this.m_j = v.m_j;
      return this;
    }

    public CVector2d Normalize()
    {
      int denom = this.Length();
      if (denom != 0)
      {
        this.m_i = CMathFixed.Div(this.m_i, denom);
        this.m_j = CMathFixed.Div(this.m_j, denom);
      }
      return this;
    }

    public CVector2d ProjOnto(CVector2d v) => this;

    public CVector2d LerpTo(CVector2d v, int u)
    {
      this.m_i += CMathFixed.Mul(u, v.m_i - this.m_i);
      this.m_j += CMathFixed.Mul(u, v.m_j - this.m_j);
      return this;
    }

    public CVector2d Rotate(int degrees)
    {
      if (degrees != 0)
      {
        int val1_1 = CMathFixed.Sin(degrees);
        int val1_2 = CMathFixed.Cos(degrees);
        int i = this.m_i;
        this.m_i = CMathFixed.Mul(val1_2, i) + CMathFixed.Mul(-val1_1, this.m_j);
        this.m_j = CMathFixed.Mul(val1_1, i) + CMathFixed.Mul(val1_2, this.m_j);
      }
      return this;
    }

    public int Angle()
    {
      CVector2d cvector2d = new CVector2d(this);
      cvector2d.Normalize();
      int num = CMathFixed.ACos(cvector2d.m_i);
      if (this.m_j < 0 && num != 0)
        num = 23592960 - num;
      return num;
    }

    public int Angle(CVector2d v)
    {
      int num = this.Length() * v.Length();
      return num != 0 ? CMathFixed.ACos(this * v / num) : 0;
    }

    public int AngleTo(CVector2d v)
    {
      int num1 = this.Angle();
      int num2 = v.Angle() - num1;
      if (num2 <= -11796480)
        num2 += 23592960;
      else if (num2 >= 11796480)
        num2 -= 23592960;
      return num2;
    }

    public int ShortestDistanceToLineSegment(
      CVector2d segV0,
      CVector2d segV1,
      CVector2d pntOfIntersection)
    {
      CVector2d cvector2d1 = segV1 - segV0;
      int num = (this - segV0) * cvector2d1;
      int denom = cvector2d1 * cvector2d1;
      CVector2d cvector2d2;
      if (num <= 0)
      {
        cvector2d2 = segV0 - this;
        pntOfIntersection = segV0;
      }
      else if (denom <= num)
      {
        cvector2d2 = segV1 - this;
        pntOfIntersection = segV1;
      }
      else
      {
        CVector2d cvector2d3 = this;
        pntOfIntersection = cvector2d1;
        pntOfIntersection *= CMathFixed.Div(num, denom);
        pntOfIntersection += segV0;
        cvector2d2 = cvector2d3 - pntOfIntersection;
      }
      return cvector2d2.Length();
    }

    public int IsOnLineSegment(CVector2d segV0, CVector2d segV1)
    {
      return CMathFixed.Mul(segV1.m_i - segV0.m_i, this.m_j - segV0.m_j) - CMathFixed.Mul(this.m_i - segV0.m_i, segV1.m_j - segV0.m_j);
    }

    public int IsInPoly(CVector2d[] vertex, int vertexCount)
    {
      int num = 0;
      for (int index1 = 0; index1 < vertexCount; ++index1)
      {
        int index2 = index1 + 1 < vertexCount ? index1 + 1 : 0;
        if (vertex[index1].m_j <= this.m_j)
        {
          if (vertex[index2].m_j > this.m_j && this.IsOnLineSegment(vertex[index1], vertex[index2]) >= 0)
            ++num;
        }
        else if (vertex[index2].m_j <= this.m_j && this.IsOnLineSegment(vertex[index1], vertex[index2]) <= 0)
          --num;
      }
      return num;
    }

    public bool IsInRect(CVector2d upperLeft, CVector2d lowerRight)
    {
      return this.m_i >= upperLeft.m_i && this.m_i < lowerRight.m_j && this.m_j >= upperLeft.m_j && this.m_j < lowerRight.m_j;
    }

    public int this[int idx]
    {
      get => idx != 0 ? this.m_j : this.m_i;
      set
      {
        if (idx == 0)
          this.m_i = value;
        else
          this.m_j = value;
      }
    }

    public static CVector2d operator +(CVector2d v1, CVector2d v2)
    {
      return new CVector2d(v1.m_i + v2.m_i, v1.m_j + v2.m_j);
    }

    public static CVector2d operator +(CVector2d v, int val)
    {
      return new CVector2d(v.m_i + val, v.m_j + val);
    }

    public static CVector2d operator +(int val, CVector2d v)
    {
      return new CVector2d(val + v.m_i, val + v.m_j);
    }

    public static CVector2d operator -(CVector2d v1, CVector2d v2)
    {
      return new CVector2d(v1.m_i - v2.m_i, v1.m_j - v2.m_j);
    }

    public static CVector2d operator -(CVector2d v, int val)
    {
      return new CVector2d(v.m_i - val, v.m_j - val);
    }

    public static CVector2d operator -(int val, CVector2d v)
    {
      return new CVector2d(val - v.m_i, val - v.m_j);
    }

    public static int operator *(CVector2d v1, CVector2d v2)
    {
      return CMathFixed.Mul(v1.m_i, v2.m_i) + CMathFixed.Mul(v1.m_j, v2.m_j);
    }

    public static CVector2d operator *(CVector2d v, int val)
    {
      return new CVector2d(CMathFixed.Mul(v.m_i, val), CMathFixed.Mul(v.m_j, val));
    }

    public static CVector2d operator *(int val, CVector2d v)
    {
      return new CVector2d(CMathFixed.Mul(val, v.m_i), CMathFixed.Mul(val, v.m_j));
    }

    public static CVector2d operator /(CVector2d v, int val)
    {
      return new CVector2d(CMathFixed.Div(v.m_i, val), CMathFixed.Div(v.m_j, val));
    }

    public static CVector2d operator /(int val, CVector2d v)
    {
      return new CVector2d(CMathFixed.Div(val, v.m_i), CMathFixed.Div(val, v.m_j));
    }
  }
}
