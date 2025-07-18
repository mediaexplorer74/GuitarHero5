// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSong
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;
using System.Text;

#nullable disable
namespace com.glu.game
{
  internal class CSong
  {
    private const int NUM_SONG_FILE_LETTERS = 7;
    public const int NUM_SONG_MASKS = 7;
    public static string kwcsSongExtension = ".sng";
    public static string kwcsPartialSongExtension = ".prt";
    public uint SONG_ID_VENUE_MODULUS = 10;
    public uint kMaxOriginalSongID = 9999;
    private string kwcsMainTrack = "mainTrack";
    private string kwcsInstrumentTrack = "instrumentTrack";
    private string kcsMainTrack = "mainTrack";
    private string kcsInstrumentTrack = "instrumentTrack";
    private uint kMinHeadlineLen = 2;
    private static char[] kTableSongFileLetter = new char[7]
    {
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm'
    };
    public static int[] kTableSongMask = new int[7]
    {
      1,
      2,
      4,
      8,
      16,
      32,
      64
    };
    protected Consts.eDifficulty m_difficultyRequested;
    protected uint m_headlineGroup;
    protected Consts.eInstrument m_instrumentRequested;
    protected bool m_isEncore;
    protected bool m_isHeadline;
    protected uint m_songLocalMask;
    protected string[] m_metaStrings = new string[4];
    protected ushort m_order;
    protected uint m_songID;
    protected CSongMedia m_pSongMedia;

    public bool IsBonusSong() => this.m_songID > this.kMaxOriginalSongID;

    public bool IsEncore() => this.m_isEncore;

    public bool IsHeadline() => this.m_isHeadline;

    public bool IsSongMediaLoaded()
    {
      return this.m_pSongMedia != null && this.m_pSongMedia.state == CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADED;
    }

    public uint GetHeadlineGroup() => this.m_headlineGroup;

    public ushort GetOrder() => this.m_order;

    public string GetBandName() => this.GetMetaString(CSong.eMetaItemID.METAID_BAND_TEXT);

    public Consts.eDifficulty GetDifficultyReq() => this.m_difficultyRequested;

    public Consts.eInstrument GetInstrumentReq() => this.m_instrumentRequested;

    public uint GetSongID() => this.m_songID;

    public uint GetSongVenueID() => this.m_songID % this.SONG_ID_VENUE_MODULUS;

    public uint GetSongMediaLocal() => 3;

    public string GetSongName() => this.GetMetaString(CSong.eMetaItemID.METAID_SONG_TEXT);

    public CSongMedia GetSongMedia() => this.m_pSongMedia;

    public void SetHeadlineGroup(uint group) => this.m_headlineGroup = group;

    public void SetSongID(uint sID) => this.m_songID = sID;

    public CSong()
    {
      this.m_songID = 0U;
      this.m_pSongMedia = (CSongMedia) null;
      this.m_songLocalMask = 0U;
      this.m_headlineGroup = 0U;
      this.m_isEncore = false;
      this.m_isHeadline = false;
      this.m_order = (ushort) 0;
      this.m_instrumentRequested = Consts.eInstrument.INSTRUMENT_INVALID;
      this.m_difficultyRequested = Consts.eDifficulty.DIFFICULTY_EASY;
      for (int index = 0; index < 4; ++index)
        this.m_metaStrings[index] = (string) null;
    }

    public void HandleTransferError()
    {
      this.m_instrumentRequested = Consts.eInstrument.INSTRUMENT_INVALID;
      this.m_difficultyRequested = Consts.eDifficulty.DIFFICULTY_EASY;
      this.UnloadSongMedia();
      this.DetermineSongLocal();
    }

    private bool Init() => true;

    private void NextTrack(ref Consts.eSongTrack track)
    {
      Consts.eSongNumTracks numTracksSupported = CSongListMgr.GetNumTracksSupported();
      if (numTracksSupported == Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE && track == Consts.eSongTrack.SONG_NOTE_SHEET)
      {
        switch (this.m_instrumentRequested)
        {
          case Consts.eInstrument.INSTRUMENT_GUITAR:
            track = Consts.eSongTrack.SONG_TRACK_MAIN;
            break;
          case Consts.eInstrument.INSTRUMENT_BASS:
            track = Consts.eSongTrack.SONG_TRACK_BASS;
            break;
          case Consts.eInstrument.INSTRUMENT_DRUMS:
            track = Consts.eSongTrack.SONG_TRACK_DRUMS;
            break;
        }
      }
      else
        track = 1 + track;
      if (track < Consts.eSongTrack.SONG_TRACK_MAIN)
        return;
      if (numTracksSupported == Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE)
      {
        track = Consts.eSongTrack.SONG_TRACK_MAIN;
      }
      else
      {
        Consts.eSongTrack eSongTrack1 = Consts.eSongTrack.SONG_NOTE_SHEET;
        Consts.eSongTrack eSongTrack2 = Consts.eSongTrack.SONG_NOTE_SHEET;
        switch (this.m_instrumentRequested)
        {
          case Consts.eInstrument.INSTRUMENT_GUITAR:
            eSongTrack2 = Consts.eSongTrack.SONG_TRACK_MAIN;
            eSongTrack1 = Consts.eSongTrack.SONG_TRACK_NON_GUITAR;
            break;
          case Consts.eInstrument.INSTRUMENT_BASS:
            eSongTrack2 = Consts.eSongTrack.SONG_TRACK_BASS;
            eSongTrack1 = Consts.eSongTrack.SONG_TRACK_NON_BASS;
            break;
          case Consts.eInstrument.INSTRUMENT_DRUMS:
            eSongTrack2 = Consts.eSongTrack.SONG_TRACK_DRUMS;
            eSongTrack1 = Consts.eSongTrack.SONG_TRACK_NON_DRUMS;
            break;
        }
        if (track > eSongTrack1)
        {
          track = eSongTrack1;
        }
        else
        {
          if (track <= Consts.eSongTrack.SONG_NOTE_SHEET || track >= eSongTrack2)
            return;
          track = eSongTrack2;
        }
      }
    }

    private bool PrepContinue(ref Consts.eSongTrack fetchTrack, ref uint fetchSize)
    {
      bool flag1 = true;
      fetchTrack = Consts.eSongTrack.SONG_NOTE_SHEET;
      Consts.eSongTrack track1 = fetchTrack;
      bool flag2 = true;
      if (this.m_pSongMedia == null)
        this.m_pSongMedia = new CSongMedia();
      if (this.m_pSongMedia == null)
        flag1 = false;
      ICFileMgr instance = ICFileMgr.GetInstance();
      if (flag1)
      {
        Consts.eSongTrack track2;
        do
        {
          track2 = track1;
          uint num = instance.Size(this.GetSongFilepath(track2, false));
          if (num == 0U)
          {
            if (flag2)
              fetchTrack = track2;
            flag2 = false;
            num = instance.Size(this.GetSongFilepath(track2, true));
          }
          this.m_pSongMedia.trackSizes[(int) track2] = num;
          this.NextTrack(ref track1);
        }
        while (track2 != track1);
      }
      fetchSize = this.m_pSongMedia.trackSizes[(int) fetchTrack];
      return flag1;
    }

    public string GetMetaString(CSong.eMetaItemID id)
    {
      return id < CSong.eMetaItemID.METAID_ENCORE_BOOL ? this.m_metaStrings[(int) id] : (string) null;
    }

    public int GetPreloadedSongIndex()
    {
      for (int preloadedSongIndex = 0; preloadedSongIndex < Consts.kTablePreloadedSongs.Length; ++preloadedSongIndex)
      {
        if ((int) this.m_songID == (int) Consts.kTablePreloadedSongs[preloadedSongIndex].songID)
          return preloadedSongIndex;
      }
      return -1;
    }

    public uint Gebyte(Consts.eSongNumTracks nTracks, Consts.eInstrument instrument)
    {
      if (nTracks == Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE)
        return 3;
      switch (instrument)
      {
        case Consts.eInstrument.INSTRUMENT_GUITAR:
          return 7;
        case Consts.eInstrument.INSTRUMENT_BASS:
          return 25;
        case Consts.eInstrument.INSTRUMENT_DRUMS:
          return 97;
        default:
          return 0;
      }
    }

    public string GetSongFilepath(Consts.eSongTrack track, bool partialFile)
    {
      return CSong.GetSongFilepath(this.m_songID, track, partialFile);
    }

    public static string GetSongFilepath(uint songID, Consts.eSongTrack track, bool partialFile)
    {
      ICFileMgr instance = ICFileMgr.GetInstance();
      string applicationPath = instance.GetApplicationPath();
      if (applicationPath.Length > 0)
        applicationPath += instance.GetPathSeparator();
      if (applicationPath.Length > 0)
        applicationPath += ICFileMgr.GetInstance().GetPathSeparator();
      string str = applicationPath + CUtility.UIntTostring(songID, 16U) + CSong.kTableSongFileLetter[(int) track].ToString();
      return !partialFile ? str + CSong.kwcsSongExtension : str + CSong.kwcsPartialSongExtension;
    }

    private bool IsSongMediaInDir(Consts.eSongTrack track, bool checkPartial) => true;

    public uint GetSongMediaInDir(bool checkPartial)
    {
      uint songMediaInDir = 0;
      ICFileMgr instance = ICFileMgr.GetInstance();
      for (int track = 0; track < 7; ++track)
      {
        if (instance.Exists(this.GetSongFilepath((Consts.eSongTrack) track, checkPartial)))
          songMediaInDir += (uint) CSong.kTableSongMask[track];
      }
      return songMediaInDir;
    }

    public void DetermineSongLocal()
    {
      if (this.GetPreloadedSongIndex() == -1)
        return;
      this.m_songLocalMask = 3U;
    }

    public int GetWriteSize()
    {
      int num = 0 + 4 + 8;
      for (int index = 0; index < 4; ++index)
      {
        if (this.m_metaStrings[index] != null)
          num = num + 8 + 2 + this.m_metaStrings[index].Length;
      }
      return num + 8 + 2 + 8 + 2;
    }

    public bool LoadSongMedia(Consts.eInstrument instrument, Consts.eDifficulty difficulty)
    {
      this.m_instrumentRequested = instrument;
      this.m_difficultyRequested = difficulty;
      int num = (int) this.Gebyte(CSongListMgr.GetNumTracksSupported(), instrument);
      if (this.GetPreloadedSongIndex() == -1)
        return false;
      CSongListMgr.SetCurSongLoading(this, CSong.eSongMediaSource.SONG_SOURCE_RESOURCEFILE);
      return true;
    }

    private void ReadJMUtf8(CInputStream stream, out string ppOut)
    {
      bool endian = stream.GetEndian();
      stream.SetEndian(true);
      uint length = (uint) stream.ReadUInt16();
      stream.SetEndian(endian);
      byte[] numArray = new byte[(int) length];
      if (numArray != null)
        stream.Read(numArray, length);
      ppOut = Encoding.UTF8.GetString(numArray, 0, (int) length);
    }

    public bool ReadMeta(int index)
    {
      this.SetSongID(CActualSongList.songList[index].songID);
      this.m_isEncore = CActualSongList.songList[index].isEncore;
      this.m_order = CActualSongList.songList[index].order;
      this.m_metaStrings = CActualSongList.songList[index].metaStrings;
      this.m_isHeadline = CActualSongList.songList[index].isHeadline;
      return true;
    }

    private bool ReadSongMedia(CSong.eSongMediaSource source, bool loadOneFile)
    {
      bool flag1 = true;
      int index = 0;
      if (source == CSong.eSongMediaSource.SONG_SOURCE_RESOURCEFILE)
        index = this.GetPreloadedSongIndex();
      if (index == -1)
        return false;
      CResourceManager resourceManager = CApp.GetResourceManager();
      Consts.eSongTrack track = Consts.eSongTrack.SONG_NOTE_SHEET;
      Consts.eSongNumTracks numTracksSupported = CSongListMgr.GetNumTracksSupported();
      if (loadOneFile)
      {
        int val1 = (int) track;
        while (this.m_pSongMedia.trackSizes[val1] != 0U)
        {
          track = (Consts.eSongTrack) val1;
          ++val1;
          if (numTracksSupported != Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE)
          {
            switch (this.m_instrumentRequested)
            {
              case Consts.eInstrument.INSTRUMENT_GUITAR:
                val1 = CMath.Max(val1, 1);
                continue;
              case Consts.eInstrument.INSTRUMENT_BASS:
                val1 = CMath.Max(val1, 3);
                continue;
              case Consts.eInstrument.INSTRUMENT_DRUMS:
                val1 = CMath.Max(val1, 5);
                continue;
              default:
                continue;
            }
          }
        }
        if (source != CSong.eSongMediaSource.SONG_SOURCE_HEAP)
          track = (Consts.eSongTrack) val1;
      }
      do
      {
        bool flag2 = false;
        bool flag3 = false;
        string str = (string) null;
        switch (numTracksSupported)
        {
          case Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE:
            flag2 = true;
            switch (track)
            {
              case Consts.eSongTrack.SONG_NOTE_SHEET:
                str = Consts.kTablePreloadedSongs[index].sheet;
                break;
              case Consts.eSongTrack.SONG_TRACK_MAIN:
                str = Consts.kTablePreloadedSongs[index].mainTrack;
                break;
            }
            break;
          case Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE:
            switch (track)
            {
              case Consts.eSongTrack.SONG_NOTE_SHEET:
                flag2 = true;
                str = Consts.kTablePreloadedSongs[index].sheet;
                break;
              case Consts.eSongTrack.SONG_TRACK_MAIN:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_GUITAR)
                {
                  flag2 = flag3 = true;
                  str = Consts.kTablePreloadedSongs[index].guitarTrack;
                  break;
                }
                break;
              case Consts.eSongTrack.SONG_TRACK_NON_GUITAR:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_GUITAR)
                {
                  flag2 = true;
                  str = Consts.kTablePreloadedSongs[index].nonGuitarTrack;
                  break;
                }
                break;
              case Consts.eSongTrack.SONG_TRACK_BASS:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_BASS)
                {
                  flag2 = flag3 = true;
                  str = Consts.kTablePreloadedSongs[index].bassTrack;
                  break;
                }
                break;
              case Consts.eSongTrack.SONG_TRACK_NON_BASS:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_BASS)
                {
                  flag2 = true;
                  str = Consts.kTablePreloadedSongs[index].nonBassTrack;
                  break;
                }
                break;
              case Consts.eSongTrack.SONG_TRACK_DRUMS:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_DRUMS)
                {
                  flag2 = flag3 = true;
                  str = Consts.kTablePreloadedSongs[index].drumsTrack;
                  break;
                }
                break;
              case Consts.eSongTrack.SONG_TRACK_NON_DRUMS:
                if (this.m_instrumentRequested == Consts.eInstrument.INSTRUMENT_DRUMS)
                {
                  flag2 = true;
                  str = Consts.kTablePreloadedSongs[index].nonDrumsTrack;
                  break;
                }
                break;
            }
            break;
        }
        if (flag2)
        {
          CInputStream stream = (CInputStream) null;
          switch (source)
          {
            case CSong.eSongMediaSource.SONG_SOURCE_HEAP:
              CArrayInputStream carrayInputStream = new CArrayInputStream();
              flag1 &= carrayInputStream.Open(this.m_pSongMedia.loadBuffer.ptr, this.m_pSongMedia.trackSizes[(int) track]);
              if (flag1)
              {
                stream = (CInputStream) carrayInputStream;
                break;
              }
              break;
            case CSong.eSongMediaSource.SONG_SOURCE_LOCALDIR:
              CFileInputStream cfileInputStream1 = new CFileInputStream();
              flag1 &= cfileInputStream1.Open(this.GetSongFilepath(track, false));
              if (flag1)
              {
                stream = (CInputStream) cfileInputStream1;
                break;
              }
              break;
            case CSong.eSongMediaSource.SONG_SOURCE_RESOURCEFILE:
              if (str == null)
              {
                flag1 = false;
                break;
              }
              if (track == Consts.eSongTrack.SONG_NOTE_SHEET)
              {
                stream = (CInputStream) new DataInputStream(str);
                break;
              }
              break;
            case CSong.eSongMediaSource.SONG_SOURCE_LOCAL_TO_HEAP:
              bool flag4 = false;
              CFileInputStream cfileInputStream2 = new CFileInputStream();
              bool flag5 = flag1 & cfileInputStream2.Open(this.GetSongFilepath(track, false));
              if (this.m_pSongMedia.loadBuffer.ptr == null)
              {
                this.m_pSongMedia.loadBuffer.ptr = new byte[(int) cfileInputStream2.GetSize()];
                flag5 &= this.m_pSongMedia.loadBuffer.ptr != null;
                this.m_pSongMedia.loadBuffer.size = 0U;
              }
              int destinationIndex = 0;
              if (flag5)
              {
                cfileInputStream2.Skip(this.m_pSongMedia.loadBuffer.size);
                uint length = CMath.Min(25000U, cfileInputStream2.GetSize() - this.m_pSongMedia.loadBuffer.size);
                byte[] numArray = new byte[(int) length];
                cfileInputStream2.Read(numArray, length);
                Array.Copy((Array) numArray, 0, (Array) this.m_pSongMedia.loadBuffer.ptr, destinationIndex, (int) length);
                int num = destinationIndex + (int) length;
                this.m_pSongMedia.loadBuffer.size += length;
                if (this.m_pSongMedia.loadBuffer.size >= cfileInputStream2.GetSize())
                {
                  this.m_pSongMedia.trackSizes[(int) track] = cfileInputStream2.GetSize();
                  flag4 = true;
                }
              }
              flag1 = flag5 & !cfileInputStream2.GetFail();
              cfileInputStream2.Close();
              if (!flag1 || !flag4)
              {
                if (!flag1)
                  this.m_pSongMedia.loadBuffer.ptr = (byte[]) null;
                return flag1;
              }
              goto case CSong.eSongMediaSource.SONG_SOURCE_HEAP;
          }
          if (flag1)
          {
            if (track == Consts.eSongTrack.SONG_NOTE_SHEET)
              flag1 &= this.m_pSongMedia.ParseNoteData(this.m_instrumentRequested, this.m_difficultyRequested, stream, stream.GetSize());
            else if (!flag3)
            {
              CResource resource1 = (CResource) null;
              int resource2 = (int) resourceManager.CreateResource(str, out resource1);
              this.m_pSongMedia.mainTrack = (CMedia) resource1.GetData();
            }
            if (stream != null)
              this.m_pSongMedia.trackSizes[(int) track] = stream.GetSize();
          }
          if (flag1 && stream != null)
            flag1 &= !stream.GetFail();
          stream?.Close();
          if (flag1)
          {
            CSongListMgr.HistoryRemove(this.m_songID, track);
            CSongListMgr.HistoryAdd(this.m_songID, track);
          }
        }
        Consts.eSongTrack eSongTrack = track;
        this.NextTrack(ref track);
        if (eSongTrack >= track)
          this.m_pSongMedia.state = CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADED;
      }
      while (flag1 && this.m_pSongMedia.state != CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADED && !loadOneFile);
      if (source == CSong.eSongMediaSource.SONG_SOURCE_LOCAL_TO_HEAP)
        this.m_pSongMedia.loadBuffer.ptr = (byte[]) null;
      return flag1;
    }

    private bool WriteSongMedia(CInputStream stream)
    {
      bool flag1 = true;
      Consts.eSongNumTracks numTracksSupported = CSongListMgr.GetNumTracksSupported();
      Consts.eSongTrack track1 = (Consts.eSongTrack) stream.ReadUInt8();
      int num1 = (int) stream.ReadUInt8();
      uint num2 = stream.ReadUInt32();
      uint n = stream.ReadUInt32();
      if (n > 0U)
      {
        byte[] numArray1 = (byte[]) null;
        byte[] numArray2 = new byte[(int) n];
        bool flag2 = this.m_pSongMedia.trackSizes[(int) track1] == 0U;
        if (numArray2 != null)
        {
          stream.Read(numArray2, n);
          this.m_pSongMedia.trackSizes[(int) track1] += n;
          Consts.eSongTrack track2 = track1;
          if ((int) this.m_pSongMedia.trackSizes[(int) track1] == (int) num2)
          {
            this.m_songLocalMask |= (uint) CSong.kTableSongMask[(int) track1];
            this.NextTrack(ref track1);
          }
          int trackSiz = (int) this.m_pSongMedia.trackSizes[(int) track1];
          if (flag1)
          {
            uint num3 = this.Gebyte(numTracksSupported, this.m_instrumentRequested);
            if (((int) this.GetSongMediaLocal() & (int) num3) == (int) num3)
              this.m_pSongMedia.state = CSongMedia.eSongMediaState.SONGMEDIA_STATE_DOWNLOADED;
          }
          ICFileMgr instance = ICFileMgr.GetInstance();
          if (!stream.GetFail())
          {
            uint num4 = 0;
            string songFilepath = this.GetSongFilepath(track2, true);
            int mode = !instance.Exists(songFilepath) || instance.Size(songFilepath) <= 0U ? 1 : 2;
            ICFile pFile = instance.Open(songFilepath, mode);
            if (pFile != null)
            {
              pFile.Seek(0, 2);
              num4 += pFile.Write(numArray2, n);
              if (pFile != null && flag2)
                flag1 = flag1 & CSongListMgr.HistoryAdd(this.m_songID, track2) & CSongListMgr.WriteHistoryFile();
            }
            if (pFile == null || (int) num4 != (int) n)
            {
              CSongListMgr.WriteErrorOccurred(true);
              flag1 = false;
            }
            instance.Close(pFile);
          }
          if (track1 != track2 || this.m_pSongMedia.state == CSongMedia.eSongMediaState.SONGMEDIA_STATE_DOWNLOADED)
            instance.Rename(this.GetSongFilepath(track2, true), this.GetSongFilepath(track2, false));
          numArray1 = (byte[]) null;
        }
        else
          flag1 = false;
      }
      return flag1;
    }

    public bool ReadWriteSongMedia(CInputStream stream, CSong.eSongMediaSource source)
    {
      bool flag1 = true;
      if (this.m_pSongMedia == null)
        this.m_pSongMedia = new CSongMedia();
      bool flag2 = flag1 & this.m_pSongMedia != null;
      if (source == CSong.eSongMediaSource.SONG_SOURCE_NETWORK)
      {
        flag2 &= (int) this.m_songID == (int) stream.ReadUInt32();
        ushort num = stream.ReadUInt16();
        if (CSongListMgr.GetNumTracksSupported() == Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE)
        {
          if (num != (ushort) 6)
            flag2 = false;
        }
        else if (CSongListMgr.GetNumTracksSupported() == Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE && num != (ushort) 1)
          flag2 = false;
      }
      if (flag2 && this.m_pSongMedia.state == CSongMedia.eSongMediaState.SONGMEDIA_STATE_INITIAL)
      {
        if (source == CSong.eSongMediaSource.SONG_SOURCE_NETWORK)
          flag2 &= CSongListMgr.PrepSongWrite(this.m_songID, this.m_instrumentRequested);
        this.m_pSongMedia.state = CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADING;
      }
      if (flag2)
        flag2 &= this.ReadSongMedia(source, false);
      if (flag2)
      {
        this.m_pSongMedia.ResetTrackSizes();
        this.m_pSongMedia.state = CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADING;
        CSongListMgr.SetCurSongLoading(this, CSong.eSongMediaSource.SONG_SOURCE_LOCAL_TO_HEAP);
        this.LoadSongMedia(this.m_instrumentRequested, this.m_difficultyRequested);
        if (flag2)
          CSongListMgr.WriteHistoryFile();
      }
      if (flag2 && stream != null)
        flag2 &= !stream.GetFail();
      stream?.Close();
      if (!flag2)
        this.HandleTransferError();
      return flag2;
    }

    private void UnloadSongMedia()
    {
      this.m_pSongMedia = (CSongMedia) null;
      if (CSongListMgr.GetCurSongLoading() == null)
        return;
      CSongListMgr.SetCurSongLoading((CSong) null, CSong.eSongMediaSource.SONG_SOURCE_NONE);
    }

    public void WriteMeta(COutputStream stream)
    {
      uint val1 = 0;
      stream.WriteUInt32(this.m_songID);
      uint num = 0;
      for (int index = 0; index < 4; ++index)
      {
        if (this.m_metaStrings[index] != null)
          ++num;
      }
      uint val2 = num + 2U;
      stream.WriteUInt32(val2);
      stream.WriteUInt32(val1);
      for (uint val3 = 0; val3 < 4U; ++val3)
      {
        if (this.m_metaStrings[(int) val3] != null)
        {
          stream.WriteUInt32(val3);
          stream.WriteUInt32(val1);
          this.WriteJMUtf8(stream, this.m_metaStrings[(int) val3]);
        }
      }
      stream.WriteUInt32(4U);
      stream.WriteUInt32(val1);
      stream.WriteUInt16(this.m_isEncore ? (ushort) 1 : (ushort) 0);
      stream.WriteUInt32(5U);
      stream.WriteUInt32(val1);
      stream.WriteUInt16(this.m_order);
    }

    private void WriteJMUtf8(COutputStream stream, string pIn)
    {
      bool endian = stream.GetEndian();
      stream.SetEndian(true);
      ushort length = (ushort) pIn.Length;
      stream.WriteUInt16(length);
      stream.SetEndian(endian);
      foreach (short val in pIn.ToCharArray())
        stream.WriteInt16(val);
    }

    public enum eSongMediaSource
    {
      SONG_SOURCE_NONE,
      SONG_SOURCE_NETWORK,
      SONG_SOURCE_HEAP,
      SONG_SOURCE_LOCALDIR,
      SONG_SOURCE_RESOURCEFILE,
      SONG_SOURCE_LOCAL_TO_HEAP,
    }

    public enum eSongMediaFileLetter
    {
      SONG_FILE_INVALID = 102, // 0x00000066
      SONG_FILE_NOTES = 103, // 0x00000067
      SONG_FILE_GUITAR = 104, // 0x00000068
      SONG_FILE_MAIN = 104, // 0x00000068
      SONG_FILE_NOGUITAR = 105, // 0x00000069
      SONG_FILE_BASS = 106, // 0x0000006A
      SONG_FILE_NOBASS = 107, // 0x0000006B
      SONG_FILE_DRUMS = 108, // 0x0000006C
      SONG_FILE_NODRUMS = 109, // 0x0000006D
      SONG_FILE_LAST = 110, // 0x0000006E
    }

    public enum eSongMediaMask
    {
      SONG_MASK_NONE = 0,
      SONG_MASK_NOTES = 1,
      SONG_MASK_GUITAR = 2,
      SONG_MASK_MAIN = 2,
      SONG_MASK_NOGUITAR = 4,
      SONG_MASK_BASS = 8,
      SONG_MASK_NOBASS = 16, // 0x00000010
      SONG_MASK_DRUMS = 32, // 0x00000020
      SONG_MASK_NODRUMS = 64, // 0x00000040
      SONG_MASK_VOCALS = 128, // 0x00000080
      SONG_MASK_NOVOCALS = 256, // 0x00000100
    }

    public enum eNoteTrack
    {
      NOTETRACK_INVALID = -1, // 0xFFFFFFFF
      NOTETRACK_TEMPO = 0,
      NOTETRACK_GUITAR_EASY = 1,
      NOTETRACK_GUITAR_MEDIUM = 2,
      NOTETRACK_GUITAR_HARD = 3,
      NOTETRACK_BASS_EASY = 4,
      NOTETRACK_BASS_MEDIUM = 5,
      NOTETRACK_BASS_HARD = 6,
      NOTETRACK_DRUMS_EASY = 7,
      NOTETRACK_DRUMS_MEDIUM = 8,
      NOTETRACK_DRUMS_HARD = 9,
      NUM_NOTESTRACKS = 10, // 0x0000000A
    }

    public enum eMetaItemID
    {
      METAID_SONG_TEXT = 0,
      METAID_BAND_TEXT = 1,
      METAID_YEAR_TEXT = 2,
      METAID_HEADLINE_TEXT = 3,
      METAID_ENCORE_BOOL = 4,
      NUM_METAID_STRINGS = 4,
      METAID_ORDER_NUMER = 5,
      NUM_METAIDS = 6,
    }
  }
}
