// Decompiled with JetBrains decompiler
// Type: com.glu.shared.tGTFHeader
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class tGTFHeader
  {
    public const uint SizeOf = 12;
    public sbyte m_versionGeneration;
    public sbyte m_versionMajor;
    public sbyte m_versionMinor;
    public sbyte m_flags;
    public sbyte m_ascent;
    public sbyte m_height;
    public sbyte m_minCharSpacing;
    public sbyte m_minLineSpacing;
    public short m_numChars;
    public short m_numControlChars;

    public bool Load(CInputStream inStream)
    {
      this.m_versionGeneration = inStream.ReadInt8();
      this.m_versionMajor = inStream.ReadInt8();
      this.m_versionMinor = inStream.ReadInt8();
      this.m_flags = inStream.ReadInt8();
      this.m_ascent = inStream.ReadInt8();
      this.m_height = inStream.ReadInt8();
      this.m_minCharSpacing = inStream.ReadInt8();
      this.m_minLineSpacing = inStream.ReadInt8();
      this.m_numChars = inStream.ReadInt16();
      this.m_numControlChars = inStream.ReadInt16();
      return !inStream.GetFail();
    }
  }
}
