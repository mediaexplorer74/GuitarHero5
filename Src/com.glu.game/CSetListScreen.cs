// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSetListScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CSetListScreen : CWidgetScreen
  {
    public const int SWIPE_GOTO_CURSOR_INVALID = -1;
    public const int SPACE_BETWEEN_SONGS = 70;
    public const int STATE_IDLE = 0;
    public const int STATE_MOVE_TO_UP = 1;
    public const int STATE_MOVE_TO_DOWN = 2;
    public const int SPACING_BETWEEN_SCORES = 4;
    public const int MAX_SONG_TITLE_LENGTH = 100;
    private const int kAddtlSpacing = 5;
    private const string wszEllipsis = "...";
    private const uint SONGLIST_DASHED_LINE_HEIGHT = 2;
    private const uint SONGLIST_DASHED_LINE_WIDTH = 6;
    private const uint SONGLIST_DASHED_LINE_GAP = 2;
    protected new uint m_eventId;
    protected int m_x;
    protected int m_y;
    protected int m_dx;
    protected int m_dy;
    protected ushort m_cursor;
    protected ushort m_curInstrument;
    protected ushort m_curLine;
    protected ushort m_numShownLines;
    protected ushort m_totalLines;
    protected uint m_offsetY;
    protected uint m_timer;
    protected short m_state;
    protected ushort m_sgIconStarHalfWidth;
    protected ushort m_sgIconDiscWidth;
    protected ushort m_sgIconDiscHalfHeight;
    protected ushort m_selectBarTiledWidth;
    protected SG_Presenter sgIconGuitarLocked = new SG_Presenter();
    protected SG_Presenter sgIconBassLocked = new SG_Presenter();
    protected SG_Presenter sgIconDrumLocked = new SG_Presenter();
    protected SG_Presenter sgIconSelected = new SG_Presenter();
    protected SG_Presenter sgIconDisc = new SG_Presenter();
    protected SG_Presenter sgIconStar = new SG_Presenter();
    protected SG_Presenter sgIconGuitar = new SG_Presenter();
    protected SG_Presenter sgIconBass = new SG_Presenter();
    protected SG_Presenter sgIconDrum = new SG_Presenter();
    protected SG_Presenter sgUpArrow = new SG_Presenter();
    protected SG_Presenter sgDownArrow = new SG_Presenter();
    protected SG_Presenter sgLeftArrow = new SG_Presenter();
    protected SG_Presenter sgRightArrow = new SG_Presenter();
    protected SG_Presenter sg_selectedL = new SG_Presenter();
    protected SG_Presenter sg_selectedC = new SG_Presenter();
    protected SG_Presenter sg_selectedR = new SG_Presenter();
    protected string[] m_strInstruments = new string[3];
    protected string m_strSongScore;
    protected string m_strStarNum;
    protected string m_lockedSongTitle;
    protected bool m_curSongLocked;
    protected int m_additionalLines;
    protected int m_linesForCurrentSelection;
    protected int m_pointerDown_x;
    protected int m_pointerDown_y;
    protected int m_pointerLast_x;
    protected int m_pointerLast_y;
    protected bool m_pointerUp;
    protected bool m_pointerScrolled;
    protected int m_swipeOffset;
    protected int m_swipeAddtlOffsetNeeded;
    protected int m_swipeAdjSpeed;
    protected int m_swipeMinOffset;
    protected bool m_swipeGotoImmediately;
    protected bool m_swipePointerDown;
    protected int m_swipeMove;
    protected int m_swipeGotoCursor;
    protected CMarqueeText m_marquee = new CMarqueeText();

    protected bool IsSwipeGoingUp() => this.m_swipeMove < 0;

    protected bool IsSwipeGoingDown() => this.m_swipeMove > 0;

    public CSetListScreen()
    {
      this.m_eventId = 0U;
      this.m_x = 0;
      this.m_y = 0;
      this.m_dx = 0;
      this.m_dy = 0;
      this.m_cursor = (ushort) 0;
      this.m_curInstrument = (ushort) CGHStaticData.m_instrumentBeingPlayed;
      this.m_curLine = (ushort) 0;
      this.m_numShownLines = (ushort) 0;
      this.m_totalLines = (ushort) 0;
      this.m_offsetY = (uint) CGHStaticData.m_nSpaceCurrentToNextSong;
      this.m_state = (short) 0;
      this.m_timer = 0U;
      this.sg_selectedL = (SG_Presenter) null;
      this.sg_selectedC = (SG_Presenter) null;
      this.sg_selectedR = (SG_Presenter) null;
      this.m_strSongScore = "";
      this.m_strStarNum = "";
      this.m_curSongLocked = false;
      this.m_pointerScrolled = this.m_pointerUp = false;
      this.m_pointerDown_x = this.m_pointerDown_y = this.m_pointerLast_x = this.m_pointerLast_y = 0;
      this.sgUpArrow = this.sgDownArrow = (SG_Presenter) null;
      this.m_swipeAddtlOffsetNeeded = 0;
      this.m_swipeAdjSpeed = 0;
      this.m_swipeMinOffset = 0;
      this.m_swipePointerDown = false;
      this.m_swipeGotoImmediately = false;
      this.m_swipeMove = 0;
      this.m_swipeOffset = 0;
      this.m_swipeGotoCursor = -1;
    }

    public override uint Start()
    {
      uint num = base.Start();
      CGameData.SetOptionsActive(true);
      this.m_marquee.Reset();
      if (Consts.SWIPE_SUPPORTED)
        this.m_swipeAdjSpeed = CMathFixed.Int32ToFixed(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight()) / 500;
      return num;
    }

    public override void Stop() => base.Stop();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      COptionsMgr.GetInstance();
      ICMediaPlayer.GetInstance();
      bool flag = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 129075783:
          if (!this.m_curSongLocked)
          {
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            this.SetInterrupt(1);
          }
          flag = true;
          break;
        case 544526345:
        case 1600235594:
          if (!this.m_curSongLocked && !this.HandleMovieOutByAction(id, param1, (uint) param2))
          {
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            this.SetInterrupt(1);
          }
          flag = true;
          break;
        case 555763780:
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          this.SetInterrupt(2);
          flag = true;
          break;
        case 850690755:
          base.HandleEvent(id, param1, param2);
          CRectangle pRegion = new CRectangle();
          this.m_movie.Refresh();
          this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref pRegion);
          CFontMgr instance = CFontMgr.GetInstance();
          int num2 = pRegion.m_dy - CGHStaticData.m_nSpaceCurrentToNextSong - instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight();
          if (CGameApp.GetInstance().IsTouchscreenSupported())
          {
            CRectangle rect_out = new CRectangle();
            CRectangle crectangle = new CRectangle();
            this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref crectangle);
            int num3 = crectangle.m_y + (instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight() >> 1);
            this.sg_selectedC.Bounds(ref crectangle);
            int num4 = num3 + crectangle.m_dy;
            this.sgUpArrow.Bounds(ref rect_out);
            rect_out.Inset(0);
            if (pRegion.m_y - num4 < rect_out.m_dy + (rect_out.m_dy >> 1))
              num2 -= rect_out.m_dy + (rect_out.m_dy >> 1) - (pRegion.m_y - num4);
          }
          this.m_numShownLines = (ushort) (num2 / instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT).GetFontHeight());
          this.m_numShownLines = (ushort) CMath.Min((int) this.m_numShownLines, (int) this.m_totalLines);
          this.m_cursor = (ushort) CMath.Min((int) this.m_curLine + (int) this.m_numShownLines - this.m_additionalLines, (int) this.m_cursor);
          this.m_cursor = (ushort) CMath.Max((int) this.m_curLine, (int) this.m_cursor);
          this.m_marquee.Reset();
          flag = true;
          break;
        case 902008092:
        case 1386813809:
          this.m_pointerUp = true;
          this.m_swipePointerDown = false;
          break;
        case 902053462:
        case 2300082508:
          this.m_pointerDown_x = TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
          this.m_pointerDown_y = TouchUtil.TOUCH_EVENT_GET_Y((uint) param2);
          this.m_pointerLast_x = this.m_pointerDown_x;
          this.m_pointerLast_y = this.m_pointerDown_y;
          this.m_swipePointerDown = true;
          this.m_swipeMove = 0;
          break;
        case 902532892:
        case 2186393822:
          int pointerLastY = this.m_pointerLast_y;
          this.m_pointerLast_x = TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
          this.m_pointerLast_y = TouchUtil.TOUCH_EVENT_GET_Y((uint) param2);
          if (Consts.SWIPE_SUPPORTED && this.m_swipePointerDown)
          {
            this.m_swipeOffset += CMathFixed.Int32ToFixed(this.m_pointerLast_y - pointerLastY);
            this.m_swipeMove += this.m_pointerLast_y - pointerLastY;
            this.m_swipeOffset = CMathFixed.Min(this.m_swipeOffset, 0);
            this.m_swipeOffset = CMathFixed.Max(this.m_swipeOffset, this.m_swipeMinOffset);
            break;
          }
          break;
        case 1066869024:
          if (this.m_cursor > (ushort) 0)
          {
            --this.m_cursor;
            if ((int) this.m_cursor < (int) this.m_curLine && !Consts.SWIPE_SUPPORTED)
              --this.m_curLine;
            this.m_offsetY = 0U;
            this.m_timer = 0U;
            this.m_state = (short) 1;
            this.OnSelectedChange();
          }
          flag = true;
          break;
        case 1913978637:
          if (this.m_curInstrument < (ushort) 2)
          {
            ++this.m_curInstrument;
            this.OnSelectedChange();
          }
          flag = true;
          break;
        case 2535467201:
          if ((int) this.m_cursor < (int) this.m_totalLines - 1)
          {
            ++this.m_cursor;
            int currentSelection1 = this.CalculateAdditionalDisplayLinesForCurrentSelection((int) this.m_curLine, (int) this.m_numShownLines);
            int currentSelection2 = this.CalculateAdditionalDisplayLinesForCurrentSelection((int) this.m_curLine + 1, (int) this.m_numShownLines);
            if ((int) this.m_cursor >= (int) this.m_curLine + (int) this.m_numShownLines - currentSelection1 && !Consts.SWIPE_SUPPORTED)
            {
              if (currentSelection2 >= currentSelection1 || (int) this.m_cursor > (int) this.m_curLine + (int) this.m_numShownLines - currentSelection1)
                ++this.m_curLine;
              if (currentSelection2 > currentSelection1 && (int) this.m_cursor > (int) this.m_curLine + currentSelection2)
                ++this.m_curLine;
            }
            this.m_offsetY = 0U;
            this.m_timer = 0U;
            this.m_state = (short) 2;
            this.OnSelectedChange();
          }
          flag = true;
          break;
        case 2535498699:
          if (this.m_curInstrument > (ushort) 0)
          {
            --this.m_curInstrument;
            this.OnSelectedChange();
          }
          flag = true;
          break;
        case 3343010790:
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CApp.GetResourceManager();
      this.sgIconGuitarLocked = new SG_Presenter(3, 0);
      this.sgIconGuitarLocked.SetAnimation(33);
      this.sgIconGuitarLocked.SetPosition(0, 0);
      this.sgIconBassLocked = new SG_Presenter(3, 0);
      this.sgIconBassLocked.SetAnimation(34);
      this.sgIconBassLocked.SetPosition(0, 0);
      this.sgIconDrumLocked = new SG_Presenter(3, 0);
      this.sgIconDrumLocked.SetAnimation(35);
      this.sgIconDrumLocked.SetPosition(0, 0);
      this.sgIconSelected = new SG_Presenter(3, 0);
      this.sgIconSelected.SetAnimation(36);
      this.sgIconSelected.SetLoop(true);
      this.sgIconSelected.SetPosition(0, 0);
      this.sgIconStar = new SG_Presenter(3, 0);
      this.sgIconStar.SetAnimation(15);
      this.sgIconStar.SetPosition(0, 0);
      CRectangle rect_out = new CRectangle();
      this.sgIconStar.Bounds(ref rect_out);
      this.m_sgIconStarHalfWidth = (ushort) (rect_out.GetRight() - rect_out.GetLeft() >> 1);
      this.sgIconGuitar = new SG_Presenter(3, 0);
      this.sgIconGuitar.SetAnimation(30);
      this.sgIconGuitar.SetPosition(0, 0);
      this.sgIconBass = new SG_Presenter(3, 0);
      this.sgIconBass.SetAnimation(31);
      this.sgIconBass.SetPosition(0, 0);
      this.sgIconDrum = new SG_Presenter(3, 0);
      this.sgIconDrum.SetAnimation(32);
      this.sgIconDrum.SetPosition(0, 0);
      if (CGameApp.GetInstance().IsTouchscreenSupported())
      {
        this.sgUpArrow = new SG_Presenter(3, 0);
        this.sgUpArrow.SetAnimation(8);
        this.sgUpArrow.SetPosition(0, 0);
        this.sgUpArrow.SetLoop(true);
        this.sgDownArrow = new SG_Presenter(3, 0);
        this.sgDownArrow.SetAnimation(9);
        this.sgDownArrow.SetPosition(0, 0);
        this.sgDownArrow.SetLoop(true);
      }
      this.sgLeftArrow = new SG_Presenter(3, 0);
      this.sgLeftArrow.SetAnimation(10);
      this.sgLeftArrow.SetPosition(0, 0);
      this.sgLeftArrow.SetLoop(true);
      this.sgRightArrow = new SG_Presenter(3, 0);
      this.sgRightArrow.SetAnimation(11);
      this.sgRightArrow.SetPosition(0, 0);
      this.sgRightArrow.SetLoop(true);
      this.sg_selectedC = new SG_Presenter(3, 0);
      this.sg_selectedC.SetAnimation(17);
      this.sg_selectedC.SetPosition(0, 0);
      this.sg_selectedC.Bounds(ref rect_out);
      this.m_selectBarTiledWidth = (ushort) (rect_out.GetRight() - rect_out.GetLeft());
      this.sg_selectedL = new SG_Presenter(3, 0);
      this.sg_selectedL.SetAnimation(18);
      this.sg_selectedL.SetPosition(0, 0);
      this.sg_selectedR = new SG_Presenter(3, 0);
      this.sg_selectedR.SetAnimation(19);
      this.sg_selectedR.SetPosition(0, 0);
      this.sgIconDisc = new SG_Presenter(3, 0);
      this.sgIconDisc.SetAnimation(12);
      this.sgIconDisc.SetPosition(0, 0);
      this.sgIconDisc.Bounds(ref rect_out);
      this.m_sgIconDiscWidth = (ushort) (rect_out.GetRight() - rect_out.GetLeft() + 2);
      this.m_sgIconDiscHalfHeight = (ushort) (rect_out.GetBottom() - rect_out.GetTop() + 2 >> 1);
      CUtility.GetString(out this.m_strInstruments[0], "IDS_INSTRUMENT_GUITAR");
      CUtility.GetString(out this.m_strInstruments[1], "IDS_INSTRUMENT_BASS");
      CUtility.GetString(out this.m_strInstruments[2], "IDS_INSTRUMENT_DRUMS");
      CUtility.GetString(out this.m_lockedSongTitle, "IDS_LOCKED_SONG_TITLE");
      if (CGameApp.GetInstance().IsTouchscreenSupported())
        return;
      this.SetSlideBar(0);
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CApp.GetResourceManager();
      this.sgIconGuitarLocked = (SG_Presenter) null;
      this.sgIconBassLocked = (SG_Presenter) null;
      this.sgIconDrumLocked = (SG_Presenter) null;
      this.sgIconSelected = (SG_Presenter) null;
      this.sgIconGuitar = (SG_Presenter) null;
      this.sgIconBass = (SG_Presenter) null;
      this.sgIconDrum = (SG_Presenter) null;
      this.sgIconStar = (SG_Presenter) null;
      this.sgUpArrow = (SG_Presenter) null;
      this.sgDownArrow = (SG_Presenter) null;
      this.sgLeftArrow = (SG_Presenter) null;
      this.sgRightArrow = (SG_Presenter) null;
      this.sg_selectedC = (SG_Presenter) null;
      this.sg_selectedL = (SG_Presenter) null;
      this.sg_selectedR = (SG_Presenter) null;
      this.sgIconDisc = (SG_Presenter) null;
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      CFont font = instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      this.m_page.SetWrap(true);
      CRectangle rect_out1 = new CRectangle();
      this.sgIconGuitar.Bounds(ref rect_out1);
      CGHStaticData.m_nIconHeight = rect_out1.m_dy;
      CGHStaticData.m_nSpaceCurrentToNextSong = CGHStaticData.m_nIconHeight + font.GetFontHeight() * 2 + 4;
      CRectangle pRegion = new CRectangle();
      this.m_movie.SetChapter(this.m_chapterIdle, false);
      this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref pRegion);
      this.m_dy = pRegion.m_dy;
      this.m_movie.SetChapter(this.m_chapterIn, false);
      int num1 = this.m_dy - CGHStaticData.m_nSpaceCurrentToNextSong - instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight();
      if (CGameApp.GetInstance().IsTouchscreenSupported())
      {
        CRectangle rect_out2 = new CRectangle();
        CRectangle crectangle = new CRectangle();
        this.m_movie.GetUserRegion((uint) this.REGION_TITLE, ref crectangle);
        int num2 = crectangle.m_y + (instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight() >> 1);
        this.sg_selectedC.Bounds(ref crectangle);
        int num3 = num2 + crectangle.m_dy;
        this.sgUpArrow.Bounds(ref rect_out2);
        rect_out2.Inset(0);
        if (pRegion.m_y - num3 < rect_out2.m_dy + (rect_out2.m_dy >> 1))
          num1 -= rect_out2.m_dy + (rect_out2.m_dy >> 1) - (pRegion.m_y - num3);
      }
      this.m_numShownLines = (ushort) (num1 / font.GetFontHeight());
      this.m_totalLines = !CDemoMgr.IsDemo() ? CSongListMgr.GetNumSongs() : (ushort) 3;
      this.m_numShownLines = !Consts.SWIPE_SUPPORTED ? (ushort) CMath.Min((int) this.m_numShownLines, (int) this.m_totalLines) : this.m_totalLines;
      CSong songSelected = CSongListMgr.GetSongSelected();
      if (songSelected != null)
      {
        for (ushort index = 0; (int) index < (int) CSongListMgr.GetNumSongs(); ++index)
        {
          CSong song = CSongListMgr.GetSong(index);
          if (song == null || (int) song.GetSongID() != (int) songSelected.GetSongID())
            this.HandleEvent(2535467201U, 0U, (object) 0);
          else
            break;
        }
      }
      this.OnSelectedChange();
    }

    public override void Layout()
    {
      base.Layout();
      if (this.m_movie.GetUserRegionVisible((uint) CWidgetScreen.REGION_CONTENT))
      {
        CRectangle pRegion = new CRectangle();
        this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref pRegion);
        this.m_x = pRegion.m_x;
        this.m_y = pRegion.m_y;
        this.m_dx = pRegion.m_dx;
      }
      else
      {
        this.m_x = -1000;
        this.m_y = -1000;
      }
    }

    public override bool HandleRender()
    {
      base.HandleRender();
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      int x1 = this.m_x;
      int y1 = this.m_y;
      CFont font1 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      CFont font2 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT);
      CFont font3 = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_NUMBERFONT);
      this.RenderBegin();
      int num1 = x1 + (int) this.m_sgIconDiscWidth;
      int num2;
      bool flag1 = (num2 = 0) != 0;
      bool flag2 = num2 != 0;
      bool flag3 = num2 != 0;
      bool flag4 = CGameApp.GetInstance().IsTouchscreenSupported();
      int num3 = -1;
      int num4 = -1;
      int num5 = 0;
      int num6 = y1;
      CRectangle rect_out1 = new CRectangle();
      CRectangle rect_out2 = new CRectangle();
      int num7 = 0;
      if (flag4)
      {
        this.sgUpArrow.Bounds(ref rect_out1);
        CGHStaticData.m_pTitleTile.Bounds(ref rect_out2);
        if (y1 - (rect_out2.m_y + rect_out2.m_dy) < rect_out1.m_dy + (rect_out1.m_dy >> 1))
          num7 = rect_out1.m_dy + (rect_out1.m_dy >> 1) - (y1 - (rect_out2.m_y + rect_out2.m_dy));
        this.sgUpArrow.SetPosition((int) Phone.GetWidth() >> 1, rect_out2.m_y + rect_out2.m_dy + rect_out1.m_dy);
        this.sgUpArrow.Draw();
      }
      int num8 = y1 + num7;
      CRectangle rect1 = new CRectangle();
      if (Consts.SWIPE_SUPPORTED && flag4)
      {
        rect1.Set(instance.GetClip());
        instance.SetClip((uint) this.m_x, (uint) (this.m_y + num7), (uint) this.m_dx, (uint) (this.m_dy - num7));
        num8 += CMathFixed.FixedToInt32(this.m_swipeOffset);
      }
      int num9 = num8;
      this.m_additionalLines = 0;
      this.m_linesForCurrentSelection = 1;
      int maxWidth = this.m_x + this.m_dx - num1;
      for (ushort curLine = this.m_curLine; (Consts.SWIPE_SUPPORTED || (int) curLine <= (int) this.m_curLine + (int) this.m_numShownLines - this.m_additionalLines) && (int) curLine < (int) this.m_totalLines; ++curLine)
      {
        if (flag4)
          num5 = num6;
        bool flag5 = false;
        int num10 = num8;
        if (Consts.SWIPE_SUPPORTED && this.m_swipePointerDown)
          flag5 = this.IsSwipeGoingUp() && num10 > this.m_y && num10 < this.m_y + this.m_dy - num7 || this.IsSwipeGoingDown() && num10 < this.m_y;
        if ((int) curLine < (int) CSongListMgr.GetNumSongs())
        {
          CSong song = CSongListMgr.GetSong(curLine);
          uint songMediaLocal = song.GetSongMediaLocal();
          uint num11 = song.Gebyte(CSongListMgr.GetNumTracksSupported(), Consts.eInstrument.INSTRUMENT_GUITAR);
          bool flag6 = ((int) songMediaLocal & (int) num11) == (int) num11;
          uint num12 = song.Gebyte(CSongListMgr.GetNumTracksSupported(), Consts.eInstrument.INSTRUMENT_BASS);
          bool flag7 = ((int) songMediaLocal & (int) num12) == (int) num12;
          uint num13 = song.Gebyte(CSongListMgr.GetNumTracksSupported(), Consts.eInstrument.INSTRUMENT_DRUMS);
          bool flag8 = ((int) songMediaLocal & (int) num13) == (int) num13;
          if (this.m_state == (short) 2 || this.m_state == (short) 0)
          {
            if ((int) this.m_cursor == (int) curLine)
              num8 += (int) ((long) CGHStaticData.m_nSpaceCurrentToNextSong - (long) this.m_offsetY);
            if ((int) this.m_cursor + 1 == (int) curLine)
              num8 += (int) this.m_offsetY;
          }
          if (song.IsHeadline())
          {
            int num14 = font2.MeasureTextWidth(song.GetMetaString(CSong.eMetaItemID.METAID_HEADLINE_TEXT));
            font2.PaintText(song.GetMetaString(CSong.eMetaItemID.METAID_HEADLINE_TEXT), song.GetMetaString(CSong.eMetaItemID.METAID_HEADLINE_TEXT).Length, num1 + (maxWidth - num14 >> 1), num8);
            num8 += font2.GetFontHeight();
            ++this.m_additionalLines;
            if ((int) this.m_cursor == (int) curLine)
              ++this.m_linesForCurrentSelection;
          }
          if ((int) this.m_cursor == (int) curLine)
          {
            int num15 = font1.GetFontHeight() >> 1;
            this.sg_selectedL.SetPosition(num1, num8 + num15);
            this.sg_selectedR.SetPosition((int) this.sg_selectedL.GetPositionX() + maxWidth, num8 + num15);
            this.sg_selectedC.SetPosition((int) this.sg_selectedL.GetPositionX(), num8 + num15);
            for (int x2 = 0; x2 < maxWidth - (int) this.m_selectBarTiledWidth; x2 += (int) this.m_selectBarTiledWidth)
              this.sg_selectedC.Draw(x2, 0);
            this.sg_selectedC.Draw(maxWidth - (int) this.m_selectBarTiledWidth, 0);
            this.sg_selectedL.Draw();
            this.sg_selectedR.Draw();
          }
          string str1 = "";
          string str2 = !song.IsEncore() || CSongListMgr.IsSongUnlocked(song.GetSongID()) ? str1 + song.GetSongName() : str1 + this.m_lockedSongTitle;
          if ((int) this.m_cursor == (int) curLine)
          {
            CRectangle rect_out3 = new CRectangle();
            this.sg_selectedC.Bounds(ref rect_out3);
            if (Consts.SWIPE_SUPPORTED && flag4)
              rect_out3.Clip(instance.GetClip());
            CRectangle rect2 = new CRectangle();
            rect2.Set(instance.GetClip());
            instance.SetClip((uint) this.m_x, (uint) (this.m_y + num7), (uint) this.m_dx, (uint) (this.m_dy - num7));
            if (rect_out3.m_x > 0 && maxWidth > 0)
            {
              rect_out3.m_dx = maxWidth;
              this.m_marquee.SetRect(rect_out3);
              this.m_marquee.SetYPos(num8);
              this.m_marquee.SetText(font1, str2);
              this.m_marquee.Paint();
            }
            instance.SetClip(rect2);
          }
          else if (Consts.SWIPE_SUPPORTED || (int) curLine <= (int) this.m_curLine + (int) this.m_numShownLines - this.m_additionalLines)
          {
            int num16 = font1.MeasureTextWidth(str2);
            int length = str2.Length;
            int num17 = font1.MeasureTextWidth("...");
            int numChars = length;
            if (num16 > maxWidth && maxWidth > 0)
            {
              for (; num16 > maxWidth - num17; num16 = font1.MeasureTextWidth(str2, numChars))
                --numChars;
            }
            font1.PaintText(str2, numChars, num1, num8, maxWidth);
            if (length != numChars)
              font1.PaintText("...", "...".Length, num1 + num16, num8);
          }
          else if (flag4)
            num5 = num8 + font1.GetFontHeight();
          if ((Consts.SWIPE_SUPPORTED || (int) curLine <= (int) this.m_curLine + (int) this.m_numShownLines - this.m_additionalLines) && (flag6 || flag7 || flag8))
            this.sgIconDisc.Draw(num1 - (int) this.m_sgIconDiscWidth, num8 - (int) this.m_sgIconDiscHalfHeight + (font1.GetFontHeight() >> 1));
          num8 += font1.GetFontHeight();
          if (flag4)
            num6 = num8;
          if ((int) this.m_cursor == (int) curLine)
          {
            flag1 = flag6;
            flag3 = flag7;
            flag2 = flag8;
            int width = (int) Phone.GetWidth();
            int num18 = num8 + CGHStaticData.m_nIconHeight;
            if (this.m_curInstrument == (ushort) 0)
              this.sgIconSelected.Draw(width / 3, num18);
            if (flag6)
            {
              this.sgIconGuitar.SetPosition(width / 3, num18);
              this.sgIconGuitar.Draw();
            }
            else
            {
              this.sgIconGuitarLocked.SetPosition(width / 3, num18);
              this.sgIconGuitarLocked.Draw();
            }
            if (this.m_curInstrument == (ushort) 1)
              this.sgIconSelected.Draw(width / 2, num18);
            if (flag7)
            {
              this.sgIconBass.SetPosition(width / 2, num18);
              this.sgIconBass.Draw();
            }
            else
            {
              this.sgIconBassLocked.SetPosition(width / 2, num18);
              this.sgIconBassLocked.Draw();
            }
            if (this.m_curInstrument == (ushort) 2)
              this.sgIconSelected.Draw(width * 2 / 3, num18);
            if (flag8)
            {
              this.sgIconDrum.SetPosition(width * 2 / 3, num18);
              this.sgIconDrum.Draw();
            }
            else
            {
              this.sgIconDrumLocked.SetPosition(width * 2 / 3, num18);
              this.sgIconDrumLocked.Draw();
            }
            if (!Consts.SWIPE_SUPPORTED)
            {
              this.sgLeftArrow.SetPosition(width / 5, num18);
              this.sgLeftArrow.Draw();
              this.sgRightArrow.SetPosition(width * 4 / 5, num18);
              this.sgRightArrow.Draw();
            }
            int y2 = num18 + font1.GetFontHeight();
            string strInstrument = this.m_strInstruments[(int) this.m_curInstrument];
            font1.PaintText(strInstrument, strInstrument.Length, num1, y2);
            if (CGHStaticData.m_gameplayMode == 0)
            {
              font3.PaintText(this.m_strSongScore, this.m_strSongScore.Length, num1 + this.m_dx * 6 / 20, y2);
              this.sgIconStar.Draw(num1 + this.m_dx * 15 / 20 - (int) this.m_sgIconStarHalfWidth, y2 + font3.GetFontHeight() / 2);
              font3.PaintText(this.m_strStarNum, this.m_strStarNum.Length, num1 + this.m_dx * 15 / 20, y2);
            }
            int num19 = y2 + font1.GetFontHeight();
            if (flag4)
              num6 = num19;
            for (int index = num1; index < this.m_dx; index += 8)
            {
              CRectangle rect3;
              rect3.m_x = index;
              rect3.m_y = num19;
              rect3.m_dx = 6;
              rect3.m_dy = 2;
              CDrawUtil.FillRect(rect3, Consts.Color_MakeA8R8G8B8(0, 0, 106, 133));
            }
          }
          if (this.m_state == (short) 1)
          {
            if ((int) this.m_cursor == (int) curLine)
              num8 += (int) this.m_offsetY;
            if ((int) this.m_cursor + 1 == (int) curLine)
              num8 += (int) ((long) CGHStaticData.m_nSpaceCurrentToNextSong - (long) this.m_offsetY);
          }
          if (this.m_swipeGotoCursor != -1 && (int) curLine == this.m_swipeGotoCursor)
          {
            if (this.m_swipeMinOffset != 0)
            {
              if (num10 < this.m_y)
                this.m_swipeAddtlOffsetNeeded = CMathFixed.Int32ToFixed(this.m_y - num10);
              else if (num8 > this.m_y + this.m_dy - num7)
                this.m_swipeAddtlOffsetNeeded = CMathFixed.Int32ToFixed((int) ((long) (this.m_y + this.m_dy - num7 - num8) - (long) this.m_offsetY));
              this.m_swipeGotoCursor = -1;
              if (this.m_swipeGotoImmediately)
              {
                this.m_swipeOffset += this.m_swipeAddtlOffsetNeeded;
                this.m_swipeAddtlOffsetNeeded = 0;
                this.m_swipeGotoImmediately = false;
              }
            }
            else
              this.m_swipeGotoImmediately = true;
          }
          if (flag5)
          {
            if (this.IsSwipeGoingUp() && num8 > this.m_y + this.m_dy - num7)
              this.m_swipeAddtlOffsetNeeded = CMathFixed.Int32ToFixed(this.m_y + this.m_dy - num7 - num8);
            else if (this.IsSwipeGoingDown() && num10 < this.m_y && num8 > this.m_y)
              this.m_swipeAddtlOffsetNeeded = CMathFixed.Int32ToFixed(num8 - num10);
          }
          if (flag4 && this.m_pointerDown_x > num1)
          {
            if ((!Consts.SWIPE_SUPPORTED || this.m_pointerDown_y >= this.m_y) && this.m_pointerLast_y <= this.m_y + this.m_dy - num7)
            {
              if (this.m_pointerDown_y > num5 && this.m_pointerDown_y < num6)
                num3 = (int) curLine;
              if (this.m_pointerLast_y > num5 && this.m_pointerLast_y < num6)
                num4 = (int) curLine;
            }
            else
              continue;
          }
          num8 += 5;
        }
        else
          break;
      }
      if (flag4)
      {
        if ((int) this.m_cursor != (int) this.m_totalLines - 1)
        {
          CRectangle rect4 = new CRectangle();
          rect4.Set(instance.GetClip());
          if (Consts.SWIPE_SUPPORTED)
            instance.SetClip(rect1);
          CRectangle pRegion = new CRectangle();
          this.m_movie.GetUserRegion((uint) CWidgetScreen.REGION_CONTENT, ref pRegion);
          this.sgDownArrow.SetPosition((short) ((int) Phone.GetWidth() >> 1), (short) (pRegion.m_y + pRegion.m_dy + (rect_out1.m_dy >> 1)));
          this.sgDownArrow.Draw();
          if (Consts.SWIPE_SUPPORTED)
            instance.SetClip(rect4);
        }
        if (num3 != -1 && num4 != -1 && num3 != num4)
          this.m_pointerScrolled = true;
        if (this.m_pointerLast_x == 0 && this.m_pointerLast_y == 0)
        {
          this.m_pointerLast_x = this.m_pointerDown_x;
          this.m_pointerLast_y = this.m_pointerDown_y;
        }
        if (num4 == -1)
          num4 = num3;
        if (this.m_pointerUp)
        {
          if (num3 == num4 && !this.m_pointerScrolled)
          {
            if ((int) this.m_cursor != num4 && num4 != -1)
            {
              this.m_cursor = (ushort) num4;
              if ((int) this.m_cursor < (int) this.m_curLine && !Consts.SWIPE_SUPPORTED)
                this.m_curLine = this.m_cursor;
              this.m_offsetY = 0U;
              this.m_timer = 0U;
              this.m_state = (short) 1;
              this.OnSelectedChange();
              if (!Consts.SWIPE_SUPPORTED)
                ;
            }
            else
            {
              CRectangle rect_out4 = new CRectangle();
              for (int index = 0; index < 7; ++index)
              {
                SG_Presenter sgPresenter = (SG_Presenter) null;
                switch (index)
                {
                  case 0:
                    sgPresenter = !flag1 ? this.sgIconGuitarLocked : this.sgIconGuitar;
                    break;
                  case 1:
                    sgPresenter = !flag3 ? this.sgIconBassLocked : this.sgIconBass;
                    break;
                  case 2:
                    sgPresenter = !flag2 ? this.sgIconDrumLocked : this.sgIconDrum;
                    break;
                  case 3:
                    sgPresenter = this.sgUpArrow;
                    break;
                  case 4:
                    sgPresenter = this.sgDownArrow;
                    break;
                  case 5:
                    sgPresenter = this.sgLeftArrow;
                    break;
                  case 6:
                    sgPresenter = this.sgRightArrow;
                    break;
                }
                sgPresenter.Bounds(ref rect_out4);
                rect_out4.Inset(0);
                if (index == 4 || index == 3)
                  rect_out4.Inset(0);
                if (this.m_pointerLast_x > rect_out4.m_x && this.m_pointerLast_x < rect_out4.m_x + rect_out4.m_dx && this.m_pointerLast_y > rect_out4.m_y && this.m_pointerLast_y < rect_out4.m_y + rect_out4.m_dy)
                {
                  if ((int) this.m_curInstrument == index)
                  {
                    this.HandleEvent(544526345U, 0U, (object) 0U);
                  }
                  else
                  {
                    switch (index)
                    {
                      case 0:
                      case 1:
                      case 2:
                        this.m_curInstrument = (ushort) index;
                        this.OnSelectedChange();
                        continue;
                      case 3:
                        this.HandleEvent(1066869024U, 0U, (object) 0);
                        continue;
                      case 4:
                        this.HandleEvent(2535467201U, 0U, (object) 0);
                        continue;
                      case 5:
                        this.HandleEvent(2535498699U, 0U, (object) 0);
                        continue;
                      case 6:
                        this.HandleEvent(1913978637U, 0U, (object) 0);
                        continue;
                      default:
                        continue;
                    }
                  }
                }
              }
            }
          }
          this.m_pointerScrolled = this.m_pointerUp = false;
          this.m_pointerDown_x = this.m_pointerLast_x = 0;
          this.m_pointerDown_y = this.m_pointerLast_y = 0;
        }
      }
      if (this.m_swipeMinOffset == 0 && this.m_y != -1000 && this.m_movie.GetChapter() == this.m_chapterIdle)
      {
        this.m_swipeMinOffset = 0;
        this.m_swipeMinOffset = CMathFixed.Int32ToFixed(num9 + this.m_dy - (num8 + font2.GetFontHeight()));
        if ((int) this.m_cursor >= (int) this.m_totalLines - 1)
          this.m_swipeMinOffset -= CMathFixed.Int32ToFixed(this.m_offsetY);
      }
      if (Consts.SWIPE_SUPPORTED && flag4)
        instance.SetClip(rect1);
      this.RenderEnd();
      return true;
    }

    private int CalculateAdditionalDisplayLinesForCurrentSelection(
      int songIndex,
      int maxDisplayLines)
    {
      int currentSelection = 0;
      for (ushort index = (ushort) songIndex; (int) index <= songIndex + maxDisplayLines - currentSelection; ++index)
      {
        if ((int) index < (int) CSongListMgr.GetNumSongs() && CSongListMgr.GetSong(index).IsHeadline())
          ++currentSelection;
      }
      return currentSelection;
    }

    private int GetInterpolationTime(int timeMS, int timeMSA, int timeMSB)
    {
      if (timeMS < timeMSA)
        timeMS = timeMSA;
      else if (timeMS > timeMSB)
        timeMS = timeMSB;
      int denom = timeMSB - timeMSA;
      return denom == 0 ? 0 : CMathFixed.Div(timeMS - timeMSA, denom);
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      this.m_marquee.HandleUpdate(timeElapsedMS);
      this.sgIconSelected.Update(timeElapsedMS);
      if (Consts.SWIPE_SUPPORTED && !this.m_swipePointerDown && this.m_swipeAddtlOffsetNeeded != 0)
      {
        int num = CMathFixed.Min(this.m_swipeAdjSpeed * timeElapsedMS, CMathFixed.Abs(this.m_swipeAddtlOffsetNeeded));
        if (this.m_swipeAddtlOffsetNeeded > 0)
        {
          this.m_swipeOffset += num;
          this.m_swipeAddtlOffsetNeeded -= num;
          if (this.m_swipeOffset > 0)
          {
            this.m_swipeOffset = 0;
            this.m_swipeAddtlOffsetNeeded = 0;
          }
        }
        else
        {
          this.m_swipeOffset -= num;
          this.m_swipeAddtlOffsetNeeded += num;
        }
      }
      if (this.m_timer < 200U)
        this.m_timer += (uint) timeElapsedMS;
      if (this.m_offsetY >= (uint) CGHStaticData.m_nSpaceCurrentToNextSong)
        this.m_state = (short) 0;
      int interpolationTime = this.GetInterpolationTime(CMathFixed.Int32ToFixed(this.m_timer), CMathFixed.Int32ToFixed(0), CMathFixed.Int32ToFixed(200));
      this.m_offsetY = (uint) CMathFixed.FixedToInt32(CMathFixed.Lerp(0, CMathFixed.Int32ToFixed(CGHStaticData.m_nSpaceCurrentToNextSong), interpolationTime));
      if (CGameApp.GetInstance().IsTouchscreenSupported())
      {
        this.sgUpArrow.Update(timeElapsedMS);
        this.sgDownArrow.Update(timeElapsedMS);
      }
      this.sgLeftArrow.Update(timeElapsedMS);
      this.sgRightArrow.Update(timeElapsedMS);
      return true;
    }

    private void OnSelectedChange()
    {
      if (Consts.SWIPE_SUPPORTED)
        this.m_swipeGotoCursor = (int) this.m_cursor;
      CGHStaticData.m_instrumentBeingPlayed = (CGHStaticData.eGameInstrument) this.m_curInstrument;
      if (!CGameApp.GetInstance().IsTouchscreenSupported())
        this.SetSlideBar((int) this.m_cursor * 100 / ((int) this.m_totalLines - 1));
      CSong song = CSongListMgr.GetSong(this.m_cursor);
      CGHStaticData.m_venueSelected = (int) song.GetSongVenueID();
      if (CGHStaticData.m_venueSelected > 0)
        --CGHStaticData.m_venueSelected;
      this.m_strSongScore = "0";
      this.m_strStarNum = "\\0";
      CSongScoreMgr.SongScore songScore = CSongScoreMgr.GetSongScore(song.GetSongID());
      if (songScore != null)
      {
        this.m_strSongScore = string.Concat((object) songScore.GetScore((Consts.eDifficulty) CGHStaticData.m_difficultyLevel, (Consts.eInstrument) this.m_curInstrument));
        this.m_strStarNum = string.Concat((object) songScore.GetStarRating((Consts.eDifficulty) CGHStaticData.m_difficultyLevel, (Consts.eInstrument) this.m_curInstrument));
      }
      this.m_curSongLocked = !CSongListMgr.IsSongUnlocked(song.GetSongID());
      this.m_marquee.Reset();
      int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuScrollSFX);
    }

    public uint GetSelectedSongID() => CSongListMgr.GetSong(this.m_cursor).GetSongID();
  }
}
