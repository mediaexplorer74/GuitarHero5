// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CLinkList
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CLinkList
  {
    public CLinkListNode m_pHead;
    public CLinkListNode m_pTail;
    public int m_count;

    public CLinkListNode GetHead() => this.m_pHead;

    public CLinkListNode GetTail() => this.m_pTail;

    public int Size() => this.m_count;

    public void Insert(CLinkListNode node, object data)
    {
      this.InsertBefore(this.m_pHead, node, data);
    }

    public void InsertBefore(CLinkListNode position, CLinkListNode node, object data)
    {
      node.m_pList = this;
      node.m_pData = data != null ? data : (object) node;
      node.m_pNext = position != null ? position : this.m_pHead;
      node.m_pPrev = position?.m_pPrev;
      if (node.m_pNext != null)
        node.m_pNext.m_pPrev = node;
      if (node.m_pPrev != null)
        node.m_pPrev.m_pNext = node;
      if (position == null || this.m_pHead == position)
        this.m_pHead = node;
      if (this.m_pTail == null)
        this.m_pTail = node;
      ++this.m_count;
    }

    public void InsertAfter(CLinkListNode pPos, CLinkListNode pNode, object pData)
    {
      if (pNode == null)
        return;
      pNode.m_pList = this;
      pNode.m_pData = pData != null ? pData : (object) pNode;
      pNode.m_pPrev = pPos != null ? pPos : this.m_pTail;
      pNode.m_pNext = pPos?.m_pNext;
      if (pNode.m_pNext != null)
        pNode.m_pNext.m_pPrev = pNode;
      if (pNode.m_pPrev != null)
        pNode.m_pPrev.m_pNext = pNode;
      if (this.m_pHead == null)
        this.m_pHead = pNode;
      if (pPos == null || this.m_pTail == pPos)
        this.m_pTail = pNode;
      ++this.m_count;
    }

    public void InsertAtEnd(CLinkListNode pNode, object pData)
    {
      this.InsertAfter(this.m_pTail, pNode, pData);
    }

    public void InsertSorted(tfnLinkListCompareItem fCompare, CLinkListNode pNode, object pData)
    {
      if (pNode == null)
        return;
      CLinkListNode clinkListNode = this.m_pHead;
      while (clinkListNode != null && fCompare(clinkListNode, pData != null ? pData : (object) pNode) > 0)
        clinkListNode = clinkListNode.m_pNext;
      if (clinkListNode == null)
        this.InsertAfter(this.m_pTail, pNode, pData);
      else
        this.InsertBefore(clinkListNode, pNode, pData);
    }

    public void Remove(CLinkListNode pNode)
    {
      if (pNode == null || pNode.m_pList == null)
        return;
      if (pNode.m_pList != this)
      {
        pNode.m_pList.Remove(pNode);
      }
      else
      {
        --this.m_count;
        if (pNode == this.m_pHead)
          this.m_pHead = pNode.m_pNext;
        if (pNode == this.m_pTail)
          this.m_pTail = pNode.m_pPrev;
        if (pNode.m_pNext != null)
          pNode.m_pNext.m_pPrev = pNode.m_pPrev;
        if (pNode.m_pPrev != null)
          pNode.m_pPrev.m_pNext = pNode.m_pNext;
        pNode.m_pList = (CLinkList) null;
        pNode.m_pPrev = (CLinkListNode) null;
        pNode.m_pNext = (CLinkListNode) null;
      }
    }

    public void RemoveAll()
    {
      CLinkListNode pHead;
      while ((pHead = this.m_pHead) != null)
        this.Remove(pHead);
    }

    public CLinkListNode Find(CLinkListNode pStart, tfnLinkListCompareKey fCompare, object pKey)
    {
      CLinkListNode pNode = (CLinkListNode) null;
      if (pKey != null)
      {
        pNode = pStart == null ? this.m_pHead : pStart;
        while (pNode != null && (fCompare == null || !fCompare(pNode, pKey)) && (fCompare != null || pKey != pNode.m_pData))
          pNode = pNode.m_pNext;
      }
      return pNode;
    }
  }
}
