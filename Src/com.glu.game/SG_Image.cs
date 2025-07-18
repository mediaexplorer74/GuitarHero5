// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Image
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class SG_Image
  {
    public const ushort SG_QUANTUM_SIZE_MAX = 1000;
    public ushort m_imageID;
    private byte m_transformsLoaded;
    private ICRenderSurface m_pSrcImage;
    private ICRenderSurface m_pSrcImageTransposed;

    public SG_Image()
    {
      this.m_transformsLoaded = SquareTransform.BITFIELD_EMPTY;
      this.m_pSrcImage = (ICRenderSurface) null;
      this.m_pSrcImageTransposed = (ICRenderSurface) null;
    }

    public void GetSize(byte transform, out uint width, out uint height)
    {
      if (SquareTransform.IsDimensionSwitching(transform))
        this.m_pSrcImageTransposed.GetWidthAndHeight(out width, out height);
      else
        this.m_pSrcImage.GetWidthAndHeight(out width, out height);
    }

    public bool LoadTransformRequiresRawImage(byte transform)
    {
      return SquareTransform.IsDimensionSwitching(transform) ? !this.IsAnyDimensionSwitchingTransformLoaded() : !this.IsAnyDimensionPreservingTransformLoaded();
    }

    public bool IsAnyDimensionSwitchingTransformLoaded()
    {
      return SquareTransform.BitFieldContainsAnyDimensionSwitchingTransform(this.m_transformsLoaded);
    }

    public bool IsAnyDimensionPreservingTransformLoaded()
    {
      return SquareTransform.BitFieldContainsAnyDimensionPreservingTransform(this.m_transformsLoaded);
    }

    public bool IsTransformLoaded(byte transform)
    {
      return SquareTransform.BitFieldContainsTransform(this.m_transformsLoaded, transform);
    }

    public bool LoadTransform(byte transform, ICRenderSurface p_ICRenderSurface)
    {
      if (SquareTransform.IsDimensionSwitching(transform))
      {
        if (this.m_pSrcImageTransposed == null)
          this.m_pSrcImageTransposed = p_ICRenderSurface;
      }
      else if (this.m_pSrcImage == null)
        this.m_pSrcImage = p_ICRenderSurface;
      bool flag = !SquareTransform.BitFieldContainsTransform(this.m_transformsLoaded, transform);
      SquareTransform.ActivateBitFieldWithTransform(ref this.m_transformsLoaded, transform);
      return flag;
    }

    public bool DumpTransform(byte transform)
    {
      bool flag = true;
      SquareTransform.DeactivateBitFieldWithTransform(ref this.m_transformsLoaded, transform);
      if (SquareTransform.IsDimensionSwitching(transform))
      {
        if (!this.IsAnyDimensionSwitchingTransformLoaded())
        {
          this.m_pSrcImageTransposed = (ICRenderSurface) null;
          flag = false;
        }
      }
      else if (!this.IsAnyDimensionPreservingTransformLoaded())
      {
        this.m_pSrcImage = (ICRenderSurface) null;
        flag = false;
      }
      return flag;
    }

    public uint DumpAllTransforms()
    {
      uint num = 0;
      this.m_transformsLoaded = SquareTransform.BITFIELD_EMPTY;
      if (this.m_pSrcImageTransposed != null)
      {
        this.m_pSrcImageTransposed = (ICRenderSurface) null;
        ++num;
      }
      if (this.m_pSrcImage != null)
      {
        this.m_pSrcImage = (ICRenderSurface) null;
        ++num;
      }
      return num;
    }

    public void GetSrcImageAndRenderInfo(
      byte transform,
      out ICRenderSurface p_ICRenderSurface,
      ref Graphics.RenderInfo renderInfo)
    {
      if (SquareTransform.IsDimensionSwitching(transform))
      {
        p_ICRenderSurface = this.m_pSrcImageTransposed;
        if (SquareTransform.FlipsY(transform) != 0U && SquareTransform.FlipsX(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.HorizontalAndVerticalFlip;
        else if (SquareTransform.FlipsX(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.VerticalFlip;
        else if (SquareTransform.FlipsY(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.HorizontalFlip;
        else
          renderInfo.m_flip = ICGraphics2d.Flip.NoFlip;
      }
      else
      {
        p_ICRenderSurface = this.m_pSrcImage;
        if (SquareTransform.FlipsY(transform) != 0U && SquareTransform.FlipsX(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.HorizontalAndVerticalFlip;
        else if (SquareTransform.FlipsY(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.VerticalFlip;
        else if (SquareTransform.FlipsX(transform) != 0U)
          renderInfo.m_flip = ICGraphics2d.Flip.HorizontalFlip;
        else
          renderInfo.m_flip = ICGraphics2d.Flip.NoFlip;
      }
    }
  }
}
