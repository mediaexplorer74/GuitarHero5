// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMenuScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMenuScreen : CWidgetScreen
  {
    protected int m_items;
    protected int m_selection;
    protected bool m_scrollable;
    protected string m_strText;
    protected CTextWidget m_text = new CTextWidget();
    protected CSpacerWidget m_spacer = new CSpacerWidget();
    protected CSelectItemGroupWidget m_sig = new CSelectItemGroupWidget();
    protected CSelectItemWidget[] m_si = new CSelectItemWidget[Consts.kMaxMenuItems];
    protected string[] m_strItem = new string[Consts.kMaxMenuItems];
    protected bool m_bDontPlaySFXOnAdvance;
    protected bool m_bHasSpacer;
    private CRectangle arrowRect;

    public int GetSelection() => this.m_selection;

    public void SetDontPlaySFXOnAdvance() => this.m_bDontPlaySFXOnAdvance = true;

    public void SetScroll(bool scrollable) => this.m_scrollable = scrollable;

    public void IncludeSpacing(bool hasSpace) => this.m_bHasSpacer = hasSpace;

    public CMenuScreen()
    {
      this.m_items = 0;
      this.m_selection = 0;
      this.m_scrollable = false;
      this.m_bDontPlaySFXOnAdvance = false;
      this.m_bHasSpacer = true;
    }

    public void SetTextWithID(string textID) => CUtility.GetString(out this.m_strText, textID);

    public void SetText(string pwszText) => this.m_strText = pwszText;

    private void SetItems(int items) => this.m_items = items;

    public void SetMenu(string[] pResId, int items)
    {
      this.m_items = items;
      if (pResId == null)
        return;
      for (int idx = 0; idx < items; ++idx)
        this.SetMenuItem(idx, pResId[idx]);
    }

    public void SetMenuItem(int idx, string resId)
    {
      if (idx >= this.m_items)
        return;
      CUtility.GetString(out this.m_strItem[idx], resId);
    }

    public override void SetTouchRegions()
    {
      this.ClearAllTouchRegions();
      this.m_page.GetUpArrowRect(ref this.arrowRect);
      this.arrowRect.Inset(0);
      this.AddTouchRegion(this.arrowRect, 1066869024U);
      this.m_page.GetDownArrowRect(ref this.arrowRect);
      this.arrowRect.Inset(0);
      this.AddTouchRegion(this.arrowRect, 2535467201U);
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      CGHStaticData.GetInstance();
      bool flag = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 129075783:
          this.m_selection = ((CWidget) param2).GetID();
          if (!this.m_bDontPlaySFXOnAdvance)
          {
            int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
          }
          this.SetInterrupt(1);
          flag = true;
          break;
        case 555763780:
          this.m_selection = 0;
          int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          this.SetInterrupt(2);
          flag = true;
          break;
        case 850690755:
          this.m_movie.Refresh();
          this.HandleEvent(id, param1, param2);
          this.m_page.SetDirty();
          flag = true;
          break;
        case 1066869024:
        case 2535467201:
          int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override void Build()
    {
      base.Build();
      this.m_page.SetWrap(true);
      CFontMgr instance = CFontMgr.GetInstance();
      if (this.m_strText != null && this.m_strText.Length > 0)
      {
        this.m_text.SetID(0);
        this.m_text.SetAlignment(2);
        this.m_text.SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_text.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
        this.m_text.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
        this.m_text.SetText(this.m_strText);
        this.m_page.Add((CUIWidget) this.m_text);
        this.m_spacer.SetID(0);
        this.m_spacer.SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_spacer.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
        this.m_spacer.SetHeight(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight());
        if (this.m_bHasSpacer)
          this.m_page.Add((CUIWidget) this.m_spacer);
      }
      this.m_sig.SetID(0);
      this.m_sig.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_sig.SetFocusable(true);
      this.m_sig.SetSelectable(true);
      this.m_sig.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_sig.SetMode(CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU);
      this.m_sig.SetWrap(true);
      this.m_page.Add((CUIWidget) this.m_sig);
      for (int id = 0; id < this.m_items; ++id)
      {
        this.m_si[id] = new CSelectItemWidget();
        this.m_si[id].SetID(id);
        this.m_si[id].SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_si[id].SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
        this.m_si[id].SetFocusable(true);
        this.m_si[id].SetSelectable(true);
        this.m_si[id].SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
        this.m_si[id].SetChecked(false);
        this.m_si[id].SetAlignment(2);
        this.m_sig.Add(this.m_si[id]);
      }
      for (int index = 0; index < this.m_items; ++index)
        this.m_si[index].SetText(this.m_strItem[index]);
      this.m_sig.SetSelected(this.m_selection);
      this.SetContentLineHeightMultiple(instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight());
      this.m_page.SetScrollArrows((ICRenderSurface) null, (ICRenderSurface) null);
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      this.SetTouchRegions();
      return true;
    }

    public void SetSelection(int idx) => this.m_selection = idx;

    public override void Layout()
    {
      base.Layout();
      int num = this.m_forceDisableSlider ? 1 : 0;
    }
  }
}
