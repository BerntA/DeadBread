//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Global/Shared Class containing most of the logic necessary to do stuff in this program.
//
//=============================================================================================//

using DeadBread.Controls;
using DeadBread.Database;
using DeadBread.Filesystem;
using DeadBread.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread.Base
{
    public static class Globals
    {
        // PUBLIC 
        // Application General
        public static string GetAppPath() { return Application.StartupPath; }
        public static bool IsDownloadingBaseGame() { return _m_bDownloadingBaseGame; } // An engine or base build for a game?
        public static void SetDownloadingBaseGame(bool value) { _m_bDownloadingBaseGame = value; }

        // User Info + Accessors
        public static bool IsInGame() { return _m_bIsInGame; } // In game?

        // Misc   
        public static int GetSelectedGame() { return _m_iSelectedGame; }
        public static void SetSelectedGame(int iVal, bool bSwitch = false) { _m_iSelectedGame = iVal; if (bSwitch) _baseFormAccessor.GetGameForm().SelectGame(iVal); }
        public static string GetGoldSrcPath() { return _szGoldsourceModPath; }

        public static string GetLatestVersion() { return _pchServerVersion; }
        public static string GetMaintenanceMessage() { return _maintenanceMessage; }
        public static string GetUpdateURL() { return _pchUpdateURL; }

        // Form Accessors
        public static SettingsForm GetSettingsForm() { return _settingsForm; }
        public static BaseForm GetBaseForm() { return _baseFormAccessor; }
        public static SDKForm GetSDKForm() { return _sdkForm; }

        // Lists & Struct Items:
        public static List<gameInfo_t> GetGameDataList() { return pszGameDataList; }
        public static gameInfo_t GetActiveGameItem()
        {
            for (int i = 0; i < GetGameDataList().Count; i++)
            {
                if (GetGameDataList()[i].gameID == GetSelectedGame())
                    return GetGameDataList()[i];
            }

            return GetGameDataList()[0];
        }
        public static Dictionary<string, string> GetDownloadMirrors() { return pszDownloadMirrors; }
        public static List<engineInfo_t> GetEngineDataList() { return pszEngineDataList; }
        public static string GetEnginePackage(string engine)
        {
            for (int i = 0; i < pszEngineDataList.Count; i++)
            {
                if (pszEngineDataList[i].title == engine)
                    return pszEngineDataList[i].package_url_base;
            }

            return null;
        }

        public static bool GameCanUseEditorTools()
        {
            if (!GetActiveGameItem().isMod || string.IsNullOrEmpty(GetEnginePackage(GetActiveGameItem().engine)) || (GetActiveGameItem().engine == "goldsrc"))
                return false;

            return true;
        }

        public static bool GameCanUseServerTools()
        {
            if (!GetActiveGameItem().isMod || (string.IsNullOrEmpty(GetEnginePackage(GetActiveGameItem().engine)) && (GetActiveGameItem().engine != "goldsrc")) ||
                ((GetActiveGameItem().engine == "goldsrc") && string.IsNullOrEmpty(GetGoldSrcPath())))
                return false;

            return true;
        }

        // PRIVATE
        // Application General
        private static SysTrayForm _systemTrayApp;
        private static bool _m_bDownloadingBaseGame = false;

        private static string _pchServerVersion;
        private static string _pchUpdateURL;
        private static string _maintenanceMessage;

        // User Info
        private static bool _m_bIsInGame = false;

        // Game Info
        private static int _m_iSelectedGame = 0;

        // Misc - Thinking - Etc
        private static Timer _clientThink;

        // Lists & Structs
        private static List<gameInfo_t> pszGameDataList;
        private static List<engineInfo_t> pszEngineDataList;

        // Form Accessor(s)
        private static SettingsForm _settingsForm;
        private static BaseForm _baseFormAccessor;
        private static WarningNotifyForm _WarningForm;
        private static SDKForm _sdkForm;

        // Steam Accessors / Path
        private static string _szSteamPath;
        private static string _szGoldsourceModPath;

        // Download System - Handler:
        private static Dictionary<string, string> pszDownloadMirrors;

        // Message Logic - Allows us to run ShowWarning before starting vital processes that will 'freeze' the message GUI (a time in short)
        private static Timer _timMessageDelay;
        private static string szTimerMessage;

        public struct gameInfo_t
        {
            public string title; // Title of the game.
            public string description; // Brief info about the game.
            public string developer; // Who've developed this game/mod?
            public string developer_url; // Developer's homepage, if any.
            public string icon_url; // URL for the game icon.
            public string background_url; // URL for the background.
            public string filetable_url; // The table for game downloads. Includes sdk, servers, etc... (search path)
            public string patchnotes_url; // URL to the patch notes.
            public string news_url; // URL to the news.
            public string manual_url; // Wiki/Documentation URL! If available...
            public string package_url; // Base Content .rar file package for first time downloading.
            public string tabs; // A list of the available main options.
            public string root; // The root folder of every normal user, for instance brainbread2.
            public string startupArgs; // Arguments to use at startup.
            public string appID; // Steam Identifier.
            public string engine; // Which engine does this game run on? If empty we're running a custom engine!!!
            public string version; // Which version is this?
            public int gameID; // ID of this game.
            public bool isMod; // Is this a mod or game?
            public bool isLocked; // Is this item locked?
        };

        public struct engineInfo_t
        {
            public string title; // Title of the engine.
            public string package_url_base; // Base Engine Content for clients & servers.
        };

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetPageName(int iID)
        {
            switch (iID)
            {
                case 0:
                    return "Home";
                case 1:
                    return "News";
                case 2:
                    return "Addons";
                case 3:
                    return "Scoreboard";
                case 4:
                    return "Tools";
            }

            return null;
        }

        public static string GetGamePath(int iID)
        {
            string path = null;

            // Non-Mods return some path from Steam by default, if this dir doesn't exist then we have to promt the user to update it. (on game init(switching game))
            if (!GetGameItemByID(iID).isMod)
            {
                KeyValues pkvData = new KeyValues();
                if (!pkvData.LoadFromFile(string.Format("{0}\\BaseLauncher\\config\\{1}.txt", GetAppPath(), GetGameItemByID(iID).root)))
                    return null;

                path = pkvData.GetString("Path");
                if (path.Equals("default", StringComparison.CurrentCulture))
                    return string.Format("{0}\\steamapps\\common\\{1}\\{1}\\", _szSteamPath, GetGameItemByID(iID).root);
                else
                    return (string.Format("{0}\\", path));
            }

            // Legacy mods (Half-Life 1), they must be directed to the old gold src dir, because we can't launch them from outside half-life.
            if (GetGameItemByID(iID).engine == "goldsrc")
                return string.Format("{0}\\{1}\\", GetGoldSrcPath(), GetGameItemByID(iID).root);

            return string.Format("{0}\\games\\{1}\\{2}\\", GetAppPath(), GetGameItemByID(iID).engine, GetGameItemByID(iID).root);
        }

        public static void SetGamePath(string path)
        {
            string game_settings =
@"""Settings""
{
    ""Path"" ""%s1""
}
";
            game_settings = game_settings.Replace("%s1", path);
            File.WriteAllText(string.Format("{0}\\BaseLauncher\\config\\{1}.txt", GetAppPath(), GetActiveGameItem().root), game_settings);
        }

        public static gameInfo_t GetGameItemByID(int iID)
        {
            for (int i = 0; i < pszGameDataList.Count; i++)
            {
                if (pszGameDataList[i].gameID == iID)
                {
                    return pszGameDataList[i];
                }
            }

            return pszGameDataList[0]; // Freaky Friday, I know...
        }

        /// <summary>
        /// Use your prefered mirror to retrieve the appropriate URL for your mirror.
        /// </summary>
        /// <param name="mirror"></param>
        /// <returns></returns>
        public static string GetURLFromMirrorList(string mirror)
        {
            for (int i = 0; i < pszDownloadMirrors.Count; i++)
            {
                if (pszDownloadMirrors.Keys.ToArray()[i] == mirror)
                    return pszDownloadMirrors.Values.ToArray()[i];
            }

            return null;
        }

        // Log all actions / data:
        public static void WriteToLogFile(string message)
        {
            using (StreamWriter write = new StreamWriter(string.Format("{0}\\logs\\event_log.log", GetAppPath()), true))
            {
                write.Write(message + "\n");
            }
        }

        /// <summary>
        /// Base Init:
        /// </summary>
        public static void Initialize()
        {
            // Vital checks:
            if (!CanOpenLauncher())
            {
                MessageBox.Show("You're missing a vital program!\nThe launcher will not function until this gets fixed!", "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return;
            }

            if (!CheckForInternetConnection())
            {
                MessageBox.Show("You're not connected to the internet, please restart the launcher when you've regained internet connectivity!", "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return;
            }

            // Initialize other base accessors:
            pszGameDataList = new List<gameInfo_t>();
            pszEngineDataList = new List<engineInfo_t>();
            pszDownloadMirrors = new Dictionary<string, string>();

            // Regenerate default folders... (if deleted)
            Directory.CreateDirectory(string.Format("{0}\\logs", GetAppPath()));
            Directory.CreateDirectory(string.Format("{0}\\temp", GetAppPath()));
            Directory.CreateDirectory(string.Format("{0}\\games", GetAppPath()));

            // Save Steam Dir
            InitAndLookupSteamFolders();

            // Run every sec:
            _clientThink = new Timer();
            _clientThink.Interval = 1000;
            _clientThink.Tick += new EventHandler(HandleThinking);
            _clientThink.Enabled = false;

            DownloadHandler.Initialize();
            DataHandler.Initialize();

            // Create our timers and workers:
            _timMessageDelay = new Timer();
            _timMessageDelay.Tick += new EventHandler(onTickMessageTimer);
            _timMessageDelay.Interval = 1000;
            _timMessageDelay.Enabled = false;

            BaseForm pBase = new BaseForm();
            _baseFormAccessor = pBase;

            Application.Run(pBase); // Late Run.
        }

        public static void LoadLauncherData(string version, string updateURL, string maintenanceMsg)
        {
            _pchServerVersion = version;
            _pchUpdateURL = updateURL;
            _maintenanceMessage = maintenanceMsg;
        }

        /// <summary>
        /// Called upon finishing the update checker...
        /// </summary>
        public static void LaunchApp()
        {
            _baseFormAccessor.InitBaseLauncher();

            // Gather all available games:
            if (pszGameDataList.Count > 0)
            {
                SetSelectedGame(GetGameDataList()[0].gameID);
                _baseFormAccessor.GetGameForm().PerformFirstTimeSetup(GetGameDataList()[0].gameID);
            }

            _clientThink.Enabled = true;
        }

        // Handle Forms & Windows
        public static void MinimizeToSystemTray()
        {
            _baseFormAccessor.GetGameForm().Visible = false;
            _baseFormAccessor.GetGameForm().ShowInTaskbar = false;
            _baseFormAccessor.GetGameForm().BringToFront();

            if (_systemTrayApp == null)
                _systemTrayApp = new SysTrayForm();
        }

        public static void RestoreLauncherToTaskbar()
        {
            if (_baseFormAccessor.GetGameForm() != null)
            {
                _baseFormAccessor.GetGameForm().Visible = true;
                _baseFormAccessor.GetGameForm().ShowInTaskbar = true;
                _baseFormAccessor.GetGameForm().WindowState = FormWindowState.Normal;
                _baseFormAccessor.GetGameForm().BringToFront();
                _baseFormAccessor.GetGameForm().QuickRefresh();
            }

            if (_systemTrayApp != null)
            {
                _systemTrayApp.Close();
                _systemTrayApp.Dispose();
                _systemTrayApp = null;
            }
        }

        public static void ShowWarning(string message, int iWarningMode = 0)
        {
            if (_baseFormAccessor.GetGameForm() != null)
            {
                if (_WarningForm != null)
                {
                    _WarningForm.Close();
                    _WarningForm.Dispose();
                    _WarningForm = null;
                }

                _WarningForm = new WarningNotifyForm(message, iWarningMode);
                _WarningForm.Show();
            }
        }

        public static DialogResult ShowWarning(string message)
        {
            if (_baseFormAccessor.GetGameForm() != null)
            {
                if (_WarningForm != null)
                {
                    _WarningForm.Close();
                    _WarningForm.Dispose();
                    _WarningForm = null;
                }

                _WarningForm = new WarningNotifyForm(message, 2);

                return _WarningForm.ShowDialog(_baseFormAccessor.GetGameForm());
            }

            return DialogResult.None;
        }

        public static DialogResult ShowWarning(Form link, string message)
        {
            if (_baseFormAccessor.GetGameForm() != null)
            {
                if (_WarningForm != null)
                {
                    _WarningForm.Close();
                    _WarningForm.Dispose();
                    _WarningForm = null;
                }

                _WarningForm = new WarningNotifyForm(message, 2);
                return _WarningForm.ShowDialog(link);
            }

            return DialogResult.None;
        }

        public static void ExitLauncher()
        {
            if (_systemTrayApp != null)
            {
                _systemTrayApp.Close();
                _systemTrayApp.Dispose();
                _systemTrayApp = null;
            }

            if (_clientThink != null)
                _clientThink.Enabled = false; // Prevent our thinking from adding us again.

            SendClientCommand(null, "LAUNCHER_EXIT", 1000);
        }

        public static bool WillRunOnWinStart()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (rkApp.GetValue("DeadBread") != null)
                return true;

            return false;
        }

        public static void RunLauncherOnWinStartup(bool bState)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (bState)
                rkApp.SetValue("DeadBread", Application.ExecutablePath.ToString());
            else
                rkApp.DeleteValue("DeadBread", false);
        }

        /// <summary>
        /// Input: Game Title, ID or Base Root...
        /// </summary>
        /// <param name="game"></param>
        public static void PerformGameSelectionByGameName(string game)
        {
            for (int i = 0; i < GetGameDataList().Count; i++)
            {
                if ((game == GetGameDataList()[i].title) || (game == GetGameDataList()[i].root) || (game == GetGameDataList()[i].gameID.ToString()))
                {
                    PerformGameSelection(GetGameDataList()[i].gameID);
                    break;
                }
            }
        }

        public static bool CanLaunchGame(string gameIDString)
        {
            int iGameID = -1;
            for (int i = 0; i < GetGameDataList().Count; i++)
            {
                if ((gameIDString == GetGameDataList()[i].title) || (gameIDString == GetGameDataList()[i].root) || (gameIDString == GetGameDataList()[i].gameID.ToString()))
                {
                    iGameID = i;
                    break;
                }
            }

            if (iGameID != -1)
            {
                gameInfo_t pszActiveItem = GetGameDataList()[iGameID];
                if (pszActiveItem.isLocked)
                    return false;

                return true;
            }

            return false;
        }

        public static void PerformGameSelection(int iVal)
        {
            SetSelectedGame(iVal, true);
        }

        public static void NavigateToPage(int iPageID)
        {
            // Callback to game form, indicate that we WANT to change the page!
            _baseFormAccessor.GetGameForm().ChangePage(iPageID);
        }

        public static Image GetTextureImage(string localpath, bool excludeSkin = false)
        {
            try
            {
                string path = string.Format("{0}\\BaseLauncher\\{1}\\{2}", GetAppPath(), GetTextureDirectory(excludeSkin), localpath);
                if (!File.Exists(path))
                    path = string.Format("{0}\\BaseLauncher\\textures\\{1}", GetAppPath(), localpath);

                return Image.FromFile(path);
            }
            catch
            {
                return Resources.error;
            }
        }

        public static string GetTexturePath(string localpath, bool excludeSkin = false)
        {
            return string.Format("{0}\\BaseLauncher\\{1}\\{2}", GetAppPath(), GetTextureDirectory(excludeSkin), localpath);
        }

        public static string GetClientGameVersion(int iGameID) // IF N/A that means you don't own the game!!!
        {
            string versionFilePath = string.Format("{0}Version.txt", GetGamePath(iGameID));
            if (!File.Exists(versionFilePath))
                return "N/A";

            string version = "N/A";
            KeyValues pkvVersionData = new KeyValues();
            if (pkvVersionData.LoadFromFile(versionFilePath))
                version = pkvVersionData.GetString("Game", "N/A");

            pkvVersionData.Dispose();
            return version;
        }

        // Cancel, Resume or Pause a download:
        // 0 == Cancel
        // 1 == Pause
        // 2 == Resume
        public static void HandleDownloadState(int iState)
        {
            if (DownloadHandler.GetDownloadQueue().Count <= 0)
                return;

            switch (iState)
            {
                case 0:
                    DownloadHandler.Stop(true);
                    break;

                case 1:
                    DownloadHandler.Stop(false);
                    break;

                case 2:
                    DownloadHandler.Continue();
                    break;
            }
        }

        /// <summary>
        /// 0 = Settings
        /// 1 = SDK
        /// </summary>
        /// <param name="iForm"></param>
        public static void HandleClosingOfForm(int iForm)
        {
            switch (iForm)
            {
                case 0:
                    _settingsForm = null;
                    break;

                case 1:
                    _sdkForm = null;
                    break;
            }
        }

        public static void SetupSDKWindow()
        {
            if (GetSDKForm() == null)
            {
                _sdkForm = new SDKForm();
                _sdkForm.Show();
            }

            GetSDKForm().SetupSDKForm(GetActiveGameItem().title, GetActiveGameItem().root, GetActiveGameItem().manual_url, GetActiveGameItem().engine);
        }

        public static void ShowSettings()
        {
            if (GetSettingsForm() == null)
            {
                _settingsForm = new SettingsForm();
                _settingsForm.Show();
            }

            _settingsForm.BringToFront();
        }

        public static void RepairGame()
        {
            if (IsDownloadingBaseGame())
            {
                ShowWarning("You have one or more big downloads on going, please wait for them to finish!", 1);
                return;
            }

            if (DownloadHandler.GetDownloadQueue().Count > 0)
            {
                ShowWarning("A download is on going, please wait for it to finish!", 1);
                return;
            }

            if (string.IsNullOrEmpty(_szSteamPath))
            {
                ShowWarning("You must have Steam in order to download/repair this game!", 1);
                return;
            }

            // If we're launching a gold source mod:
            if (string.IsNullOrEmpty(_szGoldsourceModPath) && (GetActiveGameItem().engine == "goldsrc"))
            {
                ShowWarning("You don't have Half-Life 1 properly installed, please restart Steam and the Launcher!", 1);
                return;
            }

            if ((GetActiveGameItem().engine != "goldsrc") && !EngineHandler.HasThisEngine(GetActiveGameItem().engine))
            {
                ShowWarning("You don't have the required engine to download this game!\nClick Play in order to download the engine and the game.\nClick Repair to make sure the downloaded files is up to date/if you delete any.", 1);
                return;
            }

            if (ShowWarning("This will download any missing or outdated files!\nProceed?") != DialogResult.No)
                DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, GetActiveGameItem().filetable_url, string.Format("{0} Repair", GetActiveGameItem().title));
        }

        // Run the game you're currently browsing:
        public static void PlayGame()
        {
            if (IsDownloadingBaseGame())
            {
                ShowWarning("You have one or more big downloads on going, please wait for them to finish!", 1);
                return;
            }

            if (DownloadHandler.GetDownloadQueue().Count > 0)
            {
                ShowWarning("A download is on going, please wait for it to finish!", 1);
                return;
            }

            if (IsInGame())
            {
                ShowWarning("You're already playing a Steam related game!", 1);
                return;
            }

            if (string.IsNullOrEmpty(_szSteamPath))
            {
                ShowWarning("You must have Steam in order to play this game!", 1);
                return;
            }

            // If we're launching a gold source mod:
            if (string.IsNullOrEmpty(_szGoldsourceModPath) && (GetActiveGameItem().engine == "goldsrc"))
            {
                ShowWarning("You don't have Half-Life 1 properly installed, please restart Steam and the Launcher!", 1);
                return;
            }

            if (GetActiveGameItem().isMod)
            {
                // Check if we have the base engine : this doesn't count for HL1!!!
                if (!EngineHandler.HasThisEngine(GetActiveGameItem().engine) && (GetActiveGameItem().engine != "goldsrc"))
                {
                    string message = string.Format("Do you wish to install this game?\nThe {0} engine will be installed as well.", GetActiveGameItem().engine);
                    if (ShowWarning(message) != DialogResult.No)
                    {
                        _m_bDownloadingBaseGame = true;
                        SendClientCommand("You'll download the base engine and game content for this game shortly!\nThis may take a while...", "BASE_DOWNLOAD_FULL", 600);
                    }

                    return;
                }

                // Version Check! Only if it is a mod!
                string clientVersion = GetClientGameVersion(GetActiveGameItem().gameID);
                string dbVersion = GetActiveGameItem().version;
                if (clientVersion != dbVersion)
                {
                    string message = (clientVersion.Contains("N/A")) ? "Install this game?\nIf you already have it then click Repair to retrieve lost files." : "Your version is outdated!\nCheck for updates?";
                    if (ShowWarning(message) != DialogResult.No)
                    {
                        if (clientVersion.Contains("N/A"))
                        {
                            _m_bDownloadingBaseGame = true;
                            SendClientCommand("You'll download the base content for this game shortly!\nThis may take a while...", "BASE_DOWNLOAD_MED", 600);
                        }
                        else
                            DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, GetActiveGameItem().filetable_url, string.Format("{0} Patch {1}", GetActiveGameItem().title, dbVersion));
                    }

                    return;
                }
            }

            // If the game has an appID then that means it's linked to Steam, we then must have Steam up and running to play...
            if (!string.IsNullOrEmpty(GetActiveGameItem().appID))
            {
                if (Process.GetProcessesByName("Steam").Length <= 0)
                {
                    ShowWarning("Steam must be running in order for you to play this game!", 1);
                    return;
                }
            }

            string szArgs = string.Format("{0} {1} {2}", GetActiveGameItem().startupArgs, Settings.Default.szCustomLaunchOptions, (Settings.Default.m_bLaunchWithConsole ? "-console" : ""));

            using (Process _pszGameProcess = new Process())
            {
                _pszGameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                _pszGameProcess.StartInfo.CreateNoWindow = true;
                _pszGameProcess.StartInfo.UseShellExecute = false;
                _pszGameProcess.StartInfo.RedirectStandardOutput = true;
                _pszGameProcess.StartInfo.WorkingDirectory = _szSteamPath;
                _pszGameProcess.StartInfo.FileName = string.Format("{0}\\steam.exe", _szSteamPath);

                if (GetActiveGameItem().engine == "goldsrc")
                    _pszGameProcess.StartInfo.Arguments = string.Format("\"{0}\\steam.exe\" -applaunch {1} -game {2} {3} -steam", _szSteamPath, GetActiveGameItem().appID, GetActiveGameItem().root, szArgs);
                else if (!GetActiveGameItem().isMod)
                    _pszGameProcess.StartInfo.Arguments = string.Format("\"{0}\\steam.exe\" -applaunch {1} {2}", _szSteamPath, GetActiveGameItem().appID, szArgs);
                else
                    _pszGameProcess.StartInfo.Arguments = string.Format("\"{0}\\steam.exe\" -applaunch {1} -game \"{2}\\games\\{3}\\{4}\" {5} -steam", _szSteamPath, GetActiveGameItem().appID, GetAppPath(), GetActiveGameItem().engine, GetActiveGameItem().root, szArgs);

                _pszGameProcess.Start();
            }

            MinimizeToSystemTray();
        }

        // If we use skins, return a diff dir.
        public static string GetTextureDirectory(bool excludeSkin = false)
        {
            string szSkinPath = Properties.Settings.Default.szSkinPath;
            if (string.IsNullOrEmpty(szSkinPath) || excludeSkin)
                return "textures";

            return szSkinPath;
        }

        /// <summary>
        /// Destination & Path to RAR file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dest"></param>
        public static void unRarFile(string path, string dest)
        {
            try
            {
                using (Process unRarProcess = new Process())
                {
                    unRarProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    unRarProcess.StartInfo.CreateNoWindow = true;
                    unRarProcess.StartInfo.UseShellExecute = false;
                    unRarProcess.StartInfo.WorkingDirectory = GetAppPath();
                    unRarProcess.StartInfo.FileName = string.Format("{0}\\UnRAR.exe", GetAppPath());
                    unRarProcess.StartInfo.Arguments = string.Format(" x -o+ -p- \"{0}\" \"{1}\"", path, dest);
                    unRarProcess.Start();
                    unRarProcess.WaitForExit();
                }

                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                WriteToLogFile("Unable to extract or delete package/addon!\nIs the file in use somewhee else?");
            }
        }

        // Make sure we can't launch if these dependencies are missing!
        private static bool CanOpenLauncher()
        {
            if (!File.Exists(string.Format("{0}\\ClientUpdater.exe", GetAppPath())))
                return false;

            return true;
        }

        // Find and set our steam string paths:
        private static void InitAndLookupSteamFolders()
        {
            _szSteamPath = null;
            _szGoldsourceModPath = null;

            try
            {
                RegistryKey regSteamPath = Registry.CurrentUser;
                regSteamPath = regSteamPath.OpenSubKey(@"Software\Valve\Steam");
                _szSteamPath = regSteamPath.GetValue("SteamPath").ToString();
                _szGoldsourceModPath = regSteamPath.GetValue("ModInstallPath").ToString();
            }
            catch
            {
                WriteToLogFile("Tried to open a registry key that didn't exist!");
            }
        }

        // Handle Important tasks:
        private static void HandleThinking(object sender, EventArgs e)
        {
            if ((Process.GetProcessesByName("hl").Length > 0) || (Process.GetProcessesByName("hl2").Length > 0))
                _m_bIsInGame = true;
            else
                _m_bIsInGame = false;
        }

        /// <summary>
        /// Send a client command, runs a timer which is done in the interval time specified. Delays the action so you have time to show a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="command"></param>
        /// <param name="interval"></param>
        private static void SendClientCommand(string message, string command, int interval = 1000)
        {
            szTimerMessage = command;

            if (!string.IsNullOrEmpty(message))
                ShowWarning(message, 1);

            _timMessageDelay.Interval = interval;
            _timMessageDelay.Enabled = true;
        }

        private static void onTickMessageTimer(object sender, EventArgs e)
        {
            _timMessageDelay.Enabled = false;

            string enginePath = string.Format("{0}\\games\\{1}", GetAppPath(), GetActiveGameItem().engine);
            if (szTimerMessage == "BASE_DOWNLOAD_FULL")
            {
                if (!string.IsNullOrEmpty(GetActiveGameItem().engine))
                    DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, null, "Engine Base", false, (GetActiveGameItem().engine == "goldsrc") ? GetGoldSrcPath() : enginePath, EngineHandler.GetDownloadPackageForEngine(GetActiveGameItem().engine));

                DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, null, string.Format("{0} Game Base", GetActiveGameItem().title), false, (GetActiveGameItem().engine == "goldsrc") ? GetGoldSrcPath() : enginePath, EngineHandler.GetDownloadPackageForGame(GetActiveGameItem().package_url));
                DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, GetActiveGameItem().filetable_url, string.Format("{0} Repair", GetActiveGameItem().title));
            }
            else if (szTimerMessage == "BASE_DOWNLOAD_MED")
            {
                DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, null, string.Format("{0} Game Base", GetActiveGameItem().title), false, (GetActiveGameItem().engine == "goldsrc") ? GetGoldSrcPath() : enginePath, EngineHandler.GetDownloadPackageForGame(GetActiveGameItem().package_url));
                DownloadHandler.AddDownloadToQueue(GetActiveGameItem().gameID, GetActiveGameItem().filetable_url, string.Format("{0} Repair", GetActiveGameItem().title));
            }
            else if (szTimerMessage == "LAUNCHER_EXIT")
                Application.Exit();
        }
    }
}
