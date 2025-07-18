// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CResourceManager
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System.Collections.Generic;

#nullable disable
namespace com.glu.shared
{
  public abstract class CResourceManager : CPriorityClass
  {
    protected Dictionary<string, CResource> m_dic;
    protected CIdToObjectRouter m_idToObject;

    public abstract bool Initialize();

    public abstract bool AddDatabase(string filename);

    public abstract bool RemoveDatabase(string filename, bool destroyRelatedResources);

    public abstract CResourceManager.CConsecutiveResourceIdItr CreateAndInitConsecutiveResourceIdItr(
      string beginName,
      uint count);

    public abstract bool AddResource(CResource resource);

    public abstract bool RemoveResource(string name);

    public abstract bool RemoveResource(ref CResource resource);

    public abstract CResource.CreationContext CreateResource(string name, out CResource resource);

    public abstract bool GetResource(string name, out CResource resource);

    public abstract bool ReleaseResource(string name);

    public abstract bool ReleaseResource(ref CResource resource);

    public abstract bool DestroyResource(string name);

    protected abstract CResource.CreationContext CreateResource(
      out CResource resource,
      uint handle,
      string name,
      uint mimeKey,
      CInputStream inStream);

    public abstract class CConsecutiveResourceIdItr
    {
      public abstract uint GetHandle();

      public abstract string GetName();

      public abstract CResourceManager.CConsecutiveResourceIdItr Next();

      public abstract CResourceManager.CConsecutiveResourceIdItr Prev();

      public abstract CResourceManager.CConsecutiveResourceIdItr Seek(
        CResourceManager.CConsecutiveResourceIdItr.SeekFrom seekFrom,
        int val);

      public static CResourceManager.CConsecutiveResourceIdItr operator ++(
        CResourceManager.CConsecutiveResourceIdItr itr)
      {
        return itr.Next();
      }

      public static CResourceManager.CConsecutiveResourceIdItr operator --(
        CResourceManager.CConsecutiveResourceIdItr itr)
      {
        return itr.Prev();
      }

      public static CResourceManager.CConsecutiveResourceIdItr operator +(
        CResourceManager.CConsecutiveResourceIdItr itr,
        int val)
      {
        return itr.Seek(CResourceManager.CConsecutiveResourceIdItr.SeekFrom.CurrentPosition, val);
      }

      public static CResourceManager.CConsecutiveResourceIdItr operator -(
        CResourceManager.CConsecutiveResourceIdItr itr,
        int val)
      {
        return itr.Seek(CResourceManager.CConsecutiveResourceIdItr.SeekFrom.CurrentPosition, -val);
      }

      public enum SeekFrom
      {
        Beginning,
        CurrentPosition,
      }
    }
  }
}
