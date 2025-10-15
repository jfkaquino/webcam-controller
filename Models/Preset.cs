using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebcamController.Models
{
    public class Preset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Hotkey Hotkey { get; set; } = null;
        public List<CameraControl> CameraControls { get; set; } = new();
        public Device Device { get; set; }
    }
}
