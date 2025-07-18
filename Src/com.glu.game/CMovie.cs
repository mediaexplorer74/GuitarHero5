// Decompiled with JetBrains decompiler
// Type: com.glu.game.CMovie
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class CMovie
  {
    public short X;
    public short Y;
    public bool Loop;
    protected Vector<CMovieObject> m_Objects = new Vector<CMovieObject>();
    protected CMovieRegion[] m_Regions = new CMovieRegion[16];
    protected uint m_Length;
    protected uint m_TimeMS;
    protected uint m_TimeMS0;
    protected byte m_NumRegions;
    protected sbyte m_LoopChapter;
    protected sbyte m_Chapter;
    protected bool m_Looped;
    protected bool m_Visible;
    protected CMovieChapter m_pChapterSet;
    protected short m_ReferenceWidth;
    protected short m_ReferenceHeight;
    public static bool[] updated = new bool[30];

    public byte GetNumRegions() => this.m_NumRegions;

    public uint GetTimeMS() => this.m_TimeMS;

    public bool GetLooped() => this.m_Looped;

    public bool IsVisible() => this.m_Visible;

    public uint GetLengthMS() => this.m_Length;

    public int GetChapter() => (int) this.m_Chapter;

    public int GetReferenceWidth() => (int) this.m_ReferenceWidth;

    public int GetReferenceHeight() => (int) this.m_ReferenceHeight;

    public CMovie()
    {
      this.m_TimeMS0 = this.m_TimeMS = 0U;
      this.m_Visible = true;
      this.m_Looped = false;
      this.Loop = true;
      this.X = (short) 0;
      this.Y = (short) 0;
      this.m_pChapterSet = (CMovieChapter) null;
      this.m_Chapter = (sbyte) -1;
      this.m_LoopChapter = (sbyte) -1;
    }

    public void Free()
    {
      this.m_pChapterSet = (CMovieChapter) null;
      this.m_Objects.Clear();
    }

    public void Init(string movie)
    {
      this.Free();
      CArrayInputStream pStream = new CArrayInputStream();
      CResource resource1;
      int resource2 = (int) CApp.GetResourceManager().CreateResource(movie, out resource1);
      byte[] data = ((CBinary) resource1.GetData()).GetData();
      pStream.Open(data, (uint) data.Length);
      this.m_ReferenceWidth = pStream.ReadInt16();
      this.m_ReferenceHeight = pStream.ReadInt16();
      this.m_Length = pStream.ReadUInt32();
      this.m_Objects.Allocate(pStream.ReadUInt16());
      this.m_NumRegions = (byte) 0;
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        MovieObjectType movieObjectType = (MovieObjectType) pStream.ReadUInt8();
        this.m_Objects[index] = (CMovieObject) null;
        switch (movieObjectType)
        {
          case MovieObjectType.MOTY_SPRITE:
            this.m_Objects[index] = (CMovieObject) new CMovieSprite();
            break;
          case MovieObjectType.MOTY_TILED_SPRITE:
            this.m_Objects[index] = (CMovieObject) new CMovieTiledSprite();
            break;
          case MovieObjectType.MOTY_EMBEDED_MOVIE:
            this.m_Objects[index] = (CMovieObject) new CEmbededMovie();
            break;
          case MovieObjectType.MOTY_TEXT:
            this.m_Objects[index] = (CMovieObject) new CMovieText();
            break;
          case MovieObjectType.MOTY_CHAPTER_SET:
            this.m_pChapterSet = new CMovieChapter();
            this.m_Objects[index] = (CMovieObject) this.m_pChapterSet;
            break;
          case MovieObjectType.MOTY_USER_REGION:
            this.m_Regions[(int) this.m_NumRegions] = new CMovieRegion();
            this.m_Objects[index] = (CMovieObject) this.m_Regions[(int) this.m_NumRegions];
            ++this.m_NumRegions;
            break;
          case MovieObjectType.MOTY_FILL:
            this.m_Objects[index] = (CMovieObject) new CMovieFill();
            break;
        }
        if (this.m_Objects[index] != null)
        {
          this.m_Objects[index].Init(this, (CInputStream) pStream);
          this.m_Objects[index].Index = (byte) index;
        }
      }
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        if (this.m_Objects[index] != null && this.m_Objects[index].GetMovieObjectType() == MovieObjectType.MOTY_EMBEDED_MOVIE)
          ((CEmbededMovie) this.m_Objects[index]).InitEmbededMovie();
      }
    }

    public void Load()
    {
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        if (this.m_Objects[index] != null)
          this.m_Objects[index].Load();
      }
    }

    public void Update(uint elapseMS)
    {
      if (this.m_pChapterSet == null)
        return;
      uint num1 = this.m_Length;
      uint num2 = 0;
      this.m_Looped = false;
      this.m_TimeMS0 = this.m_TimeMS;
      this.m_TimeMS += elapseMS;
      if (this.m_LoopChapter != (sbyte) -1 && this.m_TimeMS >= this.m_pChapterSet.GetChapterTimeMS((int) this.m_LoopChapter))
      {
        this.m_Chapter = this.m_LoopChapter;
        this.m_LoopChapter = (sbyte) -1;
      }
      if (this.m_Chapter >= (sbyte) 0)
      {
        num2 = this.m_pChapterSet.GetChapterTimeMS((int) this.m_Chapter);
        num1 = this.m_pChapterSet.GetChapterLengthMS((int) this.m_Chapter);
      }
      if (this.m_TimeMS > num2 + num1)
      {
        if (!this.Loop)
        {
          this.m_TimeMS = num2 + num1;
        }
        else
        {
          this.m_TimeMS0 = num2;
          this.m_TimeMS = (uint) ((int) num2 + (int) this.m_TimeMS - ((int) num2 + (int) num1));
        }
        this.m_Looped = true;
      }
      this.Refresh();
    }

    public void Draw()
    {
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        if (this.m_Objects[index] != null)
          this.m_Objects[index].Draw();
      }
    }

    public void SetTime(uint timeMS)
    {
      uint num = this.m_Length;
      if (this.m_Chapter >= (sbyte) 0)
        num = this.m_pChapterSet.GetChapterTimeMS((int) this.m_Chapter) + this.m_pChapterSet.GetChapterLengthMS((int) this.m_Chapter);
      timeMS = timeMS > num ? num : timeMS;
      this.m_TimeMS0 = this.m_TimeMS = timeMS;
      this.m_Looped = false;
      this.Refresh();
    }

    public void SetVisible(bool visible)
    {
      if (this.m_Visible == visible)
        return;
      this.m_Visible = visible;
      this.Refresh();
    }

    public void SetChapter(int chapter, bool playThrough)
    {
      if (this.m_pChapterSet == null)
        return;
      this.m_Chapter = !playThrough ? (sbyte) chapter : (sbyte) -1;
      this.m_TimeMS0 = chapter >= 0 ? (this.m_TimeMS = this.m_pChapterSet.GetChapterTimeMS(chapter)) : (this.m_TimeMS = 0U);
      this.m_Looped = false;
      this.Refresh();
    }

    public void SetLoopChapter(int chapter)
    {
      if (this.m_pChapterSet == null)
        return;
      this.m_LoopChapter = (sbyte) chapter;
    }

    public CMovieObject GetObject(int i) => this.m_Objects[i];

    protected void RefreshMovieObject(
      CMovie pMovie,
      CMovieObject pObject,
      bool[] pUpdated,
      uint time0,
      uint time)
    {
      byte anchorObject = pObject.GetAnchorObject();
      if (anchorObject != byte.MaxValue)
        this.RefreshMovieObject(pMovie, pMovie.GetObject((int) anchorObject), pUpdated, time0, time);
      if (pUpdated[(int) pObject.Index])
        return;
      pUpdated[(int) pObject.Index] = true;
      pObject.Refresh(time0, time);
    }

    public void Refresh()
    {
      for (int index = 0; index < this.m_Objects.Count; ++index)
        CMovie.updated[index] = false;
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        if (!CMovie.updated[index])
          this.RefreshMovieObject(this, this.m_Objects[index], CMovie.updated, this.m_TimeMS0, this.m_TimeMS);
      }
    }

    public void ResetPlayback()
    {
      this.m_Chapter = (sbyte) -1;
      this.m_LoopChapter = (sbyte) -1;
      this.m_Looped = false;
      this.m_TimeMS = this.m_TimeMS0 = 0U;
    }

    public bool GetUserRegion(uint i, ref CRectangle pRegion)
    {
      return i < (uint) this.m_NumRegions && this.m_Regions[(int) i] != null && this.m_Regions[(int) i].GetRegion(ref pRegion);
    }

    public bool GetUserRegionVisible(uint i)
    {
      return i < (uint) this.m_NumRegions && this.m_Regions[(int) i] != null && this.m_Regions[(int) i].IsVisible();
    }

    public int GetUserRegionID(CMovieRegion pRegion)
    {
      for (int userRegionId = 0; userRegionId < (int) this.m_NumRegions; ++userRegionId)
      {
        if (this.m_Regions[userRegionId] == pRegion)
          return userRegionId;
      }
      return -1;
    }

    public void SetUserRegionCallback(int i, MovieRegionCallback pCallback, object pCaller)
    {
      if (i >= (int) this.m_NumRegions || this.m_Regions[i] == null)
        return;
      this.m_Regions[i].SetCallback(pCallback, pCaller);
    }

    public int GetChapterLength(sbyte chapter)
    {
      return this.m_pChapterSet == null ? 0 : (int) this.m_pChapterSet.GetChapterLengthMS((int) chapter);
    }

    public int GetChapterTimeMS(sbyte chapter)
    {
      return this.m_pChapterSet == null ? 0 : (int) this.m_pChapterSet.GetChapterTimeMS((int) chapter);
    }
  }
}
