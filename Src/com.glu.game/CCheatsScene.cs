// Decompiled with JetBrains decompiler
// Type: com.glu.game.CCheatsScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CCheatsScene : CStateMachineNode
  {
    public const int CHEATS_ITEM_COUNT = 7;
    protected bool m_inset;
    protected int[] m_cheatItems = new int[7];
    private int m_cheatMenuSelection;
    public int CHEATS_STANDARD_DPLPGM_CMP_NUM = 512;
    public int CHEATS_STANDARD_DPLPGM_VTX_SIZE = 204800;

    public void SetInset(bool inset) => this.m_inset = inset;

    public void CCHeatsScene()
    {
      this.m_inset = false;
      this.m_cheatMenuSelection = 0;
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
      pOut = cnode;
      return state;
    }

    protected override void OnStateInterrupt(int id, CNode pState)
    {
    }

    public enum eOptionsState
    {
      CHEATS_NONE,
      CHEATS_CHANGE,
      CHEATS_SAVE_ERROR,
    }
  }
}
