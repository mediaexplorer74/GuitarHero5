// Decompiled with JetBrains decompiler
// Type: com.glu.game.CContainerWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CContainerWidget : CUIWidget
  {
    protected CLinkList m_childList;
    protected int m_contentInsetX;
    protected int m_contentInsetTopY;
    protected int m_contentInsetBotY;
    protected bool m_shrinkX;
    protected bool m_shrinkY;
    protected int m_scrollPositionX;
    protected int m_scrollPositionY;
    protected int m_numItems;
    protected CUIWidget m_pFocus;
    private CRectangle ldrect;

    public CContainerWidget()
    {
      this.m_contentInsetX = 0;
      this.m_contentInsetTopY = 0;
      this.m_contentInsetBotY = 0;
      this.m_shrinkX = true;
      this.m_shrinkY = true;
      this.m_scrollPositionX = 0;
      this.m_scrollPositionY = 0;
      this.m_numItems = 0;
      this.m_pFocus = (CUIWidget) null;
      this.m_childList = new CLinkList();
    }

    public void AddChild(CUIWidget pChild, int position)
    {
      if (pChild == null)
        return;
      pChild.SetParent((CWidget) this);
      pChild.SetChildPosition(position);
      this.m_childList.InsertSorted(new tfnLinkListCompareItem(this.ChildListInsertCompare), pChild.GetChildNode(), (object) pChild);
    }

    public void RemoveChild(CUIWidget pChild)
    {
      if (pChild == null)
        return;
      this.m_childList.Remove(pChild.GetChildNode());
      pChild.SetParent((CWidget) null);
    }

    public CUIWidget GetChild(int id)
    {
      CUIWidget pWidget = this.GetFirstChild();
      while (pWidget != null && pWidget.GetID() != id)
        pWidget = this.GetNextChild(pWidget);
      return pWidget;
    }

    public CUIWidget GetFirstChild()
    {
      return this.m_childList.GetHead() == null ? (CUIWidget) null : (CUIWidget) this.m_childList.GetHead().GetData();
    }

    public CUIWidget GetLastChild()
    {
      return this.m_childList.GetTail() == null ? (CUIWidget) null : (CUIWidget) this.m_childList.GetTail().GetData();
    }

    public CUIWidget GetPrevChild(CUIWidget pWidget)
    {
      CLinkListNode prev = pWidget?.GetChildNode().GetPrev();
      return prev == null ? (CUIWidget) null : (CUIWidget) prev.GetData();
    }

    public CUIWidget GetNextChild(CUIWidget pWidget)
    {
      CLinkListNode next = pWidget?.GetChildNode().GetNext();
      return next == null ? (CUIWidget) null : (CUIWidget) next.GetData();
    }

    public void SetContentInset(int insetX, int insetY)
    {
      this.SetContentInset(insetX, insetY, insetY);
    }

    public void SetContentInset(int insetX, int insetTopY, int insetBotY)
    {
      this.m_contentInsetX = insetX;
      this.m_contentInsetTopY = insetTopY;
      this.m_contentInsetBotY = insetBotY;
      this.OnSetNewLayout();
    }

    public void SetShrinkToFit(bool shrinkX, bool shrinkY)
    {
      this.m_shrinkX = shrinkX;
      this.m_shrinkY = shrinkY;
      this.OnSetNewLayout();
    }

    public CUIWidget GetFocusWidget() => this.m_pFocus;

    public virtual void GetDrawOffset(
      CWidget pWidget,
      CRectangle parent,
      ref int offsetX,
      ref int offsetY,
      ref int offsetAccumX,
      ref int offsetAccumY)
    {
      if (this.m_parent != null)
      {
        this.m_parent.GetDrawOffset(pWidget, out parent, ref offsetX, ref offsetY, ref offsetAccumX, ref offsetAccumY);
        offsetX = offsetAccumX;
        offsetY = offsetAccumY;
        offsetAccumX += this.m_rect.m_x - this.m_scrollPositionX;
        offsetAccumY += this.m_rect.m_y - this.m_scrollPositionY;
      }
      else
      {
        parent = this.m_rect;
        offsetX = 0;
        offsetY = 0;
        offsetAccumX = this.m_rect.m_x;
        offsetAccumY = this.m_rect.m_y;
      }
    }

    public override void Layout()
    {
      if (this.m_pFocus == null)
        this.SetFocusToNextFocusableWidget((CUIWidget) null, true);
      this.m_contentWidth = 0U;
      this.m_contentHeight = 0U;
    }

    public override void Paint()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      instance1.PushTransform();
      instance1.Translate(CMathFixed.Int32ToFixed(-this.m_scrollPositionX), CMathFixed.Int32ToFixed(-this.m_scrollPositionY));
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        data.SetDirty();
        data.HandleRender();
      }
      instance1.PopTransform();
    }

    public override bool OnUpdate(int timeElapsedMS)
    {
      bool flag = false;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        flag |= data.HandleUpdate(timeElapsedMS);
      }
      return flag;
    }

    public override bool OnSetFocus(bool focus)
    {
      base.OnSetFocus(focus);
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (data == this.m_pFocus)
          data.SetFocus(focus);
        else
          data.SetFocus(false);
      }
      return true;
    }

    public override bool OnSetSelection(bool selection)
    {
      base.OnSetSelection(selection);
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (data == this.m_pFocus)
          data.SetSelection(selection);
        else
          data.SetSelection(false);
      }
      return true;
    }

    public override bool OnRenderSurfaceChanged()
    {
      this.PassEventToChildren(850690755U, 0U, 0U);
      this.m_pFocus = (CUIWidget) null;
      this.m_scrollPositionX = 0;
      this.m_scrollPositionY = 0;
      this.OnSetNewLayout();
      return true;
    }

    public override bool OnInputEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.m_pFocus != null && this.m_pFocus.GetActive() && this.m_pFocus.GetVisible())
      {
        flag = this.m_pFocus.HandleEvent(id, param1, (object) param2);
        if (flag)
          this.OnSetDirty();
      }
      return flag;
    }

    public override bool OnKeyEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.m_pFocus != null && this.m_pFocus.GetActive() && this.m_pFocus.GetVisible())
      {
        flag = this.m_pFocus.HandleEvent(id, param1, (object) param2);
        if (flag)
          this.OnSetDirty();
      }
      return flag;
    }

    public override bool OnMouseEvent(uint id, uint param1, uint param2)
    {
      bool flag1 = false;
      if (!flag1 && this.GetActive() && this.GetVisible() && this.GetFocusable())
      {
        this.clip.Clear();
        this.GetClipRect(ref this.clip);
        CRectangle r;
        r.m_x = (int) (short) MouseUtil.MOUSE_EVENT_GET_X(param2);
        r.m_y = (int) (short) MouseUtil.MOUSE_EVENT_GET_Y(param2);
        r.m_dx = 1;
        r.m_dy = 1;
        uint width;
        uint height;
        ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
        bool flag2 = (long) (this.clip.m_dx * this.clip.m_dy) > (long) (width * height >> 1) & !this.clip.Contains(r);
        if (this.clip.Contains(r) || flag2)
        {
          switch (id)
          {
            case 902053462:
            case 2300082508:
              CLinkListNode clinkListNode = this.m_childList.GetHead();
              while (clinkListNode != null)
              {
                CUIWidget data = (CUIWidget) clinkListNode.GetData();
                clinkListNode = clinkListNode.GetNext();
                if (data.HandleEvent(id, param1, (object) param2))
                {
                  if (data.GetFocusable())
                  {
                    this.SetFocusToWidget(data);
                    if (!flag2)
                    {
                      flag1 = true;
                      break;
                    }
                    break;
                  }
                  break;
                }
              }
              break;
            case 1386813809:
            case 2072258765:
            case 2186393822:
              this.PassEventToChildren(id, param1, param2);
              if (!flag2)
              {
                flag1 = true;
                break;
              }
              break;
          }
          this.OnSetDirty();
        }
      }
      return flag1;
    }

    public override bool OnNotification(
      CWidget.eWidgetNotification notification,
      uint param1,
      object param2)
    {
      bool flag = false;
      switch (notification)
      {
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SET_NEW_LAYOUT:
          flag = this.OnSetNewLayout();
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SET_DIRTY:
          flag = this.OnSetDirty();
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST:
          flag = this.OnScrollRequest((CWidget.eWidgetScrollDir) param1, (int) (uint) param2);
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE:
          if (this.m_parent != null)
          {
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, param2);
            break;
          }
          if (this.SoftkeyAllowed(true))
          {
            this.PostEvent(129075783U, param1, param2);
            break;
          }
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK:
          if (this.m_parent != null)
          {
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK, param1, param2);
            break;
          }
          if (this.SoftkeyAllowed(false))
          {
            this.PostEvent(555763780U, param1, param2);
            break;
          }
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION:
          if (this.m_parent != null)
          {
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_CHANGE_SELECTION, param1, param2);
            break;
          }
          this.PostEvent(1053973641U, param1, (object) (uint) param2);
          break;
      }
      return flag;
    }

    public bool IsWidgetOnScreen(CUIWidget pWidget)
    {
      bool flag = false;
      if (pWidget != null && pWidget.GetActive() && pWidget.GetVisible())
      {
        pWidget.GetLocalDrawRect(ref this.ldrect);
        if (this.ldrect.m_dx > 0 && this.ldrect.m_dy > 0)
          flag = true;
      }
      return flag;
    }

    public CUIWidget GetFirstOnScreenItem() => this.GetNextOnScreenItem((CUIWidget) null, true);

    public CUIWidget GetLastOnScreenItem() => this.GetNextOnScreenItem((CUIWidget) null, false);

    public CUIWidget GetNextOnScreenItem(CUIWidget pItem, bool forward)
    {
      CLinkListNode clinkListNode;
      if (pItem != null)
      {
        CLinkListNode childNode = pItem.GetChildNode();
        clinkListNode = forward ? childNode.GetNext() : childNode.GetPrev();
      }
      else
        clinkListNode = forward ? this.m_childList.GetHead() : this.m_childList.GetTail();
      CUIWidget nextOnScreenItem = (CUIWidget) null;
      while (clinkListNode != null && nextOnScreenItem == null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = forward ? clinkListNode.GetNext() : clinkListNode.GetPrev();
        if (this.IsWidgetOnScreen(data))
          nextOnScreenItem = data;
      }
      return nextOnScreenItem;
    }

    public bool IsWidgetFocusable(CUIWidget pWidget)
    {
      bool flag = false;
      if (pWidget != null && pWidget.GetActive() && pWidget.GetVisible() && pWidget.GetFocusable())
        flag = true;
      return flag;
    }

    public CUIWidget GetFirstFocusableItem() => this.GetNextFocusableItem((CUIWidget) null, true);

    public CUIWidget GetLastFocusableItem() => this.GetNextFocusableItem((CUIWidget) null, false);

    public CUIWidget GetNextFocusableItem(CUIWidget pItem, bool forward)
    {
      CLinkListNode clinkListNode;
      if (pItem != null)
      {
        CLinkListNode childNode = pItem.GetChildNode();
        clinkListNode = forward ? childNode.GetNext() : childNode.GetPrev();
      }
      else
        clinkListNode = forward ? this.m_childList.GetHead() : this.m_childList.GetTail();
      CUIWidget nextFocusableItem = (CUIWidget) null;
      while (clinkListNode != null && nextFocusableItem == null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = forward ? clinkListNode.GetNext() : clinkListNode.GetPrev();
        if (this.IsWidgetFocusable(data))
          nextFocusableItem = data;
      }
      return nextFocusableItem;
    }

    public void SetFocusToWidget(CUIWidget pWidget)
    {
      if (this.m_pFocus != null)
      {
        this.m_pFocus.SetFocus(false);
        this.m_pFocus.SetSelection(false);
        this.m_pFocus = (CUIWidget) null;
      }
      if (this.IsWidgetFocusable(pWidget))
      {
        this.m_pFocus = pWidget;
        if (this.GetFocus())
        {
          this.m_pFocus.SetFocus(true);
          this.m_pFocus.SetSelection(true);
        }
      }
      this.OnSetDirty();
    }

    public bool SetFocusToNextFocusableWidget(CUIWidget pWidget, bool forward)
    {
      bool nextFocusableWidget = false;
      pWidget = this.GetNextFocusableItem(pWidget, forward);
      if (pWidget != null)
      {
        this.SetFocusToWidget(pWidget);
        nextFocusableWidget = true;
      }
      return nextFocusableWidget;
    }

    public bool SetFocusToNextOnScreenFocusableWidget(CUIWidget pWidget, bool forward)
    {
      bool screenFocusableWidget = false;
      while (!screenFocusableWidget)
      {
        pWidget = this.GetNextOnScreenItem(pWidget, forward);
        if (pWidget != null)
        {
          if (pWidget.GetFocusable())
          {
            this.SetFocusToWidget(pWidget);
            screenFocusableWidget = true;
          }
        }
        else
          break;
      }
      return screenFocusableWidget;
    }

    public bool SoftkeyAllowed(bool advance)
    {
      bool flag = false;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (data is CSoftkeyWidget)
        {
          CSoftkeyWidget csoftkeyWidget = (CSoftkeyWidget) data;
          if (advance && csoftkeyWidget.HasLeft() || !advance && csoftkeyWidget.HasRight())
          {
            flag = true;
            break;
          }
          break;
        }
      }
      return flag;
    }

    public bool PassEventToChildren(uint id, uint param1, uint param2)
    {
      bool children = false;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        clinkListNode = clinkListNode.GetNext();
        if (data.HandleEvent(id, param1, (object) param2))
          children = true;
      }
      return children;
    }

    public int ChildListInsertCompare(CLinkListNode pNode, object pData)
    {
      CUIWidget data = (CUIWidget) pNode.GetData();
      return ((CUIWidget) pData).GetChildPosition() >= data.GetChildPosition() ? 1 : -1;
    }
  }
}
