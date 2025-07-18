// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovieChapter
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CMovieChapter : CMovieObject
  {
    protected Vector<CMovieObject.KeyFrame> m_KeyFrames = new Vector<CMovieObject.KeyFrame>();

    public uint GetNumChapters() => (uint) this.m_KeyFrames.Length();

    public uint GetChapterTimeMS(int chapter) => this.m_KeyFrames[chapter].timeMS;

    public override void Init(CMovie pMovie, CInputStream pStream)
    {
      base.Init(pMovie, pStream);
      this.m_KeyFrames.Allocate((ushort) ((uint) pStream.ReadUInt16() + 1U));
      if (this.m_KeyFrames[0] == null)
        this.m_KeyFrames[0] = new CMovieObject.KeyFrame();
      this.m_KeyFrames[0].timeMS = 0U;
      for (int index = 1; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CMovieObject.KeyFrame();
        this.m_KeyFrames[index].timeMS = pStream.ReadUInt32();
      }
    }

    public override CMovieObject.KeyFrame GetKeyFrame(int i) => this.m_KeyFrames[i];

    public uint GetChapterLengthMS(int chapter)
    {
      if (chapter == -1 || this.m_KeyFrames.Length() == 0)
        return this.m_pMovie.GetLengthMS();
      return (long) (uint) chapter < (long) (this.m_KeyFrames.Length() - 1) ? this.m_KeyFrames[chapter + 1].timeMS - this.m_KeyFrames[chapter].timeMS : this.m_pMovie.GetLengthMS() - this.m_KeyFrames[chapter].timeMS;
    }
  }
}
