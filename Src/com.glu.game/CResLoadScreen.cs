// Decompiled with JetBrains decompiler
// Type: com.glu.game.CResLoadScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CResLoadScreen : CProgressScreen
  {
    protected int m_loadDelayMS;
    protected int m_timeElapsedMS;
    protected bool m_isLoading;
    protected bool m_error;
    protected CResBank m_pBank;

    public bool GetError() => this.m_error;

    public CResLoadScreen()
    {
      this.m_loadDelayMS = 0;
      this.m_timeElapsedMS = 0;
      this.m_isLoading = false;
      this.m_error = false;
      this.m_pBank = (CResBank) null;
      this.SetMovie("GLU_MOVIE_LOADING");
    }

    public void SetResInfo(CResBank pBank, string keysetId, int loadDelayMS)
    {
      this.m_pBank = pBank;
      if (this.m_pBank != null)
        this.m_pBank.Create(keysetId);
      this.m_loadDelayMS = loadDelayMS;
    }

    public override uint Start()
    {
      uint num = base.Start();
      this.m_progress.SetVisible(false);
      return num;
    }

    public override void Stop() => base.Stop();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (id == 544526345U)
      {
        this.m_timeElapsedMS = this.m_loadDelayMS;
        flag = true;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      if (this.m_isLoading)
      {
        if (this.m_pBank != null)
        {
          this.m_pBank.HandleUpdate(timeElapsedMS);
          if (this.m_pBank.GetState() == CResBank.eResBankState.BANK_LOAD_FAILURE)
          {
            this.m_bProgressCompleted = true;
            this.m_error = true;
          }
          else
          {
            this.m_progressPercent += 5;
            this.m_bProgressCompleted = (int) this.m_pBank.GetProgressPercent() >= 100;
          }
        }
      }
      else
      {
        this.m_timeElapsedMS += timeElapsedMS;
        if (this.m_timeElapsedMS >= this.m_loadDelayMS)
        {
          this.m_isLoading = true;
          this.m_progress.SetVisible(true);
        }
      }
      return true;
    }
  }
}
