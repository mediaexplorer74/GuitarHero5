// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CIdToObjectRouter
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CIdToObjectRouter : CClass
  {
    public const uint ClassId = 2959325110;
    protected CIdToObjectRouter.Handler_NameToInputStream m_handler_NameToInputStream;

    public CIdToObjectRouter()
      : base(2959325110U)
    {
      this.m_handler_NameToInputStream = (CIdToObjectRouter.Handler_NameToInputStream) null;
    }

    public void SetCallbackNameToInputStream(
      CIdToObjectRouter.Handler_NameToInputStream handler)
    {
      this.m_handler_NameToInputStream = handler;
    }

    public bool NameToInputStream(string name, out CInputStream stream, out uint mimeKey)
    {
      if (this.m_handler_NameToInputStream != null)
        return this.m_handler_NameToInputStream(name, out stream, out mimeKey);
      stream = (CInputStream) null;
      mimeKey = 0U;
      return false;
    }

    public delegate bool Handler_NameToInputStream(
      string name,
      out CInputStream stream,
      out uint mimeKey);
  }
}
