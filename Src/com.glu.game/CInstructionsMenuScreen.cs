// Decompiled with JetBrains decompiler
// Type: com.glu.game.CInstructionsMenuScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CInstructionsMenuScreen : CMenuScreen
  {
    public string[] m_instructionsMenuItems = new string[10];

    public CInstructionsMenuScreen() => this.m_eventId = 0U;

    public override uint Start() => base.Start();

    public override void Stop() => base.Stop();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 129075783:
          int num1 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuSelectSFX);
          break;
        case 555763780:
          int num2 = (int) ICMediaPlayer.GetInstance().Play(CGHStaticData.m_pMenuBackSFX);
          this.SetInterrupt(2);
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      if (id == 129075783U)
        CGHStaticData.m_instructionsMenuSelection = this.m_instructionsMenuItems[this.GetSelection()];
      return flag;
    }

    public override void CreateResources() => base.CreateResources();

    public override void ReleaseResources() => base.ReleaseResources();

    public override void Build()
    {
      CFontMgr.GetInstance();
      int num1 = 0;
      string[] instructionsMenuItems1 = this.m_instructionsMenuItems;
      int index1 = num1;
      int num2 = index1 + 1;
      instructionsMenuItems1[index1] = "IDS_MENU_INSTRUCTIONS_CAREER_TITLE";
      if (!CGHStaticData.m_bAchievementsDisabled)
        this.m_instructionsMenuItems[num2++] = "IDS_MENU_INSTRUCTIONS_ACHIEVEMENTS_TITLE";
      string[] instructionsMenuItems2 = this.m_instructionsMenuItems;
      int index2 = num2;
      int num3 = index2 + 1;
      instructionsMenuItems2[index2] = "IDS_MENU_INSTRUCTIONS_APPEARANCE_TITLE";
      string[] instructionsMenuItems3 = this.m_instructionsMenuItems;
      int index3 = num3;
      int num4 = index3 + 1;
      instructionsMenuItems3[index3] = "IDS_MENU_INSTRUCTIONS_UNLOCKS_TITLE";
      string[] instructionsMenuItems4 = this.m_instructionsMenuItems;
      int index4 = num4;
      int num5 = index4 + 1;
      instructionsMenuItems4[index4] = "IDS_MENU_INSTRUCTIONS_PRACTICE_TITLE";
      string[] instructionsMenuItems5 = this.m_instructionsMenuItems;
      int index5 = num5;
      int items = index5 + 1;
      instructionsMenuItems5[index5] = "IDS_MENU_INSTRUCTIONS_LEADERBOARDS_TITLE";
      this.SetMenu(this.m_instructionsMenuItems, items);
      this.m_page.SetWrap(true);
      base.Build();
    }

    public override void Layout() => base.Layout();
  }
}
