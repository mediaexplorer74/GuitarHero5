// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRectangle
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public struct CRectangle
  {
    public int m_x;
    public int m_y;
    public int m_dx;
    public int m_dy;

    public CRectangle(int x, int y, int dx, int dy)
    {
      this.m_x = x;
      this.m_y = y;
      this.m_dx = dx;
      this.m_dy = dy;
    }

    public CRectangle(CRectangle r)
    {
      this.m_x = r.m_x;
      this.m_y = r.m_y;
      this.m_dx = r.m_dx;
      this.m_dy = r.m_dy;
    }

    public void Clear() => this.m_x = this.m_y = this.m_dx = this.m_dy = 0;

    public static void Set(out CRectangle rect, int x, int y, int dx, int dy)
    {
      rect.m_x = x;
      rect.m_y = y;
      rect.m_dx = dx;
      rect.m_dy = dy;
    }

    public CRectangle Set(int x, int y, int dx, int dy)
    {
      this.m_x = x;
      this.m_y = y;
      this.m_dx = dx;
      this.m_dy = dy;
      return this;
    }

    public CRectangle Set(CRectangle r)
    {
      this.m_x = r.m_x;
      this.m_y = r.m_y;
      this.m_dx = r.m_dx;
      this.m_dy = r.m_dy;
      return this;
    }

    public CRectangle SetEdges(int left, int top, int right, int bottom)
    {
      this.m_x = left;
      this.m_y = top;
      this.m_dx = right - left;
      this.m_dy = bottom - top;
      return this;
    }

    public int SetTop(int top)
    {
      this.m_dy += this.m_y - top;
      this.m_y = top;
      return top;
    }

    public int SetBottom(int bottom)
    {
      this.m_dy = bottom - this.m_y;
      return bottom;
    }

    public int SetLeft(int left)
    {
      this.m_dx += this.m_x - left;
      this.m_x = left;
      return left;
    }

    public int SetRight(int right)
    {
      this.m_dx = right - this.m_x;
      return right;
    }

    public int GetTop() => this.m_y;

    public int GetBottom() => this.m_y + this.m_dy;

    public int GetLeft() => this.m_x;

    public int GetRight() => this.m_x + this.m_dx;

    public int GetCenterX() => this.m_x + (this.m_dx >> 1);

    public int GetCenterY() => this.m_y + (this.m_dy >> 1);

    public int Area() => this.m_dx * this.m_dy;

    public void CenterX(int dx) => this.m_x = dx - this.m_dx >> 1;

    public void CenterY(int dy) => this.m_y = dy - this.m_dy >> 1;

    public void Center(int dx, int dy)
    {
      this.CenterX(dx);
      this.CenterY(dy);
    }

    public void Move(int dx, int dy)
    {
      this.m_x += dx;
      this.m_y += dy;
    }

    public void MoveUp(int padding) => this.m_y -= this.m_dy + padding;

    public void MoveDown(int padding) => this.m_y += this.m_dy + padding;

    public void MoveLeft(int padding) => this.m_x -= this.m_dx + padding;

    public void MoveRight(int padding) => this.m_x += this.m_dx + padding;

    public void Inset(int inset) => this.Inset(inset, inset, inset, inset);

    public void Inset(int insetX, int insetY) => this.Inset(insetX, insetY, insetX, insetY);

    public void Inset(int insetLeft, int insetTop, int insetRight, int insetBottom)
    {
      this.m_x += insetLeft;
      this.m_y += insetTop;
      this.m_dx = CMath.Max(0, this.m_dx - (insetLeft + insetRight));
      this.m_dy = CMath.Max(0, this.m_dy - (insetTop + insetBottom));
    }

    public void FitToRect(CRectangle r, int padding)
    {
      if (this.m_x + this.m_dx + padding > r.m_x + r.m_dx)
        this.m_x = r.m_x + r.m_dx - (this.m_dx + padding);
      if (this.m_y + this.m_dy + padding > r.m_y + r.m_dy)
        this.m_y = r.m_y + r.m_dy - (this.m_dy + padding);
      if (this.m_x < r.m_x + padding)
        this.m_x = r.m_x + padding;
      if (this.m_y >= r.m_y + padding)
        return;
      this.m_y = r.m_y + padding;
    }

    public void Combine(CRectangle r)
    {
      if (r.m_dx == 0 || r.m_dy == 0)
        return;
      if (this.m_dx == 0 || this.m_dy == 0)
      {
        this.Set(r);
      }
      else
      {
        int num1 = r.m_x >= this.m_x ? this.m_x : r.m_x;
        int num2 = r.m_y >= this.m_y ? this.m_y : r.m_y;
        int num3 = r.m_x + r.m_dx;
        int num4 = r.m_y + r.m_dy;
        int num5 = this.m_x + this.m_dx;
        int num6 = this.m_y + this.m_dy;
        if (num3 > num5)
          num5 = num3;
        if (num4 > num6)
          num6 = num4;
        this.m_x = num1;
        this.m_y = num2;
        this.m_dx = num5 - this.m_x;
        this.m_dy = num6 - this.m_y;
      }
    }

    public void Subtract(CRectangle r)
    {
      if (r.m_x < this.m_x)
      {
        int num = this.m_x - r.m_x;
        this.m_x += num;
        this.m_dx -= num;
      }
      else if (r.m_x > this.m_x)
        this.m_dx -= r.m_dx;
      else if (r.m_y < this.m_y)
      {
        int num = this.m_y - r.m_y;
        this.m_y += num;
        this.m_dy -= num;
      }
      else
      {
        if (r.m_y <= this.m_y)
          return;
        this.m_dy -= r.m_dy;
      }
    }

    public void Clip(CRectangle r)
    {
      CGenUtil.ClipRegionToRegion(r.m_x, r.m_dx, ref this.m_x, ref this.m_dx);
      CGenUtil.ClipRegionToRegion(r.m_y, r.m_dy, ref this.m_y, ref this.m_dy);
    }

    public bool Intersects(CRectangle r)
    {
      return (this.m_x >= r.m_x && r.m_x + r.m_dx >= this.m_x || this.m_x < r.m_x && this.m_x + this.m_dx >= r.m_x) && (this.m_y >= r.m_y && r.m_y + r.m_dy >= this.m_y || this.m_y < r.m_y && this.m_y + this.m_dy >= r.m_y);
    }

    public bool Contains(CRectangle r)
    {
      return this.m_x <= r.m_x && this.m_y <= r.m_y && this.m_y + this.m_dy >= r.m_y + r.m_dy && this.m_x + this.m_dx >= r.m_x + r.m_dx;
    }

    public void CopyFrom(CRectangle r)
    {
      this.m_x = r.m_x;
      this.m_y = r.m_y;
      this.m_dx = r.m_dx;
      this.m_dy = r.m_dy;
    }

    public static bool operator ==(CRectangle l, CRectangle r)
    {
      return (ValueType) l == null ? (ValueType) r == null : l.Equals((object) r);
    }

    public static bool operator !=(CRectangle l, CRectangle r) => !(l == r);

    public override bool Equals(object r)
    {
      return r != null && r is CRectangle crectangle && this.m_x == crectangle.m_x && this.m_y == crectangle.m_y && this.m_dx == crectangle.m_dx && this.m_dy == crectangle.m_dy;
    }

    public override int GetHashCode() => base.GetHashCode();
  }
}
