// Decompiled with JetBrains decompiler
// Type: com.glu.game.CAchievementsScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CAchievementsScene : CStateMachineNode
  {
    protected bool m_inset;
    protected bool m_bDisplayingUnlockedAchievement;
    protected int m_singleItemIndex;
    public int ACHIEVEMENTS_STANDARD_DPLPGM_CMP_NUM = 512;
    public int ACHIEVEMENTS_STANDARD_DPLPGM_VTX_SIZE = 204800;

    public void SetInset(bool inset) => this.m_inset = inset;

    public void SetDisplaySingleItem(int itemIndex)
    {
      this.m_bDisplayingUnlockedAchievement = true;
      this.m_singleItemIndex = itemIndex;
    }

    public void CAchievmentsScene()
    {
      this.m_inset = false;
      this.m_bDisplayingUnlockedAchievement = false;
    }

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
          string[] pImageIds = new string[20];
          string[] ptextIds = new string[20];
          for (int achievementIndex = 0; achievementIndex < 20; ++achievementIndex)
          {
            pImageIds[achievementIndex] = CGHStaticData.UnlockedAchievement(achievementIndex) ? "SUR_UI_ACHIEVEMENT_" + (1 + achievementIndex).ToString("D2") : "SUR_UI_ACHIEVEMENT_LOCKED";
            ptextIds[achievementIndex] = "IDS_ACHIEVEMENT_TEXT_" + (1 + achievementIndex).ToString("D2");
          }
          CAchievementsScreen cachievementsScreen = new CAchievementsScreen();
          cachievementsScreen.SetTitle("IDS_ACHIEVEMENTS");
          if (this.m_inset)
            cachievementsScreen.SetInset(30, 70);
          cachievementsScreen.SetInfo(pImageIds, ptextIds, 20);
          cachievementsScreen.SetMovie("GLU_MOVIE_ACHIEVEMENTS");
          cachievementsScreen.SetSoftkeys(this.m_bDisplayingUnlockedAchievement ? "SUR_SOFTKEY_CHECK" : (string) null, "SUR_SOFTKEY_ARROW");
          cachievementsScreen.SetFlags(5);
          if (this.m_bDisplayingUnlockedAchievement)
            cachievementsScreen.SetDisplaySingleItem(this.m_singleItemIndex);
          for (int index = 0; index < 20; ++index)
            cachievementsScreen.SetItemSelectable(index, CGHStaticData.UnlockedAchievement(index));
          cnode = (CNode) cachievementsScreen;
          break;
      }
      pOut = new CNode();
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
            if (this.m_bDisplayingUnlockedAchievement)
            {
              this.SetInterrupt(1);
              break;
            }
            pState.ClearInterrupt();
            break;
          }
          pState.ClearInterrupt();
          this.SetInterrupt(2);
          break;
      }
    }

    public enum eAchievementsState
    {
      ACHIEVEMENTS_NONE,
      ACHIEVEMENTS_CHANGE,
    }
  }
}
