//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: A simple icon based button, with text.
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
    public partial class IconButton : UserControl
    {
        public string GetIconName() { return iconImg; }
        public string GetCommand() { return szCommand; }
        private string szCommand = null;
        private string szText = null;
        private string iconImg = null;
        private bool m_bHover = false;
        private int iFontSize = 8;
        public IconButton(string icon, string text, int fontSize = 8, string command = null)
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            szCommand = command;
            szText = text;
            iconImg = icon;
            iFontSize = fontSize;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            m_bHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            m_bHover = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                e.Graphics.DrawImage(Globals.GetTextureImage(string.Format("controls\\{0}.png", iconImg)), 0, 0, Height, Height);
            }

            Rectangle szbounds = new Rectangle(Height + 2, 0, Width, Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font eFont;
            if (m_bHover)
                eFont = new System.Drawing.Font("Arial", iFontSize, FontStyle.Underline);
            else
                eFont = new System.Drawing.Font("Arial", iFontSize, FontStyle.Regular);

            e.Graphics.DrawString(szText, eFont, new SolidBrush(Color.White), szbounds, stringFormat);

            base.OnPaint(e);
        }
    }
}
