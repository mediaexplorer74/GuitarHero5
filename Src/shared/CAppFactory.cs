// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CAppFactory
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CAppFactory
  {
    protected static CAppFactory m_instance;

    public static CAppFactory Instance => CAppFactory.m_instance;

    public static CAppFactory GetInstance() => CAppFactory.m_instance;

    protected CAppFactory() => CAppFactory.m_instance = this;

    public abstract CAppExecutor CreateExecutor();

    public abstract CResourceManager CreateResourceManager();

    public abstract CRegistry CreateRegistry();
  }
}
