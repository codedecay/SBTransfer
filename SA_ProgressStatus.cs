// Type: SBTransfer.SA_ProgressStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public abstract class SA_ProgressStatus : SA_Status
  {
    private long targetQuantity;
    private long currentQuantity;

    public long TargetQuantity
    {
      get
      {
        return this.targetQuantity;
      }
      set
      {
        this.targetQuantity = value;
      }
    }

    public long CurrentQuantity
    {
      get
      {
        return this.currentQuantity;
      }
      set
      {
        this.currentQuantity = value;
      }
    }

    public int ProgressPercentage
    {
      get
      {
        if (this.TargetQuantity > 0L)
          return (int) (this.CurrentQuantity * 100L / this.TargetQuantity);
        else
          return 0;
      }
    }

    public SA_ProgressStatus()
    {
    }

    public SA_ProgressStatus(SA_ProgressStatus s)
      : base((SA_Status) s)
    {
      this.targetQuantity = s.targetQuantity;
      this.currentQuantity = s.currentQuantity;
    }
  }
}
