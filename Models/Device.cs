using System.ComponentModel.DataAnnotations;

namespace WebcamController.Models
{
    public class Device
    {
        public int Id { get; set; }
        public required String DevicePath { get; set; }
        public required String FriendlyName { get; set; }
        public List<Preset> Presets { get; set; } = new();

        public void UpdateFrom(Device other)
        {
            DevicePath = other.DevicePath;
            FriendlyName = other.FriendlyName;
        }
    }
}
