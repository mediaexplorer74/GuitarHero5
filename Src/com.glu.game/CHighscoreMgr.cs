// Decompiled with JetBrains decompiler
// Type: com.glu.game.CHighscoreMgr
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
  public sealed class CHighscoreMgr : CSingleton
  {
    public static int kHighscoreChars = 5;
    public static int kMaxHighScores = 5;
    public static int kNumScoreTables = 1;
    public static uint ClassId = 1427371037;
    public tHSData m_hsData = new tHSData();
    public static string kwcsHSFileName = "hs.dat";
    public static string kwszNoName = "xxx";
    protected new static CSingleton m_instance = (CSingleton) null;

    public bool GetChecksumFailed() => false;

    public static CHighscoreMgr GetInstance()
    {
      if (CHighscoreMgr.m_instance == null)
        CHighscoreMgr.m_instance = (CSingleton) new CHighscoreMgr();
      return CHighscoreMgr.m_instance as CHighscoreMgr;
    }

    public CHighscoreMgr()
      : base(CHighscoreMgr.ClassId)
    {
      this.Reset();
    }

    public bool Read()
    {
      bool flag = false;
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CHighscoreMgr.kwcsHSFileName);
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Open, FileAccess.Read);
        this.m_hsData = (tHSData) new XmlSerializer(typeof (tHSData)).Deserialize(fileStream);
        fileStream.Dispose();
        flag = true;
      }
      catch (Exception ex)
      {
      }
      if (!flag)
      {
        this.Reset();
        this.Write();
      }
      return flag;
    }

    public bool Write()
    {
      bool flag = false;
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CHighscoreMgr.kwcsHSFileName);
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Create, FileAccess.Write);
        new XmlSerializer(typeof (tHSData)).Serialize(fileStream, (object) this.m_hsData);
        fileStream.Dispose();
        flag = true;
      }
      catch (Exception ex)
      {
      }
      return flag;
    }

    public void Reset() => this.m_hsData.Reset();

    public string GetLastEnteredNamePtr() => this.m_hsData.m_lastEnteredName;

    public void SetLastEnteredName(string szName)
    {
      this.m_hsData.m_lastEnteredName = szName == null ? (string) null : szName.Substring(0);
      this.Write();
    }

    public string GetName(int tableIdx, int idx)
    {
      string name = (string) null;
      if (tableIdx >= 0 && tableIdx < CHighscoreMgr.kNumScoreTables && idx >= 0 && idx < Consts.kMaxHighScores)
        name = this.m_hsData.m_names[tableIdx, idx];
      return name;
    }

    public int GetScore(int tableIdx, int idx)
    {
      int score = 0;
      if (tableIdx >= 0 && tableIdx < CHighscoreMgr.kNumScoreTables && idx >= 0 && idx < Consts.kMaxHighScores)
        score = this.m_hsData.m_scores[tableIdx, idx];
      return score;
    }

    public bool IsNewHighScore(int tableIdx, int score)
    {
      bool flag = false;
      if (tableIdx >= 0 && tableIdx < CHighscoreMgr.kNumScoreTables)
        flag = score >= this.m_hsData.m_scores[tableIdx, Consts.kMaxHighScores - 1];
      return flag;
    }

    public bool StoreScore(string pwszName, int tableIdx, int score)
    {
      bool flag1 = false;
      this.SetLastEnteredName(pwszName);
      if (tableIdx >= 0 && tableIdx < CHighscoreMgr.kNumScoreTables)
      {
        bool flag2 = false;
        for (int index1 = 0; index1 < Consts.kMaxHighScores; ++index1)
        {
          if (score >= this.m_hsData.m_scores[tableIdx, index1])
          {
            for (int index2 = Consts.kMaxHighScores - 1; index2 > index1; --index2)
            {
              this.m_hsData.m_scores[tableIdx, index2] = this.m_hsData.m_scores[tableIdx, index2 - 1];
              this.m_hsData.m_names[tableIdx, index2] = this.m_hsData.m_names[tableIdx, index2 - 1].Substring(0);
            }
            this.m_hsData.m_scores[tableIdx, index1] = score;
            this.m_hsData.m_names[tableIdx, index1] = pwszName == null || pwszName[0] == char.MinValue ? CHighscoreMgr.kwszNoName.Substring(0) : pwszName.Substring(0);
            flag2 = true;
            break;
          }
        }
        flag1 = !flag2 || this.Write();
      }
      return flag1;
    }
  }
}
