// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGuitarHeroGame
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework.Media;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace com.glu.game
{
  internal class CGuitarHeroGame : CNode
  {
    public const int MAXIMUM_SONGS_AVAILABLE = 128;
    public const int AUDIO_SYNCH_TWEAK_VALUE_MS = 0;
    public const int STAR_POWER_VIBRATION_TIME_IN_MS = 400;
    public const int KICK_PEDAL_NOTE_ANIM_SCALE_REDUCTION = 0;
    public const int DRUM_KICK_PEDAL_FRET_ANIM_SCALE_REDUCTION = 2;
    public const uint COLOR_GUITAR_PLAYFIELD = 4290822336;
    public const uint COLOR_GUITAR_FRETBOARD = 4286611584;
    public const uint COLOR_SUSTAIN_STARPOWERED_NOTE = 4278728573;
    public const uint COLOR_SUSTAIN_STARPOWERED_CENTER = 4279010815;
    public const uint COLOR_SUSTAIN_STARPOWERED_PLAYING = 4278242508;
    public const uint COLOR_SUSTAIN_STARPOWERED_CENTER_PLAYING = 4278255615;
    public const uint COLOR_SUSTAIN_GREEN_NOTE = 4278215424;
    public const uint COLOR_SUSTAIN_GREEN_CENTER = 4278306561;
    public const uint COLOR_SUSTAIN_GREEN_PLAYING = 3422617344;
    public const uint COLOR_SUSTAIN_GREEN_CENTER_PLAYING = 4278255360;
    public const uint COLOR_SUSTAIN_RED_NOTE = 4286841346;
    public const uint COLOR_SUSTAIN_RED_CENTER = 4293920261;
    public const uint COLOR_SUSTAIN_RED_PLAYING = 4291559424;
    public const uint COLOR_SUSTAIN_RED_CENTER_PLAYING = 4294901760;
    public const uint COLOR_SUSTAIN_YELLOW_NOTE = 4286804225;
    public const uint COLOR_SUSTAIN_YELLOW_CENTER = 4294103815;
    public const uint COLOR_SUSTAIN_YELLOW_PLAYING = 4292730112;
    public const uint COLOR_SUSTAIN_YELLOW_CENTER_PLAYING = 4294967040;
    public const uint COLOR_SUSTAIN_MISSED_NOTE = 4288256409;
    public const uint COLOR_SUSTAIN_MISSED_CENTER = 4282664004;
    public const int SUSTAIN_NOTE_CENTER_WIDTH = 5;
    public const int SUSTAIN_NOTE_LINE_WIDTH = 12;
    public const int SUSTAIN_PLAYING_NOTE_LINE_WIDTH = 18;
    public const int MINIMUM_SUSTAIN_LINE_WIDTH_SCALE = 16384;
    public const int TOTAL_NOTE_TYPES = 4;
    public const int TOTAL_NOTE_SIZES = 4;
    public const int TOTAL_NOTE_TRACKS = 4;
    public const int TOTAL_BUTTONS = 4;
    public const int TOTAL_TOUCH_NOTE_BUTTONS = 3;
    public const int TOTAL_BUTTON_KEY_ROWS = 4;
    public const int MAX_NOTE_BARS_VISIBLE = 10;
    public const int NUMBER_OF_BARS_IN_A_QUARTER_NOTE = 2;
    public const int MAX_NOTE_ROWS_VISIBLE = 240;
    public const int SONG_END_QUARTER_NOTE_PAD_LENGTH = 5;
    public const int DEFAULT_NOTE_INPUT_PRE_NOTE_FORGIVENESS = 24;
    public const int DEFAULT_NOTE_INPUT_POST_NOTE_FORGIVENESS = 36;
    public const int NUMBER_OF_NOTE_DIVISIONS_IN_A_BAR = 24;
    public const int NUMBER_OF_NOTE_DIVISIONS_BETWEEN_FRET_BARS = 48;
    public const int NUMBER_OF_NOTE_DIVISIONS_IN_A_QUARTER_NOTE = 48;
    public const int NUMBER_OF_NOTE_DIVISIONS_PER_MEASURE = 192;
    public const int NUMBER_OF_NOTE_DIVISIONS_FOR_FULL_STAR_POWER = 1536;
    public const int NOTE_SCROLL_FIXED_ONE = 65536;
    public const int MICROSECONDS_PER_MINUTE = 60000000;
    public const int MS_PER_SECOND = 1000;
    public const int MS_PER_MINUTE = 60000;
    public const int DEFAULT_STAR_POINTS_PER_GROUP = 50;
    public const int MAX_STAR_POWER_PHRASES_PER_SONG = 16;
    public const int MAX_STAR_POWER_POINTS = 100;
    public const int MINIMUM_STAR_POINTS_REQUIRED_TO_ACTIVE_STAR_POWER = 50;
    public const int SUSTAIN_NOTE_MINIMUM_DURATION = 240;
    public const int DEFAULT_SCORE_MULTIPLIER = 1;
    public const int MAXIMUM_SCORE_MULTIPLIER = 4;
    public const int DEFAULT_NOTE_SCORE = 50;
    public const int DEFAULT_SUSTAIN_DIVISION_SCORE = 2;
    public const int MISSED_NOTE_MUTE_TIME_IN_MS = 800;
    public const int BAD_NOTE_MUTE_TIME_IN_MS = 800;
    public const int AUDIO_TRACK_FADE_OUT_TIME_MS = 200;
    public const int AUDIO_TRACK_FADE_IN_TIME_MS = 200;
    public const int INVALID_INDEX = -1;
    public const int DEBUG_TRACK_START_OFFSET = 0;
    public const int SONG_DEBUG_STATS_Y_POSITION = 10;
    public const int MAX_STAR_PARTICLE_EFFECTS = 40;
    public const int TITLE_DISPLAY_TIME = 2000;
    public const int YOU_ROCK_DISPLAY_TIME = 4000;
    public const int RESUME_COUNTDOWN_TIME = 1500;
    public const int RESUME_COUNTDOWN_COUNT = 3;
    public const int NOTES_TO_SCAN = 48;
    public const int NOTE_PLAYABLE = 9;
    public const int NOTE_PLAYING = 128;
    public static bool m_zunePlaying;
    public bool MINIMAL_DRAW_TEST;
    public bool loadingDone;
    protected int m_pointerX;
    protected int m_realPointerX;
    protected int m_pointerY;
    protected int m_realPointerY;
    protected bool m_bPointerActive;
    protected bool m_bRealPointerActive;
    protected bool m_bJustReleaseRecieved;
    protected CGuitarHeroGame.eRenderState m_renderState;
    protected int m_constAlpha;
    protected int m_gameInput;
    protected bool m_moveWithPointer;
    protected int m_lastPointerX;
    protected int m_lastPointerY;
    protected bool m_bGamePaused;
    protected CGHStaticData m_CGHStaticData;
    public int m_loadingLoopCounter;
    protected CGuitarHeroGame.sSustainNoteInfo[] m_pSustainNotesPositionInScore;
    protected int m_totalStarPowerPhrasesCompleted;
    protected int m_totalStarPowerPhrases;
    protected int m_starPowerScore;
    protected CHud pHud;
    protected int m_activedStarPowerRemainingNoteDivisions;
    protected int m_numStarPowersInList;
    protected CGuitarHeroGame.sStarPower[] m_pStarPowersList;
    protected int m_totalConsecutiveStarPowerNotesHit;
    protected byte[][] m_pMusicScore;
    protected byte[] m_pSongData;
    protected byte[] m_pTempoData;
    protected int[] m_pTempoList;
    protected int m_numTemposInList;
    protected int[] m_pTempoIndexList;
    protected int[] m_pSongBarOffsetInMSList;
    protected int m_songTimeDivision;
    protected int m_songLengthInQuarterNotes;
    protected int m_songTotalNoteDivisions;
    protected int m_currentTempo;
    protected int m_songPlaybackPositionMS;
    protected int m_musicScoreOffset;
    protected int m_previousMusicScoreOffset;
    protected int m_noteScrollOffset;
    protected int m_currentBarOffset;
    protected string m_strSongTitle;
    protected string m_strSongAsMadeFamousBy;
    protected string m_strSongArtist;
    protected SG_Presenter m_pMusicianSprite;
    protected SG_Presenter[] m_pButtonSprites;
    protected SG_Presenter[] m_pTouchButtonSprites;
    protected SG_Presenter[] m_pButtonOverlaySprites;
    protected SG_Presenter[] m_pButtonStarPowerOverlaySprites;
    protected SG_Presenter[][] m_pNoteSprites;
    protected SG_Presenter[][] m_pStarNoteSprites;
    protected SG_Presenter m_pLeftRail;
    protected SG_Presenter m_pRightRail;
    protected SG_Presenter m_pLeftNoteTrack;
    protected SG_Presenter m_pCenterNoteTrack;
    protected SG_Presenter m_pRightNoteTrack;
    protected SG_Presenter m_pHighwayBackground;
    protected SG_Presenter m_pCrowdSprite;
    protected SG_Presenter m_pStarPowerOverlaySprite;
    protected SG_Presenter m_pStageSprite;
    protected SG_Presenter m_pStarRatingIcon;
    protected int m_starRatingIconWidth;
    protected int m_starRatingIconHeight;
    protected CStarParticle[] m_pStarParticles;
    protected ICRenderSurface m_pTrackBackgroundTexture;
    protected CGuitarHeroGame.eButtonID m_touchButtonHeld;
    protected SG_Presenter m_pStarPowerIcon;
    protected int[] m_pNoteRowVerticalFixedHeight;
    protected int[] m_pNoteRowVerticalFixedOffset;
    protected int[] m_pNoteRowHorizontalFixedWidth;
    protected int[] m_pNoteRowHorizontalFixedOffset;
    protected int[] m_pTrackRowHorizontalFixedWidth;
    protected int[] m_pPixelRowTableReferenceIndex;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldTopLeft;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldTopCenter;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldTopRight;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldBottomLeft;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldBottomCenter;
    protected CGHStaticData.s2dCoord m_guitarPlayFieldBottomRight;
    protected int m_guitarPlayFieldTopWidth;
    protected int m_guitarPlayFieldBottomWidth;
    protected int m_heightOfNoteTrack;
    protected int m_heightOfTrackRails;
    protected CGHStaticData.s2dCoord m_guitarLeftTrackRailPos;
    protected CGHStaticData.s2dCoord m_guitarRightTrackRailPos;
    protected CGHStaticData.s2dCoord[] m_pInstrumentButtonPositions;
    protected CGHStaticData.s2dCoord[] m_pTouchButtonPositions;
    public int m_stageNudge;
    protected CSong m_pCurSong;
    protected CMedia m_pSongAudioTrack;
    protected CMedia m_pInstrumentAudioTrack;
    protected int m_songAudioPlaybackID;
    protected int m_instrumentAudioPlaybackID;
    protected int m_missedSoundMuteTimeMS;
    protected bool m_bFadingDownAudioTrack;
    protected bool m_bFadingUpAudioTrack;
    protected int m_missedSoundVolumePercentage;
    protected int MAX_FADE_VOLUME_PERCENTAGE = 100;
    protected int MIN_FADE_VOLUME_PERCENTAGE;
    protected bool[][] m_bButtonPressed = new bool[4][];
    protected CGuitarHeroGame.eButtonState[] m_buttonState = new CGuitarHeroGame.eButtonState[4];
    protected int[] m_buttonOverlayStateFlags = new int[4];
    protected int m_noteInputPreForgiveness;
    protected int m_noteInputPostForgiveness;
    protected int m_noteDivisionOffsetOfInstrumentButtons;
    protected int m_touchRowTop;
    protected int m_touchRowBottom;
    protected int m_touchButton1Left;
    protected int m_touchButton1Right;
    protected int m_touchButton2Left;
    protected int m_touchButton2Right;
    protected int m_touchButton3Left;
    protected int m_touchButton3Right;
    protected int m_musicianArchetypeID;
    protected int m_musicianCharacterID;
    protected int m_venueArchetypeID;
    protected int m_venueCharacterID;
    protected int m_venueAnimationID;
    protected int m_crowdArchetypeID;
    protected int m_crowdCharacterID;
    protected int m_crowdAnimationID;
    protected int m_highwayBackgroundArchetypeID;
    protected int m_highwayBackgroundCharacterID;
    protected int m_highwayBackgroundAnimationID;
    protected string m_highwayTextureSurfaceID;
    protected bool m_bPlayingDrumsOrBass;
    protected int m_songAudioResourceID;
    protected int m_instrumentAudioResourceID;
    protected int m_tempoResourceID;
    protected int m_songDataResourceID;
    public bool m_touchscreenSupport;
    private int[] NOTE_SPRITE_CHARACTERS = new int[4]
    {
      2,
      1,
      0,
      3
    };
    private int[] BUTTON_SPRITE_CHARACTERS = new int[4]
    {
      2,
      1,
      0,
      2
    };
    private int TOUCH_BUTTON_UP;
    private int TOUCH_BUTTON_DOWN = 1;
    private int[,] TOUCH_BUTTON_ANIMATIONS = new int[3, 2]
    {
      {
        0,
        1
      },
      {
        2,
        3
      },
      {
        4,
        5
      }
    };
    private int MAXIMUM_VENUES = 3;
    private int[] VENUE_SPRITE_ARCHETYPES = new int[3]
    {
      9,
      7,
      8
    };
    private int[] VENUE_SPRITE_CHARACTERS = new int[3];
    private int[] VENUE_SPRITE_ANIMATIONS = new int[3];
    private int[] CROWD_SPRITE_CHARACTERS = new int[3]
    {
      2,
      0,
      1
    };
    private ICRenderSurface green;
    private ICRenderSurface green_active;
    private ICRenderSurface red;
    private ICRenderSurface red_active;
    private ICRenderSurface yellow;
    private ICRenderSurface yellow_active;
    private ICRenderSurface blue_left;
    private ICRenderSurface blue_left_active;
    private ICRenderSurface blue_right;
    private ICRenderSurface blue_right_active;
    private ICRenderSurface blue_middle;
    private ICRenderSurface blue_middle_active;
    private CRectangle buttonRect;
    private CRectangle dummyRect;
    private DateTime timeStart;
    private CRectangle boundsResult;
    private CRectangle touchButtonRect;
    private CRectangle rockerRect;

    public bool IsLoadingGame() => CGHStaticData.m_state == 0 || 1 == CGHStaticData.m_state;

    public bool IsLoadingGameCompleted() => 1 <= CGHStaticData.m_state;

    public CGuitarHeroGame()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1;
      int resource2 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_LEFT", out resource1);
      this.blue_left = (ICRenderSurface) resource1.GetData();
      int resource3 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_LEFT_SELECTED", out resource1);
      this.blue_left_active = (ICRenderSurface) resource1.GetData();
      int resource4 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_RIGHT", out resource1);
      this.blue_right = (ICRenderSurface) resource1.GetData();
      int resource5 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_RIGHT_SELECTED", out resource1);
      this.blue_right_active = (ICRenderSurface) resource1.GetData();
      int resource6 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_MIDDLE", out resource1);
      this.blue_middle = (ICRenderSurface) resource1.GetData();
      int resource7 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_BLUE_MIDDLE_SELECTED", out resource1);
      this.blue_middle_active = (ICRenderSurface) resource1.GetData();
      int resource8 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_GREEN", out resource1);
      this.green = (ICRenderSurface) resource1.GetData();
      int resource9 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_GREEN_ACTIVE", out resource1);
      this.green_active = (ICRenderSurface) resource1.GetData();
      int resource10 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_RED", out resource1);
      this.red = (ICRenderSurface) resource1.GetData();
      int resource11 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_RED_ACTIVE", out resource1);
      this.red_active = (ICRenderSurface) resource1.GetData();
      int resource12 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_YELLOW", out resource1);
      this.yellow = (ICRenderSurface) resource1.GetData();
      int resource13 = (int) resourceManager.CreateResource("SUR_SUSTAIN_NOTE_YELLOW_ACTIVE", out resource1);
      this.yellow_active = (ICRenderSurface) resource1.GetData();
      this.m_CGHStaticData = CGHStaticData.GetInstance();
      CGHStaticData.m_statePriorToPause = 0;
      this.m_bGamePaused = false;
      this.m_pCurSong = (CSong) null;
      this.m_musicianArchetypeID = 0;
      this.m_musicianCharacterID = 0;
      this.m_venueArchetypeID = 0;
      this.m_venueCharacterID = 0;
      this.m_venueAnimationID = 0;
      this.m_highwayBackgroundArchetypeID = 0;
      this.m_highwayBackgroundCharacterID = 0;
      this.m_highwayBackgroundAnimationID = 0;
      this.m_highwayTextureSurfaceID = (string) null;
      this.m_crowdArchetypeID = 0;
      this.m_crowdCharacterID = 0;
      this.m_crowdAnimationID = 0;
      this.m_pInstrumentButtonPositions = (CGHStaticData.s2dCoord[]) null;
      this.m_pTouchButtonPositions = (CGHStaticData.s2dCoord[]) null;
      this.m_pNoteRowVerticalFixedHeight = (int[]) null;
      this.m_pNoteRowVerticalFixedOffset = (int[]) null;
      this.m_pNoteRowHorizontalFixedWidth = (int[]) null;
      this.m_pNoteRowHorizontalFixedOffset = (int[]) null;
      this.m_pTrackRowHorizontalFixedWidth = (int[]) null;
      this.m_pPixelRowTableReferenceIndex = (int[]) null;
      this.m_pSustainNotesPositionInScore = (CGuitarHeroGame.sSustainNoteInfo[]) null;
      this.m_pTempoList = (int[]) null;
      this.m_pTempoIndexList = (int[]) null;
      this.m_pSongBarOffsetInMSList = (int[]) null;
      this.m_pMusicScore = (byte[][]) null;
      this.m_pStarPowersList = (CGuitarHeroGame.sStarPower[]) null;
      this.m_pStarParticles = (CStarParticle[]) null;
      this.m_pMusicianSprite = (SG_Presenter) null;
      this.m_pLeftRail = (SG_Presenter) null;
      this.m_pRightRail = (SG_Presenter) null;
      this.m_pLeftNoteTrack = (SG_Presenter) null;
      this.m_pCenterNoteTrack = (SG_Presenter) null;
      this.m_pRightNoteTrack = (SG_Presenter) null;
      this.m_pHighwayBackground = (SG_Presenter) null;
      this.m_pCrowdSprite = (SG_Presenter) null;
      this.m_pStarPowerOverlaySprite = (SG_Presenter) null;
      this.m_pStageSprite = (SG_Presenter) null;
      this.m_pStarRatingIcon = (SG_Presenter) null;
      this.m_pNoteSprites = (SG_Presenter[][]) null;
      this.m_pStarNoteSprites = (SG_Presenter[][]) null;
      this.m_pButtonSprites = (SG_Presenter[]) null;
      this.m_pTouchButtonSprites = (SG_Presenter[]) null;
      this.m_pButtonOverlaySprites = (SG_Presenter[]) null;
      this.m_pButtonStarPowerOverlaySprites = (SG_Presenter[]) null;
      this.m_pStarPowerIcon = (SG_Presenter) null;
      this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_INVALID;
      this.m_stageNudge = 0;
      this.m_touchscreenSupport = true;
      this.pHud = (CHud) null;
      this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_LOADING);
    }

    public override uint Start()
    {
      string.Format("({0}/{1})", (object) 1, (object) 2);
      return base.Start();
    }

    public override void Stop() => base.Stop();

    public override void Activate()
    {
      base.Activate();
      if (!this.m_bGamePaused)
        return;
      if (CGHStaticData.m_statePriorToPause != 2 && CGHStaticData.m_statePriorToPause != 0 && CGHStaticData.m_statePriorToPause != 1)
      {
        int statePriorToPause = CGHStaticData.m_statePriorToPause;
      }
      this.m_bGamePaused = false;
    }

    public override void Deactivate()
    {
      base.Deactivate();
      this.m_bGamePaused = true;
      if (5 == CGHStaticData.m_state)
        return;
      CGHStaticData.m_statePriorToPause = CGHStaticData.m_state;
    }

    private void CGuitarHeroGameDestructor()
    {
      this.UnloadHighwayTrackTexture();
      this.UnloadIngameSprites();
      this.UnloadSongMedia();
      this.UnloadSongData();
      this.UnloadGameSounds();
      this.FreeIngameVariables();
      this.pHud = (CHud) null;
    }

    public bool GamePlayFinished()
    {
      bool flag = 7 == CGHStaticData.m_state || 6 == CGHStaticData.m_state;
      if (flag)
      {
        ICMediaPlayer.GetInstance().Stop();
        ICMediaPlayer.GetInstance().StopVibrate();
      }
      return flag;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 94257030:
          if (4 == CGHStaticData.m_state)
          {
            this.ActivateStarPower();
            break;
          }
          break;
        case 850690755:
          this.m_pRightRail.Bounds(ref this.dummyRect);
          this.m_pLeftNoteTrack.Bounds(ref this.dummyRect);
          this.m_pCenterNoteTrack.Bounds(ref this.dummyRect);
          this.m_pRightNoteTrack.Bounds(ref this.dummyRect);
          this.m_pStageSprite.Bounds(ref this.dummyRect);
          this.m_pStarRatingIcon.Bounds(ref this.dummyRect);
          uint width;
          uint height;
          ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
          CGHStaticData.m_guitarTrackOriginPos.m_x = (int) (width >> 1);
          CGHStaticData.m_guitarTrackOriginPos.m_y = (int) height;
          this.pHud.RecalculatePositions();
          this.InitializeLayoutCoordinates();
          if (CGHStaticData.m_state == 4 || CGHStaticData.m_state == 5 || CGHStaticData.m_state == 2)
          {
            CGHStaticData.m_statePriorToPause = 4;
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_RESUME_COUNTDOWN);
            break;
          }
          break;
        case 902008092:
          this.pointerReleased(this.m_lastPointerX, this.m_lastPointerY);
          this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_INVALID;
          this.m_moveWithPointer = false;
          this.m_lastPointerX = 0;
          this.m_lastPointerY = 0;
          if (this.m_touchscreenSupport)
          {
            CRectangle r = new CRectangle(this.m_realPointerX, this.m_realPointerY, 1, 1);
            this.m_pStarPowerIcon.Bounds(ref this.buttonRect);
            if (this.buttonRect.Contains(r))
            {
              this.HandleEvent(94257030U, 0U, (object) 0);
              break;
            }
            break;
          }
          break;
        case 902053462:
          int x1 = TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
          int y1 = TouchUtil.TOUCH_EVENT_GET_Y((uint) param2);
          this.pointerPressed(x1, y1);
          this.m_moveWithPointer = true;
          this.m_lastPointerX = x1;
          this.m_lastPointerY = y1;
          break;
        case 902532892:
          if (this.m_moveWithPointer)
          {
            int x2 = TouchUtil.TOUCH_EVENT_GET_X((uint) param2);
            int y2 = TouchUtil.TOUCH_EVENT_GET_Y((uint) param2);
            this.pointerDragged(x2, y2);
            this.m_lastPointerX = x2;
            this.m_lastPointerY = y2;
            break;
          }
          break;
        case 1386813809:
          this.pointerReleased(this.m_lastPointerX, this.m_lastPointerY);
          this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_INVALID;
          this.m_moveWithPointer = false;
          this.m_lastPointerX = 0;
          this.m_lastPointerY = 0;
          if (this.m_touchscreenSupport)
          {
            CRectangle r = new CRectangle(this.m_realPointerX, this.m_realPointerY, 1, 1);
            this.m_pStarPowerIcon.Bounds(ref this.buttonRect);
            if (this.buttonRect.Contains(r))
            {
              this.HandleEvent(94257030U, 0U, (object) 0);
              break;
            }
            break;
          }
          break;
        case 2186393822:
          if (this.m_moveWithPointer)
          {
            int x3 = MouseUtil.MOUSE_EVENT_GET_X((uint) param2);
            int y3 = MouseUtil.MOUSE_EVENT_GET_Y((uint) param2);
            this.pointerDragged(x3, y3);
            this.m_lastPointerX = x3;
            this.m_lastPointerY = y3;
            break;
          }
          break;
        case 2300082508:
          int x4 = MouseUtil.MOUSE_EVENT_GET_X((uint) param2);
          int y4 = MouseUtil.MOUSE_EVENT_GET_Y((uint) param2);
          this.pointerPressed(x4, y4);
          this.m_moveWithPointer = true;
          this.m_lastPointerX = x4;
          this.m_lastPointerY = y4;
          break;
      }
      return flag;
    }

    public void LoadGame()
    {
      CSongListMgr.GetSongSelected().LoadSongMedia((Consts.eInstrument) CGHStaticData.m_instrumentBeingPlayed, (Consts.eDifficulty) CGHStaticData.m_difficultyLevel);
      CSongListMgr.ReadSong((CInputStream) null);
      this.InitIngameVariables();
      this.InitializeDefaultLayoutCoordinates();
      this.LoadHighwayTrackTexture();
      lock (CGameApp.loadQueuedLock)
      {
        this.QueueIngameSprites();
        do
          ;
        while (!this.LoadIngameSprites());
      }
      this.SetupIngameSprites();
      this.LoadSongMedia();
      this.LoadSongData();
      this.LoadSongInformation();
      this.InitializeLayoutCoordinates();
      this.ResetSongData();
      this.pHud = new CHud();
      this.pHud.init();
      this.LoadGameSounds();
      this.m_renderState = CGuitarHeroGame.eRenderState.RM_COLORKEYTEST_OR_ALPHATEST_NOBLENDING;
      this.m_constAlpha = CMathFixed.FloatToFixed(0.5f);
      this.loadingDone = true;
      this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_LOADING_COMPLETE);
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      CGHStaticData.m_timer -= timeElapsedMS;
      this.setPointer();
      switch (CGHStaticData.m_state)
      {
        case 0:
          if (this.loadingDone)
          {
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_LOADING_COMPLETE);
            this.loadingDone = false;
            break;
          }
          break;
        case 1:
          CGHStaticData.m_statePriorToPause = 1;
          break;
        case 2:
          if (CGHStaticData.m_timer < 0)
          {
            this.pHud.animate_on(0, 750);
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_ANIMATE_HUD_ON);
            break;
          }
          break;
        case 3:
          if (this.pHud.animate_on(-CGHStaticData.m_timer, 750))
          {
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_PLAY);
            break;
          }
          break;
        case 5:
          if (CGHStaticData.m_timer < 0)
          {
            ICMediaPlayer.GetInstance().Resume();
            ICMediaPlayer.GetInstance().ResumeVibrate();
            this.HandleSongAudioResume();
            this.NewState((CGuitarHeroGame.eGameState) CGHStaticData.m_statePriorToPause);
            break;
          }
          break;
        case 10:
          this.HandleAnimateStarPowerOverlay(timeElapsedMS);
          if (CGHStaticData.m_timer < 0)
          {
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_COMPLETE);
            break;
          }
          break;
        case 12:
          if (CGHStaticData.m_timer < 0)
          {
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_YOU_ROCK);
            break;
          }
          break;
        default:
          this.HandleSongAudioUpdate(timeElapsedMS);
          this.HandleMuteAudioTrackUpdate(timeElapsedMS);
          int musicScoreScrollInNoteDivisions = this.HandleNotesScroll(timeElapsedMS);
          this.HandleMissedNotes();
          this.HandleStarPowerUpdate(musicScoreScrollInNoteDivisions);
          this.HandleButtonsUpdate(timeElapsedMS);
          this.HandleAnimateStarNotes(timeElapsedMS);
          this.HandleAnimateButtonOverlays(timeElapsedMS);
          this.HandleAnimateMusician(timeElapsedMS);
          this.HandleAnimateCrowd(timeElapsedMS);
          this.HandleAnimateTrackRails(timeElapsedMS);
          if (!CGameApp.GetInstance().IsVenueAnimationDisabled())
            this.HandleAnimateVenue(timeElapsedMS);
          this.HandleAnimateStarPowerOverlay(timeElapsedMS);
          this.HandleAnimateStarParticles(timeElapsedMS);
          this.pHud.tick(timeElapsedMS);
          if (1 != CGHStaticData.m_gameplayMode && this.pHud.RockMeterIsEmpty())
            CGHStaticData.m_songState = 2;
          if (1 == CGHStaticData.m_songState)
          {
            this.CalculateSongFinalStatistics();
            if (1 != CGHStaticData.m_gameplayMode && !CGHStaticData.m_bAchievementsDisabled)
            {
              if (!CDemoMgr.IsDemo())
                this.UpdateLeaderboards();
              this.UpdateUnlockedAchievements();
            }
            this.m_pStarPowerOverlaySprite.SetAnimation(1, true);
            this.pHud.ClearMessage();
            if (CDemoMgr.IsDemo() && CGHStaticData.m_bAchievementInDemoMode)
            {
              this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_DEMO_ACHIEVEMENT);
              break;
            }
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_YOU_ROCK);
            break;
          }
          if (2 == CGHStaticData.m_songState)
          {
            this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_GAMEOVER);
            break;
          }
          break;
      }
      this.m_gameInput = 0;
      return true;
    }

    public override bool HandleRender()
    {
      if (CGHStaticData.m_state == 0 || 1 == CGHStaticData.m_state)
        return false;
      ICGraphics instance1 = ICGraphics.GetInstance();
      ICGraphics2d instance2 = ICGraphics2d.GetInstance();
      instance1.GetTargetSurface();
      this.RenderBegin();
      CDrawUtil.FillRect(0, 0, 480, 320, 4278190080U);
      instance1.SetClearColor(new Color.ARGB_fixed((int) byte.MaxValue, 0, 0, 0));
      instance2.SetColor(Consts.Color_MakeA8R8G8B8(85, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      instance2.Enable(ICGraphics2d.State.ConfigStateBasedOnSrcFormat);
      this.RenderGame();
      this.RenderEnd();
      return true;
    }

    private void HandleSongAudioUpdate(int timeElapsedMS)
    {
      if (!ICMediaPlayer.GetInstance().IsPlaying((uint) this.m_songAudioPlaybackID))
      {
        double num = (DateTime.Now - this.timeStart).TotalMilliseconds + 1.0;
      }
      if (!ICMediaPlayer.GetInstance().GetSoundEnabled())
      {
        CGHStaticData.m_bPlayingSong = false;
        CGHStaticData.m_bPlayingInstrument = false;
        if (this.m_songAudioPlaybackID != 0)
        {
          ICMediaPlayer.GetInstance().Stop((uint) this.m_songAudioPlaybackID);
          this.m_songAudioPlaybackID = 0;
        }
        if (this.m_instrumentAudioPlaybackID == 0)
          return;
        ICMediaPlayer.GetInstance().Stop((uint) this.m_instrumentAudioPlaybackID);
        this.m_instrumentAudioPlaybackID = 0;
      }
      else
      {
        if (CGHStaticData.m_bPlayingSong || !ICMediaPlayer.GetInstance().GetSoundEnabled())
          return;
        if (!CGameApp.GetInstance().musicWasPlayingAtLaunch)
        {
          this.timeStart = DateTime.Now;
          try
          {
            this.m_songAudioPlaybackID = (int) ICMediaPlayer.GetInstance().Play(this.m_pSongAudioTrack, (byte) 0, (byte) 0);
          }
          catch (UnauthorizedAccessException ex)
          {
            CGuitarHeroGame.m_zunePlaying = true;
          }
          if (this.m_pInstrumentAudioTrack != null)
            this.m_instrumentAudioPlaybackID = (int) ICMediaPlayer.GetInstance().Play(this.m_pInstrumentAudioTrack, (byte) 0, (byte) 0);
        }
        CGHStaticData.m_bPlayingSong = true;
        CGHStaticData.m_bPlayingInstrument = true;
      }
    }

    private void HandleSongAudioResume()
    {
      if (CGHStaticData.m_bPlayingSong && !CGHStaticData.m_bResumeAudioFromSuspend)
        return;
      if (ICMediaPlayer.GetInstance().GetSoundEnabled())
      {
        this.m_songAudioPlaybackID = (int) ICMediaPlayer.GetInstance().Play(this.m_pSongAudioTrack, (byte) 0, (byte) 0);
        if (this.m_pInstrumentAudioTrack != null)
          this.m_instrumentAudioPlaybackID = (int) ICMediaPlayer.GetInstance().Play(this.m_pInstrumentAudioTrack, (byte) 0, (byte) 0);
        CGHStaticData.m_bPlayingSong = true;
        CGHStaticData.m_bPlayingInstrument = true;
      }
      CGHStaticData.m_bResumeAudioFromSuspend = false;
    }

    private int HandleNotesScroll(int timeElapsedMS)
    {
      int musicScoreOffset = this.m_musicScoreOffset;
      if (ICCore.GetInstance().IsSoundEnabled() && ICMediaPlayer.GetInstance().GetSoundEnabled())
      {
        int totalMilliseconds = !ICMediaPlayer.GetInstance().IsPlaying((uint) this.m_instrumentAudioPlaybackID) || !MediaPlayer.GameHasControl ? 0 : (int) MediaPlayer.PlayPosition.TotalMilliseconds;
        if (totalMilliseconds > 0)
        {
          if (this.m_songPlaybackPositionMS + timeElapsedMS + CGHStaticData.m_audioSynchTweakAdjustmentMS > totalMilliseconds)
            return 0;
          if (this.m_songPlaybackPositionMS + timeElapsedMS + CGHStaticData.m_audioSynchTweakAdjustmentMS < totalMilliseconds)
            timeElapsedMS += totalMilliseconds - (this.m_songPlaybackPositionMS + timeElapsedMS + CGHStaticData.m_audioSynchTweakAdjustmentMS);
        }
      }
      this.m_songPlaybackPositionMS += timeElapsedMS;
      if (this.m_songPlaybackPositionMS < 0)
        return 0;
      while (this.m_songPlaybackPositionMS >= this.m_pSongBarOffsetInMSList[this.m_currentBarOffset] && this.m_currentBarOffset < this.m_songLengthInQuarterNotes)
        ++this.m_currentBarOffset;
      --this.m_currentBarOffset;
      int num1 = this.m_songPlaybackPositionMS - this.m_pSongBarOffsetInMSList[this.m_currentBarOffset];
      this.m_currentTempo = this.m_pTempoList[this.m_pTempoIndexList[this.m_currentBarOffset]];
      int v = num1 * 48;
      int num2 = 1;
      for (; v >= 32768; v >>= 1)
        num2 *= 2;
      this.m_noteScrollOffset = CMathFixed.Div(CMathFixed.Int32ToFixed(v), CMathFixed.Int32ToFixed(this.m_currentTempo / (1000 * num2)));
      this.m_musicScoreOffset = this.m_currentBarOffset * 48;
      while (this.m_noteScrollOffset >= 65536)
      {
        this.m_noteScrollOffset -= 65536;
        if (this.m_musicScoreOffset < (this.m_songLengthInQuarterNotes - 4) * 48)
        {
          ++this.m_musicScoreOffset;
          this.UpdateScoreMultiplierStatistics();
        }
        else
        {
          this.m_musicScoreOffset = (this.m_songLengthInQuarterNotes - 4) * 48;
          CGHStaticData.m_songState = 1;
        }
      }
      return this.m_musicScoreOffset - musicScoreOffset;
    }

    private bool NotePriorToMusicalScoreOffsetIsRemovedForSingleNoteHandsets(
      int noteID,
      int musicScoreOffset)
    {
      while (1 != ((int) this.m_pMusicScore[noteID][musicScoreOffset] & 3) && musicScoreOffset > 0)
        --musicScoreOffset;
      bool singleNoteHandsets = false;
      if (noteID != 3)
      {
        for (int index = noteID - 1; index >= 0; --index)
        {
          if (1 == ((int) this.m_pMusicScore[index][musicScoreOffset] & 3))
            singleNoteHandsets = true;
        }
      }
      return singleNoteHandsets;
    }

    private bool HasNotePriorToMusicalScoreOffsetBeenPlayed(int noteID, int musicScoreOffset)
    {
      while (1 != ((int) this.m_pMusicScore[noteID][musicScoreOffset] & 3) && musicScoreOffset > 0)
        --musicScoreOffset;
      return 4 == ((int) this.m_pMusicScore[noteID][musicScoreOffset] & 4) && 8 != ((int) this.m_pMusicScore[noteID][musicScoreOffset] & 8) || 128 == ((int) this.m_pMusicScore[noteID][musicScoreOffset] & 128);
    }

    private bool HandlePlayNoteAndUpdateOverlays(
      int buttonID,
      int musicScoreOffset,
      bool bSingleKeyPressHandsetCloneInput)
    {
      CGuitarHeroGame.eNotePlayedState eNotePlayedState = this.HandlePlayNoteAtMusicalScoreOffset(buttonID, musicScoreOffset, bSingleKeyPressHandsetCloneInput);
      bool flag;
      if (eNotePlayedState != CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_NONE)
      {
        if (!CGHStaticData.m_bSingleNoteDisplayHandset || !bSingleKeyPressHandsetCloneInput)
        {
          if (3 == buttonID)
          {
            this.m_pButtonOverlaySprites[buttonID].SetAnimation(8);
          }
          else
          {
            this.m_pButtonOverlaySprites[buttonID].SetAnimation(eNotePlayedState == CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_SUSTAIN ? 9 : 2);
            if (eNotePlayedState == CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_SUSTAIN)
            {
              for (int index = 0; index <= 3; ++index)
              {
                if (index != buttonID && 2 == (this.m_buttonOverlayStateFlags[index] & 2))
                {
                  this.m_pButtonOverlaySprites[buttonID].SynchAnimations(this.m_pButtonOverlaySprites[index]);
                  break;
                }
              }
            }
          }
          this.m_buttonOverlayStateFlags[buttonID] &= -4;
          this.m_buttonOverlayStateFlags[buttonID] |= eNotePlayedState == CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_SUSTAIN ? 2 : 1;
        }
        flag = true;
      }
      else
        flag = false;
      return flag;
    }

    private CGuitarHeroGame.eNotePlayedState HandlePlayNoteAtMusicalScoreOffset(
      int noteID,
      int musicScoreOffset,
      bool bSingleKeyPressHandsetCloneInput)
    {
      Debug.WriteLine(noteID);
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      int musicScoreOffset1 = 0;
      int num1 = this.m_noteInputPostForgiveness;
      if (bSingleKeyPressHandsetCloneInput)
        num1 = 0;
      for (; !flag3 && num1 >= 0; --num1)
      {
        if (musicScoreOffset - num1 >= 0 && this.m_pMusicScore[noteID][musicScoreOffset - num1] != (byte) 0 && (!CGHStaticData.m_bSingleNoteDisplayHandset || bSingleKeyPressHandsetCloneInput || !this.NotePriorToMusicalScoreOffsetIsRemovedForSingleNoteHandsets(noteID, musicScoreOffset - num1)) && !this.HasNotePriorToMusicalScoreOffsetBeenPlayed(noteID, musicScoreOffset - num1))
        {
          flag3 = true;
          musicScoreOffset1 = musicScoreOffset - num1;
          if (!bSingleKeyPressHandsetCloneInput)
            CGHStaticData.m_lastNonClonedHitNoteMusicScoreOffset = musicScoreOffset1;
        }
      }
      if (!bSingleKeyPressHandsetCloneInput)
      {
        int num2 = 1;
        if (!CGameApp.autoplay)
        {
          for (; !flag3 && num2 <= this.m_noteInputPreForgiveness; ++num2)
          {
            if (this.m_pMusicScore[noteID][musicScoreOffset + num2] != (byte) 0 && (!CGHStaticData.m_bSingleNoteDisplayHandset || bSingleKeyPressHandsetCloneInput || !this.NotePriorToMusicalScoreOffsetIsRemovedForSingleNoteHandsets(noteID, musicScoreOffset + num2)) && !this.HasNotePriorToMusicalScoreOffsetBeenPlayed(noteID, musicScoreOffset + num2))
            {
              flag3 = true;
              musicScoreOffset1 = musicScoreOffset + num2;
              if (!bSingleKeyPressHandsetCloneInput)
                CGHStaticData.m_lastNonClonedHitNoteMusicScoreOffset = musicScoreOffset1;
            }
          }
        }
      }
      if (flag3)
      {
        while (1 != ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 3) && musicScoreOffset1 > 0)
          --musicScoreOffset1;
        if (128 != ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 128))
        {
          this.m_pMusicScore[noteID][musicScoreOffset1] |= (byte) 128;
          if (8 == ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 8))
          {
            flag2 = true;
            int sustainNoteLength = 0;
            int index = musicScoreOffset1;
            while (((int) this.m_pMusicScore[noteID][index] & 4) != 0)
            {
              ++index;
              ++sustainNoteLength;
              if (1 == ((int) this.m_pMusicScore[noteID][index] & 1))
                break;
            }
            this.SetPlayingSustainNoteInformation(noteID, musicScoreOffset1, sustainNoteLength);
            if (CGHStaticData.m_bSingleKeypressHandset)
            {
              for (int noteID1 = 0; noteID1 <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++noteID1)
              {
                if (noteID != noteID1)
                  this.SetPlayingSustainNoteInformation(noteID1, musicScoreOffset1, sustainNoteLength);
              }
            }
          }
          if (64 == ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 64))
          {
            ++this.m_totalConsecutiveStarPowerNotesHit;
            if (16 == ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 48))
              this.ActivateStarPowerPhraseCompletedEffect(noteID);
          }
          if (4 != ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 4) || 8 == ((int) this.m_pMusicScore[noteID][musicScoreOffset1] & 8))
          {
            flag1 = true;
            for (int index = 0; index < 4; ++index)
            {
              if (noteID != index && 1 == ((int) this.m_pMusicScore[index][musicScoreOffset1] & 3) && 128 != ((int) this.m_pMusicScore[index][musicScoreOffset1] & 128))
                flag4 = true;
            }
          }
        }
      }
      else if (!CGameApp.autoplay && !bSingleKeyPressHandsetCloneInput)
      {
        ++CGHStaticData.m_badNoteCount;
        this.ClearStarPowerAtMusicScoreOffset(musicScoreOffset);
        this.ClearConsecutiveHitNotes();
        this.pHud.updateHud(false);
        this.MuteAudioTrack(800);
        int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMissedNoteSFX);
      }
      if (flag1)
      {
        ++CGHStaticData.m_hitNoteCount;
        if (!flag4)
        {
          ++CGHStaticData.m_hitNoteGroupCount;
          this.IncrementConsecutiveHitNotes();
        }
        this.UpdateScoreForPlayingNote();
        this.pHud.updateHud(true);
        this.UnMuteAudioTrack();
      }
      if (!flag1)
        return CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_NONE;
      return !flag2 ? CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_NORMAL : CGuitarHeroGame.eNotePlayedState.NOTE_PLAYED_SUSTAIN;
    }

    private void HandleMissedNotes()
    {
      int num1 = this.m_musicScoreOffset - (this.m_noteInputPostForgiveness + 1);
      int musicScoreOffset = this.m_previousMusicScoreOffset - this.m_noteInputPostForgiveness;
      if (musicScoreOffset < 0)
        musicScoreOffset = 0;
      int num2 = 0;
      for (; musicScoreOffset <= num1; ++musicScoreOffset)
      {
        bool flag = false;
        for (int noteID = 0; noteID < (this.m_bPlayingDrumsOrBass ? 4 : 3); ++noteID)
        {
          byte num3 = this.m_pMusicScore[noteID][musicScoreOffset];
          if (1 == ((int) num3 & 3) && 128 != ((int) num3 & 128))
          {
            //TEMP / DEBUG
            //++CGHStaticData.m_missedNoteCount;
            //flag = true;
            
            this.MuteAudioTrack(800);
            this.ClearConsecutiveHitNotes();
            if (64 == ((int) num3 & 64))
              this.ClearStarPowerAtMusicScoreOffset(musicScoreOffset);
            if (8 == ((int) num3 & 8))
              this.ClearNoteSustainAtMusicScoreOffset((CGuitarHeroGame.eNoteID) noteID, musicScoreOffset, false);
            this.pHud.updateHud(false);
          }
          int num4 = (int) this.m_pMusicScore[noteID][musicScoreOffset];
          if ((num4 & 3) != 0 && 8 == (num4 & 8))
          {
            this.m_pMusicScore[noteID][musicScoreOffset] &= (byte) 247;
            ++num2;
          }
        }
        if (flag)
        {
            //++CGHStaticData.m_missedNoteGroupCount;
        }
      }
      if (num2 == 0)
      {
        CGHStaticData.m_sustainScoreFractionalRemainder = 0;
      }
      else
      {
        int num5 = CGHStaticData.m_sustainScoreFractionalRemainder + num2 * 50 * CGHStaticData.m_scoreMultiplier * (CGHStaticData.m_bStarPowerScoringActivated ? 2 : 1);
        int num6 = 96;
        int num7 = num5 / num6;
        CGHStaticData.m_sustainScoreFractionalRemainder = num5 % num6;
        CGHStaticData.m_score += num7;
      }
      this.m_previousMusicScoreOffset = this.m_musicScoreOffset;
    }

    private void HandleButtonStateUpdate(CGuitarHeroGame.eButtonID buttonID, bool bButtonPressed)
    {
      switch (this.m_buttonState[(int) buttonID])
      {
        case CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED:
          if (bButtonPressed)
          {
            this.m_buttonState[(int) buttonID] = CGuitarHeroGame.eButtonState.BUTTON_STATE_PRESSED;
            break;
          }
          break;
        case CGuitarHeroGame.eButtonState.BUTTON_STATE_PRESSED:
          if (bButtonPressed)
          {
            this.m_buttonState[(int) buttonID] = CGuitarHeroGame.eButtonState.BUTTON_STATE_HELD;
            break;
          }
          this.m_buttonState[(int) buttonID] = CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED;
          this.ClearNoteSustainAtMusicScoreOffset((CGuitarHeroGame.eNoteID) buttonID, this.m_musicScoreOffset, true);
          if (CGHStaticData.m_bSingleKeypressHandset)
          {
            for (int noteID = 0; noteID <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++noteID)
            {
              if (buttonID != (CGuitarHeroGame.eButtonID) noteID)
                this.ClearNoteSustainAtMusicScoreOffset((CGuitarHeroGame.eNoteID) noteID, this.m_musicScoreOffset, true);
            }
            break;
          }
          break;
        case CGuitarHeroGame.eButtonState.BUTTON_STATE_HELD:
          if (!bButtonPressed)
          {
            this.m_buttonState[(int) buttonID] = CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED;
            this.ClearNoteSustainAtMusicScoreOffset((CGuitarHeroGame.eNoteID) buttonID, this.m_musicScoreOffset, true);
            if (CGHStaticData.m_bSingleKeypressHandset)
            {
              for (int noteID = 0; noteID <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++noteID)
              {
                if (buttonID != (CGuitarHeroGame.eButtonID) noteID)
                  this.ClearNoteSustainAtMusicScoreOffset((CGuitarHeroGame.eNoteID) noteID, this.m_musicScoreOffset, true);
              }
              break;
            }
            break;
          }
          break;
      }
      if (CGuitarHeroGame.eButtonState.BUTTON_STATE_PRESSED != this.m_buttonState[(int) buttonID] && !CGameApp.autoplay || !this.HandlePlayNoteAndUpdateOverlays((int) buttonID, this.m_musicScoreOffset, false) || !CGHStaticData.m_bSingleKeypressHandset)
        return;
      for (int buttonID1 = 0; buttonID1 <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++buttonID1)
      {
        if (buttonID != (CGuitarHeroGame.eButtonID) buttonID1)
          this.HandlePlayNoteAndUpdateOverlays(buttonID1, CGHStaticData.m_lastNonClonedHitNoteMusicScoreOffset, true);
      }
    }

    private void HandleButtonsUpdate(int timeElapsedMS)
    {
      bool bButtonPressed1 = false;
      bool bButtonPressed2 = false;
      bool bButtonPressed3 = false;
      bool bButtonPressed4 = false;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool[] flagArray = new bool[3];
      bool flag5 = false;
      if (this.m_touchscreenSupport && (this.m_bPointerActive || this.m_bJustReleaseRecieved) && this.m_pointerY > this.m_touchRowTop && this.m_pointerY < this.m_touchRowBottom && this.m_gameInput == 0)
      {
        if (this.m_pointerX > this.m_touchButton1Left && this.m_pointerX < this.m_touchButton1Right)
          flag5 = bButtonPressed1 = true;
        if (this.m_pointerX > this.m_touchButton2Left && this.m_pointerX < this.m_touchButton2Right)
          flag5 = bButtonPressed2 = true;
        if (this.m_pointerX > this.m_touchButton3Left && this.m_pointerX < this.m_touchButton3Right)
          flag5 = bButtonPressed3 = true;
        if (this.m_pointerX > this.m_touchButton1Left && this.m_pointerX < this.m_touchButton3Right)
          flag5 = bButtonPressed4 = true;
        flagArray[0] = bButtonPressed1;
        flagArray[1] = bButtonPressed2;
        flagArray[2] = bButtonPressed3;
        this.m_bJustReleaseRecieved = false;
      }
      bool flag6 = 0 != (this.m_buttonOverlayStateFlags[3] & 3);
      if (CGHStaticData.m_bSingleKeypressHandset && !CGHStaticData.m_bSingleNoteDisplayHandset)
      {
        flag1 = 0 != (this.m_buttonOverlayStateFlags[0] & 3);
        flag2 = 0 != (this.m_buttonOverlayStateFlags[1] & 3);
        flag3 = 0 != (this.m_buttonOverlayStateFlags[2] & 3);
        flag4 = 0 != (this.m_buttonOverlayStateFlags[3] & 3);
      }
      if (this.m_bPlayingDrumsOrBass)
      {
        if (this.m_touchscreenSupport && this.m_gameInput == 0)
        {
          switch (this.m_touchButtonHeld)
          {
            case CGuitarHeroGame.eButtonID.BUTTON_1:
              int num1;
              bButtonPressed4 = (num1 = 0) != 0;
              bButtonPressed3 = num1 != 0;
              bButtonPressed2 = num1 != 0;
              break;
            case CGuitarHeroGame.eButtonID.BUTTON_2:
              int num2;
              bButtonPressed4 = (num2 = 0) != 0;
              bButtonPressed3 = num2 != 0;
              bButtonPressed1 = num2 != 0;
              break;
            case CGuitarHeroGame.eButtonID.BUTTON_3:
              int num3;
              bButtonPressed4 = (num3 = 0) != 0;
              bButtonPressed2 = num3 != 0;
              bButtonPressed1 = num3 != 0;
              break;
            case CGuitarHeroGame.eButtonID.BUTTON_4:
              int num4;
              bButtonPressed3 = (num4 = 0) != 0;
              bButtonPressed2 = num4 != 0;
              bButtonPressed1 = num4 != 0;
              break;
            default:
              if (flag5)
              {
                bool flag7 = false;
                if (bButtonPressed1)
                  flag7 |= this.IsNotePlayable(CGuitarHeroGame.eButtonID.BUTTON_1);
                if (bButtonPressed2)
                  flag7 |= this.IsNotePlayable(CGuitarHeroGame.eButtonID.BUTTON_2);
                if (bButtonPressed3)
                  flag7 |= this.IsNotePlayable(CGuitarHeroGame.eButtonID.BUTTON_3);
                if (bButtonPressed4)
                {
                  if (!bButtonPressed1 && !bButtonPressed2 && !bButtonPressed3)
                    bButtonPressed4 = true;
                  else if (!this.IsNotePlayable(CGuitarHeroGame.eButtonID.BUTTON_4) && flag7)
                    bButtonPressed4 = false;
                  else if (bButtonPressed1)
                    bButtonPressed4 = this.IsFourthNoteNearest(CGuitarHeroGame.eButtonID.BUTTON_1);
                  else if (bButtonPressed2)
                    bButtonPressed4 = this.IsFourthNoteNearest(CGuitarHeroGame.eButtonID.BUTTON_2);
                  else if (bButtonPressed3)
                    bButtonPressed4 = this.IsFourthNoteNearest(CGuitarHeroGame.eButtonID.BUTTON_3);
                  if (bButtonPressed4)
                  {
                    int num5;
                    bButtonPressed3 = (num5 = 0) != 0;
                    bButtonPressed2 = num5 != 0;
                    bButtonPressed1 = num5 != 0;
                  }
                }
                if (bButtonPressed1)
                {
                  this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_1;
                  break;
                }
                if (bButtonPressed2)
                {
                  this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_2;
                  break;
                }
                if (bButtonPressed3)
                {
                  this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_3;
                  break;
                }
                if (bButtonPressed4)
                {
                  this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_4;
                  break;
                }
                break;
              }
              break;
          }
        }
        if (CGuitarHeroGame.eButtonState.BUTTON_STATE_PRESSED == this.m_buttonState[3] || flag4)
          this.m_pButtonSprites[3].SetAnimation(7);
        this.m_pButtonSprites[3].Update(timeElapsedMS);
        this.HandleButtonStateUpdate(CGuitarHeroGame.eButtonID.BUTTON_4, bButtonPressed4);
      }
      this.m_pButtonSprites[0].SetAnimation(bButtonPressed1 || flag6 || flag1 ? 1 : 0);
      this.m_pButtonSprites[1].SetAnimation(bButtonPressed2 || flag6 || flag2 ? 1 : 0);
      this.m_pButtonSprites[2].SetAnimation(bButtonPressed3 || flag6 || flag3 ? 1 : 0);
      for (int index = 0; index < 3; ++index)
        this.m_pTouchButtonSprites[index].SetAnimation(this.TOUCH_BUTTON_ANIMATIONS[index, flagArray[index] ? 1 : 0]);
      this.HandleButtonStateUpdate(CGuitarHeroGame.eButtonID.BUTTON_1, bButtonPressed1);
      this.HandleButtonStateUpdate(CGuitarHeroGame.eButtonID.BUTTON_2, bButtonPressed2);
      this.HandleButtonStateUpdate(CGuitarHeroGame.eButtonID.BUTTON_3, bButtonPressed3);
    }

    private void HandleAnimateStarNotes(int timeElapsedMS)
    {
      for (int index1 = 0; index1 < 4; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
          this.m_pStarNoteSprites[index1][index2].Update(timeElapsedMS);
      }
    }

    private void HandleAnimateButtonOverlays(int timeElapsedMS)
    {
      for (int index1 = 0; index1 <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++index1)
      {
        if ((this.m_buttonOverlayStateFlags[index1] & 3) != 0)
        {
          bool flag1 = this.m_pButtonOverlaySprites[index1].Update(timeElapsedMS);
          bool flag2 = false;
          if (2 == (this.m_buttonOverlayStateFlags[index1] & 3))
          {
            if (this.m_buttonState[index1] != CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED)
            {
              if (flag1)
              {
                flag2 = true;
                flag1 = false;
              }
              if (this.m_pSustainNotesPositionInScore[index1].m_musicScoreOffset <= this.m_musicScoreOffset && this.m_pSustainNotesPositionInScore[index1].m_musicScoreOffset + this.m_pSustainNotesPositionInScore[index1].m_sustainLength - 1 <= this.m_musicScoreOffset)
                flag1 = true;
            }
            else
            {
              if (CGHStaticData.m_bSingleKeypressHandset && !CGHStaticData.m_bSingleNoteDisplayHandset)
              {
                for (int index2 = 0; index2 <= (this.m_bPlayingDrumsOrBass ? 3 : 2); ++index2)
                {
                  if (index2 != index1 && this.m_buttonState[index2] != CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED && 2 == (this.m_buttonOverlayStateFlags[index2] & 3))
                  {
                    if (flag1)
                    {
                      flag2 = true;
                      flag1 = false;
                    }
                    if (this.m_pSustainNotesPositionInScore[index2].m_musicScoreOffset <= this.m_musicScoreOffset && this.m_pSustainNotesPositionInScore[index2].m_musicScoreOffset + this.m_pSustainNotesPositionInScore[index2].m_sustainLength - 1 <= this.m_musicScoreOffset)
                      flag1 = true;
                  }
                }
              }
              if (!flag2)
                flag1 = true;
            }
          }
          if (flag1)
            this.m_buttonOverlayStateFlags[index1] &= -4;
          else if (flag2)
            this.m_pButtonOverlaySprites[index1].Reset();
        }
        if ((this.m_buttonOverlayStateFlags[index1] & 4) != 0 && this.m_pButtonStarPowerOverlaySprites[index1].Update(timeElapsedMS))
          this.m_buttonOverlayStateFlags[index1] &= -5;
      }
    }

    private void HandleAnimateMusician(int timeElapsedMS)
    {
      switch (CGHStaticData.m_rocker_animation_state)
      {
        case 1:
          if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
          {
            if (CGHStaticData.m_musicianAppearance == 0)
              this.m_pMusicianSprite.SetAnimation(0, true);
            else
              this.m_pMusicianSprite.SetAnimation(0, true);
          }
          else if (CGHStaticData.m_musicianAppearance == 0)
            this.m_pMusicianSprite.SetAnimation(0, true);
          else
            this.m_pMusicianSprite.SetAnimation(0, true);
          CGHStaticData.m_rocker_animation_state = 0;
          break;
        case 2:
          if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
          {
            if (CGHStaticData.m_musicianAppearance == 0)
              this.m_pMusicianSprite.SetAnimation(1, false);
            else
              this.m_pMusicianSprite.SetAnimation(1, false);
          }
          else if (CGHStaticData.m_musicianAppearance == 0)
            this.m_pMusicianSprite.SetAnimation(1, false);
          else
            this.m_pMusicianSprite.SetAnimation(1, false);
          CGHStaticData.m_rocker_animation_state = 3;
          break;
        case 4:
          if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
          {
            if (CGHStaticData.m_musicianAppearance == 0)
              this.m_pMusicianSprite.SetAnimation(2, true);
            else
              this.m_pMusicianSprite.SetAnimation(2, true);
          }
          else if (CGHStaticData.m_musicianAppearance == 0)
            this.m_pMusicianSprite.SetAnimation(2, true);
          else
            this.m_pMusicianSprite.SetAnimation(2, true);
          CGHStaticData.m_rocker_animation_state = 5;
          break;
      }
      if (!this.m_pMusicianSprite.Update(timeElapsedMS) || CGHStaticData.m_rocker_animation_state != 3)
        return;
      if (CGHStaticData.m_bStarPowerScoringActivated)
        CGHStaticData.m_rocker_animation_state = 4;
      else
        CGHStaticData.m_rocker_animation_state = 1;
    }

    private void HandleAnimateCrowd(int timeElapsedMS) => this.m_pCrowdSprite.Update(timeElapsedMS);

    private void HandleAnimateStarPowerOverlay(int timeElapsedMS)
    {
      this.m_pStarPowerOverlaySprite.Update(timeElapsedMS);
    }

    private void HandleAnimateTrackRails(int timeElapsedMS)
    {
      this.m_pLeftRail.Update(timeElapsedMS);
      this.m_pRightRail.Update(timeElapsedMS);
      if (!this.pHud.RockMeterStatusChanged())
        return;
      if (this.pHud.RockMeterSetToFailing())
      {
        this.m_pLeftRail.SetAnimation(5);
        this.m_pLeftRail.SetLoop(true);
        this.m_pRightRail.SetAnimation(6);
        this.m_pRightRail.SetLoop(true);
      }
      else
      {
        this.m_pLeftRail.SetAnimation(0);
        this.m_pLeftRail.SetLoop(false);
        this.m_pRightRail.SetAnimation(1);
        this.m_pRightRail.SetLoop(false);
      }
    }

    private void HandleAnimateVenue(int timeElapsedMS) => this.m_pStageSprite.Update(timeElapsedMS);

    private void HandleAnimateStarParticles(int timeElapsedMS)
    {
      for (int index = 0; index < 40; ++index)
      {
        if (this.m_pStarParticles[index].IsActive())
          this.m_pStarParticles[index].tick(timeElapsedMS);
      }
    }

    private void SpawnStarParticle(int x, int y, int velocityPercentage)
    {
      for (int index = 0; index < 40; ++index)
      {
        if (!this.m_pStarParticles[index].IsActive())
        {
          this.m_pStarParticles[index].init(x, y, velocityPercentage);
          break;
        }
      }
    }

    private void RenderStageBackground()
    {
      this.m_pStageSprite.Draw(CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarTrackOriginPos.m_y + this.m_stageNudge);
      this.m_pCrowdSprite.Draw(CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarTrackOriginPos.m_y + this.m_stageNudge);
      if (CGHStaticData.m_state < 6 && CGHStaticData.m_bStarPowerScoringActivated)
        this.m_pStarPowerOverlaySprite.Draw(CGHStaticData.m_guitarPlayerAnchorPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarPlayerAnchorPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y + this.m_stageNudge);
      this.m_pMusicianSprite.Draw(CGHStaticData.m_guitarPlayerAnchorPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarPlayerAnchorPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y + this.m_stageNudge);
      this.RenderPerspectiveTrack();
    }

    private void RenderGuitarPlayfield()
    {
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
        return;
      this.m_pLeftNoteTrack.Draw(CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarTrackOriginPos.m_y);
      this.m_pCenterNoteTrack.Draw(CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarTrackOriginPos.m_y);
      this.m_pRightNoteTrack.Draw(CGHStaticData.m_guitarTrackOriginPos.m_x, CGHStaticData.m_guitarTrackOriginPos.m_y);
    }

    private void RenderGuitarEdgeRails()
    {
      this.m_pLeftRail.Draw(this.m_guitarLeftTrackRailPos.m_x, this.m_guitarLeftTrackRailPos.m_y);
      this.m_pRightRail.Draw(this.m_guitarRightTrackRailPos.m_x, this.m_guitarRightTrackRailPos.m_y);
    }

    private void RenderFretBars()
    {
      int index1 = (this.m_musicScoreOffset - this.m_noteDivisionOffsetOfInstrumentButtons) % 48;
      while (index1 < 0)
        index1 += 48;
      for (; index1 < 240; index1 += 48)
      {
        int num1 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[index1]);
        int num2 = 3 - index1 * 4 / 240;
        int int32_1 = CMathFixed.FixedToInt32(this.m_pTrackRowHorizontalFixedWidth[index1]);
        int int32_2 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1] + num1);
        int x0 = this.m_guitarPlayFieldTopCenter.m_x - (int32_1 >> 1);
        int x1 = this.m_guitarPlayFieldTopCenter.m_x + (int32_1 >> 1);
        int num3 = this.m_guitarPlayFieldTopCenter.m_y + int32_2;
        int num4 = num2;
        if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
          num4 += 2;
        uint[] numArray;
        int num5;
        switch (num4)
        {
          case 0:
            numArray = Consts.kTableFretBar4Colors;
            num5 = 4;
            break;
          case 1:
            numArray = Consts.kTableFretBar3Colors;
            num5 = 3;
            break;
          case 2:
            numArray = Consts.kTableFretBar2Colors;
            num5 = 2;
            break;
          case 3:
            numArray = Consts.kTableFretBar1Colors;
            num5 = 1;
            break;
          default:
            numArray = Consts.kTableFretBar1Colors;
            num5 = 1;
            break;
        }
        int num6 = num5 >> 1;
        for (int index2 = 0; index2 < num5; ++index2)
          CDrawUtil.FillLine(x0, num3 - num6 + index2, x1, num3 - num6 + index2, numArray[index2]);
      }
    }

    private void RenderPerspectiveTrack()
    {
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      int abstractionLayer = (int) instance.GetActiveAbstractionLayer();
      ICRenderSurface backgroundTexture = this.m_pTrackBackgroundTexture;
      uint width;
      uint height;
      backgroundTexture.GetWidthAndHeight(out width, out height);
      int num1 = CMathFixed.Int32ToFixed(width);
      CMathFixed.Int32ToFixed(height);
      int val1 = CMathFixed.Div(CMathFixed.Int32ToFixed(CGHStaticData.m_trackEdgeTopRight.m_x * 2), num1);
      int num2 = CMathFixed.Int32ToFixed(this.m_guitarPlayFieldTopCenter.m_x);
      int num3 = CMathFixed.Int32ToFixed(this.m_guitarPlayFieldTopCenter.m_y);
      for (int v = this.m_heightOfNoteTrack > this.m_heightOfTrackRails ? this.m_heightOfNoteTrack - this.m_heightOfTrackRails : 0; v < this.m_heightOfNoteTrack; ++v)
      {
        int index = this.m_pPixelRowTableReferenceIndex[v];
        CRectangle crectangle = new CRectangle(0, (int) (short) ((long) (index - this.m_musicScoreOffset) % (long) height), (int) (short) CMathFixed.FixedToInt32(num1), 1);
        instance.PushTransform();
        int x = CMathFixed.Mul(val1, CMathFixed.Div(this.m_pTrackRowHorizontalFixedWidth[index], this.m_pTrackRowHorizontalFixedWidth[0]));
        instance.Translate(num2 - (x >> 1), num3 + CMathFixed.Int32ToFixed(v));
        instance.Scale(x, CMathFixed.Int32ToFixed(1));
        instance.Translate(-(num1 >> 1), 0);
        instance.Draw(backgroundTexture, ICGraphics2d.Flip.NoFlip, new CRectangle?(crectangle));
        instance.PopTransform();
      }
    }

    private void RenderNotesSubLayer()
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      int scrollPixelVerticalOffset = 0;
      int scrollPixelHorizontalOffset = 0;
      for (int rowIndex = 0; rowIndex < 240; ++rowIndex)
      {
        bool flag4 = false;
        int musicScoreOffset = this.m_musicScoreOffset - this.m_noteDivisionOffsetOfInstrumentButtons + (239 - rowIndex);
        if (musicScoreOffset >= 0 && musicScoreOffset < this.m_songTotalNoteDivisions)
        {
          bool flag5 = false;
          byte num1 = this.m_pMusicScore[0][musicScoreOffset];
          if (4 == ((int) num1 & 4))
          {
            if (!flag1)
            {
              int num2 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[rowIndex]);
              int num3 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex]);
              scrollPixelVerticalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[rowIndex] + num2);
              scrollPixelHorizontalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[rowIndex] + num3) + (this.m_guitarPlayFieldTopWidth >> 1);
              flag4 = true;
              this.RenderSustainNoteLine(CGuitarHeroGame.eNoteID.NOTE_1, rowIndex, scrollPixelHorizontalOffset, scrollPixelVerticalOffset, this.IsSustainedNoteBeingPlayed(0, musicScoreOffset), 8 != ((int) num1 & 8));
              flag1 = true;
            }
            flag5 = true;
            if (1 == ((int) num1 & 1))
              flag1 = false;
          }
          else
            flag1 = false;
          if (!CGHStaticData.m_bSingleNoteDisplayHandset || !flag5)
          {
            byte num4 = this.m_pMusicScore[1][musicScoreOffset];
            if (4 == ((int) num4 & 4))
            {
              if (!flag2)
              {
                if (!flag4)
                {
                  int num5 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[rowIndex]);
                  int num6 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex]);
                  scrollPixelVerticalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[rowIndex] + num5);
                  scrollPixelHorizontalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[rowIndex] + num6) + (this.m_guitarPlayFieldTopWidth >> 1);
                  flag4 = true;
                }
                this.RenderSustainNoteLine(CGuitarHeroGame.eNoteID.NOTE_2, rowIndex, scrollPixelHorizontalOffset, scrollPixelVerticalOffset, this.IsSustainedNoteBeingPlayed(1, musicScoreOffset), 8 != ((int) num4 & 8));
                flag2 = true;
              }
              flag5 = true;
              if (1 == ((int) num4 & 1))
                flag2 = false;
            }
            else
              flag2 = false;
          }
          if (!CGHStaticData.m_bSingleNoteDisplayHandset || !flag5)
          {
            byte num7 = this.m_pMusicScore[2][musicScoreOffset];
            if (4 == ((int) num7 & 4))
            {
              if (!flag3)
              {
                if (!flag4)
                {
                  int num8 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[rowIndex]);
                  int num9 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex]);
                  scrollPixelVerticalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[rowIndex] + num8);
                  scrollPixelHorizontalOffset = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[rowIndex] + num9) + (this.m_guitarPlayFieldTopWidth >> 1);
                }
                this.RenderSustainNoteLine(CGuitarHeroGame.eNoteID.NOTE_3, rowIndex, scrollPixelHorizontalOffset, scrollPixelVerticalOffset, this.IsSustainedNoteBeingPlayed(2, musicScoreOffset), 8 != ((int) num7 & 8));
                flag3 = true;
              }
              if (1 == ((int) num7 & 1))
                flag3 = false;
            }
            else
              flag3 = false;
          }
        }
      }
    }

    private void RenderNotes()
    {
      int num1 = 0;
      int num2 = 0;
      for (int index1 = 0; index1 < 240; ++index1)
      {
        bool flag1 = false;
        int index2 = 3 - index1 * 4 / 240;
        int index3 = this.m_musicScoreOffset - this.m_noteDivisionOffsetOfInstrumentButtons + (239 - index1);
        if (index3 >= 0 && index3 < this.m_songTotalNoteDivisions)
        {
          bool flag2 = index3 > this.m_musicScoreOffset + this.m_noteDivisionOffsetOfInstrumentButtons;
          bool flag3 = false;
          if (this.m_bPlayingDrumsOrBass)
          {
            byte num3 = this.m_pMusicScore[3][index3];
            if (1 == ((int) num3 & 3))
            {
              if (128 != ((int) num3 & 128) || flag2)
              {
                int num4 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[index1]);
                int num5 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[index1]);
                num1 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1] + num4);
                num2 = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[index1] + num5) + (this.m_guitarPlayFieldTopWidth >> 1);
                flag1 = true;
                int int32 = CMathFixed.FixedToInt32(this.m_pTrackRowHorizontalFixedWidth[index1]);
                int x0 = this.m_guitarPlayFieldTopCenter.m_x - (int32 >> 1);
                int x1 = this.m_guitarPlayFieldTopCenter.m_x + (int32 >> 1);
                int num6 = this.m_guitarPlayFieldTopCenter.m_y + num1;
                uint[] numArray;
                int num7;
                switch (index2)
                {
                  case 0:
                    numArray = CGHStaticData.m_bStarPowerScoringActivated ? Consts.kTableKickPedalStar8Colors : Consts.kTableKickPedal8Colors;
                    num7 = 8;
                    break;
                  case 1:
                    numArray = CGHStaticData.m_bStarPowerScoringActivated ? Consts.kTableKickPedalStar7Colors : Consts.kTableKickPedal7Colors;
                    num7 = 7;
                    break;
                  case 2:
                    numArray = CGHStaticData.m_bStarPowerScoringActivated ? Consts.kTableKickPedalStar6Colors : Consts.kTableKickPedal6Colors;
                    num7 = 6;
                    break;
                  case 3:
                    numArray = CGHStaticData.m_bStarPowerScoringActivated ? Consts.kTableKickPedalStar5Colors : Consts.kTableKickPedal5Colors;
                    num7 = 5;
                    break;
                  default:
                    numArray = CGHStaticData.m_bStarPowerScoringActivated ? Consts.kTableKickPedalStar4Colors : Consts.kTableKickPedal4Colors;
                    num7 = 4;
                    break;
                }
                int num8 = num7 >> 1;
                for (int index4 = 0; index4 < num7; ++index4)
                  CDrawUtil.FillRect(x0, num6 - num8 + index4, x1, num6 - num8 + index4 + 1, numArray[index4]);
              }
              flag3 = true;
            }
          }
          if (!CGHStaticData.m_bSingleNoteDisplayHandset || !flag3)
          {
            byte num9 = this.m_pMusicScore[0][index3];
            if (1 == ((int) num9 & 3))
            {
              if (128 != ((int) num9 & 128) || flag2)
              {
                if (!flag1)
                {
                  int num10 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[index1]);
                  int num11 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[index1]);
                  num1 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1] + num10);
                  num2 = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[index1] + num11) + (this.m_guitarPlayFieldTopWidth >> 1);
                  flag1 = true;
                }
                if (64 == ((int) num9 & 64))
                  this.m_pStarNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 0][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x - num2, this.m_guitarPlayFieldTopCenter.m_y + num1);
                else
                  this.m_pNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 0][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x - num2, this.m_guitarPlayFieldTopCenter.m_y + num1);
              }
              flag3 = true;
            }
          }
          if (!CGHStaticData.m_bSingleNoteDisplayHandset || !flag3)
          {
            byte num12 = this.m_pMusicScore[1][index3];
            if (1 == ((int) num12 & 3))
            {
              if (128 != ((int) num12 & 128) || flag2)
              {
                if (!flag1)
                {
                  int num13 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[index1]);
                  int num14 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[index1]);
                  num1 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1] + num13);
                  num2 = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[index1] + num14) + (this.m_guitarPlayFieldTopWidth >> 1);
                  flag1 = true;
                }
                if (64 == ((int) num12 & 64))
                  this.m_pStarNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 1][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x, this.m_guitarPlayFieldTopCenter.m_y + num1);
                else
                  this.m_pNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 1][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x, this.m_guitarPlayFieldTopCenter.m_y + num1);
              }
              flag3 = true;
            }
          }
          if (!CGHStaticData.m_bSingleNoteDisplayHandset || !flag3)
          {
            byte num15 = this.m_pMusicScore[2][index3];
            if (1 == ((int) num15 & 3))
            {
              if (128 != ((int) num15 & 128) || flag2)
              {
                if (!flag1)
                {
                  int num16 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[index1]);
                  int num17 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[index1]);
                  num1 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1] + num16);
                  num2 = CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[index1] + num17) + (this.m_guitarPlayFieldTopWidth >> 1);
                }
                if (64 == ((int) num15 & 64))
                  this.m_pStarNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 2][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x + num2, this.m_guitarPlayFieldTopCenter.m_y + num1);
                else
                  this.m_pNoteSprites[CGHStaticData.m_bStarPowerScoringActivated ? 3 : 2][index2].Draw(this.m_guitarPlayFieldTopCenter.m_x + num2, this.m_guitarPlayFieldTopCenter.m_y + num1);
              }
            }
          }
        }
      }
    }

    private void RenderSustainNoteLine(
      CGuitarHeroGame.eNoteID noteID,
      int rowIndex,
      int scrollPixelHorizontalOffset,
      int scrollPixelVerticalOffset,
      bool bPlaying,
      bool bMissed)
    {
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      ICRenderSurface surface = (ICRenderSurface) null;
      int num1 = this.m_musicScoreOffset - this.m_noteDivisionOffsetOfInstrumentButtons + (239 - rowIndex);
      switch (noteID)
      {
        case CGuitarHeroGame.eNoteID.NOTE_1:
          surface = !bPlaying ? (!CGHStaticData.m_bStarPowerScoringActivated ? this.green : this.blue_left) : (!CGHStaticData.m_bStarPowerScoringActivated ? this.green_active : this.blue_left_active);
          break;
        case CGuitarHeroGame.eNoteID.NOTE_2:
          surface = !bPlaying ? (!CGHStaticData.m_bStarPowerScoringActivated ? this.red : this.blue_middle) : (!CGHStaticData.m_bStarPowerScoringActivated ? this.red_active : this.blue_middle_active);
          break;
        case CGuitarHeroGame.eNoteID.NOTE_3:
          surface = !bPlaying ? (!CGHStaticData.m_bStarPowerScoringActivated ? this.yellow : this.blue_right) : (!CGHStaticData.m_bStarPowerScoringActivated ? this.yellow_active : this.blue_right_active);
          break;
      }
      int num2 = 0;
      int index = num1;
      while (index > this.m_musicScoreOffset && 4 == ((int) this.m_pMusicScore[(int) noteID][index] & 4) && 1 != ((int) this.m_pMusicScore[(int) noteID][index] & 1))
      {
        --index;
        ++num2;
      }
      if (num2 <= 0)
        return;
      int num3 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowVerticalFixedHeight[rowIndex + num2]);
      int int32 = CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[rowIndex + num2] + num3);
      int num4 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex + num2]);
      CMathFixed.FixedToInt32(this.m_pNoteRowHorizontalFixedOffset[rowIndex + num2] + num4);
      int v1;
      switch (noteID)
      {
        case CGuitarHeroGame.eNoteID.NOTE_1:
          v1 = 75;
          break;
        case CGuitarHeroGame.eNoteID.NOTE_3:
          v1 = 190;
          break;
        default:
          v1 = 149;
          break;
      }
      int y = this.m_guitarPlayFieldTopCenter.m_y + scrollPixelVerticalOffset;
      int num5 = this.m_guitarPlayFieldTopCenter.m_y + int32;
      int num6 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex]);
      int val2_1 = CMathFixed.Mul(CMathFixed.Div(this.m_pNoteRowHorizontalFixedOffset[rowIndex] + num6, CMathFixed.Int32ToFixed(this.m_guitarPlayFieldBottomWidth - this.m_guitarPlayFieldTopWidth >> 1)), 49152) + 16384;
      int num7 = CMathFixed.Mul(this.m_noteScrollOffset, this.m_pNoteRowHorizontalFixedWidth[rowIndex + num2]);
      int val2_2 = CMathFixed.Mul(CMathFixed.Div(this.m_pNoteRowHorizontalFixedOffset[rowIndex + num2] + num7, CMathFixed.Int32ToFixed(this.m_guitarPlayFieldBottomWidth - this.m_guitarPlayFieldTopWidth >> 1)), 49152) + 16384;
      int num8 = bPlaying ? 1 : 0;
      int v2 = 0;
      CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(v2), val2_1));
      CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(v2), val2_2));
      CRectangle clip = instance.GetClip();
      instance.PushTransform();
      instance.Translate(CMathFixed.Int32ToFixed(v1), CMathFixed.Int32ToFixed(this.m_guitarPlayFieldTopCenter.m_y));
      instance.SetClip(0U, (uint) y, 480U, (uint) CMath.Abs(y - num5));
      instance.Draw(surface);
      instance.PopTransform();
      instance.SetClip(clip);
      int v3 = 0;
      CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(v3), val2_1));
      CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(v3), val2_2));
    }

    private void RenderInstrumentButtons()
    {
      for (int index = this.m_bPlayingDrumsOrBass ? 3 : 2; index >= 0; --index)
      {
        if (3 == index)
        {
          this.m_pButtonSprites[3].Draw(this.m_pInstrumentButtonPositions[1].m_x, this.m_pInstrumentButtonPositions[1].m_y);
        }
        else
        {
          this.m_pButtonSprites[index].Draw(this.m_pInstrumentButtonPositions[index].m_x, this.m_pInstrumentButtonPositions[index].m_y);
          if (index < 3)
            this.m_pTouchButtonSprites[index].Draw(this.m_pTouchButtonPositions[index].m_x, this.m_pTouchButtonPositions[index].m_y);
        }
      }
    }

    private void RenderInstrumentButtonOverlays()
    {
      for (int index = this.m_bPlayingDrumsOrBass ? 3 : 2; index >= 0; --index)
      {
        if (3 == index)
        {
          if ((this.m_buttonOverlayStateFlags[index] & 3) != 0)
          {
            this.m_pButtonOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[0].m_x, this.m_pInstrumentButtonPositions[0].m_y);
            this.m_pButtonOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[1].m_x, this.m_pInstrumentButtonPositions[1].m_y);
            this.m_pButtonOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[2].m_x, this.m_pInstrumentButtonPositions[2].m_y);
          }
          if ((this.m_buttonOverlayStateFlags[index] & 4) != 0)
            this.m_pButtonStarPowerOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[index].m_x, this.m_pInstrumentButtonPositions[index].m_y);
        }
        else
        {
          if ((this.m_buttonOverlayStateFlags[index] & 3) != 0)
            this.m_pButtonOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[index].m_x, this.m_pInstrumentButtonPositions[index].m_y);
          if ((this.m_buttonOverlayStateFlags[index] & 4) != 0)
            this.m_pButtonStarPowerOverlaySprites[index].Draw(this.m_pInstrumentButtonPositions[index].m_x, this.m_pInstrumentButtonPositions[index].m_y);
        }
      }
    }

    private void RenderTouchscreenButtons()
    {
      if (!this.m_touchscreenSupport || this.m_pStarPowerIcon == null)
        return;
      if (!CGHStaticData.m_bStarPowerScoringActivated && this.m_starPowerScore >= 50)
        this.m_pStarPowerIcon.SetAnimation(14);
      else
        this.m_pStarPowerIcon.SetAnimation(15);
      this.m_pStarPowerIcon.Draw();
    }

    private void RenderUnderlays() => this.pHud.paint(true, false);

    private void RenderOverlays(bool bRenderMessages) => this.pHud.paint(false, bRenderMessages);

    private void RenderStarParticles()
    {
      for (int index = 0; index < 40; ++index)
      {
        if (this.m_pStarParticles[index].IsActive())
          this.m_pStarParticles[index].paint();
      }
    }

    private void RenderYouRockMessage()
    {
      if (10 == CGHStaticData.m_state)
      {
        uint width;
        uint height;
        ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
        this.m_pStarPowerOverlaySprite.Draw((int) (width >> 1), (int) ((long) (height >> 1) - (long) (CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT).GetFontHeight() >> 1)));
      }
      uint width1;
      uint height1;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width1, out height1);
      uint num1 = width1 >> 1;
      uint num2 = height1 >> 1;
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT);
      int fontHeight = font.GetFontHeight();
      string inGameString = this.pHud.inGameStrings[7];
      int num3 = font.MeasureTextWidth(inGameString);
      font.PaintText(inGameString, inGameString.Length, (int) ((long) num1 - (long) (num3 >> 1)), (int) ((long) num2 - (long) fontHeight));
      uint y = num2 + (uint) (font.GetFontHeight() >>> 1);
      int num4 = CGHStaticData.m_starRating * this.m_starRatingIconWidth;
      uint x = (uint) ((ulong) num1 - (ulong) (num4 >> 1) + (ulong) (this.m_starRatingIconWidth >> 1));
      for (int index = 0; index < CGHStaticData.m_starRating; ++index)
      {
        this.m_pStarRatingIcon.Draw((int) x, (int) y);
        x += (uint) this.m_starRatingIconWidth;
      }
    }

    private void RenderCenteredStringOnMultipleLines(
      string wString,
      int drawX,
      int drawY,
      int maxDrawWidth,
      bool bExpandAbove)
    {
      int[] numArray = new int[10];
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      int fontHeight = font.GetFontHeight();
      string text = wString;
      int num1 = font.MeasureTextWidth(text);
      int length = text.Length;
      int index1 = 1;
      numArray[0] = 0;
      int num2 = maxDrawWidth;
      if (num1 > num2)
      {
        int index2 = 0;
        while (index2 < length)
        {
          int num3 = numArray[index1 - 1];
          index2 = num3;
          bool flag = false;
          while (!flag)
          {
            while (text[index2] != ' ' && index2 < length)
            {
              ++index2;
              if (index2 >= text.Length)
                break;
            }
            if (index2 < text.Length)
            {
              if (font.MeasureTextWidth(text.Substring(numArray[index1 - 1]), index2 - numArray[index1 - 1] + 1) <= num2)
              {
                if (index2 >= length)
                {
                  flag = true;
                }
                else
                {
                  ++index2;
                  num3 = index2;
                }
              }
              else
                flag = true;
            }
            else
              break;
          }
          numArray[index1] = num3;
          ++index1;
        }
      }
      numArray[index1] = text.Length;
      for (int index3 = 1; index3 <= index1; ++index3)
      {
        int num4 = font.MeasureTextWidth(text.Substring(numArray[index3 - 1]), numArray[index3] - numArray[index3 - 1]);
        font.PaintText(text.Substring(numArray[index3 - 1]), numArray[index3] - numArray[index3 - 1], drawX - (num4 >> 1), 
            drawY - (bExpandAbove ? index1 + 1 - index3 : -(index3 - 1)) * fontHeight);
      }
    }

    private void RenderDemoAchievementMessage()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      uint drawX = width >> 1;
      uint drawY = height >> 1;
      string output;
      CUtility.GetString(out output, "IDS_ACHIEVEMENT_IN_DEMO_MODE");
      this.RenderCenteredStringOnMultipleLines(output, (int) drawX, (int) drawY, (int) width - 60, true);
    }

    private void RenderSongTitle()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      uint drawX = width >> 1;
      uint num1 = height >> 1;
      this.RenderCenteredStringOnMultipleLines(this.m_strSongTitle, (int) drawX, (int) num1, (int) width - 60, true);
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      int fontHeight = font.GetFontHeight();
      string songAsMadeFamousBy = this.m_strSongAsMadeFamousBy;
      int num2 = font.MeasureTextWidth(songAsMadeFamousBy);
      font.PaintText(songAsMadeFamousBy, songAsMadeFamousBy.Length, (int) ((long) drawX - (long) (num2 >> 1)), (int) num1);
      this.RenderCenteredStringOnMultipleLines(this.m_strSongArtist, (int) drawX, (int) ((long) num1 + (long) fontHeight), (int) width - 60, false);
    }

    private void RenderCountdown()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      uint num1 = width >> 1;
      uint num2 = height >> 1;
      CFont font = CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_REGULARFONT);
      int fontHeight = font.GetFontHeight();
      string text = (CGHStaticData.m_timer / 500).ToString() + (object) 1;
      int num3 = font.MeasureTextWidth(text);
      font.PaintText(text, text.Length, (int) ((long) num1 - (long) (num3 >> 1)), (int) ((long) num2 - (long) fontHeight));
    }

    private void RenderGame()
    {
      if (CGHStaticData.m_state == 0 || 1 == CGHStaticData.m_state)
        return;
      if (!this.MINIMAL_DRAW_TEST)
      {
        this.RenderStageBackground();
        if (2 != CGHStaticData.m_state)
          this.RenderUnderlays();
      }
      this.RenderFretBars();
      this.RenderGuitarPlayfield();
      this.RenderNotesSubLayer();
      this.RenderGuitarEdgeRails();
      this.RenderInstrumentButtons();
      this.RenderNotes();
      this.RenderInstrumentButtonOverlays();
      this.RenderTouchscreenButtons();
      if (CGHStaticData.m_state < 7)
        this.RenderStarParticles();
      if (2 != CGHStaticData.m_state)
        this.RenderOverlays(6 > CGHStaticData.m_state && 5 != CGHStaticData.m_state);
      if (2 == CGHStaticData.m_state)
        this.RenderSongTitle();
      if (12 == CGHStaticData.m_state)
        this.RenderDemoAchievementMessage();
      if (5 == CGHStaticData.m_state)
        this.RenderCountdown();
      if (10 != CGHStaticData.m_state)
        return;
      this.RenderYouRockMessage();
    }

    public void ResetGameplay()
    {
      if (this.m_songAudioPlaybackID != 0)
      {
        ICMediaPlayer.GetInstance().Stop((uint) this.m_songAudioPlaybackID);
        this.m_songAudioPlaybackID = 0;
      }
      if (this.m_instrumentAudioPlaybackID != 0)
      {
        ICMediaPlayer.GetInstance().Stop((uint) this.m_instrumentAudioPlaybackID);
        this.m_instrumentAudioPlaybackID = 0;
      }
      this.ResetSongData();
      this.ResetIngameVariables();
      this.pHud.resetHudVariables();
      this.pHud.SetMeterMultiplierDisplay();
      this.pHud.updateMultiplier(false);
      this.ResetStarParticleEffects();
      this.m_pStarPowerOverlaySprite.SetAnimation(0, true);
      this.m_pLeftRail.SetAnimation(0);
      this.m_pLeftRail.SetLoop(false);
      this.m_pRightRail.SetAnimation(1);
      this.m_pRightRail.SetLoop(false);
      if (this.m_touchscreenSupport)
        this.m_pStarPowerIcon.SetAnimation(15);
      CGHStaticData.m_statePriorToPause = 2;
      this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_TITLE);
    }

    public void KeyPressedOnLoadingComplete()
    {
      this.ResetIngameVariables();
      CGHStaticData.m_statePriorToPause = 2;
      this.NewState(CGuitarHeroGame.eGameState.GAME_STATE_TITLE);
    }

    private void ResetSongData()
    {
      int num = this.m_songLengthInQuarterNotes * 48;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        for (int index2 = 0; index2 < num; ++index2)
        {
          this.m_pMusicScore[index1][index2] &= (byte) 127;
          if (4 == ((int) this.m_pMusicScore[index1][index2] & 4))
            this.m_pMusicScore[index1][index2] |= (byte) 8;
          if (((int) this.m_pMusicScore[index1][index2] & 48) != 0)
            this.m_pMusicScore[index1][index2] |= (byte) 64;
        }
      }
    }

    private void SetPlayingSustainNoteInformation(
      int noteID,
      int musicScoreOffset,
      int sustainNoteLength)
    {
      this.m_pSustainNotesPositionInScore[noteID].m_sustainLength = sustainNoteLength;
      this.m_pSustainNotesPositionInScore[noteID].m_musicScoreOffset = musicScoreOffset;
    }

    private void ClearPlayingSustainNoteInformation(int noteID)
    {
      this.m_pSustainNotesPositionInScore[noteID].m_sustainLength = 0;
      this.m_pSustainNotesPositionInScore[noteID].m_musicScoreOffset = -1;
    }

    private bool IsSustainedNoteBeingPlayed(int noteID, int musicScoreOffset)
    {
      bool flag = false;
      if (this.m_pSustainNotesPositionInScore[noteID].m_musicScoreOffset != -1 && this.m_pSustainNotesPositionInScore[noteID].m_musicScoreOffset <= musicScoreOffset && musicScoreOffset < this.m_pSustainNotesPositionInScore[noteID].m_musicScoreOffset + this.m_pSustainNotesPositionInScore[noteID].m_sustainLength)
        flag = true;
      return flag;
    }

    private void ClearNoteSustainAtMusicScoreOffset(
      CGuitarHeroGame.eNoteID noteID,
      int musicScoreOffset,
      bool bReleasedSustainNote)
    {
      if (this.m_pSustainNotesPositionInScore[(int) noteID].m_musicScoreOffset != -1 && this.m_pSustainNotesPositionInScore[(int) noteID].m_musicScoreOffset > musicScoreOffset)
        musicScoreOffset = this.m_pSustainNotesPositionInScore[(int) noteID].m_musicScoreOffset;
      int num1 = (this.m_songLengthInQuarterNotes - 5) * 48;
      int num2 = musicScoreOffset;
      bool flag = false;
      if (bReleasedSustainNote && this.m_pSustainNotesPositionInScore[(int) noteID].m_musicScoreOffset != -1 && 8 == ((int) this.m_pMusicScore[(int) noteID][musicScoreOffset] & 8))
      {
        int sustainLength = this.m_pSustainNotesPositionInScore[(int) noteID].m_sustainLength;
        int num3 = musicScoreOffset - this.m_pSustainNotesPositionInScore[(int) noteID].m_musicScoreOffset + 1;
        if (sustainLength - num3 <= CMath.Min(sustainLength >> 1, 24))
          flag = true;
      }
      if (!flag && 8 == ((int) this.m_pMusicScore[(int) noteID][musicScoreOffset] & 8))
        this.pHud.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_HOLD_THAT_NOTE, CHud.eHUDMessageID.INDEX_STRINVALID);
      int num4 = 0;
      for (int index = num2 + 1; index <= num1 && 4 == ((int) this.m_pMusicScore[(int) noteID][index] & 4) 
                && 1 != ((int) this.m_pMusicScore[(int) noteID][index] & 1); this.m_pMusicScore[(int) noteID][index++] &= (byte) 247)
      {
        if (flag && 8 == ((int) this.m_pMusicScore[(int) noteID][index] & 8))
          ++num4;
      }
      for (int index = num2; index >= 0 && 4 == ((int) this.m_pMusicScore[(int) noteID][index] & 4); this.m_pMusicScore[(int) noteID][index--] &= (byte) 247)
      {
        if (flag && 8 == ((int) this.m_pMusicScore[(int) noteID][index] & 8))
          ++num4;
      }
      if (num4 != 0)
      {
        int num5 = (CGHStaticData.m_sustainScoreFractionalRemainder + num4 * 50 * CGHStaticData.m_scoreMultiplier * (CGHStaticData.m_bStarPowerScoringActivated ? 2 : 1)) / 96;
        CGHStaticData.m_sustainScoreFractionalRemainder = 0;
        CGHStaticData.m_score += num5;
      }
      this.ClearPlayingSustainNoteInformation((int) noteID);
    }

    private void ClearStarPowerAtMusicScoreOffset(int musicScoreOffset)
    {
      int num1 = (this.m_songLengthInQuarterNotes - 5) * 48;
      int num2 = musicScoreOffset;
      if (!this.IsWithinActiveStarPowerPhrase(num2 * this.m_songTimeDivision / 48))
        return;
      int index1 = num2;
      bool flag1 = false;
      bool flag2 = false;
      for (; index1 < num1 && !flag1; ++index1)
      {
        if (flag2)
          flag1 = true;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          if (16 == ((int) this.m_pMusicScore[index2][index1] & 48))
            flag2 = true;
          if (index1 + 1 < num1 && flag2 && 32 == ((int) this.m_pMusicScore[index2][index1 + 1] & 32))
            flag1 = false;
          this.m_pMusicScore[index2][index1] &= (byte) 191;
        }
      }
      int index3 = num2;
      for (bool flag3 = false; index3 >= 0 && !flag3; --index3)
      {
        for (int index4 = 0; index4 < 4; ++index4)
        {
          if (48 == ((int) this.m_pMusicScore[index4][index3] & 48))
            flag3 = true;
          this.m_pMusicScore[index4][index3] &= (byte) 191;
        }
      }
      ++this.m_totalStarPowerPhrases;
      this.m_totalConsecutiveStarPowerNotesHit = 0;
    }

    private bool IsWithinActiveStarPowerPhrase(int songPosition)
    {
      bool flag = false;
      for (int index = 0; !flag && index < this.m_numStarPowersInList; ++index)
      {
        if (this.m_pStarPowersList[index].m_bActive && this.m_pStarPowersList[index].m_eventTime <= songPosition && songPosition < this.m_pStarPowersList[index].m_eventTime + this.m_pStarPowersList[index].m_eventLength)
          flag = true;
      }
      return flag;
    }

    private bool DeactivateStarPowerPhrase(int songPosition)
    {
      bool flag = false;
      for (int index = 0; !flag && index < this.m_numStarPowersInList; ++index)
      {
        if (this.m_pStarPowersList[index].m_bActive && this.m_pStarPowersList[index].m_eventTime <= songPosition
                    && songPosition < this.m_pStarPowersList[index].m_eventTime + this.m_pStarPowersList[index].m_eventLength)
        {
          this.m_pStarPowersList[index].m_bActive = false;
          flag = true;
        }
      }
      return flag;
    }

    private void ActivateStarPowerPhraseCompletedEffect(int noteID)
    {
      ++this.m_totalStarPowerPhrasesCompleted;
      ++this.m_totalStarPowerPhrases;
      this.m_totalConsecutiveStarPowerNotesHit = 0;
      if (this.m_starPowerScore < 100)
      {
        if (!CGHStaticData.m_bStarPowerScoringActivated)
        {
          if (this.m_starPowerScore < 50 && this.m_starPowerScore + 50 >= 50)
          {
            this.pHud.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_STAR_POWER_READY, CHud.eHUDMessageID.INDEX_STRINGAME_STAR_POWER_ACTIVATE_BUTTON);
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pStarPowerAvailableSFX);
          }
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pStarPowerAwardedSFX);
        }
        this.m_starPowerScore += 50;
        if (this.m_starPowerScore >= 100)
        {
          this.m_starPowerScore = 100;
          this.pHud.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_STAR_POWER_FULL, CHud.eHUDMessageID.INDEX_STRINVALID);
        }
        this.m_activedStarPowerRemainingNoteDivisions = this.m_starPowerScore * 1536 / 100;
        this.pHud.updateStarMeter(this.m_starPowerScore);
      }
      this.SetStarPowerNoteOverlay(noteID, true);
    }

    private void SetStarPowerNoteOverlay(int noteID, bool bSingleStarPower)
    {
      this.m_pButtonStarPowerOverlaySprites[noteID].SetArchetypeCharacter(6, 0);
      this.m_pButtonStarPowerOverlaySprites[noteID].SetAnimation(0);
      this.m_buttonOverlayStateFlags[noteID] |= 4;
      int velocityPercentage = this.m_starPowerScore * 100 / 100;
      for (int index = 0; index < (bSingleStarPower ? 10 : 5); ++index)
        this.SpawnStarParticle(this.m_pInstrumentButtonPositions[noteID].m_x, this.m_pInstrumentButtonPositions[noteID].m_y, velocityPercentage);
    }

    private void ActivateStarPower()
    {
      if (CGHStaticData.m_bStarPowerScoringActivated)
        return;
      if (this.m_starPowerScore >= 50)
      {
        CGHStaticData.m_bStarPowerScoringActivated = true;
        ++CGHStaticData.m_totalTimesStarPowerDeployed;
        this.m_pStarPowerOverlaySprite.Reset();
        this.m_activedStarPowerRemainingNoteDivisions = this.m_starPowerScore * 1536 / 100;
        this.SetStarPowerNoteOverlay(0, false);
        this.SetStarPowerNoteOverlay(1, false);
        this.SetStarPowerNoteOverlay(2, false);
        this.pHud.activateStarPower();
        CGHStaticData.m_rocker_animation_state = 4;
        int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdCheerSFX);
        int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pStarPowerDeploySFX);
        if (!ICMediaPlayer.GetInstance().GetVibrationEnabled() || CGameApp.GetInstance().IsVibrationDisabled())
          return;
        int num3 = (int) ICMediaPlayer.GetInstance().Vibrate(400U, (byte) 0);
      }
      else
        this.pHud.SetMessage(CHud.eHUDMessageID.INDEX_STRINGAME_STAR_POWER_INSUFFICIENT, CHud.eHUDMessageID.INDEX_STRINVALID);
    }

    private void HandleStarPowerUpdate(int musicScoreScrollInNoteDivisions)
    {
      if (!CGHStaticData.m_bStarPowerScoringActivated)
        return;
      this.m_activedStarPowerRemainingNoteDivisions -= musicScoreScrollInNoteDivisions;
      this.m_starPowerScore = this.m_activedStarPowerRemainingNoteDivisions * 100 / 1536;
      if (this.m_activedStarPowerRemainingNoteDivisions <= 0)
      {
        CGHStaticData.m_bStarPowerScoringActivated = false;
        this.m_starPowerScore = 0;
        this.m_activedStarPowerRemainingNoteDivisions = 0;
        this.pHud.deactivateStarPower();
        CGHStaticData.m_rocker_animation_state = 1;
        int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pStarPowerDepletedSFX);
      }
      this.pHud.updateStarMeter(this.m_starPowerScore);
    }

    private void UpdateScoreForPlayingNote()
    {
      CGHStaticData.m_score += 50 * CGHStaticData.m_scoreMultiplier * (CGHStaticData.m_bStarPowerScoringActivated ? 2 : 1);
    }

    private void IncrementConsecutiveHitNotes()
    {
      ++CGHStaticData.m_currentConsecutiveNotesHit;
      if (CGHStaticData.m_currentConsecutiveNotesHit > CGHStaticData.m_maximumConsecutiveNotesHit)
        CGHStaticData.m_maximumConsecutiveNotesHit = CGHStaticData.m_currentConsecutiveNotesHit;
      if (CGHStaticData.m_scoreMultiplier >= 4)
        return;
      CGHStaticData.m_scoreMultiplierStep = CGHStaticData.m_currentConsecutiveNotesHit % 10;
      CGHStaticData.m_scoreMultiplier = CGHStaticData.m_currentConsecutiveNotesHit / 10 + 1;
      if (CGHStaticData.m_scoreMultiplier > 4)
        CGHStaticData.m_scoreMultiplier = 4;
      this.pHud.updateMultiplier(true);
    }

    private void ClearConsecutiveHitNotes()
    {
      CGHStaticData.m_currentConsecutiveNotesHit = 0;
      CGHStaticData.m_scoreMultiplierStep = 0;
      CGHStaticData.m_scoreMultiplier = 1;
      this.pHud.updateMultiplier(false);
    }

    private void ClearScoreMultiplierStatistics()
    {
      CGHStaticData.m_totalScoreMultiplierSamples = 0;
      CGHStaticData.m_totalValueOfScoreMultiplierSamples = 0;
    }

    private void UpdateScoreMultiplierStatistics()
    {
      ++CGHStaticData.m_totalScoreMultiplierSamples;
      CGHStaticData.m_totalValueOfScoreMultiplierSamples += CGHStaticData.m_scoreMultiplier;
    }

    private void CalculateSongFinalStatistics()
    {
      CGHStaticData.m_percentageNotesHit = CGHStaticData.m_hitNoteGroupCount 
                + CGHStaticData.m_missedNoteGroupCount == 0 
                ? 0 : CGHStaticData.m_hitNoteGroupCount * 100 / (CGHStaticData.m_hitNoteGroupCount + CGHStaticData.m_missedNoteGroupCount);
      if (CGHStaticData.m_score > CGHStaticData.m_starRatingbaseScore * 280 / 100)
        CGHStaticData.m_starRating = 5;
      else if (CGHStaticData.m_score > CGHStaticData.m_starRatingbaseScore << 1)
        CGHStaticData.m_starRating = 4;
      else if (CGHStaticData.m_score > CGHStaticData.m_starRatingbaseScore)
        CGHStaticData.m_starRating = 3;
      else if (CGHStaticData.m_score > CGHStaticData.m_starRatingbaseScore * 75 / 100)
        CGHStaticData.m_starRating = 2;
      else
        CGHStaticData.m_starRating = 1;
    }

    private void UpdateLeaderboards()
    {
      int instrumentBeingPlayed = (int) CGHStaticData.m_instrumentBeingPlayed;
      LiveLeaderBoards.GetInstance().AddLeaderboardEntry(instrumentBeingPlayed, CGHStaticData.m_score);
    }

    private void UpdateUnlockedAchievements()
    {
      CGHStaticData.m_newlyUnlockedAchievements = 0U;
      CGHStaticData.m_newlyUnlockedAchievementsCounter = 0U;
      int songID = 0;
      CSong songSelected = CSongListMgr.GetSongSelected();
      if (songSelected != null)
        songID = (int) songSelected.GetSongID();
      int achievementsIndex = CGHStaticData.FindSongAchievementsIndex((uint) songID);
      CGHStaticData.m_pSongAchievementData[achievementsIndex].m_songID = (uint) songID;
      if (!CDemoMgr.IsDemo())
        CGHStaticData.m_pSongAchievementData[achievementsIndex].SetCompleted(CGHStaticData.m_instrumentBeingPlayed, CGHStaticData.m_difficultyLevel, 5 == CGHStaticData.m_starRating);
      if (CGHStaticData.m_instrumentBeingPlayed == CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR)
        this.UnlockAchievement(0);
      if (this.GetTotalSongsCompletedForLocation(0) >= 2)
        this.UnlockAchievement(17);
      if (this.GetTotalSongsCompletedForLocation(1) >= 2)
        this.UnlockAchievement(18);
      if (this.GetTotalSongsCompletedForLocation(2) >= 2)
        this.UnlockAchievement(19);
      if (CGHStaticData.m_maximumConsecutiveNotesHit >= 200)
        this.UnlockAchievement(1);
      if (this.GetTotalSongsCompletedForLocation(0) == 5)
        this.UnlockAchievement(2);
      if (this.GetTotalSongsCompletedForLocation(1) == 5)
        this.UnlockAchievement(3);
      if (this.GetTotalSongsCompletedForLocation(2) == 5)
        this.UnlockAchievement(4);
      if (CGHStaticData.m_pSongAchievementData[achievementsIndex].AchievedFiveStarsForAllInstrumentsOnASpecificDifficulty())
        this.UnlockAchievement(8);
      if (this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_ALL, CGHStaticData.eDifficulty.GAME_DIFFICULTY_EASY, true) >= 4 || this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_ALL, CGHStaticData.eDifficulty.GAME_DIFFICULTY_MEDIUM, true) >= 4 || this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_ALL, CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT, true) >= 4)
        this.UnlockAchievement(9);
      if (CGHStaticData.m_instrumentBeingPlayed == CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR && 5 == CGHStaticData.m_starRating && CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT == CGHStaticData.m_difficultyLevel)
        this.UnlockAchievement(10);
      if (this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR, CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT, true) >= 4)
        this.UnlockAchievement(11);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS == CGHStaticData.m_instrumentBeingPlayed && 5 == CGHStaticData.m_starRating && CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT == CGHStaticData.m_difficultyLevel)
        this.UnlockAchievement(12);
      if (this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS, CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT, true) >= 4)
        this.UnlockAchievement(13);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed && 5 == CGHStaticData.m_starRating && CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT == CGHStaticData.m_difficultyLevel)
        this.UnlockAchievement(14);
      if (this.GetTotalSongsCompletedWithAchievements(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS, CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT, true) >= 4)
        this.UnlockAchievement(15);
      if (CGHStaticData.m_instrumentBeingPlayed == CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR && 100 == CGHStaticData.m_percentageNotesHit && CGHStaticData.m_badNoteCount == 0)
        this.UnlockAchievement(5);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS == CGHStaticData.m_instrumentBeingPlayed && 100 == CGHStaticData.m_percentageNotesHit && CGHStaticData.m_badNoteCount == 0)
        this.UnlockAchievement(6);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed && 100 == CGHStaticData.m_percentageNotesHit && CGHStaticData.m_badNoteCount == 0)
        this.UnlockAchievement(7);
      if (CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT != CGHStaticData.m_difficultyLevel || 100 != CGHStaticData.m_percentageNotesHit || CGHStaticData.m_badNoteCount != 0)
        return;
      this.UnlockAchievement(16);
    }

    private void UnlockAchievement(int achievementIndex)
    {
      if (CDemoMgr.IsDemo())
      {
        CGHStaticData.m_bAchievementInDemoMode = true;
      }
      else
      {
        int num = 1 << achievementIndex;
        if (0L == ((long) CGHStaticData.m_unlockedAchievements & (long) num))
        {
          CGHStaticData.m_unlockedAchievements |= (uint) num;
          CGHStaticData.m_newlyUnlockedAchievements |= (uint) num;
          ++CGHStaticData.m_newlyUnlockedAchievementsCounter;
        }
        LiveAchievements.GetInstance().AwardAchievement((eAchievementID) achievementIndex);
      }
    }

    private bool UnlockedAllAchievements() => false;

    private int GetTotalSongsCompletedWithAchievements(
      CGHStaticData.eGameInstrument instrument,
      CGHStaticData.eDifficulty difficulty,
      bool bFiveStar)
    {
      int withAchievements = 0;
      for (int index = 0; index < 128; ++index)
      {
        if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_ALL == instrument)
        {
          if (CGHStaticData.m_pSongAchievementData[index].SongAchievementCompleted(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR, difficulty, bFiveStar) && CGHStaticData.m_pSongAchievementData[index].SongAchievementCompleted(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS, difficulty, bFiveStar) && CGHStaticData.m_pSongAchievementData[index].SongAchievementCompleted(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS, difficulty, bFiveStar))
            ++withAchievements;
        }
        else if (CGHStaticData.m_pSongAchievementData[index].SongAchievementCompleted(instrument, difficulty, bFiveStar))
          ++withAchievements;
      }
      return withAchievements;
    }

    private int GetTotalSongsCompletedForLocation(int locationID)
    {
      int completedForLocation = 0;
      for (int index = 0; index < 128; ++index)
      {
        if (CGHStaticData.m_pSongAchievementData[index].CompletedAnyInstrumentOnAnyDifficulty())
        {
          uint songId = CGHStaticData.m_pSongAchievementData[index].GetSongID();
          if (songId != 0U)
          {
            CSong song = CSongListMgr.GetSong(songId);
            if (song != null && !song.IsBonusSong() && (long) song.GetHeadlineGroup() == (long) locationID)
              ++completedForLocation;
          }
        }
      }
      return completedForLocation;
    }

    private void MuteAudioTrack(int muteTimeInMS)
    {
      this.m_bFadingDownAudioTrack = true;
      this.m_bFadingUpAudioTrack = false;
      this.m_missedSoundMuteTimeMS += muteTimeInMS;
    }

    private void UnMuteAudioTrack()
    {
      this.m_bFadingUpAudioTrack = true;
      this.m_bFadingDownAudioTrack = false;
    }

    private void HandleMuteAudioTrackUpdate(int timeElapsedMS)
    {
      if (this.m_missedSoundMuteTimeMS <= 0 && !this.m_bFadingUpAudioTrack)
        return;
      this.m_missedSoundMuteTimeMS -= timeElapsedMS;
      if (this.m_missedSoundMuteTimeMS <= 0)
        this.m_missedSoundMuteTimeMS = 0;
      if (this.m_bFadingDownAudioTrack)
      {
        this.m_missedSoundVolumePercentage -= timeElapsedMS * this.MAX_FADE_VOLUME_PERCENTAGE / 200;
        if (this.m_missedSoundVolumePercentage <= this.MIN_FADE_VOLUME_PERCENTAGE)
        {
          this.m_missedSoundVolumePercentage = this.MIN_FADE_VOLUME_PERCENTAGE;
          this.m_bFadingDownAudioTrack = false;
        }
      }
      if (!this.m_bFadingUpAudioTrack)
        return;
      this.m_missedSoundVolumePercentage += timeElapsedMS * this.MAX_FADE_VOLUME_PERCENTAGE / 200;
      if (this.m_missedSoundVolumePercentage < this.MAX_FADE_VOLUME_PERCENTAGE)
        return;
      this.m_missedSoundVolumePercentage = this.MAX_FADE_VOLUME_PERCENTAGE;
      this.m_bFadingUpAudioTrack = false;
    }

    private void LoadSongMedia()
    {
      this.m_pCurSong = CSongListMgr.GetSongSelected();
      CSongMedia csongMedia = (CSongMedia) null;
      if (this.m_pCurSong != null)
        csongMedia = this.m_pCurSong.GetSongMedia();
      if (csongMedia == null)
        return;
      this.m_pSongAudioTrack = csongMedia.mainTrack;
      this.m_pInstrumentAudioTrack = CSongListMgr.GetNumTracksSupported() != Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE ? (CMedia) null : csongMedia.instrumentTrack;
      this.m_pTempoData = csongMedia.tempoNotes.ptr;
      this.m_pSongData = csongMedia.instrumentNotes.ptr;
    }

    private void UnloadSongMedia()
    {
      CApp.GetResourceManager();
      this.m_pSongAudioTrack = (CMedia) null;
      this.m_pInstrumentAudioTrack = (CMedia) null;
    }

    private void GenerateSongBarOffsetsInMS()
    {
      int num1 = 0;
      int num2 = 0;
      this.m_pSongBarOffsetInMSList = new int[this.m_songLengthInQuarterNotes];
      for (int index = 0; index < this.m_songLengthInQuarterNotes; ++index)
      {
        this.m_pSongBarOffsetInMSList[index] = num1;
        int pTempo = this.m_pTempoList[this.m_pTempoIndexList[index]];
        num2 += pTempo;
        num1 = num2 / 1000;
      }
    }

    private void LoadSongData()
    {
      int num1 = 0;
      byte[] pTempoData1 = this.m_pTempoData;
      int index1 = num1;
      int num2 = index1 + 1;
      this.m_songTimeDivision = (int) pTempoData1[index1];
      CGuitarHeroGame cguitarHeroGame1 = this;
      int songTimeDivision = cguitarHeroGame1.m_songTimeDivision;
      byte[] pTempoData2 = this.m_pTempoData;
      int index2 = num2;
      int num3 = index2 + 1;
      int num4 = (int) pTempoData2[index2] << 8;
      cguitarHeroGame1.m_songTimeDivision = songTimeDivision | num4;
      byte[] pTempoData3 = this.m_pTempoData;
      int index3 = num3;
      int num5 = index3 + 1;
      this.m_songLengthInQuarterNotes = (int) pTempoData3[index3];
      CGuitarHeroGame cguitarHeroGame2 = this;
      int lengthInQuarterNotes = cguitarHeroGame2.m_songLengthInQuarterNotes;
      byte[] pTempoData4 = this.m_pTempoData;
      int index4 = num5;
      int num6 = index4 + 1;
      int num7 = (int) pTempoData4[index4] << 8;
      cguitarHeroGame2.m_songLengthInQuarterNotes = lengthInQuarterNotes | num7;
      this.m_songLengthInQuarterNotes += 5;
      this.m_songTotalNoteDivisions = this.m_songLengthInQuarterNotes * 48;
      byte[] pTempoData5 = this.m_pTempoData;
      int index5 = num6;
      int num8 = index5 + 1;
      this.m_numTemposInList = (int) pTempoData5[index5];
      CGuitarHeroGame cguitarHeroGame3 = this;
      int numTemposInList = cguitarHeroGame3.m_numTemposInList;
      byte[] pTempoData6 = this.m_pTempoData;
      int index6 = num8;
      int num9 = index6 + 1;
      int num10 = (int) pTempoData6[index6] << 8;
      cguitarHeroGame3.m_numTemposInList = numTemposInList | num10;
      this.m_pTempoList = new int[this.m_numTemposInList];
      for (int index7 = 0; index7 < this.m_numTemposInList; ++index7)
      {
        int[] pTempoList = this.m_pTempoList;
        int index8 = index7;
        byte[] pTempoData7 = this.m_pTempoData;
        int index9 = num9;
        int num11 = index9 + 1;
        int num12 = (int) pTempoData7[index9];
        pTempoList[index8] = num12;
        ref int local1 = ref this.m_pTempoList[index7];
        int num13 = local1;
        byte[] pTempoData8 = this.m_pTempoData;
        int index10 = num11;
        int num14 = index10 + 1;
        int num15 = (int) pTempoData8[index10] << 8;
        local1 = num13 | num15;
        ref int local2 = ref this.m_pTempoList[index7];
        int num16 = local2;
        byte[] pTempoData9 = this.m_pTempoData;
        int index11 = num14;
        num9 = index11 + 1;
        int num17 = (int) pTempoData9[index11] << 16;
        local2 = num16 | num17;
      }
      this.m_pTempoIndexList = new int[this.m_songLengthInQuarterNotes];
      for (int index12 = 0; index12 < this.m_songLengthInQuarterNotes; ++index12)
      {
        if (index12 < this.m_songLengthInQuarterNotes - 5)
        {
          byte[] pTempoData10 = this.m_pTempoData;
          int index13 = num9;
          int num18 = index13 + 1;
          int num19 = (int) pTempoData10[index13];
          byte[] pTempoData11 = this.m_pTempoData;
          int index14 = num18;
          num9 = index14 + 1;
          int num20 = (int) pTempoData11[index14] << 8;
          int num21 = num19 | num20;
          this.m_pTempoIndexList[index12] = num21;
        }
        else
          this.m_pTempoIndexList[index12] = 0;
      }
      int length = this.m_songLengthInQuarterNotes * 48;
      int num22 = 0;
      this.m_pMusicScore = new byte[4][];
      for (int index15 = 0; index15 < 4; ++index15)
      {
        this.m_pMusicScore[index15] = new byte[length];
        for (int index16 = 0; index16 < length; ++index16)
          this.m_pMusicScore[index15][index16] = (byte) 0;
      }
      this.m_pStarPowersList = new CGuitarHeroGame.sStarPower[16];
      for (int index17 = 0; index17 < 16; ++index17)
        this.m_pStarPowersList[index17] = new CGuitarHeroGame.sStarPower();
      this.m_numStarPowersInList = 0;
      CGHStaticData.m_starRatingbaseScore = 0;
      bool flag1 = false;
      while (!flag1)
      {
        byte[] pSongData1 = this.m_pSongData;
        int index18 = num22;
        int num23 = index18 + 1;
        int num24 = (int) pSongData1[index18];
        byte[] pSongData2 = this.m_pSongData;
        int index19 = num23;
        int num25 = index19 + 1;
        int num26 = (int) pSongData2[index19] << 8;
        int num27 = num24 | num26;
        byte[] pSongData3 = this.m_pSongData;
        int index20 = num25;
        num22 = index20 + 1;
        int num28 = (int) pSongData3[index20] << 16;
        int num29 = num27 | num28;
        if (16777215 == num29)
        {
          flag1 = true;
        }
        else
        {
          byte[] pSongData4 = this.m_pSongData;
          int index21 = num22;
          int num30 = index21 + 1;
          int index22 = (int) pSongData4[index21];
          if (index22 >= 4 && 7 != index22)
            index22 = 3;
          byte[] pSongData5 = this.m_pSongData;
          int index23 = num30;
          int num31 = index23 + 1;
          int num32 = (int) pSongData5[index23];
          byte[] pSongData6 = this.m_pSongData;
          int index24 = num31;
          num22 = index24 + 1;
          int num33 = (int) pSongData6[index24] << 8;
          int num34 = num32 | num33;
          if (7 == index22)
          {
            this.m_pStarPowersList[this.m_numStarPowersInList].m_eventTime = num29;
            this.m_pStarPowersList[this.m_numStarPowersInList].m_eventLength = num34;
            this.m_pStarPowersList[this.m_numStarPowersInList].m_bActive = true;
            ++this.m_numStarPowersInList;
          }
          else
          {
            int index25 = num29 * 48 / this.m_songTimeDivision;
            CGuitarHeroGame.eNoteStatus eNoteStatus = CGuitarHeroGame.eNoteStatus.NOTE_OFF;
            if (num34 >= 240)
            {
              eNoteStatus = CGuitarHeroGame.eNoteStatus.NOTE_SUSTAIN;
              CGHStaticData.m_starRatingbaseScore += num34 * 50 / (2 * this.m_songTimeDivision);
            }
            this.m_pMusicScore[index22][index25] = (byte) (CGuitarHeroGame.eNoteStatus.NOTE_ON | eNoteStatus);
            CGHStaticData.m_starRatingbaseScore += 50;
            int num35 = num34 * 48 / this.m_songTimeDivision;
            for (int index26 = 1; index26 < num35; ++index26)
              this.m_pMusicScore[index22][index25 + index26] = (byte) (CGuitarHeroGame.eNoteStatus.NOTE_HELD | eNoteStatus);
          }
        }
      }
      for (int index27 = 0; index27 < this.m_numStarPowersInList; ++index27)
      {
        int num36 = this.m_pStarPowersList[index27].m_eventTime * 48 / this.m_songTimeDivision;
        int num37 = this.m_pStarPowersList[index27].m_eventLength * 48 / this.m_songTimeDivision;
        int index28 = -1;
        int index29 = 0;
        bool flag2 = false;
        for (int index30 = 0; index30 < num37; ++index30)
        {
          for (int index31 = 0; index31 < 4; ++index31)
          {
            byte num38 = this.m_pMusicScore[index31][num36 + index30];
            if (((int) num38 & 3) != 0)
            {
              if (1 == ((int) num38 & 3) && index28 < num36 + index30)
              {
                index28 = num36 + index30;
                index29 = index31;
                if (!flag2)
                {
                  this.m_pMusicScore[index31][num36 + index30] |= (byte) 48;
                  flag2 = true;
                }
              }
              this.m_pMusicScore[index31][num36 + index30] |= (byte) 32;
            }
          }
        }
        if (index28 >= 0)
        {
          this.m_pMusicScore[index29][index28] &= (byte) 223;
          this.m_pMusicScore[index29][index28] |= (byte) 16;
        }
      }
      CGHStaticData.m_totalSongNoteGroupCount = 0;
      for (int index32 = 0; index32 < this.m_songTotalNoteDivisions; ++index32)
      {
        if (1 == ((int) this.m_pMusicScore[0][index32] & 1) || 1 == ((int) this.m_pMusicScore[1][index32] & 1) || 1 == ((int) this.m_pMusicScore[2][index32] & 1) || 1 == ((int) this.m_pMusicScore[3][index32] & 1))
          ++CGHStaticData.m_totalSongNoteGroupCount;
      }
      this.GenerateSongBarOffsetsInMS();
    }

    private void UnloadSongData()
    {
      this.m_pTempoList = (int[]) null;
      this.m_pTempoIndexList = (int[]) null;
      this.m_pSongBarOffsetInMSList = (int[]) null;
      if (this.m_pMusicScore != null)
      {
        for (int index = 0; index < 4; ++index)
          this.m_pMusicScore[index] = (byte[]) null;
      }
      this.m_pMusicScore = (byte[][]) null;
      this.m_pStarPowersList = (CGuitarHeroGame.sStarPower[]) null;
    }

    private void LoadSongInformation()
    {
      if (this.m_pCurSong == null)
        return;
      this.m_strSongTitle = this.m_pCurSong.GetSongName();
      CUtility.GetString(out this.m_strSongAsMadeFamousBy, "IDS_INGAME_MADE_FAMOUS_BY");
      this.m_strSongArtist = this.m_pCurSong.GetBandName();
    }

    private void InitializeStarParticleEffects()
    {
      CStarParticle.StaticInitialization();
      this.m_pStarParticles = new CStarParticle[40];
      for (int index = 0; index < 40; ++index)
        this.m_pStarParticles[index] = new CStarParticle();
    }

    private void ResetStarParticleEffects()
    {
      if (this.m_pStarParticles == null)
        return;
      for (int index = 0; index < 40; ++index)
        this.m_pStarParticles[index].disable();
    }

    private void ReleaseStarParticleEffects()
    {
      if (this.m_pStarParticles != null)
      {
        for (int index = 0; index < 40; ++index)
          this.m_pStarParticles[index] = (CStarParticle) null;
      }
      this.m_pStarParticles = (CStarParticle[]) null;
    }

    private void QueueIngameSprites()
    {
      SG_Home.GetInstance().Init();
      this.m_musicianArchetypeID = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS != CGHStaticData.m_instrumentBeingPlayed ? (CGHStaticData.m_musicianAppearance == 0 ? 12 : 10) : (CGHStaticData.m_musicianAppearance == 0 ? 13 : 11);
      switch (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed ? CGHStaticData.m_drumsAppearance : CGHStaticData.m_guitarAppearance)
      {
        case 1:
          this.m_musicianCharacterID = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS != CGHStaticData.m_instrumentBeingPlayed ? (CGHStaticData.m_musicianAppearance == 0 ? 3 : 3) : (CGHStaticData.m_musicianAppearance == 0 ? 1 : 1);
          break;
        case 2:
          this.m_musicianCharacterID = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS != CGHStaticData.m_instrumentBeingPlayed ? (CGHStaticData.m_musicianAppearance == 0 ? 2 : 2) : (CGHStaticData.m_musicianAppearance == 0 ? 2 : 2);
          break;
        case 3:
          this.m_musicianCharacterID = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS != CGHStaticData.m_instrumentBeingPlayed ? (CGHStaticData.m_musicianAppearance == 0 ? 1 : 1) : (CGHStaticData.m_musicianAppearance == 0 ? 3 : 3);
          break;
        default:
          this.m_musicianCharacterID = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS != CGHStaticData.m_instrumentBeingPlayed ? (CGHStaticData.m_musicianAppearance == 0 ? 0 : 0) : (CGHStaticData.m_musicianAppearance == 0 ? 0 : 0);
          break;
      }
      SG_Home.GetInstance().QueueArchetypeCharacter(this.m_musicianArchetypeID, this.m_musicianCharacterID);
      SG_Home.GetInstance().QueueArchetypeCharacter(16, 0);
      int index = CGHStaticData.m_venueSelected % this.MAXIMUM_VENUES;
      this.m_venueArchetypeID = this.VENUE_SPRITE_ARCHETYPES[index];
      this.m_venueCharacterID = this.VENUE_SPRITE_CHARACTERS[index];
      this.m_venueAnimationID = this.VENUE_SPRITE_ANIMATIONS[index];
      SG_Home.GetInstance().QueueArchetypeCharacter(this.m_venueArchetypeID, this.m_venueCharacterID);
      SG_Home.GetInstance().QueueArchetypeCharacter(4, 0);
      this.m_highwayBackgroundArchetypeID = CGHStaticData.m_musicianAppearance == 0 ? 15 : 14;
      this.m_highwayBackgroundCharacterID = CGHStaticData.m_musicianAppearance == 0 ? 0 : 0;
      this.m_highwayBackgroundAnimationID = CGHStaticData.m_musicianAppearance == 0 ? 0 : 0;
      SG_Home.GetInstance().QueueArchetypeCharacter(this.m_highwayBackgroundArchetypeID, this.m_highwayBackgroundCharacterID);
      this.m_crowdArchetypeID = 17;
      this.m_crowdCharacterID = this.CROWD_SPRITE_CHARACTERS[CGHStaticData.m_venueSelected % this.MAXIMUM_VENUES];
      this.m_crowdAnimationID = 0;
      SG_Home.GetInstance().QueueArchetypeCharacter(this.m_crowdArchetypeID, this.m_crowdCharacterID);
      SG_Home.GetInstance().QueueArchetypeCharacter(0, 0);
      SG_Home.GetInstance().QueueArchetypeCharacter(0, 1);
      SG_Home.GetInstance().QueueArchetypeCharacter(0, 2);
      SG_Home.GetInstance().QueueArchetypeCharacter(0, 3);
      SG_Home.GetInstance().QueueArchetypeCharacter(1, 0);
      SG_Home.GetInstance().QueueArchetypeCharacter(1, 1);
      SG_Home.GetInstance().QueueArchetypeCharacter(1, 2);
      SG_Home.GetInstance().QueueArchetypeCharacter(18, 0);
      SG_Home.GetInstance().QueueArchetypeCharacter(6, 0);
      SG_Home.GetInstance().QueueArchetypeCharacter(2, 0);
    }

    private bool LoadIngameSprites()
    {
      return !SG_Home.GetInstance().LoadQueued(CResBank.kResMaxTimePerUpdateMS, out bool _);
    }

    private void SetupIngameSprites()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      CGHStaticData.m_guitarTrackOriginPos.m_x = (int) (width >> 1);
      CGHStaticData.m_guitarTrackOriginPos.m_y = (int) height;
      this.m_pMusicianSprite = new SG_Presenter();
      this.m_pMusicianSprite.SetArchetypeCharacter(this.m_musicianArchetypeID, this.m_musicianCharacterID);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
        this.m_pMusicianSprite.SetAnimation(CGHStaticData.m_musicianAppearance == 0 ? 0 : 0);
      else
        this.m_pMusicianSprite.SetAnimation(CGHStaticData.m_musicianAppearance == 0 ? 0 : 0);
      this.m_pMusicianSprite.SetLoop(true);
      this.m_pLeftRail = new SG_Presenter();
      this.m_pLeftRail.SetArchetypeCharacter(4, 0);
      this.m_pLeftRail.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pLeftRail.SetAnimation(0);
      this.m_pRightRail = new SG_Presenter();
      this.m_pRightRail.SetArchetypeCharacter(4, 0);
      this.m_pRightRail.SetAnimation(1);
      this.m_pRightRail.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pRightRail.Bounds(ref this.boundsResult);
      this.m_pLeftNoteTrack = new SG_Presenter();
      this.m_pLeftNoteTrack.SetArchetypeCharacter(4, 0);
      this.m_pLeftNoteTrack.SetAnimation(2);
      this.m_pLeftNoteTrack.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pLeftNoteTrack.Bounds(ref this.boundsResult);
      this.m_pCenterNoteTrack = new SG_Presenter();
      this.m_pCenterNoteTrack.SetArchetypeCharacter(4, 0);
      this.m_pCenterNoteTrack.SetAnimation(3);
      this.m_pCenterNoteTrack.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pCenterNoteTrack.Bounds(ref this.boundsResult);
      this.m_pRightNoteTrack = new SG_Presenter();
      this.m_pRightNoteTrack.SetArchetypeCharacter(4, 0);
      this.m_pRightNoteTrack.SetAnimation(4);
      this.m_pRightNoteTrack.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pRightNoteTrack.Bounds(ref this.boundsResult);
      this.m_pHighwayBackground = new SG_Presenter();
      this.m_pHighwayBackground.SetArchetypeCharacter(this.m_highwayBackgroundArchetypeID, this.m_highwayBackgroundCharacterID);
      this.m_pHighwayBackground.SetAnimation(this.m_highwayBackgroundAnimationID);
      this.m_pCrowdSprite = new SG_Presenter();
      this.m_pCrowdSprite.SetArchetypeCharacter(this.m_crowdArchetypeID, this.m_crowdCharacterID);
      this.m_pCrowdSprite.SetAnimation(this.m_crowdAnimationID, true);
      this.m_pStarPowerOverlaySprite = new SG_Presenter();
      this.m_pStarPowerOverlaySprite.SetArchetypeCharacter(16, 0);
      this.m_pStarPowerOverlaySprite.SetAnimation(0, true);
      this.m_pStageSprite = new SG_Presenter();
      this.m_pStageSprite.SetArchetypeCharacter(this.m_venueArchetypeID, this.m_venueCharacterID);
      this.m_pStageSprite.SetAnimation(this.m_venueAnimationID, true);
      this.m_pStageSprite.SetDrawCallbackPolicy(SG_Defines.DRAW_CALLBACK_SPRITE_PRE);
      this.m_pStageSprite.Bounds(ref this.boundsResult);
      this.m_pStarRatingIcon = new SG_Presenter(3, 0);
      this.m_pStarRatingIcon.SetAnimation(15);
      this.m_pStarRatingIcon.Bounds(ref this.boundsResult);
      this.m_starRatingIconWidth = this.boundsResult.m_dx;
      this.m_starRatingIconHeight = this.boundsResult.m_dy;
      if (this.m_touchscreenSupport)
      {
        this.m_pStarPowerIcon = new SG_Presenter(2, 0);
        this.m_pStarPowerIcon.SetAnimation(15);
      }
      this.m_pNoteSprites = new SG_Presenter[4][];
      for (int index = 0; index < 4; ++index)
      {
        this.m_pNoteSprites[index] = new SG_Presenter[4];
        for (int frameIndexNew = 0; frameIndexNew < 4; ++frameIndexNew)
        {
          this.m_pNoteSprites[index][frameIndexNew] = new SG_Presenter();
          this.m_pNoteSprites[index][frameIndexNew].SetArchetypeCharacter(0, this.NOTE_SPRITE_CHARACTERS[index]);
          this.m_pNoteSprites[index][frameIndexNew].SetAnimation(0);
          this.m_pNoteSprites[index][frameIndexNew].SetFrameIndex(frameIndexNew);
        }
      }
      this.m_pStarNoteSprites = new SG_Presenter[4][];
      for (int index1 = 0; index1 < 4; ++index1)
      {
        this.m_pStarNoteSprites[index1] = new SG_Presenter[4];
        for (int index2 = 0; index2 < 4; ++index2)
        {
          this.m_pStarNoteSprites[index1][index2] = new SG_Presenter();
          this.m_pStarNoteSprites[index1][index2].SetArchetypeCharacter(0, this.NOTE_SPRITE_CHARACTERS[index1]);
          this.m_pStarNoteSprites[index1][index2].SetAnimation(1 + index2);
          this.m_pStarNoteSprites[index1][index2].SetFrameIndex(0);
          this.m_pStarNoteSprites[index1][index2].SetLoop(true);
        }
      }
      this.m_pButtonSprites = new SG_Presenter[4];
      this.m_pTouchButtonSprites = new SG_Presenter[3];
      this.m_pButtonOverlaySprites = new SG_Presenter[4];
      this.m_pButtonStarPowerOverlaySprites = new SG_Presenter[4];
      for (int index = 0; index < 4; ++index)
      {
        this.m_pButtonSprites[index] = new SG_Presenter();
        this.m_pButtonSprites[index].SetArchetypeCharacter(1, this.BUTTON_SPRITE_CHARACTERS[index]);
        if (index < 3)
        {
          this.m_pTouchButtonSprites[index] = new SG_Presenter();
          this.m_pTouchButtonSprites[index].SetArchetypeCharacter(18, 0);
          this.m_pTouchButtonSprites[index].SetAnimation(this.TOUCH_BUTTON_ANIMATIONS[index, this.TOUCH_BUTTON_UP]);
        }
        this.m_pButtonOverlaySprites[index] = new SG_Presenter();
        if (3 == index)
        {
          this.m_pButtonOverlaySprites[index].SetArchetypeCharacter(1, this.BUTTON_SPRITE_CHARACTERS[index]);
          this.m_pButtonOverlaySprites[index].SetAnimation(8);
        }
        else
        {
          this.m_pButtonOverlaySprites[index].SetArchetypeCharacter(1, this.BUTTON_SPRITE_CHARACTERS[index]);
          this.m_pButtonOverlaySprites[index].SetAnimation(2);
        }
        this.m_pButtonStarPowerOverlaySprites[index] = new SG_Presenter();
        this.m_pButtonStarPowerOverlaySprites[index].SetArchetypeCharacter(6, 0);
        this.m_pButtonStarPowerOverlaySprites[index].SetAnimation(0);
      }
      this.m_pButtonSprites[0].SetAnimation(0);
      this.m_pButtonSprites[1].SetAnimation(0);
      this.m_pButtonSprites[2].SetAnimation(0);
      this.m_pButtonSprites[3].SetAnimation(7);
      this.m_pButtonSprites[3].Finish();
      this.InitializeStarParticleEffects();
    }

    private void UnloadIngameSprites()
    {
      this.m_pMusicianSprite = (SG_Presenter) null;
      this.m_pLeftRail = (SG_Presenter) null;
      this.m_pRightRail = (SG_Presenter) null;
      this.m_pLeftNoteTrack = (SG_Presenter) null;
      this.m_pCenterNoteTrack = (SG_Presenter) null;
      this.m_pRightNoteTrack = (SG_Presenter) null;
      this.m_pHighwayBackground = (SG_Presenter) null;
      this.m_pCrowdSprite = (SG_Presenter) null;
      this.m_pStarPowerOverlaySprite = (SG_Presenter) null;
      this.m_pStarPowerIcon = (SG_Presenter) null;
      this.m_pStageSprite = (SG_Presenter) null;
      this.m_pStarRatingIcon = (SG_Presenter) null;
      if (this.m_pNoteSprites != null)
      {
        for (int index1 = 0; index1 < 4; ++index1)
        {
          for (int index2 = 0; index2 < 4; ++index2)
            this.m_pNoteSprites[index1][index2] = (SG_Presenter) null;
          this.m_pNoteSprites[index1] = (SG_Presenter[]) null;
        }
      }
      this.m_pNoteSprites = (SG_Presenter[][]) null;
      if (this.m_pStarNoteSprites != null)
      {
        for (int index3 = 0; index3 < 4; ++index3)
        {
          for (int index4 = 0; index4 < 4; ++index4)
            this.m_pStarNoteSprites[index3][index4] = (SG_Presenter) null;
          this.m_pStarNoteSprites[index3] = (SG_Presenter[]) null;
        }
      }
      this.m_pStarNoteSprites = (SG_Presenter[][]) null;
      for (int index = 0; index < 4; ++index)
      {
        if (this.m_pButtonSprites != null)
          this.m_pButtonSprites[index] = (SG_Presenter) null;
        if (index < 3 && this.m_pTouchButtonSprites != null)
          this.m_pTouchButtonSprites[index] = (SG_Presenter) null;
        if (this.m_pButtonOverlaySprites != null)
          this.m_pButtonOverlaySprites[index] = (SG_Presenter) null;
        if (this.m_pButtonStarPowerOverlaySprites != null)
          this.m_pButtonStarPowerOverlaySprites[index] = (SG_Presenter) null;
      }
      this.m_pButtonSprites = (SG_Presenter[]) null;
      this.m_pTouchButtonSprites = (SG_Presenter[]) null;
      this.m_pButtonOverlaySprites = (SG_Presenter[]) null;
      this.m_pButtonStarPowerOverlaySprites = (SG_Presenter[]) null;
      this.ReleaseStarParticleEffects();
      SG_Home.GetInstance().DumpArchetypeCharacter(this.m_musicianArchetypeID, this.m_musicianCharacterID);
      SG_Home.GetInstance().DumpArchetypeCharacter(16, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(this.m_venueArchetypeID, this.m_venueCharacterID);
      SG_Home.GetInstance().DumpArchetypeCharacter(this.m_crowdArchetypeID, this.m_crowdCharacterID);
      SG_Home.GetInstance().DumpArchetypeCharacter(this.m_highwayBackgroundArchetypeID, this.m_highwayBackgroundCharacterID);
      SG_Home.GetInstance().DumpArchetypeCharacter(4, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(0, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(0, 1);
      SG_Home.GetInstance().DumpArchetypeCharacter(0, 2);
      SG_Home.GetInstance().DumpArchetypeCharacter(0, 3);
      SG_Home.GetInstance().DumpArchetypeCharacter(1, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(1, 1);
      SG_Home.GetInstance().DumpArchetypeCharacter(1, 2);
      SG_Home.GetInstance().DumpArchetypeCharacter(18, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(6, 0);
      SG_Home.GetInstance().DumpArchetypeCharacter(2, 0);
    }

    private void LoadHighwayTrackTexture()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      this.m_highwayTextureSurfaceID = CGHStaticData.m_musicianAppearance == 0 ? "SUR_TRACK_TEXTURE_2" : "SUR_TRACK_TEXTURE_1";
      int resource2 = (int) resourceManager.CreateResource(this.m_highwayTextureSurfaceID, out resource1);
      if (resource1 == null)
        return;
      this.m_pTrackBackgroundTexture = (ICRenderSurface) resource1.GetData();
    }

    private void UnloadHighwayTrackTexture()
    {
      if (this.m_highwayTextureSurfaceID != null)
        CApp.GetResourceManager().DestroyResource(this.m_highwayTextureSurfaceID);
      this.m_pTrackBackgroundTexture = (ICRenderSurface) null;
    }

    private void LoadGameSounds()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CGHStaticData.m_pCrowdCheerSFX = (CMedia) null;
      CGHStaticData.m_pMissedNoteSFX = (CMedia) null;
      CGHStaticData.m_pGuitarIntroSFX = (CMedia) null;
      CGHStaticData.m_pYouRockOutroSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerDeploySFX = (CMedia) null;
      CGHStaticData.m_pStarPowerDepletedSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAwardedSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAwarded2SFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAvailableSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNeutralToPositiveSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNeutralToNegativeSFX = (CMedia) null;
      CGHStaticData.m_pCrowdPositiveToNeutralSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNegativeToNeutralSFX = (CMedia) null;
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource("IDM_CROWD_CHEER_SFX", out resource1);
      if (resource1 != null)
        CGHStaticData.m_pCrowdCheerSFX = (CMedia) resource1.GetData();
      CResource resource3 = (CResource) null;
      int resource4 = (int) resourceManager.CreateResource(CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed ? "IDM_BAD_NOTE_DRUMS_SFX" : (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS == CGHStaticData.m_instrumentBeingPlayed ? "IDM_BAD_NOTE_BASS_SFX" : "IDM_BAD_NOTE_GUITAR_SFX"), out resource3);
      if (resource3 != null)
        CGHStaticData.m_pMissedNoteSFX = (CMedia) resource3.GetData();
      CResource resource5 = (CResource) null;
      int resource6 = (int) resourceManager.CreateResource("IDM_HIGHWAY_RISE_SFX", out resource5);
      if (resource5 != null)
        CGHStaticData.m_pGuitarIntroSFX = (CMedia) resource5.GetData();
      CResource resource7 = (CResource) null;
      int resource8 = (int) resourceManager.CreateResource("IDM_YOU_ROCK_SFX", out resource7);
      if (resource7 != null)
        CGHStaticData.m_pYouRockOutroSFX = (CMedia) resource7.GetData();
      CResource resource9 = (CResource) null;
      int resource10 = (int) resourceManager.CreateResource("IDM_STAR_POWER_AWARDED_SFX", out resource9);
      if (resource9 != null)
        CGHStaticData.m_pStarPowerAwardedSFX = (CMedia) resource9.GetData();
      CResource resource11 = (CResource) null;
      int resource12 = (int) resourceManager.CreateResource("IDM_STAR_POWER_AWARDED_2_SFX", out resource11);
      if (resource11 != null)
        CGHStaticData.m_pStarPowerAwarded2SFX = (CMedia) resource11.GetData();
      CResource resource13 = (CResource) null;
      int resource14 = (int) resourceManager.CreateResource("IDM_STAR_POWER_AVAILABLE_SFX", out resource13);
      if (resource13 != null)
        CGHStaticData.m_pStarPowerAvailableSFX = (CMedia) resource13.GetData();
      CResource resource15 = (CResource) null;
      int resource16 = (int) resourceManager.CreateResource("IDM_STAR_POWER_DEPLOY_SFX", out resource15);
      if (resource15 != null)
        CGHStaticData.m_pStarPowerDeploySFX = (CMedia) resource15.GetData();
      CResource resource17 = (CResource) null;
      int resource18 = (int) resourceManager.CreateResource("IDM_STAR_POWER_DEPLETED_SFX", out resource17);
      if (resource17 != null)
        CGHStaticData.m_pStarPowerDepletedSFX = (CMedia) resource17.GetData();
      CResource resource19 = (CResource) null;
      int resource20 = (int) resourceManager.CreateResource("IDM_CROWD_NEUTRAL_TO_POSITIVE_SFX", out resource19);
      if (resource19 != null)
        CGHStaticData.m_pCrowdNeutralToPositiveSFX = (CMedia) resource19.GetData();
      CResource resource21 = (CResource) null;
      int resource22 = (int) resourceManager.CreateResource("IDM_CROWD_NEUTRAL_TO_NEGATIVE_SFX", out resource21);
      if (resource21 != null)
        CGHStaticData.m_pCrowdNeutralToNegativeSFX = (CMedia) resource21.GetData();
      CResource resource23 = (CResource) null;
      int resource24 = (int) resourceManager.CreateResource("IDM_CROWD_POSITIVE_TO_NEUTRAL_SFX", out resource23);
      if (resource23 != null)
        CGHStaticData.m_pCrowdPositiveToNeutralSFX = (CMedia) resource23.GetData();
      CResource resource25 = (CResource) null;
      int resource26 = (int) resourceManager.CreateResource("IDM_CROWD_NEGATIVE_TO_NEUTRAL_SFX", out resource25);
      if (resource25 == null)
        return;
      CGHStaticData.m_pCrowdNegativeToNeutralSFX = (CMedia) resource25.GetData();
    }

    private void UnloadGameSounds()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.DestroyResource("IDM_YOU_ROCK_SFX");
      resourceManager.DestroyResource("IDM_CROWD_NEUTRAL_TO_POSITIVE_SFX");
      CGHStaticData.m_pCrowdCheerSFX = (CMedia) null;
      CGHStaticData.m_pMissedNoteSFX = (CMedia) null;
      CGHStaticData.m_pGuitarIntroSFX = (CMedia) null;
      CGHStaticData.m_pYouRockOutroSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerDeploySFX = (CMedia) null;
      CGHStaticData.m_pStarPowerDepletedSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAwardedSFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAwarded2SFX = (CMedia) null;
      CGHStaticData.m_pStarPowerAvailableSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNeutralToPositiveSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNeutralToNegativeSFX = (CMedia) null;
      CGHStaticData.m_pCrowdPositiveToNeutralSFX = (CMedia) null;
      CGHStaticData.m_pCrowdNegativeToNeutralSFX = (CMedia) null;
    }

    public static void SetLoadingTip()
    {
      CGHStaticData.m_loadingScreenTipResourceID = "IDS_LOADING_TIP_" + new Random().Next(20).ToString("D2");
    }

    public static void ClearLoadingTip()
    {
      CGHStaticData.m_loadingScreenTipResourceID = (string) null;
    }

    private void InitializeLayoutCoordinates()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      int num1 = (int) height;
      int num2 = (int) (width >> 1);
      CGHStaticData.m_guitarTrackOriginPos.m_x = (int) (width >> 1);
      CGHStaticData.m_guitarTrackOriginPos.m_y = (int) height;
      SG_Presenter sgPresenter = new SG_Presenter();
      sgPresenter.SetArchetypeCharacter(this.m_musicianArchetypeID, this.m_musicianCharacterID);
      if (CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed)
      {
        if (CGHStaticData.m_musicianAppearance == 0)
          sgPresenter.SetAnimation(0, true);
        else
          sgPresenter.SetAnimation(0, true);
      }
      else if (CGHStaticData.m_musicianAppearance == 0)
        sgPresenter.SetAnimation(0, true);
      else
        sgPresenter.SetAnimation(0, true);
      sgPresenter.Bounds(ref this.rockerRect);
      this.m_stageNudge = CMath.Max(0, this.rockerRect.m_dy - (CGHStaticData.m_guitarPlayerAnchorPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y));
      this.m_pLeftRail.Bounds(ref this.boundsResult);
      this.m_guitarLeftTrackRailPos.m_x = (int) (width >> 1);
      this.m_guitarLeftTrackRailPos.m_y = (int) height;
      this.m_pLeftRail.Bounds(ref this.boundsResult);
      this.m_guitarRightTrackRailPos.m_x = (int) (width >> 1);
      this.m_guitarRightTrackRailPos.m_y = (int) height;
      this.m_heightOfNoteTrack = CGHStaticData.m_noteTrackEndCenterPosition.m_y - CGHStaticData.m_noteTrackStartCenterPosition.m_y;
      int num3 = CGHStaticData.m_guitarTrackOriginPos.m_y - (CGHStaticData.m_noteTrackEndCenterPosition.m_y - CGHStaticData.m_noteTrackStartCenterPosition.m_y);
      this.m_heightOfTrackRails = this.m_heightOfNoteTrack;
      this.m_heightOfTrackRails = CGHStaticData.m_trackEdgeBottomRight.m_y - CGHStaticData.m_trackEdgeTopRight.m_y + 1;
      this.m_guitarPlayFieldTopWidth = CGHStaticData.m_noteTrackStartRightPosition.m_x - CGHStaticData.m_noteTrackStartLeftPosition.m_x;
      this.m_guitarPlayFieldBottomWidth = CGHStaticData.m_noteTrackEndRightPosition.m_x - CGHStaticData.m_noteTrackEndLeftPosition.m_x;
      int num4 = this.m_guitarPlayFieldTopWidth >> 1;
      int num5 = this.m_guitarPlayFieldBottomWidth >> 1;
      this.m_guitarPlayFieldTopLeft.m_x = num2 - num4;
      this.m_guitarPlayFieldTopLeft.m_y = num3;
      this.m_guitarPlayFieldTopCenter.m_x = num2;
      this.m_guitarPlayFieldTopCenter.m_y = num3;
      this.m_guitarPlayFieldTopRight.m_x = num2 + num4;
      this.m_guitarPlayFieldTopRight.m_y = num3;
      this.m_guitarPlayFieldBottomLeft.m_x = num2 - num5;
      this.m_guitarPlayFieldBottomLeft.m_y = num1;
      this.m_guitarPlayFieldBottomCenter.m_x = num2;
      this.m_guitarPlayFieldBottomCenter.m_y = num1;
      this.m_guitarPlayFieldBottomRight.m_x = num2 + num5;
      this.m_guitarPlayFieldBottomRight.m_y = num1;
      int[] numArray = new int[240];
      int v1 = 200;
      int val1_1 = CMathFixed.Div(CMathFixed.Int32ToFixed(this.m_heightOfNoteTrack), CMathFixed.Int32ToFixed(239));
      int num6 = CMathFixed.Int32ToFixed(v1);
      for (int v2 = 0; v2 < 240; ++v2)
        numArray[v2] = CMathFixed.Div(CMathFixed.Mul(CMathFixed.Mul(val1_1, CMathFixed.Int32ToFixed(v2)), CMathFixed.Div(num6, CMathFixed.Int32ToFixed(v1 + 240 - 1 - v2))), CMathFixed.Int32ToFixed(this.m_heightOfNoteTrack));
      this.m_pNoteRowHorizontalFixedWidth = new int[240];
      this.m_pNoteRowHorizontalFixedOffset = new int[240];
      this.m_pTrackRowHorizontalFixedWidth = new int[240];
      int val1_2 = CMathFixed.Int32ToFixed(this.m_heightOfNoteTrack);
      int val1_3 = CMathFixed.Int32ToFixed(this.m_guitarPlayFieldBottomWidth - this.m_guitarPlayFieldTopWidth >> 1);
      int val1_4 = CMathFixed.Int32ToFixed(2 * (CGHStaticData.m_trackEdgeBottomRight.m_x - CGHStaticData.m_trackEdgeTopRight.m_x + 1));
      int v3 = CGHStaticData.m_trackEdgeTopRight.m_x * 2;
      for (int index = 0; index < 240; ++index)
      {
        this.m_pNoteRowVerticalFixedOffset[index] = CMathFixed.Mul(val1_2, numArray[index]);
        this.m_pNoteRowVerticalFixedHeight[index] = index == 0 ? 0 : this.m_pNoteRowVerticalFixedOffset[index] - this.m_pNoteRowVerticalFixedOffset[index - 1];
        this.m_pNoteRowHorizontalFixedOffset[index] = CMathFixed.Mul(val1_3, numArray[index]);
        this.m_pNoteRowHorizontalFixedWidth[index] = index == 0 ? 0 : this.m_pNoteRowHorizontalFixedOffset[index] - this.m_pNoteRowHorizontalFixedOffset[index - 1];
        this.m_pTrackRowHorizontalFixedWidth[index] = CMathFixed.Mul(val1_4, numArray[index]);
        this.m_pTrackRowHorizontalFixedWidth[index] += CMathFixed.Int32ToFixed(v3);
      }
      this.m_pInstrumentButtonPositions[0].m_x = CGHStaticData.m_noteButtonLeftPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pInstrumentButtonPositions[0].m_y = CGHStaticData.m_noteButtonLeftPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pInstrumentButtonPositions[1].m_x = CGHStaticData.m_noteButtonCenterPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pInstrumentButtonPositions[1].m_y = CGHStaticData.m_noteButtonCenterPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pInstrumentButtonPositions[2].m_x = CGHStaticData.m_noteButtonRightPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pInstrumentButtonPositions[2].m_y = CGHStaticData.m_noteButtonRightPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pTouchButtonPositions[0].m_x = CGHStaticData.m_touchButtonLeftPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pTouchButtonPositions[0].m_y = CGHStaticData.m_touchButtonLeftPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pTouchButtonPositions[1].m_x = CGHStaticData.m_touchButtonCenterPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pTouchButtonPositions[1].m_y = CGHStaticData.m_touchButtonCenterPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pTouchButtonPositions[2].m_x = CGHStaticData.m_touchButtonRightPosition.m_x + CGHStaticData.m_guitarTrackOriginPos.m_x;
      this.m_pTouchButtonPositions[2].m_y = CGHStaticData.m_touchButtonRightPosition.m_y + CGHStaticData.m_guitarTrackOriginPos.m_y;
      this.m_pInstrumentButtonPositions[3].m_x = this.m_pInstrumentButtonPositions[1].m_x;
      this.m_pInstrumentButtonPositions[3].m_y = this.m_pInstrumentButtonPositions[1].m_y;
      int y = this.m_pInstrumentButtonPositions[1].m_y;
      int index1 = 239;
      while (CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index1]) + num3 >= y && index1 > 0)
        --index1;
      this.m_noteDivisionOffsetOfInstrumentButtons = 240 - index1;
      this.m_pPixelRowTableReferenceIndex = new int[this.m_heightOfNoteTrack];
      for (int index2 = 0; index2 < this.m_heightOfNoteTrack; ++index2)
      {
        this.m_pPixelRowTableReferenceIndex[index2] = 239;
        for (int index3 = 0; index3 < 240; ++index3)
        {
          if (index2 <= CMathFixed.FixedToInt32(this.m_pNoteRowVerticalFixedOffset[index3]))
          {
            this.m_pPixelRowTableReferenceIndex[index2] = index3;
            break;
          }
        }
      }
      if (!this.m_touchscreenSupport)
        return;
      this.m_pButtonSprites[0].Bounds(ref this.buttonRect);
      int num7 = 0;
      this.m_pTouchButtonSprites[0].Bounds(ref this.touchButtonRect);
      if (Consts.SWIPE_SUPPORTED)
        num7 = this.m_pTouchButtonPositions[0].m_y + this.touchButtonRect.m_dy - (this.m_pInstrumentButtonPositions[0].m_y + this.buttonRect.m_dy);
      this.buttonRect.m_dx += 10;
      this.buttonRect.m_dy += 50;
      this.m_touchRowTop = this.m_pInstrumentButtonPositions[0].m_y - (this.buttonRect.m_dy >> 1);
      this.m_touchRowBottom = this.m_pInstrumentButtonPositions[0].m_y + (this.buttonRect.m_dy >> 1) + num7;
      this.m_touchButton1Left = this.m_pInstrumentButtonPositions[0].m_x - (this.buttonRect.m_dx >> 1);
      this.m_touchButton1Right = this.m_pInstrumentButtonPositions[0].m_x + (this.buttonRect.m_dx >> 1);
      this.m_touchButton2Left = this.m_pInstrumentButtonPositions[1].m_x - (this.buttonRect.m_dx >> 1);
      this.m_touchButton2Right = this.m_pInstrumentButtonPositions[1].m_x + (this.buttonRect.m_dx >> 1);
      this.m_touchButton3Left = this.m_pInstrumentButtonPositions[2].m_x - (this.buttonRect.m_dx >> 1);
      this.m_touchButton3Right = this.m_pInstrumentButtonPositions[2].m_x + (this.buttonRect.m_dx >> 1);
      this.m_pStarPowerIcon.Bounds(ref this.buttonRect);
      if (this.buttonRect.m_dx > CGHStaticData.m_starPowerAnchorPostion.m_x + this.m_guitarLeftTrackRailPos.m_x)
      {
        this.m_pStarPowerIcon.SetPositionX((short) (CGHStaticData.m_starPowerAnchorPostion.m_x + this.m_guitarLeftTrackRailPos.m_x - (this.buttonRect.m_dx >> 1)));
        this.m_pStarPowerIcon.SetPositionY((short) (CGHStaticData.m_starPowerAnchorPostion.m_y + this.m_guitarLeftTrackRailPos.m_y));
      }
      else
      {
        int num8 = CMathFixed.Int32ToFixed(CGHStaticData.m_trackEdgeBottomRight.m_y - CGHStaticData.m_trackEdgeTopRight.m_y) / (CGHStaticData.m_trackEdgeBottomRight.m_x - CGHStaticData.m_trackEdgeTopRight.m_x);
        int num9 = this.m_guitarLeftTrackRailPos.m_x - CGHStaticData.m_trackEdgeBottomRight.m_x;
        int num10 = CMathFixed.Int32ToFixed((int) Phone.GetHeight() - (CGHStaticData.m_starPowerAnchorPostion.m_y + this.m_guitarLeftTrackRailPos.m_y)) / num8;
        int num11 = num9 + num10 - (CGHStaticData.m_starPowerAnchorPostion.m_x + this.m_guitarLeftTrackRailPos.m_x);
        int num12 = this.buttonRect.m_dx + num11 - num9;
        int int32 = CMathFixed.FixedToInt32(num8 * num12);
        if (int32 < 0)
        {
          this.m_pStarPowerIcon.SetPositionX((short) (num9 >> 1));
          short num13 = -45;
          this.m_pStarPowerIcon.SetPositionY((short) ((int) Phone.GetHeight() - (this.buttonRect.m_dy >> 1) + (int) num13));
        }
        else
        {
          this.m_pStarPowerIcon.SetPositionX((short) (num9 + num12 - num11 - (this.buttonRect.m_dx >> 1)));
          this.m_pStarPowerIcon.SetPositionY((short) CMath.Max((int) Phone.GetHeight() - int32, (int) Phone.GetHeight() - (this.buttonRect.m_dy >> 1)));
        }
      }
    }

    private void InitializeDefaultLayoutCoordinates()
    {
      ICGraphics instance = ICGraphics.GetInstance();
      ICGraphics2d.GetInstance();
      uint width;
      uint height;
      instance.GetTargetSurface().GetWidthAndHeight(out width, out height);
      int num1 = (int) (width >> 1);
      int num2 = (int) (width >> 2);
      int num3 = (int) (height >> 2);
      int v1 = -(num3 >> 1);
      CGHStaticData.m_noteTrackStartLeftPosition.m_x = num1 - (num2 >> 2);
      CGHStaticData.m_noteTrackStartLeftPosition.m_y = num3;
      CGHStaticData.m_noteTrackStartCenterPosition.m_x = num1;
      CGHStaticData.m_noteTrackStartCenterPosition.m_y = num3;
      CGHStaticData.m_noteTrackStartRightPosition.m_x = num1 + (num2 >> 2);
      CGHStaticData.m_noteTrackStartRightPosition.m_y = num3;
      CGHStaticData.m_noteTrackEndLeftPosition.m_x = num2 - (num2 >> 3);
      CGHStaticData.m_noteTrackEndLeftPosition.m_y = (int) height;
      CGHStaticData.m_noteTrackEndCenterPosition.m_x = num1;
      CGHStaticData.m_noteTrackEndCenterPosition.m_y = (int) height;
      CGHStaticData.m_noteTrackEndRightPosition.m_x = num1 + num2 + (num2 >> 3);
      CGHStaticData.m_noteTrackEndRightPosition.m_y = (int) height;
      int int32_1 = CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(CGHStaticData.m_noteTrackStartLeftPosition.m_x - CGHStaticData.m_noteTrackEndLeftPosition.m_x + 1), CMathFixed.Div(CMathFixed.Int32ToFixed(v1), CMathFixed.Int32ToFixed(CGHStaticData.m_noteTrackStartLeftPosition.m_y - CGHStaticData.m_noteTrackEndLeftPosition.m_y + 1))));
      CGHStaticData.m_noteButtonLeftPosition.m_x = -(num1 - (CGHStaticData.m_noteTrackEndLeftPosition.m_x + int32_1));
      CGHStaticData.m_noteButtonLeftPosition.m_y = v1;
      CGHStaticData.m_noteButtonCenterPosition.m_x = 0;
      CGHStaticData.m_noteButtonCenterPosition.m_y = v1;
      CGHStaticData.m_noteButtonRightPosition.m_x = num1 - (CGHStaticData.m_noteTrackEndLeftPosition.m_x + int32_1);
      CGHStaticData.m_noteButtonRightPosition.m_y = v1;
      CGHStaticData.m_trackEdgeTopRight.m_x = (num2 >> 2) + (num2 >> 3);
      CGHStaticData.m_trackEdgeTopRight.m_y = num3;
      CGHStaticData.m_trackEdgeBottomRight.m_x = num2 + (num2 >> 1);
      CGHStaticData.m_trackEdgeBottomRight.m_y = (int) height;
      int v2 = CGHStaticData.m_trackEdgeBottomRight.m_y - CGHStaticData.m_trackEdgeTopRight.m_y + 1 >> 1;
      int int32_2 = CMathFixed.FixedToInt32(CMathFixed.Mul(CMathFixed.Int32ToFixed(CGHStaticData.m_trackEdgeTopRight.m_x - CGHStaticData.m_trackEdgeBottomRight.m_x + 1), CMathFixed.Div(CMathFixed.Int32ToFixed(v2), CMathFixed.Int32ToFixed(CGHStaticData.m_trackEdgeTopRight.m_y - CGHStaticData.m_trackEdgeBottomRight.m_y + 1))));
      CGHStaticData.m_multiMeterAnchorPosition.m_x = CGHStaticData.m_trackEdgeBottomRight.m_x - int32_2;
      CGHStaticData.m_multiMeterAnchorPosition.m_y = -v2;
    }

    private void InitIngameVariables()
    {
      this.m_pInstrumentButtonPositions = new CGHStaticData.s2dCoord[4];
      this.m_pTouchButtonPositions = new CGHStaticData.s2dCoord[4];
      this.m_pNoteRowVerticalFixedHeight = new int[240];
      this.m_pNoteRowVerticalFixedOffset = new int[240];
      this.m_pSustainNotesPositionInScore = new CGuitarHeroGame.sSustainNoteInfo[4];
    }

    private void ResetIngameVariables()
    {
      for (int index = 0; index < 4; ++index)
        this.m_pSustainNotesPositionInScore[index].m_musicScoreOffset = -1;
      for (int index = 0; index < 4; ++index)
      {
        this.m_buttonState[index] = CGuitarHeroGame.eButtonState.BUTTON_STATE_RELEASED;
        this.m_buttonOverlayStateFlags[index] = 0;
      }
      this.m_pButtonSprites[0].SetAnimation(0);
      this.m_pButtonSprites[1].SetAnimation(0);
      this.m_pButtonSprites[2].SetAnimation(0);
      this.m_pButtonSprites[3].SetAnimation(7);
      this.m_pButtonSprites[3].Finish();
      this.m_pTouchButtonSprites[0].SetAnimation(this.TOUCH_BUTTON_ANIMATIONS[0, this.TOUCH_BUTTON_UP]);
      this.m_pTouchButtonSprites[1].SetAnimation(this.TOUCH_BUTTON_ANIMATIONS[1, this.TOUCH_BUTTON_UP]);
      this.m_pTouchButtonSprites[2].SetAnimation(this.TOUCH_BUTTON_ANIMATIONS[2, this.TOUCH_BUTTON_UP]);
      CGHStaticData.m_score = 0;
      CGHStaticData.m_sustainScoreFractionalRemainder = 0;
      CGHStaticData.m_scoreMultiplier = 1;
      CGHStaticData.m_scoreMultiplierStep = 0;
      this.m_noteScrollOffset = 0;
      this.m_musicScoreOffset = 0;
      this.m_currentBarOffset = 0;
      this.m_previousMusicScoreOffset = this.m_musicScoreOffset;
      this.m_songPlaybackPositionMS = -CGameApp.m_noteDelay;
      CGHStaticData.m_hitNoteCount = 0;
      CGHStaticData.m_missedNoteCount = 0;
      CGHStaticData.m_badNoteCount = 0;
      CGHStaticData.m_hitNoteGroupCount = 0;
      CGHStaticData.m_missedNoteGroupCount = 0;
      CGHStaticData.m_percentageNotesHit = 0;
      CGHStaticData.m_starRating = 0;
      CGHStaticData.m_currentConsecutiveNotesHit = 0;
      CGHStaticData.m_maximumConsecutiveNotesHit = 0;
      this.m_totalConsecutiveStarPowerNotesHit = 0;
      this.m_totalStarPowerPhrasesCompleted = 0;
      this.m_totalStarPowerPhrases = 0;
      CGHStaticData.m_bStarPowerScoringActivated = false;
      CGHStaticData.m_totalTimesStarPowerDeployed = 0;
      this.m_starPowerScore = 0;
      this.ClearScoreMultiplierStatistics();
      this.m_noteInputPreForgiveness = 24;
      this.m_noteInputPostForgiveness = 36;
      this.m_bPlayingDrumsOrBass = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_DRUMS == CGHStaticData.m_instrumentBeingPlayed || CGHStaticData.eGameInstrument.GAME_INSTRUMENT_BASS == CGHStaticData.m_instrumentBeingPlayed;
      CGHStaticData.m_rocker_animation_state = 0;
      CGHStaticData.m_bPlayingSong = false;
      CGHStaticData.m_bPlayingInstrument = false;
      CGHStaticData.m_bResumeAudioFromSuspend = false;
      this.m_songAudioPlaybackID = 0;
      this.m_instrumentAudioPlaybackID = 0;
      this.m_missedSoundMuteTimeMS = 0;
      this.m_missedSoundVolumePercentage = this.MAX_FADE_VOLUME_PERCENTAGE;
      this.m_bFadingDownAudioTrack = false;
      this.m_bFadingUpAudioTrack = false;
      this.m_gameInput = 0;
      this.m_moveWithPointer = false;
      this.m_lastPointerX = 0;
      this.m_lastPointerY = 0;
      CGHStaticData.m_timer = 0;
      CGHStaticData.m_songState = 0;
      CGHStaticData.m_rocker_animation_state = 1;
    }

    private void FreeIngameVariables()
    {
      this.m_pInstrumentButtonPositions = (CGHStaticData.s2dCoord[]) null;
      this.m_pTouchButtonPositions = (CGHStaticData.s2dCoord[]) null;
      this.m_pNoteRowVerticalFixedHeight = (int[]) null;
      this.m_pNoteRowVerticalFixedOffset = (int[]) null;
      this.m_pNoteRowHorizontalFixedWidth = (int[]) null;
      this.m_pNoteRowHorizontalFixedOffset = (int[]) null;
      this.m_pPixelRowTableReferenceIndex = (int[]) null;
      this.m_pTrackRowHorizontalFixedWidth = (int[]) null;
      this.m_pSustainNotesPositionInScore = (CGuitarHeroGame.sSustainNoteInfo[]) null;
    }

    private void NewState(CGuitarHeroGame.eGameState newGameState)
    {
      CGHStaticData.m_state = (int) newGameState;
      CGHStaticData.m_timer = 0;
      switch (newGameState)
      {
        case CGuitarHeroGame.eGameState.GAME_STATE_LOADING:
          this.loadingDone = false;
          //new Thread(new ThreadStart(this.LoadGame)).Start();
          Task.Run(() => this.LoadGame());//Task.Run(new Action(this.LoadGame));
          break;
        case CGuitarHeroGame.eGameState.GAME_STATE_TITLE:
          CGHStaticData.m_timer = 2000;
          break;
        case CGuitarHeroGame.eGameState.GAME_STATE_ANIMATE_HUD_ON:
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pGuitarIntroSFX);
          break;
        case CGuitarHeroGame.eGameState.GAME_STATE_RESUME_COUNTDOWN:
          CGHStaticData.m_timer = 1500;
          break;
        case CGuitarHeroGame.eGameState.GAME_STATE_YOU_ROCK:
          CGHStaticData.m_timer = 4000;
          int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pYouRockOutroSFX);
          break;
        case CGuitarHeroGame.eGameState.GAME_STATE_DEMO_ACHIEVEMENT:
          CGHStaticData.m_timer = 4000;
          break;
      }
    }

    private bool IsFourthNoteNearest(CGuitarHeroGame.eButtonID button)
    {
      int val1 = this.m_songLengthInQuarterNotes * 48;
      for (int index1 = 0; index1 < 48; ++index1)
      {
        int index2 = CMath.Max(0, this.m_musicScoreOffset - index1);
        int index3 = CMath.Min(val1, this.m_musicScoreOffset + index1);
        if (((int) this.m_pMusicScore[3][index2] & 9) != 0 && ((int) this.m_pMusicScore[3][index2] & 128) == 0 || ((int) this.m_pMusicScore[3][index3] & 9) != 0 && ((int) this.m_pMusicScore[3][index3] & 128) == 0)
          return true;
        if (((int) this.m_pMusicScore[(int) button][index2] & 9) != 0 && ((int) this.m_pMusicScore[(int) button][index2] & 128) == 0 || ((int) this.m_pMusicScore[(int) button][index3] & 9) != 0 && ((int) this.m_pMusicScore[(int) button][index3] & 128) == 0)
          return false;
      }
      return false;
    }

    private bool IsNotePlayable(CGuitarHeroGame.eButtonID button)
    {
      int num1 = CMath.Max(0, this.m_musicScoreOffset - this.m_noteInputPostForgiveness);
      for (int musicScoreOffset = this.m_musicScoreOffset; musicScoreOffset >= num1; --musicScoreOffset)
      {
        if (((int) this.m_pMusicScore[(int) button][musicScoreOffset] & 9) != 0 && ((int) this.m_pMusicScore[(int) button][musicScoreOffset] & 128) == 0)
          return true;
      }
      int num2 = CMath.Min(this.m_songLengthInQuarterNotes * 48, this.m_musicScoreOffset + this.m_noteInputPreForgiveness);
      for (int index = this.m_musicScoreOffset + 1; index <= num2; ++index)
      {
        if (((int) this.m_pMusicScore[(int) button][index] & 9) != 0 && ((int) this.m_pMusicScore[(int) button][index] & 128) == 0)
          return true;
      }
      return false;
    }

    private void setPointer()
    {
      this.m_pointerX = this.m_realPointerX;
      this.m_pointerY = this.m_realPointerY;
      this.m_bPointerActive = this.m_bRealPointerActive;
    }

    private void pointerPressed(int x, int y)
    {
      this.m_realPointerX = x;
      this.m_realPointerY = y;
      if (this.m_bPointerActive && this.m_bRealPointerActive)
        this.m_touchButtonHeld = CGuitarHeroGame.eButtonID.BUTTON_INVALID;
      this.m_bRealPointerActive = true;
      
      // Включаем видимость курсора мыши
      com.glu.shared.MouseInput.MouseVisible = true;
    }

    private void pointerReleased(int x, int y)
    {
      this.m_realPointerX = x;
      this.m_realPointerY = y;
      if (!this.m_bPointerActive)
        this.m_bJustReleaseRecieved = true;
      this.m_bRealPointerActive = false;
    }

    private void pointerDragged(int x, int y)
    {
      this.m_realPointerX = x;
      this.m_realPointerY = y;
    }

    public enum eNotePlayedState
    {
      NOTE_PLAYED_NONE,
      NOTE_PLAYED_NORMAL,
      NOTE_PLAYED_SUSTAIN,
    }

    public enum eSongNoteDataID
    {
      SONG_DATA_NOTE_01 = 0,
      SONG_DATA_NOTE_02 = 1,
      SONG_DATA_NOTE_03 = 2,
      SONG_DATA_NOTE_04 = 3,
      SONG_DATA_NOTE_05 = 4,
      SONG_DATA_NOTE_STAR_POWER_PHRASE = 7,
      SONG_DATA_NOTE_PLAYER1_SECTION = 9,
      SONG_DATA_NOTE_PLAYER2_SECTION = 10, // 0x0000000A
    }

    public enum eNoteID
    {
      NOTE_1 = 0,
      NOTE_2 = 1,
      NOTE_3 = 2,
      NOTE_DRUM = 3,
      NOTE_SP = 3,
      TOTAL_INSTRUMENT_NOTES_WITHOUT_DRUMS_OR_BASS = 3,
      TOTAL_INSTRUMENT_NOTES = 4,
      TOTAL_INSTRUMENT_NOTES_WITH_DRUMS_OR_BASS = 4,
    }

    public enum eNoteStatus
    {
      NOTE_OFF = 0,
      NOTE_ON = 1,
      NOTE_HELD = 2,
      NOTE_ACTIVE_MASK = 3,
      NOTE_SUSTAIN = 4,
      NOTE_SUSTAIN_ACTIVE = 8,
      NOTE_SUSTAIN_ACTIVE_MASK = 12, // 0x0000000C
      NOTE_STAR_POWER_END = 16, // 0x00000010
      NOTE_STAR_POWER_PHRASE_BOUNDARY_MARKER = 16, // 0x00000010
      NOTE_STAR_POWER = 32, // 0x00000020
      NOTE_STAR_POWER_MASK = 48, // 0x00000030
      NOTE_STAR_POWER_START = 48, // 0x00000030
      NOTE_STAR_POWER_START_END_MASK = 48, // 0x00000030
      NOTE_STAR_POWER_ACTIVE = 64, // 0x00000040
      NOTE_PLAYED = 128, // 0x00000080
    }

    public enum eButtonID
    {
      BUTTON_INVALID = -1, // 0xFFFFFFFF
      BUTTON_1 = 0,
      BUTTON_2 = 1,
      BUTTON_3 = 2,
      BUTTON_4 = 3,
      BUTTON_DRUMS = 3,
    }

    public enum eButtonState
    {
      BUTTON_STATE_RELEASED,
      BUTTON_STATE_PRESSED,
      BUTTON_STATE_HELD,
    }

    public enum eButtonOverlayStateFlag
    {
      BUTTON_OVERLAY_NONE,
      BUTTON_OVERLAY_FLAME,
      BUTTON_OVERLAY_SUSTAIN,
      BUTTON_OVERLAY_PLAYING,
      BUTTON_OVERLAY_STAR_POWER,
    }

    public enum eRowID
    {
      TOP_ROW,
      MIDDLE_ROW,
      BOTTOM_ROW,
      ZERO_ROW,
    }

    public enum eRenderState
    {
      RM_NOCOLORKEYTEST_OR_NOALPHATEST_NOBLENDING,
      RM_COLORKEYTEST_OR_ALPHATEST_NOBLENDING,
      RM_COLORKEYTEST_OR_ALPHATEST_CONSTALPHAINVCONSTALPHAADD,
      RM_COLORKEYTEST_OR_ALPHATEST_CONSTALPHAONEADD,
      RM_COLORKEYTEST_OR_ALPHATEST_ONEONEADD,
      RM_LAST,
    }

    public enum eGameInput
    {
      GAME_INPUT_NONE,
      GAME_INPUT_UP,
      GAME_INPUT_DOWN,
      GAME_INPUT_LEFT,
      GAME_INPUT_RIGHT,
      GAME_INPUT_FIRE,
      GAME_INPUT_LAST,
    }

    public enum eGameState
    {
      GAME_STATE_LOADING,
      GAME_STATE_LOADING_COMPLETE,
      GAME_STATE_TITLE,
      GAME_STATE_ANIMATE_HUD_ON,
      GAME_STATE_PLAY,
      GAME_STATE_RESUME_COUNTDOWN,
      GAME_STATE_GAMEOVER,
      GAME_STATE_COMPLETE,
      GAME_STATE_ACHIEVEMENT_UNLOCKED,
      GAME_STATE_ENCORE_UNLOCKED,
      GAME_STATE_YOU_ROCK,
      GAME_STATE_ZUNE_RUNNING,
      GAME_STATE_DEMO_ACHIEVEMENT,
    }

    public enum eSongState
    {
      SONG_STATE_PLAYING,
      SONG_STATE_PASSED,
      SONG_STATE_FAILED,
    }

    public enum eRockerAnimationState
    {
      ROCKER_ANIMATION_IDLE,
      ROCKER_ANIMATION_START_IDLE,
      ROCKER_ANIMATION_START_FLAIR,
      ROCKER_ANIMATION_FLAIRING,
      ROCKER_ANIMATION_START_STARPOWER,
      ROCKER_ANIMATION_STARPOWERING,
    }

    public enum eAchievements
    {
      ACHIEVEMENT_COMPLETE_SONG_GUITAR,
      ACHIEVEMENT_TWO_HUNDRED_NOTES,
      ACHIEVEMENT_COMPLETE_SAN_FRANCISCO,
      ACHIEVEMENT_COMPLETE_NEW_YORK,
      ACHIEVEMENT_COMPLETE_CAIRO,
      ACHIEVEMENT_PERFECT_GUITAR,
      ACHIEVEMENT_PERFECT_BASS,
      ACHIEVEMENT_PERFECT_DRUMS,
      ACHIEVEMENT_FIVE_STARS_ONE_SONG,
      ACHIEVEMENT_FIVE_STARS_FOUR_SONGS,
      ACHIEVEMENT_FIVE_STARS_GUITAR_EXPERT,
      ACHIEVEMENT_FIVE_STARS_GUITAR_EXP_4_SONGS,
      ACHIEVEMENT_FIVE_STARS_BASS_EXPERT,
      ACHIEVEMENT_FIVE_STARS_BASS_EXP_4_SONGS,
      ACHIEVEMENT_FIVE_STARS_DRUMS_EXPERT,
      ACHIEVEMENT_FIVE_STARS_DRUMS_EXP_4_SONGS,
      ACHIEVEMENT_PERFECT_ON_EXPERT,
      ACHIEVEMENT_COMPLETE_TWO_SF,
      ACHIEVEMENT_COMPLETE_TWO_NEW_YORK,
      ACHIEVEMENT_COMPLETE_TWO_CAIRO,
    }

    public class sStarPower
    {
      public int m_eventTime;
      public int m_eventLength;
      public bool m_bActive;

      public sStarPower()
      {
        this.m_bActive = true;
        this.m_eventTime = 0;
        this.m_eventLength = 0;
      }
    }

    public struct sSustainNoteInfo
    {
      public int m_musicScoreOffset;
      public int m_sustainLength;
    }
  }
}
