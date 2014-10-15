// Type: SBTransfer.SA_DataObject
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Data;
using System.Drawing;
using System.Xml.Serialization;

namespace SBTransfer
{
  public abstract class SA_DataObject
  {
    protected Image uiImage = (Image) new Bitmap(32, 32);
    protected Image uiBadge = (Image) new Bitmap(16, 16);
    protected string name;
    protected DataTable content;
    protected SA_Status status;

    public virtual string Name
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

    [XmlIgnore]
    public virtual SA_Status Status
    {
      get
      {
        return this.status;
      }
      set
      {
        this.status.TerseStatus = value.TerseStatus;
        this.status.VerboseStatus = value.VerboseStatus;
        this.OnStatusChanged(EventArgs.Empty);
      }
    }

    [XmlIgnore]
    public DataTable Content
    {
      get
      {
        return this.content;
      }
      set
      {
        this.content = value;
        this.OnContentChanged(EventArgs.Empty);
      }
    }

    [XmlIgnore]
    public Image UIImage
    {
      get
      {
        return this.uiImage;
      }
      set
      {
        this.uiImage = value;
        this.OnUIImageChanged(EventArgs.Empty);
      }
    }

    [XmlIgnore]
    public Image UIBadge
    {
      get
      {
        return this.uiBadge;
      }
      set
      {
        this.uiBadge = value;
        this.OnUIBadgeChanged(EventArgs.Empty);
      }
    }

    public event EventHandler StatusChanged;

    public event EventHandler ContentChanged;

    public event EventHandler UIImageChanged;

    public event EventHandler UIBadgeChanged;

    public SA_DataObject()
    {
      this.Content = new DataTable("Indhold");
    }

    protected void OnStatusChanged(EventArgs e)
    {
      if (this.StatusChanged == null)
        return;
      this.StatusChanged((object) this, e);
    }

    protected void OnContentChanged(EventArgs e)
    {
      if (this.ContentChanged == null)
        return;
      this.ContentChanged((object) this, e);
    }

    protected void OnUIImageChanged(EventArgs e)
    {
      if (this.UIImageChanged == null)
        return;
      this.UIImageChanged((object) this, e);
    }

    protected void OnUIBadgeChanged(EventArgs e)
    {
      if (this.UIBadgeChanged == null)
        return;
      this.UIBadgeChanged((object) this, e);
    }
  }
}
