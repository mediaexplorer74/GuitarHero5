// Decompiled with JetBrains decompiler
// Type: com.glu.game.CFrameWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CFrameWidget : CContainerWidget
  {
    protected ICRenderSurface m_tlImage;
    protected ICRenderSurface m_trImage;
    protected ICRenderSurface m_blImage;
    protected ICRenderSurface m_brImage;
    protected ICRenderSurface m_tImage;
    protected ICRenderSurface m_bImage;
    protected ICRenderSurface m_lImage;
    protected ICRenderSurface m_rImage;

    public ICRenderSurface GetTImage() => this.m_tImage;

    public ICRenderSurface GetBImage() => this.m_bImage;

    public ICRenderSurface GetLImage() => this.m_lImage;

    public ICRenderSurface GetRImage() => this.m_rImage;

    public ICRenderSurface GetTLImage() => this.m_tlImage;

    public ICRenderSurface GetTRImage() => this.m_trImage;

    public ICRenderSurface GetBLImage() => this.m_blImage;

    public ICRenderSurface GetBRImage() => this.m_brImage;

    public CFrameWidget()
    {
      this.m_tlImage = (ICRenderSurface) null;
      this.m_trImage = (ICRenderSurface) null;
      this.m_blImage = (ICRenderSurface) null;
      this.m_brImage = (ICRenderSurface) null;
      this.m_tImage = (ICRenderSurface) null;
      this.m_bImage = (ICRenderSurface) null;
      this.m_lImage = (ICRenderSurface) null;
      this.m_rImage = (ICRenderSurface) null;
    }

    public void SetTImage(ICRenderSurface image)
    {
      this.m_tImage = image;
      this.OnSetNewLayout();
    }

    public void SetBImage(ICRenderSurface image)
    {
      this.m_bImage = image;
      this.OnSetNewLayout();
    }

    public void SetLImage(ICRenderSurface image)
    {
      this.m_lImage = image;
      this.OnSetNewLayout();
    }

    public void SetRImage(ICRenderSurface image)
    {
      this.m_rImage = image;
      this.OnSetNewLayout();
    }

    public void SetTLImage(ICRenderSurface image)
    {
      this.m_tlImage = image;
      this.OnSetNewLayout();
    }

    public void SetTRImage(ICRenderSurface image)
    {
      this.m_trImage = image;
      this.OnSetNewLayout();
    }

    public void SetBLImage(ICRenderSurface image)
    {
      this.m_blImage = image;
      this.OnSetNewLayout();
    }

    public void SetBRImage(ICRenderSurface image)
    {
      this.m_brImage = image;
      this.OnSetNewLayout();
    }

    public CUIWidget GetContent() => this.GetChild(0);

    public void SetContent(CUIWidget content)
    {
      this.RemoveChild(this.GetChild(0));
      if (content == null)
        return;
      this.AddChild(content, 0);
    }

    public override void Layout()
    {
      uint width1 = 0;
      uint height1 = 0;
      uint width2 = 0;
      uint height2 = 0;
      uint width3 = 0;
      uint height3 = 0;
      uint width4 = 0;
      uint height4 = 0;
      if (this.m_tImage != null)
        this.m_tlImage.GetWidthAndHeight(out width1, out height1);
      if (this.m_bImage != null)
        this.m_trImage.GetWidthAndHeight(out width2, out height2);
      if (this.m_lImage != null)
        this.m_blImage.GetWidthAndHeight(out width3, out height3);
      if (this.m_rImage != null)
        this.m_brImage.GetWidthAndHeight(out width4, out height4);
      int insetTop = (int) ((long) height1 + (long) this.m_contentInsetTopY);
      int insetBottom = (int) ((long) height2 + (long) this.m_contentInsetBotY);
      int insetLeft = (int) ((long) width3 + (long) this.m_contentInsetX);
      int insetRight = (int) ((long) width4 + (long) this.m_contentInsetX);
      this.m_contentWidth = (uint) (insetLeft + insetRight);
      this.m_contentHeight = (uint) (insetTop + insetBottom);
      CUIWidget child = this.GetChild(0);
      if (child == null)
        return;
      CRectangle rect;
      rect.m_x = 0;
      rect.m_y = 0;
      rect.m_dx = this.m_rect.m_dx;
      rect.m_dy = this.m_rect.m_dy;
      rect.Inset(insetLeft, insetTop, insetRight, insetBottom);
      child.SetRect(rect);
      child.HandleLayout();
      int contentWidth = child.GetContentWidth();
      int contentHeight = child.GetContentHeight();
      if (contentWidth > 0 && this.m_shrinkX)
        this.m_contentWidth += (uint) contentWidth;
      else
        this.m_contentWidth = 0U;
      if (contentHeight > 0 && this.m_shrinkY)
        this.m_contentHeight += (uint) contentHeight;
      else
        this.m_contentHeight = 0U;
      this.SetFocusToNextOnScreenFocusableWidget((CUIWidget) null, true);
    }

    public override void Paint()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CRectangle crectangle = new CRectangle(this.m_rect);
        crectangle.Inset(this.m_contentInsetX, this.m_contentInsetTopY, this.m_contentInsetX, this.m_contentInsetBotY);
        CDrawUtil.FillRect(0, 0, crectangle.m_dx, crectangle.m_dy, this.m_backgroundColor);
      }
      CUIWidget child = this.GetChild(0);
      if (child != null)
      {
        child.SetDirty();
        child.HandleRender();
      }
      uint width1 = 0;
      uint height1 = 0;
      uint width2 = 0;
      uint height2 = 0;
      uint width3 = 0;
      uint height3 = 0;
      uint width4 = 0;
      uint height4 = 0;
      if (this.m_tlImage != null)
        this.m_tlImage.GetWidthAndHeight(out width1, out height1);
      if (this.m_trImage != null)
        this.m_trImage.GetWidthAndHeight(out width2, out height2);
      if (this.m_blImage != null)
        this.m_blImage.GetWidthAndHeight(out width3, out height3);
      if (this.m_brImage != null)
        this.m_brImage.GetWidthAndHeight(out width4, out height4);
      if (this.m_tImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        uint height5;
        this.m_tImage.GetWidthAndHeight(out uint _, out height5);
        CDrawUtil.RenderImageTiled(this.m_tImage, (int) width1, 0, (int) ((long) this.m_rect.m_dx - (long) width2), (int) height5);
      }
      if (this.m_bImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        uint height6;
        this.m_bImage.GetWidthAndHeight(out uint _, out height6);
        CDrawUtil.RenderImageTiled(this.m_bImage, (int) width3, (int) ((long) this.m_rect.m_dy - (long) height6), (int) ((long) this.m_rect.m_dx - (long) width4), this.m_rect.m_dy);
      }
      if (this.m_lImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        uint width5;
        this.m_lImage.GetWidthAndHeight(out width5, out uint _);
        CDrawUtil.RenderImageTiled(this.m_lImage, 0, (int) height1, (int) width5, (int) ((long) this.m_rect.m_dy - (long) height3));
      }
      if (this.m_rImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        uint width6;
        this.m_rImage.GetWidthAndHeight(out width6, out uint _);
        CDrawUtil.RenderImageTiled(this.m_rImage, (int) ((long) this.m_rect.m_dx - (long) width6), (int) height2, this.m_rect.m_dx, (int) ((long) this.m_rect.m_dy - (long) height4));
      }
      if (this.m_tlImage != null)
      {
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dx, (int) width1, 0U, 0U)), CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dy, (int) height1, 0U, 0U)));
        instance1.Draw(this.m_tlImage);
        instance1.PopTransform();
      }
      if (this.m_trImage != null)
      {
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dx, (int) width2, 65536U, 65536U)), CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dy, (int) height1, 0U, 0U)));
        instance1.Draw(this.m_trImage);
        instance1.PopTransform();
      }
      if (this.m_blImage != null)
      {
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dx, (int) width3, 0U, 0U)), CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dy, (int) height3, 65536U, 65536U)));
        instance1.Draw(this.m_blImage);
        instance1.PopTransform();
      }
      if (this.m_brImage == null)
        return;
      instance1.PushTransform();
      instance1.Translate(CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dx, (int) width4, 65536U, 65536U)), CMathFixed.Int32ToFixed(CGenUtil.AlignedPosition(0, this.m_rect.m_dy, (int) height4, 65536U, 65536U)));
      instance1.Draw(this.m_brImage);
      instance1.PopTransform();
    }
  }
}
