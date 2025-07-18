// Decompiled with JetBrains decompiler
// Type: com.glu.game.CHighscoreTableWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CHighscoreTableWidget : CDisplayTableWidget
  {
    protected CFont m_pFont;
    protected string[] m_pwszNames;
    protected int[] m_pScores;
    protected int m_numEntries;
    private string kwszVal = "%i";

    public CHighscoreTableWidget()
    {
      this.m_pFont = (CFont) null;
      this.m_pwszNames = (string[]) null;
      this.m_pScores = (int[]) null;
      this.m_numEntries = 0;
    }

    public void SetContent(CFont pFont, string[] pwszNames, int[] pScores, int numEntries)
    {
      this.m_pFont = pFont;
      this.m_pwszNames = pwszNames;
      this.m_pScores = pScores;
      this.m_numEntries = numEntries;
    }

    public override int MeasureRowHeight(int row)
    {
      int num = 0;
      if (this.m_pFont != null)
        num = this.m_pFont.GetFontHeight();
      return num;
    }

    public override int MeasureColumnWidth(int col, int available)
    {
      return col != 0 ? available : available / 2;
    }

    public override void RenderCellBackground(int row, int col, int width, int height)
    {
    }

    public override void RenderCellContent(int row, int col, int width, int height)
    {
      int x = 0;
      string text;
      if (col == 0)
      {
        text = this.m_pwszNames[row];
      }
      else
      {
        text = string.Concat((object) this.m_pScores[row]);
        x = width - this.m_pFont.MeasureTextWidth(string.Concat((object) this.m_pScores[row])) - 1;
      }
      if (text == null)
        return;
      this.m_pFont.PaintText(text, -1, x, 0, width - x, -1);
    }
  }
}
