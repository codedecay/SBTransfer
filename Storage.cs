// Type: SBTransfer.Storage
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace SBTransfer
{
  [XmlInclude(typeof (RemoteStorageSB))]
  [XmlInclude(typeof (LocalStorage))]
  [XmlInclude(typeof(RemoteStorageBitMag))]
  public abstract class Storage : SA_BackgroundDataObject, IContentCarrier
  {
    private bool verifyMD5;
    private List<JobProvider> jobProviders;
    private int maxMD5VerificationAttempts;
    private int maxVerificationAttempts;
    private bool canDelete;

    [UILabels("Navn", "Navn på lagerelementet", "Indstillinger")]
    public override string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    [UILabels("MD5 check", "Inkluder MD5 check når jobs skal verificeres\r\nFor lager der optræder som kilde kræves en forudberegnet MD5-værdi i jobbeskrivelsen\r\nVerificering er på kildesiden en nødvendig forudsætning for at en kørsel kan begynde\r\n og på destinationssiden for at kørslen regnes som fuldført\r\nSåfremt et MD5 check ikke inkluderes i verificeringen,\r\ncheckes der blot om en fil med det rette navn eksisterer på lageret.", "Indstillinger")]
    public bool VerifyMD5
    {
      get
      {
        return this.verifyMD5;
      }
      set
      {
        this.verifyMD5 = value;
      }
    }

    [UILabels("Sletning mulig", "Hvorvidt filer kan slettes eller overskrives på dette lager", "Indstillinger")]
    public bool CanDelete
    {
      get
      {
        return this.canDelete;
      }
      set
      {
        this.canDelete = value;
      }
    }

    [UILabels("Maksimalt antal forsøg på verificering af MD5", "", "Indstillinger")]
    public int MaxMD5VerificationAttempts
    {
      get
      {
        return this.maxMD5VerificationAttempts;
      }
      set
      {
        this.maxMD5VerificationAttempts = value;
      }
    }

    [UILabels("Maksimalt antal forsøg på verificering", "", "Indstillinger")]
    public int MaxVerificationAttempts
    {
      get
      {
        return this.maxVerificationAttempts;
      }
      set
      {
        this.maxVerificationAttempts = value;
      }
    }

    [XmlIgnore]
    public SA_StorageStatus Status
    {
      get
      {
        return (SA_StorageStatus) this.status;
      }
      set
      {
        base.Status = (SA_Status) value;
      }
    }

    public List<JobProvider> JobProviders
    {
      get
      {
        return this.jobProviders;
      }
      set
      {
        this.jobProviders = value;
      }
    }

    public Storage()
    {
      if (this.JobProviders == null)
        this.JobProviders = new List<JobProvider>();
      this.status = (SA_Status) new SA_StorageStatus();
      this.UIImage = (Image) Resources.Storage;
      this.UIBadge = (Image) Resources.BadgeStorage;
      this.maxVerificationAttempts = 50;
      this.maxMD5VerificationAttempts = 3;
      this.verifyMD5 = false;
      this.canDelete = false;
    }

    public virtual void CleanUp()
    {
      foreach (JobProvider jobProvider in this.JobProviders)
        jobProvider.CleanUp();
    }

    public virtual void Delete(Job j)
    {
    }

    private void ProviderStatusChanged(object sender, EventArgs e)
    {
      if (this.bw_UpdateStatus.IsBusy)
        return;
      this.bw_UpdateStatus.RunWorkerAsync();
    }

    public void Initialize()
    {
      foreach (SA_DataObject saDataObject in this.JobProviders)
        saDataObject.StatusChanged += new EventHandler(this.ProviderStatusChanged);
    }
       
    public override void UpdateStatus()
    {
      foreach (SA_BackgroundDataObject backgroundDataObject in this.JobProviders)
        backgroundDataObject.UpdateStatus();
      if (this.bw_UpdateStatus.IsBusy)
        return;
      this.bw_UpdateStatus.RunWorkerAsync();
    }

    public SA_StorageWorker Transfer(Job job, Storage destination)
    {
      SA_StorageWorker saStorageWorker;
      if (destination is LocalStorage)
      {
        saStorageWorker = new SA_StorageWorker(job);
        saStorageWorker.DoWork += new DoWorkEventHandler(this.bw_Transfer_DoWork);
      }
      else
      {
        saStorageWorker = new SA_StorageWorker(job); 
        saStorageWorker.DoWork += new DoWorkEventHandler(destination.bw_Transfer_DoWork);
      }
      return saStorageWorker;
    }

    public SA_StorageWorker Verify(Job job)
    {
      SA_StorageWorker saStorageWorker = new SA_StorageWorker(job);
      saStorageWorker.DoWork += new DoWorkEventHandler(this.bw_Verify_DoWork);
      return saStorageWorker;
    }

    public SA_StorageWorker Approve(Job job)
    {
      SA_StorageWorker saStorageWorker = new SA_StorageWorker(job);
      saStorageWorker.DoWork += new DoWorkEventHandler(this.bw_Finalize_DoWork);
      return saStorageWorker;
    }

    protected abstract void bw_Verify_DoWork(object sender, DoWorkEventArgs e);

    protected abstract void bw_Finalize_DoWork(object sender, DoWorkEventArgs e);

    protected abstract void bw_Transfer_DoWork(object sender, DoWorkEventArgs e);

    protected void AddProviderStatus(SA_StorageStatus st)
    {
      if (this.JobProviders.Count > 0)
      {
        SA_StorageStatus saStorageStatus = st;
        string str = saStorageStatus.VerboseStatus + "\r\n";
        saStorageStatus.VerboseStatus = str;
      }
      foreach (JobProvider jobProvider in this.JobProviders)
      {
        switch (jobProvider.Status.TerseStatus)
        {
          case ProviderStatus.Error:
            st.TerseStatus = StorageStatus.Error;
            SA_StorageStatus saStorageStatus1 = st;
            string str1 = saStorageStatus1.VerboseStatus + string.Format("\r\n Jobliste \"{0}\" melder fejl", (object) jobProvider.Name);
            saStorageStatus1.VerboseStatus = str1;
            break;
          case ProviderStatus.Unavailable:
            if (st.TerseStatus != StorageStatus.Error)
              st.TerseStatus = StorageStatus.Available;
            SA_StorageStatus saStorageStatus2 = st;
            string str2 = saStorageStatus2.VerboseStatus + string.Format("\r\n Jobliste \"{0}\" ikke fundet", (object) jobProvider.Name);
            saStorageStatus2.VerboseStatus = str2;
            break;
          case ProviderStatus.Ready:
            SA_StorageStatus saStorageStatus3 = st;
            string str3 = saStorageStatus3.VerboseStatus + string.Format("\r\n Jobliste \"{0}\" klar", (object) jobProvider.Name);
            saStorageStatus3.VerboseStatus = str3;
            break;
        }
      }
    }

    public abstract long Size(string FileName);

    public List<Job> GetJobList(Storage destination)
    {
      List<Job> list = new List<Job>();
      if (this.Status.TerseStatus == StorageStatus.Ready || this.Status.TerseStatus == StorageStatus.Locked)
      {
        foreach (JobProvider jobProvider in this.JobProviders)
          list.AddRange((IEnumerable<Job>) jobProvider.GetJobList(this, destination));
      }
      return list;
    }

    protected void setJobStatusVerified(SA_JobStatus js)
    {
      if (js.TerseStatus == JobStatus.Created)
      {
        js.TerseStatus = JobStatus.VerifiedAtSource;
        js.VerboseStatus = "Job verificeret ved kilde";
      }
      if (js.TerseStatus != JobStatus.Transferred)
        return;
      js.TerseStatus = JobStatus.VerifiedAtDestination;
      js.VerboseStatus = "Job verificeret ved destination";
    }

    protected void setJobStatusMD5Error(SA_JobStatus js)
    {
      if (js.TerseStatus == JobStatus.Created)
      {
        js.TerseStatus = JobStatus.SourceMD5Error;
        js.VerboseStatus = "MD5 fejl ved kilde\r\nBeregnet MD5=";
      }
      if (js.TerseStatus != JobStatus.Transferred)
        return;
      js.TerseStatus = JobStatus.DestinationMD5Error;
      js.VerboseStatus = "MD5 fejl ved destination\r\nBeregnet MD5=";
    }

    protected void setJobStatusError(SA_JobStatus js)
    {
      if (js.TerseStatus == JobStatus.Created)
      {
        js.TerseStatus = JobStatus.SourceError;
        js.VerboseStatus = "Verificeringsfejl ved kilde";
      }
      if (js.TerseStatus != JobStatus.Transferred)
        return;
      js.TerseStatus = JobStatus.DestinationError;
      js.VerboseStatus = "Verificeringsfejl ved destination";
    }
  }
}
