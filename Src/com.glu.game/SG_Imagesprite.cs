// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Imagesprite
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Imagesprite
  {
    public ushort tintID;
    public byte tag;
    public byte transform;

    public SG_Imagesprite()
    {
      this.tintID = (ushort) 0;
      this.tag = (byte) 0;
      this.transform = SquareTransform.TRANS_NONE;
    }

    public bool Load(DataInputStream dis)
    {
      this.tintID = dis.ReadUInt16();
      this.transform = dis.ReadUInt8();
      this.tag = dis.ReadUInt8();
      return !dis.GetFail();
    }
  }
}
