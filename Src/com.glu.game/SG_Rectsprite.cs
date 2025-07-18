// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Rectsprite
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Rectsprite
  {
    public int color;
    public ushort width;
    public ushort height;
    public byte tag;

    public SG_Rectsprite()
    {
      this.color = -1;
      this.width = (ushort) 0;
      this.height = (ushort) 0;
      this.tag = (byte) 0;
    }

    public bool Load(DataInputStream dis)
    {
      this.color = dis.ReadInt32();
      this.width = dis.ReadUInt16();
      this.height = dis.ReadUInt16();
      this.tag = (byte) dis.ReadInt8();
      return !dis.GetFail();
    }
  }
}
