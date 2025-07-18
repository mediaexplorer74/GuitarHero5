// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMarqueeText
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CMarqueeText
  {
    private uint kMarqueeIdleLeftMS = 2000;
    private uint kMarqueeIdleRightMS = 2000;
    private int kMarqueeRightSpeedMSpx = 100;
    private int kMarqueeLeftSpeedMSpx = 20;
    private string kTestCharacter = "n";
    private int kTestCharBaseWidth = CMathFixed.Int32ToFixed(15);
    protected CFont font;
    protected CRectangle rect;
    protected uint m_time;
    protected CMarqueeText.eMarqueeState state;
    protected string text;
    protected int textWidth;
    protected int velocityAdj;
    protected int xPos;
    protected int yPos;

    public void SetRect(CRectangle rect)
    {
      this.rect = rect;
      if (this.state != CMarqueeText.eMarqueeState.MARQUEE_STATE_START)
        return;
      this.xPos = rect.m_x;
    }

    public void SetYPos(int yPos) => this.yPos = yPos;

    public void Reset()
    {
      this.textWidth = 0;
      this.Restart();
    }

    protected void Restart()
    {
      this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_START;
      this.m_time = 0U;
      this.xPos = 0;
    }

    public CMarqueeText()
    {
      this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_START;
      this.xPos = 0;
      this.textWidth = 0;
      this.rect = new CRectangle(0, 0, 0, 0);
      this.font = (CFont) null;
      this.text = (string) null;
      this.m_time = 0U;
    }

    public void Paint()
    {
      ICGraphics2d.GetInstance();
      this.font.PaintText(this.text, -1, this.xPos, this.yPos);
    }

    public void HandleUpdate(int timeElapsedMS)
    {
      this.m_time += (uint) timeElapsedMS;
      switch (this.state)
      {
        case CMarqueeText.eMarqueeState.MARQUEE_STATE_START:
          this.xPos = this.rect.m_x;
          if (this.textWidth <= this.rect.m_dx)
            break;
          this.m_time = 0U;
          this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_IDLE_LEFT;
          break;
        case CMarqueeText.eMarqueeState.MARQUEE_STATE_IDLE_LEFT:
          if (this.m_time <= this.kMarqueeIdleLeftMS)
            break;
          this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_MOVING_RIGHT;
          this.m_time = 0U;
          break;
        case CMarqueeText.eMarqueeState.MARQUEE_STATE_MOVING_RIGHT:
          this.xPos -= (int) ((long) this.m_time / (long) CMathFixed.FixedToInt32((uint) (this.kMarqueeRightSpeedMSpx * this.velocityAdj)));
          if (this.xPos + this.textWidth >= this.rect.m_x + this.rect.m_dx)
            break;
          this.xPos = this.rect.m_x + this.rect.m_dx - this.textWidth;
          this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_IDLE_RIGHT;
          this.m_time = 0U;
          break;
        case CMarqueeText.eMarqueeState.MARQUEE_STATE_IDLE_RIGHT:
          if (this.m_time <= this.kMarqueeIdleRightMS)
            break;
          this.state = CMarqueeText.eMarqueeState.MARQUEE_STATE_MOVING_LEFT;
          this.m_time = 0U;
          break;
        case CMarqueeText.eMarqueeState.MARQUEE_STATE_MOVING_LEFT:
          this.xPos += (int) ((long) this.m_time / (long) CMathFixed.FixedToInt32((uint) (this.kMarqueeLeftSpeedMSpx * this.velocityAdj)));
          if (this.xPos <= this.rect.m_x)
            break;
          this.Restart();
          this.HandleUpdate(0);
          break;
      }
    }

    public void SetText(CFont pFont, string pText)
    {
      this.font = pFont;
      this.text = pText;
      this.textWidth = pText == null ? 0 : pFont.MeasureTextWidth(pText);
      if (pFont == null)
        return;
      this.velocityAdj = this.kTestCharBaseWidth / pFont.MeasureTextWidth(this.kTestCharacter, 1);
    }

    public enum eMarqueeState
    {
      MARQUEE_STATE_START,
      MARQUEE_STATE_IDLE_LEFT,
      MARQUEE_STATE_MOVING_RIGHT,
      MARQUEE_STATE_IDLE_RIGHT,
      MARQUEE_STATE_MOVING_LEFT,
      NUM_MARQUEE_STATES,
    }
  }
}
