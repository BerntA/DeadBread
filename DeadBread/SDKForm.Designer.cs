namespace DeadBread
{
    partial class SDKForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDKForm));
            this.labelSDKTitle = new System.Windows.Forms.Label();
            this.minimizeBtn = new DeadBread.Controls.SimpleButton();
            this.closeBtn = new DeadBread.Controls.SimpleButton();
            this.labelDocumentation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelSDKTitle
            // 
            this.labelSDKTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelSDKTitle.Location = new System.Drawing.Point(0, 0);
            this.labelSDKTitle.Name = "labelSDKTitle";
            this.labelSDKTitle.Size = new System.Drawing.Size(250, 25);
            this.labelSDKTitle.TabIndex = 0;
            this.labelSDKTitle.Text = "Game SDK";
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
            this.minimizeBtn.Location = new System.Drawing.Point(200, 0);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(28, 28);
            this.minimizeBtn.TabIndex = 17;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBtn.DefaultImage = "controls\\CloseDefault.png";
            this.closeBtn.DisabledImage = null;
            this.closeBtn.ForeColor = System.Drawing.Color.Transparent;
            this.closeBtn.HoverImage = "controls\\CloseHover.png";
            this.closeBtn.Location = new System.Drawing.Point(222, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(28, 28);
            this.closeBtn.TabIndex = 16;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // labelDocumentation
            // 
            this.labelDocumentation.AutoSize = true;
            this.labelDocumentation.BackColor = System.Drawing.Color.Transparent;
            this.labelDocumentation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDocumentation.Location = new System.Drawing.Point(0, 335);
            this.labelDocumentation.Name = "labelDocumentation";
            this.labelDocumentation.Size = new System.Drawing.Size(90, 15);
            this.labelDocumentation.TabIndex = 18;
            this.labelDocumentation.Text = "Documentation";
            this.labelDocumentation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDocumentation.Click += new System.EventHandler(this.labelDocumentation_Click);
            // 
            // SDKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(250, 350);
            this.ControlBox = false;
            this.Controls.Add(this.labelDocumentation);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.labelSDKTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SDKForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDK";
            this.TransparencyKey = System.Drawing.Color.MediumOrchid;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SDKForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSDKTitle;
        private Controls.SimpleButton minimizeBtn;
        private Controls.SimpleButton closeBtn;
        private System.Windows.Forms.Label labelDocumentation;
    }
}