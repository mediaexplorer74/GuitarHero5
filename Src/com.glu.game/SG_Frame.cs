// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Frame
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Frame
  {
    public ushort orderedlayerCount;
    public SG_Frame.SG_Orderedlayer[] orderedlayers;

    public SG_Frame()
    {
      this.orderedlayerCount = (ushort) 0;
      this.orderedlayers = (SG_Frame.SG_Orderedlayer[]) null;
    }

    public bool Load(DataInputStream dis)
    {
      bool flag = false;
      this.orderedlayerCount = (ushort) dis.ReadUInt8();
      if (this.orderedlayerCount > (ushort) 0)
      {
        this.orderedlayers = new SG_Frame.SG_Orderedlayer[(int) this.orderedlayerCount];
        if (this.orderedlayers != null)
        {
          for (int index = 0; index < (int) this.orderedlayerCount; ++index)
            this.orderedlayers[index] = new SG_Frame.SG_Orderedlayer()
            {
              layerID = dis.ReadUInt16(),
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

    public struct SG_Orderedlayer
    {
      public ushort layerID;
      public short locationX;
      public short locationY;
    }
  }
}
