// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.CStringToKey
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

#nullable disable
namespace com.glu.resgen_content
{
  public struct CStringToKey
  {
    public uint m_key;

    public CStringToKey(string str) => this.m_key = CStringToKey.HashBytes(str);

    public CStringToKey(string str, bool forceLower)
    {
      if (forceLower)
        this.m_key = CStringToKey.HashBytesICase(str);
      else
        this.m_key = CStringToKey.HashBytes(str);
    }

    public static uint HashBytes(string str)
    {
      uint num = (uint) str.Length;
      foreach (char ch in str)
        num = num << 4 ^ num >> 28 ^ (uint) ch;
      return num;
    }

    public static uint HashBytesICase(string str)
    {
      uint num1 = (uint) str.Length;
      foreach (uint num2 in str)
      {
        uint num3 = num2;
        if (num3 >= 65U && num3 <= 90U)
          num3 += 32U;
        num1 = num1 << 4 ^ num1 >> 28 ^ num3;
      }
      return num1;
    }

    public static uint Call(string str) => CStringToKey.HashBytes(str);

    public static uint Call(string str, bool forceLower)
    {
      return forceLower ? CStringToKey.HashBytesICase(str) : CStringToKey.HashBytes(str);
    }

    public static explicit operator uint(CStringToKey key) => key.m_key;
  }
}
