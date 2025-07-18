// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieText
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMovieText : CMovieObject
  {
    protected Vector<CMovieText.KeyFrame> m_KeyFrames = new Vector<CMovieText.KeyFrame>();

    public override CMovieObject.KeyFrame GetKeyFrame(int i)
    {
      return (CMovieObject.KeyFrame) this.m_KeyFrames[i];
    }

    public override void Init(CMovie pMovie, CInputStream pStream)
    {
      base.Init(pMovie, pStream);
      this.m_KeyFrames.Allocate(pStream.ReadUInt16());
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CMovieText.KeyFrame();
        CMovieText.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.x = (short) pStream.ReadUInt16();
        keyFrame.y = (short) pStream.ReadUInt16();
        keyFrame.length = (short) pStream.ReadUInt16();
        keyFrame.visible = pStream.ReadUInt8();
        keyFrame.stringID = pStream.ReadUInt8();
      }
    }

    public override void Load()
    {
    }

    public override void Refresh(uint time0, uint time1)
    {
      if (!this.m_pMovie.IsVisible())
        return;
      this.GetKeyFrames(time1, (CMovieObject) this, (uint) this.m_KeyFrames.Length());
      CMovieText.KeyFrame keyFrame1 = (CMovieText.KeyFrame) CMovieObject.pp[0];
      CMovieText.KeyFrame keyFrame2 = (CMovieText.KeyFrame) CMovieObject.pp[1];
    }

    public override void Draw()
    {
    }

    public new class KeyFrame : CMovieObject.KeyFrame
    {
      public ushort animationTimeMS;
      public short x;
      public short y;
      public byte visible;
      public short length;
      public byte stringID;
    }
  }
}
