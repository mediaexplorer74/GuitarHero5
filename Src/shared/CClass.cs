// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CClass
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CClass : ICClass
  {
    protected uint m_classId;

    public uint ClassId => this.m_classId;

    public uint GetClassId() => this.m_classId;

    protected CClass() => this.m_classId = (uint) this.GetHashCode();

    protected CClass(uint classId) => this.m_classId = classId;

    protected void SetClassId(uint classId) => this.m_classId = classId;
  }
}
