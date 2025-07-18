// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameApplet
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public sealed class CGameApplet : CApplet
  {
    public CGameApplet()
      : base(new CApplet.CreateApp(CGameApp.CreateInstance), new CApplet.CreateAppFactory(CGameAppFactory.CreateInstance))
    {
    }
  }
}
