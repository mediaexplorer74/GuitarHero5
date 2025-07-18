// Decompiled with JetBrains decompiler
// Type: com.glu.game.CTouchManager
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  internal class CTouchManager
  {
    public static int sm_pointerX;
    public static int sm_pointerY;
    public static int sm_realPointerX;
    public static int sm_realPointerY;
    public static bool sm_bPointerLatched;
    public static bool sm_bPointerUnlatched;
    public static bool sm_bPointerActive;
    public static bool sm_bRealPointerLatched;
    public static bool sm_bRealPointerUnlatched;
    public static bool sm_bRealPointerActive;
    public static bool sm_bCheck;
    public static int sm_startX;
    public static int sm_startY;
    public int FLICK_DISTANCE = 10;
    public byte MAX_TOUCH_GROUPS = 6;
    public static int sm_release;
    private static bool sm_bKBug = false;

    private void OnFree()
    {
    }

    private void init() => this.resetTouchVariables();

    private void pointerPressed(int x, int y)
    {
      CTouchManager.sm_realPointerX = x;
      CTouchManager.sm_realPointerY = y;
      CTouchManager.sm_bRealPointerActive = true;
      CTouchManager.sm_bRealPointerLatched = true;
      if (x >= 15 || y >= 15)
        return;
      CTouchManager.sm_bKBug = !CTouchManager.sm_bKBug;
    }

    private void pointerReleased(int x, int y)
    {
      CTouchManager.sm_realPointerX = x;
      CTouchManager.sm_realPointerY = y;
      CTouchManager.sm_bRealPointerActive = false;
      CTouchManager.sm_bRealPointerUnlatched = true;
    }

    private void pointerDragged(int x, int y)
    {
      CTouchManager.sm_realPointerX = x;
      CTouchManager.sm_realPointerY = y;
    }

    private bool addGroup(uint key, object group) => false;

    private void removeGroup(uint key)
    {
    }

    private void enableGroup(uint key)
    {
    }

    private void disableGroup(uint key)
    {
    }

    private void tick(int deltaTime)
    {
      CTouchManager.sm_bPointerLatched = CTouchManager.sm_bRealPointerLatched;
      CTouchManager.sm_bPointerUnlatched = CTouchManager.sm_bRealPointerUnlatched;
      if (!CTouchManager.sm_bPointerLatched && !CTouchManager.sm_bPointerUnlatched && CTouchManager.sm_pointerX == CTouchManager.sm_realPointerX && CTouchManager.sm_pointerY == CTouchManager.sm_realPointerY && !CTouchManager.sm_bCheck)
        return;
      CTouchManager.sm_bRealPointerLatched = false;
      CTouchManager.sm_bRealPointerUnlatched = false;
      CTouchManager.sm_pointerX = CTouchManager.sm_realPointerX;
      CTouchManager.sm_pointerY = CTouchManager.sm_realPointerY;
      CTouchManager.sm_bPointerActive = CTouchManager.sm_bRealPointerActive;
    }

    public static void handleCustomActions(CTouchArea area, int trigger, int param)
    {
    }

    private void drawActiveGroups()
    {
    }

    private void _print(char msg, int x, int y)
    {
    }

    private void resetTouchVariables()
    {
      CTouchManager.sm_bCheck = false;
      CTouchManager.sm_startX = -1;
      CTouchManager.sm_startY = -1;
      CTouchManager.sm_release = -1;
    }
  }
}
