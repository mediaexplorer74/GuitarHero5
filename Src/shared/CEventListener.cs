// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CEventListener
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CEventListener : CPriorityClass
  {
    public const uint ClassId = 2754624434;
    protected object m_owner;
    protected uint m_handle;
    protected CEventListener.CEventListerner_EventHandler m_handler;

    public CEventListener()
    {
      this.m_classId = 2754624434U;
      this.m_handle = 0U;
      this.SetOwner((object) null);
    }

    public bool Initialize(
      uint handle,
      object owner,
      CEventListener.CEventListerner_EventHandler handler)
    {
      this.m_handle = handle;
      this.SetOwnerAndEventHandler(owner, handler);
      return true;
    }

    public void Destroy()
    {
      if (this.m_handle != 0U)
      {
        this.UnregisterAll();
        this.m_handle = 0U;
      }
      this.SetOwnerAndEventHandler((object) null, (CEventListener.CEventListerner_EventHandler) null);
    }

    public void SetOwnerAndEventHandler(
      object owner,
      CEventListener.CEventListerner_EventHandler handler)
    {
      this.SetOwner(owner);
      this.SetHandler(handler);
    }

    public void SetOwner(object owner) => this.m_owner = owner;

    public object GetOwner() => this.m_owner;

    public void SetHandler(
      CEventListener.CEventListerner_EventHandler handler)
    {
      this.m_handler = handler;
    }

    public CEventListener.CEventListerner_EventHandler GetHandler() => this.m_handler;

    public bool Register(uint eventId) => this.Register(eventId, 1073741823);

    public bool Register(uint eventId, int priority)
    {
      CSystemElement element = (CSystemElement) null;
      bool flag;
      if (CApp.GetRegistry().Query(0U, 4150451705U, out element))
      {
        this.RegisterIdSpecfic(eventId, priority, (CSystem) element.GetData());
        flag = true;
      }
      else
        flag = false;
      return flag;
    }

    public bool Register(uint[] eventIds) => this.Register(eventIds, 1073741823);

    public bool Register(uint[] eventIds, int priority)
    {
      CSystemElement element = (CSystemElement) null;
      bool flag;
      if (CApp.GetRegistry().Query(0U, 4150451705U, out element))
      {
        int index = 0;
        do
        {
          this.RegisterIdSpecfic(eventIds[index], priority, (CSystem) element.GetData());
          ++index;
        }
        while (eventIds[index] != 0U);
        flag = true;
      }
      else
        flag = false;
      return flag;
    }

    public void Unregister(uint eventId)
    {
      CSystemElement element = (CSystemElement) null;
      if (!CApp.GetRegistry().Query(0U, 4150451705U, out element))
        return;
      this.UnregisterIdSpecfic(eventId, (CSystem) element.GetData());
    }

    public void Unregister(ref uint eventId)
    {
      CSystemElement element = (CSystemElement) null;
      if (!CApp.GetRegistry().Query(0U, 4150451705U, out element))
        return;
      do
      {
        this.UnregisterIdSpecfic(eventId, (CSystem) element.GetData());
        ++eventId;
      }
      while (eventId != 0U);
    }

    public void UnregisterAll()
    {
      CSystemElement element1 = (CSystemElement) null;
      if (this.m_handle == 0U || !CApp.GetRegistry().Query(0U, 4150451705U, out element1))
        return;
      CRegistry data1 = (CRegistry) element1.GetData();
      CRegistryItr cregistryItr = new CRegistryItr(data1.Begin());
      while (cregistryItr != data1.End())
      {
        CRegistry data2 = (CRegistry) cregistryItr.GetElement().GetData();
        CSystemElement element2 = (CSystemElement) null;
        if (data2.Query(0U, this.m_handle, out element2))
        {
          data2.Remove(element2);
          if (data2.Begin() == data2.End())
          {
            CRegistryElement element3 = cregistryItr.GetElement();
            ++cregistryItr;
            data1.Remove((CSystemElement) element3);
          }
          else
            ++cregistryItr;
        }
        else
          ++cregistryItr;
      }
    }

    public void RegisterIdSpecfic(uint eventId, int priority, CSystem sys)
    {
      CSystemElement element = (CSystemElement) null;
      CRegistry data;
      if (!sys.Query(0U, eventId, out element))
      {
        data = new CRegistry();
        sys.Add(sys.CreateSystemElement(eventId, (object) data, 0U, 1073741823U));
      }
      else
        data = (CRegistry) element.GetData();
      data.Add(data.CreateSystemElement(this.m_handle, (object) this, this.GetClassId(), (uint) priority));
    }

    public void UnregisterIdSpecfic(uint eventId, CSystem sys)
    {
      CSystemElement element1 = (CSystemElement) null;
      if (this.m_handle == 0U || !sys.Query(0U, eventId, out element1))
        return;
      CRegistry data = (CRegistry) element1.GetData();
      CSystemElement element2 = (CSystemElement) null;
      if (!data.Query(0U, this.m_handle, out element2))
        return;
      data.Remove(element2);
      if (data.Begin() != data.End())
        return;
      sys.Remove(element1);
    }

    public delegate bool CEventListerner_EventHandler(CEvent Event, object data);

    public static class classss
    {
      public const uint val1 = 255;
    }
  }
}
