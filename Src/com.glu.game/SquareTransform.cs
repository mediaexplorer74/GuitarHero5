// Decompiled with JetBrains decompiler
// Type: com.glu.game.SquareTransform
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SquareTransform
  {
    public static readonly byte TRANS_NONE = 0;
    public static readonly byte TRANS_MIRROR_ROT180 = 1;
    public static readonly byte TRANS_MIRROR = 2;
    public static readonly byte TRANS_ROT180 = 3;
    public static readonly byte TRANS_MIRROR_ROT270 = 4;
    public static readonly byte TRANS_ROT90 = 5;
    public static readonly byte TRANS_ROT270 = 6;
    public static readonly byte TRANS_MIRROR_ROT90 = 7;
    public static readonly byte TRANS_COUNT = 8;
    public static readonly int SHIFT_FLIP_Y = 0;
    public static readonly int SHIFT_FLIP_X = 1;
    public static readonly int SHIFT_TRANSPOSE = 2;
    public static readonly int MASK_FLIP_Y = 1 << SquareTransform.SHIFT_FLIP_Y;
    public static readonly int MASK_FLIP_X = 1 << SquareTransform.SHIFT_FLIP_X;
    public static readonly int MASK_TRANSPOSE = 1 << SquareTransform.SHIFT_TRANSPOSE;
    public static readonly byte BITFIELD_EMPTY = 0;

    public static byte Compose(byte second, byte first)
    {
      return (byte) ((uint) first ^ (uint) ((int) second & 4 | (int) second << ((int) first >> 2) & 2 | (int) second >> ((int) first >> 2) & 1));
    }

    public static byte RemoveTransposeIfPresent(byte transform) => (byte) ((uint) transform % 4U);

    public static bool BitFieldContainsTransform(byte bitField, byte transform)
    {
      return ((int) bitField & 1 << (int) transform) != 0;
    }

    public static bool BitFieldContainsAnyDimensionSwitchingTransform(byte bitField)
    {
      return ((int) bitField & 240) != 0;
    }

    public static bool BitFieldContainsAnyDimensionPreservingTransform(byte bitField)
    {
      return ((int) bitField & 15) != 0;
    }

    public static bool IsDimensionSwitching(byte transform)
    {
      return (int) transform >= (int) SquareTransform.TRANS_MIRROR_ROT270;
    }

    public static bool IsDimensionPreserving(byte transform)
    {
      return (int) transform < (int) SquareTransform.TRANS_MIRROR_ROT270;
    }

    public static bool OrientationsAreSame(byte transform0, byte transform1)
    {
      return !SquareTransform.OrientationsAreOpposite(transform0, transform1);
    }

    public static bool OrientationsAreOpposite(byte transform0, byte transform1)
    {
      return (((int) transform0 ^ (int) transform1) & SquareTransform.MASK_TRANSPOSE) != 0;
    }

    public static uint FlipsY(byte transform)
    {
      return (uint) (((int) transform & SquareTransform.MASK_FLIP_Y) >> SquareTransform.SHIFT_FLIP_Y);
    }

    public static uint FlipsX(byte transform)
    {
      return (uint) (((int) transform & SquareTransform.MASK_FLIP_X) >> SquareTransform.SHIFT_FLIP_X);
    }

    public static void TransformRectangle(
      byte transform,
      ref int x,
      ref int y,
      ref uint w,
      ref uint h)
    {
      if (((int) transform & SquareTransform.MASK_FLIP_X) != 0)
        x = (int) -((long) x + (long) w);
      if (((int) transform & SquareTransform.MASK_FLIP_Y) != 0)
        y = (int) -((long) y + (long) h);
      if (((int) transform & SquareTransform.MASK_TRANSPOSE) == 0)
        return;
      int num1 = x;
      x = y;
      y = num1;
      uint num2 = w;
      w = h;
      h = num2;
    }

    public static void TransformPoint(byte transform, ref int x, ref int y)
    {
      if (((int) transform & SquareTransform.MASK_FLIP_X) != 0)
        x = -x;
      if (((int) transform & SquareTransform.MASK_FLIP_Y) != 0)
        y = -y;
      if (((int) transform & SquareTransform.MASK_TRANSPOSE) == 0)
        return;
      int num = x;
      x = y;
      y = num;
    }

    public static void ActivateBitFieldWithTransform(ref byte bitField, byte transform)
    {
      bitField |= (byte) (1U << (int) transform);
    }

    public static void DeactivateBitFieldWithTransform(ref byte bitField, byte transform)
    {
      bitField &= (byte) ~(1 << (int) transform);
    }
  }
}
