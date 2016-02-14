//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: SDK Form - Allows the user to open some level editor, model editor and faceposer editor.
//
//=============================================================================================//

using DeadBread.Base;
using DeadBread.Controls;
using DeadBread.Filesystem;
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
    public partial class SDKForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string szRoot;
        private string szEngine;
        private string szManual;
        public SDKForm()
        {
            InitializeComponent();

            if (!DesignMode)
                BackgroundImage = Globals.GetTextureImage("tools\\SDKBG.png");
        }

        public void SetupSDKForm(string title, string rootFolder, string manual, string engine)
        {
            szManual = manual;
            szRoot = rootFolder;
            szEngine = engine;
            labelSDKTitle.Text = string.Format("{0} SDK", title);
            Text = labelSDKTitle.Text;

            string hammer = "", hlmv = "", faceposer = "";
            KeyValues pkvData = new KeyValues();
            if (pkvData.LoadFromFile(string.Format("{0}\\BaseLauncher\\config\\{1}_engine.txt", Globals.GetAppPath(), engine)))
            {
                hammer = pkvData.GetString("LevelEditor");
                hlmv = pkvData.GetString("ModelEditor");
                faceposer = pkvData.GetString("Faceposer");
            }

            int baseY = labelSDKTitle.Bounds.Y + labelSDKTitle.Bounds.Height + 1;

            // Init controls:
            if (!string.IsNullOrEmpty(hammer))
            {
                IconButton hammerIco = new IconButton(hammer, "Level Editor", 8, EngineHandler.GetLevelEditorExecutable());
                hammerIco.Parent = this;
                hammerIco.Bounds = new Rectangle((Width / 2) - 75, baseY + 20, 150, 30);
                hammerIco.BringToFront();
                hammerIco.Click += new EventHandler(OnClickedIconItem);
            }

            if (!string.IsNullOrEmpty(hlmv))
            {
                IconButton hlmvIco = new IconButton(hlmv, "Model Viewer", 8, EngineHandler.GetModelEditorExecutable());
                hlmvIco.Parent = this;
                hlmvIco.Bounds = new Rectangle((Width / 2) - 75, baseY + 55, 150, 30);
                hlmvIco.BringToFront();
                hlmvIco.Click += new EventHandler(OnClickedIconItem);
            }

            if (!string.IsNullOrEmpty(faceposer))
            {
                IconButton faceposerIco = new IconButton(faceposer, "Faceposer", 8, EngineHandler.GetFacePoserExecutable());
                faceposerIco.Parent = this;
                faceposerIco.Bounds = new Rectangle((Width / 2) - 75, baseY + 90, 150, 30);
                faceposerIco.BringToFront();
                faceposerIco.Click += new EventHandler(OnClickedIconItem);
            }

            EngineHandler.InitLevelEditor();

            BringToFront();
        }

        private void OnClickedIconItem(object sender, EventArgs e)
        {
            string path = ((IconButton)sender).GetCommand();

            if (!File.Exists(path))
            {
                Globals.WriteToLogFile("Tried to open an executable that didn't exist on the local computer!");
                Globals.ShowWarning(string.Format("Can't find the specified file: {0}", path), 1);
                return;
            }

            using (Process _pszGameProcess = new Process())
            {
                _pszGameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                _pszGameProcess.StartInfo.CreateNoWindow = true;
                _pszGameProcess.StartInfo.UseShellExecute = false;
                _pszGameProcess.StartInfo.RedirectStandardOutput = true;
                _pszGameProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                _pszGameProcess.StartInfo.FileName = path;
                _pszGameProcess.StartInfo.Arguments = string.Format("\"{0}\" -game \"{1}\\games\\{2}\\{3}\"", path, Globals.GetAppPath(), szEngine, szRoot);
                _pszGameProcess.Start();
            }

            WindowState = FormWindowState.Minimized;
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labelDocumentation_Click(object sender, EventArgs e)
        {
            Process.Start(szManual);
            WindowState = FormWindowState.Minimized;
        }

        private void SDKForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Globals.HandleClosingOfForm(1);
            base.OnClosing(e);
        }
    }
}
