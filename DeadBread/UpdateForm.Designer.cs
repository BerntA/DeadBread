namespace DeadBread
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.imgLoad = new System.Windows.Forms.PictureBox();
            this.timFader = new System.Windows.Forms.Timer(this.components);
            this.timEnd = new System.Windows.Forms.Timer(this.components);
            this.labelVersion = new System.Windows.Forms.Label();
            this.asyncUserData = new System.ComponentModel.BackgroundWorker();
            this.timQuit = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.BackColor = System.Drawing.Color.Transparent;
            this.imgLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgLogo.Enabled = false;
            this.imgLogo.ErrorImage = null;
            this.imgLogo.Image = global::DeadBread.Properties.Resources.logo;
            this.imgLogo.InitialImage = null;
            this.imgLogo.Location = new System.Drawing.Point(72, 8);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(457, 96);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogo.TabIndex = 10;
            this.imgLogo.TabStop = false;
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(98, 132);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(371, 33);
            this.labelStatus.TabIndex = 11;
            this.labelStatus.Text = "Preparing...";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imgLoad
            // 
            this.imgLoad.BackColor = System.Drawing.Color.Transparent;
            this.imgLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgLoad.ErrorImage = null;
            this.imgLoad.Image = global::DeadBread.Properties.Resources.loading;
            this.imgLoad.InitialImage = null;
            this.imgLoad.Location = new System.Drawing.Point(17, 110);
            this.imgLoad.Name = "imgLoad";
            this.imgLoad.Size = new System.Drawing.Size(75, 75);
            this.imgLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLoad.TabIndex = 12;
            this.imgLoad.TabStop = false;
            // 
            // timFader
            // 
            this.timFader.Interval = 65;
            this.timFader.Tick += new System.EventHandler(this.timFader_Tick);
            // 
            // timEnd
            // 
            this.timEnd.Interval = 50;
            this.timEnd.Tick += new System.EventHandler(this.timEnd_Tick);
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Location = new System.Drawing.Point(427, 174);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(175, 27);
            this.labelVersion.TabIndex = 13;
            this.labelVersion.Text = "VERSION";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // asyncUserData
            // 
            this.asyncUserData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.asyncUserData_DoWork);
            // 
            // timQuit
            // 
            this.timQuit.Interval = 50;
            this.timQuit.Tick += new System.EventHandler(this.timQuit_Tick);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.ControlBox = false;
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.imgLoad);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.imgLogo);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.Opacity = 0D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preparing to Launch...";
            this.TransparencyKey = System.Drawing.Color.Goldenrod;
            this.Load += new System.EventHandler(this.UpdateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.PictureBox imgLoad;
        private System.Windows.Forms.Timer timFader;
        private System.Windows.Forms.Timer timEnd;
        private System.Windows.Forms.Label labelVersion;
        private System.ComponentModel.BackgroundWorker asyncUserData;
        private System.Windows.Forms.Timer timQuit;
    }
}