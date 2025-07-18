// Decompiled with JetBrains decompiler
// Type: com.glu.game.CTextParser
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CTextParser : CClass
  {
    protected CFont m_pFont;
    protected string m_pwszContent;
    protected int m_width;
    protected int m_numLines;
    protected CVector m_lineInfoVector = new CVector();

    public void SetFont(CFont pFont) => this.m_pFont = pFont;

    public void SetText(string pwszContent) => this.m_pwszContent = pwszContent;

    public void SetWidth(int width) => this.m_width = width - 20;

    public CFont GetFont() => this.m_pFont;

    public string GetText() => this.m_pwszContent;

    public int GetWidth() => this.m_width;

    public void RemoveUnwantedEscapes()
    {
      if (this.m_pwszContent == null)
        return;
      for (int index = 0; index < this.m_pwszContent.Length; ++index)
      {
        if (this.m_pwszContent[index] == '\\' && this.m_pwszContent[index + 1] == 'n')
          this.m_pwszContent = this.m_pwszContent.Substring(0, index) + (object) '\n' + this.m_pwszContent.Substring(index + 2);
      }
    }

    public CTextParser()
    {
      this.m_pFont = (CFont) null;
      this.m_pwszContent = (string) null;
      this.m_width = 0;
      this.m_numLines = 0;
      this.m_lineInfoVector.Clear();
    }

    private void ReleaseMemory()
    {
      this.m_numLines = 0;
      this.m_lineInfoVector.Clear();
    }

    public int GetNumLines() => this.m_numLines;

    private string GetTextPtrForLine(int line)
    {
      string textPtrForLine = (string) null;
      if (this.m_pwszContent != null)
        textPtrForLine = this.m_pwszContent + (object) this.GetIndexForLine(line);
      return textPtrForLine;
    }

    public int GetIndexForLine(int line)
    {
      int val = 0;
      if (line < this.m_numLines)
      {
        int num = (int) this.m_lineInfoVector.Get(line, ref val);
        val = val >> 16 & (int) ushort.MaxValue;
      }
      return val;
    }

    public int GetNumCharsForLine(int line)
    {
      int val = 0;
      if (line < this.m_numLines)
      {
        int num = (int) this.m_lineInfoVector.Get(line, ref val);
        val &= (int) ushort.MaxValue;
      }
      return val;
    }

    public uint Parse()
    {
      uint num1 = 0;
      this.m_numLines = 0;
      this.m_lineInfoVector.Clear();
      if (this.m_pFont != null && this.m_pwszContent != null && this.m_width > 0)
      {
        int num2 = 0;
        int num3 = 0;
        int charsFit = 0;
        int length = this.m_pwszContent.Length;
        while (num2 < length)
        {
          CFont.eFontTextTokenType fontTextTokenType1 = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL;
          if (num2 != this.m_pwszContent.Length)
            fontTextTokenType1 = this.m_pFont.GetTokenType(this.m_pwszContent[num2]);
          switch (fontTextTokenType1)
          {
            case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL:
              goto label_16;
            case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NEWLINE:
              ++num3;
              ++num2;
              continue;
            default:
              for (; num3 > 0; --num3)
              {
                num1 = this.m_lineInfoVector.Add((num2 & (int) byte.MaxValue) << 16);
                if (num1 == 0U)
                  ++this.m_numLines;
                else
                  break;
              }
              this.m_pFont.MeasureTextWidth(this.m_pwszContent + (object) num2, length - num2, this.m_width, out charsFit);
              charsFit = this.ParseLine(num2, charsFit);
              if (charsFit > 0)
              {
                num1 = this.m_lineInfoVector.Add((num2 & (int) ushort.MaxValue) << 16 | charsFit & (int) ushort.MaxValue);
                if (num1 == 0U)
                {
                  num2 += charsFit;
                  ++this.m_numLines;
                  CFont.eFontTextTokenType fontTextTokenType2 = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL;
                  if (num2 != this.m_pwszContent.Length)
                    fontTextTokenType2 = this.m_pFont.GetTokenType(this.m_pwszContent[num2]);
                  if (fontTextTokenType2 == CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NEWLINE || fontTextTokenType2 == CFont.eFontTextTokenType.FONT_TEXT_TOKEN_SPACE)
                  {
                    ++num2;
                    continue;
                  }
                  continue;
                }
                goto label_16;
              }
              else
                goto label_16;
          }
        }
      }
label_16:
      return num1;
    }

    private int ParseLine(int idx, int charsFit)
    {
      bool flag = true;
      for (int index = idx + charsFit; index > idx; --index)
      {
        CFont.eFontTextTokenType fontTextTokenType = CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL;
        if (index != this.m_pwszContent.Length)
          fontTextTokenType = this.m_pFont.GetTokenType(this.m_pwszContent[index]);
        switch (fontTextTokenType)
        {
          case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NULL:
          case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_NEWLINE:
            charsFit = index - idx;
            flag = false;
            break;
          case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_SPACE:
            if (flag)
              charsFit = index - idx;
            flag = false;
            break;
          case CFont.eFontTextTokenType.FONT_TEXT_TOKEN_HYPHEN:
            if (flag)
              charsFit = index - idx + 1;
            flag = false;
            break;
        }
      }
      return charsFit;
    }
  }
}
