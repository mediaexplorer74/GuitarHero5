// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CMessage
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CMessage : CEvent
  {
    public new const uint ClassId = 2065979161;
    public const int CMessage_MaxNumberOfParams = 8;
    public const int MaxNumberOfParameters = 8;
    private int m_nOfParameters;
    private CMessage.Parameter[] m_parameter = new CMessage.Parameter[8];

    public CMessage()
    {
      this.m_classId = 2065979161U;
      this.m_nOfParameters = 0;
    }

    public CMessage(
      CClass sender,
      uint id,
      uint hReceiver,
      int nOfParams,
      params CMessage.Parameter[] va_arg)
    {
      this.m_classId = 2065979161U;
      this.SetId(id);
      this.SetSender(sender);
      this.SetHandleToReceiver(hReceiver);
      this.m_nOfParameters = nOfParams;
      for (int index = 0; index < nOfParams; ++index)
      {
        this.m_parameter[index].m_classId = va_arg[index].m_classId;
        this.m_parameter[index].m_access = va_arg[index].m_access;
        this.m_parameter[index].m_data = va_arg[index].m_data;
      }
    }

    public void SetNumberOfParameters(int nOfParametes) => this.m_nOfParameters = nOfParametes;

    public int GetNumberOfParameters() => this.m_nOfParameters;

    public void SetParameter(int paramId, object value, uint id, CMessage.Parameter.Access access)
    {
      this.m_parameter[paramId].m_classId = id;
      this.m_parameter[paramId].m_access = access;
      this.m_parameter[paramId].m_data = value;
    }

    public void SetParameter(int paramId, CMessage.Parameter param)
    {
      this.m_parameter[paramId] = param;
    }

    public CMessage.Parameter GetParameter(int paramId) => this.m_parameter[paramId];

    public void Initialize(
      CClass sender,
      uint id,
      uint hReceiver,
      int nOfParams,
      params CMessage.Parameter[] va_arg)
    {
      this.SetId(id);
      this.SetSender(sender);
      this.SetHandleToReceiver(hReceiver);
      this.m_nOfParameters = nOfParams;
      for (int index = 0; index < nOfParams; ++index)
      {
        this.m_parameter[index].m_classId = va_arg[index].m_classId;
        this.m_parameter[index].m_access = va_arg[index].m_access;
        this.m_parameter[index].m_data = va_arg[index].m_data;
      }
    }

    public struct Parameter(uint classId, CMessage.Parameter.Access access, object data)
    {
      public uint m_classId = classId;
      public CMessage.Parameter.Access m_access = access;
      public object m_data = data;

      public enum Access
      {
        Direct,
        Pointer,
        ConstPointer,
        Reference,
        ConstReference,
      }
    }
  }
}
