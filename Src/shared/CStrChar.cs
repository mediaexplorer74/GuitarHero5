// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CStrChar
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;
using System.Text;

#nullable disable
namespace com.glu.shared
{
  public class CStrChar : CClass
  {
    public const uint ClassId = 1681284718;
    private string m_str;
    private static UTF8Encoding m_utf8Encoding = new UTF8Encoding();

    public CStrChar()
      : base(1681284718U)
    {
      this.m_str = (string) null;
    }

    public CStrChar(char[] str)
      : base(1681284718U)
    {
      this.m_str = new string(str);
    }

    public CStrChar(string str)
      : base(1681284718U)
    {
      this.m_str = str;
    }

    public CStrChar(CStrChar str)
      : base(1681284718U)
    {
      this.m_str = str.m_str;
    }

    public bool Load(CInputStream inStream, uint mimeKey)
    {
      bool flag = false;
      this.m_str = (string) null;
      uint length = inStream.Available();
      if (length > 0U)
      {
        byte[] numArray = new byte[(int) length];
        inStream.Read(numArray, length);
        this.m_str = CStrChar.m_utf8Encoding.GetString(numArray, 0, (int) length);
        if (!inStream.GetFail())
          flag = true;
      }
      return flag;
    }

    public char[] ToCharArray() => this.m_str != null ? this.m_str.ToCharArray() : (char[]) null;

    public new string ToString() => this.m_str;

    public int GetLength() => this.m_str != null ? this.m_str.Length : 0;

    public CStrChar Trim()
    {
      return this.m_str != null ? new CStrChar(this.m_str.Trim()) : (CStrChar) null;
    }

    public int Find(string stringToFind, int firstIndex)
    {
      return this.m_str != null ? this.m_str.IndexOf(stringToFind, firstIndex) : -1;
    }

    public int GetCharIndex(int occurrenceCount, char character)
    {
      if (this.m_str == null)
        return -1;
      if (occurrenceCount <= 0)
        return this.m_str.LastIndexOf(character);
      int startIndex = 0;
      int charIndex;
      while (true)
      {
        charIndex = this.m_str.IndexOf(character, startIndex);
        --occurrenceCount;
        if (occurrenceCount > 0)
        {
          if (charIndex >= 0 && charIndex < this.m_str.Length - 2)
            startIndex = charIndex + 1;
          else
            break;
        }
        else
          goto label_7;
      }
      return -1;
label_7:
      return charIndex;
    }

    public CStrChar GetSubString(int firstIndex, int lastIndex)
    {
      if (this.m_str == null)
        return (CStrChar) null;
      return firstIndex < lastIndex ? new CStrChar(this.m_str.Substring(firstIndex, lastIndex - firstIndex)) : (CStrChar) null;
    }

    public char this[int idx] => this.m_str[idx];
  }
}
