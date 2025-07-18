// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CResource
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CResource : CPriorityClass
  {
    public const uint ClassId = 1113072771;
    protected object m_source;
    protected string m_name;
    protected uint m_handle;
    protected uint m_refCount;
    protected bool m_isCreated;

    public CResource()
    {
      this.m_classId = 1113072771U;
      this.m_source = (object) null;
      this.m_name = (string) null;
      this.m_isCreated = false;
      this.m_handle = 0U;
      this.m_refCount = 0U;
    }

    public abstract object GetData();

    public uint Handle => this.m_handle;

    public uint GetHandle() => this.m_handle;

    public string Name => this.m_name;

    public string GetName() => this.m_name;

    public bool Created => this.m_isCreated;

    public bool IsCreated() => this.m_isCreated;

    public uint RefCount
    {
      set => this.m_refCount = value;
      get => this.m_refCount;
    }

    public uint GetRefCount() => this.m_refCount;

    public abstract CResource.CreationContext CreateInternal(
      uint handle,
      string name,
      CInputStream inStream,
      uint mimeKey,
      CIdToObjectRouter idToObject);

    public void SetName(string name) => this.m_name = name;

    public enum CreationContext
    {
      UnableToCreateResource,
      CreatedResourceDefaultConstructionOnly,
      CreatedResourceFromBigFile,
      CreatedResourceFromInputStream,
      ResourceWasPreviouslyCreated,
    }
  }
}
