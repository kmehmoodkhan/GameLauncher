namespace GameLauncher
{
    partial class Launcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.buttonMin = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.linkLabelNews = new System.Windows.Forms.LinkLabel();
            this.linkHome = new System.Windows.Forms.LinkLabel();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelContainer.BackgroundImage = global::GameLauncher.Properties.Resources.background;
            this.panelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelContainer.Controls.Add(this.buttonMin);
            this.panelContainer.Controls.Add(this.buttonClose);
            this.panelContainer.Controls.Add(this.linkLabelNews);
            this.panelContainer.Controls.Add(this.linkHome);
            this.panelContainer.Controls.Add(this.buttonPlay);
            this.panelContainer.Controls.Add(this.progressBarDownload);
            this.panelContainer.Controls.Add(this.panel1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1214, 719);
            this.panelContainer.TabIndex = 0;
            // 
            // buttonMin
            // 
            this.buttonMin.Location = new System.Drawing.Point(1139, 13);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(27, 23);
            this.buttonMin.TabIndex = 6;
            this.buttonMin.Text = "-";
            this.buttonMin.UseVisualStyleBackColor = true;
            this.buttonMin.Click += new System.EventHandler(this.buttonMin_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(1175, 13);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(27, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // linkLabelNews
            // 
            this.linkLabelNews.ActiveLinkColor = System.Drawing.Color.White;
            this.linkLabelNews.AutoSize = true;
            this.linkLabelNews.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelNews.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelNews.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelNews.LinkColor = System.Drawing.Color.White;
            this.linkLabelNews.Location = new System.Drawing.Point(214, 73);
            this.linkLabelNews.Name = "linkLabelNews";
            this.linkLabelNews.Size = new System.Drawing.Size(85, 29);
            this.linkLabelNews.TabIndex = 4;
            this.linkLabelNews.TabStop = true;
            this.linkLabelNews.Text = "NEWS";
            this.linkLabelNews.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNews_LinkClicked);
            // 
            // linkHome
            // 
            this.linkHome.ActiveLinkColor = System.Drawing.Color.White;
            this.linkHome.AutoSize = true;
            this.linkHome.BackColor = System.Drawing.Color.Transparent;
            this.linkHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkHome.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkHome.LinkColor = System.Drawing.Color.White;
            this.linkHome.Location = new System.Drawing.Point(76, 73);
            this.linkHome.Name = "linkHome";
            this.linkHome.Size = new System.Drawing.Size(85, 29);
            this.linkHome.TabIndex = 3;
            this.linkHome.TabStop = true;
            this.linkHome.Text = "HOME";
            this.linkHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHome_LinkClicked);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlay.BackgroundImage = global::GameLauncher.Properties.Resources.button_on;
            this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Location = new System.Drawing.Point(983, 620);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(196, 68);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(49, 620);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(912, 23);
            this.progressBarDownload.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::GameLauncher.Properties.Resources.Logo;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(49, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 321);
            this.panel1.TabIndex = 0;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1214, 719);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Launcher";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.LinkLabel linkHome;
        private System.Windows.Forms.LinkLabel linkLabelNews;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMin;
    }
}

