// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CExecutable
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CExecutable : CPriorityClass
  {
    public const uint InfinitelyRunning = 4294967295;
    public const uint OneShot = 1;
    protected uint m_runsRemaining;
    protected int m_updateInterval;
    protected int m_elapsedUpdateTime;
    protected bool m_isCurrentlyRunning;
    protected bool m_isSetToDeleteOnTerminalRun;

    public CExecutable()
    {
      this.m_runsRemaining = uint.MaxValue;
      this.m_updateInterval = 0;
      this.m_elapsedUpdateTime = 0;
      this.m_isCurrentlyRunning = false;
      this.m_isSetToDeleteOnTerminalRun = false;
    }

    public CExecutable.RunResult Run()
    {
      if (this.m_runsRemaining != 0U)
      {
        CAppExecutor executor = CApp.GetExecutor();
        this.m_isCurrentlyRunning = true;
        if (executor.GetMode() == CAppExecutor.Mode.Update)
        {
          this.m_elapsedUpdateTime += executor.GetElapsedUpdateTime();
          if (this.m_elapsedUpdateTime >= this.m_updateInterval)
          {
            this.OnExecute();
            this.AdjustRemainingRuns();
            this.m_elapsedUpdateTime = 0;
          }
        }
        else
        {
          this.OnExecute();
          this.AdjustRemainingRuns();
        }
        this.m_isCurrentlyRunning = false;
      }
      if (this.IsSetToDeleteOnTerminalRun() && this.DeleteIfRunIsTerminal())
        return CExecutable.RunResult.TerminalAndExecutableWasDeleted;
      return this.m_runsRemaining <= 0U ? CExecutable.RunResult.Terminal : CExecutable.RunResult.Active;
    }

    public void SetRunFrequency(uint numOfOccurrences) => this.m_runsRemaining = numOfOccurrences;

    public uint GetNumberOfRemainingRuns() => this.m_runsRemaining;

    public void SetToDeleteOnTerminalRun(bool deleteOnTerminalRun)
    {
      this.m_isSetToDeleteOnTerminalRun = deleteOnTerminalRun;
    }

    public void SetToDeleteOnTerminalRun() => this.SetToDeleteOnTerminalRun(true);

    public bool IsSetToDeleteOnTerminalRun() => this.m_isSetToDeleteOnTerminalRun;

    public void SetUpdateInterval(int time) => this.m_updateInterval = time;

    public int GetUpdateInterval() => this.m_updateInterval;

    public void SetElapsedUpdateTime(int time) => this.m_elapsedUpdateTime = time;

    public int GetElapsedUpdateTime() => this.m_elapsedUpdateTime;

    public bool IsCurrentlyRunning() => this.m_isCurrentlyRunning;

    protected abstract void OnExecute();

    protected void AdjustRemainingRuns()
    {
      if (this.m_runsRemaining == uint.MaxValue || this.m_runsRemaining <= 0U)
        return;
      --this.m_runsRemaining;
    }

    protected bool DeleteIfRunIsTerminal() => this.m_runsRemaining == 0U;

    public enum RunResult
    {
      Active,
      Terminal,
      TerminalAndExecutableWasDeleted,
    }
  }
}
