// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CHandle
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public static class CHandle
  {
    public static CHandle.Subtype GetSubtype(uint handle)
    {
      uint subtype = 0;
      for (uint index = 2147483648; ((int) handle & (int) index) == 0 && subtype < 32U; index >>= 1)
        ++subtype;
      return (CHandle.Subtype) subtype;
    }

    public static uint Make(uint key, CHandle.Subtype type)
    {
      return CHandle.MakeWithMagic(key, (uint) type);
    }

    public static bool IsHashKey(uint handle) => CHandle.IsSubtype(handle, CHandle.Subtype.HashKey);

    public static bool IsRuntime(uint handle) => CHandle.IsSubtype(handle, CHandle.Subtype.Runtime);

    private static uint MakeWithMagic(uint h, uint n) => h & CHandle.AndMask(n) | CHandle.OrMask(n);

    private static uint AndMask(uint n) => (uint) ((1 << 31 - (int) n) - 1);

    private static uint OrMask(uint n) => (uint) (1 << 31 - (int) n);

    private static bool IsSubtype(uint h, CHandle.Subtype st) => CHandle.GetSubtype(h) == st;

    public enum Subtype
    {
      HashKey,
      Runtime,
    }
  }
}
