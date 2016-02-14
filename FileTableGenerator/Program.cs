//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Create file tables which can be used for version controlling.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileTableGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Please enter a path to some game!");
            string path = string.Format("{0}\\", Console.ReadLine());

            Console.WriteLine("Please enter the name of this file!");
            string fileName = Console.ReadLine();

            string executableDir = AppDomain.CurrentDomain.BaseDirectory;

            if (Directory.Exists(path))
            {
                string filePath = string.Format("{0}\\{1}.txt", executableDir, fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("\"FileData\"");
                    writer.WriteLine("{");

                    foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
                    {
                        Console.WriteLine(string.Format("Adding {0}!", Path.GetFileName(file)));

                        string newFile = null;
                        long fileSize = 0;
                        string hash = "";

                        newFile = file.Replace(path, "");

                        // Fixup slashes.
                        if (newFile.Contains(@"\"))
                            newFile = newFile.Replace(@"\", "/");

                        // Exclude folders & files/extensions:
                        if (newFile.Contains("addons/") || newFile.Contains("download/") || newFile.Contains("downloadlists/") || newFile.Contains("mapsrc/") || newFile.Contains(".cache") || newFile.Contains(".pdb") || newFile.Contains("cfg/config.cfg") || newFile.Contains("'"))
                            continue;

                        // Get file size, new filename and hash all files under 10MB!
                        FileInfo fileInfo = new FileInfo(file);
                        fileSize = fileInfo.Length;
                        if (fileSize < 10485761)
                        {
                            using (var md5 = MD5.Create())
                            {
                                using (var stream = File.OpenRead(file))
                                {
                                    hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                }
                            }
                        }

                        // Write to the file:
                        writer.WriteLine(string.Format("    \"{0}\"", newFile));
                        writer.WriteLine("	{");
                        writer.WriteLine(string.Format("	    \"Hash\" \"{0}\"", hash));
                        writer.WriteLine(string.Format("	    \"Size\" \"{0}\"", fileSize));
                        writer.WriteLine("	}");
                    }

                    writer.WriteLine("}");
                }
            }

            Console.WriteLine("Complete!");
            Console.ReadKey();
        }
    }
}
