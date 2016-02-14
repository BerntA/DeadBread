//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Simple Custom Button
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
    public partial class SimpleButton : UserControl
    {
        [PropertyTab("DefaultImage")]
        [Browsable(true)]
        [Description("Path to the default image"), Category("Misc")]
        public string DefaultImage
        {
            get { return pchDefaultImage; }
            set { pchDefaultImage = value; Invalidate(); }
        }

        [PropertyTab("HoverImage")]
        [Browsable(true)]
        [Description("Path to the hover image"), Category("Misc")]
        public string HoverImage
        {
            get { return pchHoverImage; }
            set { pchHoverImage = value; Invalidate(); }
        }

        [PropertyTab("DisabledImage")]
        [Browsable(true)]
        [Description("Path to the disabled image"), Category("Misc")]
        public string DisabledImage
        {
            get { return pchDisabledImage; }
            set { pchDisabledImage = value; Invalidate(); }
        }

        private bool m_bHover = false;
        private string pchDefaultImage = null;
        private string pchHoverImage = null;
        private string pchDisabledImage = null;
        public SimpleButton()
        {
            Initialize();
        }

        public SimpleButton(string imgDef, string imgHover, string imgDisabled)
        {
            pchDefaultImage = imgDef;
            pchHoverImage = imgHover;
            pchDisabledImage = imgDisabled;
            Initialize();
        }

        public void SetImages(string imgDef, string imgHover, string imgDisabled)
        {
            pchDefaultImage = imgDef;
            pchHoverImage = imgHover;
            pchDisabledImage = imgDisabled;
            Invalidate();
        }

        public void Reset()
        {
            m_bHover = false;
            Invalidate();
        }

        private void Initialize()
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

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled && Visible)
                base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                string image = pchDisabledImage;
                if (Enabled)
                    image = (m_bHover ? pchHoverImage : pchDefaultImage);

                e.Graphics.DrawImage(Globals.GetTextureImage(image), 0, 0, Bounds.Width, Bounds.Height);
            }

            base.OnPaint(e);
        }
    }
}
