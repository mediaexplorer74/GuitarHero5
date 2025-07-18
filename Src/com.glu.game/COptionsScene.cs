// Decompiled with JetBrains decompiler
// Type: com.glu.game.COptionsScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class COptionsScene : CStateMachineNode
  {
    protected bool m_inset;

    public void SetInset(bool inset) => this.m_inset = inset;

    public COptionsScene() => this.m_inset = false;

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
          COptionsScreen coptionsScreen = new COptionsScreen();
          coptionsScreen.SetTitle("IDS_OPTIONS_TITLE");
          coptionsScreen.SetFlags(5);
          if (this.m_inset)
            coptionsScreen.SetInset(30, 70);
          coptionsScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cnode = (CNode) coptionsScreen;
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
          this.SetInterrupt(1);
          break;
      }
    }

    public enum eOptionsState
    {
      OPTIONS_NONE,
      OPTIONS_CHANGE,
      OPTIONS_SAVE_ERROR,
    }
  }
}
