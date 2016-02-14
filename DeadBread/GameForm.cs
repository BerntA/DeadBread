//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Game Form : Handles the available game selections and options per game.
//
//=============================================================================================//

using DeadBread.Base;
using DeadBread.Controls;
using DeadBread.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class GameForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void SetVersionText(string version, int iGameID)
        {
            if (Globals.GetActiveGameItem().gameID == iGameID)
            {
                if (labelVersion.InvokeRequired)
                    labelVersion.Invoke((MethodInvoker)(() => labelVersion.Text = string.Format("Version: {0}", version)));
                else
                    labelVersion.Text = string.Format("Version: {0}", version);
            }
        }

        public void QuickRefresh()
        {
            btnPlay.Reset();
        }

        public void ResetDownloadBar(int iID)
        {
            GameSelectionBox gBox = GetGameSelectionBox(iID);
            gBox.UpdateProgress(0);

            m_lDownloadedBytes = 0;
            downloadBar.Reset();
            downloadBar.UpdateProgress(0, "", "", false);
            downloadBar.Visible = false;
        }

        private long m_lDownloadedBytes;
        private BaseForm _parent;
        private int m_iCurrentPageID;
        public GameForm(BaseForm parent)
        {
            InitializeComponent();
            _parent = parent;

            // Init all image files:
            if (!DesignMode)
            {
                imgPatchNotes.Image = Globals.GetTextureImage("controls\\PatchNotes.png");
                BackgroundImage = Globals.GetTextureImage("game_backgrounds\\default.png");
                downloadBar.Visible = false;

                // Set Events for download bar:
                downloadBar.OnPause += new EventHandler(OnDownloadPause);
                downloadBar.OnResume += new EventHandler(OnDownloadResume);
                downloadBar.OnCancel += new EventHandler(OnDownloadCancel);

                toolBoxSDK.SetImage("SDK", "tools");
                toolBoxServer.SetImage("Server", "tools");
            }

            infoTips.Active = Settings.Default.m_bEnableToolTips;
        }

        // Called at initialization:
        public void PerformFirstTimeSetup(int iVal)
        {
            // Add the Navigation Pages: (their visibility is later decided by the struct item!!!)
            for (int i = 0; i < 5; i++)
            {
                NavigationButton pNavButton = new NavigationButton(i, Globals.GetPageName(i));
                pNavButton.Parent = this;

                // Add new bounds for the btn:
                int fv;

                if (i == 1 || i == 2)
                    fv = 80;
                else if (i == 3)
                    fv = 90;
                else if (i == 4)
                    fv = 95;
                else
                    fv = 100;

                pNavButton.Bounds = new Rectangle(85 + (i * fv), 22, 100, 24);
                pNavButton.BringToFront();
            }

            // Create our selection buttons + link em to the proper id:
            for (int i = 0; i < Globals.GetGameDataList().Count; i++)
            {
                // Perform Base Stuff
                GameSelectionBox pSelectionBox = new GameSelectionBox(2, 70 + (i * 70), 65, 65);
                pSelectionBox.Parent = this;
                pSelectionBox.SetupSelectionBox(Globals.GetGameDataList()[i].title, Globals.GetGameDataList()[i].gameID);
            }

            // Considering we do the same proceedure here except for creating stuff we call our friend:
            SelectGame(iVal);
        }

        // Called whenever you change between available games:
        public void SelectGame(int iVal)
        {
            int iLastIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                bool bState = Globals.GetActiveGameItem().tabs.Contains(Globals.GetPageName(i));

                NavigationButton navButton = GetNavigationButton(i);
                if (navButton == null)
                    continue;

                navButton.Visible = bState;

                // Re-Order Navigation:
                if (bState)
                {
                    int fv = 85;
                    int size = 0;

                    if (i > 0)
                        fv = (GetNavigationButton(iLastIndex).Bounds.X + GetNavigationButton(iLastIndex).Bounds.Width) + 8;

                    if (i == 0)
                        size = 66;
                    else if (i == 1)
                        size = 65;
                    else if (i == 2)
                        size = 98;
                    else if (i == 3)
                        size = 151;
                    else if (i == 4)
                        size = 75;

                    iLastIndex = i;

                    navButton.Bounds = new Rectangle(fv, 14, size, 32);
                    navButton.BringToFront();
                }
            }

            // We know that ALL games have a 'Home' page so we'll always set that to our def start page.
            ChangePage(0);

            labelDeveloperInfo.Visible = true;
            labelDeveloperInfo.Text = string.Format("Developer: {0}", Globals.GetActiveGameItem().developer);

            for (int i = 0; i < Globals.GetGameDataList().Count; i++)
            {
                GameSelectionBox gameBox = GetGameSelectionBox(Globals.GetGameDataList()[i].gameID);
                if (gameBox == null)
                    continue;

                bool bState = (gameBox.GetIDLink() == iVal);

                gameBox.RefreshState(bState);

                if (bState)
                {
                    try
                    {
                        BackgroundImage = Image.FromFile(string.Format("{0}\\temp\\{1}_bg.png", Globals.GetAppPath(), iVal));
                    }
                    catch
                    {
                        BackgroundImage = Globals.GetTextureImage("game_backgrounds\\default.png");
                    }
                }
            }

            // Check our dir, does it exist? (if we're a non-mod we care because that means you can't download it to the path)
            if (!Globals.GetActiveGameItem().isMod)
            {
                if (!Directory.Exists(Globals.GetGamePath(Globals.GetActiveGameItem().gameID)))
                {
                    var warning_dialog = Globals.ShowWarning(this, "You have to setup the directory for this game manually, if you don't then the launcher may not work properly.");
                    if (warning_dialog == System.Windows.Forms.DialogResult.Yes)
                    {
                        FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                        folderDialog.Description = string.Format("Locate the folder for {0}", Globals.GetActiveGameItem().title);
                        folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                        folderDialog.ShowNewFolderButton = true;
                        var folder_dialog = folderDialog.ShowDialog();

                        if (folder_dialog == System.Windows.Forms.DialogResult.OK)
                        {
                            Globals.SetGamePath(folderDialog.SelectedPath);
                            SelectGame(iVal);
                        }

                        folderDialog.Dispose();
                    }
                }
            }
        }

        // Decide which controls should be visible:
        public void ChangePage(int iTAB)
        {
            // Clear the states:
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is NavigationButton)
                {
                    NavigationButton navButton = ((NavigationButton)Controls[i]);
                    if (navButton.GetPageLink() == iTAB)
                        navButton.SetState(true);
                    else
                        navButton.SetState(false);
                }
            }

            DoControlCleanUp();

            // Base + Home
            m_iCurrentPageID = iTAB;
            downloadBar.Visible = false;
            btnPlay.Visible = (iTAB == 0);
            labelVersion.Visible = (iTAB == 0);
            labelVersion.Text = string.Format("Version: {0}", Globals.GetClientGameVersion(Globals.GetActiveGameItem().gameID));
            imgPatchNotes.Visible = (iTAB == 0);
            btnRepair.Visible = ((iTAB == 0) && (Globals.GetActiveGameItem().isMod));
            labelDescription.Visible = (iTAB == 0);
            labelDescription.Text = Globals.GetActiveGameItem().description;

            if (string.IsNullOrEmpty(Globals.GetActiveGameItem().patchnotes_url))
                webBrowser.Visible = false;
            else
            {
                webBrowser.Visible = (iTAB == 0);
                webBrowser.Navigate(Globals.GetActiveGameItem().patchnotes_url);
            }

            if (string.IsNullOrEmpty(Globals.GetActiveGameItem().news_url))
                newsBrowser.Visible = false;
            else
            {
                newsBrowser.Visible = (iTAB == 1);
                newsBrowser.Navigate(Globals.GetActiveGameItem().news_url);
            }

            if (DownloadHandler.GetDownloadQueue().Count > 0)
            {
                if (DownloadHandler.GetDownloadQueue()[0].iGameID == Globals.GetActiveGameItem().gameID)
                {
                    if (m_iCurrentPageID == 0)
                    {
                        if (!downloadBar.Visible)
                            downloadBar.Visible = true;
                    }
                }
            }

            if (btnPlay.Visible)
            {
                bool bEnablePlay = true;

                if (Globals.GetActiveGameItem().isLocked)
                    bEnablePlay = false;

                btnPlay.Enabled = bEnablePlay;
                btnRepair.Enabled = bEnablePlay;
            }

            // Tools
            toolBoxSDK.Visible = (iTAB == 4 && Globals.GameCanUseEditorTools());
            toolBoxServer.Visible = (iTAB == 4 && Globals.GameCanUseServerTools());
        }

        public void SetToolTipState(bool bValue)
        {
            infoTips.Active = bValue;
        }

        // Update DL Progress! Callback (called) from async download progress updater in Globals.
        public void UpdateDownloadProgress(string file, string progress, int iPercent, int m_iGameID, bool bShouldShow = true, bool hideButtons = false, long kiloBytes = 0)
        {
            GameSelectionBox gBox = GetGameSelectionBox(m_iGameID);
            bool bNeedInvoke = downloadBar.InvokeRequired;
            if (!bShouldShow)
            {
                if (bNeedInvoke)
                {
                    downloadBar.Invoke((MethodInvoker)(() => downloadBar.UpdateProgress(iPercent, progress, "", hideButtons)));
                    gBox.Invoke((MethodInvoker)(() => gBox.UpdateProgress(iPercent)));
                }
                else
                {
                    downloadBar.UpdateProgress(iPercent, progress, "", hideButtons);
                    gBox.UpdateProgress(iPercent);
                }
                return;
            }

            // Prevents spamming the invalidate function on the dl and gbox object if there's no difference in the value.
            if (kiloBytes > 0)
            {
                if (m_lDownloadedBytes < kiloBytes)
                    m_lDownloadedBytes = kiloBytes + 50;
                else
                    return;
            }

            if (bNeedInvoke)
                gBox.Invoke((MethodInvoker)(() => gBox.UpdateProgress(iPercent)));
            else
                gBox.UpdateProgress(iPercent);

            if (Globals.GetActiveGameItem().gameID == m_iGameID) // The DL Bar visible in the home area, only update if we're browsing the specific game we're updating.
            {
                if (m_iCurrentPageID == 0)
                {
                    if (bNeedInvoke)
                    {
                        downloadBar.Invoke((MethodInvoker)(() => downloadBar.Visible = true));
                        downloadBar.Invoke((MethodInvoker)(() => downloadBar.UpdateProgress(iPercent, progress, file, hideButtons)));
                    }
                    else
                    {
                        downloadBar.Visible = true;
                        downloadBar.UpdateProgress(iPercent, progress, file, hideButtons);
                    }
                }
                else
                {
                    if (bNeedInvoke)
                        downloadBar.Invoke((MethodInvoker)(() => downloadBar.Visible = false));
                    else
                        downloadBar.Visible = false;
                }
            }
        }

        private void OnDownloadResume(object sender, EventArgs e)
        {
            m_lDownloadedBytes = 0;
            Globals.HandleDownloadState(2);
        }

        private void OnDownloadPause(object sender, EventArgs e)
        {
            m_lDownloadedBytes = 0;
            Globals.HandleDownloadState(1);
        }

        private void OnDownloadCancel(object sender, EventArgs e)
        {
            m_lDownloadedBytes = 0;
            downloadBar.Reset();
            Globals.HandleDownloadState(0);
        }

        private void DoControlCleanUp()
        {
        }

        private NavigationButton GetNavigationButton(int ID)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is NavigationButton)
                {
                    if (((NavigationButton)Controls[i]).GetPageLink() == ID)
                        return ((NavigationButton)Controls[i]);
                }
            }

            return null;
        }

        private GameSelectionBox GetGameSelectionBox(int ID)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is GameSelectionBox)
                {
                    if (((GameSelectionBox)Controls[i]).GetIDLink() == ID)
                        return ((GameSelectionBox)Controls[i]);
                }
            }

            return null;
        }

        private void timFader_Tick(object sender, EventArgs e)
        {
            Opacity += .05;
            if (Opacity >= .95)
            {
                Opacity = 1;
                timFader.Enabled = false;
            }
        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            Globals.MinimizeToSystemTray();
        }

        private void minimizeButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Globals.ExitLauncher();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Globals.PlayGame();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            Globals.RepairGame();
        }

        private void imgReperioStudios_Click(object sender, EventArgs e)
        {
            Globals.ShowSettings();
        }

        private void toolBoxSDK_Click(object sender, EventArgs e)
        {
            Globals.SetupSDKWindow();
        }

        private void toolBoxServer_Click(object sender, EventArgs e)
        {
            if (!ToolHandler.CanLaunchServerForEngine(Globals.GetActiveGameItem().engine))
                return;

            ServerForm server_form = new ServerForm(Globals.GetActiveGameItem().title, Globals.GetActiveGameItem().root, Globals.GetActiveGameItem().engine);
            server_form.Show();
            server_form.BringToFront();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.ExitLauncher();
        }

        private void newsBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString() != Globals.GetActiveGameItem().news_url)
                e.Cancel = true;
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString() != Globals.GetActiveGameItem().patchnotes_url)
                e.Cancel = true;
        }

        private void labelDeveloperInfo_Click(object sender, EventArgs e)
        {
            Process.Start(Globals.GetActiveGameItem().developer_url);
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
