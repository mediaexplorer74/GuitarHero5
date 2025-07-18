// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgenText
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  [XmlType(AnonymousType = true, Namespace = "http://www.glu.com/schema/resGen")]
  public class resgenText
  {
    private string fileField;
    private string localeField;

    [XmlAttribute]
    public string file
    {
      get => this.fileField;
      set => this.fileField = value;
    }

    [XmlAttribute]
    public string locale
    {
      get => this.localeField;
      set => this.localeField = value;
    }
  }
}
