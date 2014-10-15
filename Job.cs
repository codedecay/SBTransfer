// Type: SBTransfer.Job
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;

namespace SBTransfer
{
  public class Job : SA_LoggedDataObject
  {
    public bool busy = false;
    private bool sleeping;
    private string md5;
    private string path;
    private string uniqueID;
    private bool sourceMD5Verified;
    private int sourceVerificationAttempts;
    private int sourceMD5Attempts;
    private int transferAttempts;
    private int destinationVerificationAttempts;
    private int destinationMD5Attempts;
    private int approvalAttempts;
    private int maxTransferAttempts;
    private int maxApprovalAttempts;
    private int gracePeriod;
    private int gracePeriodMD5;
    private bool forceMD5Check;
    private SA_StorageWorker bw_CurrentWorker;
    public Storage source;
    public Storage destination;

    [UILabels("Navn", "", "Indstillinger")]
    public override string Name
    {
      get
      {
        return this.name;
      }
    }

    [UILabels("Filnavn", "", "Indstillinger")]
    public string FileName
    {
      get
      {
        return this.path;
      }
    }

    [UILabels("Max antal verificeringsforsøg ved kilde", "", "Indstillinger")]
    public int MaxVerificationAttemptsSource
    {
      get
      {
        return this.source.MaxVerificationAttempts;
      }
    }

    [UILabels("Max antal godkendelsesforsøg", "", "Indstillinger")]
    public int MaxApprovalAttempts
    {
      get
      {
        return this.maxApprovalAttempts;
      }
      set
      {
        this.maxApprovalAttempts = value;
      }
    }

    [UILabels("Max antal overførselsforsøg", "", "Indstillinger")]
    public int MaxTransferAttempts
    {
      get
      {
        return this.maxTransferAttempts;
      }
      set
      {
        this.maxTransferAttempts = value;
      }
    }

    [UILabels("Max antal verificeringsforsøg af MD5 ved kilde", "", "Indstillinger")]
    public int MaxMD5VerificationAttemptsSource
    {
      get
      {
        return this.source.MaxMD5VerificationAttempts;
      }
    }

    [UILabels("Max antal verificeringsforsøg ved destination", "", "Indstillinger")]
    public int MaxVerificationAttemptsDestination
    {
      get
      {
        return this.destination.MaxVerificationAttempts;
      }
    }

    [UILabels("Max antal verificeringsforsøg  af MD5 ved destination", "", "Indstillinger")]
    public int MaxMD5VerificationAttemptsDestination
    {
      get
      {
        return this.destination.MaxMD5VerificationAttempts;
      }
    }

    [UILabels("Ventetid mellem forsøg ved MD5 fejl", "Tid i sekunder mellem gentagne forsøg ved MD5 fejl) ", "Indstillinger")]
    public int GracePeriodMD5
    {
      get
      {
        return this.gracePeriodMD5;
      }
      set
      {
        this.gracePeriodMD5 = value;
      }
    }

    [UILabels("Ventetid mellem forsøg ved andre fejl end MD5", "Tid i sekunder mellem gentagne forsøg ved fejl (for alle fejl pånær MD5 fejl) ", "Indstillinger")]
    public int GracePeriod
    {
      get
      {
        return this.gracePeriod;
      }
      set
      {
        this.gracePeriod = value;
      }
    }

    [UILabels("Tvunget MD5check ved kilde", "Gennemtving MD5-check ved kilde i tilfælde af MD5-fejl ved destination ", "Indstillinger")]
    public bool ForceMD5check
    {
      get
      {
        return this.forceMD5Check;
      }
    }

    [UILabels("FilStørrelse", "", "Status")]
    public string FileSize
    {
      get
      {
        return SA_Convert.BytesToPrefixedValue(this.Status.TargetQuantity);
      }
    }

    [UILabels("Verificeringsforsøg ved kilde", "", "Status")]
    public int SourceVerificationAttempts
    {
      get
      {
        return this.sourceVerificationAttempts;
      }
      set
      {
        this.sourceVerificationAttempts = value;
      }
    }

    [UILabels("Verificeringsforsøg ved kilde med MD5", "", "Status")]
    public int SourceMD5Attempts
    {
      get
      {
        return this.sourceMD5Attempts;
      }
    }

    [UILabels("Overførselsforsøg", "", "Status")]
    public int TransferAttempts
    {
      get
      {
        return this.transferAttempts;
      }
    }

    [UILabels("Verificeringsforsøg ved destination", "", "Status")]
    public int DestinationVerificationAttempts
    {
      get
      {
        return this.destinationVerificationAttempts;
      }
    }

    [UILabels("Verificeringsforsøg ved destination med MD5", "", "Status")]
    public int DestinationMD5Attempts
    {
      get
      {
        return this.destinationMD5Attempts;
      }
    }

    [UILabels("Godkendelsesforsøg", "", "Status")]
    public int ApprovalAttempts
    {
      get
      {
        return this.approvalAttempts;
      }
    }

    [UILabels("Oprindelig MD5", "", "Status")]
    public string MD5
    {
      get
      {
        return this.md5.ToLower();
      }
    }

    [UILabels("MD5 fra sidste verificering", "", "Status")]
    public string MD5LastCalculated
    {
      get
      {
        return this.Status.CalculatedMD5;
      }
    }

    public bool Sleeping
    {
      get
      {
        return this.sleeping;
      }
    }

    public string UniqueID
    {
      get
      {
        return this.uniqueID; 
      }
    }

    public SA_JobStatus Status
    {
      get
      {
        return (SA_JobStatus) this.status;
      }
      set
      {
        this.Status.CalculatedMD5 = value.CalculatedMD5;
        this.Status.TargetQuantity = value.TargetQuantity;
        this.Status.CurrentQuantity = value.CurrentQuantity;
        base.Status = (SA_ProgressStatus) value;
      }
    }

    public event EventHandler SourceVerificationAttemptsChanged;

    public event EventHandler SourceMD5AttemptsChanged;

    public event EventHandler TransferAttemptsChanged;

    public event EventHandler DestinationVerificationAttemptsChanged;

    public event EventHandler DestinationMD5AttemptsChanged;

    public event EventHandler ApprovalAttemptsChanged;

    public event EventHandler MD5Changed;

    public event EventHandler MD5LastCalculatedChanged;

    public Job(string path, Storage source, Storage destination)
    {
      this.path = path;
      this.name = Path.GetFileNameWithoutExtension(path);
      this.source = source;
      this.destination = destination;
      this.status = (SA_Status) new SA_JobStatus();
      this.UIImage = (Image) Resources.Package;
      this.UIBadge = (Image) Resources.BadgePackage;
      this.uniqueID = this.name + (object) this.GetHashCode();
      this.Log = new SA_Log(this.Name);
    }

    public Job(string path, Storage source, Storage destination, long fileSize)
      : this(path, source, destination)
    {
      this.Status.TargetQuantity = fileSize;
    }

    public Job(string path, Storage source, Storage destination, string md5)
      : this(path, source, destination)
    {
      this.md5 = md5;
    }

    public Job(string path, Storage source, Storage destination, string md5, long fileSize)
      : this(path, source, destination)
    {
      this.md5 = md5;
      this.Status.TargetQuantity = fileSize;
    }

    private void reTry(RetryDelegate retryAction)
    {
      if (this.sleeping)
      {
        this.sleeping = false;
        retryAction.DynamicInvoke(new object[0]);
      }
      else
      {
        this.sleeping = true;
        SA_StorageWorker bw_NewWorker = new SA_StorageWorker(this);
        bw_NewWorker.DoWork += new DoWorkEventHandler(this.bw_Sleep_DoWork);
        this.SetCurrentWorker(bw_NewWorker);
      }
    }

    private void SetCurrentWorker(SA_StorageWorker bw_NewWorker)
    {
      this.busy = true;
      if (!this.sleeping)
        this.Log.PrepareEntry();
      if (this.bw_CurrentWorker != null && !this.bw_CurrentWorker.IsBusy)
        this.bw_CurrentWorker.Dispose();
      if (bw_NewWorker == null)
        return;
      this.bw_CurrentWorker = bw_NewWorker;
      this.bw_CurrentWorker.ProgressChanged += new ProgressChangedEventHandler(this.CurrentWorkerProgressChanged);
      this.bw_CurrentWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.CurrentWorkerCompleted);
      this.bw_CurrentWorker.RunWorkerAsync();
    }

    private void verifyAtSource()
    {
      if (this.source.VerifyMD5 || this.ForceMD5check)
      {
        ++this.sourceMD5Attempts;
        this.OnSourceMD5AttemptsChanged(EventArgs.Empty);
      }
      ++this.sourceVerificationAttempts;
      this.OnSourceVerificationAttemptsChanged(EventArgs.Empty);
      this.SetCurrentWorker(this.source.Verify(this));
    }

    private void transfer()
    {
      ++this.transferAttempts;
      this.OnTransferAttemptsChanged(EventArgs.Empty);
      this.SetCurrentWorker(this.source.Transfer(this, this.destination));
    }

    private void verifyAtDestination()
    {
      if (this.destination.VerifyMD5)
      {
        ++this.destinationMD5Attempts;
        this.OnDestinationMD5AttemptsChanged(EventArgs.Empty);
      }
      ++this.destinationVerificationAttempts;
      this.OnDestinationVerificationAttemptsChanged(EventArgs.Empty);
      this.SetCurrentWorker(this.destination.Verify(this));
    }

    private void approve()
    {
      ++this.approvalAttempts;
      this.OnApprovalAttemptsChanged(EventArgs.Empty);
      this.SetCurrentWorker(this.destination.Approve(this));
    }

    private void bw_Sleep_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
      SA_JobStatus saJobStatus = new SA_JobStatus(saStorageWorker.job.Status);
      string verboseStatus = saJobStatus.VerboseStatus;
      for (int percentProgress = this.Status.TerseStatus != JobStatus.SourceMD5Error && this.Status.TerseStatus != JobStatus.DestinationMD5Error ? saStorageWorker.job.GracePeriod : saStorageWorker.job.GracePeriodMD5; percentProgress > 0; --percentProgress)
      {
        saJobStatus.VerboseStatus = string.Concat(new object[4]
        {
          (object) verboseStatus,
          (object) " (Nyt forsøg om ",
          (object) percentProgress,
          (object) " s)"
        });
        saStorageWorker.ReportProgress(percentProgress, (object) saJobStatus);
        Thread.Sleep(1000);
      }
      saJobStatus.VerboseStatus = verboseStatus;
      e.Result = (object) this.Status;
    }

    private void CurrentWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (!(e.UserState is SA_JobStatus))
        return;
      this.Status = e.UserState as SA_JobStatus;
    }

    private void CurrentWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.busy = false;
      SA_JobStatus saJobStatus = e.Result as SA_JobStatus;
      if (saJobStatus.TerseStatus == JobStatus.Approved && (this.sourceVerificationAttempts > 1 || this.sourceMD5Attempts > 1 || (this.transferAttempts > 1 || this.destinationVerificationAttempts > 1) || this.destinationMD5Attempts > 1 || this.approvalAttempts > 1))
        saJobStatus.TerseStatus = JobStatus.ApprovedWithComments;
      this.Status = saJobStatus;
      this.Log.Submit((SA_ProgressStatus) this.Status);
      this.OnMD5LastCalculatedChanged(EventArgs.Empty);
    }

    public void Process()
    {
      switch (this.Status.TerseStatus)
      {
        case JobStatus.Created:
          this.verifyAtSource();
          break;
        case JobStatus.VerifiedAtSource:
          this.transfer();
          break;
        case JobStatus.SourceError:
          if (this.sourceVerificationAttempts < this.MaxVerificationAttemptsSource)
          {
            this.Status.TerseStatus = JobStatus.SourceError;
            this.Status.VerboseStatus = " (" + (object) this.source.Name + ", " + (object) this.sourceVerificationAttempts.ToString() + " forsøg)";
            this.reTry(new RetryDelegate(this.verifyAtSource));
            break;
          }
          else
          {
            this.Status.TerseStatus = JobStatus.FinishedWithSourceError;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (" + this.source.Name + ", " + (object) this.sourceVerificationAttempts.ToString() + " forsøg)";
            status.VerboseStatus = str;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
        case JobStatus.SourceMD5Error:
          if (this.sourceMD5Attempts < this.MaxMD5VerificationAttemptsSource)
          {
            this.Status.TerseStatus = JobStatus.SourceMD5Error;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (" + this.source.Name + ", " +  (object) this.sourceMD5Attempts.ToString() + " forsøg)";
            status.VerboseStatus = str;
            this.reTry(new RetryDelegate(this.verifyAtSource));
            break;
          }
          else
          {
            this.Status.TerseStatus = JobStatus.FinishedWithSourceMD5Error;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (" + this.source.Name + ", " + (object) this.sourceMD5Attempts.ToString() + " forsøg)";
            status.VerboseStatus = str;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
        case JobStatus.Transferred:
          this.verifyAtDestination();
          break;
        case JobStatus.TransferError:
          if (this.transferAttempts < this.maxTransferAttempts)
          {
            this.reTry(new RetryDelegate(this.transfer));
            break;
          }
          else
          {
            this.Status.TerseStatus = JobStatus.FinishedWithTransferError;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (fra " + this.source.Name + " til " + this.destination.Name + ", " + (object) this.transferAttempts.ToString() + " forsøg )";
            status.VerboseStatus = str;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
        case JobStatus.VerifiedAtDestination:
          this.approve();
          break;
        case JobStatus.DestinationError:
          if (this.destinationVerificationAttempts < this.MaxVerificationAttemptsDestination)
          {
            this.reTry(new RetryDelegate(this.verifyAtDestination));
            break;
          }
          else
          {
            this.Status.TerseStatus = JobStatus.FinishedWithDestinationError;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (" + this.destination.Name + ", " + (object) this.MaxMD5VerificationAttemptsDestination.ToString() + " forsøg )";
            status.VerboseStatus = str;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
        case JobStatus.DestinationMD5Error:
          if (!this.sourceMD5Verified)
          {
            this.forceMD5Check = true;
            this.verifyAtSource();
            this.sourceMD5Verified = true;
            break;
          }
          else
          {
            if (this.destinationMD5Attempts < this.MaxMD5VerificationAttemptsDestination)
            {
              this.Status.VerboseStatus = " (" + (object) this.destination.Name + ", " + (object) this.destinationMD5Attempts.ToString() + " forsøg )";
              this.reTry(new RetryDelegate(this.verifyAtDestination));
            }
            else
            {
              this.Status.TerseStatus = JobStatus.FinishedWithDestinationMD5Error;
              SA_JobStatus status = this.Status;
              string str = status.VerboseStatus + (object) " (" + this.destination.Name + ", " + (object) this.MaxMD5VerificationAttemptsDestination.ToString() + " forsøg )";
              status.VerboseStatus = str;
              this.OnStatusChanged(EventArgs.Empty);
            }
            break;
          }
        case JobStatus.ApprovalError:
          if (this.approvalAttempts < this.maxApprovalAttempts)
          {
            this.reTry(new RetryDelegate(this.approve));
            break;
          }
          else
          {
            this.Status.TerseStatus = JobStatus.FinishedWithApprovalError;
            SA_JobStatus status = this.Status;
            string str = status.VerboseStatus + (object) " (" + this.destination.Name + ", " +  (object) this.transferAttempts.ToString() + " forsøg )";
            status.VerboseStatus = str;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
        case JobStatus.FinishedWithSourceError:
        case JobStatus.FinishedWithSourceMD5Error:
        case JobStatus.FinishedWithTransferError:
        case JobStatus.FinishedWithDestinationError:
        case JobStatus.FinishedWithDestinationMD5Error:
        case JobStatus.FinishedWithApprovalError:
          this.Log.Close();
          break;
      }
    }

    public void CleanUp()
    {
      foreach (JobProvider jobProvider in this.source.JobProviders)
        jobProvider.RemoveEntry(this);
      if (!this.source.CanDelete)
        return;
      this.source.Delete(this);
    }

    public void Stop()
    {
      if (this.bw_CurrentWorker == null || !this.bw_CurrentWorker.IsBusy)
        return;
      this.bw_CurrentWorker.CancelAsync();
    }

    protected void OnSourceVerificationAttemptsChanged(EventArgs e)
    {
      if (this.SourceVerificationAttemptsChanged == null)
        return;
      this.SourceVerificationAttemptsChanged((object) this, e);
    }

    protected void OnSourceMD5AttemptsChanged(EventArgs e)
    {
      if (this.SourceMD5AttemptsChanged == null)
        return;
      this.SourceMD5AttemptsChanged((object) this, e);
    }

    protected void OnTransferAttemptsChanged(EventArgs e)
    {
      if (this.TransferAttemptsChanged == null)
        return;
      this.TransferAttemptsChanged((object) this, e);
    }

    protected void OnDestinationVerificationAttemptsChanged(EventArgs e)
    {
      if (this.DestinationVerificationAttemptsChanged == null)
        return;
      this.DestinationVerificationAttemptsChanged((object) this, e);
    }

    protected void OnDestinationMD5AttemptsChanged(EventArgs e)
    {
      if (this.DestinationMD5AttemptsChanged == null)
        return;
      this.DestinationMD5AttemptsChanged((object) this, e);
    }

    protected void OnApprovalAttemptsChanged(EventArgs e)
    {
      if (this.ApprovalAttemptsChanged == null)
        return;
      this.ApprovalAttemptsChanged((object) this, e);
    }

    protected void OnMD5Changed(EventArgs e)
    {
      if (this.MD5Changed == null)
        return;
      this.MD5Changed((object) this, e);
    }

    protected void OnMD5LastCalculatedChanged(EventArgs e)
    {
      if (this.MD5LastCalculatedChanged == null)
        return;
      this.MD5LastCalculatedChanged((object) this, e);
    }
  }
}
