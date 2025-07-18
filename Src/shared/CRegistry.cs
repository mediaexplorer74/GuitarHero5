// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRegistry
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CRegistry : CSystem
  {
    public const uint ClassId = 1126056847;
    private static CRegistryElement m_dummyEndElement = new CRegistryElement(true);
    private CRegistryElement m_head;
    private CRegistryElement m_tail;

    public CRegistry()
    {
      this.m_classId = 1126056847U;
      this.m_head = CRegistry.m_dummyEndElement;
      this.m_tail = CRegistry.m_dummyEndElement;
    }

    public override CSystemElement CreateSystemElement(
      uint handle,
      object data,
      uint dataClassId,
      uint priority)
    {
      return (CSystemElement) new CRegistryElement(handle, data, dataClassId, priority);
    }

    public override CSystemElement CreateSystemElement(uint handle, object data, uint dataClassId)
    {
      return (CSystemElement) new CRegistryElement(handle, data, dataClassId, 1073741823U);
    }

    public override CSystemElement CreateSystemElement(uint handle, uint priority)
    {
      return (CSystemElement) new CRegistryElement(handle, (object) null, 0U, priority);
    }

    public override void Add(CSystemElement element)
    {
      CRegistryElement cregistryElement = (CRegistryElement) element;
      CRegistryItr cregistryItr = new CRegistryItr(this.m_tail);
      if (cregistryItr.GetElement() != CRegistry.m_dummyEndElement)
      {
        bool flag = false;
        while (cregistryItr.GetElement().GetPriority() < cregistryElement.GetPriority())
        {
          if (cregistryItr == this.Begin())
          {
            flag = true;
            break;
          }
          --cregistryItr;
        }
        if (flag)
        {
          this.m_head.m_prev = cregistryElement;
          cregistryElement.m_prev = (CRegistryElement) null;
          cregistryElement.m_next = this.m_head;
          this.m_head = cregistryElement;
        }
        else
        {
          cregistryElement.m_prev = cregistryItr.GetElement();
          cregistryElement.m_next = cregistryItr.GetElement().m_next;
          cregistryItr.GetElement().m_next = cregistryElement;
          if (cregistryElement.m_next == CRegistry.m_dummyEndElement)
            this.m_tail = cregistryElement;
          cregistryElement.m_next.m_prev = cregistryElement;
        }
      }
      else
      {
        cregistryElement.m_prev = (CRegistryElement) null;
        cregistryElement.m_next = CRegistry.m_dummyEndElement;
        CRegistry.m_dummyEndElement.m_prev = cregistryElement;
        this.m_head = cregistryElement;
        this.m_tail = cregistryElement;
      }
    }

    public override bool Remove(CSystemElement element)
    {
      CRegistryElement cregistryElement = (CRegistryElement) element;
      if (cregistryElement.m_prev != null)
      {
        cregistryElement.m_prev.m_next = cregistryElement.m_next;
      }
      else
      {
        this.m_head = cregistryElement.m_next;
        if (this.m_head == CRegistry.m_dummyEndElement)
        {
          this.m_tail = CRegistry.m_dummyEndElement;
          return true;
        }
      }
      if (cregistryElement.m_next != null)
        cregistryElement.m_next.m_prev = cregistryElement.m_prev;
      if (cregistryElement.m_next == CRegistry.m_dummyEndElement)
        this.m_tail = cregistryElement.m_next.m_prev;
      return true;
    }

    public override void RemoveAll()
    {
      this.m_head = CRegistry.m_dummyEndElement;
      this.m_tail = CRegistry.m_dummyEndElement;
    }

    public override bool Query(uint classId, uint handle, out CSystemElement element)
    {
      bool flag = false;
      CRegistryElement element1;
      if (classId != 0U && handle != 0U)
        flag = this.QueryClassIdAndHandle(classId, handle, out element1);
      else if (classId != 0U)
        flag = this.QueryClassId(classId, out element1);
      else if (handle != 0U)
        flag = this.QueryHandle(handle, out element1);
      else
        element1 = (CRegistryElement) null;
      element = (CSystemElement) element1;
      return flag;
    }

    public CRegistryElement Begin() => this.m_head;

    public CRegistryElement End() => CRegistry.m_dummyEndElement;

    protected override void OnExecute()
    {
    }

    public bool QueryClassId(uint classId, out CRegistryElement element)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.Begin());
      bool flag = false;
      while (cregistryItr != this.End())
      {
        if ((int) classId == (int) cregistryItr.GetElement().GetDataClassId())
        {
          flag = true;
          break;
        }
        ++cregistryItr;
      }
      element = !flag ? (CRegistryElement) null : cregistryItr.GetElement();
      return flag;
    }

    public bool QueryHandle(uint handle, out CRegistryElement element)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.Begin());
      bool flag = false;
      while (cregistryItr != this.End())
      {
        if ((int) handle == (int) cregistryItr.GetElement().GetHandle())
        {
          flag = true;
          break;
        }
        ++cregistryItr;
      }
      element = !flag ? (CRegistryElement) null : cregistryItr.GetElement();
      return flag;
    }

    public bool QueryClassIdAndHandle(uint classId, uint handle, out CRegistryElement element)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.Begin());
      bool flag = false;
      while (cregistryItr != this.End())
      {
        if ((int) classId == (int) cregistryItr.GetElement().GetHandle() && (int) classId == (int) cregistryItr.GetElement().GetDataClassId())
        {
          flag = true;
          break;
        }
        ++cregistryItr;
      }
      element = !flag ? (CRegistryElement) null : cregistryItr.GetElement();
      return flag;
    }

    public CRegistryElement GetPrev(CRegistryElement element) => element.m_prev;

    public CRegistryElement GetNext(CRegistryElement element) => element.m_next;
  }
}
