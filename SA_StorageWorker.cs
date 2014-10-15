// Type: SBTransfer.SA_StorageWorker
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System.ComponentModel;

namespace SBTransfer
{
  public class SA_StorageWorker : BackgroundWorker
  {
    public Job job;

    public SA_StorageWorker(Job job)
    {
      this.job = job;
      this.WorkerReportsProgress = true;
      this.WorkerSupportsCancellation = true;
    }
  }
}
