// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CResourceManager_v3
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using com.glu.resgen_content;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public sealed class CResourceManager_v3 : CResourceManager
  {
    private CRegistry m_databaseReg;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public override bool Initialize()
    {
      this.m_dic = new Dictionary<string, CResource>();
      this.m_idToObject = new CIdToObjectRouter();
      this.m_idToObject.SetCallbackNameToInputStream(new CIdToObjectRouter.Handler_NameToInputStream(this.Handler_NameToSourceImageStream));
      return true;
    }

    public CResourceManager_v3() => this.m_databaseReg = new CRegistry();

    public override bool AddDatabase(string filename)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.m_databaseReg.Begin());
      while (cregistryItr != this.m_databaseReg.End())
      {
        CResourceManager_v3.DataBaseElement data = (CResourceManager_v3.DataBaseElement) cregistryItr.GetElement().GetData();
        if (filename == data.m_name)
          return true;
        ++cregistryItr;
      }
      bool flag = false;
      resgen resgen = CApplet.GetInstance().Content.Load<resgen>(filename);
      if (resgen != null)
      {
        this.m_databaseReg.Add(this.m_databaseReg.CreateSystemElement(CStringToKey.Call(nameof (filename)), (object) new CResourceManager_v3.DataBaseElement()
        {
          m_database = resgen,
          m_name = filename
        }, 0U, 0U));
        flag = true;
      }
      return flag;
    }

    public override bool RemoveDatabase(string filename, bool destroyRelatedResources)
    {
      bool flag = false;
      CRegistryItr itr = new CRegistryItr(this.m_databaseReg.Begin());
      while (itr != this.m_databaseReg.End())
      {
        CResourceManager_v3.DataBaseElement data = (CResourceManager_v3.DataBaseElement) itr.GetElement().GetData();
        if (filename == data.m_name)
        {
          if (destroyRelatedResources)
          {
            List<string> stringList = new List<string>();
            Dictionary<string, CResource>.Enumerator enumerator = this.m_dic.GetEnumerator();
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, CResource> current = enumerator.Current;
              if (current.Key != null)
                stringList.Add(current.Key);
            }
            foreach (string key in stringList)
              this.m_dic.Remove(key);
          }
          CRegistryElement element = itr.GetElement();
          //CRegistryItr.
          itr++;
          this.m_databaseReg.Remove((CSystemElement) element);
          flag = true;
          break;
        }
                // Replace this line:
                // CRegistryItr.op_Increment(itr);

                // With this line:
                ++itr;
      }
      return flag;
    }

    public override CResourceManager.CConsecutiveResourceIdItr CreateAndInitConsecutiveResourceIdItr(
      string beginName,
      uint count)
    {
      CResourceManager_v3.CConsecutiveResourceIdItr_v2 consecutiveResourceIdItr = new CResourceManager_v3.CConsecutiveResourceIdItr_v2();
      CRegistryItr cregistryItr = new CRegistryItr(this.m_databaseReg.Begin());
      while (cregistryItr != this.m_databaseReg.End())
      {
        if (consecutiveResourceIdItr.Initialize(((CResourceManager_v3.DataBaseElement) cregistryItr.GetElement().GetData()).m_database, beginName, count))
          return (CResourceManager.CConsecutiveResourceIdItr) consecutiveResourceIdItr;
        ++cregistryItr;
      }
      return (CResourceManager.CConsecutiveResourceIdItr) null;
    }

    public override bool AddResource(CResource resource)
    {
      bool flag = false;
      this.m_dic.Add(resource.Name, resource);
      return flag;
    }

    public override bool RemoveResource(string name)
    {
      if (name == null || !this.m_dic.ContainsKey(name))
        return false;
      this.m_dic.Remove(name);
      return true;
    }

    public override bool RemoveResource(ref CResource resource)
    {
      if (resource == null || !this.RemoveResource(resource.Name))
        return false;
      resource = (CResource) null;
      return true;
    }

    public override CResource.CreationContext CreateResource(string name, out CResource resource)
    {
      return this.CreateResource(out resource, CHandleFactory.CreateHashKey(name), name, 0U, (CInputStream) null);
    }

    public override bool GetResource(string name, out CResource resource)
    {
      if (name != null && this.m_dic.ContainsKey(name))
      {
        this.m_dic.TryGetValue(name, out resource);
        if (resource != null)
          return true;
      }
      resource = (CResource) null;
      return false;
    }

    public override bool ReleaseResource(string name)
    {
      if (name != null && this.m_dic.ContainsKey(name))
      {
        CResource cresource;
        this.m_dic.TryGetValue(name, out cresource);
        if (cresource != null)
        {
          --cresource.RefCount;
          if (cresource.RefCount == 0U)
            this.m_dic.Remove(name);
          return true;
        }
      }
      return false;
    }

    public override bool ReleaseResource(ref CResource resource)
    {
      if (resource != null)
      {
        string name = resource.Name;
        if (name != null && this.m_dic.ContainsKey(name))
        {
          CResource cresource;
          this.m_dic.TryGetValue(name, out cresource);
          if (cresource != null)
          {
            --cresource.RefCount;
            if (cresource.RefCount == 0U)
            {
              this.m_dic.Remove(name);
              resource = (CResource) null;
            }
            return true;
          }
        }
      }
      return false;
    }

    public override bool DestroyResource(string name)
    {
      if (name == null || !this.m_dic.ContainsKey(name))
        return false;
      this.m_dic.Remove(name);
      return true;
    }

    protected override CResource.CreationContext CreateResource(
      out CResource resource,
      uint handle,
      string name,
      uint mimeKey,
      CInputStream inStream)
    {
      CResource.CreationContext resource1 = CResource.CreationContext.UnableToCreateResource;
      uint resMimeKey = 0;
      uint streamMimeKey = mimeKey;
      bool flag = false;
      if (this.m_dic.ContainsKey(name))
      {
        this.m_dic.TryGetValue(name, out resource);
        if (!resource.IsCreated())
        {
          flag = false;
        }
        else
        {
          resource1 = CResource.CreationContext.ResourceWasPreviouslyCreated;
          flag = true;
        }
        ++resource.RefCount;
      }
      else
        resource = (CResource) null;
      if (!flag)
      {
        if (inStream == null)
          this.SearchDatabaseRegistryForStream(name, out resMimeKey, out inStream, out streamMimeKey, out CSystemElement _, out string _, out bool _);
        if (resource == null)
        {
          switch (resMimeKey)
          {
            case 802794068:
            case 802796362:
            case 855475778:
            case 855480801:
            case 855482183:
            case 4253517186:
            case 4253710164:
            case 4254380993:
              resource = (CResource) new CResourceMedia();
              break;
            case 1776669532:
              resource = (CResource) new CResourceKeyset();
              break;
            case 4108329507:
              resource = (CResource) new CResourceBinary();
              break;
            case 4136020700:
              resource = (CResource) new CResourceStrWChar();
              break;
            case 4231102733:
              resource = (CResource) new CResourceRenderSurface();
              break;
            default:
              resource = (CResource) null;
              break;
          }
        }
        if (resource != null)
        {
          resource1 = resource.CreateInternal(handle, name, inStream, streamMimeKey, this.m_idToObject);
          if (resource1 != CResource.CreationContext.UnableToCreateResource)
          {
            ++resource.RefCount;
            this.AddResource(resource);
          }
        }
      }
      return resource1;
    }

    private bool SearchDatabaseRegistryForStream(
      string name,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey,
      out CSystemElement ele,
      out string databaseName,
      out bool databaseNameWasDecompressedIntoTempBuffer)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.m_databaseReg.Begin());
      while (cregistryItr != this.m_databaseReg.End())
      {
        if (this.GetStream(((CResourceManager_v3.DataBaseElement) cregistryItr.GetElement().GetData()).m_database, name, out resMimeKey, out stream, out streamMimeKey, out databaseName, true, out databaseNameWasDecompressedIntoTempBuffer))
        {
          ele = (CSystemElement) cregistryItr.GetElement();
          return true;
        }
        ++cregistryItr;
      }
      ele = (CSystemElement) null;
      stream = (CInputStream) null;
      resMimeKey = 0U;
      streamMimeKey = 0U;
      databaseName = "";
      databaseNameWasDecompressedIntoTempBuffer = false;
      return false;
    }

    private bool GetStream(
      resgen database,
      string name,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey,
      out string databaseName,
      bool databaseNameIsAssignedUncompressed,
      out bool databaseNameWasDecompressedIntoTempBuffer)
    {
      stream = (CInputStream) null;
      resMimeKey = 0U;
      streamMimeKey = 0U;
      databaseName = (string) null;
      databaseNameWasDecompressedIntoTempBuffer = false;
      IList<int> itemList = resgen.HashToKeyInfoLUT.GetItemList(database.m_hashToKeyInfo, (int) ((long) CStringToKey.Call(name) % (long) database.m_hashToKeyInfoLength));
      bool flag = false;
      foreach (int index in (IEnumerable<int>) itemList)
      {
        if ((object) database.Items[index].GetType() == (object) typeof (resgenBinary) && name == ((resgenBinary) database.Items[index]).name)
        {
          if (this.GetStream((resgenBinary) database.Items[index], out resMimeKey, out stream, out streamMimeKey))
            flag = true;
        }
        else if ((object) database.Items[index].GetType() != (object) typeof (resgenImage) || !(name == ((resgenImage) database.Items[index]).name))
        {
          if ((object) database.Items[index].GetType() == (object) typeof (resgenKeyset) && name == ((resgenKeyset) database.Items[index]).name)
          {
            if (this.GetStream((resgenKeyset) database.Items[index], out resMimeKey, out stream, out streamMimeKey))
              flag = true;
          }
          else if ((object) database.Items[index].GetType() == (object) typeof (resgenSound) && name == ((resgenSound) database.Items[index]).name)
          {
            if (this.GetStream((resgenSound) database.Items[index], out resMimeKey, out stream, out streamMimeKey))
              flag = true;
          }
          else if ((object) database.Items[index].GetType() == (object) typeof (resgenString) && name == ((resgenString) database.Items[index]).name)
          {
            if (this.GetStream((resgenString) database.Items[index], out resMimeKey, out stream, out streamMimeKey))
              flag = true;
          }
          else if ((object) database.Items[index].GetType() == (object) typeof (resgenSurface) && name == ((resgenSurface) database.Items[index]).name && this.GetStream((resgenSurface) database.Items[index], out resMimeKey, out stream, out streamMimeKey))
            flag = true;
        }
        if (flag)
          return true;
      }
      return false;
    }

    private bool GetStream(
      resgenBinary res,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey)
    {
      byte[] bytes = CResourceManager_v3.m_utf8Encoding.GetBytes(res.value);
      CArrayInputStream carrayInputStream = new CArrayInputStream();
      carrayInputStream.Open(bytes, (uint) bytes.Length);
      stream = (CInputStream) carrayInputStream;
      streamMimeKey = 1930331075U;
      resMimeKey = 4108329507U;
      return true;
    }

    private bool GetStream(
      resgenKeyset res,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey)
    {
      uint size1 = (uint) (res.entry.Length * 256);
      byte[] pBuf = new byte[(int) size1];
      CArrayOutputStream carrayOutputStream = new CArrayOutputStream();
      carrayOutputStream.Open(pBuf, size1);
      carrayOutputStream.WriteUInt16((ushort) res.entry.Length);
      uint size2 = 2;
      foreach (resgenKeysetEntry resgenKeysetEntry in res.entry)
      {
        byte[] bytes = CResourceManager_v3.m_utf8Encoding.GetBytes(resgenKeysetEntry.name);
        carrayOutputStream.Write(bytes, (uint) bytes.Length);
        uint num = size2 + (uint) bytes.Length;
        carrayOutputStream.WriteUInt8((byte) 0);
        size2 = num + 1U;
      }
      CArrayInputStream carrayInputStream = new CArrayInputStream();
      carrayInputStream.Open(pBuf, size2);
      stream = (CInputStream) carrayInputStream;
      streamMimeKey = 1776669532U;
      resMimeKey = 1776669532U;
      return true;
    }

    private bool GetStream(
      resgenSound res,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey)
    {
      byte[] bytes = CResourceManager_v3.m_utf8Encoding.GetBytes(res.value);
      CArrayInputStream carrayInputStream = new CArrayInputStream();
      carrayInputStream.Open(bytes, (uint) bytes.Length);
      stream = (CInputStream) carrayInputStream;
      streamMimeKey = 1930331075U;
      resMimeKey = 4253710164U;
      return true;
    }

    private bool GetStream(
      resgenString res,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey)
    {
      byte[] bytes = CResourceManager_v3.m_utf8Encoding.GetBytes(res.value);
      CArrayInputStream carrayInputStream = new CArrayInputStream();
      carrayInputStream.Open(bytes, (uint) bytes.Length);
      stream = (CInputStream) carrayInputStream;
      streamMimeKey = 4136020700U;
      resMimeKey = 4136020700U;
      return true;
    }

    private bool GetStream(
      resgenSurface res,
      out uint resMimeKey,
      out CInputStream stream,
      out uint streamMimeKey)
    {
      uint size1 = 1024;
      byte[] pBuf = new byte[(int) size1];
      CArrayOutputStream carrayOutputStream = new CArrayOutputStream();
      carrayOutputStream.Open(pBuf, size1);
      uint num1 = 0;
      uint num2;
      if (res.format != null)
      {
        carrayOutputStream.WriteUInt32(6U);
        uint num3 = num1 + 4U;
        carrayOutputStream.WriteUInt32((uint) Color.GetFormat(res.format));
        num2 = num3 + 4U;
      }
      else
      {
        carrayOutputStream.WriteUInt32(6U);
        uint num4 = num1 + 4U;
        carrayOutputStream.WriteUInt32(0U);
        num2 = num4 + 4U;
      }
      if (res.width != null)
      {
        carrayOutputStream.WriteUInt32(1U);
        uint num5 = num2 + 4U;
        carrayOutputStream.WriteUInt32(Convert.ToUInt32(res.width));
        num2 = num5 + 4U;
      }
      if (res.height != null)
      {
        carrayOutputStream.WriteUInt32(2U);
        uint num6 = num2 + 4U;
        carrayOutputStream.WriteUInt32(Convert.ToUInt32(res.height));
        num2 = num6 + 4U;
      }
      if (res.srcimg != null)
      {
        carrayOutputStream.WriteUInt32(102U);
        uint num7 = num2 + 4U;
        byte[] bytes = CResourceManager_v3.m_utf8Encoding.GetBytes(res.srcimg);
        carrayOutputStream.Write(bytes, (uint) bytes.Length);
        uint num8 = num7 + (uint) bytes.Length;
        carrayOutputStream.WriteUInt8((byte) 0);
        num2 = num8 + 1U;
      }
      if (res.transform != null)
      {
        carrayOutputStream.WriteUInt32(17U);
        uint num9 = num2 + 4U;
        ICRenderSurface.SourceImageStreamTransformType val;
        switch (res.transform)
        {
          case "vflip":
            val = ICRenderSurface.SourceImageStreamTransformType.VerticalFlip;
            break;
          case "hflip_vflip":
            val = ICRenderSurface.SourceImageStreamTransformType.HorizontalAndVerticalFlip;
            break;
          case "transpose":
            val = ICRenderSurface.SourceImageStreamTransformType.Transpose;
            break;
          case "transpose_hflip":
            val = ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByHorizontalFlip;
            break;
          case "transpose_vflip":
            val = ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByVerticalFlip;
            break;
          case "transpose_hflip_vflip":
            val = ICRenderSurface.SourceImageStreamTransformType.TransposeFollowedByHorizontalAndVerticalFlip;
            break;
          default:
            val = ICRenderSurface.SourceImageStreamTransformType.HorizontalFlip;
            break;
        }
        carrayOutputStream.WriteUInt32((uint) val);
        num2 = num9 + 4U;
      }
      carrayOutputStream.WriteUInt32(0U);
      uint size2 = num2 + 4U;
      CArrayInputStream carrayInputStream = new CArrayInputStream();
      carrayInputStream.Open(pBuf, size2);
      stream = (CInputStream) carrayInputStream;
      streamMimeKey = 4231102733U;
      resMimeKey = 4231102733U;
      return true;
    }

    private bool SearchDatabaseRegistryForStreamValue(string name, out string value)
    {
      CRegistryItr cregistryItr = new CRegistryItr(this.m_databaseReg.Begin());
      while (cregistryItr != this.m_databaseReg.End())
      {
        if (this.GetStreamValue(((CResourceManager_v3.DataBaseElement) cregistryItr.GetElement().GetData()).m_database, name, out value))
          return true;
        ++cregistryItr;
      }
      value = (string) null;
      return false;
    }

    private bool GetStreamValue(resgen database, string name, out string value)
    {
      value = (string) null;
      foreach (int index in (IEnumerable<int>) resgen.HashToKeyInfoLUT.GetItemList(database.m_hashToKeyInfo, (int) ((long) CStringToKey.Call(name) % (long) database.m_hashToKeyInfoLength)))
      {
        if ((object) database.Items[index].GetType() == (object) typeof (resgenBinary))
        {
          if (name == ((resgenBinary) database.Items[index]).name)
          {
            value = ((resgenBinary) database.Items[index]).value;
            break;
          }
        }
        else if ((object) database.Items[index].GetType() == (object) typeof (resgenImage))
        {
          if (name == ((resgenImage) database.Items[index]).name)
          {
            value = ((resgenImage) database.Items[index]).value;
            break;
          }
        }
        else if ((object) database.Items[index].GetType() == (object) typeof (resgenSound))
        {
          if (name == ((resgenSound) database.Items[index]).name)
          {
            value = ((resgenSound) database.Items[index]).value;
            break;
          }
        }
        else if ((object) database.Items[index].GetType() == (object) typeof (resgenString) && name == ((resgenString) database.Items[index]).name)
        {
          value = ((resgenString) database.Items[index]).value;
          break;
        }
      }
      return value != null;
    }

    private bool Handler_NameToSourceImageStream(
      string name,
      out CInputStream stream,
      out uint mimeKey)
    {
      string str;
      if (this.SearchDatabaseRegistryForStreamValue(name, out str))
      {
        uint size = 100;
        byte[] pBuf = new byte[(int) size];
        CArrayOutputStream carrayOutputStream = new CArrayOutputStream();
        carrayOutputStream.Open(pBuf, size);
        foreach (char val in str)
          carrayOutputStream.WriteUInt8((byte) val);
        carrayOutputStream.WriteUInt32(0U);
        CArrayInputStream carrayInputStream = new CArrayInputStream();
        carrayInputStream.Open(pBuf, size);
        stream = (CInputStream) carrayInputStream;
        mimeKey = 1930331075U;
        return true;
      }
      stream = (CInputStream) null;
      mimeKey = 0U;
      return false;
    }

    public sealed class CConsecutiveResourceIdItr_v2 : CResourceManager.CConsecutiveResourceIdItr
    {
      private resgenGroup m_group;
      private int m_position;

      public CConsecutiveResourceIdItr_v2()
      {
        this.m_group = (resgenGroup) null;
        this.m_position = 0;
      }

      public bool Initialize(resgen resFile, uint beginHandle, uint count) => false;

      public bool Initialize(resgen resFile, string beginName, uint count)
      {
        foreach (object obj1 in resFile.Items)
        {
          if ((object) obj1.GetType() == (object) typeof (resgenGroup))
          {
            resgenGroup resgenGroup = (resgenGroup) obj1;
            int num = 0;
            foreach (object obj2 in resgenGroup.Items)
            {
              if ((object) obj2.GetType() == (object) typeof (resgenGroupEntry) && beginName == ((resgenGroupEntry) obj2).name)
              {
                this.m_group = resgenGroup;
                this.m_position = num;
                return true;
              }
              ++num;
            }
          }
        }
        return false;
      }

      public override uint GetHandle()
      {
        return CHandleFactory.CreateHashKey(((resgenGroupEntry) this.m_group.Items[this.m_position]).name);
      }

      public override string GetName()
      {
        return ((resgenGroupEntry) this.m_group.Items[this.m_position]).name;
      }

      public override CResourceManager.CConsecutiveResourceIdItr Next()
      {
        ++this.m_position;
        return (CResourceManager.CConsecutiveResourceIdItr) this;
      }

      public override CResourceManager.CConsecutiveResourceIdItr Prev()
      {
        --this.m_position;
        return (CResourceManager.CConsecutiveResourceIdItr) this;
      }

      public override CResourceManager.CConsecutiveResourceIdItr Seek(
        CResourceManager.CConsecutiveResourceIdItr.SeekFrom seekFrom,
        int val)
      {
        if (seekFrom == CResourceManager.CConsecutiveResourceIdItr.SeekFrom.Beginning)
          this.m_position = val;
        else
          this.m_position += val;
        return (CResourceManager.CConsecutiveResourceIdItr) this;
      }
    }

    private struct DataBaseElement
    {
      public resgen m_database;
      public string m_name;

      private DataBaseElement(resgen resfile, string name)
      {
        this.m_database = resfile;
        this.m_name = name;
      }
    }
  }
}
