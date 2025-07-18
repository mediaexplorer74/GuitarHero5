// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSoftkeyWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSoftkeyWidget : CUIWidget
  {
    public int m_ofsX;
    public int m_ofsY;
    protected ICRenderSurface m_pLeftImage;
    protected CFont m_pLeftFont;
    protected string m_pwszLeftText;
    protected eSoftkeyPosition m_leftPosition;
    protected CRectangle m_leftRect;
    protected ICRenderSurface m_pRightImage;
    protected CFont m_pRightFont;
    protected string m_pwszRightText;
    protected eSoftkeyPosition m_rightPosition;
    protected CRectangle m_rightRect;
    protected bool m_reversed;
    protected bool m_draw;
    protected int m_offset;
    protected bool m_leftSelected;
    protected bool m_rightSelected;
    private int x;
    private int y;
    private CRectangle ldrect;

    public CSoftkeyWidget()
    {
      this.m_pLeftImage = (ICRenderSurface) null;
      this.m_pLeftFont = (CFont) null;
      this.m_pwszLeftText = (string) null;
      this.m_leftPosition = eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT;
      this.m_pRightImage = (ICRenderSurface) null;
      this.m_pRightFont = (CFont) null;
      this.m_pwszRightText = (string) null;
      this.m_rightPosition = eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_RIGHT;
      this.m_draw = true;
      this.m_leftSelected = false;
      this.m_rightSelected = false;
      this.m_ofsX = 0;
      this.m_ofsY = 0;
    }

    public bool IsReversed() => this.m_reversed;

    public bool HasLeft()
    {
      if (this.m_reversed)
      {
        if (this.m_pRightImage != null)
          return true;
        return this.m_pRightFont != null && this.m_pwszRightText != null;
      }
      if (this.m_pLeftImage != null)
        return true;
      return this.m_pLeftFont != null && this.m_pwszLeftText != null;
    }

    public bool HasRight()
    {
      if (this.m_reversed)
      {
        if (this.m_pLeftImage != null)
          return true;
        return this.m_pLeftFont != null && this.m_pwszLeftText != null;
      }
      if (this.m_pRightImage != null)
        return true;
      return this.m_pRightFont != null && this.m_pwszRightText != null;
    }

    public void SetLeftFont(CFont pFont)
    {
      if (this.m_reversed)
        this.m_pRightFont = pFont;
      else
        this.m_pLeftFont = pFont;
      this.OnSetNewLayout();
    }

    public void SetLeftText(string pwszText)
    {
      if (this.m_reversed)
        this.m_pwszRightText = pwszText;
      else
        this.m_pwszLeftText = pwszText;
      this.OnSetNewLayout();
    }

    public void SetLeftImage(ICRenderSurface pImage)
    {
      if (this.m_reversed)
        this.m_pRightImage = pImage;
      else
        this.m_pLeftImage = pImage;
      this.OnSetNewLayout();
    }

    public void SetLeftPosition(eSoftkeyPosition position)
    {
      this.m_leftPosition = position;
      this.OnSetNewLayout();
    }

    public void SetRightFont(CFont pFont)
    {
      if (this.m_reversed)
        this.m_pLeftFont = pFont;
      else
        this.m_pRightFont = pFont;
      this.OnSetNewLayout();
    }

    public void SetRightText(string pwszText)
    {
      if (this.m_reversed)
        this.m_pwszLeftText = pwszText;
      else
        this.m_pwszRightText = pwszText;
      this.OnSetNewLayout();
    }

    public void SetRightImage(ICRenderSurface pImage)
    {
      if (this.m_reversed)
        this.m_pLeftImage = pImage;
      else
        this.m_pRightImage = pImage;
      this.OnSetNewLayout();
    }

    public void SetRightPosition(eSoftkeyPosition position)
    {
      this.m_rightPosition = position;
      this.OnSetNewLayout();
    }

    public override void Layout()
    {
      this.m_leftRect.Clear();
      if (this.m_pLeftImage != null)
      {
        this.GetSoftkeyRenderPosition(ref this.x, ref this.y, this.m_pLeftImage, this.m_leftPosition);
        this.m_leftRect.m_x = (int) (short) this.x;
        this.m_leftRect.m_y = (int) (short) this.y;
        uint width;
        uint height;
        this.m_pLeftImage.GetWidthAndHeight(out width, out height);
        this.m_leftRect.m_dx = (int) (short) width;
        this.m_leftRect.m_dy = (int) (short) height;
      }
      else if (this.m_pLeftFont != null && this.m_pwszLeftText != null)
      {
        this.GetSoftkeyRenderPosition(ref this.x, ref this.y, this.m_pLeftFont, this.m_pwszLeftText, this.m_leftPosition);
        this.m_leftRect.m_x = (int) (short) this.x;
        this.m_leftRect.m_y = (int) (short) this.y;
        this.m_leftRect.m_dx = (int) (short) this.m_pLeftFont.MeasureTextWidth(this.m_pwszLeftText);
        this.m_leftRect.m_dy = (int) (short) this.m_pLeftFont.GetFontHeight();
      }
      this.m_rightRect.Clear();
      if (this.m_pRightImage != null)
      {
        this.GetSoftkeyRenderPosition(ref this.x, ref this.y, this.m_pRightImage, this.m_rightPosition);
        this.m_rightRect.m_x = (int) (short) this.x;
        this.m_rightRect.m_y = (int) (short) this.y;
        uint width;
        uint height;
        this.m_pRightImage.GetWidthAndHeight(out width, out height);
        this.m_rightRect.m_dx = (int) (short) width;
        this.m_rightRect.m_dy = (int) (short) height;
      }
      else if (this.m_pRightFont != null && this.m_pwszRightText != null)
      {
        this.GetSoftkeyRenderPosition(ref this.x, ref this.y, this.m_pRightFont, this.m_pwszRightText, this.m_rightPosition);
        this.m_rightRect.m_x = (int) (short) this.x;
        this.m_rightRect.m_y = (int) (short) this.y;
        this.m_rightRect.m_dx = (int) (short) this.m_pRightFont.MeasureTextWidth(this.m_pwszRightText);
        this.m_rightRect.m_dy = (int) (short) this.m_pRightFont.GetFontHeight();
      }
      if (this.m_leftPosition == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT && this.m_rightPosition == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_RIGHT || this.m_leftPosition == eSoftkeyPosition.SOFTKEY_POSITION_TOP_RIGHT && this.m_rightPosition == eSoftkeyPosition.SOFTKEY_POSITION_TOP_LEFT)
      {
        this.m_contentWidth = 0U;
        this.m_contentHeight = (uint) CMath.Max(this.m_leftRect.m_dy, this.m_rightRect.m_dy);
      }
      else
      {
        this.m_contentWidth = (uint) CMath.Max(this.m_leftRect.m_dx, this.m_rightRect.m_dx);
        this.m_contentHeight = 0U;
      }
    }

    public override void Paint()
    {
      if (!this.m_draw)
        return;
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      if (this.m_pLeftImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(this.m_leftRect.m_x + this.m_ofsX), CMathFixed.Int32ToFixed(this.m_leftRect.m_y + this.m_ofsY));
        instance1.Draw(this.m_pLeftImage);
        instance1.PopTransform();
      }
      else if (this.m_pLeftFont != null && this.m_pwszLeftText != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        if (this.m_pwszLeftText != null)
          this.m_pLeftFont.PaintText(this.m_pwszLeftText, -1, this.m_leftRect.m_x, this.m_leftRect.m_y);
      }
      if (this.m_pRightImage != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(this.m_rightRect.m_x - this.m_ofsX), CMathFixed.Int32ToFixed(this.m_rightRect.m_y + this.m_ofsY));
        instance1.Draw(this.m_pRightImage);
        instance1.PopTransform();
      }
      else
      {
        if (this.m_pRightFont == null || this.m_pwszRightText == null)
          return;
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        this.m_pRightFont.PaintText(this.m_pwszRightText, -1, this.m_rightRect.m_x, this.m_rightRect.m_y);
      }
    }

    public override bool OnMouseEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      if (this.GetActive() && this.GetVisible())
      {
        this.clip.Clear();
        this.GetClipRect(ref this.clip);
        CRectangle r;
        r.m_x = (int) (short) MouseUtil.MOUSE_EVENT_GET_X(param2);
        r.m_y = (int) (short) MouseUtil.MOUSE_EVENT_GET_Y(param2);
        r.m_dx = 1;
        r.m_dy = 1;
        if (this.clip.Contains(r))
        {
          switch (id)
          {
            case 902053462:
            case 2300082508:
              this.SetFocus(true);
              this.SetSelection(true);
              this.m_leftSelected = false;
              this.m_rightSelected = false;
              this.GetLocalDrawRect(ref this.ldrect);
              r.m_x -= this.clip.m_x + this.ldrect.m_x;
              r.m_y -= this.clip.m_y + this.ldrect.m_y;
              if (this.m_leftRect.Contains(r))
                this.m_leftSelected = true;
              else if (this.m_rightRect.Contains(r))
                this.m_rightSelected = true;
              flag = true;
              break;
            case 1386813809:
              this.GetLocalDrawRect(ref this.ldrect);
              r.m_x -= this.clip.m_x + this.ldrect.m_x;
              r.m_y -= this.clip.m_y + this.ldrect.m_y;
              CRectangle leftRect = this.m_leftRect;
              CRectangle rightRect = this.m_rightRect;
              leftRect.Inset(-5);
              rightRect.Inset(-5);
              if (this.m_leftSelected && leftRect.Contains(r))
                CApplet.GetInstance().m_eventQueue.Queue(1600235594U, 0U, 0U);
              else if (this.m_rightSelected && rightRect.Contains(r))
              {
                CApplet.GetInstance().m_eventQueue.Queue(3563016926U, 0U, 0U);
                CApplet.GetInstance().m_eventQueue.Queue(1912541268U, 0U, 0U);
              }
              this.m_leftSelected = false;
              this.m_rightSelected = false;
              flag = true;
              break;
          }
          this.OnSetDirty();
        }
        else
        {
          switch (id)
          {
            case 902053462:
            case 1386813809:
            case 2300082508:
              this.m_leftSelected = false;
              this.m_rightSelected = false;
              break;
          }
          this.OnSetDirty();
        }
      }
      return flag;
    }

    public void GetSoftkeyRenderPosition(
      ref int x,
      ref int y,
      ICRenderSurface pImage,
      eSoftkeyPosition position)
    {
      uint width;
      uint height;
      pImage.GetWidthAndHeight(out width, out height);
      this.GetSoftkeyRenderPosition(ref x, ref y, (int) width, (int) height, position);
    }

    public void GetSoftkeyRenderPosition(
      ref int x,
      ref int y,
      CFont pFont,
      string pwszText,
      eSoftkeyPosition position)
    {
      this.GetSoftkeyRenderPosition(ref x, ref y, pFont.MeasureTextWidth(pwszText), pFont.GetFontHeight(), position);
    }

    public void GetSoftkeyRenderPosition(
      ref int x,
      ref int y,
      int w,
      int h,
      eSoftkeyPosition position)
    {
      x = 0;
      y = 0;
      if (this.m_rect.m_dx > this.m_rect.m_dy)
      {
        x = position == eSoftkeyPosition.SOFTKEY_POSITION_TOP_LEFT || position == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT ? this.m_offset + CGameApp.GetInstance().GetSoftkeyOffsetX(true) : this.m_rect.m_dx - w - this.m_offset - CGameApp.GetInstance().GetSoftkeyOffsetX(false);
        y = CGenUtil.AlignedPosition(0, this.m_rect.m_dy, h, 32768U, 32768U);
      }
      else
      {
        y = position == eSoftkeyPosition.SOFTKEY_POSITION_TOP_LEFT || position == eSoftkeyPosition.SOFTKEY_POSITION_TOP_RIGHT ? this.m_offset + CGameApp.GetInstance().GetSoftkeyOffsetX(true) : this.m_rect.m_dy - h - this.m_offset - CGameApp.GetInstance().GetSoftkeyOffsetX(false);
        x = CGenUtil.AlignedPosition(0, this.m_rect.m_dx, w, 32768U, 32768U);
      }
    }
  }
}
