// Decompiled with JetBrains decompiler
// Type: com.glu.game.Vector`1
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace com.glu.game
{
  public class Vector<T> : List<T>
  {
    public void Allocate(ushort numObjects)
    {
      for (int index = 0; index < (int) numObjects; ++index)
        this.Add(default (T));
    }

    public T Get(int i) => this.ElementAt<T>(i);

    public int Length() => this.Count;

    public void ClearMemory() => this.Clear();

    public void Free() => this.Clear();
  }
}
