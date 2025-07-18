// Decompiled with JetBrains decompiler
// Type: com.glu.game.tPrefsData
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class tPrefsData
  {
    public uint m_checksum;
    public int m_demoPlays;
    public bool m_soundEnabled;
    public bool m_vibrationEnabled;
    public byte m_volume;
    public byte m_orientation;
    public uint m_locale;
    public bool m_introTextSeen;
    public bool m_profilerEnabled;
    public bool m_intrusiveProfilerEnabled;

    public tPrefsData() => this.Reset();

    public void Reset()
    {
      this.m_checksum = 0U;
      this.m_demoPlays = 0;
      this.m_soundEnabled = false;
      this.m_vibrationEnabled = false;
      this.m_volume = (byte) 0;
      this.m_orientation = (byte) 0;
      this.m_locale = 0U;
      this.m_introTextSeen = false;
      this.m_profilerEnabled = false;
      this.m_intrusiveProfilerEnabled = false;
    }
  }
}
