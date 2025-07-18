// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CGenUtil
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public static class CGenUtil
  {
    public static uint HexAToI(string szHex)
    {
      uint num1 = 0;
      foreach (char ch in szHex)
      {
        byte num2;
        if (ch >= '0' && ch <= '9')
          num2 = (byte) ((uint) ch - 48U);
        else if (ch == 'a' || ch == 'A')
          num2 = (byte) 10;
        else if (ch == 'b' || ch == 'B')
          num2 = (byte) 11;
        else if (ch == 'c' || ch == 'C')
          num2 = (byte) 12;
        else if (ch == 'd' || ch == 'D')
          num2 = (byte) 13;
        else if (ch == 'e' || ch == 'E')
          num2 = (byte) 14;
        else if (ch == 'f' || ch == 'F')
          num2 = (byte) 15;
        else
          break;
        num1 = num1 << 4 | (uint) num2;
      }
      return num1;
    }

    public static int BinarySearch(int[] pArray, int size, int value)
    {
      int num1 = -1;
      if (pArray != null)
      {
        int num2 = 0;
        int num3 = size - 1;
        while (num2 <= num3)
        {
          int index = num2 + num3 >> 1;
          if (value == pArray[index])
          {
            num1 = index;
            break;
          }
          if (value < pArray[index])
            num3 = index - 1;
          else
            num2 = index + 1;
        }
      }
      return num1;
    }

    public static int AnchorAlign(int anchor, int itemSize, uint align)
    {
      return anchor - CMathFixed.Mul(itemSize, (int) align);
    }

    public static int AlignedPosition(
      int containerStart,
      int containerEnd,
      int itemSize,
      uint anchorPointAlign,
      uint itemAlign)
    {
      return CGenUtil.AnchorAlign(CMathFixed.Lerp(containerStart, containerEnd, (int) anchorPointAlign), itemSize, itemAlign);
    }

    public static int AlignedPosition(
      CRectangle rect,
      int itemSize,
      uint anchorPointAlign,
      uint itemAlign)
    {
      return CGenUtil.AnchorAlign(CMathFixed.Lerp(rect.m_x, rect.m_x + rect.m_dx, (int) anchorPointAlign), itemSize, itemAlign);
    }

    public static void ClipRegionToRegion(int start1, int size1, ref int start2, ref int size2)
    {
      if (start1 + size1 <= start2 || start1 >= start2 + size2)
        return;
      int num = size2;
      if (start2 + size2 > start1 + size1)
        size2 = start1 + size1 - start2;
      if (start2 < start1)
      {
        size2 -= start1 - start2;
        start2 = start1;
      }
      if ((num <= 0 || size2 >= 0) && (num >= 0 || size2 <= 0))
        return;
      size2 = 0;
    }
  }
}
