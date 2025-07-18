// Decompiled with JetBrains decompiler
// Type: com.glu.game.CVibrationScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CVibrationScene : CStateMachineNode
  {
    public const int VIBRATION_OFF = 0;
    public const int VIBRATION_ON = 1;
    public const int VIBRATION_ITEM_COUNT = 2;
    public const int VIBRATION_TIME_IN_MS = 500;
    protected bool m_inset;
    protected string[] m_vibrationItems = new string[2];

    public void SetInset(bool inset) => this.m_inset = inset;

    public CVibrationScene() => this.m_inset = false;

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
          string[] vibrationItems1 = this.m_vibrationItems;
          int index1 = num1;
          int num2 = index1 + 1;
          vibrationItems1[index1] = "IDS_VIBRATION_ON";
          string[] vibrationItems2 = this.m_vibrationItems;
          int index2 = num2;
          int items = index2 + 1;
          vibrationItems2[index2] = "IDS_VIBRATION_OFF";
          CMenuScreen cmenuScreen = new CMenuScreen();
          cmenuScreen.SetTitle("IDS_MENU_VIBRATION");
          cmenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen.SetMenu(this.m_vibrationItems, items);
          cmenuScreen.SetFlags(5);
          cmenuScreen.SetMovie("GLU_MOVIE_COMMON");
          bool flag = !ICMediaPlayer.GetInstance().GetVibrationEnabled();
          cmenuScreen.SetSelection(flag ? 1 : 0);
          cnode = (CNode) cmenuScreen;
          break;
        case 2:
          CTextScreen ctextScreen = new CTextScreen();
          ctextScreen.SetTitle("IDS_SAVE_ERROR_TITLE");
          ctextScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen.SetMovie("GLU_MOVIE_COMMON");
          if (this.m_inset)
            ctextScreen.SetInset(30, 70);
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
            switch (this.m_vibrationItems[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_VIBRATION_ON":
                COptionsMgr.SetVibrationEnabled(true);
                ICMediaPlayer.GetInstance().SetVibrationEnabled(true);
                if (false)
                  ICMediaPlayer.GetInstance().SetSoundEnabled(false);
                int num = (int) ICMediaPlayer.GetInstance().Vibrate(500U);
                break;
              case "IDS_VIBRATION_OFF":
                COptionsMgr.SetVibrationEnabled(false);
                ICMediaPlayer.GetInstance().SetVibrationEnabled(false);
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
      VIBRATION_NONE,
      VIBRATION_CHANGE,
      VIBRATION_SAVE_ERROR,
    }
  }
}
