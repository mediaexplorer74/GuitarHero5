// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Character
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Character
  {
    public BitVector imagespriteUseBitVector = new BitVector();
    public bool loaded;
    public sbyte spritemapID;

    public SG_Character()
    {
      this.loaded = false;
      this.spritemapID = (sbyte) -1;
    }

    public bool Load(DataInputStream dis, ushort imagespriteCount)
    {
      this.loaded = false;
      this.imagespriteUseBitVector.Init((uint) imagespriteCount, dis);
      this.spritemapID = dis.ReadInt8();
      return !dis.GetFail();
    }
  }
}
