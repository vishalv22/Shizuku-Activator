namespace ShizukuActivator;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        instructionLabel = new Label();
        readmeLinkLabel = new LinkLabel();
        detectButton = new Button();
        activateButton = new Button();
        browseButton = new Button();
        copyButton = new Button();
        adbLabel = new Label();
        adbPathValueLabel = new Label();
        adbSourceLabel = new Label();
        adbSourceValueLabel = new Label();
        summaryLabel = new Label();
        summaryValueLabel = new Label();
        deviceListView = new ListView();
        idColumnHeader = new ColumnHeader();
        nameColumnHeader = new ColumnHeader();
        modelColumnHeader = new ColumnHeader();
        stateColumnHeader = new ColumnHeader();
        selectedDeviceTitleLabel = new Label();
        deviceLabel = new Label();
        deviceValueLabel = new Label();
        nameLabel = new Label();
        nameValueLabel = new Label();
        modelLabel = new Label();
        modelValueLabel = new Label();
        stateLabel = new Label();
        stateValueLabel = new Label();
        detailsTextBox = new TextBox();
        statusStrip = new StatusStrip();
        statusStripLabel = new ToolStripStatusLabel();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // instructionLabel
        // 
        instructionLabel.AutoSize = true;
        instructionLabel.Location = new Point(18, 20);
        instructionLabel.Name = "instructionLabel";
        instructionLabel.Size = new Size(354, 15);
        instructionLabel.TabIndex = 0;
        instructionLabel.Text = "Set ADB Folder if auto-detect does not work. For proper steps,";
        // 
        // readmeLinkLabel
        // 
        readmeLinkLabel.AutoSize = true;
        readmeLinkLabel.Location = new Point(378, 20);
        readmeLinkLabel.Name = "readmeLinkLabel";
        readmeLinkLabel.Size = new Size(56, 15);
        readmeLinkLabel.TabIndex = 1;
        readmeLinkLabel.TabStop = true;
        readmeLinkLabel.Text = "Click Here";
        readmeLinkLabel.LinkClicked += ReadmeLinkLabel_LinkClicked;
        // 
        // detectButton
        // 
        detectButton.Location = new Point(18, 53);
        detectButton.Name = "detectButton";
        detectButton.Size = new Size(92, 31);
        detectButton.TabIndex = 2;
        detectButton.Text = "Detect";
        detectButton.UseVisualStyleBackColor = true;
        detectButton.Click += DetectButton_Click;
        // 
        // activateButton
        // 
        activateButton.Location = new Point(116, 53);
        activateButton.Name = "activateButton";
        activateButton.Size = new Size(118, 31);
        activateButton.TabIndex = 3;
        activateButton.Text = "Activate Shizuku";
        activateButton.UseVisualStyleBackColor = true;
        activateButton.Click += ActivateButton_Click;
        // 
        // browseButton
        // 
        browseButton.Location = new Point(240, 53);
        browseButton.Name = "browseButton";
        browseButton.Size = new Size(108, 31);
        browseButton.TabIndex = 4;
        browseButton.Text = "Set ADB Folder";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += BrowseButton_Click;
        // 
        // copyButton
        // 
        copyButton.Location = new Point(354, 53);
        copyButton.Name = "copyButton";
        copyButton.Size = new Size(91, 31);
        copyButton.TabIndex = 5;
        copyButton.Text = "Copy";
        copyButton.UseVisualStyleBackColor = true;
        copyButton.Click += CopyButton_Click;
        // 
        // adbLabel
        // 
        adbLabel.AutoSize = true;
        adbLabel.Location = new Point(18, 102);
        adbLabel.Name = "adbLabel";
        adbLabel.Size = new Size(56, 15);
        adbLabel.TabIndex = 6;
        adbLabel.Text = "ADB path";
        // 
        // adbPathValueLabel
        // 
        adbPathValueLabel.AutoEllipsis = true;
        adbPathValueLabel.Location = new Point(104, 102);
        adbPathValueLabel.Name = "adbPathValueLabel";
        adbPathValueLabel.Size = new Size(582, 18);
        adbPathValueLabel.TabIndex = 7;
        adbPathValueLabel.Text = "Checking...";
        // 
        // adbSourceLabel
        // 
        adbSourceLabel.AutoSize = true;
        adbSourceLabel.Location = new Point(18, 125);
        adbSourceLabel.Name = "adbSourceLabel";
        adbSourceLabel.Size = new Size(43, 15);
        adbSourceLabel.TabIndex = 8;
        adbSourceLabel.Text = "Source";
        // 
        // adbSourceValueLabel
        // 
        adbSourceValueLabel.AutoEllipsis = true;
        adbSourceValueLabel.Location = new Point(104, 125);
        adbSourceValueLabel.Name = "adbSourceValueLabel";
        adbSourceValueLabel.Size = new Size(582, 18);
        adbSourceValueLabel.TabIndex = 9;
        adbSourceValueLabel.Text = "-";
        // 
        // summaryLabel
        // 
        summaryLabel.AutoSize = true;
        summaryLabel.Location = new Point(18, 148);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Size = new Size(55, 15);
        summaryLabel.TabIndex = 10;
        summaryLabel.Text = "Summary";
        // 
        // summaryValueLabel
        // 
        summaryValueLabel.AutoSize = true;
        summaryValueLabel.Location = new Point(104, 148);
        summaryValueLabel.Name = "summaryValueLabel";
        summaryValueLabel.Size = new Size(55, 15);
        summaryValueLabel.TabIndex = 11;
        summaryValueLabel.Text = "Checking";
        // 
        // deviceListView
        // 
        deviceListView.Columns.AddRange(new ColumnHeader[] { idColumnHeader, nameColumnHeader, modelColumnHeader, stateColumnHeader });
        deviceListView.FullRowSelect = true;
        deviceListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        deviceListView.Location = new Point(18, 178);
        deviceListView.MultiSelect = false;
        deviceListView.Name = "deviceListView";
        deviceListView.Size = new Size(462, 162);
        deviceListView.TabIndex = 12;
        deviceListView.UseCompatibleStateImageBehavior = false;
        deviceListView.View = View.Details;
        deviceListView.SelectedIndexChanged += DeviceListView_SelectedIndexChanged;
        // 
        // idColumnHeader
        // 
        idColumnHeader.Text = "Device";
        idColumnHeader.Width = 120;
        // 
        // nameColumnHeader
        // 
        nameColumnHeader.Text = "Name";
        nameColumnHeader.Width = 120;
        // 
        // modelColumnHeader
        // 
        modelColumnHeader.Text = "Model";
        modelColumnHeader.Width = 140;
        // 
        // stateColumnHeader
        // 
        stateColumnHeader.Text = "State";
        stateColumnHeader.Width = 95;
        // 
        // selectedDeviceTitleLabel
        // 
        selectedDeviceTitleLabel.AutoSize = true;
        selectedDeviceTitleLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        selectedDeviceTitleLabel.Location = new Point(493, 178);
        selectedDeviceTitleLabel.Name = "selectedDeviceTitleLabel";
        selectedDeviceTitleLabel.Size = new Size(106, 19);
        selectedDeviceTitleLabel.TabIndex = 13;
        selectedDeviceTitleLabel.Text = "Selected Device";
        // 
        // deviceLabel
        // 
        deviceLabel.AutoSize = true;
        deviceLabel.Location = new Point(493, 211);
        deviceLabel.Name = "deviceLabel";
        deviceLabel.Size = new Size(42, 15);
        deviceLabel.TabIndex = 14;
        deviceLabel.Text = "Device";
        // 
        // deviceValueLabel
        // 
        deviceValueLabel.AutoEllipsis = true;
        deviceValueLabel.Location = new Point(549, 211);
        deviceValueLabel.Name = "deviceValueLabel";
        deviceValueLabel.Size = new Size(143, 18);
        deviceValueLabel.TabIndex = 15;
        deviceValueLabel.Text = "-";
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(493, 238);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(39, 15);
        nameLabel.TabIndex = 16;
        nameLabel.Text = "Name";
        // 
        // nameValueLabel
        // 
        nameValueLabel.AutoEllipsis = true;
        nameValueLabel.Location = new Point(549, 238);
        nameValueLabel.Name = "nameValueLabel";
        nameValueLabel.Size = new Size(143, 18);
        nameValueLabel.TabIndex = 17;
        nameValueLabel.Text = "-";
        // 
        // modelLabel
        // 
        modelLabel.AutoSize = true;
        modelLabel.Location = new Point(493, 265);
        modelLabel.Name = "modelLabel";
        modelLabel.Size = new Size(39, 15);
        modelLabel.TabIndex = 18;
        modelLabel.Text = "Model";
        // 
        // modelValueLabel
        // 
        modelValueLabel.AutoEllipsis = true;
        modelValueLabel.Location = new Point(549, 265);
        modelValueLabel.Name = "modelValueLabel";
        modelValueLabel.Size = new Size(143, 18);
        modelValueLabel.TabIndex = 19;
        modelValueLabel.Text = "-";
        // 
        // stateLabel
        // 
        stateLabel.AutoSize = true;
        stateLabel.Location = new Point(493, 292);
        stateLabel.Name = "stateLabel";
        stateLabel.Size = new Size(32, 15);
        stateLabel.TabIndex = 20;
        stateLabel.Text = "State";
        // 
        // stateValueLabel
        // 
        stateValueLabel.AutoEllipsis = true;
        stateValueLabel.Location = new Point(549, 292);
        stateValueLabel.Name = "stateValueLabel";
        stateValueLabel.Size = new Size(143, 18);
        stateValueLabel.TabIndex = 21;
        stateValueLabel.Text = "-";
        // 
        // detailsTextBox
        // 
        detailsTextBox.Location = new Point(18, 355);
        detailsTextBox.Multiline = true;
        detailsTextBox.Name = "detailsTextBox";
        detailsTextBox.ReadOnly = true;
        detailsTextBox.ScrollBars = ScrollBars.Vertical;
        detailsTextBox.Size = new Size(674, 81);
        detailsTextBox.TabIndex = 22;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { statusStripLabel });
        statusStrip.Location = new Point(0, 445);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(710, 22);
        statusStrip.TabIndex = 23;
        // 
        // statusStripLabel
        // 
        statusStripLabel.Name = "statusStripLabel";
        statusStripLabel.Size = new Size(53, 17);
        statusStripLabel.Text = "Starting...";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(710, 467);
        Controls.Add(statusStrip);
        Controls.Add(detailsTextBox);
        Controls.Add(stateValueLabel);
        Controls.Add(stateLabel);
        Controls.Add(modelValueLabel);
        Controls.Add(modelLabel);
        Controls.Add(nameValueLabel);
        Controls.Add(nameLabel);
        Controls.Add(deviceValueLabel);
        Controls.Add(deviceLabel);
        Controls.Add(selectedDeviceTitleLabel);
        Controls.Add(deviceListView);
        Controls.Add(summaryValueLabel);
        Controls.Add(summaryLabel);
        Controls.Add(adbSourceValueLabel);
        Controls.Add(adbSourceLabel);
        Controls.Add(adbPathValueLabel);
        Controls.Add(adbLabel);
        Controls.Add(copyButton);
        Controls.Add(browseButton);
        Controls.Add(activateButton);
        Controls.Add(detectButton);
        Controls.Add(readmeLinkLabel);
        Controls.Add(instructionLabel);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Shizuku Activator";
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label instructionLabel;
    private LinkLabel readmeLinkLabel;
    private Button detectButton;
    private Button activateButton;
    private Button browseButton;
    private Button copyButton;
    private Label adbLabel;
    private Label adbPathValueLabel;
    private Label adbSourceLabel;
    private Label adbSourceValueLabel;
    private Label summaryLabel;
    private Label summaryValueLabel;
    private ListView deviceListView;
    private ColumnHeader idColumnHeader;
    private ColumnHeader nameColumnHeader;
    private ColumnHeader modelColumnHeader;
    private ColumnHeader stateColumnHeader;
    private Label selectedDeviceTitleLabel;
    private Label deviceLabel;
    private Label deviceValueLabel;
    private Label nameLabel;
    private Label nameValueLabel;
    private Label modelLabel;
    private Label modelValueLabel;
    private Label stateLabel;
    private Label stateValueLabel;
    private TextBox detailsTextBox;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusStripLabel;
}
