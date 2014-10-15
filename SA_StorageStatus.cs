// Type: SBTransfer.SA_StorageStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System.Drawing;

namespace SBTransfer
{
  public class SA_StorageStatus : SA_Status
  {
    public StorageStatus TerseStatus
    {
      get
      {
        return (StorageStatus) this.terseStatus;
      }
      set
      {
        base.TerseStatus = (short) value;
        this.VerboseStatus = SA_Reflect.GetUIName((object) value);
      }
    }

    public SA_StorageStatus(SA_StorageStatus j)
      : base((SA_Status) j)
    {
    }

    public SA_StorageStatus()
    {
      this.StatusImageOverlays[2] = (Image) Resources.OverlayStorageAvailable;
      this.StatusImageOverlays[0] = (Image) Resources.OverlayWarning;
      this.StatusImageOverlays[4] = (Image) Resources.OverlayStorageLocked;
      this.StatusImageOverlays[3] = (Image) Resources.OverlayOK;
      this.StatusImageOverlays[1] = (Image) new Bitmap(1, 1);
      this.StatusImageBadges[2] = (Image) Resources.BadgeStorage;
      this.StatusImageBadges[0] = (Image) Resources.BadgeWarning;
      this.StatusImageBadges[4] = (Image) Resources.BadgeLocked;
      this.StatusImageBadges[3] = (Image) Resources.BadgeOK;
      this.StatusImageBadges[1] = (Image) new Bitmap(1, 1);
    }
  }
}
