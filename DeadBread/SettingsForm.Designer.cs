namespace DeadBread
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.labelName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDownloadMirrors = new System.Windows.Forms.Label();
            this.labelTheme = new System.Windows.Forms.Label();
            this.labelCustomLaunch = new System.Windows.Forms.Label();
            this.infoTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxEnableToolTip = new DeadBread.Controls.CheckBoxNew();
            this.checkBoxLaunchWithConsole = new DeadBread.Controls.CheckBoxNew();
            this.btnSelectedTheme = new DeadBread.Controls.ListButton();
            this.btnSelectedMirror = new DeadBread.Controls.ListButton();
            this.checkBoxStartWindows = new DeadBread.Controls.CheckBoxNew();
            this.textFieldLaunchOptions = new DeadBread.Controls.WritableField();
            this.listThemes = new DeadBread.Controls.ItemList();
            this.listMirrors = new DeadBread.Controls.ItemList();
            this.btnDeveloper = new DeadBread.Controls.GroupSelectionBox();
            this.btnGeneral = new DeadBread.Controls.GroupSelectionBox();
            this.btnClose = new DeadBread.Controls.SimpleButton();
            this.btnMinimize = new DeadBread.Controls.SimpleButton();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(5, 5);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(62, 19);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Settings";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(240, 250);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(119, 21);
            this.labelVersion.TabIndex = 12;
            this.labelVersion.Text = "Launcher Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.infoTip.SetToolTip(this.labelVersion, "Your version of the launcher.");
            // 
            // labelDownloadMirrors
            // 
            this.labelDownloadMirrors.BackColor = System.Drawing.Color.Transparent;
            this.labelDownloadMirrors.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDownloadMirrors.Location = new System.Drawing.Point(12, 121);
            this.labelDownloadMirrors.Name = "labelDownloadMirrors";
            this.labelDownloadMirrors.Size = new System.Drawing.Size(153, 23);
            this.labelDownloadMirrors.TabIndex = 15;
            this.labelDownloadMirrors.Text = "Download Mirrors";
            this.labelDownloadMirrors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTheme
            // 
            this.labelTheme.BackColor = System.Drawing.Color.Transparent;
            this.labelTheme.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTheme.Location = new System.Drawing.Point(179, 121);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(153, 23);
            this.labelTheme.TabIndex = 17;
            this.labelTheme.Text = "Theme";
            this.labelTheme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCustomLaunch
            // 
            this.labelCustomLaunch.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomLaunch.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomLaunch.Location = new System.Drawing.Point(37, 102);
            this.labelCustomLaunch.Name = "labelCustomLaunch";
            this.labelCustomLaunch.Size = new System.Drawing.Size(193, 23);
            this.labelCustomLaunch.TabIndex = 24;
            this.labelCustomLaunch.Text = "Custom Launch Options";
            this.labelCustomLaunch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoTip
            // 
            this.infoTip.AutomaticDelay = 100;
            this.infoTip.AutoPopDelay = 5000;
            this.infoTip.InitialDelay = 100;
            this.infoTip.ReshowDelay = 20;
            this.infoTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.infoTip.ToolTipTitle = "Info";
            // 
            // checkBoxEnableToolTip
            // 
            this.checkBoxEnableToolTip.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableToolTip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBoxEnableToolTip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxEnableToolTip.ForeColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableToolTip.LabelTxt = "Enable ToolTips?";
            this.checkBoxEnableToolTip.Location = new System.Drawing.Point(12, 238);
            this.checkBoxEnableToolTip.Name = "checkBoxEnableToolTip";
            this.checkBoxEnableToolTip.Size = new System.Drawing.Size(200, 20);
            this.checkBoxEnableToolTip.TabIndex = 26;
            this.infoTip.SetToolTip(this.checkBoxEnableToolTip, "Only affects the non settings window...");
            this.checkBoxEnableToolTip.Click += new System.EventHandler(this.checkBoxEnableToolTip_Click);
            // 
            // checkBoxLaunchWithConsole
            // 
            this.checkBoxLaunchWithConsole.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxLaunchWithConsole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBoxLaunchWithConsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxLaunchWithConsole.ForeColor = System.Drawing.Color.Transparent;
            this.checkBoxLaunchWithConsole.LabelTxt = "Launch with Console";
            this.checkBoxLaunchWithConsole.Location = new System.Drawing.Point(40, 79);
            this.checkBoxLaunchWithConsole.Name = "checkBoxLaunchWithConsole";
            this.checkBoxLaunchWithConsole.Size = new System.Drawing.Size(200, 20);
            this.checkBoxLaunchWithConsole.TabIndex = 23;
            this.infoTip.SetToolTip(this.checkBoxLaunchWithConsole, "Launch any game with the console.");
            this.checkBoxLaunchWithConsole.Visible = false;
            this.checkBoxLaunchWithConsole.Click += new System.EventHandler(this.checkBoxLaunchWithConsole_Click);
            // 
            // btnSelectedTheme
            // 
            this.btnSelectedTheme.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectedTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectedTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectedTheme.ForeColor = System.Drawing.Color.Transparent;
            this.btnSelectedTheme.LabelTxt = null;
            this.btnSelectedTheme.Location = new System.Drawing.Point(182, 147);
            this.btnSelectedTheme.Name = "btnSelectedTheme";
            this.btnSelectedTheme.Size = new System.Drawing.Size(150, 20);
            this.btnSelectedTheme.TabIndex = 18;
            this.infoTip.SetToolTip(this.btnSelectedTheme, "Default user-interface, check the addons folder in the launcher root folder for m" +
        "ore info...");
            this.btnSelectedTheme.Click += new System.EventHandler(this.btnSelectedTheme_Click);
            // 
            // btnSelectedMirror
            // 
            this.btnSelectedMirror.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectedMirror.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectedMirror.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectedMirror.ForeColor = System.Drawing.Color.Transparent;
            this.btnSelectedMirror.LabelTxt = null;
            this.btnSelectedMirror.Location = new System.Drawing.Point(15, 147);
            this.btnSelectedMirror.Name = "btnSelectedMirror";
            this.btnSelectedMirror.Size = new System.Drawing.Size(150, 20);
            this.btnSelectedMirror.TabIndex = 13;
            this.infoTip.SetToolTip(this.btnSelectedMirror, "A download mirror is the same as a download location, choose the one closest to y" +
        "ou.");
            this.btnSelectedMirror.Click += new System.EventHandler(this.btnSelectedMirror_Click);
            // 
            // checkBoxStartWindows
            // 
            this.checkBoxStartWindows.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxStartWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBoxStartWindows.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxStartWindows.ForeColor = System.Drawing.Color.Transparent;
            this.checkBoxStartWindows.LabelTxt = "Start on Windows Startup";
            this.checkBoxStartWindows.Location = new System.Drawing.Point(15, 65);
            this.checkBoxStartWindows.Name = "checkBoxStartWindows";
            this.checkBoxStartWindows.Size = new System.Drawing.Size(226, 20);
            this.checkBoxStartWindows.TabIndex = 11;
            this.infoTip.SetToolTip(this.checkBoxStartWindows, "Start DeadBread when Windows starts.");
            this.checkBoxStartWindows.Visible = false;
            this.checkBoxStartWindows.Click += new System.EventHandler(this.checkBoxStartWindows_Click);
            // 
            // textFieldLaunchOptions
            // 
            this.textFieldLaunchOptions.BackColor = System.Drawing.Color.Black;
            this.textFieldLaunchOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textFieldLaunchOptions.ForeColor = System.Drawing.Color.White;
            this.textFieldLaunchOptions.Location = new System.Drawing.Point(40, 128);
            this.textFieldLaunchOptions.Name = "textFieldLaunchOptions";
            this.textFieldLaunchOptions.Size = new System.Drawing.Size(245, 30);
            this.textFieldLaunchOptions.TabIndex = 25;
            // 
            // listThemes
            // 
            this.listThemes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.listThemes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.listThemes.ForeColor = System.Drawing.Color.Transparent;
            this.listThemes.Location = new System.Drawing.Point(182, 173);
            this.listThemes.Name = "listThemes";
            this.listThemes.Size = new System.Drawing.Size(150, 20);
            this.listThemes.TabIndex = 19;
            // 
            // listMirrors
            // 
            this.listMirrors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.listMirrors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.listMirrors.ForeColor = System.Drawing.Color.Transparent;
            this.listMirrors.Location = new System.Drawing.Point(15, 173);
            this.listMirrors.Name = "listMirrors";
            this.listMirrors.Size = new System.Drawing.Size(150, 20);
            this.listMirrors.TabIndex = 14;
            // 
            // btnDeveloper
            // 
            this.btnDeveloper.BackColor = System.Drawing.Color.Transparent;
            this.btnDeveloper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeveloper.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeveloper.ForeColor = System.Drawing.Color.Transparent;
            this.btnDeveloper.Location = new System.Drawing.Point(96, 34);
            this.btnDeveloper.Name = "btnDeveloper";
            this.btnDeveloper.Size = new System.Drawing.Size(80, 20);
            this.btnDeveloper.TabIndex = 9;
            // 
            // btnGeneral
            // 
            this.btnGeneral.BackColor = System.Drawing.Color.Transparent;
            this.btnGeneral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneral.ForeColor = System.Drawing.Color.Transparent;
            this.btnGeneral.Location = new System.Drawing.Point(10, 34);
            this.btnGeneral.Name = "btnGeneral";
            this.btnGeneral.Size = new System.Drawing.Size(80, 20);
            this.btnGeneral.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(340, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 6;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.SetImages("controls\\CloseDefault.png", "controls\\CloseHover.png", "");
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.ForeColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Location = new System.Drawing.Point(320, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.TabIndex = 5;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.SetImages("controls\\MinimizeDefault.png", "controls\\MinimizeHover.png", "");
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(360, 270);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxEnableToolTip);
            this.Controls.Add(this.textFieldLaunchOptions);
            this.Controls.Add(this.labelCustomLaunch);
            this.Controls.Add(this.checkBoxLaunchWithConsole);
            this.Controls.Add(this.listThemes);
            this.Controls.Add(this.btnSelectedTheme);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.labelDownloadMirrors);
            this.Controls.Add(this.listMirrors);
            this.Controls.Add(this.btnSelectedMirror);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.checkBoxStartWindows);
            this.Controls.Add(this.btnDeveloper);
            this.Controls.Add(this.btnGeneral);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.labelName);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TransparencyKey = System.Drawing.Color.Turquoise;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingsForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private Controls.SimpleButton btnClose;
        private Controls.SimpleButton btnMinimize;
        private Controls.GroupSelectionBox btnGeneral;
        private Controls.GroupSelectionBox btnDeveloper;
        private Controls.CheckBoxNew checkBoxStartWindows;
        private System.Windows.Forms.Label labelVersion;
        private Controls.ListButton btnSelectedMirror;
        private Controls.ItemList listMirrors;
        private System.Windows.Forms.Label labelDownloadMirrors;
        private System.Windows.Forms.Label labelTheme;
        private Controls.ListButton btnSelectedTheme;
        private Controls.ItemList listThemes;
        private Controls.CheckBoxNew checkBoxLaunchWithConsole;
        private System.Windows.Forms.Label labelCustomLaunch;
        private Controls.WritableField textFieldLaunchOptions;
        private Controls.CheckBoxNew checkBoxEnableToolTip;
        private System.Windows.Forms.ToolTip infoTip;
    }
}