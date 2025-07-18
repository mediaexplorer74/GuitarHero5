// Decompiled with JetBrains decompiler
// Type: com.glu.game.CDifficultyScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CDifficultyScene : CStateMachineNode
  {
    protected bool m_inset;
    protected string[] m_optionItems = new string[3];
    public int DIFFICULTY_STANDARD_DPLPGM_CMP_NUM = 512;
    public int DIFFICULTY_STANDARD_DPLPGM_VTX_SIZE = 204800;

    public void SetInset(bool inset) => this.m_inset = inset;

    public CDifficultyScene() => this.m_inset = false;

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
          string[] optionItems1 = this.m_optionItems;
          int index1 = num1;
          int num2 = index1 + 1;
          optionItems1[index1] = "IDS_EASY";
          string[] optionItems2 = this.m_optionItems;
          int index2 = num2;
          int num3 = index2 + 1;
          optionItems2[index2] = "IDS_MEDIUM";
          string[] optionItems3 = this.m_optionItems;
          int index3 = num3;
          int items = index3 + 1;
          optionItems3[index3] = "IDS_EXPERT";
          CMenuScreen cmenuScreen = new CMenuScreen();
          cmenuScreen.SetTitle("IDS_DIFFICULTY");
          cmenuScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cmenuScreen.SetMenu(this.m_optionItems, items);
          cmenuScreen.SetFlags(5);
          cmenuScreen.SetMovie("GLU_MOVIE_COMMON");
          cnode = (CNode) cmenuScreen;
          break;
        case 2:
          CTextScreen ctextScreen = new CTextScreen();
          ctextScreen.SetTitle("IDS_SAVE_ERROR_TITLE");
          if (this.m_inset)
            ctextScreen.SetInset(30, 70);
          ctextScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
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
            switch (this.m_optionItems[((CMenuScreen) pState).GetSelection()])
            {
              case "IDS_EASY":
                CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EASY;
                break;
              case "IDS_MEDIUM":
                CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_MEDIUM;
                break;
              case "IDS_EXPERT":
                CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT;
                break;
            }
            pState.ClearInterrupt();
            this.SetInterrupt(1);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 2:
          this.SetInterrupt(1);
          break;
      }
    }

    public enum eOptionsState
    {
      DIFFICULTY_NONE,
      DIFFICULTY_CHANGE,
      DIFFICULTY_SAVE_ERROR,
    }
  }
}
