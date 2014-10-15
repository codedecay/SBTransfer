// Type: SBTransfer.SA_BackgroundDataObject
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SBTransfer
{
  public abstract class SA_BackgroundDataObject : SA_DataObject
  {
    protected BackgroundWorker bw_UpdateStatus = new BackgroundWorker();
    protected BackgroundWorker bw_GetContent = new BackgroundWorker();

    public SA_BackgroundDataObject()
    {
      this.bw_UpdateStatus.DoWork += new DoWorkEventHandler(this.bw_Status_DoWork);
      this.bw_UpdateStatus.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_Status_RunWorkerCompleted);
      this.bw_GetContent.DoWork += new DoWorkEventHandler(this.bw_GetContent_DoWork);
      this.bw_GetContent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_GetContent_RunWorkerCompleted);
    }

    public virtual void UpdateStatus()
    {
      if (this.bw_UpdateStatus.IsBusy)
        return;
      this.bw_UpdateStatus.RunWorkerAsync();
    }

    public virtual void UpdateContent()
    {
      if (this.bw_GetContent.IsBusy)
        return;
      this.bw_GetContent.RunWorkerAsync();
    }

    protected abstract void bw_Status_DoWork(object sender, DoWorkEventArgs e);

    protected void bw_Status_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Result == null || e.Cancelled)
        return;
      if (e.Error != null)
      {
        int num = (int) MessageBox.Show(e.Error.Message);
      }
      else
        this.Status = e.Result as SA_Status;
    }

    protected abstract void bw_GetContent_DoWork(object sender, DoWorkEventArgs e);

    protected void bw_GetContent_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled)
        return;
      if (e.Error != null)
      {
        int num = (int) MessageBox.Show(e.Error.Message);
      }
      else if (e.Result != null)
      {
        this.Content.Rows.Clear();
        this.Content.Merge(e.Result as DataTable);
        this.OnContentChanged(EventArgs.Empty);
      }
    }
  }
}
