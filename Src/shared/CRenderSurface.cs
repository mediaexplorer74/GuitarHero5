// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CRenderSurface
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public abstract class CRenderSurface : ICRenderSurface
  {
    public bool m_usingLoadtimeTransform;
    public bool m_transpose;
    public ICRenderSurface.SourceImageStreamTransformType m_srcImgTransformType;
    protected static byte[] m_tmpName = new byte[256];
    protected static CArrayOutputStream m_tmpArrayOut = new CArrayOutputStream();
    protected static CArrayInputStream m_tmpArrayIn = new CArrayInputStream();

    public override bool Load(CInputStream inStream, uint mimeKey, CIdToObjectRouter idToObject)
    {
      bool flag1 = false;
      switch (mimeKey)
      {
        case 1497334080:
        case 1508883522:
        case 1509211202:
        case 1509211225:
        case 2672542834:
        case 3782864830:
        case 3782866110:
        case 4231102733:
          bool flag2 = false;
          uint mimeKey1 = 0;
          uint num = 0;
          ICRenderSurface.Param[] objArray = new ICRenderSurface.Param[20];
          flag1 = true;
          uint index = 0;
          while (inStream.Available() > 0U)
          {
            ICRenderSurface.LoadId loadId1 = (ICRenderSurface.LoadId) inStream.ReadUInt32();
            switch (loadId1)
            {
              case ICRenderSurface.LoadId.LI_Unknown:
                goto label_15;
              case ICRenderSurface.LoadId.LI_ColorBufferFormat:
                objArray[(int) index].m_id = ICRenderSurface.ParamId.ColorBufferFormat;
                ICRenderSurface.LoadId loadId2 = (ICRenderSurface.LoadId) inStream.ReadUInt32();
                switch (loadId2)
                {
                  case ICRenderSurface.LoadId.LI_Unpalettize:
                    objArray[(int) index].m_val = (object) Color.Format.Unknown;
                    break;
                  case ICRenderSurface.LoadId.LI_Unknown:
                    objArray[(int) index].m_val = (object) Color.Format.Unknown;
                    flag2 = true;
                    break;
                  default:
                    objArray[(int) index].m_val = (object) loadId2;
                    break;
                }
                break;
              case ICRenderSurface.LoadId.LI_NameOfSourceImageStream:
                string name = "";
                for (char ch = (char) inStream.ReadUInt8(); ch != char.MinValue; ch = (char) inStream.ReadUInt8())
                  name +=  ch;
                CInputStream stream;
                flag1 = idToObject.NameToInputStream(name, out stream, out mimeKey1);
                if (flag1)
                {
                  objArray[(int) index].m_id = ICRenderSurface.ParamId.PointerToSourceImageStream;
                  objArray[(int) index].m_val = (object) stream;
                  break;
                }
                break;
              default:
                objArray[(int) index].m_id = (ICRenderSurface.ParamId) loadId1;
                objArray[(int) index].m_val = (object) inStream.ReadUInt32();
                break;
            }
            ++index;
          }
label_15:
          if (flag2)
          {
            objArray[(int) index].m_id = ICRenderSurface.ParamId.MaintainPossiblePalettization;
            objArray[(int) index].m_val = (object) flag2;
            ++index;
          }
          if (mimeKey1 != 0U && num != 0U)
            flag1 = false;
          else if (mimeKey1 != 0U)
          {
            objArray[(int) index].m_id = ICRenderSurface.ParamId.MimeKeyOfSourceImageStream;
            objArray[(int) index].m_val = (object) mimeKey1;
          }
          else if (num != 0U)
          {
            objArray[(int) index].m_id = ICRenderSurface.ParamId.MimeKeyOfReferenceImage;
            objArray[(int) index].m_val = (object) num;
          }
          objArray[(int) (index + 1U)].m_id = ICRenderSurface.ParamId.Unknown;
          if (flag1)
          {
            flag1 = this.Initialize(objArray);
            break;
          }
          break;
      }
      return flag1;
    }

    protected CRenderSurface()
    {
      this.m_usingLoadtimeTransform = false;
      this.m_transpose = false;
    }
  }
}
