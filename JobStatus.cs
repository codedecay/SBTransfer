// Type: SBTransfer.JobStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public enum JobStatus : short
  {
    [UILabels("Oprettet")] Created,
    [UILabels("Verificeret på kilde")] VerifiedAtSource,
    [UILabels("Verificeringsfejl ved kilde")] SourceError,
    [UILabels("MD5-fejl ved kilde")] SourceMD5Error,
    [UILabels("Overført")] Transferred,
    [UILabels("Allerede overført")] AlreadyTransferred,
    [UILabels("Overførselsfejl")] TransferError,
    [UILabels("Verificeret på destination")] VerifiedAtDestination,
    [UILabels("Verificeringsfejl ved destination")] DestinationError,
    [UILabels("MD5-fejl ved destination")] DestinationMD5Error,
    [UILabels("Godkendt")] Approved,
    [UILabels("Godkendelsesfejl")] ApprovalError,
    [UILabels("Godkendt med kommentarer")] ApprovedWithComments,
    [UILabels("Afsluttet med verificeringsfejl ved kilde")] FinishedWithSourceError,
    [UILabels("Afsluttet med MD5-fejl ved kilde")] FinishedWithSourceMD5Error,
    [UILabels("Afsluttet med overførselsfejl")] FinishedWithTransferError,
    [UILabels("Afsluttet med verificeringsfejl på destination")] FinishedWithDestinationError,
    [UILabels("Afsluttet med MD5-fejl på destinationen")] FinishedWithDestinationMD5Error,
    [UILabels("Afsluttet med godkendelsesfejl")] FinishedWithApprovalError,
  }
}
