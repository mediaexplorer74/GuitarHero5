// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CLinkListNode
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CLinkListNode
  {
    public CLinkList m_pList;
    public CLinkListNode m_pNext;
    public CLinkListNode m_pPrev;
    public object m_pData;

    public CLinkList GetList() => this.m_pList;

    public CLinkListNode GetNext() => this.m_pNext;

    public CLinkListNode GetPrev() => this.m_pPrev;

    public object GetData() => this.m_pData;
  }
}
