// Type: SBTransfer.SA_Serializer
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.IO;
using System.Xml.Serialization;

namespace SBTransfer
{
  public static class SA_Serializer
  {
    public static void Serialize(string filepath, object obj)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filepath));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      FileStream fileStream = new FileStream(filepath, FileMode.Create);
      new XmlSerializer(obj.GetType()).Serialize((Stream) fileStream, obj);
      fileStream.Close();
    }

    public static object Deserialize(string filepath, Type T)
    {
      FileStream fileStream = new FileStream(filepath, FileMode.OpenOrCreate);
      object obj = new XmlSerializer(T).Deserialize((Stream) fileStream);
      fileStream.Close();
      return obj;
    }
  }
}
