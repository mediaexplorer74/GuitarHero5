// Decompiled with JetBrains decompiler
// Type: com.glu.game.CStarParticle
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  internal class CStarParticle : CClass
  {
    public const int STAR_PARTICLE_DEFAULT_SCREEN_WIDTH = 320;
    public const int STAR_PARTICLE_DEFAULT_SCREEN_HEIGHT = 480;
    public int STAR_PARTICLE_BASE_ACTIVE_TIME_IN_MS = 1600;
    public int STAR_PARTICLE_ACTIVE_TIME_RANGE_IN_MS = 400;
    public int STAR_PARTICLE_SPRITE_TYPES = 4;
    public int MS_IN_ONE_SECOND = 1000;
    public int STAR_PARTICLE_GRAVITY = CMathFixed.FloatToFixed(980f);
    public int STAR_MINIMUM_INITIAL_Y_VELOCITY = CMathFixed.FloatToFixed(-300f);
    public int STAR_POWER_ADDITIONAL_Y_VELOCITY = CMathFixed.FloatToFixed(-150f);
    public int STAR_RANDOM_RANGE_INITIAL_Y_VELOCITY = CMathFixed.FloatToFixed(200f);
    public static float STAR_MAXIMUM_INITIAL_X_VELOCITY_FLOAT_VALUE = 40f;
    public int STAR_MAXIMUM_INITIAL_X_VELOCITY = CMathFixed.FloatToFixed(CStarParticle.STAR_MAXIMUM_INITIAL_X_VELOCITY_FLOAT_VALUE);
    public int STAR_RANGE_INITIAL_X_VELOCITY = CMathFixed.FloatToFixed(CStarParticle.STAR_MAXIMUM_INITIAL_X_VELOCITY_FLOAT_VALUE * 2f);
    protected SG_Presenter m_pStarSprite;
    protected int m_xFixed;
    protected int m_yFixed;
    protected int m_xVelocity;
    protected int m_yVelocity;
    protected bool m_bIsActive;
    protected int m_lifespanInMS;
    protected int m_ageInMS;

    public void disable() => this.m_bIsActive = false;

    public bool IsActive() => this.m_bIsActive;

    public CStarParticle()
    {
      this.m_pStarSprite = new SG_Presenter();
      this.m_pStarSprite.SetArchetypeCharacter(1, 2);
      this.m_pStarSprite.SetAnimation(3);
      this.m_pStarSprite.SetLoop(true);
      this.m_bIsActive = false;
    }

    public static void StaticInitialization()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      CGHStaticData.GetInstance();
      CGHStaticData.m_velocityScaleX = CMathFixed.Div(CMathFixed.Int32ToFixed(width), CMathFixed.Int32ToFixed(320));
      CGHStaticData.m_velocityScaleY = CMathFixed.Div(CMathFixed.Int32ToFixed(height), CMathFixed.Int32ToFixed(480));
    }

    public void init(int xPos, int yPos, int percentage)
    {
      this.m_xFixed = CMathFixed.Int32ToFixed(xPos);
      this.m_yFixed = CMathFixed.Int32ToFixed(yPos);
      this.m_ageInMS = 0;
      this.m_lifespanInMS = this.STAR_PARTICLE_BASE_ACTIVE_TIME_IN_MS + new Random().Next(this.STAR_PARTICLE_ACTIVE_TIME_RANGE_IN_MS);
      this.m_xVelocity = this.STAR_MAXIMUM_INITIAL_X_VELOCITY - new Random().Next(this.STAR_RANGE_INITIAL_X_VELOCITY);
      this.m_yVelocity = this.STAR_MINIMUM_INITIAL_Y_VELOCITY + CMathFixed.Mul(this.STAR_POWER_ADDITIONAL_Y_VELOCITY, CMathFixed.Div(CMathFixed.Int32ToFixed(percentage), CMathFixed.Int32ToFixed(100))) - new Random().Next(this.STAR_RANDOM_RANGE_INITIAL_Y_VELOCITY);
      switch (new Random().Next(this.STAR_PARTICLE_SPRITE_TYPES))
      {
        case 0:
          this.m_pStarSprite.SetAnimation(3);
          break;
        case 1:
          this.m_pStarSprite.SetAnimation(4);
          break;
        case 2:
          this.m_pStarSprite.SetAnimation(5);
          break;
        case 3:
          this.m_pStarSprite.SetAnimation(6);
          break;
      }
      this.m_bIsActive = true;
    }

    public void tick(int deltaMS)
    {
      this.m_pStarSprite.Update(deltaMS);
      CGHStaticData.GetInstance();
      int val2 = CMathFixed.Div(CMathFixed.Int32ToFixed(deltaMS), CMathFixed.Int32ToFixed(this.MS_IN_ONE_SECOND));
      this.m_yFixed += CMathFixed.Mul(CGHStaticData.m_velocityScaleY, CMathFixed.Mul(this.m_yVelocity, val2));
      this.m_xFixed += CMathFixed.Mul(CGHStaticData.m_velocityScaleX, CMathFixed.Mul(this.m_xVelocity, val2));
      this.m_yVelocity += CMathFixed.Mul(this.STAR_PARTICLE_GRAVITY, val2);
      this.m_ageInMS += deltaMS;
      if (this.m_ageInMS < this.m_lifespanInMS)
        return;
      this.m_bIsActive = false;
    }

    public void paint()
    {
      this.m_pStarSprite.Draw(CMathFixed.FixedToInt32(this.m_xFixed), CMathFixed.FixedToInt32(this.m_yFixed));
    }
  }
}
