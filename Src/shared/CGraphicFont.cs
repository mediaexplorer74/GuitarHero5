// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CGraphicFont
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class CGraphicFont : CFont
  {
    public new const uint ClassId = 1862236626;
    private ICRenderSurface m_pImage;
    private tGTFHeader m_pHeaderData;
    private tGTFChar[] m_pCharData;
    private tGTFControlChar[] m_pControlCharData;

    public CGraphicFont()
    {
      this.m_classId = 1862236626U;
      this.m_pImage = (ICRenderSurface) null;
      this.m_pHeaderData = (tGTFHeader) null;
      this.m_pCharData = (tGTFChar[]) null;
      this.m_pControlCharData = (tGTFControlChar[]) null;
    }

    public void SetFontImage(ICRenderSurface image) => this.m_pImage = image;

    public void ParseFontMetrics(byte[] pFontMetricsData, uint metricsLength)
    {
      this.m_pHeaderData = new tGTFHeader();
      if (this.m_pHeaderData == null)
        return;
      CArrayInputStream inStream = new CArrayInputStream();
      inStream.Open(pFontMetricsData, metricsLength);
      this.m_pHeaderData.Load((CInputStream) inStream);
      this.m_pCharData = (tGTFChar[]) null;
      if (this.m_pHeaderData.m_numChars > (short) 0)
        this.m_pCharData = new tGTFChar[(int) this.m_pHeaderData.m_numChars];
      this.m_pControlCharData = (tGTFControlChar[]) null;
      if (this.m_pHeaderData.m_numControlChars > (short) 0)
        this.m_pControlCharData = new tGTFControlChar[(int) this.m_pHeaderData.m_numControlChars];
      uint buflen = (uint) (CMath.Max((int) this.m_pHeaderData.m_numChars, (int) this.m_pHeaderData.m_numControlChars) + 1);
      char[] szOut = new char[(int) buflen];
      if (this.m_pCharData != null)
      {
        if (this.m_pHeaderData.m_versionGeneration == (sbyte) 2)
        {
          int num = (int) inStream.ReadJMUtf(szOut, buflen);
        }
        for (int index = 0; index < (int) this.m_pHeaderData.m_numChars; ++index)
        {
          this.m_pCharData[index] = new tGTFChar();
          this.m_pCharData[index].m_charCode = this.m_pHeaderData.m_versionGeneration != (sbyte) 2 ? inStream.ReadUInt16() : (ushort) szOut[index];
          this.m_pCharData[index].m_imageOffsetX = inStream.ReadInt16();
          this.m_pCharData[index].m_imageOffsetY = inStream.ReadInt16();
          this.m_pCharData[index].m_drawWidth = inStream.ReadInt8();
          this.m_pCharData[index].m_drawHeight = inStream.ReadInt8();
          this.m_pCharData[index].m_drawOffsetX = inStream.ReadInt8();
          this.m_pCharData[index].m_drawOffsetY = inStream.ReadInt8();
          this.m_pCharData[index].m_width = inStream.ReadInt8();
          this.m_pCharData[index].m_reserved = inStream.ReadInt8();
        }
      }
      if (this.m_pControlCharData != null)
      {
        if (this.m_pHeaderData.m_versionGeneration == (sbyte) 2)
        {
          int num = (int) inStream.ReadJMUtf(szOut, buflen);
        }
        for (int index = 0; index < (int) this.m_pHeaderData.m_numControlChars; ++index)
        {
          this.m_pControlCharData[index] = new tGTFControlChar();
          this.m_pControlCharData[index].m_charCode = this.m_pHeaderData.m_versionGeneration != (sbyte) 2 ? inStream.ReadUInt16() : (ushort) szOut[index];
          this.m_pControlCharData[index].m_reserved = inStream.ReadInt8();
          this.m_pControlCharData[index].m_width = inStream.ReadInt8();
        }
      }
      inStream.Close();
    }

    public override int GetFontHeight()
    {
      return (int) this.m_pHeaderData.m_height + (int) this.m_pHeaderData.m_minLineSpacing;
    }

    public override int GetMinLineSpacing() => (int) this.m_pHeaderData.m_minLineSpacing;

    public override int MeasureTextWidth(
      string text,
      int numChars,
      int maxWidth,
      out int charsFit)
    {
      bool flag = false;
      int num = 0;
      charsFit = 0;
      if (numChars < 0)
        numChars = text.Length;
      if (maxWidth < 0)
        flag = true;
      for (int index = 0; index < numChars; ++index)
      {
        int width = this.GetWidth(text[index]);
        int val2 = width + (int) this.m_pHeaderData.m_minCharSpacing;
        if (flag || CMath.Max(width, val2) <= maxWidth - num)
        {
          num += val2;
          ++charsFit;
        }
        else
          break;
      }
      if (charsFit > 0)
        num -= (int) this.m_pHeaderData.m_minCharSpacing;
      return num;
    }

    public override void PaintText(
      string text,
      int numChars,
      int x,
      int y,
      int maxWidth,
      int maxHeight)
    {
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      if (numChars < 0)
        numChars = text.Length;
      int num1 = 0;
      for (int index = 0; index < numChars; ++index)
      {
        char c = text[index];
        tGTFControlChar controlChar = this.GetControlChar(c);
        if (controlChar != null)
        {
          num1 += (int) controlChar.m_width + (int) this.m_pHeaderData.m_minCharSpacing;
        }
        else
        {
          tGTFChar tGtfChar = this.GetChar(c);
          if (tGtfChar != null)
          {
            int num2 = (int) tGtfChar.m_drawWidth;
            if (maxWidth >= 0)
              num2 = CMath.Max(0, CMath.Min((int) tGtfChar.m_drawWidth, maxWidth - num1 - (int) tGtfChar.m_drawOffsetX));
            int num3 = (int) tGtfChar.m_drawHeight;
            if (maxHeight >= 0)
              num3 = CMath.Max(0, CMath.Min((int) tGtfChar.m_drawHeight, maxHeight - (int) tGtfChar.m_drawOffsetY));
            CRectangle crectangle;
            crectangle.m_x = (int) tGtfChar.m_imageOffsetX;
            crectangle.m_y = (int) tGtfChar.m_imageOffsetY;
            crectangle.m_dx = num2;
            crectangle.m_dy = num3;
            instance.PushTransform();
            instance.Translate(x + num1 + (int) tGtfChar.m_drawOffsetX << 16, y + (int) tGtfChar.m_drawOffsetY << 16);
            instance.Draw(this.m_pImage, ICGraphics2d.Flip.NoFlip, new CRectangle?(crectangle));
            instance.PopTransform();
            num1 += (int) tGtfChar.m_width + (int) this.m_pHeaderData.m_minCharSpacing;
          }
        }
      }
    }

    public override int GetWidth(char c)
    {
      int width = 0;
      tGTFControlChar controlChar = this.GetControlChar(c);
      if (controlChar != null)
      {
        width = (int) controlChar.m_width;
      }
      else
      {
        tGTFChar tGtfChar = this.GetChar(c);
        if (tGtfChar != null)
          width = (int) tGtfChar.m_width;
      }
      return width;
    }

    public ICRenderSurface GetImage() => this.m_pImage;

    public tGTFHeader GetHeader() => this.m_pHeaderData;

    public tGTFChar GetChar(char c)
    {
      tGTFChar tGtfChar1 = (tGTFChar) null;
      if (this.m_pHeaderData.m_numChars > (short) 0)
      {
        int num1 = 0;
        int num2 = (int) this.m_pHeaderData.m_numChars - 1;
        while (num1 <= num2)
        {
          int index = num1 + num2 >> 1;
          tGTFChar tGtfChar2 = this.m_pCharData[index];
          if ((int) c == (int) tGtfChar2.m_charCode)
          {
            tGtfChar1 = tGtfChar2;
            break;
          }
          if ((int) c < (int) tGtfChar2.m_charCode)
            num2 = index - 1;
          else
            num1 = index + 1;
        }
      }
      return tGtfChar1;
    }

    public tGTFControlChar GetControlChar(char c)
    {
      tGTFControlChar controlChar = (tGTFControlChar) null;
      if (this.m_pHeaderData.m_numControlChars > (short) 0)
      {
        for (int index = 0; index < (int) this.m_pHeaderData.m_numControlChars; ++index)
        {
          if ((int) this.m_pControlCharData[index].m_charCode == (int) c)
          {
            controlChar = this.m_pControlCharData[index];
            break;
          }
        }
      }
      return controlChar;
    }
  }
}
