namespace DeadBread
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.timFader = new System.Windows.Forms.Timer(this.components);
            this.imgReperioStudios = new System.Windows.Forms.PictureBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.imgPatchNotes = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.infoTips = new System.Windows.Forms.ToolTip(this.components);
            this.btnRepair = new DeadBread.Controls.CustomButton();
            this.labelDescription = new System.Windows.Forms.Label();
            this.newsBrowser = new System.Windows.Forms.WebBrowser();
            this.labelDeveloperInfo = new System.Windows.Forms.Label();
            this.btnLogout = new DeadBread.Controls.SimpleButton();
            this.toolBoxServer = new DeadBread.Controls.SelectionBox();
            this.toolBoxSDK = new DeadBread.Controls.SelectionBox();
            this.downloadBar = new DeadBread.Controls.DownloadBar();
            this.btnPlay = new DeadBread.Controls.SimpleButton();
            this.closeButton1 = new DeadBread.Controls.SimpleButton();
            this.minimizeButton1 = new DeadBread.Controls.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imgReperioStudios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPatchNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // timFader
            // 
            this.timFader.Enabled = true;
            this.timFader.Interval = 50;
            this.timFader.Tick += new System.EventHandler(this.timFader_Tick);
            // 
            // imgReperioStudios
            // 
            this.imgReperioStudios.BackColor = System.Drawing.Color.Transparent;
            this.imgReperioStudios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgReperioStudios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgReperioStudios.ErrorImage = null;
            this.imgReperioStudios.Image = global::DeadBread.Properties.Resources.reperio_studios;
            this.imgReperioStudios.InitialImage = null;
            this.imgReperioStudios.Location = new System.Drawing.Point(8, 8);
            this.imgReperioStudios.Name = "imgReperioStudios";
            this.imgReperioStudios.Size = new System.Drawing.Size(60, 50);
            this.imgReperioStudios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgReperioStudios.TabIndex = 7;
            this.imgReperioStudios.TabStop = false;
            this.imgReperioStudios.Click += new System.EventHandler(this.imgReperioStudios_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.ForeColor = System.Drawing.Color.White;
            this.labelVersion.Location = new System.Drawing.Point(90, 123);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(59, 15);
            this.labelVersion.TabIndex = 8;
            this.labelVersion.Text = "VERSION";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelVersion.Visible = false;
            // 
            // imgPatchNotes
            // 
            this.imgPatchNotes.BackColor = System.Drawing.Color.Transparent;
            this.imgPatchNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgPatchNotes.Enabled = false;
            this.imgPatchNotes.ErrorImage = null;
            this.imgPatchNotes.InitialImage = null;
            this.imgPatchNotes.Location = new System.Drawing.Point(93, 141);
            this.imgPatchNotes.Name = "imgPatchNotes";
            this.imgPatchNotes.Size = new System.Drawing.Size(440, 270);
            this.imgPatchNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPatchNotes.TabIndex = 9;
            this.imgPatchNotes.TabStop = false;
            this.imgPatchNotes.Visible = false;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(95, 192);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(436, 217);
            this.webBrowser.TabIndex = 10;
            this.webBrowser.TabStop = false;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser.Visible = false;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            // 
            // infoTips
            // 
            this.infoTips.AutomaticDelay = 100;
            this.infoTips.AutoPopDelay = 5000;
            this.infoTips.InitialDelay = 100;
            this.infoTips.ReshowDelay = 20;
            this.infoTips.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.infoTips.ToolTipTitle = "Info";
            // 
            // btnRepair
            // 
            this.btnRepair.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRepair.BackColor = System.Drawing.Color.Transparent;
            this.btnRepair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepair.ForeColor = System.Drawing.Color.White;
            this.btnRepair.LabelTxt = "Repair";
            this.btnRepair.Location = new System.Drawing.Point(93, 417);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(112, 25);
            this.btnRepair.TabIndex = 11;
            this.infoTips.SetToolTip(this.btnRepair, "Click here to validate your game files.");
            this.btnRepair.Visible = false;
            this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.ForeColor = System.Drawing.Color.White;
            this.labelDescription.Location = new System.Drawing.Point(95, 73);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(639, 45);
            this.labelDescription.TabIndex = 16;
            this.labelDescription.Text = "DESCRIPTION HERE";
            this.labelDescription.Visible = false;
            // 
            // newsBrowser
            // 
            this.newsBrowser.AllowWebBrowserDrop = false;
            this.newsBrowser.IsWebBrowserContextMenuEnabled = false;
            this.newsBrowser.Location = new System.Drawing.Point(83, 69);
            this.newsBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.newsBrowser.Name = "newsBrowser";
            this.newsBrowser.Size = new System.Drawing.Size(716, 530);
            this.newsBrowser.TabIndex = 61;
            this.newsBrowser.TabStop = false;
            this.newsBrowser.Visible = false;
            this.newsBrowser.WebBrowserShortcutsEnabled = false;
            this.newsBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.newsBrowser_Navigating);
            // 
            // labelDeveloperInfo
            // 
            this.labelDeveloperInfo.AutoSize = true;
            this.labelDeveloperInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelDeveloperInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDeveloperInfo.ForeColor = System.Drawing.Color.White;
            this.labelDeveloperInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDeveloperInfo.Location = new System.Drawing.Point(82, 49);
            this.labelDeveloperInfo.Name = "labelDeveloperInfo";
            this.labelDeveloperInfo.Size = new System.Drawing.Size(0, 15);
            this.labelDeveloperInfo.TabIndex = 63;
            this.labelDeveloperInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDeveloperInfo.Visible = false;
            this.labelDeveloperInfo.Click += new System.EventHandler(this.labelDeveloperInfo_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.DefaultImage = "controls\\logout.png";
            this.btnLogout.DisabledImage = null;
            this.btnLogout.ForeColor = System.Drawing.Color.Transparent;
            this.btnLogout.HoverImage = "controls\\logoutHover.png";
            this.btnLogout.Location = new System.Drawing.Point(731, 3);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(17, 18);
            this.btnLogout.TabIndex = 62;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // toolBoxServer
            // 
            this.toolBoxServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.toolBoxServer.BackColor = System.Drawing.Color.Transparent;
            this.toolBoxServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolBoxServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolBoxServer.ForeColor = System.Drawing.Color.Transparent;
            this.toolBoxServer.LabelTxt = "Host a Server";
            this.toolBoxServer.Location = new System.Drawing.Point(551, 211);
            this.toolBoxServer.Name = "toolBoxServer";
            this.toolBoxServer.Size = new System.Drawing.Size(199, 200);
            this.toolBoxServer.TabIndex = 60;
            this.toolBoxServer.Visible = false;
            this.toolBoxServer.Click += new System.EventHandler(this.toolBoxServer_Click);
            // 
            // toolBoxSDK
            // 
            this.toolBoxSDK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.toolBoxSDK.BackColor = System.Drawing.Color.Transparent;
            this.toolBoxSDK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolBoxSDK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolBoxSDK.ForeColor = System.Drawing.Color.Transparent;
            this.toolBoxSDK.LabelTxt = "Launch SDK";
            this.toolBoxSDK.Location = new System.Drawing.Point(122, 211);
            this.toolBoxSDK.Name = "toolBoxSDK";
            this.toolBoxSDK.Size = new System.Drawing.Size(199, 200);
            this.toolBoxSDK.TabIndex = 59;
            this.toolBoxSDK.Visible = false;
            this.toolBoxSDK.Click += new System.EventHandler(this.toolBoxSDK_Click);
            // 
            // downloadBar
            // 
            this.downloadBar.BackColor = System.Drawing.Color.Transparent;
            this.downloadBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downloadBar.ForeColor = System.Drawing.Color.Transparent;
            this.downloadBar.Location = new System.Drawing.Point(332, 545);
            this.downloadBar.Name = "downloadBar";
            this.downloadBar.Size = new System.Drawing.Size(440, 43);
            this.downloadBar.TabIndex = 6;
            this.downloadBar.Visible = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.DefaultImage = "controls\\PlayButton_Default.png";
            this.btnPlay.DisabledImage = "controls\\PlayButton_Disabled.png";
            this.btnPlay.ForeColor = System.Drawing.Color.Transparent;
            this.btnPlay.HoverImage = "controls\\PlayButton_Hover.png";
            this.btnPlay.Location = new System.Drawing.Point(83, 538);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(213, 62);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Visible = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // closeButton1
            // 
            this.closeButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeButton1.BackColor = System.Drawing.Color.Transparent;
            this.closeButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton1.DefaultImage = "controls\\CloseDefault.png";
            this.closeButton1.DisabledImage = null;
            this.closeButton1.ForeColor = System.Drawing.Color.Transparent;
            this.closeButton1.HoverImage = "controls\\CloseHover.png";
            this.closeButton1.Location = new System.Drawing.Point(773, 0);
            this.closeButton1.Name = "closeButton1";
            this.closeButton1.Size = new System.Drawing.Size(24, 24);
            this.closeButton1.TabIndex = 2;
            this.closeButton1.Click += new System.EventHandler(this.closeButton1_Click);
            // 
            // minimizeButton1
            // 
            this.minimizeButton1.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeButton1.DefaultImage = "controls\\MinimizeDefault.png";
            this.minimizeButton1.DisabledImage = null;
            this.minimizeButton1.ForeColor = System.Drawing.Color.Transparent;
            this.minimizeButton1.HoverImage = "controls\\MinimizeHover.png";
            this.minimizeButton1.Location = new System.Drawing.Point(750, 0);
            this.minimizeButton1.Name = "minimizeButton1";
            this.minimizeButton1.Size = new System.Drawing.Size(24, 24);
            this.minimizeButton1.TabIndex = 1;
            this.minimizeButton1.Click += new System.EventHandler(this.minimizeButton1_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.labelDeveloperInfo);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.newsBrowser);
            this.Controls.Add(this.toolBoxServer);
            this.Controls.Add(this.toolBoxSDK);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.btnRepair);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.imgPatchNotes);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.imgReperioStudios);
            this.Controls.Add(this.downloadBar);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.closeButton1);
            this.Controls.Add(this.minimizeButton1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.Opacity = 0D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeadBread";
            this.TransparencyKey = System.Drawing.Color.DarkTurquoise;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.imgReperioStudios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPatchNotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timFader;
        private Controls.SimpleButton minimizeButton1;
        private Controls.SimpleButton closeButton1;
        private Controls.SimpleButton btnPlay;
        private Controls.DownloadBar downloadBar;
        private System.Windows.Forms.PictureBox imgReperioStudios;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox imgPatchNotes;
        private System.Windows.Forms.WebBrowser webBrowser;
        private Controls.CustomButton btnRepair;
        private System.Windows.Forms.ToolTip infoTips;
        private System.Windows.Forms.Label labelDescription;
        private Controls.SelectionBox toolBoxSDK;
        private Controls.SelectionBox toolBoxServer;
        private System.Windows.Forms.WebBrowser newsBrowser;
        private Controls.SimpleButton btnLogout;
        private System.Windows.Forms.Label labelDeveloperInfo;
    }
}