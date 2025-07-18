// Decompiled with JetBrains decompiler
// Type: com.glu.game.CImageScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CImageScreen : CWidgetScreen
  {
    protected new uint m_backgroundColor;
    protected string m_resId;
    protected int m_displayTimeMS;
    protected int m_timeElapsedMS;
    private ICRenderSurface m_pImage;
    protected CImageWidget m_image = new CImageWidget();

    public CImageScreen()
    {
      this.m_backgroundColor = Consts.Color_MakeA8R8G8B8((int) byte.MaxValue, 0, 0, 0);
      this.m_resId = (string) null;
      this.m_displayTimeMS = 0;
      this.m_timeElapsedMS = -1;
      this.m_pImage = (ICRenderSurface) null;
      this.SetFlags(0);
    }

    public void SetInfo(uint backgroundColor, string resId, int displayTimeMS)
    {
      this.m_backgroundColor = backgroundColor;
      base.m_backgroundColor = this.m_backgroundColor;
      this.m_resId = resId;
      this.m_displayTimeMS = displayTimeMS;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 129075783:
        case 544526345:
          this.SetInterrupt(1);
          flag = true;
          break;
        case 555763780:
          this.SetInterrupt(2);
          flag = true;
          break;
        case 850690755:
          this.SetInterrupt(1);
          flag = true;
          break;
        case 902053462:
        case 2300082508:
          this.SetInterrupt(1);
          flag = true;
          break;
      }
      if (!flag)
        flag = base.HandleEvent(id, param1, param2);
      return flag;
    }

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      if (this.m_timeElapsedMS < 0)
      {
        this.m_timeElapsedMS = 0;
      }
      else
      {
        this.m_timeElapsedMS += timeElapsedMS;
        if (this.m_timeElapsedMS >= this.m_displayTimeMS)
          this.SetInterrupt(1);
      }
      return true;
    }

    public override void CreateResources()
    {
      base.CreateResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      if (this.m_resId == null)
        return;
      int resource2 = (int) resourceManager.CreateResource(this.m_resId, out resource1);
      if (resource1 == null)
        return;
      this.m_pImage = (ICRenderSurface) resource1.GetData();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      if (this.m_resId == null)
        return;
      resourceManager.ReleaseResource(this.m_resId);
    }

    public override void Build()
    {
      base.Build();
      if (this.m_pImage == null)
        return;
      this.m_image.SetID(0);
      this.m_image.SetColor(this.m_backgroundColor, uint.MaxValue, 4278233031U);
      this.m_image.SetImage(this.m_pImage);
      this.m_base.AddChild((CUIWidget) this.m_image, 0);
    }

    public override void Layout()
    {
      base.Layout();
      CRectangle rect = this.m_base.GetRect();
      this.m_image.SetRect(rect);
      this.m_image.HandleLayout();
      rect.m_x = (int) (short) (rect.m_dx - this.m_image.GetContentWidth() >> 1);
      if ((this.m_flags & 22) == 0 || rect.m_dy <= this.m_image.GetContentHeight())
        rect.m_y = (this.m_flags & 8) == 0 ? (int) (short) (rect.m_dy - this.m_image.GetContentHeight() >> 1) : (int) (short) (rect.m_dy - this.m_image.GetContentHeight());
      rect.m_dx = (int) (short) this.m_image.GetContentWidth();
      rect.m_dy = (int) (short) this.m_image.GetContentHeight();
      this.m_image.SetRect(rect);
    }
  }
}
