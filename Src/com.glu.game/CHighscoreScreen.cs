// Decompiled with JetBrains decompiler
// Type: com.glu.game.CHighscoreScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CHighscoreScreen : CWidgetScreen
  {
    private const string kwszNoName = "xxx";
    protected CHighscoreScreen.Mode m_mode;
    protected string[] m_pNavID;
    protected int m_items;
    protected int m_curPage;
    protected ICRenderSurface m_pLeft;
    protected ICRenderSurface m_pRight;
    protected CNavigatorWidget m_navigator;
    protected CHighscoreTableWidget m_table;
    protected string m_strTableName;
    protected string[] m_pNames = new string[Consts.kMaxHighScores];
    protected int[] m_pScores = new int[Consts.kMaxHighScores];
    private static string kwcsHSFileName = "hs.dat";

    public CHighscoreScreen()
    {
      this.m_mode = CHighscoreScreen.Mode.HS_MODE_NONE;
      this.m_pNavID = (string[]) null;
      this.m_items = 0;
      this.m_curPage = 0;
      this.m_pLeft = (ICRenderSurface) null;
      this.m_pRight = (ICRenderSurface) null;
    }

    public void SetHighscoreMgrInfo(string[] pNavID, int items, int startPage)
    {
      this.m_mode = CHighscoreScreen.Mode.HS_MODE_HS_MGR;
      this.m_pNavID = pNavID;
      this.m_items = items;
      this.m_curPage = startPage;
    }

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      if (id == 1053973641U)
      {
        this.m_curPage = ((CNavigatorWidget) param2).GetSelectionIndex();
        this.BuildHSTitleInfo();
        this.BuildHSTableInfo();
        this.m_base.SetDirty();
        flag = true;
      }
      if (!flag)
        flag = this.HandleEvent(id, param1, param2);
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
      if (this.m_items > 1)
      {
        this.m_navigator.SetID(0);
        this.m_navigator.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
        this.m_navigator.SetColor(4278190080U, uint.MaxValue, 4278233031U);
        this.m_navigator.SetLeftImage(this.m_pLeft);
        this.m_navigator.SetRightImage(this.m_pRight);
        this.m_navigator.SetPageCount(this.m_items);
        this.m_navigator.SetFocusable(false);
        this.m_navigator.SetSelectable(false);
        this.m_navigator.SetSelectionIndex(this.m_curPage);
        this.m_navigator.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
        this.m_navigator.SetFont(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT));
        this.m_layout.Add((CUIWidget) this.m_navigator, CLayoutWidget.eLayoutWidgetPosition.WIDGET_LAYOUT_POSITION_NAVIGATOR);
      }
      this.m_table.SetID(0);
      this.m_table.SetAlignment(CBitMath.TEST_MASK(this.m_flags, 2) ? 2 : 1);
      this.m_table.SetColor(4278190080U, uint.MaxValue, 4278233031U);
      this.m_table.SetTransparent(CBitMath.TEST_MASK(this.m_flags, 1));
      this.m_table.SetAttributes(Consts.kMaxHighScores, 2, 0, 0);
      this.m_page.Add((CUIWidget) this.m_table);
      this.BuildHSTitleInfo();
      this.BuildHSTableInfo();
    }

    private void BuildHSTitleInfo()
    {
      this.m_strTableName = "";
      if (this.m_pNavID[this.m_curPage] != null)
        CUtility.GetString(out this.m_strTableName, this.m_pNavID[this.m_curPage]);
      this.m_navigator.SetSingleText(this.m_strTableName);
    }

    private void BuildHSTableInfo()
    {
      if (this.m_mode == CHighscoreScreen.Mode.HS_MODE_HS_MGR)
      {
        CHighscoreMgr instance = CHighscoreMgr.GetInstance();
        for (int idx = 0; idx < Consts.kMaxHighScores; ++idx)
        {
          this.m_pNames[idx] = instance.GetName(this.m_curPage, idx);
          this.m_pScores[idx] = instance.GetScore(this.m_curPage, idx);
        }
      }
      for (int index = 0; index < Consts.kMaxHighScores; ++index)
      {
        if (this.m_pNames[index] == null || this.m_pNames[index] == null)
          this.m_pNames[index] = "xxx";
      }
      this.m_table.SetContent(CFontMgr.GetInstance().GetFont(CFontMgr.eGameFont.FONT_TITLEFONT), this.m_pNames, this.m_pScores, Consts.kMaxHighScores);
    }

    public enum Mode
    {
      HS_MODE_NONE,
      HS_MODE_HS_MGR,
      HS_MODE_ATB,
    }
  }
}
