// Type: SBTransfer.Form_Settings
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace SBTransfer
{
  public class Form_Settings : Form
  {
    private IContainer components = (IContainer) null;
    private TreeView tV_Settings;
    private SplitContainer splitContainer1;
    private TabControl tabControlSettings;
    private TabPage tabPageProgram;
    private TabPage tabPageSessions;
    private GroupBox groupBoxSessions;
    private Button buttonBrowseSessionTypes;
    private TextBox textBoxSessionTypePath;
    private Label labelSessionTypePath;
    private Button buttonApply;
    private Button buttonCancel;
    private Button buttonOK;
    private ContextMenuStrip cMS_Settings;
    private ErrorProvider errorProvider1;
    private TableLayoutPanel tableLayoutPanel1;
    private ToolTip toolTip1;
    private TextBox textBoxTempFilePath;
    private Label labelTempFiles;
    private TextBox textBoxOldLogPath;
    private Label labelOldLogs;
    private TextBox textBoxNewLogPath;
    private Label labelNewLogs;

    public Form_Settings()
    {
      this.InitializeComponent();
      this.LoadGeneralUserSettings();
      this.Icon = Icon.FromHandle(Resources.Settings.GetHicon());
      this.tV_Settings.ImageList = new ImageList();
      this.tV_Settings.ImageList.Images.Add(Icon.FromHandle(Resources.BadgeSession.GetHicon()));
      this.tV_Settings.ImageList.Images.Add(Icon.FromHandle(Resources.BadgeStorage.GetHicon()));
      this.tV_Settings.ImageList.Images.Add(Resources.Joblist);
      this.tV_Settings.ImageList.Images.Add(Resources.Folder);
      this.BuildTree();
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
      this.tV_Settings = new TreeView();
      this.splitContainer1 = new SplitContainer();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.tabControlSettings = new TabControl();
      this.tabPageProgram = new TabPage();
      this.groupBoxSessions = new GroupBox();
      this.textBoxOldLogPath = new TextBox();
      this.labelOldLogs = new Label();
      this.textBoxNewLogPath = new TextBox();
      this.labelNewLogs = new Label();
      this.textBoxTempFilePath = new TextBox();
      this.labelTempFiles = new Label();
      this.buttonBrowseSessionTypes = new Button();
      this.textBoxSessionTypePath = new TextBox();
      this.labelSessionTypePath = new Label();
      this.tabPageSessions = new TabPage();
      this.buttonApply = new Button();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.cMS_Settings = new ContextMenuStrip(this.components);
      this.errorProvider1 = new ErrorProvider(this.components);
      this.toolTip1 = new ToolTip(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControlSettings.SuspendLayout();
      this.tabPageProgram.SuspendLayout();
      this.groupBoxSessions.SuspendLayout();
      this.tabPageSessions.SuspendLayout();
      //this.errorProvider1.BeginInit();
      this.SuspendLayout();
      this.tV_Settings.Dock = DockStyle.Fill;
      this.tV_Settings.Location = new Point(0, 0);
      this.tV_Settings.Name = "tV_Settings";
      this.tV_Settings.Size = new Size(184, 409);
      this.tV_Settings.TabIndex = 3;
      this.tV_Settings.MouseUp += new MouseEventHandler(this.tV_Settings_MouseUp);
      this.tV_Settings.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.tV_Settings);
      this.splitContainer1.Panel2.Controls.Add((Control) this.tableLayoutPanel1);
      this.splitContainer1.Size = new Size(670, 409);
      this.splitContainer1.SplitterDistance = 184;
      this.splitContainer1.TabIndex = 4;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.64301f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.35699f));
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.Padding = new Padding(10, 0, 13, 0);
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.tableLayoutPanel1.Size = new Size(482, 409);
      this.tableLayoutPanel1.TabIndex = 3;
      this.tabControlSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tabControlSettings.Controls.Add((Control) this.tabPageProgram);
      this.tabControlSettings.Controls.Add((Control) this.tabPageSessions);
      this.tabControlSettings.Location = new Point(0, 1);
      this.tabControlSettings.Name = "tabControlSettings";
      this.tabControlSettings.SelectedIndex = 0;
      this.tabControlSettings.Size = new Size(684, 441);
      this.tabControlSettings.TabIndex = 6;
      this.tabPageProgram.Controls.Add((Control) this.groupBoxSessions);
      this.tabPageProgram.Location = new Point(4, 22);
      this.tabPageProgram.Name = "tabPageProgram";
      this.tabPageProgram.Padding = new Padding(3);
      this.tabPageProgram.Size = new Size(676, 415);
      this.tabPageProgram.TabIndex = 0;
      this.tabPageProgram.Text = "Program";
      this.tabPageProgram.UseVisualStyleBackColor = true;
      this.groupBoxSessions.Controls.Add((Control) this.textBoxOldLogPath);
      this.groupBoxSessions.Controls.Add((Control) this.labelOldLogs);
      this.groupBoxSessions.Controls.Add((Control) this.textBoxNewLogPath);
      this.groupBoxSessions.Controls.Add((Control) this.labelNewLogs);
      this.groupBoxSessions.Controls.Add((Control) this.textBoxTempFilePath);
      this.groupBoxSessions.Controls.Add((Control) this.labelTempFiles);
      this.groupBoxSessions.Controls.Add((Control) this.buttonBrowseSessionTypes);
      this.groupBoxSessions.Controls.Add((Control) this.textBoxSessionTypePath);
      this.groupBoxSessions.Controls.Add((Control) this.labelSessionTypePath);
      this.groupBoxSessions.Location = new Point(0, 6);
      this.groupBoxSessions.Name = "groupBoxSessions";
      this.groupBoxSessions.Size = new Size(680, 430);
      this.groupBoxSessions.TabIndex = 1;
      this.groupBoxSessions.TabStop = false;
      this.groupBoxSessions.Text = "Jobs og Jobgrupper";
      this.textBoxOldLogPath.Location = new Point(175, 217);
      this.textBoxOldLogPath.Name = "textBoxOldLogPath";
      this.textBoxOldLogPath.Size = new Size(389, 20);
      this.textBoxOldLogPath.TabIndex = 10;
      this.labelOldLogs.AutoSize = true;
      this.labelOldLogs.Location = new Point(54, 222);
      this.labelOldLogs.Name = "labelOldLogs";
      this.labelOldLogs.Size = new Size(59, 13);
      this.labelOldLogs.TabIndex = 9;
      this.labelOldLogs.Text = "Ældre logs:";
      this.toolTip1.SetToolTip((Control) this.labelOldLogs, "STi til ældre, godkendte logs");
      this.textBoxNewLogPath.Location = new Point(175, 164);
      this.textBoxNewLogPath.Name = "textBoxNewLogPath";
      this.textBoxNewLogPath.Size = new Size(389, 20);
      this.textBoxNewLogPath.TabIndex = 7;
      this.labelNewLogs.AutoSize = true;
      this.labelNewLogs.Location = new Point(54, 169);
      this.labelNewLogs.Name = "labelNewLogs";
      this.labelNewLogs.Size = new Size(55, 13);
      this.labelNewLogs.TabIndex = 6;
      this.labelNewLogs.Text = "Nye Logs:";
      this.toolTip1.SetToolTip((Control) this.labelNewLogs, "Sti til nye, endnu ikke godkendte logs");
      this.textBoxTempFilePath.Location = new Point(175, 111);
      this.textBoxTempFilePath.Name = "textBoxTempFilePath";
      this.textBoxTempFilePath.Size = new Size(389, 20);
      this.textBoxTempFilePath.TabIndex = 4;
      this.labelTempFiles.AutoSize = true;
      this.labelTempFiles.Location = new Point(54, 115);
      this.labelTempFiles.Name = "labelTempFiles";
      this.labelTempFiles.Size = new Size(84, 13);
      this.labelTempFiles.TabIndex = 3;
      this.labelTempFiles.Text = "Temporære filer:";
      this.toolTip1.SetToolTip((Control) this.labelTempFiles, "Sti til temporære filer (logs for aktive kørsler m.v.)");
      this.buttonBrowseSessionTypes.Location = new Point(595, 56);
      this.buttonBrowseSessionTypes.Name = "buttonBrowseSessionTypes";
      this.buttonBrowseSessionTypes.Size = new Size(31, 23);
      this.buttonBrowseSessionTypes.TabIndex = 2;
      this.buttonBrowseSessionTypes.Text = "...";
      this.buttonBrowseSessionTypes.UseVisualStyleBackColor = true;
      this.buttonBrowseSessionTypes.Click += new EventHandler(this.buttonBrowse_Click);
      this.textBoxSessionTypePath.Location = new Point(175, 58);
      this.textBoxSessionTypePath.Name = "textBoxSessionTypePath";
      this.textBoxSessionTypePath.Size = new Size(389, 20);
      this.textBoxSessionTypePath.TabIndex = 1;
      this.labelSessionTypePath.AutoSize = true;
      this.labelSessionTypePath.Location = new Point(54, 61);
      this.labelSessionTypePath.Name = "labelSessionTypePath";
      this.labelSessionTypePath.Size = new Size(115, 13);
      this.labelSessionTypePath.TabIndex = 0;
      this.labelSessionTypePath.Text = "Anvend jobgrupper fra:";
      this.tabPageSessions.Controls.Add((Control) this.splitContainer1);
      this.tabPageSessions.Location = new Point(4, 22);
      this.tabPageSessions.Name = "tabPageSessions";
      this.tabPageSessions.Padding = new Padding(3);
      this.tabPageSessions.Size = new Size(676, 415);
      this.tabPageSessions.TabIndex = 1;
      this.tabPageSessions.Text = "Jobgrupper";
      this.tabPageSessions.UseVisualStyleBackColor = true;
      this.buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonApply.Location = new Point(597, 448);
      this.buttonApply.Name = "buttonApply";
      this.buttonApply.Size = new Size(75, 23);
      this.buttonApply.TabIndex = 7;
      this.buttonApply.Text = "Anvend";
      this.buttonApply.UseVisualStyleBackColor = true;
      this.buttonApply.Click += new EventHandler(this.buttonApply_Click);
      this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonCancel.Location = new Point(516, 448);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 8;
      this.buttonCancel.Text = "Fortryd";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonOK.ImageAlign = ContentAlignment.MiddleLeft;
      this.buttonOK.Location = new Point(435, 448);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(75, 23);
      this.buttonOK.TabIndex = 9;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.cMS_Settings.Name = "contextMenuStrip1";
      this.cMS_Settings.Size = new Size(61, 4);
      this.errorProvider1.ContainerControl = (ContainerControl) this;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(684, 483);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonApply);
      this.Controls.Add((Control) this.tabControlSettings);
      this.Name = "Form_Settings";
      this.Text = "Indstillinger";
      this.Load += new EventHandler(this.Form_Settings_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tabControlSettings.ResumeLayout(false);
      this.tabPageProgram.ResumeLayout(false);
      this.groupBoxSessions.ResumeLayout(false);
      this.groupBoxSessions.PerformLayout();
      this.tabPageSessions.ResumeLayout(false);
      //this.errorProvider1.EndInit();
      this.ResumeLayout(false);
    }

    private void BuildTree()
    {
      this.tV_Settings.Nodes.Clear();
      foreach (SessionType sessionType in Setup.SessionTypes)
        this.tV_Settings.Nodes.Add(new TreeNode(sessionType.Name)
        {
          Tag = (object) sessionType,
          ImageIndex = 0,
          SelectedImageIndex = 0,
          Nodes = {
            this.BuildSubTree(sessionType.Sources, "Kilder"),
            this.BuildSubTree(sessionType.Destinations, "Destinationer")
          }
        });
    }

    private TreeNode BuildSubTree(List<Storage> List, string name)
    {
      TreeNode treeNode = new TreeNode(name);
      treeNode.Tag = (object) List;
      treeNode.ImageIndex = 3;
      treeNode.SelectedImageIndex = 3;
      foreach (Storage storage in List)
      {
        TreeNode node = new TreeNode(storage.Name);
        node.Tag = (object) storage;
        node.ImageIndex = 1;
        node.SelectedImageIndex = 1;
        treeNode.Nodes.Add(node);
        if (treeNode.Text == "Kilder")
          node.Nodes.Add(this.BuildSubTree(storage.JobProviders, "JobUdbydere"));
      }
      return treeNode;
    }

    private TreeNode BuildSubTree(List<JobProvider> List, string name)
    {
      TreeNode treeNode = new TreeNode(name);
      treeNode.Tag = (object) List;
      treeNode.ImageIndex = 3;
      treeNode.SelectedImageIndex = 3;
      foreach (JobProvider jobProvider in List)
        treeNode.Nodes.Add(new TreeNode(jobProvider.Name)
        {
          Tag = (object) jobProvider,
          ImageIndex = 2,
          SelectedImageIndex = 2
        });
      return treeNode;
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.tableLayoutPanel1.Controls.Clear();
      this.tableLayoutPanel1.RowCount = 1;
      if (this.tV_Settings.SelectedNode.Tag == null)
        return;
      SA_Reflect.PopulatePanel(this.tV_Settings.SelectedNode.Tag, this.tableLayoutPanel1, this.toolTip1, true, "Indstillinger");
      foreach (Control control in (ArrangedElementCollection) this.tableLayoutPanel1.Controls)
      {
        if (control is ComboBox)
          (control as ComboBox).SelectedValueChanged += new EventHandler(this.ComboBoxValueChanged);
        control.Validating += new CancelEventHandler(this.Ctrl_Validating);
      }
    }

    private void ComboBoxValueChanged(object sender, EventArgs e)
    {
      if (!((sender as ComboBox).Tag is PropertyInfo))
        return;
      ((sender as ComboBox).Tag as PropertyInfo).SetValue(this.tV_Settings.SelectedNode.Tag, (object) (sender as ComboBox).SelectedIndex, (object[]) null);
    }

    private void Ctrl_Validating(object sender, CancelEventArgs e)
    {
      if (sender is TextBox && string.IsNullOrEmpty((sender as TextBox).Text))
      {
        this.errorProvider1.SetError(sender as Control, "Strengen må ikke være tom");
        e.Cancel = true;
      }
      if (e.Cancel)
      {
        this.buttonApply.Enabled = false;
        this.buttonOK.Enabled = false;
      }
      else
      {
        this.buttonApply.Enabled = true;
        this.buttonOK.Enabled = true;
      }
    }

    private void tV_Settings_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.tV_Settings.SelectedNode = this.tV_Settings.GetNodeAt(e.X, e.Y);
      Point point = new Point(e.X, e.Y);
      if (this.tV_Settings.GetNodeAt(point) == null)
        return;
      Point position = this.PointToClient(this.tV_Settings.PointToScreen(point));
      this.cMS_Settings.Items.Clear();
      if (this.tV_Settings.SelectedNode.Tag != null)
      {
        if (this.tV_Settings.SelectedNode.Tag is SessionType)
        {
          ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Slet ");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem1);
          toolStripMenuItem1.Click += new EventHandler(this.MenuStrip_DeleteSessionClicked);
          ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Dupliker Session");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem2);
          toolStripMenuItem2.Click += new EventHandler(this.MenuStrip_DuplicateSessionClicked);
          ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Tilføj Session");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem3);
          toolStripMenuItem3.Click += new EventHandler(this.MenuStrip_AddSessionClicked);
        }
        if (this.tV_Settings.SelectedNode.Tag is List<Storage>)
        {
          ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Tilføj ");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem1);
          foreach (System.Type type in SA_Reflect.GetStorageTypes())
          {
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(SA_Reflect.GetUIName((object) type));
            toolStripMenuItem2.Tag = (object) type;
            toolStripMenuItem1.DropDownItems.Add((ToolStripItem) toolStripMenuItem2);
          }
          toolStripMenuItem1.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.MenuStrip_AddStorageClicked);
        }
        if (this.tV_Settings.SelectedNode.Tag is Storage)
        {
          ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Slet ");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem);
          toolStripMenuItem.Click += new EventHandler(this.MenuStrip_DeleteStorageClicked);
        }
        if (this.tV_Settings.SelectedNode.Tag is JobProvider)
        {
          ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Slet ");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem);
          toolStripMenuItem.Click += new EventHandler(this.MenuStrip_DeleteJobProviderClicked);
        }
        if (this.tV_Settings.SelectedNode.Tag is List<JobProvider>)
        {
          ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Tilføj ");
          this.cMS_Settings.Items.Add((ToolStripItem) toolStripMenuItem1);
          foreach (System.Type type in SA_Reflect.GetProviderTypes())
          {
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(SA_Reflect.GetUIName((object) type));
            toolStripMenuItem2.Tag = (object) type;
            toolStripMenuItem1.DropDownItems.Add((ToolStripItem) toolStripMenuItem2);
          }
          toolStripMenuItem1.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.MenuStrip_AddProviderClicked);
        }
      }
      this.cMS_Settings.Show((Control) this, position);
    }

    private void MenuStrip_AddStorageClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      Storage storage = Assembly.GetExecutingAssembly().CreateInstance((e.ClickedItem.Tag as System.Type).FullName) as Storage;
      (this.tV_Settings.SelectedNode.Tag as List<Storage>).Add(storage);
      TreeNode node = new TreeNode(storage.Name);
      node.Tag = (object) storage;
      this.tV_Settings.SelectedNode.Nodes.Add(node);
      if (!(this.tV_Settings.SelectedNode.Text == "Kilder"))
        return;
      node.Nodes.Add(this.BuildSubTree(storage.JobProviders, "Jobudbydere"));
    }

    private void MenuStrip_DeleteStorageClicked(object sender, EventArgs e)
    {
      (this.tV_Settings.SelectedNode.Parent.Tag as List<Storage>).Remove(this.tV_Settings.SelectedNode.Tag as Storage);
      this.tV_Settings.SelectedNode.Remove();
    }

    private void MenuStrip_AddProviderClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      JobProvider jobProvider = Assembly.GetExecutingAssembly().CreateInstance((e.ClickedItem.Tag as System.Type).FullName) as JobProvider;
      (this.tV_Settings.SelectedNode.Tag as List<JobProvider>).Add(jobProvider);
      this.tV_Settings.SelectedNode.Nodes.Add(new TreeNode(jobProvider.Name)
      {
        Tag = (object) jobProvider
      });
    }

    private void MenuStrip_DeleteJobProviderClicked(object sender, EventArgs e)
    {
      (this.tV_Settings.SelectedNode.Parent.Tag as List<JobProvider>).Remove(this.tV_Settings.SelectedNode.Tag as JobProvider);
      this.tV_Settings.SelectedNode.Remove();
    }

    private void MenuStrip_AddSessionClicked(object sender, EventArgs e)
    {
      SessionType sessionType = new SessionType();
      Setup.SessionTypes.Add(sessionType);
      TreeNode node = new TreeNode(sessionType.Name);
      node.Tag = (object) sessionType;
      this.tV_Settings.Nodes.Add(node);
      node.Nodes.Add(this.BuildSubTree(sessionType.Sources, "Kilder"));
      node.Nodes.Add(this.BuildSubTree(sessionType.Destinations, "Destinationer"));
    }

    private void MenuStrip_DuplicateSessionClicked(object sender, EventArgs e)
    {
      SessionType sessionType = (this.tV_Settings.SelectedNode.Tag as SessionType).Clone();
      Setup.SessionTypes.Add(sessionType);
      TreeNode node = new TreeNode(sessionType.Name);
      node.Tag = (object) sessionType;
      this.tV_Settings.Nodes.Add(node);
      node.Nodes.Add(this.BuildSubTree(sessionType.Sources, "Kilder"));
      node.Nodes.Add(this.BuildSubTree(sessionType.Destinations, "Destinationer"));
    }

    private void MenuStrip_DeleteSessionClicked(object sender, EventArgs e)
    {
      Setup.SessionTypes.Remove(this.tV_Settings.SelectedNode.Tag as SessionType);
      this.tV_Settings.SelectedNode.Remove();
    }

    private void Form_Settings_Load(object sender, EventArgs e)
    {
    }

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxSessionTypePath.Text = openFileDialog.FileName;
    }

    private void SaveGeneralUserSettings()
    {
      Settings.Default.SessionTypePath = this.textBoxSessionTypePath.Text;
      Settings.Default.TempFilePath = this.textBoxTempFilePath.Text;
      Settings.Default.NewLogPath = this.textBoxNewLogPath.Text;
      Settings.Default.OldLogPath = this.textBoxOldLogPath.Text;
      Settings.Default.Save();
    }

    private void LoadGeneralUserSettings()
    {
      this.textBoxTempFilePath.Text = Settings.Default.TempFilePath;
      this.textBoxNewLogPath.Text = Settings.Default.NewLogPath;
      this.textBoxOldLogPath.Text = Settings.Default.OldLogPath;
      this.textBoxSessionTypePath.Text = Settings.Default.SessionTypePath;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.buttonApply_Click(sender, e);
      this.Close();
    }

    private void buttonApply_Click(object sender, EventArgs e)
    {
      this.SaveGeneralUserSettings();
      if (this.tabControlSettings.SelectedTab.Name == "tabPageProgram")
      {
        Setup.Deserialize(Settings.Default.SessionTypePath);
        this.tableLayoutPanel1.Controls.Clear();
        this.BuildTree();
      }
      if (!(this.tabControlSettings.SelectedTab.Name == "tabPageSessions"))
        return;
      Setup.Serialize(Settings.Default.SessionTypePath);
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.LoadGeneralUserSettings();
      Setup.Deserialize(Settings.Default.SessionTypePath);
      this.tableLayoutPanel1.Controls.Clear();
      this.BuildTree();
    }
  }
}
