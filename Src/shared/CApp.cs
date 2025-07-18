// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CApp
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CApp : CClass, ICClass
  {
    protected static CApp m_instance = (CApp) null;
    protected static CResourceManager m_resourceManager = (CResourceManager) null;
    protected static CAppExecutor m_executor;
    protected static CRegistry m_registry;
    public CAccelerometer m_pAccelerometer;

    public static CApp Instance => CApp.m_instance;

    public static CApp GetInstance() => CApp.m_instance;

    protected CApp()
      : base(101U)
    {
      CApp.m_instance = this;
    }

    public abstract uint OnInit();

    public static CAppExecutor GetExecutor() => CApp.m_executor;

    public static CResourceManager GetResourceManager() => CApp.m_resourceManager;

    public static CRegistry GetRegistry() => CApp.m_registry;

    public abstract void HandleUpdate();

    public abstract void HandleRender();

    public abstract void OnSystemEvent(uint id, uint param1, uint param2);
  }
}
