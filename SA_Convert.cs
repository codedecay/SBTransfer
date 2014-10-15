// Type: SBTransfer.SA_Convert
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;

namespace SBTransfer
{
  internal static class SA_Convert
  {
    private static string[][] prefixes = new string[4][]
    {
      new string[7]
      {
        "",
        "Ki",
        "Mi",
        "Gi",
        "Ti",
        "Pi",
        "Ei"
      },
      new string[7]
      {
        "",
        "kibi",
        "mebi",
        "gibi",
        "tebi",
        "pebi",
        "exbi"
      },
      new string[7]
      {
        "",
        "k",
        "M",
        "G",
        "T",
        "P",
        "E"
      },
      new string[7]
      {
        "",
        "kilo",
        "mega",
        "giga",
        "tera",
        "Peta",
        "exa"
      }
    };
    private static string[][] suffixes = new string[2][]
    {
      new string[2]
      {
        "b",
        "Bit"
      },
      new string[2]
      {
        "B",
        "Byte"
      }
    };
    private static long[][] prefixvalues = new long[7][]
    {
      new long[2]
      {
        1L,
        1L
      },
      new long[2]
      {
        1024L,
        1000L
      },
      new long[2]
      {
        1048576L,
        1000000L
      },
      new long[2]
      {
        1073741824L,
        1000000000L
      },
      new long[2]
      {
        1099511627776L,
        1000000000000L
      },
      new long[2]
      {
        1125899906842624L,
        1000000000000000L
      },
      new long[2]
      {
        1152921504606846976L,
        1000000000000000000L
      }
    };
    private static long[] suffixvalues = new long[2]
    {
      8L,
      1L
    };

    static SA_Convert()
    {
    }

    public static string BytesToPrefixedValue(long Size, PrefixType PType, OutputForm PForm, SuffixType SType, OutputForm SForm)
    {
      for (int index = 0; index <= SA_Convert.prefixvalues.GetLength(0); ++index)
      {
        if (Size < SA_Convert.prefixvalues[index + 1][(int) PType])
          return string.Concat(new object[4]
          {
            (object) ((Decimal) Size / (Decimal) SA_Convert.prefixvalues[index][(int) PType] * (Decimal) SA_Convert.suffixvalues[(int) SType]).ToString("#.##"),
            (object) ' ',
            (object) SA_Convert.prefixes[(int) (PType + (int) PForm)][index],
            (object) SA_Convert.suffixes[(int) SType][(int) SForm]
          });
      }
      return "";
    }

    public static string BytesToPrefixedValue(long Size)
    {
      return SA_Convert.BytesToPrefixedValue(Size, PrefixType.Binary, OutputForm.Short, SuffixType.Byte, OutputForm.Short);
    }
  }
}
