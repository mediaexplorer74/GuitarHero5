// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CGraphics
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace com.glu.shared
{
  public class CGraphics : ICGraphics
  {
    protected GraphicsDevice m_graphics_device_xna;
    protected CRenderSurface_XNA_Window m_windowSurface;
    protected ICRenderSurface m_targetSurface;
    private CGraphics.ColorPkg m_clearColor;
    private Microsoft.Xna.Framework.Color m_clearColor_xna;
    private int m_clearDepth;
    private int m_clearStencil;

    public override bool Initialize()
    {
      this.m_graphics_device_xna = CApplet.GetInstance().GraphicsDevice;
      this.m_windowSurface = new CRenderSurface_XNA_Window();
      this.m_targetSurface = (ICRenderSurface) this.m_windowSurface;
      return true;
    }

    public override ICRenderSurface GetTargetSurface() => this.m_targetSurface;

    public override void RenderBegin()
    {
    }

    public override void RenderEnd()
    {
    }

    public override void ClearBuffers(ICGraphics.BufferFlags bufferFlags)
    {
      this.m_clearColor_xna.R = this.m_clearColor.m_colorfx.GetRed();
      this.m_clearColor_xna.G = this.m_clearColor.m_colorfx.GetGreen();
      this.m_clearColor_xna.B = this.m_clearColor.m_colorfx.GetBlue();
      this.m_clearColor_xna.A = this.m_clearColor.m_colorfx.GetAlpha();
      this.m_graphics_device_xna.Clear(this.m_clearColor_xna);
    }

    public override void SwapBuffers()
    {
    }

    public override void SetDefaultActiveAbstractionLayer(ICGraphics.Abstraction abstraction)
    {
    }

    public override ICGraphics.Abstraction GetDefaultActiveAbstractionLayer()
    {
      return ICGraphics.Abstraction.Hardware;
    }

    public override void SetDefaultColorFormat(Color.Format format)
    {
    }

    public override Color.Format GetDefaultColorFormat() => Color.Format.B5G6R5;

    public override void SetClearColor(int a, int r, int g, int b)
    {
      this.m_clearColor.m_colorfx.SetFixed(a, r, g, b);
      this.m_clearColor.m_color8 = Color.A8R8G8B8_t.Make(this.m_clearColor.m_colorfx.GetAlpha(), this.m_clearColor.m_colorfx.GetRed(), this.m_clearColor.m_colorfx.GetGreen(), this.m_clearColor.m_colorfx.GetBlue());
    }

    public override void SetClearColor(Color.ARGB_fixed color)
    {
      this.m_clearColor.m_colorfx.Set(color);
      this.m_clearColor.m_color8 = Color.A8R8G8B8_t.Make(this.m_clearColor.m_colorfx.GetAlpha(), this.m_clearColor.m_colorfx.GetRed(), this.m_clearColor.m_colorfx.GetGreen(), this.m_clearColor.m_colorfx.GetBlue());
    }

    public override void SetClearColor(uint color)
    {
      this.m_clearColor.m_color8 = color;
      this.m_clearColor.m_colorfx.Make(Color.Format.A8R8G8B8, (object) color);
    }

    public override void SetClearDepth(int depth) => this.m_clearDepth = depth;

    public override void SetClearStencil(int stencil) => this.m_clearStencil = stencil;

    public override ICGraphics.Description GetDescription(ICGraphics.Abstraction abstraction)
    {
      return (ICGraphics.Description) null;
    }

    public override string GetVersion(ICGraphics.Abstraction abstraction) => "XNA 4.0";

    public GraphicsDevice GetGraphicsDeviceXNA() => this.m_graphics_device_xna;

    private struct ColorPkg
    {
      public uint m_color8;
      public Color.ARGB_fixed m_colorfx;
    }
  }
}
