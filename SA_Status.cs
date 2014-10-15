// Type: SBTransfer.SA_Status
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Drawing;

namespace SBTransfer
{
  public abstract class SA_Status
  {
    protected short terseStatus;
    protected string verboseStatus;
    protected Image visualStatusOverlay;
    protected Image visualStatusBadge;
    protected Image[] StatusImageOverlays;
    protected Image[] StatusImageBadges;

    public virtual short TerseStatus
    {
      get
      {
        return this.terseStatus;
      }
      set
      {
        this.terseStatus = value;
        if ((int) this.terseStatus < this.StatusImageOverlays.Length && this.StatusImageOverlays[(int) this.terseStatus] != null)
          this.visualStatusOverlay = this.StatusImageOverlays[(int) this.terseStatus];
        if ((int) this.terseStatus < this.StatusImageBadges.Length && this.StatusImageBadges[(int) this.terseStatus] != null)
          this.visualStatusBadge = this.StatusImageBadges[(int) this.terseStatus];
        this.OnTerseStatusChanged(EventArgs.Empty);
        this.OnVisualStatusBadgeChanged(EventArgs.Empty);
        this.OnVisualStatusOverlayChanged(EventArgs.Empty);
        this.OnVerboseStatusChanged(EventArgs.Empty);
      }
    }

    public string VerboseStatus
    {
      get
      {
        return this.verboseStatus;
      }
      set
      {
        this.verboseStatus = value;
        this.OnVerboseStatusChanged(EventArgs.Empty);
      }
    }

    public Image VisualStatusOverlay
    {
      get
      {
        return this.visualStatusOverlay;
      }
      set
      {
        this.visualStatusOverlay = value;
        this.OnVisualStatusOverlayChanged(EventArgs.Empty);
      }
    }

    public Image VisualStatusBadge
    {
      get
      {
        return this.visualStatusBadge;
      }
      set
      {
        this.visualStatusBadge = value;
        this.OnVisualStatusBadgeChanged(EventArgs.Empty);
      }
    }

    public event EventHandler TerseStatusChanged;

    public event EventHandler VisualStatusOverlayChanged;

    public event EventHandler VisualStatusBadgeChanged;

    public event EventHandler VerboseStatusChanged;

    public SA_Status()
    {
      this.verboseStatus = "";
      this.visualStatusOverlay = (Image) new Bitmap(1, 1);
      this.visualStatusBadge = (Image) new Bitmap(1, 1);
      this.StatusImageOverlays = new Image[20];
      this.StatusImageBadges = new Image[20];
    }

    public SA_Status(SA_Status s)
    {
      this.StatusImageOverlays = s.StatusImageOverlays;
      this.StatusImageBadges = s.StatusImageBadges;
      this.TerseStatus = s.TerseStatus;
      this.VerboseStatus = s.VerboseStatus;
      this.VisualStatusOverlay = s.VisualStatusOverlay;
    }

    protected void OnTerseStatusChanged(EventArgs e)
    {
      if (this.TerseStatusChanged == null)
        return;
      this.TerseStatusChanged((object) this, e);
    }

    protected void OnVisualStatusOverlayChanged(EventArgs e)
    {
      if (this.VisualStatusOverlayChanged == null)
        return;
      this.VisualStatusOverlayChanged((object) this, e);
    }

    protected void OnVisualStatusBadgeChanged(EventArgs e)
    {
      if (this.VisualStatusBadgeChanged == null)
        return;
      this.VisualStatusBadgeChanged((object) this, e);
    }

    protected void OnVerboseStatusChanged(EventArgs e)
    {
      if (this.VerboseStatusChanged == null)
        return;
      this.VerboseStatusChanged((object) this, e);
    }
  }
}
