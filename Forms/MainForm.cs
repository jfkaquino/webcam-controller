using WebcamController.Controllers;
using WebcamController.Properties;
using WebcamController.Services;

namespace WebcamController.Forms
{
    public interface IClosableComponent
    {
        public void OnMainFormClosing(object sender, FormClosingEventArgs e);
    }

    public partial class MainForm : Form
    {
        private CameraService _cameraService;
        private HotkeyService _hotkeyService;
        private WindowsService _windowsService;
        private PresetController _presetController;
        private CameraController _cameraController;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(
        CameraService cameraService,
        HotkeyService hotkeyService,
        WindowsService windowsService,
        PresetController presetController,
        CameraController cameraController) : this()
        {
            _cameraService = cameraService;
            _hotkeyService = hotkeyService;
            _windowsService = windowsService;
            _presetController = presetController;
            _cameraController = cameraController;

            presetsView.PresetController = presetController;
            presetsView.CameraController = cameraController;
            presetsView.HotkeyService = hotkeyService;

            cameraView.CameraController = cameraController;
        }

        #region Ciclo de vide

        private void MainForm_Load(object sender, EventArgs e)
        {
            HandleStartupArguments();
            LoadSettings();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
            _hotkeyService.Dispose();
            presetsView.OnMainFormClosing(sender, e);
        }

        private void HandleStartupArguments()
        {
            if (Environment.GetCommandLineArgs().Contains("/minimized"))
            {
                MinimizeToTray();
            }
        }

        #endregion

        #region Configurações

        private void LoadSettings()
        {
            itemAlwaysVisible.Checked = Settings.Default.AlwaysVisible;
            itemStartWithSystem.Checked = _windowsService.IsStartupEnabled();
            itemRestoreBeforeExit.Checked = Settings.Default.RestoreBeforeExit;
        }
        private void itemAlwaysVisible_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            TopMost = isChecked;
            Settings.Default.AlwaysVisible = isChecked;
        }

        private void itemStartWithSystem_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            _windowsService.SetStartup(isChecked);
        }

        private void itemRestoreBeforeExit_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            Settings.Default.RestoreBeforeExit = isChecked;
        }

        #endregion

        #region Eventos de UI

        private void itemInstallFolder_Click(object sender, EventArgs e) => _windowsService.OpenProgramFolder();
        private void itemExit_Click(object sender, EventArgs e) => Close();
        private void itemAbout_Click(object sender, EventArgs e) => new AboutBox().ShowDialog();

        #endregion

        #region Ícone da bandeja

        private void MinimizeToTray()
        {
            Hide();
            ShowInTaskbar = false;
        }

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Activate();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e) => RestoreFromTray();
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e) => RestoreFromTray();
        private void sairToolStripMenuItem1_Click(object sender, EventArgs e) => Close();

        private bool _isShowing = false;

        private void ShowNotification(string message, string title)
        {
            if (_isShowing) return;
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = message;
            _isShowing = true;
            trayIcon.ShowBalloonTip(3000);
        }

        private void trayIcon_BalloonTipClosed(object sender, EventArgs e) => _isShowing = false;
        private void trayIcon_BalloonTipClicked(object sender, EventArgs e) => _isShowing = false;

        #endregion

    }
}
