// Decompiled with JetBrains decompiler
// Type: com.glu.game.CIntroScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace com.glu.game
{
  internal class CIntroScene : CStateMachineNode
  {
    protected string[] m_pwszLanguageSelectItems = new string[Consts.kMaxMenuItems];

    public override uint Start()
    {
      uint num = base.Start();
      if (!COptionsMgr.GetInstance().GetIntroTextSeen() && CGameApp.GetInstance().GetAppIntroText().Length > 0)
        this.ChangeState(3, 2);
      else if (CGameApp.GetInstance().IsNetWarnNeeded())
        this.ChangeState(6, 2);
      else if (ICCore.GetInstance().CanPlaySound())
      {
        if (!ICCore.GetInstance().IsSoundEnabled() && !CGameApp.GetInstance().IsMannerModeDisabled())
          this.ChangeState(5, 1);
        else
          this.ChangeState(6, 1);
      }
      else
        this.ChangeState(8, 1);
      SG_Home.GetInstance().Init();
      lock (CGameApp.loadQueuedLock)
      {
        SG_Home.GetInstance().QueueArchetypeCharacter(3, 0);
        do
          ;
        while (SG_Home.GetInstance().LoadQueued(CResBank.kResMaxTimePerUpdateMS, out bool _));
      }
      CGHStaticData.m_GOTO_SET_LIST = false;
      CGHStaticData.m_GOTO_SONG_LOADING = false;
      return num;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (id == 2535475076U)
        flag = true;
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      bool flag = base.HandleUpdate(timeElapsedMS);
      int stateId = this.m_stateID;
      return flag;
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          CResLoadScreen cresLoadScreen1 = new CResLoadScreen();
          cresLoadScreen1.SetInfo(4278190080U, (string) null, (string) null);
          cresLoadScreen1.SetSoftkeys((string) null, (string) null);
          cresLoadScreen1.SetInfo(4278190080U, (string) null, "IDS_LOADING");
          cresLoadScreen1.SetTitle("IDS_LOADING");
          cresLoadScreen1.SetProgressPercent(0);
          cnode = (CNode) cresLoadScreen1;
          break;
        case 2:
          CResLoadScreen cresLoadScreen2 = new CResLoadScreen();
          cresLoadScreen2.SetInfo(4278190080U, (string) null, (string) null);
          cresLoadScreen2.SetResInfo(CGameData.GetPreloadBank(), "KEYSET_RES_PRELOAD", 0);
          cresLoadScreen2.SetSoftkeys((string) null, (string) null);
          cresLoadScreen2.SetInfo(4278190080U, (string) null, "IDS_LOADING");
          cresLoadScreen2.SetTitle("IDS_LOADING");
          cresLoadScreen2.SetProgressPercent(0);
          cresLoadScreen2.SetMovie("GLU_MOVIE_DOWNLOAD");
          cnode = (CNode) cresLoadScreen2;
          break;
        case 3:
          CTextScreen ctextScreen1 = new CTextScreen();
          ctextScreen1.SetTitle("IDS_INTRO_TITLE");
          ctextScreen1.SetInset(30, 70);
          ctextScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          ctextScreen1.SetText(CGameApp.GetInstance().GetAppIntroText());
          ctextScreen1.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen1.SetInset(30, 0);
          cnode = (CNode) ctextScreen1;
          break;
        case 4:
          CMenuScreen cmenuScreen1 = new CMenuScreen();
          cmenuScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen1.SetFlags(5);
          cmenuScreen1.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen1.SetText("IDS_NETWORK_WARNING");
          cmenuScreen1.SetMenu(Consts.kTableMenuConfirmation, 2);
          cnode = (CNode) cmenuScreen1;
          break;
        case 5:
          CMenuScreen cmenuScreen2 = new CMenuScreen();
          cmenuScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          cmenuScreen2.SetFlags(5);
          cmenuScreen2.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen2.SetText("IDS_SOUND_MUTED_TEXT");
          cmenuScreen2.SetMenu(Consts.kTableMenuConfirmation, 2);
          cnode = (CNode) cmenuScreen2;
          break;
        case 6:
            //new Thread(new ThreadStart(CGameApp.ReadData)).Start();
            Task.Run(new Action(CGameApp.ReadData));
            COptionsMgr.SetSoundEnabled(true);
          if (COptionsMgr.GetInstance().GetVolume() == (byte) 0)
          {
            ICMediaPlayer.GetInstance().SetVolume((byte) 8);
            COptionsMgr.SetVolume((byte) 8);
          }
          ICMediaPlayer.GetInstance().SetSoundEnabled(COptionsMgr.GetSoundEnabled());
          ICMediaPlayer.GetInstance().SetVibrationEnabled(COptionsMgr.GetVibrationEnabled());
          if (!CGameApp.GetInstance().musicWasPlayingAtLaunch)
          {
            this.ChangeState(8, 3);
            break;
          }
          CMenuScreen cmenuScreen3 = new CMenuScreen();
          cmenuScreen3.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen3.SetFlags(5);
          cmenuScreen3.SetMovie("GLU_MOVIE_COMMON");
          cmenuScreen3.SetTitle("IDS_ENABLE_SOUND_TEXT");
          cmenuScreen3.SetTextWithID("IDS_CONFIRM_STOP_MUSIC");
          cmenuScreen3.SetMenu(Consts.kTableMenuConfirmation, 2);
          cnode = (CNode) cmenuScreen3;
          break;
        case 8:
          cnode = (CNode) new CGluLogoScreen();
          break;
        case 9:
          CImageScreen cimageScreen1 = new CImageScreen();
          cimageScreen1.SetInfo(uint.MaxValue, "SUR_LICENSOR", Consts.kScreenDisplayTimeMS);
          cnode = (CNode) cimageScreen1;
          break;
        case 10:
          CResourceManager resourceManager = CApp.GetResourceManager();
          CResource resource = (CResource) null;
          resourceManager.GetResource("IDM_CROWD_NEUTRAL_TO_POSITIVE_SFX", out resource);
          if (resource != null)
          {
            int num = (int) ICMediaPlayer.GetInstance().Play((CMedia) resource.GetData(), (byte) 0, (byte) 0);
          }
          CImageScreen cimageScreen2 = new CImageScreen();
          cimageScreen2.SetInfo(4278190080U, "SUR_TITLE", Consts.kScreenDisplayTimeMS);
          cimageScreen2.SetFlags(22);
          cnode = (CNode) cimageScreen2;
          break;
        case 11:
          CTextScreen ctextScreen2 = new CTextScreen();
          ctextScreen2.SetTitle("IDS_DATA_CORRUPTED_TITLE");
          ctextScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", (string) null);
          ctextScreen2.SetText("IDS_DATA_CORRUPTED_TEXT");
          cnode = (CNode) ctextScreen2;
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
            this.ChangeState(6, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 2:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(9, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 3:
          if (pState.GetInterrupt() == 1)
          {
            COptionsMgr.SetIntroTextSeen();
            COptionsMgr.Write();
            if (CGameApp.GetInstance().IsNetWarnNeeded())
            {
              this.ChangeState(4, 2);
              break;
            }
            if (ICCore.GetInstance().CanPlaySound())
            {
              if (!ICCore.GetInstance().IsSoundEnabled() && !CGameApp.GetInstance().IsMannerModeDisabled())
              {
                this.ChangeState(5, 1);
                break;
              }
              this.ChangeState(6, 1);
              break;
            }
            this.ChangeState(8, 1);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 4:
          CMenuScreen cmenuScreen = (CMenuScreen) pState;
          if (cmenuScreen.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                CGameApp.GetInstance().QueueExit();
                return;
              case "IDS_YES":
                if (ICCore.GetInstance().CanPlaySound())
                {
                  if (!ICCore.GetInstance().IsSoundEnabled() && !CGameApp.GetInstance().IsMannerModeDisabled())
                  {
                    this.ChangeState(5, 1);
                    return;
                  }
                  this.ChangeState(6, 1);
                  return;
                }
                this.ChangeState(8, 1);
                return;
              default:
                return;
            }
          }
          else
          {
            if (cmenuScreen.GetInterrupt() != 2)
              break;
            CGameApp.GetInstance().QueueExit();
            break;
          }
        case 5:
          if (pState.GetInterrupt() == 1)
          {
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case null:
                return;
              case "IDS_NO":
                CGameApp.GetInstance().QueueExit();
                return;
              case "IDS_YES":
                COptionsMgr.SetSoundEnabled(false);
                ICMediaPlayer.GetInstance().SetSoundEnabled(COptionsMgr.GetSoundEnabled());
                this.ChangeState(8, 2);
                return;
              default:
                return;
            }
          }
          else
          {
            if (pState.GetInterrupt() != 2)
              break;
            ICCore.GetInstance().ExitApp();
            break;
          }
        case 6:
          if (pState.GetInterrupt() == 1)
          {
            if (!CGameApp.GetInstance().IsMannerModeDisabled() && ICCore.GetInstance().CanPlaySound())
              ICCore.GetInstance().IsSoundEnabled();
            switch (Consts.kTableMenuConfirmation[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_YES":
                MediaPlayer.Stop();
                CGameApp.GetInstance().musicWasPlayingAtLaunch = false;
                break;
            }
            this.ChangeState(8, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 7:
          if (pState.GetInterrupt() == 1)
          {
            if (!COptionsMgr.GetInstance().GetIntroTextSeen() && CGameApp.GetInstance().GetAppIntroText().Length > 0)
            {
              this.ChangeState(3, 2);
              break;
            }
            if (CGameApp.GetInstance().IsNetWarnNeeded())
            {
              this.ChangeState(4, 2);
              break;
            }
            if (ICCore.GetInstance().CanPlaySound())
            {
              if (!ICCore.GetInstance().IsSoundEnabled() && !CGameApp.GetInstance().IsMannerModeDisabled())
              {
                this.ChangeState(5, 2);
                break;
              }
              this.ChangeState(6, 2);
              break;
            }
            this.ChangeState(8, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 8:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(2, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 9:
          if (pState.GetInterrupt() == 1)
          {
            this.ChangeState(10, 2);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 10:
          if (pState.GetInterrupt() == 1)
          {
            if (CSaveGameMgr.GetInstance().GetChecksumFailed())
            {
              this.ChangeState(11, 2);
              break;
            }
            this.SetInterrupt(1);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 11:
          if (pState.GetInterrupt() == 1)
          {
            this.SetInterrupt(1);
            break;
          }
          this.SetInterrupt(2);
          break;
      }
    }

    public enum eIntroState
    {
      INTRO_NONE,
      INTRO_START_SCREEN,
      INTRO_PRELOAD,
      INTRO_MESSAGE,
      INTRO_NETWORK_WARNING,
      INTRO_SOUND_MUTED,
      INTRO_ENABLE_SOUND,
      INTRO_LANGUAGE_SELECT,
      INTRO_GLU_LOGO,
      INTRO_LICENSOR,
      INTRO_SPLASH,
      INTRO_DATA_CORRUPTED,
      INTRO_LAST,
    }
  }
}
