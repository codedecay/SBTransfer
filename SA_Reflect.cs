// Type: SBTransfer.SA_Reflect
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SBTransfer
{
  public static class SA_Reflect
  {
    private static System.Type[] GetSubClasses(Assembly asm, System.Type baseClassType)
    {
      List<System.Type> list = new List<System.Type>();
      foreach (System.Type type in asm.GetTypes())
      {
        if (type.IsSubclassOf(baseClassType))
          list.Add(type);
      }
      return list.ToArray();
    }

    private static System.Type[] GetSubTypesFromAssembly(string typeName)
    {
      return SA_Reflect.GetSubClasses(Assembly.GetExecutingAssembly(), System.Type.GetType(typeName));
    }

    private static object[] GetAttributeList(object Obj)
    {
      object[] customAttributes;
      if (Obj is MemberInfo)
      {
        customAttributes = (Obj as MemberInfo).GetCustomAttributes(typeof (UILabelsAttribute), false);
      }
      else
      {
        Obj.GetType().GetField(Obj.ToString());
        customAttributes = Obj.GetType().GetField(Obj.ToString()).GetCustomAttributes(typeof (UILabelsAttribute), false);
      }
      return customAttributes;
    }

    public static System.Type[] GetStorageTypes()
    {
      return SA_Reflect.GetSubTypesFromAssembly("SBTransfer.Storage");
    }

    public static System.Type[] GetProviderTypes()
    {
      return SA_Reflect.GetSubTypesFromAssembly("SBTransfer.JobProvider");
    }

    public static bool hasUILabels(object Obj)
    {
      return SA_Reflect.GetAttributeList(Obj).Length != 0;
    }

    public static string GetUIName(object Obj)
    {
      string str = "";
      foreach (object obj in SA_Reflect.GetAttributeList(Obj))
        str = (obj as UILabelsAttribute).Name;
      return str;
    }

    public static string GetUIDescription(object Obj)
    {
      string str = "";
      foreach (object obj in SA_Reflect.GetAttributeList(Obj))
        str = (obj as UILabelsAttribute).Description;
      return str;
    }

    public static string GetUIRole(object Obj)
    {
      string str = "";
      foreach (object obj in SA_Reflect.GetAttributeList(Obj))
        str = (obj as UILabelsAttribute).Role;
      return str;
    }

    public static string GetUIGrouping(object Obj)
    {
      string str = "";
      foreach (object obj in SA_Reflect.GetAttributeList(Obj))
        str = (obj as UILabelsAttribute).Grouping;
      return str;
    }

    public static void PopulateNonWriteablePanel(object obj, TableLayoutPanel tlp, ToolTip tt, string role)
    {
      Dictionary<string, List<MemberInfo>> dictionary = new Dictionary<string, List<MemberInfo>>();
      foreach (MemberInfo memberInfo in obj.GetType().GetMembers())
      {
        if (memberInfo.MemberType == MemberTypes.Property && SA_Reflect.hasUILabels((object) memberInfo) && SA_Reflect.GetUIRole((object) memberInfo) == role)
        {
          if (dictionary.ContainsKey(SA_Reflect.GetUIGrouping((object) memberInfo)))
            dictionary[SA_Reflect.GetUIGrouping((object) memberInfo)].Add(memberInfo);
          else
            dictionary.Add(SA_Reflect.GetUIGrouping((object) memberInfo), new List<MemberInfo>()
            {
              memberInfo
            });
        }
      }
      int row1 = 0;
      foreach (KeyValuePair<string, List<MemberInfo>> keyValuePair in dictionary)
      {
        Label label1 = new Label();
        label1.Dock = DockStyle.Fill;
        label1.Text = keyValuePair.Key;
        label1.Font = new Font(label1.Font, FontStyle.Bold);
        int row2 = row1 + 1;
        tlp.Controls.Add((Control) label1, 0, row2);
        row1 = row2 + 1;
        foreach (MemberInfo memberInfo in keyValuePair.Value)
        {
          Label label2 = new Label();
          label2.Dock = DockStyle.Fill;
          label2.Text = SA_Reflect.GetUIName((object) memberInfo);
          Control control = (Control) new Label();
          control.DataBindings.Add("Text", obj, memberInfo.Name);
          control.Dock = DockStyle.Top;
          control.Enabled = false;
          tt.SetToolTip((Control) label2, SA_Reflect.GetUIDescription((object) memberInfo));
          tlp.Controls.Add((Control) label2, 0, row1);
          tlp.Controls.Add(control, 1, row1);
          ++row1;
        }
      }
      tlp.Visible = true;
    }

    public static void PopulatePanel(object obj, TableLayoutPanel tlp, ToolTip tt, bool writeable, string role)
    {
      int row = 0;
      foreach (MemberInfo memberInfo in obj.GetType().GetMembers())
      {
        if (memberInfo.MemberType == MemberTypes.Property && SA_Reflect.hasUILabels((object) memberInfo) && SA_Reflect.GetUIRole((object) memberInfo) == role)
        {
          PropertyInfo property = obj.GetType().GetProperty(memberInfo.Name);
          if (!(property.GetValue(obj, (object[]) null) is IList<Storage>))
          {
            tlp.Visible = false;
            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.Text = SA_Reflect.GetUIName((object) memberInfo);
            Control control = new Control();
            if (property.GetValue(obj, (object[]) null) is int)
            {
              control = (Control) new NumericUpDown();
              control.DataBindings.Add("Value", obj, memberInfo.Name);
            }
            if (property.GetValue(obj, (object[]) null) is string)
            {
              control = (Control) new TextBox();
              control.DataBindings.Add("Text", obj, memberInfo.Name);
            }
            if (property.GetValue(obj, (object[]) null) is bool)
            {
              control = (Control) new CheckBox();
              control.DataBindings.Add("Checked", obj, memberInfo.Name);
              if (memberInfo.Name == "VerifyMD5" && obj is Storage)
              {
                foreach (JobProvider jobProvider in (obj as Storage).JobProviders)
                {
                  if (jobProvider.Input == ProviderInput.Filnavn || jobProvider.Input == ProviderInput.FileSize)
                    control.Enabled = false;
                }
              }
            }
            if (property.GetValue(obj, (object[]) null) is Enum)
            {
              control = (Control) new ComboBox();
              foreach (Enum @enum in Enum.GetValues(property.GetValue(obj, (object[]) null).GetType()))
              {
                if (SA_Reflect.hasUILabels((object) @enum))
                  (control as ComboBox).Items.Add((object) SA_Reflect.GetUIName((object) @enum));
                else
                  (control as ComboBox).Items.Add((object) @enum);
              }
              control.DataBindings.Add("SelectedIndex", obj, memberInfo.Name);
            }
            control.Tag = (object) property;
            control.Dock = DockStyle.Top;
            if (!writeable)
              control.Enabled = false;
            tt.SetToolTip((Control) label, SA_Reflect.GetUIDescription((object) memberInfo));
            tlp.Controls.Add((Control) label, 0, row);
            tlp.Controls.Add(control, 1, row);
            tlp.Visible = true;
          }
        }
        ++row;
      }
    }
  }
}
