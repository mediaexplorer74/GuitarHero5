// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Archetype
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using System;

#nullable disable
namespace com.glu.game
{
  public class SG_Archetype
  {
    protected ushort layerCount;
    protected SG_Layer[] layers;
    protected ushort frameCount;
    protected SG_Frame[] frames;
    protected ushort animationCount;
    private SG_Animation[] animations;
    protected byte characterCount;
    private SG_Character[] characters;

    public SG_Archetype()
    {
      this.layerCount = (ushort) 0;
      this.layers = (SG_Layer[]) null;
      this.frameCount = (ushort) 0;
      this.frames = (SG_Frame[]) null;
      this.animationCount = (ushort) 0;
      this.animations = (SG_Animation[]) null;
      this.characterCount = (byte) 0;
      this.characters = (SG_Character[]) null;
    }

    public bool LoadLayers(DataInputStream dis)
    {
      bool flag = false;
      this.layerCount = dis.ReadUInt16();
      if (this.layerCount > (ushort) 0)
      {
        this.layers = new SG_Layer[(int) this.layerCount];
        if (this.layers != null)
        {
          for (int index = 0; index < (int) this.layerCount; ++index)
          {
            SG_Layer sgLayer = new SG_Layer();
            sgLayer.Load(dis);
            this.layers[index] = sgLayer;
          }
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public bool LoadFrames(DataInputStream dis)
    {
      bool flag = false;
      this.frameCount = dis.ReadUInt16();
      if (this.frameCount > (ushort) 0)
      {
        this.frames = new SG_Frame[(int) this.frameCount];
        if (this.frames != null)
        {
          for (int index = 0; index < (int) this.frameCount; ++index)
          {
            SG_Frame sgFrame = new SG_Frame();
            sgFrame.Load(dis);
            this.frames[index] = sgFrame;
          }
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public bool LoadAnimations(DataInputStream dis)
    {
      bool flag = false;
      this.animationCount = dis.ReadUInt16();
      if (this.animationCount > (ushort) 0)
      {
        this.animations = new SG_Animation[(int) this.animationCount];
        if (this.animations != null)
        {
          for (uint index = 0; index < (uint) this.animationCount; ++index)
          {
            SG_Animation sgAnimation = new SG_Animation();
            sgAnimation.Load(dis);
            this.animations[(int) index] = sgAnimation;
          }
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public bool LoadCharacters(DataInputStream dis, ushort imagespriteCount)
    {
      bool flag = false;
      this.characterCount = dis.ReadUInt8();
      if (this.characterCount > (byte) 0)
      {
        this.characters = new SG_Character[(int) this.characterCount];
        if (this.characters != null)
        {
          for (uint index = 0; index < (uint) this.characterCount; ++index)
          {
            SG_Character sgCharacter = new SG_Character();
            sgCharacter.Load(dis, imagespriteCount);
            this.characters[(int) index] = sgCharacter;
          }
          flag = !dis.GetFail();
        }
      }
      else
        flag = true;
      return flag;
    }

    public ushort GetLayerCount() => this.layerCount;

    public SG_Layer GetLayer(uint idx) => this.layers[(int) idx];

    public ushort GetFrameCount() => this.frameCount;

    public SG_Frame GetFrame(int idx) => this.frames[idx];

    public ushort GetAnimationCount() => this.animationCount;

    public SG_Animation GetAnimation(int idx) => this.animations[idx];

    public ushort GetCharacterCount() => (ushort) this.characterCount;

    public SG_Character GetCharacter(int idx) => this.characters[idx];
  }
}
