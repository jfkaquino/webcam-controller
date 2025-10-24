using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebcamController.Models
{
    [Owned]
    public class CameraControl
    {
        public int Value { get; set; }
        public SettingFlag Flags { get; set; }

        [NotMapped]
        public virtual Enum PropertyType { get; set; }
    }

    public class ControlSetting : CameraControl
    {
        public ControlProperty Property { get; set; }
        public override Enum PropertyType => Property;
    }

    public class VideoSetting : CameraControl
    {
        public VideoProperty Property { get; set; }
        public override Enum PropertyType => Property;
    }

    public enum VideoProperty
    {
        Brightness,
        Contrast,
        Hue,
        Saturation,
        Sharpness,
        Gamma,
        ColorEnable,
        WhiteBalance,
        BacklightCompensation,
        Gain
    }

    public enum ControlProperty
    {
        Pan,
        Tilt,
        Roll,
        Zoom,
        Exposure,
        Iris,
        Focus
    }

    public enum SettingFlag
    {
        None,
        Auto,
        Manual
    }
}
