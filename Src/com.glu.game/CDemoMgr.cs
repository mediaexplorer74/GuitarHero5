// Decompiled with JetBrains decompiler
// Type: com.glu.game.CDemoMgr
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;

#nullable disable
namespace com.glu.game
{
  internal class CDemoMgr : CSingleton
  {
    protected static bool m_enabled;
    protected static int m_playLimit;
    protected static int m_timeLimit;
    protected static int m_gameLimit;
    protected static string m_urlStr;
    protected static string m_menuStr;
    protected static string m_infoStr;
    protected static string m_playExpiredStr;
    protected static string m_timeGameExpiredStr;
    protected static string m_upgradePromptStr;
    protected string m_unavailableStr;
    protected static bool m_started;
    protected static int m_time;
    protected static int m_game;

    public void SetDemoEnabled(bool enable) => CDemoMgr.m_enabled = enable;

    private int GetPlayLimit() => CDemoMgr.m_playLimit;

    private int GetTimeLimit() => CDemoMgr.m_timeLimit;

    private int GetGameLimit() => CDemoMgr.m_gameLimit;

    public CDemoMgr()
    {
      CDemoMgr.m_enabled = false;
      CDemoMgr.m_playLimit = 0;
      CDemoMgr.m_timeLimit = 0;
      CDemoMgr.m_gameLimit = 0;
      CDemoMgr.m_started = false;
      CDemoMgr.m_time = 0;
      CDemoMgr.m_game = 0;
    }

    public static bool Read()
    {
      CDemoMgr.m_enabled = false;//Guide.IsTrialMode;
      CDemoMgr.m_playLimit = 3;
      CDemoMgr.m_timeLimit = 300000;
      CDemoMgr.m_gameLimit = 3;
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_MENU");
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_INFO_TEXT");
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_PLAY_EXPIRED_TEXT");
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_TIME_EXPIRED_TEXT");
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_UPGRADE_PROMPT_TEXT");
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_UNAVAILABLE_TEXT");
      return true;
    }

    public static bool IsDemo() => CDemoMgr.m_enabled;

    public static bool IsPlayStarted() => CDemoMgr.m_enabled && CDemoMgr.m_started;

    public static bool IsPlayExpired()
    {
      if (!CDemoMgr.m_enabled || CDemoMgr.m_playLimit <= 0 || COptionsMgr.GetDemoPlays() < CDemoMgr.m_playLimit)
        return false;
      return !CDemoMgr.m_started || CDemoMgr.IsTimeOrGameExpired();
    }

    public static bool IsTimeOrGameExpired()
    {
      if (!CDemoMgr.m_enabled || !CDemoMgr.m_started)
        return false;
      if (CDemoMgr.m_timeLimit > 0 && CDemoMgr.m_time >= CDemoMgr.m_timeLimit)
        return true;
      return CDemoMgr.m_gameLimit > 0 && CDemoMgr.m_game >= CDemoMgr.m_gameLimit;
    }

    public static bool StartPlay()
    {
      bool flag = false;
      if (CDemoMgr.m_enabled)
      {
        if (CDemoMgr.m_started)
          flag = !CDemoMgr.IsTimeOrGameExpired();
        else if (!CDemoMgr.IsPlayExpired())
        {
          CDemoMgr.AddPlay();
          CDemoMgr.m_started = true;
          flag = true;
        }
      }
      else
        flag = true;
      return flag;
    }

    public static bool LaunchUpgrade()
    {
      bool flag = false;
      //if (Guide.IsTrialMode)
      //  Guide.ShowMarketplace(PlayerIndex.One);
      return flag;
    }

    public static bool LaunchUpSell() => false;

    public static void AddTime(int value) => CDemoMgr.m_time += value;

    public static void AddGame(int value) => CDemoMgr.m_game += value;

    public static string GetMenu()
    {
      CUtility.GetString(out CDemoMgr.m_menuStr, "IDS_DEMO_MENU");
      return CDemoMgr.m_menuStr;
    }

    public static string GetInfoText()
    {
      CUtility.GetString(out CDemoMgr.m_infoStr, "IDS_DEMO_INFO_TEXT");
      return CDemoMgr.m_infoStr;
    }

    public static string GetPlayExpiredText()
    {
      CUtility.GetString(out CDemoMgr.m_playExpiredStr, "IDS_DEMO_PLAY_EXPIRED_TEXT");
      return CDemoMgr.m_playExpiredStr;
    }

    public static string GetTimeOrGameExpiredText()
    {
      CUtility.GetString(out CDemoMgr.m_timeGameExpiredStr, "IDS_DEMO_TIME_EXPIRED_TEXT");
      return CDemoMgr.m_timeGameExpiredStr;
    }

    public static string GetUpgradePromptText()
    {
      CUtility.GetString(out CDemoMgr.m_upgradePromptStr, "IDS_DEMO_UPGRADE_PROMPT_TEXT");
      return CDemoMgr.m_upgradePromptStr;
    }

    private string GetUnavailableText()
    {
      CUtility.GetString(out this.m_unavailableStr, "IDS_DEMO_UNAVAILABLE_TEXT");
      return this.m_unavailableStr;
    }

    public static void AddPlay()
    {
      COptionsMgr.SetDemoPlays(COptionsMgr.GetDemoPlays() + 1);
      COptionsMgr.Write();
    }
  }
}
