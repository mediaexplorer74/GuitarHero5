// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRenderSurface_XNA_Window
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace com.glu.shared
{
  public sealed class CRenderSurface_XNA_Window : CRenderSurface
  {
    public override bool Initialize(ICRenderSurface.Param[] param)
    {
      uint num = 0;
      bool flag = true;
      CInputStream cinputStream = (CInputStream) null;
      foreach (ICRenderSurface.Param obj in param)
      {
        if (obj.m_id != ICRenderSurface.ParamId.Unknown)
        {
          switch (obj.m_id)
          {
            case ICRenderSurface.ParamId.PointerToSourceImageStream:
              cinputStream = (CInputStream) obj.m_val;
              break;
            case ICRenderSurface.ParamId.MimeKeyOfSourceImageStream:
              num = (uint) obj.m_val;
              break;
          }
        }
        else
          break;
      }
      if (cinputStream != null && num == 1930331075U)
      {
        string str = "";
        for (char ch = (char) cinputStream.ReadUInt8(); ch != char.MinValue; ch = (char) cinputStream.ReadUInt8())
          str += (string) (object) ch;
      }
      return flag;
    }

    public override bool GetWidthAndHeight(out uint width, out uint height)
    {
      GraphicsDevice graphicsDeviceXna = ((CGraphics) ICGraphics.GetInstance()).GetGraphicsDeviceXNA();
      width = (uint) graphicsDeviceXna.Viewport.Width;
      height = (uint) graphicsDeviceXna.Viewport.Height;
      return false;
    }
  }
}
