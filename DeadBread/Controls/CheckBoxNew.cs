//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Custom Check Box!
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
    public partial class CheckBoxNew : UserControl
    {
        [Browsable(true)]
        [Description("Label Text"), Category("Appearance")]
        public string LabelTxt
        {
            get { return szText; }
            set { szText = value; }
        }

        public bool IsChecked() { return m_bChecked; }
        public string GetConVar() { return ConVar; }
        private string szText;
        private bool m_bChecked = false;
        private bool m_bHover = false;
        private string ConVar;
        public CheckBoxNew()
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

        public CheckBoxNew(string name, string convar, bool value)
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

            ConVar = convar;
            LabelTxt = name;
            SetCheckedState(value);
        }

        public void SetCheckedState(bool bValue)
        {
            m_bChecked = bValue;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle txtBounds = new Rectangle(Height, 0, Width, Height);
            if (!DesignMode)
            {
                string image = (IsChecked() && Enabled) ? "controls\\CBox_Check.png" : "controls\\CBox_UnCheck.png";
                e.Graphics.DrawImage(Globals.GetTextureImage(image), 0, 0, Bounds.Height, Bounds.Height);
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font eFont = new System.Drawing.Font("Arial", 12, m_bHover ? FontStyle.Bold : FontStyle.Regular);

            e.Graphics.DrawString(LabelTxt, eFont, new SolidBrush(Color.White), txtBounds, stringFormat);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            m_bHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            m_bHover = false;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            m_bChecked = !m_bChecked;
            Invalidate();
            base.OnClick(e);
        }
    }
}
