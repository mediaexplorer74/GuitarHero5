// Decompiled with JetBrains decompiler
// Type: com.glu.game.CPageWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CPageWidget : CVerticalContainerWidget
  {
    public const float PAGEWIDGET_SCROLLRATIO_DEFAULT = 45875.2f;
    public const int PAGEWIDGET_MIN_SCROLL_DIST = 5;
    protected bool m_wrap;
    protected int m_scrollRatio;
    protected int m_scrollDistPixels;
    protected bool m_scrollWithPointer;
    protected int m_lastPointerX;
    protected int m_lastPointerY;
    private CRectangle rect;

    public void SetWrap(bool wrap)
    {
      this.m_wrap = wrap;
      this.SetNoArrowsUnlessScrollable(!wrap);
    }

    public CPageWidget()
    {
      this.SetWrap(false);
      this.m_scrollRatio = 45875;
      this.m_scrollDistPixels = 0;
      this.m_scrollWithPointer = false;
      this.m_lastPointerX = 0;
      this.m_lastPointerY = 0;
    }

    private void SetScrollRatio(int scrollRatio) => this.m_scrollRatio = scrollRatio;

    private void SetScrollPixels(int pixels)
    {
      this.m_scrollRatio = 0;
      this.m_scrollDistPixels = pixels;
    }

    public void Add(CUIWidget pWidget) => this.Add(pWidget, this.m_numItems);

    public void Add(CUIWidget pWidget, int index)
    {
      if (pWidget == null)
        return;
      for (CLinkListNode clinkListNode = this.m_childList.GetHead(); clinkListNode != null; clinkListNode = clinkListNode.GetNext())
      {
        CUIWidget data = (CUIWidget) clinkListNode.GetData();
        if (data.GetChildPosition() >= index)
          data.SetChildPosition(1 + data.GetChildPosition());
      }
      this.AddChild(pWidget, index);
      ++this.m_numItems;
      this.OnSetNewLayout();
    }

    private void Remove(CUIWidget pWidget)
    {
      if (pWidget == this.GetFocusWidget() && !this.Scroll(CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN))
        this.Scroll(CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP);
      this.RemoveChild(pWidget);
      int position = 0;
      CLinkListNode clinkListNode = this.m_childList.GetHead();
      while (clinkListNode != null)
      {
        ((CUIWidget) clinkListNode.GetData()).SetChildPosition(position);
        clinkListNode = clinkListNode.GetNext();
        ++position;
      }
      this.OnSetNewLayout();
    }

    private bool Scroll(CWidget.eWidgetScrollDir dir)
    {
      bool flag = false;
      int num1 = 0;
      if (dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP || dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN)
      {
        flag = this.SetFocusToNextOnScreenFocusableWidget(this.m_pFocus, dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN);
        if (flag && this.m_pFocus != null)
        {
          this.rect.Clear();
          this.m_pFocus.GetClipRect(ref this.rect);
          int contentHeight = this.m_pFocus.GetContentHeight();
          if (contentHeight > this.rect.m_dy && contentHeight < this.m_rect.m_dy)
            num1 = CMath.Max(0, this.m_scrollDistPixels - this.rect.m_dy);
        }
        else
          num1 = this.m_scrollDistPixels;
        if (num1 > 0)
        {
          CUIWidget cuiWidget = dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP ? this.GetLastOnScreenItem() : this.GetFirstOnScreenItem();
          if (cuiWidget != null)
          {
            this.rect = cuiWidget.GetRect();
            int contentHeight = cuiWidget.GetContentHeight();
            if (contentHeight > this.rect.m_dy && contentHeight < this.m_rect.m_dy)
              num1 = CMath.Max(5, this.m_scrollDistPixels - this.rect.m_dy);
          }
          int num2 = this.m_scrollPositionY;
          switch (dir)
          {
            case CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP:
              num2 = CMath.Max(0, num2 - num1);
              break;
            case CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN:
              int val1 = (int) ((long) this.m_contentHeight - (long) this.m_rect.m_dy);
              if (this.m_pDownArrow != null)
              {
                uint height;
                this.m_pDownArrow.GetWidthAndHeight(out uint _, out height);
                val1 += (int) ((long) height + (long) this.m_scrollArrowOffset);
              }
              if (this.m_pUpArrow != null)
              {
                uint height;
                this.m_pUpArrow.GetWidthAndHeight(out uint _, out height);
                val1 += (int) ((long) height + (long) this.m_scrollArrowOffset);
              }
              num2 = CMath.Max(0, CMath.Min(val1, num2 + num1));
              break;
          }
          if (this.m_scrollPositionY != num2)
          {
            this.m_scrollPositionY = num2;
            this.OnSetDirty();
            flag = true;
          }
        }
        if (!flag && this.m_wrap)
        {
          int num3 = this.m_scrollPositionY;
          if (dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP)
          {
            if ((long) this.m_contentHeight >= (long) this.m_rect.m_dy)
              num3 = (int) ((long) this.m_contentHeight - (long) this.m_rect.m_dy);
            this.SetFocusToNextFocusableWidget((CUIWidget) null, false);
          }
          else
          {
            num3 = 0;
            this.SetFocusToNextFocusableWidget((CUIWidget) null, true);
          }
          if (num3 != this.m_scrollPositionY)
          {
            this.m_scrollPositionY = num3;
            this.OnSetDirty();
          }
          flag = true;
        }
      }
      return flag;
    }

    public override void Layout()
    {
      base.Layout();
      if (this.m_scrollRatio <= 0)
        return;
      this.m_scrollDistPixels = CMathFixed.FixedToInt32(this.m_rect.m_dy * this.m_scrollRatio);
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
            if (this.m_pFocus != null && this.m_pFocus.GetActive() && this.m_pFocus.GetVisible() && this.m_pFocus.GetSelection())
              this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, (object) this.m_pFocus);
            else
              this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, (object) this);
            flag = true;
            break;
          case 1066869024:
            flag = this.Scroll(CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP);
            break;
          case 2535467201:
            flag = this.Scroll(CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN);
            break;
          case 2535475076:
          case 3563016926:
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK, param1, (object) this);
            flag = true;
            break;
        }
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
              CUIWidget cuiWidget = (CUIWidget) null;
              CLinkListNode clinkListNode = this.m_childList.GetHead();
              while (clinkListNode != null)
              {
                CUIWidget data = (CUIWidget) clinkListNode.GetData();
                clinkListNode = clinkListNode.GetNext();
                if (data.HandleEvent(id, param1, (object) param2))
                {
                  if (data.GetFocusable())
                    this.SetFocusToWidget(data);
                  cuiWidget = data;
                  break;
                }
              }
              if (cuiWidget == null || !cuiWidget.GetFocusable())
              {
                this.m_scrollWithPointer = true;
                this.m_lastPointerX = r.m_x;
                this.m_lastPointerY = r.m_y;
              }
              if (!flag2)
              {
                flag1 = true;
                break;
              }
              break;
            case 1386813809:
              if (this.m_scrollWithPointer)
              {
                this.m_scrollWithPointer = false;
                this.m_lastPointerX = 0;
                this.m_lastPointerY = 0;
              }
              else
                this.PassEventToChildren(id, param1, param2);
              if (!flag2)
              {
                flag1 = true;
                break;
              }
              break;
            case 2072258765:
              this.PassEventToChildren(id, param1, param2);
              if (!flag2)
              {
                flag1 = true;
                break;
              }
              break;
            case 2186393822:
              if (this.m_scrollWithPointer)
              {
                int val = r.m_y - this.m_lastPointerY;
                this.OnScrollRequest(val < 0 ? CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN : CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP, CMath.Abs(val));
                this.m_lastPointerX = r.m_x;
                this.m_lastPointerY = r.m_y;
              }
              else
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

    public override bool OnScrollRequest(CWidget.eWidgetScrollDir dir, int scrollDist)
    {
      bool flag = false;
      if (dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP || dir == CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN)
      {
        int num = this.m_scrollPositionY;
        switch (dir)
        {
          case CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_UP:
            num = CMath.Max(0, num - scrollDist);
            break;
          case CWidget.eWidgetScrollDir.WIDGET_SCROLLDIR_DOWN:
            int val1 = (int) ((long) this.m_contentHeight - (long) this.m_rect.m_dy);
            if (this.m_pDownArrow != null)
            {
              uint height;
              this.m_pDownArrow.GetWidthAndHeight(out uint _, out height);
              val1 += (int) ((long) height + (long) this.m_scrollArrowOffset);
            }
            if (this.m_pUpArrow != null)
            {
              uint height;
              this.m_pUpArrow.GetWidthAndHeight(out uint _, out height);
              val1 += (int) ((long) height + (long) this.m_scrollArrowOffset);
            }
            num = CMath.Max(0, CMath.Min(val1, num + scrollDist));
            break;
        }
        if (this.m_scrollPositionY != num)
        {
          this.m_scrollPositionY = num;
          flag = true;
        }
      }
      if (flag)
        this.OnSetDirty();
      else
        this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, (uint) dir, (object) (uint) scrollDist);
      return flag;
    }
  }
}
