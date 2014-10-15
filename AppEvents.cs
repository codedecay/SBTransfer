// Type: SBTransfer.AppEvents
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Diagnostics;

namespace SBTransfer
{
  public class AppEvents
  {
    private EventLog log = (EventLog) null;
    private string source = "";
    private string logName = "";
    private string machineName = ".";

    public string Name
    {
      get
      {
        return this.logName;
      }
    }

    public string SourceName
    {
      get
      {
        return this.source;
      }
    }

    public string Machine
    {
      get
      {
        return this.machineName;
      }
    }

    public AppEvents(string logName)
      : this(logName, Process.GetCurrentProcess().ProcessName, Environment.MachineName)
    {
    }

    public AppEvents(string logName, string source)
      : this(logName, source, Environment.MachineName)
    {
    }

    public AppEvents(string logName, string source, string machineName)
    {
      this.logName = logName;
      this.source = source;
      this.machineName = machineName;
      if (!EventLog.SourceExists(source, machineName))
      {
        if (!(EventLog.LogNameFromSourceName(source, machineName) != logName))
          ;
        EventLog.CreateEventSource(new EventSourceCreationData(source, logName)
        {
          MachineName = machineName
        });
      }
      else
        logName = EventLog.LogNameFromSourceName(source, machineName);
      this.log = new EventLog(logName, machineName, source);
      this.log.EnableRaisingEvents = true;
    }

    public void WriteToLog(string message, EventLogEntryType type, CategoryType category, EventIDType eventID)
    {
      if (this.log == null)
        throw new ArgumentNullException("log", "This Event Log has not been opened or has been closed.");
      this.log.WriteEntry(message, type, (int) eventID, (short) category);
    }

    public void WriteToLog(string message, EventLogEntryType type, CategoryType category, EventIDType eventID, byte[] rawData)
    {
      if (this.log == null)
        throw new ArgumentNullException("log", "This Event Log has not been opened or has been closed.");
      this.log.WriteEntry(message, type, (int) eventID, (short) category, rawData);
    }

    public EventLogEntryCollection GetEntries()
    {
      if (this.log == null)
        throw new ArgumentNullException("log", "This Event Log has not been opened or has been closed.");
      else
        return this.log.Entries;
    }

    public void ClearLog()
    {
      if (this.log == null)
        throw new ArgumentNullException("log", "This Event Log has not been opened or has been closed.");
      this.log.Clear();
    }

    public void CloseLog()
    {
      if (this.log == null)
        throw new ArgumentNullException("log", "This Event Log has not been opened or has been closed.");
      this.log.Close();
      this.log = (EventLog) null;
    }

    public void DeleteLog()
    {
      if (EventLog.SourceExists(this.source, this.machineName))
        EventLog.DeleteEventSource(this.source, this.machineName);
      if (this.logName != "Application" && this.logName != "Security" && this.logName != "System" && EventLog.Exists(this.logName, this.machineName))
        EventLog.Delete(this.logName, this.machineName);
      if (this.log == null)
        return;
      this.log.Close();
      this.log = (EventLog) null;
    }
  }
}
