// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieRegion
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMovieRegion : CMovieObject
  {
    protected Vector<CMovieRegion.KeyFrame> m_KeyFrames = new Vector<CMovieRegion.KeyFrame>();
    protected short m_X;
    protected short m_Y;
    protected short m_Width;
    protected short m_Height;
    protected bool m_Visible;
    protected object m_pCaller;
    protected MovieRegionCallback m_pCallback;
    private CRectangle area;

    public bool IsVisible() => this.m_Visible;

    public override void Init(CMovie pMovie, CInputStream pStream)
    {
      base.Init(pMovie, pStream);
      this.m_KeyFrames.Allocate(pStream.ReadUInt16());
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CMovieRegion.KeyFrame();
        CMovieRegion.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.x = pStream.ReadInt16();
        keyFrame.y = pStream.ReadInt16();
        keyFrame.anchor = pStream.ReadUInt8();
        keyFrame.z = pStream.ReadUInt8();
        keyFrame.width = (short) pStream.ReadUInt16();
        keyFrame.height = (short) pStream.ReadUInt16();
        keyFrame.visible = pStream.ReadUInt8() == (byte) 1;
        int num = (int) pStream.ReadUInt8();
      }
      this.m_pCallback = (MovieRegionCallback) null;
    }

    public override CMovieObject.KeyFrame GetKeyFrame(int i)
    {
      return (CMovieObject.KeyFrame) this.m_KeyFrames[i];
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
        CMovieRegion.KeyFrame pA = (CMovieRegion.KeyFrame) CMovieObject.pp[0];
        CMovieRegion.KeyFrame pB = (CMovieRegion.KeyFrame) CMovieObject.pp[1];
        if (pA == null)
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
          this.m_Visible = pA.visible;
        }
      }
    }

    public override void Draw()
    {
      if (this.m_pCallback == null)
        return;
      int userRegionId = this.m_pMovie.GetUserRegionID(this);
      this.area.Clear();
      this.GetRegion(ref this.area);
      this.m_pCallback(this.m_pCaller, userRegionId, ref this.area);
    }

    public void SetCallback(MovieRegionCallback callback, object pCaller)
    {
      this.m_pCallback = callback;
      this.m_pCaller = pCaller;
    }

    public bool GetRegion(ref CRectangle pRegion)
    {
      if (this.m_Visible)
      {
        pRegion.Set((int) this.m_pMovie.X + (int) this.m_X, (int) this.m_pMovie.Y + (int) this.m_Y, (int) this.m_Width, (int) this.m_Height);
        return true;
      }
      pRegion.Clear();
      return false;
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
    }
  }
}
