// Decompiled with JetBrains decompiler
// Type: com.glu.game.CFontMgr
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public sealed class CFontMgr : CSingleton
  {
    public uint ClassId = 1889078030;
    private new static CSingleton m_instance;
    private CFont[] m_fontArray;

    public static CFontMgr GetInstance()
    {
      if (CFontMgr.m_instance == null)
        CFontMgr.m_instance = (CSingleton) new CFontMgr();
      return CFontMgr.m_instance as CFontMgr;
    }

    public CFont GetFont(CFontMgr.eGameFont fontId)
    {
      CFont font = this.m_fontArray[(int) fontId];
      if (font == null)
      {
        string resIdData = Consts.kTableGameFontInfo[(int) fontId].m_resIdData;
        string resIdImage = Consts.kTableGameFontInfo[(int) fontId].m_resIdImage;
        CGraphicFont cgraphicFont = new CGraphicFont();
        if (cgraphicFont != null)
        {
          CResourceManager resourceManager = CApp.GetResourceManager();
          CResource resource1;
          int resource2 = (int) resourceManager.CreateResource(resIdImage, out resource1);
          ICRenderSurface data1 = (ICRenderSurface) resource1.GetData();
          int resource3 = (int) resourceManager.CreateResource(resIdData, out resource1);
          CBinary data2 = (CBinary) resource1.GetData();
          cgraphicFont.SetFontImage(data1);
          cgraphicFont.ParseFontMetrics(data2.GetData(), data2.GetSize());
          this.m_fontArray[(int) fontId] = (CFont) cgraphicFont;
          font = (CFont) cgraphicFont;
        }
      }
      return font;
    }

    public void FreeFont(int idx)
    {
      if (this.m_fontArray[idx] == null)
        return;
      this.m_fontArray[idx] = (CFont) null;
    }

     private CFontMgr() => this.m_fontArray = new CFont[5];

    public enum eGameFont
    {
      FONT_TITLEFONT,
      FONT_INGAME_NUMBERS1,
      FONT_INGAME_NUMBERS2,
      FONT_REGULARFONT,
      FONT_NUMBERFONT,
      FONT_LAST,
    }

    public struct tFontInfo(string data, string image)
    {
      public string m_resIdData = data;
      public string m_resIdImage = image;
    }
  }
}
