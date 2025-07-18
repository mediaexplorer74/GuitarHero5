// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgenString
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  [XmlType(AnonymousType = true, Namespace = "http://www.glu.com/schema/resGen")]
  public class resgenString
  {
    private string nameField;
    private string valueField;

    [XmlAttribute]
    public string name
    {
      get => this.nameField;
      set => this.nameField = value;
    }

    [XmlAttribute]
    public string value
    {
      get => this.valueField;
      set => this.valueField = value;
    }
  }
}
