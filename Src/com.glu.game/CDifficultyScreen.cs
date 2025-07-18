// Decompiled with JetBrains decompiler
// Type: com.glu.game.CDifficultyScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CDifficultyScreen : CWidgetScreen
  {
    private const int kDifficultyEasyID = 1;
    private const int kDifficultyMediumID = 2;
    private const int kDifficultyHardID = 3;
    protected ICRenderSurface m_pLeft;
    protected ICRenderSurface m_pRight;
    protected CNavigatorWidget m_easyDifficultyNavigator;
    protected CNavigatorWidget m_mediumDifficultyNavigator;
    protected CNavigatorWidget m_hardDifficultyNavigator;
    protected string m_strEasyDifficulty;
    protected string m_strMediumDifficulty;
    protected string m_strHardDifficulty;

    public CDifficultyScreen()
    {
      this.m_pLeft = (ICRenderSurface) null;
      this.m_pRight = (ICRenderSurface) null;
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
      ICMediaPlayer.GetInstance();
      bool flag = false;
      switch (id)
      {
        case 129075783:
          switch (((CWidget) param2).GetID())
          {
            case 1:
              CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EASY;
              break;
            case 2:
              CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_MEDIUM;
              break;
            case 3:
              CGHStaticData.m_difficultyLevel = CGHStaticData.eDifficulty.GAME_DIFFICULTY_EXPERT;
              break;
          }
          this.SetInterrupt(1);
          flag = true;
          break;
        case 555763780:
          this.SetInterrupt(2);
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
      if (resource1 == null)
        return;
      this.m_pRight = (ICRenderSurface) resource1.GetData();
    }

    public override void ReleaseResources()
    {
      base.ReleaseResources();
      CResourceManager resourceManager = CApp.GetResourceManager();
      resourceManager.ReleaseResource("SUR_ARROW_LEFT");
      resourceManager.ReleaseResource("SUR_ARROW_RIGHT");
    }

    public override void Build()
    {
      base.Build();
      CFontMgr instance = CFontMgr.GetInstance();
      this.m_page.SetWrap(true);
      this.m_easyDifficultyNavigator.SetID(1);
      this.m_easyDifficultyNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_easyDifficultyNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_easyDifficultyNavigator.SetFocusable(true);
      this.m_easyDifficultyNavigator.SetSelectable(true);
      this.m_easyDifficultyNavigator.SetPageCount(1);
      this.m_easyDifficultyNavigator.SetSelectionIndex(0);
      this.m_easyDifficultyNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_easyDifficultyNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_page.Add((CUIWidget) this.m_easyDifficultyNavigator);
      this.m_mediumDifficultyNavigator.SetID(2);
      this.m_mediumDifficultyNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_mediumDifficultyNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_mediumDifficultyNavigator.SetFocusable(true);
      this.m_mediumDifficultyNavigator.SetSelectable(true);
      this.m_mediumDifficultyNavigator.SetPageCount(1);
      this.m_mediumDifficultyNavigator.SetSelectionIndex(0);
      this.m_mediumDifficultyNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_mediumDifficultyNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_page.Add((CUIWidget) this.m_mediumDifficultyNavigator);
      this.m_hardDifficultyNavigator.SetID(3);
      this.m_hardDifficultyNavigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_hardDifficultyNavigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_hardDifficultyNavigator.SetPageCount(1);
      this.m_hardDifficultyNavigator.SetFocusable(true);
      this.m_hardDifficultyNavigator.SetSelectable(true);
      this.m_hardDifficultyNavigator.SetSelectionIndex(0);
      this.m_hardDifficultyNavigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_hardDifficultyNavigator.SetFont(instance.GetFont(CFontMgr.eGameFont.FONT_REGULARFONT));
      this.m_page.Add((CUIWidget) this.m_hardDifficultyNavigator);
    }

    public override void Layout()
    {
      base.Layout();
      this.BuildDifficultyText();
    }

    private void BuildDifficultyText()
    {
      CUtility.GetString(out this.m_strEasyDifficulty, "IDS_EASY");
      CUtility.GetString(out this.m_strMediumDifficulty, "IDS_MEDIUM");
      CUtility.GetString(out this.m_strHardDifficulty, "IDS_EXPERT");
      this.m_easyDifficultyNavigator.SetSingleText(this.m_strEasyDifficulty);
      this.m_mediumDifficultyNavigator.SetSingleText(this.m_strMediumDifficulty);
      this.m_hardDifficultyNavigator.SetSingleText(this.m_strHardDifficulty);
    }
  }
}
