// Type: SBTransfer.Program
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SBTransfer
{
  internal static class Program
  {
    public static EventProcessor Events;
    public static List<Session> SessionList;

    [STAThread]
    private static void Main()
    {
      Program.Events = new EventProcessor("SBTransfer");
      Program.SessionList = new List<Session>();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form_Main());
    }
  }
}
