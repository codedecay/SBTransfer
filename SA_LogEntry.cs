// Type: SBTransfer.SA_LogEntry
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;

namespace SBTransfer
{
  public class SA_LogEntry
  {
    private DateTime start;
    private DateTime end;
    private string state;

    public DateTime Start
    {
      get
      {
        return this.start;
      }
      set
      {
        this.start = value;
      }
    }

    public DateTime End
    {
      get
      {
        return this.end;
      }
      set
      {
        this.end = value;
      }
    }

    public string State
    {
      get
      {
        return this.state;
      }
      set
      {
        this.state = value;
      }
    }
  }
}
