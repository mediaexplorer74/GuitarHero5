// Decompiled with JetBrains decompiler
// Type: com.glu.game.CNavigatorWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CNavigatorWidget : CUIWidget
  {
    protected int m_pageCount;
    protected string[] m_pwszContent;
    protected int m_selIndex;
    protected ICRenderSurface m_pLeftImage;
    protected ICRenderSurface m_pRightImage;
    protected CRectangle m_leftRect;
    protected CRectangle m_rightRect;
    protected CTextWidget m_text;
    private CRectangle ldrect;

    public CNavigatorWidget()
    {
      this.m_pageCount = 0;
      this.m_pwszContent = (string[]) null;
      this.m_selIndex = 0;
      this.m_pLeftImage = (ICRenderSurface) null;
      this.m_pRightImage = (ICRenderSurface) null;
    }

    public void SetLeftImage(ICRenderSurface pImg)
    {
      this.m_pLeftImage = pImg;
      this.OnSetNewLayout();
    }

    public void SetRightImage(ICRenderSurface pImg)
    {
      this.m_pRightImage = pImg;
      this.OnSetNewLayout();
    }

    private void SetMaxLines(int maxLines)
    {
      this.m_text.SetMaxLines(maxLines);
      this.OnSetNewLayout();
    }

    public void SetPageCount(int pageCount)
    {
      this.m_pageCount = pageCount;
      this.OnSetNewLayout();
    }

    public void SetFont(CFont pFont)
    {
      this.m_text.SetFont(pFont);
      this.OnSetNewLayout();
    }

    public void SetSingleText(string pwszContent)
    {
      this.m_text.SetText(pwszContent);
      this.OnSetNewLayout();
    }

    private void SetAllText(string[] pwszContent)
    {
      this.m_pwszContent = pwszContent;
      this.SetSingleText(pwszContent[this.m_selIndex]);
    }

    public void SetSelectionIndex(int idx)
    {
      idx = CMath.Max(0, idx);
      idx = CMath.Min(this.m_pageCount - 1, idx);
      this.m_selIndex = idx;
      if (this.m_pwszContent == null)
        return;
      this.SetSingleText(this.m_pwszContent[idx]);
      this.OnSetNewLayout();
    }

    public int GetSelectionIndex() => this.m_selIndex;

    public override void Layout()
    {
      this.m_text.SetParent((CWidget) this);
      this.m_text.SetColor(this.m_backgroundColor, this.m_foregroundColor, this.m_highlightColor);
      this.m_text.SetFocusable(this.GetFocusable());
      this.m_text.SetSelectable(false);
      this.m_text.SetTransparent(this.GetTransparent());
      this.m_text.SetAlignment(2);
      this.m_leftRect.Clear();
      if (this.m_pLeftImage != null)
      {
        uint width;
        uint height;
        this.m_pLeftImage.GetWidthAndHeight(out width, out height);
        this.m_leftRect.m_dx = (int) (short) width;
        this.m_leftRect.m_dy = (int) (short) height;
      }
      if (this.m_pRightImage != null)
      {
        uint width;
        uint height;
        this.m_pRightImage.GetWidthAndHeight(out width, out height);
        this.m_rightRect.m_dx = (int) (short) width;
        this.m_rightRect.m_dy = (int) (short) height;
      }
      int val1 = CMath.Max(this.m_leftRect.m_dy, this.m_rightRect.m_dy);
      CRectangle rect;
      rect.m_x = this.m_leftRect.m_dx;
      rect.m_dx = (int) (short) CMath.Max(0, this.m_rect.m_dx - this.m_leftRect.m_dx - this.m_rightRect.m_dx);
      rect.m_y = 0;
      rect.m_dy = this.m_rect.m_dy;
      this.m_text.SetRect(rect);
      this.m_text.HandleLayout();
      int contentHeight = this.m_text.GetContentHeight();
      int num = CMath.Max(val1, contentHeight);
      this.m_leftRect.m_x = 0;
      this.m_leftRect.m_y = num - this.m_leftRect.m_dy >> 1;
      this.m_rightRect.m_x = this.m_rect.m_dx - this.m_rightRect.m_dx;
      this.m_rightRect.m_y = num - this.m_rightRect.m_dy >> 1;
      rect.m_y = num - contentHeight >> 1;
      rect.m_dy = contentHeight;
      this.m_text.SetRect(rect);
      this.m_text.HandleLayout();
      this.m_contentWidth = 0U;
      this.m_contentHeight = (uint) num;
    }

    public override void Paint()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (this.GetFocus())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_highlightColor);
      }
      else if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      if (this.m_pLeftImage != null && this.m_selIndex > 0)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(this.m_leftRect.m_x), CMathFixed.Int32ToFixed(this.m_leftRect.m_y));
        instance1.Draw(this.m_pLeftImage);
        instance1.PopTransform();
      }
      if (this.m_pRightImage != null && this.m_selIndex < this.m_pageCount - 1)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        this.m_pRightImage.GetWidthAndHeight(out uint _, out uint _);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(this.m_rightRect.m_x), CMathFixed.Int32ToFixed(this.m_rightRect.m_y));
        instance1.Draw(this.m_pRightImage);
        instance1.PopTransform();
      }
      this.m_text.SetDirty();
      this.m_text.HandleRender();
    }

    public override bool OnInputEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      switch (id)
      {
        case 1913978637:
          if (this.m_selIndex < this.m_pageCount - 1)
          {
            this.SetSelectionIndex(this.m_selIndex + 1);
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION, 0U, (object) this);
            flag = true;
            break;
          }
          break;
        case 2535498699:
          if (this.m_selIndex > 0)
          {
            this.SetSelectionIndex(this.m_selIndex - 1);
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION, 0U, (object) this);
            flag = true;
            break;
          }
          break;
      }
      return flag;
    }

    public override bool OnMouseEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.GetActive() && this.GetVisible() && this.GetFocusable())
      {
        this.clip.Clear();
        this.GetClipRect(ref this.clip);
        CRectangle r;
        r.m_x = (int) (short) MouseUtil.MOUSE_EVENT_GET_X(param2);
        r.m_y = (int) (short) MouseUtil.MOUSE_EVENT_GET_Y(param2);
        r.m_dx = 1;
        r.m_dy = 1;
        if (this.clip.Contains(r))
        {
          switch (id)
          {
            case 902053462:
            case 2300082508:
              this.SetFocus(true);
              this.SetSelection(true);
              this.GetLocalDrawRect(ref this.ldrect);
              r.m_x -= this.clip.m_x + this.ldrect.m_x;
              r.m_y -= this.clip.m_y + this.ldrect.m_y;
              if (this.m_leftRect.Contains(r))
              {
                if (this.m_selIndex > 0)
                {
                  this.SetSelectionIndex(this.m_selIndex - 1);
                  this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION, 0U, (object) this);
                }
              }
              else if (this.m_rightRect.Contains(r))
              {
                if (this.m_selIndex < this.m_pageCount - 1)
                {
                  this.SetSelectionIndex(this.m_selIndex + 1);
                  this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION, 0U, (object) this);
                }
              }
              else
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, 0U, (object) this);
              flag = true;
              break;
          }
          this.OnSetDirty();
        }
      }
      return flag;
    }

    public override bool OnSetFocus(bool focus)
    {
      base.OnSetFocus(focus);
      this.m_text.SetFocus(focus);
      return true;
    }

    public override bool OnSetSelection(bool selection)
    {
      base.OnSetSelection(selection);
      this.m_text.SetSelection(selection);
      return true;
    }
  }
}
