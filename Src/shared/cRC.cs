// Decompiled with JetBrains decompiler
// Type: com.glu.shared.cRC
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class cRC
  {
    public const uint RC_SUCCESS = 0;
    public const uint RC_FAILURE = 1;
    public const uint RC_OUT_OF_MEMORY = 2;
    public const uint RC_BAD_PARAMS = 3;
    public const uint RC_INVALID_STATE = 4;
    public const uint RC_OUT_OF_BOUNDS = 5;
    public const uint RC_NOT_FOUND = 6;
    public const uint RC_EXISTS = 7;
    public const uint RC_EOF = 8;
    public const uint RC_APPLICATION = 160;

    public static string ToString(uint rc)
    {
      switch (rc)
      {
        case 0:
          return "RC_SUCCESS";
        case 1:
          return "RC_FAILURE";
        case 2:
          return "RC_OUT_OF_MEMORY";
        case 3:
          return "RC_BAD_PARAMS";
        case 4:
          return "RC_INVALID_STATE";
        case 5:
          return "RC_OUT_OF_BOUNDS";
        case 6:
          return "RC_NOT_FOUND";
        case 7:
          return "RC_EXISTS";
        case 8:
          return "RC_EOF";
        default:
          return "RC_APPLICATION or Unknown";
      }
    }
  }
}
