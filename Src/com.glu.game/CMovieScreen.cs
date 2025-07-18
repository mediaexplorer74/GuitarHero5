// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieScreen
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CMovieScreen : CSoftkeyScreen
  {
    private string kwszGluMovieFile = "glu.m4v";

    public override uint Start()
    {
      uint num = base.Start();
      CFileUtil.GetApplicationPathForFile(out string _, this.kwszGluMovieFile);
      return num;
    }

    public override void Stop() => base.Stop();

    public override void Activate() => base.Activate();

    public override void Deactivate() => base.Deactivate();

    public override bool HandleEvent(uint id, uint param1, object param2)
    {
      bool flag = false;
      switch (id)
      {
        case 129075783:
        case 544526345:
        case 902053462:
        case 2300082508:
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

    public override bool HandleUpdate(int timeElapsedMS)
    {
      base.HandleUpdate(timeElapsedMS);
      return true;
    }

    public override void Build()
    {
      base.Build();
      this.m_base.SetTransparent(true);
    }
  }
}
