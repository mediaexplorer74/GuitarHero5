// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CSystemEventQueue
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class CSystemEventQueue
  {
    public const int CSystemEventQueue_kMaxQueueSize = 500;
    private CSystemEventQueue.SystemEvent[] m_pQueue;
    private int m_size;
    private int m_count;

    public CSystemEventQueue()
    {
      this.m_pQueue = (CSystemEventQueue.SystemEvent[]) null;
      this.m_size = 0;
      this.m_count = 0;
    }

    public void Clear() => this.m_count = 0;

    public int GetCount() => this.m_count;

    public void Queue(uint eventId, uint param1) => this.Queue(eventId, param1, 0U);

    public void Queue(uint eventId) => this.Queue(eventId, 0U, 0U);

    public void ReleaseMemory()
    {
      this.m_pQueue = (CSystemEventQueue.SystemEvent[]) null;
      this.m_size = 0;
      this.m_count = 0;
    }

    public void GetEvent(ref CSystemEventQueue.SystemEvent outValue, int idx)
    {
      outValue.Clear();
      if (idx >= this.m_count)
        return;
      outValue = this.m_pQueue[idx];
    }

    public void Queue(CSystemEventQueue.SystemEvent anEvent)
    {
      if (this.m_size == 0)
      {
        this.m_pQueue = new CSystemEventQueue.SystemEvent[500];
        this.m_size = 500;
      }
      else if (this.m_size == this.m_count)
      {
        CSystemEventQueue.SystemEvent[] destinationArray = new CSystemEventQueue.SystemEvent[this.m_size << 1];
        Array.Copy((Array) this.m_pQueue, (Array) destinationArray, this.m_size);
        this.m_pQueue = destinationArray;
        this.m_size <<= 1;
      }
      if (this.m_count >= this.m_size - 1)
        return;
      this.m_pQueue[this.m_count++] = anEvent;
    }

    public void Queue(uint eventId, uint param1, uint param2)
    {
      this.Queue(new CSystemEventQueue.SystemEvent()
      {
        m_id = eventId,
        m_param1 = param1,
        m_param2 = param2
      });
    }

    public struct SystemEvent
    {
      public uint m_id;
      public uint m_param1;
      public uint m_param2;

      public void Clear()
      {
        this.m_id = 0U;
        this.m_param1 = 0U;
        this.m_param2 = 0U;
      }
    }
  }
}
