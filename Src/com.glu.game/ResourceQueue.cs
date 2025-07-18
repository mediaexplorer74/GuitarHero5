// Decompiled with JetBrains decompiler
// Type: com.glu.game.ResourceQueue
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class ResourceQueue
  {
    public ResourceQueue.CharacterQueue m_characterQueue = new ResourceQueue.CharacterQueue();
    public ResourceQueue.ImagespriteQueue m_imagespriteQueue;
    public SG_Home m_pSGHome;
    public SG_QueueState m_queueState;

    public ResourceQueue() => this.m_imagespriteQueue.imagespritesBitVector = (BitVector) null;

    public void Init(SG_Home pSGHome)
    {
      this.m_pSGHome = pSGHome;
      this.m_queueState = SG_QueueState.SG_QueueState_EMPTY;
      this.m_imagespriteQueue.imagespritesBitVector = new BitVector((uint) this.m_pSGHome.m_imagespriteCount);
      this.m_imagespriteQueue.imagespriteIDNext = this.m_pSGHome.m_imagespriteCount;
      this.m_characterQueue.characterCountOriginal = (ushort) 0;
      this.m_characterQueue.charactersProcessed = (ushort) 0;
    }

    public void Clean()
    {
      this.m_queueState = SG_QueueState.SG_QueueState_EMPTY;
      this.m_imagespriteQueue.imagespritesBitVector = (BitVector) null;
      this.m_characterQueue.archetypeVector.Clear();
      this.m_characterQueue.characterVector.Clear();
      this.m_imagespriteQueue.imagespritesBitVector = new BitVector((uint) this.m_pSGHome.m_imagespriteCount);
      this.m_imagespriteQueue.imagespriteIDNext = this.m_pSGHome.m_imagespriteCount;
      this.m_characterQueue.characterCountOriginal = (ushort) 0;
      this.m_characterQueue.charactersProcessed = (ushort) 0;
    }

    public int GetQueuedCount()
    {
      int queuedCount = 0;
      if (this.m_queueState != SG_QueueState.SG_QueueState_EMPTY)
      {
        if (this.m_queueState == SG_QueueState.SG_QueueState_NONEMPTY)
          queuedCount = this.m_characterQueue.characterVector.Size() + (int) this.m_pSGHome.m_imagespriteCount;
        else if (this.m_queueState == SG_QueueState.SG_QueueState_PROCESSING)
        {
          int num = this.IsImageSpriteQueueBeingProcessed() ? (int) this.m_pSGHome.m_imagespriteCount - (int) this.m_imagespriteQueue.imagespriteIDNext : (int) this.m_pSGHome.m_imagespriteCount;
          queuedCount = this.m_characterQueue.characterVector.Size() + num;
        }
      }
      return queuedCount;
    }

    public bool IsImageSpriteQueueBeingProcessed()
    {
      return (int) this.m_imagespriteQueue.imagespriteIDNext != (int) this.m_pSGHome.m_imagespriteCount;
    }

    public bool IsQueueBeingProcessed()
    {
      return this.m_queueState == SG_QueueState.SG_QueueState_PROCESSING;
    }

    public bool QueueArchetypeCharacter(byte archetypeID, byte characterID)
    {
      CVector archetypeVector = this.m_characterQueue.archetypeVector;
      CVector characterVector = this.m_characterQueue.characterVector;
      for (int idx = 0; idx < archetypeVector.Size(); ++idx)
      {
        int val1 = -1;
        int num1 = (int) archetypeVector.Get(idx, ref val1);
        if ((int) archetypeID == val1)
        {
          int val2 = -1;
          int num2 = (int) characterVector.Get(idx, ref val2);
          if ((int) characterID == val2)
            return false;
        }
      }
      if (this.m_queueState == SG_QueueState.SG_QueueState_EMPTY)
        this.m_queueState = SG_QueueState.SG_QueueState_NONEMPTY;
      int num3 = (int) archetypeVector.Add((int) archetypeID);
      int num4 = (int) characterVector.Add((int) characterID);
      return true;
    }

    public bool LoadQueued(uint timeLimitThisCall, out bool out_success)
    {
      out_success = true;
      if (timeLimitThisCall == 0U)
      {
        out_success = false;
        return false;
      }
      CVector archetypeVector = this.m_characterQueue.archetypeVector;
      CVector characterVector = this.m_characterQueue.characterVector;
      if (this.m_queueState == SG_QueueState.SG_QueueState_EMPTY)
      {
        SG_Home.DDD(" |  SPRITEGLU  | : LoadQueued() invoked even though queue is empty");
        return false;
      }
      if (this.m_queueState == SG_QueueState.SG_QueueState_NONEMPTY)
      {
        SG_Home.DDD("\\_/ SPRITEGLU \\_/: LoadQueued() queue now being processed");
        this.m_queueState = SG_QueueState.SG_QueueState_PROCESSING;
        this.m_characterQueue.characterCountOriginal = (ushort) characterVector.Size();
        this.m_characterQueue.charactersProcessed = (ushort) 0;
        this.m_characterQueue.debug_timeElapsed = 0U;
      }
      if (characterVector.Size() > 0)
      {
        int val1 = -1;
        int val2 = -1;
        int num1 = (int) archetypeVector.Remove(archetypeVector.Size() - 1, ref val1);
        int num2 = (int) characterVector.Remove(characterVector.Size() - 1, ref val2);
        if (!this.m_pSGHome.LoadArchetypeCharacter((byte) val1, (byte) val2))
        {
          out_success = false;
          return false;
        }
        ++this.m_characterQueue.charactersProcessed;
        if (characterVector.Size() > 0)
          return true;
        SG_Home.DDD("{0}", (object) " |  SPRITEGLU  | : LoadQueued() finished loading character binaries");
        SG_Home.DDD("{0} {1}{2} {3}", (object) " |  SPRITEGLU  | : LoadQueued() loaded characters should be", (object) this.m_characterQueue.characterCountOriginal, (object) ".  Actual value", (object) this.m_characterQueue.charactersProcessed);
        SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadQueued() loaded characters time elapsed", (object) this.m_characterQueue.debug_timeElapsed);
        if (this.m_imagespriteQueue.imagespritesBitVector.GetOnCount() != 0U)
          return true;
        this.m_queueState = SG_QueueState.SG_QueueState_EMPTY;
        return false;
      }
      if (this.LoadQueuedSprites(timeLimitThisCall, out out_success))
        return true;
      this.m_queueState = SG_QueueState.SG_QueueState_EMPTY;
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: LoadQueued() done processing queue");
      return false;
    }

    public bool LoadQueuedSprites(uint timeLimitThisCall, out bool out_success)
    {
      out_success = false;
      if ((int) this.m_imagespriteQueue.imagespriteIDNext == (int) this.m_pSGHome.m_imagespriteCount)
      {
        SG_Home.DDD("\\_/ SPRITEGLU \\_/: LoadQueuedSprites() queue begin processing sprites");
        this.m_imagespriteQueue.imagespriteIDNext = (ushort) 0;
        this.m_imagespriteQueue.debug_wereSeen = (ushort) 0;
        this.m_imagespriteQueue.debug_wereLoaded = (ushort) 0;
        this.m_imagespriteQueue.debug_wereAllocated = (ushort) 0;
        this.m_imagespriteQueue.debug_wereRedundant = (ushort) 0;
        this.m_imagespriteQueue.debug_predictSeen = this.m_pSGHome.m_imagespriteCount;
        this.m_imagespriteQueue.debug_predictLoaded = (ushort) this.m_imagespriteQueue.imagespritesBitVector.GetOnCount();
        this.m_imagespriteQueue.debug_timeElapsed = 0U;
        this.m_imagespriteQueue.debug_ticksRequired = (ushort) 0;
      }
      ++this.m_imagespriteQueue.debug_ticksRequired;
      DateTime now = DateTime.Now;
      double num = 0.0;
      for (ushort imagespriteIdNext = this.m_imagespriteQueue.imagespriteIDNext; (int) imagespriteIdNext < (int) this.m_pSGHome.m_imagespriteCount; ++imagespriteIdNext)
      {
        ++this.m_imagespriteQueue.debug_wereSeen;
        if (num >= (double) timeLimitThisCall)
        {
          this.m_imagespriteQueue.debug_timeElapsed += (uint) num;
          return true;
        }
        if (this.m_imagespriteQueue.imagespritesBitVector.IsMember((int) imagespriteIdNext))
        {
          ++this.m_imagespriteQueue.debug_wereLoaded;
          SG_Imagesprite imagesprite = this.m_pSGHome.GetImagesprite(imagespriteIdNext);
          byte transform = imagesprite.transform;
          SG_Image image = this.m_pSGHome.GetTint(imagesprite.tintID).image;
          ICRenderSurface p_ICRenderSurface = (ICRenderSurface) null;
          if (image.LoadTransformRequiresRawImage(transform))
          {
            ++this.m_imagespriteQueue.debug_wereAllocated;
            string surfaceImageIdString = this.m_pSGHome.generateSurfaceImageIDString((int) image.m_imageID, SquareTransform.IsDimensionSwitching(transform));
            CResource resource1;
            int resource2 = (int) CApp.GetResourceManager().CreateResource(surfaceImageIdString, out resource1);
            p_ICRenderSurface = (ICRenderSurface) resource1.GetData();
          }
          if (!image.LoadTransform(transform, p_ICRenderSurface))
            ++this.m_imagespriteQueue.debug_wereRedundant;
          this.m_imagespriteQueue.imagespritesBitVector.SetMember((int) imagespriteIdNext, false);
        }
        num = (DateTime.Now - now).TotalMilliseconds;
      }
      SG_Home.DDD("{0} {1}{2} {3}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() visited should be", (object) this.m_imagespriteQueue.debug_predictSeen, (object) ".  Actual value", (object) this.m_imagespriteQueue.debug_wereSeen);
      SG_Home.DDD("{0} {1}{2} {3}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() loaded should be", (object) this.m_imagespriteQueue.debug_predictLoaded, (object) ".  Actual value", (object) this.m_imagespriteQueue.debug_wereLoaded);
      SG_Home.DDD("{0} {1}{2} {3}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() allocated should be <=", (object) this.m_imagespriteQueue.debug_wereLoaded, (object) ".  Actual value", (object) this.m_imagespriteQueue.debug_wereAllocated);
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() redundant should be 0.  Actual value", (object) this.m_imagespriteQueue.debug_wereRedundant);
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() time elapsed", (object) this.m_imagespriteQueue.debug_timeElapsed);
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadQueuedSprites() ticks required", (object) this.m_imagespriteQueue.debug_ticksRequired);
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: LoadQueuedSprites() done processing sprites");
      return false;
    }

    public class CharacterQueue
    {
      public ushort characterCountOriginal;
      public ushort charactersProcessed;
      public CVector archetypeVector = new CVector();
      public CVector characterVector = new CVector();
      public uint debug_timeElapsed;
    }

    public struct ImagespriteQueue
    {
      public BitVector imagespritesBitVector;
      public ushort imagespriteIDNext;
      public ushort debug_wereSeen;
      public ushort debug_wereLoaded;
      public ushort debug_wereAllocated;
      public ushort debug_wereRedundant;
      public ushort debug_predictSeen;
      public ushort debug_predictLoaded;
      public uint debug_timeElapsed;
      public ushort debug_ticksRequired;
    }
  }
}
