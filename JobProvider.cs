// Type: SBTransfer.JobProvider
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Xml.Serialization;

namespace SBTransfer
{
  [XmlInclude(typeof (LocalFileJobProvider))]
  public abstract class JobProvider : SA_BackgroundDataObject, IContentCarrier
  {
    protected ProviderInput input;

    [UILabels("Navn", "Navn på Jobudbyderen", "Indstillinger")]
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

    [UILabels("Jobbeskrivelse", "Hvilket input der forventes i joblisterne\r\n Filnavn er obligatorisk, MD5 og Filstørrelse er valgfrie.", "Indstillinger")]
    public ProviderInput Input
    {
      get
      {
        return this.input;
      }
      set
      {
        this.input = value;
      }
    }

    [XmlIgnore]
    public SA_ProviderStatus Status
    {
      get
      {
        return (SA_ProviderStatus) this.status;
      }
      set
      {
        this.status = (SA_Status) value;
        this.OnStatusChanged(EventArgs.Empty);
      }
    }

    public JobProvider()
    {
      this.Status = new SA_ProviderStatus();
      this.UIImage = (Image) Resources.Package;
      this.UIBadge = (Image) Resources.BadgePackage;
      this.ContentChanged += new EventHandler(this.JobProvider_ContentChanged);
    }

    public virtual List<Job> GetJobList(Storage source, Storage destination)
    {
      List<Job> list = new List<Job>();
      for (int index = 0; index < this.Content.Rows.Count; ++index)
      {
        DataRow dataRow = this.Content.Rows[index];
        switch (this.Input)
        {
          case ProviderInput.Filnavn:
            list.Add(new Job(dataRow["Navn"].ToString(), source, destination));
            break;
          case ProviderInput.MD5:
            list.Add(new Job(dataRow["Navn"].ToString(), source, destination, dataRow["MD5"].ToString()));
            break;
          case ProviderInput.FileSize:
            list.Add(new Job(dataRow["Navn"].ToString(), source, destination, (long) dataRow["Størrelse"]));
            break;
          case ProviderInput.FileSizeAndMD5:
            list.Add(new Job(dataRow["Navn"].ToString(), source, destination, dataRow["MD5"].ToString(), (long) dataRow["Størrelse"]));
            break;
        }
      }
      return list;
    }

    protected void JobProvider_ContentChanged(object sender, EventArgs e)
    {
      if (this.bw_UpdateStatus.IsBusy)
        return;
      this.bw_UpdateStatus.RunWorkerAsync();
    }

    public override void UpdateStatus()
    {
      if (this.bw_GetContent.IsBusy)
        return;
      this.bw_GetContent.RunWorkerAsync();
    }

    public abstract void CleanUp();

    public abstract void RemoveEntry(Job job);
  }
}
