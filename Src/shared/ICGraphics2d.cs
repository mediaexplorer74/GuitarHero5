// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICGraphics2d
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class ICGraphics2d : CSingleton
  {
    public const uint ClassId = 1726453568;
    protected new static CSingleton m_instance;

    public static ICGraphics2d GetInstance()
    {
      if (ICGraphics2d.m_instance == null)
        ICGraphics2d.m_instance = (CSingleton) new CGraphics2d();
      return (ICGraphics2d) (ICGraphics2d.m_instance as CGraphics2d);
    }

    public abstract bool Initialize();

    public abstract void SetActiveAbstractionLayer(ICGraphics.Abstraction abstraction);

    public abstract ICGraphics.Abstraction GetActiveAbstractionLayer();

    public abstract void RenderBegin();

    public abstract void RenderEnd();

    public abstract void SetColor(int a, int r, int g, int b);

    public abstract void SetColor(Color.ARGB_fixed color);

    public abstract void SetColor(uint color);

    public abstract void Enable(ICGraphics2d.State state);

    public abstract void Disable(ICGraphics2d.State State);

    public abstract void PushState(ICGraphics2d.State state);

    public abstract void PopState(ICGraphics2d.State state);

    public abstract void SetBlendArg(ICGraphics2d.BlendArg blendArg);

    public abstract void LoadIdentity();

    public abstract void LoadTransform(CMatrix2d m, CVector2d t);

    public abstract void MultiplyTransform(CMatrix2d m, CVector2d t);

    public abstract void Translate(int x, int y);

    public abstract void Rotate(int angle);

    public abstract void Scale(int x, int y);

    public abstract void PushTransform();

    public abstract void PopTransform();

    public abstract void GetTransform(out CMatrix2d m, out CVector2d t);

    public abstract void SetVertexPositionCoords(
      int numOfCompsPerVertex,
      uint type,
      int stride,
      byte[] pos);

    public abstract void SetClip(uint x, uint y, uint dx, uint dy);

    public abstract void SetClip(CRectangle rect);

    public abstract CRectangle GetClip();

    public abstract void Draw(ICRenderSurface surface, ICGraphics2d.Flip flip, CRectangle? srcClip);

    public abstract void Draw(ICRenderSurface surface, ICGraphics2d.Flip flip);

    public abstract void Draw(ICRenderSurface surface);

    public abstract void Draw(ICGraphics2d.Primitive prim, int startIdx, int count);

    public enum Origin
    {
      LowerLeft,
      UpperLeft,
    }

    public enum ShaderMode
    {
      IntrinsicShaders,
      UserDefinedShader,
    }

    public enum State
    {
      AlphaTest,
      Blend,
      Color,
      ColorKeyTest,
      ConfigStateBasedOnSrcFormat,
    }

    public enum BlendArg
    {
      ConstAlphaInvConstAlphaAdd,
      ConstAlphaOneAdd,
      SrcAlphaInvSrcAlphaAdd,
      SrcAlphaOneAdd,
      OneOneAdd,
    }

    public enum Primitive
    {
      Unknown,
      Lines,
      Polygon,
      Rectangles,
    }

    public enum Flip
    {
      NoFlip,
      HorizontalFlip,
      VerticalFlip,
      HorizontalAndVerticalFlip,
    }
  }
}
