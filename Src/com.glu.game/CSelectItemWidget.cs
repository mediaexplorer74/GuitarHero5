// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSelectItemWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSelectItemWidget : CUIWidget
  {
    protected CTextWidget m_text = new CTextWidget();
    protected ICRenderSurface m_pCheckedImg;
    protected ICRenderSurface m_pUncheckedImg;
    protected CSelectItemWidget.eSelectItemWidgetImagePosition m_imagePosition;
    protected int m_checkedImgPosX;
    protected int m_checkedImgPosY;
    protected int m_uncheckedImgPosX;
    protected int m_uncheckedImgPosY;
    protected bool m_checked;

    public bool GetChecked() => this.m_checked;

    public int GetTextWidth() => this.m_text.GetTextWidth();

    public CSelectItemWidget()
    {
      this.m_pCheckedImg = (ICRenderSurface) null;
      this.m_pUncheckedImg = (ICRenderSurface) null;
      this.m_imagePosition = CSelectItemWidget.eSelectItemWidgetImagePosition.SELECTITEM_POSITION_LEFT;
      this.m_checkedImgPosX = 0;
      this.m_checkedImgPosY = 0;
      this.m_uncheckedImgPosX = 0;
      this.m_uncheckedImgPosY = 0;
      this.m_checked = false;
    }

    private void SetMaxLines(int maxLines)
    {
      this.m_text.SetMaxLines(maxLines);
      this.OnSetNewLayout();
    }

    public void SetFont(CFont pFont)
    {
      this.m_text.SetFont(pFont);
      this.OnSetNewLayout();
    }

    public void SetText(string pwszContent)
    {
      this.m_text.SetText(pwszContent);
      this.OnSetNewLayout();
    }

    private void SetCheckedImage(ICRenderSurface pImg)
    {
      this.m_pCheckedImg = pImg;
      this.OnSetNewLayout();
    }

    private void SetUncheckedImage(ICRenderSurface pImg)
    {
      this.m_pUncheckedImg = pImg;
      this.OnSetNewLayout();
    }

    private void SetImagePosition(
      CSelectItemWidget.eSelectItemWidgetImagePosition imagePosition)
    {
      this.m_imagePosition = imagePosition;
      this.OnSetNewLayout();
    }

    public void SetChecked(bool boolchecked)
    {
      this.m_checked = boolchecked;
      this.OnSetDirty();
    }

    public override void Layout()
    {
      this.m_text.SetParent((CWidget) this);
      this.m_text.SetColor(this.m_backgroundColor, this.m_foregroundColor, this.m_highlightColor);
      this.m_text.SetAlignment(this.GetAlignment());
      this.m_text.SetFocusable(this.GetFocusable());
      this.m_text.SetSelectable(this.GetSelectable());
      this.m_text.SetTransparent(this.GetTransparent());
      uint width1 = 0;
      uint height1 = 0;
      uint width2 = 0;
      uint height2 = 0;
      if (this.m_pCheckedImg != null)
        this.m_pCheckedImg.GetWidthAndHeight(out width1, out height1);
      if (this.m_pUncheckedImg != null)
        this.m_pUncheckedImg.GetWidthAndHeight(out width2, out height2);
      int num1 = (int) CMath.Max(width2, width1);
      int val1 = (int) CMath.Max(height2, height1);
      CRectangle rect;
      rect.m_x = num1;
      rect.m_y = 0;
      rect.m_dx = this.m_rect.m_dx - num1;
      rect.m_dy = this.m_rect.m_dy;
      this.m_text.SetRect(rect);
      this.m_text.GetContentWidth();
      int contentHeight = this.m_text.GetContentHeight();
      int num2 = CMath.Max(val1, contentHeight);
      this.m_checkedImgPosX = (int) ((long) (num1 >> 1) - (long) (width1 >> 1));
      this.m_checkedImgPosY = (int) ((long) (num2 >> 1) - (long) (height1 >> 1));
      this.m_uncheckedImgPosX = (int) ((long) (num1 >> 1) - (long) (width2 >> 1));
      this.m_uncheckedImgPosY = (int) ((long) (num2 >> 1) - (long) (height2 >> 1));
      rect.m_x = num1;
      rect.m_y = (num2 >> 1) - (contentHeight >> 1);
      rect.m_dx = this.m_rect.m_dx - num1;
      rect.m_dy = contentHeight;
      this.m_text.SetRect(rect);
      this.m_text.HandleLayout();
      this.m_contentWidth = 0U;
      this.m_contentHeight = (uint) num2;
    }

    public override void Paint()
    {
      ICGraphics2d instance1 = ICGraphics2d.GetInstance();
      CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
      if (this.GetFocus())
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
      else if (!this.GetTransparent())
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        CDrawUtil.FillRect(0, 0, this.m_rect.m_dx, this.m_rect.m_dy, this.m_backgroundColor);
      }
      ICRenderSurface surface;
      int v1;
      int v2;
      if (this.m_checked)
      {
        surface = this.m_pCheckedImg;
        v1 = this.m_checkedImgPosX;
        v2 = this.m_checkedImgPosY;
      }
      else
      {
        surface = this.m_pUncheckedImg;
        v1 = this.m_uncheckedImgPosX;
        v2 = this.m_uncheckedImgPosY;
      }
      if (surface != null)
      {
        this.ConsiderAdvancing2dGraphicsLayer(instance2);
        instance1.PushTransform();
        instance1.Translate(CMathFixed.Int32ToFixed(v1), CMathFixed.Int32ToFixed(v2));
        instance1.Draw(surface);
        instance1.PopTransform();
      }
      this.m_text.SetDirty();
      this.m_text.HandleRender();
    }

    public override bool OnInputEvent(uint id, uint param1, uint param2)
    {
      bool flag = false;
      switch (id)
      {
        case 1913978637:
          this.m_checked = true;
          flag = true;
          break;
        case 2535498699:
          this.m_checked = false;
          flag = true;
          break;
      }
      if (flag)
        this.OnSetDirty();
      return flag;
    }

    public override bool OnSetFocus(bool focus)
    {
      base.OnSetFocus(focus);
      this.m_text.SetFocus(focus);
      return true;
    }

    public override bool OnSetSelection(bool selection)
    {
      base.OnSetSelection(selection);
      this.m_text.SetSelection(selection);
      return true;
    }

    public enum eSelectItemWidgetImagePosition
    {
      SELECTITEM_POSITION_LEFT,
      SELECTITEM_POSITION_RIGHT,
    }
  }
}
