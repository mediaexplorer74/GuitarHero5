// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Palette
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

#nullable disable
namespace com.glu.game
{
  public class SG_Palette
  {
    public ushort byteCount;
    public byte[] bytes;

    public SG_Palette()
    {
      this.byteCount = (ushort) 0;
      this.bytes = (byte[]) null;
    }

    public bool Load(DataInputStream dis)
    {
      this.byteCount = dis.ReadUInt16();
      bool flag;
      if (this.byteCount > (ushort) 0)
      {
        this.bytes = new byte[(int) this.byteCount];
        if (this.bytes != null)
          dis.Read(this.bytes, (uint) this.byteCount);
        flag = !dis.GetFail();
      }
      else
        flag = true;
      return flag;
    }
  }
}
