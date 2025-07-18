// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Special
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public static class SG_Special
  {
    public static bool DrawSpecialFramePre(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID)
    {
      return false;
    }

    public static void DrawSpecialFramePost(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID)
    {
    }

    public static bool DrawSpecialLayerPre(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID,
      int layerID)
    {
      return false;
    }

    public static void DrawSpecialLayerPost(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID,
      int layerID)
    {
    }

    public static bool DrawSpecialSpritePre(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID,
      int layerID,
      int spriteID,
      int x,
      int y)
    {
      SG_Home instance = SG_Home.GetInstance();
      if (traverseMode == SG_TraverseMode.SG_TraverseMode_DRAW)
      {
        switch (instance.GetTag(spriteID))
        {
          case 1:
            CHud.paintChainCount(x + instance.GetOriginX(), y + instance.GetOriginY());
            return true;
          case 2:
            CHud.paintScore(x + instance.GetOriginX(), y + instance.GetOriginY());
            return true;
          case 3:
          case 4:
          case 5:
          case 6:
          case 7:
          case 8:
          case 9:
          case 10:
          case 11:
          case 12:
          case 13:
          case 14:
          case 15:
          case 16:
          case 17:
          case 18:
            return true;
        }
      }
      if (traverseMode == SG_TraverseMode.SG_TraverseMode_BOUNDS)
      {
        int tag = instance.GetTag(spriteID);
        uint width;
        uint height;
        instance.GetSize(spriteID, out width, out height);
        switch (tag)
        {
          case 3:
            CGHStaticData.m_noteTrackStartLeftPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackStartLeftPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 4:
            CGHStaticData.m_noteTrackStartCenterPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackStartCenterPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 5:
            CGHStaticData.m_noteTrackStartRightPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackStartRightPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 6:
            CGHStaticData.m_noteTrackEndLeftPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackEndLeftPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 7:
            CGHStaticData.m_noteTrackEndCenterPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackEndCenterPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 8:
            CGHStaticData.m_noteTrackEndRightPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteTrackEndRightPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 9:
            CGHStaticData.m_noteButtonLeftPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteButtonLeftPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 10:
            CGHStaticData.m_noteButtonCenterPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteButtonCenterPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 11:
            CGHStaticData.m_noteButtonRightPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_noteButtonRightPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 12:
            CGHStaticData.m_guitarPlayerAnchorPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_guitarPlayerAnchorPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 13:
            CGHStaticData.m_multiMeterAnchorPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_multiMeterAnchorPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 16:
            CGHStaticData.m_trackEdgeTopRight.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_trackEdgeTopRight.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 17:
            CGHStaticData.m_trackEdgeBottomRight.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_trackEdgeBottomRight.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 18:
            CGHStaticData.m_starPowerAnchorPostion.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_starPowerAnchorPostion.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 19:
            CGHStaticData.m_touchButtonLeftPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_touchButtonLeftPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 20:
            CGHStaticData.m_touchButtonCenterPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_touchButtonCenterPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
          case 21:
            CGHStaticData.m_touchButtonRightPosition.m_x = (int) ((long) x + (long) (width >> 1));
            CGHStaticData.m_touchButtonRightPosition.m_y = (int) ((long) y + (long) (height >> 1));
            return true;
        }
      }
      return false;
    }

    public static void DrawSpecialSpritePost(
      SG_TraverseMode traverseMode,
      SG_Presenter sgPresenter,
      int frameID,
      int layerID,
      int spriteID,
      int x,
      int y)
    {
    }
  }
}
