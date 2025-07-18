// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CResourceRenderSurface
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CResourceRenderSurface : CResource
  {
    public new const uint ClassId = 591665909;
    private ICRenderSurface m_surface;

    public CResourceRenderSurface() => this.m_classId = 591665909U;

    public override object GetData() => (object) this.m_surface;

    public override CResource.CreationContext CreateInternal(
      uint handle,
      string name,
      CInputStream inStream,
      uint mimeKey,
      CIdToObjectRouter idToObject)
    {
      this.m_handle = handle;
      ICGraphics.Abstraction abstraction = ICGraphics.Abstraction.Undefined;
      ICRenderSurface.Type type = ICRenderSurface.Type.OffScreen;
      ICRenderSurface.Targetability targetability = ICRenderSurface.Targetability.NotTargetable;
      bool flag = this.GetCreationParams(mimeKey, ref abstraction, ref type, ref targetability);
      CResource.CreationContext creationContext;
      if (flag)
      {
        this.m_surface = ICRenderSurface.CreateInstance(abstraction, type, targetability);
        if (this.m_surface != null)
        {
          if (inStream != null)
          {
            flag = this.m_surface.Load(inStream, mimeKey, idToObject);
            creationContext = flag ? CResource.CreationContext.CreatedResourceFromInputStream : CResource.CreationContext.UnableToCreateResource;
          }
          else
          {
            flag = true;
            creationContext = flag ? CResource.CreationContext.CreatedResourceDefaultConstructionOnly : CResource.CreationContext.UnableToCreateResource;
          }
        }
        else
          creationContext = CResource.CreationContext.UnableToCreateResource;
      }
      else
        creationContext = CResource.CreationContext.UnableToCreateResource;
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

    private bool GetCreationParams(
      uint mimeKey,
      ref ICGraphics.Abstraction abstraction,
      ref ICRenderSurface.Type type,
      ref ICRenderSurface.Targetability targetability)
    {
      bool creationParams = true;
      switch (mimeKey)
      {
        case 1497334080:
          switch (ICGraphics.GetInstance().GetDefaultActiveAbstractionLayer())
          {
            case ICGraphics.Abstraction.Hardware:
              mimeKey = 1509211225U;
              break;
            case ICGraphics.Abstraction.Software:
              mimeKey = 1508883522U;
              break;
            default:
              mimeKey = 0U;
              break;
          }
          break;
        case 4231102733:
          switch (ICGraphics.GetInstance().GetDefaultActiveAbstractionLayer())
          {
            case ICGraphics.Abstraction.Hardware:
              mimeKey = 3782866110U;
              break;
            case ICGraphics.Abstraction.Software:
              mimeKey = 3782864830U;
              break;
            default:
              mimeKey = 0U;
              break;
          }
          break;
      }
      switch (mimeKey)
      {
        case 1508883522:
          abstraction = ICGraphics.Abstraction.Software;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.TargetableBySoftwareRenderer;
          break;
        case 1509211202:
          abstraction = ICGraphics.Abstraction.Hardware;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.TargetableBySoftwareRenderer;
          break;
        case 1509211225:
          abstraction = ICGraphics.Abstraction.Hardware;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.TargetableByHardwareRenderer;
          break;
        case 2672542834:
          abstraction = ICGraphics.Abstraction.Hardware;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.TargetableBySoftwareAndHardwareRenderers;
          break;
        case 3782864830:
          abstraction = ICGraphics.Abstraction.Software;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.NotTargetable;
          break;
        case 3782866110:
          abstraction = ICGraphics.Abstraction.Hardware;
          type = ICRenderSurface.Type.OffScreen;
          targetability = ICRenderSurface.Targetability.NotTargetable;
          break;
        default:
          creationParams = false;
          break;
      }
      return creationParams;
    }
  }
}
