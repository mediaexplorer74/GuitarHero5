// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CStrWChar
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CStrWChar : CStrChar
  {
    public new const uint ClassId = 1131634356;

    public CStrWChar() => this.m_classId = 1131634356U;

    public CStrWChar(char[] str)
      : base(str)
    {
      this.m_classId = 1131634356U;
    }

    public CStrWChar(string str)
      : base(str)
    {
      this.m_classId = 1131634356U;
    }

    public CStrWChar(CStrChar str)
      : base(str)
    {
      this.m_classId = 1681284718U;
    }
  }
}
