// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CBitMath
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CBitMath
  {
    public static void SET_BIT(ref int vector, int bit) => vector |= 1 << bit;

    public static bool TEST_MASK(int vector, int bits) => (vector & bits) == bits;

    public static void SET_MASK(ref int vector, int bits) => vector |= bits;

    public static void CLEAR_MASK(ref int vector, int bits) => vector &= ~bits;
  }
}
