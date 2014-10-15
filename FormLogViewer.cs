// Type: SBTransfer.FormLogViewer
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SBTransfer
{
  public class FormLogViewer : Form
  {
    private IContainer components = (IContainer) null;
    private TreeView treeViewNewLogs;
    private TabControl tabControlLogs;
    private TabPage tabPageNewLogs;
    private TabPage tabPageOldLogs;
    private TreeView treeViewOldLogs;
    private ContextMenuStrip cMS_NewLogs;

    public FormLogViewer()
    {
      this.InitializeComponent();
      this.Icon = Icon.FromHandle(Resources.Log.GetHicon());
      this.ReadNewLogs();
      this.ReadOldLogs();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.treeViewNewLogs = new TreeView();
      this.tabControlLogs = new TabControl();
      this.tabPageNewLogs = new TabPage();
      this.tabPageOldLogs = new TabPage();
      this.treeViewOldLogs = new TreeView();
      this.cMS_NewLogs = new ContextMenuStrip(this.components);
      this.tabControlLogs.SuspendLayout();
      this.tabPageNewLogs.SuspendLayout();
      this.tabPageOldLogs.SuspendLayout();
      this.SuspendLayout();
      this.treeViewNewLogs.Dock = DockStyle.Fill;
      this.treeViewNewLogs.Location = new Point(3, 3);
      this.treeViewNewLogs.Name = "treeViewNewLogs";
      this.treeViewNewLogs.Size = new Size(714, 508);
      this.treeViewNewLogs.TabIndex = 0;
      this.treeViewNewLogs.MouseUp += new MouseEventHandler(this.treeViewNewLogs_MouseUp);
      this.tabControlLogs.Controls.Add((Control) this.tabPageNewLogs);
      this.tabControlLogs.Controls.Add((Control) this.tabPageOldLogs);
      this.tabControlLogs.Dock = DockStyle.Fill;
      this.tabControlLogs.Location = new Point(0, 0);
      this.tabControlLogs.Name = "tabControlLogs";
      this.tabControlLogs.SelectedIndex = 0;
      this.tabControlLogs.Size = new Size(728, 540);
      this.tabControlLogs.TabIndex = 1;
      this.tabPageNewLogs.Controls.Add((Control) this.treeViewNewLogs);
      this.tabPageNewLogs.Location = new Point(4, 22);
      this.tabPageNewLogs.Name = "tabPageNewLogs";
      this.tabPageNewLogs.Padding = new Padding(3);
      this.tabPageNewLogs.Size = new Size(720, 514);
      this.tabPageNewLogs.TabIndex = 0;
      this.tabPageNewLogs.Text = "Nye Logs";
      this.tabPageNewLogs.ToolTipText = "Nye, endnu ikke godkendte logs";
      this.tabPageNewLogs.UseVisualStyleBackColor = true;
      this.tabPageOldLogs.Controls.Add((Control) this.treeViewOldLogs);
      this.tabPageOldLogs.Location = new Point(4, 22);
      this.tabPageOldLogs.Name = "tabPageOldLogs";
      this.tabPageOldLogs.Padding = new Padding(3);
      this.tabPageOldLogs.Size = new Size(720, 514);
      this.tabPageOldLogs.TabIndex = 1;
      this.tabPageOldLogs.Text = "Gamle Logs";
      this.tabPageOldLogs.ToolTipText = "Ældre, Godkendte logs";
      this.tabPageOldLogs.UseVisualStyleBackColor = true;
      this.treeViewOldLogs.Dock = DockStyle.Fill;
      this.treeViewOldLogs.Location = new Point(3, 3);
      this.treeViewOldLogs.Name = "treeViewOldLogs";
      this.treeViewOldLogs.Size = new Size(714, 508);
      this.treeViewOldLogs.TabIndex = 1;
      this.cMS_NewLogs.Name = "cMS_NewLogs";
      this.cMS_NewLogs.Size = new Size(61, 4);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(728, 540);
      this.Controls.Add((Control) this.tabControlLogs);
      this.Name = "FormLogViewer";
      this.Text = "Logs";
      this.tabControlLogs.ResumeLayout(false);
      this.tabPageNewLogs.ResumeLayout(false);
      this.tabPageOldLogs.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void ReadNewLogs()
    {
      this.treeViewNewLogs.Nodes.Clear();
      string path = Path.Combine(Application.StartupPath, Settings.Default.NewLogPath);
      if (!Directory.Exists(path))
        return;
      DirectoryInfo directoryInfo1 = new DirectoryInfo(path);
      foreach (DirectoryInfo directoryInfo2 in directoryInfo1.GetDirectories())
      {
        foreach (FileInfo fi in directoryInfo2.GetFiles("*.log"))
          this.treeViewNewLogs.Nodes.Add(this.MakeLogNode((SA_Log) SA_Serializer.Deserialize(fi.FullName, typeof (SA_Log)), fi));
      }
    }

    private void ReadOldLogs()
    {
      this.treeViewOldLogs.Nodes.Clear();
      string path = Path.Combine(Application.StartupPath, Settings.Default.OldLogPath);
      if (!Directory.Exists(path))
        return;
      DirectoryInfo directoryInfo1 = new DirectoryInfo(path);
      foreach (DirectoryInfo directoryInfo2 in directoryInfo1.GetDirectories())
      {
        TreeNode node = new TreeNode(directoryInfo2.Name);
        this.treeViewOldLogs.Nodes.Add(node);
        foreach (FileSystemInfo fileSystemInfo in directoryInfo2.GetFiles("*.log"))
        {
          SA_Log log = (SA_Log) SA_Serializer.Deserialize(fileSystemInfo.FullName, typeof (SA_Log));
          node.Nodes.Add(this.MakeLogNode(log, (FileInfo) null));
        }
      }
    }

    private void AddEntries(TreeNode Log, SA_Log log)
    {
      TreeNode node1 = new TreeNode("Begivenheder");
      foreach (SA_LogEntry saLogEntry in (Collection<SA_LogEntry>) log.Entries)
      {
        TreeNode node2 = new TreeNode(saLogEntry.State);
        node1.Nodes.Add(node2);
      }
      Log.Nodes.Add(node1);
    }

    private void AddSubLogs(TreeNode Log, SA_Log log)
    {
      if (log.SubLogs.Count <= 0)
        return;
      TreeNode node1 = new TreeNode("Jobs");
      foreach (SA_Log log1 in (Collection<SA_Log>) log.SubLogs)
      {
        TreeNode node2 = this.MakeLogNode(log1, (FileInfo) null);
        node1.Nodes.Add(node2);
      }
      Log.Nodes.Add(node1);
    }

    private TreeNode MakeLogNode(SA_Log log, FileInfo fi)
    {
      TreeNode Log = new TreeNode();
      Log.Tag = (object) fi;
      TimeSpan timeSpan = log.End.Subtract(log.Start);
      string str = "";
      Log.Text = string.Concat(new object[4]
      {
        (object) log.Name,
        (object) " (",
        (object) log.Start,
        (object) ")"
      });
      Log.Name = log.Name;
      Log.Nodes.Add(new TreeNode("Start:" + (object) log.Start));
      Log.Nodes.Add(new TreeNode("Slut:" + (object) log.End));
      Log.Nodes.Add(new TreeNode("Varighed:" + (object) timeSpan.Seconds + "s"));
      if (log.Quantity > 0L)
      {
        Log.Nodes.Add(new TreeNode("Størrelse:" + SA_Convert.BytesToPrefixedValue(log.Quantity)));
        if (timeSpan.Seconds > 0)
          str = SA_Convert.BytesToPrefixedValue(Convert.ToInt64((Decimal) (log.Quantity / (long) timeSpan.Seconds)), PrefixType.Binary, OutputForm.Short, SuffixType.Bit, OutputForm.Short) + "/s (" + SA_Convert.BytesToPrefixedValue(Convert.ToInt64((Decimal) (log.Quantity * 3600L / (long) timeSpan.Seconds)), PrefixType.Binary, OutputForm.Short, SuffixType.Byte, OutputForm.Short) + "/time)";
        Log.Nodes.Add(new TreeNode("Effektiv hastighed:" + str));
      }
      this.AddEntries(Log, log);
      this.AddSubLogs(Log, log);
      return Log;
    }

    private void treeViewNewLogs_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.treeViewNewLogs.SelectedNode = this.treeViewNewLogs.GetNodeAt(e.X, e.Y);
      Point point = new Point(e.X, e.Y);
      if (this.treeViewNewLogs.GetNodeAt(point) == null)
        return;
      Point position = this.PointToClient(this.treeViewNewLogs.PointToScreen(point));
      this.cMS_NewLogs.Items.Clear();
      if (this.treeViewNewLogs.SelectedNode.Tag != null)
      {
        if (this.treeViewNewLogs.SelectedNode.Tag is FileInfo)
        {
          ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Godkend ");
          this.cMS_NewLogs.Items.Add((ToolStripItem) toolStripMenuItem);
          toolStripMenuItem.Tag = (object) this.treeViewNewLogs.SelectedNode;
          toolStripMenuItem.Click += new EventHandler(this.ApproveLog);
        }
        this.cMS_NewLogs.Show((Control) this, position);
      }
    }

    private void ApproveLog(object sender, EventArgs e)
    {
      TreeNode treeNode = (sender as ToolStripMenuItem).Tag as TreeNode;
      FileInfo fileInfo = treeNode.Tag as FileInfo;
      string str = Path.Combine(Path.Combine(Path.Combine(Application.StartupPath, Settings.Default.OldLogPath), treeNode.Name), fileInfo.Name);
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(str));
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      File.Copy(fileInfo.FullName, str);
      File.Delete(fileInfo.FullName);
      this.ReadNewLogs();
      this.ReadOldLogs();
    }
  }
}
