// Type: SBTransfer.EventProcessor
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Diagnostics;

namespace SBTransfer
{
  public class EventProcessor
  {
    private AppEvents LocalEventLog;

    public EventProcessor(string logname)
    {
      this.LocalEventLog = new AppEvents(logname);
    }

    public void LogOrNotify(string message, EventLogEntryType type, CategoryType category, EventIDType eventID)
    {
    }

    public void LogOrNotify(Exception e)
    {
      this.LogOrNotify("", e);
    }

    public void LogOrNotify(string message, Exception e)
    {
      message = string.Concat(new object[4]
      {
        (object) message,
        (object) ((object) e.Source).ToString(),
        (object) '\n',
        (object) e.Message
      });
      if (e.InnerException != null)
        message = message + "\n" + e.InnerException.Message;
      this.LocalEventLog.WriteToLog(message, EventLogEntryType.Error, CategoryType.None, EventIDType.ExceptionThrown);
    }
  }
}
