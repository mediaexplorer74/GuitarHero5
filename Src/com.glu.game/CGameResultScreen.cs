// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameResultScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGameResultScreen : CWidgetScreen
  {
    protected CTextWidget m_text;
    protected string m_strText;
    protected string m_strInstrument;
    protected string m_strDifficulty;
    protected string m_strScore;
    protected string m_strNoteStreak;
    protected string m_strHit;
    protected string m_strPercentage;
    protected string m_strNumScore;
    protected string m_strNumNoteStreak;
    protected string m_strNumStarRating;
    protected string m_strNumHit;
    protected string m_strNumPercentage;
    protected SG_Presenter m_sgIconStar = new SG_Presenter();
    protected int m_sgIconStarWidth;
    protected int m_sgIconStarHeight;
    protected bool m_bFailed;
    protected CMarqueeText m_marquee = new CMarqueeText();
    private string kwszNetErrorMask = "(0x%2.2x:0x%2.2x:0x%2.2x)";
    private CRectangle rect;
    private CRectangle regionRect;

    public void SetFailed() => this.m_bFailed = true;

    public CGameResultScreen()
    {
      this.m_sgIconStar = (SG_Presenter) null;
      this.m_sgIconStarWidth = 0;
      this.m_bFailed = false;
    }

    private void SetText(string textID) => CUtility.GetString(out this.m_strText, textID);

    public override void CreateResources()
    {
      base.CreateResources();
      this.m_sgIconStar = new SG_Presenter(3, 0);
      this.m_sgIconStar.SetAnimation(15);
      this.m_sgIconStar.SetPosition(0, 0);
      this.m_sgIconStar.Bounds(ref this.rect);
      this.m_sgIconStarWidth = this.rect.m_dx + 2;
      this.m_sgIconStarHeight = this.rect.m_dy;
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      this.m_sgIconStar = (SG_Presenter) null;
    }

    public override void Build()
    {
      base.Build();
      CFontMgr.GetInstance();
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
      CUtility.GetString(out this.m_strDifficulty, id1);
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
      CUtility.GetString(out this.m_strInstrument, id2);
      CUtility.GetString(out this.m_strScore, "IDS_GAME_STATISTICS_SCORE");
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_SCORE_FORMAT");
      this.m_strNumScore = string.Format(output, (object) CGHStaticData.m_score);
      CUtility.GetString(out this.m_strHit, "IDS_GAME_STATISTICS_HIT");
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_HIT_FORMAT");
      this.m_strNumHit = string.Format(output, (object) CGHStaticData.m_percentageNotesHit, (object) CGHStaticData.m_hitNoteGroupCount, (object) (CGHStaticData.m_hitNoteGroupCount + CGHStaticData.m_missedNoteGroupCount));
      CUtility.GetString(out this.m_strNoteStreak, "IDS_GAME_STATISTICS_MAXIMUM_NOTE_STREAK");
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_MAXIMUM_NOTE_STREAK_FORMAT");
      this.m_strNumNoteStreak = string.Format(output, (object) CGHStaticData.m_maximumConsecutiveNotesHit);
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_STAR_RATING_FORMAT");
      this.m_strNumStarRating = string.Format(output, (object) CGHStaticData.m_starRating);
      CUtility.GetString(out this.m_strPercentage, "IDS_GAME_STATISTICS_PERCENTAGE_COMPLETED");
      CUtility.GetString(out output, "IDS_GAME_STATISTICS_PERCENTAGE_COMPLETED_FORMAT");
      int num = CMath.Min(100, (CGHStaticData.m_hitNoteGroupCount + CGHStaticData.m_missedNoteGroupCount) * 100 / CGHStaticData.m_totalSongNoteGroupCount);
      this.m_strNumPercentage = string.Format(output, (object) num);
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      if (id == 850690755U)
        this.m_marquee.Reset();
      return this.HandleMovieOut(id, param1, param2) || base.HandleEvent(id, param1, param2);
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = base.HandleUpdate(timeElapsedMS);
      this.m_marquee.HandleUpdate(timeElapsedMS);
      return flag;
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      this.RenderBegin();
      CFont font1 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      CFont font2 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_NUMBERFONT);
      CFont font3 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT);
      CSong songSelected = CSongListMgr.GetSongSelected();
      if (this.m_movie.GetUserRegionVisible(2U))
      {
        this.m_movie.GetUserRegion(2U, ref this.rect);
        font1.PaintText(this.m_strDifficulty, this.m_strDifficulty.Length, this.rect.m_x, this.rect.m_y);
      }
      if (this.m_movie.GetUserRegionVisible(3U))
      {
        this.m_movie.GetUserRegion(3U, ref this.rect);
        font1.PaintText(this.m_strInstrument, this.m_strInstrument.Length, this.rect.m_x, this.rect.m_y);
      }
      if (this.m_movie.GetUserRegionVisible(4U))
      {
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.regionRect);
        this.m_movie.GetUserRegion(4U, ref this.rect);
        int num = this.rect.m_y + (this.rect.m_dy - (font3.GetFontHeight() + font1.GetFontHeight()) >> 1);
        if (font3.MeasureTextWidth(songSelected.GetSongName()) > this.regionRect.m_dx)
        {
          if (this.rect.m_x >= 0)
          {
            this.rect.m_x = this.regionRect.m_x;
            this.rect.m_dx = this.regionRect.m_dx;
          }
          this.m_marquee.SetRect(this.rect);
          this.m_marquee.SetYPos(num);
          this.m_marquee.SetText(font3, songSelected.GetSongName());
          this.m_marquee.Paint();
        }
        else
        {
          int x = this.rect.m_x + (this.rect.m_dx - font3.MeasureTextWidth(songSelected.GetSongName()) >> 1);
          font3.PaintText(songSelected.GetSongName(), songSelected.GetSongName().Length, x, num);
        }
        int x1 = this.rect.m_x + (this.rect.m_dx - font1.MeasureTextWidth(songSelected.GetBandName()) >> 1);
        int y = num + font3.GetFontHeight();
        font1.PaintText(songSelected.GetBandName(), songSelected.GetBandName().Length, x1, y);
      }
      if (this.m_movie.GetChapter() == 0)
        this.m_marquee.Reset();
      if (this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
      {
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref this.rect);
        int x2 = this.rect.m_x;
        int y1 = this.rect.m_y;
        if (this.m_bFailed)
        {
          font1.PaintText(this.m_strPercentage, this.m_strPercentage.Length, x2, y1);
          int x3 = x2 + font1.MeasureTextWidth(this.m_strPercentage);
          font2.PaintText(this.m_strNumPercentage, this.m_strNumPercentage.Length, x3, y1);
        }
        else
        {
          font1.PaintText(this.m_strScore, this.m_strScore.Length, x2, y1);
          int x4 = x2 + font1.MeasureTextWidth(this.m_strScore);
          font2.PaintText(this.m_strNumScore, this.m_strNumScore.Length, x4, y1);
          int x5 = this.rect.m_x + this.rect.m_dx - font2.MeasureTextWidth(this.m_strNumStarRating);
          font2.PaintText(this.m_strNumStarRating, this.m_strNumStarRating.Length, x5, y1);
          this.m_sgIconStar.Draw(x5 - (this.m_sgIconStarWidth >> 1), y1 + (font2.GetFontHeight() >> 1));
          int x6 = this.rect.m_x;
          int y2 = y1 + font1.GetFontHeight();
          font1.PaintText(this.m_strNoteStreak, this.m_strNoteStreak.Length, x6, y2);
          int x7 = x6 + font1.MeasureTextWidth(this.m_strNoteStreak);
          font2.PaintText(this.m_strNumNoteStreak, this.m_strNumNoteStreak.Length, x7, y2);
          int x8 = this.rect.m_x;
          int y3 = y2 + font1.GetFontHeight();
          font1.PaintText(this.m_strHit, this.m_strHit.Length, x8, y3);
          int x9 = x8 + font1.MeasureTextWidth(this.m_strHit);
          font2.PaintText(this.m_strNumHit, this.m_strNumHit.Length, x9, y3);
        }
      }
      this.RenderEnd();
      return true;
    }

    public override void Layout() => base.Layout();

    public override void ChangeState(CWidgetScreen.eMovieStates newState)
    {
      base.ChangeState(newState);
    }
  }
}
