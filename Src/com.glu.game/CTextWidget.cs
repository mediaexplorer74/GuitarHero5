// Decompiled with JetBrains decompiler
// Type: com.glu.game.CTextWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CTextWidget : CUIWidget
  {
    private const string wszEllipsis = "...";
    protected int m_minLines;
    protected int m_maxLines;
    protected int m_textWidth;
    protected int m_textHeight;
    protected CFont m_pFont;
    protected string m_pwszContent;
    protected int m_numLines;
    protected int m_lastParseWidth;
    protected CTextParser m_parser = new CTextParser();

    public CTextWidget()
    {
      this.m_minLines = -1;
      this.m_maxLines = -1;
      this.m_pFont = (CFont) null;
      this.m_pwszContent = (string) null;
      this.m_numLines = 0;
      this.m_lastParseWidth = 0;
    }

    public void SetFont(CFont pFont)
    {
      this.m_pFont = pFont;
      this.m_lastParseWidth = 0;
      this.OnSetNewLayout();
    }

    public void SetText(string pwszContent)
    {
      this.m_pwszContent = pwszContent;
      this.m_lastParseWidth = 0;
      this.OnSetNewLayout();
    }

    public void SetMinLines(int minLines)
    {
      this.m_minLines = minLines;
      this.OnSetNewLayout();
    }

    public void SetMaxLines(int maxLines)
    {
      this.m_maxLines = maxLines;
      this.OnSetNewLayout();
    }

    public override void Layout()
    {
      if (this.m_lastParseWidth != this.m_rect.m_dx)
      {
        this.m_parser.SetFont(this.m_pFont);
        this.m_parser.SetText(this.m_pwszContent);
        this.m_parser.SetWidth(this.m_rect.m_dx);
        int num = (int) this.m_parser.Parse();
        this.m_lastParseWidth = this.m_rect.m_dx;
      }
      this.m_numLines = this.m_parser.GetNumLines();
      if (this.m_minLines > 0 && this.m_numLines < this.m_minLines)
        this.m_numLines = this.m_minLines;
      if (this.m_maxLines > 0 && this.m_numLines > this.m_maxLines)
        this.m_numLines = this.m_maxLines;
      this.m_contentWidth = 100U;
      this.m_contentHeight = (uint) (this.m_numLines * (this.m_pFont != null ? this.m_pFont.GetFontHeight() : 0));
    }

    public override void Paint()
    {
      CWidget.G2dDisplayProgramInfo instance = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (this.GetFocus())
        this.ConsiderAdvancing2dGraphicsLayer(instance);
      else if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      this.ConsiderAdvancing2dGraphicsLayer(instance);
      this.RenderText();
    }

    public void RenderText()
    {
      if (this.m_pFont == null || this.m_pwszContent == null)
        return;
      int fontHeight = this.m_pFont.GetFontHeight();
      int num1 = this.m_pFont.MeasureTextWidth("...");
      for (int line = 0; line < this.m_numLines; ++line)
      {
        int indexForLine = this.m_parser.GetIndexForLine(line);
        int charsFit = this.m_parser.GetNumCharsForLine(line);
        if (charsFit > 0)
        {
          bool flag = this.m_maxLines > 0 && line == this.m_maxLines - 1 && this.m_parser.GetNumLines() > this.m_maxLines;
          string text = this.m_pwszContent.Substring(indexForLine);
          int num2 = this.m_pFont.MeasureTextWidth(text, charsFit, flag ? this.m_rect.m_dx - num1 : this.m_rect.m_dx, out charsFit);
          int horizontalAlignedPosition = this.GetHorizontalAlignedPosition(0, this.m_rect.m_dx, flag ? num2 + num1 : num2);
          this.m_pFont.PaintText(text, charsFit, horizontalAlignedPosition, line * fontHeight);
          if (flag)
            this.m_pFont.PaintText("...", -1, horizontalAlignedPosition + num2, line * fontHeight);
        }
      }
    }

    public int GetTextWidth()
    {
      this.m_textWidth = this.m_pFont != null ? this.m_pFont.MeasureTextWidth(this.m_pwszContent) : 0;
      return this.m_textWidth;
    }

    public int GetTextHeight()
    {
      this.m_textHeight = this.m_pFont != null ? this.m_pFont.GetFontHeight() : 0;
      return this.m_textHeight;
    }
  }
}
