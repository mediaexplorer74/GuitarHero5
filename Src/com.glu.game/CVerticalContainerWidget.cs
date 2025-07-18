// Decompiled with JetBrains decompiler
// Type: com.glu.game.CVerticalContainerWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CVerticalContainerWidget : CContainerWidget
  {
    protected bool m_needsToScroll;
    protected CSliderWidget m_pSlider;
    protected int m_scrollArrowOffset;
    protected ICRenderSurface m_pUpArrow;
    protected ICRenderSurface m_pDownArrow;
    protected SG_Presenter sg_arrowUp;
    protected SG_Presenter sg_arrowDown;
    protected bool m_bNoArrowUnlessScrollable;
    private CRectangle arrowRect;

    public void SetNoArrowsUnlessScrollable(bool bArrowsOnlyOnScroll)
    {
      this.m_bNoArrowUnlessScrollable = bArrowsOnlyOnScroll;
    }

    public CVerticalContainerWidget()
    {
      this.m_needsToScroll = false;
      this.m_pSlider = (CSliderWidget) null;
      this.m_scrollArrowOffset = 0;
      this.m_pUpArrow = (ICRenderSurface) null;
      this.m_pDownArrow = (ICRenderSurface) null;
      this.sg_arrowUp = (SG_Presenter) null;
      this.sg_arrowDown = (SG_Presenter) null;
      this.m_bNoArrowUnlessScrollable = false;
    }

    public void SetSlider(CSliderWidget pSlider)
    {
      this.m_pSlider = pSlider;
      this.m_pUpArrow = (ICRenderSurface) null;
      this.m_pDownArrow = (ICRenderSurface) null;
      this.OnSetNewLayout();
    }

    public void SetScrollArrows(ICRenderSurface pTopArrow, ICRenderSurface pBottomArrow)
    {
      if (this.sg_arrowUp == null)
      {
        this.sg_arrowUp = new SG_Presenter(3, 0);
        this.sg_arrowUp.SetAnimation(8);
        this.sg_arrowUp.SetPosition(0, 0);
        this.sg_arrowUp.SetLoop(true);
      }
      if (this.sg_arrowDown == null)
      {
        this.sg_arrowDown = new SG_Presenter(3, 0);
        this.sg_arrowDown.SetAnimation(9);
        this.sg_arrowDown.SetPosition(0, 0);
        this.sg_arrowDown.SetLoop(true);
      }
      this.OnSetNewLayout();
    }

    public override void GetDrawOffset(
      CWidget pWidget,
      CRectangle parent,
      ref int offsetX,
      ref int offsetY,
      ref int offsetAccumX,
      ref int offsetAccumY)
    {
      if (this.m_parent != null)
      {
        this.m_parent.GetDrawOffset(pWidget, out parent, ref offsetX, ref offsetY, ref offsetAccumX, ref offsetAccumY);
        offsetX = offsetAccumX;
        offsetY = offsetAccumY;
        offsetAccumX += this.m_rect.m_x;
        offsetAccumY += this.m_rect.m_y;
        if ((CSliderWidget) pWidget == this.m_pSlider)
          return;
        offsetAccumX -= this.m_scrollPositionX;
        offsetAccumY -= this.m_scrollPositionY;
      }
      else
      {
        parent = this.m_rect;
        offsetX = 0;
        offsetY = 0;
        offsetAccumX = this.m_rect.m_x;
        offsetAccumY = this.m_rect.m_y;
      }
    }

    public void GetDownArrowRect(ref CRectangle rect_out)
    {
      if (this.sg_arrowDown == null)
        return;
      this.clip.Clear();
      this.GetClipRect(ref this.clip);
      this.sg_arrowDown.Bounds(ref rect_out);
      rect_out.m_x += this.clip.m_x;
      rect_out.m_y += this.clip.m_y;
    }

    public void GetUpArrowRect(ref CRectangle rect_out)
    {
      if (this.sg_arrowUp == null)
        return;
      this.clip.Clear();
      this.GetClipRect(ref this.clip);
      this.sg_arrowUp.Bounds(ref rect_out);
      rect_out.m_x += this.clip.m_x;
      rect_out.m_y += this.clip.m_y;
    }

    public override void Layout()
    {
      int itemHeight = 0;
      uint height1;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out uint _, out height1);
      this.m_scrollArrowOffset = (int) (height1 / 50U);
      this.m_needsToScroll = false;
      int num1 = 0;
      while (num1 < 2)
      {
        ++num1;
        int contentInsetX = this.m_contentInsetX;
        int contentInsetTopY = this.m_contentInsetTopY;
        if (num1 == 2)
        {
          if (this.m_needsToScroll)
          {
            if (this.m_pUpArrow != null)
            {
              uint height2;
              this.m_pUpArrow.GetWidthAndHeight(out uint _, out height2);
              contentInsetTopY += (int) height2 + this.m_scrollArrowOffset;
            }
          }
          else
            contentInsetTopY += this.GetVerticalAlignedPosition(0, this.m_rect.m_dy, itemHeight);
        }
        CLinkListNode clinkListNode = this.m_childList.GetHead();
        while (clinkListNode != null)
        {
          CUIWidget data = (CUIWidget) clinkListNode.GetData();
          clinkListNode = clinkListNode.GetNext();
          CRectangle rect;
          rect.m_x = contentInsetX;
          rect.m_dx = this.m_rect.m_dx - (this.m_contentInsetX << 1);
          rect.m_y = contentInsetTopY;
          rect.m_dy = this.m_rect.m_dy - this.m_contentInsetTopY - this.m_contentInsetBotY;
          if (this.m_needsToScroll)
          {
            if (this.m_pSlider != null)
              rect.m_dx -= this.m_pSlider.GetWidth();
            else if (this.m_pDownArrow != null)
            {
              this.m_pDownArrow.GetWidthAndHeight(out uint _, out uint _);
              rect.m_dy -= (int) (short) height1 + this.m_scrollArrowOffset;
            }
          }
          data.SetRect(rect);
          int num2 = data.GetContentHeight();
          if (num2 < 0)
            num2 = CMath.Max(0, (int) (short) height1 * 3 / 4);
          rect.m_dy = num2;
          data.SetRect(rect);
          contentInsetTopY += num2;
          if ((this.m_pSlider != null || this.sg_arrowUp != null && this.sg_arrowDown != null) && !this.m_needsToScroll && contentInsetTopY > this.m_rect.m_dy - this.m_contentInsetBotY)
          {
            this.m_needsToScroll = true;
            break;
          }
        }
        itemHeight = contentInsetTopY + this.m_contentInsetBotY;
      }
      if (this.m_pSlider != null)
      {
        this.m_pSlider.SetParent((CWidget) this);
        this.m_pSlider.SetFocusable(false);
        this.m_pSlider.SetSelectable(false);
        if (this.m_needsToScroll)
        {
          CRectangle rect;
          rect.m_x = this.m_rect.m_dx - this.m_pSlider.GetWidth();
          rect.m_dx = this.m_pSlider.GetWidth();
          rect.m_y = this.m_contentInsetTopY;
          rect.m_dy = this.m_rect.m_dy - this.m_contentInsetTopY - this.m_contentInsetBotY;
          this.m_pSlider.SetActive(true);
          this.m_pSlider.SetVisible(true);
          this.m_pSlider.SetRect(rect);
          this.m_pSlider.HandleLayout();
        }
        else
        {
          this.m_pSlider.SetActive(false);
          this.m_pSlider.SetVisible(false);
        }
      }
      CLinkListNode clinkListNode1 = this.m_childList.GetHead();
      while (clinkListNode1 != null)
      {
        CUIWidget data = (CUIWidget) clinkListNode1.GetData();
        clinkListNode1 = clinkListNode1.GetNext();
        data.SetFocus(false);
        data.SetSelection(false);
      }
      if (this.m_pFocus == null)
        this.SetFocusToNextOnScreenFocusableWidget((CUIWidget) null, true);
      else if (this.GetFocus())
      {
        this.m_pFocus.SetFocus(true);
        this.m_pFocus.SetSelection(true);
      }
      this.m_contentWidth = 0U;
      this.m_contentHeight = (uint) itemHeight;
    }

    public override void Paint()
    {
      base.Paint();
      if (this.m_needsToScroll)
      {
        ICGraphics2d instance1 = ICGraphics2d.GetInstance();
        CWidget.G2dDisplayProgramInfo instance2 = CWidget.G2dDisplayProgramInfo.GetInstance();
        if (this.m_pSlider == null)
        {
          int num = 0;
          this.ConsiderAdvancing2dGraphicsLayer(instance2);
          if (this.m_pUpArrow != null)
          {
            uint width;
            uint height;
            this.m_pUpArrow.GetWidthAndHeight(out width, out height);
            num += (int) height + this.m_scrollArrowOffset;
            if (this.m_scrollPositionY > 0)
            {
              instance1.PushTransform();
              instance1.Translate(CMathFixed.Int32ToFixed((int) ((long) this.m_rect.m_dx - (long) width) >> 1), CMathFixed.Int32ToFixed(0));
              instance1.Draw(this.m_pUpArrow);
              instance1.PopTransform();
            }
          }
          if (this.m_pDownArrow != null)
          {
            uint width;
            uint height;
            this.m_pDownArrow.GetWidthAndHeight(out width, out height);
            if ((long) this.m_contentHeight - (long) this.m_scrollPositionY - (long) this.m_rect.m_dy + (long) (num + ((int) height + this.m_scrollArrowOffset)) > 0L)
            {
              instance1.PushTransform();
              instance1.Translate(CMathFixed.Int32ToFixed((int) ((long) this.m_rect.m_dx - (long) width) >> 1), CMathFixed.Int32ToFixed((int) ((long) this.m_rect.m_dy - (long) height - (long) this.m_scrollArrowOffset)));
              instance1.Draw(this.m_pDownArrow);
              instance1.PopTransform();
            }
          }
        }
        else
        {
          this.ConsiderAdvancing2dGraphicsLayer(instance2);
          this.m_pSlider.SetSlider(this.m_scrollPositionY, (int) ((long) this.m_contentHeight - (long) this.m_rect.m_dy), this.m_rect.m_dy);
          this.m_pSlider.GetRect();
          this.m_pSlider.SetDirty();
          this.m_pSlider.HandleRender();
        }
      }
      if (this.sg_arrowUp == null || this.sg_arrowDown == null)
        return;
      ICGraphics2d instance = ICGraphics2d.GetInstance();
      CRectangle rect1;
      rect1.m_x = 0;
      rect1.m_y = 0;
      rect1.m_dx = (int) Phone.GetWidth();
      rect1.m_dy = (int) Phone.GetHeight();
      CRectangle clip = instance.GetClip();
      instance.SetClip(rect1);
      if (this.m_needsToScroll)
      {
        this.sg_arrowUp.Bounds(ref this.arrowRect);
        this.sg_arrowUp.SetPosition((short) (this.m_rect.m_dx / 2), (short) (this.m_rect.m_y - (this.arrowRect.m_dy >> 1)));
        if (!this.m_bNoArrowUnlessScrollable || this.m_scrollPositionY != 0)
          this.sg_arrowUp.Draw();
        this.sg_arrowDown.Bounds(ref this.arrowRect);
        this.sg_arrowDown.SetPosition((short) (this.m_rect.m_dx / 2), (short) (this.m_rect.m_y + this.m_rect.m_dy + (this.arrowRect.m_dy >> 1)));
        if (!this.m_bNoArrowUnlessScrollable || (long) this.m_scrollPositionY < (long) this.m_contentHeight - (long) this.m_rect.m_dy)
          this.sg_arrowDown.Draw();
      }
      else
      {
        CUIWidget pWidget = this.GetFirstChild();
        if (pWidget != null)
        {
          while (!(pWidget is CSelectItemGroupWidget))
          {
            pWidget = this.GetNextChild(pWidget);
            if (pWidget == null)
              break;
          }
        }
        if (pWidget != null)
        {
          this.sg_arrowUp.Bounds(ref this.arrowRect);
          CRectangle rect2 = pWidget.GetRect();
          this.clip.Clear();
          this.GetClipRect(ref this.clip);
          this.sg_arrowUp.SetPosition((short) (this.m_rect.m_dx / 2), (short) (rect2.m_y - (this.arrowRect.m_dy >> 1)));
          if (!this.m_bNoArrowUnlessScrollable || this.m_scrollPositionY != 0)
            this.sg_arrowUp.Draw();
          this.sg_arrowDown.SetPosition((short) (this.m_rect.m_dx / 2), (short) (rect2.m_y + rect2.m_dy + (this.arrowRect.m_dy >> 1)));
          if (!this.m_bNoArrowUnlessScrollable || (long) this.m_scrollPositionY < (long) this.m_contentHeight - (long) this.m_rect.m_dy)
            this.sg_arrowDown.Draw();
        }
      }
      instance.SetClip(clip);
    }

    public override bool OnUpdate(int timeElapsedMS)
    {
      base.OnUpdate(timeElapsedMS);
      if (this.sg_arrowUp != null)
        this.sg_arrowUp.Update(timeElapsedMS);
      if (this.sg_arrowDown != null)
        this.sg_arrowDown.Update(timeElapsedMS);
      return true;
    }
  }
}
