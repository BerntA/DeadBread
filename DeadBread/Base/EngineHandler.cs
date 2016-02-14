//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Integrated Engine Information. Which engines do we support and how to handle them?
//
//=============================================================================================//

using DeadBread.Controls;
using DeadBread.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadBread.Base
{
    public static class EngineHandler
    {
        static string[] szEngines = { "source", "goldsrc" };
        static string[] szAppIDs = { "SDK 2013 MP", "SDK 2013 SP", "SDK 2007", "SDK 2006", "Half-Life", "Half-Life 2" };

        public static string GetAppIDGame(long game)
        {
            if (game == 243750)
                return szAppIDs[0];
            else if (game == 243730)
                return szAppIDs[1];
            else if (game == 218)
                return szAppIDs[2];
            else if (game == 215)
                return szAppIDs[3];
            else if (game == 70)
                return szAppIDs[4];
            else if (game == 220)
                return szAppIDs[5];

            return null;
        }

        public static string GetEngineFromGame(string game)
        {
            if (game == szAppIDs[0])
                return "source";
            else if (game == szAppIDs[1])
                return "source";
            else if (game == szAppIDs[2])
                return "source";
            else if (game == szAppIDs[3])
                return "source";
            else if (game == szAppIDs[4])
                return "goldsrc";
            else if (game == szAppIDs[5])
                return "source";

            return null;
        }

        public static long GetAppIDFromName(string game)
        {
            if (game == szAppIDs[0])
                return 243750;
            else if (game == szAppIDs[1])
                return 243730;
            else if (game == szAppIDs[2])
                return 218;
            else if (game == szAppIDs[3])
                return 215;
            else if (game == szAppIDs[4])
                return 70;
            else if (game == szAppIDs[5])
                return 220;

            return 0;
        }

        public static string GetDedicatedServerExecutable(string engine)
        {
            switch (engine)
            {
                case "source":
                    return string.Format("{0}\\games\\source\\srcds.exe", Globals.GetAppPath());
                case "goldsrc":
                    return string.Format("{0}\\hlds.exe", Globals.GetGoldSrcPath());
            }

            return null;
        }

        public static string GetLevelEditorExecutable()
        {
            switch (Globals.GetActiveGameItem().engine)
            {
                case "source":
                    return string.Format("{0}\\games\\source\\bin\\hammer.exe", Globals.GetAppPath());
            }

            return null;
        }

        public static string GetModelEditorExecutable()
        {
            switch (Globals.GetActiveGameItem().engine)
            {
                case "source":
                    return string.Format("{0}\\games\\source\\bin\\hlmv.exe", Globals.GetAppPath());
            }

            return null;
        }

        public static string GetFacePoserExecutable()
        {
            switch (Globals.GetActiveGameItem().engine)
            {
                case "source":
                    return string.Format("{0}\\games\\source\\bin\\hlfaceposer.exe", Globals.GetAppPath());
            }

            return null;
        }

        public static string GetDownloadPackageForEngine(string engine)
        {
            return string.Format("{0}{1}", Globals.GetURLFromMirrorList(Settings.Default.szDownloadMirror), Globals.GetEnginePackage(engine));
        }

        public static string GetDownloadPackageForGame(string package)
        {
            return string.Format("{0}{1}", Globals.GetURLFromMirrorList(Settings.Default.szDownloadMirror), package);
        }

        public static void InitLevelEditor()
        {
            switch (Globals.GetActiveGameItem().engine)
            {
                case "source":
                    {
                        string GameConfig = Resources.hammerTemplate;

                        GameConfig = GameConfig.Replace("%s1", Globals.GetActiveGameItem().title);
                        GameConfig = GameConfig.Replace("%s2", Globals.GetActiveGameItem().root);

                        string path = string.Format("{0}\\games\\source\\bin\\GameConfig.txt", Globals.GetAppPath());
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllText(path, GameConfig);
                        break;
                    }
            }
        }

        public static bool HasThisEngine(string engine)
        {
            switch (engine)
            {
                case "source":
                    return File.Exists(string.Format("{0}\\games\\source\\srcds.exe", Globals.GetAppPath()));
            }

            return false;
        }

        public static void AddMaps(string engine, string root, ItemList mapList, ListButton selectedMap)
        {
            selectedMap.Enabled = false;

            switch (engine)
            {
                // for source we search all .bsp files in the maps, addons and custom folder. TODO: Parse gameinfo.txt for search paths?
                case "source":
                    {
                        string[] searchPaths = 
                        { 
                             string.Format("{0}\\games\\{1}\\{2}\\addons\\", Globals.GetAppPath(), engine, root), 
                             string.Format("{0}\\games\\{1}\\{2}\\custom\\", Globals.GetAppPath(), engine, root), 
                             string.Format("{0}\\games\\{1}\\{2}\\maps\\", Globals.GetAppPath(), engine, root), 
                        };

                        if (Directory.Exists(searchPaths[0]))
                        {
                            foreach (string map in Directory.EnumerateFiles(searchPaths[0], "*.bsp", SearchOption.AllDirectories))
                            {
                                mapList.AddItem(Path.GetFileNameWithoutExtension(map));
                                selectedMap.Enabled = true;
                            }
                        }

                        if (Directory.Exists(searchPaths[1]))
                        {
                            foreach (string map in Directory.EnumerateFiles(searchPaths[1], "*.bsp", SearchOption.AllDirectories))
                            {
                                mapList.AddItem(Path.GetFileNameWithoutExtension(map));
                                selectedMap.Enabled = true;
                            }
                        }

                        if (Directory.Exists(searchPaths[2]))
                        {
                            foreach (string map in Directory.EnumerateFiles(searchPaths[2], "*.bsp", SearchOption.TopDirectoryOnly))
                            {
                                mapList.AddItem(Path.GetFileNameWithoutExtension(map));
                                selectedMap.LabelTxt = Path.GetFileNameWithoutExtension(map);
                                selectedMap.Enabled = true;
                            }
                        }

                        break;
                    }
                case "goldsrc":
                    {
                        string[] searchPaths = 
                        { 
                             string.Format("{0}\\{1}\\maps\\", Globals.GetGoldSrcPath(), root), 
                        };

                        if (Directory.Exists(searchPaths[0]))
                        {
                            foreach (string map in Directory.EnumerateFiles(searchPaths[0], "*.bsp", SearchOption.TopDirectoryOnly))
                            {
                                mapList.AddItem(Path.GetFileNameWithoutExtension(map));
                                selectedMap.LabelTxt = Path.GetFileNameWithoutExtension(map);
                                selectedMap.Enabled = true;
                            }
                        }

                        break;
                    }
            }

            if (!selectedMap.Enabled)
                Globals.ShowWarning("No maps were found!", 1);
        }
    }
}
