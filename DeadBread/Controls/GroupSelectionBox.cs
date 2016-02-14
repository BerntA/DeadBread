//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Group Selection Box, used for tabs, in the settings form.
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
    public partial class GroupSelectionBox : UserControl
    {
        public void SetText(string text) { szText = text; }
        public bool IsSelected() { return bSelected; }

        private string szImage = null;
        private string szText = "TEXT";
        private bool bSelected = false;
        public GroupSelectionBox()
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;
            szImage = "controls\\GroupSelection_Default.png";
        }

        public void SetSelected(bool bVal)
        {
            bSelected = bVal;

            if (bVal)
            {
                szImage = "controls\\GroupSelection_ActiveHover.png";
                Invalidate();
            }
            else
            {
                szImage = "controls\\GroupSelection_Default.png";
                Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!bSelected)
            {
                szImage = "controls\\GroupSelection_ActiveHover.png";
                Invalidate();
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!bSelected)
            {
                szImage = "controls\\GroupSelection_Default.png";
                Invalidate();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!bSelected)
            {
                bSelected = true;
                szImage = "controls\\GroupSelection_ActiveHover.png";
                Invalidate();

                base.OnClick(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                e.Graphics.DrawImage(Globals.GetTextureImage(szImage), 0, 0, Width, Height);
            }

            Rectangle szbounds = new Rectangle(0, 0, Width, Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font eFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(szText, eFont, new SolidBrush(Color.White), szbounds, stringFormat);

            base.OnPaint(e);
        }
    }
}
