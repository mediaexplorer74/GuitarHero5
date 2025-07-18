// Decompiled with JetBrains decompiler
// Type: com.glu.game.CEmbededMovie
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class CEmbededMovie : CMovieObject
  {
    protected CMovie m_pEmbededMovie;
    protected Vector<CEmbededMovie.KeyFrame> m_KeyFrames = new Vector<CEmbededMovie.KeyFrame>();

    public CEmbededMovie()
    {
      this.m_Type = MovieObjectType.MOTY_EMBEDED_MOVIE;
      this.m_pEmbededMovie = (CMovie) null;
    }

    public override void Init(CMovie pMovie, CInputStream pStream)
    {
      base.Init(pMovie, pStream);
      this.m_KeyFrames.Allocate(pStream.ReadUInt16());
      for (int index = 0; index < this.m_KeyFrames.Length(); ++index)
      {
        if (this.m_KeyFrames[index] == null)
          this.m_KeyFrames[index] = new CEmbededMovie.KeyFrame();
        CEmbededMovie.KeyFrame keyFrame = this.m_KeyFrames[index];
        keyFrame.timeMS = pStream.ReadUInt32();
        keyFrame.movieID = pStream.ReadUInt8();
        keyFrame.visible = pStream.ReadUInt8() == (byte) 1;
      }
    }

    public override void Load()
    {
      if (this.m_pEmbededMovie == null)
        return;
      this.m_pEmbededMovie.Load();
    }

    public override void Refresh(uint time0, uint time1)
    {
      if (this.m_pEmbededMovie == null)
        return;
      if (!this.m_pMovie.IsVisible())
      {
        this.m_pEmbededMovie.SetVisible(false);
      }
      else
      {
        this.GetKeyFrames(time1, (CMovieObject) this, (uint) this.m_KeyFrames.Length());
        CEmbededMovie.KeyFrame keyFrame1 = (CEmbededMovie.KeyFrame) CMovieObject.pp[0];
        CEmbededMovie.KeyFrame keyFrame2 = (CEmbededMovie.KeyFrame) CMovieObject.pp[1];
        if (keyFrame1 == null || !keyFrame1.visible)
        {
          this.m_pEmbededMovie.SetVisible(false);
        }
        else
        {
          this.m_pEmbededMovie.SetVisible(true);
          uint timeMS = (this.m_pMovie.GetTimeMS() - keyFrame1.timeMS) % this.m_pEmbededMovie.GetLengthMS();
          this.m_pEmbededMovie.X = this.m_pMovie.X;
          this.m_pEmbededMovie.Y = this.m_pMovie.Y;
          this.m_pEmbededMovie.SetTime(timeMS);
        }
      }
    }

    public override void Draw()
    {
      if (this.m_pEmbededMovie == null)
        return;
      this.m_pEmbededMovie.Draw();
    }

    public void InitEmbededMovie()
    {
      if (this.m_pEmbededMovie != null)
        return;
      this.m_pEmbededMovie = new CMovie();
      this.m_pEmbededMovie.Init("GLU_MOVIE_MOVIE" + (object) this.m_KeyFrames[0].movieID);
      this.m_pEmbededMovie.SetVisible(false);
    }

    public new class KeyFrame : CMovieObject.KeyFrame
    {
      public byte movieID;
      public bool visible;
    }
  }
}
