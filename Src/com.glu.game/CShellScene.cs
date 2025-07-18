// Decompiled with JetBrains decompiler
// Type: com.glu.game.CShellScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using System;

#nullable disable
namespace com.glu.game
{
  internal class CShellScene : CStateMachineNode
  {
    public const int SHELL_STANDARD_DPLPGM_CMP_NUM = 512;
    public const int SHELL_STANDARD_DPLPGM_VTX_SIZE = 307200;
    public static string[] leaderboardNames;
    public static long[] leaderboardScores;
    public static bool leaderboardLoadingDone;
    public static string longLeaderboardString;
    protected string[] m_mainMenuItems = new string[25];
    protected string m_pwszLanguageSelectItems;
    protected CMedia m_pSoundFX;
    protected uint m_soundId;
    public int m_Leaderboard;
    public bool m_isSignedIn;

    public override uint Start()
    {
      uint num1 = base.Start();
      CGHStaticData.GetInstance();
      if (CGHStaticData.m_GOTO_SET_LIST || CGHStaticData.m_GOTO_SONG_LOADING)
      {
        CNode pNode = (CNode) null;
        int state1 = (int) this.OnCreateState(out pNode, 1);
        int num2 = (int) this.PushState(1, pNode);
        int state2 = (int) this.OnCreateState(out pNode, 2);
        int num3 = (int) this.PushState(2, pNode);
        if (CGHStaticData.m_GOTO_SET_LIST)
        {
          CGuitarHeroGame.ClearLoadingTip();
          this.ChangeState(3, 1);
        }
        if (CGHStaticData.m_GOTO_SONG_LOADING)
        {
          int state3 = (int) this.OnCreateState(out pNode, 3);
          int num4 = (int) this.PushState(3, pNode);
          CGuitarHeroGame.SetLoadingTip();
          this.ChangeState(30, 1);
        }
      }
      else if (CApplet.displayTitleUpdateMessage)
      {
        this.ChangeState(39, 1);
      }
      else
      {
        CGuitarHeroGame.ClearLoadingTip();
        this.ChangeState(1, 1);
      }
      CGHStaticData.m_GOTO_SONG_LOADING = false;
      return num1;
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          int num1 = 0;
          if (CDemoMgr.IsDemo())
            this.m_mainMenuItems[num1++] = "IDS_DEMO_MENU";
          string[] mainMenuItems1 = this.m_mainMenuItems;
          int index1 = num1;
          int num2 = index1 + 1;
          mainMenuItems1[index1] = "IDS_CAREER";
          string[] mainMenuItems2 = this.m_mainMenuItems;
          int index2 = num2;
          int num3 = index2 + 1;
          mainMenuItems2[index2] = "IDS_PRACTICE";
          string[] mainMenuItems3 = this.m_mainMenuItems;
          int index3 = num3;
          int num4 = index3 + 1;
          mainMenuItems3[index3] = "IDS_APPEARANCE";
          if (!CDemoMgr.IsDemo())
          {
            string[] mainMenuItems4 = this.m_mainMenuItems;
            int index4 = num4;
            int num5 = index4 + 1;
            mainMenuItems4[index4] = "IDS_ACHIEVEMENTS";
            string[] mainMenuItems5 = this.m_mainMenuItems;
            int index5 = num5;
            num4 = index5 + 1;
            mainMenuItems5[index5] = "IDS_MENU_SCORES";
          }
          string[] mainMenuItems6 = this.m_mainMenuItems;
          int index6 = num4;
          int num6 = index6 + 1;
          mainMenuItems6[index6] = "IDS_MENU_OPTIONS";
          string[] mainMenuItems7 = this.m_mainMenuItems;
          int index7 = num6;
          int items = index7 + 1;
          mainMenuItems7[index7] = "IDS_MENU_EXIT";
          CMenuScreen cmenuScreen1 = new CMenuScreen();
          cmenuScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen1.SetMenu(this.m_mainMenuItems, items);
          cmenuScreen1.SetFlags(5);
          cmenuScreen1.SetScroll(true);
          cmenuScreen1.SetMovie("GLU_MOVIE_MOVIE");
          cnode = (CNode) cmenuScreen1;
          break;
        case 2:
          CDifficultyScene cdifficultyScene = new CDifficultyScene();
          cdifficultyScene.SetInset(false);
          cnode = (CNode) cdifficultyScene;
          CGHStaticData.m_bWasPreviouslyInRegistrationScreen = false;
          break;
        case 3:
          CSetListScene csetListScene = new CSetListScene();
          csetListScene.SetInset(false);
          cnode = (CNode) csetListScene;
          break;
        case 4:
          if (CGameApp.GetInstance().m_allowLiveCalls)
          {
            CAchievementsScene cachievementsScene = new CAchievementsScene();
            cachievementsScene.SetInset(false);
            cnode = (CNode) cachievementsScene;
            break;
          }
          this.ChangeState(39, 2);
          break;
        case 5:
          this.m_pSoundFX = (CMedia) null;
          CResourceManager resourceManager = CApp.GetResourceManager();
          CResource resource1 = (CResource) null;
          this.m_soundId = 0U;
          int resource2 = (int) resourceManager.CreateResource("IDM_PROMPT", out resource1);
          if (resource1 != null)
            this.m_pSoundFX = (CMedia) resource1.GetData();
          CSettingsScreen csettingsScreen = new CSettingsScreen();
          csettingsScreen.SetTitle("IDS_MENU_OPTIONS");
          csettingsScreen.SetFlags(5);
          csettingsScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          csettingsScreen.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) csettingsScreen;
          break;
        case 6:
        case 31:
          cnode = (CNode) new CAppearanceScene();
          break;
        case 7:
          CVolumeScene cvolumeScene = new CVolumeScene();
          cvolumeScene.SetInset(false);
          cnode = (CNode) cvolumeScene;
          break;
        case 8:
          CVibrationScene cvibrationScene = new CVibrationScene();
          cvibrationScene.SetInset(false);
          cnode = (CNode) cvibrationScene;
          break;
        case 9:
          COptionsScene coptionsScene = new COptionsScene();
          coptionsScene.SetInset(false);
          cnode = (CNode) coptionsScene;
          break;
        case 10:
          CHelpScreen chelpScreen = new CHelpScreen();
          chelpScreen.SetTitle("IDS_MENU_HELP");
          chelpScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          chelpScreen.SetMovie("GLU_MOVIE_COMMON");
          chelpScreen.SetFlags(5);
          cnode = (CNode) chelpScreen;
          break;
        case 11:
          CInstructionsMenuScreen cinstructionsMenuScreen = new CInstructionsMenuScreen();
          cinstructionsMenuScreen.SetTitle("IDS_MENU_INSTRUCTIONS");
          cinstructionsMenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cinstructionsMenuScreen.SetMovie("GLU_MOVIE_COMMON");
          cinstructionsMenuScreen.SetFlags(5);
          cnode = (CNode) cinstructionsMenuScreen;
          break;
        case 12:
          CInstructionsScreen cinstructionsScreen = new CInstructionsScreen();
          cinstructionsScreen.SetSoftkeys("ID_UNDEFINED", "SUR_SOFTKEY_ARROW");
          cinstructionsScreen.SetMovie("GLU_MOVIE_COMMON");
          cinstructionsScreen.EnableSliderBar();
          cnode = (CNode) cinstructionsScreen;
          break;
        case 13:
          CLeaderboardScreen cleaderboardScreen = new CLeaderboardScreen();
          cleaderboardScreen.SetTitle("IDS_HIGHSCORES_TITLE");
          cleaderboardScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          cleaderboardScreen.SetMovie("GLU_MOVIE_LEADERBOARDS");
          cnode = (CNode) cleaderboardScreen;
          break;
        case 14:
          CMenuScreen cmenuScreen2 = new CMenuScreen();
          cmenuScreen2.SetTitle("IDS_CLEAR_SONGS_CONFIRM_TITLE");
          cmenuScreen2.SetTextWithID("IDS_CLEAR_SONGS_CONFIRM_TEXT");
          cmenuScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen2.SetInset(30, 0);
          cmenuScreen2.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen2.SetFlags(5);
          cmenuScreen2.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen2;
          break;
        case 15:
          CTextScreen ctextScreen1 = new CTextScreen();
          ctextScreen1.SetTitle("IDS_MENU_CLEAR_SONGS");
          ctextScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen1.SetText("IDS_CLEAR_SONGS_TEXT");
          ctextScreen1.SetFlags(7);
          ctextScreen1.SetInset(30, 0);
          ctextScreen1.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen1;
          break;
        case 16:
        case 19:
          CProgressScreen cprogressScreen1 = new CProgressScreen();
          if (id == 19)
            cprogressScreen1.SetInfo(4278190080U, (string) null, "IDS_RESET_DATA_PROGRESS_TEXT");
          else
            cprogressScreen1.SetInfo(4278190080U, (string) null, "IDS_CLEAR_SONGS_PROGRESS_TEXT");
          cprogressScreen1.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          cprogressScreen1.SetProgressPercent(0);
          cprogressScreen1.SetMovie("GLU_MOVIE_DOWNLOAD");
          cnode = (CNode) cprogressScreen1;
          break;
        case 17:
          CMenuScreen cmenuScreen3 = new CMenuScreen();
          cmenuScreen3.SetTitle("IDS_RESET_DATA_CONFIRM_TITLE");
          cmenuScreen3.SetTextWithID("IDS_RESET_DATA_CONFIRM_TEXT");
          cmenuScreen3.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen3.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen3.SetInset(30, 0);
          cmenuScreen3.SetFlags(5);
          cmenuScreen3.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen3.ForceDisableSlider();
          cnode = (CNode) cmenuScreen3;
          break;
        case 18:
          CTextScreen ctextScreen2 = new CTextScreen();
          ctextScreen2.SetTitle("IDS_MENU_RESET_DATA");
          ctextScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen2.SetInset(30, 0);
          ctextScreen2.SetText("IDS_RESET_DATA_TEXT");
          ctextScreen2.SetFlags(7);
          ctextScreen2.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen2;
          break;
        case 20:
          CTextScreen ctextScreen3 = new CTextScreen();
          ctextScreen3.SetTitle("IDS_MENU_ABOUT");
          ctextScreen3.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen3.SetAboutText();
          ctextScreen3.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen3.EnableSliderBar();
          ctextScreen3.SetInset(30, 0);
          cnode = (CNode) ctextScreen3;
          break;
        case 23:
          CTextScreen ctextScreen4 = new CTextScreen();
          ctextScreen4.SetTitle("IDS_DEMO_TITLE");
          ctextScreen4.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen4.SetTextDirectly(CDemoMgr.GetUpgradePromptText());
          ctextScreen4.SetInset(30, 0);
          ctextScreen4.SetFlags(5);
          ctextScreen4.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen4.ForceDisableSlider();
          cnode = (CNode) ctextScreen4;
          break;
        case 25:
        case 26:
        case 27:
          CTextScreen ctextScreen5 = new CTextScreen();
          ctextScreen5.SetTitle("IDS_SAVE_ERROR_TITLE");
          ctextScreen5.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen5.SetText("IDS_SAVE_ERROR_TEXT");
          ctextScreen5.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen5.SetFlags(5);
          ctextScreen5.SetInset(30, 0);
          cnode = (CNode) ctextScreen5;
          break;
        case 29:
          this.ChangeState(3, 1);
          break;
        case 30:
          this.SetInterrupt(1);
          break;
        case 32:
          CImageMenuScreen cimageMenuScreen = new CImageMenuScreen();
          cimageMenuScreen.SetTitle("IDS_INSTRUCTIONS_TITLE");
          cimageMenuScreen.SetInfo("KEYSET_UI_INSTRUMENT");
          cimageMenuScreen.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cnode = (CNode) cimageMenuScreen;
          break;
        case 35:
          if (CGameApp.GetInstance().m_allowLiveCalls)
          {
            CMenuScreen cmenuScreen4 = new CMenuScreen();
            string[] pResId = new string[3]
            {
              "IDS_INSTRUMENT_GUITAR",
              "IDS_INSTRUMENT_BASS",
              "IDS_INSTRUMENT_DRUMS"
            };
            cmenuScreen4.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
            cmenuScreen4.SetMenu(pResId, pResId.Length);
            cmenuScreen4.SetScroll(true);
            cmenuScreen4.SetFlags(5);
            cmenuScreen4.SetMovie("GLU_MOVIE_MOVIE");
            cnode = (CNode) cmenuScreen4;
            break;
          }
          this.ChangeState(39, 2);
          break;
        case 36:
          CShellScene.leaderboardLoadingDone = false;
          if (LiveLeaderBoards.GetInstance().ReadLeaderboardEntries(this.m_Leaderboard, false, 10, (AsyncCallback) null))
          {
            CProgressScreen cprogressScreen2 = new CProgressScreen();
            cprogressScreen2.SetInfo(4278190080U, (string) null, "IDS_LOADING");
            cprogressScreen2.SetTipsText(CGHStaticData.m_loadingScreenTipResourceID);
            cprogressScreen2.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
            cprogressScreen2.SetProgressPercent(0);
            cprogressScreen2.SetMovie("GLU_MOVIE_DOWNLOAD");
            cnode = (CNode) cprogressScreen2;
            break;
          }
          CTextScreen ctextScreen6 = new CTextScreen();
          ctextScreen6.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen6.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen6.SetInset(30, 0);
          ctextScreen6.SetText("IDS_ERRORMSG_LOGIN_TO_LIVE_FOR_LEADERBOARDS");
          cnode = (CNode) ctextScreen6;
          break;
        case 37:
          CTextScreen ctextScreen7 = new CTextScreen();
          ctextScreen7.SetTitle("IDS_HIGHSCORES_TITLE");
          ctextScreen7.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen7.SetInset(30, 0);
          ctextScreen7.SetMovie("GLU_MOVIE_COMMON");
          if (CShellScene.longLeaderboardString.Equals(""))
            ctextScreen7.SetText("IDS_ERRORMSG_LOGIN_TO_LIVE_FOR_LEADERBOARDS");
          else
            ctextScreen7.SetTextDirectly(CShellScene.longLeaderboardString);
          cnode = (CNode) ctextScreen7;
          break;
        case 38:
          CSong songSelected = CSongListMgr.GetSongSelected();
          string output = (string) null;
          CUtility.GetString(out output, "IDS_PLAY_NOW");
          string pwszText = songSelected.GetSongName() + "\n" + songSelected.GetBandName() + "\n" + output;
          CMenuScreen cmenuScreen5 = new CMenuScreen();
          cmenuScreen5.SetTitle("IDS_ENCORE_UNLOCKED");
          cmenuScreen5.SetText(pwszText);
          cmenuScreen5.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          cmenuScreen5.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen5.SetFlags(5);
          cmenuScreen5.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen5;
          break;
        case 39:
          CMenuScreen cmenuScreen6 = new CMenuScreen();
          cmenuScreen6.SetTextWithID("IDS_UPDATE_TEXT");
          cmenuScreen6.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen6.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen6.SetInset(40, 0);
          cmenuScreen6.SetFlags(5);
          cmenuScreen6.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen6.ForceDisableSlider();
          cnode = (CNode) cmenuScreen6;
          break;
        case 42:
          CMenuScreen cmenuScreen7 = new CMenuScreen();
          cmenuScreen7.SetTitle("IDS_EXIT_MENU");
          cmenuScreen7.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen7.SetMenu(Consts.kTableMenuConfirmation, 2);
          cmenuScreen7.SetFlags(5);
          cmenuScreen7.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen7;
          break;
        case 44:
          CTextScreen ctextScreen8 = new CTextScreen();
          ctextScreen8.SetTitle("IDS_DEMO_EXPIRED_TITLE");
          ctextScreen8.SetText("IDS_DEMO_UPGRADE_NOT_AVAILABLE");
          ctextScreen8.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          ctextScreen8.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) ctextScreen8;
          break;
      }
      pOut = cnode;
      return state;
    }

    protected override void OnStateInterrupt(int id, CNode pState)
    {
      switch (id)
      {
        case 1:
          if (pState.GetInterrupt() == 1)
          {
            CGHStaticData.m_bWasPreviouslyInRegistrationScreen = false;
            CGHStaticData.m_GOTO_SET_LIST = false;
            switch (this.m_mainMenuItems[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_PRACTICE":
                CGHStaticData.m_gameplayMode = 1;
                this.ChangeState(2, 1);
                return;
              case "IDS_CAREER":
                CGHStaticData.m_gameplayMode = 0;
                this.ChangeState(2, 1);
                return;
              case "IDS_ACHIEVEMENTS":
                this.ChangeState(4, 1);
                return;
              case "IDS_MENU_CONTINUE":
                this.SetInterrupt(2);
                return;
              case "IDS_MENU_SCORES":
                this.ChangeState(35, 1);
                return;
              case "IDS_APPEARANCE":
                this.ChangeState(6, 1);
                return;
              case "IDS_MENU_OPTIONS":
                this.ChangeState(5, 1);
                return;
              case "IDS_MENU_HELP":
                this.ChangeState(10, 1);
                return;
              case "IDS_MENU_INSTRUCTIONS":
                this.ChangeState(11, 1);
                return;
              case "IDS_MENU_CLEAR_SONGS":
                this.ChangeState(14, 1);
                return;
              case "IDS_MENU_RESET_DATA":
                this.ChangeState(17, 1);
                return;
              case "IDS_DEMO_MENU":
                this.ChangeState(23, 1);
                return;
              case "IDS_MENU_MORE_GAMES":
                CDemoMgr.LaunchUpSell();
                return;
              case "IDS_MENU_ABOUT":
                this.ChangeState(20, 1);
                return;
              case "IDS_MENU_EXIT":
                this.ChangeState(42, 1);
                return;
              case "IDS_MENU_LANGUAGE_SELECT":
                this.ChangeState(41, 1);
                return;
              case null:
                return;
              default:
                return;
            }
          }
          else
          {
            this.ChangeState(42, 1);
            break;
          }
        case 2:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(3, 1);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 3:
          if (pState.GetInterrupt() == 1)
          {
            CSong songSelected = CSongListMgr.GetSongSelected();
            int songMediaLocal = (int) songSelected.GetSongMediaLocal();
            int num = (int) songSelected.Gebyte(CSongListMgr.GetNumTracksSupported(), (Consts.eInstrument) CGHStaticData.m_instrumentBeingPlayed);
            bool flag = true;
            CGuitarHeroGame.SetLoadingTip();
            CGHStaticData.m_bSongAlreadyDownloaded = flag;
            this.SetInterrupt(1);
            break;
          }
          CNode pNode = (CNode) null;
          this.ClearStates();
          int state1 = (int) this.OnCreateState(out pNode, 1);
          int num1 = (int) this.PushState(1, pNode);
          int state2 = (int) this.OnCreateState(out pNode, 2);
          int num2 = (int) this.PushState(2, pNode);
          break;
        case 4:
        case 9:
        case 12:
        case 13:
        case 20:
        case 21:
        case 22:
        case 24:
          this.ChangeState(0, 4);
          break;
        case 5:
        case 40:
          if (pState.GetInterrupt() == 1)
          {
            int num3 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
            switch (((CSettingsScreen) pState).GetSelectionMenuStringID())
            {
              case "IDS_MENU_VOLUME":
              case "IDS_MENU_SOUND":
                this.ChangeState(7, 1);
                return;
              case "IDS_MENU_LANGUAGE_SELECT":
                this.ChangeState(41, 1);
                return;
              case "IDS_MENU_VIBRATION":
                this.ChangeState(8, 1);
                return;
              case "IDS_MENU_CLEAR_SONGS":
                this.ChangeState(14, 1);
                return;
              case "IDS_MENU_RESET_DATA":
                this.ChangeState(17, 1);
                return;
              case "IDS_MENU_INSTRUCTIONS":
                this.ChangeState(11, 1);
                return;
              case "IDS_MENU_ABOUT":
                this.ChangeState(20, 1);
                return;
              default:
                this.ChangeState(1, 3);
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
            int num4 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
            this.ChangeState(1, 3);
            break;
          }
        case 6:
          if (CSaveGameMgr.GetInstance().Write())
          {
            this.ChangeState(0, 4);
            break;
          }
          this.ChangeState(25, 2);
          break;
        case 7:
          if (pState.GetInterrupt() == 1)
            this.m_soundId = ICMediaPlayer.GetInstance().Play(this.m_pSoundFX);
          this.ChangeState(0, 4);
          break;
        case 8:
          this.ChangeState(0, 4);
          break;
        case 10:
          if (pState.GetInterrupt() == 1)
          {
            switch (((CMenuScreen) pState).GetSelection())
            {
              case 0:
                this.ChangeState(11, 1);
                return;
              case 1:
                this.ChangeState(20, 1);
                return;
              default:
                this.ChangeState(0, 4);
                return;
            }
          }
          else
          {
            int num5 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
            this.ChangeState(0, 4);
            break;
          }
        case 11:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(12, 1);
            break;
          }
          if (pState.GetInterrupt() != 2)
            break;
          this.ChangeState(0, 4);
          break;
        case 14:
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
                this.ChangeState(16, 2);
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
        case 15:
          if (pState.GetInterrupt() == 2)
          {
            this.ChangeState(0, 4);
            break;
          }
          this.ChangeState(5, 5);
          break;
        case 16:
        case 19:
          if (pState.GetInterrupt() != 2)
            break;
          this.ChangeState(0, 4);
          break;
        case 17:
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
                CHighscoreMgr.GetInstance().Reset();
                CSaveGameMgr.GetInstance().Reset();
                CSongScoreMgr.Reset();
                CSongListMgr.Reset();
                CGHStaticData.m_musicianAppearance = 0;
                CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EASY;
                CGHStaticData.m_instrumentBeingPlayed = CGHStaticData.eGameInstrument.GAME_INSTRUMENT_GUITAR;
                CGHStaticData.m_guitarAppearance = 0;
                CGHStaticData.m_drumsAppearance = 0;
                if (CSaveGameMgr.GetInstance().Write())
                {
                  this.ChangeState(19, 2);
                  return;
                }
                this.ChangeState(27, 2);
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
        case 18:
          if (pState.GetInterrupt() == 2)
          {
            this.ChangeState(0, 4);
            break;
          }
          this.ChangeState(5, 5);
          break;
        case 23:
          if (pState.GetInterrupt() == 1)
          {
            if (CDemoMgr.LaunchUpgrade())
            {
              this.ChangeState(0, 4);
              break;
            }
            this.ChangeState(24, 2);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 25:
        case 26:
        case 27:
          switch (id - 26)
          {
            case 0:
              this.ChangeState(14, 2);
              return;
            case 1:
              this.ChangeState(17, 2);
              return;
            default:
              this.ChangeState(1, 3);
              return;
          }
        case 28:
          this.ChangeState(0, 4);
          break;
        case 29:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(3, 2);
            break;
          }
          if (pState.GetInterrupt() != 2)
            break;
          this.ChangeState(0, 4);
          break;
        case 31:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(32, 2);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 32:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(5, 5);
            break;
          }
          this.ChangeState(31, 4);
          break;
        case 33:
          if (pState.GetInterrupt() == 2)
          {
            if (!CGHStaticData.m_bWasPreviouslyInRegistrationScreen)
            {
              this.ChangeState(0, 4);
              break;
            }
            CGHStaticData.m_bWasPreviouslyInRegistrationScreen = false;
            if (this.FindState(33) != null)
            {
              this.ChangeState(33, 5);
              break;
            }
            this.ChangeState(33, 2);
            break;
          }
          if (pState.GetInterrupt() != 1)
            break;
          this.ChangeState(2, 2);
          break;
        case 34:
          if (pState.GetInterrupt() == 2 || (int) CLeaderBoard.GetUserID() == (int) Consts.kDefaultUserId)
          {
            this.ChangeState(0, 4);
            break;
          }
          if (pState.GetInterrupt() != 1)
            break;
          this.ChangeState(36, 2);
          break;
        case 35:
          if (pState.GetInterrupt() == 1)
          {
            this.m_Leaderboard = ((CMenuScreen) pState).GetSelection();
            this.ChangeState(36, 1);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 36:
          if (LiveLeaderBoards.IsSignedInToLive())
          {
            if (pState.GetInterrupt() != 1)
              break;
            this.ChangeState(37, 2);
            break;
          }
          this.ChangeState(0, 4);
          break;
        case 37:
          if (pState.GetInterrupt() != 2)
            break;
          this.ChangeState(0, 4);
          break;
        case 38:
          if (pState.GetInterrupt() != 1)
            break;
          switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
          {
            case null:
              return;
            case "IDS_NO":
              this.ChangeState(3, 2);
              return;
            case "IDS_YES":
              CGuitarHeroGame.SetLoadingTip();
              this.SetInterrupt(1);
              return;
            default:
              return;
          }
        case 39:
          if (pState.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                //CApplet.GetInstance().gamerServiceInstance.Enabled = false;
                CGameApp.GetInstance().m_allowLiveCalls = false;
                this.ChangeState(1, 2);
                return;
              case "IDS_YES":
                //if (Guide.IsTrialMode)
                //{
                //    //Guide.ShowMarketplace(PlayerIndex.One);
                //}
                //else
                //{
                    //new MarketplaceDetailTask()
                    //{
                    //    ContentType = ((MarketplaceContentType)1)
                    //}.Show();
                //}
                this.ChangeState(0, 4); //?
                return;
              default:
                return;
            }
          }
          else
          {
            //CApplet.GetInstance().gamerServiceInstance.Enabled = false;
            CGameApp.GetInstance().m_allowLiveCalls = false;
            this.ChangeState(1, 2);
            break;
          }
        case 41:
          CShellScene.eShellState id1 = CShellScene.eShellState.SHELL_MAIN_MENU;
          if (pState.GetInterrupt() == 1)
          {
            COptionsMgr.GetInstance().SetLocale((uint) ((CMenuScreen) pState).GetSelection());
            if (COptionsMgr.Write())
            {
              this.ChangeState((int) id1, 3);
              break;
            }
            this.ChangeState(25, 2);
            break;
          }
          this.ChangeState((int) id1, 2);
          break;
        case 42:
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
                this.SetInterrupt(3);
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
        case 43:
          if (pState.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                this.SetInterrupt(3);
                return;
              case "IDS_YES":
                if (CDemoMgr.LaunchUpgrade())
                {
                  this.SetInterrupt(3);
                  return;
                }
                this.ChangeState(44, 2);
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
        case 44:
          if (pState.GetInterrupt() == 1)
          {
            this.SetInterrupt(3);
            break;
          }
          this.ChangeState(0, 4);
          break;
      }
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = base.HandleUpdate(timeElapsedMS);
      switch (this.m_stateID)
      {
        case 16:
        case 19:
          switch (CSongListMgr.PurgeSongCache())
          {
            case CSongListMgr.ePurgeResult.PURGE_FAILURE:
              if (this.m_stateID == 16)
              {
                this.ChangeState(26, 2);
                break;
              }
              this.ChangeState(27, 2);
              break;
            case CSongListMgr.ePurgeResult.PURGE_COMPLETE:
              this.ChangeState(5, 5);
              break;
            default:
              CProgressScreen pState1 = (CProgressScreen) this.m_pState;
              pState1.SetProgressPercent((pState1.GetProgressPercent() + 5) % 100);
              break;
          }
          flag = true;
          break;
        case 36:
          if (LiveLeaderBoards.IsSignedInToLive())
          {
            CShellScene.longLeaderboardString = "";
            CProgressScreen pState2 = (CProgressScreen) this.m_pState;
            pState2.SetProgressPercent((pState2.GetProgressPercent() + 5) % 100);
            if (CShellScene.leaderboardLoadingDone)
            {
              pState2.SetProgressComplete();
              if (CShellScene.leaderboardNames != null)
              {
                for (int index1 = 0; index1 < CShellScene.leaderboardNames.Length; ++index1)
                {
                  CShellScene.longLeaderboardString = CShellScene.longLeaderboardString + (object) (index1 + 1) + ". " + CShellScene.leaderboardNames[index1];
                  for (int index2 = 0; index2 < 20 - CShellScene.leaderboardNames[index1].Length; ++index2)
                    CShellScene.longLeaderboardString += " ";
                  CShellScene.longLeaderboardString += " ";
                  CShellScene.longLeaderboardString = CShellScene.longLeaderboardString + (object) CShellScene.leaderboardScores[index1] + "\n";
                }
              }
              this.ChangeState(37, 2);
              break;
            }
            break;
          }
          break;
      }
      return flag;
    }

    public enum eShellInterrupt
    {
      SHELL_INTERRUPT_NONE,
      SHELL_INTERRUPT_SINGLE_PLAY,
      SHELL_INTERRUPT_SINGLE_PLAY_CONTINUE,
      SHELL_INTERRUPT_EXIT,
    }

    public enum eShellState
    {
      SHELL_NONE,
      SHELL_MAIN_MENU,
      SHELL_DIFFICULTY,
      SHELL_SET_LIST,
      SHELL_ACHIEVEMENTS,
      SHELL_SETTINGS,
      SHELL_APPEARANCE,
      SHELL_VOLUME,
      SHELL_VIBRATION,
      SHELL_OPTIONS,
      SHELL_HELP,
      SHELL_INSTRUCTIONS_MENU,
      SHELL_INSTRUCTIONS,
      SHELL_SCORES,
      SHELL_CLEAR_SONGS_CONFIRM,
      SHELL_CLEAR_SONGS,
      SHELL_CLEAR_SONGS_PROGRESS,
      SHELL_RESET_DATA_CONFIRM,
      SHELL_RESET_DATA,
      SHELL_RESET_DATA_PROGRESS,
      SHELL_ABOUT,
      SHELL_COOL_FEATURE,
      SHELL_DEMO_UNAVAILABLE,
      SHELL_DEMO_UPGRADE_PROMPT,
      SHELL_DEMO_UPGRADE,
      SHELL_SAVE_ERROR,
      SHELL_CLEAR_SONGS_SAVE_ERROR,
      SHELL_RESET_DATA_SAVE_ERROR,
      SHELL_DEBUG_DELETE_USER,
      SHELL_SONGLIST_LOADING,
      SHELL_ENTER_GAME_LOADING,
      SHELL_CHOOSEROCKER,
      SHELL_CHOOSEINSTRUMENT,
      SHELL_REGISTRATION_NEW,
      SHELL_REGISTRATION_LEADERBOARD,
      SHELL_LEADERBOARD_TYPE,
      SHELL_LEADERBOARD_LOADING,
      SHELL_LEADERBOARD_VIEWING,
      SHELL_PLAY_ENCORE_CONFIRM,
      SHELL_UPDATE_REQUIRED,
      SHELL_HELP_AND_OPTIONS,
      SHELL_LANGUAGE_SELECT,
      SHELL_EXIT_CONFIRM,
      SHELL_DEMO_UPGRADE_EXIT_PROMPT,
      SHELL_DEMO_UPGRADE_EXIT,
      SHELL_LAST,
    }
  }
}
