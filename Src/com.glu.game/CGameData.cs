// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameData
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGameData : CSingleton
  {
    private static uint ClassId_CGameData = 397485814;
    public static CResBank m_pPreloadBank;
    public static bool m_dontRender;
    public static bool m_optionsActive;
    public static bool m_continueGame;
    public static int m_score;

    public static void SetPreloadBank(CResBank pBank) => CGameData.m_pPreloadBank = pBank;

    public static void SetDontRender(bool dontRender) => CGameData.m_dontRender = dontRender;

    public static void SetOptionsActive(bool optionsActive)
    {
      CGameData.m_optionsActive = optionsActive;
    }

    private void SetContinueGame(bool continueGame) => CGameData.m_continueGame = continueGame;

    public static void SetScore(int score) => CGameData.m_score = score;

    public static CResBank GetPreloadBank() => CGameData.m_pPreloadBank;

    private bool GetDontRender() => CGameData.m_dontRender;

    private bool GetOptionsActive() => CGameData.m_optionsActive;

    public static bool GetContinueGame() => CGameData.m_continueGame;

    private int GetScore() => CGameData.m_score;

    public CGameData()
      : base(CGameData.ClassId_CGameData)
    {
      CGameData.m_dontRender = false;
      CGameData.m_optionsActive = false;
      CGameData.m_continueGame = false;
      CGameData.m_score = 0;
    }
  }
}
