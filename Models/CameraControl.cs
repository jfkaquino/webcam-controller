using DirectShowLib;
using Microsoft.EntityFrameworkCore;

namespace WebcamController.Models
{
    [Owned]
    public class CameraControl
    {
        public CameraControlProperty Property { get; set; }
        public int Value { get; set; }
        public CameraControlFlags Flags { get; set; }
    }
}
