// Type: SBTransfer.Form_Info
// Assembly: SBTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA6C0CCA-1F2F-406B-8959-D7EDEC7097A1
// Assembly location: D:\Data\SB\SBTransfer.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SBTransfer
{
  public class Form_Info : Form
  {
    private IContainer components = (IContainer) null;
    private SA_DataObject source;
    private BindingManagerBase bm;
    private TabControl tabControl1;
    private TabPage tabPageContent;
    private TabPage tabPageLog;
    private TabPage tabPageSettings;
    private ContextMenuStrip contextMenuStrip1;
    private TableLayoutPanel tableLayoutPanelSettings;
    private ToolTip toolTip1;
    private TabPage tabPageStatus;
    private DataGridView dataGridView1;
    private SplitContainer splitContainer2;
    private RichTextBox richTextBox1;
    private PictureBox pictureBoxUIImage;
    private TableLayoutPanel tableLayoutPanelStatus;
    private DataGridView dataGridViewContent;
    private Panel panelUpdateSave;
    private Button buttonUpdate;
    private Button buttonSave;

    public Form_Info()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }

    public Form_Info(SA_DataObject infosource)
      : this()
    {
      this.source = infosource;
      this.Text = this.source.Name;
      this.Icon = Icon.FromHandle((this.source.UIBadge as Bitmap).GetHicon());
      this.pictureBoxUIImage.DataBindings.Add("BackgroundImage", (object) this.source, "UIImage");
      this.pictureBoxUIImage.DataBindings.Add("Image", (object) this.source.Status, "VisualStatusOverlay");
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Top = 20;
      this.richTextBox1.DataBindings.Add("Text", (object) this.source.Status, "VerboseStatus");
      if (this.source is Session)
        SA_Reflect.PopulateNonWriteablePanel((object) (this.source as Session).Settings, this.tableLayoutPanelSettings, this.toolTip1, "Indstillinger");
      else
        SA_Reflect.PopulateNonWriteablePanel((object) this.source, this.tableLayoutPanelSettings, this.toolTip1, "Indstillinger");
      SA_Reflect.PopulateNonWriteablePanel((object) this.source, this.tableLayoutPanelStatus, this.toolTip1, "Status");
      if (infosource is SA_BackgroundDataObject)
      {
        (this.source as SA_BackgroundDataObject).UpdateStatus();
        (this.source as SA_BackgroundDataObject).UpdateContent();
        this.tabControl1.TabPages.Remove(this.tabPageLog);
      }
      if (infosource is SA_LoggedDataObject)
      {
        this.dataGridView1.DataSource = (object) (infosource as SA_LoggedDataObject).Log.Entries;
        this.dataGridView1.Columns[0].FillWeight = 15f;
        this.dataGridView1.Columns[1].FillWeight = 15f;
        this.dataGridView1.Columns[2].FillWeight = 70f;
        this.panelUpdateSave.Visible = false;
        this.dataGridViewContent.Dock = DockStyle.Fill;
      }
      if (!(infosource is IContentCarrier))
        this.tabControl1.TabPages.Remove(this.tabPageContent);
      else if (this.source.Content != null)
      {
        this.dataGridViewContent.DataSource = (object) this.source.Content.DefaultView;
        this.bm = this.dataGridViewContent.BindingContext[this.dataGridViewContent.DataSource, this.dataGridViewContent.DataMember];
        this.dataGridViewContent.ContextMenuStrip = new ContextMenuStrip();
        this.dataGridViewContent.MouseUp += new MouseEventHandler(this.dataGridViewContent_MouseUp);
        if (infosource is Session)
        {
          ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Vis Detaljer");
          toolStripMenuItem.Click += new EventHandler(this.tm_Click);
          this.dataGridViewContent.ContextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem);
          this.dataGridViewContent.Columns[0].FillWeight = 15f;
          this.dataGridViewContent.Columns[1].FillWeight = 25f;
          this.dataGridViewContent.Columns[2].FillWeight = 55f;
          this.dataGridViewContent.Columns[3].FillWeight = 5f;
        }
      }
      else
        this.dataGridViewContent.Visible = false;
    }

    private void tm_Click(object sender, EventArgs e)
    {
      new Form_Info(this.dataGridViewContent.ContextMenuStrip.Tag as SA_DataObject).Show((IWin32Window) this);
    }

    private void dataGridViewContent_MouseUp(object sender, MouseEventArgs e)
    {
      if (!(this.source is Session) || e.Button != MouseButtons.Right || this.dataGridViewContent.HitTest(e.X, e.Y).Type != DataGridViewHitTestType.Cell)
        return;
      this.dataGridViewContent.ContextMenuStrip.Tag = (object) (this.source.Content.Rows[this.source.Content.Rows.IndexOf(this.source.Content.Rows.Find(((DataRowView) this.bm.Current).Row[0]))].ItemArray[1] as SA_DataObject);
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
      if (!(this.source is SA_BackgroundDataObject))
        return;
      try
      {
        DataTable content = (this.source as SA_BackgroundDataObject).Content;
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|xml files (*.xml)|*.xml";
        saveFileDialog.FilterIndex = 1;
        Stream stream;
        if (saveFileDialog.ShowDialog() == DialogResult.OK && (stream = saveFileDialog.OpenFile()) != null)
        {
          if (saveFileDialog.FilterIndex == 3)
          {
            content.WriteXml(stream);
          }
          else
          {
            string format = "";
            if (saveFileDialog.FilterIndex == 1)
              format = "{0}\t{1}";
            if (saveFileDialog.FilterIndex == 2)
              format = "{0},{1}";
            StreamWriter streamWriter = new StreamWriter(stream);
            foreach (DataRow dataRow in (InternalDataCollectionBase) content.Rows)
              streamWriter.WriteLine(format, dataRow.ItemArray);
            streamWriter.Close();
          }
          stream.Close();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void buttonUpdate_Click(object sender, EventArgs e)
    {
      if (!(this.source is SA_BackgroundDataObject))
        return;
      (this.source as SA_BackgroundDataObject).UpdateContent();
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
      this.tabControl1 = new TabControl();
      this.tabPageStatus = new TabPage();
      this.splitContainer2 = new SplitContainer();
      this.richTextBox1 = new RichTextBox();
      this.pictureBoxUIImage = new PictureBox();
      this.tableLayoutPanelStatus = new TableLayoutPanel();
      this.tabPageContent = new TabPage();
      this.panelUpdateSave = new Panel();
      this.buttonSave = new Button();
      this.buttonUpdate = new Button();
      this.dataGridViewContent = new DataGridView();
      this.tabPageLog = new TabPage();
      this.dataGridView1 = new DataGridView();
      this.tabPageSettings = new TabPage();
      this.tableLayoutPanelSettings = new TableLayoutPanel();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolTip1 = new ToolTip(this.components);
      this.tabControl1.SuspendLayout();
      this.tabPageStatus.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      //this.pictureBoxUIImage.BeginInit();
      this.tabPageContent.SuspendLayout();
      this.panelUpdateSave.SuspendLayout();
      //this.dataGridViewContent.BeginInit();
      this.tabPageLog.SuspendLayout();
      //this.dataGridView1.BeginInit();
      this.tabPageSettings.SuspendLayout();
      this.SuspendLayout();
      this.tabControl1.Controls.Add((Control) this.tabPageStatus);
      this.tabControl1.Controls.Add((Control) this.tabPageContent);
      this.tabControl1.Controls.Add((Control) this.tabPageLog);
      this.tabControl1.Controls.Add((Control) this.tabPageSettings);
      this.tabControl1.Dock = DockStyle.Fill;
      this.tabControl1.Location = new Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(589, 567);
      this.tabControl1.TabIndex = 3;
      this.tabPageStatus.Controls.Add((Control) this.splitContainer2);
      this.tabPageStatus.Location = new Point(4, 22);
      this.tabPageStatus.Name = "tabPageStatus";
      this.tabPageStatus.Padding = new Padding(3);
      this.tabPageStatus.Size = new Size(581, 541);
      this.tabPageStatus.TabIndex = 3;
      this.tabPageStatus.Text = "Status";
      this.tabPageStatus.UseVisualStyleBackColor = true;
      this.splitContainer2.CausesValidation = false;
      this.splitContainer2.Dock = DockStyle.Fill;
      this.splitContainer2.FixedPanel = FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new Point(3, 3);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = Orientation.Horizontal;
      this.splitContainer2.Panel1.Controls.Add((Control) this.richTextBox1);
      this.splitContainer2.Panel1.Controls.Add((Control) this.pictureBoxUIImage);
      this.splitContainer2.Panel1MinSize = 70;
      this.splitContainer2.Panel2.Controls.Add((Control) this.tableLayoutPanelStatus);
      this.splitContainer2.Size = new Size(575, 535);
      this.splitContainer2.SplitterDistance = 70;
      this.splitContainer2.TabIndex = 7;
      this.richTextBox1.AcceptsTab = true;
      this.richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.richTextBox1.BackColor = SystemColors.Control;
      this.richTextBox1.BorderStyle = BorderStyle.None;
      this.richTextBox1.Location = new Point(71, 3);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new Size(929, 64);
      this.richTextBox1.TabIndex = 7;
      this.richTextBox1.Text = "";
      this.pictureBoxUIImage.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBoxUIImage.Location = new Point(3, 3);
      this.pictureBoxUIImage.Name = "pictureBoxUIImage";
      this.pictureBoxUIImage.Size = new Size(64, 64);
      this.pictureBoxUIImage.SizeMode = PictureBoxSizeMode.CenterImage;
      this.pictureBoxUIImage.TabIndex = 6;
      this.pictureBoxUIImage.TabStop = false;
      this.tableLayoutPanelStatus.AutoScroll = true;
      this.tableLayoutPanelStatus.AutoScrollMinSize = new Size(200, 200);
      this.tableLayoutPanelStatus.ColumnCount = 2;
      this.tableLayoutPanelStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanelStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanelStatus.Dock = DockStyle.Fill;
      this.tableLayoutPanelStatus.Location = new Point(0, 0);
      this.tableLayoutPanelStatus.Name = "tableLayoutPanelStatus";
      this.tableLayoutPanelStatus.RowCount = 1;
      this.tableLayoutPanelStatus.RowStyles.Add(new RowStyle());
      this.tableLayoutPanelStatus.Size = new Size(575, 461);
      this.tableLayoutPanelStatus.TabIndex = 2;
      this.tabPageContent.BackColor = Color.Transparent;
      this.tabPageContent.Controls.Add((Control) this.panelUpdateSave);
      this.tabPageContent.Controls.Add((Control) this.dataGridViewContent);
      this.tabPageContent.Location = new Point(4, 22);
      this.tabPageContent.Name = "tabPageContent";
      this.tabPageContent.Padding = new Padding(3);
      this.tabPageContent.Size = new Size(581, 541);
      this.tabPageContent.TabIndex = 0;
      this.tabPageContent.Text = "Indhold";
      this.tabPageContent.UseVisualStyleBackColor = true;
      this.panelUpdateSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.panelUpdateSave.Controls.Add((Control) this.buttonSave);
      this.panelUpdateSave.Controls.Add((Control) this.buttonUpdate);
      this.panelUpdateSave.Location = new Point(3, 490);
      this.panelUpdateSave.Name = "panelUpdateSave";
      this.panelUpdateSave.Size = new Size(575, 47);
      this.panelUpdateSave.TabIndex = 2;
      this.buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonSave.Location = new Point(473, 14);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new Size(75, 23);
      this.buttonSave.TabIndex = 1;
      this.buttonSave.Text = "Gem";
      this.buttonSave.UseVisualStyleBackColor = true;
      this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
      this.buttonUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.buttonUpdate.Location = new Point(25, 12);
      this.buttonUpdate.Name = "buttonUpdate";
      this.buttonUpdate.Size = new Size(75, 23);
      this.buttonUpdate.TabIndex = 0;
      this.buttonUpdate.Text = "Opdater";
      this.buttonUpdate.UseVisualStyleBackColor = true;
      this.buttonUpdate.Click += new EventHandler(this.buttonUpdate_Click);
      this.dataGridViewContent.AllowUserToAddRows = false;
      this.dataGridViewContent.AllowUserToDeleteRows = false;
      this.dataGridViewContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridViewContent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridViewContent.BackgroundColor = SystemColors.Window;
      this.dataGridViewContent.BorderStyle = BorderStyle.Fixed3D;
      this.dataGridViewContent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
      this.dataGridViewContent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dataGridViewContent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewContent.Location = new Point(0, 0);
      this.dataGridViewContent.Name = "dataGridViewContent";
      this.dataGridViewContent.ReadOnly = true;
      this.dataGridViewContent.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
      this.dataGridViewContent.Size = new Size(578, 485);
      this.dataGridViewContent.TabIndex = 1;
      this.tabPageLog.Controls.Add((Control) this.dataGridView1);
      this.tabPageLog.Location = new Point(4, 22);
      this.tabPageLog.Name = "tabPageLog";
      this.tabPageLog.Padding = new Padding(3);
      this.tabPageLog.Size = new Size(581, 541);
      this.tabPageLog.TabIndex = 1;
      this.tabPageLog.Text = "Log";
      this.tabPageLog.UseVisualStyleBackColor = true;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Dock = DockStyle.Fill;
      this.dataGridView1.Location = new Point(3, 3);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(575, 535);
      this.dataGridView1.TabIndex = 1;
      this.tabPageSettings.Controls.Add((Control) this.tableLayoutPanelSettings);
      this.tabPageSettings.Location = new Point(4, 22);
      this.tabPageSettings.Name = "tabPageSettings";
      this.tabPageSettings.Padding = new Padding(3);
      this.tabPageSettings.Size = new Size(581, 541);
      this.tabPageSettings.TabIndex = 2;
      this.tabPageSettings.Text = "Indstillinger";
      this.tabPageSettings.UseVisualStyleBackColor = true;
      this.tableLayoutPanelSettings.ColumnCount = 2;
      this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanelSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanelSettings.Dock = DockStyle.Fill;
      this.tableLayoutPanelSettings.Location = new Point(3, 3);
      this.tableLayoutPanelSettings.Name = "tableLayoutPanelSettings";
      this.tableLayoutPanelSettings.RowCount = 1;
      this.tableLayoutPanelSettings.RowStyles.Add(new RowStyle());
      this.tableLayoutPanelSettings.Size = new Size(575, 535);
      this.tableLayoutPanelSettings.TabIndex = 0;
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(61, 4);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.ButtonFace;
      this.ClientSize = new Size(589, 567);
      this.Controls.Add((Control) this.tabControl1);
      this.Name = "Form_Info";
      this.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "Info";
      this.tabControl1.ResumeLayout(false);
      this.tabPageStatus.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      //this.pictureBoxUIImage.EndInit();
      this.tabPageContent.ResumeLayout(false);
      this.panelUpdateSave.ResumeLayout(false);
      //this.dataGridViewContent.EndInit();
      this.tabPageLog.ResumeLayout(false);
      //this.dataGridView1.EndInit();
      this.tabPageSettings.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
