// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CApplet
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace com.glu.shared
{
  public class CApplet : Game
  {
    public GamerServicesComponent gamerServiceInstance;
    public static bool displayTitleUpdateMessage;
    public string m_gameNameString = "GuitarHero5";
    private object m_saveObject;
    protected static CApplet m_instance;
    protected static CApplet.CreateApp m_createApp;
    protected static CApp m_app;
    protected static CApplet.CreateAppFactory m_createAppFactory;
    protected GraphicsDeviceManager m_graphics_mgr_xna;
    public CSystemEventQueue m_eventQueue = new CSystemEventQueue();
    public GameTime m_gameTime_xna;

    public static CApplet GetInstance() => CApplet.m_instance;

    protected override void OnActivated(object sender, EventArgs events)
    {
      base.OnActivated(sender, events);
    }

    protected override void OnDeactivated(object sender, EventArgs events)
    {
      base.OnDeactivated(sender, events);
      this.m_eventQueue.Queue(1411673571U, 0U, 0U);
      this.m_eventQueue.Queue(1967276899U, 0U, 0U);
    }

    protected override void OnExiting(object sender, EventArgs events) => this.ExitApp();

    protected CApplet(CApplet.CreateApp createApp, CApplet.CreateAppFactory createAppFactory)
    {
      CApplet.m_createApp = createApp;
      CApplet.m_createAppFactory = createAppFactory;
      CApplet.m_instance = this;
      CApplet.m_app = CApplet.m_createApp();
      this.m_graphics_mgr_xna = new GraphicsDeviceManager((Game) this);
      this.m_graphics_mgr_xna.PreferredBackBufferWidth = 320;
      this.m_graphics_mgr_xna.PreferredBackBufferHeight = 533;
      this.m_graphics_mgr_xna.IsFullScreen = true;
      this.Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      CApplet.m_app = CApplet.m_createApp();
      if (CApplet.m_app == null)
        throw new cRC_Exception("CApplet:", 1U, ":Unable to create CApp");
      CApplet.m_createApp = (CApplet.CreateApp) null;
      CAppFactory cappFactory = CApplet.m_createAppFactory();
      if (CAppFactory.GetInstance() == null)
        throw new cRC_Exception("CApplet:", 1U, ":Unable to create CAppFactory");
      CApplet.m_createAppFactory = (CApplet.CreateAppFactory) null;
      uint rc = CApplet.m_app.OnInit();
      if (rc != 0U)
        throw new cRC_Exception("CApplet:", rc, ":Unable to initialize CApp");
      base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      this.m_gameTime_xna = gameTime;
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.m_eventQueue.Queue(3563016926U, 0U, 0U);
      this.generateEvents();
      this.processEvents();
      CAppExecutor executor = CApp.GetExecutor();
      executor.SetMode(CAppExecutor.Mode.Update);
      executor.Run();
      try
      {
        base.Update(gameTime);
      }
      catch (GameUpdateRequiredException ex)
      {
        this.HandleGameUpdateRequired(ex);
      }
    }

    private void HandleGameUpdateRequired(GameUpdateRequiredException e)
    {
      this.gamerServiceInstance.Enabled = false;
      CApplet.displayTitleUpdateMessage = true;
    }

    protected override void Draw(GameTime gameTime)
    {
      this.m_gameTime_xna = gameTime;
      CAppExecutor executor = CApp.GetExecutor();
      executor.SetMode(CAppExecutor.Mode.Render);
      executor.Run();
      base.Draw(gameTime);
    }

    protected void processEvents()
    {
      for (int idx = 0; idx < this.m_eventQueue.GetCount(); ++idx)
      {
        CSystemEventQueue.SystemEvent outValue = new CSystemEventQueue.SystemEvent();
        this.m_eventQueue.GetEvent(ref outValue, idx);
        CApplet.m_app.OnSystemEvent(outValue.m_id, outValue.m_param1, outValue.m_param2);
      }
      this.m_eventQueue.Clear();
    }

    protected void generateEvents()
    {
      if (CApp.GetExecutor().GetModeQualifier() == CAppExecutor.ModeQualifier.Starting)
        this.m_eventQueue.Queue(1733568088U, 0U, 0U);
      if (!TouchPanel.GetCapabilities().IsConnected)
        return;
      uint eventId = 0;
      foreach (TouchLocation touchLocation in TouchPanel.GetState())
      {
        switch (touchLocation.State)
        {
          case TouchLocationState.Released:
            eventId = 902008092U;
            break;
          case TouchLocationState.Pressed:
            eventId = 902053462U;
            break;
          case TouchLocationState.Moved:
            eventId = 902532892U;
            break;
        }
        int x = (int) touchLocation.Position.X;
        int y = (int) touchLocation.Position.Y;
        this.TransposeCoordinatesForScreenOrientation(ref x, ref y);
        this.ScaleCoordinates(ref x, ref y);
        uint num1 = 0;
        uint num2 = TouchUtil.TOUCH_EVENT_SET_Y(TouchUtil.TOUCH_EVENT_SET_X(0U, x), y);
        this.m_eventQueue.Queue(eventId, num1, num2);
      }
    }

    protected void TransposeCoordinatesForScreenOrientation(ref int x, ref int y)
    {
    }

    protected void ScaleCoordinates(ref int x, ref int y)
    {
    }

    public void ExitApp() => this.Exit();

    public void QueueSystemEvent(uint id) => this.QueueSystemEvent(id, 0U, 0U);

    public void QueueSystemEvent(uint id, uint param1) => this.QueueSystemEvent(id, param1, 0U);

    public void QueueSystemEvent(uint id, uint param1, uint param2)
    {
      CApplet.GetInstance().m_eventQueue.Queue(id, param1, param2);
    }

    public void SetAccelerometerFrequency(uint freqHz)
    {
      CApp.GetInstance().m_pAccelerometer.SetAccelerometerFrequency(freqHz);
    }

    public delegate CApp CreateApp();

    public delegate CAppFactory CreateAppFactory();
  }
    // Add this class definition to resolve CS0246 if GameUpdateRequiredException is not defined elsewhere.
    // Place it in an appropriate location, such as at the end of the file or in a separate file if needed.
    public class GameUpdateRequiredException : Exception
    {
        public GameUpdateRequiredException() { }
        public GameUpdateRequiredException(string message) : base(message) { }
        public GameUpdateRequiredException(string message, Exception inner) : base(message, inner) { }
    }
}
