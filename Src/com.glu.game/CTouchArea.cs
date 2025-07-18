// Decompiled with JetBrains decompiler
// Type: com.glu.game.CTouchArea
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CTouchArea
  {
    public const int ACTION_TYPE_KEYCODE = 0;
    public const int ACTION_TYPE_CUSTOM = 1;
    public const int ACTION_TYPE_DOUBLE_TAP = 2;
    public const int ACTION_TYPE_DOUBLE_TAP_HOLD = 3;
    public const int ACTION_TYPE_SKIP = 4;
    public int TYPE_RECTANGLE;
    public int TYPE_CIRCLE = 1;
    public int TRIGGER_PRESSED;
    public int TRIGGER_RELEASED = 1;
    public int m_type;
    public int m_actionType;
    public int m_actionParam;
    public int m_x;
    public int m_y;
    public int m_w;
    public int m_h;
    public bool m_bActive;
    private CRectangle rect;

    public CTouchArea(int type)
    {
      this.m_type = type;
      this.m_actionType = -1;
      this.m_bActive = false;
    }

    private CTouchArea createCircle(int x, int y, int diameter)
    {
      return new CTouchArea(this.TYPE_CIRCLE)
      {
        m_x = x,
        m_y = y,
        m_w = diameter,
        m_h = (diameter >> 1) * (diameter >> 1)
      };
    }

    private CTouchArea createRect(int x, int y, int w, int h)
    {
      return new CTouchArea(this.TYPE_RECTANGLE)
      {
        m_x = x,
        m_y = y,
        m_w = w,
        m_h = h
      };
    }

    private void setAction(CTouchArea ta, int actionType, int actionParam)
    {
      ta.m_actionType = actionType;
      ta.m_actionParam = actionParam;
    }

    private void setState(CTouchArea ta, bool bActive)
    {
      ta.m_bActive = bActive;
      if (ta.m_actionType != 0)
        return;
      int actionParam = ta.m_actionParam;
      int num = bActive ? 1 : 0;
    }

    private bool contains(CTouchArea ta, int pointX, int pointY)
    {
      if (ta.m_type == this.TYPE_CIRCLE)
      {
        int num1 = pointX - ta.m_x;
        int num2 = pointY - ta.m_y;
        return num1 * num1 + num2 * num2 < ta.m_h;
      }
      return pointX >= ta.m_x && pointX < ta.m_x + ta.m_w && pointY >= ta.m_y && pointY < ta.m_y + ta.m_h;
    }

    private bool isActive(CTouchArea ta) => ta.m_bActive;

    private void drawDebug(CTouchArea ta)
    {
      uint frameColor = !ta.m_bActive ? Consts.Color_MakeA8R8G8B8(0, (int) byte.MaxValue, 0, 0) : Consts.Color_MakeA8R8G8B8(0, 0, (int) byte.MaxValue, 0);
      if (ta.m_type == this.TYPE_CIRCLE)
        return;
      this.rect.Set(ta.m_x, ta.m_y, ta.m_w, ta.m_h);
      CDrawUtil.DrawFrame(this.rect, frameColor, 1, CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL);
    }

    private void latchAction(CTouchArea ta, int trigger)
    {
      switch (ta.m_actionType)
      {
        case 0:
          if (trigger != this.TRIGGER_PRESSED)
            break;
          CTouchManager.sm_release = ta.m_actionParam;
          break;
        case 1:
        case 2:
        case 3:
        case 4:
          CTouchManager.handleCustomActions(ta, trigger, ta.m_actionParam);
          break;
      }
    }

    private void setPosition(CTouchArea ta, int new_x, int new_y)
    {
      ta.m_x = new_x;
      ta.m_y = new_y;
    }

    private void setSize(CTouchArea ta, int new_w, int new_h)
    {
      ta.m_w = new_w;
      ta.m_h = new_h;
    }
  }
}
