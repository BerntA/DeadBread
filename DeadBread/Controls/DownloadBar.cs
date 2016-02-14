//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Download Progress Bar...
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeadBread.Base;

namespace DeadBread.Controls
{
    public partial class DownloadBar : UserControl
    {
        public event EventHandler OnPause;
        public event EventHandler OnResume;
        public event EventHandler OnCancel;

        private int _m_iProgressValue = 0;
        private string _szInfo;
        private string _szFile;
        private CustomButton btnResumePause;
        private CustomButton btnCancel;
        public void UpdateProgress(int iProgress, string Status, string File, bool bHide = false)
        {
            double dProgress = (iProgress * (Width - 90)) / 100;
            _m_iProgressValue = (int)Math.Truncate(dProgress);

            btnResumePause.Visible = !bHide;
            btnCancel.Visible = !bHide;

            _szInfo = string.Format("Downloading: {0}", Status);
            _szFile = File;
            Invalidate();
        }

        public void Reset()
        {
            btnResumePause.SetIcon("controls\\ProgressBar_Pause.png");
        }

        public DownloadBar()
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;

            if (!DesignMode)
            {
                btnResumePause = new CustomButton("controls\\ProgressBar_Pause.png");
                btnResumePause.Parent = this;
                btnResumePause.Click += new EventHandler(OnPauseResumeClick);

                btnCancel = new CustomButton();
                btnCancel.Parent = this;
                btnCancel.LabelTxt = "Cancel";
                btnCancel.Click += new EventHandler(OnCancelClick);
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            OnCancel(this, e);
        }

        private void OnPauseResumeClick(object sender, EventArgs e)
        {
            string icon = btnResumePause.GetIcon();
            if (icon.Contains("Resume"))
            {
                btnResumePause.SetIcon("controls\\ProgressBar_Pause.png");
                OnResume(this, e);
            }
            else
            {
                btnResumePause.SetIcon("controls\\ProgressBar_Resume.png");
                OnPause(this, e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                Font eFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
                SizeF textSize = TextRenderer.MeasureText(_szFile, eFont);

                e.Graphics.DrawImage(Globals.GetTextureImage("controls\\ProgressBar_Base.png"), 0, 20 + 1, Width - 90, Height - 20);

                btnResumePause.Bounds = new Rectangle(Width - 90 + 3, 20 + 1, 23, Height - 20);
                btnCancel.Bounds = new Rectangle(Width - 90 + 29, 20 + 1, 60, Height - 20);

                e.Graphics.DrawImage(Globals.GetTextureImage("controls\\ProgressBar_Progress.png"), 0, 20 + 1, _m_iProgressValue, Height - 20);

                if (_m_iProgressValue > 0)
                    e.Graphics.DrawImage(Globals.GetTextureImage("controls\\ProgressBar_Tick.png"), _m_iProgressValue - 14, -6 + 20 + 1, 16, Height - 20 + 12);

                Rectangle szbounds = new Rectangle(0, 20 + 1, Width - 90, Height - 20);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(_szInfo, eFont, new SolidBrush(Color.White), szbounds, stringFormat);

                if (!string.IsNullOrEmpty(_szFile))
                    e.Graphics.DrawString(_szFile, eFont, new SolidBrush(Color.White), new Rectangle(0, 5, Width, (int)eFont.Height), stringFormat);
            }

            base.OnPaint(e);
        }
    }
}
