//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Game Selection Box, used when selecting between games.
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
using System.IO;

namespace DeadBread.Controls
{
    public partial class GameSelectionBox : UserControl
    {
        public int GetIDLink() { return _iID; }
        private int _iID = -1;
        private string _title = null;
        private string _filename = "game_selections\\BaseDeselect.png";
        private int _m_iProgressValue = 0;
        public GameSelectionBox(int x, int y, int w, int h)
        {
            InitializeComponent();

            Bounds = new Rectangle(x, y, w, h);

            this.SetStyle(
System.Windows.Forms.ControlStyles.UserPaint |
System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
true);

            DoubleBuffered = true;

            ForeColor = Color.Transparent;
            BackColor = Color.Transparent;
        }

        public void SetupSelectionBox(string title, int iID) { _title = title; _iID = iID; Invalidate(); }
        public void RefreshState(bool bSelected) { _filename = (bSelected) ? "game_selections\\BaseSelect.png" : "game_selections\\BaseDeselect.png"; Invalidate(); }
        public void UpdateProgress(int iProgress) { double dProgress = (iProgress * Width) / 100; _m_iProgressValue = (int)Math.Truncate(dProgress); Invalidate(); } // Scale up our progress bar 'line' from 0 - 100 equals our width.

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!DesignMode)
            {
                int iNewWidth = Width / 2;

                try
                {
                    e.Graphics.DrawImage(Image.FromFile(string.Format("{0}\\temp\\{1}_icon.png", Globals.GetAppPath(), _iID)), iNewWidth - iNewWidth / 2, 0, iNewWidth, Height / 2);
                }
                catch
                {
                    e.Graphics.DrawImage(Globals.GetTextureImage("game_icons\\unknown.png"), iNewWidth - iNewWidth / 2, 0, iNewWidth, Height / 2);
                }
                finally
                {
                    if (!string.IsNullOrEmpty(_title))
                    {
                        Rectangle szbounds = new Rectangle(0, Height / 2, Width, 20);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        Font eFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
                        e.Graphics.DrawString(_title, eFont, new SolidBrush(Color.White), szbounds, stringFormat);
                    }

                    e.Graphics.DrawImage(Globals.GetTextureImage(_filename), 0, 0, Width, Height);

                    if (_m_iProgressValue > 0)
                    {
                        e.Graphics.DrawImage(Globals.GetTextureImage("progress_bar\\base.png"), 0, Height - 14, Width, 10);
                        e.Graphics.DrawImage(Globals.GetTextureImage("progress_bar\\progress.png"), 0, Height - 14, _m_iProgressValue, 10);
                    }
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            Globals.PerformGameSelection(_iID);
        }
    }
}
