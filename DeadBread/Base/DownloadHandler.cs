//=========       Copyright © Reperio Studios 2013-2016 @ Bernt Andreas Eide!       ============//
//
// Purpose: Download Handlers, handles all downloading, queuing, addons & packages.
//
//=============================================================================================//

using DeadBread.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadBread.Base
{
    public static class DownloadHandler
    {
        public struct pszFileInfo
        {
            public string file; // Full Path.
            public string url; // URL to the file.
            public string hash; // MD5 checksum hashing.
            public long fileSize; // Size of the file. In KB. 1024KB = 1 MB...
        };

        public struct pszDownloadItem
        {
            public int iGameID; // ID of the game we're downloading to.
            public string szTable; // File table : url to the file list to queue.
            public bool bAddon; // Is it an addon?
            public string packageTitle; // Name of the download. Like: BrainBread 2 Build, Addon: The Phoenix... Etc...
            public string unRarPath; // If we're downloading a package or addon (.rar) file we will use this path to unrar the files.
            public string rarURL; // URL to an addon, package, etc... (skips the _pszFileList)
        };

        public static List<pszDownloadItem> GetDownloadQueue() { return _pszDownloadQueue; }
        public static List<pszFileInfo> GetFileList() { return _pszFileList; }

        private static List<pszDownloadItem> _pszDownloadQueue; // The queue.
        private static List<pszFileInfo> _pszFileList; // The list of files to download.
        private static List<pszDownloadItem> _tempList; // To allow us to properly shift / reorder the list.
        private static BackgroundWorker _downloadThread; // The thread we use to do the DL process.
        private static WebClient _downloadClient; // The main download client.

        // Misc
        private static bool m_bWantsToCancel; // A cancel request...
        private static double m_lDownloadSizeMax; // Max Size of file list.
        private static double m_lDownloadSizeCurrent; // Keeps track of how much we've downloaded. (adds per file = eventually this is the same as the max size of the download. All the files, .rar...)
        private static double m_lDownloadSizeContstant; // How much has been downloaded per file (size of the current file)
        private static int m_iCurrentFile; // The active file being downloaded. (index of the file list)
        private static int m_iFileIndex; // Stores the index of the file check / sync func.
        private static bool m_bFinishedFileList; // Synced files / found files and ready to download?
        private static bool m_bNoNeedForUpdate; // We don't need to udpate = no files added to file list.

        /// <summary>
        /// Setup our class:
        /// </summary>
        public static void Initialize()
        {
            m_iCurrentFile = 0;
            m_iFileIndex = 0;
            m_bNoNeedForUpdate = false;
            m_bWantsToCancel = false;
            m_bFinishedFileList = false;
            _pszFileList = new List<pszFileInfo>();
            _pszDownloadQueue = new List<pszDownloadItem>();
            _tempList = new List<pszDownloadItem>();

            _downloadThread = new BackgroundWorker();
            _downloadThread.WorkerSupportsCancellation = true;
            _downloadThread.WorkerReportsProgress = true;
            _downloadThread.DoWork += new DoWorkEventHandler(GenerateFileList);
            _downloadThread.ProgressChanged += new ProgressChangedEventHandler(GenerateFileListProgress);
            _downloadThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GenerateFileListFinished);
        }

        /// <summary>
        /// We input necessary vars to add a new queue to our download list!
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="searchTable"></param>
        /// <param name="title"></param>
        /// <param name="size"></param>
        /// <param name="bIsAddon"></param>
        /// <param name="unRarPath"></param>
        /// <param name="rarURL"></param>
        public static void AddDownloadToQueue(int gameID, string searchTable, string title, bool bIsAddon = false, string unRarPath = null, string rarURL = null)
        {
            for (int i = 0; i < _pszDownloadQueue.Count; i++)
            {
                if (_pszDownloadQueue[i].packageTitle == title)
                    return;

                if (string.IsNullOrEmpty(unRarPath))
                {
                    if (_pszDownloadQueue[i].szTable == searchTable)
                        return;
                }
            }

            // Create Base Dir(s):
            Directory.CreateDirectory(Globals.GetGamePath(gameID));

            bool bCanStart = (_pszDownloadQueue.Count <= 0);

            pszDownloadItem dlItem;
            dlItem.iGameID = gameID;
            dlItem.packageTitle = title;
            dlItem.szTable = searchTable;
            dlItem.unRarPath = unRarPath;
            dlItem.bAddon = bIsAddon;
            dlItem.rarURL = rarURL;
            _pszDownloadQueue.Add(dlItem);

            if (bCanStart)
            {
                if (string.IsNullOrEmpty(unRarPath))
                    _downloadThread.RunWorkerAsync();
                else
                    DownloadFile();
            }
        }

        /// <summary>
        /// Reorder our queue.
        /// </summary>
        public static void ReorderQueue(bool bCancelled = false)
        {
            _pszFileList.Clear();
            m_iFileIndex = 0;

            if (!bCancelled)
            {
                m_iCurrentFile = 0;

                if (!_pszDownloadQueue[0].bAddon)
                    Globals.GetBaseForm().GetGameForm().SetVersionText(Globals.GetClientGameVersion(_pszDownloadQueue[0].iGameID), _pszDownloadQueue[0].iGameID);

                Globals.ShowWarning(string.Format("{0} has finished downloading!", _pszDownloadQueue[0].packageTitle), 1);
                Globals.GetBaseForm().GetGameForm().ResetDownloadBar(_pszDownloadQueue[0].iGameID);
            }

            if (_pszDownloadQueue.Count <= 1)
            {
                Globals.SetDownloadingBaseGame(false);
                _pszDownloadQueue.Clear();
                return;
            }

            for (int i = 1; i < _pszDownloadQueue.Count; i++)
                _tempList.Add(_pszDownloadQueue[i]);

            _pszDownloadQueue.Clear();

            for (int i = 0; i < _tempList.Count; i++)
                _pszDownloadQueue.Add(_tempList[i]);

            _tempList.Clear();

            if (string.IsNullOrEmpty(_pszDownloadQueue[0].unRarPath))
                _downloadThread.RunWorkerAsync();
            else
                DownloadFile();
        }

        /// <summary>
        /// Continues the download (if paused)
        /// </summary>
        public static void Continue()
        {
            if (m_bFinishedFileList)
                DownloadFile();
            else
            {
                if (!_downloadThread.IsBusy)
                    _downloadThread.RunWorkerAsync();
            }
        }

        public static void Stop(bool cancel)
        {
            m_bWantsToCancel = cancel;

            if (m_bFinishedFileList)
            {
                if (_downloadClient != null)
                    _downloadClient.CancelAsync();
                else
                    CancelDownload(m_bWantsToCancel, m_bWantsToCancel);
            }
            else
            {
                if (_downloadThread.IsBusy)
                    _downloadThread.CancelAsync();
                else
                    CancelDownload(m_bWantsToCancel, m_bWantsToCancel);
            }
        }

        private static void AddFileToList(pszFileInfo item)
        {
            _pszFileList.Add(item);
        }

        private static void GenerateFileList(object sender, DoWorkEventArgs e)
        {
            m_bFinishedFileList = false;
            m_iCurrentFile = 0;
            m_lDownloadSizeMax = 0;
            m_lDownloadSizeCurrent = 0;
            m_lDownloadSizeContstant = 0;

            List<pszFileInfo> tempList = new List<pszFileInfo>();

            if (DataHandler.LoadFileData(tempList, _pszDownloadQueue[0].iGameID))
            {
                // Compare the files!
                for (int i = m_iFileIndex; i < tempList.Count; i++)
                {
                    string filePath = string.Format("{0}{1}", Globals.GetGamePath(_pszDownloadQueue[0].iGameID), tempList[i].file);

                    // Do Checksum on local file and compare checksum in list:(compare files)
                    if (File.Exists(filePath))
                    {
                        if (tempList[i].fileSize < 10485761)
                        {
                            using (var md5 = MD5.Create())
                            {
                                using (var stream = File.OpenRead(filePath))
                                {
                                    string hashedFile = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                    if (!hashedFile.Equals(tempList[i].hash, StringComparison.CurrentCulture))
                                        AddFileToList(tempList[i]);
                                }
                            }
                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(filePath);
                            long fileSize = fileInfo.Length;
                            if (fileSize != tempList[i].fileSize)
                                AddFileToList(tempList[i]);
                        }
                    }
                    else
                        AddFileToList(tempList[i]);

                    // Report how far we've gotten:
                    _downloadThread.ReportProgress((((i + 1) * 100) / tempList.Count));

                    // Allow Cancellation:
                    if (_downloadThread.CancellationPending)
                    {
                        m_iFileIndex = i;
                        e.Cancel = true;
                        break;
                    }
                }
            }
            else
                e.Cancel = true; // If we can't get the file list then terminate the download.

            tempList.Clear();
            tempList = null;

            if (_pszFileList.Count <= 0)
            {
                m_bNoNeedForUpdate = true;
                return;
            }

            for (int i = 0; i < _pszFileList.Count; i++)
                m_lDownloadSizeMax += (double)(_pszFileList[i].fileSize / 1024); // The filesize to download in Kilo Bytes.
        }

        // 0 - 100% Progress setting up the file list.
        private static void GenerateFileListProgress(object sender, ProgressChangedEventArgs e)
        {
            Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress("", string.Format("Preparing the download: {0}%", e.ProgressPercentage.ToString()), e.ProgressPercentage, _pszDownloadQueue[0].iGameID);
        }

        // When we're done figuring out what files we want to download we promt to download, if we cancelled then reset the stuff and reorder the queue.
        private static void GenerateFileListFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (m_bWantsToCancel)
                {
                    CancelDownload();
                    return;
                }

                Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress("", "The file check has been paused!", 0, _pszDownloadQueue[0].iGameID);
                return;
            }

            if (m_bNoNeedForUpdate)
            {
                m_bNoNeedForUpdate = false;
                CancelDownload(false);
                Globals.ShowWarning("Your game content is clean!", 1);
                return;
            }

            DownloadFile();
        }

        private static void DownloadFile()
        {
            m_bFinishedFileList = true;
            m_lDownloadSizeContstant = 0;

            _downloadClient = new WebClient();
            _downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
            _downloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);

            string rarPath = string.Format("{0}\\temp\\{1}_{2}.rar", Globals.GetAppPath(), (_pszDownloadQueue[0].bAddon ? "addon" : "package"), Globals.GetGameItemByID(_pszDownloadQueue[0].iGameID).root);

            if (!string.IsNullOrEmpty(_pszDownloadQueue[0].unRarPath))
                _downloadClient.DownloadFileAsync(new Uri(_pszDownloadQueue[0].rarURL), rarPath);
            else
            {
                string defPath = string.Format("{0}{1}", Globals.GetGamePath(_pszDownloadQueue[0].iGameID), _pszFileList[m_iCurrentFile].file);
                Directory.CreateDirectory(Path.GetDirectoryName(defPath)); // Create all dirs & sub-dirs...        

                if (defPath.Contains("/")) // Fixup faulty escape character(s)...@ from the DB files.
                    defPath = defPath.Replace("/", @"\");

                _downloadClient.DownloadFileAsync(new Uri(_pszFileList[m_iCurrentFile].url), defPath);
            }
        }

        private static void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_pszDownloadQueue[0].unRarPath))
            {
                double MB_Received = (e.BytesReceived / 1024);
                m_lDownloadSizeCurrent += (MB_Received - m_lDownloadSizeContstant);
                m_lDownloadSizeContstant = MB_Received;
                double percent = (m_lDownloadSizeCurrent / m_lDownloadSizeMax) * 100;

                string filePath = GetFileList()[m_iCurrentFile].file;
                if (filePath.Contains("/"))
                    filePath = filePath.Replace("/", @"\");

                string downloadProgress = string.Format("Game Content {0}KB of {1}KB", m_lDownloadSizeCurrent, m_lDownloadSizeMax);
                Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress(string.Format("{0}\\{1}", Globals.GetGameItemByID(_pszDownloadQueue[0].iGameID).root, filePath), downloadProgress, (int)Math.Truncate(percent), _pszDownloadQueue[0].iGameID, true, false, (e.BytesReceived / 1024));
            }
            else
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;

                string downloadProgress = string.Format("{0}{1} {2}KB of {3}KB", (_pszDownloadQueue[0].bAddon ? "Addon " : ""), _pszDownloadQueue[0].packageTitle, (e.BytesReceived / 1024), (e.TotalBytesToReceive / 1024));
                Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress("", downloadProgress, (int)Math.Truncate(percentage), _pszDownloadQueue[0].iGameID, true, false, (e.BytesReceived / 1024));
            }
        }

        private static void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_downloadClient != null)
            {
                _downloadClient.Dispose();
                _downloadClient = null;
            }

            if (e.Cancelled)
            {
                if (string.IsNullOrEmpty(_pszDownloadQueue[0].unRarPath))
                    m_lDownloadSizeCurrent -= m_lDownloadSizeContstant; // This removes the x MB downloaded from our var which stores the total size downloaded...               

                if (!m_bWantsToCancel)
                    Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress("", "The download has been paused!", 0, _pszDownloadQueue[0].iGameID);
                else
                    CancelDownload();

                return;
            }

            if (string.IsNullOrEmpty(_pszDownloadQueue[0].unRarPath))
            {
                m_iCurrentFile++; // Proceed to the next file.

                // Finished ?
                if (_pszFileList.Count == (m_iCurrentFile))
                    ReorderQueue();
                else
                    DownloadFile();
            }
            else
            {
                Globals.ShowWarning(string.Format("Extracting {0}!", _pszDownloadQueue[0].bAddon ? "Addon" : "Package"), 1);
                Globals.unRarFile(string.Format("{0}\\temp\\{1}_{2}.rar", Globals.GetAppPath(), (_pszDownloadQueue[0].bAddon ? "addon" : "package"), Globals.GetGameItemByID(_pszDownloadQueue[0].iGameID).root), _pszDownloadQueue[0].unRarPath);
                ReorderQueue();
            }
        }

        private static void CancelDownload(bool bWarning = true, bool bShouldCancel = true)
        {
            if (!bShouldCancel)
            {
                Globals.GetBaseForm().GetGameForm().UpdateDownloadProgress("", string.Format("The {0} has been paused!", m_bFinishedFileList ? "download" : "file check"), 0, _pszDownloadQueue[0].iGameID);
                return;
            }

            m_iFileIndex = 0;
            m_iCurrentFile = 0;
            m_bWantsToCancel = false;

            Globals.GetBaseForm().GetGameForm().ResetDownloadBar(_pszDownloadQueue[0].iGameID);

            if (bWarning)
                Globals.ShowWarning(string.Format("{0} has been canceled!", _pszDownloadQueue[0].packageTitle), 1);

            ReorderQueue(true);
        }
    }
}
