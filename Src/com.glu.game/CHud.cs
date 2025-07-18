// Decompiled with JetBrains decompiler
// Type: com.glu.game.CHud
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CHud : CClass
  {
    public const int HUD_SCORE_SCREEN_PERCENTAGE_X_OFFSET = 2;
    public const int HUD_SCORE_SCREEN_PERCENTAGE_Y_OFFSET = 2;
    public const int HUD_ROCK_METER_SCREEN_PERCENTAGE_X_OFFSET = 2;
    public const int HUD_ROCK_METER_SCREEN_PERCENTAGE_Y_OFFSET = 7;
    public const int HUD_MESSAGE_TEXT_Y_OFFSET = 18;
    public const int CHAIN_METER_THRESHOLD = 25;
    public const int CHAIN_COUNT_MESSAGE_NOTE_INTERVAL = 50;
    public const int MAX_MESSAGE_DISPLAY_TIME_MS = 1000;
    protected SG_Presenter sg_rockMeter;
    protected SG_Presenter sg_rockMeterEnds;
    protected SG_Presenter sg_rockMeterIndicator;
    protected SG_Presenter sg_scoreMeter;
    protected SG_Presenter sg_chainMeter;
    protected SG_Presenter sg_starPowerMeterBack;
    protected SG_Presenter sg_starPowerMeterFill;
    protected SG_Presenter sg_starPowerMeterHalf;
    protected SG_Presenter sg_starPowerMeterIndicator;
    protected SG_Presenter sg_starPowerGlow;
    protected SG_Presenter sg_multiplierMeter;
    protected int rock_meter_width;
    protected int rock_meter_height;
    protected int cur_rock_meter_level;
    protected int cur_rock_meter_indicator_x_offset;
    protected int cur_rock_meter_frame_index;
    protected int prev_rock_meter_frame_index;
    protected int rock_meter_gained_per_note;
    protected int rock_meter_lost_per_note;
    protected int rock_meter_x;
    protected int rock_meter_y;
    protected int multiplier_meter_x;
    protected int multiplier_meter_y;
    protected int star_meter_x;
    protected int star_meter_y;
    protected int star_meter_indicator_x;
    protected int star_meter_indicator_y;
    protected int score_meter_x;
    protected int score_meter_y;
    protected int rock_hud_x_start;
    protected int multi_hud_x_start;
    protected int star_hud_x_start;
    protected int score_hud_x_start;
    protected int star_power_fill_height;
    protected int star_power_fill_width;
    protected int star_power_empty_x;
    protected int cur_star_power_fill_width;
    protected int cur_star_power_score;
    public string[] inGameStrings;
    protected int cur_message_index;
    protected int cur_sub_message_index;
    protected int message_timer;
    protected int chain_message_value;
    private CRectangle boundsResult;

    public bool RockMeterIsEmpty() => 0 == this.cur_rock_meter_level;

    public bool RockMeterSetToFailing() => 0 == this.sg_rockMeter.GetFrameIndex();

    public bool RockMeterStatusChanged()
    {
      return this.cur_rock_meter_frame_index != this.prev_rock_meter_frame_index;
    }

    private void staticInitialization()
    {
      this.rock_meter_gained_per_note = 2;
      this.rock_meter_lost_per_note = 4;
      this.chain_message_value = 0;
      this.cur_star_power_fill_width = 0;
      this.cur_star_power_score = 0;
      CGHStaticData.m_bStarPowerAvailable = 0;
      this.sg_rockMeter = (SG_Presenter) null;
      this.sg_rockMeterEnds = (SG_Presenter) null;
      this.sg_rockMeterIndicator = (SG_Presenter) null;
      this.sg_scoreMeter = (SG_Presenter) null;
      this.sg_chainMeter = (SG_Presenter) null;
      this.sg_starPowerMeterBack = (SG_Presenter) null;
      this.sg_starPowerMeterFill = (SG_Presenter) null;
      this.sg_starPowerMeterHalf = (SG_Presenter) null;
      this.sg_starPowerMeterIndicator = (SG_Presenter) null;
      this.sg_starPowerGlow = (SG_Presenter) null;
      this.sg_multiplierMeter = (SG_Presenter) null;
      this.inGameStrings = (string[]) null;
    }

    public void init()
    {
      SG_Home.GetInstance().Init();
      this.staticInitialization();
      this.resetHudVariables();
      lock (CGameApp.loadQueuedLock)
      {
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 0);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 1);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 2);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 3);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 4);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 5);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 6);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 7);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 8);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 9);
        SG_Home.GetInstance().QueueArchetypeCharacter(2, 10);
        do
          ;
        while (SG_Home.GetInstance().LoadQueued(CResBank.kResMaxTimePerUpdateMS, out bool _));
      }
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.multiplier_meter_x = CGHStaticData.m_multiMeterAnchorPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.multiplier_meter_y = CGHStaticData.m_multiMeterAnchorPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.sg_scoreMeter = new SG_Presenter(2, 0);
      this.sg_scoreMeter.SetAnimation(3);
      this.sg_scoreMeter.SetFrameIndex(0);
      this.sg_scoreMeter.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.sg_chainMeter = new SG_Presenter(2, 0);
      this.sg_chainMeter.SetAnimation(5);
      this.sg_chainMeter.SetFrameIndex(0);
      this.sg_chainMeter.Reverse();
      this.sg_chainMeter.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.score_meter_x = (int) (width * 2U / 100U);
      this.score_meter_y = (int) (height * 2U / 100U);
      this.score_hud_x_start = -55;
      this.sg_scoreMeter.SetPosition((short) this.score_hud_x_start, (short) this.score_meter_y);
      this.sg_chainMeter.SetPosition((short) this.score_hud_x_start, (short) this.score_meter_y);
      this.sg_rockMeter = new SG_Presenter(2, 0);
      this.sg_rockMeter.SetAnimation(0);
      this.sg_rockMeter.SetFrameIndex(1);
      this.sg_rockMeterEnds = new SG_Presenter(2, 0);
      this.sg_rockMeterEnds.SetAnimation(1);
      this.sg_rockMeterIndicator = new SG_Presenter(2, 0);
      this.sg_rockMeterIndicator.SetAnimation(2);
      this.sg_rockMeterIndicator.SetFrameIndex(0);
      this.sg_rockMeter.Bounds(ref this.boundsResult);
      this.rock_meter_width = this.boundsResult.GetRight() - this.boundsResult.GetLeft();
      this.rock_meter_height = this.boundsResult.GetBottom() - this.boundsResult.GetTop();
      this.rock_hud_x_start = (int) width + 55;
      this.sg_rockMeterEnds.Bounds(ref this.boundsResult);
      this.rock_meter_x = (int) (width * 98U / 100U) - (this.boundsResult.GetRight() - this.boundsResult.GetLeft() >> 1);
      this.rock_meter_y = this.score_meter_y + (this.boundsResult.GetBottom() - this.boundsResult.GetTop() >> 1);
      this.cur_rock_meter_level = 50;
      this.cur_rock_meter_indicator_x_offset = (50 - this.cur_rock_meter_level) * this.rock_meter_width / 100;
      this.sg_rockMeter.SetPosition((short) this.rock_hud_x_start, (short) this.rock_meter_y);
      this.sg_rockMeterEnds.SetPosition((short) this.rock_hud_x_start, (short) this.rock_meter_y);
      this.sg_rockMeterIndicator.SetPosition((short) (this.rock_hud_x_start + this.cur_rock_meter_indicator_x_offset), (short) this.rock_meter_y);
      this.sg_starPowerMeterBack = new SG_Presenter(2, 0);
      this.sg_starPowerMeterBack.SetAnimation(6);
      this.sg_starPowerMeterFill = new SG_Presenter(2, 0);
      this.sg_starPowerMeterFill.SetAnimation(7);
      this.sg_starPowerMeterIndicator = new SG_Presenter(2, 0);
      this.sg_starPowerMeterIndicator.SetAnimation(9);
      this.sg_starPowerMeterHalf = new SG_Presenter(2, 0);
      this.sg_starPowerMeterHalf.SetAnimation(10);
      this.sg_starPowerGlow = new SG_Presenter(2, 0);
      this.sg_starPowerGlow.SetAnimation(9, true);
      this.sg_starPowerMeterFill.Bounds(ref this.boundsResult);
      this.star_power_fill_height = this.boundsResult.GetBottom() - this.boundsResult.GetTop();
      this.star_power_fill_width = this.boundsResult.GetRight() - this.boundsResult.GetLeft();
      this.star_meter_x = this.rock_meter_x;
      this.star_power_empty_x = this.star_meter_x + (this.star_power_fill_width >> 1);
      this.star_meter_y = this.rock_meter_y + (this.rock_meter_height >> 1) + 4 + (this.star_power_fill_height >> 1);
      this.star_meter_indicator_y = this.star_meter_y;
      this.star_hud_x_start = (int) width + 55;
      this.sg_starPowerMeterBack.SetPosition((short) this.star_hud_x_start, (short) this.star_meter_y);
      this.sg_starPowerMeterFill.SetPosition((short) this.star_hud_x_start, (short) this.star_meter_y);
      this.sg_starPowerMeterHalf.SetPosition((short) this.star_hud_x_start, (short) this.star_meter_y);
      this.sg_starPowerMeterIndicator.SetPosition((short) this.star_hud_x_start, (short) this.star_meter_y);
      this.sg_multiplierMeter = new SG_Presenter(2, 0);
      this.sg_multiplierMeter.SetAnimation(11);
      this.sg_multiplierMeter.SetFrameIndex(0);
      this.multi_hud_x_start = (int) width + 26;
      this.sg_multiplierMeter.SetPosition((short) this.multi_hud_x_start, (short) this.multiplier_meter_y);
      CApp.GetResourceManager();
      this.inGameStrings = new string[11];
      CUtility.GetString(out this.inGameStrings[0], "IDS_INGAME_NOTE_STREAK");
      CUtility.GetString(out this.inGameStrings[1], "IDS_INGAME_STAR_POWER_READY");
      this.inGameStrings[2] = CGameApp.GetInstance().GetOverrideText("IDS_INGAME_STAR_POWER_ACTIVATE_BUTTON");
      CUtility.GetString(out this.inGameStrings[3], "IDS_INGAME_STAR_POWER_FULL");
      CUtility.GetString(out this.inGameStrings[4], "IDS_INGAME_NOTE_MISSED");
      CUtility.GetString(out this.inGameStrings[5], "IDS_INGAME_STAR_POWER_INSUFFICIENT");
      CUtility.GetString(out this.inGameStrings[6], "IDS_INGAME_STAR_POWER_ACTIVATED");
      CUtility.GetString(out this.inGameStrings[7], "IDS_INGAME_YOU_ROCK");
      CUtility.GetString(out this.inGameStrings[8], "IDS_INGAME_IN_THE_RED");
      CUtility.GetString(out this.inGameStrings[9], "IDS_INGAME_HOLD_THAT_NOTE");
      CUtility.GetString(out this.inGameStrings[10], "IDS_INGAME_ROCKIN");
    }

    public void resetHudVariables()
    {
      this.cur_star_power_fill_width = 0;
      this.cur_star_power_score = 0;
      this.cur_rock_meter_level = 50;
      this.message_timer = 0;
      this.cur_message_index = -1;
      this.cur_sub_message_index = -1;
      CGHStaticData.m_bStarPowerAvailable = 0;
      if (this.sg_rockMeter != null)
        this.sg_rockMeter.SetFrameIndex(1);
      this.cur_rock_meter_frame_index = 1;
      this.prev_rock_meter_frame_index = 0;
      this.cur_rock_meter_indicator_x_offset = 0;
    }

    private void free()
    {
      this.sg_rockMeter = (SG_Presenter) null;
      this.sg_rockMeterEnds = (SG_Presenter) null;
      this.sg_rockMeterIndicator = (SG_Presenter) null;
      this.sg_scoreMeter = (SG_Presenter) null;
      this.sg_chainMeter = (SG_Presenter) null;
      this.sg_starPowerMeterBack = (SG_Presenter) null;
      this.sg_starPowerMeterFill = (SG_Presenter) null;
      this.sg_starPowerMeterHalf = (SG_Presenter) null;
      this.sg_starPowerMeterIndicator = (SG_Presenter) null;
      this.sg_starPowerGlow = (SG_Presenter) null;
      this.sg_multiplierMeter = (SG_Presenter) null;
      this.inGameStrings = (string[]) null;
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 1);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 2);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 3);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 4);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 5);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 6);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 7);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 8);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 9);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 10);
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("IDS_INGAME_NOTE_STREAK");
      resourceManager.ReleaseResource("IDS_INGAME_STAR_POWER_READY");
      resourceManager.ReleaseResource("IDS_INGAME_STAR_POWER_ACTIVATE_BUTTON");
      resourceManager.ReleaseResource("IDS_INGAME_STAR_POWER_FULL");
      resourceManager.ReleaseResource("IDS_INGAME_NOTE_MISSED");
      resourceManager.ReleaseResource("IDS_INGAME_STAR_POWER_INSUFFICIENT");
      resourceManager.ReleaseResource("IDS_INGAME_STAR_POWER_ACTIVATED");
      resourceManager.ReleaseResource("IDS_INGAME_YOU_ROCK");
      resourceManager.ReleaseResource("IDS_INGAME_IN_THE_RED");
      resourceManager.ReleaseResource("IDS_INGAME_HOLD_THAT_NOTE");
      resourceManager.ReleaseResource("IDS_INGAME_ROCKIN");
    }

    public void tick(int deltaMS)
    {
      this.prev_rock_meter_frame_index = this.cur_rock_meter_frame_index;
      this.cur_rock_meter_frame_index = this.sg_rockMeter.GetFrameIndex();
      this.sg_chainMeter.Update(deltaMS);
      this.sg_starPowerMeterFill.Update(deltaMS);
      this.sg_starPowerGlow.Update(deltaMS);
      if (this.message_timer <= 0)
        return;
      this.message_timer -= deltaMS;
    }

    public void updateHud(bool hit)
    {
      if (hit)
      {
        if (this.cur_rock_meter_level < 100)
        {
          if (this.cur_rock_meter_level < 33 && this.cur_rock_meter_level + this.rock_meter_gained_per_note >= 33)
          {
            this.sg_rockMeter.SetFrameIndex(1);
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdNegativeToNeutralSFX);
          }
          else if (this.cur_rock_meter_level < 66 && this.cur_rock_meter_level + this.rock_meter_gained_per_note >= 66)
          {
            this.sg_rockMeter.SetFrameIndex(2);
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdNeutralToPositiveSFX);
          }
          this.cur_rock_meter_level += this.rock_meter_gained_per_note;
          if (this.cur_rock_meter_level >= 100)
            this.cur_rock_meter_level = 100;
          this.cur_rock_meter_indicator_x_offset = (50 - this.cur_rock_meter_level) * this.rock_meter_width / 100;
          this.sg_rockMeterIndicator.SetPositionX((short) (this.rock_meter_x + this.cur_rock_meter_indicator_x_offset));
        }
        if (CGHStaticData.m_currentConsecutiveNotesHit == 25 && !this.sg_chainMeter.IsForward())
          this.sg_chainMeter.Reverse();
        if (CGHStaticData.m_currentConsecutiveNotesHit == 0 || CGHStaticData.m_currentConsecutiveNotesHit % 50 != 0)
          return;
        this.chain_message_value = CGHStaticData.m_currentConsecutiveNotesHit;
        this.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_NOTE_STREAK, CHud.eHUDMessageID.INDEX_STRINVALID);
        CGHStaticData.m_rocker_animation_state = 2;
        int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdCheerSFX);
      }
      else
      {
        this.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_NOTE_MISSED, CHud.eHUDMessageID.INDEX_STRINVALID);
        if (this.cur_rock_meter_level > 0)
        {
          if (this.cur_rock_meter_level >= 33 && this.cur_rock_meter_level - this.rock_meter_lost_per_note < 33)
          {
            this.sg_rockMeter.SetFrameIndex(0);
            this.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_IN_THE_RED, CHud.eHUDMessageID.INDEX_STRINVALID);
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdNeutralToNegativeSFX);
          }
          else if (this.cur_rock_meter_level >= 66 && this.cur_rock_meter_level - this.rock_meter_lost_per_note < 66)
          {
            this.sg_rockMeter.SetFrameIndex(1);
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdPositiveToNeutralSFX);
          }
          this.cur_rock_meter_level -= this.rock_meter_lost_per_note;
          if (this.cur_rock_meter_level < 0)
            this.cur_rock_meter_level = 0;
          this.cur_rock_meter_indicator_x_offset = (50 - this.cur_rock_meter_level) * this.rock_meter_width / 100;
          this.sg_rockMeterIndicator.SetPositionX((short) (this.rock_meter_x + this.cur_rock_meter_indicator_x_offset));
        }
        if (!this.sg_chainMeter.IsForward())
          return;
        this.sg_chainMeter.Reverse();
      }
    }

    public void SetMeterMultiplierDisplay()
    {
      int characterID;
      switch (CGHStaticData.m_scoreMultiplier * (CGHStaticData.m_bStarPowerScoringActivated ? 2 : 1))
      {
        case 2:
          characterID = CGHStaticData.m_bStarPowerScoringActivated ? 5 : 4;
          break;
        case 3:
          characterID = 6;
          break;
        case 4:
          characterID = CGHStaticData.m_bStarPowerScoringActivated ? 8 : 7;
          break;
        case 6:
          characterID = 9;
          break;
        case 8:
          characterID = 10;
          break;
        default:
          characterID = 0;
          break;
      }
      this.sg_multiplierMeter.SetCharacter(characterID);
    }

    public void updateMultiplier(bool hit)
    {
      if (hit)
      {
        this.sg_multiplierMeter.SetFrameIndex(CGHStaticData.m_scoreMultiplierStep == 0 ? this.sg_multiplierMeter.GetFrameCount() : CGHStaticData.m_scoreMultiplierStep * (this.sg_multiplierMeter.GetFrameCount() / 10));
        this.SetMeterMultiplierDisplay();
      }
      else
      {
        this.sg_multiplierMeter.SetCharacter(CGHStaticData.m_bStarPowerScoringActivated ? 5 : 0);
        if (!CGHStaticData.m_bStarPowerScoringActivated)
          this.sg_multiplierMeter.SetCharacter(0);
        this.sg_multiplierMeter.SetFrameIndex(0);
      }
    }

    public void updateStarMeter(int starPower)
    {
      int curStarPowerScore = this.cur_star_power_score;
      this.cur_star_power_score = starPower;
      if (this.cur_star_power_score >= 50 && curStarPowerScore < 50)
      {
        this.sg_starPowerMeterFill.SetAnimation(8, true);
        CGHStaticData.m_bStarPowerAvailable = 1;
      }
      if (CGHStaticData.m_bStarPowerAvailable != 0 && this.cur_star_power_score == 0)
      {
        this.sg_starPowerMeterFill.SetAnimation(7);
        CGHStaticData.m_bStarPowerAvailable = 0;
      }
      this.cur_star_power_fill_width = starPower * this.star_power_fill_width / 100;
      this.star_meter_indicator_x = (int) this.sg_starPowerMeterFill.GetPositionX() - this.cur_star_power_fill_width;
      this.sg_starPowerMeterIndicator.SetPosition((short) this.star_meter_indicator_x, (short) this.star_meter_indicator_y);
    }

    public void activateStarPower()
    {
      this.SetMeterMultiplierDisplay();
      this.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_STAR_POWER_ACTIVATED, CHud.eHUDMessageID.INDEX_STRINVALID);
    }

    public void deactivateStarPower()
    {
      this.SetMeterMultiplierDisplay();
      this.cur_star_power_fill_width = 0;
    }

    public bool animate_on(int cur_time, int animation_time)
    {
      if (cur_time > animation_time)
      {
        this.set_default_hud_positions();
        return true;
      }
      int num = cur_time * 100 / animation_time;
      int positionX1 = this.rock_hud_x_start + (this.rock_meter_x - this.rock_hud_x_start) * num / 100;
      this.sg_rockMeter.SetPosition((short) positionX1, (short) this.rock_meter_y);
      this.sg_rockMeterEnds.SetPosition((short) positionX1, (short) this.rock_meter_y);
      this.sg_rockMeterIndicator.SetPosition((short) (positionX1 + this.cur_rock_meter_indicator_x_offset), (short) this.rock_meter_y);
      this.sg_starPowerMeterBack.SetPosition((short) positionX1, (short) this.star_meter_y);
      this.sg_starPowerMeterFill.SetPosition((short) positionX1, (short) this.star_meter_y);
      this.sg_starPowerMeterHalf.SetPosition((short) positionX1, (short) this.star_meter_y);
      this.star_meter_indicator_x = (int) this.sg_starPowerMeterFill.GetPositionX() - this.cur_star_power_fill_width;
      this.sg_starPowerMeterIndicator.SetPosition((short) this.star_meter_indicator_x, (short) this.star_meter_y);
      int positionX2 = this.score_hud_x_start + (this.score_meter_x - this.score_hud_x_start) * num / 100;
      this.sg_scoreMeter.SetPosition((short) positionX2, (short) this.score_meter_y);
      this.sg_chainMeter.SetPosition((short) positionX2, (short) this.score_meter_y);
      this.sg_multiplierMeter.SetPosition((short) (this.multi_hud_x_start + (this.multiplier_meter_x - this.multi_hud_x_start) * num / 100), (short) this.multiplier_meter_y);
      return false;
    }

    public void paint(bool bRenderUnderlays, bool bRenderMessages)
    {
      if (bRenderUnderlays)
      {
        this.sg_multiplierMeter.Draw();
      }
      else
      {
        this.sg_rockMeter.Draw();
        this.sg_rockMeterIndicator.Draw();
        this.sg_rockMeterEnds.SetPosition(this.sg_rockMeter.GetPositionX(), this.sg_rockMeter.GetPositionY());
        this.sg_rockMeterEnds.Draw();
        this.sg_chainMeter.Draw();
        this.sg_scoreMeter.Draw();
        this.sg_starPowerMeterBack.Draw();
        ICGraphics2d instance = ICGraphics2d.GetInstance();
        CRectangle rect;
        rect.m_x = this.star_power_empty_x - this.cur_star_power_fill_width;
        rect.m_y = this.star_meter_indicator_y - (this.star_power_fill_height >> 1) - 1;
        rect.m_dx = this.cur_star_power_fill_width;
        rect.m_dy = this.star_power_fill_height + 1;
        CRectangle clip = instance.GetClip();
        instance.SetClip(rect);
        this.sg_starPowerMeterFill.Draw();
        this.sg_starPowerMeterIndicator.Draw();
        instance.SetClip(clip);
        int positionX = (int) this.sg_starPowerMeterBack.GetPositionX();
        int positionY = (int) this.sg_starPowerMeterBack.GetPositionY();
        this.sg_starPowerMeterHalf.Draw();
        this.sg_rockMeterEnds.SetPosition((short) positionX, (short) positionY);
        this.sg_rockMeterEnds.Draw();
        if (CGHStaticData.m_bStarPowerAvailable != 0)
          this.sg_starPowerGlow.Draw(this.star_power_empty_x - this.cur_star_power_fill_width + 1, this.star_meter_indicator_y);
        if (this.message_timer <= 0 || !bRenderMessages)
          return;
        uint width;
        uint height;
        ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
        CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
        if (-1 != this.cur_message_index)
        {
          string text = this.cur_message_index != 0 ? this.inGameStrings[this.cur_message_index] ?? "" : this.chain_message_value.ToString() + this.inGameStrings[this.cur_message_index];
          int num = font.MeasureTextWidth(text);
          int fontHeight = font.GetFontHeight();
          font.PaintText(text, text.Length, (int) ((long) width - (long) num >> 1), (int) ((long) height - (long) fontHeight >> 1) - 18);
        }
        if (-1 == this.cur_sub_message_index)
          return;
        string text1 = this.inGameStrings[this.cur_sub_message_index] ?? "";
        int num1 = font.MeasureTextWidth(text1);
        int fontHeight1 = font.GetFontHeight();
        font.PaintText(text1, text1.Length, (int) ((long) width - (long) num1 >> 1), (int) ((long) height - (long) fontHeight1 >> 1) - 18 + fontHeight1);
      }
    }

    public static void paintScore(int x, int y)
    {
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_INGAME_NUMBERS1);
      string text = string.Concat((object) CGHStaticData.m_score);
      int num = font.MeasureTextWidth(text);
      int fontHeight = font.GetFontHeight();
      font.PaintText(text, text.Length, x - num, y - (fontHeight >> 1));
    }

    public static void paintChainCount(int x, int y)
    {
      if (CGHStaticData.m_currentConsecutiveNotesHit < 25)
        return;
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_INGAME_NUMBERS2);
      string text = string.Concat((object) CGHStaticData.m_currentConsecutiveNotesHit);
      int num = font.MeasureTextWidth(text);
      int fontHeight = font.GetFontHeight();
      font.PaintText(text, text.Length, x - num, y - (fontHeight >> 1));
    }

    public void RecalculatePositions()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.sg_rockMeterEnds.Bounds(ref this.boundsResult);
      this.rock_meter_x = (int) width * 98 / 100 - (this.boundsResult.GetRight() - this.boundsResult.GetLeft() >> 1);
      this.rock_meter_y = this.score_meter_y + (this.boundsResult.GetBottom() - this.boundsResult.GetTop() >> 1);
      this.score_meter_x = (int) width * 2 / 100;
      this.score_meter_y = (int) height * 2 / 100;
      this.star_meter_x = this.rock_meter_x;
      this.star_power_empty_x = this.star_meter_x + (this.star_power_fill_width >> 1);
      this.star_meter_y = this.rock_meter_y + (this.rock_meter_height >> 1) + 4 + (this.star_power_fill_height >> 1);
      this.multiplier_meter_y = CGHStaticData.m_multiMeterAnchorPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.multiplier_meter_x = CGHStaticData.m_multiMeterAnchorPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.set_default_hud_positions();
    }

    private void set_default_hud_positions()
    {
      this.sg_rockMeter.SetPosition(this.rock_meter_x, this.rock_meter_y);
      this.sg_rockMeterIndicator.SetPosition(this.rock_meter_x + this.cur_rock_meter_indicator_x_offset, this.rock_meter_y);
      this.sg_rockMeterEnds.SetPosition(this.rock_meter_x, this.rock_meter_y);
      this.sg_scoreMeter.SetPosition(this.score_meter_x, this.score_meter_y);
      this.sg_chainMeter.SetPosition(this.score_meter_x, this.score_meter_y);
      this.sg_starPowerMeterFill.SetPosition(this.star_meter_x, this.star_meter_y);
      this.sg_starPowerMeterBack.SetPosition(this.star_meter_x, this.star_meter_y);
      this.sg_starPowerMeterIndicator.SetPosition(this.star_meter_indicator_x, this.star_meter_indicator_y);
      this.sg_starPowerMeterHalf.SetPosition(this.star_meter_x, this.star_meter_y);
      this.sg_multiplierMeter.SetPosition(this.multiplier_meter_x, this.multiplier_meter_y);
    }

    public void SetMessage(CHud.eHUDMessageID messageID, CHud.eHUDMessageID subMessageID)
    {
      this.cur_message_index = (int) messageID;
      this.cur_sub_message_index = (int) subMessageID;
      this.message_timer = 1000;
    }

    public void ClearMessage()
    {
      this.message_timer = 0;
      this.cur_message_index = -1;
      this.cur_sub_message_index = -1;
    }

    public enum eHUDMessageID
    {
      INDEX_STRINVALID = -1, // 0xFFFFFFFF
      INDEX_STRINGAME_NOTE_STREAK = 0,
      INDEX_STRINGAME_STAR_POWER_READY = 1,
      INDEX_STRINGAME_STAR_POWER_ACTIVATE_BUTTON = 2,
      INDEX_STRINGAME_STAR_POWER_FULL = 3,
      INDEX_STRINGAME_NOTE_MISSED = 4,
      INDEX_STRINGAME_STAR_POWER_INSUFFICIENT = 5,
      INDEX_STRINGAME_STAR_POWER_ACTIVATED = 6,
      INDEX_STRINGAME_YOU_ROCK = 7,
      INDEX_STRINGAME_IN_THE_RED = 8,
      INDEX_STRINGAME_HOLD_THAT_NOTE = 9,
      INDEX_STRINGAME_ROCKIN = 10, // 0x0000000A
      TOTAL_HUD_MESSAGE_STRINGS = 11, // 0x0000000B
    }
  }
}
