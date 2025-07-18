// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgenGroup
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  [XmlType(AnonymousType = true, Namespace = "http://www.glu.com/schema/resGen")]
  public class resgenGroup
  {
    private object[] itemsField;
    private string nameField;

    [XmlElement("define", typeof (resgenGroupDefine))]
    [XmlElement("entry", typeof (resgenGroupEntry))]
    public object[] Items
    {
      get => this.itemsField;
      set => this.itemsField = value;
    }

    [XmlAttribute]
    public string name
    {
      get => this.nameField;
      set => this.nameField = value;
    }
  }
}
