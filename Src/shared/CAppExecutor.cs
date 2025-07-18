// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CAppExecutor
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CAppExecutor : CPriorityClass
  {
    public const uint ClassId = 775705846;
    public const int DefaultMaxUpdateTimeMillis = 1000;
    private CAppExecutor.Mode m_mode;
    private CAppExecutor.ModeQualifier m_modeQualifier;
    private CExecutableRegistry m_registry;
    private int m_maxElapsedUpdateTimeMillis;
    private int m_elapsedUpdateTimeMillis;
    private int m_elapsedUpdateTime;
    private double m_lastUpdateTimeMillis;
    private CAppExecutor.OnExecute m_onExecute;
    private CAppExecutor.ReleaseRegistry m_releaseRegistry;

    public CAppExecutor()
    {
      this.m_classId = 775705846U;
      this.m_mode = CAppExecutor.Mode.Update;
      this.m_modeQualifier = CAppExecutor.ModeQualifier.Starting;
      this.m_registry = (CExecutableRegistry) null;
      this.m_maxElapsedUpdateTimeMillis = 1000;
      this.m_elapsedUpdateTimeMillis = 0;
      this.m_elapsedUpdateTime = 0;
      this.m_lastUpdateTimeMillis = 0.0;
      this.m_onExecute = (CAppExecutor.OnExecute) null;
      this.m_releaseRegistry = (CAppExecutor.ReleaseRegistry) null;
    }

    public bool Init(
      CAppExecutor.OnExecute onExecute,
      CAppExecutor.InitRegistry initRegistry,
      CAppExecutor.ReleaseRegistry releaseRegistry)
    {
      this.m_onExecute = onExecute;
      this.m_registry = new CExecutableRegistry();
      if (this.m_registry != null)
      {
        if (initRegistry != null && initRegistry() == 0U)
          return true;
        this.m_releaseRegistry = releaseRegistry;
      }
      return false;
    }

    public void Destroy()
    {
      if (this.m_releaseRegistry != null)
      {
        this.m_releaseRegistry();
        this.m_releaseRegistry = (CAppExecutor.ReleaseRegistry) null;
      }
      this.m_registry = (CExecutableRegistry) null;
    }

    public void Run()
    {
      double num = 0.0;
      CAppExecutor.Mode mode = this.m_mode;
      if (mode == CAppExecutor.Mode.Update)
      {
        num = CApplet.GetInstance().m_gameTime_xna.TotalGameTime.TotalMilliseconds;
        this.m_elapsedUpdateTimeMillis = CMath.Min((int) (num - this.GetLastUpdateTimeMillis()), this.m_maxElapsedUpdateTimeMillis);
        this.m_elapsedUpdateTimeMillis = CMath.Max(this.m_elapsedUpdateTimeMillis, 10);
        this.m_elapsedUpdateTime = CMathFixed.Int32ToFixed(this.m_elapsedUpdateTimeMillis) / 1000;
      }
      this.m_onExecute();
      if (mode != CAppExecutor.Mode.Update)
        return;
      this.m_lastUpdateTimeMillis = num;
    }

    public void OnStart()
    {
      this.SetMode(CAppExecutor.Mode.Update);
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Starting);
      this.SetLastUpdateTimeToNow();
      this.Run();
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Normal);
    }

    public void OnStop()
    {
      this.SetMode(CAppExecutor.Mode.Update);
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Stopping);
      this.Run();
    }

    public void OnSuspend()
    {
      this.SetMode(CAppExecutor.Mode.Update);
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Suspending);
      this.Run();
    }

    public void OnResume()
    {
      this.SetMode(CAppExecutor.Mode.Render);
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Resuming);
      this.SetLastUpdateTimeToNow();
      this.Run();
      this.SetModeQualifier(CAppExecutor.ModeQualifier.Normal);
    }

    public CAppExecutor.Mode GetMode() => this.m_mode;

    public CAppExecutor.ModeQualifier GetModeQualifier() => this.m_modeQualifier;

    public CExecutableRegistry GetRegistry() => this.m_registry;

    public void SetMaxElapsedUpdateTimeMillis(int time) => this.m_maxElapsedUpdateTimeMillis = time;

    public int GetMaxElapsedUpdateTimeMillis() => this.m_maxElapsedUpdateTimeMillis;

    public int GetElapsedUpdateTimeMillis() => this.m_elapsedUpdateTimeMillis;

    public int GetElapsedUpdateTime() => this.m_elapsedUpdateTime;

    public double GetLastUpdateTimeMillis() => this.m_lastUpdateTimeMillis;

    public void SetMode(CAppExecutor.Mode mode) => this.m_mode = mode;

    public void SetModeQualifier(CAppExecutor.ModeQualifier qualifier)
    {
      this.m_modeQualifier = qualifier;
    }

    private void SetLastUpdateTimeToNow()
    {
      this.m_lastUpdateTimeMillis = CApplet.GetInstance().m_gameTime_xna.TotalGameTime.TotalMilliseconds;
    }

    public enum Mode
    {
      Update,
      Render,
    }

    public enum ModeQualifier
    {
      Normal,
      Starting,
      Stopping,
      Suspending,
      Resuming,
    }

    public delegate uint InitRegistry();

    public delegate void ReleaseRegistry();

    public delegate void OnExecute();
  }
}
