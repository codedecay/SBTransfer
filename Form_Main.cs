// Type: SBTransfer.Form_Main
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using SBTransfer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SBTransfer
{
  public class Form_Main : Form
  {
    private IContainer components = (IContainer) null;
    private TableLayoutPanel tableLayoutPanel1;
    private ToolStripMenuItem redigerToolStripMenuItem;
    private ToolStripMenuItem indstillingerToolStripMenuItem;
    private ToolStripButton toolStripButton1;
    protected ToolTip toolTip1;
    private ToolStripButton toolStripButton2;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonSettings;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripButton toolStripButtonLogs;
    private Button buttonUpdate;
    private Button buttonStop;
    private Button buttonRun;

    public Form_Main()
    {
      this.InitializeComponent();
      Program.Events.LogOrNotify("Program startet", EventLogEntryType.Information, CategoryType.AppStartUp, EventIDType.NA);
      this.BuildAll();
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
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.redigerToolStripMenuItem = new ToolStripMenuItem();
      this.indstillingerToolStripMenuItem = new ToolStripMenuItem();
      this.toolTip1 = new ToolTip(this.components);
      this.buttonUpdate = new Button();
      this.buttonStop = new Button();
      this.buttonRun = new Button();
      this.toolStripButton1 = new ToolStripButton();
      this.toolStripButton2 = new ToolStripButton();
      this.toolStrip1 = new ToolStrip();
      this.toolStripButtonLogs = new ToolStripButton();
      this.toolStripButtonSettings = new ToolStripButton();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tableLayoutPanel1.AutoScroll = true;
      this.tableLayoutPanel1.AutoScrollMinSize = new Size(400, 150);
      this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.77778f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.44444f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.77778f));
      this.tableLayoutPanel1.Location = new Point(0, 28);
      this.tableLayoutPanel1.MinimumSize = new Size(100, 200);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.tableLayoutPanel1.Size = new Size(966, 380);
      this.tableLayoutPanel1.TabIndex = 3;
      this.redigerToolStripMenuItem.Name = "redigerToolStripMenuItem";
      this.redigerToolStripMenuItem.Size = new Size(56, 20);
      this.redigerToolStripMenuItem.Text = "Rediger";
      this.indstillingerToolStripMenuItem.Name = "indstillingerToolStripMenuItem";
      this.indstillingerToolStripMenuItem.Size = new Size(152, 22);
      this.indstillingerToolStripMenuItem.Text = "Indstillinger";
      this.buttonUpdate.Anchor = AnchorStyles.Bottom;
      this.buttonUpdate.Image = (Image) Resources.BadgeUpdating;
      this.buttonUpdate.Location = new Point(357, 426);
      this.buttonUpdate.Name = "buttonUpdate";
      this.buttonUpdate.Size = new Size(50, 25);
      this.buttonUpdate.TabIndex = 8;
      this.toolTip1.SetToolTip((Control) this.buttonUpdate, "Opdater lager og joblister");
      this.buttonUpdate.UseVisualStyleBackColor = true;
      this.buttonUpdate.Click += new EventHandler(this.buttonUpdate_Click);
      this.buttonStop.Anchor = AnchorStyles.Bottom;
      this.buttonStop.Image = (Image) Resources.Stop;
      this.buttonStop.Location = new Point(559, 426);
      this.buttonStop.Name = "buttonStop";
      this.buttonStop.Size = new Size(50, 25);
      this.buttonStop.TabIndex = 9;
      this.toolTip1.SetToolTip((Control) this.buttonStop, "Stop alle kørsler og opdateringer");
      this.buttonStop.UseVisualStyleBackColor = true;
      this.buttonStop.Click += new EventHandler(this.ButtonStop_Click);
      this.buttonRun.Anchor = AnchorStyles.Bottom;
      this.buttonRun.Image = (Image) Resources.Play;
      this.buttonRun.Location = new Point(460, 426);
      this.buttonRun.Name = "buttonRun";
      this.buttonRun.Size = new Size(50, 25);
      this.buttonRun.TabIndex = 10;
      this.toolTip1.SetToolTip((Control) this.buttonRun, "Start opdateringer og klargjorte kørsler");
      this.buttonRun.UseVisualStyleBackColor = true;
      this.buttonRun.Click += new EventHandler(this.ButtonPlay_Click);
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new Size(23, 23);
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new Size(23, 23);
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripButtonLogs,
        (ToolStripItem) this.toolStripButtonSettings
      });
      this.toolStrip1.Location = new Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = ToolStripRenderMode.System;
      this.toolStrip1.Size = new Size(968, 25);
      this.toolStrip1.TabIndex = 7;
      this.toolStrip1.Text = "toolStrip1";
      this.toolStripButtonLogs.Alignment = ToolStripItemAlignment.Right;
      this.toolStripButtonLogs.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonLogs.Image = (Image) Resources.Log;
      this.toolStripButtonLogs.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonLogs.Name = "toolStripButtonLogs";
      this.toolStripButtonLogs.Size = new Size(23, 22);
      this.toolStripButtonLogs.Text = "Logs";
      this.toolStripButtonLogs.Click += new EventHandler(this.toolStripButtonLogs_Click);
      this.toolStripButtonSettings.Alignment = ToolStripItemAlignment.Right;
      this.toolStripButtonSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonSettings.DoubleClickEnabled = true;
      this.toolStripButtonSettings.Image = (Image) Resources.Settings;
      this.toolStripButtonSettings.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonSettings.Name = "toolStripButtonSettings";
      this.toolStripButtonSettings.Size = new Size(23, 22);
      this.toolStripButtonSettings.Text = "Indstillinger";
      this.toolStripButtonSettings.Click += new EventHandler(this.toolStripButtonSettings_Click);
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(61, 4);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(968, 461);
      this.Controls.Add((Control) this.buttonRun);
      this.Controls.Add((Control) this.buttonStop);
      this.Controls.Add((Control) this.buttonUpdate);
      this.Controls.Add((Control) this.toolStrip1);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.MinimumSize = new Size(600, 250);
      this.Name = "Form_Main";
      this.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "SA Overførselsprogram";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void BuildAll()
    {
      Setup.Deserialize(Settings.Default.SessionTypePath);
      Program.SessionList = Setup.CreateSessions();
      this.MakeInterface();
    }

    private void MakeInterface()
    {
      this.tableLayoutPanel1.Controls.Clear();
      foreach (Session session in Program.SessionList)
      {
        SessionType settings = session.Settings;
        int row = Setup.SessionTypes.IndexOf(settings);
        TableLayoutPanel tableLayoutPanel1 = this.MakeStoragePanel(settings.Sources);
        this.tableLayoutPanel1.Controls.Add((Control) tableLayoutPanel1, 0, row);
        tableLayoutPanel1.Anchor = tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        Panel panel = new Panel();
        panel.Dock = DockStyle.Top;
        Label label = new Label();
        label.TextAlign = ContentAlignment.TopCenter;
        label.Dock = DockStyle.Top;
        label.Text = settings.Name;
        Font font = new Font(this.Font, FontStyle.Bold);
        label.Font = font;
        panel.Controls.Add((Control) label);
        PictureBox pictureBox = new PictureBox();
        pictureBox.Tag = (object) session;
        pictureBox.Size = new Size(48, 48);
        pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        pictureBox.Dock = DockStyle.Fill;
        panel.Controls.Add((Control) pictureBox);
        pictureBox.DataBindings.Add("BackgroundImage", (object) session, "UIImage");
        pictureBox.DataBindings.Add("Image", (object) session.Status, "VisualStatusOverlay");
        pictureBox.BackgroundImageLayout = ImageLayout.Center;
        pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        pictureBox.Invalidated += new InvalidateEventHandler(this.PicBox_Invalidated);
        pictureBox.MouseUp += new MouseEventHandler(this.PicBox_MouseUp);
        StatusBar statusBar = new StatusBar();
        statusBar.Height = 20;
        statusBar.SizingGrip = false;
        statusBar.DataBindings.Add("Text", (object) session.Status, "VerboseStatus");
        panel.Controls.Add((Control) statusBar);
        this.tableLayoutPanel1.Controls.Add((Control) panel, 1, row);
        TableLayoutPanel tableLayoutPanel2 = this.MakeStoragePanel(settings.Destinations);
        tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.tableLayoutPanel1.Controls.Add((Control) tableLayoutPanel2, 2, row);
      }
    }

    private TableLayoutPanel MakeStoragePanel(List<Storage> StorageList)
    {
      TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      tableLayoutPanel.AutoScrollMinSize = new Size(150, 0);
      tableLayoutPanel.AutoScroll = false;
      tableLayoutPanel.AutoSize = true;
      for (int column = 0; column < StorageList.Count; ++column)
      {
        PictureBox pictureBox = new PictureBox();
        pictureBox.Size = new Size(64, 64);
        pictureBox.Tag = (object) StorageList[column];
        pictureBox.Anchor = AnchorStyles.None;
        pictureBox.DataBindings.Add("BackgroundImage", (object) StorageList[column], "UIImage");
        pictureBox.DataBindings.Add("Image", (object) StorageList[column].Status, "VisualStatusOverlay");
        pictureBox.BackgroundImageLayout = ImageLayout.Center;
        pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        pictureBox.Invalidated += new InvalidateEventHandler(this.PicBox_Invalidated);
        pictureBox.MouseUp += new MouseEventHandler(this.PicBox_MouseUp);
        Label label = new Label();
        label.Text = StorageList[column].Name;
        label.TextAlign = ContentAlignment.TopCenter;
        label.Anchor = AnchorStyles.Top;
        tableLayoutPanel.Controls.Add((Control) label, column, 0);
        tableLayoutPanel.Controls.Add((Control) pictureBox, column, 1);
      }
      return tableLayoutPanel;
    }

    private void PicBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      Point p = new Point(e.X, e.Y);
      Point position = this.PointToClient((sender as Control).PointToScreen(p));
      this.contextMenuStrip1.Items.Clear();
      if ((sender as PictureBox).Tag is Storage)
      {
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Opdater ");
        toolStripMenuItem1.Tag = (sender as PictureBox).Tag;
        this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem1);
        toolStripMenuItem1.Click += new EventHandler(this.Update_Click);
        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Vis Indhold ... ");
        toolStripMenuItem2.Tag = (sender as PictureBox).Tag;
        this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem2);
        toolStripMenuItem2.Click += new EventHandler(this.Info_Click);
        if (((sender as PictureBox).Tag as Storage).JobProviders.Count > 0)
        {
          ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Joblister ");
          this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem3);
          foreach (JobProvider jobProvider in ((sender as PictureBox).Tag as Storage).JobProviders)
          {
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("Vis " + jobProvider.Name);
            toolStripMenuItem4.Image = jobProvider.Status.VisualStatusBadge;
            toolStripMenuItem4.Tag = (object) jobProvider;
            toolStripMenuItem4.Click += new EventHandler(this.Info_Click);
            toolStripMenuItem3.DropDownItems.Add((ToolStripItem) toolStripMenuItem4);
          }
        }
      }
      if ((sender as PictureBox).Tag is Session)
      {
        Session session = (sender as PictureBox).Tag as Session;
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Start");
        this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem1);
        toolStripMenuItem1.Tag = (object) session;
        toolStripMenuItem1.Image = (Image) Resources.Play;
        toolStripMenuItem1.Click += new EventHandler(this.Session_Start_Click);
        if (!(session.Status.TerseStatus == SessionStatus.JobsReady | session.Status.TerseStatus == SessionStatus.UpdatingStoppedByUser | session.Status.TerseStatus == SessionStatus.PreparingJobsStoppedByUser | session.Status.TerseStatus == SessionStatus.RunningStoppedByUser))
          toolStripMenuItem1.Enabled = false;
        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Stop");
        this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem2);
        toolStripMenuItem2.Tag = (object) session;
        toolStripMenuItem2.Image = (Image) Resources.Stop;
        toolStripMenuItem2.Click += new EventHandler(this.Session_Stop_Click);
        if (session.Status.TerseStatus == SessionStatus.UpdatingStoppedByUser | session.Status.TerseStatus == SessionStatus.PreparingJobsStoppedByUser | session.Status.TerseStatus == SessionStatus.RunningStoppedByUser)
          toolStripMenuItem2.Enabled = false;
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Vis Detaljer ... ");
        this.contextMenuStrip1.Items.Add((ToolStripItem) toolStripMenuItem3);
        toolStripMenuItem3.Tag = (sender as PictureBox).Tag;
        toolStripMenuItem3.Click += new EventHandler(this.Info_Click);
      }
      this.contextMenuStrip1.Show((Control) this, position);
    }

    private void Info_Click(object sender, EventArgs e)
    {
      new Form_Info((sender as ToolStripMenuItem).Tag as SA_DataObject).Show((IWin32Window) this);
    }

    private void Update_Click(object sender, EventArgs e)
    {
      if (!((sender as ToolStripMenuItem).Tag is SA_BackgroundDataObject))
        return;
      ((sender as ToolStripMenuItem).Tag as SA_BackgroundDataObject).UpdateStatus();
    }

    private void Session_Start_Click(object sender, EventArgs e)
    {
      if (!((sender as ToolStripMenuItem).Tag is Session))
        return;
      ((sender as ToolStripMenuItem).Tag as Session).Run();
    }

    private void Session_Stop_Click(object sender, EventArgs e)
    {
      if (!((sender as ToolStripMenuItem).Tag is Session))
        return;
      ((sender as ToolStripMenuItem).Tag as Session).Stop();
    }

    private void PicBox_Invalidated(object sender, InvalidateEventArgs e)
    {
      this.toolTip1.SetToolTip((Control) (sender as PictureBox), ((sender as PictureBox).Tag as SA_DataObject).Status.VerboseStatus);
    }

    private void toolStripButtonSettings_Click(object sender, EventArgs e)
    {
      int num = (int) new Form_Settings().ShowDialog((IWin32Window) this);
      this.BuildAll();
    }

    private void toolStripButtonLogs_Click(object sender, EventArgs e)
    {
      new FormLogViewer().Show((IWin32Window) this);
    }

    private void ButtonPlay_Click(object sender, EventArgs e)
    {
      foreach (Session session in Program.SessionList)
        session.Run();
    }

    private void ButtonStop_Click(object sender, EventArgs e)
    {
      foreach (Session session in Program.SessionList)
        session.Stop();
    }

    private void buttonUpdate_Click(object sender, EventArgs e)
    {
    }
  }
}
