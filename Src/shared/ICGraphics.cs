// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICGraphics
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class ICGraphics : CClass
  {
    public const uint ClassId = 73851284;
    protected static CGraphics m_instance;
    protected ICGraphics.Abstraction m_abstraction;

    public static ICGraphics Instance => (ICGraphics) ICGraphics.m_instance;

    public static ICGraphics CreateInstance()
    {
      ICGraphics.m_instance = new CGraphics();
      return (ICGraphics) ICGraphics.m_instance;
    }

    public static ICGraphics GetInstance()
    {
      return ICGraphics.m_instance == null ? ICGraphics.CreateInstance() : (ICGraphics) ICGraphics.m_instance;
    }

    public abstract bool Initialize();

    public abstract ICRenderSurface GetTargetSurface();

    public abstract void RenderBegin();

    public abstract void RenderEnd();

    public abstract void ClearBuffers(ICGraphics.BufferFlags bufferFlags);

    public abstract void SwapBuffers();

    public abstract void SetDefaultActiveAbstractionLayer(ICGraphics.Abstraction abstraction);

    public abstract ICGraphics.Abstraction GetDefaultActiveAbstractionLayer();

    public abstract void SetDefaultColorFormat(Color.Format format);

    public abstract Color.Format GetDefaultColorFormat();

    public abstract void SetClearColor(int a, int r, int g, int b);

    public abstract void SetClearColor(Color.ARGB_fixed color);

    public abstract void SetClearColor(uint color);

    public abstract void SetClearDepth(int depth);

    public abstract void SetClearStencil(int stencil);

    public abstract ICGraphics.Description GetDescription(ICGraphics.Abstraction abstraction);

    public abstract string GetVersion(ICGraphics.Abstraction abstraction);

    protected ICGraphics()
      : base(73851284U)
    {
    }

    public enum Abstraction
    {
      Undefined,
      Hardware,
      Software,
    }

    public enum BufferFlags
    {
      ColorBuffer = 65536, // 0x00010000
      DepthBuffer = 131072, // 0x00020000
      StencilBuffer = 262144, // 0x00040000
      SurfaceBuffers = 458752, // 0x00070000
    }

    public class Description
    {
    }
  }
}
