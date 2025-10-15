using Microsoft.EntityFrameworkCore;

namespace WebcamController.Models
{
    public class Hotkey
    {
        public int Id { get; set; }
        public string DisplayString { get; set; }
        public Keys KeyData { get; set; }
        public bool NoRepeat { get; set; }
    }
}

