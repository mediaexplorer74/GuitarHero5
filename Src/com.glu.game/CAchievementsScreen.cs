// Decompiled with JetBrains decompiler
// Type: com.glu.game.CAchievementsScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CAchievementsScreen : CImageMenuScreen
  {
    private static int NUM_PICKS = 5;
    protected ICRenderSurface m_pLeft;
    protected ICRenderSurface m_pRight;
    protected ICRenderSurface m_pSmallPick;
    protected ICRenderSurface m_pSmallLockedPick;
    protected ICRenderSurface m_pUnlockedBG;
    protected string pPageIndexString;
    protected CTextWidget m_pageIndexText = new CTextWidget();
    protected CImageWidget m_UnlockedBGImage = new CImageWidget();
    protected bool m_bDisplayingSingleAchievement;
    protected int m_singleAchievementItemIndex;
    protected bool m_bAnimatingPickRevealTurning;
    protected bool m_bAnimatingPickRevealDisabled;
    protected bool m_bAnimatingPickHideTurning;
    protected bool m_bAnimatingPickHideDisabled;
    protected int m_lastState;
    protected int m_swipeLastRegion;
    protected int[] m_swipeRegionBoundaries = new int[CAchievementsScreen.NUM_PICKS];
    private CRectangle rect;

    public void SetDisplaySingleItem(int itemIndex)
    {
      this.m_bDisplayingSingleAchievement = true;
      this.m_singleAchievementItemIndex = itemIndex;
    }

    public bool DisplayingSingleItem() => this.m_bDisplayingSingleAchievement;

    public CAchievementsScreen()
    {
      this.m_pLeft = (ICRenderSurface) null;
      this.m_pRight = (ICRenderSurface) null;
      this.m_pSmallPick = (ICRenderSurface) null;
      this.m_pSmallLockedPick = (ICRenderSurface) null;
      this.m_pUnlockedBG = (ICRenderSurface) null;
      this.m_bDisplayingSingleAchievement = false;
      this.m_bAnimatingPickRevealTurning = false;
      this.m_bAnimatingPickRevealDisabled = true;
      this.m_bAnimatingPickHideTurning = false;
      this.m_bAnimatingPickHideDisabled = true;
      this.m_lastState = 0;
      this.m_eventId = 0U;
      this.m_swipePointerDown = false;
      this.m_swipeLastRegion = -1;
      for (int index = 0; index < CAchievementsScreen.NUM_PICKS; ++index)
        this.m_swipeRegionBoundaries[index] = 0;
    }

    public override void Activate()
    {
      base.Activate();
      if (!this.m_bDisplayingSingleAchievement)
        return;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_IDLE);
    }

    public override uint Start()
    {
      uint num = base.Start();
      CGameData.SetOptionsActive(true);
      return num;
    }

    public override void Stop()
    {
      base.Stop();
      CGameData.SetOptionsActive(false);
      ICMediaPlayer.GetInstance();
      int eventId = (int) this.m_eventId;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = base.HandleUpdate(timeElapsedMS);
      if (this.m_lastState != this.m_state)
      {
        if (this.m_state == 0)
        {
          this.m_bAnimatingPickRevealTurning = true;
          this.m_bAnimatingPickRevealDisabled = false;
          this.ResetPickRevealAnimations();
        }
        this.m_lastState = this.m_state;
      }
      return flag;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (!this.m_bDisplayingSingleAchievement && this.HandleMovieOut(id, param1, param2) || this.m_bDisplayingSingleAchievement && (id == 1913978637U || id == 2535498699U))
        return true;
      switch (id)
      {
        case 129075783:
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
          this.SetInterrupt(1);
          flag = true;
          break;
        case 555763780:
          int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          this.SetInterrupt(2);
          flag = true;
          break;
        case 902008092:
        case 1386813809:
          this.m_swipePointerDown = false;
          break;
        case 902053462:
        case 2300082508:
          this.m_swipePointerDown = true;
          this.m_swipeLastRegion = this.DetermineRegion(TouchUtil.TOUCH_EVENT_GET_X((uint) param2));
          break;
        case 902532892:
        case 2186393822:
          if (Consts.SWIPE_SUPPORTED && !this.m_bDisplayingSingleAchievement && this.m_swipePointerDown)
          {
            int region = this.DetermineRegion(TouchUtil.TOUCH_EVENT_GET_X((uint) param2));
            for (int index = 0; index < region - this.m_swipeLastRegion; ++index)
              CApp.GetInstance().OnSystemEvent(2535498699U, 0U, 0U);
            for (int index = 0; index < this.m_swipeLastRegion - region; ++index)
              CApp.GetInstance().OnSystemEvent(1913978637U, 0U, 0U);
            this.m_swipeLastRegion = region;
            flag = true;
            break;
          }
          break;
        case 1913978637:
        case 2535498699:
          this.m_bAnimatingPickHideTurning = true;
          this.m_bAnimatingPickHideDisabled = false;
          this.ResetPickHideAnimations();
          int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      this.RenderBegin();
      this.RenderEnd();
      return true;
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
      int resource4 = (int) resourceManager.CreateResource("SUR_UI_ACHIEVEMENT_COVER", out resource1);
      if (resource1 != null)
        this.m_pSmallPick = (ICRenderSurface) resource1.GetData();
      int resource5 = (int) resourceManager.CreateResource("SUR_UI_ACHIEVEMENT_LOCKED_SMALL", out resource1);
      if (resource1 != null)
        this.m_pSmallLockedPick = (ICRenderSurface) resource1.GetData();
      int resource6 = (int) resourceManager.CreateResource("SUR_UI_ACHIEVEMENT_UNLOCKED_BACKGROUND", out resource1);
      if (resource1 == null)
        return;
      this.m_pUnlockedBG = (ICRenderSurface) resource1.GetData();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_ARROW_LEFT");
      resourceManager.ReleaseResource("SUR_ARROW_RIGHT");
      resourceManager.ReleaseResource("SUR_UI_ACHIEVEMENT_COVER");
      resourceManager.ReleaseResource("SUR_UI_ACHIEVEMENT_LOCKED_SMALL");
      resourceManager.ReleaseResource("SUR_UI_ACHIEVEMENT_UNLOCKED_BACKGROUND");
    }

    public override void Build()
    {
      base.Build();
      string[] titles;
      string[] descriptions;
      LiveAchievements.GetInstance().getText(out titles, out descriptions);
      if (this.m_bDisplayingSingleAchievement)
        this.m_base.AddChild((CUIWidget) this.m_UnlockedBGImage, 0);
      if (this.m_pImages != null)
      {
        for (int index = 0; index < 20; ++index)
        {
          if (this.m_pImages[index] == null)
            this.m_pImages[index] = new CImageWidget();
          if (LiveAchievements.GetInstance().m_icons[index] != null)
            this.m_pImages[index].SetImage(LiveAchievements.GetInstance().m_icons[index]);
          else
            this.m_pImages[index].SetImage(this.m_pSmallLockedPick);
          this.m_base.AddChild((CUIWidget) this.m_pImages[index], 0);
        }
      }
      if (this.m_pImageTexts != null)
      {
        this.m_pTexts = new CTextWidget[20];
        for (int index = 0; index < 20; ++index)
        {
          this.m_pTexts[index] = new CTextWidget();
          this.m_pTexts[index].SetID(0);
          this.m_pTexts[index].SetAlignment(2);
          this.m_pTexts[index].SetTransparent(true);
          this.m_pTexts[index].SetFont(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
          this.m_pTexts[index].SetText(titles[index] + (object) '\n' + descriptions[index]);
          this.m_base.AddChild((CUIWidget) this.m_pTexts[index], 0);
        }
      }
      CFontMgr.GetInstance();
      this.m_pageIndexText.SetID(0);
      this.m_pageIndexText.SetAlignment(2);
      this.m_pageIndexText.SetTransparent(true);
      this.m_pageIndexText.SetFont(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
      this.m_pageIndexText.SetText(this.pPageIndexString);
      this.m_base.AddChild((CUIWidget) this.m_pageIndexText, 0);
      if (this.m_bDisplayingSingleAchievement)
      {
        this.m_UnlockedBGImage.SetID(0);
        this.m_UnlockedBGImage.SetImage(this.m_pUnlockedBG);
        this.m_UnlockedBGImage.SetTransparent(true);
      }
      this.m_page.SetWrap(true);
    }

    public override void Layout()
    {
      base.Layout();
      for (int index = 0; index < this.m_imageCount; ++index)
      {
        this.m_pImages[index].SetVisible(false);
        if (this.m_pTexts != null)
          this.m_pTexts[index].SetVisible(false);
      }
      int i;
      string name;
      if (this.m_state == 0)
      {
        i = 1;
        name = this.m_pImageIds[this.m_curImageIndex];
      }
      else
      {
        i = 13;
        name = !this.ItemIsSelectable(this.m_curImageIndex) ? "SUR_UI_ACHIEVEMENT_LOCKED_SMALL" : "SUR_UI_ACHIEVEMENT_COVER";
      }
      if (this.m_movie.GetUserRegionVisible((uint) i))
      {
        CResourceManager resourceManager = CApp.GetResourceManager();
        CResource resource1 = (CResource) null;
        int resource2 = (int) resourceManager.CreateResource(name, out resource1);
        this.m_movie.GetUserRegion((uint) i, ref this.rect);
        this.rect.m_x += this.rect.m_dx - this.m_pImages[this.m_curImageIndex].GetContentWidth() >> 1;
        if (CBitMath.TEST_MASK(this.m_flags, 8))
          this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[this.m_curImageIndex].GetContentHeight());
        else
          this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[this.m_curImageIndex].GetContentHeight() >> 1);
        this.rect.m_dx = (int) (short) this.m_pImages[this.m_curImageIndex].GetContentWidth();
        this.rect.m_dy = (int) (short) this.m_pImages[this.m_curImageIndex].GetContentHeight();
        this.m_pImages[this.m_curImageIndex].SetRect(this.rect);
        this.m_pImages[this.m_curImageIndex].SetVisible(true);
        this.m_pTexts[this.m_curImageIndex].SetVisible(true);
        if (this.m_bDisplayingSingleAchievement)
        {
          this.m_movie.GetUserRegion((uint) i, ref this.rect);
          this.rect.m_x += (int) (short) (this.rect.m_dx - this.m_UnlockedBGImage.GetContentWidth() >> 1);
          if (CBitMath.TEST_MASK(this.m_flags, 8))
            this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_UnlockedBGImage.GetContentHeight());
          else
            this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_UnlockedBGImage.GetContentHeight() >> 1);
          this.rect.m_dx = (int) (short) this.m_UnlockedBGImage.GetContentWidth();
          this.rect.m_dy = (int) (short) this.m_UnlockedBGImage.GetContentHeight();
          if (CGameApp.GetInstance().IsTouchscreenSupported())
          {
            this.SetTouchRegions();
            this.AddTouchRegion(this.rect, 544526345U);
          }
          this.m_UnlockedBGImage.SetRect(this.rect);
          this.m_UnlockedBGImage.SetVisible(true);
        }
      }
      if (!this.m_bDisplayingSingleAchievement)
      {
        if (this.m_imageCount > 1)
        {
          int index1 = this.m_curImageIndex;
          if (this.m_movie.GetUserRegionVisible(3U))
          {
            this.m_movie.GetUserRegion(3U, ref this.rect);
            index1 = this.GetPrevImage(index1);
            this.m_pImages[index1].SetVisible(true);
            this.rect.m_dx = (int) (short) this.m_pImages[index1].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[index1].GetContentHeight();
            this.m_pImages[index1].SetRect(this.rect);
            if (this.m_swipeRegionBoundaries[1] == 0 && this.m_movie.GetChapter() == 1)
              this.m_swipeRegionBoundaries[1] = this.rect.m_x + this.rect.m_dx;
          }
          if (this.m_movie.GetUserRegionVisible(5U))
          {
            this.m_movie.GetUserRegion(5U, ref this.rect);
            index1 = this.GetPrevImage(index1);
            this.m_pImages[index1].SetVisible(true);
            this.rect.m_dx = (int) (short) this.m_pImages[index1].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[index1].GetContentHeight();
            this.m_pImages[index1].SetRect(this.rect);
            if (this.m_swipeRegionBoundaries[0] == 0 && this.m_movie.GetChapter() == 1)
              this.m_swipeRegionBoundaries[0] = this.rect.m_x + this.rect.m_dx;
          }
          if (this.m_state == 1)
          {
            this.m_movie.GetUserRegion(12U, ref this.rect);
            int prevImage = this.GetPrevImage(index1);
            this.rect.m_dx = (int) (short) this.m_pImages[prevImage].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[prevImage].GetContentHeight();
            this.m_pImages[prevImage].SetRect(this.rect);
            this.m_pImages[prevImage].SetVisible(true);
          }
          int index2 = this.m_curImageIndex;
          if (this.m_movie.GetUserRegionVisible(4U))
          {
            this.m_movie.GetUserRegion(4U, ref this.rect);
            index2 = this.GetNextImage(index2);
            this.rect.m_dx = (int) (short) this.m_pImages[index2].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[index2].GetContentHeight();
            this.m_pImages[index2].SetRect(this.rect);
            this.m_pImages[index2].SetVisible(true);
            if (this.m_movie.GetChapter() == 1)
            {
              if (this.m_swipeRegionBoundaries[2] == 0)
                this.m_swipeRegionBoundaries[2] = this.rect.m_x;
              if (this.m_swipeRegionBoundaries[3] == 0)
                this.m_swipeRegionBoundaries[3] = this.rect.m_x + this.rect.m_dx;
            }
          }
          if (this.m_movie.GetUserRegionVisible(6U))
          {
            this.m_movie.GetUserRegion(6U, ref this.rect);
            index2 = this.GetNextImage(index2);
            this.rect.m_dx = (int) (short) this.m_pImages[index2].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[index2].GetContentHeight();
            this.m_pImages[index2].SetRect(this.rect);
            this.m_pImages[index2].SetVisible(true);
            if (this.m_swipeRegionBoundaries[4] == 0 && this.m_movie.GetChapter() == 1)
              this.m_swipeRegionBoundaries[4] = this.rect.m_x + this.rect.m_dx;
          }
          if (this.m_state == 2)
          {
            this.m_movie.GetUserRegion(12U, ref this.rect);
            int nextImage = this.GetNextImage(index2);
            this.rect.m_dx = (int) (short) this.m_pImages[nextImage].GetContentWidth();
            this.rect.m_dy = (int) (short) this.m_pImages[nextImage].GetContentHeight();
            this.m_pImages[nextImage].SetRect(this.rect);
            this.m_pImages[nextImage].SetVisible(true);
          }
          if (this.m_movie.GetUserRegionVisible(9U))
          {
            this.m_movie.GetUserRegion(9U, ref this.rect);
            this.sgArrowLeft.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
          }
          if (this.m_movie.GetUserRegionVisible(10U))
          {
            this.m_movie.GetUserRegion(10U, ref this.rect);
            this.sgArrowRight.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
          }
        }
        if (this.m_movie.GetUserRegionVisible(7U))
        {
          this.m_movie.GetUserRegion(7U, ref this.rect);
          this.pPageIndexString = (this.m_curImageIndex + 1).ToString() + "/" + (object) this.m_imageCount;
          this.m_pageIndexText.SetText(this.pPageIndexString);
          this.rect.m_dy = (int) (short) CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight();
          this.m_pageIndexText.SetRect(this.rect);
          this.m_pageIndexText.SetVisible(true);
        }
      }
      else
        this.m_curImageIndex = this.m_singleAchievementItemIndex;
      if (!this.m_movie.GetUserRegionVisible(0U))
        return;
      this.m_movie.GetUserRegion(0U, ref this.rect);
      this.rect.m_x += 30;
      this.rect.m_dx -= 60;
      this.rect.m_dy = (int) (short) CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight() * 4;
      this.m_pTexts[this.m_curImageIndex].SetRect(this.rect);
      this.m_pTexts[this.m_curImageIndex].SetVisible(true);
    }

    private void ResetPickHideAnimations()
    {
    }

    private void ResetPickRevealAnimations()
    {
    }

    public override void SetTouchRegions()
    {
      this.ClearAllTouchRegions();
      base.SetTouchRegions();
    }

    private int DetermineRegion(int pos)
    {
      int region = 0;
      while (pos > this.m_swipeRegionBoundaries[region])
      {
        ++region;
        if (region >= CAchievementsScreen.NUM_PICKS)
        {
          region = CAchievementsScreen.NUM_PICKS - 1;
          break;
        }
      }
      return region;
    }
  }
}
