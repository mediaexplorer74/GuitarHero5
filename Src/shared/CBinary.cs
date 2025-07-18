// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CBinary
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using com.glu.binary_content;
using System;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public sealed class CBinary : CClass
  {
    public const uint ClassId = 927958617;
    private binary m_binary;
    private uint m_size;
    private uint m_mimeKey;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public CBinary()
      : base(927958617U)
    {
      this.m_binary = (binary) null;
      this.m_size = 0U;
      this.m_mimeKey = 0U;
    }

    public void Destroy()
    {
      this.m_binary = (binary) null;
      this.m_size = 0U;
      this.m_mimeKey = 0U;
    }

    public bool Load(CInputStream inStream, uint mimeKey)
    {
      bool flag = false;
      uint length = inStream.Available();
      if (mimeKey == 1930331075U)
      {
        if (length > 0U)
        {
          byte[] numArray = new byte[(int) length];
          inStream.Read(numArray, length);
          string assetName = CBinary.m_utf8Encoding.GetString(numArray, 0, (int) length);
          this.m_binary = CApplet.GetInstance().Content.Load<binary>(assetName);
          if (this.m_binary != null)
          {
            this.m_size = (uint) this.m_binary.m_data.Length;
            flag = true;
          }
        }
      }
      else if (length > 0U)
      {
        if (this.m_binary == null)
          this.m_binary = new binary();
        this.m_binary.m_data = new byte[(int) length];
        inStream.Read(this.m_binary.m_data, length);
        if (!inStream.GetFail())
        {
          this.m_size = length;
          this.m_mimeKey = mimeKey;
          flag = true;
        }
      }
      if (!flag)
        this.Destroy();
      return flag;
    }

    public byte[] GetData() => this.m_binary.m_data;

    public uint GetSize() => this.m_size;

    public uint GetMimeKey() => this.m_mimeKey;
  }
}
