// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CKeysetResource
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public sealed class CKeysetResource
  {
    private uint m_count;
    private string[] m_key;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public CKeysetResource()
    {
      this.m_count = 0U;
      this.m_key = (string[]) null;
    }

    public bool Load(CInputStream inStream, uint mimeKey, CIdToObjectRouter idToObject)
    {
      bool flag = false;
      this.m_count = (uint) inStream.ReadUInt16();
      if (this.m_count == 0U)
      {
        flag = true;
      }
      else
      {
        this.m_key = new string[(int) this.m_count];
        if (this.m_key != null)
        {
          byte[] bytes = new byte[256];
          for (uint index = 0; index < this.m_count; ++index)
          {
            int count = 0;
            byte num = inStream.ReadUInt8();
            while (num != (byte) 0)
            {
              bytes[count] = num;
              num = inStream.ReadUInt8();
              ++count;
            }
            this.m_key[(int) index] = CKeysetResource.m_utf8Encoding.GetString(bytes, 0, count);
          }
          if (!inStream.GetFail())
            flag = true;
        }
      }
      return flag;
    }

    public uint GetCount() => this.m_count;

    public string GetKey(uint idx) => this.m_key[(int) idx];

    public string[] GetKeys() => this.m_key;
  }
}
