// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Layer
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Layer
  {
    public ushort positionedspriteCount;
    public SG_Layer.SG_Positionedsprite[] positionedsprites;

    public SG_Layer()
    {
      this.positionedspriteCount = (ushort) 0;
      this.positionedsprites = (SG_Layer.SG_Positionedsprite[]) null;
    }

    public bool Load(DataInputStream dis)
    {
      bool flag = false;
      this.positionedspriteCount = (ushort) dis.ReadUInt8();
      if (this.positionedspriteCount > (ushort) 0)
      {
        this.positionedsprites = new SG_Layer.SG_Positionedsprite[(int) this.positionedspriteCount];
        if (this.positionedsprites != null)
        {
          for (int index = 0; index < (int) this.positionedspriteCount; ++index)
            this.positionedsprites[index] = new SG_Layer.SG_Positionedsprite()
            {
              spriteID = dis.ReadUInt16(),
              locationX = dis.ReadInt16(),
              locationY = dis.ReadInt16()
            };
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public struct SG_Positionedsprite
    {
      public ushort spriteID;
      public short locationX;
      public short locationY;
    }
  }
}
