// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CMedia
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public class CMedia
  {
    public const uint ClassId = 41628401;
    public SoundEffect m_soundEffect;
    public SoundEffectInstance m_soundInstance;
    public Song m_song;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public CMedia()
    {
    }

    public CMedia(SoundEffect se)
    {
      this.m_soundEffect = se;
      this.m_soundInstance = se.CreateInstance();
    }

    public bool Load(CInputStream inStream) => this.Load(inStream, 0U);

    public bool Load(CInputStream inStream, uint mimeKey)
    {
      bool flag = false;
      uint length = inStream.Available();
      if (length > 0U)
      {
        byte[] numArray = new byte[(int) length];
        inStream.Read(numArray, length);
        string assetName = CMedia.m_utf8Encoding.GetString(numArray, 0, (int) length);
        try
        {
          this.m_soundEffect = CApplet.GetInstance().Content.Load<SoundEffect>(assetName);
          if (this.m_soundEffect != null)
          {
            this.m_soundInstance = this.m_soundEffect.CreateInstance();
            flag = true;
          }
        }
        catch (Exception ex)
        {
          this.m_song = CApplet.GetInstance().Content.Load<Song>(assetName);
        }
      }
      return flag;
    }

    public void unLoad()
    {
      if (this.m_soundEffect != null)
      {
        if (this.m_soundInstance != null)
        {
          this.m_soundInstance.Stop();
          this.m_soundInstance.Dispose();
          this.m_soundInstance = (SoundEffectInstance) null;
        }
        this.m_soundEffect.Dispose();
        this.m_soundEffect = (SoundEffect) null;
      }
      if (!(this.m_song != (Song) null))
        return;
      this.m_song.Dispose();
      this.m_song = (Song) null;
    }

    public void Play(bool looped)
    {
      if (this.m_soundEffect != null)
      {
        if (looped)
          this.m_soundInstance.IsLooped = true;
        this.m_soundInstance.Play();
      }
      else
      {
        MediaPlayer.IsRepeating = looped;
        MediaPlayer.Play(this.m_song);
      }
    }

    public void Stop()
    {
      if (this.m_soundInstance != null)
        this.m_soundInstance.Stop();
      else
        MediaPlayer.Stop();
    }

    public void Pause()
    {
      if (this.m_soundInstance != null)
        this.m_soundInstance.Pause();
      else
        MediaPlayer.Pause();
    }

    public void Resume()
    {
      if (this.m_soundInstance != null)
        this.m_soundInstance.Resume();
      else
        MediaPlayer.Resume();
    }

    public bool IsPlaying()
    {
      return this.m_soundInstance != null ? this.m_soundInstance.State == SoundState.Playing : MediaPlayer.State == MediaState.Playing;
    }

    public bool IsPaused()
    {
      return this.m_soundInstance != null ? this.m_soundInstance.State == SoundState.Paused : MediaPlayer.State == MediaState.Paused;
    }

    public bool IsStopped()
    {
      return this.m_soundInstance != null ? this.m_soundInstance.State == SoundState.Stopped : MediaPlayer.State == MediaState.Stopped;
    }
  }
}
