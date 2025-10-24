using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebcamController.Models
{
    public class Preset
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Hotkey? Hotkey { get; set; } = null;
        public List<CameraControl> CameraSettings { get; set; } = new();
        public required Device Device { get; set; }
        public Options Options { get; set; } = new();

        public void UpdateFrom(Preset other)
        {
            var device = new Device
            {
                DevicePath = other.Device.DevicePath,
                FriendlyName = other.Device.FriendlyName
            };
            
            Name = other.Name;
            CameraSettings = other.CameraSettings;
            Device = device;
        }
    }

    [Owned]
    public class Options
    {
        public bool ReadOnly { get; set; } = false;
    }
}
