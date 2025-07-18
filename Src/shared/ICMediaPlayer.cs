// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICMediaPlayer
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace com.glu.shared
{
  public class ICMediaPlayer : CSingleton
  {
    protected new static CSingleton m_instance;
    protected CEventListener m_listener;
    protected bool m_soundEnabled;
    protected bool m_vibrationEnabled;
    protected byte m_volume;
    protected uint m_eventCounter;
    protected List<CMedia> m_soundList = new List<CMedia>();
    protected bool m_suspended;

    public static ICMediaPlayer GetInstance()
    {
      if (ICMediaPlayer.m_instance == null)
        ICMediaPlayer.m_instance = (CSingleton) new ICMediaPlayer();
      return ICMediaPlayer.m_instance as ICMediaPlayer;
    }

    public void SetSoundEnabled(bool enabled) => this.m_soundEnabled = enabled;

    public void SetVibrationEnabled(bool enabled) => this.m_vibrationEnabled = enabled;

    public void SetVolume(byte volume) => SoundEffect.MasterVolume = (float) volume / 10f;

    public bool GetSoundEnabled() => this.m_soundEnabled;

    public bool GetVibrationEnabled() => this.m_vibrationEnabled;

    public byte GetVolume() => (byte) ((double) SoundEffect.MasterVolume * 10.0);

    public bool CanSetProperty(int globalProperty) => false;

    public bool CanGetProperty(int globalProperty) => false;

    public bool SetProperty(int globalProperty, int value) => false;

    public bool SetPropertyF(int globalProperty, int value) => false;

    public bool SetPropertyFV(int globalProperty, int[] pValues, int count) => false;

    public bool GetProperty(int globalProperty, out int value)
    {
      value = -1;
      return false;
    }

    public bool GetPropertyF(int globalProperty, out int value)
    {
      value = -1;
      return false;
    }

    public bool GetPropertyFV(int globalProperty, int[] pValues, int count) => false;

    public bool IsPlaying() => this.IsPlaying(0U);

    public bool IsPlaying(uint id)
    {
      bool flag = false;
      if (id == 0U)
      {
        foreach (CMedia sound in this.m_soundList)
        {
          if (sound.IsPlaying())
          {
            flag = true;
            break;
          }
        }
      }
      else if (this.m_soundList.ElementAt<CMedia>((int) id - 1).IsPlaying())
        flag = true;
      return flag;
    }

    public bool IsPaused() => this.IsPaused(0U);

    public bool IsPaused(uint id)
    {
      bool flag = false;
      if (id == 0U)
      {
        foreach (CMedia sound in this.m_soundList)
        {
          if (sound.IsPaused())
          {
            flag = true;
            break;
          }
        }
      }
      else if (this.m_soundList.ElementAt<CMedia>((int) id - 1).IsPaused())
        flag = true;
      return flag;
    }

    public bool IsVibrating(uint id) => false;

    public bool IsVibrationPaused(uint id) => false;

    public uint Play(CMedia pMediaData) => this.Play(pMediaData, (byte) 0, byte.MaxValue);

    public uint Play(CMedia pMediaData, byte flags) => this.Play(pMediaData, flags, byte.MaxValue);

    public uint Play(CMedia pMediaData, byte flags, byte priority)
    {
      if (pMediaData == null || !this.GetSoundEnabled())
        return uint.MaxValue;
      pMediaData.Play(((int) flags & 1) != 0);
      this.m_soundList.Add(pMediaData);
      return (uint) (this.m_soundList.IndexOf(pMediaData) + 1);
    }

    public void Stop() => this.Stop(0U);

    public void Stop(uint id)
    {
      if (id == 0U)
      {
        foreach (CMedia sound in this.m_soundList)
          sound.Stop();
      }
      else
      {
        if ((long) id >= (long) this.m_soundList.Count)
          return;
        this.m_soundList.ElementAt<CMedia>((int) id - 1).Stop();
      }
    }

    public void Pause() => this.Pause(0U);

    public void Pause(uint id)
    {
      if (id == 0U)
      {
        foreach (CMedia sound in this.m_soundList)
          sound.Pause();
      }
      else
        this.m_soundList.ElementAt<CMedia>((int) id - 1).Pause();
    }

    public void Resume() => this.Resume(0U);

    public void Resume(uint id)
    {
      if (id == 0U)
      {
        foreach (CMedia sound in this.m_soundList)
          sound.Resume();
      }
      else
        this.m_soundList.ElementAt<CMedia>((int) id - 1).Resume();
    }

    public bool SetProperty(uint id, int soundProperty, int value) => false;

    public bool SetPropertyF(uint id, int soundProperty, int value) => false;

    public bool SetPropertyFV(uint id, int soundProperty, int[] pValues, int count) => false;

    public bool GetProperty(uint id, int soundProperty, out int value)
    {
      value = -1;
      return false;
    }

    public bool GetPropertyF(uint id, int soundProperty, out int value)
    {
      value = -1;
      return false;
    }

    public bool GetPropertyFV(uint id, int soundProperty, int[] pValues, int count) => false;

    public uint Vibrate(uint duration) => this.Vibrate(duration, byte.MaxValue);

    public uint Vibrate(uint duration, byte priority) => 0;

    public void StopVibrate() => this.StopVibrate(0U);

    public void StopVibrate(uint id)
    {
    }

    public void PauseVibrate() => this.PauseVibrate(0U);

    public void PauseVibrate(uint id)
    {
    }

    public void ResumeVibrate() => this.ResumeVibrate(0U);

    public void ResumeVibrate(uint id)
    {
    }

    public void HandleSuspend()
    {
    }

    public void HandleResume()
    {
    }

    public void HandleUpdate(int timeElapsedMS)
    {
    }
  }
}
