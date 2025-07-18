// Decompiled with JetBrains decompiler
// Type: com.glu.game.CLayoutWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CLayoutWidget : CContainerWidget
  {
    protected CUIWidget m_pNavigator;
    protected CUIWidget m_pContent;

    public CLayoutWidget()
    {
      this.m_pNavigator = (CUIWidget) null;
      this.m_pContent = (CUIWidget) null;
    }

    public void Add(CUIWidget pWidget, CLayoutWidget.eLayoutWidgetPosition position)
    {
      switch (position)
      {
        case CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_NAVIGATOR:
          this.RemoveChild(this.m_pNavigator);
          this.m_pNavigator = pWidget;
          break;
        case CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_CONTENT:
          this.RemoveChild(this.m_pContent);
          this.m_pContent = pWidget;
          break;
      }
      if (pWidget != null)
        this.AddChild(pWidget, (int) position);
      this.OnSetNewLayout();
    }

    private CUIWidget GetFirstChild(CLayoutWidget.eLayoutWidgetPosition position)
    {
      CUIWidget pWidget = this.GetFirstChild();
      if (pWidget != null && (CLayoutWidget.eLayoutWidgetPosition) pWidget.GetChildPosition() != position)
        pWidget = this.GetNextChild(pWidget, position);
      return pWidget;
    }

    private CUIWidget GetPrevChild(CUIWidget pWidget, CLayoutWidget.eLayoutWidgetPosition position)
    {
      CUIWidget prevChild = this.GetPrevChild(pWidget);
      while (prevChild != null && (CLayoutWidget.eLayoutWidgetPosition) prevChild.GetChildPosition() != position)
        prevChild = this.GetPrevChild(prevChild);
      return prevChild;
    }

    private CUIWidget GetNextChild(CUIWidget pWidget, CLayoutWidget.eLayoutWidgetPosition position)
    {
      CUIWidget nextChild = this.GetNextChild(pWidget);
      while (nextChild != null && (CLayoutWidget.eLayoutWidgetPosition) nextChild.GetChildPosition() != position)
        nextChild = this.GetNextChild(nextChild);
      return nextChild;
    }

    public override void Layout()
    {
      CRectangle rect = this.m_rect with
      {
        m_x = this.m_contentInsetX,
        m_y = this.m_contentInsetTopY
      };
      rect.m_dx -= this.m_contentInsetX << 1;
      rect.m_dy -= this.m_contentInsetBotY << 1;
      this.LayoutSection(CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_TOP, true, rect);
      this.LayoutSection(CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_NAVIGATOR, true, rect);
      this.LayoutSection(CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_BOTTOM, false, rect);
      this.LayoutSection(CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_CONTENT, true, rect);
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        data.HandleLayout();
        data.SetFocus(false);
        data.SetSelection(false);
      }
      if (this.m_pFocus == null)
        this.SetFocusToNextOnScreenFocusableWidget((CUIWidget) null, true);
      else if (this.GetFocus())
      {
        this.m_pFocus.SetFocus(true);
        this.m_pFocus.SetSelection(true);
      }
      this.m_contentWidth = 0U;
      this.m_contentHeight = 0U;
    }

    public override bool OnInputEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.m_pNavigator != null && this.m_pNavigator.GetActive() && this.m_pNavigator.GetVisible() && !this.m_pNavigator.GetFocusable())
        flag = this.m_pNavigator.HandleEvent(id, param1, (object) param2);
      if (!flag && this.m_pFocus != null && this.m_pFocus.GetActive() && this.m_pFocus.GetVisible())
        flag = this.m_pFocus.HandleEvent(id, param1, (object) param2);
      if (!flag)
      {
        switch (id)
        {
          case 544526345:
          case 1600235594:
            if (this.m_pFocus != null && this.m_pFocus.GetActive() && this.m_pFocus.GetVisible() && this.m_pFocus.GetSelection())
              this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, (object) this.m_pFocus);
            else
              this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, (object) this);
            flag = true;
            break;
          case 1066869024:
            flag = this.SetFocusToNextOnScreenFocusableWidget(this.m_pFocus, false);
            break;
          case 2535467201:
            flag = this.SetFocusToNextOnScreenFocusableWidget(this.m_pFocus, true);
            break;
          case 2535475076:
          case 3563016926:
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK, param1, (object) this);
            flag = true;
            break;
        }
      }
      if (flag)
        this.OnSetDirty();
      return flag;
    }

    private void LayoutSection(
      CLayoutWidget.eLayoutWidgetPosition position,
      bool forward,
      CRectangle rectLeft)
    {
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if ((CLayoutWidget.eLayoutWidgetPosition) data.GetChildPosition() == position)
        {
          CRectangle rect;
          int num;
          if (position == CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_CONTENT)
          {
            rect.m_x = rectLeft.m_x + this.m_contentInsetX;
            rect.m_dx = rectLeft.m_dx - (this.m_contentInsetX << 1);
            num = rectLeft.m_dy;
          }
          else
          {
            rect.m_x = rectLeft.m_x + this.m_contentInsetX;
            rect.m_dx = rectLeft.m_dx - (this.m_contentInsetX << 1);
            rect.m_y = rectLeft.m_y;
            rect.m_dy = (int) (short) CMath.Max(0, rectLeft.m_dy);
            data.SetRect(rect);
            data.HandleLayout();
            num = data.GetContentHeight();
            if (num < 0)
              num = CMath.Max(0, rectLeft.m_dy);
          }
          rect.m_y = !forward ? rectLeft.m_y + rectLeft.m_dy - num : rectLeft.m_y;
          rect.m_dy = num;
          rect.Clip(rectLeft);
          if (rect.m_dx != 0 && rect.m_dy != 0)
          {
            data.SetActive(true);
            data.SetRect(rect);
          }
          else
            data.SetActive(false);
          if (forward)
          {
            rectLeft.m_y += num;
            rectLeft.m_dy -= num;
          }
          else
            rectLeft.m_dy -= num;
        }
      }
    }

    public enum eLayoutWidgetPosition
    {
      WIDGET_LAYOUT_POSITION_NONE,
      WIDGET_LAYOUT_POSITION_TOP,
      WIDGET_LAYOUT_POSITION_NAVIGATOR,
      WIDGET_LAYOUT_POSITION_CONTENT,
      WIDGET_LAYOUT_POSITION_BOTTOM,
    }
  }
}
