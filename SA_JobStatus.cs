// Type: SBTransfer.SA_JobStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System.Drawing;

namespace SBTransfer
{
  public class SA_JobStatus : SA_ProgressStatus
  {
    private string calculatedMD5 = "";

    public JobStatus TerseStatus
    {
      get
      {
        return (JobStatus) this.terseStatus;
      }
      set
      {
        base.TerseStatus = (short) value;
        this.VerboseStatus = SA_Reflect.GetUIName((object) value);
      }
    }

    public string CalculatedMD5
    {
      get
      {
        return this.calculatedMD5;
      }
      set
      {
        if (this.calculatedMD5 == null)
          return;
        this.calculatedMD5 = value.ToLower();
      }
    }

    public SA_JobStatus(SA_JobStatus j)
      : base((SA_ProgressStatus) j)
    {
    }

    public SA_JobStatus()
    {
      this.terseStatus = (short) 0;
      this.verboseStatus = "Job oprettet";
      this.StatusImageOverlays[5] = (Image) Resources.OverlayStorageAvailable;
      this.StatusImageOverlays[11] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[10] = (Image) Resources.OverlayOK;
      this.StatusImageOverlays[12] = (Image) Resources.OverlayWarning;
      this.StatusImageOverlays[0] = (Image) Resources.OverlayStorageAvailable;
      this.StatusImageOverlays[8] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[9] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[18] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[16] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[17] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[13] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[14] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[15] = (Image) Resources.OverlayError;
      this.StatusImageOverlays[2] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[3] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[6] = (Image) Resources.OverlayRunningWarning;
      this.StatusImageOverlays[4] = (Image) Resources.OverlayRunning;
      this.StatusImageOverlays[7] = (Image) Resources.OverlayRunning;
      this.StatusImageOverlays[1] = (Image) Resources.OverlayRunning;
      this.StatusImageBadges[5] = (Image) Resources.BadgeStorage;
      this.StatusImageBadges[11] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[10] = (Image) Resources.BadgeOK;
      this.StatusImageBadges[12] = (Image) Resources.BadgeWarning;
      this.StatusImageOverlays[0] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[8] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[9] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[18] = (Image) Resources.BadgeError;
      this.StatusImageBadges[16] = (Image) Resources.BadgeError;
      this.StatusImageBadges[17] = (Image) Resources.BadgeError;
      this.StatusImageBadges[13] = (Image) Resources.BadgeError;
      this.StatusImageBadges[14] = (Image) Resources.BadgeError;
      this.StatusImageBadges[15] = (Image) Resources.BadgeError;
      this.StatusImageBadges[2] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[3] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[6] = (Image) Resources.BadgeRunningError;
      this.StatusImageBadges[4] = (Image) Resources.BadgeRunning;
      this.StatusImageBadges[7] = (Image) Resources.BadgeRunning;
      this.StatusImageBadges[1] = (Image) Resources.BadgeRunning;
    }
  }
}
