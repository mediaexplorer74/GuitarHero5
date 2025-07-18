// Decompiled with JetBrains decompiler
// Type: com.glu.game.CProgressWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CProgressWidget : CUIWidget
  {
    public const int PROGRESS_BORDER_WIDTH = 1;
    public const int PROGRESS_INSET = 1;
    protected uint m_shadeColor;
    protected int m_width;
    protected int m_length;
    protected bool m_horizontal;
    protected int m_percent;

    public CProgressWidget()
    {
      this.m_shadeColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, 0, 0, 0);
      this.m_width = this.GetStandardProgressWidth();
      this.m_length = 0;
      this.m_horizontal = true;
      this.m_percent = 0;
    }

    private void SetShadeColor(uint shadeColor)
    {
      this.m_shadeColor = shadeColor;
      this.OnSetDirty();
    }

    public void SetWidth(int width)
    {
      this.m_width = width;
      this.OnSetNewLayout();
    }

    private void SetLength(int length)
    {
      this.m_length = length;
      this.OnSetNewLayout();
    }

    public void SetOrientation(bool horizontal)
    {
      this.m_horizontal = horizontal;
      this.OnSetNewLayout();
    }

    public void SetPercent(int percent)
    {
      if (this.m_percent == percent)
        return;
      this.m_percent = percent;
      this.OnSetDirty();
    }

    public int GetWidth() => this.m_width;

    public bool GetOrientation() => this.m_horizontal;

    public int GetPercent() => this.m_percent;

    public int GetStandardProgressWidth() => ICCore.GetInstance().GetScrollbarWidth();

    public override void Layout()
    {
      if (this.m_horizontal)
      {
        this.m_contentWidth = this.m_length > 0 ? (uint) (this.m_length + 2) : 4294967196U;
        this.m_contentHeight = (uint) (this.m_width + 2);
      }
      else
      {
        this.m_contentWidth = (uint) (this.m_width + 2);
        this.m_contentHeight = this.m_length > 0 ? (uint) (this.m_length + 2) : 4294967196U;
      }
    }

    public override void Paint()
    {
      CWidget.G2dDisplayProgramInfo instance = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      short num1;
      short num2;
      if (this.m_horizontal)
      {
        num1 = this.m_length > 0 ? (short) this.m_length : (short) (this.m_rect.m_dx - 2);
        num2 = (short) this.m_width;
      }
      else
      {
        num1 = (short) this.m_width;
        num2 = this.m_length > 0 ? (short) this.m_length : (short) (this.m_rect.m_dy - 2);
      }
      int num3 = 1 + (this.m_rect.m_dx >> 1) - ((int) num1 >> 1);
      int num4 = 1 + (this.m_rect.m_dy >> 1) - ((int) num2 >> 1);
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      CRectangle rect;
      rect.m_x = num3;
      rect.m_dx = (int) num1;
      rect.m_y = num4;
      rect.m_dy = (int) num2;
      CDrawUtil.DrawFrame(rect, this.m_highlightColor, 1, CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL);
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      rect.m_x = num3 + 1;
      rect.m_dx = (int) num1 - 2;
      rect.m_y = num4 + 1;
      rect.m_dy = (int) num2 - 2;
      CDrawUtil.FillRect(rect, this.m_shadeColor);
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      rect.m_x = num3 + 1;
      rect.m_dx = (int) num1 - 2;
      rect.m_y = num4 + 1;
      rect.m_dy = (int) num2 - 2;
      if (this.m_horizontal)
      {
        int num5 = rect.m_dx * this.m_percent / 100;
        if (num5 < 0)
          num5 = 0;
        else if (num5 > rect.m_dx)
          num5 = rect.m_dx;
        rect.m_dx = num5;
      }
      else
      {
        int num6 = rect.m_dy * this.m_percent / 100;
        if (num6 < 0)
          num6 = 0;
        else if (num6 > rect.m_dy)
          num6 = rect.m_dy;
        rect.m_dy = num6;
      }
      CDrawUtil.FillRect(rect, this.m_foregroundColor);
    }
  }
}
