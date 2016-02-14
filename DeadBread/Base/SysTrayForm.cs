//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: System Tray Form, spawns when you close the launcher.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread.Base
{
    public partial class SysTrayForm : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public SysTrayForm()
        {
            InitializeComponent();

            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();

            List<Globals.gameInfo_t> gameList = Globals.GetGameDataList();
            MenuItem[] playItems = new MenuItem[gameList.Count];
            for (int i = 0; i < gameList.Count; i++)
                playItems[i] = new MenuItem(gameList[i].title, OnPlay);

            trayMenu.MenuItems.Add("Play", playItems);
            trayMenu.MenuItems.Add("Open", OnReOpen);
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "DeadBread";
            trayIcon.Icon = new Icon(Globals.GetTexturePath("icon.ico", true), 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += OnReOpen;

            trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            trayIcon.BalloonTipText = "DeadBread will be running in the background...";
            trayIcon.BalloonTipTitle = "Info";
            trayIcon.ShowBalloonTip(500);
        }

        private void OnReOpen(object sender, EventArgs e)
        {
            trayIcon.Dispose();
            trayMenu.Dispose();
            Globals.RestoreLauncherToTaskbar();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Globals.ExitLauncher();
        }

        private void OnPlay(object sender, EventArgs e)
        {
            string szGame = ((MenuItem)sender).Text;

            Globals.PerformGameSelectionByGameName(szGame);

            if (Globals.CanLaunchGame(szGame))
                Globals.PlayGame();
            else
                Globals.ShowWarning("The game is temporarily locked!", 1);
        }

        private void SysTrayForm_Load(object sender, EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
        }
    }
}
