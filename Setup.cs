// Type: SBTransfer.Setup
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SBTransfer
{
  public static class Setup
  {
    public static List<SessionType> SessionTypes = new List<SessionType>();

    static Setup()
    {
    }

    public static void Serialize(string filepath)
    {
      SA_Serializer.Serialize(filepath, (object) Setup.SessionTypes);
    }

    public static void Deserialize(string filepath)
    {
      try
      {
        Setup.SessionTypes = (List<SessionType>) SA_Serializer.Deserialize(filepath, typeof (List<SessionType>));
      }
      catch (Exception ex)
      {
      }
      if (Setup.SessionTypes.Count != 0)
        return;
      Setup.SessionTypes.Add(new SessionType());
    }

    public static List<Session> CreateSessions()
    {
      string tmpLogPath = Path.Combine(Application.StartupPath, Settings.Default.TempFilePath);
      string finalLogPath = Path.Combine(Application.StartupPath, Settings.Default.NewLogPath);
      List<Session> list = new List<Session>();
      foreach (SessionType ST in Setup.SessionTypes)
        list.Add(new Session(ST, tmpLogPath, finalLogPath));
      return list;
    }
  }
}
