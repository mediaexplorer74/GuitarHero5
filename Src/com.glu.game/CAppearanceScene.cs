// Decompiled with JetBrains decompiler
// Type: com.glu.game.CAppearanceScene
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CAppearanceScene : CStateMachineNode
  {
    protected bool m_inset;
    private CResourceManager globalResourceMgr;
    private CResource globalResource;

    public void SetInset(bool inset) => this.m_inset = inset;

    public CAppearanceScene() => this.m_inset = false;

    public override uint Start()
    {
      uint num = base.Start();
      this.ChangeState(1, 1);
      return num;
    }

    protected override uint OnCreateState(out CNode pOut, int id)
    {
      uint state = 0;
      CNode cnode = (CNode) null;
      switch (id)
      {
        case 1:
          string[] pImageIds1 = new string[2];
          string[] ptextIds1 = new string[2];
          for (int index = 0; index < 2; ++index)
          {
            pImageIds1[index] = Consts.kTableAppearanceRockers[index * 2];
            ptextIds1[index] = Consts.kTableAppearanceRockers[index * 2 + 1];
          }
          CImageMenuScreen cimageMenuScreen1 = new CImageMenuScreen();
          this.globalResourceMgr = CApp.GetResourceManager();
          string output;
          CUtility.GetString(out output, "IDS_PICK_ROCKER");
          cimageMenuScreen1.SetTitle(output);
          cimageMenuScreen1.SetInfo(pImageIds1, ptextIds1, 2);
          cimageMenuScreen1.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen1.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cimageMenuScreen1.SetFlags(9);
          cnode = (CNode) cimageMenuScreen1;
          break;
        case 2:
          string[] pImageIds2 = new string[4];
          string[] ptextIds2 = new string[4];
          for (int index = 0; index < 4; ++index)
          {
            pImageIds2[index] = "SUR_UI_INSTRUMENT_LOCKED";
            ptextIds2[index] = "IDS_GUITAR_LOCKED";
            if (((long) CGHStaticData.m_unlockedIdx & (long) (1 << index)) != 0L)
            {
              pImageIds2[index] = Consts.kTableAppearanceGuitars[index * 2];
              ptextIds2[index] = Consts.kTableAppearanceGuitars[index * 2 + 1];
            }
          }
          CImageMenuScreen cimageMenuScreen2 = new CImageMenuScreen();
          cimageMenuScreen2.SetTitle("IDS_PICK_INSTRUMENTS");
          cimageMenuScreen2.SetInfo(pImageIds2, ptextIds2, 4);
          cimageMenuScreen2.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen2.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cimageMenuScreen2.SetFlags(1);
          cnode = (CNode) cimageMenuScreen2;
          for (int itemIndex = 0; itemIndex < 4; ++itemIndex)
          {
            if (((long) CGHStaticData.m_unlockedIdx & (long) (1 << itemIndex)) == 0L)
              cimageMenuScreen2.SetItemSelectable(itemIndex, false);
          }
          break;
        case 3:
          string[] pImageIds3 = new string[4];
          string[] ptextIds3 = new string[4];
          for (int index = 0; index < 4; ++index)
          {
            pImageIds3[index] = "SUR_UI_DRUMSET_LOCKED";
            ptextIds3[index] = "IDS_DRUMSET_LOCKED";
            if (((long) CGHStaticData.m_unlockedIdx & (long) (1 << index + 8)) != 0L)
            {
              pImageIds3[index] = Consts.kTableAppearanceDrumsets[index * 2];
              ptextIds3[index] = Consts.kTableAppearanceDrumsets[index * 2 + 1];
            }
          }
          CImageMenuScreen cimageMenuScreen3 = new CImageMenuScreen();
          cimageMenuScreen3.SetTitle("IDS_PICK_DRUMS");
          cimageMenuScreen3.SetInfo(pImageIds3, ptextIds3, 4);
          cimageMenuScreen3.SetMovie("GLU_MOVIE_SELECTION");
          cimageMenuScreen3.SetSoftkeys("SUR_SOFTKEY_CHECK", "SUR_SOFTKEY_ARROW");
          cimageMenuScreen3.SetFlags(1);
          cnode = (CNode) cimageMenuScreen3;
          for (int itemIndex = 0; itemIndex < 4; ++itemIndex)
          {
            if (((long) CGHStaticData.m_unlockedIdx & (long) (1 << itemIndex + 8)) == 0L)
              cimageMenuScreen3.SetItemSelectable(itemIndex, false);
          }
          break;
      }
      pOut = new CNode();
      pOut = cnode;
      return state;
    }

    protected override void OnStateInterrupt(int id, CNode pState)
    {
      switch (id)
      {
        case 1:
          if (pState.GetInterrupt() == 1)
          {
            CGHStaticData.m_musicianAppearance = ((CImageMenuScreen) pState).GetSelected();
            this.ChangeState(2, 1);
            break;
          }
          this.SetInterrupt(2);
          break;
        case 2:
          if (pState.GetInterrupt() == 1)
          {
            if (((CImageMenuScreen) pState).ItemIsSelectable(((CImageMenuScreen) pState).GetSelected()))
            {
              CGHStaticData.m_guitarAppearance = ((CImageMenuScreen) pState).GetSelected();
              this.ChangeState(3, 1);
              break;
            }
            pState.ClearInterrupt();
            break;
          }
          this.ChangeState(1, 4);
          break;
        case 3:
          if (pState.GetInterrupt() == 1)
          {
            if (((CImageMenuScreen) pState).ItemIsSelectable(((CImageMenuScreen) pState).GetSelected()))
            {
              CGHStaticData.m_drumsAppearance = ((CImageMenuScreen) pState).GetSelected();
              pState.ClearInterrupt();
              this.SetInterrupt(1);
              break;
            }
            pState.ClearInterrupt();
            break;
          }
          this.ChangeState(1, 4);
          break;
      }
    }

    public enum eOptionsState
    {
      APPEARANCE_NONE,
      APPEARANCE_CHANGE,
      APPEARANCE_INSTRUMENT_SELECTION,
      APPEARANCE_DRUMSET_SELECTION,
    }
  }
}
