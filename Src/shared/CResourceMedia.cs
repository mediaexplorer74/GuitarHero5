// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CResourceMedia
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CResourceMedia : CResource
  {
    public new const uint ClassId = 1333729139;
    private CMedia m_media;

    public CResourceMedia() => this.m_classId = 1333729139U;

    public override object GetData() => (object) this.m_media;

    public override CResource.CreationContext CreateInternal(
      uint handle,
      string name,
      CInputStream inStream,
      uint mimeKey,
      CIdToObjectRouter idToObject)
    {
      this.m_handle = handle;
      this.m_media = new CMedia();
      bool flag;
      CResource.CreationContext creationContext;
      if (inStream != null)
      {
        flag = this.m_media.Load(inStream, mimeKey);
        creationContext = flag ? CResource.CreationContext.CreatedResourceFromInputStream : CResource.CreationContext.UnableToCreateResource;
      }
      else
      {
        flag = true;
        creationContext = flag ? CResource.CreationContext.CreatedResourceDefaultConstructionOnly : CResource.CreationContext.UnableToCreateResource;
      }
      if (!flag)
      {
        creationContext = CResource.CreationContext.UnableToCreateResource;
      }
      else
      {
        this.SetName(name);
        this.m_isCreated = true;
      }
      return creationContext;
    }
  }
}
