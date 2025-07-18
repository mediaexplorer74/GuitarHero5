// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSettingsScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CSettingsScreen : CMenuScreen
  {
    protected string[] m_settingsMenuItems = new string[Consts.kMaxMenuItems];

    public string GetSelectionMenuStringID() => this.m_settingsMenuItems[this.GetSelection()];

    public CSettingsScreen() => this.m_eventId = 0U;

    public override uint Start()
    {
      uint num = base.Start();
      CGameData.SetOptionsActive(true);
      return num;
    }

    public override void Stop() => base.Stop();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (this.HandleMovieOut(id, param1, param2))
        return true;
      switch (id)
      {
        case 555763780:
          this.SetInterrupt(2);
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override void CreateResources() => base.CreateResources();

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_ARROW_LEFT");
      resourceManager.ReleaseResource("SUR_ARROW_RIGHT");
    }

    public override void Build()
    {
      CFontMgr.GetInstance();
      int num1 = 0;
      int num2;
      if (CGameApp.GetInstance().IsVolumeControlDisabled())
      {
        string[] settingsMenuItems = this.m_settingsMenuItems;
        int index = num1;
        num2 = index + 1;
        settingsMenuItems[index] = "IDS_MENU_SOUND";
      }
      else
      {
        string[] settingsMenuItems = this.m_settingsMenuItems;
        int index = num1;
        num2 = index + 1;
        settingsMenuItems[index] = "IDS_MENU_VOLUME";
      }
      if (ICCore.GetInstance().CanVibrate() && !CGameApp.GetInstance().IsVibrationDisabled())
        this.m_settingsMenuItems[num2++] = "IDS_MENU_VIBRATION";
      string[] settingsMenuItems1 = this.m_settingsMenuItems;
      int index1 = num2;
      int num3 = index1 + 1;
      settingsMenuItems1[index1] = "IDS_MENU_RESET_DATA";
      string[] settingsMenuItems2 = this.m_settingsMenuItems;
      int index2 = num3;
      int num4 = index2 + 1;
      settingsMenuItems2[index2] = "IDS_MENU_INSTRUCTIONS";
      string[] settingsMenuItems3 = this.m_settingsMenuItems;
      int index3 = num4;
      int items = index3 + 1;
      settingsMenuItems3[index3] = "IDS_MENU_ABOUT";
      this.SetMenu(this.m_settingsMenuItems, items);
      this.m_page.SetWrap(true);
      base.Build();
    }

    public override void Layout() => base.Layout();
  }
}
