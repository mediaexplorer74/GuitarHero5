// Decompiled with JetBrains decompiler
// Type: com.glu.shared.TouchUtil
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class TouchUtil
  {
    public static int TOUCH_EVENT_GET_X(uint param2) => (int) param2 & 16383;

    public static int TOUCH_EVENT_GET_Y(uint param2) => (int) (param2 >> 14) & 16383;

    public static int TOUCH_EVENT_GET_TAP_COUNT(uint param2) => (int) (param2 >> 28) & 15;

    public static uint TOUCH_EVENT_SET_X(uint param2, int x)
    {
      param2 &= 4294705152U;
      param2 |= (uint) (x & 16383);
      return param2;
    }

    public static uint TOUCH_EVENT_SET_Y(uint param2, int y)
    {
      param2 &= 16383U;
      param2 |= (uint) ((y & 16383) << 14);
      return param2;
    }

    public static uint TOUCH_EVENT_SET_TAP_COUNT(uint param2, int tapCount)
    {
      param2 &= 4026531840U;
      param2 |= (uint) ((tapCount & 15) << 28);
      return param2;
    }
  }
}
