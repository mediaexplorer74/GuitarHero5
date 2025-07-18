// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CGraphics2dAbstract
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CGraphics2dAbstract : ICGraphics2d
  {
    protected struct ColorPkg
    {
      public uint m_color8;
      public Color.ARGB_fixed m_colorfx;
    }

    protected class AbstractState
    {
      public TCStack<bool> m_alphaTest;
      public TCStack<bool> m_blend;
      public TCStack<ICGraphics2d.BlendArg> m_blendArg;
      public TCStack<CGraphics2dAbstract.ColorPkg> m_color;
      public TCStack<bool> m_colorKeyTest;
      public TCStack<bool> m_configStateBasedOnSrcFormat;

      public void Initialize(uint stackSize)
      {
        this.m_alphaTest = new TCStack<bool>();
        this.m_blend = new TCStack<bool>();
        this.m_blendArg = new TCStack<ICGraphics2d.BlendArg>();
        this.m_color = new TCStack<CGraphics2dAbstract.ColorPkg>();
        this.m_colorKeyTest = new TCStack<bool>();
        this.m_configStateBasedOnSrcFormat = new TCStack<bool>();
        this.m_alphaTest.Initialize(stackSize);
        this.m_blend.Initialize(stackSize);
        this.m_blendArg.Initialize(stackSize);
        this.m_color.Initialize(stackSize);
        this.m_colorKeyTest.Initialize(stackSize);
        this.m_configStateBasedOnSrcFormat.Initialize(stackSize);
      }

      public void Destroy()
      {
        this.m_alphaTest.Destroy();
        this.m_blend.Destroy();
        this.m_blendArg.Destroy();
        this.m_color.Destroy();
        this.m_colorKeyTest.Destroy();
        this.m_configStateBasedOnSrcFormat.Destroy();
      }

      public void Push(ICGraphics2d.State state)
      {
        switch (state)
        {
          case ICGraphics2d.State.AlphaTest:
            this.m_alphaTest.Push();
            break;
          case ICGraphics2d.State.Blend:
            this.m_blend.Push();
            this.m_blendArg.Push();
            break;
          case ICGraphics2d.State.Color:
            this.m_color.Push();
            break;
          case ICGraphics2d.State.ColorKeyTest:
            this.m_colorKeyTest.Push();
            break;
          case ICGraphics2d.State.ConfigStateBasedOnSrcFormat:
            this.m_configStateBasedOnSrcFormat.Push();
            break;
        }
      }

      public void Pop(ICGraphics2d.State state)
      {
        switch (state)
        {
          case ICGraphics2d.State.AlphaTest:
            this.m_alphaTest.Pop();
            break;
          case ICGraphics2d.State.Blend:
            this.m_blend.Pop();
            this.m_blendArg.Pop();
            break;
          case ICGraphics2d.State.Color:
            this.m_color.Pop();
            break;
          case ICGraphics2d.State.ColorKeyTest:
            this.m_colorKeyTest.Pop();
            break;
          case ICGraphics2d.State.ConfigStateBasedOnSrcFormat:
            this.m_configStateBasedOnSrcFormat.Pop();
            break;
        }
      }
    }

    protected struct Matrix(int mtx00, int mtx01, int mtx10, int mtx11, int x, int y)
    {
      public const int FPRECISION = 16;
      public const float FPRECISIONToFloat = 1.52587891E-05f;
      public const float FPRECISIONToFloatSqr = 2.32830644E-10f;
      public static CGraphics2dAbstract.Matrix Identity = new CGraphics2dAbstract.Matrix(65536, 0, 0, 65536, 0, 0);
      public int m00 = mtx00;
      public int m01 = mtx01;
      public int m10 = mtx10;
      public int m11 = mtx11;
      public int t0 = x;
      public int t1 = y;

      public void loadIdentity()
      {
        this.m00 = 65536;
        this.m10 = 0;
        this.m01 = 0;
        this.m11 = 65536;
        this.t0 = 0;
        this.t1 = 0;
      }

      public void xform(int vx, int vy, int[] outv)
      {
        int num1 = this.smult16(vx, this.m00) + this.smult16(vy, this.m01);
        int num2 = this.smult16(vx, this.m10) + this.smult16(vy, this.m11);
        outv[0] = num1 + this.t0;
        outv[1] = num2 + this.t1;
      }

      public void xform_vertex2(int x, int y, out int xp, out int yp)
      {
        xp = this.smult16(x, this.m00) + this.smult16(y, this.m01) + this.t0;
        yp = this.smult16(x, this.m10) + this.smult16(y, this.m11) + this.t1;
      }

      public void precat_T(int tx, int ty) => this.xform_vertex2(tx, ty, out this.t0, out this.t1);

      public void precat_R(CGraphics2dAbstract.Matrix mp2)
      {
        int m00 = this.m00;
        int m10 = this.m10;
        this.m00 = this.smult16(mp2.m00, m00) + this.smult16(mp2.m10, this.m01);
        this.m10 = this.smult16(mp2.m00, m10) + this.smult16(mp2.m10, this.m11);
        this.m01 = this.smult16(mp2.m01, m00) + this.smult16(mp2.m11, this.m01);
        this.m11 = this.smult16(mp2.m01, m10) + this.smult16(mp2.m11, this.m11);
      }

      public void precat_S(int sx, int sy)
      {
        this.m00 = this.smult16(this.m00, sx);
        this.m01 = this.smult16(this.m01, sy);
        this.m10 = this.smult16(this.m10, sx);
        this.m11 = this.smult16(this.m11, sy);
      }

      public void rotate(int angle)
      {
        int num1 = CMathFixed.Sin(angle);
        int num2 = CMathFixed.Cos(angle);
        CGraphics2dAbstract.Matrix mp2 = new CGraphics2dAbstract.Matrix()
        {
          m00 = num2,
          m01 = -num1,
          m10 = num1,
          m11 = num2
        };
        mp2.t0 = mp2.t1 = 0;
        this.precat_R(mp2);
      }

      public void load(int mtx00, int mtx01, int mtx10, int mtx11, int x, int y)
      {
        this.m00 = mtx00;
        this.m01 = mtx01;
        this.m10 = mtx10;
        this.m11 = mtx11;
        this.t0 = x;
        this.t1 = y;
      }

      public void multiply(int mtx00, int mtx01, int mtx10, int mtx11, int x, int y)
      {
        this.xform_vertex2(x, y, out this.t0, out this.t1);
        int m00 = this.m00;
        int m10 = this.m10;
        this.m00 = this.smult16(mtx00, m00) + this.smult16(mtx10, this.m01);
        this.m10 = this.smult16(mtx01, m10) + this.smult16(mtx10, this.m11);
        this.m01 = this.smult16(mtx00, m00) + this.smult16(mtx11, this.m01);
        this.m11 = this.smult16(mtx01, m10) + this.smult16(mtx11, this.m11);
      }

      public void decompose(out CVector2d trans, out int rot, out CVector2d scale)
      {
        rot = 0;
        trans = new CVector2d(this.t0, this.t1);
        scale = new CVector2d(CMathFixed.Sqrt((int) ((long) this.m00 * (long) this.m00 + (long) this.m10 * (long) this.m10 >> 16)), CMathFixed.Sqrt((int) ((long) this.m01 * (long) this.m01 + (long) this.m11 * (long) this.m11 >> 16)));
        if (scale.m_i != 0 && scale.m_j != 0)
          rot = CMathFixed.ATan2(CMathFixed.Div(this.m01, scale.m_j), CMathFixed.Div(this.m00, scale.m_i));
        else
          rot = 0;
      }

      private int smult16(int x, int y) => (int) ((long) x * (long) y >> 16);
    }

    protected enum TransformCondition
    {
      Identity = 0,
      Rotate = 1,
      Scale = 2,
      Translate = 4,
    }
  }
}
