// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSaveGameMgr
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
  public class CSaveGameMgr : CSingleton
  {
    public static uint ClassId = 292781666;
    protected new static CSingleton m_instance = (CSingleton) null;
    public tSGData m_sgData = new tSGData();
    public static string kwcsSGFileName = "savegame.dat";

    public static CSaveGameMgr GetInstance()
    {
      if (CSaveGameMgr.m_instance == null)
        CSaveGameMgr.m_instance = (CSingleton) new CSaveGameMgr();
      return CSaveGameMgr.m_instance as CSaveGameMgr;
    }

    public bool GetChecksumFailed() => false;

    public CSaveGameMgr()
      : base(CSaveGameMgr.ClassId)
    {
      this.Reset();
    }

    public bool GetSavedGame() => this.m_sgData.m_savedGame;

    public void SetSavedGame(bool savedGame) => this.m_sgData.m_savedGame = savedGame;

    public bool Read()
    {
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CSaveGameMgr.kwcsSGFileName);
      bool flag;
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Open, FileAccess.Read);
        this.m_sgData = (tSGData) new XmlSerializer(typeof (tSGData)).Deserialize(fileStream);
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
        this.Write();
      }
      CGHStaticData.m_musicianAppearance = this.m_sgData.m_musicianAppearance;
      CGHStaticData.m_guitarAppearance = this.m_sgData.m_guitarAppearance;
      CGHStaticData.m_drumsAppearance = this.m_sgData.m_drumsAppearance;
      return flag;
    }

    public bool Write()
    {
      this.m_sgData.m_musicianAppearance = CGHStaticData.m_musicianAppearance;
      this.m_sgData.m_guitarAppearance = CGHStaticData.m_guitarAppearance;
      this.m_sgData.m_drumsAppearance = CGHStaticData.m_drumsAppearance;
      string outValue;
      CFileUtil.GetApplicationDataPathForFile(out outValue, CSaveGameMgr.kwcsSGFileName);
      try
      {
        Stream fileStream = (Stream) ICFileMgr.GetInstance().GetFileStream(outValue, FileMode.Create, FileAccess.Write);
        new XmlSerializer(typeof (tSGData)).Serialize(fileStream, (object) this.m_sgData);
        fileStream.Dispose();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void Reset() => this.m_sgData.Reset();
  }
}
