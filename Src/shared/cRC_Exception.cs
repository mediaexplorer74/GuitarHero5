// Decompiled with JetBrains decompiler
// Type: com.glu.shared.cRC_Exception
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.Diagnostics;

#nullable disable
namespace com.glu.shared
{
  public class cRC_Exception : Exception
  {
    public cRC_Exception(uint rc) => Debug.WriteLine("cRC.{0}", (object) cRC.ToString(rc));

    public cRC_Exception(string preMsg, uint rc)
    {
      Debug.WriteLine("{0} cRC.{1}", (object) preMsg, (object) cRC.ToString(rc));
    }

    public cRC_Exception(uint rc, string postMsg)
    {
      Debug.WriteLine("cRC.{0} {1}", (object) cRC.ToString(rc), (object) postMsg);
    }

    public cRC_Exception(string preMsg, uint rc, string postMsg)
    {
      Debug.WriteLine("{0} cRC.{1} {2}", (object) preMsg, (object) cRC.ToString(rc), (object) postMsg);
    }
  }
}
