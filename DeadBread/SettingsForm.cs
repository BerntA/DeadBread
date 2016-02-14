//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Settings Form.
//
//=============================================================================================//

using DeadBread.Base;
using DeadBread.Controls;
using DeadBread.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class SettingsForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private int m_iPageTAB = 0;
        public SettingsForm()
        {
            InitializeComponent();

            if (!DesignMode)
                BackgroundImage = Globals.GetTextureImage("friends\\Background.png");

            btnGeneral.SetText("General");
            btnGeneral.SetSelected(true);
            btnDeveloper.SetText("Developer");

            btnGeneral.Click += new EventHandler(btnGroup_Click);
            btnDeveloper.Click += new EventHandler(btnGroup_Click);

            SetTab(0);
            labelVersion.Text = Application.ProductVersion;

            // Tab 0      
            listMirrors.bUseFixedWidth = false;
            listThemes.bUseFixedWidth = false;

            listMirrors.OnItemClick += new EventHandler(OnListMirrorsClick);
            listThemes.OnItemClick += new EventHandler(OnListThemesClick);

            foreach (string item in Globals.GetDownloadMirrors().Keys.ToArray())
                listMirrors.AddItem(item);

            listThemes.AddItem("Default");
            foreach (string item in Directory.EnumerateDirectories(string.Format("{0}\\BaseLauncher\\addons\\", Globals.GetAppPath()), "*.*", SearchOption.TopDirectoryOnly))
                listThemes.AddItem(new DirectoryInfo(item).Name);

            textFieldLaunchOptions.SetText(Settings.Default.szCustomLaunchOptions);
        }

        private void OnListMirrorsClick(object sender, EventArgs e)
        {
            string szItem = ((Label)sender).Text;

            if (m_iPageTAB == 0)
            {
                Settings.Default.szDownloadMirror = szItem;
                Settings.Default.Save();
                btnSelectedMirror.LabelTxt = szItem;
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is ItemList)
                    Controls[i].Visible = false;
            }
        }

        private void OnListThemesClick(object sender, EventArgs e)
        {
            string szItem = ((Label)sender).Text;

            if (m_iPageTAB == 0)
            {
                Settings.Default.szSkinName = szItem;
                if (szItem != "Default")
                    Settings.Default.szSkinPath = string.Format("addons\\{0}", szItem);
                else
                    Settings.Default.szSkinPath = null;
                Settings.Default.Save();
                btnSelectedTheme.LabelTxt = szItem;
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is ItemList)
                    Controls[i].Visible = false;
            }
        }

        private void OnListGamesClick(object sender, EventArgs e)
        {
            string szItem = ((Label)sender).Text;

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is ItemList)
                    Controls[i].Visible = false;
            }
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            GroupSelectionBox grpBox = ((GroupSelectionBox)sender);
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is GroupSelectionBox)
                {
                    GroupSelectionBox grpFoundBox = ((GroupSelectionBox)Controls[i]);
                    if (grpBox != grpFoundBox)
                        grpFoundBox.SetSelected(false);
                    else
                        grpFoundBox.SetSelected(true);
                }
            }

            // Handle action(s)...:
            if (grpBox == btnGeneral)
                SetTab(0);
            else if (grpBox == btnDeveloper)
                SetTab(1);
        }

        private void SetTab(int iTAB)
        {
            m_iPageTAB = iTAB;

            checkBoxStartWindows.Visible = (iTAB == 0);

            btnSelectedMirror.Visible = (iTAB == 0);
            listMirrors.Visible = false;
            labelDownloadMirrors.Visible = (iTAB == 0);

            labelTheme.Visible = (iTAB == 0);
            listThemes.Visible = false;
            btnSelectedTheme.Visible = (iTAB == 0);

            checkBoxEnableToolTip.Visible = (iTAB == 0);

            checkBoxLaunchWithConsole.Visible = (iTAB == 1);
            labelCustomLaunch.Visible = (iTAB == 1);
            textFieldLaunchOptions.Visible = (iTAB == 1);

            if (iTAB == 0)
            {
                checkBoxEnableToolTip.SetCheckedState(Settings.Default.m_bEnableToolTips);
                btnSelectedMirror.LabelTxt = Settings.Default.szDownloadMirror;
                btnSelectedTheme.LabelTxt = Settings.Default.szSkinName;
                checkBoxStartWindows.SetCheckedState(Globals.WillRunOnWinStart());
            }
            else if (iTAB == 1)
                checkBoxLaunchWithConsole.SetCheckedState(Settings.Default.m_bLaunchWithConsole);

            listThemes.BringToFront();
            listMirrors.BringToFront();
        }

        private void SaveCustomLaunchOptions()
        {
            string szLaunchOptsCustom = textFieldLaunchOptions.GetText();
            Settings.Default.szCustomLaunchOptions = szLaunchOptsCustom;
            Settings.Default.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SettingsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void checkBoxStartWindows_Click(object sender, EventArgs e)
        {
            Globals.RunLauncherOnWinStartup(checkBoxStartWindows.IsChecked());
        }

        private void btnSelectedMirror_Click(object sender, EventArgs e)
        {
            listMirrors.Visible = !listMirrors.Visible;
        }

        private void btnSelectedTheme_Click(object sender, EventArgs e)
        {
            listThemes.Visible = !listThemes.Visible;
        }

        private void checkBoxLaunchWithConsole_Click(object sender, EventArgs e)
        {
            Settings.Default.m_bLaunchWithConsole = checkBoxLaunchWithConsole.IsChecked();
            Settings.Default.Save();
        }

        private void checkBoxEnableToolTip_Click(object sender, EventArgs e)
        {
            bool bVal = checkBoxEnableToolTip.IsChecked();
            Settings.Default.m_bEnableToolTips = bVal;
            Settings.Default.Save();
            Globals.GetBaseForm().GetGameForm().SetToolTipState(bVal);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveCustomLaunchOptions();
            Globals.HandleClosingOfForm(0);
            base.OnClosing(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
