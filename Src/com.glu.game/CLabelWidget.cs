// Decompiled with JetBrains decompiler
// Type: com.glu.game.CLabelWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CLabelWidget : CUIWidget
  {
    protected CFont m_pFont;
    protected string m_pwszContent;

    public CLabelWidget()
    {
      this.m_pFont = (CFont) null;
      this.m_pwszContent = (string) null;
    }

    public void SetFont(CFont pFont)
    {
      this.m_pFont = pFont;
      this.OnSetNewLayout();
    }

    public void SetText(string pwszContent)
    {
      this.m_pwszContent = pwszContent;
      this.OnSetNewLayout();
    }

    public override void Layout()
    {
      if (this.m_pFont == null || this.m_pwszContent == null)
        return;
      this.m_contentWidth = (uint) this.m_pFont.MeasureTextWidth(this.m_pwszContent);
      this.m_contentHeight = (uint) this.m_pFont.GetFontHeight();
    }

    public override void Paint()
    {
      CWidget.G2dDisplayProgramInfo instance = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (this.GetFocus())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_highlightColor);
      }
      else if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      if (this.m_pFont == null || this.m_pwszContent == null)
        return;
      int horizontalAlignedPosition = this.GetHorizontalAlignedPosition(0, this.m_rect.m_dx, (int) this.m_contentWidth);
      int verticalAlignedPosition = this.GetVerticalAlignedPosition(0, this.m_rect.m_dy, this.m_pFont.GetFontHeight());
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      this.m_pFont.PaintText(this.m_pwszContent, -1, horizontalAlignedPosition, verticalAlignedPosition);
    }
  }
}
