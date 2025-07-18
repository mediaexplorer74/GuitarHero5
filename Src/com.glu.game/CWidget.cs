// Decompiled with JetBrains decompiler
// Type: com.glu.game.CWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CWidget : CClass
  {
    protected CEventListener m_listener;
    protected int m_id;
    protected int m_attFlags;
    protected int m_stFlags;
    protected CWidget.tfnWidgetRenderHook m_pfnRender;
    protected object m_pRenderData;
    protected CWidget m_parent;
    protected CRectangle m_rect;
    protected uint m_contentWidth;
    protected uint m_contentHeight;
    private CRectangle rect;
    private CRectangle prect;

    public void ConsiderAdvancing2dGraphicsLayer(CWidget.G2dDisplayProgramInfo info)
    {
    }

    public static bool StaticHandleEvent(CEvent Event, object data)
    {
      CWidget owner = (CWidget) ((CEventListener) data).GetOwner();
      CMessage cmessage = (CMessage) Event;
      uint data1 = cmessage.GetNumberOfParameters() > 0 ? (uint) cmessage.GetParameter(0).m_data : 0U;
      uint data2 = cmessage.GetNumberOfParameters() > 1 ? (uint) cmessage.GetParameter(1).m_data : 0U;
      return owner.HandleEvent(cmessage.GetId(), data1, (object) data2);
    }

    public CWidget()
    {
      this.m_listener = new CEventListener();
      this.m_listener.Initialize(CHandleFactory.GetInstance().CreateRuntime(), (object) this, new CEventListener.CEventListerner_EventHandler(CWidget.StaticHandleEvent));
      this.m_listener.Register(850690755U);
      this.m_id = 0;
      this.m_attFlags = 0;
      this.m_stFlags = 275;
      this.m_pfnRender = (CWidget.tfnWidgetRenderHook) null;
      this.m_pRenderData = (object) null;
      this.m_parent = (CWidget) null;
      this.m_contentWidth = 0U;
      this.m_contentHeight = 0U;
    }

    public virtual void SetID(int id) => this.m_id = id;

    public virtual void SetActive(bool active)
    {
      if (active)
        CBitMath.SET_MASK(ref this.m_stFlags, 1);
      else
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 1);
      this.OnSetDirty();
    }

    public virtual void SetVisible(bool visible)
    {
      if (visible)
        CBitMath.SET_MASK(ref this.m_stFlags, 2);
      else
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 2);
      this.OnSetDirty();
    }

    public virtual void SetDirty() => this.OnSetDirty();

    public virtual void SetRect(CRectangle rect)
    {
      this.m_rect = rect;
      this.OnSetNewLayout();
    }

    public void SetRenderCB(CWidget.tfnWidgetRenderHook pfnRender, object pData)
    {
      this.m_pfnRender = pfnRender;
      this.m_pRenderData = pData;
      this.OnSetDirty();
    }

    public virtual void SetParent(CWidget pParent) => this.m_parent = pParent;

    public virtual int GetID() => this.m_id;

    public virtual bool GetActive() => CBitMath.TEST_MASK(this.m_stFlags, 1);

    public virtual bool GetVisible() => CBitMath.TEST_MASK(this.m_stFlags, 2);

    public virtual bool GetDirty() => CBitMath.TEST_MASK(this.m_stFlags, 16);

    public virtual CRectangle GetRect()
    {
      this.HandleLayout();
      return this.m_rect;
    }

    public void GetRenderCB(CWidget.tfnWidgetRenderHook pfnRender, object pData)
    {
      if (pfnRender != null)
        pfnRender = this.m_pfnRender;
      if (pData == null)
        return;
      pData = this.m_pRenderData;
    }

    public CWidget GetParent() => this.m_parent;

    public virtual bool HandleLayout()
    {
      bool flag = false;
      if (CBitMath.TEST_MASK(this.m_stFlags, 256))
      {
        CBitMath.SET_MASK(ref this.m_stFlags, 512);
        this.m_contentWidth = 0U;
        this.m_contentHeight = 0U;
        this.Layout();
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 256);
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 512);
        this.OnSetDirty();
        flag = true;
      }
      return flag;
    }

    public virtual bool HandleRender()
    {
      bool flag = false;
      this.HandleLayout();
      if (this.GetActive() && this.GetVisible() && this.GetDirty())
      {
        ICGraphics2d instance = ICGraphics2d.GetInstance();
        CBitMath.SET_MASK(ref this.m_stFlags, 4096);
        if (this.m_pfnRender != null)
          this.m_pfnRender(this, CWidget.eWidgetRenderLayer.WIDGET_RENDER_LAYER_PRE, this.m_pRenderData);
        this.rect.Clear();
        this.GetClipRect(ref this.rect);
        CRectangle clip = instance.GetClip();
        instance.SetClip(this.rect);
        instance.PushTransform();
        instance.Translate(CMathFixed.Int32ToFixed(this.m_rect.m_x), CMathFixed.Int32ToFixed(this.m_rect.m_y));
        this.Paint();
        instance.PopTransform();
        instance.SetClip(clip);
        if (this.m_pfnRender != null)
          this.m_pfnRender(this, CWidget.eWidgetRenderLayer.WIDGET_RENDER_LAYER_POST, this.m_pRenderData);
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 16);
        CBitMath.CLEAR_MASK(ref this.m_stFlags, 4096);
        flag = true;
      }
      return flag;
    }

    public virtual bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = false;
      if (this.GetActive())
        flag = this.OnUpdate(timeElapsedMS);
      return flag;
    }

    public virtual bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 544526345:
        case 1066869024:
        case 1600235594:
        case 1913978637:
        case 2535467201:
        case 2535475076:
        case 2535498699:
        case 3563016926:
          flag = this.OnInputEvent(id, param1, (uint) param2);
          break;
        case 607208024:
        case 1364371259:
        case 1368267323:
        case 1967276899:
          this.OnSetDirty();
          flag = true;
          break;
        case 850690755:
          this.OnRenderSurfaceChanged();
          flag = true;
          break;
        case 902008092:
        case 902053462:
        case 902532892:
        case 1460124008:
          flag = this.OnTouchEvent(id, param1, (uint) param2);
          break;
        case 1134794776:
        case 3343010790:
          flag = this.OnKeyEvent(id, param1, (uint) param2);
          break;
        case 1386813809:
        case 2072258765:
        case 2186393822:
        case 2300082508:
          flag = this.OnMouseEvent(id, param1, (uint) param2);
          break;
        default:
          if (this.GetActive())
          {
            flag = this.OnMiscEvent(id, param1, (uint) param2);
            break;
          }
          break;
      }
      return flag;
    }

    public bool HandleNotification(
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
        default:
          if (this.GetActive())
          {
            flag = this.OnNotification(notification, param1, param2);
            break;
          }
          break;
      }
      return flag;
    }

    public virtual int GetContentWidth()
    {
      this.HandleLayout();
      return (int) this.m_contentWidth;
    }

    public virtual int GetContentHeight()
    {
      this.HandleLayout();
      return (int) this.m_contentHeight;
    }

    public virtual void SendNotification(
      CWidget.eWidgetNotification notification,
      uint param1,
      object param2)
    {
      if (this.m_parent == null)
        return;
      this.m_parent.HandleNotification(notification, param1, param2);
    }

    public virtual void GetClipRect(ref CRectangle output)
    {
      int offsetX = 0;
      int offsetY = 0;
      int offsetAccumX = 0;
      int offsetAccumY = 0;
      this.GetDrawOffset(this, out CRectangle _, ref offsetX, ref offsetY, ref offsetAccumX, ref offsetAccumY);
      output.m_x = this.m_rect.m_x + offsetX;
      output.m_y = this.m_rect.m_y + offsetY;
      output.m_dx = this.m_rect.m_dx;
      output.m_dy = this.m_rect.m_dy;
      if (this.m_parent == null)
        return;
      this.prect.Clear();
      this.m_parent.GetClipRect(ref this.prect);
      output.Clip(this.prect);
    }

    public virtual void GetLocalDrawRect(ref CRectangle output)
    {
      int offsetX = 0;
      int offsetY = 0;
      int offsetAccumX = 0;
      int offsetAccumY = 0;
      this.GetDrawOffset(this, out CRectangle _, ref offsetX, ref offsetY, ref offsetAccumX, ref offsetAccumY);
      output.m_x = this.m_rect.m_x + offsetX;
      output.m_y = this.m_rect.m_y + offsetY;
      output.m_dx = this.m_rect.m_dx;
      output.m_dy = this.m_rect.m_dy;
      if (this.m_parent != null)
      {
        this.prect.Clear();
        this.m_parent.GetClipRect(ref this.prect);
        output.Clip(this.prect);
      }
      output.m_x -= this.m_rect.m_x + offsetX;
      output.m_y -= this.m_rect.m_y + offsetY;
    }

    public virtual void GetDrawOffset(
      CWidget pWidget,
      out CRectangle parent,
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
        offsetAccumX += this.m_rect.m_x;
        offsetAccumY += this.m_rect.m_y;
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

    public virtual void Layout()
    {
      this.m_contentWidth = 0U;
      this.m_contentHeight = 0U;
    }

    public virtual void Paint()
    {
    }

    public virtual bool OnUpdate(int timeElapsedMS) => false;

    public virtual bool OnSetNewLayout()
    {
      bool flag = false;
      if (!CBitMath.TEST_MASK(this.m_stFlags, 512))
      {
        CBitMath.SET_MASK(ref this.m_stFlags, 256);
        this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SET_NEW_LAYOUT, 0U, (object) 0);
        flag = true;
      }
      return flag;
    }

    public virtual bool OnSetDirty()
    {
      bool flag = false;
      if (!CBitMath.TEST_MASK(this.m_stFlags, 4096))
      {
        CBitMath.SET_MASK(ref this.m_stFlags, 16);
        this.SendNotification(CWidget.eWidgetNotification.WIDGET_NOTIFICATION_SET_DIRTY, 0U, (object) 0);
        flag = true;
      }
      return flag;
    }

    public virtual bool OnRenderSurfaceChanged() => false;

    public virtual bool OnInputEvent(uint id, uint param1, uint param2) => false;

    public virtual bool OnKeyEvent(uint id, uint param1, uint param2) => false;

    public virtual bool OnMouseEvent(uint id, uint param1, uint param2) => false;

    public virtual bool OnTouchEvent(uint id, uint param1, uint param2) => false;

    public bool OnMiscEvent(uint id, uint param1, uint param2) => false;

    public virtual bool OnNotification(
      CWidget.eWidgetNotification notification,
      uint param1,
      object param2)
    {
      return false;
    }

    public delegate void tfnWidgetRenderHook(
      CWidget pWidget,
      CWidget.eWidgetRenderLayer layer,
      object pData);

    public enum eWidgetRenderLayer
    {
      WIDGET_RENDER_LAYER_NONE,
      WIDGET_RENDER_LAYER_PRE,
      WIDGET_RENDER_LAYER_POST,
    }

    public enum eWidgetNotification
    {
      WIDGET_NOTIFICATION_NONE,
      WIDGET_NOTIFICATION_SET_NEW_LAYOUT,
      WIDGET_NOTIFICATION_SET_DIRTY,
      WIDGET_NOTIFICATION_SCROLL_REQUEST,
      WIDGET_NOTIFICATION_ADVANCE,
      WIDGET_NOTIFICATION_BACK,
      WIDGET_NOTIFICATION_CHANGE_SELECTION,
    }

    public enum eWidgetScrollDir
    {
      WIDGET_SCROLLDIR_NONE,
      WIDGET_SCROLLDIR_UP,
      WIDGET_SCROLLDIR_DOWN,
      WIDGET_SCROLLDIR_LEFT,
      WIDGET_SCROLLDIR_RIGHT,
    }

    public enum eWidgetAlignmentFlags
    {
      WIDGET_ALIGN_NONE = 0,
      WIDGET_ALIGN_LEFT = 1,
      WIDGET_ALIGN_CENTER = 2,
      WIDGET_ALIGN_RIGHT = 4,
      WIDGET_ALIGN_TOP = 16, // 0x00000010
      WIDGET_ALIGN_MIDDLE = 32, // 0x00000020
      WIDGET_ALIGN_BOTTOM = 64, // 0x00000040
    }

    public enum eWidgetAttributeFlags
    {
      WIDGET_ATT_FLAGS_NONE = 0,
      WIDGET_ATT_FLAGS_TRANSPARENT = 1,
      WIDGET_ATT_FLAGS_FOCUSABLE = 2,
      WIDGET_ATT_FLAGS_SELECTABLE = 4,
    }

    public enum eWidgetStateFlags
    {
      WIDGET_ST_FLAGS_NONE = 0,
      WIDGET_ST_FLAGS_ACTIVE = 1,
      WIDGET_ST_FLAGS_VISIBLE = 2,
      WIDGET_ST_FLAGS_FOCUS = 4,
      WIDGET_ST_FLAGS_SELECTION = 8,
      WIDGET_ST_FLAGS_DIRTY = 16, // 0x00000010
      WIDGET_ST_FLAGS_NEW_LAYOUT = 256, // 0x00000100
      WIDGET_ST_FLAGS_IN_LAYOUT = 512, // 0x00000200
      WIDGET_ST_FLAGS_IN_PAINT = 4096, // 0x00001000
    }

    public class G2dDisplayProgramInfo : CSingleton
    {
      public bool m_autoIncLayers;
      protected new static CSingleton m_instance;

      public static CWidget.G2dDisplayProgramInfo GetInstance()
      {
        if (CWidget.G2dDisplayProgramInfo.m_instance == null)
          CWidget.G2dDisplayProgramInfo.m_instance = (CSingleton) new CWidget.G2dDisplayProgramInfo();
        return CWidget.G2dDisplayProgramInfo.m_instance as CWidget.G2dDisplayProgramInfo;
      }

      public G2dDisplayProgramInfo() => this.m_autoIncLayers = false;
    }
  }
}
