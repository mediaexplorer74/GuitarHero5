// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgenReader
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  public class resgenReader : ContentTypeReader<resgen>
  {
    protected override resgen Read(ContentReader input, resgen existingInstance)
    {
      existingInstance = (resgen) new XmlSerializer(typeof (resgen)).Deserialize(input.BaseStream);
      return existingInstance;
    }
  }
}
