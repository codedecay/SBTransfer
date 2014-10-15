// Type: SBTransfer.StorageStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public enum StorageStatus : short
  {
    [UILabels("Fejl")] Error,
    [UILabels("Ikke tilgængelig")] Unavailable,
    [UILabels("Til rådighed")] Available,
    [UILabels("Klar")] Ready,
    [UILabels("I brug")] Locked,
  }
}
