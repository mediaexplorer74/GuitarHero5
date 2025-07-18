// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgenSurface
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  [XmlType(AnonymousType = true, Namespace = "http://www.glu.com/schema/resGen")]
  public class resgenSurface
  {
    private string nameField;
    private string formatField;
    private string widthField;
    private string heightField;
    private string srcimgField;
    private string refimgField;
    private string paletteField;
    private string transformField;
    private string mimeField;

    [XmlAttribute]
    public string name
    {
      get => this.nameField;
      set => this.nameField = value;
    }

    [XmlAttribute]
    public string format
    {
      get => this.formatField;
      set => this.formatField = value;
    }

    [XmlAttribute]
    public string width
    {
      get => this.widthField;
      set => this.widthField = value;
    }

    [XmlAttribute]
    public string height
    {
      get => this.heightField;
      set => this.heightField = value;
    }

    [XmlAttribute]
    public string srcimg
    {
      get => this.srcimgField;
      set => this.srcimgField = value;
    }

    [XmlAttribute]
    public string refimg
    {
      get => this.refimgField;
      set => this.refimgField = value;
    }

    [XmlAttribute]
    public string palette
    {
      get => this.paletteField;
      set => this.paletteField = value;
    }

    [XmlAttribute]
    public string transform
    {
      get => this.transformField;
      set => this.transformField = value;
    }

    [XmlAttribute]
    public string mime
    {
      get => this.mimeField;
      set => this.mimeField = value;
    }
  }
}
