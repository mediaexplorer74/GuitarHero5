// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSelectItemGroupWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSelectItemGroupWidget : CVerticalContainerWidget
  {
    protected bool m_wrap;
    protected CSelectItemGroupWidget.eSIGWidgetMode m_mode;
    protected int m_maxMultiSelectItems;
    protected SG_Presenter sg_selectedL;
    protected SG_Presenter sg_selectedC;
    protected SG_Presenter sg_selectedR;
    protected int m_selectedWidth;
    protected SG_Presenter sg_cursor;
    private CRectangle ldrect;
    private CRectangle ldrect2;
    private CRectangle rect;

    public void SetWrap(bool wrap) => this.m_wrap = wrap;

    public CSelectItemGroupWidget()
    {
      this.m_wrap = false;
      this.m_mode = CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU;
      this.m_maxMultiSelectItems = 1;
      this.sg_selectedL = (SG_Presenter) null;
      this.sg_selectedC = (SG_Presenter) null;
      this.sg_selectedR = (SG_Presenter) null;
      this.sg_cursor = (SG_Presenter) null;
    }

    public void Add(CSelectItemWidget pWidget)
    {
      if (pWidget == null)
        return;
      pWidget.SetFocusable(true);
      pWidget.SetSelectable(true);
      this.AddChild((CUIWidget) pWidget, this.m_numItems);
      ++this.m_numItems;
      this.OnSetNewLayout();
    }

    public void SetMode(CSelectItemGroupWidget.eSIGWidgetMode mode)
    {
      this.m_mode = mode;
      this.OnSetNewLayout();
    }

    private void SetMaxMultiSelectItems(int maxItems)
    {
      this.m_maxMultiSelectItems = maxItems;
      this.OnSetNewLayout();
    }

    public void SetChecked(int idx, bool boolchecked) => this.Find(idx)?.SetChecked(boolchecked);

    public void SetSelected(int idx)
    {
      CSelectItemWidget pWidget = this.Find(idx);
      if (pWidget == null)
        return;
      this.SetFocusToWidget((CUIWidget) pWidget);
    }

    public void SetAllChecked(bool boolchecked)
    {
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CWidget data = (CWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        ((CSelectItemWidget) data).SetChecked(boolchecked);
      }
    }

    public bool GetChecked(int idx)
    {
      bool flag = false;
      CSelectItemWidget cselectItemWidget = this.Find(idx);
      if (cselectItemWidget != null)
        flag = cselectItemWidget.GetChecked();
      return flag;
    }

    public int GetNumCheckedItems()
    {
      int numCheckedItems = 0;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CWidget data = (CWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (((CSelectItemWidget) data).GetChecked())
          ++numCheckedItems;
      }
      return numCheckedItems;
    }

    private CSelectItemWidget GetNextCheckedItem(CSelectItemWidget pStart)
    {
      CSelectItemWidget nextCheckedItem = (CSelectItemWidget) null;
      CLinkListNode clinkListNode = pStart == null ? this.m_childList.GetHead() : pStart.GetChildNode().GetNext();
      while (clinkListNode != null)
      {
        CWidget data = (CWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        CSelectItemWidget cselectItemWidget = (CSelectItemWidget) data;
        if (cselectItemWidget.GetChecked())
        {
          nextCheckedItem = cselectItemWidget;
          break;
        }
      }
      return nextCheckedItem;
    }

    public override bool OnInputEvent(uint id, uint param1, uint param2)
    {
      bool flag = base.OnInputEvent(id, param1, param2);
      if (!flag)
      {
        switch (id)
        {
          case 544526345:
          case 1600235594:
            if (this.m_pFocus != null)
            {
              if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, (object) this.m_pFocus);
              else if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_RADIO)
              {
                this.SetAllChecked(false);
                ((CSelectItemWidget) this.m_pFocus).SetChecked(true);
              }
              else if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MULTI)
              {
                if (((CSelectItemWidget) this.m_pFocus).GetChecked())
                  ((CSelectItemWidget) this.m_pFocus).SetChecked(false);
                else if (this.m_maxMultiSelectItems <= 0 || this.GetNumCheckedItems() < this.m_maxMultiSelectItems)
                  ((CSelectItemWidget) this.m_pFocus).SetChecked(true);
              }
              flag = true;
              break;
            }
            break;
          case 1066869024:
            if (this.m_pFocus != null)
            {
              if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
                ((CSelectItemWidget) this.m_pFocus).SetChecked(false);
              CWidget pFocus = (CWidget) this.m_pFocus;
              this.GetLocalDrawRect(ref this.ldrect);
              this.SetFocusToNextFocusableWidget(this.m_pFocus, false);
              flag = this.m_pFocus != pFocus;
              CRectangle rect = this.m_pFocus.GetRect();
              if (this.ldrect.m_y > rect.m_y)
              {
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, 1U, (object) (uint) (this.ldrect.m_y - rect.m_y));
                flag = true;
              }
              if (!flag && this.m_wrap)
              {
                this.GetLocalDrawRect(ref this.ldrect2);
                this.SetFocusToNextFocusableWidget((CUIWidget) null, false);
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, 2U, (object) (uint) (this.m_rect.m_dy - this.ldrect2.m_dy));
                flag = true;
              }
              if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
              {
                ((CSelectItemWidget) this.m_pFocus).SetChecked(true);
                break;
              }
              break;
            }
            break;
          case 2535467201:
            if (this.m_pFocus != null)
            {
              if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
                ((CSelectItemWidget) this.m_pFocus).SetChecked(false);
              CWidget pFocus = (CWidget) this.m_pFocus;
              this.GetLocalDrawRect(ref this.ldrect);
              this.SetFocusToNextFocusableWidget(this.m_pFocus, true);
              flag = this.m_pFocus != pFocus;
              CRectangle rect = this.m_pFocus.GetRect();
              if (this.ldrect.m_y + this.ldrect.m_dy < rect.m_y + rect.m_dy)
              {
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, 2U, (object) (uint) (rect.m_y + rect.m_dy - (this.ldrect.m_y + this.ldrect.m_dy)));
                flag = true;
              }
              if (!flag && this.m_wrap)
              {
                this.GetLocalDrawRect(ref this.ldrect2);
                this.SetFocusToNextFocusableWidget((CUIWidget) null, true);
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, 1U, (object) (uint) this.ldrect2.m_y);
                flag = true;
              }
              if (this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
              {
                ((CSelectItemWidget) this.m_pFocus).SetChecked(true);
                break;
              }
              break;
            }
            break;
        }
        if (flag)
          this.OnSetDirty();
      }
      return flag;
    }

    public override bool OnSetFocus(bool focus)
    {
      base.OnSetFocus(focus);
      if (this.m_pFocus != null && this.m_mode == CSelectItemGroupWidget.eSIGWidgetMode.SIGWIDGET_MODE_MENU)
        ((CSelectItemWidget) this.m_pFocus).SetChecked(true);
      return true;
    }

    private CSelectItemWidget Find(int idx)
    {
      CSelectItemWidget cselectItemWidget = (CSelectItemWidget) null;
      int num = 0;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CWidget data = (CWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (idx == num)
        {
          cselectItemWidget = (CSelectItemWidget) data;
          break;
        }
        ++num;
      }
      return cselectItemWidget;
    }

    private void Verify()
    {
    }

    public override void Paint()
    {
      if (this.sg_selectedC == null)
      {
        base.Paint();
      }
      else
      {
        for (int x = 0; x < this.m_selectedWidth - 21; x += 21)
          this.sg_selectedC.Draw(x, 0);
        this.sg_selectedC.Draw(this.m_selectedWidth - 21, 0);
        this.sg_selectedL.Draw();
        this.sg_selectedR.Draw();
        base.Paint();
        ICGraphics2d instance = ICGraphics2d.GetInstance();
        CRectangle rect;
        rect.m_x = 0;
        rect.m_y = 0;
        rect.m_dx = (int) Phone.GetWidth();
        rect.m_dy = (int) Phone.GetHeight();
        CRectangle clip = instance.GetClip();
        instance.SetClip(rect);
        this.sg_cursor.Draw();
        instance.SetClip(clip);
      }
    }

    public override bool OnUpdate(int timeElapsedMS)
    {
      if (this.sg_selectedC == null)
      {
        this.sg_selectedC = new SG_Presenter(3, 0);
        this.sg_selectedC.SetAnimation(17);
        this.sg_selectedC.SetPosition(0, 0);
      }
      if (this.sg_selectedL == null)
      {
        this.sg_selectedL = new SG_Presenter(3, 0);
        this.sg_selectedL.SetAnimation(18);
        this.sg_selectedL.SetPosition(0, 0);
      }
      if (this.sg_selectedR == null)
      {
        this.sg_selectedR = new SG_Presenter(3, 0);
        this.sg_selectedR.SetAnimation(19);
        this.sg_selectedR.SetPosition(0, 0);
      }
      if (this.sg_cursor == null)
      {
        this.sg_cursor = new SG_Presenter(3, 0);
        this.sg_cursor.SetAnimation(7);
        this.sg_cursor.SetPosition(0, 0);
        this.sg_cursor.SetLoop(true);
      }
      this.sg_cursor.Update(timeElapsedMS);
      return true;
    }

    public override void Layout()
    {
      base.Layout();
      CSelectItemWidget focusWidget = (CSelectItemWidget) this.GetFocusWidget();
      if (focusWidget == null)
        return;
      int textWidth = focusWidget.GetTextWidth();
      this.m_selectedWidth = this.m_rect.m_dx / 2 > textWidth ? this.m_rect.m_dx / 2 : textWidth;
      CRectangle rect = focusWidget.GetRect();
      if (this.sg_cursor == null)
        return;
      this.sg_cursor.Bounds(ref this.rect);
      int num1 = (this.m_rect.m_dx >> 1) - (-(this.rect.m_dx / 5) + this.rect.m_dx);
      this.sg_selectedL.Bounds(ref this.rect);
      int num2 = num1 - (this.rect.m_dx >> 1);
      this.m_selectedWidth = CMath.Max(textWidth, num2 << 1);
      int num3 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight() >> 1;
      this.sg_selectedL.SetPosition(this.m_rect.m_dx / 2 - this.m_selectedWidth / 2, rect.m_y + num3);
      this.sg_selectedR.SetPosition((int) this.sg_selectedL.GetPositionX() + this.m_selectedWidth, rect.m_y + num3);
      this.sg_selectedC.SetPosition((int) this.sg_selectedL.GetPositionX(), rect.m_y + num3);
      this.sg_cursor.SetPosition(0, rect.m_y + num3);
    }

    public enum eSIGWidgetMode
    {
      SIGWIDGET_MODE_MENU,
      SIGWIDGET_MODE_RADIO,
      SIGWIDGET_MODE_MULTI,
    }
  }
}
