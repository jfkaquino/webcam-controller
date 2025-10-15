namespace WebcamController.Forms
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            itemOpenFolder = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            itemExit = new ToolStripMenuItem();
            exibirToolStripMenuItem = new ToolStripMenuItem();
            itemAlwaysVisible = new ToolStripMenuItem();
            preferênciasToolStripMenuItem = new ToolStripMenuItem();
            itemStartWithSystem = new ToolStripMenuItem();
            itemRestoreBeforeExit = new ToolStripMenuItem();
            ajudaToolStripMenuItem = new ToolStripMenuItem();
            itemAbout = new ToolStripMenuItem();
            trayIcon = new NotifyIcon(components);
            contextTray = new ContextMenuStrip(components);
            toolStripSeparator2 = new ToolStripSeparator();
            mostrarToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem1 = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            presetsView = new WebcamController.Views.PresetsView();
            cameraView = new WebcamController.Views.CameraView();
            menuStrip1.SuspendLayout();
            contextTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, exibirToolStripMenuItem, preferênciasToolStripMenuItem, ajudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(584, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemOpenFolder, toolStripSeparator1, itemExit });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(61, 20);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // itemOpenFolder
            // 
            itemOpenFolder.Name = "itemOpenFolder";
            itemOpenFolder.Size = new Size(200, 22);
            itemOpenFolder.Text = "Abrir local de instalação";
            itemOpenFolder.Click += itemInstallFolder_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(197, 6);
            // 
            // itemExit
            // 
            itemExit.Name = "itemExit";
            itemExit.Size = new Size(200, 22);
            itemExit.Text = "Sair";
            itemExit.Click += itemExit_Click;
            // 
            // exibirToolStripMenuItem
            // 
            exibirToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemAlwaysVisible });
            exibirToolStripMenuItem.Name = "exibirToolStripMenuItem";
            exibirToolStripMenuItem.Size = new Size(47, 20);
            exibirToolStripMenuItem.Text = "Exibir";
            // 
            // itemAlwaysVisible
            // 
            itemAlwaysVisible.CheckOnClick = true;
            itemAlwaysVisible.Name = "itemAlwaysVisible";
            itemAlwaysVisible.Size = new Size(149, 22);
            itemAlwaysVisible.Text = "Sempre visível";
            itemAlwaysVisible.CheckedChanged += itemAlwaysVisible_CheckedChanged;
            // 
            // preferênciasToolStripMenuItem
            // 
            preferênciasToolStripMenuItem.CheckOnClick = true;
            preferênciasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemStartWithSystem, itemRestoreBeforeExit });
            preferênciasToolStripMenuItem.Name = "preferênciasToolStripMenuItem";
            preferênciasToolStripMenuItem.Size = new Size(83, 20);
            preferênciasToolStripMenuItem.Text = "Preferências";
            // 
            // itemStartWithSystem
            // 
            itemStartWithSystem.CheckOnClick = true;
            itemStartWithSystem.Name = "itemStartWithSystem";
            itemStartWithSystem.Size = new Size(200, 22);
            itemStartWithSystem.Text = "Iniciar com o sistema";
            itemStartWithSystem.CheckedChanged += itemStartWithSystem_CheckedChanged;
            // 
            // itemRestoreBeforeExit
            // 
            itemRestoreBeforeExit.CheckOnClick = true;
            itemRestoreBeforeExit.Name = "itemRestoreBeforeExit";
            itemRestoreBeforeExit.Size = new Size(200, 22);
            itemRestoreBeforeExit.Text = "Restaurar padrão ao sair";
            itemRestoreBeforeExit.CheckedChanged += itemRestoreBeforeExit_CheckedChanged;
            // 
            // ajudaToolStripMenuItem
            // 
            ajudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemAbout });
            ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            ajudaToolStripMenuItem.Size = new Size(50, 20);
            ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // itemAbout
            // 
            itemAbout.Name = "itemAbout";
            itemAbout.Size = new Size(104, 22);
            itemAbout.Text = "Sobre";
            itemAbout.Click += itemAbout_Click;
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = contextTray;
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "Webcam Manager";
            trayIcon.Visible = true;
            trayIcon.BalloonTipClicked += trayIcon_BalloonTipClicked;
            trayIcon.BalloonTipClosed += trayIcon_BalloonTipClosed;
            trayIcon.DoubleClick += trayIcon_DoubleClick;
            // 
            // contextTray
            // 
            contextTray.Items.AddRange(new ToolStripItem[] { toolStripSeparator2, mostrarToolStripMenuItem, sairToolStripMenuItem1 });
            contextTray.Name = "contextTray";
            contextTray.Size = new Size(119, 54);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(115, 6);
            // 
            // mostrarToolStripMenuItem
            // 
            mostrarToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            mostrarToolStripMenuItem.Name = "mostrarToolStripMenuItem";
            mostrarToolStripMenuItem.Size = new Size(118, 22);
            mostrarToolStripMenuItem.Text = "Mostrar";
            mostrarToolStripMenuItem.Click += mostrarToolStripMenuItem_Click;
            // 
            // sairToolStripMenuItem1
            // 
            sairToolStripMenuItem1.Name = "sairToolStripMenuItem1";
            sairToolStripMenuItem1.Size = new Size(118, 22);
            sairToolStripMenuItem1.Text = "Sair";
            sairToolStripMenuItem1.Click += sairToolStripMenuItem1_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(presetsView);
            splitContainer1.Panel1.Padding = new Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(cameraView);
            splitContainer1.Panel2.Padding = new Padding(3);
            splitContainer1.Size = new Size(584, 327);
            splitContainer1.SplitterDistance = 292;
            splitContainer1.TabIndex = 1;
            // 
            // presetsView
            // 
            presetsView.CameraController = null;
            presetsView.Dock = DockStyle.Fill;
            presetsView.HotkeyService = null;
            presetsView.Location = new Point(3, 3);
            presetsView.Name = "presetsView";
            presetsView.PresetController = null;
            presetsView.Size = new Size(286, 321);
            presetsView.TabIndex = 0;
            // 
            // cameraView
            // 
            cameraView.CameraController = null;
            cameraView.Dock = DockStyle.Fill;
            cameraView.Location = new Point(3, 3);
            cameraView.Name = "cameraView";
            cameraView.Size = new Size(282, 321);
            cameraView.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 351);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Webcam Manager";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextTray.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem itemOpenFolder;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem exibirToolStripMenuItem;
        private ToolStripMenuItem itemAlwaysVisible;
        private ToolStripMenuItem preferênciasToolStripMenuItem;
        private ToolStripMenuItem itemStartWithSystem;
        private ToolStripMenuItem ajudaToolStripMenuItem;
        private ToolStripMenuItem itemRestoreBeforeExit;
        private ToolStripMenuItem itemAbout;
        private NotifyIcon trayIcon;
        private ContextMenuStrip contextTray;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mostrarToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem1;
        private SplitContainer splitContainer1;
        private Views.PresetsView presetsView;
        private Views.CameraView cameraView;
    }
}