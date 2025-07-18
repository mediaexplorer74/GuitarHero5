// Decompiled with JetBrains decompiler
// Type: com.glu.game.CImageMenuScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CImageMenuScreen : CWidgetScreen
  {
    protected string m_keysetId;
    protected int m_timeElapsedMS;
    protected CTextWidget[] m_pTexts;
    protected CImageWidget[] m_pImages;
    protected int m_state;
    protected int m_curImageIndex;
    protected int m_imageCount;
    protected CKeysetResource m_pKeyset;
    protected string[] m_pImageIds;
    protected string[] m_pImageTexts;
    protected int[] m_pSelectableFlags;
    protected bool m_swipePointerDown;
    protected bool m_swipeRecently;
    protected uint m_swipeDownXPos;
    protected uint m_swipeLastXPos;
    public int CHAPTER_SLIDE_TO_LEFT = 4;
    public int CHAPTER_SLIDE_TO_RIGHT = 2;
    private CRectangle arrowRect;
    private CRectangle rect;
    private CRectangle titleRect;

    public int GetSelected() => this.m_curImageIndex;

    public bool CurrentItemIsSelectable() => this.m_pSelectableFlags[this.m_curImageIndex] == 0;

    public bool ItemIsSelectable(int itemIndex) => this.m_pSelectableFlags[itemIndex] == 0;

    public void SetItemSelectable(int itemIndex, bool bSelectable)
    {
      this.m_pSelectableFlags[itemIndex] = bSelectable ? 0 : 1;
    }

    public CImageMenuScreen()
    {
      this.m_keysetId = (string) null;
      this.m_timeElapsedMS = -1;
      this.SetFlags(0);
      this.m_state = 0;
      this.m_pImages = (CImageWidget[]) null;
      this.m_curImageIndex = 0;
      this.m_imageCount = 0;
      this.m_chapterOut = 6;
      this.m_pImageIds = (string[]) null;
      this.m_pImageTexts = (string[]) null;
      this.m_pSelectableFlags = (int[]) null;
      this.m_pTexts = (CTextWidget[]) null;
      this.m_swipePointerDown = false;
      this.m_swipeRecently = false;
    }

    public override void Activate()
    {
      base.Activate();
      this.m_state = 0;
    }

    public override void Deactivate()
    {
      base.Deactivate();
      this.m_movieState = 1;
      this.m_movie.SetChapter(this.m_chapterIdle, false);
      if (this.m_state == 2)
        this.m_curImageIndex = this.GetNextImage();
      else if (this.m_state == 1)
        this.m_curImageIndex = this.GetPrevImage();
      this.m_state = 0;
    }

    public void SetInfo(string keysetId) => this.m_keysetId = keysetId;

    public void SetInfo(string[] pImageIds, string[] ptextIds, int count)
    {
      if (count <= 0 || pImageIds == null)
        return;
      this.m_pImageIds = new string[count];
      for (int index = 0; index < count; ++index)
        this.m_pImageIds[index] = pImageIds[index];
      this.m_pSelectableFlags = new int[count];
      for (int index = 0; index < count; ++index)
        this.m_pSelectableFlags[index] = 0;
      if (ptextIds != null)
      {
        this.m_pImageTexts = new string[count];
        for (int index = 0; index < count; ++index)
          CUtility.GetString(out this.m_pImageTexts[index], ptextIds[index]);
      }
      this.m_imageCount = count;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 129075783:
        case 544526345:
          if (this.CurrentItemIsSelectable())
          {
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            this.SetInterrupt(1);
            flag = true;
            break;
          }
          break;
        case 555763780:
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          this.SetInterrupt(2);
          flag = true;
          break;
        case 850690755:
          this.m_movie.Refresh();
          flag = base.HandleEvent(id, param1, param2);
          this.SetTouchRegions();
          break;
        case 902008092:
        case 1386813809:
          if (this.m_swipeRecently)
          {
            flag = true;
            int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          this.m_swipeRecently = false;
          this.m_swipePointerDown = false;
          break;
        case 902053462:
        case 2300082508:
          this.m_swipePointerDown = true;
          this.m_swipeRecently = false;
          this.m_swipeLastXPos = this.m_swipeDownXPos = (uint) TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
          break;
        case 902532892:
        case 2186393822:
          if (Consts.SWIPE_SUPPORTED && this.m_swipePointerDown)
          {
            this.m_swipeLastXPos = (uint) TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
            uint width;
            ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
            uint num3 = width / 3U;
            int val = (int) this.m_swipeLastXPos - (int) this.m_swipeDownXPos;
            if ((long) CMath.Abs(val) > (long) num3)
            {
              if (val > 0)
              {
                this.m_swipeDownXPos += num3;
                CApp.GetInstance().OnSystemEvent(2535498699U, 0U, 0U);
              }
              else
              {
                this.m_swipeDownXPos -= num3;
                CApp.GetInstance().OnSystemEvent(1913978637U, 0U, 0U);
              }
              this.m_swipeRecently = true;
              break;
            }
            break;
          }
          break;
        case 1913978637:
          if (this.m_imageCount > 1 && this.m_state == 0)
          {
            this.m_chapterDef = this.CHAPTER_SLIDE_TO_LEFT;
            this.m_state = 2;
            this.m_movieState = 5;
            this.m_movie.SetChapter(this.m_chapterDef, false);
            int num4 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          flag = true;
          break;
        case 2535498699:
          if (this.m_imageCount > 1 && this.m_state == 0)
          {
            this.m_chapterDef = this.CHAPTER_SLIDE_TO_RIGHT;
            this.m_state = 1;
            this.m_movieState = 5;
            this.m_movie.SetChapter(this.m_chapterDef, false);
            int num5 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          }
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      if (this.m_movieState == 5 && this.m_movie.GetLooped())
      {
        this.m_movieState = 1;
        this.m_movie.SetChapter(this.m_chapterIdle, false);
        if (this.m_state == 2)
          this.m_curImageIndex = this.GetNextImage();
        else if (this.m_state == 1)
          this.m_curImageIndex = this.GetPrevImage();
        this.m_state = 0;
      }
      return true;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_keysetId != null)
      {
        int resource2 = (int) resourceManager.CreateResource(this.m_keysetId, out resource1);
        if (resource1 != null)
        {
          this.m_pKeyset = (CKeysetResource) resource1.GetData();
          this.m_imageCount = (int) this.m_pKeyset.GetCount();
        }
        if (this.m_imageCount > 0)
        {
          this.m_pImages = new CImageWidget[this.m_imageCount];
          for (int idx = 0; idx < this.m_imageCount; ++idx)
          {
            int resource3 = (int) resourceManager.CreateResource(this.m_pKeyset.GetKey((uint) idx), out resource1);
            if (resource1 != null)
            {
              this.m_pImages[idx] = new CImageWidget();
              this.m_pImages[idx].SetID(0);
              this.m_pImages[idx].SetImage((ICRenderSurface) resource1.GetData());
              this.m_pImages[idx].SetTransparent(true);
            }
          }
        }
      }
      if (this.m_pImageIds != null)
      {
        this.m_pImages = new CImageWidget[this.m_imageCount];
        for (int index = 0; index < this.m_imageCount; ++index)
        {
          int resource4 = (int) resourceManager.CreateResource(this.m_pImageIds[index], out resource1);
          if (resource1 != null)
          {
            this.m_pImages[index] = new CImageWidget();
            this.m_pImages[index].SetID(0);
            this.m_pImages[index].SetImage((ICRenderSurface) resource1.GetData());
            this.m_pImages[index].SetTransparent(true);
          }
        }
      }
      if (this.m_imageCount <= 1)
        return;
      this.SetArrow(true, true);
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      if (this.m_keysetId != null)
        resourceManager.ReleaseResource(this.m_keysetId);
      this.m_pImages = (CImageWidget[]) null;
      if (this.m_pImageIds == null)
        return;
      for (int index = 0; index < this.m_imageCount; ++index)
        resourceManager.ReleaseResource(this.m_pImageIds[index]);
    }

    public override void Build()
    {
      base.Build();
      if (this.m_pImages != null)
      {
        for (int index = 0; index < this.m_imageCount; ++index)
          this.m_base.AddChild((CUIWidget) this.m_pImages[index], 0);
      }
      if (this.m_pImageTexts == null)
        return;
      this.m_pTexts = new CTextWidget[this.m_imageCount];
      for (int index = 0; index < this.m_imageCount; ++index)
      {
        this.m_pTexts[index] = new CTextWidget();
        this.m_pTexts[index].SetID(0);
        this.m_pTexts[index].SetAlignment(2);
        this.m_pTexts[index].SetTransparent(true);
        this.m_pTexts[index].SetFont(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
        this.m_pTexts[index].SetText(this.m_pImageTexts[index]);
        this.m_base.AddChild((CUIWidget) this.m_pTexts[index], 0);
      }
    }

    public override void SetTouchRegions()
    {
      this.ClearAllTouchRegions();
      if (this.sgArrowLeft != null)
      {
        this.sgArrowLeft.Bounds(ref this.arrowRect);
        this.AddTouchRegion(this.arrowRect, 2535498699U);
      }
      if (this.sgArrowRight != null)
      {
        this.sgArrowRight.Bounds(ref this.arrowRect);
        this.AddTouchRegion(this.arrowRect, 1913978637U);
      }
      CRectangle rect = this.m_pImages[this.m_curImageIndex].GetRect();
      if (rect.m_y + rect.m_dy > this.arrowRect.m_y)
        rect.m_dy = this.arrowRect.m_y - rect.m_y - 5;
      this.AddTouchRegion(rect, 544526345U);
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
      if (this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
      {
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
        this.rect.m_x += (int) (short) (this.rect.m_dx - this.m_pImages[this.m_curImageIndex].GetContentWidth() >> 1);
        int dy = this.rect.m_dy;
        if (CBitMath.TEST_MASK(this.m_flags, 8))
          this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[this.m_curImageIndex].GetContentHeight());
        else
          this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[this.m_curImageIndex].GetContentHeight() + this.m_pTexts[this.m_curImageIndex].GetTextHeight() >> 1);
        this.rect.m_dx = (int) (short) this.m_pImages[this.m_curImageIndex].GetContentWidth();
        this.rect.m_dy = (int) (short) this.m_pImages[this.m_curImageIndex].GetContentHeight();
        this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref this.titleRect);
        short num = (short) this.rect.m_y;
        if (this.rect.m_y < this.titleRect.m_y)
        {
          this.SetFlags(9);
          num = (short) (this.titleRect.m_y + this.titleRect.m_dy + (this.titleRect.m_dy >> 1));
        }
        this.m_pImages[this.m_curImageIndex].SetVisible(true);
        if (this.m_pTexts != null)
        {
          this.m_pTexts[this.m_curImageIndex].SetVisible(true);
          if (!CBitMath.TEST_MASK(this.m_flags, 8))
            this.rect.m_y += (int) (short) (dy - this.m_pImages[this.m_curImageIndex].GetContentHeight() - this.m_pTexts[this.m_curImageIndex].GetTextHeight() >> 1);
          this.rect.m_y += this.rect.m_dy;
          this.m_pTexts[this.m_curImageIndex].SetRect(this.rect);
        }
        this.rect.m_y = (int) num;
        this.m_pImages[this.m_curImageIndex].SetRect(this.rect);
      }
      if (this.m_movie.GetUserRegionVisible(4U))
      {
        this.m_movie.GetUserRegion(4U, ref this.rect);
        int itemIndex = -1;
        if (this.m_state == 1)
        {
          itemIndex = this.GetPrevImage();
          this.UpdateSoftkeys(this.ItemIsSelectable(itemIndex) ? "SUR_SOFTKEY_CHECK" : "", "SUR_SOFTKEY_ARROW");
        }
        else if (this.m_state == 2)
        {
          itemIndex = this.GetNextImage();
          this.UpdateSoftkeys(this.ItemIsSelectable(itemIndex) ? "SUR_SOFTKEY_CHECK" : "", "SUR_SOFTKEY_ARROW");
        }
        if (itemIndex >= 0)
        {
          this.rect.m_x += (int) (short) (this.rect.m_dx - this.m_pImages[itemIndex].GetContentWidth() >> 1);
          int dy = this.rect.m_dy;
          if (CBitMath.TEST_MASK(this.m_flags, 8))
            this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[itemIndex].GetContentHeight());
          else
            this.rect.m_y += (int) (short) (this.rect.m_dy - this.m_pImages[itemIndex].GetContentHeight() + this.m_pTexts[this.m_curImageIndex].GetTextHeight() >> 1);
          this.rect.m_dx = (int) (short) this.m_pImages[itemIndex].GetContentWidth();
          this.rect.m_dy = (int) (short) this.m_pImages[itemIndex].GetContentHeight();
          this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref this.titleRect);
          short num = (short) this.rect.m_y;
          if (this.rect.m_y < this.titleRect.m_y)
          {
            this.SetFlags(9);
            num = (short) (this.titleRect.m_y + this.titleRect.m_dy + (this.titleRect.m_dy >> 1));
          }
          this.m_pImages[itemIndex].SetVisible(true);
          if (this.m_pTexts != null)
          {
            this.m_pTexts[itemIndex].SetVisible(true);
            if (!CBitMath.TEST_MASK(this.m_flags, 8))
              this.rect.m_y += (int) (short) (dy - this.m_pImages[itemIndex].GetContentHeight() - this.m_pTexts[this.m_curImageIndex].GetTextHeight() >> 1);
            this.rect.m_y += this.rect.m_dy;
            this.m_pTexts[itemIndex].SetRect(this.rect);
          }
          this.rect.m_y = (int) num;
          this.m_pImages[itemIndex].SetRect(this.rect);
        }
      }
      if (this.m_imageCount <= 1)
        return;
      if (this.m_movie.GetUserRegionVisible(5U))
      {
        this.m_movie.GetUserRegion(5U, ref this.rect);
        this.sgArrowLeft.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
      }
      if (!this.m_movie.GetUserRegionVisible(6U))
        return;
      this.m_movie.GetUserRegion(6U, ref this.rect);
      this.sgArrowRight.SetPosition((short) (this.rect.m_x + this.rect.m_dx / 2), (short) (this.rect.m_y + this.rect.m_dy / 2));
    }

    public int GetNextImage() => (this.m_curImageIndex + 1) % this.m_imageCount;

    public int GetPrevImage()
    {
      int num = this.m_curImageIndex - 1;
      return num >= 0 ? num : this.m_imageCount - 1;
    }

    public int GetNextImage(int index) => (index + 1) % this.m_imageCount;

    public int GetPrevImage(int index)
    {
      int num = index - 1;
      return num >= 0 ? num : this.m_imageCount - 1;
    }

    public enum SelectableMode
    {
      MENU_ITEM_IS_SELECTABLE,
      MENU_ITEM_IS_NON_SELECTABLE,
    }

    public enum eSubState
    {
      STATE_IDLE,
      STATE_MOVE_LEFT_TO_RIGHT,
      STATE_MOVE_RIGHT_TO_LEFT,
    }
  }
}
