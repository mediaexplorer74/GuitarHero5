// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CExecutableRegistry
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public class CExecutableRegistry : CRegistry
  {
    public new const uint ClassId = 2000502999;

    public CExecutableRegistry() => this.m_classId = 2000502999U;

    protected override void OnExecute()
    {
      CApp.GetResourceManager();
      CRegistryItr cregistryItr = new CRegistryItr(this.Begin());
      while (cregistryItr != this.End())
      {
        if (((CExecutable) cregistryItr.GetElement().GetData()).Run() == CExecutable.RunResult.TerminalAndExecutableWasDeleted)
        {
          CRegistryElement element = cregistryItr.GetElement();
          ++cregistryItr;
          this.Remove((CSystemElement) element);
        }
        else
          ++cregistryItr;
      }
    }
  }
}
