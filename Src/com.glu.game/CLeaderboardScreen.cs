// Decompiled with JetBrains decompiler
// Type: com.glu.game.CLeaderboardScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  internal class CLeaderboardScreen : CWidgetScreen
  {
    public uint COLOR_GH_BLUE = 4278233032;
    public int ACCELERATION = CMathFixed.Int32ToFixed(25) / 1000;
    private short LDRBRD_UNDERLINE_HEIGHT = 2;
    protected CLeaderboardScreen.Mode m_mode;
    protected int[] m_pNavID;
    protected int m_curPage;
    protected ICRenderSurface m_pLeft;
    protected ICRenderSurface m_pRight;
    protected CImageWidget m_guitarIcon;
    protected CImageWidget m_bassIcon;
    protected CImageWidget m_drumsIcon;
    protected CLabelWidget m_userScore;
    protected CNavigatorWidget m_navigator;
    protected CHighscoreTableWidget m_table;
    protected SG_Presenter sgIconGuita;
    protected SG_Presenter sgIconBass;
    protected SG_Presenter sgIconDrum;
    protected int m_x;
    protected int m_y;
    protected int m_dx;
    protected int m_dy;
    protected CMarqueeText m_marquee;
    protected CLeaderboardScreen.LeaderboardData m_pCurData;
    protected CLeaderboardScreen.LeaderboardData m_pOtherData;
    protected bool m_swipePointerDown;
    protected bool m_swipeRecently;
    protected int m_swipeDownXPos;
    protected int m_swipeLastXPos;
    protected int m_swipeAddtlOffsetNeeded;
    protected int m_swipeMinSpeed;
    protected int m_swipeSpeed;
    protected int m_swipeTopSpeed;
    protected int m_swipeXOffset;
    protected int m_swipeAnimDist;
    protected CLeaderboardScreen.eLdrBrdAnimState m_animState;
    private CRectangle rect;
    private CRectangle arrowRect;
    private CRectangle movieRect;
    private CRectangle aboveRect;
    private CRectangle belowRect;
    private CRectangle lineRect;

    public CLeaderboardScreen()
    {
      this.m_mode = CLeaderboardScreen.Mode.HS_MODE_NONE;
      this.m_pNavID = (int[]) null;
      this.m_curPage = 0;
      this.m_pLeft = (ICRenderSurface) null;
      this.m_pRight = (ICRenderSurface) null;
      this.sgIconGuita = (SG_Presenter) null;
      this.sgIconDrum = (SG_Presenter) null;
      this.sgIconBass = (SG_Presenter) null;
      this.m_swipePointerDown = false;
      this.m_swipeRecently = false;
      this.m_swipeAddtlOffsetNeeded = 0;
      this.m_swipeSpeed = 0;
      this.m_swipeXOffset = 0;
      this.m_pOtherData = (CLeaderboardScreen.LeaderboardData) null;
      this.m_pCurData = (CLeaderboardScreen.LeaderboardData) null;
      this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      bool left = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 850690755:
          base.HandleEvent(id, param1, param2);
          this.m_movie.Refresh();
          this.Layout();
          this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
          if (this.m_pCurData != null)
          {
            this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
            this.m_pCurData.CalcScrollbar(this.rect);
            if ((int) this.m_pCurData.m_totalLines <= (int) this.m_pCurData.m_numShownLines)
              this.SetSlideBar(-1);
            else
              this.SetSlideBar((int) this.m_pCurData.m_curLine * 100 / ((int) this.m_pCurData.m_totalLines - (int) this.m_pCurData.m_numShownLines));
          }
          if (this.m_pOtherData != null)
          {
            this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
            this.m_pOtherData.CalcScrollbar(this.rect);
          }
          this.ClearAllTouchRegions();
          this.SetTouchRegions();
          this.m_marquee.Reset();
          flag = true;
          break;
        case 902008092:
        case 1386813809:
          if (this.m_swipeRecently)
          {
            flag = true;
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          this.m_swipeRecently = false;
          this.m_swipePointerDown = false;
          break;
        case 902053462:
        case 2300082508:
          this.m_swipePointerDown = true;
          this.m_swipeRecently = false;
          this.m_swipeLastXPos = this.m_swipeDownXPos = TouchUtil.TOUCH_EVENT_GET_X((uint) param2) - CMathFixed.FixedToInt32(this.m_swipeXOffset);
          break;
        case 902532892:
        case 2186393822:
          if (Consts.SWIPE_SUPPORTED && this.m_swipePointerDown)
          {
            this.m_swipeLastXPos = TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
            uint width;
            ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
            int num1 = (int) (width / 2U);
            int num2 = this.m_swipeLastXPos - this.m_swipeDownXPos;
            if ((long) CMath.Abs(num2) > (long) (width / 15U))
            {
              if (this.m_pOtherData == null || num2 < 0 && CMathFixed.FixedToInt32(this.m_swipeXOffset) > 0 || num2 > 0 && CMathFixed.FixedToInt32(this.m_swipeXOffset) < 0)
              {
                if (this.m_pOtherData == null)
                  this.m_pOtherData = new CLeaderboardScreen.LeaderboardData();
                this.m_pOtherData.m_cursor = this.m_pCurData.m_cursor;
                this.FetchLeaderboardData(this.m_pOtherData, num2 < 0);
              }
              this.m_swipeXOffset = CMathFixed.Int32ToFixed(num2);
              this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_SCROLLING;
              break;
            }
            break;
          }
          break;
        case 1066869024:
          if (this.m_animState != CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE && this.m_pCurData.m_curLine > (ushort) 0)
          {
            --this.m_pCurData.m_curLine;
            this.SetSlideBar((int) this.m_pCurData.m_curLine * 100 / ((int) this.m_pCurData.m_totalLines - (int) this.m_pCurData.m_numShownLines));
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          flag = true;
          break;
        case 1913978637:
          if (this.m_pOtherData == null)
            this.m_pOtherData = new CLeaderboardScreen.LeaderboardData();
          this.m_pOtherData.m_cursor = this.m_pCurData.m_cursor;
          this.FetchLeaderboardData(this.m_pOtherData, left);
          int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          flag = true;
          break;
        case 2535467201:
          if (this.m_animState != CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE && (int) this.m_pCurData.m_curLine + (int) this.m_pCurData.m_numShownLines < (int) this.m_pCurData.m_totalLines)
          {
            ++this.m_pCurData.m_curLine;
            this.SetSlideBar((int) this.m_pCurData.m_curLine * 100 / ((int) this.m_pCurData.m_totalLines - (int) this.m_pCurData.m_numShownLines));
            int num4 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          flag = true;
          break;
        case 2535475076:
        case 3563016926:
          int num5 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          break;
        case 2535498699:
          left = true;
          goto case 1913978637;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource("SUR_ARROW_LEFT", out resource1);
      if (resource1 != null)
        this.m_pLeft = (ICRenderSurface) resource1.GetData();
      int resource3 = (int) resourceManager.CreateResource("SUR_ARROW_RIGHT", out resource1);
      if (resource1 != null)
        this.m_pRight = (ICRenderSurface) resource1.GetData();
      this.sgIconGuita = new SG_Presenter(3, 0);
      this.sgIconGuita.SetAnimation(30);
      this.sgIconGuita.SetPosition(-1000, -1000);
      this.sgIconBass = new SG_Presenter(3, 0);
      this.sgIconBass.SetAnimation(31);
      this.sgIconBass.SetPosition(-1000, -1000);
      this.sgIconDrum = new SG_Presenter(3, 0);
      this.sgIconDrum.SetAnimation(32);
      this.sgIconDrum.SetPosition(-1000, -1000);
      this.sgArrowLeft = new SG_Presenter(3, 0);
      this.sgArrowLeft.SetAnimation(10);
      this.sgArrowLeft.SetPosition(-1000, -1000);
      this.sgArrowLeft.SetLoop(true);
      this.sgArrowRight = new SG_Presenter(3, 0);
      this.sgArrowRight.SetAnimation(11);
      this.sgArrowRight.SetPosition(-1000, -1000);
      this.sgArrowRight.SetLoop(true);
      this.SetSlideBar(0);
    }

    private void FetchLeaderboardData(CLeaderboardScreen.LeaderboardData pData, bool left)
    {
      this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
      pData.CalcScrollbar(this.rect);
      pData.RemoveAllScores();
      ushort cursor = pData.m_cursor;
      if (left)
      {
        int num = (int) CLeaderBoard.PopulatePrevLeadboard(ref cursor, pData);
        this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTOLEFT;
      }
      else
      {
        int num = (int) CLeaderBoard.PopulateNextLeadboard(ref cursor, pData);
        this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTORIGHT;
      }
      uint width;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
      this.m_swipeMinSpeed = this.m_swipeSpeed = CMathFixed.Int32ToFixed((int) width) / 2000;
      this.m_swipeTopSpeed = CMathFixed.Int32ToFixed((int) width) / 400;
      pData.m_cursor = cursor;
      pData.CalcScrollbar(this.rect);
      this.SetSlideBar((int) pData.m_totalLines <= (int) pData.m_numShownLines ? -1 : 0);
      this.m_marquee.Reset();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_ARROW_LEFT");
      resourceManager.ReleaseResource("SUR_ARROW_RIGHT");
      CLeaderBoard.Empty();
      this.sgIconGuita = (SG_Presenter) null;
      this.sgIconBass = (SG_Presenter) null;
      this.sgIconDrum = (SG_Presenter) null;
    }

    public override void Build()
    {
      base.Build();
      CFontMgr.GetInstance();
      this.m_movie.SetChapter(this.m_chapterIdle, false);
      this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
      this.m_dy = this.rect.m_dy;
      this.m_movie.SetChapter(this.m_chapterIn, false);
      this.m_pCurData = new CLeaderboardScreen.LeaderboardData();
      this.m_pCurData.CalcScrollbar(this.rect);
      this.m_pCurData.m_cursor = CLeaderBoard.PopulateCareerLeadboard(this.m_pCurData);
      this.m_pCurData.CalcScrollbar(this.rect);
      this.SetSlideBar((int) this.m_pCurData.m_totalLines <= (int) this.m_pCurData.m_numShownLines ? -1 : 0);
    }

    public override void SetTouchRegions()
    {
      this.sgArrowLeft.Bounds(ref this.arrowRect);
      this.arrowRect.Inset(0);
      this.AddTouchRegion(this.arrowRect, 2535498699U);
      this.sgArrowRight.Bounds(ref this.arrowRect);
      this.arrowRect.Inset(0);
      this.AddTouchRegion(this.arrowRect, 1913978637U);
      this.m_movie.GetUserRegion((uint) this.REGION_SLIDE_BAR, ref this.movieRect);
      uint width;
      uint height;
      this.m_pSlideBarBotImage.GetWidthAndHeight(out width, out height);
      this.arrowRect.m_x = this.movieRect.m_x + this.movieRect.m_dx / 2 - 10;
      this.arrowRect.m_y = this.movieRect.m_y + this.movieRect.m_dy;
      this.arrowRect.m_dx = (int) (short) width;
      this.arrowRect.m_dy = (int) (short) height;
      this.AddTouchRegion(this.arrowRect, 2535467201U);
      this.m_pSlideBarTopImage.GetWidthAndHeight(out width, out height);
      this.arrowRect.m_x = this.movieRect.m_x + this.movieRect.m_dx / 2 - 10;
      this.arrowRect.m_y = this.movieRect.m_y - 19;
      this.arrowRect.m_dx = (int) (short) width;
      this.arrowRect.m_dy = (int) (short) height;
      this.AddTouchRegion(this.arrowRect, 1066869024U);
    }

    public override void Layout()
    {
      base.Layout();
      if (this.m_movie.GetUserRegionVisible(5U))
      {
        this.m_movie.GetUserRegion(5U, ref this.rect);
        this.sgArrowLeft.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
      }
      if (this.m_movie.GetUserRegionVisible(6U))
      {
        this.m_movie.GetUserRegion(6U, ref this.rect);
        this.sgArrowRight.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
      }
      if (!this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
        return;
      this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
      this.m_x = this.rect.m_x;
      this.m_y = this.rect.m_y;
      this.m_dx = this.rect.m_dx;
      this.m_dy = this.rect.m_dy;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      if (this.m_animState != CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE)
        this.m_drawUI = false;
      else
        this.m_drawUI = true;
      if (Consts.SWIPE_SUPPORTED)
      {
        uint width;
        uint height;
        ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
        int num1 = 1;
        switch (this.m_animState)
        {
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_SCROLLING:
            int int32 = CMathFixed.FixedToInt32(this.m_swipeXOffset);
            if ((long) CMath.Abs(int32) > (long) width)
            {
              this.m_swipeXOffset = 0;
              if (this.m_swipeDownXPos < 0)
                this.m_swipeDownXPos += (int) width;
              else
                this.m_swipeDownXPos -= (int) width;
              this.m_pCurData = (CLeaderboardScreen.LeaderboardData) null;
              this.m_pCurData = this.m_pOtherData;
              this.m_pOtherData = (CLeaderboardScreen.LeaderboardData) null;
              this.m_pOtherData = new CLeaderboardScreen.LeaderboardData();
              this.m_pOtherData.m_cursor = this.m_pCurData.m_cursor;
              this.FetchLeaderboardData(this.m_pOtherData, int32 < 0);
              break;
            }
            if (!this.m_swipePointerDown)
            {
              int num2 = (int) width / 2;
              if (CMath.Abs(int32) < num2)
                this.m_animState = int32 >= 0 ? CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_LEFT : CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_RIGHT;
              else if (int32 < 0 && (long) int32 > (long) -width)
                this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_LEFT;
              else if (int32 > 0 && (long) int32 < (long) width)
              {
                this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_RIGHT;
              }
              else
              {
                if ((long) CMath.Abs(int32) == (long) width)
                {
                  this.m_swipeXOffset = 0;
                  this.m_pCurData = (CLeaderboardScreen.LeaderboardData) null;
                  this.m_pCurData = this.m_pOtherData;
                  this.m_pOtherData = (CLeaderboardScreen.LeaderboardData) null;
                  this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE;
                  this.m_marquee.Reset();
                  break;
                }
                if (int32 == 0)
                {
                  this.m_swipeXOffset = 0;
                  this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE;
                  this.m_marquee.Reset();
                  break;
                }
                this.m_swipeAnimDist = CMathFixed.Int32ToFixed(width) - CMathFixed.Abs(this.m_swipeXOffset);
              }
              this.m_swipeMinSpeed = this.m_swipeSpeed = this.m_swipeAnimDist / 2000;
              this.m_swipeTopSpeed = this.m_swipeAnimDist / 400;
              break;
            }
            this.m_swipeAnimDist = this.m_swipeSpeed = 0;
            break;
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTORIGHT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_RIGHT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_RIGHT:
            ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
            if (CMathFixed.Int32ToFixed(width) - CMathFixed.Abs(this.m_swipeXOffset) < this.m_swipeAnimDist / 2)
              this.m_swipeSpeed -= this.ACCELERATION * timeElapsedMS;
            else
              this.m_swipeSpeed += this.ACCELERATION * timeElapsedMS;
            this.m_swipeSpeed = CMathFixed.Min(this.m_swipeTopSpeed, this.m_swipeSpeed);
            this.m_swipeSpeed = CMathFixed.Max(this.m_swipeMinSpeed, this.m_swipeSpeed);
            this.m_swipeXOffset += this.m_swipeSpeed * timeElapsedMS * num1;
            bool flag = false;
            switch (this.m_animState)
            {
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTOLEFT:
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTORIGHT:
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_LEFT:
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_RIGHT:
                if ((long) CMath.Abs(CMathFixed.FixedToInt32(this.m_swipeXOffset)) >= (long) width)
                {
                  this.m_pCurData = (CLeaderboardScreen.LeaderboardData) null;
                  this.m_pCurData = this.m_pOtherData;
                  this.m_pOtherData = (CLeaderboardScreen.LeaderboardData) null;
                  break;
                }
                break;
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_LEFT:
              case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_RIGHT:
                if (flag || CMathFixed.Abs(this.m_swipeXOffset) <= CMathFixed.Abs(this.m_swipeSpeed * timeElapsedMS))
                {
                  this.m_swipeXOffset = 0;
                  this.m_animState = CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE;
                  this.m_marquee.Reset();
                  break;
                }
                break;
            }
            break;
        }
      }
      base.HandleUpdate(timeElapsedMS);
      this.m_marquee.HandleUpdate(timeElapsedMS);
      return true;
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      this.RenderBegin();
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CFontMgr instance2 = CFontMgr.GetInstance();
      CFont font1 = instance2.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      CFont font2 = instance2.GetFont(CFontMgr.eGameFont.FONT_NUMBERFONT);
      CFont font3 = instance2.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT);
      font1.MeasureTextWidth(" ");
      bool flag1 = CGameApp.GetInstance().IsTouchscreenSupported();
      uint num1 = 0;
      uint num2 = 0;
      int num3 = 0;
      int num4 = -1;
      int num5 = -1;
      uint num6 = 0;
      uint width;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
      for (int index = 0; index < 2; ++index)
      {
        num1 = 0U;
        num3 = 0;
        int num7 = font1.MeasureTextWidth(" ");
        CLeaderboardScreen.LeaderboardData leaderboardData = (CLeaderboardScreen.LeaderboardData) null;
        switch (index)
        {
          case 0:
            leaderboardData = this.m_pCurData;
            break;
          case 1:
            leaderboardData = this.m_pOtherData;
            break;
        }
        switch (this.m_animState)
        {
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_SCROLLING:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTOLEFT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_AUTORIGHT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_LEFT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_CONTINUE_RIGHT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_LEFT:
          case CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_RETURN_RIGHT:
            switch (index)
            {
              case 0:
                instance1.Translate(this.m_swipeXOffset, 0);
                break;
              case 1:
                if (CMathFixed.FixedToInt32(this.m_swipeXOffset) < 0)
                {
                  instance1.Translate(CMathFixed.Int32ToFixed((int) width), 0);
                  break;
                }
                instance1.Translate(CMathFixed.Int32ToFixed((int) -width), 0);
                break;
            }
            break;
        }
        if (!CLeaderBoard.IsCareerLeaderboard(leaderboardData.m_cursor) && this.m_movie.GetUserRegionVisible(8U))
        {
          this.m_movie.GetUserRegion(8U, ref this.rect);
          int songId = CLeaderBoard.GetSongID(leaderboardData.m_cursor);
          CGHStaticData.GetInstance();
          int achievementsIndex = CGHStaticData.FindSongAchievementsIndex((uint) songId);
          bool flag2 = CGHStaticData.m_pSongAchievementData[achievementsIndex].CompletedInstrumentOnAnyDifficulty(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR);
          bool flag3 = CGHStaticData.m_pSongAchievementData[achievementsIndex].CompletedInstrumentOnAnyDifficulty(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS);
          bool flag4 = CGHStaticData.m_pSongAchievementData[achievementsIndex].CompletedInstrumentOnAnyDifficulty(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS);
          if (flag2)
          {
            this.sgIconGuita.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 4), (short) (this.rect.m_y + this.rect.m_dy / 2 + 1));
            this.sgIconGuita.Draw();
          }
          if (flag3)
          {
            this.sgIconBass.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2 + 1));
            this.sgIconBass.Draw();
          }
          if (flag4)
          {
            this.sgIconDrum.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2 + this.rect.m_dx / 4), (short) (this.rect.m_y + this.rect.m_dy / 2 + 1));
            this.sgIconDrum.Draw();
          }
        }
        if (this.m_movie.GetUserRegionVisible(7U))
        {
          this.m_movie.GetUserRegion(7U, ref this.rect);
          uint y = (uint) (this.rect.m_y + this.rect.m_dy - font3.GetFontHeight());
          font3.PaintText(leaderboardData.m_strUserPos, leaderboardData.m_strUserPos.Length, this.m_x, (int) y);
          int num8 = font3.MeasureTextWidth(leaderboardData.m_strUserPos) + num7;
          font3.PaintText(leaderboardData.m_strUserName, leaderboardData.m_strUserName.Length, this.m_x + num8, (int) y);
          int num9 = font3.MeasureTextWidth(leaderboardData.m_strUserScore);
          font3.PaintText(leaderboardData.m_strUserScore, leaderboardData.m_strUserScore.Length, this.m_x + this.m_dx - num9, (int) y);
        }
        if (this.m_movie.GetUserRegionVisible(1U))
        {
          this.m_movie.GetUserRegion(1U, ref this.rect);
          int num10 = font3.MeasureTextWidth(leaderboardData.m_strSongTitle);
          this.rect.m_y = (int) this.sgArrowLeft.GetPositionY() - font3.GetFontHeight() / 2;
          if (num10 > this.rect.m_dx && this.m_animState == CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE)
          {
            this.m_marquee.SetRect(this.rect);
            this.m_marquee.SetYPos(this.rect.m_y);
            this.m_marquee.SetText(font3, leaderboardData.m_strSongTitle);
            this.m_marquee.Paint();
          }
          else
            font3.PaintText(leaderboardData.m_strSongTitle, leaderboardData.m_strSongTitle.Length, this.rect.m_x + (this.rect.m_dx - num10) / 2, this.rect.m_y);
        }
        num2 = (uint) (this.m_y + font1.GetFontHeight() / 2);
        if (this.m_movie.GetUserRegionVisible(7U) && this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
        {
          this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.belowRect);
          this.m_movie.GetUserRegion(7U, ref this.aboveRect);
          this.lineRect = this.belowRect;
          this.lineRect.m_dy = (int) this.LDRBRD_UNDERLINE_HEIGHT;
          this.lineRect.m_y = (this.aboveRect.m_y + this.aboveRect.m_dy + this.belowRect.m_y) / 2;
          this.lineRect.m_y -= (int) this.LDRBRD_UNDERLINE_HEIGHT;
          CDrawUtil.FillRect(this.lineRect, this.COLOR_GH_BLUE);
        }
        for (int curLine = (int) leaderboardData.m_curLine; curLine < (int) leaderboardData.m_curLine + (int) leaderboardData.m_numShownLines; ++curLine)
        {
          num6 = num2;
          if (curLine < leaderboardData.m_items)
          {
            uint x1 = (uint) this.m_x;
            int num11 = font2.MeasureTextWidth(leaderboardData.m_pNumbers[curLine]);
            font2.PaintText(leaderboardData.m_pNumbers[curLine], leaderboardData.m_pNumbers[curLine].Length, (int) x1, (int) num2 - font2.GetFontHeight() / 2);
            uint x2 = x1 + (uint) (num11 + (num7 >> 1));
            font1.PaintText(leaderboardData.m_strNames[curLine], leaderboardData.m_strNames[curLine].Length, (int) x2, (int) num2 - font1.GetFontHeight() / 2);
            int num12 = font2.MeasureTextWidth(leaderboardData.m_pScores[index]);
            font2.PaintText(leaderboardData.m_pScores[curLine], leaderboardData.m_pScores[curLine].Length, this.m_x + this.m_dx - num12, (int) num2 - font2.GetFontHeight() / 2);
            num2 += (uint) font1.GetFontHeight();
            if (flag1 && this.m_pointerDown_x > x2 && this.m_pointerDown_x < (uint) (this.m_x + this.m_dx))
            {
              if (this.m_pointerDown_y > num6 && this.m_pointerDown_y < num2)
                num5 = curLine;
              if (this.m_pointerLast_y > num6 && this.m_pointerLast_y < num2)
                num4 = curLine;
            }
          }
          else
            break;
        }
        instance1.Translate(0, 0);
        if (this.m_animState == CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE || this.m_pOtherData == null)
          break;
      }
      if (flag1 && this.m_animState == CLeaderboardScreen.eLdrBrdAnimState.LDRBRD_ANIM_IDLE && num5 != -1 && num4 != -1 && num5 != num4)
      {
        if (num5 < num4)
        {
          this.m_pCurData.m_curLine = (ushort) CMath.Max(0, (int) this.m_pCurData.m_curLine - 1);
          this.m_pointerDown_y = CMath.Min(this.m_pointerLast_y, this.m_pointerDown_y - (num2 - num6));
        }
        else if (num5 > num4)
        {
          this.m_pCurData.m_curLine = (ushort) CMath.Min((int) this.m_pCurData.m_totalLines - (int) this.m_pCurData.m_numShownLines, (int) this.m_pCurData.m_curLine + 1);
          this.m_pointerDown_y = CMath.Max(this.m_pointerLast_y, this.m_pointerDown_y - (num2 - num6));
        }
        this.SetSlideBar((int) this.m_pCurData.m_curLine * 100 / ((int) this.m_pCurData.m_totalLines - (int) this.m_pCurData.m_numShownLines));
      }
      this.RenderEnd();
      return true;
    }

    public class LeaderboardData
    {
      public string m_strSongTitle;
      public string[] m_pNumbers = new string[Consts.kMaxHighScores];
      public string[] m_pNames = new string[Consts.kMaxHighScores];
      public string[] m_strNames = new string[Consts.kMaxHighScores];
      public string[] m_pScores = new string[Consts.kMaxHighScores];
      public string m_strUserPos;
      public string m_strUserName;
      public string m_strUserScore;
      public int m_items;
      public ushort m_cursor;
      public ushort m_curLine;
      public ushort m_numShownLines;
      public ushort m_totalLines;
      public ushort m_nNameMaxWidth;

      public LeaderboardData()
      {
        this.m_curLine = (ushort) 0;
        this.m_numShownLines = (ushort) 0;
        this.m_totalLines = (ushort) 0;
        this.m_nNameMaxWidth = (ushort) 0;
        this.m_strUserPos = "";
        this.m_strUserScore = "";
        this.m_strUserName = "";
        this.m_cursor = (ushort) 0;
        this.RemoveAllScores();
      }

      public void AppendHighscore(string name, int score)
      {
        CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
        if (this.m_items + 1 > Consts.kMaxHighScores || score <= 0)
          return;
        this.m_pNames[this.m_items] = name;
        this.m_strNames[this.m_items] = name;
        if (font.MeasureTextWidth(name) > (int) this.m_nNameMaxWidth)
        {
          this.m_strNames[this.m_items] = this.m_strNames[this.m_items].Substring(0, 3);
          string[] strNames;
          IntPtr items;
          (strNames = this.m_strNames)[(int) (items = (IntPtr) this.m_items)] = strNames[(int)items] + "...";
        }
        this.m_pScores[this.m_items] = score.ToString();
        ++this.m_items;
      }

      public void CalcScrollbar(CRectangle rect)
      {
        this.m_nNameMaxWidth = (ushort) (rect.m_dx / 3);
        int dy = rect.m_dy;
        this.m_totalLines = (ushort) CMath.Min(this.m_items, Consts.kMaxHighScores);
        this.m_numShownLines = (ushort) (dy / CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight());
        this.m_numShownLines = (ushort) CMath.Min((int) this.m_numShownLines, (int) this.m_totalLines);
        this.m_curLine = (ushort) CMath.Min((int) this.m_curLine, (int) this.m_totalLines - (int) this.m_numShownLines);
      }

      public void SetUser(int pos, string name, int score)
      {
        this.m_strUserName = name;
        this.m_strUserPos = pos.ToString();
        this.m_strUserScore = score.ToString();
      }

      public void SetUserBlank(string name)
      {
        this.m_strUserName = name;
        CUtility.GetString(out this.m_strUserPos, "IDS_NON_RANK_AND_SCORE");
        CUtility.GetString(out this.m_strUserScore, "IDS_NON_RANK_AND_SCORE");
      }

      public void RemoveAllScores()
      {
        this.m_items = 0;
        for (int index = 0; index < Consts.kMaxHighScores; ++index)
          this.m_pNumbers[index] = index + 1 >= 10 ? (index + 1).ToString() : "0" + (object) (index + 1);
      }

      public void SetSongTitle(string title) => this.m_strSongTitle = title;
    }

    public enum eLdrBrdDraw
    {
      LDRBRD_DRAW_MAIN,
      LDRBRD_DRAW_OTHER,
      NUM_LDRBRD_DRAWN,
    }

    public enum eLdrBrdAnimState
    {
      LDRBRD_ANIM_IDLE,
      LDRBRD_ANIM_SCROLLING,
      LDRBRD_ANIM_AUTOLEFT,
      LDRBRD_ANIM_AUTORIGHT,
      LDRBRD_ANIM_CONTINUE_LEFT,
      LDRBRD_ANIM_CONTINUE_RIGHT,
      LDRBRD_ANIM_RETURN_LEFT,
      LDRBRD_ANIM_RETURN_RIGHT,
      NUM_LDRBRD_ANIM,
    }

    protected enum Mode
    {
      HS_MODE_NONE,
      HS_MODE_HS_MGR,
      HS_MODE_ATB,
    }
  }
}
