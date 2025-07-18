// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieTiledSprite
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class CMovieTiledSprite : CMovieObject
  {
    protected Vector<CMovieTiledSprite.KeyFrame> m_KeyFrames = new Vector<CMovieTiledSprite.KeyFrame>();
    protected int m_Alpha;
    protected int m_U;
    protected int m_V;
    protected short m_X;
    protected short m_Y;
    protected short m_Width;
    protected short m_Height;
    protected byte m_Archetype;
    protected byte m_Character;
    protected byte m_Animation;
    protected byte m_Frame;
    protected bool m_Visible;
    private SG_Presenter player = new SG_Presenter();
    private CRectangle b;
    public CRectangle bounds;

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
      this.m_Visible = false;
      this.m_KeyFrames.Allocate(pStream.ReadUInt16());
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CMovieTiledSprite.KeyFrame();
        CMovieTiledSprite.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.x = pStream.ReadInt16();
        keyFrame.y = pStream.ReadInt16();
        keyFrame.anchor = pStream.ReadUInt8();
        keyFrame.width = (short) pStream.ReadUInt16();
        keyFrame.height = (short) pStream.ReadUInt16();
        keyFrame.visible = pStream.ReadUInt8() == (byte) 1;
        keyFrame.archeTypeID = pStream.ReadUInt8();
        keyFrame.characterID = pStream.ReadUInt8();
        keyFrame.animationID = pStream.ReadUInt8();
        keyFrame.loop = pStream.ReadUInt8() == (byte) 1;
        keyFrame.alpha = pStream.ReadInt32();
        keyFrame.u = pStream.ReadInt32();
        keyFrame.v = pStream.ReadInt32();
      }
    }

    public override void Load()
    {
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        lock (CGameApp.loadQueuedLock)
        {
          SG_Home.GetInstance().QueueArchetypeCharacter((int) this.m_KeyFrames[index].archeTypeID, (int) this.m_KeyFrames[index].characterID);
          bool out_success = true;
          do
            ;
          while (SG_Home.GetInstance().LoadQueued(CMovieSprite.kResMaxTimePerUpdateMS, out out_success));
        }
      }
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
        CMovieTiledSprite.KeyFrame keyFrame = (CMovieTiledSprite.KeyFrame) CMovieObject.pp[0];
        CMovieTiledSprite.KeyFrame pB = (CMovieTiledSprite.KeyFrame) CMovieObject.pp[1];
        if (keyFrame == null || !keyFrame.visible)
        {
          this.m_Visible = false;
        }
        else
        {
          this.m_Archetype = keyFrame.archeTypeID;
          this.m_Character = keyFrame.characterID;
          this.m_Animation = keyFrame.animationID;
          this.player.SetArchetypeCharacter((int) this.m_Archetype, (int) this.m_Character);
          this.player.SetAnimation((int) this.m_Animation);
          this.player.SetFrameIndex(0);
          this.player.Bounds(ref this.b);
          int interpolationTime = this.GetInterpolationTime(time1, (CMovieObject.KeyFrame) keyFrame, (CMovieObject.KeyFrame) pB);
          int num1 = CMathFixed.Int32ToFixed(this.CalculateWidth(keyFrame.anchor, (int) keyFrame.width, this.b.m_dx));
          int num2 = CMathFixed.Int32ToFixed(this.CalculateHeight(keyFrame.anchor, (int) keyFrame.height, this.b.m_dy));
          int num3 = CMathFixed.Int32ToFixed(this.CalculateWidth(pB.anchor, (int) pB.width, this.b.m_dx));
          int num4 = CMathFixed.Int32ToFixed(this.CalculateHeight(pB.anchor, (int) pB.height, this.b.m_dy));
          this.m_Width = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(num1, num3, interpolationTime));
          this.m_Height = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(num2, num4, interpolationTime));
          int begin1 = CMathFixed.Int32ToFixed(this.CalculateLeft(keyFrame.anchor, (int) keyFrame.x, CMathFixed.FixedToInt32(num1), this.m_AnchorObject));
          int begin2 = CMathFixed.Int32ToFixed(this.CalculateTop(keyFrame.anchor, (int) keyFrame.y, CMathFixed.FixedToInt32(num2), this.m_AnchorObject));
          int end1 = CMathFixed.Int32ToFixed(this.CalculateLeft(pB.anchor, (int) pB.x, CMathFixed.FixedToInt32(num3), this.m_AnchorObject));
          int end2 = CMathFixed.Int32ToFixed(this.CalculateTop(pB.anchor, (int) pB.y, CMathFixed.FixedToInt32(num4), this.m_AnchorObject));
          this.m_X = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(begin1, end1, interpolationTime));
          this.m_Y = (short) CMathFixed.FixedToInt32(CMathFixed.Lerp(begin2, end2, interpolationTime));
          this.m_Alpha = CMathFixed.Lerp(keyFrame.alpha, pB.alpha, interpolationTime);
          this.m_U = CMathFixed.Lerp(keyFrame.u, pB.u, interpolationTime);
          this.m_V = CMathFixed.Lerp(keyFrame.v, pB.v, interpolationTime);
          this.m_Frame = (byte) this.GetCurrentFrame(keyFrame, time1);
          this.m_Visible = true;
        }
      }
    }

    public override void Draw()
    {
      if (!this.m_Visible)
        return;
      this.player.SetArchetypeCharacter((int) this.m_Archetype, (int) this.m_Character);
      this.player.SetAnimation((int) this.m_Animation);
      this.player.SetFrameIndex((int) this.m_Frame);
      this.player.Bounds(ref this.bounds);
      int v1 = CMathFixed.FloatToFixed(1f) - this.m_U;
      int v2 = CMathFixed.FloatToFixed(1f) - this.m_V;
      uint num1 = (uint) ((int) this.m_Width / this.bounds.m_dx + (this.bounds.m_dx % (int) this.m_Width == 1 ? 1 : 0) + (v1 % CMathFixed.FloatToFixed(1f) == 1 ? 1 : 0));
      uint num2 = (uint) ((int) this.m_Height / this.bounds.m_dy + (this.bounds.m_dy % (int) this.m_Height == 1 ? 1 : 0) + (v2 % CMathFixed.FloatToFixed(1f) == 1 ? 1 : 0));
      short int32_1 = (short) CMathFixed.FixedToInt32(CMathFixed.Mul(v1 - CMathFixed.Int32ToFixed(CMathFixed.FixedToInt32(v1)), CMathFixed.Int32ToFixed(-this.bounds.m_dx)));
      short int32_2 = (short) CMathFixed.FixedToInt32(CMathFixed.Mul(v2 - CMathFixed.Int32ToFixed(CMathFixed.FixedToInt32(v2)), CMathFixed.Int32ToFixed(-this.bounds.m_dy)));
      short num3 = (short) ((int) this.m_X + -this.bounds.m_x + (int) int32_1 + (int) this.m_pMovie.X);
      short num4 = (short) ((int) this.m_Y + -this.bounds.m_y + (int) int32_2 + (int) this.m_pMovie.Y);
      for (uint index1 = 0; index1 < num2; ++index1)
      {
        for (uint index2 = 0; index2 < num1; ++index2)
          this.player.Draw((int) ((long) index2 * (long) this.bounds.m_dx + (long) num3), (int) ((long) index1 * (long) this.bounds.m_dy + (long) num4));
      }
    }

    protected uint GetCurrentFrame(CMovieTiledSprite.KeyFrame pCurrent, uint timeMS)
    {
      int num1 = this.m_KeyFrames.IndexOf(pCurrent);
      int timeMs = (int) pCurrent.timeMS;
      for (; num1 != 0; --num1)
      {
        CMovieTiledSprite.KeyFrame keyFrame = this.m_KeyFrames[num1 - 1];
        if ((int) pCurrent.archeTypeID == (int) keyFrame.archeTypeID && (int) pCurrent.characterID == (int) keyFrame.characterID && (int) pCurrent.animationID == (int) keyFrame.animationID)
          timeMs = (int) keyFrame.timeMS;
        else
          break;
      }
      int num2 = (int) ((long) timeMS - (long) timeMs);
      SG_Animation animation = SG_Home.GetInstance().GetArchetype(pCurrent.archeTypeID).GetAnimation((int) pCurrent.animationID);
      uint num3 = 0;
      for (uint currentFrame = 0; currentFrame < (uint) animation.timedframeCount; ++currentFrame)
      {
        if ((int) animation.timedframeID__to__duration[(int) currentFrame] >= num2)
          return currentFrame;
        num3 += (uint) animation.timedframeID__to__duration[(int) currentFrame];
        num2 -= (int) animation.timedframeID__to__duration[(int) currentFrame];
      }
      int num4 = (int) ((long) num2 % (long) num3);
      for (uint currentFrame = 0; currentFrame < (uint) animation.timedframeCount; ++currentFrame)
      {
        if ((int) animation.timedframeID__to__duration[(int) currentFrame] >= num4)
          return currentFrame;
        num3 += (uint) animation.timedframeID__to__duration[(int) currentFrame];
        num4 -= (int) animation.timedframeID__to__duration[(int) currentFrame];
      }
      return 0;
    }

    public new class KeyFrame : CMovieObject.KeyFrame
    {
      public ushort animationTimeMS;
      public short x;
      public short y;
      public byte anchor;
      public short width;
      public short height;
      public byte archeTypeID;
      public byte animationID;
      public byte characterID;
      public int u;
      public int v;
      public bool loop;
      public bool visible;
      public int alpha;
    }
  }
}
