// Type: SBTransfer.SA_NotificationCollection`1
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Collections.ObjectModel;

namespace SBTransfer
{
  public class SA_NotificationCollection<T> : Collection<T>
  {
    public event EventHandler ContentChanged;

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      this.OnContentChanged(EventArgs.Empty);
    }

    protected override void RemoveItem(int index)
    {
      base.RemoveItem(index);
      this.OnContentChanged(EventArgs.Empty);
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      this.OnContentChanged(EventArgs.Empty);
    }

    protected override void SetItem(int index, T item)
    {
      base.SetItem(index, item);
      this.OnContentChanged(EventArgs.Empty);
    }

    protected void OnContentChanged(EventArgs e)
    {
      if (this.ContentChanged == null)
        return;
      this.ContentChanged((object) this, e);
    }

    public void Submit(T item)
    {
      this.InsertItem(this.Count, item);
    }

    public T Retrieve()
    {
      T obj = this.Items[0];
      this.RemoveItem(0);
      return obj;
    }
  }
}
