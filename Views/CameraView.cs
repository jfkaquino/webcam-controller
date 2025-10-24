using DirectShowLib;
using WebcamController.Controllers;
using WebcamController.Models;
using WebcamController.Properties;

namespace WebcamController.Views
{
    public partial class CameraView : UserControl
    {
        public CameraController CameraController { get; set; }

        public bool _isUpdating = false;

        public CameraView()
        {
            InitializeComponent();
        }

        public void Initialize(CameraController cameraController)
        {
            CameraController = cameraController;

            LoadDevices();

            ConnectCamera();
            ConfigureCameraControls();
            UpdateCameraControls();
            CameraController.DeviceConnected += OnDeviceChanged;
            CameraController.PropertyChanged += OnPropertyChanged;
        }

        #region Life cycle

        private void OnDeviceChanged(object? sender, DeviceConnectedEventArgs e)
        {
            _isUpdating = true;
            if (e.Status == DeviceStatus.Connected) cmbDevices.SelectedItem = e.Device;

            ConfigureCameraControls();
            UpdateCameraControls();
            _isUpdating = false;
        }

        private void LoadDevices()
        {
            cmbDevices.DataSource = CameraController.Devices;
            cmbDevices.DisplayMember = "FriendlyName";
            cmbDevices.ValueMember = "DevicePath";

            if (!String.IsNullOrEmpty(Settings.Default.DefaultDevicePath))
            {
                cmbDevices.SelectedValue = Settings.Default.DefaultDevicePath;
            }
            else
            {
                cmbDevices.SelectedIndex = 0;
            }
        }

        private void ConnectCamera()
        {
            CameraController.Connect((Device)cmbDevices.SelectedItem);
        }

        private void RestoreDefaults()
        {

        }

        #endregion

        #region Configure UI

        private void ConfigureCameraControls()
        {
            bool isConnected = CameraController.ConnectedDevice != null;
            btnRestoreDefault.Enabled = isConnected;
            linkAdvancedProperties.Enabled = CameraController.SupportsPropertyPages();

            // Pan
            var panSetting = CameraController.GetSettingRange(ControlProperty.Pan);
            grpPan.Enabled = isConnected && panSetting.IsSupported;

            trkPan.Minimum = panSetting.Min;
            trkPan.Maximum = panSetting.Max;
            trkPan.SmallChange = panSetting.Step;

            numPan.Minimum = panSetting.Min;
            numPan.Maximum = panSetting.Max;
            numPan.Increment = panSetting.Step;

            // Tilt
            var tiltSetting = CameraController.GetSettingRange(ControlProperty.Tilt);
            grpTilt.Enabled = isConnected && tiltSetting.IsSupported;

            trkTilt.Minimum = tiltSetting.Min;
            trkTilt.Maximum = tiltSetting.Max;
            trkTilt.SmallChange = tiltSetting.Step;

            numTilt.Minimum = tiltSetting.Min;
            numTilt.Maximum = tiltSetting.Max;
            numTilt.Increment = tiltSetting.Step;

            // Zoom
            var zoomSetting = CameraController.GetSettingRange(ControlProperty.Zoom);
            grpZoom.Enabled = isConnected && zoomSetting.IsSupported;

            trkZoom.Minimum = zoomSetting.Min;
            trkZoom.Maximum = zoomSetting.Max;
            trkZoom.SmallChange = zoomSetting.Step;

            numZoom.Minimum = zoomSetting.Min;
            numZoom.Maximum = zoomSetting.Max;
            numZoom.Increment = zoomSetting.Step;

        }

        private void UpdateCameraControls()
        {
            var panSetting = CameraController.GetSetting(ControlProperty.Pan);
            trkPan.Value = panSetting.Value;
            numPan.Value = panSetting.Value;

            var tiltSetting = CameraController.GetSetting(ControlProperty.Tilt);
            trkTilt.Value = tiltSetting.Value;
            numTilt.Value = tiltSetting.Value;

            var zoomSetting = CameraController.GetSetting(ControlProperty.Tilt);
            trkZoom.Value = zoomSetting.Value;
            numZoom.Value = zoomSetting.Value;
        }

        #endregion

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => UpdateCameraControls();

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e) => ConnectCamera();

        private void linkAdvancedProperties_Click(object sender, EventArgs e) => CameraController.ShowPropertiesPage(Handle);
    }
}
