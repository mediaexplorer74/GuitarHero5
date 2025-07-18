// Decompiled with JetBrains decompiler
// Type: com.glu.game.CStateMachineNode
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public abstract class CStateMachineNode : CNode
  {
    public const int STATE_CHANGE_NONE = 0;
    public const int STATE_CHANGE_ADD = 1;
    public const int STATE_CHANGE_REPLACE = 2;
    public const int STATE_CHANGE_REPLACEALL = 3;
    public const int STATE_CHANGE_PREVIOUS = 4;
    public const int STATE_CHANGE_GOTO = 5;
    public new static uint ClassId = 1113725908;
    public static int kMaxStateStackSize = 10;
    public CStateMachineNode.tStateStackEntry[] m_stateStack = new CStateMachineNode.tStateStackEntry[CStateMachineNode.kMaxStateStackSize];
    public int m_stateStackSize;
    public int m_stateTransitionID;
    public int m_stateTransitionType;
    public CNode m_pState;
    public int m_stateID;

    protected abstract uint OnCreateState(out CNode pNode, int id);

    protected abstract void OnStateInterrupt(int id, CNode pNode);

    public CStateMachineNode()
    {
      this.m_classId = CStateMachineNode.ClassId;
      this.m_stateStackSize = 0;
      this.m_stateTransitionID = 0;
      this.m_stateTransitionType = 0;
      this.m_pState = (CNode) null;
      this.m_stateID = 0;
    }

    public override uint Start()
    {
      this.m_stateStackSize = 0;
      this.m_stateTransitionID = 0;
      this.m_stateTransitionType = 0;
      this.m_pState = (CNode) null;
      this.m_stateID = 0;
      return 0;
    }

    public override void Stop() => this.ClearStates();

    public override void Activate()
    {
      if (this.m_pState != null)
        this.m_pState.Activate();
      else
        this.SetActiveState();
    }

    public override void Deactivate()
    {
      if (this.m_pState == null)
        return;
      this.m_pState.Deactivate();
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 607208024:
        case 850690755:
        case 1364371259:
        case 1368267323:
        case 1411673571:
        case 1732285487:
        case 1967276899:
        case 2215179113:
          for (int index = 0; index < this.m_stateStackSize; ++index)
            this.m_stateStack[index].m_pState.HandleEvent(id, param1, param2);
          flag = true;
          break;
        default:
          if (this.m_pState != null)
          {
            flag = this.m_pState.HandleEvent(id, param1, param2);
            break;
          }
          break;
      }
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      int stateTransitionId = this.m_stateTransitionID;
      int stateTransitionType = this.m_stateTransitionType;
      this.m_stateTransitionID = 0;
      this.m_stateTransitionType = 0;
      switch (stateTransitionType)
      {
        case 1:
          int num1 = (int) this.AddNewState(stateTransitionId);
          break;
        case 2:
          this.PopState();
          int num2 = (int) this.AddNewState(stateTransitionId);
          break;
        case 3:
          this.ClearStates();
          int num3 = (int) this.AddNewState(stateTransitionId);
          break;
        case 4:
          this.PopState();
          break;
        case 5:
          int state = (int) this.GoToState(stateTransitionId);
          break;
      }
      if (this.m_pState != null)
      {
        this.m_pState.HandleUpdate(timeElapsedMS);
        if (this.m_pState.GetInterrupt() != 0)
          this.OnStateInterrupt(this.m_stateID, this.m_pState);
      }
      return false;
    }

    public override bool HandleRender()
    {
      bool flag = true;
      for (int index = 0; index < this.m_stateStackSize; ++index)
      {
        if (flag)
          this.m_stateStack[index].m_pState.HandleEvent(607208024U, 0U, (object) 0U);
        flag |= this.m_stateStack[index].m_pState.HandleRender();
      }
      return flag;
    }

    public void ChangeState(int id, int type)
    {
      this.m_stateTransitionID = id;
      this.m_stateTransitionType = type;
    }

    public uint AddNewState(int id)
    {
      CNode pNode = (CNode) null;
      uint num = this.OnCreateState(out pNode, id);
      CNode cnode;
      if (num == 0U)
      {
        num = this.PushState(id, pNode);
        if (num == 0U)
          cnode = (CNode) null;
      }
      cnode = (CNode) null;
      return num;
    }

    public uint GoToState(int id)
    {
      uint state = 0;
      while (this.m_stateStackSize > 0 && this.m_stateStack[this.m_stateStackSize - 1].m_id != id)
        this.PopState();
      if (this.m_stateStackSize == 0)
        state = this.AddNewState(id);
      return state;
    }

    public CNode FindState(int id)
    {
      CNode state = (CNode) null;
      for (int index = this.m_stateStackSize - 1; index >= 0; --index)
      {
        if (this.m_stateStack[index].m_id == id)
          state = this.m_stateStack[index].m_pState;
      }
      return state;
    }

    public uint PushState(int id, CNode pState)
    {
      uint num = 1;
      if (pState != null && this.m_stateStackSize < CStateMachineNode.kMaxStateStackSize)
      {
        this.m_stateStack[this.m_stateStackSize].m_id = id;
        this.m_stateStack[this.m_stateStackSize].m_pState = pState;
        ++this.m_stateStackSize;
        num = pState.Start();
      }
      this.SetActiveState();
      return num;
    }

    public void PopState()
    {
      if (this.m_pState != null)
      {
        this.m_pState.Deactivate();
        this.m_pState.Stop();
      }
      if (this.m_stateStackSize > 0)
      {
        --this.m_stateStackSize;
        this.m_stateStack[this.m_stateStackSize].m_id = 0;
        this.m_stateStack[this.m_stateStackSize].m_pState = (CNode) null;
      }
      this.m_pState = (CNode) null;
      this.m_stateID = 0;
      this.SetActiveState();
    }

    public void SetActiveState()
    {
      if (this.m_pState != null)
        this.m_pState.Deactivate();
      if (this.m_stateStackSize > 0)
      {
        this.m_pState = this.m_stateStack[this.m_stateStackSize - 1].m_pState;
        this.m_stateID = this.m_stateStack[this.m_stateStackSize - 1].m_id;
        this.m_pState.ClearInterrupt();
        this.m_pState.Activate();
      }
      else
      {
        this.m_pState = (CNode) null;
        this.m_stateID = 0;
      }
      for (int index = 0; index < this.m_stateStackSize; ++index)
        this.m_stateStack[index].m_pState.HandleEvent(607208024U, 0U, (object) 0U);
    }

    public void ClearStates()
    {
      this.m_pState = (CNode) null;
      this.m_stateID = 0;
      for (int index = 0; index < this.m_stateStackSize; ++index)
        this.m_stateStack[index].m_pState.Deactivate();
      for (int index = 0; index < this.m_stateStackSize; ++index)
      {
        this.m_stateStack[index].m_pState.Stop();
        this.m_stateStack[index].m_id = 0;
        this.m_stateStack[index].m_pState = (CNode) null;
      }
      this.m_stateStackSize = 0;
    }

    public enum eStateTransitionType
    {
      STATE_CHANGE_NONE,
      STATE_CHANGE_ADD,
      STATE_CHANGE_REPLACE,
      STATE_CHANGE_REPLACEALL,
      STATE_CHANGE_PREVIOUS,
      STATE_CHANGE_GOTO,
    }

    public struct tStateStackEntry
    {
      public int m_id;
      public CNode m_pState;
    }
  }
}
