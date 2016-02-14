//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Server Form - Allows the user to host a server for some game.
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread
{
    public partial class ServerForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string szCFGPath;
        private string szGame;
        private string szRoot;
        private string szEngine;
        private ConVarList varList;
        public ServerForm(string game, string root, string engine)
        {
            InitializeComponent();

            if (!DesignMode)
                BackgroundImage = Globals.GetTextureImage("controls\\LoginBG.png");

            szRoot = root;
            szGame = game;
            szEngine = engine;

            string title = string.Format("{0} Server", szGame);
            this.Text = title;
            labelName.Text = title;

            textMaxPlrs.SetText("10");
            textTimelimit.SetText("30");
            mapList.Visible = false;
            mapList.bUseFixedWidth = false;
            mapList.OnItemClick += new EventHandler(OnSelectMap);

            szCFGPath = ToolHandler.GetGameCfgPath(szEngine, szRoot);

            // Add maps:
            EngineHandler.AddMaps(szEngine, szRoot, mapList, selectedMap);

            textHostname.SetText(string.Format("{0} Dedicated Server", szGame));
            mapList.BringToFront();

            varList = new ConVarList(szRoot, szCFGPath);
            varList.Parent = this;
            varList.Bounds = new Rectangle(0, labelMap.Bounds.Y, 400, Height - labelMap.Bounds.Y - 1);
            varList.Visible = false;
        }

        private void StartServer()
        {
            if (!ToolHandler.StartServer(szEngine, szRoot, selectedMap.LabelTxt, textMaxPlrs.GetText()))
                return;

            Dispose();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (varList.Visible)
                varList.Visible = false;
            else
                Dispose();
        }

        private void OnSelectMap(object sender, EventArgs e)
        {
            string szItem = ((Label)sender).Text;

            selectedMap.LabelTxt = szItem;
            mapList.Visible = false;
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            // Setup Stuff:
            Directory.CreateDirectory(Path.GetDirectoryName(szCFGPath));

            if (File.Exists(szCFGPath))
                File.Delete(szCFGPath);

            string players = textMaxPlrs.GetText();

            try
            {
                int iPlayers = int.Parse(players);
                if (iPlayers <= 0 || iPlayers > 128)
                    players = "10";
            }
            catch
            {
                players = "10";
            }
            finally
            {
                textMaxPlrs.SetText(players);

                WriteToConfig("hostname " + "\"" + textHostname.GetText() + "\"");
                WriteToConfig("mp_timelimit " + textTimelimit.GetText());
                WriteToConfig("sv_password " + "\"" + textPassword.GetText() + "\"");
                WriteToConfig("sv_lan " + (checkLAN.IsChecked() ? 1 : 0).ToString());
                WriteToConfig("rcon_password " + "\"" + textRCON.GetText() + "\"");
                WriteToConfig("");

                // Specified per game:
                string configFile = ToolHandler.GetDefaultServerConfig(szRoot);
                if (!string.IsNullOrEmpty(configFile))
                {
                    WriteToConfig(configFile);
                }

                // Add the game default convars:
                if (varList.m_bCanOpen)
                {
                    WriteToConfig("// Game Specific");
                    varList.WriteConVarsToConfig();
                    WriteToConfig("");
                }

                WriteToConfig("heartbeat"); // MUST BE AT THE END!

                StartServer();
            }
        }

        /// <summary>
        /// Write to the server.cfg file...
        /// </summary>
        /// <param name="text"></param>
        private void WriteToConfig(string text)
        {
            using (StreamWriter writer = new StreamWriter(szCFGPath, true))
            {
                writer.Write(text + Environment.NewLine);
            }
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void selectedMap_Click(object sender, EventArgs e)
        {
            mapList.Visible = !mapList.Visible;
            mapList.BringToFront();
        }

        private void ServerForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
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

        private void btnOther_Click(object sender, EventArgs e)
        {
            if (varList != null)
            {
                if (!varList.m_bCanOpen)
                {
                    Globals.ShowWarning("There's no available convar list for this game!", 1);
                    return;
                }

                varList.Visible = true;
                varList.BringToFront();
            }
        }
    }
}
