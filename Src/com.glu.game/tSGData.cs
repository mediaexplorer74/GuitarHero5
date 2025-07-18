// Decompiled with JetBrains decompiler
// Type: com.glu.game.tSGData
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class tSGData
  {
    public bool m_savedGame;
    public int m_musicianAppearance;
    public int m_guitarAppearance;
    public int m_drumsAppearance;

    public tSGData() => this.Reset();

    public void Reset()
    {
      this.m_savedGame = false;
      this.m_musicianAppearance = 0;
      this.m_guitarAppearance = 0;
      this.m_drumsAppearance = 0;
    }
  }
}
