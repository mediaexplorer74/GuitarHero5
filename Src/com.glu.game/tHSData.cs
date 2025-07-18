// Decompiled with JetBrains decompiler
// Type: com.glu.game.tHSData
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class tHSData
  {
    public string m_lastEnteredName;
    public string[,] m_names = new string[CHighscoreMgr.kNumScoreTables, CHighscoreMgr.kMaxHighScores];
    public int[,] m_scores = new int[CHighscoreMgr.kNumScoreTables, CHighscoreMgr.kMaxHighScores];

    public tHSData() => this.Reset();

    public void Reset()
    {
      this.m_lastEnteredName = CHighscoreMgr.kwszNoName.Substring(0);
      for (int index1 = 0; index1 < CHighscoreMgr.kNumScoreTables; ++index1)
      {
        for (int index2 = 0; index2 < CHighscoreMgr.kNumScoreTables; ++index2)
        {
          this.m_names[index1, index2] = CHighscoreMgr.kwszNoName.Substring(0);
          this.m_scores[index1, index2] = 0;
        }
      }
    }
  }
}
