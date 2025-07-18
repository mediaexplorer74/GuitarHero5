// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Presenter
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class SG_Presenter
  {
    public ushort m_quantum;
    public bool m_needToScale;
    public byte m_archetypeID;
    public byte m_characterID;
    public ushort m_animationID;
    public byte m_transform;
    public short m_positionX;
    public short m_positionY;
    public byte m_drawCallbackPolicy;
    public ushort[] m_pDurationsArray;
    public bool m_loop;
    public sbyte m_direction;
    public int m_timer;
    public ushort m_timerPoleInitial;
    public ushort m_timerPoleFinal;
    public byte m_timedFrameCount;
    public byte m_timedFrameIndex;
    public ushort m_durationTotal;
    public bool m_collideOnlyWithCollisionRects;
    public SG_Home m_pSGHome;
    public static readonly bool DEBUGLOCAL_SET_ANIMATION;

    public ushort GetAnimationID() => this.m_animationID;

    public void GetArchetypeCharacter(out byte out_archetypeID, out byte out_characterID)
    {
      out_archetypeID = this.m_archetypeID;
      out_characterID = this.m_characterID;
    }

    public byte GetTransform() => this.m_transform;

    public short GetPositionX() => this.m_positionX;

    public short GetPositionY() => this.m_positionY;

    public void SetPositionX(short xPos) => this.m_positionX = xPos;

    public void SetPositionY(short yPos) => this.m_positionY = yPos;

    public void SetArchetypeCharacter(int archetypeID, int characterID)
    {
      SG_Home.ASSERT_CHECK_RANGE_ARCHETYPEID(archetypeID);
      SG_Home.ASSERT_CHECK_RANGE_CHARACTERID(characterID);
      this.m_archetypeID = (byte) archetypeID;
      this.m_characterID = (byte) characterID;
    }

    public void SetCharacter(int characterID)
    {
      SG_Home.ASSERT_CHECK_RANGE_CHARACTERID(characterID);
      this.m_characterID = (byte) characterID;
    }

    public SG_Animation GetAnimation()
    {
      return this.GetArchetype().GetAnimation((int) this.m_animationID);
    }

    public SG_Character GetCharacter()
    {
      return this.GetArchetype().GetCharacter((int) this.m_characterID);
    }

    public void SetAnimation(int animationID) => this.SetAnimation(animationID, this.m_loop);

    public void SetAnimation(int animationID, bool loop)
    {
      this.SetAnimation(animationID, loop, this.m_transform);
    }

    public void SetTransform(byte transform) => this.m_transform = transform;

    public void setScale(ushort scaleFactor)
    {
      this.m_quantum = scaleFactor;
      this.m_needToScale = this.m_quantum != (ushort) 1000;
    }

    public void Reset(int direction)
    {
      this.ResetPoles(direction);
      this.m_timer = (int) this.m_timerPoleInitial;
      this.m_direction = (sbyte) direction;
    }

    public void ResetPoles(int direction)
    {
      if (direction == 1)
      {
        this.m_timedFrameIndex = (byte) 0;
        this.m_timerPoleInitial = (ushort) 0;
        this.m_timerPoleFinal = this.m_pDurationsArray[0];
      }
      else
      {
        this.m_timedFrameIndex = (byte) ((uint) this.m_timedFrameCount - 1U);
        this.m_timerPoleInitial = this.m_durationTotal;
        this.m_timerPoleFinal = (ushort) ((uint) this.m_durationTotal - (uint) this.m_pDurationsArray[(int) this.m_timedFrameCount - 1]);
      }
    }

        public void Reverse()
        {
            ushort timerPoleInitial = this.m_timerPoleInitial;
            this.m_timerPoleInitial = this.m_timerPoleFinal;
            this.m_timerPoleFinal = timerPoleInitial;
            this.m_direction = (sbyte)(-this.m_direction);
        }

    public void SetPosition(short positionX, short positionY)
    {
      this.m_positionX = positionX;
      this.m_positionY = positionY;
    }

    public void SetPosition(int positionX, int positionY)
    {
      this.m_positionX = (short) positionX;
      this.m_positionY = (short) positionY;
    }

    public void SetDrawCallbackPolicy(byte drawCallbackPolicy)
    {
      this.m_drawCallbackPolicy = drawCallbackPolicy;
    }

    public void SetLoop(bool loop) => this.m_loop = loop;

    public void Reset() => this.Reset((int) this.m_direction);

    public void Finish()
    {
      this.Reset(-1 * (int) this.m_direction);
      this.Reverse();
    }

    public bool GetLoop() => this.m_loop;

    public bool HasFinished()
    {
      return !this.m_loop && (int) this.m_timedFrameIndex == (this.m_direction > (sbyte) 0 ? (int) this.m_timedFrameCount - 1 : 0) && this.m_timer == (int) this.m_timerPoleFinal;
    }

    public bool IsForward() => this.m_direction == (sbyte) 1;

    public int GetFrameIndex() => (int) this.m_timedFrameIndex;

    public int GetFrameCount() => (int) this.m_timedFrameCount;

    public int GetTimeTotalElapsed() => this.m_timer;

    public int GetTimeTotalDuration() => (int) this.m_durationTotal;

    public int GetTimeFrameElasped()
    {
      return (int) this.m_direction * (this.m_timer - (int) this.m_timerPoleInitial);
    }

    public int GetTimeFrameDuration() => (int) this.m_pDurationsArray[(int) this.m_timedFrameIndex];

    public void Draw() => this.Draw(0, 0);

    public SG_Presenter() => this.Init();

    public SG_Presenter(int archetypeID, int characterID)
    {
      this.Init();
      this.SetArchetypeCharacter(archetypeID, characterID);
    }

    public bool IsViable()
    {
      return this.m_pSGHome.IsInitialized() && this.m_pSGHome.IsLoadedArchetypeCharacter((int) this.m_archetypeID, (int) this.m_characterID) && this.m_pDurationsArray != null;
    }

    public void SetAnimation(int animationID, bool loop, byte transform)
    {
      if (SG_Presenter.DEBUGLOCAL_SET_ANIMATION)
        SG_Home.DDD("\\_/ SPRITEGLU \\_/: setAnimation() begin");
      SG_Archetype archetype = this.m_pSGHome.GetArchetype(this.m_archetypeID);
      archetype.GetCharacter((int) this.m_characterID);
      SG_Animation animation = archetype.GetAnimation(animationID);
      if (SG_Presenter.DEBUGLOCAL_SET_ANIMATION)
      {
        SG_Home.DDD(" |  SPRITEGLU  | : setAnimation() with archetype {0} ({1})", (object) this.m_archetypeID, (object) "archetype");
        SG_Home.DDD(" |  SPRITEGLU  | : setAnimation() with character {0} ({1})", (object) this.m_characterID, (object) "character");
        SG_Home.DDD(" |  SPRITEGLU  | : setAnimation() with animation {0} ({1})", (object) animationID, (object) "animation");
        SG_Home.DDD(" |  SPRITEGLU  | : setAnimation() with transform " + (object) transform);
      }
      this.m_animationID = (ushort) animationID;
      this.m_pDurationsArray = animation.timedframeID__to__duration;
      this.m_timedFrameCount = animation.timedframeCount;
      this.m_durationTotal = (ushort) 0;
      for (int index = 0; index < (int) this.m_timedFrameCount; ++index)
        this.m_durationTotal += this.m_pDurationsArray[index];
      this.SetLoop(loop);
      this.Reset(1);
      this.SetTransform(transform);
      if (!SG_Presenter.DEBUGLOCAL_SET_ANIMATION)
        return;
      SG_Home.DDD("{0}", (object) "/^\\ SPRITEGLU /^\\: setAnimation() return");
    }

    public bool Update(int elapsed)
    {
      if (!this.HasFinished() && elapsed != 0)
      {
        this.m_timer += (int) this.m_direction * elapsed;
        while (this.m_direction > (sbyte) 0 && this.m_timer >= (int) this.m_timerPoleFinal || this.m_direction < (sbyte) 0 && this.m_timer <= (int) this.m_timerPoleFinal)
        {
          if ((int) this.m_timedFrameIndex == (this.m_direction > (sbyte) 0 ? (int) this.m_timedFrameCount - 1 : 0))
          {
            if (this.m_loop)
            {
              this.m_timer -= (int) this.m_direction * (int) this.m_durationTotal;
              this.ResetPoles((int) this.m_direction);
            }
            else
            {
              this.m_timer = this.m_direction > (sbyte) 0 ? (int) this.m_durationTotal : 0;
              return true;
            }
          }
          else
          {
            this.m_timedFrameIndex += (byte) this.m_direction;
            this.m_timerPoleInitial = this.m_timerPoleFinal;
            this.m_timerPoleFinal += (ushort) ((uint) this.m_direction * (uint) this.m_pDurationsArray[(int) this.m_timedFrameIndex]);
          }
        }
      }
      return false;
    }

    public void Draw(int x, int y)
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      CRectangle clipRect = new CRectangle(0, 0, (int) (short) width, (int) (short) height);
      this.m_pSGHome.drawInit(x + (int) this.m_positionX, y + (int) this.m_positionY, clipRect);
      this.TraverseCurrentFrame(SG_TraverseMode.SG_TraverseMode_DRAW);
      this.m_pSGHome.drawResolve(this);
    }

    public void Bounds(ref CRectangle rect_out)
    {
      this.m_pSGHome.boundsInit();
      this.TraverseCurrentFrame(SG_TraverseMode.SG_TraverseMode_BOUNDS);
      this.m_pSGHome.boundsResolve(ref rect_out, this);
    }

    public short Collide(CRectangle collideWith)
    {
      this.m_pSGHome.collisionInit((short) (collideWith.m_x - (int) this.m_positionX), (short) (collideWith.m_y - (int) this.m_positionY), (short) collideWith.m_dx, (short) collideWith.m_dy, this);
      this.TraverseCurrentFrame(SG_TraverseMode.SG_TraverseMode_COLLIDE);
      return this.m_pSGHome.collisionResolve();
    }

    public void SetFrameIndex(int frameIndexNew)
    {
      this.Reset((int) this.m_direction);
      int elapsed = 0;
      int num = this.m_direction > (sbyte) 0 ? 0 : (int) this.m_timedFrameCount - 1;
      for (int index = 0; index < (int) this.m_direction * (frameIndexNew - num); ++index)
        elapsed += (int) this.m_pDurationsArray[(int) this.m_direction * (index - num)];
      this.Update(elapsed);
    }

    public void SynchAnimations(SG_Presenter pCloneSprite)
    {
      this.m_direction = pCloneSprite.m_direction;
      this.m_timer = pCloneSprite.m_timer;
      this.m_timerPoleInitial = pCloneSprite.m_timerPoleInitial;
      this.m_timerPoleFinal = pCloneSprite.m_timerPoleFinal;
      this.m_timedFrameCount = pCloneSprite.m_timedFrameCount;
      this.m_timedFrameIndex = pCloneSprite.m_timedFrameIndex;
    }

    public void TraverseCurrentFrame(SG_TraverseMode traverseMode)
    {
      SG_Archetype archetype = this.GetArchetype();
      this.GetCharacter();
      int num = (int) archetype.GetAnimation((int) this.m_animationID).timedframeID__to__frameID[(int) this.m_timedFrameIndex];
      if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_FRAME_PRE) != 0 && SG_Special.DrawSpecialFramePre(traverseMode, this, num))
        return;
      SG_Frame frame = archetype.GetFrame(num);
      for (int index1 = (int) frame.orderedlayerCount - 1; index1 >= 0; --index1)
      {
        ushort layerId = frame.orderedlayers[index1].layerID;
        SG_Layer layer = archetype.GetLayer((uint) layerId);
        int locationX = (int) frame.orderedlayers[index1].locationX;
        int locationY = (int) frame.orderedlayers[index1].locationY;
        if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_LAYER_PRE) == 0 || !SG_Special.DrawSpecialLayerPre(traverseMode, this, num, (int) layerId))
        {
          for (int index2 = (int) layer.positionedspriteCount - 1; index2 >= 0; --index2)
          {
            uint zCount = 1;
            SpriteDrawInfo[] spriteDrawInfos = (SpriteDrawInfo[]) null;
            SG_Layer.SG_Positionedsprite positionedsprite = layer.positionedsprites[index2];
            ushort spriteId1 = positionedsprite.spriteID;
            this.m_pSGHome.computeSpriteDrawInfos(out spriteDrawInfos, out zCount, (uint) this.m_archetypeID, (uint) this.m_characterID, spriteId1);
            for (uint index3 = 0; index3 < zCount; ++index3)
            {
              ushort spriteId2 = spriteDrawInfos[(int) index3].spriteID;
              int x = locationX + (int) positionedsprite.locationX + (int) spriteDrawInfos[(int) index3].offsetX;
              int y = locationY + (int) positionedsprite.locationY + (int) spriteDrawInfos[(int) index3].offsetY;
              uint width;
              uint height;
              this.m_pSGHome.GetSize((int) spriteId2, out width, out height);
              SquareTransform.TransformRectangle(this.m_transform, ref x, ref y, ref width, ref height);
              if (this.m_needToScale)
              {
                x = (int) this.m_quantum * x / 1000;
                y = (int) this.m_quantum * y / 1000;
                width = (uint) this.m_quantum * width / 1000U;
                height = (uint) this.m_quantum * height / 1000U;
              }
              if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_SPRITE_PRE) == 0 || !SG_Special.DrawSpecialSpritePre(traverseMode, this, num, (int) layerId, (int) spriteId2, x, y))
              {
                switch (traverseMode)
                {
                  case SG_TraverseMode.SG_TraverseMode_DRAW:
                    this.m_pSGHome.drawAccumulate(x, y, width, height, spriteId2, this.m_transform, (int) this.m_quantum);
                    break;
                  case SG_TraverseMode.SG_TraverseMode_COLLIDE:
                    if (!this.m_collideOnlyWithCollisionRects || this.m_pSGHome.IsCollisionSprite((int) spriteId2))
                    {
                      this.m_pSGHome.collisionAccumulate(x, y, (int) width, (int) height, (short) spriteId2);
                      break;
                    }
                    break;
                  default:
                    this.m_pSGHome.boundsAccumulate(x, y, (int) width, (int) height);
                    break;
                }
                if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_SPRITE_POST) != 0)
                  SG_Special.DrawSpecialSpritePost(traverseMode, this, num, (int) layerId, (int) spriteId2, x, y);
              }
            }
          }
          if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_LAYER_POST) != 0)
            SG_Special.DrawSpecialLayerPost(traverseMode, this, num, (int) layerId);
        }
      }
      if (((int) this.m_drawCallbackPolicy & (int) SG_Defines.DRAW_CALLBACK_FRAME_POST) == 0)
        return;
      SG_Special.DrawSpecialFramePost(traverseMode, this, num);
    }

    public SG_Archetype GetArchetype() => this.m_pSGHome.GetArchetype(this.m_archetypeID);

    public void Init()
    {
      this.m_archetypeID = (byte) 0;
      this.m_characterID = (byte) 0;
      this.m_animationID = (ushort) 0;
      this.m_transform = SquareTransform.TRANS_NONE;
      this.SetPosition(0, 0);
      this.SetDrawCallbackPolicy((byte) 0);
      this.m_collideOnlyWithCollisionRects = false;
      this.m_pDurationsArray = (ushort[]) null;
      this.m_loop = false;
      this.m_direction = (sbyte) 0;
      this.m_timer = 0;
      this.m_timerPoleInitial = (ushort) 0;
      this.m_timerPoleFinal = (ushort) 0;
      this.m_timedFrameCount = (byte) 0;
      this.m_timedFrameIndex = (byte) 0;
      this.m_durationTotal = (ushort) 0;
      this.m_quantum = (ushort) 1000;
      this.m_needToScale = false;
      this.m_pSGHome = SG_Home.GetInstance();
    }
  }
}
