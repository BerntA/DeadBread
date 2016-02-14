//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Launch Form - Checks for updates and fetches important data before opening the main form.
//
//=============================================================================================//

using DeadBread.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class UpdateForm : Form
    {
        private Timer messageTimer;
        private string maintenance = null;
        public UpdateForm()
        {
            InitializeComponent();

            labelVersion.Text = Application.ProductVersion;

            if (!DesignMode)
                BackgroundImage = Globals.GetTextureImage("controls\\LoginBG.png");

            messageTimer = new Timer();
            messageTimer.Tick += new EventHandler(onMsgTick);
            messageTimer.Interval = 1000;
            messageTimer.Enabled = false;
            messageTimer.Stop();
        }

        private void SendMessage(string message, int interval = 1000)
        {
            labelStatus.Text = message;
            messageTimer.Interval = interval;
            messageTimer.Enabled = true;
            messageTimer.Start();
        }

        private void onMsgTick(object sender, EventArgs e)
        {
            messageTimer.Enabled = false;
            messageTimer.Stop();

            string message = labelStatus.Text;
            if (message == "Checking for updates...")
            {
                string myVersion = Application.ProductVersion;
                string currentVersion = Globals.GetLatestVersion();
                maintenance = Globals.GetMaintenanceMessage();

                if (!string.IsNullOrEmpty(maintenance))
                {
                    SendMessage(maintenance, 2500);
                    return;
                }

                if (currentVersion != myVersion)
                {
                    SendMessage("Update found, downloading!");
                    return;
                }

                SendMessage("Launching!");
            }
            else if (message == "Restarting!")
            {
                using (Process updateLauncher = new Process())
                {
                    updateLauncher.StartInfo.UseShellExecute = false;
                    updateLauncher.StartInfo.Arguments = "-update";
                    updateLauncher.StartInfo.WorkingDirectory = Globals.GetAppPath();
                    updateLauncher.StartInfo.FileName = string.Format("{0}\\ClientUpdater.exe", Globals.GetAppPath());
                    updateLauncher.Start();
                }
            }
            else if (message == "Fetching data...")
            {
                // Download Game Icons & Bgs...
                asyncUserData.RunWorkerAsync();

                SendMessage("Checking for updates...");
            }
            else if (message == "Update found, downloading!")
            {
                string update_link = Globals.GetUpdateURL();
                using (WebClient launcher_updat = new WebClient())
                {
                    launcher_updat.DownloadFile(update_link, string.Format("{0}\\DeadBreadClientApp.rar", Globals.GetAppPath()));
                    SendMessage("Restarting!");
                }
            }
            else if (message == "Launching!")
            {
                timEnd.Enabled = true;
            }
            else if (message == maintenance)
            {
                timQuit.Enabled = true;
            }
        }

        private void timFader_Tick(object sender, EventArgs e)
        {
            Opacity += 0.05;
            if (Opacity >= .95)
            {
                Opacity = 1;
                timFader.Enabled = false;
                SendMessage("Fetching data...", 1000);
            }
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            timFader.Enabled = true;
        }

        private void timEnd_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.05;
            if (Opacity <= .05)
            {
                Opacity = 0;
                timEnd.Enabled = false;
                Dispose();
                Globals.LaunchApp();
            }
        }

        private void asyncUserData_DoWork(object sender, DoWorkEventArgs e)
        {
            // Download the game icon and background.
            for (int i = 0; i < Globals.GetGameDataList().Count; i++)
            {
                WebClient game_dl = new WebClient();

                try
                {
                    game_dl.DownloadFile(Globals.GetGameDataList()[i].icon_url, string.Format("{0}\\temp\\{1}_icon.png", Globals.GetAppPath(), Globals.GetGameDataList()[i].gameID));
                    game_dl.DownloadFile(Globals.GetGameDataList()[i].background_url, string.Format("{0}\\temp\\{1}_bg.png", Globals.GetAppPath(), Globals.GetGameDataList()[i].gameID));
                }
                catch
                {
                    Globals.WriteToLogFile(string.Format("Failed to download the background and icon for {0}", Globals.GetGameDataList()[i].title));
                }
                finally
                {
                    game_dl.Dispose();
                }
            }
        }

        private void timQuit_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.05;
            if (Opacity <= .05)
            {
                Opacity = 0;
                timQuit.Enabled = false;
                Close();
                Globals.ExitLauncher();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Globals.ExitLauncher();
        }
    }
}
