// Decompiled with JetBrains decompiler
// Type: com.glu.game.LiveLeaderBoards
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using System;

#nullable disable
namespace com.glu.game
{
  public class LiveLeaderBoards
  {
    public const float LEADERBOARDS_SLIDE_SPEED = 1.5f;
    public const int LEADERBOARDS_AWARD_ICON_SIZE = 64;
    public const int LEADERBOARDS_TITLE_VPAD = 12;
    public const int LEADERBOARDS_TEXT_VPAD = 11;
    public const int LEADERBOARDS_TITLE_HPAD = 10;
    public const int LEADERBOARDS_AWARD_VPAD = 20;
    public const int LEADERBOARDS_AWARD_HPAD = 33;
    public const int LEADERBOARDS_AWARD_TEXT_VPAD = 2;
    public const int LEADERBOARDS_BACK_OFFSETX = -1;
    public const int LEADERBOARDS_BACK_OFFSETY = -40;
    public const int LEADERBOARDS_BACK_SIZE = 25;
    public const int LEADERBOARDS_LR_ARROW_HPAD = 100;
    public const int BORDER_WIDTH = 110;
    public const int RANK_NAME_SPACING_WIDTH = 3;
    public const int LEADERBOARDS_AWARD_TRANSITION_TIME = 500;
    public const float LEADERBOARDS_DISTANCE_SCALE_FACTOR = 8f;
    public const float LEADERBOARDS_TEXT_BG_RATIO = 0.7f;
    public const int LEADERBOARDS_POINTER_DELAY = 250;
    public const int NETWORK_TIMEOUT_TIME = 2000;
    private const int PAGE_ARROW_TOUCH_HALF_WIDTH = 200;
    private const int PAGE_ARROW_TOUCH_HEIGHT = 30;
    private const int PAGE_DOWN_ARROW_PADDING = 10;
    private const int PAGE_UP_ARROW_PADDING = 10;
    public string[] dummyNames = new string[24]
    {
      "WWWWWWWWWWWWWWW",
      "Name: 0000002",
      "Name: 0000003",
      "Name: 0000004",
      "Name: 0000005",
      "Name: 0000006",
      "Name: 0000007",
      "Name: 0000008",
      "Name: 0000009",
      "Name: 0000010",
      "Name: 0000011",
      "Name: 0000012",
      "Name: 0000013",
      "Name: 0000014",
      "Name: 0000015",
      "Name: 0000016",
      "Name: 0000017",
      "Name: 0000018",
      "Name: 0000019",
      "Name: 0000020",
      "Name: 0000021",
      "Name: 0000022",
      "Name: 0000023",
      "Name: 0000024"
    };
    public int[] dummyDistances = new int[24]
    {
      24,
      23,
      22,
      21,
      20,
      19,
      18,
      17,
      16,
      15,
      14,
      13,
      12,
      11,
      10,
      9,
      8,
      7,
      6,
      5,
      4,
      3,
      2,
      1
    };
    public int[] dummyTimes = new int[24]
    {
      2400100,
      2400150,
      2400200,
      2400250,
      2400300,
      2400350,
      2400400,
      2400450,
      2400500,
      2400550,
      2400600,
      2400650,
      2400700,
      2400750,
      2400800,
      2400850,
      2400900,
      2400950,
      2401000,
      2401050,
      2401100,
      2401150,
      2401200,
      2401250
    };
    public string[] m_leaderboard_title_IDS = new string[20]
    {
      "IDS_BEST_STAT_TIME",
      "IDS_BEST_STAT_DISTANCE",
      "IDS_TRIP1_1_TITLE",
      "IDS_TRIP1_2_TITLE",
      "IDS_TRIP1_3_TITLE",
      "IDS_TRIP2_1_TITLE",
      "IDS_TRIP2_2_TITLE",
      "IDS_TRIP2_3_TITLE",
      "IDS_TRIP3_1_TITLE",
      "IDS_TRIP3_2_TITLE",
      "IDS_TRIP3_3_TITLE",
      "IDS_TRIP4_1_TITLE",
      "IDS_TRIP4_2_TITLE",
      "IDS_TRIP4_3_TITLE",
      "IDS_TRIP5_1_TITLE",
      "IDS_TRIP5_2_TITLE",
      "IDS_TRIP5_3_TITLE",
      "IDS_TRIP6_1_TITLE",
      "IDS_TRIP6_2_TITLE",
      "IDS_TRIP6_3_TITLE"
    };
    private LeaderboardReader m_leaderboardReader;
    private SignedInGamer m_gamer;
    public int m_state;
    public eLeaderBoardState m_networkState;
    private int animation_timer;
    private int animation_count;
    private int networkTimeoutTimer;
    public bool m_gamePause;
    private int m_bgWidth;
    private int m_bgHeight;
    private int m_arrowWidth;
    private int m_arrowHeight;
    private int m_arrowLeftX;
    private int m_arrowRightX;
    private int m_arrowY;
    private int m_backX;
    private int m_backY;
    private int m_titleWidth;
    private int m_titleHeight;
    private int m_entryWidth;
    private int m_entryHeight;
    private int m_rankX;
    private int m_nameX;
    private int m_scoreX;
    private int m_startY;
    private int m_pageArrowHeight;
    private int m_pageArrowWidth;
    private int m_pageArrowDownY;
    private int m_pageArrowUpY;
    private int m_pageArrowX;
    private bool m_pageUpVis = true;
    private bool m_pageDownVis = true;
    private float m_offsetY;
    private bool m_justLoaded;
    private int m_pageNum;
    private int m_itemsPerPage;
    private int m_numTotalPages = 3;
    private int m_pointerDelay;
    private int m_loadingTextWidth;
    private string m_errorText;
    private int curPosition;
    public object leaderboardLockObject = new object();
    public static LiveLeaderBoards m_instance;

    public static LiveLeaderBoards GetInstance()
    {
      if (LiveLeaderBoards.m_instance == null)
        LiveLeaderBoards.m_instance = new LiveLeaderBoards();
      return LiveLeaderBoards.m_instance;
    }

    public void AddLeaderboardEntry(int leaderboard, int rating)
    {
    }

    public static bool IsSignedInToLive()
    {
      SignedInGamer signedInGamer = default;//Gamer.SignedInGamers[PlayerIndex.One];
      return signedInGamer != null && signedInGamer.IsSignedInToLive;
    }

    public bool ReadLeaderboardEntries(
      int leaderboard,
      bool pivotAroundSignedinGamer,
      int numberOfEntriesToRetrieve,
      AsyncCallback LeaderboardCallback)
    {
      try
      {
        SignedInGamer signedInGamer = default;//Gamer.SignedInGamers[PlayerIndex.One];
        if (signedInGamer == null || !signedInGamer.IsSignedInToLive)
          return false;
        //LeaderboardReader.BeginRead(LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, leaderboard),
        //    (Gamer) signedInGamer, 100, new AsyncCallback(this.PopulateLeaderboard), (object) signedInGamer);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void PopulateLeaderboard(IAsyncResult result)
    {
      if (!(result.AsyncState is SignedInGamer))
        return;
      lock (this.leaderboardLockObject)
      {
        LeaderboardReader leaderboardReader = (LeaderboardReader) null;
        try
        {
          //leaderboardReader = LeaderboardReader.EndRead(result);
        }
        catch (Exception ex)
        {
          ex.ToString();
        }
        int index = 0;
        if (leaderboardReader != null)
        {
          CShellScene.leaderboardScores = new long[leaderboardReader.Entries.Count];
          CShellScene.leaderboardNames = new string[leaderboardReader.Entries.Count];
          //foreach (LeaderboardEntry entry in leaderboardReader.Entries)
          //{
          //  CShellScene.leaderboardScores[index] = entry.Rating;
          //  CShellScene.leaderboardNames[index] = entry.Gamer.Gamertag;
          //  ++index;
          //}
        }
        CShellScene.leaderboardLoadingDone = true;
      }
    }

    protected void LeaderboardReadCallback(IAsyncResult result)
    {
      if (this.m_networkState != eLeaderBoardState.waitingForLeaderBoard || !(result.AsyncState is SignedInGamer asyncState))
        return;
      if (!asyncState.IsSignedInToLive)
      {
        this.m_networkState = eLeaderBoardState.transitionToError;
      }
      else
      {
        try
        {
          this.m_leaderboardReader = LeaderboardReader.EndRead(result);
          this.m_networkState = eLeaderBoardState.viewBoard;
          this.m_pageUpVis = false;//this.m_leaderboardReader.CanPageUp;
          this.m_pageDownVis = false;//this.m_leaderboardReader.CanPageDown;
        }
        catch (Exception ex)
        {
          this.m_networkState = eLeaderBoardState.transitionToError;
        }
      }
    }

    protected void LeaderboardPageDownCallback(IAsyncResult result)
    {
      if (!(result.AsyncState is SignedInGamer))
        return;
      try
      {
        //this.m_leaderboardReader.EndPageDown(result);
        this.m_networkState = eLeaderBoardState.viewBoard;
        this.m_pageUpVis = false;//this.m_leaderboardReader.CanPageUp;
        this.m_pageDownVis = false;//this.m_leaderboardReader.CanPageDown;
      }
      catch (Exception ex)
      {
        this.m_networkState = eLeaderBoardState.transitionToError;
      }
    }

    protected void LeaderboardPageUpCallback(IAsyncResult result)
    {
      if (!(result.AsyncState is SignedInGamer))
        return;
      try
      {
        //this.m_leaderboardReader.EndPageUp(result);
        this.m_networkState = eLeaderBoardState.viewBoard;
        this.m_pageUpVis = false;//this.m_leaderboardReader.CanPageUp;
        this.m_pageDownVis = false;//this.m_leaderboardReader.CanPageDown;
      }
      catch (Exception ex)
      {
        this.m_networkState = eLeaderBoardState.transitionToError;
      }
    }

    public delegate void leaderboardCallback(IAsyncResult result);
  }
}
