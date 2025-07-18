// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CDrawUtil
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CDrawUtil
  {
    private static byte[] m_vbuf = new byte[8];

    public static void FillRect(int x0, int y0, int x1, int y1, uint color)
    {
      CDrawUtil.Fill(x0, y0, x1, y1, color, ICGraphics2d.Primitive.Rectangles);
    }

    public static void FillRect(CRectangle rect, uint color)
    {
      CDrawUtil.Fill(rect.m_x, rect.m_y, rect.m_x + rect.m_dx, rect.m_y + rect.m_dy, color, ICGraphics2d.Primitive.Rectangles);
    }

    public static void FillLine(int x0, int y0, int x1, int y1, uint color)
    {
      CDrawUtil.Fill(x0, y0, x1, y1, color, ICGraphics2d.Primitive.Lines);
    }

    public static void DrawFrame(
      CRectangle rect,
      uint frameColor,
      int width,
      CDrawUtil.eDrawFrameFlags flags)
    {
      CRectangle rect1;
      if ((flags & CDrawUtil.eDrawFrameFlags.DRAWFRAME_NO_TOP) == CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL)
      {
        rect1.m_x = rect.m_x;
        rect1.m_dx = rect.m_dx;
        rect1.m_y = rect.m_y;
        rect1.m_dy = width;
        CDrawUtil.FillRect(rect1, frameColor);
      }
      if ((flags & CDrawUtil.eDrawFrameFlags.DRAWFRAME_NO_BOTTOM) == CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL)
      {
        rect1.m_x = rect.m_x;
        rect1.m_dx = rect.m_dx;
        rect1.m_y = rect.m_y + rect.m_dy - width;
        rect1.m_dy = width;
        CDrawUtil.FillRect(rect1, frameColor);
      }
      if ((flags & CDrawUtil.eDrawFrameFlags.DRAWFRAME_NO_LEFT) == CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL)
      {
        rect1.m_x = rect.m_x;
        rect1.m_dx = width;
        rect1.m_y = rect.m_y;
        rect1.m_dy = rect.m_dy;
        CDrawUtil.FillRect(rect1, frameColor);
      }
      if ((flags & CDrawUtil.eDrawFrameFlags.DRAWFRAME_NO_RIGHT) != CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL)
        return;
      rect1.m_x = rect.m_x + rect.m_dx - width;
      rect1.m_dx = width;
      rect1.m_y = rect.m_y;
      rect1.m_dy = rect.m_dy;
      CDrawUtil.FillRect(rect1, frameColor);
    }

    public static void RenderImageTiled(ICRenderSurface image, int x, int y, int xEnd, int yEnd)
    {
      if (image != null)
        return;
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      instance.PushTransform();
      uint width;
      uint height;
      image.GetWidthAndHeight(out width, out height);
      CRectangle crectangle;
      crectangle.m_x = 0;
      crectangle.m_y = 0;
      instance.Translate(x << 16, y << 16);
      for (int index1 = y; index1 < yEnd; index1 += (int) height)
      {
        instance.PushTransform();
        crectangle.m_dy = CMath.Min((int) height, yEnd - index1);
        for (int index2 = x; index2 < xEnd; index2 += (int) width)
        {
          crectangle.m_dx = CMath.Min((int) width, xEnd - index2);
          instance.Draw(image, ICGraphics2d.Flip.NoFlip, new CRectangle?(crectangle));
          instance.Translate(crectangle.m_dx << 16, 0);
        }
        instance.PopTransform();
        instance.Translate(0, (int) height << 16);
      }
      instance.PopTransform();
    }

    private static void Fill(
      int x0,
      int y0,
      int x1,
      int y1,
      uint color,
      ICGraphics2d.Primitive prim)
    {
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      int alpha = (int) Color.A8R8G8B8_t.GetAlpha(color);
      if (alpha > 240)
      {
        instance.PushState(ICGraphics2d.State.AlphaTest);
        instance.Disable(ICGraphics2d.State.AlphaTest);
      }
      instance.SetColor(color);
      CDrawUtil.m_vbuf[0] = (byte) (x0 & (int) byte.MaxValue);
      CDrawUtil.m_vbuf[1] = (byte) ((x0 & 65280) >> 8);
      CDrawUtil.m_vbuf[2] = (byte) (y0 & (int) byte.MaxValue);
      CDrawUtil.m_vbuf[3] = (byte) ((y0 & 65280) >> 8);
      CDrawUtil.m_vbuf[4] = (byte) (x1 & (int) byte.MaxValue);
      CDrawUtil.m_vbuf[5] = (byte) ((x1 & 65280) >> 8);
      CDrawUtil.m_vbuf[6] = (byte) (y1 & (int) byte.MaxValue);
      CDrawUtil.m_vbuf[7] = (byte) ((y1 & 65280) >> 8);
      instance.SetVertexPositionCoords(2, 4167462U, 4, CDrawUtil.m_vbuf);
      instance.Draw(prim, 0, 2);
      if (alpha <= 240)
        return;
      instance.PopState(ICGraphics2d.State.AlphaTest);
    }

    public enum eDrawFrameFlags
    {
      DRAWFRAME_ALL = 0,
      DRAWFRAME_NO_TOP = 1,
      DRAWFRAME_NO_BOTTOM = 2,
      DRAWFRAME_NO_LEFT = 4,
      DRAWFRAME_NO_RIGHT = 8,
    }
  }
}
