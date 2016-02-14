//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Handle 'database' related stuff, such as fetching the game, engine, app, mirror and file data.
//
//=============================================================================================//

using DeadBread.Base;
using DeadBread.Filesystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeadBread.Database
{
    public static class DataHandler
    {
        static KeyValues appManifestData = null;
        static KeyValues appData = null;
        static KeyValues gameData = null;
        static KeyValues mirrorData = null;
        static KeyValues engineData = null;
        public static bool Initialize()
        {
            appManifestData = new KeyValues();
            if (appManifestData.LoadFromFile(string.Format("{0}\\BaseLauncher\\config\\manifest.txt", Globals.GetAppPath())))
            {
                return LoadData();
            }

            Globals.WriteToLogFile("Unable to parse manifest file!");
            return false;
        }

        public static bool ReloadData()
        {
            if (appManifestData == null || appData == null || gameData == null || mirrorData == null || engineData == null)
                return false;

            appData.Dispose();
            appData = null;

            gameData.Dispose();
            gameData = null;

            mirrorData.Dispose();
            mirrorData = null;

            engineData.Dispose();
            engineData = null;

            GC.Collect();

            Globals.GetGameDataList().Clear();
            Globals.GetDownloadMirrors().Clear();
            Globals.GetEngineDataList().Clear();

            LoadAppData(appManifestData.GetString("AppDataURL"));
            LoadGameData(appManifestData.GetString("GameDataURL"));
            LoadMirrorData(appManifestData.GetString("MirrorDataURL"));
            LoadEngineData(appManifestData.GetString("EngineDataURL"));

            if (Globals.GetGameDataList().Count > 0)
                Globals.SetSelectedGame(Globals.GetGameDataList()[0].gameID);

            return true;
        }

        public static bool LoadFileData(List<DownloadHandler.pszFileInfo> list, int gameID)
        {
            if (Globals.GetGameDataList().Count <= 0)
                return false;

            Globals.gameInfo_t info = Globals.GetGameItemByID(gameID);

            KeyValues pkvData = new KeyValues();
            if (!pkvData.LoadFromUrl(info.filetable_url))
                return false;

            for (KeyValues sub = pkvData.GetFirstKey(); sub != null; sub = pkvData.GetNextKey())
            {
                DownloadHandler.pszFileInfo item;
                item.file = sub.GetName();
                item.url = string.Format("{0}{1}/{2}", Globals.GetURLFromMirrorList(Properties.Settings.Default.szDownloadMirror), info.root, sub.GetName());
                item.hash = sub.GetString("Hash");
                item.fileSize = sub.GetLong("Size");
                list.Add(item);
            }

            pkvData.Dispose();
            pkvData = null;

            return true;
        }

        private static bool LoadData()
        {
            if (appManifestData == null)
                return false;

            LoadAppData(appManifestData.GetString("AppDataURL"));
            LoadGameData(appManifestData.GetString("GameDataURL"));
            LoadMirrorData(appManifestData.GetString("MirrorDataURL"));
            LoadEngineData(appManifestData.GetString("EngineDataURL"));

            return true;
        }

        private static void LoadAppData(string url)
        {
            appData = new KeyValues();
            if (!appData.LoadFromUrl(url))
            {
                Globals.WriteToLogFile("Unable to load AppData!");
                return;
            }

            Globals.LoadLauncherData(appData.GetString("Version"), appData.GetString("UpdateURL"), appData.GetString("Maintenance"));
        }

        private static void LoadGameData(string url)
        {
            gameData = new KeyValues();
            if (!gameData.LoadFromUrl(url))
            {
                Globals.WriteToLogFile("Unable to load GameData!");
                return;
            }

            int index = 0;
            for (KeyValues sub = gameData.GetFirstKey(); sub != null; sub = gameData.GetNextKey())
            {
                Globals.gameInfo_t item;
                item.title = sub.GetString("Title");
                item.description = sub.GetString("Description");
                item.developer = sub.GetString("Developer");
                item.developer_url = sub.GetString("DeveloperURL");
                item.patchnotes_url = sub.GetString("PatchNotesURL");
                item.news_url = sub.GetString("NewsURL");
                item.manual_url = sub.GetString("ManualURL");
                item.icon_url = sub.GetString("IconURL");
                item.background_url = sub.GetString("BackgroundURL");
                item.package_url = sub.GetString("PackageURL");
                item.tabs = sub.GetString("Tabs");
                item.root = sub.GetString("BaseRoot");
                item.filetable_url = sub.GetString("FileData");
                item.startupArgs = sub.GetString("StartupArgs");
                item.version = sub.GetString("Version");
                item.appID = sub.GetString("AppID");
                item.engine = sub.GetString("Engine");
                item.isMod = (sub.GetInt("Modification") >= 1);
                item.isLocked = (sub.GetInt("Locked") >= 1);
                item.gameID = index;
                Globals.GetGameDataList().Add(item);
                index++;
            }
        }

        private static void LoadMirrorData(string url)
        {
            mirrorData = new KeyValues();
            if (!mirrorData.LoadFromUrl(url))
            {
                Globals.WriteToLogFile("Unable to load MirrorData!");
                return;
            }

            for (KeyValues sub = mirrorData.GetFirstKey(); sub != null; sub = mirrorData.GetNextKey())
            {
                Globals.GetDownloadMirrors().Add(sub.GetName(), sub.GetString("url"));
            }
        }

        private static void LoadEngineData(string url)
        {
            engineData = new KeyValues();
            if (!engineData.LoadFromUrl(url))
            {
                Globals.WriteToLogFile("Unable to load EngineData!");
                return;
            }

            for (KeyValues sub = engineData.GetFirstKey(); sub != null; sub = engineData.GetNextKey())
            {
                Globals.engineInfo_t item;
                item.title = sub.GetName();
                item.package_url_base = sub.GetString("package_base");
                Globals.GetEngineDataList().Add(item);
            }
        }
    }
}
