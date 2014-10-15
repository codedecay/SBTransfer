// Type: SBTransfer.SessionStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public enum SessionStatus : short
  {
    [UILabels("Afventer ")] WaitingForStorageAndProviders,
    [UILabels("Klar til indlæsning af jobs")] StorageAndProvidersReady,
    [UILabels("Indlæser og verificerer jobs")] PreparingJobs,
    [UILabels("Klar til overførsel")] JobsReady,
    [UILabels("Initialiseringsfejl")] JobsNotReady,
    [UILabels("Overførsel startet")] Started,
    [UILabels("Stoppet af bruger")] UpdatingStoppedByUser,
    [UILabels("Stoppet af bruger")] PreparingJobsStoppedByUser,
    [UILabels("Stoppet af bruger")] RunningStoppedByUser,
    [UILabels("Afsluttet")] FinishedSuccesfully,
    [UILabels("Afsluttet med fejl")] FinishedWithError,
    [UILabels("Afsluttet med kommentarer, se log")] FinishedWithWarning,
  }
}
