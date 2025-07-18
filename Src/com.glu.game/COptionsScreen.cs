// Decompiled with JetBrains decompiler
// Type: com.glu.game.COptionsScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  internal class COptionsScreen : CWidgetScreen
  {
    private const int kOptionsSoundID = 1;
    private const int kOptionsSoundItems = 2;
    private const int kOptionsVibrationID = 2;
    private const int kOptionsVibrationItems = 2;
    private const int kOptionsVolumeID = 3;
    private const int kOptionsOrientationID = 4;
    protected bool m_soundEnabled;
    protected bool m_vibrationEnabled;
    protected uint m_volume;
    protected uint m_orientation;
    protected ICRenderSurface m_pLeft;
    protected ICRenderSurface m_pRight;
    protected CMedia m_pSound;
    protected CNavigatorWidget m_soundNavigator;
    protected CNavigatorWidget m_vibrationNavigator;
    protected CNavigatorWidget m_volumeNavigator;
    protected CNavigatorWidget m_orientationNavigator;
    protected string m_strSound;
    protected string m_strVibration;
    protected string m_strVolume;
    protected string m_strOrientation;
    protected string[] m_tableOptionOrientations = new string[5];
    protected int m_orientationItems;
    private string[] kTableOptionsSound = new string[2]
    {
      "IDS_OPTIONS_SOUNDOFF",
      "IDS_OPTIONS_SOUNDON"
    };
    private string[] kTableOptionsVibration = new string[2]
    {
      "IDS_OPTIONS_VIBOFF",
      "IDS_OPTIONS_VIBON"
    };

    public COptionsScreen()
    {
      this.m_soundEnabled = COptionsMgr.GetSoundEnabled();
      this.m_vibrationEnabled = COptionsMgr.GetVibrationEnabled();
      this.m_volume = (uint) COptionsMgr.GetInstance().GetVolume();
      this.m_orientation = 0U;
      this.m_pLeft = (ICRenderSurface) null;
      this.m_pRight = (ICRenderSurface) null;
      this.m_pSound = (CMedia) null;
      this.m_orientationItems = 0;
      this.m_eventId = 0U;
    }

    public override uint Start()
    {
      uint num = base.Start();
      CGameData.SetOptionsActive(true);
      return num;
    }

    public override void Stop()
    {
      base.Stop();
      CGameData.SetOptionsActive(false);
      ICMediaPlayer instance = ICMediaPlayer.GetInstance();
      if (this.m_eventId <= 0U)
        return;
      instance.Stop(this.m_eventId);
      instance.StopVibrate(this.m_eventId);
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      COptionsMgr.GetInstance();
      ICMediaPlayer instance1 = ICMediaPlayer.GetInstance();
      bool flag = false;
      switch (id)
      {
        case 129075783:
          COptionsMgr.SetSoundEnabled(this.m_soundEnabled);
          COptionsMgr.SetVibrationEnabled(this.m_vibrationEnabled);
          COptionsMgr.SetVolume((byte) this.m_volume);
          instance1.SetSoundEnabled(this.m_soundEnabled);
          instance1.SetVibrationEnabled(this.m_vibrationEnabled);
          instance1.SetVolume((byte) this.m_volume);
          ICCore instance2 = ICCore.GetInstance();
          if (instance2.ApplicationCanSetOrientation())
          {
            CGameData.SetDontRender(true);
            switch (this.m_tableOptionOrientations[(int) this.m_orientation])
            {
              case "IDS_OPTIONS_ORIENTATION_ROTATE_LEFT":
                instance2.DeviceOrientationSetRotateLeft();
                break;
              case "IDS_OPTIONS_ORIENTATION_ROTATE_RIGHT":
                instance2.DeviceOrientationSetRotateRight();
                break;
              case "IDS_OPTIONS_ORIENTATION_FLIP":
                instance2.DeviceOrientationSetFlip();
                break;
              case "IDS_OPTIONS_ORIENTATION_DEFAULT":
                instance2.DeviceOrientationSetDefault();
                break;
              default:
                CGameData.SetDontRender(false);
                break;
            }
          }
          COptionsMgr.SetOrientation((byte) instance2.GetDeviceOrientation());
          this.SetInterrupt(1);
          flag = true;
          break;
        case 555763780:
          instance1.SetSoundEnabled(COptionsMgr.GetSoundEnabled());
          instance1.SetVibrationEnabled(COptionsMgr.GetVibrationEnabled());
          instance1.SetVolume(COptionsMgr.GetInstance().GetVolume());
          this.SetInterrupt(2);
          flag = true;
          break;
        case 1053973641:
          CNavigatorWidget cnavigatorWidget = (CNavigatorWidget) param2;
          switch (cnavigatorWidget.GetID())
          {
            case 1:
              this.m_soundEnabled = cnavigatorWidget.GetSelectionIndex() != 0;
              instance1.SetSoundEnabled(this.m_soundEnabled);
              instance1.SetVibrationEnabled(this.m_vibrationEnabled);
              instance1.SetVolume((byte) this.m_volume);
              if (this.m_soundEnabled)
              {
                this.m_eventId = instance1.Play(this.m_pSound, (byte) 0, (byte) 0);
                break;
              }
              break;
            case 2:
              this.m_vibrationEnabled = cnavigatorWidget.GetSelectionIndex() != 0;
              instance1.SetSoundEnabled(this.m_soundEnabled);
              instance1.SetVibrationEnabled(this.m_vibrationEnabled);
              instance1.SetVolume((byte) this.m_volume);
              if (this.m_vibrationEnabled)
              {
                this.m_eventId = instance1.Vibrate(500U, (byte) 0);
                break;
              }
              break;
            case 3:
              this.m_volume = (uint) cnavigatorWidget.GetSelectionIndex();
              instance1.SetVolume((byte) this.m_volume);
              break;
            case 4:
              this.m_orientation = (uint) cnavigatorWidget.GetSelectionIndex();
              break;
          }
          this.BuildOptionsText();
          this.m_base.SetDirty();
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource("SUR_ARROW_LEFT", out resource1);
      if (resource1 != null)
        this.m_pLeft = (ICRenderSurface) resource1.GetData();
      int resource3 = (int) resourceManager.CreateResource("SUR_ARROW_RIGHT", out resource1);
      if (resource1 != null)
        this.m_pRight = (ICRenderSurface) resource1.GetData();
      int resource4 = (int) resourceManager.CreateResource("IDM_PROMPT", out resource1);
      if (resource1 == null)
        return;
      this.m_pSound = (CMedia) resource1.GetData();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_ARROW_LEFT");
      resourceManager.ReleaseResource("SUR_ARROW_RIGHT");
      resourceManager.ReleaseResource("IDM_PROMPT");
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      this.m_page.SetWrap(true);
      this.m_soundNavigator.SetID(1);
      this.m_soundNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_soundNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_soundNavigator.SetLeftImage(this.m_pLeft);
      this.m_soundNavigator.SetRightImage(this.m_pRight);
      this.m_soundNavigator.SetPageCount(2);
      this.m_soundNavigator.SetFocusable(true);
      this.m_soundNavigator.SetSelectable(true);
      this.m_soundNavigator.SetSelectionIndex(this.m_soundEnabled ? 1 : 0);
      this.m_soundNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_soundNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_page.Add((CUIWidget) this.m_soundNavigator);
      this.m_volumeNavigator.SetID(3);
      this.m_volumeNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_volumeNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_volumeNavigator.SetLeftImage(this.m_pLeft);
      this.m_volumeNavigator.SetRightImage(this.m_pRight);
      this.m_volumeNavigator.SetPageCount(11);
      this.m_volumeNavigator.SetFocusable(true);
      this.m_volumeNavigator.SetSelectable(true);
      this.m_volumeNavigator.SetSelectionIndex((int) this.m_volume);
      this.m_volumeNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_volumeNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_page.Add((CUIWidget) this.m_volumeNavigator);
      if (ICCore.GetInstance().ApplicationCanSetOrientation())
      {
        this.m_orientationNavigator.SetID(4);
        this.m_orientationNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
        this.m_orientationNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_orientationNavigator.SetLeftImage(this.m_pLeft);
        this.m_orientationNavigator.SetRightImage(this.m_pRight);
        this.m_orientationNavigator.SetFocusable(true);
        this.m_orientationNavigator.SetSelectable(true);
        this.m_orientationNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
        this.m_orientationNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
        this.m_page.Add((CUIWidget) this.m_orientationNavigator);
      }
      this.SetMovie("GLU_MOVIE_COMMON");
    }

    public override void Layout()
    {
      base.Layout();
      this.BuildOptionsText();
    }

    private void BuildOptionsText()
    {
      string output = (string) null;
      CUtility.GetString(out this.m_strSound, this.kTableOptionsSound[this.m_soundEnabled ? 1 : 0]);
      CUtility.GetString(out this.m_strVibration, this.kTableOptionsVibration[this.m_vibrationEnabled ? 1 : 0]);
      CUtility.GetString(out output, "IDS_OPTIONS_VOLUME");
      this.m_strVolume = string.Format(output, (object) this.m_volume);
      this.m_orientationItems = 0;
      this.m_strOrientation = "";
      ICCore instance = ICCore.GetInstance();
      if (instance.ApplicationCanSetOrientation())
      {
        this.m_tableOptionOrientations[this.m_orientationItems++] = "IDS_OPTIONS_ORIENTATION_CURRENT";
        if (instance.DeviceOrientationCanSetRotateLeft())
          this.m_tableOptionOrientations[this.m_orientationItems++] = "IDS_OPTIONS_ORIENTATION_ROTATE_LEFT";
        if (instance.DeviceOrientationCanSetRotateRight())
          this.m_tableOptionOrientations[this.m_orientationItems++] = "IDS_OPTIONS_ORIENTATION_ROTATE_RIGHT";
        if (instance.DeviceOrientationCanSetFlip())
          this.m_tableOptionOrientations[this.m_orientationItems++] = "IDS_OPTIONS_ORIENTATION_FLIP";
        if (instance.DeviceOrientationCanSetDefault())
          this.m_tableOptionOrientations[this.m_orientationItems++] = "IDS_OPTIONS_ORIENTATION_DEFAULT";
        this.m_orientationNavigator.SetPageCount(this.m_orientationItems);
        if ((long) this.m_orientation > (long) this.m_orientationItems)
          this.m_orientation = 0U;
        CUtility.GetString(out this.m_strOrientation, this.m_tableOptionOrientations[(int) this.m_orientation]);
      }
      this.m_soundNavigator.SetSingleText(this.m_strSound);
      this.m_vibrationNavigator.SetSingleText(this.m_strVibration);
      this.m_volumeNavigator.SetSingleText(this.m_strVolume);
      this.m_orientationNavigator.SetSingleText(this.m_strOrientation);
    }
  }
}
