// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSongMedia
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class CSongMedia
  {
    public uint[] trackSizes = new uint[7];
    public CMedia mainTrack;
    public CMedia instrumentTrack = new CMedia();
    public CSongMedia.eSongMediaState state;
    public CSongMedia.SongMediaPtr instrumentNotes = new CSongMedia.SongMediaPtr();
    public CSongMedia.SongMediaPtr tempoNotes = new CSongMedia.SongMediaPtr();
    public CSongMedia.SongMediaPtr loadBuffer = new CSongMedia.SongMediaPtr();

    public bool ParseNoteData(
      Consts.eInstrument instrument,
      Consts.eDifficulty difficulty,
      CInputStream stream,
      uint noteSize)
    {
      bool flag1 = true;
      bool endian = stream.GetEndian();
      stream.SetEndian(true);
      uint num1 = (uint) stream.ReadUInt8();
      uint num2 = (uint) ((int) stream.ReadUInt8() * (int) num1 + 1);
      uint n = 0;
      int num3 = (int) (1 + 3 * (int) instrument + difficulty);
      for (uint index = 0; index < num2; ++index)
      {
        uint num4 = stream.ReadUInt32();
        if (index == 0U)
        {
          this.tempoNotes.size = num4;
        }
        else
        {
          if ((long) index == (long) num3)
          {
            this.instrumentNotes.size = num4;
            stream.Skip((uint) (((int) num2 - ((int) index + 1)) * 4));
            break;
          }
          n += num4;
        }
      }
      uint num5 = (uint) ((int) n + (int) this.tempoNotes.size + (int) this.instrumentNotes.size + (int) num2 * 4 + 2);
      bool flag2 = ((flag1 & num5 <= noteSize ? 1 : 0) & (this.tempoNotes.size <= 0U ? 0 : (this.instrumentNotes.size > 0U ? 1 : 0))) != 0;
      if (flag2)
      {
        this.tempoNotes.ptr = new byte[(int) this.tempoNotes.size];
        if (this.tempoNotes.ptr != null)
          stream.Read(this.tempoNotes.ptr, this.tempoNotes.size);
        else
          flag2 = false;
      }
      if (flag2)
      {
        stream.Skip(n);
        this.instrumentNotes.ptr = new byte[(int) this.instrumentNotes.size];
        if (this.instrumentNotes.ptr != null)
          stream.Read(this.instrumentNotes.ptr, this.instrumentNotes.size);
        else
          flag2 = false;
      }
      if (flag2)
        stream.Skip(noteSize - num5);
      bool noteData = flag2 & !stream.GetFail();
      stream.SetEndian(endian);
      return noteData;
    }

    public CSongMedia()
    {
      this.state = CSongMedia.eSongMediaState.SONGMEDIA_STATE_INITIAL;
      this.mainTrack = (CMedia) null;
      this.instrumentTrack = (CMedia) null;
    }

    public void ResetTrackSizes()
    {
      for (int index = 0; index < 7; ++index)
        this.trackSizes[index] = 0U;
    }

    public enum eSongMediaState
    {
      SONGMEDIA_STATE_ERROR,
      SONGMEDIA_STATE_INITIAL,
      SONGMEDIA_STATE_DOWNLOADED,
      SONGMEDIA_STATE_LOADING,
      SONGMEDIA_STATE_LOADED,
    }

    public class SongMediaPtr
    {
      public byte[] ptr;
      public uint size;

      public SongMediaPtr()
      {
        this.ptr = (byte[]) null;
        this.size = 0U;
      }
    }
  }
}
