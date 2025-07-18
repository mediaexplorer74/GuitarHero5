// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieSprite
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class CMovieSprite : CMovieObject
  {
    public Vector<CMovieSprite.KeyFrame> m_KeyFrames = new Vector<CMovieSprite.KeyFrame>();
    public int m_Rotation;
    public int m_Scale;
    public int m_Alpha;
    public short m_X;
    public short m_Y;
    public short m_BoundsX;
    public short m_BoundsY;
    public short m_Width;
    public short m_Height;
    public byte m_Archetype;
    public byte m_Character;
    public byte m_Animation;
    public byte m_Frame;
    public bool m_Visible;
    public static readonly uint kResMaxTimePerUpdateMS = 100;
    public CRectangle boundsA;
    public CRectangle boundsB;
    public SG_Presenter player = new SG_Presenter();

    public override void GetBounds(ref CRectangle pRect)
    {
      pRect.Set((int) this.m_BoundsX, (int) this.m_BoundsY, (int) this.m_Width, (int) this.m_Height);
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
          this.m_KeyFrames[index] = new CMovieSprite.KeyFrame();
        CMovieSprite.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.x = pStream.ReadInt16();
        keyFrame.y = pStream.ReadInt16();
        keyFrame.anchor = pStream.ReadUInt8();
        keyFrame.z = pStream.ReadUInt8();
        keyFrame.archeTypeID = pStream.ReadUInt8();
        keyFrame.characterID = pStream.ReadUInt8();
        keyFrame.animationID = pStream.ReadUInt8();
        keyFrame.loop = pStream.ReadUInt8() == (byte) 1;
        keyFrame.visible = pStream.ReadUInt8() == (byte) 1;
        keyFrame.alpha = pStream.ReadInt32();
        keyFrame.scale = pStream.ReadInt32();
        keyFrame.rotation = pStream.ReadInt32();
      }
    }

    public override void Load()
    {
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        lock (CGameApp.loadQueuedLock)
        {
          SG_Home.GetInstance().QueueArchetypeCharacter((int) this.m_KeyFrames[index].archeTypeID, (int) this.m_KeyFrames[index].characterID);
          do
            ;
          while (SG_Home.GetInstance().LoadQueued(CMovieSprite.kResMaxTimePerUpdateMS, out bool _));
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
        CMovieSprite.KeyFrame keyFrame = (CMovieSprite.KeyFrame) CMovieObject.pp[0];
        CMovieSprite.KeyFrame pB = (CMovieSprite.KeyFrame) CMovieObject.pp[1];
        if (keyFrame == null || !keyFrame.visible)
        {
          this.m_Visible = false;
        }
        else
        {
          int interpolationTime = this.GetInterpolationTime(time1, (CMovieObject.KeyFrame) keyFrame, (CMovieObject.KeyFrame) pB);
          this.m_Alpha = CMathFixed.Lerp(keyFrame.alpha, pB.alpha, interpolationTime);
          this.m_Scale = CMathFixed.Lerp(keyFrame.scale, pB.scale, interpolationTime);
          this.m_Rotation = CMathFixed.Lerp(keyFrame.rotation, pB.rotation, interpolationTime);
          this.m_Archetype = keyFrame.archeTypeID;
          this.m_Character = keyFrame.characterID;
          this.m_Animation = keyFrame.animationID;
          this.m_Frame = (byte) this.GetCurrentFrame(keyFrame, time1);
          this.player.SetArchetypeCharacter((int) keyFrame.archeTypeID, (int) keyFrame.characterID);
          this.player.SetAnimation((int) keyFrame.animationID);
          this.player.Bounds(ref this.boundsA);
          if ((int) pB.archeTypeID == (int) keyFrame.archeTypeID && (int) pB.characterID == (int) keyFrame.characterID && (int) pB.animationID == (int) keyFrame.animationID)
          {
            this.boundsB = this.boundsA;
          }
          else
          {
            this.player.SetArchetypeCharacter((int) pB.archeTypeID, (int) pB.characterID);
            this.player.SetAnimation((int) pB.animationID);
            this.player.Bounds(ref this.boundsB);
          }
          int begin1 = CMathFixed.Int32ToFixed(this.CalculateLeft((byte) ((uint) keyFrame.anchor & (uint) ~CMovieObject.ANCHOR_HOT_SPOT_HCENTER), (int) keyFrame.x, this.boundsA.m_dx, this.m_AnchorObject));
          int begin2 = CMathFixed.Int32ToFixed(this.CalculateTop((byte) ((uint) keyFrame.anchor & (uint) ~CMovieObject.ANCHOR_HOT_SPOT_VCENTER), (int) keyFrame.y, this.boundsA.m_dy, this.m_AnchorObject));
          int end1 = CMathFixed.Int32ToFixed(this.CalculateLeft((byte) ((uint) pB.anchor & (uint) ~CMovieObject.ANCHOR_HOT_SPOT_HCENTER), (int) pB.x, this.boundsB.m_dx, this.m_AnchorObject));
          int end2 = CMathFixed.Int32ToFixed(this.CalculateTop((byte) ((uint) pB.anchor & (uint) ~CMovieObject.ANCHOR_HOT_SPOT_VCENTER), (int) pB.y, this.boundsB.m_dy, this.m_AnchorObject));
          int int32_1 = CMathFixed.FixedToInt32(CMathFixed.Lerp(begin1, end1, interpolationTime));
          int int32_2 = CMathFixed.FixedToInt32(CMathFixed.Lerp(begin2, end2, interpolationTime));
          int x1 = ((int) keyFrame.anchor & CMovieObject.ANCHOR_HOT_SPOT_HCENTER) != 0 ? 0 : this.boundsA.m_x;
          int y1 = ((int) keyFrame.anchor & CMovieObject.ANCHOR_HOT_SPOT_VCENTER) != 0 ? 0 : this.boundsA.m_y;
          int x2 = ((int) pB.anchor & CMovieObject.ANCHOR_HOT_SPOT_HCENTER) != 0 ? 0 : this.boundsB.m_x;
          int y2 = ((int) pB.anchor & CMovieObject.ANCHOR_HOT_SPOT_VCENTER) != 0 ? 0 : this.boundsB.m_y;
          int int32_3 = CMathFixed.FixedToInt32(CMathFixed.Lerp(CMathFixed.Int32ToFixed(x1), CMathFixed.Int32ToFixed(x2), interpolationTime));
          int int32_4 = CMathFixed.FixedToInt32(CMathFixed.Lerp(CMathFixed.Int32ToFixed(y1), CMathFixed.Int32ToFixed(y2), interpolationTime));
          this.m_X = (short) (int32_1 - int32_3);
          this.m_Y = (short) (int32_2 - int32_4);
          this.m_BoundsX = (short) ((int) this.m_X + this.boundsA.m_x);
          this.m_BoundsY = (short) ((int) this.m_Y + this.boundsA.m_y);
          this.m_Width = (short) this.boundsA.m_dx;
          this.m_Height = (short) this.boundsA.m_dy;
          this.m_Visible = true;
        }
      }
    }

    public override void Draw()
    {
      if (!this.m_Visible)
        return;
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      instance.PushState(ICGraphics2d.State.Blend);
      if (this.m_Alpha != CMathFixed.FloatToFixed(1f))
      {
        instance.Enable(ICGraphics2d.State.Blend);
        instance.SetBlendArg(ICGraphics2d.BlendArg.ConstAlphaInvConstAlphaAdd);
      }
      instance.PushTransform();
      instance.LoadIdentity();
      instance.Translate(CMathFixed.Int32ToFixed((int) this.m_X + (int) this.m_pMovie.X), CMathFixed.Int32ToFixed((int) this.m_Y + (int) this.m_pMovie.Y));
      instance.SetColor(this.m_Alpha, CMathFixed.FloatToFixed(1f), CMathFixed.FloatToFixed(1f), CMathFixed.FloatToFixed(1f));
      instance.Rotate(this.m_Rotation);
      instance.Scale(this.m_Scale, this.m_Scale);
      this.player.Reset();
      this.player.SetArchetypeCharacter((int) this.m_Archetype, (int) this.m_Character);
      this.player.SetAnimation((int) this.m_Animation);
      this.player.SetFrameIndex((int) this.m_Frame);
      this.player.Draw(0, 0);
      if (this.m_Alpha != CMathFixed.FloatToFixed(1f))
        instance.Disable(ICGraphics2d.State.Blend);
      instance.PopState(ICGraphics2d.State.Blend);
      instance.PopTransform();
    }

    public uint GetCurrentFrame(CMovieSprite.KeyFrame pCurrent, uint timeMS)
    {
      int num1 = this.m_KeyFrames.IndexOf(pCurrent);
      int timeMs = (int) pCurrent.timeMS;
      for (; num1 != 0; --num1)
      {
        CMovieSprite.KeyFrame keyFrame = this.m_KeyFrames[num1 - 1];
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
      public byte z;
      public byte archeTypeID;
      public byte characterID;
      public byte animationID;
      public byte anchor;
      public bool loop;
      public bool visible;
      public int alpha;
      public int scale;
      public int rotation;
    }
  }
}
