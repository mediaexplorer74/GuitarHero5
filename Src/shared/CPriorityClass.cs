// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CPriorityClass
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CPriorityClass : CClass, ICPriorityClass
  {
    public const int Priority_Lowest = 0;
    public const int Priority_Median = 1073741823;
    public const int Priority_Highest = 2147483647;
    protected uint m_priority;

    public uint Priority => this.m_priority;

    public uint GetPriority() => this.m_priority;

    protected CPriorityClass() => this.m_priority = 0U;

    protected CPriorityClass(uint priority) => this.m_priority = priority;

    protected void SetPriority(uint priority) => this.m_priority = priority;
  }
}
