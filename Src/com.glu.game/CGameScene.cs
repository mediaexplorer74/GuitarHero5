// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGameScene : CStateMachineNode
  {
    public const int MENU_RESUME_ITEMS = 1;
    public const int MENU_PAUSE_ITEMS = 6;
    public const int MENU_PAUSE_NO_RESUME_ITEMS = 5;
    public const int MENU_PAUSE_LOADING_ITEMS = 5;
    protected CResBank m_bank = new CResBank();
    protected CGuitarHeroGame m_pGHGame;
    public static int m_loadStep;
    public static bool m_bSetLoadingComplete;
    protected int m_curUnlockedInstrument;
    protected uint m_curUnlockedEncoreSong;
    protected int m_curUnlockedAchievements;
    protected CMedia m_pSoundFX;
    protected uint m_soundId;
    private string[] kTableMenuPause = new string[6]
    {
      "IDS_PAUSE_MENU_RESUME",
      "IDS_PAUSE_MENU_RESTART",
      "IDS_SET_LIST",
      "IDS_MENU_OPTIONS",
      "IDS_MENU_INSTRUCTIONS",
      "IDS_PAUSE_MENU_QUIT"
    };
    private string[] kTableMenuPauseNoResume = new string[5]
    {
      "IDS_PAUSE_MENU_RESTART",
      "IDS_SET_LIST",
      "IDS_MENU_OPTIONS",
      "IDS_MENU_INSTRUCTIONS",
      "IDS_PAUSE_MENU_QUIT"
    };
    private string[] kTableMenuPauseLoading = new string[5]
    {
      "IDS_PAUSE_MENU_RESUME",
      "IDS_SET_LIST",
      "IDS_MENU_OPTIONS",
      "IDS_MENU_INSTRUCTIONS",
      "IDS_PAUSE_MENU_QUIT"
    };

    public CGameScene()
    {
      this.m_pGHGame = (CGuitarHeroGame) null;
      CGameScene.m_loadStep = -1;
      CGameScene.m_bSetLoadingComplete = false;
      this.m_curUnlockedInstrument = 0;
      this.m_curUnlockedEncoreSong = 0U;
    }

    public override uint Start()
    {
      uint num = base.Start();
      this.m_pGHGame = new CGuitarHeroGame();
      if (CDemoMgr.IsDemo())
        this.ChangeState(2, 1);
      else if (CGHStaticData.m_bSongAlreadyDownloaded)
        this.ChangeState(8, 1);
      else
        this.ChangeState(7, 1);
      return num;
    }

    public override void Stop()
    {
      base.Stop();
      if (this.m_pGHGame != null && this.m_pGHGame.IsLoadingGame())
        this.m_pGHGame = (CGuitarHeroGame) null;
      this.m_bank.Release();
      if (!CDemoMgr.IsDemo())
        return;
      CDemoMgr.AddGame(1);
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = base.HandleEvent(id, param1, param2);
      switch (id)
      {
        case 850690755:
          if (this.m_pGHGame.IsLoadingGameCompleted())
          {
            this.m_pGHGame.HandleEvent(850690755U, param1, param2);
            break;
          }
          break;
        case 1364371259:
        case 1411673571:
        case 2215179113:
        case 3343010790:
          int stateId = this.m_stateID;
          break;
      }
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = base.HandleUpdate(timeElapsedMS);
      switch (this.m_stateID)
      {
        case 8:
          CProgressScreen pState = (CProgressScreen) this.m_pState;
          if (this.m_pGHGame.loadingDone && !CGameScene.m_bSetLoadingComplete)
          {
            CGameScene.m_bSetLoadingComplete = true;
            pState.UpdateTextString(CGameApp.GetInstance().GetOverrideText("IDS_LOADED_PRESS_A_KEY"));
            pState.SetProgressPercent(100);
            pState.UpdateSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW", true, true);
          }
          if (!CGameScene.m_bSetLoadingComplete)
            pState.SetProgressPercent((pState.GetProgressPercent() + 5) % 100);
          if (CGameApp.GetInstance().WasLoadError())
          {
            CGameApp.GetInstance().SetLoadError(false);
            this.ChangeState(20, 2);
            break;
          }
          break;
        case 9:
          if (CDemoMgr.IsDemo())
          {
            CDemoMgr.AddTime(timeElapsedMS);
            if (CDemoMgr.IsTimeOrGameExpired())
            {
              this.ChangeState(5, 2);
              break;
            }
            break;
          }
          if (CGuitarHeroGame.m_zunePlaying)
          {
            CGuitarHeroGame.m_zunePlaying = false;
            this.ChangeState(31, 1);
            break;
          }
          break;
      }
      return flag;
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state1 = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          CTextScreen ctextScreen1 = new CTextScreen();
          ctextScreen1.SetTitle("IDS_DEMO_TITLE");
          ctextScreen1.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen1.SetText("IDS_DEMO_LICENSE_CHECK");
          ctextScreen1.SetInset(30, 0);
          ctextScreen1.SetFlags(5);
          ctextScreen1.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen1;
          break;
        case 2:
          CTextScreen ctextScreen2 = new CTextScreen();
          ctextScreen2.SetTitle("IDS_DEMO_TITLE");
          ctextScreen2.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen2.SetText("IDS_DEMO_LICENSE_CHECK");
          ctextScreen2.SetInset(30, 0);
          ctextScreen2.SetFlags(5);
          ctextScreen2.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen2;
          if (CDemoMgr.IsDemo())
          {
            if (CDemoMgr.IsPlayExpired())
            {
              this.ChangeState(4, 2);
              break;
            }
            if (CDemoMgr.IsTimeOrGameExpired())
            {
              this.ChangeState(5, 2);
              break;
            }
            if (!CDemoMgr.IsPlayStarted())
            {
              CDemoMgr.StartPlay();
              this.ChangeState(8, 2);
              break;
            }
            if (CGHStaticData.m_bSongAlreadyDownloaded)
            {
              this.ChangeState(8, 2);
              break;
            }
            this.ChangeState(7, 2);
            break;
          }
          if (CGHStaticData.m_bSongAlreadyDownloaded)
          {
            this.ChangeState(8, 2);
            break;
          }
          this.ChangeState(7, 2);
          break;
        case 3:
          CTextScreen ctextScreen3 = new CTextScreen();
          ctextScreen3.SetTitle("IDS_DEMO_TITLE");
          ctextScreen3.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen3.SetTextDirectly(CDemoMgr.GetInfoText());
          ctextScreen3.SetInset(30, 0);
          ctextScreen3.SetFlags(5);
          ctextScreen3.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen3.ForceDisableSlider();
          cnode = (CNode) ctextScreen3;
          break;
        case 4:
          CTextScreen ctextScreen4 = new CTextScreen();
          ctextScreen4.SetTitle("IDS_DEMO_EXPIRED_TITLE");
          ctextScreen4.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen4.SetTextDirectly(CDemoMgr.GetPlayExpiredText());
          ctextScreen4.SetInset(30, 0);
          ctextScreen4.SetFlags(5);
          ctextScreen4.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen4.ForceDisableSlider();
          cnode = (CNode) ctextScreen4;
          break;
        case 5:
          CTextScreen ctextScreen5 = new CTextScreen();
          ctextScreen5.SetTitle("IDS_DEMO_EXPIRED_TITLE");
          ctextScreen5.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen5.SetTextDirectly(CDemoMgr.GetTimeOrGameExpiredText());
          ctextScreen5.SetInset(30, 0);
          ctextScreen5.SetFlags(5);
          ctextScreen5.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen5.ForceDisableSlider();
          cnode = (CNode) ctextScreen5;
          break;
        case 6:
          CTextScreen ctextScreen6 = new CTextScreen();
          ctextScreen6.SetTitle("IDS_DEMO_EXPIRED_TITLE");
          ctextScreen6.SetText("IDS_DEMO_UPGRADE_NOT_AVAILABLE");
          ctextScreen6.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen6.SetInset(30, 0);
          ctextScreen6.SetFlags(5);
          ctextScreen6.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen6;
          break;
        case 8:
          CProgressScreen cprogressScreen = new CProgressScreen();
          cprogressScreen.SetInfo(4278190080U, (string) null, "IDS_LOADING");
          cprogressScreen.SetTipsText(CGHStaticData.m_loadingScreenTipResourceID);
          cprogressScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          cprogressScreen.SetProgressPercent(0);
          cprogressScreen.SetMovie("GLU_MOVIE_DOWNLOAD");
          cnode = (CNode) cprogressScreen;
          break;
        case 9:
          CGameScreen cgameScreen = new CGameScreen();
          cgameScreen.SetGameNode((CNode) this.m_pGHGame);
          cgameScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          cnode = (CNode) cgameScreen;
          break;
        case 10:
          if (CGameApp.GetInstance().IsSongResumeEnabled())
          {
            ICMediaPlayer.GetInstance().Pause();
            ICMediaPlayer.GetInstance().PauseVibrate();
          }
          else
          {
            ICMediaPlayer.GetInstance().Stop();
            ICMediaPlayer.GetInstance().StopVibrate();
          }
          int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          CMenuScreen cmenuScreen1 = new CMenuScreen();
          cmenuScreen1.SetTitle("IDS_PAUSE_MENU");
          cmenuScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          CGameScreen state2 = (CGameScreen) this.FindState(9);
          if (this.m_pGHGame.IsLoadingGame() || state2 == null)
            cmenuScreen1.SetMenu(this.kTableMenuPauseLoading, 5);
          else if (!CGameApp.GetInstance().IsSongResumeEnabled() && CGHStaticData.m_state < 6)
            cmenuScreen1.SetMenu(this.kTableMenuPauseNoResume, 5);
          else
            cmenuScreen1.SetMenu(this.kTableMenuPause, 6);
          cmenuScreen1.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen1;
          break;
        case 11:
          CMenuScreen cmenuScreen2 = new CMenuScreen();
          cmenuScreen2.SetTitle("IDS_MENU_OPTIONS");
          cmenuScreen2.SetInset(30, 70);
          cmenuScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          if (CGameApp.GetInstance().IsVolumeControlDisabled())
          {
            if (ICCore.GetInstance().CanVibrate() && !CGameApp.GetInstance().IsVibrationDisabled())
              cmenuScreen2.SetMenu(Consts.kTablePauseSettingsMenuNoVolume, 2);
            else
              cmenuScreen2.SetMenu(Consts.kTablePauseSettingsMenuNoVolume, 1);
          }
          else if (ICCore.GetInstance().CanVibrate() && !CGameApp.GetInstance().IsVibrationDisabled())
            cmenuScreen2.SetMenu(Consts.kTablePauseSettingsMenu, 2);
          else
            cmenuScreen2.SetMenu(Consts.kTablePauseSettingsMenu, 1);
          cmenuScreen2.SetFlags(5);
          cmenuScreen2.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen2;
          this.m_pSoundFX = (CMedia) null;
          CResourceManager resourceManager = CApp.GetResourceManager();
          CResource resource1 = (CResource) null;
          this.m_soundId = 0U;
          int resource2 = (int) resourceManager.CreateResource("IDM_PROMPT", out resource1);
          if (resource1 != null)
          {
            this.m_pSoundFX = (CMedia) resource1.GetData();
            break;
          }
          break;
        case 12:
          CInstructionsMenuScreen cinstructionsMenuScreen = new CInstructionsMenuScreen();
          cinstructionsMenuScreen.SetTitle("IDS_MENU_INSTRUCTIONS");
          cinstructionsMenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cinstructionsMenuScreen.SetMovie("GLU_MOVIE_COMMON");
          cinstructionsMenuScreen.SetFlags(5);
          cnode = (CNode) cinstructionsMenuScreen;
          break;
        case 13:
          CInstructionsScreen cinstructionsScreen = new CInstructionsScreen();
          cinstructionsScreen.SetSoftkeys("ID_UNDEFINED", "SUR_SOFTKEY_ARROW");
          cinstructionsScreen.SetMovie("GLU_MOVIE_COMMON");
          cinstructionsScreen.EnableSliderBar();
          cnode = (CNode) cinstructionsScreen;
          break;
        case 14:
          CVolumeScene cvolumeScene = new CVolumeScene();
          cvolumeScene.SetInset(true);
          cnode = (CNode) cvolumeScene;
          break;
        case 15:
          CVibrationScene cvibrationScene = new CVibrationScene();
          cvibrationScene.SetInset(true);
          cnode = (CNode) cvibrationScene;
          break;
        case 16:
          CMenuScreen cmenuScreen3 = new CMenuScreen();
          cmenuScreen3.SetTitle("IDS_QUIT_MENU");
          cmenuScreen3.SetInset(30, 70);
          cmenuScreen3.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen3.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen3.SetFlags(5);
          cmenuScreen3.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen3;
          break;
        case 17:
          CTextScreen ctextScreen7 = new CTextScreen();
          ctextScreen7.SetTitle("IDS_SAVE_WARNING_TITLE");
          ctextScreen7.SetInset(30, 70);
          ctextScreen7.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen7.SetText("IDS_SAVE_WARNING_TEXT");
          ctextScreen7.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen7;
          break;
        case 18:
          CTextScreen ctextScreen8 = new CTextScreen();
          ctextScreen8.SetTitle("IDS_SAVE_SUCCESS_TITLE");
          ctextScreen8.SetInset(30, 70);
          ctextScreen8.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen8.SetText("IDS_SAVE_SUCCESS_TEXT");
          ctextScreen8.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen8;
          break;
        case 19:
          CTextScreen ctextScreen9 = new CTextScreen();
          ctextScreen9.SetTitle("IDS_SAVE_ERROR_TITLE");
          ctextScreen9.SetInset(30, 70);
          ctextScreen9.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen9.SetText("IDS_SAVE_ERROR_TEXT");
          ctextScreen9.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen9.SetInset(30, 0);
          cnode = (CNode) ctextScreen9;
          break;
        case 20:
          CTextScreen ctextScreen10 = new CTextScreen();
          ctextScreen10.SetTitle("IDS_NETWORK_ERROR_TITLE");
          ctextScreen10.SetInset(30, 70);
          ctextScreen10.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen10.SetText("IDS_NETWORK_ERROR_TEXT");
          ctextScreen10.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen10.SetInset(30, 0);
          cnode = (CNode) ctextScreen10;
          break;
        case 21:
          if (CGHStaticData.m_gameplayMode == 0)
            this.m_soundId = ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdNeutralToPositiveSFX);
          CGameResultScreen cgameResultScreen1 = new CGameResultScreen();
          cgameResultScreen1.SetTitle("IDS_GAME_RESULT_TITLE_PASSED");
          cgameResultScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          cgameResultScreen1.SetMovie("GLU_MOVIE_JUDGEMENT");
          cnode = (CNode) cgameResultScreen1;
          break;
        case 22:
          CGameResultScreen cgameResultScreen2 = new CGameResultScreen();
          cgameResultScreen2.SetTitle("IDS_GAME_RESULT_TITLE_FAILED");
          cgameResultScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          cgameResultScreen2.SetMovie("GLU_MOVIE_JUDGEMENT");
          cgameResultScreen2.SetFailed();
          cnode = (CNode) cgameResultScreen2;
          this.m_soundId = ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pCrowdNeutralToNegativeSFX);
          break;
        case 24:
          CTextScreen ctextScreen11 = new CTextScreen();
          ctextScreen11.SetTitle("IDS_SAVE_ERROR_TITLE");
          ctextScreen11.SetInset(30, 70);
          ctextScreen11.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen11.SetText("IDS_SAVE_ERROR_TEXT");
          ctextScreen11.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen11.SetInset(30, 0);
          cnode = (CNode) ctextScreen11;
          break;
        case 25:
          CHighscoreScreen chighscoreScreen = new CHighscoreScreen();
          chighscoreScreen.SetTitle("IDS_HIGHSCORES_TITLE");
          chighscoreScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          chighscoreScreen.SetHighscoreMgrInfo(Consts.kTableNavScores, Consts.kNumScoreTables, 0);
          chighscoreScreen.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) chighscoreScreen;
          break;
        case 26:
          CMenuScreen cmenuScreen4 = new CMenuScreen();
          cmenuScreen4.SetTitle("IDS_RESTART_MENU");
          cmenuScreen4.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen4.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen4.SetFlags(5);
          cmenuScreen4.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen4;
          break;
        case 27:
          CSongListMgr.SetSelected(this.m_curUnlockedEncoreSong);
          CSong song = CSongListMgr.GetSong(this.m_curUnlockedEncoreSong);
          string output = (string) null;
          CUtility.GetString(out output, "IDS_PLAY_NOW");
          string pwszText = song.GetSongName() + "\n" + song.GetBandName() + "\n" + output;
          CMenuScreen cmenuScreen5 = new CMenuScreen();
          cmenuScreen5.SetTitle("IDS_ENCORE_UNLOCKED");
          cmenuScreen5.SetText(pwszText);
          cmenuScreen5.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          cmenuScreen5.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen5.SetFlags(5);
          cmenuScreen5.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen5.ForceDisableSlider();
          cnode = (CNode) cmenuScreen5;
          break;
        case 28:
          string[] pImageIds1 = new string[1];
          string[] ptextIds1 = new string[1];
          pImageIds1[0] = Consts.kTableAppearanceGuitars[this.m_curUnlockedInstrument * 2];
          ptextIds1[0] = Consts.kTableAppearanceGuitars[this.m_curUnlockedInstrument * 2 + 1];
          CImageMenuScreen cimageMenuScreen1 = new CImageMenuScreen();
          cimageMenuScreen1.SetTitle("IDS_GUITAR_UNLOCKED");
          cimageMenuScreen1.SetInfo(pImageIds1, ptextIds1, 1);
          cimageMenuScreen1.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cimageMenuScreen1.SetFlags(1);
          cnode = (CNode) cimageMenuScreen1;
          break;
        case 29:
          string[] pImageIds2 = new string[1];
          string[] ptextIds2 = new string[1];
          pImageIds2[0] = Consts.kTableAppearanceDrumsets[this.m_curUnlockedInstrument * 2];
          ptextIds2[0] = Consts.kTableAppearanceDrumsets[this.m_curUnlockedInstrument * 2 + 1];
          CImageMenuScreen cimageMenuScreen2 = new CImageMenuScreen();
          cimageMenuScreen2.SetTitle("IDS_DRUMS_UNLOCKED");
          cimageMenuScreen2.SetInfo(pImageIds2, ptextIds2, 1);
          cimageMenuScreen2.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cimageMenuScreen2.SetFlags(1);
          cnode = (CNode) cimageMenuScreen2;
          break;
        case 30:
          int itemIndex = 0;
          while (itemIndex < 20 && ((long) CGHStaticData.m_newlyUnlockedAchievements & (long) (1 << itemIndex)) == 0L)
            ++itemIndex;
          CGHStaticData.m_newlyUnlockedAchievements &= (uint) ~(1 << itemIndex);
          CAchievementsScene cachievementsScene = new CAchievementsScene();
          cachievementsScene.SetInset(false);
          cachievementsScene.SetDisplaySingleItem(itemIndex);
          cnode = (CNode) cachievementsScene;
          break;
        case 31:
          CTextScreen ctextScreen12 = new CTextScreen();
          ctextScreen12.SetTitle("IDS_ZUNE_RUNNING_TITLE");
          ctextScreen12.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen12.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen12.SetText("IDS_ZUNE_RUNNING");
          cnode = (CNode) ctextScreen12;
          break;
      }
      pOut = cnode;
      return state1;
    }

    protected override void OnStateInterrupt(int id, CNode pState)
    {
      switch (id)
      {
        case 3:
          if (pState.GetInterrupt() == 1)
          {
            CDemoMgr.StartPlay();
            this.ChangeState(8, 2);
            break;
          }
          CGHStaticData.m_GOTO_SET_LIST = true;
          this.SetInterrupt(2);
          break;
        case 4:
          if (pState.GetInterrupt() == 1)
          {
            if (CDemoMgr.LaunchUpgrade())
            {
              this.SetInterrupt(2);
              break;
            }
            this.ChangeState(6, 2);
            break;
          }
          CGHStaticData.m_GOTO_SET_LIST = true;
          this.SetInterrupt(2);
          break;
        case 5:
          if (pState.GetInterrupt() == 1)
          {
            if (CDemoMgr.LaunchUpgrade())
            {
              this.SetInterrupt(2);
              break;
            }
            this.ChangeState(6, 2);
            break;
          }
          CGHStaticData.m_GOTO_SET_LIST = true;
          this.SetInterrupt(2);
          break;
        case 7:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(8, 2);
            break;
          }
          if (pState.GetInterrupt() != 2)
            break;
          CGHStaticData.m_GOTO_SET_LIST = true;
          this.SetInterrupt(2);
          break;
        case 8:
          if (pState.GetInterrupt() == 1)
          {
            CProgressScreen pState1 = (CProgressScreen) this.m_pState;
            if (pState1.ProgressCompleted())
            {
              this.ChangeState(9, 2);
              break;
            }
            if (!this.m_pGHGame.IsLoadingGameCompleted())
              break;
            int num = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            pState1.SetProgressComplete();
            this.m_pGHGame.KeyPressedOnLoadingComplete();
            break;
          }
          if (CGameApp.GetInstance().WasLoadError())
          {
            CGameApp.GetInstance().SetLoadError(false);
            this.ChangeState(20, 2);
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 9:
          switch (pState.GetInterrupt())
          {
            case 1:
              if (CGHStaticData.m_gameplayMode == 0)
              {
                CSong songSelected = CSongListMgr.GetSongSelected();
                CSongScoreMgr.StoreScore(songSelected.GetSongID(), (Consts.eDifficulty) CGHStaticData.m_difficultyLevel, (Consts.eInstrument) CGHStaticData.m_instrumentBeingPlayed, (uint) CGHStaticData.m_score, (uint) CGHStaticData.m_starRating);
                CSongScoreMgr.SongScore songScore = CSongScoreMgr.GetSongScore(songSelected.GetSongID());
                this.m_curUnlockedEncoreSong = CSongListMgr.CheckUnlockEvent(songSelected.GetSongID());
                if (this.m_curUnlockedEncoreSong > 0U)
                  CSongListMgr.UnlockSong(this.m_curUnlockedEncoreSong);
                if (songSelected.IsEncore() && songScore.numPassed == (ushort) 1)
                {
                  int num = 0;
                  while (num < 4 && ((long) CGHStaticData.m_unlockedIdx & (long) (1 << num)) != 0L)
                    ++num;
                  this.m_curUnlockedInstrument = num;
                  CGHStaticData.m_unlockedIdx |= (uint) (1 << num);
                  CGHStaticData.m_unlockedIdx |= (uint) (1 << num + 8);
                }
                CSongScoreMgr.Write();
              }
              switch (CGHStaticData.m_state)
              {
                case 6:
                  this.ChangeState(22, 1);
                  return;
                case 7:
                  this.ChangeState(21, 1);
                  return;
                case 11:
                  this.ChangeState(31, 1);
                  return;
                default:
                  return;
              }
            case 2:
              this.ChangeState(10, 1);
              return;
            default:
              return;
          }
        case 10:
          if (pState.GetInterrupt() == 1)
          {
            CGameScreen state1 = (CGameScreen) this.FindState(9);
            switch (this.m_pGHGame.IsLoadingGame() || state1 == null ? this.kTableMenuPauseLoading[((CMenuScreen) pState).GetSelection()] : (CGameApp.GetInstance().IsSongResumeEnabled() || CGHStaticData.m_state >= 6 ? this.kTableMenuPause[((CMenuScreen) pState).GetSelection()] : this.kTableMenuPauseNoResume[((CMenuScreen) pState).GetSelection()]))
            {
              case "IDS_PAUSE_MENU_RESUME":
                this.ChangeState(0, 4);
                return;
              case "IDS_SET_LIST":
                this.ChangeState(16, 1);
                CGHStaticData.m_GOTO_SET_LIST = true;
                return;
              case "IDS_MENU_OPTIONS":
                this.ChangeState(11, 1);
                return;
              case "IDS_MENU_INSTRUCTIONS":
                this.ChangeState(12, 1);
                return;
              case "IDS_PAUSE_MENU_RESTART":
                this.ChangeState(26, 1);
                return;
              case "IDS_PAUSE_MENU_SAVE":
                if (CSaveGameMgr.GetInstance().GetSavedGame())
                {
                  this.ChangeState(17, 1);
                  return;
                }
                CGameScreen state2 = (CGameScreen) this.FindState(9);
                if (state2 != null && state2.SaveGame())
                {
                  this.ChangeState(18, 1);
                  return;
                }
                this.ChangeState(19, 1);
                return;
              case "IDS_PAUSE_MENU_QUIT":
                this.ChangeState(16, 1);
                CGHStaticData.m_GOTO_SET_LIST = false;
                return;
              case null:
                return;
              default:
                return;
            }
          }
          else
          {
            if (!CGameApp.GetInstance().IsSongResumeEnabled() && CGHStaticData.m_state > 1 && CGHStaticData.m_state < 6)
            {
              this.ChangeState(26, 1);
              break;
            }
            this.ChangeState(0, 4);
            break;
          }
        case 11:
          if (pState.GetInterrupt() == 1)
          {
            switch (!CGameApp.GetInstance().IsVolumeControlDisabled() ? Consts.kTablePauseSettingsMenu[((CMenuScreen) pState).GetSelection()] : Consts.kTablePauseSettingsMenuNoVolume[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_MENU_VOLUME":
              case "IDS_MENU_SOUND":
                this.ChangeState(14, 1);
                return;
              case "IDS_MENU_VIBRATION":
                this.ChangeState(15, 1);
                return;
              default:
                return;
            }
          }
          else
          {
            ICMediaPlayer instance = ICMediaPlayer.GetInstance();
            if (this.m_soundId > 0U)
            {
              instance.Stop(this.m_soundId);
              instance.StopVibrate(this.m_soundId);
            }
            CApp.GetResourceManager().ReleaseResource("IDM_PROMPT");
            this.ChangeState(0, 4);
            break;
          }
        case 12:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(13, 1);
            break;
          }
          if (pState.GetInterrupt() != 2)
            break;
          this.ChangeState(0, 4);
          break;
        case 13:
          this.ChangeState(0, 4);
          break;
        case 14:
          if (pState.GetInterrupt() == 1)
            this.m_soundId = ICMediaPlayer.GetInstance().Play(this.m_pSoundFX);
          this.ChangeState(0, 4);
          break;
        case 15:
          this.ChangeState(0, 4);
          break;
        case 16:
          if (pState.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                this.ChangeState(0, 4);
                return;
              case "IDS_YES":
                this.SetInterrupt(2);
                return;
              default:
                return;
            }
          }
          else
          {
            this.ChangeState(0, 4);
            break;
          }
        case 17:
          if (pState.GetInterrupt() == 1)
          {
            CGameScreen state = (CGameScreen) this.FindState(9);
            if (state != null && state.SaveGame())
            {
              this.ChangeState(18, 2);
              break;
            }
            this.ChangeState(19, 2);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 18:
        case 19:
          this.ChangeState(0, 4);
          break;
        case 20:
          this.SetInterrupt(2);
          break;
        case 21:
          if (pState.GetInterrupt() == 1)
          {
            if (this.m_curUnlockedEncoreSong > 0U)
            {
              this.ChangeState(27, 1);
              break;
            }
            if (this.m_curUnlockedInstrument > 0)
            {
              this.ChangeState(28, 1);
              break;
            }
            this.SetInterrupt(1);
            CGHStaticData.m_GOTO_SET_LIST = true;
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 22:
          if (pState.GetInterrupt() == 1)
          {
            this.SetInterrupt(1);
            CGHStaticData.m_GOTO_SET_LIST = true;
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 24:
          this.ChangeState(25, 2);
          break;
        case 25:
          this.SetInterrupt(1);
          break;
        case 26:
          if (pState.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                this.ChangeState(0, 4);
                return;
              case "IDS_YES":
                if (CDemoMgr.IsDemo())
                  CDemoMgr.AddGame(1);
                ((CGameScreen) this.FindState(9)).ResetGame();
                this.ChangeState(9, 5);
                return;
              default:
                return;
            }
          }
          else
          {
            this.ChangeState(0, 4);
            break;
          }
        case 27:
          if (pState.GetInterrupt() == 1)
          {
            CGHStaticData.m_GOTO_SET_LIST = false;
            CGHStaticData.m_GOTO_SONG_LOADING = false;
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_NO":
                CGHStaticData.m_GOTO_SONG_LOADING = false;
                CGHStaticData.m_GOTO_SET_LIST = false;
                break;
              case "IDS_YES":
                CGHStaticData.m_GOTO_SET_LIST = true;
                CGHStaticData.m_GOTO_SONG_LOADING = true;
                break;
            }
            this.SetInterrupt(1);
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 28:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(29, 1);
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 29:
          if (pState.GetInterrupt() == 1)
          {
            CGHStaticData.m_GOTO_SET_LIST = true;
            CGHStaticData.m_GOTO_SONG_LOADING = false;
            this.SetInterrupt(1);
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 30:
          if (pState.GetInterrupt() == 1)
          {
            --CGHStaticData.m_newlyUnlockedAchievementsCounter;
            if (CGHStaticData.m_newlyUnlockedAchievementsCounter == 0U)
            {
              this.ChangeState(21, 2);
              break;
            }
            this.ChangeState(30, 2);
            break;
          }
          this.ChangeState(10, 1);
          break;
        case 31:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(9, 2);
            break;
          }
          this.SetInterrupt(1);
          CGHStaticData.m_GOTO_SET_LIST = true;
          break;
      }
    }

    private int UnlockAchievement()
    {
      int num = 0;
      while (num < (int) CSongListMgr.GetNumSongs())
        ++num;
      return -1;
    }

    public enum eGameSceneInterrupt
    {
      GAMESCENE_INTERRUPT_NONE,
      GAMESCENE_INTERRUPT_GOTO_MAINMENU,
      GAMESCENE_INTERRUPT_BACK,
      GAMESCENE_INTERRUPT_GOTO_SETLIST,
    }

    public enum eGameSceneState
    {
      GAME_NONE,
      GAME_LICENSE_CHECK,
      GAME_LICENSE_CHECK_FINISHED,
      GAME_DEMO_PLAY_CONFIRM,
      GAME_DEMO_PLAY_EXPIRED,
      GAME_DEMO_TIME_EXPIRED,
      GAME_DEMO_UPGRADE,
      GAME_SONG_DOWNLOADING,
      GAME_LOAD,
      GAME_PLAYING,
      GAME_PAUSED,
      GAME_SETTINGS,
      GAME_INSTRUCTIONS_MENU,
      GAME_INSTRUCTIONS,
      GAME_VOLUME,
      GAME_VIBRATION,
      GAME_QUIT_CONFIRM,
      GAME_SAVE_WARNING,
      GAME_SAVE_SUCCESS,
      GAME_SAVE_ERROR,
      GAME_LOAD_ERROR,
      GAME_RESULT,
      GAME_FAILED,
      GAME_ENTER_SCORE,
      GAME_ENTER_SCORE_ERROR,
      GAME_SCORES,
      GAME_RESTART_CONFIRM,
      GAME_PLAY_ENCORE_CONFIRM,
      GAME_UNLOCK_GUITAR,
      GAME_UNLOCK_DRUMS,
      GAME_UNLOCK_ACHIEVEMENT,
      GAME_ZUNE_RUNNING,
    }
  }
}
