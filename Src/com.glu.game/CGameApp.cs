// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameApp
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework.Media;
using System.Globalization;
using System.Threading;

#nullable disable
namespace com.glu.game
{
  public sealed class CGameApp : CApp
  {
    private const int kSoftkeyOffsetNull = -128;
    public bool m_allowLiveCalls = true;
    public static object loadQueuedLock;
    private int lsk_portrait_x;
    private int rsk_portrait_x;
    private int lsk_landscape_x;
    private int rsk_landscape_x;
    private string m_appIntroText = "";
    private bool m_localeRestricted;
    private bool m_forceLocaleChooser;
    private bool m_disableMannerMode;
    private bool m_netWarnEnabled;
    private bool m_songResumeEnabled;
    private bool m_supportTouchscreen = true;
    private bool m_disableVenueAnimation;
    private bool m_disableVibration;
    private bool m_disableVolumeControl;
    private bool m_loadErrorOccured;
    private bool m_queueExit;
    private bool m_exitApp;
    public static int m_noteDelay;
    private string m_controlText = "";
    public bool musicWasPlayingAtLaunch;
    public uint APPLET_TICK_RATE_MS = 20;
    public string PROPERTIES_FILE = "properties.dat";
    private CNode m_pRootNode;
    private CEventListener m_listener;
    private static uint[] CGameApp_InterestingEvents = new uint[35]
    {
      1733568088U,
      913654400U,
      2215179113U,
      1732285487U,
      1411673571U,
      1967276899U,
      1368267323U,
      1364371259U,
      1767879702U,
      3343010790U,
      1134794776U,
      2186393822U,
      2300082508U,
      1386813809U,
      2072258765U,
      902053462U,
      902532892U,
      902008092U,
      1460124008U,
      2903985391U,
      850690755U,
      607208024U,
      129075783U,
      555763780U,
      1053973641U,
      1066869024U,
      2535467201U,
      2535498699U,
      1913978637U,
      544526345U,
      2535475076U,
      1600235594U,
      3563016926U,
      1912541268U,
      0U
    };
    public static bool autoplay = false;

    public bool IsLocaleRestricted() => this.m_localeRestricted;

    public bool IsLocaleChooserForced() => this.m_forceLocaleChooser;

    public bool IsMannerModeDisabled() => this.m_disableMannerMode;

    public bool IsNetWarnNeeded() => this.m_netWarnEnabled;

    public bool IsSongResumeEnabled() => this.m_songResumeEnabled;

    public bool IsTouchscreenSupported() => this.m_supportTouchscreen;

    public bool IsVenueAnimationDisabled() => this.m_disableVenueAnimation;

    public bool IsVibrationDisabled() => this.m_disableVibration;

    public bool IsVolumeControlDisabled() => this.m_disableVolumeControl;

    public bool WasLoadError() => this.m_loadErrorOccured;

    public void SetLoadError(bool error) => this.m_loadErrorOccured = error;

    public string GetAppIntroText() => this.m_appIntroText;

    public void QueueExit()
    {
      if (this.m_queueExit)
        this.m_exitApp = true;
      else
        ICCore.GetInstance().ExitApp();
    }

    public string GetOverrideText(string id)
    {
      string output1 = (string) null;
      string output2 = (string) null;
      string output3 = (string) null;
      CUtility.GetString(out output2, id);
      bool flag = false;
      switch (id)
      {
        case "IDS_LOADED_PRESS_A_KEY":
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output1, "IDS_LOADED_PRESS_A_KEY_TOUCH");
          else
            CUtility.GetString(out output1, "IDS_LOADED_PRESS_A_KEY");
          flag = false;
          break;
        case "IDS_LOADING_TIP_01":
        case "IDS_LOADING_TIP_15":
        case "IDS_LOADING_TIP_18":
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output3, "IDS_LOADING_NOTES_TOUCH");
          else
            CUtility.GetString(out output3, "IDS_LOADING_NOTES");
          flag = true;
          break;
        case "IDS_LOADING_TIP_02":
        case "IDS_LOADING_TIP_16":
        case "IDS_LOADING_TIP_19":
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output3, "IDS_LOADING_BAR_TOUCH");
          else
            CUtility.GetString(out output3, "IDS_LOADING_BAR");
          flag = true;
          break;
        case "IDS_LOADING_TIP_17":
        case "IDS_LOADING_TIP_20":
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output3, "IDS_LOADING_SUSTAIN_TOUCH");
          else
            CUtility.GetString(out output3, "IDS_LOADING_SUSTAIN");
          flag = true;
          break;
        case "IDS_INGAME_STAR_POWER_ACTIVATE_BUTTON":
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output1, "IDS_INGAME_STAR_POWER_ACTIVATE_TOUCH");
          else
            CUtility.GetString(out output1, "IDS_INGAME_STAR_POWER_ACTIVATE_BUTTON");
          flag = false;
          break;
        case "IDS_MENU_INSTRUCTIONS_CAREER_TEXT":
          if (this.m_controlText.Length > 0)
          {
            output1 = this.m_controlText;
            flag = false;
            break;
          }
          string output4 = (string) null;
          string output5 = (string) null;
          string output6 = (string) null;
          string output7 = (string) null;
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output4, "IDS_INSTRUCTIONS_NOTES_TOUCH");
          else
            CUtility.GetString(out output4, "IDS_INSTRUCTIONS_NOTES");
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output5, "IDS_INSTRUCTIONS_BAR_TOUCH");
          else
            CUtility.GetString(out output5, "IDS_INSTRUCTIONS_BAR");
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output6, "IDS_INSTRUCTIONS_STAR_TOUCH");
          else
            CUtility.GetString(out output6, "IDS_INSTRUCTIONS_STAR");
          if (this.m_supportTouchscreen)
            CUtility.GetString(out output7, "IDS_INSTRUCTIONS_PAUSE_TOUCH");
          else
            CUtility.GetString(out output7, "IDS_INSTRUCTIONS_PAUSE_RIGHT");
          output1 = string.Format(output2, (object) output4, (object) output5, (object) output6, (object) output7);
          flag = false;
          break;
        case "":
          flag = false;
          output1 = "";
          break;
        default:
          output1 = output2;
          break;
      }
      if (flag)
        output1 = string.Format(output2, (object) output3);
      return output1;
    }

    public static CGameApp CreateInstance()
    {
      if (CApp.m_instance == null)
      {
        CApp.m_instance = (CApp) new CGameApp();
        CGameApp.loadQueuedLock = new object();
      }
      return CApp.m_instance as CGameApp;
    }

    public static CGameApp GetInstance()
    {
      if (CApp.m_instance == null)
        CGameApp.CreateInstance();
      return CApp.m_instance as CGameApp;
    }

    public void CAppExecutor_OnExecute()
    {
      CAppExecutor executor = CApp.GetExecutor();
      if (executor.GetModeQualifier() == CAppExecutor.ModeQualifier.Normal)
      {
        if (executor.GetMode() == CAppExecutor.Mode.Update)
        {
          int num = (int) executor.GetRegistry().Run();
          this.HandleUpdate();
          executor.SetMode(CAppExecutor.Mode.Render);
        }
        else
        {
          if (executor.GetMode() != CAppExecutor.Mode.Render)
            return;
          this.HandleRender();
          executor.SetMode(CAppExecutor.Mode.Update);
        }
      }
      else
      {
        int modeQualifier = (int) executor.GetModeQualifier();
      }
    }

    public uint CAppExecutor_InitRegistry()
    {
      CSystem registry = (CSystem) CApp.GetExecutor().GetRegistry();
      registry.Add(registry.CreateSystemElement(2507990544U, (object) new CExecutableRegistry(), 2000502999U));
      return 0;
    }

    public override uint OnInit()
  {
    uint num = 1;
    CApp.m_registry = CAppFactory.Instance.CreateRegistry();
    if (CApp.m_registry != null)
    {
      this.InitRegistry();
      num = 0U;
    }
    CApp.m_resourceManager = CAppFactory.Instance.CreateResourceManager();
    if (CApp.m_resourceManager != null)
    {
      if (CApp.m_resourceManager.Initialize())
        num = 0U;
    }
    else
      num = 1U;
    CApp.m_executor = CAppFactory.Instance.CreateExecutor();
    if (CApp.m_executor != null)
    {
      if (CApp.m_executor.Init(new CAppExecutor.OnExecute(this.CAppExecutor_OnExecute), new CAppExecutor.InitRegistry(this.CAppExecutor_InitRegistry), (CAppExecutor.ReleaseRegistry) null))
        num = 0U;
    }
    else
      num = 1U;
    this.m_listener = new CEventListener();
    this.m_listener.Initialize(CHandleFactory.CreateHashKey("GameApp"), (object) this, new CEventListener.CEventListerner_EventHandler(CGameApp.EventCB));
    this.m_listener.Register(CGameApp.CGameApp_InterestingEvents);
    ICMediaPlayer.GetInstance();
    this.m_pAccelerometer = new CAccelerometer();
    ICGraphics.GetInstance().Initialize();
    ICGraphics2d.GetInstance().Initialize();
    LiveAchievements.GetInstance().init();
    
    // Enable mouse cursor visibility by default
    com.glu.shared.MouseInput.MouseVisible = true;
      this.m_pRootNode = (CNode) new CRootNode();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.AddDatabase("resource");
      resourceManager.AddDatabase("SG_Resgen");
      resourceManager.AddDatabase("Movie_assets");

      string letterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
      if (!resourceManager.AddDatabase(letterIsoLanguageName))
        resourceManager.AddDatabase("en");
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource("IDM_MENU_SELECT_SFX", out resource1);
      CGHStaticData.m_pMenuSelectSFX = (CMedia) resource1.GetData();
      int resource3 = (int) resourceManager.CreateResource("IDM_MENU_BACK_SFX", out resource1);
      CGHStaticData.m_pMenuBackSFX = (CMedia) resource1.GetData();
      int resource4 = (int) resourceManager.CreateResource("IDM_MENU_SCROLL_SFX", out resource1);
      CGHStaticData.m_pMenuScrollSFX = (CMedia) resource1.GetData();
      return num;
    }

    public override void OnSystemEvent(uint id, uint param1, uint param2)
    {
      int num = (int) new CMessage((CClass) this, id, 0U, 2, new CMessage.Parameter[2]
      {
        new CMessage.Parameter(2065979161U, CMessage.Parameter.Access.Direct, (object) param1),
        new CMessage.Parameter(2065979161U, CMessage.Parameter.Access.Direct, (object) param2)
      }).Run();
      switch (id)
      {
        case 913654400:
          if (!this.musicWasPlayingAtLaunch)
            MediaPlayer.Stop();
          CApp.m_executor.OnStop();
          break;
        case 1411673571:
          CApp.m_executor.OnSuspend();
          break;
        case 1733568088:
          this.musicWasPlayingAtLaunch = !MediaPlayer.GameHasControl;
          CApp.m_executor.OnStart();
          break;
        case 1967276899:
          CApp.m_executor.OnResume();
          break;
      }
    }

    public int InitRegistry()
    {
      CSystem registry = (CSystem) CApp.GetRegistry();
      CRegistry data = new CRegistry();
      registry.Add(registry.CreateSystemElement(4150451705U, (object) data, 1126056847U, 1073741823U));
      return 0;
    }

    public override void HandleUpdate()
    {
      this.m_pRootNode.HandleUpdate(CApp.m_executor.GetElapsedUpdateTimeMillis());
    }

    public override void HandleRender() => this.m_pRootNode.HandleRender();

    public static void ReadData()
    {
      CHighscoreMgr.GetInstance().Read();
      COptionsMgr.GetInstance().Read();
      CSaveGameMgr.GetInstance().Read();
      CSongListMgr.Init();
      CSongScoreMgr.Init();
    }

    private static bool EventCB(CEvent eventIn, object pData)
    {
      CGameApp owner = (CGameApp) ((CEventListener) pData).GetOwner();
      CMessage cmessage = (CMessage) eventIn;
      return owner.HandleEvent(cmessage.GetId(), (uint) cmessage.GetParameter(0).m_data, cmessage.GetParameter(1).m_data);
    }

    private bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 850690755:
          flag = this.m_pRootNode.HandleEvent(id, param1, param2);
          goto case 1767879702;
        case 913654400:
          this.m_pRootNode.Stop();
          ICMediaPlayer.GetInstance().Stop();
          ICMediaPlayer.GetInstance().StopVibrate();
          flag = true;
          goto case 1767879702;
        case 1134794776:
        case 3343010790:
          flag = this.m_pRootNode.HandleEvent(id, param1, param2);
          goto case 1767879702;
        case 1364371259:
          flag = this.m_pRootNode.HandleEvent(id, param1, param2);
          ICCore.GetInstance().ExitApp();
          goto case 1767879702;
        case 1368267323:
          flag = this.m_pRootNode.HandleEvent(id, param1, param2);
          goto case 1767879702;
        case 1411673571:
          ICMediaPlayer.GetInstance().HandleSuspend();
          this.m_pRootNode.HandleEvent(id, param1, param2);
          flag = true;
          goto case 1767879702;
        case 1733568088:
          CDemoMgr.Read();
          ICMediaPlayer.GetInstance().SetSoundEnabled(COptionsMgr.GetSoundEnabled());
          ICMediaPlayer.GetInstance().SetVibrationEnabled(COptionsMgr.GetVibrationEnabled());
          ICMediaPlayer.GetInstance().SetVolume(COptionsMgr.GetInstance().GetVolume());
          int num = (int) this.m_pRootNode.Start();
          flag = true;
          goto case 1767879702;
        case 1767879702:
          return flag;
        case 1967276899:
          ICMediaPlayer.GetInstance().HandleResume();
          this.m_pRootNode.HandleEvent(id, param1, param2);
          flag = true;
          goto case 1767879702;
        default:
          flag = this.m_pRootNode.HandleEvent(id, param1, param2);
          goto case 1767879702;
      }
    }

    public int GetSoftkeyOffsetX(bool left)
    {
      CRectangle pScreen;
      Phone.GetScreen(out pScreen);
      return left ? (pScreen.m_dx > pScreen.m_dy ? (this.lsk_landscape_x != (int) sbyte.MinValue ? this.lsk_landscape_x : 0) : (this.lsk_portrait_x != (int) sbyte.MinValue ? this.lsk_portrait_x : 0)) : (pScreen.m_dx > pScreen.m_dy ? (this.rsk_landscape_x != (int) sbyte.MinValue ? this.rsk_landscape_x : 0) : (this.rsk_portrait_x != (int) sbyte.MinValue ? this.rsk_portrait_x : 0));
    }
  }
}
