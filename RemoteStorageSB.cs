// Type: SBTransfer.RemoteStorageSB
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SBTransfer
{
  [UILabels("SB", "Statsbibliotekets lagerløsning via SSH/CygWin")]
  public class RemoteStorageSB : Storage
  {
    private long freeSpace;
    private long maxFileSize;
    private string tmpPath;
    private string cmdPath;

    [UILabels("Kommandofil", "Den fulde sti til SB kommandofil")]
    public string CmdPath
    {
      get
      {
        return this.cmdPath;
      }
      set
      {
        this.cmdPath = value;
      }
    }

    [UILabels("Temporære filer", "Sti til lagring af temporære statusfiler")]
    public string TmpPath
    {
      get
      {
        return this.tmpPath;
      }
      set
      {
        this.tmpPath = value;
      }
    }

    public RemoteStorageSB()
    {
      string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
      this.Name = "SB";
      this.cmdPath = directoryName + "\\SBCmd\\SB.cmd";
      this.tmpPath = directoryName + "\\tmp";
    }

    public RemoteStorageSB(string CmdPath, string TmpPath)
    {
      this.cmdPath = CmdPath;
      this.tmpPath = TmpPath;
    }

    private Process Execute(string strArgs)
    {
      Process process = new Process();
      process.EnableRaisingEvents = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.FileName = this.cmdPath;
      process.StartInfo.Arguments = strArgs;
      process.Start();
      return process;
    }

    protected override void bw_GetContent_DoWork(object sender, DoWorkEventArgs e)
    {
      DataTable dataTable = new DataTable("Indhold");
      dataTable.Columns.Add("Navn");
      dataTable.Columns.Add("MD5");
      dataTable.Columns.Add("Dato");
      string path = this.tmpPath + "\\md5s.txt" + this.GetHashCode().ToString();
      string resultfile = this.tmpPath + "\\md5s_result.txt" + this.GetHashCode().ToString();
      this.Execute("getmd5s " + path + " " + resultfile).WaitForExit();
      try
      {
        if (this.Result(resultfile) == "0")
        {
          if (File.Exists(path))
          {
            StreamReader streamReader = new StreamReader(path);
            while (!streamReader.EndOfStream)
            {
              string[] strArray = streamReader.ReadLine().Split(new char[1]
              {
                ' '
              });
              dataTable.Rows.Add((object) strArray[0].Trim(), (object) strArray[1].Trim(), (object) strArray[2].Trim());
            }
          }
        }
      }
      catch (Exception ex)
      {
        Program.Events.LogOrNotify(ex);
      }
      e.Result = (object) dataTable;
    }

    protected void VerifySetup(SA_StorageStatus st)
    {
      string str1 = "";
      string str2 = "";
      if (File.Exists(this.CmdPath))
      {
        StreamReader streamReader = new StreamReader(this.CmdPath);
        while (!streamReader.EndOfStream && (str1 == "" || str2 == ""))
        {
          string[] strArray = streamReader.ReadLine().Split(new char[1]
          {
            '='
          });
          if (strArray[0].Contains("set DEST"))
            str1 = strArray[1];
          if (strArray[0].Contains("set SSH"))
            str2 = strArray[1];
        }
        streamReader.Close();
        if (str1 == "")
          st.VerboseStatus = string.Format("{0} \r\n Problem med læsning af {1}\r\n Kunne ikke finde server-angivelse (set DEST)", (object) this.Name, (object) this.CmdPath);
        if (str2 == "")
          st.VerboseStatus = string.Format("{0} \r\n Problem med læsning af {1}\r\n Kunne ikke finde sti-angivelse til SSH (set SSH)", (object) this.Name, (object) this.CmdPath);
        else if (File.Exists(str2 + ".exe"))
        {
          st.VerboseStatus = string.Format("{0} på {1}\r\n", (object) this.Name, (object) str1);
          st.TerseStatus = StorageStatus.Available;
        }
        else
          st.VerboseStatus = string.Format("{0} \r\nKunne ikke finde {1}\r\n Check at stien er korrekt samt at CygWin med SSH er installeret\r\n", (object) this.Name, (object) str2);
      }
      else
        st.VerboseStatus = string.Format("{0} \r\n Kunne ikke finde {1}\r\n Check at stien er korrekt", (object) this.Name, (object) this.CmdPath);
    }

    protected override void bw_Status_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
      string path = this.tmpPath + "\\spaceleft.txt" + this.GetHashCode().ToString();
      string resultfile = this.tmpPath + "\\spaceleft_result.txt" + this.GetHashCode().ToString();
      SA_StorageStatus st = new SA_StorageStatus(this.Status);
      st.TerseStatus = StorageStatus.Error;
      try
      {
        this.VerifySetup(st);
        if (st.TerseStatus == StorageStatus.Available)
        {
          this.Execute("getspace " + path + " " + resultfile).WaitForExit();
          string str1 = this.Result(resultfile);
          if (str1 == "0")
          {
            if (File.Exists(path))
            {
              StreamReader streamReader = new StreamReader(path);
              while (!streamReader.EndOfStream)
              {
                string[] strArray = streamReader.ReadLine().Split(new char[1]
                {
                  ':'
                });
                if (strArray[0].Contains("Max file size"))
                  this.maxFileSize = Convert.ToInt64(strArray[1].Trim());
                else
                  this.freeSpace = Convert.ToInt64(strArray[1].Trim());
              }
              st.TerseStatus = StorageStatus.Ready;
              SA_StorageStatus saStorageStatus = st;
              string str2 = saStorageStatus.VerboseStatus + string.Format("{0} fri plads i permanent lager \r\n {1} fri plads i modtagebuffer", (object) SA_Convert.BytesToPrefixedValue(this.freeSpace), (object) SA_Convert.BytesToPrefixedValue(this.maxFileSize));
              saStorageStatus.VerboseStatus = str2;
              this.AddProviderStatus(st);
            }
          }
          else
          {
            st.TerseStatus = StorageStatus.Error;
            SA_StorageStatus saStorageStatus = st;
            string str2 = saStorageStatus.VerboseStatus + string.Format("{0} returnerer  fejl {1} på statusforespørgsel", (object) this.Name, (object) str1);
            saStorageStatus.VerboseStatus = str2;
          }
        }
      }
      catch (Exception ex)
      {
        st.TerseStatus = StorageStatus.Error;
        st.VerboseStatus = string.Format("{0} Kan ikke tilgås \r\n Fejl: {1}", (object) this.Name, (object) e.ToString());
        Program.Events.LogOrNotify(ex);
      }
      e.Result = (object) st;
    }

    private string Result(string resultfile)
    {
      string str = "";
      if (File.Exists(resultfile))
      {
        StreamReader streamReader = new StreamReader(resultfile);
        str = streamReader.ReadLine().Trim();
        streamReader.Close();
      }
      return str;
    }

    protected override void bw_Verify_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
      SA_JobStatus js = new SA_JobStatus(saStorageWorker.job.Status);
      Process process1 = new Process();
      string str1 = this.tmpPath + "\\getmd5" + saStorageWorker.job.FileName + ".txt";
      string str2 = this.tmpPath + "\\getmd5_result" + saStorageWorker.job.FileName + ".txt";
      try
      {
        Process process2 = this.Execute("md5 " + saStorageWorker.job.FileName + " " + str1 + " " + str2);
        while (!process2.HasExited)
        {
          Thread.Sleep(1000);
          saStorageWorker.ReportProgress(100);
          if (saStorageWorker.CancellationPending)
          {
            process2.Kill();
            File.Delete(str2);
            File.Delete(str1);
            e.Result = (object) js;
            return;
          }
        }
        Thread.Sleep(200);
        if (this.Result(str2) == "0")
        {
          js.CalculatedMD5 = this.Result(str1);
          if (js.CalculatedMD5 == saStorageWorker.job.MD5)
            this.setJobStatusVerified(js);
          else
            this.setJobStatusMD5Error(js);
        }
        else
          this.setJobStatusError(js);
        e.Result = (object) js;
        File.Delete(str2);
        File.Delete(str1);
      }
      catch (Exception ex)
      {
        this.setJobStatusMD5Error(js);
        Program.Events.LogOrNotify(ex);
        e.Result = (object) js;
      }
    }

    protected override void bw_Transfer_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
      SA_JobStatus saJobStatus = new SA_JobStatus(saStorageWorker.job.Status);
      saJobStatus.VerboseStatus = "Overfører";
      string str1 = "";
      try
      {
        if (saStorageWorker.job.destination is LocalStorage)
        {
          str1 = this.tmpPath + "\\GetResult" + saStorageWorker.job.UniqueID + ".txt";
          Process process = this.Execute("get " + saStorageWorker.job.FileName + " " + (saStorageWorker.job.destination as LocalStorage).LocalPath + saStorageWorker.job.FileName + " " + str1);
          while (!process.HasExited)
          {
            Thread.Sleep(1000);
            saStorageWorker.ReportProgress(100, (object) saJobStatus);
            if (saStorageWorker.CancellationPending)
            {
              process.Kill();
              File.Delete(str1);
              e.Result = (object) saJobStatus;
              return;
            }
          }
          Thread.Sleep(200);
          if (File.Exists(str1))
          {
            if (this.Result(str1) == "0")
            {
              saJobStatus.TerseStatus = JobStatus.Transferred;
              saJobStatus.VerboseStatus = "Job overført til " + saStorageWorker.job.destination.Name;
            }
            else
            {
              saJobStatus.TerseStatus = JobStatus.TransferError;
              saJobStatus.VerboseStatus = "Overførselsfejl " + this.Name;
            }
          }
        }
        if (saStorageWorker.job.source is LocalStorage)
        {
          str1 = this.tmpPath + "\\SendResult" + saStorageWorker.job.UniqueID + ".txt";
          string str2 = this.tmpPath + "\\SendResultMD5" + saStorageWorker.job.UniqueID + ".txt";
          Process process = this.Execute("sendmd5 " + (saStorageWorker.job.source as LocalStorage).LocalPath + saStorageWorker.job.FileName + " " + str2 + " " + str1);
          while (!process.HasExited)
          {
            Thread.Sleep(1000);
            saStorageWorker.ReportProgress(100, (object) saJobStatus);
            if (saStorageWorker.CancellationPending)
            {
              process.Kill();
              File.Delete(str1);
              File.Delete(str2);
              e.Result = (object) saJobStatus;
              return;
            }
          }
          Thread.Sleep(200);
          if (File.Exists(str1))
          {
            if (this.Result(str1) == "0")
            {
              if (this.Result(str2).ToLower() == saStorageWorker.job.MD5.ToLower())
              {
                saJobStatus.TerseStatus = JobStatus.VerifiedAtDestination;
                saJobStatus.CurrentQuantity = saJobStatus.TargetQuantity;
                saJobStatus.VerboseStatus = "Job godkendt på " + this.Name;
              }
              else
              {
                saJobStatus.TerseStatus = JobStatus.DestinationMD5Error;
                saJobStatus.VerboseStatus = "MD5 fejl på " + this.Name;
              }
              File.Delete(str2);
            }
            else
            {
              if (this.Result(str2).Contains("was stored!"))
              {
                saJobStatus.TerseStatus = JobStatus.AlreadyTransferred;
                saJobStatus.VerboseStatus = "Allerede lagret " + this.Name;
              }
              else
              {
                saJobStatus.TerseStatus = JobStatus.TransferError;
                saJobStatus.VerboseStatus = "Overførselsfejl " + this.Name;
              }
              File.Delete(str2);
            }
          }
        }
      }
      catch (Exception ex)
      {
        saJobStatus.TerseStatus = JobStatus.TransferError;
        saJobStatus.VerboseStatus = "Overførselsfejl " + this.Name;
        Program.Events.LogOrNotify(ex);
        e.Result = (object) saJobStatus;
      }
      File.Delete(str1);
      e.Result = (object) saJobStatus;
    }

    protected override void bw_Finalize_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
      SA_JobStatus saJobStatus = new SA_JobStatus(saStorageWorker.job.Status);
      try
      {
        string str = this.tmpPath + "\\ApproveResult" + saStorageWorker.job.UniqueID + ".txt";
        Process process = this.Execute("approve " + saStorageWorker.job.FileName + " " + str);
        while (!process.HasExited)
        {
          Thread.Sleep(1000);
          saStorageWorker.ReportProgress(100);
          if (saStorageWorker.CancellationPending)
          {
            process.Kill();
            File.Delete(str);
            e.Result = (object) saJobStatus;
            return;
          }
        }
        Thread.Sleep(1000);
        if (this.Result(str) == "0")
        {
          saJobStatus.TerseStatus = JobStatus.Approved;
          saJobStatus.VerboseStatus = "Job godkendt på " + this.Name;
        }
        else
        {
          saJobStatus.TerseStatus = JobStatus.ApprovalError;
          saJobStatus.VerboseStatus = "Fejl ved godkendelse på " + this.Name;
        }
        File.Delete(str);
        e.Result = (object) saJobStatus;
      }
      catch (Exception ex)
      {
        saJobStatus.TerseStatus = JobStatus.ApprovalError;
        saJobStatus.VerboseStatus = "Fejl ved godkendelse på " + this.Name;
        Program.Events.LogOrNotify(ex);
        e.Result = (object) saJobStatus;
      }
    }

    public override long Size(string FileName)
    {
      return -1L;
    }
  }
}
