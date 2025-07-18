// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CMath
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

#nullable disable
namespace com.glu.shared
{
  public sealed class CMath
  {
    private static readonly uint[] kSqrtTable = new uint[256]
    {
      0U,
      16U,
      22U,
      27U,
      32U,
      35U,
      39U,
      42U,
      45U,
      48U,
      50U,
      53U,
      55U,
      57U,
      59U,
      61U,
      64U,
      65U,
      67U,
      69U,
      71U,
      73U,
      75U,
      76U,
      78U,
      80U,
      81U,
      83U,
      84U,
      86U,
      87U,
      89U,
      90U,
      91U,
      93U,
      94U,
      96U,
      97U,
      98U,
      99U,
      101U,
      102U,
      103U,
      104U,
      106U,
      107U,
      108U,
      109U,
      110U,
      112U,
      113U,
      114U,
      115U,
      116U,
      117U,
      118U,
      119U,
      120U,
      121U,
      122U,
      123U,
      124U,
      125U,
      126U,
      128U,
      128U,
      129U,
      130U,
      131U,
      132U,
      133U,
      134U,
      135U,
      136U,
      137U,
      138U,
      139U,
      140U,
      141U,
      142U,
      143U,
      144U,
      144U,
      145U,
      146U,
      147U,
      148U,
      149U,
      150U,
      150U,
      151U,
      152U,
      153U,
      154U,
      155U,
      155U,
      156U,
      157U,
      158U,
      159U,
      160U,
      160U,
      161U,
      162U,
      163U,
      163U,
      164U,
      165U,
      166U,
      167U,
      167U,
      168U,
      169U,
      170U,
      170U,
      171U,
      172U,
      173U,
      173U,
      174U,
      175U,
      176U,
      176U,
      177U,
      178U,
      178U,
      179U,
      180U,
      181U,
      181U,
      182U,
      183U,
      183U,
      184U,
      185U,
      185U,
      186U,
      187U,
      187U,
      188U,
      189U,
      189U,
      190U,
      191U,
      192U,
      192U,
      193U,
      193U,
      194U,
      195U,
      195U,
      196U,
      197U,
      197U,
      198U,
      199U,
      199U,
      200U,
      201U,
      201U,
      202U,
      203U,
      203U,
      204U,
      204U,
      205U,
      206U,
      206U,
      207U,
      208U,
      208U,
      209U,
      209U,
      210U,
      211U,
      211U,
      212U,
      212U,
      213U,
      214U,
      214U,
      215U,
      215U,
      216U,
      217U,
      217U,
      218U,
      218U,
      219U,
      219U,
      220U,
      221U,
      221U,
      222U,
      222U,
      223U,
      224U,
      224U,
      225U,
      225U,
      226U,
      226U,
      227U,
      227U,
      228U,
      229U,
      229U,
      230U,
      230U,
      231U,
      231U,
      232U,
      232U,
      233U,
      234U,
      234U,
      235U,
      235U,
      236U,
      236U,
      237U,
      237U,
      238U,
      238U,
      239U,
      240U,
      240U,
      241U,
      241U,
      242U,
      242U,
      243U,
      243U,
      244U,
      244U,
      245U,
      245U,
      246U,
      246U,
      247U,
      247U,
      248U,
      248U,
      249U,
      249U,
      250U,
      250U,
      251U,
      251U,
      252U,
      252U,
      253U,
      253U,
      254U,
      254U,
      (uint) byte.MaxValue
    };

    public static int Abs(int val) => val <= 0 ? -val : val;

    public static int Max(int val1, int val2) => val1 <= val2 ? val2 : val1;

    public static int Min(int val1, int val2) => val1 >= val2 ? val2 : val1;

    public static uint Max(uint val1, uint val2) => val1 <= val2 ? val2 : val1;

    public static uint Min(uint val1, uint val2) => val1 >= val2 ? val2 : val1;

    public static int Sqrt(int val)
    {
      if (val < 0)
        return 0;
      uint num1;
      if (val >= 65536)
      {
        uint num2;
        if (val >= 16777216)
        {
          uint num3;
          if (val >= 268435456)
          {
            if (val >= 1073741824)
            {
              if ((ulong) (uint) val >= 4294836225UL)
                return (int) ushort.MaxValue;
              num3 = CMath.kSqrtTable[val >> 24] << 8;
            }
            else
              num3 = CMath.kSqrtTable[val >> 22] << 7;
          }
          else
            num3 = val < 67108864 ? CMath.kSqrtTable[val >> 18] << 5 : CMath.kSqrtTable[val >> 20] << 6;
          num2 = (uint) ((ulong) (num3 + 1U) + (ulong) val / (ulong) num3) >> 1;
        }
        else
          num2 = val < 1048576 ? (val < 262144 ? CMath.kSqrtTable[val >> 10] << 1 : CMath.kSqrtTable[val >> 12] << 2) : (val < 4194304 ? CMath.kSqrtTable[val >> 14] << 3 : CMath.kSqrtTable[val >> 16] << 4);
        num1 = (uint) ((ulong) (num2 + 1U) + (ulong) val / (ulong) num2) >> 1;
      }
      else
      {
        if (val < 256)
          return (int) CMath.kSqrtTable[val] >> 4;
        num1 = val < 4096 ? (val < 1024 ? (CMath.kSqrtTable[val >> 2] >> 3) + 1U : (CMath.kSqrtTable[val >> 4] >> 2) + 1U) : (val < 16384 ? (CMath.kSqrtTable[val >> 6] >> 1) + 1U : CMath.kSqrtTable[val >> 8] + 1U);
      }
      if (num1 * num1 > (uint) val)
        --num1;
      return (int) num1;
    }
  }
}
