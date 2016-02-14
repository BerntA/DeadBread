//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Custom Text Entry
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
    public partial class WritableField : UserControl
    {
        public void SetText(string text) { textBox.Text = text; }
        public string GetText() { return textBox.Text; }
        public void SetShouldHideChars(bool bVal) { textBox.UseSystemPasswordChar = bVal; if (bVal) textBox.PasswordChar = '*'; }
        string szFocusImg = null;
        public WritableField()
        {
            InitializeComponent();

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            if (!DesignMode)
                szFocusImg = "controls\\WriteField_NoFocus.png";

            textBox.AutoSize = false;
            textBox.BackColor = Color.FromArgb(11, 11, 11);
            textBox.ForeColor = Color.White;

            textBox.Enter += new EventHandler(TextGotFocus);
            textBox.Leave += new EventHandler(TextLostFocus);
        }

        private void TextGotFocus(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                szFocusImg = "controls\\WriteField_Focus.png";
                Invalidate();
            }
        }

        private void TextLostFocus(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                szFocusImg = "controls\\WriteField_NoFocus.png";
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode)
            {
                e.Graphics.DrawImage(Globals.GetTextureImage(szFocusImg), 0, 0, Width, Height);
            }

            textBox.Bounds = new Rectangle(5, 6, Width - 10, Height - 10);

            base.OnPaint(e);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
    }
}
