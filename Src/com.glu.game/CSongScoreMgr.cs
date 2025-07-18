// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSongScoreMgr
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace com.glu.game
{
  internal class CSongScoreMgr : CSingleton
  {
    public static tSaveSongScore m_saver = new tSaveSongScore();
    private static string kSongScoreFileName = "scores.dat";
    protected static CLinkList m_scores = new CLinkList();

    public static int GetNumbSongScores() => CSongScoreMgr.m_scores.m_count;

    private int CompareSongScore(CLinkListNode pNode, object pData)
    {
      CSongScoreMgr.SongScore data = (CSongScoreMgr.SongScore) pNode.GetData();
      return ((CSongScoreMgr.SongScore) pData).songID >= data.songID ? 1 : -1;
    }

    public static void Empty()
    {
      CSongScoreMgr.m_scores = new CLinkList();
      CGHStaticData.m_unlockedIdx = 1U;
      CGHStaticData.m_unlockedIdx |= 256U;
      CGHStaticData.ResetSongAchievementData();
    }

    public static CSongScoreMgr.SongScore GetSongScore(uint songID)
    {
      CSongScoreMgr.SongScore songScore = (CSongScoreMgr.SongScore) null;
      for (CLinkListNode clinkListNode = CSongScoreMgr.m_scores.GetHead(); clinkListNode != null; clinkListNode = clinkListNode.GetNext())
      {
        CSongScoreMgr.SongScore data = (CSongScoreMgr.SongScore) clinkListNode.GetData();
        if ((int) data.songID == (int) songID)
        {
          songScore = data;
          break;
        }
      }
      return songScore;
    }

    public static void Init() => CSongScoreMgr.Read();

    private static bool Read()
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CSongScoreMgr.kSongScoreFileName);
      bool flag;
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Open, FileAccess.Read);
        CSongScoreMgr.m_saver = (tSaveSongScore) new XmlSerializer(typeof (tSaveSongScore)).Deserialize(fileStream);
        fileStream.Dispose();
        flag = true;
      }
      catch (Exception ex)
      {
        return false;
      }
      if (CSongScoreMgr.m_saver.userName != null)
        CLeaderBoard.SetUserName(CSongScoreMgr.m_saver.userName);
      else
        CLeaderBoard.SetUserName("Rocker");
      for (int index1 = 0; index1 < CSongScoreMgr.m_saver.numScores + 1; ++index1)
      {
        CSongScoreMgr.SongScore pData = new CSongScoreMgr.SongScore();
        pData.songID = CSongScoreMgr.m_saver.songID[index1];
        for (int index2 = 0; index2 < 3; ++index2)
        {
          for (int index3 = 0; index3 < 3; ++index3)
          {
            pData.scores[index2, index3] = CSongScoreMgr.m_saver.scores[index1][index2][index3];
            pData.starRating[index2, index3] = CSongScoreMgr.m_saver.starRating[index1][index2][index3];
          }
        }
        pData.isUnlocked = CSongScoreMgr.m_saver.isUnlocked[index1];
        pData.numPassed = CSongScoreMgr.m_saver.numPassed[index1];
        CLinkListNode pNode = new CLinkListNode();
        CSongScoreMgr.m_scores.InsertAtEnd(pNode, (object) pData);
      }
      CGHStaticData.m_unlockedAchievements = CSongScoreMgr.m_saver.unlockedAchievements;
      CGHStaticData.m_unlockedIdx = CSongScoreMgr.m_saver.unlockedIdx;
      CGHStaticData.m_bCompletedSongWithEddie = CSongScoreMgr.m_saver.completedSongWithEddie;
      CGHStaticData.m_bCompletedSongWithPandora = CSongScoreMgr.m_saver.completedSongWithPandora;
      for (int index = 0; index < 128; ++index)
        CGHStaticData.SetSongCompletionData(CSongScoreMgr.m_saver.completionDataSongIDs[index], CSongScoreMgr.m_saver.completionDataForSongIDs[index]);
      CGHStaticData.InitializeSongAchievements();
      return flag;
    }

    public static void Reset()
    {
      CLeaderBoard.Reset();
      CSongScoreMgr.Empty();
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CSongScoreMgr.kSongScoreFileName);
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Create, FileAccess.Write);
        fileStream.WriteByte((byte) 0);
        fileStream.Dispose();
      }
      catch (Exception ex)
      {
      }
    }

    public static bool StoreScore(
      uint songID,
      Consts.eDifficulty difficulty,
      Consts.eInstrument instrument,
      uint score,
      uint star)
    {
      CSongScoreMgr.SongScore pData = CSongScoreMgr.GetSongScore(songID);
      if (pData == null)
      {
        pData = new CSongScoreMgr.SongScore(songID);
        if (pData == null)
          return false;
        CLinkListNode pNode = new CLinkListNode();
        if (pNode == null)
          return false;
        CSongScoreMgr.m_scores.InsertAtEnd(pNode, (object) pData);
      }
      if (score > 0U)
        ++pData.numPassed;
      if (score <= pData.GetScore(difficulty, instrument))
        return true;
      pData.SetScore(difficulty, instrument, score, star);
      return CSongScoreMgr.Write();
    }

    public static bool Write()
    {
      CSongScoreMgr.m_saver.userName = CLeaderBoard.GetUsername();
      CSongScoreMgr.GetNumbSongScores();
      int index1 = 0;
      for (CLinkListNode clinkListNode = CSongScoreMgr.m_scores.GetHead(); clinkListNode != null; clinkListNode = clinkListNode.GetNext())
      {
        CSongScoreMgr.SongScore data = (CSongScoreMgr.SongScore) clinkListNode.GetData();
        CSongScoreMgr.m_saver.songID[index1] = data.songID;
        for (int index2 = 0; index2 < 3; ++index2)
        {
          for (int index3 = 0; index3 < 3; ++index3)
          {
            CSongScoreMgr.m_saver.scores[index1][index2][index3] = data.scores[index2, index3];
            CSongScoreMgr.m_saver.starRating[index1][index2][index3] = data.starRating[index2, index3];
          }
        }
        CSongScoreMgr.m_saver.isUnlocked[index1] = data.isUnlocked;
        CSongScoreMgr.m_saver.numPassed[index1] = data.numPassed;
        ++index1;
      }
      CSongScoreMgr.m_saver.numScores = index1;
      CSongScoreMgr.m_saver.unlockedAchievements = CGHStaticData.m_unlockedAchievements;
      CSongScoreMgr.m_saver.unlockedIdx = CGHStaticData.m_unlockedIdx;
      CSongScoreMgr.m_saver.completedSongWithEddie = CGHStaticData.m_bCompletedSongWithEddie;
      CSongScoreMgr.m_saver.completedSongWithPandora = CGHStaticData.m_bCompletedSongWithPandora;
      for (int songIndex = 0; songIndex < 128; ++songIndex)
      {
        CSongScoreMgr.m_saver.completionDataSongIDs[songIndex] = CGHStaticData.GetCompletionDataSongID(songIndex);
        CSongScoreMgr.m_saver.completionDataForSongIDs[songIndex] = CGHStaticData.GetCompletionDataForSongID(songIndex);
      }
      ICFileMgr instance = ICFileMgr.GetInstance();
      string applicationPath = instance.GetApplicationPath();
      if (applicationPath.Length > 0)
        applicationPath += instance.GetPathSeparator();
      string wcsFileName = applicationPath + CSongScoreMgr.kSongScoreFileName;
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(wcsFileName, FileMode.Create, FileAccess.Write);
        new XmlSerializer(typeof (tSaveSongScore)).Serialize(fileStream, (object) CSongScoreMgr.m_saver);
        fileStream.Dispose();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public class SongScore
    {
      public uint songID;
      public uint[,] scores = new uint[3, 3];
      public uint[,] starRating = new uint[3, 3];
      public bool isUnlocked;
      public ushort numPassed;

      public SongScore() => this.Setup(0U);

      public SongScore(uint songID)
      {
        this.Setup(songID);
        CSong song = CSongListMgr.GetSong(songID);
        if (song == null || !song.IsEncore())
          return;
        this.isUnlocked = false;
      }

      private void Setup(uint songID)
      {
        this.songID = songID;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          for (int index2 = 0; index2 < 3; ++index2)
          {
            this.scores[index1, index2] = 0U;
            this.starRating[index1, index2] = 0U;
          }
        }
        this.isUnlocked = true;
        this.numPassed = (ushort) 0;
      }

      public void Unlock() => this.isUnlocked = true;

      public bool IsUnlocked() => this.isUnlocked;

      public uint GetTotalScore()
      {
        uint totalScore = 0;
        CLinkListNode clinkListNode = CSongScoreMgr.m_scores.GetHead();
        for (int instrument = 0; instrument < 3; ++instrument)
        {
          uint val1 = 0;
          for (int difficulty = 0; difficulty < 3; ++difficulty)
          {
            CSongScoreMgr.SongScore data = (CSongScoreMgr.SongScore) clinkListNode.GetData();
            val1 = CMath.Max(val1, data.GetScore((Consts.eDifficulty) difficulty, (Consts.eInstrument) instrument));
          }
          totalScore += val1;
          clinkListNode = clinkListNode.GetNext();
        }
        return totalScore;
      }

      public uint GetScore(Consts.eDifficulty difficulty, Consts.eInstrument instrument)
      {
        return this.scores[(int) instrument, (int) difficulty];
      }

      public uint GetStarRating(Consts.eDifficulty difficulty, Consts.eInstrument instrument)
      {
        return this.starRating[(int) instrument, (int) difficulty];
      }

      public void SetScore(
        Consts.eDifficulty difficulty,
        Consts.eInstrument instrument,
        uint score,
        uint star)
      {
        this.scores[(int) instrument, (int) difficulty] = score;
        this.starRating[(int) instrument, (int) difficulty] = star;
      }
    }
  }
}
