// Decompiled with JetBrains decompiler
// Type: com.glu.shared.MouseUtil
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class MouseUtil
  {
    public static int MOUSE_EVENT_GET_X(uint param2) => (int) param2 & (int) ushort.MaxValue;

    public static int MOUSE_EVENT_GET_Y(uint param2)
    {
      return (int) (param2 >> 16) & (int) ushort.MaxValue;
    }

    public static uint MOUSE_EVENT_SET_X(uint param2, int x)
    {
      param2 &= 4294901760U;
      param2 |= (uint) (x & (int) ushort.MaxValue);
      return param2;
    }

    public static uint MOUSE_EVENT_SET_Y(uint param2, int y)
    {
      param2 &= (uint) ushort.MaxValue;
      param2 |= (uint) ((y & (int) ushort.MaxValue) << 16);
      return param2;
    }
  }
}
