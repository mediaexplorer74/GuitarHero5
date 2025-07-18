// Decompiled with JetBrains decompiler
// Type: com.glu.game.COptionsMgr
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
  public class COptionsMgr : CSingleton
  {
    public static uint ClassId = 798592133;
    protected new static CSingleton m_instance = (CSingleton) null;
    public static tPrefsData m_prefsData = new tPrefsData();
    public static string kwcsPrefsFileName = "prefs.dat";

    public static COptionsMgr GetInstance()
    {
      if (COptionsMgr.m_instance == null)
      {
        COptionsMgr.m_instance = (CSingleton) new COptionsMgr();
        ((COptionsMgr) COptionsMgr.m_instance).Read();
      }
      return COptionsMgr.m_instance as COptionsMgr;
    }

    public bool GetChecksumFailed() => false;

    public COptionsMgr()
      : base(COptionsMgr.ClassId)
    {
      this.Reset();
    }

    public bool GetIntroTextSeen() => COptionsMgr.m_prefsData.m_introTextSeen;

    public static void SetIntroTextSeen() => COptionsMgr.m_prefsData.m_introTextSeen = true;

    public static int GetDemoPlays() => COptionsMgr.m_prefsData.m_demoPlays;

    public static bool GetSoundEnabled() => COptionsMgr.m_prefsData.m_soundEnabled;

    public static bool GetVibrationEnabled() => COptionsMgr.m_prefsData.m_vibrationEnabled;

    public byte GetVolume() => COptionsMgr.m_prefsData.m_volume;

    public static byte GetOrientation() => COptionsMgr.m_prefsData.m_orientation;

    public static uint GetLocale() => COptionsMgr.m_prefsData.m_locale;

    public static void SetDemoPlays(int demoPlays)
    {
      COptionsMgr.m_prefsData.m_demoPlays = demoPlays;
    }

    public static void SetSoundEnabled(bool soundEnabled)
    {
      COptionsMgr.m_prefsData.m_soundEnabled = soundEnabled;
    }

    public static void SetVibrationEnabled(bool vibrationEnabled)
    {
      COptionsMgr.m_prefsData.m_vibrationEnabled = vibrationEnabled;
    }

    public static void SetVolume(byte volume) => COptionsMgr.m_prefsData.m_volume = volume;

    public static void SetOrientation(byte orientation)
    {
      COptionsMgr.m_prefsData.m_orientation = orientation;
    }

    public void SetLocale(uint locale) => COptionsMgr.m_prefsData.m_locale = locale;

    public bool Read()
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, COptionsMgr.kwcsPrefsFileName);
      bool flag;
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Open, FileAccess.Read);
        COptionsMgr.m_prefsData = (tPrefsData) new XmlSerializer(typeof (tPrefsData)).Deserialize(fileStream);
        fileStream.Dispose();
        flag = true;
      }
      catch (Exception ex)
      {
        flag = false;
      }
      if (!flag)
      {
        this.Reset();
        COptionsMgr.Write();
      }
      COptionsMgr.m_prefsData.m_soundEnabled = false;
      return flag;
    }

    public static bool Write()
    {
      bool flag = false;
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, COptionsMgr.kwcsPrefsFileName);
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Create, FileAccess.Write);
        new XmlSerializer(typeof (tPrefsData)).Serialize(fileStream, (object) COptionsMgr.m_prefsData);
        fileStream.Dispose();
        flag = true;
      }
      catch (Exception ex)
      {
      }
      return flag;
    }

    public void Reset()
    {
      ICCore instance = ICCore.GetInstance();
      COptionsMgr.m_prefsData.m_introTextSeen = false;
      COptionsMgr.m_prefsData.m_soundEnabled = false;
      COptionsMgr.m_prefsData.m_vibrationEnabled = instance.IsVibrationEnabled();
      COptionsMgr.m_prefsData.m_volume = instance.GetDefaultVolume();
      COptionsMgr.m_prefsData.m_orientation = (byte) 5;
    }
  }
}
