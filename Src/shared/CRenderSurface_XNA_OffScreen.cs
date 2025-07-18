// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRenderSurface_XNA_OffScreen
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public sealed class CRenderSurface_XNA_OffScreen : CRenderSurface
  {
    public Texture2D m_texture;
    public Vector2 m_origin;
    public uint m_width;
    public uint m_height;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public override bool Initialize(ICRenderSurface.Param[] param)
    {
      this.m_texture = (Texture2D) null;
      this.m_origin = Vector2.Zero;
      this.m_usingLoadtimeTransform = false;
      this.m_transpose = false;
      uint width = 0;
      uint height = 0;
      uint num1 = 0;
      bool flag = true;
      CInputStream cinputStream = (CInputStream) null;
      foreach (ICRenderSurface.Param obj in param)
      {
        if (obj.m_id != ICRenderSurface.ParamId.Unknown)
        {
          switch (obj.m_id)
          {
            case ICRenderSurface.ParamId.Width:
              width = (uint) obj.m_val;
              break;
            case ICRenderSurface.ParamId.Height:
              height = (uint) obj.m_val;
              break;
            case ICRenderSurface.ParamId.ColorBufferFormat:
              int val = (int) obj.m_val;
              break;
            case ICRenderSurface.ParamId.PointerToSourceImageStream:
              cinputStream = (CInputStream) obj.m_val;
              break;
            case ICRenderSurface.ParamId.MimeKeyOfSourceImageStream:
              num1 = (uint) obj.m_val;
              break;
            case ICRenderSurface.ParamId.SourceImageStreamTransform:
              this.m_usingLoadtimeTransform = true;
              this.m_srcImgTransformType = (ICRenderSurface.SourceImageStreamTransformType) (uint) obj.m_val;
              if (this.m_srcImgTransformType == ICRenderSurface.SourceImageStreamTransformType.Transpose || this.m_srcImgTransformType == ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByHorizontalFlip || this.m_srcImgTransformType == ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByVerticalFlip || this.m_srcImgTransformType == ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByHorizontalAndVerticalFlip)
              {
                this.m_transpose = true;
                break;
              }
              break;
          }
        }
        else
          break;
      }
      if (cinputStream != null)
      {
        if (num1 == 1930331075U)
        {
          byte[] bytes = new byte[256];
          int count = 0;
          byte num2 = cinputStream.ReadUInt8();
          while (num2 != (byte) 0)
          {
            bytes[count] = num2;
            num2 = cinputStream.ReadUInt8();
            ++count;
          }
          string assetName = CRenderSurface_XNA_OffScreen.m_utf8Encoding.GetString(bytes, 0, count);
          this.m_texture = CApplet.GetInstance().Content.Load<Texture2D>(assetName);
        }
      }
      else if (width != 0U && height != 0U)
      {
        SurfaceFormat format = SurfaceFormat.Color;
        this.m_texture = new Texture2D(((CGraphics) ICGraphics.GetInstance()).GetGraphicsDeviceXNA(), (int) width, (int) height, false, format);
        Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[(int) (width * height)];
        for (int index = 0; index < data.Length; ++index)
        {
          data[index].R = byte.MaxValue;
          data[index].G = byte.MaxValue;
          data[index].B = byte.MaxValue;
          data[index].A = byte.MaxValue;
        }
        this.m_texture.SetData<Microsoft.Xna.Framework.Color>(data);
      }
      if (this.m_texture != null)
      {
        if (this.m_transpose)
        {
          this.m_origin.Y = (float) this.m_texture.Height;
          this.m_width = (uint) this.m_texture.Height;
          this.m_height = (uint) this.m_texture.Width;
        }
        else
        {
          this.m_width = (uint) this.m_texture.Width;
          this.m_height = (uint) this.m_texture.Height;
        }
      }
      else
        flag = false;
      return flag;
    }

    public override bool GetWidthAndHeight(out uint width, out uint height)
    {
      if (this.m_texture != null)
      {
        width = this.m_width;
        height = this.m_height;
        return true;
      }
      width = 0U;
      height = 0U;
      return false;
    }

    public CRenderSurface_XNA_OffScreen()
    {
      this.m_texture = (Texture2D) null;
      this.m_origin = Vector2.Zero;
      this.m_width = 0U;
      this.m_height = 0U;
    }
  }
}
