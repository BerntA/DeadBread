namespace DeadBread
{
    partial class ServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.labelName = new System.Windows.Forms.Label();
            this.labelMap = new System.Windows.Forms.Label();
            this.labelHost = new System.Windows.Forms.Label();
            this.labelRCON = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.textMaxPlrs = new DeadBread.Controls.WritableField();
            this.checkLAN = new DeadBread.Controls.CheckBoxNew();
            this.textRCON = new DeadBread.Controls.WritableField();
            this.textHostname = new DeadBread.Controls.WritableField();
            this.mapList = new DeadBread.Controls.ItemList();
            this.selectedMap = new DeadBread.Controls.ListButton();
            this.btnAction = new DeadBread.Controls.CustomButton();
            this.minimizeBtn = new DeadBread.Controls.SimpleButton();
            this.closeBtn = new DeadBread.Controls.SimpleButton();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textPassword = new DeadBread.Controls.WritableField();
            this.labelTime = new System.Windows.Forms.Label();
            this.textTimelimit = new DeadBread.Controls.WritableField();
            this.btnOther = new DeadBread.Controls.CustomButton();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(200, 20);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Server";
            // 
            // labelMap
            // 
            this.labelMap.BackColor = System.Drawing.Color.Transparent;
            this.labelMap.Location = new System.Drawing.Point(9, 27);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(153, 20);
            this.labelMap.TabIndex = 24;
            this.labelMap.Text = "Map";
            this.labelMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelHost
            // 
            this.labelHost.BackColor = System.Drawing.Color.Transparent;
            this.labelHost.Location = new System.Drawing.Point(168, 27);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(153, 20);
            this.labelHost.TabIndex = 27;
            this.labelHost.Text = "Hostname";
            this.labelHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRCON
            // 
            this.labelRCON.BackColor = System.Drawing.Color.Transparent;
            this.labelRCON.Location = new System.Drawing.Point(168, 80);
            this.labelRCON.Name = "labelRCON";
            this.labelRCON.Size = new System.Drawing.Size(153, 20);
            this.labelRCON.TabIndex = 28;
            this.labelRCON.Text = "RCON Password";
            this.labelRCON.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPlayers
            // 
            this.labelPlayers.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayers.Location = new System.Drawing.Point(12, 99);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(150, 20);
            this.labelPlayers.TabIndex = 31;
            this.labelPlayers.Text = "Max Players";
            this.labelPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textMaxPlrs
            // 
            this.textMaxPlrs.BackColor = System.Drawing.Color.Transparent;
            this.textMaxPlrs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textMaxPlrs.ForeColor = System.Drawing.Color.Transparent;
            this.textMaxPlrs.Location = new System.Drawing.Point(12, 122);
            this.textMaxPlrs.Name = "textMaxPlrs";
            this.textMaxPlrs.Size = new System.Drawing.Size(150, 27);
            this.textMaxPlrs.TabIndex = 32;
            // 
            // checkLAN
            // 
            this.checkLAN.BackColor = System.Drawing.Color.Transparent;
            this.checkLAN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkLAN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkLAN.ForeColor = System.Drawing.Color.Transparent;
            this.checkLAN.LabelTxt = "LAN";
            this.checkLAN.Location = new System.Drawing.Point(168, 242);
            this.checkLAN.Name = "checkLAN";
            this.checkLAN.Size = new System.Drawing.Size(220, 24);
            this.checkLAN.TabIndex = 30;
            // 
            // textRCON
            // 
            this.textRCON.BackColor = System.Drawing.Color.Transparent;
            this.textRCON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textRCON.ForeColor = System.Drawing.Color.Transparent;
            this.textRCON.Location = new System.Drawing.Point(168, 103);
            this.textRCON.Name = "textRCON";
            this.textRCON.Size = new System.Drawing.Size(220, 27);
            this.textRCON.TabIndex = 29;
            // 
            // textHostname
            // 
            this.textHostname.BackColor = System.Drawing.Color.Transparent;
            this.textHostname.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textHostname.ForeColor = System.Drawing.Color.Transparent;
            this.textHostname.Location = new System.Drawing.Point(168, 50);
            this.textHostname.Name = "textHostname";
            this.textHostname.Size = new System.Drawing.Size(220, 27);
            this.textHostname.TabIndex = 26;
            // 
            // mapList
            // 
            this.mapList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.mapList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mapList.ForeColor = System.Drawing.Color.Transparent;
            this.mapList.Location = new System.Drawing.Point(12, 76);
            this.mapList.Name = "mapList";
            this.mapList.Size = new System.Drawing.Size(150, 20);
            this.mapList.TabIndex = 25;
            this.mapList.Visible = false;
            // 
            // selectedMap
            // 
            this.selectedMap.BackColor = System.Drawing.Color.Transparent;
            this.selectedMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectedMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectedMap.ForeColor = System.Drawing.Color.Transparent;
            this.selectedMap.LabelTxt = null;
            this.selectedMap.Location = new System.Drawing.Point(12, 50);
            this.selectedMap.Name = "selectedMap";
            this.selectedMap.Size = new System.Drawing.Size(150, 20);
            this.selectedMap.TabIndex = 23;
            this.selectedMap.Click += new System.EventHandler(this.selectedMap_Click);
            // 
            // btnAction
            // 
            this.btnAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAction.BackColor = System.Drawing.Color.Transparent;
            this.btnAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAction.ForeColor = System.Drawing.Color.Transparent;
            this.btnAction.LabelTxt = "Start";
            this.btnAction.Location = new System.Drawing.Point(12, 266);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(100, 22);
            this.btnAction.TabIndex = 20;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeBtn.DefaultImage = "controls\\MinimizeDefault.png";
            this.minimizeBtn.DisabledImage = "";
            this.minimizeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.HoverImage = "controls\\MinimizeHover.png";
            this.minimizeBtn.Location = new System.Drawing.Point(341, 0);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(33, 32);
            this.minimizeBtn.TabIndex = 19;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBtn.DefaultImage = "controls\\CloseDefault.png";
            this.closeBtn.DisabledImage = "";
            this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.closeBtn.HoverImage = "controls\\CloseHover.png";
            this.closeBtn.Location = new System.Drawing.Point(367, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(33, 32);
            this.closeBtn.TabIndex = 18;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Location = new System.Drawing.Point(168, 133);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(153, 20);
            this.labelPassword.TabIndex = 33;
            this.labelPassword.Text = "Password";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textPassword
            // 
            this.textPassword.BackColor = System.Drawing.Color.Transparent;
            this.textPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textPassword.ForeColor = System.Drawing.Color.Transparent;
            this.textPassword.Location = new System.Drawing.Point(168, 156);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(220, 27);
            this.textPassword.TabIndex = 34;
            // 
            // labelTime
            // 
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Location = new System.Drawing.Point(168, 186);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(153, 20);
            this.labelTime.TabIndex = 35;
            this.labelTime.Text = "Timelimit";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textTimelimit
            // 
            this.textTimelimit.BackColor = System.Drawing.Color.Transparent;
            this.textTimelimit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.textTimelimit.ForeColor = System.Drawing.Color.Transparent;
            this.textTimelimit.Location = new System.Drawing.Point(168, 209);
            this.textTimelimit.Name = "textTimelimit";
            this.textTimelimit.Size = new System.Drawing.Size(220, 27);
            this.textTimelimit.TabIndex = 36;
            // 
            // btnOther
            // 
            this.btnOther.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOther.BackColor = System.Drawing.Color.Transparent;
            this.btnOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOther.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOther.ForeColor = System.Drawing.Color.Transparent;
            this.btnOther.LabelTxt = "Other";
            this.btnOther.Location = new System.Drawing.Point(296, 280);
            this.btnOther.Name = "btnOther";
            this.btnOther.Size = new System.Drawing.Size(100, 15);
            this.btnOther.TabIndex = 37;
            this.btnOther.Click += new System.EventHandler(this.btnOther_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.ControlBox = false;
            this.Controls.Add(this.btnOther);
            this.Controls.Add(this.textTimelimit);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textMaxPlrs);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.checkLAN);
            this.Controls.Add(this.textRCON);
            this.Controls.Add(this.labelRCON);
            this.Controls.Add(this.labelHost);
            this.Controls.Add(this.textHostname);
            this.Controls.Add(this.mapList);
            this.Controls.Add(this.labelMap);
            this.Controls.Add(this.selectedMap);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.labelName);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.TransparencyKey = System.Drawing.Color.DarkOrchid;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerForm_FormClosed);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ServerForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private Controls.SimpleButton minimizeBtn;
        private Controls.SimpleButton closeBtn;
        private Controls.CustomButton btnAction;
        private Controls.ListButton selectedMap;
        private System.Windows.Forms.Label labelMap;
        private Controls.ItemList mapList;
        private Controls.WritableField textHostname;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelRCON;
        private Controls.WritableField textRCON;
        private Controls.CheckBoxNew checkLAN;
        private System.Windows.Forms.Label labelPlayers;
        private Controls.WritableField textMaxPlrs;
        private System.Windows.Forms.Label labelPassword;
        private Controls.WritableField textPassword;
        private System.Windows.Forms.Label labelTime;
        private Controls.WritableField textTimelimit;
        private Controls.CustomButton btnOther;
    }
}