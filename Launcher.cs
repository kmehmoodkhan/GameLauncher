using GameLauncher.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GameLauncher
{
    public partial class Launcher : Form
    {

        const string NewsURL = "https://eu.middlearth.net";
        const string HomeURL = "https://eu.middlearth.net/forum/forum";
        const string UpdatesURL = "https://eu.middlearth.net/middleearth/versions/";
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
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiddleEarth", true))
                {
                    key.SetValue("Version", value);
                }
            }
        }



        WebClient client = null;

        public Launcher()
        {
            InitializeComponent();
            this.panelContainer.BorderStyle = BorderStyle.FixedSingle;
        }


       
        private string CheckForUpdates()
        {

            string latestFileName = "";
            bool isUpdateAvailable = true;

            if(isUpdateAvailable)
            {
                buttonPlay.BackgroundImage = Resources.button_off;
            }
            else
            {
                buttonPlay.BackgroundImage = Resources.button_on;
            }
            return latestFileName;
        }

        private void StartDownload(string fileName)
        {
            string updatesURL = UpdatesURL + fileName;
            string downloadPath = DownloadsPath;

            if (!downloadPath.EndsWith("\\"))
            {
                downloadPath += "\\";
            }
            downloadPath += fileName;

            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri(updatesURL), downloadPath);
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBarDownload.Maximum = (int)e.TotalBytesToReceive / 100;
            this.progressBarDownload.Value = (int)e.BytesReceived / 100;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

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
            string fileName = CheckForUpdates();
            if (!string.IsNullOrEmpty(fileName))
            {
                StartDownload(fileName);
            }
        }
    }
}
