// Decompiled with JetBrains decompiler
// Type: com.glu.game.CVolumeScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CVolumeScene : CStateMachineNode
  {
    public const int VOLUME_HIGH_VALUE = 10;
    public const int VOLUME_MEDIUM_HIGH_VALUE = 8;
    public const int VOLUME_MEDIUM_VALUE = 6;
    public const int VOLUME_MEDIUM_LOW_VALUE = 4;
    public const int VOLUME_LOW_VALUE = 2;
    public const int VOLUME_OFF_VALUE = 0;
    public const int VOLUME_ITEM_COUNT = 6;
    protected bool m_inset;
    protected string[] m_volumeItems = new string[6];
    protected int m_volumeHigh;
    protected int m_volumeMediumHigh;
    protected int m_volumeMedium;
    protected int m_volumeMediumLow;
    protected int m_volumeLow;
    protected int m_volumeOff;

    public void SetInset(bool inset) => this.m_inset = inset;

    public CVolumeScene() => this.m_inset = false;

    public override uint Start()
    {
      uint num = base.Start();
      this.ChangeState(1, 1);
      return num;
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          int num1 = 0;
          CMenuScreen cmenuScreen = new CMenuScreen();
          int items;
          if (CGameApp.GetInstance().IsVolumeControlDisabled())
          {
            cmenuScreen.SetTitle("IDS_MENU_SOUND");
            string[] volumeItems1 = this.m_volumeItems;
            int index1 = num1;
            int num2 = index1 + 1;
            volumeItems1[index1] = "IDS_OPTIONS_SOUNDON";
            string[] volumeItems2 = this.m_volumeItems;
            int index2 = num2;
            items = index2 + 1;
            volumeItems2[index2] = "IDS_OPTIONS_SOUNDOFF";
          }
          else
          {
            cmenuScreen.SetTitle("IDS_MENU_VOLUME");
            this.m_volumeHigh = num1;
            string[] volumeItems3 = this.m_volumeItems;
            int index3 = num1;
            int num3 = index3 + 1;
            volumeItems3[index3] = "IDS_VOLUME_HIGH";
            this.m_volumeMediumHigh = num3;
            string[] volumeItems4 = this.m_volumeItems;
            int index4 = num3;
            int num4 = index4 + 1;
            volumeItems4[index4] = "IDS_VOLUME_MEDIUM_HIGH";
            this.m_volumeMedium = num4;
            string[] volumeItems5 = this.m_volumeItems;
            int index5 = num4;
            int num5 = index5 + 1;
            volumeItems5[index5] = "IDS_VOLUME_MEDIUM";
            this.m_volumeMediumLow = num5;
            string[] volumeItems6 = this.m_volumeItems;
            int index6 = num5;
            int num6 = index6 + 1;
            volumeItems6[index6] = "IDS_VOLUME_MEDIUM_LOW";
            this.m_volumeLow = num6;
            string[] volumeItems7 = this.m_volumeItems;
            int index7 = num6;
            int num7 = index7 + 1;
            volumeItems7[index7] = "IDS_VOLUME_LOW";
            this.m_volumeOff = num7;
            string[] volumeItems8 = this.m_volumeItems;
            int index8 = num7;
            items = index8 + 1;
            volumeItems8[index8] = "IDS_VOLUME_OFF";
          }
          cmenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen.SetMenu(this.m_volumeItems, items);
          cmenuScreen.ForceDisableSlider();
          cmenuScreen.SetDontPlaySFXOnAdvance();
          cmenuScreen.SetInset(30, 0);
          cmenuScreen.SetFlags(1);
          cmenuScreen.SetMovie("GLU_MOVIE_COMMON");
          if (ICMediaPlayer.GetInstance().GetSoundEnabled())
          {
            int volume = (int) ICMediaPlayer.GetInstance().GetVolume();
            int num8 = 5 - volume / 2;
            int idx;
            if (!CGameApp.GetInstance().IsVolumeControlDisabled())
            {
              idx = -1;
              while (idx < 0)
              {
                switch (volume)
                {
                  case 0:
                    idx = this.m_volumeOff;
                    break;
                  case 2:
                    idx = this.m_volumeLow;
                    break;
                  case 4:
                    idx = this.m_volumeMediumLow;
                    break;
                  case 6:
                    idx = this.m_volumeMedium;
                    break;
                  case 8:
                    idx = this.m_volumeMediumHigh;
                    break;
                  case 10:
                    idx = this.m_volumeHigh;
                    break;
                }
                if (idx < 0)
                  --volume;
              }
            }
            else
              idx = volume == 0 ? items - 1 : 0;
            cmenuScreen.SetSelection(idx);
          }
          else
            cmenuScreen.SetSelection(items - 1);
          cnode = (CNode) cmenuScreen;
          break;
        case 2:
          CTextScreen ctextScreen = new CTextScreen();
          ctextScreen.SetTitle("IDS_SAVE_ERROR_TITLE");
          if (this.m_inset)
            ctextScreen.SetInset(30, 70);
          ctextScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen.SetMovie("GLU_MOVIE_COMMON");
          ctextScreen.SetText("IDS_SAVE_ERROR_TEXT");
          ctextScreen.SetInset(30, 0);
          cnode = (CNode) ctextScreen;
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
            if ("IDS_VOLUME_OFF" == this.m_volumeItems[((CMenuScreen) pState).GetSelection()])
            {
              COptionsMgr.SetSoundEnabled(false);
              ICMediaPlayer.GetInstance().SetSoundEnabled(COptionsMgr.GetSoundEnabled());
            }
            else
            {
              COptionsMgr.SetSoundEnabled(true);
              ICMediaPlayer.GetInstance().SetSoundEnabled(COptionsMgr.GetSoundEnabled());
              if (COptionsMgr.GetSoundEnabled())
              {
                COptionsMgr.SetVibrationEnabled(false);
                ICMediaPlayer.GetInstance().SetVibrationEnabled(false);
              }
            }
            switch (this.m_volumeItems[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_VOLUME_OFF":
                ICMediaPlayer.GetInstance().SetVolume((byte) 0);
                COptionsMgr.SetVolume((byte) 0);
                break;
              case "IDS_VOLUME_HIGH":
                ICMediaPlayer.GetInstance().SetVolume((byte) 10);
                COptionsMgr.SetVolume((byte) 10);
                break;
              case "IDS_VOLUME_MEDIUM_HIGH":
                ICMediaPlayer.GetInstance().SetVolume((byte) 8);
                COptionsMgr.SetVolume((byte) 8);
                break;
              case "IDS_VOLUME_MEDIUM":
                ICMediaPlayer.GetInstance().SetVolume((byte) 6);
                COptionsMgr.SetVolume((byte) 6);
                break;
              case "IDS_VOLUME_MEDIUM_LOW":
                ICMediaPlayer.GetInstance().SetVolume((byte) 4);
                COptionsMgr.SetVolume((byte) 4);
                break;
              case "IDS_VOLUME_LOW":
                ICMediaPlayer.GetInstance().SetVolume((byte) 2);
                COptionsMgr.SetVolume((byte) 2);
                break;
              case "IDS_OPTIONS_SOUNDON":
                ICMediaPlayer.GetInstance().SetSoundEnabled(true);
                COptionsMgr.SetSoundEnabled(true);
                break;
              case "IDS_OPTIONS_SOUNDOFF":
                ICMediaPlayer.GetInstance().SetSoundEnabled(false);
                COptionsMgr.SetSoundEnabled(false);
                break;
            }
            if (!COptionsMgr.Write())
            {
              this.ChangeState(2, 2);
              break;
            }
            this.SetInterrupt(1);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 2:
          this.ChangeState(1, 2);
          break;
      }
    }

    public enum eOptionsState
    {
      VOLUME_NONE,
      VOLUME_CHANGE,
      VOLUME_SAVE_ERROR,
    }
  }
}
