// Type: SBTransfer.SA_SessionStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System.Drawing;

namespace SBTransfer
{
  public class SA_SessionStatus : SA_ProgressStatus
  {
    public SessionStatus TerseStatus
    {
      get
      {
        return (SessionStatus) this.terseStatus;
      }
      set
      {
        base.TerseStatus = (short) value;
        this.VerboseStatus = SA_Reflect.GetUIName((object) value);
      }
    }

    public SA_SessionStatus(SA_SessionStatus j)
      : base((SA_ProgressStatus) j)
    {
    }

    public SA_SessionStatus()
    {
      this.terseStatus = (short) 0;
      this.StatusImageOverlays[0] = (Image) new Bitmap(1, 1);
      this.StatusImageOverlays[6] = (Image) Resources.OverlayStopped;
      this.StatusImageOverlays[1] = (Image) Resources.OverlayStorageAvailable;
      this.StatusImageOverlays[5] = (Image) Resources.OverlayRunning;
      this.StatusImageOverlays[8] = (Image) Resources.OverlayStopped;
      this.StatusImageOverlays[7] = (Image) Resources.OverlayStopped;
      this.StatusImageOverlays[2] = (Image) Resources.OverlayRunning;
      this.StatusImageOverlays[3] = (Image) Resources.OverlayRunning;
      this.StatusImageOverlays[4] = (Image) Resources.OverlayWarning;
      this.StatusImageOverlays[10] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[9] = (Image) Resources.OverlayOK;
      this.StatusImageOverlays[11] = (Image) Resources.OverlayWarning;
      this.StatusImageBadges[0] = (Image) new Bitmap(1, 1);
      this.StatusImageBadges[6] = (Image) Resources.BadgeStopped;
      this.StatusImageBadges[1] = (Image) Resources.BadgeStorage;
      this.StatusImageBadges[5] = (Image) Resources.BadgeRunning;
      this.StatusImageBadges[8] = (Image) Resources.BadgeStopped;
      this.StatusImageBadges[7] = (Image) Resources.BadgeStopped;
      this.StatusImageBadges[2] = (Image) Resources.BadgeRunning;
      this.StatusImageBadges[3] = (Image) Resources.BadgeRunning;
      this.StatusImageBadges[4] = (Image) Resources.BadgeWarning;
      this.StatusImageBadges[10] = (Image) Resources.BadgeError;
      this.StatusImageBadges[9] = (Image) Resources.BadgeOK;
      this.StatusImageBadges[11] = (Image) Resources.BadgeWarning;
    }
  }
}
