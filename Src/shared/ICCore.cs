// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICCore
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class ICCore : CSingleton
  {
    public const int DEVICE_SCROLLBAR_WIDTH_MIN = 4;
    public const int DEVICE_SCROLLBAR_WIDTH_MAX = 6;
    protected new static CSingleton m_instance;
    protected int m_scrollbarWidth;
    protected uint m_ramSizeBytes;
    protected uint m_pppLingerMS;
    protected bool m_softkeys;
    protected bool m_canPlaySound;
    protected bool m_canVibrate;
    protected bool m_canPlaySoundAndVibrateSimultaneously;
    protected byte m_defaultVolume;
    protected ICCore.WindowOrientationCaps m_windowOrientationCaps;
    protected ICCore.WindowOrientationReinterpretationOfTheDisplay m_windowOrientationReinterpretationOfTheDisplayState;
    protected bool m_soundEnabled;
    protected bool m_vibrationEnabled;
    public CVector2d m_windowToDisplayScale = new CVector2d();
    public CVector2d m_displayToWindowScale = new CVector2d();

    public ICCore CreateInstance() => ICCore.GetInstance();

    public static ICCore GetInstance()
    {
      if (ICCore.m_instance == null)
        ICCore.m_instance = (CSingleton) new ICCore();
      return ICCore.m_instance as ICCore;
    }

    public ICCore()
    {
      this.m_scrollbarWidth = 6;
      this.m_ramSizeBytes = 32068000U;
      this.m_pppLingerMS = 30000U;
      this.m_softkeys = true;
      this.m_canPlaySound = true;
      this.m_canVibrate = false;
      this.m_canPlaySoundAndVibrateSimultaneously = false;
      this.m_defaultVolume = (byte) 3;
      this.m_soundEnabled = true;
      this.m_vibrationEnabled = true;
      this.m_windowToDisplayScale.Set(65536, 65536);
      this.m_displayToWindowScale.Set(65536, 65536);
    }

    public int GetScrollbarWidth() => this.m_scrollbarWidth;

    public uint GetRamSizeBytes() => this.m_ramSizeBytes;

    public uint GetPPPLingerMS() => this.m_pppLingerMS;

    public bool GetSoftkeys() => this.m_softkeys;

    public bool CanPlaySound() => this.m_canPlaySound;

    public bool CanVibrate() => this.m_canVibrate;

    public bool CanPlaySoundAndVibrateSimultaneously()
    {
      return this.m_canPlaySoundAndVibrateSimultaneously;
    }

    public byte GetDefaultVolume() => this.m_defaultVolume;

    public bool IsSoundEnabled() => this.m_soundEnabled;

    public bool IsVibrationEnabled() => this.m_vibrationEnabled;

    public bool IsWindowOrientationSupported(ICCore.eWindowOrientation orientation)
    {
      return this.m_windowOrientationCaps.IsSupported(orientation);
    }

    public bool CanWindowOrientationBeAReinterpretationOfTheDisplay()
    {
      return this.m_windowOrientationCaps.CanBeAReinterpretationOfTheDisplay();
    }

    public ICCore.WindowOrientationReinterpretationOfTheDisplay GetStateOfWindowOrientationReinterpretationOfTheDisplay()
    {
      return this.m_windowOrientationReinterpretationOfTheDisplayState;
    }

    public void ExitApp(ICCore.eExitStatus exitStatus) => CApplet.GetInstance().ExitApp();

    public void ExitApp() => CApplet.GetInstance().ExitApp();

    public bool LaunchURL(string wcsUrl) => false;

    public bool CanDetectLanguage() => false;

    public bool DetectLanguage(string outValue) => false;

    public bool CanDetectLocale() => false;

    public bool DetectLocale(string outValue)
    {
      outValue = "";
      return false;
    }

    public bool ApplicationCanSetOrientation()
    {
      return this.CanWindowOrientationBeAReinterpretationOfTheDisplay();
    }

    public ICCore.eDeviceOrientation GetDeviceOrientation()
    {
      return !this.GetStateOfWindowOrientationReinterpretationOfTheDisplay().m_isActive ? ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0 : this.WindowOrientationToDeviceOrientation(this.GetStateOfWindowOrientationReinterpretationOfTheDisplay().m_orientation);
    }

    public bool CanSetDeviceOrientation(ICCore.eDeviceOrientation orientation)
    {
      return this.IsWindowOrientationSupported(this.DeviceOrientationToWindowOrientation(orientation));
    }

    public bool SetDeviceOrientation(ICCore.eDeviceOrientation orientation)
    {
      bool flag = false;
      if (this.CanSetDeviceOrientation(orientation) && this.SetWindowOrientationReinterpretationOfTheDisplay(true, this.DeviceOrientationToWindowOrientation(orientation)))
      {
        switch (orientation)
        {
          default:
            CApplet.GetInstance().QueueSystemEvent(1954198101U);
            CApplet.GetInstance().QueueSystemEvent(850690755U);
            CApplet.GetInstance().QueueSystemEvent(607208024U);
            break;
        }
      }
      return flag;
    }

    public ICCore.eSoftkeyPosition GetSoftkeyPosition(int keyCode)
    {
      return ICCore.eSoftkeyPosition.SOFTKEY_POSITION_UNKNOWN;
    }

    public static string GetDeviceIdentifier() => "Windows Phone 7";

    public static string GetPlatformIdentifier() => "XNA";

    public static string GetPlatformVersion() => "4.0";

    public static string GetPlatformName() => "XNA";

    public bool DeviceOrientationCanSetRotateLeft()
    {
      return this.CanSetDeviceOrientation(this.GetDeviceOrientationLeft(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationCanSetRotateRight()
    {
      return this.CanSetDeviceOrientation(this.GetDeviceOrientationRight(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationCanSetFlip()
    {
      return this.CanSetDeviceOrientation(this.GetDeviceOrientationFlip(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationCanSetDefault()
    {
      return this.CanSetDeviceOrientation(ICCore.eDeviceOrientation.DEVICE_ORIENTATION_DEFAULT);
    }

    public bool DeviceOrientationSetRotateLeft()
    {
      return this.SetDeviceOrientation(this.GetDeviceOrientationLeft(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationSetRotateRight()
    {
      return this.SetDeviceOrientation(this.GetDeviceOrientationRight(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationSetFlip()
    {
      return this.SetDeviceOrientation(this.GetDeviceOrientationFlip(this.GetDeviceOrientation()));
    }

    public bool DeviceOrientationSetDefault()
    {
      return this.SetDeviceOrientation(ICCore.eDeviceOrientation.DEVICE_ORIENTATION_DEFAULT);
    }

    public CVector2d GetDisplayToWindowScale() => this.m_displayToWindowScale;

    public CVector2d GetWindowToDisplayScale() => this.m_windowToDisplayScale;

    public ICCore.eDeviceOrientation GetDeviceOrientationLeft(
      ICCore.eDeviceOrientation currentOrientation)
    {
      ICCore.eDeviceOrientation deviceOrientationLeft = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_UNKNOWN;
      switch (currentOrientation)
      {
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0:
          deviceOrientationLeft = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90:
          deviceOrientationLeft = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180:
          deviceOrientationLeft = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270:
          deviceOrientationLeft = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0;
          break;
      }
      return deviceOrientationLeft;
    }

    public ICCore.eDeviceOrientation GetDeviceOrientationRight(
      ICCore.eDeviceOrientation currentOrientation)
    {
      ICCore.eDeviceOrientation orientationRight = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_UNKNOWN;
      switch (currentOrientation)
      {
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0:
          orientationRight = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90:
          orientationRight = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180:
          orientationRight = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270:
          orientationRight = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180;
          break;
      }
      return orientationRight;
    }

    public ICCore.eDeviceOrientation GetDeviceOrientationFlip(
      ICCore.eDeviceOrientation currentOrientation)
    {
      ICCore.eDeviceOrientation deviceOrientationFlip = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_UNKNOWN;
      switch (currentOrientation)
      {
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0:
          deviceOrientationFlip = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90:
          deviceOrientationFlip = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180:
          deviceOrientationFlip = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270:
          deviceOrientationFlip = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90;
          break;
      }
      return deviceOrientationFlip;
    }

    public ICCore.eDeviceOrientation WindowOrientationToDeviceOrientation(
      ICCore.eWindowOrientation windowOrientation)
    {
      ICCore.eDeviceOrientation deviceOrientation = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_UNKNOWN;
      switch (windowOrientation)
      {
        case ICCore.eWindowOrientation.WINDOW_ORIENTATION_0:
          deviceOrientation = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0;
          break;
        case ICCore.eWindowOrientation.WINDOW_ORIENTATION_90:
          deviceOrientation = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90;
          break;
        case ICCore.eWindowOrientation.WINDOW_ORIENTATION_180:
          deviceOrientation = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180;
          break;
        case ICCore.eWindowOrientation.WINDOW_ORIENTATION_270:
          deviceOrientation = ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270;
          break;
      }
      return deviceOrientation;
    }

    public ICCore.eWindowOrientation DeviceOrientationToWindowOrientation(
      ICCore.eDeviceOrientation deviceOrientation)
    {
      ICCore.eWindowOrientation windowOrientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_UNKNOWN;
      switch (deviceOrientation)
      {
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_0:
          windowOrientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_0;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_90:
          windowOrientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_90;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_180:
          windowOrientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_180;
          break;
        case ICCore.eDeviceOrientation.DEVICE_ORIENTATION_270:
          windowOrientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_270;
          break;
      }
      return windowOrientation;
    }

    public void SetWindowOrientationCaps(ICCore.WindowOrientationCaps caps)
    {
      this.m_windowOrientationCaps = caps;
    }

    public bool SetWindowOrientationReinterpretationOfTheDisplay(
      bool active,
      ICCore.eWindowOrientation orientation)
    {
      bool flag = active && this.CanWindowOrientationBeAReinterpretationOfTheDisplay() && this.IsWindowOrientationSupported(orientation) || this.IsWindowOrientationSupported(orientation);
      if (flag)
      {
        this.m_windowOrientationReinterpretationOfTheDisplayState.m_isActive = active;
        this.m_windowOrientationReinterpretationOfTheDisplayState.m_orientation = orientation;
      }
      return flag;
    }

    public void SetDisplayToWindowScale(CVector2d scale)
    {
      this.m_displayToWindowScale = scale;
      this.m_windowToDisplayScale = 65536 / scale;
    }

    public void SetWindowToDisplayScale(CVector2d scale)
    {
      this.m_windowToDisplayScale = scale;
      this.m_displayToWindowScale = 65536 / scale;
    }

    public void SetAccelerometerFrequency(uint freqHz)
    {
      CApplet.GetInstance().SetAccelerometerFrequency(freqHz);
    }

    public enum eExitStatus
    {
      EXIT_STATUS_OK,
      EXIT_STATUS_OUT_OF_MEMORY,
    }

    public enum eWindowOrientation
    {
      WINDOW_ORIENTATION_UNKNOWN,
      WINDOW_ORIENTATION_0,
      WINDOW_ORIENTATION_90,
      WINDOW_ORIENTATION_180,
      WINDOW_ORIENTATION_270,
    }

    public enum eDeviceOrientation
    {
      DEVICE_ORIENTATION_UNKNOWN,
      DEVICE_ORIENTATION_0,
      DEVICE_ORIENTATION_90,
      DEVICE_ORIENTATION_180,
      DEVICE_ORIENTATION_270,
      DEVICE_ORIENTATION_DEFAULT,
    }

    public enum eSoftkeyPosition
    {
      SOFTKEY_POSITION_UNKNOWN,
      SOFTKEY_POSITION_TOP_LEFT,
      SOFTKEY_POSITION_TOP_RIGHT,
      SOFTKEY_POSITION_BOTTOM_LEFT,
      SOFTKEY_POSITION_BOTTOM_RIGHT,
    }

    public class WindowOrientationCaps
    {
      public bool[] m_orientationSupport = new bool[5];
      public bool m_orientationCanBeAReinterpretationOfTheDisplay;
      public byte[] m_reserved = new byte[3];

      public WindowOrientationCaps()
      {
        this.m_orientationSupport[0] = false;
        this.m_orientationSupport[1] = true;
        this.m_orientationSupport[2] = false;
        this.m_orientationSupport[3] = false;
        this.m_orientationSupport[4] = false;
        this.m_orientationCanBeAReinterpretationOfTheDisplay = false;
      }

      public bool IsSupported(ICCore.eWindowOrientation orientation)
      {
        return this.m_orientationSupport[(int) orientation];
      }

      public bool CanBeAReinterpretationOfTheDisplay()
      {
        return this.m_orientationCanBeAReinterpretationOfTheDisplay;
      }
    }

    public class WindowOrientationReinterpretationOfTheDisplay
    {
      public bool m_isActive;
      public byte[] m_reserved = new byte[3];
      public ICCore.eWindowOrientation m_orientation;

      public WindowOrientationReinterpretationOfTheDisplay()
      {
        this.m_isActive = false;
        this.m_orientation = ICCore.eWindowOrientation.WINDOW_ORIENTATION_0;
      }
    }
  }
}
