// Decompiled with JetBrains decompiler
// Type: com.glu.shared.ICRenderSurface
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public abstract class ICRenderSurface : CClass
  {
    public const uint ClassId = 3376430967;

    public static ICRenderSurface CreateInstance(
      ICGraphics.Abstraction abstraction,
      ICRenderSurface.Type type,
      ICRenderSurface.Targetability targetability)
    {
      ICRenderSurface instance = (ICRenderSurface) null;
      bool flag = targetability == ICRenderSurface.Targetability.TargetableByHardwareRenderer || targetability == ICRenderSurface.Targetability.TargetableBySoftwareAndHardwareRenderers;
      if (abstraction != ICGraphics.Abstraction.Software)
      {
        ICGraphics.GetInstance();
        switch (type)
        {
          case ICRenderSurface.Type.OffScreen:
            instance = flag ? (ICRenderSurface) null : (ICRenderSurface) new CRenderSurface_XNA_OffScreen();
            break;
          case ICRenderSurface.Type.Window:
            instance = (ICRenderSurface) null;
            break;
        }
      }
      return instance;
    }

    public abstract bool Load(CInputStream inStream, uint mimeKey, CIdToObjectRouter idToObject);

    public abstract bool Initialize(ICRenderSurface.Param[] param);

    public abstract bool GetWidthAndHeight(out uint width, out uint height);

    protected ICRenderSurface()
      : base(3376430967U)
    {
    }

    public enum Type
    {
      OffScreen,
      Window,
    }

    public enum Targetability
    {
      NotTargetable,
      TargetableBySoftwareRenderer,
      TargetableByHardwareRenderer,
      TargetableBySoftwareAndHardwareRenderers,
    }

    public struct Param
    {
      public ICRenderSurface.ParamId m_id;
      public object m_val;
    }

    public enum ParamId
    {
      Unknown,
      Width,
      Height,
      FullScreen,
      Lockable,
      MipMappable,
      ColorBufferFormat,
      DepthBufferFormat,
      StencilBufferFormat,
      ColorKey,
      MaintainPossiblePalettization,
      PointerToPalette,
      PointerToReferenceImage,
      MimeKeyOfReferenceImage,
      PointerToSourceImageStream,
      MimeKeyOfSourceImageStream,
      MaintainHighDegreeOfSourceImageStreamColorInfo,
      SourceImageStreamTransform,
    }

    public enum SourceImageStreamTransformType
    {
      HorizontalFlip,
      VerticalFlip,
      HorizontalAndVerticalFlip,
      Transpose,
      TransposeFollowedByHorizontalFlip,
      TransposeFollowedByVerticalFlip,
      TransposeFollowedByHorizontalAndVerticalFlip,
    }

    public enum Buffer
    {
      Color,
      Depth,
      Stencil,
    }

    public enum LockType
    {
      ReadWrite,
      ReadOnly,
      WriteOnly,
    }

    public enum SwapEffect
    {
      None,
      CopyAndDiscard,
      CopyAndRetain,
      TrueSwapAndDiscard,
      TrueSwapAndRetain,
    }

    public class Description
    {
    }

    public enum LoadId
    {
      LI_Unpalettize = -1, // 0xFFFFFFFF
      LI_Unknown = 0,
      LI_Width = 1,
      LI_Height = 2,
      LI_FullScreen = 3,
      LI_Lockable = 4,
      LI_MipMappable = 5,
      LI_ColorBufferFormat = 6,
      LI_DepthBufferFormat = 7,
      LI_StencilBufferFormat = 8,
      LI_ColorKey = 9,
      LI_KeyToPalette = 11, // 0x0000000B
      LI_KeyToReferenceImage = 12, // 0x0000000C
      LI_MimeKeyOfReferenceImage = 13, // 0x0000000D
      LI_KeyToSourceImageStream = 14, // 0x0000000E
      LI_MimeKeyOfSourceImageStream = 15, // 0x0000000F
      LI_MaintainHighDegreeOfSourceImageStreamColorInfo = 16, // 0x00000010
      LI_SourceImageStreamTransform = 17, // 0x00000011
      LI_NameOfPalette = 100, // 0x00000064
      LI_NameOfReferenceImage = 101, // 0x00000065
      LI_NameOfSourceImageStream = 102, // 0x00000066
    }

    private class IDebug : CSingleton
    {
      protected const uint ICRenderSurface_IDebug = 1044788928;

      private enum State
      {
        LogParamsAll,
        LogParamsAllOnError,
        LogDescriptionAll,
        LogDescriptionAllOnError,
        LogDescriptionWindow,
        LogMessageAll,
        LogMessageAllOnError,
        LogMessageWindow,
      }
    }
  }
}
