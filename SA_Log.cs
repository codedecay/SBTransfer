// Type: SBTransfer.SA_Log
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SBTransfer
{
  public class SA_Log
  {
    private string tempPath;
    private string finalPath;
    private string tempPathRoot;
    private string finalPathRoot;
    private string subPath;
    private string name;
    private string uniqueName;
    private DateTime start;
    private DateTime end;
    private string finalState;
    private long quantity;
    public BindingList<SA_LogEntry> Entries;
    public BindingList<SA_Log> SubLogs;
    public SA_LogEntry PendingEntry;

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

    public string UniqueName
    {
      get
      {
        return this.uniqueName;
      }
      set
      {
        this.uniqueName = value;
      }
    }

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

    public string FinalState
    {
      get
      {
        return this.finalState;
      }
      set
      {
        this.finalState = value;
      }
    }

    public long Quantity
    {
      get
      {
        return this.quantity;
      }
      set
      {
        this.quantity = value;
      }
    }

    public event EventHandler LogChanged;

    public SA_Log()
    {
    }

    public SA_Log(string name)
    {
      this.name = name;
      this.Entries = new BindingList<SA_LogEntry>();
      this.SubLogs = new BindingList<SA_Log>();
    }

    public SA_Log(string name, string tempPathRoot, string finalPathRoot)
      : this(name)
    {
      this.tempPathRoot = tempPathRoot;
      this.finalPathRoot = finalPathRoot;
    }

    public void Open()
    {
      this.Start = DateTime.Now;
      this.uniqueName = this.name + this.Start.ToString().Replace(':', '-');
      this.subPath = Path.Combine(this.Name, this.UniqueName + ".log");
      this.finalPath = Path.Combine(this.finalPathRoot, this.subPath);
      this.tempPath = Path.Combine(this.tempPathRoot, this.subPath);
    }

    public void Close()
    {
      this.End = DateTime.Now;
      File.Delete(this.tempPath);
      this.WriteToDisk(this.finalPath);
    }

    public void Reset()
    {
      this.Entries.Clear();
      this.SubLogs.Clear();
    }

    public void PrepareEntry()
    {
      this.PendingEntry = new SA_LogEntry();
      this.PendingEntry.Start = DateTime.Now;
    }

    public void Submit(SA_ProgressStatus status)
    {
      if (this.PendingEntry == null)
        return;
      this.PendingEntry.End = DateTime.Now;
      this.PendingEntry.State = status.VerboseStatus;
      this.FinalState = status.VerboseStatus;
      this.Quantity = status.CurrentQuantity;
      this.Entries.Add(this.PendingEntry);
      this.PendingEntry = (SA_LogEntry) null;
      this.OnLogChanged(EventArgs.Empty);
    }

    private void WriteToDisk(string path)
    {
      try
      {
        if (path == null)
          return;
        SA_Serializer.Serialize(path, (object) this);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Kunne ikke skrive log " + path);
        Program.Events.LogOrNotify(ex);
      }
    }

    public void SubLogChanged(object sender, EventArgs e)
    {
      this.SubLogs.Add(sender as SA_Log);
      this.WriteToDisk(this.tempPath);
    }

    protected void OnLogChanged(EventArgs e)
    {
      if (this.LogChanged != null)
        this.LogChanged((object) this, e);
      this.WriteToDisk(this.finalPath);
    }
  }
}
