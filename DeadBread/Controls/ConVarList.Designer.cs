namespace DeadBread.Controls
{
    partial class ConVarList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.infoTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
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
            // ConVarList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "ConVarList";
            this.Size = new System.Drawing.Size(400, 280);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip infoTip;
    }
}
