// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CEvent
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CEvent : CExecutable
  {
    public const uint ClassId = 41026452;
    protected uint m_id;
    protected CClass m_sender;
    protected uint m_hReceiver;
    protected bool m_hasBeenHandled;
    protected bool m_hasBeenHandledThisExecution;
    protected byte[] m_reserved = new byte[2];

    public CEvent()
    {
      this.m_classId = 41026452U;
      this.m_id = 0U;
      this.SetToDeleteOnTerminalRun();
      this.SetSender((CClass) null);
      this.SetHandleToReceiver(0U);
      this.SetRunFrequency(1U);
      this.m_hasBeenHandled = false;
      this.m_hasBeenHandledThisExecution = false;
    }

    public CEvent(
      CClass sender,
      uint id,
      uint hReceiver,
      int updateInterval,
      uint numOfOccurrences)
    {
      this.m_classId = 41026452U;
      this.m_id = id;
      this.SetToDeleteOnTerminalRun();
      this.SetSender(sender);
      this.SetHandleToReceiver(hReceiver);
      this.SetUpdateInterval(updateInterval);
      this.SetRunFrequency(numOfOccurrences);
      this.m_hasBeenHandled = false;
      this.m_hasBeenHandledThisExecution = false;
    }

    public void SetId(uint id) => this.m_id = id;

    public uint GetId() => this.m_id;

    public void SetSender(CClass sender) => this.m_sender = sender;

    public CClass GetSender() => this.m_sender;

    public void SetHandleToReceiver(uint hReceiver) => this.m_hReceiver = hReceiver;

    public uint GetHandleToReceiver() => this.m_hReceiver;

    public bool HasBeenHandled() => this.m_hasBeenHandled;

    public bool HasBeenHandledThisExecution() => this.m_hasBeenHandledThisExecution;

    protected override void OnExecute()
    {
      CSystemElement element = (CSystemElement) null;
      CSystem registry = (CSystem) CApp.GetRegistry();
      this.m_hasBeenHandledThisExecution = false;
      if (!registry.Query(0U, 4150451705U, out element) || !((CSystem) element.GetData()).Query(0U, this.m_id, out element))
        return;
      CRegistry data1 = (CRegistry) element.GetData();
      if (this.m_hReceiver != 0U)
      {
        if (!data1.Query(0U, this.m_hReceiver, out element))
          return;
        CEventListener data2 = (CEventListener) element.GetData();
        if (!data2.GetHandler()(this, (object) data2))
          return;
        if (!this.m_hasBeenHandledThisExecution)
          this.m_hasBeenHandledThisExecution = true;
        if (this.m_hasBeenHandled)
          return;
        this.m_hasBeenHandled = true;
      }
      else
      {
        CRegistryItr cregistryItr = new CRegistryItr(data1.Begin());
        while (cregistryItr != data1.End())
        {
          CEventListener data3 = (CEventListener) cregistryItr.GetElement().GetData();
          if (data3.GetHandler()(this, (object) data3))
          {
            if (!this.m_hasBeenHandledThisExecution)
              this.m_hasBeenHandledThisExecution = true;
            if (!this.m_hasBeenHandled)
              this.m_hasBeenHandled = true;
          }
          ++cregistryItr;
        }
      }
    }
  }
}
