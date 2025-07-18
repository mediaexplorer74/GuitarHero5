// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSpacerWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSpacerWidget : CUIWidget
  {
    public int SPACERWIDGET_DEFAULT_HEIGHT = 2;
    protected CSpacerWidget.eSpacerWidgetStyle m_style;
    protected int m_height;

    public CSpacerWidget()
    {
      this.m_style = CSpacerWidget.eSpacerWidgetStyle.SPACERWIDGET_STYLE_NONE;
      this.m_height = this.SPACERWIDGET_DEFAULT_HEIGHT;
    }

    public void SetHeight(int height)
    {
      this.m_height = height;
      this.OnSetNewLayout();
    }

    private void SetStyle(CSpacerWidget.eSpacerWidgetStyle style)
    {
      this.m_style = style;
      this.OnSetNewLayout();
    }

    public override void Layout()
    {
      this.m_contentWidth = 0U;
      this.m_contentHeight = (uint) this.m_height;
    }

    public override void Paint()
    {
      CWidget.G2dDisplayProgramInfo instance = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      if (this.m_style != CSpacerWidget.eSpacerWidgetStyle.SPACERWIDGET_STYLE_LINE)
        return;
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_height, this.m_foregroundColor);
    }

    public enum eSpacerWidgetStyle
    {
      SPACERWIDGET_STYLE_NONE,
      SPACERWIDGET_STYLE_LINE,
    }
  }
}
