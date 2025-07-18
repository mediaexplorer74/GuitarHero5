// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRegistryElement
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CRegistryElement : CSystemElement
  {
    public new const uint ClassId = 3079821237;
    public bool m_isDummyEndElement;
    public CRegistryElement m_prev;
    public CRegistryElement m_next;

    public CRegistryElement()
    {
      this.m_classId = 3079821237U;
      this.m_isDummyEndElement = false;
      this.m_prev = (CRegistryElement) null;
      this.m_next = (CRegistryElement) null;
    }

    public CRegistryElement(uint handle, uint dataClassId, uint priority)
    {
      this.m_classId = 3079821237U;
      this.m_isDummyEndElement = false;
      this.m_handle = handle;
      this.m_dataClassId = dataClassId;
      this.m_prev = (CRegistryElement) null;
      this.m_next = (CRegistryElement) null;
      this.SetPriority(priority);
    }

    public CRegistryElement(object data, uint dataClassId, uint priority)
    {
      this.m_classId = 3079821237U;
      this.m_isDummyEndElement = false;
      this.m_dataClassId = dataClassId;
      this.m_data = data;
      this.m_prev = (CRegistryElement) null;
      this.m_next = (CRegistryElement) null;
      this.SetPriority(priority);
    }

    public CRegistryElement(uint handle, object data, uint dataClassId, uint priority)
    {
      this.m_classId = 3079821237U;
      this.m_isDummyEndElement = false;
      this.m_handle = handle;
      this.m_dataClassId = dataClassId;
      this.m_data = data;
      this.m_prev = (CRegistryElement) null;
      this.m_next = (CRegistryElement) null;
      this.SetPriority(priority);
    }

    public CRegistryElement(bool isDummyEndElement) => this.m_isDummyEndElement = true;
  }
}
