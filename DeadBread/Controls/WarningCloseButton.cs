//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Customized button for the Warning Form...
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
    public partial class WarningCloseButton : UserControl
    {
        public void SetText(string text) { szText = text; }
        private bool m_bHover = false;
        private string szText = "Close";
        public WarningCloseButton()
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            m_bHover = true;
            base.OnMouseEnter(e);

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            m_bHover = false;
            base.OnMouseLeave(e);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                string image = "controls\\LoginButton_Disabled.png";
                if (Enabled)
                    image = (m_bHover) ? "controls\\LoginButton_Hover.png" : "controls\\LoginButton_Default.png";

                e.Graphics.DrawImage(Globals.GetTextureImage(image), 0, 0, Bounds.Width, Bounds.Height);
            }

            Rectangle szbounds = new Rectangle(0, 0, Width, Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font eFont = new System.Drawing.Font("Arial", 12, FontStyle.Regular);
            e.Graphics.DrawString(szText, eFont, new SolidBrush(Color.White), szbounds, stringFormat);

            base.OnPaint(e);
        }
    }
}
