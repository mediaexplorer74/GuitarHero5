// Decompiled with JetBrains decompiler
// Type: com.glu.resgen_content.resgen
// Assembly: resgen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D905AB2-A889-4909-862C-9AB26ED62751
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\resgen.dll

using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace com.glu.resgen_content
{
  [XmlType(AnonymousType = true, Namespace = "http://www.glu.com/schema/resGen")]
  [XmlRoot(Namespace = "http://www.glu.com/schema/resGen", IsNullable = false)]
  public class resgen
  {
    [ContentSerializer]
    public resgen.HashToKeyInfo[] m_hashToKeyInfo;
    [ContentSerializer]
    public int m_hashToKeyInfoLength;
    private object[] itemsField;
    private string nameField;
    private byte versionField;

    public void BuildHeyToKeyLUT()
    {
      this.m_hashToKeyInfoLength = this.Items.Length / 4;
      this.m_hashToKeyInfo = new resgen.HashToKeyInfo[this.m_hashToKeyInfoLength];
      for (int index = 0; index < this.m_hashToKeyInfoLength; ++index)
        this.m_hashToKeyInfo[index] = new resgen.HashToKeyInfo();
      int itemIndex = 0;
      foreach (object obj in this.Items)
      {
        string str = (string) null;
        if ((object) obj.GetType() == (object) typeof (resgenBinary))
          str = ((resgenBinary) obj).name;
        else if ((object) obj.GetType() == (object) typeof (resgenKeyset))
          str = ((resgenKeyset) obj).name;
        else if ((object) obj.GetType() == (object) typeof (resgenImage))
          str = ((resgenImage) obj).name;
        else if ((object) obj.GetType() == (object) typeof (resgenSound))
          str = ((resgenSound) obj).name;
        else if ((object) obj.GetType() == (object) typeof (resgenString))
          str = ((resgenString) obj).name;
        else if ((object) obj.GetType() == (object) typeof (resgenSurface))
          str = ((resgenSurface) obj).name;
        if (str != null)
          resgen.HashToKeyInfoLUT.Add(this.m_hashToKeyInfo, (int) ((long) CStringToKey.Call(str) % (long) this.m_hashToKeyInfoLength), itemIndex);
        ++itemIndex;
      }
    }

    public static string GetNameIfSupported(object obj)
    {
      if ((object) obj.GetType() == (object) typeof (resgenBinary))
        return ((resgenBinary) obj).name;
      if ((object) obj.GetType() == (object) typeof (resgenImage))
        return ((resgenImage) obj).name;
      if ((object) obj.GetType() == (object) typeof (resgenKeyset))
        return ((resgenKeyset) obj).name;
      if ((object) obj.GetType() == (object) typeof (resgenSound))
        return ((resgenSound) obj).name;
      if ((object) obj.GetType() == (object) typeof (resgenString))
        return ((resgenString) obj).name;
      return (object) obj.GetType() == (object) typeof (resgenSurface) ? ((resgenSurface) obj).name : (string) null;
    }

    public static bool IfNameIsSupportedAreStringsEqual(object obj, string str)
    {
      string nameIfSupported = resgen.GetNameIfSupported(obj);
      return nameIfSupported != null && nameIfSupported == str;
    }

    public object Find(string name)
    {
      foreach (int index in (IEnumerable<int>) resgen.HashToKeyInfoLUT.GetItemList(this.m_hashToKeyInfo, (int) ((long) CStringToKey.Call(name) % (long) this.m_hashToKeyInfoLength)))
      {
        if (resgen.IfNameIsSupportedAreStringsEqual(this.Items[index], name))
          return this.Items[index];
      }
      return (object) null;
    }

    [XmlElement("text", typeof (resgenText))]
    [XmlElement("surface", typeof (resgenSurface))]
    [XmlElement("binary", typeof (resgenBinary))]
    [XmlElement("group", typeof (resgenGroup))]
    [XmlElement("image", typeof (resgenImage))]
    [XmlElement("import", typeof (resgenImport))]
    [XmlElement("keyset", typeof (resgenKeyset))]
    [XmlElement("sound", typeof (resgenSound))]
    [XmlElement("string", typeof (resgenString))]
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

    [XmlAttribute]
    public byte version
    {
      get => this.versionField;
      set => this.versionField = value;
    }

    [ContentSerializerRuntimeType("com.glu.resgen_content.resgen, resgen_processor")]
    public class HashToKeyInfo
    {
      [ContentSerializer]
      public List<int> m_collisionList;

      public HashToKeyInfo() => this.m_collisionList = new List<int>(5);
    }

    [ContentSerializerRuntimeType("com.glu.resgen_content.resgen, resgen_processor")]
    public static class HashToKeyInfoLUT
    {
      public static void Add(resgen.HashToKeyInfo[] lut, int hashKeyLUTIndex, int itemIndex)
      {
        lut[hashKeyLUTIndex].m_collisionList.Add(itemIndex);
      }

      public static IList<int> GetItemList(resgen.HashToKeyInfo[] lut, int hashKeyLUTIndex)
      {
        return (IList<int>) lut[hashKeyLUTIndex].m_collisionList;
      }
    }
  }
}
