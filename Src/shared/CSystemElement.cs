// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CSystemElement
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CSystemElement : CPriorityClass
  {
    public const uint ClassId = 2579978167;
    protected uint m_handle;
    protected uint m_dataClassId;
    protected object m_data;

    public CSystemElement()
    {
      this.m_classId = 2579978167U;
      this.m_handle = 0U;
      this.m_dataClassId = 0U;
      this.m_data = (object) null;
    }

    public void SetHandle(uint handle) => this.m_handle = handle;

    public void SetDataClassId(uint classId) => this.m_dataClassId = classId;

    public void SetData(object data) => this.m_data = data;

    public uint GetHandle() => this.m_handle;

    public uint GetDataClassId() => this.m_dataClassId;

    public object GetData() => this.m_data;
  }
}
