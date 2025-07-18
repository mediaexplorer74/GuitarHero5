// Decompiled with JetBrains decompiler
// Type: com.glu.game.CNode
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CNode : CClass
  {
    public const int NODE_INTERRUPT_NONE = 0;
    public const int NODE_INTERRUPT_ADVANCE = 1;
    public const int NODE_INTERRUPT_BACK = 2;
    public static uint ClassId = 40117797;
    protected int m_interrupt;

    public CNode()
    {
      this.m_classId = CNode.ClassId;
      this.m_interrupt = 0;
    }

    public virtual uint Start() => 0;

    public virtual void Stop()
    {
    }

    public virtual void Activate()
    {
    }

    public virtual void Deactivate()
    {
    }

    public virtual bool HandleEvent(uint id, uint param1, object param2) => false;

    public virtual bool HandleUpdate(int timeElapsedMS) => false;

    public virtual bool HandleRender() => false;

    public virtual void SetInterrupt(int interrupt)
    {
      if (this.m_interrupt != 0)
        return;
      this.m_interrupt = interrupt;
    }

    public virtual int GetInterrupt() => this.m_interrupt;

    public virtual void ClearInterrupt() => this.m_interrupt = 0;

    public virtual void RenderBegin()
    {
      ICGraphics instance1 = ICGraphics.GetInstance();
      ICGraphics2d instance2 = ICGraphics2d.GetInstance();
      instance1.RenderBegin();
      instance2.RenderBegin();
      instance2.Enable(ICGraphics2d.State.ConfigStateBasedOnSrcFormat);
      if (instance2.GetActiveAbstractionLayer() == ICGraphics.Abstraction.Software)
        instance2.Enable(ICGraphics2d.State.ColorKeyTest);
      this.SyncalSystemsWithRenderBegin();
    }

    public virtual void RenderEnd()
    {
      ICGraphics instance = ICGraphics.GetInstance();
      ICGraphics2d.GetInstance().RenderEnd();
      instance.RenderEnd();
    }

    public virtual void SyncalSystemsWithRenderBegin()
    {
    }
  }
}
