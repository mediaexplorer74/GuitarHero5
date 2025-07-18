// Decompiled with JetBrains decompiler
// Type: com.glu.game.CNetworkAccessScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CNetworkAccessScreen : CTextScreen
  {
    protected int m_timeMS;
    private int kFlipTimeMS = 500;

    public CNetworkAccessScreen() => this.m_timeMS = 0;

    public override uint Start()
    {
      int num = (int) base.Start();
      this.m_timeMS = 0;
      return 0;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      this.m_timeMS += timeElapsedMS;
      if (this.m_timeMS >= this.kFlipTimeMS)
        this.m_timeMS = 0;
      return true;
    }

    public override bool HandleRender()
    {
      this.RenderBegin();
      this.m_renderBeginAndEndIsInternallyHandled = false;
      bool flag = base.HandleRender();
      this.m_renderBeginAndEndIsInternallyHandled = true;
      this.RenderEnd();
      return flag;
    }
  }
}
