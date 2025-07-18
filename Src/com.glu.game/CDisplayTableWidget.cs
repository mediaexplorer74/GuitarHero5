// Decompiled with JetBrains decompiler
// Type: com.glu.game.CDisplayTableWidget
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public abstract class CDisplayTableWidget : CUIWidget
  {
    protected int m_rows;
    protected int m_cols;
    protected int m_frameWidth;
    protected int m_width;
    protected int m_height;
    protected int[] m_pRowHeight;
    protected int[] m_pColWidth;
    protected int m_lastLayoutWidth;

    public abstract int MeasureRowHeight(int row);

    public abstract int MeasureColumnWidth(int col, int available);

    public abstract void RenderCellBackground(int row, int col, int width, int height);

    public abstract void RenderCellContent(int row, int col, int width, int height);

    public CDisplayTableWidget()
    {
      this.m_rows = 0;
      this.m_cols = 0;
      this.m_frameWidth = 0;
      this.m_attFlags = 7;
      this.m_width = 0;
      this.m_height = 0;
      this.m_pRowHeight = (int[]) null;
      this.m_pColWidth = (int[]) null;
      this.m_lastLayoutWidth = 0;
    }

    public bool SetAttributes(int rows, int cols, int frameWidth, int flags)
    {
      bool flag = false;
      this.m_pRowHeight = (int[]) null;
      this.m_pColWidth = (int[]) null;
      this.m_rows = 0;
      this.m_cols = 0;
      this.m_frameWidth = frameWidth;
      this.m_attFlags = flags;
      if (rows > 0 && cols > 0)
      {
        this.m_pRowHeight = new int[rows];
        this.m_pColWidth = new int[cols];
        if (this.m_pRowHeight != null && this.m_pColWidth != null)
        {
          this.m_rows = rows;
          this.m_cols = cols;
          flag = true;
        }
      }
      this.m_lastLayoutWidth = 0;
      this.OnSetNewLayout();
      return flag;
    }

    public override void Layout()
    {
      if (this.m_lastLayoutWidth == 0 || this.m_lastLayoutWidth != this.m_rect.m_dx)
      {
        this.m_lastLayoutWidth = this.m_rect.m_dx;
        this.m_width = 0;
        this.m_height = 0;
        if (this.m_rows > 0 && this.m_cols > 0)
        {
          this.m_width = this.m_frameWidth;
          this.m_height = this.m_frameWidth;
          int available = this.m_rect.m_dx - this.m_frameWidth * (this.m_cols + 1);
          for (int col = 0; col < this.m_cols; ++col)
          {
            this.m_pColWidth[col] = this.MeasureColumnWidth(col, available);
            this.m_width += this.m_pColWidth[col] + this.m_frameWidth;
            available -= this.m_pColWidth[col];
          }
          for (int row = 0; row < this.m_rows; ++row)
          {
            this.m_pRowHeight[row] = this.MeasureRowHeight(row);
            this.m_height += this.m_pRowHeight[row] + this.m_frameWidth;
          }
        }
      }
      this.m_contentWidth = (uint) this.m_width;
      this.m_contentHeight = (uint) this.m_height;
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
      if (this.m_rows <= 0 || this.m_cols <= 0)
        return;
      int v1 = 0;
      for (int row = 0; row < this.m_rows; ++row)
      {
        int v2 = 0;
        for (int col = 0; col < this.m_cols; ++col)
        {
          instance1.PushTransform();
          this.ConsiderAdvancing2dGraphicsLayer(instance2);
          instance1.Translate(CMathFixed.Int32ToFixed(v2), CMathFixed.Int32ToFixed(v1));
          this.RenderCellBackground(row, col, 2 * this.m_frameWidth + this.m_pColWidth[col], 2 * this.m_frameWidth + this.m_pRowHeight[row]);
          this.ConsiderAdvancing2dGraphicsLayer(instance2);
          CRectangle rect;
          rect.m_x = 0;
          rect.m_y = 0;
          rect.m_dx = 2 * this.m_frameWidth + this.m_pColWidth[col];
          rect.m_dy = 2 * this.m_frameWidth + this.m_pRowHeight[row];
          CDrawUtil.DrawFrame(rect, this.m_foregroundColor, this.m_frameWidth, (CDrawUtil.eDrawFrameFlags) this.GetFrameFlags(row, col));
          this.ConsiderAdvancing2dGraphicsLayer(instance2);
          instance1.Translate(CMathFixed.Int32ToFixed(this.m_frameWidth), CMathFixed.Int32ToFixed(this.m_frameWidth));
          this.RenderCellContent(row, col, this.m_pColWidth[col], this.m_pRowHeight[row]);
          instance1.PopTransform();
          v2 += this.m_frameWidth + this.m_pColWidth[col];
        }
        v1 += this.m_frameWidth + this.m_pRowHeight[row];
      }
    }

    private int GetFrameFlags(int row, int col)
    {
      int frameFlags = 0;
      if (CBitMath.TEST_MASK(this.m_attFlags, 16) && col == 0)
      {
        frameFlags = 7;
        if (!CBitMath.TEST_MASK(this.m_attFlags, 1) || CBitMath.TEST_MASK(this.m_attFlags, 8) && row == 0)
          frameFlags |= 8;
      }
      if (CBitMath.TEST_MASK(this.m_attFlags, 8) && row == 0)
      {
        frameFlags = 13;
        if (!CBitMath.TEST_MASK(this.m_attFlags, 1) || CBitMath.TEST_MASK(this.m_attFlags, 16) && col == 0)
          frameFlags |= 2;
      }
      if (!CBitMath.TEST_MASK(this.m_attFlags, 1))
      {
        if (row == 0)
          frameFlags |= 1;
        if (row == this.m_rows - 1)
          frameFlags |= 2;
        if (col == 0)
          frameFlags |= 4;
        if (col == this.m_cols - 1)
          frameFlags |= 8;
      }
      if (!CBitMath.TEST_MASK(this.m_attFlags, 2))
      {
        if (CBitMath.TEST_MASK(this.m_attFlags, 8))
        {
          if (row > 1)
            frameFlags |= 1;
          if (row > 0 && row < this.m_rows - 1)
            frameFlags |= 2;
        }
        else
        {
          if (row > 0)
            frameFlags |= 1;
          if (row < this.m_rows - 1)
            frameFlags |= 2;
        }
      }
      if (!CBitMath.TEST_MASK(this.m_attFlags, 4))
      {
        if (CBitMath.TEST_MASK(this.m_attFlags, 16))
        {
          if (col > 1)
            frameFlags |= 4;
          if (col > 0 && col < this.m_cols - 1)
            frameFlags |= 8;
        }
        else
        {
          if (col > 0)
            frameFlags |= 4;
          if (col < this.m_cols - 1)
            frameFlags |= 8;
        }
      }
      return frameFlags;
    }

    public enum eDisplayTableWidgetAttFlags
    {
      DISPLAYTABLEWIDGET_ATT_NONE = 0,
      DISPLAYTABLEWIDGET_ATT_BORDER = 1,
      DISPLAYTABLEWIDGET_ATT_ROW_SEP = 2,
      DISPLAYTABLEWIDGET_ATT_COL_SEP = 4,
      DISPLAYTABLEWIDGET_ATT_COL_HEADINGS = 8,
      DISPLAYTABLEWIDGET_ATT_ROW_HEADINGS = 16, // 0x00000010
    }
  }
}
