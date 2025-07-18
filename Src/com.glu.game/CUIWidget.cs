// Decompiled with JetBrains decompiler
// Type: com.glu.game.CUIWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CUIWidget : CWidget
  {
    protected uint m_backgroundColor;
    protected uint m_foregroundColor;
    protected uint m_highlightColor;
    protected int m_alignmentFlags;
    protected CLinkListNode m_childNode = new CLinkListNode();
    protected int m_childPosition;
    public CRectangle clip;

    public CUIWidget()
    {
      this.m_backgroundColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, 0, 0, 0);
      this.m_foregroundColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.m_highlightColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.m_alignmentFlags = 17;
      this.m_childPosition = 0;
    }

    public void SetColor(uint backgroundColor, uint foregroundColor, uint highlightColor)
    {
      this.m_backgroundColor = backgroundColor;
      this.m_foregroundColor = foregroundColor;
      this.m_highlightColor = highlightColor;
      this.OnSetDirty();
    }

    public void SetColor(int backgroundColor, int foregroundColor, int highlightColor)
    {
      this.SetColor((uint) backgroundColor, (uint) foregroundColor, (uint) highlightColor);
    }

    public void SetAlignment(int alignment)
    {
      this.m_alignmentFlags = alignment;
      this.OnSetNewLayout();
    }

    public void SetTransparent(bool transparent)
    {
      if (transparent)
        CBitMath.SET_MASK(ref this.m_attFlags, 1);
      else
        CBitMath.CLEAR_MASK(ref this.m_attFlags, 1);
      this.OnSetDirty();
    }

    public void SetSelectable(bool selectable)
    {
      if (selectable)
      {
        CBitMath.SET_MASK(ref this.m_attFlags, 4);
      }
      else
      {
        CBitMath.CLEAR_MASK(ref this.m_attFlags, 4);
        this.SetSelection(false);
      }
      this.OnSetDirty();
    }

    public void SetSelection(bool selection) => this.OnSetSelection(selection);

    public void SetFocusable(bool focusable)
    {
      if (focusable)
      {
        CBitMath.SET_MASK(ref this.m_attFlags, 2);
      }
      else
      {
        CBitMath.CLEAR_MASK(ref this.m_attFlags, 2);
        this.SetFocus(false);
      }
      this.OnSetDirty();
    }

    public void SetFocus(bool focus) => this.OnSetFocus(focus);

    public int GetAlignment() => this.m_alignmentFlags;

    public bool GetTransparent() => CBitMath.TEST_MASK(this.m_attFlags, 1);

    public bool GetSelectable() => CBitMath.TEST_MASK(this.m_attFlags, 4);

    public bool GetSelection() => this.GetSelectable() && CBitMath.TEST_MASK(this.m_stFlags, 8);

    public bool GetFocusable() => CBitMath.TEST_MASK(this.m_attFlags, 2);

    public bool GetFocus() => this.GetFocusable() && CBitMath.TEST_MASK(this.m_stFlags, 4);

    public CLinkListNode GetChildNode() => this.m_childNode;

    public void SetChildPosition(int position) => this.m_childPosition = position;

    public int GetChildPosition() => this.m_childPosition;

    public void PostEvent(uint id, uint param1, object param2)
    {
      CMessage data = new CMessage((CClass) this, id, 0U, 2, new CMessage.Parameter[2]
      {
        new CMessage.Parameter(2065979161U, CMessage.Parameter.Access.Direct, (object) param1),
        new CMessage.Parameter(2065979161U, CMessage.Parameter.Access.Direct, param2)
      });
      if (data == null)
        return;
      CApp.GetExecutor().GetRegistry().Add(CApp.GetExecutor().GetRegistry().CreateSystemElement(CHandleFactory.GetInstance().CreateRuntime(), (object) data, 0U, (uint) CMathFixed.FloatToFixed(0.9f)));
    }

    public int GetHorizontalAlignedPosition(int containerStart, int containerEnd, int itemWidth)
    {
      int anchorPointAlign = 0;
      int itemAlign = 0;
      if (CBitMath.TEST_MASK(this.m_alignmentFlags, 2))
      {
        anchorPointAlign = 32768;
        itemAlign = 32768;
      }
      else if (CBitMath.TEST_MASK(this.m_alignmentFlags, 4))
      {
        anchorPointAlign = 65536;
        itemAlign = 65536;
      }
      return CGenUtil.AlignedPosition(containerStart, containerEnd, itemWidth, (uint) anchorPointAlign, (uint) itemAlign);
    }

    public int GetVerticalAlignedPosition(int containerStart, int containerEnd, int itemHeight)
    {
      int anchorPointAlign = 0;
      int itemAlign = 0;
      if (CBitMath.TEST_MASK(this.m_alignmentFlags, 32))
      {
        anchorPointAlign = 32768;
        itemAlign = 32768;
      }
      else if (CBitMath.TEST_MASK(this.m_alignmentFlags, 64))
      {
        anchorPointAlign = 65536;
        itemAlign = 65536;
      }
      return CGenUtil.AlignedPosition(containerStart, containerEnd, itemHeight, (uint) anchorPointAlign, (uint) itemAlign);
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
              this.OnSetDirty();
              flag = true;
              break;
            case 1386813809:
              if (this.GetSelectable() && this.GetFocus())
              {
                this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, 11U, (object) this);
                break;
              }
              break;
          }
          this.OnSetDirty();
        }
      }
      return flag;
    }

    public override bool OnTouchEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      switch (id)
      {
        case 902008092:
        case 902053462:
        case 902532892:
          int x = TouchUtil.TOUCH_EVENT_GET_X(param2);
          int y = TouchUtil.TOUCH_EVENT_GET_Y(param2);
          uint id1 = 0;
          switch (id)
          {
            case 902008092:
              id1 = 1386813809U;
              break;
            case 902053462:
              id1 = 2300082508U;
              break;
            case 902532892:
              id1 = 2186393822U;
              break;
          }
          uint num = MouseUtil.MOUSE_EVENT_SET_Y(MouseUtil.MOUSE_EVENT_SET_X(0U, x), y);
          flag = this.OnMouseEvent(id1, 0U, num);
          break;
      }
      return flag;
    }

    public override bool OnNotification(
      CWidget.eWidgetNotification notification,
      uint param1,
      object param2)
    {
      bool flag = false;
      switch (notification)
      {
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST:
          flag = this.OnScrollRequest((CWidget.eWidgetScrollDir) param1, (int) param2);
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE:
          if (this.m_parent != null)
          {
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_ADVANCE, param1, param2);
            break;
          }
          this.PostEvent(129075783U, param1, (object) (uint) param2);
          break;
        case CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK:
          if (this.m_parent != null)
          {
            this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_BACK, param1, param2);
            break;
          }
          this.PostEvent(555763780U, param1, (object) (uint) param2);
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

    public virtual bool OnSetFocus(bool focus)
    {
      if (this.GetFocusable() && focus)
        CBitMath.SET_MASK(ref this.m_stFlags, 4);
      else
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 4);
      this.OnSetDirty();
      return true;
    }

    public virtual bool OnSetSelection(bool selection)
    {
      if (this.GetSelectable() && selection)
        CBitMath.SET_MASK(ref this.m_stFlags, 8);
      else
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 8);
      this.OnSetDirty();
      return true;
    }

    public virtual bool OnScrollRequest(CWidget.eWidgetScrollDir dir, int scrollDist)
    {
      this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SCROLL_REQUEST, (uint) dir, (object) (uint) scrollDist);
      return true;
    }
  }
}
