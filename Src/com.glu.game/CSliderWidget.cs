// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSliderWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSliderWidget : CUIWidget
  {
    public const int SLIDER_INSET = 1;
    protected uint m_shadeColor;
    protected int m_width;
    protected bool m_horizontal;
    protected int m_pos;
    protected int m_max;
    protected int m_pageSize;

    public CSliderWidget()
    {
      this.m_shadeColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, 0, 0, 0);
      this.m_horizontal = false;
      this.m_width = this.GetStandardSliderWidth();
      this.m_pos = 0;
      this.m_max = 0;
      this.m_pageSize = 0;
    }

    public void SetShadeColor(uint shadeColor)
    {
      this.m_shadeColor = shadeColor;
      this.OnSetDirty();
    }

    private void SetWidth(int width)
    {
      this.m_width = width;
      this.OnSetNewLayout();
    }

    private void SetOrientation(bool horizontal)
    {
      this.m_horizontal = horizontal;
      this.OnSetNewLayout();
    }

    public void SetSlider(int pos, int max, int pageSize)
    {
      this.m_pos = pos;
      this.m_max = max;
      this.m_pageSize = pageSize;
      this.OnSetDirty();
    }

    public int GetWidth() => this.m_width;

    private bool GetOrientation() => this.m_horizontal;

    private int GetStandardSliderWidth() => ICCore.GetInstance().GetScrollbarWidth();

    public override void Layout()
    {
      if (this.m_horizontal)
      {
        this.m_contentWidth = 0U;
        this.m_contentHeight = (uint) this.m_width;
      }
      else
      {
        this.m_contentWidth = (uint) this.m_width;
        this.m_contentHeight = 0U;
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
      if (this.m_pageSize <= 0 || this.m_max <= 0 || this.m_pos < 0 || this.m_pos > this.m_max)
        return;
      short num1;
      short num2;
      if (this.m_horizontal)
      {
        num1 = (short) (this.m_rect.m_dx - 2);
        num2 = (short) (this.m_width - 2);
      }
      else
      {
        num1 = (short) (this.m_width - 2);
        num2 = (short) (this.m_rect.m_dy - 2);
      }
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      CRectangle rect;
      rect.m_x = 1;
      rect.m_dx = (int) num1;
      rect.m_y = 1;
      rect.m_dy = (int) num2;
      CDrawUtil.FillRect(rect, this.m_shadeColor);
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      if (this.m_horizontal)
      {
        rect.m_dx = (int) num1 * this.m_pageSize / (this.m_max + this.m_pageSize);
        rect.m_dx = (int) (short) CMath.Max(rect.m_dx, (int) (width >> 3));
        rect.m_x = 1 + this.m_pos * ((int) num1 - rect.m_dx) / this.m_max;
      }
      else
      {
        rect.m_dy = (int) num2 * this.m_pageSize / (this.m_max + this.m_pageSize);
        rect.m_dy = (int) (short) CMath.Max(rect.m_dy, (int) (height >> 3));
        rect.m_y = 1 + this.m_pos * ((int) num2 - rect.m_dy) / this.m_max;
      }
      CDrawUtil.FillRect(rect, this.m_foregroundColor);
    }
  }
}
