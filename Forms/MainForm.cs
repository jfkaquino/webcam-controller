using WebcamController.Controllers;
using WebcamController.Properties;
using WebcamController.Services;
using WebcamController.Views;

namespace WebcamController.Forms
{
    public interface IMainForm
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

        public MainForm(
        CameraService cameraService,
        HotkeyService hotkeyService,
        WindowsService windowsService,
        PresetController presetController,
        CameraController cameraController)
        {
            _cameraService = cameraService;
            _hotkeyService = hotkeyService;
            _windowsService = windowsService;
            _presetController = presetController;
            _cameraController = cameraController;

            InitializeComponent();
        }

        #region Ciclo de vida

        private void MainForm_Load(object sender, EventArgs e)
        {
            presetsView.Initialize(_presetController, _cameraController, _hotkeyService);
            cameraView.Initialize(_cameraController);

            HandleStartupArguments();
            LoadSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                MinimizeToTray();
                e.Cancel = true;
                ShowNotification("O programa continuará em segundo plano na bandeja de notificações.");
                return;
            }

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
        private void itemExit_Click(object sender, EventArgs e) => Application.Exit();
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

        private void trayIcon_DoubleClick(object sender, EventArgs e) => RestoreFromTray();
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e) => RestoreFromTray();

        private bool _isShowing = false;

        private void ShowNotification(string message, string title = "")
        {
            if (_isShowing) return;
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = message;
            _isShowing = true;
            trayIcon.ShowBalloonTip(3000);
        }

        private void trayIcon_BalloonTipClosed(object sender, EventArgs e) => _isShowing = false;
        private void trayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            _isShowing = false;
            RestoreFromTray();
        }

        #endregion
    }
}
