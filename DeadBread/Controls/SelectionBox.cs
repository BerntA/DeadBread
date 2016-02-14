using System;
using System.Collections.Generic;
using System.ComponentModel;
//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Selection Box / Button.
//
//=============================================================================================//

using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeadBread.Base;

namespace DeadBread.Controls
{
    public partial class SelectionBox : UserControl
    {
        [Browsable(true)]
        [Description("Label Text"), Category("Appearance")]
        public string LabelTxt
        {
            get { return szText; }
            set { szText = value; }
        }

        public string GetImage() { return szImage; }
        public void SetImage(string image, string path) { szImage = image; szPath = path; Invalidate(); }

        private string szText;
        private string szImage;
        private string szPath;
        private bool m_bHover;
        public SelectionBox()
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
            if (!string.IsNullOrEmpty(szImage))
            {
                int iWidth = string.IsNullOrEmpty(LabelTxt) ? Width : Width - 24;
                int iHeight = string.IsNullOrEmpty(LabelTxt) ? Height : Height - 24;
                int iPos = string.IsNullOrEmpty(LabelTxt) ? 0 : 12;

                if (m_bHover)
                    e.Graphics.DrawImage(Globals.GetTextureImage(string.Format("{0}\\{1}_hover.png", szPath, szImage)), iPos, 0, iWidth, iHeight);
                else
                    e.Graphics.DrawImage(Globals.GetTextureImage(string.Format("{0}\\{1}.png", szPath, szImage)), iPos, 0, iWidth, iHeight);
            }

            if (!string.IsNullOrEmpty(LabelTxt))
            {
                Rectangle szbounds = new Rectangle(0, Height - 20, Width, 20);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                Font eFont;
                if (m_bHover)
                    eFont = new System.Drawing.Font("Calibri", 12, FontStyle.Bold);
                else
                    eFont = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);

                e.Graphics.DrawString(LabelTxt, eFont, new SolidBrush(Color.White), szbounds, stringFormat);
            }

            base.OnPaint(e);
        }
    }
}
