// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieFill
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMovieFill : CMovieObject
  {
    protected Vector<CMovieFill.KeyFrame> m_KeyFrames = new Vector<CMovieFill.KeyFrame>();
    protected short m_X;
    protected short m_Y;
    protected short m_Width;
    protected short m_Height;
    protected bool m_Visible;
    protected short m_r1;
    protected short m_g1;
    protected short m_b1;
    protected short m_r2;
    protected short m_g2;
    protected short m_b2;

    public override void GetBounds(ref CRectangle pRect)
    {
      pRect.Set((int) this.m_X, (int) this.m_Y, (int) this.m_Width, (int) this.m_Height);
    }

    public override CMovieObject.KeyFrame GetKeyFrame(int i)
    {
      return (CMovieObject.KeyFrame) this.m_KeyFrames[i];
    }

    public override void Init(CMovie pMovie, CInputStream pStream)
    {
      base.Init(pMovie, pStream);
      this.m_KeyFrames.Allocate(pStream.ReadUInt16());
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CMovieFill.KeyFrame();
        CMovieFill.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.x = pStream.ReadInt16();
        keyFrame.y = pStream.ReadInt16();
        keyFrame.anchor = pStream.ReadUInt8();
        keyFrame.z = pStream.ReadUInt8();
        keyFrame.width = (short) pStream.ReadUInt16();
        keyFrame.height = (short) pStream.ReadUInt16();
        keyFrame.visible = pStream.ReadUInt8() == (byte) 1;
        keyFrame.m_r1 = (short) pStream.ReadUInt8();
        keyFrame.m_g1 = (short) pStream.ReadUInt8();
        keyFrame.m_b1 = (short) pStream.ReadUInt8();
        keyFrame.m_r2 = (short) pStream.ReadUInt8();
        keyFrame.m_g2 = (short) pStream.ReadUInt8();
        keyFrame.m_b2 = (short) pStream.ReadUInt8();
      }
      this.m_X = (short) 0;
      this.m_Y = (short) 0;
      this.m_Width = (short) 0;
      this.m_Height = (short) 0;
      this.m_Visible = false;
      this.m_r1 = (short) 0;
      this.m_g1 = (short) 0;
      this.m_b1 = (short) 0;
      this.m_r2 = (short) 0;
      this.m_g2 = (short) 0;
      this.m_b2 = (short) 0;
    }

    public override void Load()
    {
    }

    public override void Refresh(uint time0, uint time1)
    {
      if (!this.m_pMovie.IsVisible())
      {
        this.m_Visible = false;
      }
      else
      {
        this.GetKeyFrames(time1, (CMovieObject) this, (uint) this.m_KeyFrames.Length());
        CMovieFill.KeyFrame pA = (CMovieFill.KeyFrame) CMovieObject.pp[0];
        CMovieFill.KeyFrame pB = (CMovieFill.KeyFrame) CMovieObject.pp[1];
        if (pA == null || !pA.visible)
        {
          this.m_Visible = false;
        }
        else
        {
          int interpolationTime = this.GetInterpolationTime(time1, (CMovieObject.KeyFrame) pA, (CMovieObject.KeyFrame) pB);
          int num1 = CMathFixed.Int32ToFixed(this.CalculateWidth(pA.anchor, (int) pA.width, 1024));
          int num2 = CMathFixed.Int32ToFixed(this.CalculateHeight(pA.anchor, (int) pA.height, 1024));
          int num3 = CMathFixed.Int32ToFixed(this.CalculateWidth(pB.anchor, (int) pB.width, 1024));
          int num4 = CMathFixed.Int32ToFixed(this.CalculateHeight(pB.anchor, (int) pB.height, 1024));
          this.m_Width = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(num1, num3, interpolationTime));
          this.m_Height = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(num2, num4, interpolationTime));
          int begin1 = CMathFixed.Int32ToFixed(this.CalculateLeft(pA.anchor, (int) pA.x, CMathFixed.FixedToInt32(num1), this.m_AnchorObject));
          int begin2 = CMathFixed.Int32ToFixed(this.CalculateTop(pA.anchor, (int) pA.y, CMathFixed.FixedToInt32(num2), this.m_AnchorObject));
          int end1 = CMathFixed.Int32ToFixed(this.CalculateLeft(pB.anchor, (int) pB.x, CMathFixed.FixedToInt32(num3), this.m_AnchorObject));
          int end2 = CMathFixed.Int32ToFixed(this.CalculateTop(pB.anchor, (int) pB.y, CMathFixed.FixedToInt32(num4), this.m_AnchorObject));
          this.m_X = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(begin1, end1, interpolationTime));
          this.m_Y = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(begin2, end2, interpolationTime));
          this.m_r1 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_r1 + interpolationTime * (int) pB.m_r1);
          this.m_g1 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_g1 + interpolationTime * (int) pB.m_g1);
          this.m_b1 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_b1 + interpolationTime * (int) pB.m_b1);
          this.m_r2 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_r2 + interpolationTime * (int) pB.m_r2);
          this.m_g2 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_g2 + interpolationTime * (int) pB.m_g2);
          this.m_b2 = (short) CMathFixed.FixedToInt32((CMathFixed.Int32ToFixed(1) - interpolationTime) * (int) pA.m_b2 + interpolationTime * (int) pB.m_b2);
          this.m_Visible = pA.visible;
        }
      }
    }

    public static uint Make(byte r, byte g, byte b)
    {
      return CMovieFill.Color_MakeA8R8G8B8(byte.MaxValue, r, g, b);
    }

    public static uint Make(byte a, byte r, byte g, byte b)
    {
      return CMovieFill.Color_MakeA8R8G8B8(a, r, g, b);
    }

    public static uint Color_MakeA8R8G8B8(byte a, byte r, byte g, byte b)
    {
      return (uint) ((int) b | (int) g << 8 | (int) r << 16 | (int) a << 24);
    }

    public override void Draw()
    {
      CRectangle rect;
      rect.m_x = (int) this.m_X + (int) this.m_pMovie.X;
      rect.m_y = (int) this.m_Y + (int) this.m_pMovie.Y;
      rect.m_dx = (int) this.m_Width;
      rect.m_dy = (int) this.m_Height;
      int num1 = (int) this.m_r2 - (int) this.m_r1;
      int num2 = (int) this.m_g2 - (int) this.m_g1;
      int num3 = (int) this.m_b2 - (int) this.m_b1;
      if (rect.m_dy <= 1 || num1 == 0 && num2 == 0 && num3 == 0)
      {
        CDrawUtil.FillRect(rect, CMovieFill.Make((byte) this.m_r1, (byte) this.m_g1, (byte) this.m_b1));
      }
      else
      {
        int num4 = rect.m_dy - 1;
        for (int index = 0; index < rect.m_dy; ++index)
        {
          int r = index * num1 / num4 + (int) this.m_r1;
          int g = index * num2 / num4 + (int) this.m_g1;
          int b = index * num3 / num4 + (int) this.m_b1;
          CDrawUtil.FillRect(rect.m_x, rect.m_y + index, rect.m_dx, 1, CMovieFill.Make((byte) r, (byte) g, (byte) b));
        }
      }
    }

    public new class KeyFrame : CMovieObject.KeyFrame
    {
      public ushort animationTimeMS;
      public short x;
      public short y;
      public byte anchor;
      public byte z;
      public short width;
      public short height;
      public bool visible;
      public short length;
      public byte stringID;
      public short m_r1;
      public short m_g1;
      public short m_b1;
      public short m_r2;
      public short m_g2;
      public short m_b2;
    }
  }
}
