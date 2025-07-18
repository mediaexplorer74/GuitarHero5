// Decompiled with JetBrains decompiler
// Type: com.glu.game.CLeaderBoard
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CLeaderBoard : CSingleton
  {
    private static int PREV = -1;
    private static int NEXT = 1;
    protected static string m_pUserName;
    protected static bool m_bProfane;
    protected static bool m_bRegAttemped;
    protected static ushort m_nTables;
    protected static uint m_userID;
    protected static CLeaderBoardTable[] m_tables;
    private static string kwcsEmptyString = "";

    public static uint GetUserID() => CLeaderBoard.m_userID;

    public static string GetUsername() => CLeaderBoard.m_pUserName;

    public static bool IsUserNameProfane() => CLeaderBoard.m_bProfane;

    public bool RegistrationAttempted(bool tried)
    {
      return !tried ? CLeaderBoard.m_bRegAttemped : (CLeaderBoard.m_bRegAttemped = true);
    }

    public static void SetUserID(uint userID) => CLeaderBoard.m_userID = userID;

    public void SetUserNameProfane(bool profane) => CLeaderBoard.m_bProfane = profane;

    public CLeaderBoard()
    {
      CLeaderBoard.m_pUserName = (string) null;
      CLeaderBoard.m_userID = Consts.kDefaultUserId;
      CLeaderBoard.m_tables = (CLeaderBoardTable[]) null;
      CLeaderBoard.m_nTables = (ushort) 0;
      CLeaderBoard.m_bProfane = false;
      CLeaderBoard.m_bRegAttemped = false;
    }

    public static void Empty()
    {
      CLeaderBoard.m_tables = (CLeaderBoardTable[]) null;
      CLeaderBoard.m_nTables = (ushort) 0;
    }

    public static bool IsCareerLeaderboard(ushort tableIndex)
    {
      return (long) (int) CLeaderBoard.m_tables[(int) tableIndex].m_songID == (long) CSongListMgr.GetCareerSongID();
    }

    public static int GetSongID(ushort tableIndex)
    {
      int songId = 0;
      CSong song = CSongListMgr.GetSong(CLeaderBoard.m_tables[(int) tableIndex].m_songID);
      if (song != null)
        songId = (int) song.GetSongID();
      return songId;
    }

    public static ushort PopulateAvailLeaderboard(
      ref ushort curTable,
      int amt,
      ref CLeaderboardScreen.LeaderboardData pData)
    {
      ushort table = 0;
      int index = (int) curTable;
      do
      {
        index += amt;
        if (index >= (int) CLeaderBoard.m_nTables)
          index = 0;
        else if (index < 0)
          index = (int) CLeaderBoard.m_nTables - 1;
        if ((int) CLeaderBoard.m_tables[index].m_songID == (int) CSongListMgr.GetCareerSongID() || CSongListMgr.GetSong(CLeaderBoard.m_tables[index].m_songID) != null)
        {
          table = (ushort) index;
          break;
        }
      }
      while (index != (int) curTable);
      curTable = (ushort) index;
      return CLeaderBoard.PopulateLeaderboard(table, pData);
    }

    public static ushort PopulateCareerLeadboard(CLeaderboardScreen.LeaderboardData pData)
    {
      if (CLeaderBoard.m_tables == null)
        return 0;
      ushort table = 0;
      for (int index = 0; index < (int) CLeaderBoard.m_nTables; ++index)
      {
        if ((int) CLeaderBoard.m_tables[index].m_songID == (int) CSongListMgr.GetCareerSongID())
        {
          table = (ushort) index;
          break;
        }
      }
      return CLeaderBoard.PopulateLeaderboard(table, pData);
    }

    public static ushort PopulateLeaderboard(ushort table, CLeaderboardScreen.LeaderboardData pData)
    {
      CLeaderBoardTable table1 = CLeaderBoard.m_tables[(int) table];
      bool flag1 = (int) table1.m_songID == (int) CSongListMgr.GetCareerSongID();
      string output = (string) null;
      if (flag1)
      {
        CUtility.GetString(out output, "IDS_LEADERBOARD_CAREER");
      }
      else
      {
        CSong song = CSongListMgr.GetSong(table1.m_songID);
        CSongScoreMgr.SongScore songScore = CSongScoreMgr.GetSongScore(table1.m_songID);
        if (song.IsEncore() && !songScore.IsUnlocked())
          CUtility.GetString(out output, "IDS_LOCKED_SONG_TITLE");
        else
          output = song.GetSongName();
      }
      pData.SetSongTitle(output);
      if (!flag1)
      {
        CSongScoreMgr.GetSongScore(table1.m_songID);
        for (int index = 0; index < 3; ++index)
        {
          int num = 0;
          while (num < 3)
            ++num;
        }
      }
      bool flag2 = false;
      for (int index = 0; index < (int) table1.nScores; ++index)
      {
        if ((int) table1.m_scores[index].userID == (int) CLeaderBoard.m_userID && table1.m_scores[index].score > 0U)
        {
          flag2 = true;
          pData.SetUser((int) table1.m_scores[index].rank, table1.m_scores[index].name != null ? table1.m_scores[index].name : CLeaderBoard.kwcsEmptyString, (int) table1.m_scores[index].score);
          break;
        }
      }
      if (!flag2)
        pData.SetUserBlank(CLeaderBoard.GetUsername());
      pData.RemoveAllScores();
      int num1 = CMath.Min((int) table1.nScores, (int) Consts.MAX_LEADERBOARD_HIGHSCORES);
      for (int index = 0; index < num1; ++index)
        pData.AppendHighscore(table1.m_scores[index].name != null ? table1.m_scores[index].name : CLeaderBoard.kwcsEmptyString, (int) table1.m_scores[index].score);
      return table;
    }

    public static ushort PopulateNextLeadboard(
      ref ushort curTable,
      CLeaderboardScreen.LeaderboardData pData)
    {
      return CLeaderBoard.PopulateAvailLeaderboard(ref curTable, CLeaderBoard.NEXT, ref pData);
    }

    public static ushort PopulatePrevLeadboard(
      ref ushort curTable,
      CLeaderboardScreen.LeaderboardData pData)
    {
      return CLeaderBoard.PopulateAvailLeaderboard(ref curTable, CLeaderBoard.PREV, ref pData);
    }

    private bool Read(CArrayInputStream stream)
    {
      bool flag = true;
      if (stream == null)
        return false;
      ushort length = stream.ReadUInt16();
      if (length > (ushort) 0)
      {
        CLeaderBoard.Empty();
        CLeaderBoard.m_tables = new CLeaderBoardTable[(int) length];
        flag &= CLeaderBoard.m_tables != null;
      }
      if (flag && length > (ushort) 0)
      {
        for (int index1 = 0; index1 < (int) length; ++index1)
        {
          CLeaderBoard.m_tables[index1].m_songID = stream.ReadUInt32();
          ushort val1 = stream.ReadUInt16();
          CLeaderBoard.m_tables[index1].nScores = (ushort) CMath.Min((uint) val1, Consts.MAX_LEADERBOARD_TABLE_SCORES);
          for (int index2 = 0; index2 < (int) val1; ++index2)
          {
            if ((long) index2 > (long) Consts.MAX_LEADERBOARD_TABLE_SCORES)
            {
              stream.Skip(8U);
              ushort n = stream.ReadUInt16();
              stream.Skip((uint) n);
              stream.Skip(4U);
            }
            else
            {
              CLeaderBoardTable.CLeaderBoardTableScore score = CLeaderBoard.m_tables[index1].m_scores[index2];
              score.userID = stream.ReadUInt32();
              score.rank = stream.ReadUInt32();
              score.SetName(stream);
              score.score = stream.ReadUInt32();
            }
          }
        }
      }
      CLeaderBoard.m_nTables = length;
      return flag & !stream.GetFail();
    }

    public static void Reset()
    {
      CLeaderBoard.Empty();
      CLeaderBoard.m_userID = Consts.kDefaultUserId;
      CLeaderBoard.m_pUserName = (string) null;
      CLeaderBoard.m_bProfane = false;
      CLeaderBoard.m_bRegAttemped = false;
    }

    private bool RegisterUserResponse(CArrayInputStream stream)
    {
      if (stream.GetFail())
        return false;
      CLeaderBoard.m_userID = stream.ReadUInt32();
      return CSongScoreMgr.Write();
    }

    public static void SetUserName(string userName)
    {
      if (userName.Length <= 0)
        return;
      CLeaderBoard.m_pUserName = userName;
    }

    private bool Write() => CSongScoreMgr.Write();
  }
}
