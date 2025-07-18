// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CFont
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CFont : CClass
  {
    public const uint ClassId = 1509780;

    public CFont()
      : base(1509780U)
    {
    }

    public CFont.eFontTextTokenType GetTokenType(char c)
    {
      CFont.eFontTextTokenType tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NONE;
      switch (c)
      {
        case char.MinValue:
          tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL;
          break;
        case '\t':
          tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_TAB;
          break;
        case '\n':
          tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NEWLINE;
          break;
        case ' ':
          tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_SPACE;
          break;
        case '-':
        case '@':
          tokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_HYPHEN;
          break;
      }
      return tokenType;
    }

    public abstract int GetFontHeight();

    public abstract int GetMinLineSpacing();

    public int MeasureTextWidth(string text) => this.MeasureTextWidth(text, -1, -1, out int _);

    public int MeasureTextWidth(string text, int numChars)
    {
      return this.MeasureTextWidth(text, numChars, -1, out int _);
    }

    public int MeasureTextWidth(string text, int numChars, int maxWidth)
    {
      return this.MeasureTextWidth(text, numChars, maxWidth, out int _);
    }

    public abstract int MeasureTextWidth(
      string text,
      int numChars,
      int maxWidth,
      out int charsFit);

    public abstract int GetWidth(char c);

    public void PaintText(string text, int numChars, int x, int y)
    {
      this.PaintText(text, numChars, x, y, -1, -1);
    }

    public void PaintText(string text, int numChars, int x, int y, int maxWidth)
    {
      this.PaintText(text, numChars, x, y, maxWidth, -1);
    }

    public abstract void PaintText(
      string text,
      int numChars,
      int x,
      int y,
      int maxWidth,
      int maxHeight);

    public enum eFontTextTokenType
    {
      FONT_TEXT_TOKEN_NONE,
      FONT_TEXT_TOKEN_NULL,
      FONT_TEXT_TOKEN_SPACE,
      FONT_TEXT_TOKEN_TAB,
      FONT_TEXT_TOKEN_NEWLINE,
      FONT_TEXT_TOKEN_HYPHEN,
    }
  }
}
