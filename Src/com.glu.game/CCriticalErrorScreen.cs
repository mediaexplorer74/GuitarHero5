// Decompiled with JetBrains decompiler
// Type: com.glu.game.CCriticalErrorScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CCriticalErrorScreen : CSoftkeyScreen
  {
    protected CFont m_pFont;
    protected CTextWidget m_text = new CTextWidget();
    protected string m_pwszText;
    protected int m_advanceTimeMS;
    protected int m_timeElapsedMS;

    public CCriticalErrorScreen()
    {
      this.m_pwszText = (string) null;
      this.m_advanceTimeMS = 0;
      this.m_timeElapsedMS = 0;
    }

    public override void Activate() => base.Activate();

    public override void Deactivate() => base.Deactivate();

    private void SetInfo(CFont pFont, string pwszText, int advanceTimeMS)
    {
      this.m_pFont = pFont;
      this.m_pwszText = pwszText;
      this.m_advanceTimeMS = advanceTimeMS;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      this.m_timeElapsedMS += timeElapsedMS;
      if (this.m_timeElapsedMS >= this.m_advanceTimeMS)
        this.m_interrupt = 1;
      return true;
    }

    public override void Build()
    {
      base.Build();
      if (this.m_pFont == null || this.m_pwszText == null)
        return;
      this.m_text.SetID(0);
      this.m_text.SetAlignment(2);
      this.m_text.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_text.SetTransparent(false);
      this.m_text.SetFont(this.m_pFont);
      this.m_text.SetText(this.m_pwszText);
      this.m_base.AddChild((CUIWidget) this.m_text, 0);
    }

    public override void Layout() => this.m_text.SetRect(this.m_base.GetRect());
  }
}
