using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ClientAPI;
using ClientAPI.Interfaces;

namespace SBTransfer
{
    
    [UILabels("Bitmagasin", "Det nationale Bitmagasin")]
    public class RemoteStorageBitMag : Storage
    {
        private long freeSpace;
        private long maxFileSize;
        private ClientApiFacade stub;
       
        private Dictionary<string, AutoResetEvent> IncomingEvents = new Dictionary<string, AutoResetEvent>();
        private Dictionary<string, IResponseEventArgs> IncomingResults = new Dictionary<string, IResponseEventArgs>();
        
        [UILabels("Settings", "Den fulde sti til indstillinger")]
        public string SettingsPath { get; set; } 
         
        public RemoteStorageBitMag()
        {
            string directoryName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            this.Name = "Bitmagasin";
            this.SettingsPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),"bitmagsettings.xml");

           
            if (!String.IsNullOrEmpty(SettingsPath))
            {
              ClientAPISettings settings = (ClientAPISettings)SA_Serializer.Deserialize(SettingsPath, typeof(ClientAPISettings));
    //            ClientAPISettings settings =new ClientAPISettings(new FileInfo(SettingsPath));

                stub = ClientApiFacade.GetInstance(settings);
                stub.ResponseEventFromClientApiFacade += stub_ResponseEventFromClientApiFacade;
            }
        }

        private void stub_ResponseEventFromClientApiFacade(IResponseEventArgs args)
        {           
            while (!IncomingEvents.ContainsKey(args.CorrelationID))
            { 
                Thread.Sleep(1000);
            }
            IncomingResults[args.CorrelationID] = args;
            IncomingEvents[args.CorrelationID].Set();
        }

        private AutoResetEvent SetEvent(string correlationID)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false); 
            IncomingEvents.Add(correlationID, resetEvent);
            return resetEvent;
        }
        private IResponseEventArgs GetResults(string correlationID)
        {
            IResponseEventArgs args = IncomingResults[correlationID];
            IncomingEvents.Remove(correlationID);
            IncomingResults.Remove(correlationID);
            return args;
        }


        public RemoteStorageBitMag(string settingsPath)
        {
            this.SettingsPath = settingsPath; 
        }

        protected void VerifySetup(SA_StorageStatus st)
        {
            st.VerboseStatus = this.Name + " afventer";
            st.TerseStatus = StorageStatus.Available;
        }

        protected override void bw_GetContent_DoWork(object sender, DoWorkEventArgs e)
        {
            string ID = stub.GetAllChecksums();
            AutoResetEvent ResetEvent = SetEvent(ID);
            ResetEvent.WaitOne();
            IResponseEventArgs args = GetResults(ID);
            e.Result = (object) args.ReturnValues;           
        }

        protected override void bw_Status_DoWork(object sender, DoWorkEventArgs e)
        {            
            SA_StorageStatus st = new SA_StorageStatus(this.Status);
            this.VerifySetup(st);
            if (stub.Running)
            {
                st.VerboseStatus = this.Name + " klar";
                st.TerseStatus = StorageStatus.Ready;
            }
            else
            {
                st.VerboseStatus = this.Name + " Venter";
                st.TerseStatus = StorageStatus.Unavailable;
            }
            e.Result = (object)st;
        }

        protected override void bw_Verify_DoWork(object sender, DoWorkEventArgs e)
        {
            SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
            SA_JobStatus js = new SA_JobStatus(saStorageWorker.job.Status);
            this.setJobStatusVerified(js); 
            e.Result = (object)js;      
        } 
        

        protected override void bw_Transfer_DoWork(object sender, DoWorkEventArgs e)
        {
            SA_StorageWorker saStorageWorker = sender as SA_StorageWorker;
            SA_JobStatus saJobStatus = new SA_JobStatus(saStorageWorker.job.Status);           
            saJobStatus.VerboseStatus = "Overfører";
            string id = string.Empty;
            
            try
            {
                if (saStorageWorker.job.destination is LocalStorage) //get
                {

                    id = stub.Get(new Filename(saStorageWorker.job.FileName), new DirectoryPath((saStorageWorker.job.destination as LocalStorage).LocalPath));               
                }
                if (saStorageWorker.job.source is LocalStorage) //put
                {
                    id = stub.Put(new FileInfo((saStorageWorker.job.source as LocalStorage).LocalPath + saStorageWorker.job.FileName));                                   
                }
                AutoResetEvent ResetEvent = SetEvent(id);
                ResetEvent.WaitOne();
                IResponseEventArgs args = GetResults(id);
                if (args.RequestSucces)
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
            catch (Exception ex)
            {
                saJobStatus.TerseStatus = JobStatus.TransferError;
                saJobStatus.VerboseStatus = "Overførselsfejl " + this.Name;
                Program.Events.LogOrNotify(ex);
                e.Result = (object)saJobStatus;
            }
            e.Result = (object)saJobStatus;      
        } 

        protected override void bw_Finalize_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = (object)new SA_JobStatus((sender as SA_StorageWorker).job.Status)
            {
                TerseStatus = JobStatus.Approved  
            };       
        }

        public override long Size(string FileName)
        {
            return -1L;
        }
    }
}
