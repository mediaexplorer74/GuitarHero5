// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CGameScreen : CSoftkeyScreen
  {
    protected CNode m_pGame;

    public void SetGameNode(CNode pGame) => this.m_pGame = pGame;

    public CGameScreen() => this.m_pGame = (CNode) null;

    public override uint Start()
    {
      int num1 = (int) base.Start();
      if (CGameData.GetContinueGame())
        this.LoadGame();
      int num2 = (int) this.m_pGame.Start();
      return 0;
    }

    public override void Stop()
    {
      base.Stop();
      this.m_pGame.Stop();
    }

    public override void Activate()
    {
      base.Activate();
      CUtility.RegisterGameKeys();
      if (this.m_pGame == null)
        return;
      this.m_pGame.Activate();
    }

    public override void Deactivate()
    {
      base.Deactivate();
      CUtility.UnregisterGameKeys();
      if (this.m_pGame == null)
        return;
      this.m_pGame.Deactivate();
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      base.HandleEvent(id, param1, param2);
      bool flag;
      switch (id)
      {
        case 607208024:
        case 1368267323:
        case 1732285487:
        case 1967276899:
          flag = true;
          break;
        case 1364371259:
        case 1411673571:
        case 2215179113:
          this.SetInterrupt(2);
          flag = true;
          break;
        case 1912541268:
        case 3563016926:
          this.SetInterrupt(2);
          flag = true;
          break;
        default:
          flag = this.m_pGame.HandleEvent(id, param1, param2);
          if (!flag)
          {
            flag = base.HandleEvent(id, param1, param2);
            break;
          }
          break;
      }
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      this.m_pGame.HandleUpdate(timeElapsedMS);
      if (((CGuitarHeroGame) this.m_pGame).GamePlayFinished())
        this.SetInterrupt(1);
      return true;
    }

    public override bool HandleRender()
    {
      this.m_pGame.HandleRender();
      this.m_base.SetDirty();
      base.HandleRender();
      return true;
    }

    public bool SaveGame()
    {
      CSaveGameMgr instance = CSaveGameMgr.GetInstance();
      instance.SetSavedGame(true);
      return instance.Write();
    }

    private void LoadGame() => CSaveGameMgr.GetInstance();

    public void ResetGame() => ((CGuitarHeroGame) this.m_pGame).ResetGameplay();

    public override void Layout() => this.m_base.SetTransparent(true);

    public enum eGameScreenInterrupt
    {
      GAMESCREEN_INTERRUPT_NONE,
      GAMESCREEN_INTERRUPT_FINISHED,
      GAMESCREEN_INTERRUPT_PAUSE,
    }
  }
}
