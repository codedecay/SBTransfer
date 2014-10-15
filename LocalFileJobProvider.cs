// Type: SBTransfer.LocalFileJobProvider
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace SBTransfer
{
  [UILabels("Simpel Jobliste", "Simpel jobudbyder der læser jobs fra en lokal fil")]
  public class LocalFileJobProvider : JobProvider
  {
    private string filePath;

    [UILabels("Sti", "Sti til jobliste", "Indstillinger")]
    public string FilePath
    {
      get
      {
        return this.filePath;
      }
      set
      {
        this.filePath = value;
      }
    }

    public LocalFileJobProvider()
    {
      this.Name = "Ny JobListe";
      this.filePath = "";
    }

    protected override void bw_GetContent_DoWork(object sender, DoWorkEventArgs e)
    {
      DataTable dataTable = new DataTable("Indhold");
      try
      {
        if (File.Exists(this.FilePath))
        {
          StreamReader streamReader = File.OpenText(this.FilePath);
          switch (this.input)
          {
            case ProviderInput.Filnavn:
              dataTable.Columns.Add("Navn");
              break;
            case ProviderInput.MD5:
              dataTable.Columns.Add("Navn");
              dataTable.Columns.Add("MD5");
              break;
            case ProviderInput.FileSize:
              dataTable.Columns.Add("Navn");
              dataTable.Columns.Add("Størrelse");
              break;
            case ProviderInput.FileSizeAndMD5:
              dataTable.Columns.Add("Navn");
              dataTable.Columns.Add("Størrelse");
              dataTable.Columns.Add("MD5");
              break;
          }
          string str;
          while ((str = streamReader.ReadLine()) != null)
            dataTable.Rows.Add((object[]) str.Split(new char[1]
            {
              ','
            }));
          streamReader.Close();
        }
      }
      catch (Exception ex)
      {
        dataTable.Clear();
      }
      e.Result = (object) dataTable;
    }

    protected override void bw_Status_DoWork(object sender, DoWorkEventArgs e)
    {
      SA_ProviderStatus saProviderStatus = new SA_ProviderStatus();
      try
      {
        saProviderStatus.TerseStatus = ProviderStatus.Error;
        if (File.Exists(this.FilePath))
        {
          if (this.Content != null)
          {
            if (this.Content.Rows.Count > 0)
            {
              saProviderStatus.TerseStatus = ProviderStatus.Ready;
              saProviderStatus.VerboseStatus = string.Format("{0} Klar \r\n {1} Jobs klar ", (object) this.filePath, (object) this.Content.Rows.Count);
            }
            else
              saProviderStatus.VerboseStatus = string.Format("{0} Kan ikke læses eller er tom \r\n ", (object) this.filePath, (object) this.Content.Rows.Count);
          }
        }
        else
        {
          saProviderStatus.VerboseStatus = string.Format("{0} Kan ikke findes \r\n ", (object) this.filePath, (object) this.Content.Rows.Count);
          saProviderStatus.TerseStatus = ProviderStatus.Unavailable;
        }
      }
      catch (Exception ex)
      {
        saProviderStatus.TerseStatus = ProviderStatus.Error;
        saProviderStatus.VerboseStatus = string.Format("{0} Kan ikke tilgås \r\n Fejl: {1}", (object) this.filePath, (object) e.ToString());
      }
      e.Result = (object) saProviderStatus;
    }

    public override void CleanUp()
    {
      if (File.Exists(this.filePath))
        File.Delete(this.filePath);
      this.Content.Clear();
      this.UpdateStatus();
    }

    public override void RemoveEntry(Job job)
    {
      if (!File.Exists(this.filePath))
        return;
      string str1 = this.filePath + ((object) job.MD5).ToString() + Path.GetExtension(this.FilePath);
      TextReader textReader = (TextReader) File.OpenText(this.filePath);
      TextWriter textWriter = (TextWriter) File.CreateText(str1);
      string str2;
      while ((str2 = textReader.ReadLine()) != null)
      {
        if (str2.IndexOf(job.Name) == -1)
          textWriter.WriteLine(str2);
      }
      textWriter.Close();
      textReader.Close();
      File.Delete(this.filePath);
      File.Move(str1, this.filePath);
      if (new FileInfo(this.filePath).Length == 0L)
        this.CleanUp();
    }
  }
}
