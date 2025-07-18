// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CSystem
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class CSystem : CExecutable
  {
    public abstract CSystemElement CreateSystemElement(
      uint handle,
      object data,
      uint dataClassId,
      uint priority);

    public abstract CSystemElement CreateSystemElement(uint handle, object data, uint dataClassId);

    public abstract CSystemElement CreateSystemElement(uint handle, uint priority);

    public abstract void Add(CSystemElement element);

    public abstract bool Remove(CSystemElement element);

    public abstract void RemoveAll();

    public abstract bool Query(uint classId, uint handle, out CSystemElement element);
  }
}
