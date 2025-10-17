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

        #region Life cycle

        private void CameraView_Load(object sender, EventArgs e)
        {
            LoadDevices();

            ConnectCamera();
            ConfigureCameraControls();
            UpdateCameraControls();
            CameraController.DeviceChanged += OnDeviceChanged;
            CameraController.PropertyChanged += OnPropertyChanged;
        }

        private void OnDeviceChanged(object? sender, DeviceChangedEventArgs e)
        {
            _isUpdating = true;
            switch (e.Status)
            {
                case DeviceStatus.Connected: cmbDevices.SelectedItem = e.Device; break;
            }

            ConfigureCameraControls();
            UpdateCameraControls();
            _isUpdating = false;
        }

        private void LoadDevices()
        {
            CameraController.GetAvailableDevices();
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
            grpPan.Enabled = isConnected && CameraController.SupportsProperty(CameraControlProperty.Pan);
            var (minPan, maxPan, stepPan, defPan, _) = CameraController.GetPropertyRange(CameraControlProperty.Pan);

            trkPan.Minimum = minPan;
            trkPan.Maximum = maxPan;
            trkPan.SmallChange = stepPan;

            numPan.Minimum = minPan;
            numPan.Maximum = maxPan;
            numPan.Increment = stepPan;

            // Tilt
            grpTilt.Enabled = isConnected && CameraController.SupportsProperty(CameraControlProperty.Tilt);
            var (minTilt, maxTilt, stepTilt, defTilt, _) = CameraController.GetPropertyRange(CameraControlProperty.Tilt);

            trkTilt.Minimum = minTilt;
            trkTilt.Maximum = maxTilt;
            trkTilt.SmallChange = stepTilt;

            numTilt.Minimum = minTilt;
            numTilt.Maximum = maxTilt;
            numTilt.Increment = stepTilt;

            // Zoom
            grpZoom.Enabled = isConnected && CameraController.SupportsProperty(CameraControlProperty.Zoom);
            var (minZoom, maxZoom, stepZoom, defZoom, _) = CameraController.GetPropertyRange(CameraControlProperty.Zoom);

            trkZoom.Minimum = minZoom;
            trkZoom.Maximum = maxZoom;
            trkZoom.SmallChange = stepZoom;

            numZoom.Minimum = minZoom;
            numZoom.Maximum = maxZoom;
            numZoom.Increment = stepZoom;

        }

        private void UpdateCameraControls()
        {
            (int currentPan, _) = CameraController.GetProperty(CameraControlProperty.Pan);
            trkPan.Value = currentPan;
            numPan.Value = currentPan;

            (int currentTilt, _) = CameraController.GetProperty(CameraControlProperty.Pan);
            trkTilt.Value = currentTilt;
            numTilt.Value = currentTilt;

            (int currentZoom, _) = CameraController.GetProperty(CameraControlProperty.Zoom);
            trkZoom.Value = currentZoom;
            numZoom.Value = currentZoom;
        }

        #endregion

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => UpdateCameraControls();

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e) => ConnectCamera();

        private void linkAdvancedProperties_Click(object sender, EventArgs e) => CameraController.ShowPropertiesPage(Handle);
    }
}
