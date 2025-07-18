// Decompiled with JetBrains decompiler
// Type: com.glu.game.CLeaderBoardTable
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CLeaderBoardTable
  {
    public ushort nScores;
    public uint m_songID;
    public CLeaderBoardTable.CLeaderBoardTableScore[] m_scores;

    public CLeaderBoardTable()
    {
      this.nScores = (ushort) 0;
      this.m_songID = 0U;
    }

    public class CLeaderBoardTableScore
    {
      public uint userID;
      public uint score;
      public uint rank;
      public string name;

      public CLeaderBoardTableScore()
      {
        this.userID = 0U;
        this.name = (string) null;
        this.rank = 0U;
        this.score = 0U;
      }

      public void SetName(CArrayInputStream stream)
      {
        this.name = (string) null;
        ushort buflen = stream.ReadUInt16();
        char[] szOut = new char[(int) buflen];
        if (buflen <= (ushort) 0)
          return;
        int num = (int) stream.ReadJMUtf(szOut, (uint) buflen);
        string str = new string(szOut);
      }
    }
  }
}
