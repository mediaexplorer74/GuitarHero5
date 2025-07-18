// Decompiled with JetBrains decompiler
// Type: com.glu.game.LiveAchievements
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System;
using System.IO;

namespace com.glu.game
{
    internal class Achievement
    {
        internal int GamerScore;
        internal bool IsEarned;
        internal string Key;
        internal string Name;
        internal string Description;
        internal string HowToEarn;

        internal Stream GetPicture()
        {
            throw new NotImplementedException();
        }
    }
}