// Type: SBTransfer.LocalStorage
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace SBTransfer
{
    [UILabels("Lokalt lager", "internt/eksternt drev eller netværksdrev med lokal mapning")]
    public class LocalStorage : Storage
    {
        private string localPath;

        [UILabels("Sti", "Den fulde sti til lageret", "Indstillinger")]
        public string LocalPath
        {
            get
            {
                return this.localPath;
            }
            set
            {
                this.localPath = value;
            }
        }

        public LocalStorage()
        {
            this.LocalPath = "c:\\";
            this.Name = "Lokalt lager";
        }

        private void bw_Delete_DoWork(object sender, DoWorkEventArgs e)
        {
            File.Delete(Path.Combine(this.LocalPath, (e.Argument as Job).FileName));
        }

        protected override void bw_GetContent_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists(this.LocalPath))
                return;
            DataTable dataTable = new DataTable("Indhold");
            dataTable.Columns.Add("Navn");
            dataTable.Columns.Add("Størrelse");
            foreach (FileInfo fileInfo in new DirectoryInfo(this.localPath).GetFiles("*.*"))
                dataTable.Rows.Add((object)fileInfo.FullName, (object)fileInfo.Length);
            e.Result = (object)dataTable;
        }

        public string GetMD5(Stream stream)
        {

                long num2 = 0L;
                HashAlgorithm hashAlgorithm = (HashAlgorithm)MD5.Create();
                long length = stream.Length;
                byte[] buffer = new byte[4096];
                int num3 = stream.Read(buffer, 0, buffer.Length);
                long num4 = num2 + (long)num3;

                while (num3 != 0)
                {
                    int inputCount = num3;
                    byte[] numArray = buffer;
                    buffer = new byte[4096];
                    num3 = stream.Read(buffer, 0, buffer.Length);
                    num4 += (long) num3;
                    if (num3 == 0)
                        hashAlgorithm.TransformFinalBlock(numArray, 0, inputCount);
                    else
                        hashAlgorithm.TransformBlock(numArray, 0, inputCount, numArray, 0);
                }
                
                stream.Close();
                return BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToLower();     

        }




        protected override void bw_Status_DoWork(object sender, DoWorkEventArgs e)
        {
            SA_StorageStatus st = new SA_StorageStatus(this.Status);
            try
            {
                if (Directory.Exists(this.LocalPath))
                {
                    st.TerseStatus = StorageStatus.Ready;
                    DriveInfo driveInfo = new DriveInfo(this.LocalPath);
                    st.VerboseStatus = string.Format("{0} ({1})\r\n {2} fri plads", (object)this.LocalPath, (object)((object)driveInfo.DriveFormat).ToString(), (object)SA_Convert.BytesToPrefixedValue(driveInfo.AvailableFreeSpace));
                    this.AddProviderStatus(st);
                }
                else
                {
                    st.TerseStatus = StorageStatus.Unavailable;
                    st.VerboseStatus = string.Format("{0} Kan ikke findes", (object)this.LocalPath);
                }
            }
            catch (Exception ex)
            {
                st.TerseStatus = StorageStatus.Error;
                st.VerboseStatus = string.Format("{0} Kan ikke tilgås \r\n Fejl: {1}", (object)this.LocalPath, (object)e.ToString());
            }
            e.Result = (object)st;
        }

        protected override void bw_Verify_DoWork(object sender, DoWorkEventArgs e)
        {
            SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
            SA_JobStatus js = new SA_JobStatus(saStorageWorker.job.Status);
            if (File.Exists(this.LocalPath + saStorageWorker.job.FileName))
            {
                FileInfo fileInfo = new FileInfo(this.LocalPath + saStorageWorker.job.FileName);
                js.TargetQuantity = fileInfo.Length;
                if (this.VerifyMD5)
                {
                    saStorageWorker.ReportProgress(0, (object)js);
                    int num1 = 0;
                    long num2 = 0L;
                    Stream stream = (Stream)File.OpenRead(this.LocalPath + saStorageWorker.job.FileName);
                    HashAlgorithm hashAlgorithm = (HashAlgorithm)MD5.Create();
                    long length = stream.Length;
                    byte[] buffer = new byte[4096];
                    int num3 = stream.Read(buffer, 0, buffer.Length);
                    long num4 = num2 + (long)num3;
                    while (!saStorageWorker.CancellationPending)
                    {
                        int inputCount = num3;
                        byte[] numArray = buffer;
                        buffer = new byte[4096];
                        num3 = stream.Read(buffer, 0, buffer.Length);
                        num4 += (long)num3;
                        if (num3 == 0)
                            hashAlgorithm.TransformFinalBlock(numArray, 0, inputCount);
                        else
                            hashAlgorithm.TransformBlock(numArray, 0, inputCount, numArray, 0);
                        int percentProgress = (int)((double)num4 * 100.0 / (double)length);
                        if (percentProgress != num1)
                        {
                            js.VerboseStatus = "Verificerer MD5 (" + (object)percentProgress + "%)";
                            saStorageWorker.ReportProgress(percentProgress, (object)js);
                            num1 = percentProgress;
                        }
                        if (num3 == 0)
                        {
                            js.CalculatedMD5 = BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToLower();
                            if (saStorageWorker.job.MD5 == js.CalculatedMD5)
                            {
                                this.setJobStatusVerified(js);
                                goto label_16;
                            }
                            else
                            {
                                this.setJobStatusMD5Error(js);
                                goto label_16;
                            }
                        }
                    }
                    stream.Close();
                    js.VerboseStatus = "afbrudt af bruger";
                    e.Result = (object)js;
                    return;
                }
                else
                    this.setJobStatusVerified(js);
            }
            else
                this.setJobStatusError(js);
        label_16:
            e.Result = (object)js;
        }





        protected override void bw_Transfer_DoWork(object sender, DoWorkEventArgs e)
        {
            SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
            SA_JobStatus saJobStatus1 = new SA_JobStatus(saStorageWorker.job.Status);
            long num1 = 0L;
            int num2 = 0;
            int count = 10000;
            byte[] buffer = new byte[count];
            try
            {
                FileStream fileStream1 = new FileStream((saStorageWorker.job.source as LocalStorage).LocalPath + saStorageWorker.job.FileName, FileMode.Open);
                FileStream fileStream2 = new FileStream((saStorageWorker.job.destination as LocalStorage).LocalPath + saStorageWorker.job.FileName, FileMode.Create);
                long length = fileStream1.Length;
                while (fileStream1.Position < fileStream1.Length)
                {
                    num1 += (long)fileStream1.Read(buffer, 0, count);
                    if (saStorageWorker.CancellationPending)
                    {
                        fileStream1.Close();
                        fileStream2.Close();
                        e.Result = (object)saJobStatus1;
                        return;
                    }
                    else
                    {
                        fileStream2.Write(buffer, 0, buffer.Length);
                        int percentProgress = (int)((double)num1 * 100.0 / (double)length);
                        if (percentProgress != num2)
                        {
                            saJobStatus1.CurrentQuantity = num1;
                            saJobStatus1.VerboseStatus = "Overfører (" + (object)percentProgress + "%)";
                            saStorageWorker.ReportProgress(percentProgress, (object)saJobStatus1);
                            num2 = percentProgress;
                        }
                    }
                }
                saJobStatus1.TerseStatus = JobStatus.Transferred;
                fileStream1.Close();
                fileStream2.Close();
            }
            catch (Exception ex)
            {
                if (ex is IOException && File.Exists((saStorageWorker.job.destination as LocalStorage).LocalPath + saStorageWorker.job.FileName))
                {
                    saJobStatus1.TerseStatus = JobStatus.AlreadyTransferred;
                    SA_JobStatus saJobStatus2 = saJobStatus1;
                    string str = saJobStatus2.VerboseStatus + " (" + saStorageWorker.job.destination.Name + ")";
                    saJobStatus2.VerboseStatus = str;
                }
                else
                    saJobStatus1.TerseStatus = JobStatus.TransferError;
                e.Result = (object)saJobStatus1;
            }
            e.Result = (object)saJobStatus1;
        }

        protected override void bw_Finalize_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = (object)new SA_JobStatus((sender as SA_StorageWorker).job.Status)
            {
                TerseStatus = JobStatus.Approved
            };
        }

        public override long Size(string fileName)
        {
            if (File.Exists(this.LocalPath + fileName))
                return new FileInfo(this.LocalPath + fileName).Length;
            else
                return -1L;
        }

        public override void Delete(Job j)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(this.bw_Delete_DoWork);
            backgroundWorker.RunWorkerAsync((object)j);
        }
    }
}
