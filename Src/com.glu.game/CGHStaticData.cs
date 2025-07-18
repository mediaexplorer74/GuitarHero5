// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGHStaticData
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGHStaticData : CSingleton
  {
    protected new static CSingleton m_instance = (CSingleton) null;
    public static CMedia m_pMenuSelectSFX;
    public static CMedia m_pMenuBackSFX;
    public static CMedia m_pMenuScrollSFX;
    public static CMedia m_pCrowdCheerSFX;
    public static CMedia m_pMissedNoteSFX;
    public static CMedia m_pGuitarIntroSFX;
    public static CMedia m_pYouRockOutroSFX;
    public static CMedia m_pStarPowerDeploySFX;
    public static CMedia m_pStarPowerDepletedSFX;
    public static CMedia m_pStarPowerAwardedSFX;
    public static CMedia m_pStarPowerAwarded2SFX;
    public static CMedia m_pStarPowerAvailableSFX;
    public static CMedia m_pCrowdNeutralToPositiveSFX;
    public static CMedia m_pCrowdNeutralToNegativeSFX;
    public static CMedia m_pCrowdPositiveToNeutralSFX;
    public static CMedia m_pCrowdNegativeToNeutralSFX;
    public static CGHStaticData.SongAchievementData[] m_pSongAchievementData;
    public static SG_Presenter m_pTitleHead;
    public static SG_Presenter m_pTitleTail;
    public static SG_Presenter m_pTitleTile;
    public static bool m_bSongAlreadyDownloaded;
    public static bool m_bWasPreviouslyInRegistrationScreen;
    public static bool m_bResumeAudioFromSuspend;
    public static int m_state;
    public static int m_statePriorToPause;
    public static int m_songState;
    public static int m_score;
    public static int m_sustainScoreFractionalRemainder;
    public static int m_scoreMultiplier;
    public static int m_scoreMultiplierStep;
    public static int m_totalValueOfScoreMultiplierSamples;
    public static int m_totalScoreMultiplierSamples;
    public static int m_currentConsecutiveNotesHit;
    public static int m_maximumConsecutiveNotesHit;
    public static int m_hitNoteCount;
    public static int m_missedNoteCount;
    public static int m_badNoteCount;
    public static int m_hitNoteGroupCount;
    public static int m_missedNoteGroupCount;
    public static int m_totalSongNoteGroupCount;
    public static int m_percentageNotesHit;
    public static int m_starRatingbaseScore;
    public static int m_starRating;
    public static CGHStaticData.eDifficulty m_difficultyLevel;
    public static CGHStaticData.eGameInstrument m_instrumentBeingPlayed;
    public static int m_venueSelected;
    public static int m_gameplayMode;
    public static int m_musicianAppearance;
    public static int m_guitarAppearance;
    public static int m_drumsAppearance;
    public static int m_rocker_animation_state;
    public static int m_audioSynchTweakAdjustmentMS;
    public static int m_bDisplayGameplayDebugTimer;
    public static bool m_bStarPowerScoringActivated;
    public static int m_totalTimesStarPowerDeployed;
    public static CGHStaticData.s2dCoord m_guitarTrackOriginPos;
    public static CGHStaticData.s2dCoord m_guitarPlayerAnchorPosition;
    public static CGHStaticData.s2dCoord m_multiMeterAnchorPosition;
    public static CGHStaticData.s2dCoord m_starPowerAnchorPostion;
    public static CGHStaticData.s2dCoord m_trackEdgeTopRight;
    public static CGHStaticData.s2dCoord m_trackEdgeBottomRight;
    public static CGHStaticData.s2dCoord m_noteTrackStartLeftPosition;
    public static CGHStaticData.s2dCoord m_noteTrackStartCenterPosition;
    public static CGHStaticData.s2dCoord m_noteTrackStartRightPosition;
    public static CGHStaticData.s2dCoord m_noteTrackEndLeftPosition;
    public static CGHStaticData.s2dCoord m_noteTrackEndCenterPosition;
    public static CGHStaticData.s2dCoord m_noteTrackEndRightPosition;
    public static CGHStaticData.s2dCoord m_noteButtonLeftPosition;
    public static CGHStaticData.s2dCoord m_noteButtonCenterPosition;
    public static CGHStaticData.s2dCoord m_noteButtonRightPosition;
    public static CGHStaticData.s2dCoord m_touchButtonLeftPosition;
    public static CGHStaticData.s2dCoord m_touchButtonCenterPosition;
    public static CGHStaticData.s2dCoord m_touchButtonRightPosition;
    public static bool m_bAchievementInDemoMode;
    public static bool m_bAchievementsDisabled;
    public static bool m_bSingleKeypressHandset;
    public static bool m_bSingleNoteDisplayHandset;
    public static int m_lastNonClonedHitNoteMusicScoreOffset;
    public static bool m_bPlayingSong;
    public static bool m_bPlayingInstrument;
    public static uint m_unlockedIdx;
    public static uint m_unlockedAchievements;
    public static uint m_newlyUnlockedAchievements;
    public static uint m_newlyUnlockedAchievementsCounter;
    public static bool m_bCompletedSongWithEddie;
    public static bool m_bCompletedSongWithPandora;
    public static string m_loadingScreenTipResourceID;
    public static int m_bStarPowerAvailable;
    public static int m_velocityScaleX;
    public static int m_velocityScaleY;
    public static int m_nSpaceCurrentToNextSong;
    public static int m_nIconHeight;
    public static bool m_GOTO_SET_LIST;
    public static bool m_GOTO_SONG_LOADING;
    public static int m_timer;
    public static string m_instructionsMenuSelection;

    public static CGHStaticData GetInstance()
    {
      if (CGHStaticData.m_instance == null)
        CGHStaticData.m_instance = (CSingleton) new CGHStaticData();
      return CGHStaticData.m_instance as CGHStaticData;
    }

    public static bool UnlockedAchievement(int achievementIndex)
    {
      return 0L != ((long) CGHStaticData.m_unlockedAchievements & (long) (1 << achievementIndex));
    }

    public CGHStaticData()
    {
      CGHStaticData.m_nSpaceCurrentToNextSong = 0;
      CGHStaticData.m_nIconHeight = 0;
      CGHStaticData.m_GOTO_SET_LIST = false;
      CGHStaticData.m_GOTO_SONG_LOADING = false;
      CGHStaticData.m_bAchievementsDisabled = false;
      CGHStaticData.m_bAchievementInDemoMode = false;
      CGHStaticData.m_bResumeAudioFromSuspend = false;
      bool flag1 = true;
      bool flag2 = true;
      CGHStaticData.m_bSingleKeypressHandset = flag1;
      CGHStaticData.m_bSingleNoteDisplayHandset = flag2;
      CGHStaticData.m_audioSynchTweakAdjustmentMS = 0;
      CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EASY;
      CGHStaticData.m_instrumentBeingPlayed = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR;
      CGHStaticData.m_venueSelected = 0;
      CGHStaticData.m_gameplayMode = 0;
      if (CGHStaticData.m_unlockedIdx <= 257U)
      {
        CGHStaticData.m_unlockedIdx = 1U;
        CGHStaticData.m_unlockedIdx |= 256U;
      }
      CGHStaticData.m_loadingScreenTipResourceID = (string) null;
      CGHStaticData.m_newlyUnlockedAchievementsCounter = 0U;
    }

    public static int FindSongAchievementsIndex(uint songID)
    {
      int achievementsIndex = 0;
      if (CGHStaticData.m_pSongAchievementData == null)
        CGHStaticData.InitializeSongAchievements();
      while (achievementsIndex < (int) sbyte.MaxValue && CGHStaticData.m_pSongAchievementData[achievementsIndex].GetSongID() != 0U && (int) CGHStaticData.m_pSongAchievementData[achievementsIndex].GetSongID() != (int) songID)
        ++achievementsIndex;
      return achievementsIndex;
    }

    public static uint GetCompletionDataSongID(int songIndex)
    {
      return CGHStaticData.m_pSongAchievementData != null ? CGHStaticData.m_pSongAchievementData[songIndex].GetSongID() : 0U;
    }

    public static uint GetCompletionDataForSongID(int songIndex)
    {
      return CGHStaticData.m_pSongAchievementData != null ? CGHStaticData.m_pSongAchievementData[songIndex].GetSongCompletionData() : 0U;
    }

    private void Init() => CGHStaticData.InitializeSongAchievements();

    public static void InitializeSongAchievements()
    {
      if (CGHStaticData.m_pSongAchievementData != null)
        return;
      CGHStaticData.m_pSongAchievementData = new CGHStaticData.SongAchievementData[128];
    }

    private void OnFree()
    {
    }

    public static void ResetSongAchievementData()
    {
      CGHStaticData.m_unlockedAchievements = 0U;
      CGHStaticData.m_bCompletedSongWithEddie = false;
      CGHStaticData.m_bCompletedSongWithPandora = false;
      if (CGHStaticData.m_pSongAchievementData == null)
        return;
      for (int index = 0; index < 128; ++index)
        CGHStaticData.m_pSongAchievementData[index].ClearData();
    }

    public static void SetSongCompletionData(uint songID, uint songCompletionData)
    {
      int achievementsIndex = CGHStaticData.FindSongAchievementsIndex(songID);
      CGHStaticData.m_pSongAchievementData[achievementsIndex].Setup(songID, songCompletionData);
    }

    public enum eDifficulty
    {
      GAME_DIFFICULTY_EASY,
      GAME_DIFFICULTY_MEDIUM,
      GAME_DIFFICULTY_EXPERT,
    }

    public enum eGameInstrument
    {
      GAME_INSTRUMENT_GUITAR,
      GAME_INSTRUMENT_BASS,
      GAME_INSTRUMENT_DRUMS,
      GAME_INSTRUMENT_ALL,
    }

    public enum eGamePlayMode
    {
      GAME_PLAY_MODE_CAREER,
      GAME_PLAY_MODE_PRACTICE,
    }

    public enum eGameMusician
    {
      GAME_MUSICIAN_EDDIE,
      GAME_MUSICIAN_PANDORA,
    }

    public enum eSongCompletionMask
    {
      SONG_COMPLETION_MASK_NONE = 0,
      SONG_COMPLETION_MASK_EASY = 1,
      SONG_COMPLETION_MASK_DIFFICULT = 2,
      SONG_COMPLETION_MASK_EXPERT = 4,
      SONG_COMPLETION_MASK_SHIFT = 6,
      SONG_COMPLETION_MASK_ANY_DIFFICULT = 7,
      SONG_COMPLETION_MASK_5_STAR_EASY = 8,
      SONG_COMPLETION_MASK_5_STAR_DIFFICULT = 16, // 0x00000010
      SONG_COMPLETION_MASK_5_STAR_EXPERT = 32, // 0x00000020
    }

    public enum eSongCompletionInstrumentShift
    {
      SONG_COMPLETION_SHIFT_GUITAR = 0,
      SONG_COMPLETION_SHIFT_BASS = 6,
      SONG_COMPLETION_SHIFT_DRUMS = 12, // 0x0000000C
    }

    public struct SongAchievementData(uint songID, uint completionData)
    {
      public uint m_songID = songID;
      public uint m_completionData = completionData;

      public void Setup(uint songID, uint completionData)
      {
        this.m_songID = songID;
        this.m_completionData = completionData;
      }

      public uint GetSongID() => this.m_songID;

      public uint GetSongCompletionData() => this.m_completionData;

      public void ClearData()
      {
        this.m_songID = 0U;
        this.m_completionData = 0U;
      }

      public bool CompletedInstrumentOnAnyDifficulty(CGHStaticData.eGameInstrument instrument)
      {
        return 0L != ((long) this.m_completionData & (long) (1 << 6 * (int) instrument)) || 0L != ((long) this.m_completionData & (long) (2 << 6 * (int) instrument)) || 0L != ((long) this.m_completionData & (long) (4 << 6 * (int) instrument));
      }

      public bool CompletedAllThreeInstrumentsOnAnyDifficulty()
      {
        if (((int) this.m_completionData & 1) == 0 && ((int) this.m_completionData & 2) == 0 && ((int) this.m_completionData & 4) == 0 || ((int) this.m_completionData & 64) == 0 && ((int) this.m_completionData & 128) == 0 && ((int) this.m_completionData & 256) == 0)
          return false;
        return ((int) this.m_completionData & 4096) != 0 || ((int) this.m_completionData & 8192) != 0 || 0 != ((int) this.m_completionData & 16384);
      }

      public bool CompletedAnyInstrumentOnAnyDifficulty()
      {
        return ((int) this.m_completionData & 1) != 0 || ((int) this.m_completionData & 2) != 0 || ((int) this.m_completionData & 4) != 0 || ((int) this.m_completionData & 64) != 0 || ((int) this.m_completionData & 128) != 0 || ((int) this.m_completionData & 256) != 0 || ((int) this.m_completionData & 4096) != 0 || ((int) this.m_completionData & 8192) != 0 || 0 != ((int) this.m_completionData & 16384);
      }

      public bool SongAchievementCompleted(
        CGHStaticData.eGameInstrument instrument,
        CGHStaticData.eDifficulty difficulty,
        bool bFiveStars)
      {
        return 0L != ((long) this.m_completionData & (long) ((!bFiveStars ? 1 << (int) (difficulty & (CGHStaticData.eDifficulty) 31) : 8 << (int) (difficulty & (CGHStaticData.eDifficulty) 31)) << 6 * (int) instrument));
      }

      public bool AchievedFiveStarsForAllInstrumentsOnASpecificDifficulty()
      {
        if (((int) this.m_completionData & 8) != 0 && ((int) this.m_completionData & 512) != 0 && ((int) this.m_completionData & 32768) != 0 || ((int) this.m_completionData & 16) != 0 && ((int) this.m_completionData & 1024) != 0 && ((int) this.m_completionData & 65536) != 0)
          return true;
        return ((int) this.m_completionData & 32) != 0 && ((int) this.m_completionData & 2048) != 0 && 0 != ((int) this.m_completionData & 131072);
      }

      public void SetCompleted(
        CGHStaticData.eGameInstrument instrument,
        CGHStaticData.eDifficulty difficulty,
        bool bFiveStars)
      {
        int num = 1 << (int) (difficulty & (CGHStaticData.eDifficulty) 31);
        if (bFiveStars)
          num |= 8 << (int) (difficulty & (CGHStaticData.eDifficulty) 31);
        this.m_completionData |= (uint) (num << 6 * (int) instrument);
      }
    }

    public struct s2dCoord
    {
      public int m_x;
      public int m_y;
    }
  }
}
