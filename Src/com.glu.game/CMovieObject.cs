// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieObject
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMovieObject
  {
    protected static int ANCHOR_HOT_SPOT_HCENTER = 4;
    protected static int ANCHOR_HOT_SPOT_RIGHT = 8;
    protected static int ANCHOR_HOT_SPOT_VCENTER = 1;
    protected static int ANCHOR_HOT_SPOT_BOTTOM = 2;
    protected static int ANCHOR_POS_RIGHT = 32;
    protected static int ANCHOR_POS_BOTTOM = 16;
    protected static int ANCHOR_SIZE_RIGHT = 128;
    protected static int ANCHOR_SIZE_BOTTOM = 64;
    public byte Index;
    protected MovieObjectType m_Type;
    protected byte m_AnchorObject;
    protected CMovie m_pMovie;
    public static CMovieObject.KeyFrame[] pp = new CMovieObject.KeyFrame[2];
    private CRectangle bounds;

    public virtual void Init(CMovie pMovie, CInputStream pStream)
    {
      this.m_pMovie = pMovie;
      this.m_AnchorObject = pStream.ReadUInt8();
    }

    public virtual void Load()
    {
    }

    public virtual void Refresh(uint time0, uint time1)
    {
    }

    public virtual void Draw()
    {
    }

    public virtual void GetBounds(ref CRectangle pRect) => pRect.Clear();

    public MovieObjectType GetMovieObjectType() => this.m_Type;

    public byte GetAnchorObject() => this.m_AnchorObject;

    public virtual CMovieObject.KeyFrame GetKeyFrame(int i) => (CMovieObject.KeyFrame) null;

    protected void GetKeyFrames(uint timeMS, CMovieObject caller, uint numKeyFrames)
    {
      CMovieObject.pp[0] = (CMovieObject.KeyFrame) null;
      CMovieObject.pp[1] = (CMovieObject.KeyFrame) null;
      for (int i = 0; (long) i < (long) numKeyFrames; ++i)
      {
        CMovieObject.KeyFrame keyFrame = caller.GetKeyFrame(i);
        if (keyFrame.timeMS >= timeMS)
        {
          CMovieObject.pp[0] = i <= 0 ? ((int) keyFrame.timeMS != (int) timeMS ? (CMovieObject.KeyFrame) null : keyFrame) : caller.GetKeyFrame(i - 1);
          CMovieObject.pp[1] = keyFrame;
          return;
        }
      }
      CMovieObject.pp[0] = caller.GetKeyFrame((int) numKeyFrames - 1);
      CMovieObject.pp[1] = CMovieObject.pp[0];
    }

    protected int GetInterpolationTime(
      uint timeMS,
      CMovieObject.KeyFrame pA,
      CMovieObject.KeyFrame pB)
    {
      int denom = (int) pB.timeMS - (int) pA.timeMS;
      return denom == 0 ? 0 : CMathFixed.Div((int) timeMS - (int) pA.timeMS, denom);
    }

    protected int CalculateLeft(byte anchor, int x, int width, byte anchorObject)
    {
      if (anchorObject != byte.MaxValue)
      {
        this.m_pMovie.GetObject((int) anchorObject).GetBounds(ref this.bounds);
        if (((int) anchor & CMovieObject.ANCHOR_POS_RIGHT) != 0)
          x += this.bounds.GetRight();
        else
          x += this.bounds.GetLeft();
        if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_RIGHT) != 0)
          x -= width;
        else if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_HCENTER) != 0)
          x = this.bounds.GetCenterX() - width / 2;
      }
      else
      {
        if (((int) anchor & CMovieObject.ANCHOR_POS_RIGHT) != 0)
          x = (int) Phone.GetWidth() * x / this.m_pMovie.GetReferenceWidth();
        if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_HCENTER) != 0)
          x -= width / 2;
        else if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_RIGHT) != 0)
          x -= width;
      }
      return x;
    }

    protected int CalculateTop(byte anchor, int y, int height, byte anchorObject)
    {
      if (anchorObject != byte.MaxValue)
      {
        this.m_pMovie.GetObject((int) anchorObject).GetBounds(ref this.bounds);
        if (((int) anchor & CMovieObject.ANCHOR_POS_BOTTOM) != 0)
          y += this.bounds.GetBottom();
        else
          y += this.bounds.GetTop();
        if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_BOTTOM) != 0)
          y -= height;
        else if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_VCENTER) != 0)
          y -= height >> 1;
      }
      else
      {
        if (((int) anchor & CMovieObject.ANCHOR_POS_BOTTOM) != 0)
          y = ((int) Phone.GetHeight() + ((int) Phone.GetHeight() % 2 == 0 ? 0 : 1)) * y / this.m_pMovie.GetReferenceHeight();
        if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_VCENTER) != 0)
          y -= height / 2;
        else if (((int) anchor & CMovieObject.ANCHOR_HOT_SPOT_BOTTOM) != 0)
          y -= height;
      }
      return y;
    }

    protected int CalculateWidth(byte anchor, int width, int relativeWidth)
    {
      width = ((int) anchor & CMovieObject.ANCHOR_SIZE_RIGHT) == 0 ? width * relativeWidth >> 10 : (int) Phone.GetWidth() * width / this.m_pMovie.GetReferenceWidth();
      return width;
    }

    protected int CalculateHeight(byte anchor, int height, int relativeHeight)
    {
      height = ((int) anchor & CMovieObject.ANCHOR_SIZE_BOTTOM) == 0 ? height * relativeHeight >> 10 : ((int) Phone.GetHeight() + ((int) Phone.GetHeight() % 2 == 0 ? 0 : 1)) * height / this.m_pMovie.GetReferenceHeight();
      return height;
    }

    public class KeyFrame
    {
      public uint timeMS;
    }
  }
}
