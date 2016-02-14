//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Navigation Button - Used for tabs.
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
    public partial class NavigationButton : UserControl
    {
        public int GetPageLink() { return m_iPageLink; }

        public void SetState(bool bState)
        {
            m_bIsActive = bState;

            if (!bState)
                szImageToDraw = string.Format("base_navigation\\{0}_default.png", szLinkedImage);
            else
                szImageToDraw = string.Format("base_navigation\\{0}_active.png", szLinkedImage);

            Invalidate();
        }

        private int m_iPageLink = -1;
        private bool m_bIsActive = false;
        private string szLinkedImage;
        private string szImageToDraw = null;
        public NavigationButton(int iLink, string image)
        {
            InitializeComponent();

            m_iPageLink = iLink;
            szLinkedImage = image;

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;

            szImageToDraw = string.Format("base_navigation\\{0}_default.png", szLinkedImage);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            m_bIsActive = false;
            szImageToDraw = string.Format("base_navigation\\{0}_default.png", szLinkedImage);

            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!m_bIsActive)
                szImageToDraw = string.Format("base_navigation\\{0}_over.png", szLinkedImage);

            base.OnMouseEnter(e);

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!m_bIsActive)
                szImageToDraw = string.Format("base_navigation\\{0}_default.png", szLinkedImage);

            base.OnMouseLeave(e);

            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled && Visible && !m_bIsActive)
            {
                m_bIsActive = true;
                szImageToDraw = string.Format("base_navigation\\{0}_active.png", szLinkedImage);
                Invalidate();

                Globals.NavigateToPage(m_iPageLink);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                e.Graphics.DrawImage(Globals.GetTextureImage(szImageToDraw), 0, 0, Bounds.Width, Bounds.Height);
            }

            base.OnPaint(e);
        }
    }
}
