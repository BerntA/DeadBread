//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Numeric Var - Text Entry.
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

namespace DeadBread.Controls
{
    public partial class NumericVar : UserControl
    {
        public string GetConVar() { return szConVar; }

        public int GetValueInt() { try { return int.Parse(textField.GetText()); } catch { return 0; } }
        public float GetValueFloat() { try { return float.Parse(textField.GetText()); } catch { return 0.0F; } }
        public string GetValueString() { try { return textField.GetText(); } catch { return null; } }
        public int ValueType { get; set; }
        private WritableField textField;
        private string szConVar;
        private string szName;

        private void BaseInit()
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

        public NumericVar(string name, string convar, int value)
        {
            BaseInit();

            szConVar = convar;
            szName = name;

            textField = new WritableField();
            textField.Parent = this;
            textField.Bounds = new Rectangle(0, 15, Width, Height - 18);
            textField.SetText(value.ToString());

            ValueType = 0;
        }

        public NumericVar(string name, string convar, float value)
        {
            BaseInit();

            szConVar = convar;
            szName = name;

            textField = new WritableField();
            textField.Parent = this;
            textField.Bounds = new Rectangle(0, 15, Width, Height - 18);
            textField.SetText(value.ToString());

            ValueType = 1;
        }

        public NumericVar(string name, string convar, string value)
        {
            BaseInit();

            szConVar = convar;
            szName = name;

            textField = new WritableField();
            textField.Parent = this;
            textField.Bounds = new Rectangle(0, 15, Width, Height - 18);
            textField.SetText(value);

            ValueType = 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font titleFont = new System.Drawing.Font("Calibri", 8, FontStyle.Bold);
            e.Graphics.DrawString(szName, titleFont, new SolidBrush(Color.White), new Rectangle(0, 0, Width, 12), stringFormat);

            base.OnPaint(e);
        }
    }
}
