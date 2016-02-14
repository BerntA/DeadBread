//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Notification/Warning Window Form
//
//=============================================================================================//

using DeadBread.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class WarningNotifyForm : Form
    {
        private int _m_iWarningMode = 0;
        public WarningNotifyForm(string message, int iWarningMode = 0)
        {
            InitializeComponent();

            BackgroundImage = Globals.GetTextureImage("controls\\LoginBG.png");

            _m_iWarningMode = iWarningMode;
            labelMsg.Text = message;
            timQuickFade.Enabled = true;

            if (iWarningMode == 0)
            {
                CloseBtn.Visible = true;
            }
            else if (iWarningMode == 1) // Auto Close mode : notify message.
            {
                labelMsg.Bounds = new Rectangle(0, 0, Width, Height);
                CloseBtn.Visible = false;
            }
            else if (iWarningMode == 2) // Promt Yes / No
            {
                btnYes.Visible = true;
                btnNo.Visible = true;
            }
        }

        private void timQuickFade_Tick(object sender, EventArgs e)
        {
            Opacity += 0.05;
            if (Opacity >= .95)
            {
                Opacity = 1;
                timQuickFade.Enabled = false;

                if (_m_iWarningMode == 1)
                {
                    timDelay.Enabled = true;
                }
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            timQuickClose.Enabled = true;
        }

        private void timQuickClose_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.05;
            if (Opacity <= .05)
            {
                Opacity = 0;
                timQuickClose.Enabled = false;
                Close();
            }
        }

        private void timDelay_Tick(object sender, EventArgs e)
        {
            timQuickClose.Enabled = true;
            timDelay.Enabled = false;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
