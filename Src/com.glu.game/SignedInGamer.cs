// Decompiled with JetBrains decompiler
// Type: com.glu.game.LiveAchievements
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System;

namespace com.glu.game
{
    internal class SignedInGamer
    {
        internal bool IsSignedInToLive;

        internal void BeginAwardAchievement(string achivementKey, AsyncCallback asyncCallback, object signedInGamer)
        {
            throw new NotImplementedException();
        }

        internal void BeginGetAchievements(AsyncCallback asyncCallback, object gamer)
        {
            throw new NotImplementedException();
        }

        internal void EndAwardAchievement(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        internal AchievementCollection EndGetAchievements(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}