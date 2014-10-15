// Type: SBTransfer.SA_LoggedDataObject
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

namespace SBTransfer
{
  public class SA_LoggedDataObject : SA_DataObject
  {
    public SA_Log Log;

    public SA_ProgressStatus Status
    {
      get
      {
        return (SA_ProgressStatus) this.status;
      }
      set
      {
        base.Status = (SA_Status) value;
      }
    }
  }
}
