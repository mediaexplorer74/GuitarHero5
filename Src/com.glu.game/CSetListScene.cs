// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSetListScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CSetListScene : CStateMachineNode
  {
    protected bool m_inset;

    public void SetInset(bool inset) => this.m_inset = inset;

    public CSetListScene() => this.m_inset = false;

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
          CSetListScreen csetListScreen = new CSetListScreen();
          csetListScreen.SetTitle("IDS_SET_LIST");
          if (this.m_inset)
            csetListScreen.SetInset(30, 70);
          csetListScreen.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          csetListScreen.SetMovie("GLU_MOVIE_SET_LIST");
          csetListScreen.SetFlags(1);
          cnode = (CNode) csetListScreen;
          break;
        case 2:
          CTextScreen ctextScreen = new CTextScreen();
          ctextScreen.SetTitle("IDS_SAVE_ERROR_TITLE");
          if (this.m_inset)
            ctextScreen.SetInset(30, 70);
          ctextScreen.SetSoftkeys((string) null, "SUR_SOFTKEY_ARROW");
          ctextScreen.SetInset(30, 0);
          ctextScreen.SetText("IDS_SAVE_ERROR_TEXT");
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
            CSongListMgr.SetSelected(((CSetListScreen) pState).GetSelectedSongID());
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
      SET_LIST_NONE,
      SET_LIST_CHANGE,
      SET_LIST_LOAD_ERROR,
    }
  }
}
