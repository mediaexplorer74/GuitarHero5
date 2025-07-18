// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSoftkeyScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CSoftkeyScreen : CNode
  {
    protected string m_softkeyIDLeft;
    protected string m_softkeyIDRight;
    protected bool m_softkeyImageLeft;
    protected bool m_softkeyImageRight;
    protected string m_pSoftkeyLeftText;
    protected ICRenderSurface m_pSoftkeyLeftImage;
    protected string m_pSoftkeyRightText;
    protected ICRenderSurface m_pSoftkeyRightImage;
    protected CContainerWidget m_base;
    protected CSoftkeyWidget m_softkey;
    protected bool m_renderBeginAndEndIsInternallyHandled;

    public CSoftkeyScreen()
    {
      this.m_softkeyIDLeft = (string) null;
      this.m_softkeyIDRight = (string) null;
      this.m_base = new CContainerWidget();
      this.m_softkeyImageLeft = true;
      this.m_softkeyImageRight = true;
      this.m_pSoftkeyLeftText = (string) null;
      this.m_pSoftkeyLeftImage = (ICRenderSurface) null;
      this.m_pSoftkeyRightText = (string) null;
      this.m_pSoftkeyRightImage = (ICRenderSurface) null;
      this.m_renderBeginAndEndIsInternallyHandled = true;
      this.m_softkey = new CSoftkeyWidget();
    }

    public void SetSoftkeys(string idLeft, string idRight)
    {
      this.SetSoftkeys(idLeft, idRight, true, true);
    }

    public void SetSoftkeys(string idLeft, string idRight, bool imageLeft, bool imageRight)
    {
      this.m_softkeyIDLeft = idLeft;
      this.m_softkeyIDRight = idRight;
      this.m_softkeyImageLeft = imageLeft;
      this.m_softkeyImageRight = imageRight;
    }

    public void UpdateSoftkeys(string idLeft, string idRight)
    {
      this.UpdateSoftkeys(idLeft, idRight, true, true);
    }

    public void UpdateSoftkeys(string idLeft, string idRight, bool imageLeft, bool imageRight)
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      bool flag = false;
      this.m_softkeyImageLeft = imageLeft;
      this.m_softkeyImageRight = imageRight;
      if (this.m_softkeyIDLeft != idLeft)
      {
        if (this.m_softkeyIDLeft != null)
          resourceManager.ReleaseResource(this.m_softkeyIDLeft);
        this.m_softkeyIDLeft = idLeft;
        if (this.m_softkeyIDLeft != null)
        {
          int resource2 = (int) resourceManager.CreateResource(this.m_softkeyIDLeft, out resource1);
          if (resource1 != null)
          {
            if (this.m_softkeyImageLeft)
              this.m_pSoftkeyLeftImage = (ICRenderSurface) resource1.GetData();
            else
              this.m_pSoftkeyLeftText = ((CStrChar) resource1.GetData()).ToString();
          }
        }
        else
        {
          this.m_pSoftkeyLeftImage = (ICRenderSurface) null;
          this.m_pSoftkeyLeftText = (string) null;
        }
        flag = true;
      }
      if (this.m_softkeyIDRight != idRight)
      {
        if (this.m_softkeyIDRight != null)
          resourceManager.ReleaseResource(this.m_softkeyIDRight);
        this.m_softkeyIDRight = idRight;
        if (this.m_softkeyIDRight != null)
        {
          int resource3 = (int) resourceManager.CreateResource(this.m_softkeyIDRight, out resource1);
          if (resource1 != null)
          {
            if (this.m_softkeyImageRight)
              this.m_pSoftkeyRightImage = (ICRenderSurface) resource1.GetData();
            else
              this.m_pSoftkeyRightText = ((CStrChar) resource1.GetData()).ToString();
          }
        }
        else
        {
          this.m_pSoftkeyRightImage = (ICRenderSurface) null;
          this.m_pSoftkeyRightText = (string) null;
        }
        flag = true;
      }
      if (!flag)
        return;
      this.m_softkey.SetLeftImage(this.m_pSoftkeyLeftImage);
      this.m_softkey.SetRightImage(this.m_pSoftkeyRightImage);
    }

    public override uint Start()
    {
      this.CreateResources();
      this.Build();
      this.Layout();
      return 0;
    }

    public override void Stop() => this.ReleaseResources();

    public override void Activate() => this.m_softkey.SetVisible(true);

    public override void Deactivate() => this.m_softkey.SetVisible(false);

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 129075783:
          this.SetInterrupt(1);
          flag = true;
          goto case 1053973641;
        case 555763780:
          this.SetInterrupt(2);
          flag = true;
          goto case 1053973641;
        case 850690755:
          flag = true;
          goto case 1053973641;
        case 1053973641:
          return flag;
        default:
          flag = this.m_base.HandleEvent(id, param1, param2);
          goto case 1053973641;
      }
    }

    public override bool HandleUpdate(int timeElapsedMS) => this.m_base.HandleUpdate(timeElapsedMS);

    public override bool HandleRender()
    {
      if (this.m_renderBeginAndEndIsInternallyHandled)
        this.RenderBegin();
      bool flag = this.m_base.HandleRender();
      if (this.m_renderBeginAndEndIsInternallyHandled)
        this.RenderEnd();
      return flag;
    }

    public virtual void CreateResources()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_softkeyIDLeft != null)
      {
        int resource2 = (int) resourceManager.CreateResource(this.m_softkeyIDLeft, out resource1);
        if (resource1 != null)
        {
          if (this.m_softkeyImageLeft)
            this.m_pSoftkeyLeftImage = (ICRenderSurface) resource1.GetData();
          else
            this.m_pSoftkeyLeftText = ((CStrChar) resource1.GetData()).ToString();
        }
      }
      if (this.m_softkeyIDRight == null)
        return;
      int resource3 = (int) resourceManager.CreateResource(this.m_softkeyIDRight, out resource1);
      if (resource1 == null)
        return;
      if (this.m_softkeyImageRight)
        this.m_pSoftkeyRightImage = (ICRenderSurface) resource1.GetData();
      else
        this.m_pSoftkeyRightText = ((CStrChar) resource1.GetData()).ToString();
    }

    public virtual void ReleaseResources()
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      if (this.m_softkeyIDLeft != null)
        resourceManager.ReleaseResource(this.m_softkeyIDLeft);
      if (this.m_softkeyIDRight == null)
        return;
      resourceManager.ReleaseResource(this.m_softkeyIDRight);
    }

    public virtual void Build()
    {
      CFontMgr instance = CFontMgr.GetInstance();
      this.m_base.SetID(0);
      this.m_base.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_base.SetTransparent(false);
      this.m_base.SetFocusable(true);
      this.m_base.SetSelectable(true);
      if (ICCore.GetInstance().GetSoftkeys() && (this.m_softkeyIDLeft != null || this.m_softkeyIDRight != null))
      {
        this.m_softkey.SetID(0);
        this.m_softkey.SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_softkey.SetTransparent(true);
        if (this.m_softkeyIDLeft != null)
        {
          if (this.m_pSoftkeyLeftImage != null)
            this.m_softkey.SetLeftImage(this.m_pSoftkeyLeftImage);
          else if (this.m_pSoftkeyLeftText != null)
          {
            this.m_softkey.SetLeftFont(instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
            this.m_softkey.SetLeftText(this.m_pSoftkeyLeftText);
          }
        }
        if (this.m_softkeyIDRight != null)
        {
          if (this.m_softkeyImageRight)
            this.m_softkey.SetRightImage(this.m_pSoftkeyRightImage);
          else if (this.m_pSoftkeyRightText != null)
          {
            this.m_softkey.SetRightFont(instance.GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
            this.m_softkey.SetRightText(this.m_pSoftkeyRightText);
          }
        }
      }
      this.m_base.AddChild((CUIWidget) this.m_softkey, int.MaxValue);
      this.m_base.SetFocus(true);
      this.m_base.SetSelection(true);
    }

    public static eSoftkeyPosition GetSoftkeyPosition(int keyCode)
    {
      return keyCode != 15 ? eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_RIGHT : eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT;
    }

    public virtual void Layout()
    {
      CRectangle rect1 = new CRectangle();
      CRectangle rect2 = new CRectangle();
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      rect1.m_x = 0;
      rect1.m_y = 0;
      rect1.m_dx = (int) (short) width;
      rect1.m_dy = (int) (short) height;
      this.m_base.SetRect(rect1);
      this.m_softkey.SetRect(rect1);
      this.m_softkey.HandleLayout();
      eSoftkeyPosition softkeyPosition1 = CSoftkeyScreen.GetSoftkeyPosition(15);
      eSoftkeyPosition softkeyPosition2 = CSoftkeyScreen.GetSoftkeyPosition(16);
      this.m_softkey.SetLeftPosition(softkeyPosition1);
      this.m_softkey.SetRightPosition(softkeyPosition2);
      if (softkeyPosition1 == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT && softkeyPosition2 == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_RIGHT)
      {
        rect2.m_x = rect1.m_x;
        rect2.m_y = rect1.m_y + rect1.m_dy - this.m_softkey.GetContentHeight();
        rect2.m_dx = rect1.m_dx;
        rect2.m_dy = this.m_softkey.GetContentHeight();
      }
      else if (softkeyPosition1 == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_RIGHT && softkeyPosition2 == eSoftkeyPosition.SOFTKEY_POSITION_TOP_RIGHT)
      {
        rect2.m_x = rect1.m_x + rect1.m_dx - this.m_softkey.GetContentWidth();
        rect2.m_y = rect1.m_y;
        rect2.m_dx = this.m_softkey.GetContentWidth();
        rect2.m_dy = rect1.m_dy;
      }
      else if (softkeyPosition1 == eSoftkeyPosition.SOFTKEY_POSITION_TOP_RIGHT && softkeyPosition2 == eSoftkeyPosition.SOFTKEY_POSITION_TOP_LEFT)
      {
        rect2.m_x = rect1.m_x;
        rect2.m_y = rect1.m_y;
        rect2.m_dx = rect1.m_dx;
        rect2.m_dy = this.m_softkey.GetContentHeight();
      }
      else if (softkeyPosition1 == eSoftkeyPosition.SOFTKEY_POSITION_TOP_LEFT && softkeyPosition2 == eSoftkeyPosition.SOFTKEY_POSITION_BOTTOM_LEFT)
      {
        rect2.m_x = rect1.m_x;
        rect2.m_y = rect1.m_y;
        rect2.m_dx = this.m_softkey.GetContentWidth();
        rect2.m_dy = rect1.m_dy;
      }
      this.m_softkey.SetRect(rect2);
    }

    public override void SyncalSystemsWithRenderBegin()
    {
    }

    public enum eKeyCode
    {
      KEYCODE_NONE = 0,
      KEYCODE_POWER = 1,
      KEYCODE_TALK = 2,
      KEYCODE_END = 3,
      KEYCODE_UP = 4,
      KEYCODE_DOWN = 5,
      KEYCODE_LEFT = 6,
      KEYCODE_RIGHT = 7,
      KEYCODE_BACKSPACE = 8,
      KEYCODE_TAB = 9,
      KEYCODE_LINE_FEED = 10, // 0x0000000A
      KEYCODE_ACTION = 11, // 0x0000000B
      KEYCODE_BACK = 12, // 0x0000000C
      KEYCODE_GAMING_A = 13, // 0x0000000D
      KEYCODE_GAMING_B = 14, // 0x0000000E
      KEYCODE_SOFT1 = 15, // 0x0000000F
      KEYCODE_SOFT2 = 16, // 0x00000010
      KEYCODE_VOLUME_UP = 17, // 0x00000011
      KEYCODE_VOLUME_DOWN = 18, // 0x00000012
      KEYCODE_CARRIAGE_RETURN = 19, // 0x00000013
      KEYCODE_FUNCTION = 20, // 0x00000014
      KEYCODE_SPACE = 32, // 0x00000020
      KEYCODE_EXCLAMATION_MARK = 33, // 0x00000021
      KEYCODE_QUOTATION_MARK = 34, // 0x00000022
      KEYCODE_HASH = 35, // 0x00000023
      KEYCODE_CURRENCY = 36, // 0x00000024
      KEYCODE_PERCENT = 37, // 0x00000025
      KEYCODE_AMPERSAND = 38, // 0x00000026
      KEYCODE_APOSTROPHE = 39, // 0x00000027
      KEYCODE_LEFT_PARENTHESIS = 40, // 0x00000028
      KEYCODE_RIGHT_PARENTHESIS = 41, // 0x00000029
      KEYCODE_STAR = 42, // 0x0000002A
      KEYCODE_PLUS = 43, // 0x0000002B
      KEYCODE_COMMA = 44, // 0x0000002C
      KEYCODE_HYPHEN = 45, // 0x0000002D
      KEYCODE_PERIOD = 46, // 0x0000002E
      KEYCODE_FORWARD_SLASH = 47, // 0x0000002F
      KEYCODE_0 = 48, // 0x00000030
      KEYCODE_1 = 49, // 0x00000031
      KEYCODE_2 = 50, // 0x00000032
      KEYCODE_3 = 51, // 0x00000033
      KEYCODE_4 = 52, // 0x00000034
      KEYCODE_5 = 53, // 0x00000035
      KEYCODE_6 = 54, // 0x00000036
      KEYCODE_7 = 55, // 0x00000037
      KEYCODE_8 = 56, // 0x00000038
      KEYCODE_9 = 57, // 0x00000039
      KEYCODE_COLON = 58, // 0x0000003A
      KEYCODE_SEMICOLON = 59, // 0x0000003B
      KEYCODE_LESS_THAN = 60, // 0x0000003C
      KEYCODE_EQUALS = 61, // 0x0000003D
      KEYCODE_GREATER_THAN = 62, // 0x0000003E
      KEYCODE_QUESTION = 63, // 0x0000003F
      KEYCODE_AT = 64, // 0x00000040
      KEYCODE_A = 65, // 0x00000041
      KEYCODE_B = 66, // 0x00000042
      KEYCODE_C = 67, // 0x00000043
      KEYCODE_D = 68, // 0x00000044
      KEYCODE_E = 69, // 0x00000045
      KEYCODE_F = 70, // 0x00000046
      KEYCODE_G = 71, // 0x00000047
      KEYCODE_H = 72, // 0x00000048
      KEYCODE_I = 73, // 0x00000049
      KEYCODE_J = 74, // 0x0000004A
      KEYCODE_K = 75, // 0x0000004B
      KEYCODE_L = 76, // 0x0000004C
      KEYCODE_M = 77, // 0x0000004D
      KEYCODE_N = 78, // 0x0000004E
      KEYCODE_O = 79, // 0x0000004F
      KEYCODE_P = 80, // 0x00000050
      KEYCODE_Q = 81, // 0x00000051
      KEYCODE_R = 82, // 0x00000052
      KEYCODE_S = 83, // 0x00000053
      KEYCODE_T = 84, // 0x00000054
      KEYCODE_U = 85, // 0x00000055
      KEYCODE_V = 86, // 0x00000056
      KEYCODE_W = 87, // 0x00000057
      KEYCODE_X = 88, // 0x00000058
      KEYCODE_Y = 89, // 0x00000059
      KEYCODE_Z = 90, // 0x0000005A
      KEYCODE_LEFT_SQUARE_BRACKET = 91, // 0x0000005B
      KEYCODE_BACKWARD_SLASH = 92, // 0x0000005C
      KEYCODE_RIGHT_SQUARE_BRACKET = 93, // 0x0000005D
      KEYCODE_CARET = 94, // 0x0000005E
      KEYCODE_UNDERSCORE = 95, // 0x0000005F
      KEYCODE_GRAVE_ACCENT = 96, // 0x00000060
      KEYCODE_a = 97, // 0x00000061
      KEYCODE_b = 98, // 0x00000062
      KEYCODE_c = 99, // 0x00000063
      KEYCODE_d = 100, // 0x00000064
      KEYCODE_e = 101, // 0x00000065
      KEYCODE_f = 102, // 0x00000066
      KEYCODE_g = 103, // 0x00000067
      KEYCODE_h = 104, // 0x00000068
      KEYCODE_i = 105, // 0x00000069
      KEYCODE_j = 106, // 0x0000006A
      KEYCODE_k = 107, // 0x0000006B
      KEYCODE_l = 108, // 0x0000006C
      KEYCODE_m = 109, // 0x0000006D
      KEYCODE_n = 110, // 0x0000006E
      KEYCODE_o = 111, // 0x0000006F
      KEYCODE_p = 112, // 0x00000070
      KEYCODE_q = 113, // 0x00000071
      KEYCODE_r = 114, // 0x00000072
      KEYCODE_s = 115, // 0x00000073
      KEYCODE_t = 116, // 0x00000074
      KEYCODE_u = 117, // 0x00000075
      KEYCODE_v = 118, // 0x00000076
      KEYCODE_w = 119, // 0x00000077
      KEYCODE_x = 120, // 0x00000078
      KEYCODE_y = 121, // 0x00000079
      KEYCODE_z = 122, // 0x0000007A
      KEYCODE_LEFT_CURLY_BRACKET = 123, // 0x0000007B
      KEYCODE_PIPE = 124, // 0x0000007C
      KEYCODE_RIGHT_CURLY_BRACKET = 125, // 0x0000007D
      KEYCODE_TILDE = 126, // 0x0000007E
      KEYCODE_DEL = 127, // 0x0000007F
      KEYCODE_LAST = 128, // 0x00000080
    }
  }
}
