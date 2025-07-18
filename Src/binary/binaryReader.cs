// Decompiled with JetBrains decompiler
// Type: com.glu.binary_content.binaryReader
// Assembly: binary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FA9B585F-69DA-4AC5-A834-A181579706FE
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\binary.dll

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace com.glu.binary_content
{
  public class binaryReader : ContentTypeReader<binary>
  {
    protected override binary Read(ContentReader input, binary existingInstance)
    {
      existingInstance = new binary();
      existingInstance.m_data = new byte[input.BaseStream.Length - input.BaseStream.Position];
      input.Read(existingInstance.m_data, 0, (int) (input.BaseStream.Length - input.BaseStream.Position));
      return existingInstance;
    }
  }
}
