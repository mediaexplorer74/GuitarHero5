// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Spritemap
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Spritemap
  {
    public ushort spritelinkCount;
    public SG_Spritemap.SG_Spritelink[] spritelinks;

    public SG_Spritemap()
    {
      this.spritelinkCount = (ushort) 0;
      this.spritelinks = (SG_Spritemap.SG_Spritelink[]) null;
    }

    public bool Load(DataInputStream dis)
    {
      bool flag = false;
      this.spritelinkCount = dis.ReadUInt16();
      if (this.spritelinkCount > (ushort) 0)
      {
        this.spritelinks = new SG_Spritemap.SG_Spritelink[(int) this.spritelinkCount];
        if (this.spritelinks != null)
        {
          for (int index = 0; index < (int) this.spritelinkCount; ++index)
            this.spritelinks[index] = new SG_Spritemap.SG_Spritelink()
            {
              spriteIDBefore = dis.ReadUInt16(),
              spriteIDAfter = dis.ReadUInt16(),
              type = (SG_SpritelinkType) dis.ReadUInt8(),
              offsetX = dis.ReadInt16(),
              offsetY = dis.ReadInt16()
            };
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public struct SG_Spritelink
    {
      public ushort spriteIDBefore;
      public ushort spriteIDAfter;
      public SG_SpritelinkType type;
      public short offsetX;
      public short offsetY;

      public SG_SpritelinkType GetType() => this.type;

      public void GetOffsets(out short out_offsetX, out short out_offsetY)
      {
        out_offsetX = this.offsetX;
        out_offsetY = this.offsetY;
      }
    }
  }
}
