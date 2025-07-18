// Decompiled with JetBrains decompiler
// Type: com.glu.game.SG_Home
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class SG_Home : CSingleton
  {
    public const uint ClassId = 657189301;
    public byte m_pVersion;
    public byte m_paletteCount;
    public SG_Palette[] m_pPalettes;
    public ushort m_imageCount;
    public ushort m_tintCount;
    public SG_Tint[] m_pTints;
    public ushort m_imagespriteCount;
    public SG_Imagesprite[] m_pSrcImagesprites;
    public ushort m_rectspriteCount;
    public SG_Rectsprite[] m_pRectsprites;
    public ushort m_spriteCount;
    public byte m_spritemapCount;
    public SG_Spritemap[] m_pSpritemaps;
    public byte m_archetypeCount;
    public SG_Archetype[] m_ppArchetypes;
    public ResourceQueue m_resourceQueue = new ResourceQueue();
    public string m_resgenIDBinaryGlobal;
    public string m_resgenIDBinaryArchetype0;
    public string m_resgenIDImage0;
    public Graphics graphics = new Graphics();
    public CollisionInfo collisionInfo;
    public Point2DPair boundsPointPair;
    public SpriteDrawInfo[] spriteInfoArraySize1 = new SpriteDrawInfo[1];
    public SpriteDrawInfo[] spriteInfoArraySize2 = new SpriteDrawInfo[2];
    public static readonly int RUNTIME_VERSION_MAJOR = 3;
    public static readonly int RUNTIME_VERSION_MINOR = 2;
    public static readonly int VERSION_BYTE_COUNT = 2;
    protected new static CSingleton m_instance = (CSingleton) null;

    public int GetOriginX() => this.graphics.GetOriginX();

    public int GetOriginY() => this.graphics.GetOriginY();

    public static SG_Home GetInstance()
    {
      if (SG_Home.m_instance == null)
        SG_Home.m_instance = (CSingleton) new SG_Home();
      return SG_Home.m_instance as SG_Home;
    }

    public SG_Palette GetPalette(byte paletteID) => this.m_pPalettes[(int) paletteID];

    public SG_Tint GetTint(ushort tintID) => this.m_pTints[(int) tintID];

    public SG_Imagesprite GetImagesprite(ushort imagespriteID)
    {
      return this.m_pSrcImagesprites[(int) imagespriteID];
    }

    public SG_Rectsprite GetRectsprite(ushort rectspriteID)
    {
      return this.m_pRectsprites[(int) rectspriteID];
    }

    public SG_Spritemap GetSpritemap(byte spritemapID) => this.m_pSpritemaps[(int) spritemapID];

    public SG_Archetype GetArchetype(byte archetypeID) => this.m_ppArchetypes[(int) archetypeID];

    public static void DDD(string message)
    {
    }

    public static void DDD(string message, params object[] objs)
    {
    }

    public SG_Home()
      : base(657189301U)
    {
      this.m_pVersion = (byte) 0;
      this.m_paletteCount = (byte) 0;
      this.m_pPalettes = (SG_Palette[]) null;
      this.m_imageCount = (ushort) 0;
      this.m_tintCount = (ushort) 0;
      this.m_pTints = (SG_Tint[]) null;
      this.m_imagespriteCount = (ushort) 0;
      this.m_pSrcImagesprites = (SG_Imagesprite[]) null;
      this.m_rectspriteCount = (ushort) 0;
      this.m_pRectsprites = (SG_Rectsprite[]) null;
      this.m_spriteCount = (ushort) 0;
      this.m_spritemapCount = (byte) 0;
      this.m_pSpritemaps = (SG_Spritemap[]) null;
      this.m_archetypeCount = (byte) 0;
      this.m_ppArchetypes = (SG_Archetype[]) null;
    }

    public string generateArcheTypeIDString(string start, int offset)
    {
      return start.Substring(0, start.LastIndexOf('_') + 1) + offset.ToString("D3");
    }

    public string generateSurfaceImageIDString(int offset, bool isDimensionSwitchinTransform)
    {
      return (isDimensionSwitchinTransform ? "SPRITEGLU__TRANSPOSED_SURFACE_" : "SPRITEGLU__SURFACE_") + offset.ToString("D4");
    }

    public bool IsInitialized() => this.m_ppArchetypes != null;

    public void Init()
    {
      if (this.IsInitialized())
        return;
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: Init() begin");
      SG_Home.DDD(" |  SPRITEGLU  | : Init() acquiring anchor resourceIDs");
      this.m_resgenIDBinaryGlobal = "SPRITEGLU__BINARY_GLOBAL";
      this.m_resgenIDBinaryArchetype0 = "SPRITEGLU__BINARY_ARCHETYPE_000";
      this.m_resgenIDImage0 = "SPRITEGLU__IMAGE_PACK_00_IMAGE_0000";
      SG_Home.DDD(" |  SPRITEGLU  | : Init() acquiring data input stream");
      DataInputStream dis = new DataInputStream(this.m_resgenIDBinaryGlobal);
      SG_Home.DDD(" |  SPRITEGLU  | : Init() reading palettes");
      this.m_paletteCount = dis.ReadUInt8();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() paletteCount", (object) this.m_paletteCount);
      if (this.m_paletteCount > (byte) 0)
      {
        this.m_pPalettes = new SG_Palette[(int) this.m_paletteCount];
        for (int index = 0; index < (int) this.m_paletteCount; ++index)
        {
          SG_Palette sgPalette = new SG_Palette();
          sgPalette.Load(dis);
          this.m_pPalettes[index] = sgPalette;
        }
      }
      this.m_imageCount = dis.ReadUInt16();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() imageCount", (object) this.m_imageCount);
      SG_Home.DDD(" |  SPRITEGLU  | : Init() reading tints");
      this.m_tintCount = dis.ReadUInt16();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() tintCount", (object) this.m_tintCount);
      if (this.m_tintCount > (ushort) 0)
      {
        this.m_pTints = new SG_Tint[(int) this.m_tintCount];
        for (int index = 0; index < (int) this.m_tintCount; ++index)
        {
          SG_Tint sgTint = new SG_Tint();
          sgTint.Load(dis);
          this.m_pTints[index] = sgTint;
        }
      }
      SG_Home.DDD(" |  SPRITEGLU  | : Init() reading imagesprites");
      this.m_imagespriteCount = dis.ReadUInt16();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() imagespriteCount", (object) this.m_imagespriteCount);
      if (this.m_imagespriteCount > (ushort) 0)
      {
        this.m_pSrcImagesprites = new SG_Imagesprite[(int) this.m_imagespriteCount];
        for (ushort index = 0; (int) index < (int) this.m_imagespriteCount; ++index)
        {
          SG_Imagesprite sgImagesprite = new SG_Imagesprite();
          sgImagesprite.Load(dis);
          this.m_pSrcImagesprites[(int) index] = sgImagesprite;
        }
      }
      SG_Home.DDD(" |  SPRITEGLU  | : Init() reading rectsprites");
      this.m_rectspriteCount = dis.ReadUInt16();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() rectspriteCount", (object) this.m_rectspriteCount);
      if (this.m_rectspriteCount > (ushort) 0)
      {
        this.m_pRectsprites = new SG_Rectsprite[(int) this.m_rectspriteCount];
        for (ushort index = 0; (int) index < (int) this.m_rectspriteCount; ++index)
        {
          SG_Rectsprite sgRectsprite = new SG_Rectsprite();
          sgRectsprite.Load(dis);
          this.m_pRectsprites[(int) index] = sgRectsprite;
        }
      }
      this.m_spriteCount = (ushort) ((uint) this.m_imagespriteCount + (uint) this.m_rectspriteCount);
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() spriteCount", (object) this.m_spriteCount);
      SG_Home.DDD(" |  SPRITEGLU  | : Init() reading spritemaps");
      this.m_spritemapCount = dis.ReadUInt8();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() spritemapCount", (object) this.m_spritemapCount);
      if (this.m_spritemapCount > (byte) 0)
      {
        this.m_pSpritemaps = new SG_Spritemap[(int) this.m_spritemapCount];
        for (ushort index = 0; (int) index < (int) this.m_spritemapCount; ++index)
        {
          SG_Spritemap sgSpritemap = new SG_Spritemap();
          sgSpritemap.Load(dis);
          this.m_pSpritemaps[(int) index] = sgSpritemap;
        }
      }
      SG_Home.DDD(" |  SPRITEGLU  | : Init() loading archetype shell");
      this.m_archetypeCount = dis.ReadUInt8();
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : Init() archetypeCount", (object) this.m_archetypeCount);
      this.m_ppArchetypes = new SG_Archetype[(int) this.m_archetypeCount];
      this.m_resourceQueue.Init(this);
      SG_Home.DDD(" |  SPRITEGLU  | : Init() closing data input stream");
      dis.Close();
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: Init() return");
    }

    public void Destroy()
    {
      if (!this.IsInitialized())
        return;
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: Destroy() begin");
      this.DumpAllImages();
      this.DumpAllArchetypes();
      this.m_ppArchetypes = (SG_Archetype[]) null;
      for (uint index = 0; index < (uint) this.m_spritemapCount; ++index)
        this.m_pSpritemaps[(int) index] = (SG_Spritemap) null;
      this.m_pSpritemaps = (SG_Spritemap[]) null;
      for (uint index = 0; index < (uint) this.m_paletteCount; ++index)
        this.m_pPalettes[(int) index] = (SG_Palette) null;
      this.m_pPalettes = (SG_Palette[]) null;
      for (uint index = 0; index < (uint) this.m_tintCount; ++index)
        this.m_pTints[(int) index] = (SG_Tint) null;
      this.m_pTints = (SG_Tint[]) null;
      for (uint index = 0; index < (uint) this.m_imagespriteCount; ++index)
        this.m_pSrcImagesprites[(int) index] = (SG_Imagesprite) null;
      this.m_pSrcImagesprites = (SG_Imagesprite[]) null;
      for (uint index = 0; index < (uint) this.m_rectspriteCount; ++index)
        this.m_pRectsprites[(int) index] = (SG_Rectsprite) null;
      this.m_pRectsprites = (SG_Rectsprite[]) null;
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: Destroy() return");
    }

    public static void ASSERT_CHECK_RANGE_ARCHETYPEID(int archetypeID)
    {
    }

    public static void ASSERT_CHECK_RANGE_CHARACTERID(int characterID)
    {
    }

    public bool IsLoadedArchetypeCharacter(int archetypeID, int characterID)
    {
      SG_Home.ASSERT_CHECK_RANGE_ARCHETYPEID(archetypeID);
      SG_Home.ASSERT_CHECK_RANGE_CHARACTERID(characterID);
      return this.IsLoadedArchetype((byte) archetypeID) && this.GetArchetype((byte) archetypeID).GetCharacter(characterID).loaded;
    }

    public int GetQueuedCount() => this.m_resourceQueue.GetQueuedCount();

    public SG_QueueState GetQueueState() => this.m_resourceQueue.m_queueState;

    public bool IsQueueBeingProcessed() => this.m_resourceQueue.IsQueueBeingProcessed();

    public bool LoadQueued(uint timeLimitThisCall, out bool out_success)
    {
      return this.m_resourceQueue.LoadQueued(timeLimitThisCall, out out_success);
    }

    public void QueueArchetypeCharacter(int archetypeID, int characterID)
    {
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: QueueArchetypeCharacter() begin");
      SG_Home.ASSERT_CHECK_RANGE_ARCHETYPEID(archetypeID);
      SG_Home.ASSERT_CHECK_RANGE_CHARACTERID(characterID);
      if (this.IsLoadedArchetypeCharacter(archetypeID, characterID))
        SG_Home.DDD("{0} {1},%d", (object) " |  SPRITEGLU  | : QueueArchetypeCharacter() already loaded is archetype,character", (object) archetypeID, (object) characterID);
      else if (this.m_resourceQueue.QueueArchetypeCharacter((byte) archetypeID, (byte) characterID))
        SG_Home.DDD("{0} {1},%d", (object) " |  SPRITEGLU  | : QueueArchetypeCharacter() queueing now completed for archetype,character", (object) archetypeID, (object) characterID);
      else
        SG_Home.DDD("{0} {1},%d", (object) " |  SPRITEGLU  | : QueueArchetypeCharacter() no need - already queued is archetype,character", (object) archetypeID, (object) characterID);
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: QueueArchetypeCharacter() return");
    }

    public void DumpArchetypeCharacter(int archetypeID, int characterID)
    {
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: DumpArchetypeCharacter() begin");
      SG_Home.ASSERT_CHECK_RANGE_ARCHETYPEID(archetypeID);
      SG_Home.ASSERT_CHECK_RANGE_CHARACTERID(characterID);
      SG_Home.DDD("{0} {1},%d", (object) " |  SPRITEGLU  | : DumpArchetypeCharacter() dumping (if necessary) archetype,character", (object) archetypeID, (object) characterID);
      if (this.IsLoadedArchetype((byte) archetypeID))
      {
        SG_Archetype archetype = this.GetArchetype((byte) archetypeID);
        if (this.DumpCharacter((byte) archetypeID, (byte) characterID))
        {
          bool flag = false;
          for (int idx = 0; idx < (int) archetype.GetCharacterCount(); ++idx)
          {
            if (archetype.GetCharacter(idx).loaded)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            this.DumpArchetype((byte) archetypeID);
          else
            SG_Home.DDD("{0} {1} (%s)", (object) " |  SPRITEGLU  | : DumpArchetypeCharacter() leaving intact enclosing archetype", (object) archetypeID, (object) "tempDebugName");
        }
      }
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: DumpArchetypeCharacter() return");
    }

    public void DumpAllArchetypeCharacters()
    {
      this.m_resourceQueue.Clean();
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: DumpAllArchetypeCharacters() begin");
      this.DumpAllImages();
      this.DumpAllArchetypes();
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: DumpAllArchetypeCharacters() return");
    }

    public int GetTag(int spriteID)
    {
      return this.IsImageSprite(spriteID) ? (int) this.GetImagesprite((ushort) spriteID).tag : (int) this.GetRectsprite((ushort) ((uint) spriteID - (uint) this.m_imagespriteCount)).tag;
    }

    public void GetSize(int spriteID, out uint width, out uint height)
    {
      if (this.IsImageSprite(spriteID))
      {
        SG_Imagesprite imagesprite = this.GetImagesprite((ushort) spriteID);
        this.GetTint(imagesprite.tintID).image.GetSize(imagesprite.transform, out width, out height);
      }
      else
      {
        SG_Rectsprite rectsprite = this.GetRectsprite((ushort) ((uint) spriteID - (uint) this.m_imagespriteCount));
        width = (uint) rectsprite.width;
        height = (uint) rectsprite.height;
      }
    }

    public bool IsImageSprite(int spriteID) => spriteID < (int) this.m_imagespriteCount;

    public bool IsRectSprite(int spriteID) => spriteID >= (int) this.m_imagespriteCount;

    public bool IsFillSprite(int spriteID)
    {
      return this.IsRectSprite(spriteID) && this.GetRectsprite((ushort) ((uint) spriteID - (uint) this.m_imagespriteCount)).color != -1;
    }

    public bool IsCollisionSprite(int spriteID)
    {
      return this.IsRectSprite(spriteID) && this.GetRectsprite((ushort) ((uint) spriteID - (uint) this.m_imagespriteCount)).color == -1;
    }

    public bool LoadArchetypeCharacter(byte archetypeID, byte characterID)
    {
      SG_Home.DDD("\\_/ SPRITEGLU \\_/: LoadArchetypeCharacter() begin");
      SG_Home.DDD("{0} {1},%d", (object) " |  SPRITEGLU  | : LoadArchetypeCharacter() loading (if necessary) archetype,character", (object) archetypeID, (object) characterID);
      if (!this.LoadArchetype(archetypeID))
        return false;
      this.LoadCharacter(archetypeID, characterID);
      SG_Home.DDD("/^\\ SPRITEGLU /^\\: LoadArchetypeCharacter() return");
      return true;
    }

    public bool IsLoadedArchetype(byte archetypeID)
    {
      return this.m_ppArchetypes[(int) archetypeID] != null;
    }

    public bool LoadArchetype(byte archetypeID)
    {
      if (!this.IsLoadedArchetype(archetypeID))
      {
        this.m_ppArchetypes[(int) archetypeID] = new SG_Archetype();
        SG_Archetype archetype = this.GetArchetype(archetypeID);
        SG_Home.DDD(" |  SPRITEGLU  | : LoadArchetype() acquiring data input stream");
        DataInputStream dis = new DataInputStream(this.generateArcheTypeIDString(this.m_resgenIDBinaryArchetype0, (int) archetypeID));
        if (dis.GetFail())
          return false;
        SG_Home.DDD("{0} {1} (%s)", (object) " |  SPRITEGLU  | : LoadArchetype() loading archetype", (object) archetypeID, (object) "tempDebugName");
        SG_Home.DDD(" |  SPRITEGLU  | : LoadArchetype() reading layers");
        archetype.LoadLayers(dis);
        SG_Home.DDD(" |  SPRITEGLU  | : loadArchetype() reading frames");
        archetype.LoadFrames(dis);
        SG_Home.DDD(" |  SPRITEGLU  | : LoadArchetype() reading animations");
        archetype.LoadAnimations(dis);
        SG_Home.DDD(" |  SPRITEGLU  | : LoadArchetype() reading characters");
        archetype.LoadCharacters(dis, this.m_imagespriteCount);
        SG_Home.DDD(" |  SPRITEGLU  | : LoadArchetype() closing data input stream");
        dis.Close();
        return true;
      }
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadArchetype() no need to load - already loaded is archetype", (object) archetypeID);
      return true;
    }

    public void DumpArchetype(byte archetypeID)
    {
      if (this.IsLoadedArchetype(archetypeID))
      {
        SG_Archetype ppArchetype = this.m_ppArchetypes[(int) archetypeID];
        SG_Home.DDD("{0} {1} (%s)", (object) " |  SPRITEGLU  | : DumpArchetype() now dumping archetype", (object) archetypeID, (object) "tempDebugName");
        this.m_ppArchetypes[(int) archetypeID] = (SG_Archetype) null;
      }
      else
        SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : DumpArchetype() no need to dump - already not loaded is archetype", (object) archetypeID);
    }

    public void LoadCharacter(byte archetypeID, byte characterID)
    {
      SG_Character character = this.GetArchetype(archetypeID).GetCharacter((int) characterID);
      if (!character.loaded)
      {
        SG_Home.DDD("{0} {1} (%s)", (object) " |  SPRITEGLU  | : LoadCharacter() loading character", (object) characterID, (object) "tempDebugName");
        character.loaded = true;
        for (int index = 0; index < (int) this.m_imagespriteCount; ++index)
        {
          SG_Imagesprite imagesprite = this.GetImagesprite((ushort) index);
          if (character.imagespriteUseBitVector.IsMember(index))
          {
            SG_Image image = this.GetTint(imagesprite.tintID).image;
            byte transform = imagesprite.transform;
            if (!image.IsTransformLoaded(transform))
            {
              if (image.LoadTransformRequiresRawImage(transform))
                this.m_resourceQueue.m_imagespriteQueue.imagespritesBitVector.SetMember(index, true);
              else
                image.LoadTransform(transform, (ICRenderSurface) null);
            }
          }
        }
      }
      else
        SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : LoadCharacter() no need to load - already loaded is character", (object) characterID);
    }

    public bool DumpCharacter(byte archetypeID, byte characterID)
    {
      SG_Character character1 = this.GetArchetype(archetypeID).GetCharacter((int) characterID);
      if (character1.loaded)
      {
        character1.loaded = false;
        SG_Home.DDD("{0} {1} (%s)", (object) " |  SPRITEGLU  | : DumpCharacter() dumping character", (object) characterID, (object) "tempDebugName");
        SG_Home.DDD(" |  SPRITEGLU  | : DumpCharacter() searching for and losing references to unneeded images");
        for (int index = 0; index < (int) this.m_imagespriteCount; ++index)
        {
          SG_Imagesprite imagesprite = this.GetImagesprite((ushort) index);
          SG_Image image = this.GetTint(imagesprite.tintID).image;
          byte transform = imagesprite.transform;
          if (image.IsTransformLoaded(transform))
          {
            bool flag = false;
            for (int archetypeID1 = 0; archetypeID1 < (int) this.m_archetypeCount; ++archetypeID1)
            {
              if (this.IsLoadedArchetype((byte) archetypeID1))
              {
                SG_Archetype archetype = this.GetArchetype((byte) archetypeID1);
                for (int idx = 0; idx < (int) archetype.GetCharacterCount(); ++idx)
                {
                  SG_Character character2 = archetype.GetCharacter(idx);
                  if (character2.loaded && character2.imagespriteUseBitVector.IsMember(index))
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
            if (!flag)
              image.DumpTransform(transform);
          }
        }
        return true;
      }
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : DumpCharacter()  no need to dump - already not loaded is character", (object) characterID);
      return false;
    }

    public void DumpAllImages()
    {
      SG_Home.DDD(" |  SPRITEGLU  | : DumpAllImages() dumping all images");
      uint num = 0;
      for (ushort imagespriteID = 0; (int) imagespriteID < (int) this.m_imagespriteCount; ++imagespriteID)
      {
        SG_Image image = this.GetTint(this.GetImagesprite(imagespriteID).tintID).image;
        num += image.DumpAllTransforms();
      }
      SG_Home.DDD("{0} {1}", (object) " |  SPRITEGLU  | : DumpAllImages() dumped this many images", (object) num);
    }

    public void DumpAllArchetypes()
    {
      for (int archetypeID = 0; archetypeID < (int) this.m_archetypeCount; ++archetypeID)
        this.DumpArchetype((byte) archetypeID);
    }

    public void computeSpriteDrawInfos(
      out SpriteDrawInfo[] spriteDrawInfos,
      out uint zCount,
      uint archetypeID,
      uint characterID,
      ushort spriteIDBefore)
    {
      int spritemapId = (int) this.GetArchetype((byte) archetypeID).GetCharacter((int) characterID).spritemapID;
      if (spritemapId == -1)
      {
        this.configureSpriteInfo(out this.spriteInfoArraySize1[0], (short) spriteIDBefore, (short) 0, (short) 0);
        spriteDrawInfos = this.spriteInfoArraySize1;
        zCount = 1U;
      }
      else
      {
        SG_Spritemap spritemap = this.GetSpritemap((byte) spritemapId);
        int index1 = this.binarySearch(spritemap.spritelinks, (int) spritemap.spritelinkCount, (int) spriteIDBefore);
        if (index1 == -1)
        {
          this.configureSpriteInfo(out this.spriteInfoArraySize1[0], (short) spriteIDBefore, (short) 0, (short) 0);
          spriteDrawInfos = this.spriteInfoArraySize1;
          zCount = 1U;
        }
        else
        {
          SG_Spritemap.SG_Spritelink spritelink = spritemap.spritelinks[index1];
          SG_SpritelinkType type = spritelink.GetType();
          short spriteIdAfter = (short) spritelink.spriteIDAfter;
          short out_offsetX;
          short out_offsetY;
          spritelink.GetOffsets(out out_offsetX, out out_offsetY);
          int num;
          switch (type)
          {
            case SG_SpritelinkType.SPRITELINK_TYPE__REPLACE:
              this.configureSpriteInfo(out this.spriteInfoArraySize1[0], spriteIdAfter, out_offsetX, out_offsetY);
              spriteDrawInfos = this.spriteInfoArraySize1;
              zCount = 1U;
              return;
            case SG_SpritelinkType.SPRITELINK_TYPE__OVERLAY:
              num = 0;
              break;
            default:
              num = 1;
              break;
          }
          int index2 = num;
          this.configureSpriteInfo(out this.spriteInfoArraySize2[index2], (short) spriteIDBefore, (short) 0, (short) 0);
          this.configureSpriteInfo(out this.spriteInfoArraySize2[1 - index2], spriteIdAfter, out_offsetX, out_offsetY);
          spriteDrawInfos = this.spriteInfoArraySize2;
          zCount = 2U;
        }
      }
    }

    public void configureSpriteInfo(
      out SpriteDrawInfo spriteDrawInfo,
      short spriteID,
      short offsetX,
      short offsetY)
    {
      spriteDrawInfo.spriteID = (ushort) spriteID;
      spriteDrawInfo.offsetX = offsetX;
      spriteDrawInfo.offsetY = offsetY;
    }

    public int binarySearch(
      SG_Spritemap.SG_Spritelink[] spritelinks,
      int spritelinkCount,
      int spriteIDBefore)
    {
      int num1 = spritelinkCount - 1;
      int num2 = 0;
      while (num2 <= num1)
      {
        int index = num2 + num1 >> 1;
        int spriteIdBefore = (int) spritelinks[index].spriteIDBefore;
        if (spriteIdBefore == spriteIDBefore)
          return index;
        if (spriteIDBefore < spriteIdBefore)
          num1 = index - 1;
        else if (spriteIDBefore > spriteIdBefore)
          num2 = index + 1;
      }
      return -1;
    }

    public void drawInit(int x, int y, CRectangle clipRect)
    {
      this.graphics.SetOffset((short) x, (short) y);
      this.graphics.SetClip(clipRect);
    }

    public void drawAccumulate(int x, int y, uint w, uint h, ushort spriteID, byte transform)
    {
      this.drawAccumulate(x, y, w, h, spriteID, transform, 1000);
    }

    public void drawAccumulate(
      int x,
      int y,
      uint w,
      uint h,
      ushort spriteID,
      byte transform,
      int quantum)
    {
      if (this.IsImageSprite((int) spriteID))
      {
        SG_Imagesprite imagesprite = this.GetImagesprite(spriteID);
        byte transform1 = SquareTransform.Compose(transform, imagesprite.transform);
        ICRenderSurface p_ICRenderSurface = (ICRenderSurface) null;
        Graphics.RenderInfo renderInfo = new Graphics.RenderInfo();
        this.GetTint(imagesprite.tintID).image.GetSrcImageAndRenderInfo(transform1, out p_ICRenderSurface, ref renderInfo);
        this.graphics.DrawRegion(p_ICRenderSurface, renderInfo, x, y, quantum);
      }
      else
      {
        int color = this.m_pRectsprites[(int) spriteID - (int) this.m_imagespriteCount].color;
        if (color == -1)
          return;
        this.graphics.FillRect(x, y, (uint) ((ulong) w * (ulong) quantum / 1000UL), (uint) ((ulong) h * (ulong) quantum / 1000UL), color);
      }
    }

    public void drawResolve(SG_Presenter sgPresenter)
    {
    }

    public void boundsInit()
    {
      this.boundsPointPair.x0 = short.MaxValue;
      this.boundsPointPair.y0 = short.MaxValue;
      this.boundsPointPair.x1 = short.MinValue;
      this.boundsPointPair.y1 = short.MinValue;
    }

    public void boundsAccumulate(int x, int y, int w, int h)
    {
      this.boundsPointPair.x0 = (short) CMath.Min(x, (int) this.boundsPointPair.x0);
      this.boundsPointPair.x1 = (short) CMath.Max(x + w, (int) this.boundsPointPair.x1);
      this.boundsPointPair.y0 = (short) CMath.Min(y, (int) this.boundsPointPair.y0);
      this.boundsPointPair.y1 = (short) CMath.Max(y + h, (int) this.boundsPointPair.y1);
    }

    public void boundsResolve(ref CRectangle rect, SG_Presenter sgPresenter)
    {
      rect.m_x = (int) sgPresenter.m_positionX + (int) this.boundsPointPair.x0;
      rect.m_y = (int) sgPresenter.m_positionY + (int) this.boundsPointPair.y0;
      rect.m_dx = (int) this.boundsPointPair.x1 - (int) this.boundsPointPair.x0;
      rect.m_dy = (int) this.boundsPointPair.y1 - (int) this.boundsPointPair.y0;
    }

    public void collisionInit(
      short shotX,
      short shotY,
      short shotW,
      short shotH,
      SG_Presenter sgPresenter)
    {
      this.collisionInfo.x = shotX;
      this.collisionInfo.y = shotY;
      this.collisionInfo.w = shotW;
      this.collisionInfo.h = shotH;
      this.collisionInfo.spriteID = (short) -1;
    }

    public void collisionAccumulate(int x, int y, int w, int h, short spriteID)
    {
      int x1 = (int) this.collisionInfo.x;
      int y1 = (int) this.collisionInfo.y;
      int w1 = (int) this.collisionInfo.w;
      int h1 = (int) this.collisionInfo.h;
      if (x1 + w1 <= x || x1 >= x + w || y1 + h1 <= y || y1 >= y + h)
        return;
      this.collisionInfo.spriteID = spriteID;
    }

    public short collisionResolve() => this.collisionInfo.spriteID;

    public bool sgDebugCheckInitialized() => this.IsInitialized();

    public bool sgDebugCheckArchetypeID(int archetypeID)
    {
      return this.sgDebugCheckInitialized() && archetypeID >= 0 && archetypeID < (int) this.m_archetypeCount;
    }

    public bool sgDebugCheckCharacterID(int archetypeID, int characterID)
    {
      return this.sgDebugCheckArchetypeID(archetypeID) && characterID >= 0 && characterID < (int) this.GetArchetype((byte) archetypeID).GetCharacterCount();
    }

    public bool sgDebugCheckAnimationID(int archetypeID, int animationID)
    {
      return this.sgDebugCheckArchetypeID(archetypeID) && animationID >= 0 && animationID < (int) this.GetArchetype((byte) archetypeID).GetAnimationCount();
    }

    public bool sgDebugCheckIsLoadedArchetypeCharacter(int archetypeID, int characterID)
    {
      return this.IsLoadedArchetypeCharacter(archetypeID, characterID);
    }
  }
}
