// Decompiled with JetBrains decompiler
// Type: com.glu.game.CTextScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CTextScreen : CWidgetScreen
  {
    protected CTextWidget m_text = new CTextWidget();
    protected string m_strText;
    private string kwszNetErrorMask = " (0x%2.2x:0x%2.2x:0x%2.2x)";

    public void SetText(string textID) => CUtility.GetString(out this.m_strText, textID);

    public void SetTextDirectly(string wszText) => this.m_strText = wszText;

    public string GetText() => this.m_strText;

    private void SetGameResultText()
    {
      this.m_strText = "";
      string output = (string) null;
      string id1;
      switch (CGHStaticData.m_difficultyLevel)
      {
        case CGHStaticData.eDifficulty.GAME_DIFFICULTY_MEDIUM:
          id1 = "IDS_MEDIUM";
          break;
        case CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT:
          id1 = "IDS_EXPERT";
          break;
        default:
          id1 = "IDS_EASY";
          break;
      }
      CUtility.GetString(out output, id1);
      this.m_strText += output;
      string id2;
      switch (CGHStaticData.m_instrumentBeingPlayed)
      {
        case CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS:
          id2 = "IDS_INSTRUMENT_BASS";
          break;
        case CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS:
          id2 = "IDS_INSTRUMENT_DRUMS";
          break;
        default:
          id2 = "IDS_INSTRUMENT_GUITAR";
          break;
      }
      CUtility.GetString(out output, id2);
      this.m_strText += output;
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_SCORE");
      this.m_strText += output;
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_HIT");
      this.m_strText += output;
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_MAXIMUM_NOTE_STREAK");
      this.m_strText += output;
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_STAR_RATING_FORMAT");
      this.m_strText += output;
    }

    public void SetAboutText()
    {
      this.m_strText = "";
      CUtility.AppendString(ref this.m_strText, "IDS_VERSION_TEXT");
      string output;
      CUtility.GetVersionString(out output);
      CTextScreen ctextScreen = this;
      ctextScreen.m_strText = ctextScreen.m_strText + " " + output;
      CUtility.AppendString(ref this.m_strText, "IDS_ABOUT_TEXT");
      CUtility.AppendString(ref this.m_strText, "IDS_COPYRIGHT_TEXT");
      this.m_strText += "\n\n$Name: GHERO5_20110124_1721_W $";
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      this.m_text.SetID(0);
      this.m_text.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_text.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_text.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_text.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_text.SetText(this.m_strText);
      this.m_page.Add((CUIWidget) this.m_text);
      this.SetContentLineHeightMultiple(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight());
      this.m_page.SetScrollArrows((ICRenderSurface) null, (ICRenderSurface) null);
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      bool flag = base.HandleEvent(id, param1, param2);
      if (flag)
      {
        switch (id)
        {
          case 544526345:
          case 1600235594:
            int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            break;
          case 1066869024:
          case 2535467201:
            int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
            break;
          case 2535475076:
          case 3563016926:
            int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
            break;
        }
      }
      return flag;
    }

    public override bool HandleRender()
    {
      bool flag = base.HandleRender();
      this.SetTouchRegions();
      return flag;
    }

    public override void SetTouchRegions()
    {
      this.ClearAllTouchRegions();
      CRectangle rect_out = new CRectangle();
      this.m_page.GetUpArrowRect(ref rect_out);
      rect_out.Inset(0);
      this.AddTouchRegion(rect_out, 1066869024U);
      this.m_page.GetDownArrowRect(ref rect_out);
      rect_out.Inset(0);
      this.AddTouchRegion(rect_out, 2535467201U);
    }
  }
}
