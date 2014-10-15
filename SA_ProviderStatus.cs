// Type: SBTransfer.SA_ProviderStatus
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System.Drawing;

namespace SBTransfer
{
  public class SA_ProviderStatus : SA_Status
  {
    public ProviderStatus TerseStatus
    {
      get
      {
        return (ProviderStatus) this.terseStatus;
      }
      set
      {
        base.TerseStatus = (short) value;
        this.VerboseStatus = SA_Reflect.GetUIName((object) value);
      }
    }

    public SA_ProviderStatus(SA_ProviderStatus j)
      : base((SA_Status) j)
    {
    }

    public SA_ProviderStatus()
    {
      this.StatusImageOverlays[0] = (Image) Resources.OverlayWarning;
      this.StatusImageOverlays[2] = (Image) Resources.OverlayOK;
      this.StatusImageOverlays[1] = (Image) new Bitmap(1, 1);
      this.StatusImageBadges[0] = (Image) Resources.BadgeWarning;
      this.StatusImageBadges[2] = (Image) Resources.BadgeOK;
      this.StatusImageBadges[1] = (Image) new Bitmap(1, 1);
    }
  }
}
