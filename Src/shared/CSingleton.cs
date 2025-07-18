// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CSingleton
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CSingleton : CClass
  {
    protected CSingleton m_instance;

    protected CSingleton() => this.m_instance = this;

    protected CSingleton(uint classId)
      : base(classId)
    {
      this.m_instance = this;
    }

    public CSingleton Instance => this.m_instance;
  }
}
