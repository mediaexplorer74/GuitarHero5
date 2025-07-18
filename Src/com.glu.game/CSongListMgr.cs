// Decompiled with JetBrains decompiler
// Type: com.glu.game.CSongListMgr
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;
using System.Collections.Generic;

#nullable disable
namespace com.glu.game
{
  internal class CSongListMgr : CSingleton
  {
    private static string kwcsCollectionFileName = "songlist.dat";
    private static string kwcsHistoryFileName = "history.dat";
    protected static bool m_bDownloadAttemped;
    public static List<CSongListMgr.SongHistoryEntry> m_history = new List<CSongListMgr.SongHistoryEntry>();
    protected static bool m_isNotSubscriber;
    protected uint m_songListID;
    protected static Consts.eSongNumTracks m_songNumTracks;
    protected static Consts.eSongType m_songType;
    protected static CSong m_pCurSongLoading;
    protected static CSong.eSongMediaSource m_curSongSource;
    protected uint m_quickSongID;
    protected static List<CSong> m_songVector = new List<CSong>();
    protected static bool m_writeErrorOccurred;
    protected static uint m_curSelectedSongID;

    public static bool DownloadAttempted(bool tried)
    {
      return !tried ? CSongListMgr.m_bDownloadAttemped : (CSongListMgr.m_bDownloadAttemped = true);
    }

    public static uint GetCareerSongID()
    {
      return !CSongListMgr.m_isNotSubscriber ? Consts.CAREER_SONGID : Consts.NONSUB_CAREER_SONGID;
    }

    public static CSong GetCurSongLoading() => CSongListMgr.m_pCurSongLoading;

    public static CSong.eSongMediaSource GetCurSongLoadingSource() => CSongListMgr.m_curSongSource;

    public static Consts.eSongNumTracks GetNumTracksSupported() => CSongListMgr.m_songNumTracks;

    public static Consts.eSongType GetSongTypeSupported() => CSongListMgr.m_songType;

    public static bool HasWriteErrorOccurred() => CSongListMgr.m_writeErrorOccurred;

    public static void Reset() => CSongListMgr.m_bDownloadAttemped = false;

    public static void SetCurSongLoading(CSong p, CSong.eSongMediaSource source)
    {
      CSongListMgr.m_pCurSongLoading = p;
      CSongListMgr.m_curSongSource = source;
    }

    public static void WriteErrorOccurred(bool error) => CSongListMgr.m_writeErrorOccurred = error;

    public static ushort GetNumSongs() => (ushort) CSongListMgr.m_songVector.Count;

    public static void SetSelected(uint id) => CSongListMgr.m_curSelectedSongID = id;

    public CSongListMgr()
    {
      CSongListMgr.m_pCurSongLoading = (CSong) null;
      this.m_quickSongID = 0U;
      CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_INVALID;
      CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_INVALID;
      CSongListMgr.m_writeErrorOccurred = false;
      CSongListMgr.m_bDownloadAttemped = false;
      CSongListMgr.m_curSongSource = CSong.eSongMediaSource.SONG_SOURCE_NONE;
      CSongListMgr.m_isNotSubscriber = Consts.kDefaultNotSubscriber;
      this.m_songListID = (uint) Consts.kDefaultSongListID;
    }

    public static bool DeleteSongFromCache(uint songID, Consts.eSongTrack track)
    {
      bool flag1 = true;
      Consts.eSongTrack eSongTrack = Consts.eSongTrack.SONG_NOTE_SHEET;
      int num = 3;
      switch (track)
      {
        case Consts.eSongTrack.SONG_NOTE_SHEET:
          track = Consts.eSongTrack.SONG_NOTE_SHEET;
          eSongTrack = Consts.eSongTrack.SONG_NOTE_SHEET;
          break;
        case Consts.eSongTrack.SONG_TRACK_MAIN:
        case Consts.eSongTrack.SONG_TRACK_NON_GUITAR:
          track = Consts.eSongTrack.SONG_TRACK_MAIN;
          eSongTrack = Consts.eSongTrack.SONG_TRACK_NON_GUITAR;
          break;
        case Consts.eSongTrack.SONG_TRACK_BASS:
        case Consts.eSongTrack.SONG_TRACK_NON_BASS:
          track = Consts.eSongTrack.SONG_TRACK_BASS;
          eSongTrack = Consts.eSongTrack.SONG_TRACK_NON_BASS;
          break;
        case Consts.eSongTrack.SONG_TRACK_DRUMS:
        case Consts.eSongTrack.SONG_TRACK_NON_DRUMS:
          track = Consts.eSongTrack.SONG_TRACK_DRUMS;
          eSongTrack = Consts.eSongTrack.SONG_TRACK_NON_DRUMS;
          break;
      }
      bool flag2 = true;
      if (CSongListMgr.m_songNumTracks == Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE)
      {
        for (int index = 0; index < CSongListMgr.m_history.Count; ++index)
        {
          CSongListMgr.SongHistoryEntry songHistoryEntry = CSongListMgr.m_history[index];
          if ((int) songHistoryEntry.song == (int) songID && songHistoryEntry.track != track && songHistoryEntry.track != eSongTrack)
          {
            flag2 = false;
            break;
          }
        }
      }
      if (track == Consts.eSongTrack.SONG_NOTE_SHEET && !flag2)
      {
        CSongListMgr.HistoryRemove(songID, track);
        CSongListMgr.HistoryAdd(songID, track);
        return true;
      }
      ICFileMgr instance = ICFileMgr.GetInstance();
      for (int index = 0; index < num; ++index)
      {
        Consts.eSongTrack track1;
        if (index == 0)
          track1 = track;
        else if (flag2)
        {
          flag2 = false;
          track1 = Consts.eSongTrack.SONG_NOTE_SHEET;
        }
        else if (CSongListMgr.m_songNumTracks == Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE)
          track1 = eSongTrack;
        else
          continue;
        if (instance.Exists(CSong.GetSongFilepath(songID, track1, false)))
          flag1 &= ICFileMgr.GetInstance().Delete(CSong.GetSongFilepath(songID, track1, false));
        if (instance.Exists(CSong.GetSongFilepath(songID, track1, true)))
          flag1 &= ICFileMgr.GetInstance().Delete(CSong.GetSongFilepath(songID, track1, true));
        if (flag1)
          flag1 &= CSongListMgr.HistoryRemove(songID, track1);
      }
      bool flag3 = flag1 & CSongListMgr.WriteHistoryFile();
      CSongListMgr.GetSong(songID)?.DetermineSongLocal();
      return flag3;
    }

    public static void DetermineMediaSupported()
    {
      switch (0)
      {
        case 0:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_MP3;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE;
          break;
        case 1:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_OGG;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE;
          break;
        case 2:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_QCP_SINGLE;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE;
          break;
        case 3:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_QCP_MULTI;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE;
          break;
        case 4:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_MIDI;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE;
          break;
        case 5:
          CSongListMgr.m_songType = Consts.eSongType.SONG_TYPE_ADPCM;
          CSongListMgr.m_songNumTracks = Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE;
          break;
      }
    }

    private static void Empty()
    {
      for (int index = CSongListMgr.m_songVector.Count - 1; index >= 0; --index)
      {
        CSong csong = CSongListMgr.m_songVector[index];
        CSongListMgr.m_songVector.Remove(csong);
      }
    }

    public static CSong GetLastLoadedSong()
    {
      CSong csong = (CSong) null;
      if (CSongListMgr.m_history.Count > 0)
      {
        int index = CSongListMgr.m_history.Count - 1;
        csong = CSongListMgr.GetSong(CSongListMgr.m_history[index].song);
      }
      CSongMedia csongMedia = (CSongMedia) null;
      if (csong != null)
        csongMedia = csong.GetSongMedia();
      return csongMedia != null && csongMedia.state == CSongMedia.eSongMediaState.SONGMEDIA_STATE_LOADED ? csong : (CSong) null;
    }

    public static uint GetMaxSongMediaSize()
    {
      switch (CSongListMgr.m_songType)
      {
        case Consts.eSongType.SONG_TYPE_MP3:
          return 323584;
        case Consts.eSongType.SONG_TYPE_OGG:
          return 952320;
        case Consts.eSongType.SONG_TYPE_QCP_SINGLE:
          return 542720;
        case Consts.eSongType.SONG_TYPE_QCP_MULTI:
          return 1054720;
        case Consts.eSongType.SONG_TYPE_MIDI:
          return 74752;
        case Consts.eSongType.SONG_TYPE_ADPCM:
          return 1162240;
        default:
          return 0;
      }
    }

    public static CSong GetSong(uint songID)
    {
      CSong song = (CSong) null;
      for (int index = 0; index < CSongListMgr.m_songVector.Count; ++index)
      {
        CSong csong = CSongListMgr.m_songVector[index];
        if ((int) csong.GetSongID() == (int) songID)
        {
          song = csong;
          break;
        }
      }
      return song;
    }

    public static CSong GetSongSelected() => CSongListMgr.GetSong(CSongListMgr.m_curSelectedSongID);

    private void HandleTransferError()
    {
      CSongListMgr.m_curSongSource = CSong.eSongMediaSource.SONG_SOURCE_NONE;
      if (CSongListMgr.m_pCurSongLoading == null)
        return;
      CSongListMgr.m_pCurSongLoading.HandleTransferError();
    }

    public static bool HistoryAdd(uint song, Consts.eSongTrack track)
    {
      CSongListMgr.SongHistoryEntry songHistoryEntry = new CSongListMgr.SongHistoryEntry(song, track);
      CSongListMgr.m_history.Add(songHistoryEntry);
      return true;
    }

    public static bool HistoryRemove(uint song, Consts.eSongTrack track)
    {
      bool flag = true;
      for (int index = 0; index < CSongListMgr.m_history.Count; ++index)
      {
        CSongListMgr.SongHistoryEntry songHistoryEntry = CSongListMgr.m_history[index];
        if ((int) songHistoryEntry.song == (int) song && songHistoryEntry.track == track)
        {
          flag = CSongListMgr.m_history.Remove(songHistoryEntry);
          --index;
        }
      }
      return flag;
    }

    public static bool Init()
    {
      bool flag1 = true;
      ICFileMgr instance = ICFileMgr.GetInstance();
      CSongListMgr.DetermineMediaSupported();
      string applicationPath = instance.GetApplicationPath();
      string str1 = applicationPath;
      if (str1.Length > 0)
        str1 += instance.GetPathSeparator();
      string str2 = str1 + CSongListMgr.kwcsCollectionFileName;
      CArrayInputStream stream = new CArrayInputStream();
      CResource resource1 = (CResource) null;
      byte[] dest1;
      if (instance.Exists(str2))
      {
        uint outSize = 0;
        flag1 &= CUtility.FileChecksumRead(str2, out dest1, ref outSize);
        stream.Open(dest1, outSize);
      }
      else
      {
        int resource2 = (int) CApp.GetResourceManager().CreateResource("BIN_SONGLIST", out resource1);
        if (resource1 != null)
        {
          CBinary data = (CBinary) resource1.GetData();
          if (data != null)
            flag1 &= stream.Open(data.GetData(), data.GetSize());
        }
        else
          flag1 = false;
      }
      if (flag1)
      {
        bool flag2 = CSongListMgr.ReadList((CInputStream) stream, false);
        stream.Close();
        flag1 &= flag2;
      }
      if (resource1 != null)
        CApp.GetResourceManager().ReleaseResource("BIN_SONGLIST");
      dest1 = (byte[]) null;
      string str3 = applicationPath;
      if (str3.Length > 0)
        str3 += instance.GetPathSeparator();
      string str4 = str3 + CSongListMgr.kwcsHistoryFileName;
      if (instance.Exists(str4))
      {
        uint outSize = 0;
        byte[] dest2 = (byte[]) null;
        if (CUtility.FileChecksumRead(str4, out dest2, ref outSize))
        {
          CSongListMgr.m_history.Clear();
          CArrayInputStream carrayInputStream = new CArrayInputStream();
          if (carrayInputStream.Open(dest2, outSize))
          {
            while (carrayInputStream.Available() != 0U)
              CSongListMgr.HistoryAdd(carrayInputStream.ReadUInt32(), (Consts.eSongTrack) carrayInputStream.ReadUInt32());
            carrayInputStream.Close();
          }
        }
      }
      return flag1;
    }

    public static bool IsSongUnlocked(uint songID)
    {
      CSong song = CSongListMgr.GetSong(songID);
      if (song == null || !song.IsEncore())
        return true;
      CSongScoreMgr.SongScore songScore = CSongScoreMgr.GetSongScore(songID);
      return songScore != null && songScore.IsUnlocked();
    }

    public static CSongListMgr.ePurgeResult PurgeSongCache()
    {
      bool flag = true;
      double totalMilliseconds = CApplet.GetInstance().m_gameTime_xna.TotalGameTime.TotalMilliseconds;
      while (flag && CSongListMgr.m_history.Count > 0)
      {
        CSongListMgr.SongHistoryEntry songHistoryEntry = CSongListMgr.m_history[0];
        flag &= CSongListMgr.DeleteSongFromCache(songHistoryEntry.song, songHistoryEntry.track);
        totalMilliseconds = CApplet.GetInstance().m_gameTime_xna.TotalGameTime.TotalMilliseconds;
      }
      if (!flag)
        return CSongListMgr.ePurgeResult.PURGE_FAILURE;
      return CSongListMgr.m_history.Count > 0 ? CSongListMgr.ePurgeResult.PURGE_IN_PROGRESS : CSongListMgr.ePurgeResult.PURGE_COMPLETE;
    }

    private static bool ReadList(CInputStream stream, bool isDownload)
    {
      if (stream == null)
        return false;
      CSongListMgr.Empty();
      uint num = 32;
      CActualSongList cactualSongList = new CActualSongList();
      for (ushort index = 0; (uint) index < num; ++index)
      {
        CSong csong = new CSong();
        if (csong == null || !csong.ReadMeta((int) index))
          return false;
        csong.DetermineSongLocal();
        CSongListMgr.m_songVector.Add(csong);
      }
      for (ushort index1 = 1; (uint) index1 < num; ++index1)
      {
        CSong csong = CSongListMgr.m_songVector[(int) index1];
        int index2;
        for (index2 = (int) index1 - 1; index2 >= 0 && CSongListMgr.m_songVector[index2].GetSongID() > csong.GetSongID(); --index2)
          CSongListMgr.m_songVector[index2 + 1] = CSongListMgr.m_songVector[index2];
        CSongListMgr.m_songVector[index2 + 1] = csong;
      }
      uint group = 0;
      for (ushort index = 0; (uint) index < num; ++index)
      {
        CSong csong = CSongListMgr.m_songVector[(int) index];
        if (csong.IsHeadline() && index != (ushort) 0)
          ++group;
        csong.SetHeadlineGroup(group);
      }
      if (isDownload)
        CSongListMgr.WriteSongList();
      return true;
    }

    public static bool ReadSong(CInputStream stream)
    {
      return CSongListMgr.m_pCurSongLoading != null && CSongListMgr.m_pCurSongLoading.ReadWriteSongMedia(stream, CSongListMgr.m_curSongSource);
    }

    private bool ReadSongChunk()
    {
      CSong pCurSongLoading = CSongListMgr.m_pCurSongLoading;
      return false;
    }

    public static bool WriteHistoryFile()
    {
      bool flag = true;
      if (CSongListMgr.m_history.Count > 0)
      {
        uint count = (uint) CSongListMgr.m_history.Count;
        uint size = 4U * count + 4U * count + 4U;
        byte[] numArray = new byte[(int) size];
        flag &= numArray != null;
        if (flag)
        {
          CArrayOutputStream carrayOutputStream = new CArrayOutputStream();
          carrayOutputStream.Open(numArray, size);
          for (uint index = 0; index < count; ++index)
          {
            CSongListMgr.SongHistoryEntry songHistoryEntry = CSongListMgr.m_history[(int) index];
            carrayOutputStream.WriteUInt32(songHistoryEntry.song);
            carrayOutputStream.WriteUInt32((uint) songHistoryEntry.track);
          }
          carrayOutputStream.Close();
          ICFileMgr instance = ICFileMgr.GetInstance();
          string applicationPath = instance.GetApplicationPath();
          if (applicationPath.Length > 0)
            applicationPath += instance.GetPathSeparator();
          string wcsFileName = applicationPath + CSongListMgr.kwcsHistoryFileName;
          if (flag)
            instance.Delete(wcsFileName);
          if (flag)
            flag &= CFileUtil.WriteApplicationDataFile(CSongListMgr.kwcsHistoryFileName, numArray, size);
        }
      }
      return flag;
    }

    private static bool WriteSongList()
    {
      if (CSongListMgr.m_songVector.Count > 0)
      {
        CArrayOutputStream stream = new CArrayOutputStream();
        int count = CSongListMgr.m_songVector.Count;
        uint num = 0;
        for (int index = 0; index < count; ++index)
        {
          CSong csong = CSongListMgr.m_songVector[index];
          num += (uint) csong.GetWriteSize();
        }
        uint size = num + 6U;
        byte[] numArray = new byte[(int) size];
        if (!stream.Open(numArray, size))
          return false;
        stream.WriteUInt16((ushort) count);
        for (int index = 0; index < count; ++index)
          CSongListMgr.m_songVector[index].WriteMeta((COutputStream) stream);
        stream.Close();
        if (CFileUtil.SafeWriteApplicationDataFile(CSongListMgr.kwcsCollectionFileName, numArray, size) && !stream.GetFail())
          return true;
      }
      return false;
    }

    public static bool PrepSongWrite(uint songID, Consts.eInstrument instrument)
    {
      bool flag1 = true;
      CSong song = CSongListMgr.GetSong(songID);
      uint num1 = song.Gebyte(CSongListMgr.m_songNumTracks, instrument);
      bool flag2 = flag1 & !CSongListMgr.HasWriteErrorOccurred();
      if (flag2 && ((int) song.GetSongMediaLocal() & (int) num1) != (int) num1)
      {
        uint num2 = CSongListMgr.GetMaxSongMediaSize();
        int num3 = CSongListMgr.GetNumTracksSupported() != Consts.eSongNumTracks.SONG_NUMTRACKS_MULTIPLE ? Consts.kMaxSongsToCache * 2 : Consts.kMaxSongsToCache * 6;
        uint num4 = song.GetSongMediaLocal() & num1;
        uint num5 = song.GetSongMediaInDir(true) & num1;
        if (num4 != 0U || num5 != 0U)
        {
          ICFileMgr instance = ICFileMgr.GetInstance();
          if (((int) num4 & 1) != 0)
            num2 -= 30720U;
          else
            num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_NOTE_SHEET, true));
          uint num6;
          switch (CSongListMgr.GetSongTypeSupported())
          {
            case Consts.eSongType.SONG_TYPE_OGG:
              num6 = 460800U;
              break;
            case Consts.eSongType.SONG_TYPE_QCP_MULTI:
              num6 = 512000U;
              break;
            case Consts.eSongType.SONG_TYPE_ADPCM:
              num6 = 565760U;
              break;
            default:
              num6 = 0U;
              break;
          }
          if (CSongListMgr.m_songNumTracks == Consts.eSongNumTracks.SONG_NUMTRACKS_SINGLE)
          {
            if (((int) num5 & 2) != 0)
              num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_MAIN, true));
          }
          else
          {
            switch (instrument)
            {
              case Consts.eInstrument.INSTRUMENT_GUITAR:
                if (((int) num4 & 2) != 0 || ((int) num4 & 4) != 0)
                  num2 -= num6;
                if (((int) num5 & 2) != 0)
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_MAIN, true));
                if (((int) num5 & 4) != 0)
                {
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_MAIN, true));
                  break;
                }
                break;
              case Consts.eInstrument.INSTRUMENT_BASS:
                if (((int) num4 & 8) != 0 || ((int) num4 & 16) != 0)
                  num2 -= num6;
                if (((int) num5 & 8) != 0)
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_BASS, true));
                if (((int) num5 & 16) != 0)
                {
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_NON_BASS, true));
                  break;
                }
                break;
              case Consts.eInstrument.INSTRUMENT_DRUMS:
                if (((int) num4 & 32) != 0 || ((int) num4 & 64) != 0)
                  num2 -= num6;
                if (((int) num5 & 32) != 0)
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_DRUMS, true));
                if (((int) num5 & 64) != 0)
                {
                  num2 -= instance.Size(song.GetSongFilepath(Consts.eSongTrack.SONG_TRACK_NON_DRUMS, true));
                  break;
                }
                break;
            }
          }
          if (num2 < 0U)
            num2 = num6;
        }
        uint index1 = 0;
        while (flag2 && (CSongListMgr.m_history.Count >= num3 || ICFileMgr.GetInstance().GetFreeDiskSpace() < (long) num2))
        {
          if (CSongListMgr.m_history.Count == 0 || (long) CSongListMgr.m_history.Count == (long) index1)
          {
            flag2 = false;
            break;
          }
          CSongListMgr.SongHistoryEntry songHistoryEntry = CSongListMgr.m_history[(int) index1];
          if ((int) songHistoryEntry.song == (int) songID)
            ++index1;
          else
            flag2 &= CSongListMgr.DeleteSongFromCache(songHistoryEntry.song, songHistoryEntry.track);
        }
        for (int index2 = 0; index2 < 7; ++index2)
        {
          if (((long) (num4 & num1) & (long) CSong.kTableSongMask[index2]) != 0L)
          {
            Consts.eSongTrack track = Consts.eSongTrack.SONG_TRACK_NON_GUITAR;
            switch (CSong.kTableSongMask[index2])
            {
              case 1:
                track = Consts.eSongTrack.SONG_NOTE_SHEET;
                break;
              case 2:
                track = Consts.eSongTrack.SONG_TRACK_MAIN;
                break;
              case 4:
                track = Consts.eSongTrack.SONG_TRACK_MAIN;
                break;
              case 8:
                track = Consts.eSongTrack.SONG_TRACK_BASS;
                break;
              case 16:
                track = Consts.eSongTrack.SONG_TRACK_NON_BASS;
                break;
              case 32:
                track = Consts.eSongTrack.SONG_TRACK_DRUMS;
                break;
              case 64:
                track = Consts.eSongTrack.SONG_TRACK_NON_DRUMS;
                break;
            }
            flag2 = flag2 & CSongListMgr.HistoryRemove(songID, track) & CSongListMgr.HistoryAdd(songID, track);
          }
          if (!flag2)
            break;
        }
        if (flag2)
          flag2 &= CSongListMgr.WriteHistoryFile();
      }
      return flag2;
    }

    public static CSong GetSong(ushort index) => CSongListMgr.m_songVector[(int) index];

    public static void UnlockSong(uint songID)
    {
      CSong song = CSongListMgr.GetSong(songID);
      if (song == null || !song.IsEncore())
        return;
      CSongScoreMgr.GetSongScore(songID)?.Unlock();
    }

    public static uint CheckUnlockEvent(uint songID)
    {
      int num = -1;
      CSong csong1 = (CSong) null;
      for (int index = 0; index < CSongListMgr.m_songVector.Count; ++index)
      {
        CSong csong2 = CSongListMgr.m_songVector[index];
        if ((int) csong2.GetSongID() == (int) songID)
          num = index;
        if (csong2.IsEncore() && num >= 0)
        {
          csong1 = csong2;
          num = index;
          break;
        }
      }
      if (csong1 == null)
        return 0;
      CSongScoreMgr.SongScore songScore1 = CSongScoreMgr.GetSongScore(csong1.GetSongID());
      if (songScore1 == null)
      {
        if (!CSongScoreMgr.StoreScore(csong1.GetSongID(), Consts.eDifficulty.DIFFICULTY_EASY, Consts.eInstrument.INSTRUMENT_GUITAR, 0U, 0U))
          return 0;
        songScore1 = CSongScoreMgr.GetSongScore(csong1.GetSongID());
      }
      if (songScore1.IsUnlocked())
        return 0;
      for (int index = num - 1; index >= 0; --index)
      {
        CSong csong3 = CSongListMgr.m_songVector[index];
        CSongScoreMgr.SongScore songScore2 = CSongScoreMgr.GetSongScore(csong3.GetSongID());
        if (songScore2 == null || songScore2.GetTotalScore() == 0U)
          return 0;
        if (csong3.IsHeadline() || index == 0)
          return csong1.GetSongID();
      }
      return 0;
    }

    private int GetUnlockedSongCount()
    {
      int unlockedSongCount = 0;
      for (ushort index = 0; (int) index < (int) CSongListMgr.GetNumSongs(); ++index)
      {
        CSong song = CSongListMgr.GetSong(index);
        if (song != null && song.IsEncore() && CSongListMgr.IsSongUnlocked(song.GetSongID()))
          ++unlockedSongCount;
      }
      return unlockedSongCount;
    }

    public class SongHistoryEntry
    {
      public uint song;
      public Consts.eSongTrack track;

      public SongHistoryEntry()
      {
        this.song = 0U;
        this.track = Consts.eSongTrack.SONG_NOTE_SHEET;
      }

      public SongHistoryEntry(uint songID, Consts.eSongTrack trackID)
      {
        this.Setup(songID, trackID);
      }

      public void Setup(uint songID, Consts.eSongTrack trackID)
      {
        this.song = songID;
        this.track = trackID;
      }
    }

    public enum ePurgeResult
    {
      PURGE_FAILURE,
      PURGE_IN_PROGRESS,
      PURGE_COMPLETE,
    }
  }
}
