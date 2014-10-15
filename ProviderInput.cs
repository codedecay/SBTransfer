// Type: SBTransfer.ProviderInput
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public enum ProviderInput
  {
    [UILabels("Filnavn")] Filnavn,
    [UILabels("Filnavn + MD5")] MD5,
    [UILabels("Filnavn + Filstørrelse")] FileSize,
    [UILabels("Filnavn + Filstørrelse + MD5")] FileSizeAndMD5,
  }
}
