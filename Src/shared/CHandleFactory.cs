// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CHandleFactory
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CHandleFactory : CSingleton
  {
    public const uint ClassId = 436883571;
    private new static CSingleton m_instance;
    private uint m_creationCount;

    public static CHandleFactory GetInstance()
    {
      if (CHandleFactory.m_instance == null)
        CHandleFactory.m_instance = (CSingleton) new CHandleFactory();
      return CHandleFactory.m_instance as CHandleFactory;
    }

    public static uint CreateHashKey(string str)
    {
      return CHandle.Make(CHash.Hash(str), CHandle.Subtype.HashKey);
    }

    public uint CreateRuntime() => CHandle.Make(++this.m_creationCount, CHandle.Subtype.Runtime);

    private CHandleFactory()
      : base(436883571U)
    {
      this.m_creationCount = 0U;
    }
  }
}
