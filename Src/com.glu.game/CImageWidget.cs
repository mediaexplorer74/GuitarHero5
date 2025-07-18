// Decompiled with JetBrains decompiler
// Type: com.glu.game.CImageWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace com.glu.game
{
  internal class CImageWidget : CUIWidget
  {
    private const int IMAGEWIDGET_HIGHLIGHT_WIDTH = 1;
    private uint ClassId_CImageWidget = 269966995;
    protected ICRenderSurface m_pImg;
    protected Texture2D m_pImgTex2d;
    protected bool m_useBlitParam;
    protected int m_blitOffsetX;
    protected int m_blitOffsetY;
    protected int m_blitWidth;
    protected int m_blitHeight;

    public CImageWidget()
    {
      this.m_classId = this.ClassId_CImageWidget;
      this.m_pImg = (ICRenderSurface) null;
      this.m_pImgTex2d = (Texture2D) null;
      this.m_useBlitParam = false;
      this.m_blitOffsetX = 0;
      this.m_blitOffsetY = 0;
      this.m_blitWidth = 0;
      this.m_blitHeight = 0;
    }

    public void SetImage(ICRenderSurface pImg)
    {
      this.m_pImg = pImg;
      this.OnSetNewLayout();
    }

    public void SetImage(Texture2D pImg)
    {
      this.m_pImgTex2d = pImg;
      this.OnSetNewLayout();
    }

    public ICRenderSurface GetImage() => this.m_pImg;

    public void SetBlitParam(int offsetX, int offsetY, int width, int height)
    {
      this.m_useBlitParam = true;
      this.m_blitOffsetX = offsetX;
      this.m_blitOffsetY = offsetY;
      this.m_blitWidth = width;
      this.m_blitHeight = height;
      this.OnSetDirty();
    }

    public override void Layout()
    {
      if (this.m_pImg != null)
      {
        if (this.m_useBlitParam)
        {
          this.m_contentWidth = (uint) this.m_blitWidth;
          this.m_contentHeight = (uint) this.m_blitHeight;
        }
        else
          this.m_pImg.GetWidthAndHeight(out this.m_contentWidth, out this.m_contentHeight);
      }
      else
      {
        if (this.m_pImgTex2d == null)
          return;
        if (this.m_useBlitParam)
        {
          this.m_contentWidth = (uint) this.m_blitWidth;
          this.m_contentHeight = (uint) this.m_blitHeight;
        }
        else
        {
          this.m_contentWidth = (uint) this.m_pImgTex2d.Width;
          this.m_contentHeight = (uint) this.m_pImgTex2d.Height;
        }
      }
    }

    public override void Paint()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      if (this.m_pImgTex2d == null && this.m_pImg != null)
      {
        uint width;
        uint height;
        this.m_pImg.GetWidthAndHeight(out width, out height);
        int num1;
        int num2;
        int x;
        int y;
        if (this.m_useBlitParam)
        {
          num1 = this.m_blitWidth;
          num2 = this.m_blitHeight;
          x = this.m_blitOffsetX;
          y = this.m_blitOffsetY;
        }
        else
        {
          num1 = (int) width;
          num2 = (int) height;
          x = 0;
          y = 0;
        }
        int horizontalAlignedPosition = this.GetHorizontalAlignedPosition(0, this.m_rect.m_dx, num1);
        int verticalAlignedPosition = this.GetVerticalAlignedPosition(0, this.m_rect.m_dy, num2);
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CRectangle crectangle = new CRectangle(x, y, num1, num2);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(horizontalAlignedPosition), CMathFixed.Int32ToFixed(verticalAlignedPosition));
        instance1.Draw(this.m_pImg, ICGraphics2d.Flip.NoFlip, new CRectangle?(crectangle));
        instance1.PopTransform();
        if (!this.GetFocus())
          return;
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CRectangle rect;
        rect.m_x = horizontalAlignedPosition;
        rect.m_dx = (int) (short) width;
        rect.m_y = verticalAlignedPosition;
        rect.m_dy = (int) (short) height;
        CDrawUtil.DrawFrame(rect, this.m_highlightColor, 1, CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL);
      }
      else
      {
        if (this.m_pImgTex2d == null)
          return;
        SpriteBatch spriteBatch = new SpriteBatch(((CGraphics) ICGraphics.GetInstance()).GetGraphicsDeviceXNA());
        Vector2 position = new Vector2(0.0f, 0.0f);
        int width = this.m_pImgTex2d.Width;
        int height = this.m_pImgTex2d.Height;
        int num3;
        int num4;
        int x;
        int y;
        if (this.m_useBlitParam)
        {
          num3 = this.m_blitWidth;
          num4 = this.m_blitHeight;
          x = this.m_blitOffsetX;
          y = this.m_blitOffsetY;
        }
        else
        {
          num3 = width;
          num4 = height;
          x = 0;
          y = 0;
        }
        int horizontalAlignedPosition = this.GetHorizontalAlignedPosition(0, this.m_rect.m_dx, num3);
        int verticalAlignedPosition = this.GetVerticalAlignedPosition(0, this.m_rect.m_dy, num4);
        CRectangle crectangle = new CRectangle(x, y, num3, num4);
        position.X = (float) this.m_rect.m_x;
        position.Y = (float) this.m_rect.m_y;
        instance1.GetClip();
        instance1.RenderEnd();
        spriteBatch.Begin();
        spriteBatch.Draw(this.m_pImgTex2d, position, Microsoft.Xna.Framework.Color.White);
        spriteBatch.End();
        instance1.RenderBegin();
        if (!this.GetFocus())
          return;
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CRectangle rect;
        rect.m_x = horizontalAlignedPosition;
        rect.m_dx = (int) (short) width;
        rect.m_y = verticalAlignedPosition;
        rect.m_dy = (int) (short) height;
        CDrawUtil.DrawFrame(rect, this.m_highlightColor, 1, CDrawUtil.eDrawFrameFlags.DRAWFRAME_ALL);
      }
    }
  }
}
