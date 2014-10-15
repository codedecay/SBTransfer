// Type: SBTransfer.UILabelsAttribute
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;

namespace SBTransfer
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  public class UILabelsAttribute : Attribute
  {
    private string name;
    private string description;
    private string role;
    private string grouping;

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public string Description
    {
      get
      {
        return this.description;
      }
    }

    public string Role
    {
      get
      {
        return this.role;
      }
    }

    public string Grouping
    {
      get
      {
        return this.grouping;
      }
    }

    public UILabelsAttribute(string name)
      : this(name, "", "", "")
    {
    }

    public UILabelsAttribute(string name, string description)
      : this(name, description, "", "")
    {
    }

    public UILabelsAttribute(string name, string description, string role)
      : this(name, description, role, "")
    {
    }

    public UILabelsAttribute(string name, string description, string role, string grouping)
    {
      this.name = name;
      this.description = !(description != "") ? name : description;
      this.role = role;
      this.grouping = grouping;
    }
  }
}
