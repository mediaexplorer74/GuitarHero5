// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Tint
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Tint
  {
    public SG_Image image = new SG_Image();
    public sbyte paletteID;

    public SG_Tint() => this.paletteID = (sbyte) -1;

    public bool Load(DataInputStream dis)
    {
      this.image.m_imageID = dis.ReadUInt16();
      this.paletteID = dis.ReadInt8();
      return !dis.GetFail();
    }
  }
}
