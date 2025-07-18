// Decompiled with JetBrains decompiler
// Type: com.glu.game.CHelpScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CHelpScreen : CMenuScreen
  {
    protected string[] m_helpMenuItems = new string[Consts.kMaxMenuItems];

    public CHelpScreen() => this.m_eventId = 0U;

    public override uint Start() => base.Start();

    public override void Stop() => base.Stop();

    private bool HandleEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.HandleMovieOut(id, param1, (object) param2))
        return true;
      switch (id)
      {
        case 555763780:
          this.SetInterrupt(2);
          flag = true;
          break;
      }
      if (!flag)
        flag = this.HandleEvent(id, param1, (object) param2);
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
      string[] helpMenuItems1 = this.m_helpMenuItems;
      int index1 = num1;
      int num2 = index1 + 1;
      helpMenuItems1[index1] = "IDS_MENU_INSTRUCTIONS";
      string[] helpMenuItems2 = this.m_helpMenuItems;
      int index2 = num2;
      int items = index2 + 1;
      helpMenuItems2[index2] = "IDS_MENU_ABOUT";
      this.SetMenu(this.m_helpMenuItems, items);
      this.m_page.SetWrap(true);
      base.Build();
    }

    public override void Layout() => base.Layout();
  }
}
