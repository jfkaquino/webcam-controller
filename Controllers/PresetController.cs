using Microsoft.EntityFrameworkCore;
using WebcamController.Models;
using WebcamController.Services;

namespace WebcamController.Controllers
{
    public class PresetController
    {
        private readonly HotkeyService _hotkeyService;
        private readonly CameraController _cameraController;

        public event EventHandler<PresetChange> PresetChanged;
        public event EventHandler<PresetAppliedEventArgs> PresetApplied;

        public PresetController(CameraController cameraController, HotkeyService hotkeyService)
        {
            _cameraController = cameraController;
            _hotkeyService = hotkeyService;
            _hotkeyService.HotkeyPressed += ApplyPreset;
        }

        #region CRUD Preset (Operações em Memória)

        public void LoadData()
        {
            using var db = new AppDbContext();

            List<Preset> presets = db.Presets
                .Include(p => p.Device)
                .Include(p => p.Hotkey)
                .ToList();

            foreach (var preset in presets)
            {
                OnPresetChanged(new PresetChange(preset.Device, preset, PresetChangeType.Loaded));
            }
            RegisterAllHotkeys();
        }

        public int CreatePreset()
        {
            using var db = new AppDbContext();

            var connectedDevice = _cameraController.ConnectedDevice;
            var savedDevice = db.Devices
                .FirstOrDefault(d => d.DevicePath == connectedDevice.DevicePath);

            if (savedDevice == null)
            {
                savedDevice = new Device
                {
                    FriendlyName = connectedDevice.FriendlyName,
                    DevicePath = connectedDevice.DevicePath
                };
                db.Devices.Add(savedDevice);
            }

            var newPreset = new Preset
            {
                Name = "Novo preset",
                Device = savedDevice,
            };

            db.SaveChanges();

            var change = new PresetChange(savedDevice, newPreset, PresetChangeType.Created);
            OnPresetChanged(change);
            return newPreset.Id;
        }

        public void UpdatePreset(Preset updatedPreset)
        {
            using var db = new AppDbContext();

            db.Devices.Update(updatedPreset.Device);
            db.Presets.Update(updatedPreset);
            var change = new PresetChange(updatedPreset.Device, updatedPreset, PresetChangeType.Updated);
            OnPresetChanged(change);
            db.SaveChanges();
        }

        public void DeletePreset(Preset removedPreset)
        {
            using var db = new AppDbContext();
            db.Presets.Remove(removedPreset);

            var change = new PresetChange(removedPreset.Device, removedPreset, PresetChangeType.Removed);
            OnPresetChanged(change);

            if (!removedPreset.Device.Presets.Any())
            {
                db.Devices.Remove(removedPreset.Device);
            }
            db.SaveChanges();
        }

        public void SaveAllPresets()
        {
            using var db = new AppDbContext();
            db.SaveChanges();
        }

        #endregion

        #region Atalho de teclado

        public void RegisterAllHotkeys()
        {
            using var db = new AppDbContext();
            foreach (var preset in db.Presets.Include(p => p.Hotkey))
            {
                if (preset.Hotkey != null)
                {
                    _hotkeyService.Register(preset.Hotkey, preset.Id);
                }
            }
        }

        public void SetPresetHotkey(Preset preset, Hotkey hotkey)
        {
            if (hotkey == null)
            {
                _hotkeyService.Unregister(preset.Id);
            }
            else
            {
                hotkey.NoRepeat = true;
                _hotkeyService.Register(hotkey, preset.Id);
            }

            using var db = new AppDbContext();
            preset.Hotkey = hotkey;
            db.Presets.Update(preset);
            db.SaveChanges();

            var change = new PresetChange(preset.Device, preset, PresetChangeType.Updated);
            OnPresetChanged(change);
        }

        #endregion

        #region Aplicar Preset

        public void ApplyPreset(Preset preset) => ApplyPreset(preset.Id);

        public void ApplyPreset(int presetId)
        {
            using var db = new AppDbContext();

            var preset = db.Presets.FirstOrDefault(p => p.Id == presetId);
            //_cameraController.Connect(preset.Device);
            //_cameraController.ApplyPreset(preset);
            OnPresetApplied(preset.Device, preset);
        }

        #endregion

        #region Events

        private void OnPresetChanged(PresetChange change) => PresetChanged?.Invoke(this, change);

        private void OnPresetApplied(Device device, Preset preset) =>
            PresetApplied?.Invoke(this, new PresetAppliedEventArgs(device, preset));
    }
}
public enum PresetChangeType
{
    Loaded,
    Created,
    Updated,
    Removed,
    Saved
}

public class PresetChange : EventArgs
{
    public Device Device { get; }
    public Preset Preset { get; }
    public PresetChangeType ChangeType { get; }

    public PresetChange(Device device, Preset preset, PresetChangeType changeType)
    {
        Device = device;
        Preset = preset;
        ChangeType = changeType;
    }
}

public class PresetAppliedEventArgs : EventArgs
{
    public Device Device { get; }
    public Preset Preset { get; }

    public PresetAppliedEventArgs(Device device, Preset preset)
    {
        Device = device;
        Preset = preset;
    }
}

#endregion