// Decompiled with JetBrains decompiler
// Type: com.glu.shared.Color
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class Color
  {
    public static readonly string[] FormatStr = new string[26]
    {
      "Unknown",
      "r4g4b4",
      "r5g6b5",
      "b5g6r5",
      "a1r5g5b5",
      "r5g5b5a1",
      "a4r4g4b4",
      "r4g4b4a4",
      "x14r6g6b6",
      "r8g8b8",
      "b8g8r8",
      "x8r8g8b8",
      "a8r8g8b8",
      "b8g8r8a8",
      "r8g8b8a8",
      "a8b8g8r8",
      "argb_fixed",
      "rgba_fixed",
      "l8",
      "a8",
      "i8",
      "al8",
      "p16x8r8g8b8",
      "p16a8r8g8b8",
      "p256a8r8g8b8",
      "p256x8r8g8b8"
    };

    public static string GetFormatStr(Color.Format format)
    {
      return Color.FormatStr[(int) (uint) format];
    }

    public static Color.Format GetFormat(string formatStr)
    {
      switch (formatStr)
      {
        case "r4g4b4":
          return Color.Format.R4G4B4A4;
        case "r5g6b5":
          return Color.Format.R5G6B5;
        case "b5g6r5":
          return Color.Format.B5G6R5;
        case "a1r5g5b5":
          return Color.Format.A1R5G5B5;
        case "r5g5b5a1":
          return Color.Format.R5G5B5A1;
        case "a4r4g4b4":
          return Color.Format.A4R4G4B4;
        case "r4g4b4a4":
          return Color.Format.R4G4B4A4;
        case "x14r6g6b6":
          return Color.Format.X14R6G6B6;
        case "r8g8b8":
          return Color.Format.R8G8B8;
        case "b8g8r8":
          return Color.Format.B8G8R8;
        case "x8r8g8b8":
          return Color.Format.X8R8G8B8;
        case "a8r8g8b8":
          return Color.Format.A8R8G8B8;
        case "b8g8r8a8":
          return Color.Format.B8G8R8A8;
        case "r8g8b8a8":
          return Color.Format.R8G8B8A8;
        case "a8b8g8r8":
          return Color.Format.A8B8G8R8;
        case "argb_fixed":
          return Color.Format.ARGB_fixed;
        case "rgba_fixed":
          return Color.Format.RGBA_fixed;
        case "l8":
          return Color.Format.L8;
        case "a8":
          return Color.Format.A8;
        case "i8":
          return Color.Format.I8;
        case "al8":
          return Color.Format.AL8;
        case "p16x8r8g8b8":
          return Color.Format.P16X8R8G8B8;
        case "p16a8r8g8b8":
          return Color.Format.P16A8R8G8B8;
        case "p256a8r8g8b8":
          return Color.Format.P256A8R8G8B8;
        case "p256x8r8g8b8":
          return Color.Format.P256X8R8G8B8;
        default:
          return Color.Format.Unknown;
      }
    }

    public enum Format
    {
      Unknown,
      R4G4B4,
      R5G6B5,
      B5G6R5,
      A1R5G5B5,
      R5G5B5A1,
      A4R4G4B4,
      R4G4B4A4,
      X14R6G6B6,
      R8G8B8,
      B8G8R8,
      X8R8G8B8,
      A8R8G8B8,
      B8G8R8A8,
      R8G8B8A8,
      A8B8G8R8,
      ARGB_fixed,
      RGBA_fixed,
      L8,
      A8,
      I8,
      AL8,
      P16X8R8G8B8,
      P16A8R8G8B8,
      P256X8R8G8B8,
      P256A8R8G8B8,
    }

    public struct A8R8G8B8_t
    {
      public static readonly uint Black = Color.A8R8G8B8_t.Make(byte.MaxValue, (byte) 0, (byte) 0, (byte) 0);
      public static readonly uint Blue = Color.A8R8G8B8_t.Make(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
      public static readonly uint Cyan = Color.A8R8G8B8_t.Make(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
      public static readonly uint Green = Color.A8R8G8B8_t.Make(byte.MaxValue, (byte) 0, byte.MaxValue, (byte) 0);
      public static readonly uint Magenta = Color.A8R8G8B8_t.Make(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue);
      public static readonly uint Red = Color.A8R8G8B8_t.Make(byte.MaxValue, byte.MaxValue, (byte) 0, (byte) 0);
      public static readonly uint Yellow = Color.A8R8G8B8_t.Make(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 0);
      public static readonly uint White = Color.A8R8G8B8_t.Make(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
      private byte m_b;
      private byte m_g;
      private byte m_r;
      private byte m_a;

      public static void SetAlpha(ref uint argb, byte a)
      {
        argb = (uint) ((int) argb & 16777215 | (int) a << 24);
      }

      public static void SetRed(ref uint argb, byte r)
      {
        argb = (uint) ((int) argb & -16711681 | (int) r << 16);
      }

      public static void SetGreen(ref uint argb, byte g)
      {
        argb = (uint) ((int) argb & -65281 | (int) g << 8);
      }

      public static void SetBlue(ref uint argb, byte b) => argb = argb & 4294967040U | (uint) b;

      public static byte GetAlpha(uint argb) => (byte) (argb >> 24);

      public static byte GetRed(uint argb) => (byte) (argb >> 16);

      public static byte GetGreen(uint argb) => (byte) (argb >> 8);

      public static byte GetBlue(uint argb) => (byte) argb;

      public static uint Make(byte r, byte g, byte b)
      {
        return (uint) ((int) b | (int) g << 8 | (int) r << 16 | -16777216);
      }

      public static uint Make(byte a, byte r, byte g, byte b)
      {
        return (uint) ((int) b | (int) g << 8 | (int) r << 16 | (int) a << 24);
      }

      private uint Make()
      {
        return (uint) ((int) this.m_b | (int) this.m_g << 8 | (int) this.m_r << 16 | (int) this.m_a << 24);
      }

      public void Set(byte r, byte g, byte b)
      {
        this.m_a = byte.MaxValue;
        this.m_r = r;
        this.m_g = g;
        this.m_b = b;
      }

      public void Set(byte a, byte r, byte g, byte b)
      {
        this.m_a = a;
        this.m_r = r;
        this.m_g = g;
        this.m_b = b;
      }

      private void Set(uint argb)
      {
        this.m_a = (byte) (argb >> 24);
        this.m_r = (byte) (argb >> 16);
        this.m_g = (byte) (argb >> 8);
        this.m_b = (byte) argb;
      }
    }

    public struct ARGB_fixed
    {
      public int m_a;
      public int m_r;
      public int m_g;
      public int m_b;

      public ARGB_fixed(int a, int r, int g, int b)
      {
        this.m_a = a;
        this.m_r = r;
        this.m_g = g;
        this.m_b = b;
      }

      public ARGB_fixed(Color.ARGB_fixed argb)
      {
        this.m_a = argb.m_a;
        this.m_r = argb.m_r;
        this.m_g = argb.m_g;
        this.m_b = argb.m_b;
      }

      public void Set(byte a, byte r, byte g, byte b)
      {
        this.m_a = (int) a << 8 | (int) a;
        this.m_r = (int) r << 8 | (int) r;
        this.m_g = (int) g << 8 | (int) g;
        this.m_b = (int) b << 8 | (int) b;
      }

      public void Set(Color.ARGB_fixed argb)
      {
        this.m_a = argb.m_a;
        this.m_r = argb.m_r;
        this.m_g = argb.m_g;
        this.m_b = argb.m_b;
      }

      public void SetFixed(int a, int r, int g, int b)
      {
        this.m_a = a;
        this.m_r = r;
        this.m_g = g;
        this.m_b = b;
      }

      public byte GetAlpha()
      {
        int num = this.m_a >> 8;
        if (num < 0)
          return 0;
        return num <= (int) byte.MaxValue ? (byte) num : byte.MaxValue;
      }

      public byte GetRed()
      {
        int num = this.m_r >> 8;
        if (num < 0)
          return 0;
        return num <= (int) byte.MaxValue ? (byte) num : byte.MaxValue;
      }

      public byte GetGreen()
      {
        int num = this.m_g >> 8;
        if (num < 0)
          return 0;
        return num <= (int) byte.MaxValue ? (byte) num : byte.MaxValue;
      }

      public byte GetBlue()
      {
        int num = this.m_b >> 8;
        if (num < 0)
          return 0;
        return num <= (int) byte.MaxValue ? (byte) num : byte.MaxValue;
      }

      public void Make(Color.Format format, object color)
      {
        switch (format)
        {
          case Color.Format.A8R8G8B8:
            this.Set(Color.A8R8G8B8_t.GetAlpha((uint) color), Color.A8R8G8B8_t.GetRed((uint) color), Color.A8R8G8B8_t.GetGreen((uint) color), Color.A8R8G8B8_t.GetBlue((uint) color));
            break;
          case Color.Format.ARGB_fixed:
            this.Set((Color.ARGB_fixed) color);
            break;
        }
      }
    }

    public static class ARGB_fixed_t
    {
      public static readonly Color.ARGB_fixed Black = new Color.ARGB_fixed(65536, 0, 0, 0);
      public static readonly Color.ARGB_fixed Blue = new Color.ARGB_fixed(65536, 0, 0, 65536);
      public static readonly Color.ARGB_fixed Cyan = new Color.ARGB_fixed(65536, 0, 65536, 65536);
      public static readonly Color.ARGB_fixed Green = new Color.ARGB_fixed(65536, 0, 65536, 0);
      public static readonly Color.ARGB_fixed Magenta = new Color.ARGB_fixed(65536, 65536, 0, 65536);
      public static readonly Color.ARGB_fixed Red = new Color.ARGB_fixed(65536, 65536, 0, 0);
      public static readonly Color.ARGB_fixed Yellow = new Color.ARGB_fixed(65536, 65536, 65536, 0);
      public static readonly Color.ARGB_fixed White = new Color.ARGB_fixed(65536, 65536, 65536, 65536);

      public static Color.ARGB_fixed GetBlack() => Color.ARGB_fixed_t.Black;

      public static Color.ARGB_fixed GetBlue() => Color.ARGB_fixed_t.Blue;

      public static Color.ARGB_fixed GetCyan() => Color.ARGB_fixed_t.Cyan;

      public static Color.ARGB_fixed GetGreen() => Color.ARGB_fixed_t.Green;

      public static Color.ARGB_fixed GetMagenta() => Color.ARGB_fixed_t.Magenta;

      public static Color.ARGB_fixed GetRed() => Color.ARGB_fixed_t.Red;

      public static Color.ARGB_fixed GetYellow() => Color.ARGB_fixed_t.Yellow;

      public static Color.ARGB_fixed GetWhite() => Color.ARGB_fixed_t.White;
    }
  }
}
