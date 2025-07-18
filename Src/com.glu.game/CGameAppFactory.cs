// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameAppFactory
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public sealed class CGameAppFactory : CAppFactory
  {
    public static CGameAppFactory CreateInstance()
    {
      if (CAppFactory.m_instance == null)
        CAppFactory.m_instance = (CAppFactory) new CGameAppFactory();
      return CAppFactory.m_instance as CGameAppFactory;
    }

    public static CGameAppFactory GetInstance()
    {
      if (CAppFactory.m_instance == null)
        CGameAppFactory.CreateInstance();
      return CAppFactory.m_instance as CGameAppFactory;
    }

    public override CAppExecutor CreateExecutor() => new CAppExecutor();

    public override CResourceManager CreateResourceManager()
    {
      return (CResourceManager) new CResourceManager_v3();
    }

    public override CRegistry CreateRegistry() => new CRegistry();

    private CGameAppFactory()
    {
    }
  }
}
