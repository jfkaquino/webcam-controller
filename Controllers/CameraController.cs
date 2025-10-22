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

        public event EventHandler<DeviceChangedEventArgs> DeviceChanged;
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        public List<Device> Devices { get; set; } = new();

        public Device ConnectedDevice => _cameraService.ConnectedDevice;
        public List<CameraControl> CameraControls { get; set; } = new();

        public CameraController(CameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void GetAvailableDevices()
        {
            foreach (var camera in _cameraService.GetAvailableCameras())
            {
                Device device = new Device
                {
                    DevicePath = camera.DevicePath,
                    FriendlyName = camera.Name
                };
                Devices.Add(device);
                OnDeviceChanged(device, DeviceStatus.Available);
            }
        }

        public bool IsDeviceAvailable(Device device)
        {
            if(!Devices.Any()) GetAvailableDevices();
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
            OnDeviceChanged(device, DeviceStatus.Disconnected);
        }

        public void ApplyPreset(Preset preset)
        {
            if(!IsDeviceAvailable(preset.Device)) throw new InvalidOperationException("Device not available.");

            foreach (var property in preset.CameraControls)
            {
                SetProperty(property.Property, property.Value, property.Flags);
            }
        }

        public void SetProperty(CameraControlProperty property, int value, CameraControlFlags flags)
        {
            _cameraService.SetControlProperty(property, value, flags);
            OnPropertyChanged(property, value, flags);
        }

        public (int, CameraControlFlags) GetProperty(CameraControlProperty property)
        {
            return _cameraService.GetControlProperty(property);
        }

        public void GetAllProperties()
        {
            CameraControls.Clear();
            foreach (CameraControlProperty property in Enum.GetValues(typeof(CameraControlProperty)))
            {
                if (SupportsProperty(property))
                {
                    var (value, flags) = GetProperty(property);
                    CameraControls.Add(new CameraControl
                    {
                        Property = property,
                        Value = value,
                        Flags = flags
                    });
                }
            }
        }

        public bool SupportsProperty(CameraControlProperty property)
        {
            return _cameraService.SupportsControlProperty(property);
        }

        public (int, int, int, int, CameraControlFlags) GetPropertyRange(CameraControlProperty property)
        {
            return _cameraService.GetControlPropertyRange(property);
        }

        public bool SupportsPropertyPages() => _cameraService.SupportsPropertyPages();

        public void ShowPropertiesPage(nint handle) => _cameraService.ShowPropertyPage(handle);

        private void OnDeviceChanged(Device device, DeviceStatus status)
        {
            DeviceChanged?.Invoke(this, new DeviceChangedEventArgs(device, status));
        }

        private void OnPropertyChanged(CameraControlProperty property, int value, CameraControlFlags flags)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property, value, flags));
        }
    }
}

public enum DeviceStatus {
    Connected,
    Disconnected,
    Available,
    NotSupported
}

public class DeviceChangedEventArgs(Device device, DeviceStatus status) : EventArgs
{
    public Device Device { get; } = device;
    public DeviceStatus Status { get; } = status;
}

public class PropertyChangedEventArgs(CameraControlProperty property, int value, CameraControlFlags flags) : EventArgs
{
    public CameraControlProperty Property { get; } = property;
    public int Value { get; } = value;
    public CameraControlFlags Flags { get; } = flags;
}

