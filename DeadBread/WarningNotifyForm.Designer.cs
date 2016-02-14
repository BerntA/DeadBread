namespace DeadBread
{
    partial class WarningNotifyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningNotifyForm));
            this.labelMsg = new System.Windows.Forms.Label();
            this.timQuickFade = new System.Windows.Forms.Timer(this.components);
            this.timQuickClose = new System.Windows.Forms.Timer(this.components);
            this.timDelay = new System.Windows.Forms.Timer(this.components);
            this.btnNo = new DeadBread.Controls.CustomButton();
            this.btnYes = new DeadBread.Controls.CustomButton();
            this.CloseBtn = new DeadBread.Controls.WarningCloseButton();
            this.SuspendLayout();
            // 
            // labelMsg
            // 
            this.labelMsg.BackColor = System.Drawing.Color.Transparent;
            this.labelMsg.Location = new System.Drawing.Point(12, 9);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(276, 48);
            this.labelMsg.TabIndex = 0;
            this.labelMsg.Text = "Warning here";
            this.labelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timQuickFade
            // 
            this.timQuickFade.Interval = 30;
            this.timQuickFade.Tick += new System.EventHandler(this.timQuickFade_Tick);
            // 
            // timQuickClose
            // 
            this.timQuickClose.Interval = 30;
            this.timQuickClose.Tick += new System.EventHandler(this.timQuickClose_Tick);
            // 
            // timDelay
            // 
            this.timDelay.Interval = 350;
            this.timDelay.Tick += new System.EventHandler(this.timDelay_Tick);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.Transparent;
            this.btnNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNo.LabelTxt = "No";
            this.btnNo.Location = new System.Drawing.Point(168, 70);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 24);
            this.btnNo.TabIndex = 3;
            this.btnNo.Visible = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.Transparent;
            this.btnYes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.ForeColor = System.Drawing.Color.Transparent;
            this.btnYes.LabelTxt = "Yes";
            this.btnYes.Location = new System.Drawing.Point(29, 70);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(100, 24);
            this.btnYes.TabIndex = 2;
            this.btnYes.Visible = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Black;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.ForeColor = System.Drawing.Color.Black;
            this.CloseBtn.Location = new System.Drawing.Point(98, 70);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 24);
            this.CloseBtn.TabIndex = 1;
            this.CloseBtn.Visible = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // WarningNotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.ControlBox = false;
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.labelMsg);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WarningNotifyForm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.DarkOliveGreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Timer timQuickFade;
        private Controls.WarningCloseButton CloseBtn;
        private System.Windows.Forms.Timer timQuickClose;
        private Controls.CustomButton btnYes;
        private Controls.CustomButton btnNo;
        private System.Windows.Forms.Timer timDelay;
    }
}