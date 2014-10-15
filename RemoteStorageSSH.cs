// Type: SBTransfer.RemoteStorageSSH
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System.ComponentModel;

namespace SBTransfer
{
  [UILabels("SSH-lager", "Lagertype", "Fjernlager med SSH-interface")]
  internal class RemoteStorageSSH : Storage
  {
    protected override void bw_GetContent_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    protected override void bw_Status_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    protected override void bw_Verify_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    protected override void bw_Transfer_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    protected override void bw_Finalize_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    public override long Size(string FileName)
    {
      return -1L;
    }
  }
}
