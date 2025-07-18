// Decompiled with JetBrains decompiler
// Type: com.glu.game.tSaveSongScore
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class tSaveSongScore
  {
    public string userName;
    public uint[] songID = new uint[40];
    public uint[][][] scores = new uint[40][][];
    public uint[][][] starRating = new uint[40][][];
    public bool[] isUnlocked = new bool[40];
    public ushort[] numPassed = new ushort[40];
    public int numScores;
    public uint unlockedIdx;
    public uint unlockedAchievements;
    public bool completedSongWithEddie;
    public bool completedSongWithPandora;
    public uint[] completionDataSongIDs = new uint[128];
    public uint[] completionDataForSongIDs = new uint[128];

    public tSaveSongScore()
    {
      for (int index1 = 0; index1 < 40; ++index1)
      {
        this.scores[index1] = new uint[3][];
        this.starRating[index1] = new uint[3][];
        for (int index2 = 0; index2 < 3; ++index2)
        {
          this.scores[index1][index2] = new uint[3];
          this.starRating[index1][index2] = new uint[3];
        }
      }
    }
  }
}
