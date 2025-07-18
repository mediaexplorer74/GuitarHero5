// Decompiled with JetBrains decompiler
// Type: com.glu.shared.VTX2D
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class VTX2D
  {
    public const int POS_FXP = 0;
    public const int POS2_FXP = 16;
    public const int TEX_FXP = 12;
    public const int COL_FXP = 8;
    public const int POS_BITS = 16;
    public const int POS2_BITS = 32;
    public const int POS_COMP_NUM = 2;
    public const int POS2_COMP_NUM = 2;
    public const int TEX_COMP_NUM = 2;
    public const int COL_COMP_NUM = 4;

    private enum Type
    {
      VTX2D_NULL,
      VTX2D,
      VTX2D2,
      VTX2D_C,
      VTX2D2_C,
      VTX2D_T,
      VTX2D2_T,
      VTX2D_TC,
      VTX2D2_TC,
    }
  }
}
