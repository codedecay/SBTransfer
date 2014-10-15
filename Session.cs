// Type: SBTransfer.Session
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SBTransfer
{
  public class Session : SA_LoggedDataObject, IContentCarrier
  {
    private int leadInTime;
    private int leadOutTime;
    private int timeLeft;
    private Timer StorageUpdateTimer;
    private Timer LeadInTimer;
    private Timer LeadOutTimer;
    private SessionType settings;
    private SA_NotificationCollection<Job> jobs;
    private SA_NotificationCollection<Job> activeJobs;
    private SA_NotificationCollection<Job> jobsPendingSourceVerification;
    private SA_NotificationCollection<Job> jobsPendingTransfer;
    private SA_NotificationCollection<Job> jobsPendingDestinationVerification;
    private SA_NotificationCollection<Job> jobsPendingApproval;
    private SA_NotificationCollection<Job> jobsVerifyingAtSource;
    private SA_NotificationCollection<Job> jobsTransferring;
    private SA_NotificationCollection<Job> jobsVerifyingAtDestination;
    private SA_NotificationCollection<Job> jobsApproving;
    private SA_NotificationCollection<Job> jobsFinishedSuccesfully;
    private SA_NotificationCollection<Job> jobsFinishedWithError;
    private SA_NotificationCollection<Job> jobsFinishedWithComments;

    [UILabels("Jobs i alt", "Det samlede antal jobs", "Status")]
    public int Jobs
    {
      get
      {
        return this.jobs.Count;
      }
    }

    [UILabels("Aktive jobs i alt", "Det samlede antal aktive jobs", "Status", "Aktive Jobs")]
    public int ActiveJobs
    {
      get
      {
        return this.activeJobs.Count;
      }
    }

    [UILabels("Aktive verificeringer ved kilde", "", "Status", "Aktive Jobs")]
    public int JobsVerifyingAtSource
    {
      get
      {
        return this.jobsVerifyingAtSource.Count;
      }
    }

    [UILabels("Aktive overførsler", "", "Status", "Aktive Jobs")]
    public int JobsTransferring
    {
      get
      {
        return this.jobsTransferring.Count;
      }
    }

    [UILabels("Aktive verificeringer ved destination", "", "Status", "Aktive Jobs")]
    public int JobsVerifyingAtDestination
    {
      get
      {
        return this.jobsVerifyingAtDestination.Count;
      }
    }

    [UILabels("Aktive godkendelser ved destination", "", "Status", "Aktive Jobs")]
    public int JobsApproving
    {
      get
      {
        return this.jobsApproving.Count;
      }
    }

    [UILabels("Godkendte Jobs uden bemærkninger", "", "Status", "Afsluttede Jobs")]
    public int JobsFinishedSuccesfully
    {
      get
      {
        return this.jobsFinishedSuccesfully.Count;
      }
    }

    [UILabels("Godkendte jobs med bemærkninger", "", "Status", "Afsluttede Jobs")]
    public int JobsFinishedWithComments
    {
      get
      {
        return this.jobsFinishedWithComments.Count;
      }
    }

    [UILabels("Jobs afsluttet med fejl", "", "Status", "Afsluttede Jobs")]
    public int JobsFinishedWithError
    {
      get
      {
        return this.jobsFinishedWithError.Count;
      }
    }

    [UILabels("Godkendte Jobs i alt", "", "Status", "Afsluttede Jobs")]
    public int JobsFinished
    {
      get
      {
        return this.jobsFinishedSuccesfully.Count + this.jobsFinishedWithComments.Count;
      }
    }

    [UILabels("Afsluttede jobs i alt", "", "Status", "Afsluttede Jobs")]
    public int JobsFinishedTotal
    {
      get
      {
        return this.jobsFinishedWithError.Count + this.JobsFinished;
      }
    }

    [UILabels("Afventer verificering ved kilde", "", "Status", "Jobs i kø")]
    public int JobsPendingSourceVerification
    {
      get
      {
        return this.jobsPendingSourceVerification.Count;
      }
    }

    [UILabels("Afventer overførsel", "", "Status", "Jobs i kø")]
    public int JobsPendingTransfer
    {
      get
      {
        return this.jobsPendingTransfer.Count;
      }
    }

    [UILabels("Afventer verificering ved destination", "", "Status", "Jobs i kø")]
    public int JobsPendingDestinationVerification
    {
      get
      {
        return this.jobsPendingDestinationVerification.Count;
      }
    }

    [UILabels("Afventer godkendelse", "", "Status", "Jobs i kø")]
    public int JobsPendingApproval
    {
      get
      {
        return this.jobsPendingApproval.Count;
      }
    }

    private bool CanProcess
    {
      get
      {
        return this.activeJobs.Count < this.Settings.MaxConcurrentBackgroundOperations && (this.Status.TerseStatus == SessionStatus.Started || this.Status.TerseStatus == SessionStatus.JobsReady || this.Status.TerseStatus == SessionStatus.PreparingJobs);
      }
    }

    private bool CanVerifyAtSource
    {
      get
      {
        return this.jobsPendingSourceVerification.Count > 0 && this.Status.TerseStatus == SessionStatus.PreparingJobs && this.jobsVerifyingAtSource.Count < this.settings.MaxConcurrentSourceVerifications;
      }
    }

    private bool CanTransfer
    {
      get
      {
        return this.jobsPendingTransfer.Count > 0 && this.Status.TerseStatus == SessionStatus.Started && this.jobsTransferring.Count < this.settings.MaxConcurrentTransfers;
      }
    }

    private bool CanVerifyAtDestination
    {
      get
      {
        return this.jobsPendingDestinationVerification.Count > 0 && this.Status.TerseStatus == SessionStatus.Started && this.jobsVerifyingAtDestination.Count < this.settings.MaxConcurrentDestinationVerifications;
      }
    }

    private bool CanApprove
    {
      get
      {
        return this.jobsPendingApproval.Count > 0 && this.Status.TerseStatus == SessionStatus.Started && this.jobsVerifyingAtSource.Count < this.settings.MaxConcurrentSourceVerifications;
      }
    }

    public SA_SessionStatus Status
    {
      get
      {
        return (SA_SessionStatus) this.status;
      }
      set
      {
        this.Status.TargetQuantity = value.TargetQuantity;
        this.Status.CurrentQuantity = value.CurrentQuantity;
        base.Status = (SA_ProgressStatus) value;
      }
    }

    public SessionType Settings
    {
      get
      {
        return this.settings;
      }
    }

    public event EventHandler JobsChanged;

    public event EventHandler ActiveJobsChanged;

    public event EventHandler JobsVerifyingAtSourceChanged;

    public event EventHandler JobsTransferringChanged;

    public event EventHandler JobsVerifyingAtDestinationChanged;

    public event EventHandler JobsApprovingChanged;

    public event EventHandler JobsFinishedSuccesfullyChanged;

    public event EventHandler JobsFinishedWithCommentsChanged;

    public event EventHandler JobsFinishedWithErrorChanged;

    public event EventHandler JobsFinishedChanged;

    public event EventHandler JobsFinishedTotalChanged;

    public event EventHandler JobsPendingSourceVerificationChanged;

    public event EventHandler JobsPendingTransferChanged;

    public event EventHandler JobsPendingDestinationVerificationChanged;

    public event EventHandler JobsPendingApprovalChanged;

    public Session(SessionType ST, string tmpLogPath, string finalLogPath)
    {
      this.settings = ST;
      this.name = ST.Name;
      this.Log = new SA_Log(this.Name, tmpLogPath, finalLogPath);
      foreach (Storage storage in this.Settings.Storage)
      {
        storage.StatusChanged += new EventHandler(this.StorageStatusChanged);
        storage.Initialize();
      }
      this.UIImage = (Image) Resources.Session;
      this.UIBadge = (Image) Resources.BadgeSession;
      this.status = (SA_Status) new SA_SessionStatus();
      this.StatusChanged += new EventHandler(this.SessionStatusChanged);
      this.jobs = new SA_NotificationCollection<Job>();
      this.activeJobs = new SA_NotificationCollection<Job>();
      this.jobsVerifyingAtSource = new SA_NotificationCollection<Job>();
      this.jobsTransferring = new SA_NotificationCollection<Job>();
      this.jobsVerifyingAtDestination = new SA_NotificationCollection<Job>();
      this.jobsApproving = new SA_NotificationCollection<Job>();
      this.jobsFinishedSuccesfully = new SA_NotificationCollection<Job>();
      this.jobsFinishedWithError = new SA_NotificationCollection<Job>();
      this.jobsFinishedWithComments = new SA_NotificationCollection<Job>();
      this.jobsPendingSourceVerification = new SA_NotificationCollection<Job>();
      this.jobsPendingTransfer = new SA_NotificationCollection<Job>();
      this.jobsPendingDestinationVerification = new SA_NotificationCollection<Job>();
      this.jobsPendingApproval = new SA_NotificationCollection<Job>();
      this.jobs.ContentChanged += new EventHandler(this.OnJobsChanged);
      this.activeJobs.ContentChanged += new EventHandler(this.OnActiveJobsChanged);
      this.jobsVerifyingAtSource.ContentChanged += new EventHandler(this.OnJobsVerifyingAtSourceChanged);
      this.jobsTransferring.ContentChanged += new EventHandler(this.OnJobsTransferringChanged);
      this.jobsVerifyingAtDestination.ContentChanged += new EventHandler(this.OnJobsVerifyingAtDestinationChanged);
      this.jobsApproving.ContentChanged += new EventHandler(this.OnJobsApprovingChanged);
      this.jobsFinishedSuccesfully.ContentChanged += new EventHandler(this.OnJobsFinishedSuccesfullyChanged);
      this.jobsFinishedWithError.ContentChanged += new EventHandler(this.OnJobsFinishedWithErrorChanged);
      this.jobsFinishedWithComments.ContentChanged += new EventHandler(this.OnJobsFinishedWithCommentsChanged);
      this.jobsPendingSourceVerification.ContentChanged += new EventHandler(this.OnJobsPendingSourceVerificationChanged);
      this.jobsPendingTransfer.ContentChanged += new EventHandler(this.OnJobsPendingTransferChanged);
      this.jobsPendingDestinationVerification.ContentChanged += new EventHandler(this.OnJobsPendingDestinationVerificationChanged);
      this.jobsPendingApproval.ContentChanged += new EventHandler(this.OnJobsPendingApprovalChanged);
      this.Content.Columns.Add(new DataColumn("Key", typeof (string), (string) null, MappingType.Hidden));
      this.Content.Columns.Add(new DataColumn("Job", typeof (Job), (string) null, MappingType.Hidden));
      this.Content.Columns.Add(new DataColumn("FilNavn"));
      this.Content.Columns.Add(new DataColumn("Tilstand"));
      this.Content.Columns.Add(new DataColumn("Status"));
      this.Content.Columns.Add(new DataColumn(" ", typeof (Image)));
      this.Content.PrimaryKey = new DataColumn[1]
      {
        this.Content.Columns[0]
      };
      this.Status.TerseStatus = SessionStatus.WaitingForStorageAndProviders;
      this.leadInTime = 5;
      this.LeadInTimer = new Timer();
      this.LeadInTimer.Tick += new EventHandler(this.LeadInTimer_Tick);
      this.LeadInTimer.Interval = 1000;
      this.leadOutTime = 5;
      this.LeadOutTimer = new Timer();
      this.LeadOutTimer.Tick += new EventHandler(this.LeadOutTimer_Tick);
      this.LeadOutTimer.Interval = 1000;
      this.StorageUpdateTimer = new Timer();
      this.StorageUpdateTimer.Tick += new EventHandler(this.UpdateStorageStatusTimerTick);
      this.StorageUpdateTimer.Interval = 5000;
      this.StorageUpdateTimer.Start();
    }

    private void LeadInTimer_Tick(object sender, EventArgs e)
    {
      if (this.timeLeft > 0)
      {
        this.Status.VerboseStatus = SA_Reflect.GetUIName((object) this.Status.TerseStatus) + (object) " - begynder kørsel om " + this.timeLeft.ToString();
        --this.timeLeft;
      }
      else
      {
        this.LeadInTimer.Stop();
        foreach (Storage storage in this.Settings.Storage)
          storage.Status.TerseStatus = StorageStatus.Locked;
        this.PrepareTransfer();
      }
    }

    private void LeadOutTimer_Tick(object sender, EventArgs e)
    {
      if (this.timeLeft > 0)
      {
        --this.timeLeft;
      }
      else
      {
        this.LeadOutTimer.Stop();
        this.CleanUp();
      }
    }

    protected void OnJobsChanged(object sender, EventArgs e)
    {
      if (this.JobsChanged == null)
        return;
      this.JobsChanged((object) this, e);
    }

    protected void OnActiveJobsChanged(object sender, EventArgs e)
    {
      if (this.ActiveJobsChanged == null)
        return;
      this.ActiveJobsChanged((object) this, e);
    }

    protected void OnJobsVerifyingAtSourceChanged(object sender, EventArgs e)
    {
      if (this.JobsVerifyingAtSourceChanged == null)
        return;
      this.JobsVerifyingAtSourceChanged((object) this, e);
    }

    protected void OnJobsTransferringChanged(object sender, EventArgs e)
    {
      if (this.JobsTransferringChanged == null)
        return;
      this.JobsTransferringChanged((object) this, e);
    }

    protected void OnJobsVerifyingAtDestinationChanged(object sender, EventArgs e)
    {
      if (this.JobsVerifyingAtDestinationChanged == null)
        return;
      this.JobsVerifyingAtDestinationChanged((object) this, e);
    }

    protected void OnJobsApprovingChanged(object sender, EventArgs e)
    {
      if (this.JobsApprovingChanged == null)
        return;
      this.JobsApprovingChanged((object) this, e);
    }

    protected void OnJobsFinishedSuccesfullyChanged(object sender, EventArgs e)
    {
      if (this.JobsFinishedSuccesfullyChanged != null)
        this.JobsFinishedSuccesfullyChanged((object) this, e);
      this.OnJobsFinishedChanged((object) this, EventArgs.Empty);
    }

    protected void OnJobsFinishedWithCommentsChanged(object sender, EventArgs e)
    {
      if (this.JobsFinishedWithCommentsChanged != null)
        this.JobsFinishedWithCommentsChanged((object) this, e);
      this.OnJobsFinishedChanged((object) this, EventArgs.Empty);
    }

    protected void OnJobsFinishedWithErrorChanged(object sender, EventArgs e)
    {
      if (this.JobsFinishedWithErrorChanged != null)
        this.JobsFinishedWithErrorChanged((object) this, e);
      this.OnJobsFinishedTotalChanged((object) this, EventArgs.Empty);
    }

    protected void OnJobsFinishedChanged(object sender, EventArgs e)
    {
      if (this.JobsFinishedChanged != null)
        this.JobsFinishedChanged((object) this, e);
      this.OnJobsFinishedTotalChanged((object) this, EventArgs.Empty);
    }

    protected void OnJobsFinishedTotalChanged(object sender, EventArgs e)
    {
      if (this.JobsFinishedTotalChanged == null)
        return;
      this.JobsFinishedTotalChanged((object) this, e);
    }

    protected void OnJobsPendingSourceVerificationChanged(object sender, EventArgs e)
    {
      if (this.JobsPendingSourceVerificationChanged == null)
        return;
      this.JobsPendingSourceVerificationChanged((object) this, e);
    }

    protected void OnJobsPendingTransferChanged(object sender, EventArgs e)
    {
      if (this.JobsPendingTransferChanged == null)
        return;
      this.JobsPendingTransferChanged((object) this, e);
    }

    protected void OnJobsPendingDestinationVerificationChanged(object sender, EventArgs e)
    {
      if (this.JobsPendingDestinationVerificationChanged == null)
        return;
      this.JobsPendingDestinationVerificationChanged((object) this, e);
    }

    protected void OnJobsPendingApprovalChanged(object sender, EventArgs e)
    {
      if (this.JobsPendingApprovalChanged == null)
        return;
      this.JobsPendingApprovalChanged((object) this, e);
    }

    private void PrepareTransfer()
    {
      if (this.Status.TerseStatus != SessionStatus.StorageAndProvidersReady)
        return;
      this.Status.TerseStatus = SessionStatus.PreparingJobs;
      this.OnStatusChanged(EventArgs.Empty);
      this.jobs.Clear();
      this.Content.Clear();
      foreach (Storage storage in this.Settings.Sources)
      {
        foreach (Storage destination in this.Settings.Destinations)
        {
          foreach (Job job in storage.GetJobList(destination))
            this.jobs.Add(job);
        }
      }
      foreach (Job j in (Collection<Job>) this.jobs)
      {
        j.Log.LogChanged += new EventHandler(this.Log.SubLogChanged);
        j.GracePeriod = this.Settings.GracePeriod;
        j.GracePeriodMD5 = this.Settings.GracePeriodMD5;
        j.MaxApprovalAttempts = this.Settings.MaxApprovalAttempts;
        j.MaxTransferAttempts = this.Settings.MaxTransferAttempts;
        j.StatusChanged += new EventHandler(this.JobStatusChanged);
        this.UpdateContent(j);
        this.jobsPendingSourceVerification.Submit(j);
      }
      this.ProcessJobs();
    }

    public void Run()
    {
      if (this.Status.TerseStatus == SessionStatus.WaitingForStorageAndProviders || this.Status.TerseStatus == SessionStatus.UpdatingStoppedByUser)
      {
        this.Status.TerseStatus = SessionStatus.WaitingForStorageAndProviders;
        this.UpdateStorageStatus();
        this.StorageUpdateTimer.Start();
      }
      if (this.Status.TerseStatus == SessionStatus.JobsReady || this.Status.TerseStatus == SessionStatus.PreparingJobsStoppedByUser)
      {
        this.Status.TerseStatus = SessionStatus.PreparingJobs;
        this.ProcessJobs();
      }
      if (this.Status.TerseStatus != SessionStatus.RunningStoppedByUser && this.Status.TerseStatus != SessionStatus.JobsReady)
        return;
      this.Status.TerseStatus = SessionStatus.Started;
      this.ProcessJobs();
    }

    public void Stop()
    {
      if (this.Status.TerseStatus == SessionStatus.WaitingForStorageAndProviders)
      {
        this.StorageUpdateTimer.Stop();
        this.Status.TerseStatus = SessionStatus.UpdatingStoppedByUser;
      }
      if (this.Status.TerseStatus == SessionStatus.JobsReady || this.Status.TerseStatus == SessionStatus.PreparingJobs)
      {
        foreach (Job job in (Collection<Job>) this.jobs)
          job.Stop();
        this.StorageUpdateTimer.Stop();
        this.Status.TerseStatus = SessionStatus.PreparingJobsStoppedByUser;
      }
      if (this.Status.TerseStatus != SessionStatus.Started)
        return;
      foreach (Job job in (Collection<Job>) this.jobs)
        job.Stop();
      this.Status.TerseStatus = SessionStatus.RunningStoppedByUser;
      this.Status.VerboseStatus = "Kørsel afbrudt af bruger";
    }

    private void UpdateStorageStatusTimerTick(object sender, EventArgs e)
    {
      this.UpdateStorageStatus();
    }

    private void UpdateStorageStatus()
    {
      if (this.Status.TerseStatus != SessionStatus.WaitingForStorageAndProviders)
        return;
      foreach (SA_BackgroundDataObject backgroundDataObject in this.Settings.Storage)
        backgroundDataObject.UpdateStatus();
    }

    private void SessionStatusChanged(object sender, EventArgs e)
    {
      switch (this.Status.TerseStatus)
      {
        case SessionStatus.StorageAndProvidersReady:
          this.StorageUpdateTimer.Stop();
          this.timeLeft = this.leadInTime;
          this.LeadInTimer.Start();
          break;
        case SessionStatus.PreparingJobs:
          this.Log.Open();
          this.Log.PrepareEntry();
          break;
        case SessionStatus.JobsReady:
          SA_SessionStatus status1 = this.Status;
          string str1 = status1.VerboseStatus + (object) " - " + this.Jobs.ToString() + " jobs (" + SA_Convert.BytesToPrefixedValue(this.Status.TargetQuantity) + ")";
          status1.VerboseStatus = str1;
          this.Log.Submit((SA_ProgressStatus) this.Status);
          this.Log.PrepareEntry();
          this.Status.TerseStatus = SessionStatus.Started;
          this.OnStatusChanged(EventArgs.Empty);
          break;
        case SessionStatus.JobsNotReady:
          this.PrepareLeadOut();
          break;
        case SessionStatus.FinishedSuccesfully:
          SA_SessionStatus status2 = this.Status;
          string str2 = status2.VerboseStatus + (object) " - " + this.Jobs.ToString() + " jobs overført (" + SA_Convert.BytesToPrefixedValue(this.Status.TargetQuantity) + ")";
          status2.VerboseStatus = str2;
          this.PrepareLeadOut();
          break;
        case SessionStatus.FinishedWithWarning:
          this.PrepareLeadOut();
          break;
      }
      this.ProcessJobs();
    }

    private void PrepareLeadOut()
    {
      this.Log.Submit((SA_ProgressStatus) this.Status);
      this.Log.Close();
      this.timeLeft = this.leadOutTime;
      this.LeadOutTimer.Start();
    }

    private void StorageStatusChanged(object sender, EventArgs e)
    {
      SA_SessionStatus saSessionStatus = new SA_SessionStatus(this.Status);
      if (this.Status.TerseStatus != SessionStatus.WaitingForStorageAndProviders)
        return;
      saSessionStatus.TerseStatus = SessionStatus.StorageAndProvidersReady;
      foreach (Storage storage in this.Settings.Storage)
      {
        if (storage.Status.TerseStatus != StorageStatus.Ready)
          saSessionStatus.TerseStatus = SessionStatus.WaitingForStorageAndProviders;
      }
      this.Status = saSessionStatus;
    }

    private void JobStatusChanged(object sender, EventArgs e)
    {
      Job j = sender as Job;
      if (!j.busy)
      {
        this.activeJobs.Remove(j);
        this.jobsVerifyingAtSource.Remove(j);
        this.jobsTransferring.Remove(j);
        this.jobsVerifyingAtDestination.Remove(j);
        this.jobsApproving.Remove(j);
        switch (j.Status.TerseStatus)
        {
          case JobStatus.Created:
            this.jobsPendingSourceVerification.Submit(j);
            SA_JobStatus status1 = j.Status;
            string str1 = status1.VerboseStatus + " Klar til verificering";
            status1.VerboseStatus = str1;
            break;
          case JobStatus.VerifiedAtSource:
            this.jobsPendingTransfer.Submit(j);
            SA_SessionStatus status2 = this.Status;
            long num = status2.TargetQuantity + j.Status.TargetQuantity;
            status2.TargetQuantity = num;
            SA_JobStatus status3 = j.Status;
            string str2 = status3.VerboseStatus + " Klar til overførsel";
            status3.VerboseStatus = str2;
            break;
          case JobStatus.SourceError:
          case JobStatus.SourceMD5Error:
            this.jobsPendingSourceVerification.Submit(j);
            SA_JobStatus status4 = j.Status;
            string str3 = status4.VerboseStatus + " Klar til verificering";
            status4.VerboseStatus = str3;
            break;
          case JobStatus.Transferred:
            this.jobsPendingDestinationVerification.Submit(j);
            break;
          case JobStatus.AlreadyTransferred:
            this.jobsFinishedWithComments.Add(j);
            j.CleanUp();
            break;
          case JobStatus.TransferError:
            this.jobsPendingTransfer.Submit(j);
            break;
          case JobStatus.VerifiedAtDestination:
            this.jobsVerifyingAtDestination.Remove(j);
            this.jobsPendingApproval.Submit(j);
            break;
          case JobStatus.DestinationError:
          case JobStatus.DestinationMD5Error:
            this.jobsVerifyingAtDestination.Remove(j);
            this.jobsPendingDestinationVerification.Submit(j);
            break;
          case JobStatus.Approved:
            this.jobsFinishedSuccesfully.Add(j);
            j.CleanUp();
            break;
          case JobStatus.ApprovalError:
            this.jobsPendingApproval.Submit(j);
            break;
          case JobStatus.ApprovedWithComments:
            this.jobsApproving.Remove(j);
            this.jobsFinishedWithComments.Add(j);
            this.jobsFinishedSuccesfully.Add(j);
            j.CleanUp();
            break;
          case JobStatus.FinishedWithSourceError:
          case JobStatus.FinishedWithSourceMD5Error:
            this.jobsFinishedWithError.Add(j);
            break;
          case JobStatus.FinishedWithTransferError:
            this.jobsFinishedWithError.Add(j);
            break;
          case JobStatus.FinishedWithDestinationError:
          case JobStatus.FinishedWithDestinationMD5Error:
            this.jobsFinishedWithError.Add(j);
            break;
          case JobStatus.FinishedWithApprovalError:
            this.jobsFinishedWithError.Add(j);
            break;
        }
        this.UpdateSessionStatus();
        this.UpdateSessionProgress();
        this.ProcessJobs();
      }
      this.UpdateContent(j);
    }

    private void UpdateSessionStatus()
    {
      switch (this.Status.TerseStatus)
      {
        case SessionStatus.PreparingJobs:
          if (this.JobsPendingTransfer == this.Jobs)
          {
            this.Status.TerseStatus = SessionStatus.JobsReady;
            this.OnStatusChanged(EventArgs.Empty);
          }
          if (this.JobsFinishedWithError != this.Jobs)
            break;
          this.Status.TerseStatus = SessionStatus.JobsNotReady;
          this.OnStatusChanged(EventArgs.Empty);
          break;
        case SessionStatus.Started:
          if (this.JobsFinishedSuccesfully == this.Jobs)
          {
            this.Status.TerseStatus = SessionStatus.FinishedSuccesfully;
            this.OnStatusChanged(EventArgs.Empty);
            break;
          }
          else
          {
            if (this.JobsFinishedSuccesfully + this.JobsFinishedWithComments + this.JobsFinishedWithError == this.Jobs)
            {
              if (this.JobsFinishedWithError > 0)
              {
                this.Status.TerseStatus = SessionStatus.FinishedWithError;
                this.OnStatusChanged(EventArgs.Empty);
              }
              else
              {
                this.Status.TerseStatus = SessionStatus.FinishedWithWarning;
                this.OnStatusChanged(EventArgs.Empty);
              }
            }
            break;
          }
      }
    }

    private void CleanUp()
    {
      this.jobs.Clear();
      this.activeJobs.Clear();
      this.jobsPendingSourceVerification.Clear();
      this.jobsPendingTransfer.Clear();
      this.jobsPendingDestinationVerification.Clear();
      this.jobsPendingApproval.Clear();
      this.jobsVerifyingAtSource.Clear();
      this.jobsTransferring.Clear();
      this.jobsVerifyingAtDestination.Clear();
      this.jobsApproving.Clear();
      this.jobsFinishedSuccesfully.Clear();
      this.jobsFinishedWithError.Clear();
      this.jobsFinishedWithComments.Clear();
      this.content.Clear();
      this.Log.Reset();
      this.Status.TerseStatus = SessionStatus.WaitingForStorageAndProviders;
      this.StorageUpdateTimer.Start();
    }

    private void ProcessJobs()
    {
      while (this.CanProcess && (this.CanVerifyAtSource || this.CanTransfer || this.CanVerifyAtDestination || this.CanApprove))
      {
        if (this.CanVerifyAtSource && this.CanProcess)
        {
          Job job = this.jobsPendingSourceVerification.Retrieve();
          this.jobsVerifyingAtSource.Add(job);
          this.activeJobs.Add(job);
          job.Process();
        }
        if (this.CanTransfer && this.CanProcess)
        {
          Job job = this.jobsPendingTransfer.Retrieve();
          this.jobsTransferring.Add(job);
          this.activeJobs.Add(job);
          job.Process();
        }
        if (this.CanVerifyAtDestination && this.CanProcess)
        {
          Job job = this.jobsPendingDestinationVerification.Retrieve();
          this.jobsVerifyingAtDestination.Add(job);
          this.activeJobs.Add(job);
          job.Process();
        }
        if (this.CanApprove && this.CanProcess)
        {
          Job job = this.jobsPendingApproval.Retrieve();
          this.jobsApproving.Add(job);
          this.activeJobs.Add(job);
          job.Process();
        }
      }
    }

    private void UpdateContent(Job j)
    {
      DataRow dataRow = this.Content.Rows.Find((object) j.UniqueID);
      if (dataRow == null)
      {
        this.Content.Rows.Add((object) j.UniqueID, (object) j, (object) j.FileName, (object) SA_Reflect.GetUIName((object) j.Status.TerseStatus), (object) j.Status.VerboseStatus, (object) j.Status.VisualStatusBadge);
      }
      else
      {
        object[] objArray = new object[6]
        {
          (object) j.UniqueID,
          (object) j,
          (object) j.FileName,
          (object) SA_Reflect.GetUIName((object) j.Status.TerseStatus),
          (object) j.Status.VerboseStatus,
          (object) j.Status.VisualStatusBadge
        };
        dataRow.ItemArray = objArray;
      }
      if (this.Status.TerseStatus != SessionStatus.Started)
        return;
      this.Status.CurrentQuantity = 0L;
      List<Job> list = new List<Job>();
      list.AddRange((IEnumerable<Job>) this.jobsFinishedSuccesfully);
      list.AddRange((IEnumerable<Job>) this.jobsFinishedWithComments);
      list.AddRange((IEnumerable<Job>) this.jobsFinishedWithError);
      foreach (Job job in list)
      {
        SA_SessionStatus status = this.Status;
        long num = status.CurrentQuantity + job.Status.TargetQuantity;
        status.CurrentQuantity = num;
      }
      foreach (Job job in (Collection<Job>) this.jobsTransferring)
      {
        SA_SessionStatus status = this.Status;
        long num = status.CurrentQuantity + job.Status.CurrentQuantity;
        status.CurrentQuantity = num;
      }
    }

    private void UpdateSessionProgress()
    {
      if (this.Status.TerseStatus == SessionStatus.Started)
      {
        int num = this.Status.ProgressPercentage;
        string str = "";
        if (num == 0)
          num = this.JobsFinishedTotal * 100 / this.Jobs;
        if (this.Status.TargetQuantity != 0L && this.Status.CurrentQuantity != 0L)
          str = SA_Convert.BytesToPrefixedValue(this.Status.CurrentQuantity) + " af " + SA_Convert.BytesToPrefixedValue(this.Status.TargetQuantity);
        this.Status.VerboseStatus = "Overfører - " + (object) num + "% (" + this.JobsFinishedTotal.ToString() + " af " + this.Jobs.ToString() + "jobs, " + str + ")";
      }
      if (this.Status.TerseStatus != SessionStatus.PreparingJobs)
        return;
      this.Status.VerboseStatus = SA_Reflect.GetUIName((object) this.Status.TerseStatus) + (object) " - " + this.JobsPendingTransfer.ToString() + " af " + this.Jobs.ToString() + " klar";
    }
  }
}
