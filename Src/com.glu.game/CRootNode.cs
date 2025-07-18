// Decompiled with JetBrains decompiler
// Type: com.glu.game.CRootNode
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CRootNode : CStateMachineNode
  {
    public const int ROOT_NONE = 0;
    public const int ROOT_INTRO = 1;
    public const int ROOT_SHELL = 2;
    public const int ROOT_PLAY = 3;
    public const int ROOT_EXIT = 4;
    public CResBank m_preloadBank = new CResBank();

    public override uint Start()
    {
      uint num = base.Start();
      CGameData.SetPreloadBank(this.m_preloadBank);
      this.ChangeState(1, 1);
      return num;
    }

    public override void Stop() => base.Stop();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      return base.HandleEvent(id, param1, param2);
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          cnode = (CNode) new CIntroScene();
          break;
        case 2:
          cnode = (CNode) new CShellScene();
          break;
        case 3:
          cnode = (CNode) new CGameScene();
          break;
        case 4:
          ICCore.GetInstance().ExitApp();
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
            this.ChangeState(2, 2);
            break;
          }
          this.ChangeState(4, 2);
          break;
        case 2:
          switch (pState.GetInterrupt())
          {
            case 1:
              this.ChangeState(3, 2);
              return;
            case 2:
              this.ChangeState(3, 2);
              return;
            case 3:
              ICCore.GetInstance().ExitApp();
              return;
            default:
              return;
          }
        case 3:
          this.ChangeState(2, 2);
          break;
      }
    }
  }
}
