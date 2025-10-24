using DirectShowLib;
using DirectShowLib.DMO;
using System.ComponentModel;
using WebcamController.Models;
using WebcamController.Services;

namespace WebcamController.Controllers
{
    public class CameraController
    {
        private CameraService _cameraService;

        public event EventHandler<DeviceConnectedEventArgs> DeviceConnected;
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        public Device ConnectedDevice => _cameraService.ConnectedDevice;
        public List<Device> Devices
        {
            get
            {
                return _cameraService.GetAvailableDevices();
            }
        }

        public List<CameraControl> Properties
        {
            get
            {
                return GetAllSettings();
            }
        }

        public List<SettingRange> PropertiesRanges
        {
            get
            {
                return GetAllSettingsRanges();
            }
        }

        public CameraController(CameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public bool IsDeviceAvailable(Device device)
        {
            return Devices.Any(d => d.DevicePath == device.DevicePath);
        }

        public void Connect(Device device)
        {
            if(!IsDeviceAvailable(device)) throw new InvalidOperationException("Device not available.");
            try
            {
                _cameraService.Connect(device);
                OnDeviceChanged(device, DeviceStatus.Connected);
            }
            catch (NotSupportedException)
            {
                OnDeviceChanged(device, DeviceStatus.NotSupported);
                throw;
            }
        }

        public void Disconnect()
        {
            var device = _cameraService.ConnectedDevice;
            _cameraService.Disconnect();
        }

        public void ApplyPreset(Preset preset)
        {
            if(!IsDeviceAvailable(preset.Device)) throw new InvalidOperationException("Device not available.");

            foreach (var property in preset.CameraSettings)
            {
                SetSetting(property);
            }
        }

        public void SetSetting(CameraControl property)
        {
           _cameraService.SetSetting(property);
        }

        public SettingRange GetSettingRange(Enum property)
        {
            return _cameraService.GetSettingRange(property);
        }

        public CameraControl GetSetting(Enum property)
        {
            return _cameraService.GetSetting(property);
        }

        public List<SettingRange> GetAllSettingsRanges()
        {
            Array controlEnum = Enum.GetValues(typeof(ControlProperty));
            List<SettingRange> propertiesRanges = new();

            foreach (ControlProperty property in controlEnum)
            {
                propertiesRanges.Add(_cameraService.GetSettingRange(property));
            }

            return propertiesRanges;
        }

        public List<CameraControl> GetAllSettings()
        {
            Array controlEnum = Enum.GetValues(typeof(ControlProperty));
            List<CameraControl> properties = new();

            foreach (ControlProperty property in controlEnum)
            {
                Properties.Add(_cameraService.GetSetting(property));
            }

            return properties;
        }

        public bool SupportsPropertyPages() => _cameraService.SupportsPropertiesPages();

        public void ShowPropertiesPage(nint handle) => _cameraService.ShowPropertiesPage(handle);

        private void OnDeviceChanged(Device device, DeviceStatus status)
        {
            DeviceConnected?.Invoke(this, new DeviceConnectedEventArgs(device, status));
        }

        private void OnPropertyChanged(CameraControl property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

public enum DeviceStatus {
    Connected,
    NotSupported
}

public class DeviceConnectedEventArgs(Device device, DeviceStatus status) : EventArgs
{
    public Device Device { get; } = device;
    public DeviceStatus Status { get; } = status;
}

public class PropertyChangedEventArgs(CameraControl property) : EventArgs
{
    public CameraControl Property { get; } = property;
}

