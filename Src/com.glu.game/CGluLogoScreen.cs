// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGluLogoScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGluLogoScreen : CSoftkeyScreen
  {
    protected CMedia m_pLogoTheme;
    protected ICRenderSurface m_pLogoImage;
    protected int m_timeElapsedMS;
    protected int m_eventId;
    private static int TIME_WAIT = 250;
    private static int TIME_JUMP = 500;
    private static int TIME_PER_LETTER = 250;
    private static int TIME_HOLD = 500;
    private static int TIME_FADE = 500;
    private static int TIME_LOGO_FULL = 3 * CGluLogoScreen.TIME_JUMP + 3 * CGluLogoScreen.TIME_PER_LETTER + CGluLogoScreen.TIME_HOLD;
    private static int TIME_LOGO_DONE = CGluLogoScreen.TIME_LOGO_FULL + CGluLogoScreen.TIME_FADE;

    public CGluLogoScreen()
    {
      this.m_pLogoImage = (ICRenderSurface) null;
      this.m_pLogoTheme = (CMedia) null;
      this.m_eventId = 0;
      this.m_timeElapsedMS = -1;
    }

    public override uint Start()
    {
      uint num = base.Start();
      this.m_eventId = (int) ICMediaPlayer.GetInstance().Play(this.m_pLogoTheme, (byte) 0, (byte) 0);
      return num;
    }

    public override void Stop()
    {
      base.Stop();
      if (this.m_eventId <= 0)
        return;
      ICMediaPlayer.GetInstance().Stop((uint) this.m_eventId);
    }

    public override void Activate() => base.Activate();

    public override void Deactivate() => base.Deactivate();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 129075783:
        case 544526345:
        case 902053462:
        case 2300082508:
          if (this.m_timeElapsedMS < CGluLogoScreen.TIME_LOGO_FULL)
            this.m_timeElapsedMS = CGluLogoScreen.TIME_LOGO_FULL;
          flag = true;
          break;
        case 555763780:
          this.SetInterrupt(2);
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      if (this.m_timeElapsedMS < 0)
      {
        this.m_timeElapsedMS = 0;
      }
      else
      {
        this.m_timeElapsedMS += timeElapsedMS;
        if (this.m_timeElapsedMS >= CGluLogoScreen.TIME_LOGO_DONE)
        {
          ICMediaPlayer.GetInstance().Stop((uint) this.m_eventId);
          this.SetInterrupt(1);
        }
      }
      return true;
    }

    public override bool HandleRender()
    {
      ICGraphics instance1 = ICGraphics.GetInstance();
      ICGraphics2d instance2 = ICGraphics2d.GetInstance();
      instance1.ClearBuffers(ICGraphics.BufferFlags.SurfaceBuffers);
      this.RenderBegin();
      uint width1;
      uint height1;
      instance1.GetTargetSurface().GetWidthAndHeight(out width1, out height1);
      uint width2;
      uint height2;
      this.m_pLogoImage.GetWidthAndHeight(out width2, out height2);
      int num1 = this.m_timeElapsedMS - CGluLogoScreen.TIME_WAIT;
      uint num2 = height1 - height2 >> 1;
      uint num3 = width1 + width2 >> 1;
      uint v1 = (uint) ((int) width1 - (int) num3 - ((int) width2 & 1));
      uint v2 = height1 - height2 >> 1;
      uint dx = width2;
      uint dy = height2;
      if (num1 > 3 * CGluLogoScreen.TIME_JUMP)
        dx = num1 <= 3 * CGluLogoScreen.TIME_JUMP + 2 * CGluLogoScreen.TIME_PER_LETTER ? (num1 <= 3 * CGluLogoScreen.TIME_JUMP + CGluLogoScreen.TIME_PER_LETTER ? 97U : 129U) : width2;
      else if (num1 > 0)
      {
        dx = 97U;
        int num4 = num1 % CGluLogoScreen.TIME_JUMP - (CGluLogoScreen.TIME_JUMP >> 1);
        v2 -= (uint) ((ulong) num2 - (ulong) num2 * (ulong) (num4 * num4) / (ulong) (CGluLogoScreen.TIME_JUMP * CGluLogoScreen.TIME_JUMP >> 2));
        v1 += (uint) ((ulong) num3 * (ulong) (3 * CGluLogoScreen.TIME_JUMP - num1) / (ulong) (3 * CGluLogoScreen.TIME_JUMP));
      }
      if (num1 > 0)
      {
        CRectangle crectangle = new CRectangle(0, 0, (int) (short) dx, (int) (short) dy);
        int a = num1 >= CGluLogoScreen.TIME_LOGO_FULL ? (num1 < CGluLogoScreen.TIME_LOGO_DONE ? 65536 - CMathFixed.Div(CMathFixed.Int32ToFixed(num1 - CGluLogoScreen.TIME_LOGO_FULL), CMathFixed.Int32ToFixed(CGluLogoScreen.TIME_FADE)) : CMathFixed.FloatToFixed(0.0f)) : CMathFixed.FloatToFixed(1f);
        instance2.PushState(ICGraphics2d.State.Blend);
        instance2.PushState(ICGraphics2d.State.Color);
        instance2.Enable(ICGraphics2d.State.Blend);
        instance2.SetBlendArg(ICGraphics2d.BlendArg.ConstAlphaInvConstAlphaAdd);
        instance2.SetColor(a, 0, 0, 0);
        instance2.PushTransform();
        instance2.LoadIdentity();
        instance2.Translate(CMathFixed.Int32ToFixed((int) v1), CMathFixed.Int32ToFixed((int) v2));
        instance2.Draw(this.m_pLogoImage, ICGraphics2d.Flip.NoFlip, new CRectangle?(crectangle));
        instance2.PopTransform();
        instance2.PopState(ICGraphics2d.State.Color);
        instance2.PopState(ICGraphics2d.State.Blend);
      }
      this.m_base.SetDirty();
      this.m_renderBeginAndEndIsInternallyHandled = false;
      base.HandleRender();
      this.m_renderBeginAndEndIsInternallyHandled = true;
      this.RenderEnd();
      return true;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource("SUR_GLU", out resource1);
      if (resource1 != null)
        this.m_pLogoImage = (ICRenderSurface) resource1.GetData();
      int resource3 = (int) resourceManager.CreateResource("IDM_GLUTHEME", out resource1);
      if (resource1 == null)
        return;
      this.m_pLogoTheme = (CMedia) resource1.GetData();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_GLU");
      resourceManager.ReleaseResource("IDM_GLUTHEME");
    }

    public override void Build()
    {
      base.Build();
      this.m_base.SetTransparent(true);
    }
  }
}
