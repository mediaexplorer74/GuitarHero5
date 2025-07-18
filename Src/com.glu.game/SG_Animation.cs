// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Animation
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System;

#nullable disable
namespace com.glu.game
{
  public class SG_Animation
  {
    public byte transformsSupported;
    public byte timedframeCount;
    public ushort[] timedframeID__to__frameID;
    public ushort[] timedframeID__to__duration;

    public SG_Animation()
    {
      this.timedframeCount = (byte) 0;
      this.timedframeID__to__frameID = (ushort[]) null;
      this.timedframeID__to__duration = (ushort[]) null;
    }

    public bool Load(DataInputStream dis)
    {
      bool flag = false;
      this.transformsSupported = dis.ReadUInt8();
      this.timedframeCount = dis.ReadUInt8();
      if (this.timedframeCount > (byte) 0)
      {
        this.timedframeID__to__frameID = new ushort[(int) this.timedframeCount];
        this.timedframeID__to__duration = new ushort[(int) this.timedframeCount];
        if (this.timedframeID__to__frameID != null && this.timedframeID__to__duration != null)
        {
          for (uint index = 0; index < (uint) this.timedframeCount; ++index)
          {
            this.timedframeID__to__frameID[(int) index] = dis.ReadUInt16();
            this.timedframeID__to__duration[(int) index] = (ushort) (10U * (uint) dis.ReadUInt16());
          }
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }
  }
}
