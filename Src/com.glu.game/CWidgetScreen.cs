// Decompiled with JetBrains decompiler
// Type: com.glu.game.CWidgetScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CWidgetScreen : CSoftkeyScreen
  {
    public int MOVIES_USED_FOR_MENUS = 1;
    public CMovie m_movie = new CMovie();
    public int m_chapterIn;
    public int m_chapterOut;
    public int m_chapterIdle;
    public int m_chapterDef;
    public int REGION_OPTIONS;
    public int REGION_TITLE;
    public static int REGION_CONTENT;
    public int REGION_SLIDE_BAR;
    public int REGION_LSOFTKEY;
    public int REGION_SUBTITLE;
    public int m_movieState;
    public uint m_eventId;
    public uint m_eventParam1;
    public object m_eventParam2;
    protected string m_titleID;
    protected string m_skinID;
    protected int m_insetX;
    protected int m_insetY;
    protected int m_flags;
    protected int m_titleWidth;
    protected int m_titleX;
    protected string m_pTitleText;
    protected ICRenderSurface m_pFrameTopImage;
    protected ICRenderSurface m_pFrameBottomImage;
    protected ICRenderSurface m_pFrameLeftImage;
    protected ICRenderSurface m_pFrameRightImage;
    protected ICRenderSurface m_pFrameTopLeftImage;
    protected ICRenderSurface m_pFrameTopRightImage;
    protected ICRenderSurface m_pFrameBottomLeftImage;
    protected ICRenderSurface m_pFrameBottomRightImage;
    protected ICRenderSurface m_pSkinImage;
    protected CFrameWidget m_frame = new CFrameWidget();
    protected CLayoutWidget m_layout = new CLayoutWidget();
    protected CTextWidget m_title = new CTextWidget();
    protected CPageWidget m_page = new CPageWidget();
    protected CSliderWidget m_slider = new CSliderWidget();
    protected uint m_backgroundColor;
    protected string m_movieID;
    protected SG_Presenter sgArrowLeft = new SG_Presenter();
    protected SG_Presenter sgArrowRight = new SG_Presenter();
    protected ICRenderSurface m_pSlideBarTopImage;
    protected ICRenderSurface m_pSlideBarMidImage;
    protected ICRenderSurface m_pSlideBarBotImage;
    protected ICRenderSurface m_pSlideBarHandleImage;
    protected int m_sliderPercent;
    protected bool m_forceDisableSlider;
    protected bool m_enableSliderBar;
    protected int m_displayOnlyFullLineHeight;
    protected uint m_pointerDown_x;
    protected uint m_pointerDown_y;
    protected uint m_pointerLast_x;
    protected uint m_pointerLast_y;
    protected Vector<CWidgetScreen.TouchRegion> touchRegions = new Vector<CWidgetScreen.TouchRegion>();
    protected bool isRegionsSet;
    protected bool m_drawUI;
    private int MAX_TITLE_TEXT = 50;
    private CRectangle rect;

    public virtual void SetTouchRegions()
    {
    }

    public void SetTitle(string id) => this.m_titleID = id;

    public void SetSkin(string id) => this.m_skinID = id;

    public void SetInset(int insetX, int insetY)
    {
      this.m_insetX = insetX;
      this.m_insetY = insetY;
    }

    public void SetFlags(int flags) => this.m_flags = flags;

    public void ForceDisableSlider() => this.m_forceDisableSlider = true;

    public void EnableSliderBar() => this.m_enableSliderBar = true;

    public void SetContentLineHeightMultiple(int lineHeight)
    {
      this.m_displayOnlyFullLineHeight = lineHeight;
    }

    public void TouchPressed(uint x, uint y)
    {
      this.m_pointerDown_x = x;
      this.m_pointerDown_y = y;
    }

    public void TouchPressed(int x, int y)
    {
      this.m_pointerDown_x = (uint) x;
      this.m_pointerDown_y = (uint) y;
    }

    public void TouchMove(uint x, uint y)
    {
      this.m_pointerLast_x = x;
      this.m_pointerLast_y = y;
    }

    public void TouchReset()
    {
      this.m_pointerDown_x = this.m_pointerDown_y = this.m_pointerLast_x = this.m_pointerLast_y = 0U;
    }

    public CWidgetScreen()
    {
      this.touchRegions = new Vector<CWidgetScreen.TouchRegion>();
      this.m_frame = new CFrameWidget();
      this.m_titleID = (string) null;
      this.m_skinID = (string) null;
      this.m_insetX = 0;
      this.m_insetY = 0;
      this.m_titleWidth = 0;
      this.m_flags = 1;
      this.m_pTitleText = (string) null;
      this.m_pFrameTopImage = (ICRenderSurface) null;
      this.m_pFrameBottomImage = (ICRenderSurface) null;
      this.m_pFrameLeftImage = (ICRenderSurface) null;
      this.m_pFrameRightImage = (ICRenderSurface) null;
      this.m_pFrameTopLeftImage = (ICRenderSurface) null;
      this.m_pFrameTopRightImage = (ICRenderSurface) null;
      this.m_pFrameBottomLeftImage = (ICRenderSurface) null;
      this.m_pFrameBottomRightImage = (ICRenderSurface) null;
      this.m_pSkinImage = (ICRenderSurface) null;
      this.m_backgroundColor = 4278190080U;
      this.m_movieID = (string) null;
      this.m_displayOnlyFullLineHeight = 0;
      this.m_eventId = 0U;
      this.m_chapterIn = 0;
      this.m_chapterIdle = 1;
      this.m_chapterOut = 2;
      this.sgArrowLeft = (SG_Presenter) null;
      this.sgArrowRight = (SG_Presenter) null;
      this.m_pSlideBarTopImage = (ICRenderSurface) null;
      this.m_pSlideBarMidImage = (ICRenderSurface) null;
      this.m_pSlideBarBotImage = (ICRenderSurface) null;
      this.m_pSlideBarHandleImage = (ICRenderSurface) null;
      this.REGION_OPTIONS = 0;
      this.REGION_TITLE = 0;
      CWidgetScreen.REGION_CONTENT = 0;
      this.REGION_SLIDE_BAR = 0;
      this.REGION_LSOFTKEY = 0;
      this.m_forceDisableSlider = false;
      this.m_enableSliderBar = false;
      this.TouchReset();
      this.isRegionsSet = false;
      this.m_drawUI = true;
    }

    public override void Activate()
    {
      base.Activate();
      if (this.m_movieID == null)
        return;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_IN);
    }

    public override void Deactivate()
    {
      base.Deactivate();
      if (this.m_movieID == null)
        return;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_NONE);
    }

    private static void RenderCB(CWidget pWidget, CWidget.eWidgetRenderLayer layer, object pData)
    {
      ((CWidgetScreen) pData).OnRenderCB(pWidget, layer);
      if (layer != CWidget.eWidgetRenderLayer.WIDGET_RENDER_LAYER_PRE)
        return;
      ((CWidgetScreen) pData).RenderMovie();
    }

    public void AddTouchRegion(CRectangle rect, uint event_id)
    {
      if (!CGameApp.GetInstance().IsTouchscreenSupported())
        return;
      CWidgetScreen.TouchRegion touchRegion = new CWidgetScreen.TouchRegion();
      touchRegion.rect.Set(rect);
      touchRegion.rect.Inset(-5);
      touchRegion.eventId = event_id;
      if (this.touchRegions == null)
        this.touchRegions = new Vector<CWidgetScreen.TouchRegion>();
      this.touchRegions.Add(touchRegion);
    }

    public void ClearAllTouchRegions()
    {
      this.touchRegions = (Vector<CWidgetScreen.TouchRegion>) null;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_titleID != null)
      {
        int resource2 = (int) resourceManager.CreateResource(this.m_titleID, out resource1);
        if (resource1 != null)
          this.m_pTitleText = ((CStrChar) resource1.GetData()).ToString();
      }
      if (this.m_insetX <= 0)
      {
        int insetY = this.m_insetY;
      }
      if (this.m_skinID != null)
      {
        int resource3 = (int) resourceManager.CreateResource(this.m_skinID, out resource1);
        if (resource1 != null)
          this.m_pSkinImage = (ICRenderSurface) resource1.GetData();
      }
      if (this.m_movieID != null)
        this.LoadMovie();
      CGHStaticData.GetInstance();
      if (CGHStaticData.m_pTitleHead == null)
      {
        CGHStaticData.m_pTitleHead = new SG_Presenter(3, 0);
        CGHStaticData.m_pTitleHead.SetAnimation(22);
        CGHStaticData.m_pTitleHead.SetPosition(-1000, -1000);
      }
      if (CGHStaticData.m_pTitleTail == null)
      {
        CGHStaticData.m_pTitleTail = new SG_Presenter(3, 0);
        CGHStaticData.m_pTitleTail.SetAnimation(23);
        CGHStaticData.m_pTitleTail.SetPosition(-1000, -1000);
      }
      if (CGHStaticData.m_pTitleTile != null)
        return;
      CGHStaticData.m_pTitleTile = new SG_Presenter(3, 0);
      CGHStaticData.m_pTitleTile.SetAnimation(21);
      CGHStaticData.m_pTitleTile.SetPosition(-1000, -1000);
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      if (this.m_titleID != null)
        resourceManager.ReleaseResource(this.m_titleID);
      if (this.m_insetX > 0 || this.m_insetY > 0)
      {
        resourceManager.ReleaseResource("SUR_POP_UP_TOP");
        resourceManager.ReleaseResource("SUR_POP_UP_BOTTOM");
        resourceManager.ReleaseResource("SUR_POP_UP_LEFT");
        resourceManager.ReleaseResource("SUR_POP_UP_RIGHT");
        resourceManager.ReleaseResource("SUR_POP_UP_TOP_LEFT");
        resourceManager.ReleaseResource("SUR_POP_UP_TOP_RIGHT");
        resourceManager.ReleaseResource("SUR_POP_UP_BOTTOM_LEFT");
        resourceManager.ReleaseResource("SUR_POP_UP_BOTTOM_RIGHT");
      }
      if (this.m_skinID != null)
        resourceManager.ReleaseResource(this.m_skinID);
      this.m_movie.Free();
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      this.m_base.SetTransparent(true);
      if (this.m_insetX > 0 || this.m_insetY > 0)
        this.m_softkey.SetTransparent(true);
      this.m_frame.SetID(0);
      this.m_frame.SetColor(this.m_backgroundColor, uint.MaxValue, 4278233031U);
      this.m_frame.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_frame.SetFocusable(true);
      this.m_frame.SetSelectable(true);
      if (this.m_pSkinImage != null)
        this.m_frame.SetRenderCB(new CWidget.tfnWidgetRenderHook(CWidgetScreen.RenderCB), (object) this);
      if (this.m_insetX > 0 || this.m_insetY > 0)
      {
        this.m_frame.SetTImage(this.m_pFrameTopImage);
        this.m_frame.SetBImage(this.m_pFrameBottomImage);
        this.m_frame.SetLImage(this.m_pFrameLeftImage);
        this.m_frame.SetRImage(this.m_pFrameRightImage);
        this.m_frame.SetTLImage(this.m_pFrameTopLeftImage);
        this.m_frame.SetTRImage(this.m_pFrameTopRightImage);
        this.m_frame.SetBLImage(this.m_pFrameBottomLeftImage);
        this.m_frame.SetBRImage(this.m_pFrameBottomRightImage);
      }
      this.m_layout.SetID(0);
      this.m_layout.SetColor(this.m_backgroundColor, uint.MaxValue, 4278233031U);
      this.m_layout.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_layout.SetFocusable(true);
      this.m_layout.SetSelectable(true);
      this.m_frame.SetContent((CUIWidget) this.m_layout);
      if (this.m_pTitleText != null)
      {
        this.m_title.SetID(0);
        this.m_title.SetAlignment(2);
        this.m_title.SetColor(4286484626U, uint.MaxValue, 4278233031U);
        this.m_title.SetTransparent(true);
        this.m_title.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
        this.m_title.SetText(this.m_pTitleText);
        this.m_titleWidth = instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).MeasureTextWidth(this.m_pTitleText);
        this.m_titleX = (int) Phone.GetWidth() / 2 - this.m_titleWidth / 2;
        this.m_titleWidth = CMath.Max((int) Phone.GetWidth() / 2, this.m_titleWidth);
      }
      this.m_page.SetID(0);
      this.m_page.SetColor(this.m_backgroundColor, uint.MaxValue, 4278233031U);
      this.m_page.SetFocusable(true);
      this.m_page.SetSelectable(true);
      this.m_page.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_page.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 4) ? 32 : 16);
      this.m_layout.Add((CUIWidget) this.m_page, CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_CONTENT);
      if (this.m_enableSliderBar)
      {
        this.m_slider.SetID(0);
        this.m_slider.SetColor(4278190080U, uint.MaxValue, 4286484626U);
        this.m_slider.SetShadeColor(4278233031U);
        this.m_slider.SetFocusable(false);
        this.m_slider.SetSelectable(false);
        this.m_page.SetSlider(this.m_slider);
      }
      this.m_base.AddChild((CUIWidget) this.m_frame, 0);
      this.m_base.AddChild((CUIWidget) this.m_title, 0);
      this.m_frame.SetRenderCB(new CWidget.tfnWidgetRenderHook(CWidgetScreen.RenderCB), (object) this);
    }

    public override void Layout()
    {
      base.Layout();
      CRectangle rect = this.m_base.GetRect();
      if (this.m_movieID != null)
      {
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref rect);
        if (this.m_displayOnlyFullLineHeight != 0 && rect.m_dy % this.m_displayOnlyFullLineHeight != 0)
          rect.m_dy -= rect.m_dy % this.m_displayOnlyFullLineHeight;
        if (this.m_forceDisableSlider)
          rect.m_dy = (int) Phone.GetHeight() - rect.m_y;
      }
      if (this.m_insetX > 0 || this.m_insetY > 0)
      {
        rect.m_x += this.m_insetX;
        rect.m_dx -= (this.m_insetX << 1) + 5;
        rect.m_y += this.m_insetY;
        rect.m_dy -= this.m_insetY << 1;
      }
      this.m_frame.SetRect(rect);
      if (this.m_movieID != null && this.m_movie.GetUserRegionVisible((uint) this.REGION_LSOFTKEY))
      {
        this.m_movie.GetUserRegion((uint) this.REGION_LSOFTKEY, ref rect);
        this.m_softkey.m_ofsX = rect.m_x;
        this.m_softkey.m_ofsY = 0;
      }
      else
        this.m_softkey.m_ofsX = -10000;
      if (this.m_pTitleText != null)
      {
        int positionX = (int) Phone.GetWidth() / 2 - this.m_titleWidth / 2;
        if (this.m_movieID != null && this.m_movie.GetUserRegionVisible((uint) this.REGION_TITLE))
        {
          this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref rect);
          CGHStaticData.GetInstance();
          if (CGHStaticData.m_pTitleHead != null)
            CGHStaticData.m_pTitleHead.SetPosition(positionX, rect.m_y);
          if (CGHStaticData.m_pTitleTail != null)
            CGHStaticData.m_pTitleTail.SetPosition(positionX + this.m_titleWidth, rect.m_y);
          if (CGHStaticData.m_pTitleTile != null)
            CGHStaticData.m_pTitleTile.SetPosition(rect.m_x + rect.m_dx, rect.m_y);
        }
      }
      if (this.m_pSlideBarHandleImage == null)
        return;
      this.m_movie.GetUserRegionVisible((uint) this.REGION_SLIDE_BAR);
    }

    private void OnRenderCB(CWidget pWidget, CWidget.eWidgetRenderLayer layer)
    {
      if (layer != CWidget.eWidgetRenderLayer.WIDGET_RENDER_LAYER_PRE || this.m_pSkinImage == null)
        return;
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      uint width;
      uint height;
      this.m_pSkinImage.GetWidthAndHeight(out width, out height);
      CRectangle rect = pWidget.GetRect();
      int v1 = (int) ((long) rect.m_x + ((long) rect.m_dx - (long) width >> 1));
      int v2 = (int) ((long) rect.m_y + ((long) rect.m_dy - (long) height >> 1));
      instance.PushTransform();
      instance.Translate(CMathFixed.Int32ToFixed(v1), CMathFixed.Int32ToFixed(v2));
      instance.Draw(this.m_pSkinImage);
      instance.PopTransform();
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      if (this.m_movieID != null)
      {
        if (this.m_movieState != -1)
          this.m_movie.Update((uint) timeElapsedMS);
        switch (this.m_movieState)
        {
          case 0:
            if (this.m_movie.GetLooped())
            {
              this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_IDLE);
              break;
            }
            break;
          case 1:
            if (!this.isRegionsSet)
            {
              bool flag = true;
              for (int i = 0; i < (int) this.m_movie.GetNumRegions(); ++i)
                flag &= this.m_movie.GetUserRegionVisible((uint) i);
              if (flag)
              {
                this.SetTouchRegions();
                this.isRegionsSet = true;
                break;
              }
              break;
            }
            break;
          case 2:
            if (this.m_movie.GetLooped())
            {
              this.HandleEvent(this.m_eventId, this.m_eventParam1, this.m_eventParam2);
              this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_NONE);
              break;
            }
            break;
          case 4:
            this.HandleEvent(this.m_eventId, this.m_eventParam1, this.m_eventParam2);
            this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_NONE);
            break;
        }
      }
      base.HandleUpdate(timeElapsedMS);
      this.Layout();
      if (this.sgArrowLeft != null)
        this.sgArrowLeft.Update(timeElapsedMS);
      if (this.sgArrowRight != null)
        this.sgArrowRight.Update(timeElapsedMS);
      return false;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 850690755:
          CRectangle pScreen;
          Phone.GetScreen(out pScreen);
          this.m_movie.X = (short) (pScreen.m_dx >> 1);
          this.m_movie.Y = (short) (pScreen.m_dy >> 1);
          if (this.m_pTitleText != null)
          {
            this.m_titleWidth = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).MeasureTextWidth(this.m_pTitleText);
            this.m_titleX = (int) Phone.GetWidth() / 2 - this.m_titleWidth / 2;
            this.m_titleWidth = CMath.Max((int) Phone.GetWidth() / 2, this.m_titleWidth);
          }
          this.Layout();
          flag = true;
          break;
        case 902008092:
        case 1386813809:
          if (this.m_pointerLast_x == 0U)
            this.m_pointerLast_x = this.m_pointerDown_x;
          if (this.m_pointerLast_y == 0U)
            this.m_pointerLast_y = this.m_pointerDown_y;
          CRectangle r1 = new CRectangle((int) (short) this.m_pointerDown_x, (int) (short) this.m_pointerDown_y, 0, 0);
          CRectangle r2 = new CRectangle((int) (short) this.m_pointerLast_x, (int) (short) this.m_pointerLast_y, 0, 0);
          for (int index = 0; index < this.touchRegions.Count; ++index)
          {
            CWidgetScreen.TouchRegion touchRegion = this.touchRegions[index];
            if (touchRegion.rect.Contains(r1) && touchRegion.rect.Contains(r2))
            {
              CApp.GetInstance().OnSystemEvent(touchRegion.eventId, 0U, 0U);
              break;
            }
          }
          this.TouchReset();
          break;
        case 902053462: // Touch Pressed
        case 2300082508:
          this.TouchPressed(TouchUtil.TOUCH_EVENT_GET_X((uint) param2), TouchUtil.TOUCH_EVENT_GET_Y((uint) param2));
          break;
        case 902532892: // Touch Move
        case 2186393822:
          this.TouchMove((uint) TouchUtil.TOUCH_EVENT_GET_X((uint) param2), (uint) TouchUtil.TOUCH_EVENT_GET_Y((uint) param2));
          break;

        // Mouse events (IDs должны быть определены согласно системе событий)
        case 0x30000001: // Mouse Pressed (примерный ID)
          this.TouchPressed(MouseUtil.MOUSE_EVENT_GET_X((uint)param2), MouseUtil.MOUSE_EVENT_GET_Y((uint)param2));
          break;
        case 0x30000002: // Mouse Move (примерный ID)
          this.TouchMove((uint)MouseUtil.MOUSE_EVENT_GET_X((uint)param2), (uint)MouseUtil.MOUSE_EVENT_GET_Y((uint)param2));
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleRender()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      if (this.m_base.GetDirty())
      {
        ICGraphics instance2 = ICGraphics.GetInstance();
        instance2.SetClearColor(new Color.ARGB_fixed((int) byte.MaxValue, 0, 0, 0));
        instance2.ClearBuffers(ICGraphics.BufferFlags.SurfaceBuffers);
      }
      base.HandleRender();
      this.RenderBegin();
      if (this.m_pTitleText != null && this.m_movie.GetUserRegionVisible((uint) this.REGION_TITLE))
      {
        CGHStaticData.GetInstance();
        this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref this.rect);
        int positionX = (int) CGHStaticData.m_pTitleHead.GetPositionX();
        int num = (int) Phone.GetWidth() / 2 - this.m_titleWidth / 2;
        for (int index = 0; index < this.m_titleWidth; index += 32)
        {
          CGHStaticData.m_pTitleTile.SetPosition(num + index, (int) CGHStaticData.m_pTitleHead.GetPositionY());
          CGHStaticData.m_pTitleTile.Draw();
        }
        CGHStaticData.m_pTitleHead.Draw();
        CGHStaticData.m_pTitleTail.Draw();
        CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).PaintText(this.m_pTitleText, this.m_pTitleText.Length, this.m_titleX, this.rect.m_y);
      }
      if (this.m_drawUI)
      {
        if (this.sgArrowLeft != null)
          this.sgArrowLeft.Draw();
        if (this.sgArrowRight != null)
          this.sgArrowRight.Draw();
        if (this.m_sliderPercent >= 0 && this.m_pSlideBarHandleImage != null && this.m_movie.GetUserRegionVisible((uint) this.REGION_SLIDE_BAR))
        {
          this.m_movie.GetUserRegion((uint) this.REGION_SLIDE_BAR, ref this.rect);
          for (int index = 0; index < this.rect.m_dy; index += 19)
          {
            instance1.PushTransform();
            instance1.Translate(CMathFixed.Int32ToFixed(this.rect.m_x + this.rect.m_dx / 2 - 5), CMathFixed.Int32ToFixed(this.rect.m_y + index));
            instance1.Draw(this.m_pSlideBarMidImage);
            instance1.PopTransform();
          }
          instance1.PushTransform();
          instance1.Translate(CMathFixed.Int32ToFixed(this.rect.m_x + this.rect.m_dx / 2 - 10), CMathFixed.Int32ToFixed(this.rect.m_y - 19));
          instance1.Draw(this.m_pSlideBarTopImage);
          instance1.PopTransform();
          instance1.PushTransform();
          instance1.Translate(CMathFixed.Int32ToFixed(this.rect.m_x + this.rect.m_dx / 2 - 10), CMathFixed.Int32ToFixed(this.rect.m_y + this.rect.m_dy));
          instance1.Draw(this.m_pSlideBarBotImage);
          instance1.PopTransform();
          int num = this.m_sliderPercent * (this.rect.m_dy - 19) / 100;
          instance1.PushTransform();
          instance1.Translate(CMathFixed.Int32ToFixed(this.rect.m_x + this.rect.m_dx / 2 - 10), CMathFixed.Int32ToFixed(this.rect.m_y + num));
          instance1.Draw(this.m_pSlideBarHandleImage);
          instance1.PopTransform();
        }
      }
      this.RenderEnd();
      return false;
    }

    private void RenderMovie()
    {
      if (this.m_movieID == null || this.m_movieState == -1)
        return;
      this.m_movie.Draw();
    }

    private void LoadMovie()
    {
      if (this.m_movieID == null)
        return;
      this.m_movie.Init(this.m_movieID);
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.m_movie.X = (short) (width / 2U);
      this.m_movie.Y = (short) (height / 2U);
      this.m_movie.Load();
      this.m_movie.SetUserRegionCallback(0, new MovieRegionCallback(this.TestCallback), (object) this);
      this.m_movie.Loop = false;
      this.m_movie.SetTime(0U);
      this.m_movie.SetVisible(true);
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_IN);
    }

    private void RenderRegion(CRectangle rect)
    {
    }

    private void TestCallback(object pCaller, int regin, ref CRectangle pArea)
    {
      ((CWidgetScreen) pCaller).RenderRegion(pArea);
    }

    public void SetMovie(string movieID)
    {
      if (movieID == null)
        return;
      this.m_movieID = movieID;
      this.m_chapterIn = 0;
      this.m_chapterIdle = 1;
      this.m_chapterOut = 2;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_IN);
      switch (movieID)
      {
        case "GLU_MOVIE_COMMON":
          CWidgetScreen.REGION_CONTENT = 0;
          this.REGION_TITLE = 1;
          this.REGION_LSOFTKEY = 2;
          break;
        case "GLU_MOVIE_SET_LIST":
          this.REGION_TITLE = 0;
          CWidgetScreen.REGION_CONTENT = 1;
          this.REGION_SLIDE_BAR = 2;
          this.REGION_LSOFTKEY = 3;
          break;
        case "GLU_MOVIE_JUDGEMENT":
          this.REGION_TITLE = 0;
          CWidgetScreen.REGION_CONTENT = 1;
          this.REGION_LSOFTKEY = 5;
          break;
        case "GLU_MOVIE_MOVIE":
          CWidgetScreen.REGION_CONTENT = 0;
          this.REGION_LSOFTKEY = 1;
          this.m_chapterOut = 5;
          break;
        case "GLU_MOVIE_ACHIEVEMENTS":
          this.REGION_TITLE = 2;
          this.REGION_LSOFTKEY = 11;
          this.m_chapterOut = 6;
          break;
        case "GLU_MOVIE_LEADERBOARDS":
          this.REGION_TITLE = 0;
          CWidgetScreen.REGION_CONTENT = 2;
          this.REGION_SLIDE_BAR = 3;
          this.REGION_LSOFTKEY = 9;
          break;
        case "GLU_MOVIE_LOADING":
          CWidgetScreen.REGION_CONTENT = 0;
          this.REGION_SUBTITLE = 1;
          break;
        case "GLU_MOVIE_DOWNLOAD":
          this.REGION_TITLE = 1;
          CWidgetScreen.REGION_CONTENT = 0;
          this.REGION_LSOFTKEY = 4;
          this.REGION_SUBTITLE = 3;
          break;
        case "GLU_MOVIE_SELECTION":
          this.REGION_LSOFTKEY = 2;
          this.REGION_TITLE = 1;
          this.m_chapterOut = 6;
          break;
      }
    }

    public virtual void ChangeState(CWidgetScreen.eMovieStates newState)
    {
      switch (newState)
      {
        case CWidgetScreen.eMovieStates.MOVIE_STATE_IN:
          this.m_movie.SetChapter(this.m_chapterIn, false);
          break;
        case CWidgetScreen.eMovieStates.MOVIE_STATE_IDLE:
          this.m_movie.SetChapter(this.m_chapterIdle, false);
          break;
        case CWidgetScreen.eMovieStates.MOVIE_STATE_OUT1:
          this.m_movie.SetChapter(this.m_chapterOut, false);
          this.m_movie.Loop = false;
          break;
        case CWidgetScreen.eMovieStates.MOVIE_STATE_UNDEF1:
          this.m_movie.SetChapter(this.m_chapterDef, false);
          break;
      }
      this.m_movieState = (int) newState;
    }

    public bool HandleMovieOut(uint id, uint param1, object param2)
    {
      if (this.m_movieState != 1 || id != 555763780U && id != 129075783U)
        return false;
      this.m_eventId = id;
      this.m_eventParam1 = param1;
      this.m_eventParam2 = param2;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_OUT1);
      return true;
    }

    public bool HandleMovieOutByAction(uint id, uint param1, uint param2)
    {
      if (this.m_movieState != 1)
        return false;
      this.m_eventId = id;
      this.m_eventParam1 = param1;
      this.m_eventParam2 = (object) param2;
      this.ChangeState(CWidgetScreen.eMovieStates.MOVIE_STATE_OUT1);
      return true;
    }

    public void SetArrow(bool leftArrow, bool rightArrow)
    {
      if (leftArrow)
      {
        this.sgArrowLeft = new SG_Presenter(3, 0);
        this.sgArrowLeft.SetAnimation(28);
        this.sgArrowLeft.SetPosition(-1000, -1000);
        this.sgArrowLeft.SetLoop(true);
      }
      if (!rightArrow)
        return;
      this.sgArrowRight = new SG_Presenter(3, 0);
      this.sgArrowRight.SetAnimation(29);
      this.sgArrowRight.SetPosition(-1000, -1000);
      this.sgArrowRight.SetLoop(true);
    }

    public void SetSlideBar(int percent)
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_pSlideBarHandleImage == null)
      {
        int resource2 = (int) resourceManager.CreateResource("SUR_UI_SLIDE_BAR_HANDLE", out resource1);
        if (resource1 != null)
          this.m_pSlideBarHandleImage = (ICRenderSurface) resource1.GetData();
        int resource3 = (int) resourceManager.CreateResource("SUR_UI_SLIDE_BAR_CENTER", out resource1);
        if (resource1 != null)
          this.m_pSlideBarMidImage = (ICRenderSurface) resource1.GetData();
        int resource4 = (int) resourceManager.CreateResource("SUR_UI_SLIDE_BAR_HEAD", out resource1);
        if (resource1 != null)
          this.m_pSlideBarTopImage = (ICRenderSurface) resource1.GetData();
        int resource5 = (int) resourceManager.CreateResource("SUR_UI_SLIDE_BAR_HEAD_DOWN", out resource1);
        if (resource1 != null)
          this.m_pSlideBarBotImage = (ICRenderSurface) resource1.GetData();
      }
      this.m_sliderPercent = percent;
    }

    public enum eUIFlags
    {
      UI_FLAGS_NONE = 0,
      UI_FLAGS_TRANSPARENT = 1,
      UI_FLAGS_ALIGN_CENTER = 2,
      UI_FLAGS_ALIGN_MIDDLE = 4,
      UI_FLAGS_ALIGN_BOTTOM = 8,
      UI_FLAGS_ALIGN_TOP_OR_MIDDLE = 22, // 0x00000016
    }

    public enum eMovieStates
    {
      MOVIE_STATE_NONE = -1, // 0xFFFFFFFF
      MOVIE_STATE_IN = 0,
      MOVIE_STATE_IDLE = 1,
      MOVIE_STATE_OUT1 = 2,
      MOVIE_STATE_OUT2 = 3,
      MOVIE_STATE_FINISHED = 4,
      MOVIE_STATE_UNDEF1 = 5,
      MOVIE_STATE_UNDEF2 = 6,
    }

    public class TouchRegion
    {
      public CRectangle rect;
      public uint eventId;

      public TouchRegion() => this.eventId = 0U;
    }
  }
}
