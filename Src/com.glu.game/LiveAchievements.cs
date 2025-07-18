// Decompiled with JetBrains decompiler
// Type: com.glu.game.LiveAchievements
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace com.glu.game
{
  internal class LiveAchievements
  {
    private object m_liveAchievementsLockObject = new object();
    private AchievementCollection m_liveAchievements;
    private int m_liveEarnedGamerScore;
    private int m_liveMaxGamerScore;
    private Texture2D unearnedIcon;
    private static LiveAchievements m_instance;
    private string[] m_achivementKeys = new string[20]
    {
      "COMPLETE_SONG_GUITAR",
      "TWO_HUNDRED_NOTES",
      "COMPLETE_SAN_FRANCISCO",
      "COMPLETE_NEW_YORK",
      "COMPLETE_CAIRO",
      "PERFECT_GUITAR",
      "PERFECT_BASS",
      "PERFECT_DRUMS",
      "FIVE_STARS_ONE_SONG",
      "FIVE_STARS_FOUR_SONGS",
      "FIVE_STARS_GUITAR_EXPERT",
      "FIVE_STARS_GUITAR_EXP_4_SONGS",
      "FIVE_STARS_BASS_EXPERT",
      "FIVE_STARS_BASS_EXP_4_SONGS",
      "FIVE_STARS_DRUMS_EXPERT",
      "FIVE_STARS_DRUMS_EXP_4_SONGS",
      "PERFECT_ON_EXPERT",
      "COMPLETE_TWO_SF",
      "COMPLETE_TWO_NEW_YORK",
      "COMPLETE_TWO_CAIRO"
    };
    private LiveAchievements.LiveAchievementState m_liveState = LiveAchievements.LiveAchievementState.liveWaitingForSignIn;
    public Texture2D[] m_icons;

    public static LiveAchievements GetInstance()
    {
      if (LiveAchievements.m_instance == null)
        LiveAchievements.m_instance = new LiveAchievements();
      return LiveAchievements.m_instance;
    }

    public void init()
    {
      //SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
      //SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
      Debug.Write(0 + 1);
    }

    protected void GamerSignedInCallback(object sender, SignedInEventArgs args)
    {
      SignedInGamer gamer = args.Gamer;
      if (gamer == null || this.m_liveState != LiveAchievements.LiveAchievementState.liveWaitingForSignIn)
        return;
      this.m_liveState = LiveAchievements.LiveAchievementState.liveWaitingForAchivements;
      gamer.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), (object) gamer);
    }

    protected void GetAchievementsCallback(IAsyncResult result)
    {
      if (!(result.AsyncState is SignedInGamer asyncState))
        return;
      lock (this.m_liveAchievementsLockObject)
      {
        this.m_liveMaxGamerScore = 0;
        this.m_liveEarnedGamerScore = 0;
        this.m_liveAchievements = asyncState.EndGetAchievements(result);
        this.m_icons = new Texture2D[20];
        int num = 0;
        for (int index = 0; index < this.m_liveAchievements.Count; ++index)
        {
          Achievement liveAchievement = default;//this.m_liveAchievements[index];
          this.m_liveMaxGamerScore += liveAchievement.GamerScore;
          if (liveAchievement.IsEarned)
            this.m_liveEarnedGamerScore += liveAchievement.GamerScore;
          ++num;
        }
        this.m_liveState = LiveAchievements.LiveAchievementState.liveReady;
      }
    }

    public void AwardAchievement(eAchievementID achievementID)
    {
      string achivementKey = this.m_achivementKeys[(int) achievementID];
      SignedInGamer signedInGamer = default;//Gamer.SignedInGamers[PlayerIndex.One];
      if (signedInGamer == null)
        return;
      lock (this.m_liveAchievementsLockObject)
      {
        if (this.m_liveAchievements == null)
          return;
        for (int index = 0; index < this.m_liveAchievements.Count; ++index)
        {
                    Achievement liveAchievement = default;//this.m_liveAchievements[index];
          if (liveAchievement.Key == achivementKey)
          {
            if (liveAchievement.IsEarned)
              break;
            signedInGamer.BeginAwardAchievement(achivementKey, 
                new AsyncCallback(this.AwardAchievementCallback), (object) signedInGamer);
            break;
          }
        }
      }
    }

    protected void AwardAchievementCallback(IAsyncResult result)
    {
      if (!(result.AsyncState is SignedInGamer asyncState))
        return;
      asyncState.EndAwardAchievement(result);
      asyncState.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), (object) asyncState);
    }

    public void getText(out string[] titles, out string[] descriptions)
    {
      titles = new string[20];
      descriptions = new string[20];
      this.m_icons = new Texture2D[20];
      if (this.m_liveAchievements == null)
      {
        for (int index = 0; index < 20; ++index)
        {
          titles[index] = "Debug Achievement: " + (object) index;
          descriptions[index] = "Debug Achievement Description: " + (object) index;
        }
      }
      else
      {
        for (int index = 0; index < this.m_liveAchievements.Count; ++index)
        {
          Achievement liveAchievement = default;//this.m_liveAchievements[index];
          titles[index] = liveAchievement.Name;
          descriptions[index] = liveAchievement.IsEarned ? liveAchievement.Description 
                        : liveAchievement.HowToEarn;
          if (liveAchievement.IsEarned)
          {
            using (Stream picture = liveAchievement.GetPicture())
              this.m_icons[index] = Texture2D.FromStream(((CGraphics) ICGraphics.GetInstance()).GetGraphicsDeviceXNA(), picture);
          }
          else
            this.m_icons[index] = (Texture2D) null;
        }
      }
    }

    private enum LiveAchievementState
    {
      Error,
      liveWaitingForSignIn,
      liveWaitingForAchivements,
      liveReady,
    }
  }
}
