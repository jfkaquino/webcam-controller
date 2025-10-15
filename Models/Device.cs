using System.ComponentModel.DataAnnotations;

namespace WebcamController.Models
{
    public class Device
    {
        public int Id { get; set; }
        public String DevicePath { get; set; }
        public String FriendlyName { get; set; }
        public List<Preset> Presets { get; set; } = new();
    }
}
