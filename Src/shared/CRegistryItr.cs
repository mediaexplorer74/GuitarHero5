// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRegistryItr
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public struct CRegistryItr
  {
    public CRegistryElement m_currElement;

    public CRegistryItr(CRegistryItr itr) => this.m_currElement = itr.m_currElement;

    public CRegistryItr(CRegistryElement ele) => this.m_currElement = ele;

    public static CRegistryItr operator ++(CRegistryItr itr)
    {
      if (itr.m_currElement.m_next != null)
        itr.m_currElement = itr.m_currElement.m_next;
      return itr;
    }

    public static CRegistryItr operator --(CRegistryItr itr)
    {
      if (itr.m_currElement.m_prev != null)
        itr.m_currElement = itr.m_currElement.m_prev;
      return itr;
    }

    public CRegistryElement GetElement() => this.m_currElement;

    public static bool operator ==(CRegistryItr itr, CRegistryElement ele)
    {
      return itr.m_currElement == ele;
    }

    public static bool operator ==(CRegistryElement ele, CRegistryItr itr)
    {
      return itr.m_currElement == ele;
    }

    public static bool operator !=(CRegistryItr itr, CRegistryElement ele)
    {
      return itr.m_currElement != ele;
    }

    public static bool operator !=(CRegistryElement ele, CRegistryItr itr)
    {
      return itr.m_currElement != ele;
    }

    public override bool Equals(object r) => base.Equals(r);

    public override int GetHashCode() => base.GetHashCode();
  }
}
