// Decompiled with JetBrains decompiler
// Type: com.glu.game.CInstructionsScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CInstructionsScreen : CTextScreen
  {
    public CInstructionsScreen()
    {
      switch (CGHStaticData.m_instructionsMenuSelection)
      {
        case "IDS_MENU_INSTRUCTIONS_CAREER_TITLE":
          this.SetTextDirectly(CGameApp.GetInstance().GetOverrideText("IDS_MENU_INSTRUCTIONS_CAREER_TEXT"));
          this.SetTitle("IDS_MENU_INSTRUCTIONS_CAREER_TITLE");
          break;
        case "IDS_MENU_INSTRUCTIONS_ACHIEVEMENTS_TITLE":
          this.SetTitle("IDS_MENU_INSTRUCTIONS_ACHIEVEMENTS_TITLE");
          this.SetText("IDS_MENU_INSTRUCTIONS_ACHIEVEMENTS_TEXT");
          break;
        case "IDS_MENU_INSTRUCTIONS_APPEARANCE_TITLE":
          this.SetTitle("IDS_MENU_INSTRUCTIONS_APPEARANCE_TITLE");
          this.SetText("IDS_MENU_INSTRUCTIONS_APPEARANCE_TEXT");
          break;
        case "IDS_MENU_INSTRUCTIONS_UNLOCKS_TITLE":
          this.SetTitle("IDS_MENU_INSTRUCTIONS_UNLOCKS_TITLE");
          this.SetText("IDS_MENU_INSTRUCTIONS_UNLOCKS_TEXT");
          break;
        case "IDS_MENU_INSTRUCTIONS_PRACTICE_TITLE":
          this.SetTitle("IDS_MENU_INSTRUCTIONS_PRACTICE_TITLE");
          this.SetText("IDS_MENU_INSTRUCTIONS_PRACTICE_TEXT");
          break;
        case "IDS_MENU_INSTRUCTIONS_LEADERBOARDS_TITLE":
          this.SetTitle("IDS_MENU_INSTRUCTIONS_LEADERBOARDS_TITLE");
          this.SetText("IDS_MENU_INSTRUCTIONS_LEADERBOARDS_TEXT");
          break;
      }
      this.SetInset(30, 0);
    }

    public override uint Start() => base.Start();

    public override void Stop() => base.Stop();

    public override void Build()
    {
      CFontMgr.GetInstance();
      this.m_page.SetWrap(false);
      base.Build();
    }

    public override void Layout() => base.Layout();
  }
}
