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
                OnPresetChanged(new PresetChange(preset, PresetChangeType.Loaded));
            }
            RegisterAllHotkeys(presets);
        }

        public void CreatePreset()
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
                CameraControls = _cameraController.CameraControls,
                Device = savedDevice
            };

            db.Presets.Add(newPreset);
            db.SaveChanges();

            var change = new PresetChange(newPreset, PresetChangeType.Created);
            OnPresetChanged(change);
        }

        public void UpdatePreset(Preset unsavedPreset)
        {
            using var db = new AppDbContext();

            var savedPreset = GetPresetFromDb(db, unsavedPreset.Id);
            savedPreset.UpdateFrom(unsavedPreset);

            db.SaveChanges();
            var change = new PresetChange(savedPreset, PresetChangeType.Updated);
            OnPresetChanged(change);
        }

        public void DeletePreset(Preset removedPreset)
        {
            using var db = new AppDbContext();

            var presetToDelete = GetPresetFromDb(db, removedPreset.Id);

            db.Presets.Remove(presetToDelete);

            var hasOtherPresets = db.Presets.Any(p => p.Device.Id == removedPreset.Device.Id && p.Id != removedPreset.Id);
            if (!hasOtherPresets)
            {
                var deviceToRemove = db.Devices.Find(removedPreset.Device.Id);
                if (deviceToRemove != null)
                {
                    db.Devices.Remove(deviceToRemove);
                }
            }

            db.SaveChanges();
            var change = new PresetChange(removedPreset, PresetChangeType.Removed);
            OnPresetChanged(change);
        }

        private Preset GetPresetFromDb(AppDbContext db, int presetId)
        {
            var preset = db.Presets
                .Include(p => p.Device)
                .Include(p => p.Hotkey)
                .FirstOrDefault(p => p.Id == presetId);

            return preset;
        }

        #endregion

        #region Atalho de teclado

        public void RegisterAllHotkeys(List<Preset> presets)
        {
            foreach (var preset in presets)
            {
                if (preset.Hotkey != null) _hotkeyService.Register(preset.Hotkey, preset.Id);
            }
        }

        public void SetPresetHotkey(Preset preset, Hotkey hotkey)
        {
            using var db = new AppDbContext();

            if (hotkey == null)
            {
                _hotkeyService.Unregister(preset.Id);
            }
            else
            {
                hotkey.NoRepeat = true;
                _hotkeyService.Register(hotkey, preset.Id);
            }

            var presetFromDb = GetPresetFromDb(db, preset.Id);
            presetFromDb.Hotkey = hotkey;

            db.SaveChanges();
            var change = new PresetChange(presetFromDb, PresetChangeType.Updated);
            OnPresetChanged(change);
        }

        #endregion

        #region Aplicar Preset

        public void ApplyPreset(Preset preset) => ApplyPreset(preset.Id);

        public void ApplyPreset(int presetId)
        {
            using var db = new AppDbContext();

            var preset = GetPresetFromDb(db, presetId);
            _cameraController.Connect(preset.Device);
            _cameraController.ApplyPreset(preset);
            OnPresetApplied(preset.Device, preset);
        }

        #endregion

        #region Events

        private void OnPresetChanged(PresetChange change) => PresetChanged?.Invoke(this, change);

        private void OnPresetApplied(Device device, Preset preset) =>
            PresetApplied?.Invoke(this, new PresetAppliedEventArgs(preset));
    }
}
public enum PresetChangeType
{
    Loaded,
    Created,
    Updated,
    Removed
}

public class PresetChange : EventArgs
{
    public Preset Preset { get; }
    public PresetChangeType ChangeType { get; }

    public PresetChange(Preset preset, PresetChangeType changeType)
    {
        Preset = preset;
        ChangeType = changeType;
    }
}

public class PresetAppliedEventArgs : EventArgs
{
    public Preset Preset { get; }

    public PresetAppliedEventArgs(Preset preset)
    {
        Preset = preset;
    }
}

#endregion