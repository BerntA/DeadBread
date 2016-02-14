//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Handle Tools such as level editors, model viewers, dedicated server setups, etc...
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadBread.Base
{
    public static class ToolHandler
    {
        public static bool CanLaunchServerForEngine(string engine)
        {
            string path = EngineHandler.GetDedicatedServerExecutable(engine);
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Globals.ShowWarning(string.Format("You're missing {0}!\nYou won't be able to host a server, try to validate your game files!", Path.GetFileNameWithoutExtension(path)), 1);
                return false;
            }

            return true;
        }

        public static bool StartServer(string engine, string gameroot, string map, string maxplayers)
        {
            string path = EngineHandler.GetDedicatedServerExecutable(engine);
            if (!CanLaunchServerForEngine(engine))
                return false;

            using (Process dedicatedServer = new Process())
            {
                dedicatedServer.StartInfo.UseShellExecute = false;
                dedicatedServer.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                dedicatedServer.StartInfo.FileName = path;
                dedicatedServer.StartInfo.Arguments = string.Format("\"{0}\" -insecure -console -game {1} +maxplayers {2} +exec server.cfg +map {3}", path, gameroot, maxplayers, map);
                dedicatedServer.Start();
            }

            return true;
        }

        public static string GetDefaultServerConfig(string gameroot)
        {
            string path = string.Format("{0}\\BaseLauncher\\config\\{1}_server.txt", Globals.GetAppPath(), gameroot);
            if (File.Exists(path))
            {
                string config = null;
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                        config += string.Format("{0}\n", reader.ReadLine());
                }

                return config;
            }

            return null;
        }

        public static string GetGameCfgPath(string engine, string gameroot)
        {
            if (engine == "goldsrc")
                return string.Format("{0}\\{1}\\server.cfg", Globals.GetGoldSrcPath(), gameroot);

            return string.Format("{0}\\games\\{1}\\{2}\\cfg\\server.cfg", Globals.GetAppPath(), engine, gameroot);
        }
    }
}
