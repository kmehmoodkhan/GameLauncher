using GameLauncher.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace GameLauncher
{
    public partial class Launcher : Form
    {

        const string HomeURL = "https://eu.middlearth.net";
        const string  NewsURL= "https://eu.middlearth.net/forum/forum";
        const string UpdatesURL = "https://eu.middlearth.net/middleearth/versions/";
        const string FtpHost = "ftp://login.middlearth.net";
        const string Username = "ftp";
        const string UserPassword = "ftp123";


        readonly string DownloadsPath = System.IO.Path.GetTempPath();
        const string GameExeFile = "Tibia.exe";
        private string GamePath
        {
            get
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToLower();
                path = path.Replace("\\bin\\debug", "");
                return path;
            }
        }

        private int CurrentVersion
        {
            get
            {
                string version = "";
                int versionId = 0;

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiddleEarth");
                if (key != null)
                {
                    version = key.GetValue("Version").ToString();
                }

                if (!string.IsNullOrEmpty(version))
                {
                    versionId = Convert.ToInt32(version);
                }
                return versionId;
            }
            set
            {
                RegistryKey keyExists = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiddleEarth");

                if (keyExists != null)
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiddleEarth", true))
                    {
                        if (key != null)
                            key.SetValue("Version", value);
                    }
                }
                else
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MiddleEarth"))
                    {
                        if (key != null)
                            key.SetValue("Version", value);
                    }
                }
            }
        }

        string fileDownloadPath = string.Empty;
        public string LatestUpdatePath
        {
            get
            {
                return fileDownloadPath;
            }
        }


        int latestVersion = 0;
        public int LatestVersion
        {
            get
            {
                return latestVersion;
            }
        }



        WebClient client = null;

        public Launcher()
        {
            InitializeComponent();
            this.panelContainer.BorderStyle = BorderStyle.FixedSingle;

            //buttonHome.BackColor = ColorTranslator.FromHtml("#ED3A2C");
            //buttonNews.BackColor = ColorTranslator.FromHtml("#46B1F4");
            //buttonPlay.BackColor = ColorTranslator.FromHtml("#4EEB3B");
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private int CheckForUpdates()
        {
            var availableVersion = GetLatestVersion(CurrentVersion);
            
            return availableVersion;
        }

        private void StartDownload(string fileName)
        {
            string updatesURL = FtpHost+"//" + fileName;
            fileDownloadPath = DownloadsPath;

            if (!fileDownloadPath.EndsWith("\\"))
            {
                fileDownloadPath += "\\";
            }
            fileDownloadPath += fileName;

            client = new WebClient();
            client.UseDefaultCredentials = false;
         

            NetworkCredential credential= new NetworkCredential(Username,UserPassword);
            client.Credentials = credential;

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri(updatesURL), fileDownloadPath);
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBarDownload.Maximum = (int)e.TotalBytesToReceive / 100;
            this.progressBarDownload.Value = (int)e.BytesReceived / 100;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string directoryPath = ExtractZipFile(fileDownloadPath);
            ApplyUpdate(directoryPath, GamePath);
           
            CurrentVersion = latestVersion;
            LaunchGame();
            Application.Exit();
        }

        private void linkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(HomeURL);
            Process.Start(sInfo);
        }

        private void linkLabelNews_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(NewsURL);
            Process.Start(sInfo);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            latestVersion = CheckForUpdates();

            if (latestVersion > CurrentVersion)
            {
                buttonPlay.Enabled = false;
                progressBarDownload.Visible = true;
                string fileName = latestVersion + ".zip";
                StartDownload(fileName);
            }
            else
            {
                progressBarDownload.Visible = false;
                LaunchGame();
                Application.Exit();
            }
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(HomeURL);
            Process.Start(sInfo);
        }

        private void buttonNews_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(NewsURL);
            Process.Start(sInfo);
        }

        

        private void panelContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public int GetLatestVersion(int currentVersion)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpHost);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(Username, UserPassword);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string names = reader.ReadToEnd();

            reader.Close();
            response.Close();

            var list= names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var result = list.Where(t => t.ToLower().Contains(".zip")).Select(t=>t.Replace(".zip","")).Select(t => Convert.ToInt32(t)).ToList().OrderByDescending(t => t);

            int latestVersion = 0;

            if( result!=null && result.Count() > 0)
            {
                latestVersion = result.FirstOrDefault();
            }
            

            return latestVersion;


        }

        public string DownloadLatestVersion(int version)
        {
            string filePath = DownloadsPath + version + ".zip";
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(Username, UserPassword);
                byte[] fileData = request.DownloadData(FtpHost+version.ToString()+".zip");

                using (FileStream file = System.IO.File.Create(filePath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }
            return filePath;
        }

        public string ExtractZipFile(String sourceFile)
        {
            string destinationDirectory = sourceFile.Replace(Path.GetFileName(sourceFile), "");
            if(!destinationDirectory.EndsWith("\\")){
                destinationDirectory += "\\";
            }

            destinationDirectory += Guid.NewGuid();

            if(!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            Shell32.Shell objShell = new Shell32.Shell();
            Shell32.Folder destinationFolder = objShell.NameSpace(destinationDirectory);
            Shell32.Folder sourceZipFile = objShell.NameSpace(sourceFile);

            foreach (var file in sourceZipFile.Items())
            {
                destinationFolder.CopyHere(file, 4 | 16);
            }

            var files = Directory.GetFiles(destinationDirectory, "*.*", SearchOption.AllDirectories);

            if(files.Count() > 0)
            {
                return new FileInfo(files[0]).Directory.FullName;
            }
            
            return destinationDirectory;
        }

        public void ApplyUpdate(string sourceDirectory,string gameDirectory)
        {
            Copy(sourceDirectory, gameDirectory);
        }

        public void LaunchGame()
        {
            string gamePath = GamePath;
            if(!gamePath.EndsWith("\\"))
            {
                gamePath += "\\";
            }
            gamePath+= GameExeFile;
            ProcessStartInfo sInfo = new ProcessStartInfo(gamePath);
            Process.Start(sInfo);
        }


        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (FileInfo fi in source.GetFiles())
            {
                string targetFile = Path.Combine(target.FullName, fi.Name);
                System.IO.File.Copy(fi.FullName, targetFile, true);
            }
        }

        private void buttonHome_MouseHover(object sender, EventArgs e)
        {
            buttonHome.BackgroundImage = Resources.Home_Hover;
        }

        private void buttonNews_MouseHover(object sender, EventArgs e)
        {
            buttonNews.BackgroundImage = Resources.News_Hover;
        }

        private void buttonPlay_MouseHover(object sender, EventArgs e)
        {
            buttonPlay.BackgroundImage = Resources.Play_Hover;
        }

        private void buttonHome_MouseEnter(object sender, EventArgs e)
        {
            this.buttonHome.BackgroundImage = Resources.Home_Hover;
        }

        private void buttonHome_MouseLeave(object sender, EventArgs e)
        {
            this.buttonHome.BackgroundImage = Resources.Home;
        }

        private void buttonNews_MouseEnter(object sender, EventArgs e)
        {
            this.buttonNews.BackgroundImage = Resources.News_Hover;
        }

        private void buttonNews_MouseLeave(object sender, EventArgs e)
        {
            this.buttonNews.BackgroundImage = Resources.News;
        }

        private void buttonPlay_MouseEnter(object sender, EventArgs e)
        {
            this.buttonPlay.BackgroundImage = Resources.Play_Hover;
        }

        private void buttonPlay_MouseLeave(object sender, EventArgs e)
        {
            this.buttonPlay.BackgroundImage = Resources.Play;
        }

        private void buttonMin_MouseEnter(object sender, EventArgs e)
        {
            this.buttonMin.BackgroundImage = Resources.Minimize_hover;
        }

        private void buttonMin_MouseLeave(object sender, EventArgs e)
        {
            this.buttonMin.BackgroundImage = Resources.Minimize;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            this.buttonClose.BackgroundImage = Resources.Close_hover;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            this.buttonClose.BackgroundImage = Resources.Close;
        }
    }
}
