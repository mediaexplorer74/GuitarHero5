// Decompiled with JetBrains decompiler
// Type: com.glu.game.Graphics
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class Graphics
  {
    private CRectangle rect;
    private CRectangle clipRect;
    private short originX;
    private short originY;

    public void SetOffset(short x, short y)
    {
      this.originX = x;
      this.originY = y;
    }

    public void SetClip(CRectangle rect) => this.clipRect.CopyFrom(rect);

    public void DrawRegion(
      ICRenderSurface pICRenderSurface,
      Graphics.RenderInfo renderInfo,
      int x_dest,
      int y_dest)
    {
      this.DrawRegion(pICRenderSurface, renderInfo, x_dest, y_dest, 1000);
    }

    public void DrawRegion(
      ICRenderSurface pICRenderSurface,
      Graphics.RenderInfo renderInfo,
      int x_dest,
      int y_dest,
      int quantum)
    {
      x_dest += (int) this.originX;
      y_dest += (int) this.originY;
      pICRenderSurface.GetWidthAndHeight(out uint _, out uint _);
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      instance.PushTransform();
      instance.Translate(CMathFixed.Int32ToFixed(x_dest), CMathFixed.Int32ToFixed(y_dest));
      if (quantum != 1000)
      {
        int num = CMathFixed.Div(CMathFixed.Int32ToFixed(quantum), CMathFixed.Int32ToFixed(1000));
        instance.Scale(num, num);
      }
      instance.Draw(pICRenderSurface, renderInfo.m_flip, new CRectangle?());
      instance.PopTransform();
    }

    public void FillRect(int x, int y, uint width, uint height, int colorA8R8G8B8)
    {
      this.rect.Set((int) (short) (x + (int) this.originX), (int) (short) (y + (int) this.originY), (int) (short) width, (int) (short) height);
      CDrawUtil.FillRect(this.rect, (uint) (colorA8R8G8B8 | -16777216));
    }

    public int GetOriginX() => (int) this.originX;

    public int GetOriginY() => (int) this.originY;

    public class RenderInfo
    {
      public ICGraphics2d.Flip m_flip;

      public RenderInfo() => this.m_flip = ICGraphics2d.Flip.NoFlip;
    }
  }
}
