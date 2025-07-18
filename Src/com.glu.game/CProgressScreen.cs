// Decompiled with JetBrains decompiler
// Type: com.glu.game.CProgressScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CProgressScreen : CWidgetScreen
  {
    protected string m_imageID;
    protected string m_textID;
    protected string m_tipsTextID;
    protected bool m_bProgressCompleted;
    protected int m_progressPercent;
    protected bool m_dirty;
    protected ICRenderSurface m_pImage;
    protected string m_pText;
    protected string m_wcsTipsText = "";
    protected string m_downloadingText = "";
    protected string m_wcsOverrideText = "";
    protected CImageWidget m_image = new CImageWidget();
    protected CTextWidget m_text = new CTextWidget();
    protected CTextWidget m_tipsText = new CTextWidget();
    protected CProgressWidget m_progress = new CProgressWidget();
    protected SG_Presenter sgBoard = new SG_Presenter();
    protected SG_Presenter sgFills = new SG_Presenter();
    protected string m_strSubTitle;
    protected bool m_bDisplayProgressPercentage;
    protected int m_progressTextDisplayPercentage;
    protected int m_blinkTimeElapsed;
    protected int m_numDots;
    protected bool m_dontDisplayTitle;
    private int PROGRESSSCREEN_SPACING = 5;
    private int BLINK_INTERVAL = 200;
    private int MAX_DOTS = 3;
    private string kwszPercentage = "%i%";

    public void SetProgressPercent(int percent) => this.m_progressPercent = percent;

    public int GetProgressPercent() => this.m_progressPercent;

    public void SetProgressComplete() => this.m_bProgressCompleted = true;

    public bool ProgressCompleted() => this.m_bProgressCompleted;

    public void SetTipsText(string id) => this.m_tipsTextID = id;

    public void SetProgressTextDisplayPercentage(int percentage)
    {
      this.m_progressTextDisplayPercentage = percentage;
    }

    public void SetDisplayProgressPercentage() => this.m_bDisplayProgressPercentage = true;

    public CProgressScreen()
    {
      this.m_backgroundColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, 0, 0, 0);
      this.m_imageID = (string) null;
      this.m_textID = (string) null;
      this.m_tipsTextID = (string) null;
      this.m_bProgressCompleted = false;
      this.m_progressPercent = 0;
      this.m_dirty = true;
      this.m_bDisplayProgressPercentage = false;
      this.m_pImage = (ICRenderSurface) null;
      this.m_pText = (string) null;
      this.m_strSubTitle = "";
      this.m_blinkTimeElapsed = 0;
      this.m_numDots = 0;
    }

    public void SetInfo(uint backgroundColor, string imageID, string textID)
    {
      this.m_backgroundColor = backgroundColor;
      this.m_imageID = imageID;
      this.m_textID = textID;
    }

    public void UpdateTextString(string text) => this.m_pText = text;

    public void UpdateText(string textID)
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_textID != null)
        resourceManager.ReleaseResource(this.m_textID);
      this.m_textID = textID;
      if (this.m_textID == null)
        return;
      int resource2 = (int) resourceManager.CreateResource(this.m_textID, out resource1);
      if (resource1 == null)
        return;
      this.m_pText = ((CStrChar) resource1.GetData()).ToString();
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (id == 555763780U)
      {
        int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
        this.SetInterrupt(2);
        flag = true;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      if (this.m_progressPercent != this.m_progress.GetPercent())
      {
        this.m_progress.SetPercent(this.m_progressPercent);
        this.m_dirty = true;
      }
      if (this.m_bProgressCompleted && this.m_interrupt == 0)
        this.m_interrupt = 1;
      this.m_blinkTimeElapsed += timeElapsedMS;
      if (this.m_blinkTimeElapsed >= this.BLINK_INTERVAL)
      {
        this.m_blinkTimeElapsed = 0;
        this.m_numDots = (this.m_numDots + 1) % (this.MAX_DOTS + 1);
      }
      return true;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_imageID != null)
      {
        int resource2 = (int) resourceManager.CreateResource(this.m_imageID, out resource1);
        if (resource1 != null)
          this.m_pImage = (ICRenderSurface) resource1.GetData();
      }
      if (this.m_textID != null)
      {
        int resource3 = (int) resourceManager.CreateResource(this.m_textID, out resource1);
        if (resource1 != null)
          this.m_pText = ((CStrChar) resource1.GetData()).ToString();
      }
      if (this.m_tipsTextID != null)
        this.m_wcsTipsText = CGameApp.GetInstance().GetOverrideText(this.m_tipsTextID);
      lock (CGameApp.loadQueuedLock)
      {
        SG_Home.GetInstance().QueueArchetypeCharacter(5, 0);
        do
          ;
        while (SG_Home.GetInstance().LoadQueued(CResBank.kResMaxTimePerUpdateMS, out bool _));
      }
      this.sgBoard = new SG_Presenter(5, 0);
      this.sgBoard.SetAnimation(0);
      this.sgBoard.SetPosition(0, 0);
      this.sgFills = new SG_Presenter(5, 0);
      this.sgFills.SetAnimation(1);
      this.sgFills.SetPosition(0, 0);
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      if (this.m_imageID != null)
        resourceManager.ReleaseResource(this.m_imageID);
      if (this.m_textID != null)
        resourceManager.ReleaseResource(this.m_textID);
      if (this.m_tipsTextID != null)
        resourceManager.ReleaseResource(this.m_tipsTextID);
      this.sgBoard = (SG_Presenter) null;
      this.sgFills = (SG_Presenter) null;
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      if (this.m_pImage != null)
      {
        this.m_image.SetID(0);
        this.m_image.SetColor(this.m_backgroundColor, uint.MaxValue, 4278233031U);
        this.m_image.SetImage(this.m_pImage);
        this.m_base.AddChild((CUIWidget) this.m_image, 0);
      }
      this.m_text.SetID(0);
      this.m_text.SetAlignment(2);
      this.m_text.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_text.SetTransparent(true);
      this.m_text.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_text.SetText(this.m_strSubTitle);
      this.m_base.AddChild((CUIWidget) this.m_text, 0);
      if (this.m_wcsTipsText.Length <= 0)
        return;
      this.m_tipsText.SetID(0);
      this.m_tipsText.SetAlignment(18);
      this.m_tipsText.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_tipsText.SetTransparent(true);
      this.m_tipsText.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_tipsText.SetText(this.m_wcsTipsText);
      this.m_tipsText.SetActive(true);
      this.m_base.AddChild((CUIWidget) this.m_tipsText, 0);
    }

    public override void Layout()
    {
      base.Layout();
      CRectangle rect = this.m_base.GetRect();
      int num1 = this.PROGRESSSCREEN_SPACING + rect.m_dy * 3 / 4;
      CRectangle pRegion;
      if (this.m_pImage != null)
      {
        this.m_image.SetRect(rect);
        this.m_image.HandleLayout();
        pRegion.m_x = (int) (short) (rect.m_dx - this.m_image.GetContentWidth() >> 1);
        pRegion.m_y = (int) (short) (rect.m_dy - this.m_image.GetContentHeight() >> 1);
        pRegion.m_dx = (int) (short) this.m_image.GetContentWidth();
        pRegion.m_dy = (int) (short) this.m_image.GetContentHeight();
        this.m_image.SetRect(pRegion);
        num1 = this.PROGRESSSCREEN_SPACING + (rect.m_dy + pRegion.m_dy >> 1);
      }
      this.m_text.SetRect(rect);
      this.m_text.HandleLayout();
      pRegion.m_x = rect.m_x;
      pRegion.m_y = rect.m_y + num1;
      pRegion.m_dx = rect.m_dx;
      pRegion.m_dy = this.m_text.GetContentHeight();
      this.m_text.SetRect(pRegion);
      int num2 = num1 + (this.PROGRESSSCREEN_SPACING + pRegion.m_dy);
      if (!this.m_movie.GetUserRegionVisible((uint) this.REGION_SUBTITLE))
        return;
      this.m_movie.GetUserRegion((uint) this.REGION_SUBTITLE, ref pRegion);
      this.m_text.SetRect(pRegion);
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      uint width;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      this.RenderBegin();
      if (this.m_pText != null)
      {
        int num1 = font.MeasureTextWidth(this.m_pText);
        int num2 = (int) Phone.GetWidth() >> 1;
        int y1 = this.m_tipsTextID == null ? (int) Phone.GetHeight() >> 1 : (int) Phone.GetHeight() - 2 * font.GetFontHeight();
        if (!this.m_bDisplayProgressPercentage)
          ;
        int length = this.m_pText.Length;
        int num3 = 0;
        string text = (string) null;
        if (this.m_bDisplayProgressPercentage)
          text = string.Concat((object) this.m_progressTextDisplayPercentage);
        int x1 = num2 - (num1 >> 1);
        font.PaintText(this.m_pText, length, x1, y1);
        if (this.m_bDisplayProgressPercentage)
        {
          int y2 = y1 + font.GetFontHeight();
          int x2 = x1 + (num1 >> 1) - (num3 >> 1);
          font.PaintText(text, text.Length, x2, y2);
        }
      }
      CRectangle pRegion;
      pRegion.m_x = 30;
      pRegion.m_y = ((int) Phone.GetHeight() >> 1) - this.m_tipsText.GetContentHeight() + font.GetFontHeight() - (font.GetFontHeight() >> 3);
      pRegion.m_dx = (int) Phone.GetWidth() - 60;
      pRegion.m_dy = (int) Phone.GetHeight() >> 1;
      this.m_tipsText.SetRect(pRegion);
      this.m_tipsText.SetVisible(true);
      if (this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
      {
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref pRegion);
        if (this.sgBoard != null)
          this.sgBoard.Draw((int) width, pRegion.m_y + pRegion.m_dy / 2);
        CRectangle clip = instance.GetClip();
        pRegion.m_x = 0;
        pRegion.m_dx = (int) (short) ((long) this.m_progressPercent * (long) width / 100L);
        instance.SetClip(pRegion);
        if (this.sgFills != null)
          this.sgFills.Draw((int) width, pRegion.m_y + pRegion.m_dy / 2);
        instance.SetClip(clip);
      }
      this.RenderEnd();
      return true;
    }
  }
}
