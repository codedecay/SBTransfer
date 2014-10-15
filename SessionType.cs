// Type: SBTransfer.SessionType
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System.Collections.Generic;
using System.Xml.Serialization;

namespace SBTransfer
{
  public class SessionType
  {
    private string name;
    private int maxConcurrentBackgroundOperations;
    private int maxConcurrentSourceVerifications;
    private int maxConcurrentTransfers;
    private int maxConcurrentDestinationVerifications;
    private int maxConcurrentApprovals;
    private int maxTransferAttempts;
    private int maxApprovalAttempts;
    private bool deleteFromSource;
    private bool overWriteOnDestination;
    private int gracePeriod;
    private int gracePeriodMD5;
    public List<Storage> Sources;
    public List<Storage> Destinations;

    [UILabels("Navn", "Kørslens navn", "Indstillinger")]
    public string Name
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

    [UILabels("Max jobprocesser", "Maksimalt antal samtidige baggrundsprocesser indenfor denne session", "Indstillinger")]
    public int MaxConcurrentBackgroundOperations
    {
      get
      {
        return this.maxConcurrentBackgroundOperations;
      }
      set
      {
        this.maxConcurrentBackgroundOperations = value;
      }
    }

    [UILabels("Max verificeringer ved kilde", "Maksimalt antal samtidige verificeringer ved kilde indenfor denne session", "Indstillinger")]
    public int MaxConcurrentSourceVerifications
    {
      get
      {
        return this.maxConcurrentSourceVerifications;
      }
      set
      {
        this.maxConcurrentSourceVerifications = value;
      }
    }

    [UILabels("Max samtidige overførsler", "Maksimalt antal samtidige overførsler indenfor denne session", "Indstillinger")]
    public int MaxConcurrentTransfers
    {
      get
      {
        return this.maxConcurrentTransfers;
      }
      set
      {
        this.maxConcurrentTransfers = value;
      }
    }

    [UILabels("Max samtidige verificeringer på destination", "Maksimalt antal samtidige verificeringer ved destination  indenfor denne session", "Indstillinger")]
    public int MaxConcurrentDestinationVerifications
    {
      get
      {
        return this.maxConcurrentDestinationVerifications;
      }
      set
      {
        this.maxConcurrentDestinationVerifications = value;
      }
    }

    [UILabels("Max samtidige godkendelser på destination", "Maksimalt antal samtidige godkendelser indenfor denne session", "Indstillinger")]
    public int MaxConcurrentApprovals
    {
      get
      {
        return this.maxConcurrentApprovals;
      }
      set
      {
        this.maxConcurrentApprovals = value;
      }
    }

    [UILabels("Slet originaler efter overførsel", "Slet jobs fra kilde efter overførsel, såfremt kilden tillader sletning (Flytning fremfor for kopiering)", "Indstillinger")]
    public bool DeleteFromSource
    {
      get
      {
        return this.deleteFromSource;
      }
      set
      {
        this.deleteFromSource = value;
      }
    }

    [UILabels("Overskriv på destinationen", "Overskriv allerede eksisterende filer, såfremt destinationslageret tillader sletning", "Indstillinger")]
    public bool OverWriteOnDestination
    {
      get
      {
        return this.overWriteOnDestination;
      }
      set
      {
        this.overWriteOnDestination = value;
      }
    }

    [UILabels("Maksimalt antal overførselsforsøg", "Maksimalt antal overførselsforsøg", "Indstillinger")]
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

    [UILabels("Maksimalt antal godkendelsesforsøg", "Maksimalt antal godkendelsesforsøg", "Indstillinger")]
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

    [XmlIgnore]
    public List<Storage> Storage
    {
      get
      {
        List<Storage> list = new List<Storage>((IEnumerable<Storage>) this.Sources);
        list.AddRange((IEnumerable<Storage>) this.Destinations);
        return list;
      }
    }

    public SessionType()
    {
      this.Name = "Ny Gruppekørsel";
      this.MaxConcurrentBackgroundOperations = 20;
      this.Sources = new List<Storage>();
      this.Destinations = new List<Storage>();
    }

    public SessionType Clone()
    {
      return this.MemberwiseClone() as SessionType;
    }
  }
}
