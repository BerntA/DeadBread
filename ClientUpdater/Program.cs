//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Update Client - Launched during client updates.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Updating...");

            string path = Environment.CurrentDirectory + @"\DeadBreadClientApp.rar";
            if (CanLaunch(args))
            {
                if (!File.Exists(path))
                    Console.WriteLine("Unable to locate update package!");
                else
                {
                    Console.WriteLine("Closing the launcher!");

                    Process launcherProcess = Process.GetProcessesByName("DeadBread")[0];
                    if (launcherProcess != null)
                    {
                        if (!launcherProcess.HasExited)
                            launcherProcess.Kill();
                    }

                    Thread.Sleep(1000);

                    Console.WriteLine("Extracting!");

                    Process unRarProcess = new Process();
                    unRarProcess.StartInfo.Arguments = string.Format(" x -o+ -p- \"{0}\" \"{1}\"", path, Environment.CurrentDirectory);
                    unRarProcess.StartInfo.CreateNoWindow = true;
                    unRarProcess.StartInfo.FileName = string.Format("{0}\\UnRAR.exe", Environment.CurrentDirectory);
                    unRarProcess.StartInfo.UseShellExecute = false;
                    unRarProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    unRarProcess.StartInfo.RedirectStandardOutput = true;
                    unRarProcess.Start();

                    while (!unRarProcess.StandardOutput.EndOfStream)
                    {
                        string line = unRarProcess.StandardOutput.ReadLine();
                        Console.WriteLine((line + Environment.NewLine));
                    }

                    unRarProcess.WaitForExit();

                    Thread.Sleep(1000);

                    if (File.Exists(path))
                        File.Delete(path);

                    Console.WriteLine("Restarting the launcher!");
                    Process startBB2 = new Process();
                    startBB2.StartInfo.UseShellExecute = false;
                    startBB2.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startBB2.StartInfo.FileName = string.Format("{0}\\DeadBread.exe", Environment.CurrentDirectory);
                    startBB2.Start();
                }

                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("Update failed!");

            Environment.Exit(0);
        }

        private static bool CanLaunch(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("update"))
                    return true;
            }

            return false;
        }
    }
}
