//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Custom Button
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
    public partial class CustomButton : UserControl
    {
        [Browsable(true)]
        [Description("Label Text"), Category("Appearance")]
        public string LabelTxt
        {
            get { return szText; }
            set { szText = value; Invalidate(); }
        }

        public void SetIcon(string icon) { szIcon = icon; Invalidate(); }
        public string GetIcon() { return szIcon; }

        private string szText;
        private bool m_bHover = false;
        private string szIcon = null;
        public CustomButton()
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;
            szIcon = null;
        }

        public CustomButton(int x, int y, int w, int h)
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;
            szIcon = null;

            Bounds = new Rectangle(x, y, w, h);
        }

        public CustomButton(string icon)
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;
            szIcon = icon;
        }

        protected override void OnClick(EventArgs e)
        {
            if (!Enabled)
                return;

            base.OnClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!Enabled)
                return;

            m_bHover = true;
            base.OnMouseEnter(e);

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!Enabled)
                return;

            m_bHover = false;
            base.OnMouseLeave(e);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            bool bIcon = !string.IsNullOrEmpty(szIcon);
            if (!DesignMode)
            {
                string image = "controls\\LoginButton_Disabled.png";
                if (Enabled)
                    image = (m_bHover) ? "controls\\LoginButton_Hover.png" : "controls\\LoginButton_Default.png";

                e.Graphics.DrawImage(Globals.GetTextureImage(image), 0, 0, Bounds.Width, Bounds.Height);
                if (bIcon)
                    e.Graphics.DrawImage(Globals.GetTextureImage(szIcon), 2, 2, Bounds.Height - 4, Bounds.Height - 4);
            }

            Rectangle szbounds = new Rectangle(0, 0, Width, Height);

            if (bIcon)
                szbounds.X += Bounds.Height;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font eFont = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(LabelTxt, eFont, new SolidBrush(Color.White), szbounds, stringFormat);
        }
    }
}
