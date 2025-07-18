// Decompiled with JetBrains decompiler
// Type: com.glu.game.DataInputStream
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  public class DataInputStream : CArrayInputStream
  {
    private string m_resourceId;

    public DataInputStream(string resourceId)
    {
      this.m_resourceId = resourceId;
      CResource resource1 = (CResource) null;
      int resource2 = (int) CApp.GetResourceManager().CreateResource(resourceId, out resource1);
      if (resource1 == null)
      {
        this.m_fail = true;
      }
      else
      {
        CBinary data = (CBinary) resource1.GetData();
        this.Open(data.GetData(), data.GetSize());
      }
    }

    public void ReadUtfIntoCString(byte[] array)
    {
      bool endian = this.GetEndian();
      this.SetEndian(true);
      int n = (int) this.ReadUInt16();
      array = new byte[n + 1];
      this.Read(array, (uint) n);
      array[n] = (byte) 0;
      this.SetEndian(endian);
    }
  }
}
