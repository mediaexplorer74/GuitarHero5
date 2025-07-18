// Decompiled with JetBrains decompiler
// Type: com.glu.game.Phone
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public static class Phone
  {
    public static void GetScreen(out CRectangle pScreen)
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      pScreen.m_dx = (int) (short) width;
      pScreen.m_dy = (int) (short) height;
      pScreen.m_x = pScreen.m_y = 0;
    }

    public static void ClipToScreen(ref CRectangle pRect)
    {
      CRectangle pScreen;
      Phone.GetScreen(out pScreen);
      pRect.Clip(pScreen);
    }

    public static short GetWidth()
    {
      uint width;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out uint _);
      return (short) width;
    }

    public static short GetHeight()
    {
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out uint _, out height);
      return (short) height;
    }
  }
}
