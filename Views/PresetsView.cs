using DirectShowLib;
using System.Data;
using WebcamController.Controllers;
using WebcamController.Forms;
using WebcamController.Models;
using WebcamController.Properties;
using WebcamController.Services;

namespace WebcamController.Views
{
    public partial class PresetsView : UserControl, IMainForm
    {
        public PresetController PresetController { get; set; }
        public CameraController CameraController { get; set; }

        public PresetsView()
        {
            InitializeComponent();
        }

        public void Initialize(
            PresetController presetController,
            CameraController cameraController,
            HotkeyService hotkeyService)
        {
            PresetController = presetController;
            CameraController = cameraController;

            PresetController.PresetChanged += OnPresetChanged;
            PresetController.PresetApplied += OnPresetApplied;
            CameraController.DeviceConnected += OnDeviceChanged;
            ChangeListViewMode(Settings.Default.ListViewMode);
            LoadPresets();
        }

        #region Life cycle

        public void OnMainFormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void LoadPresets()
        {
            listPresets.Items.Clear();
            listPresets.BeginUpdate();
            PresetController.LoadData();
            listPresets.EndUpdate();
            UpdateButtonsState();
        }

        #endregion

        #region Event binding

        private void OnPresetChanged(object? sender, PresetChange e)
        {
            switch (e.ChangeType)
            {
                case PresetChangeType.Loaded: AddPresetToListView(e.Preset); break;
                case PresetChangeType.Created: CreatePresetinListView(e.Preset); break;
                case PresetChangeType.Updated: UpdatePresetInListView(e.Preset); break;
                case PresetChangeType.Removed: RemovePresetFromListView(e.Preset); break;
            }
        }

        private void OnPresetApplied(object? sender, PresetAppliedEventArgs e)
        {
            var item = FindItemPreset(e.Preset.Id);
            listPresets.SelectedItems.Clear();
            item.Group.CollapsedState = ListViewGroupCollapsedState.Expanded;
            item.EnsureVisible();
            item.Selected = true;
            MessageBox.Show("Preset aplicado");
        }

        private void AddPresetToListView(Preset preset)
        {
            listPresets.BeginUpdate();
            var group = listPresets.Groups.Cast<ListViewGroup>()
                .FirstOrDefault(g => g.Tag is Device d && d.DevicePath == preset.Device.DevicePath);

            if (group == null)
            {
                bool status = CameraController.IsDeviceAvailable(preset.Device);
                group = new ListViewGroup
                {
                    Header = preset.Device.FriendlyName,
                    Subtitle = status ? "Disponível" : "Indisponível",
                    Tag = preset.Device,
                    CollapsedState = status ? ListViewGroupCollapsedState.Expanded : ListViewGroupCollapsedState.Collapsed
                };
                listPresets.Groups.Add(group);
            }

            var item = new ListViewItem(preset.Name)
            {
                Name = preset.Id.ToString(),
                ToolTipText = $"ID: {preset.Id}" +
                $"\nNome: {preset.Name}" +
                $"\nAtalho: {preset.Hotkey?.DisplayString ?? "Nenhum"}" +
                $"\nDispositivo: {preset.Device.FriendlyName}",
                Group = group,
                Tag = preset
            };
            item.SubItems.Add(preset.Hotkey?.DisplayString ?? "Nenhum");
            item.SubItems.Add(preset.Id.ToString());

            listPresets.Items.Add(item);
            listPresets.EndUpdate();
        }

        private void CreatePresetinListView(Preset preset)
        {
            AddPresetToListView(preset);
            var item = FindItemPreset(preset.Id);
            listPresets.SelectedItems.Clear();
            item.Group.CollapsedState = ListViewGroupCollapsedState.Expanded;
            item.EnsureVisible();
            item.Selected = true;
            item.BeginEdit();
        }

        private void UpdatePresetInListView(Preset preset)
        {
            var item = FindItemPreset(preset.Id);
            if (item == null) return;

            item.Tag = preset;
            item.ToolTipText = $"ID: {preset.Id}" +
                $"\nNome: {preset.Name}" +
                $"\nAtalho: {preset.Hotkey?.DisplayString ?? "Nenhum"}" +
                $"\nDispositivo: {preset.Device.FriendlyName}";
            item.SubItems[1].Text = preset.Hotkey?.DisplayString ?? "Nenhum";
            item.SubItems[2].Text = preset.Id.ToString();
        }

        private void RemovePresetFromListView(Preset preset)
        {
            var item = FindItemPreset(preset.Id);
            if (item == null) return;

            listPresets.Items.Remove(item);

            var group = item.Group;
            if (group != null && !listPresets.Items.Cast<ListViewItem>().Any(i => i.Group == group))
            {
                listPresets.Groups.Remove(group);
            }
        }

        private void OnDeviceChanged(object? sender, DeviceConnectedEventArgs e)
        {
            if (e.Status == DeviceStatus.NotSupported)
            {
                btnNewPreset.Enabled = false;
                itemNewPreset.Enabled = false;
            }
        }

        #endregion

        #region Item management

        private void NewPreset()
        {
            PresetController.CreatePreset();
        }

        private void RenamePreset()
        {
            var item = listPresets.SelectedItems[0];
            listPresets.SelectedItems.Clear();
            item.Group.CollapsedState = ListViewGroupCollapsedState.Expanded;
            item.EnsureVisible();
            item.Selected = true;
            item.BeginEdit();
        }

        private void DeletePreset()
        {
            int count = listPresets.SelectedItems.Count;

            var msg = count == 1
                ? $"Excluir '{listPresets.SelectedItems[0].Text}'?" +
                $"\nEssa ação não poderá ser desfeita."
                : $"Excluir {count} predefinições?" +
                $"\nEssa ação não poderá ser desfeita.";

            if (MessageBox.Show(msg, "Excluir predefinição(s)", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) { return; }

            foreach (ListViewItem item in listPresets.SelectedItems)
            {
                PresetController.DeletePreset((Preset)item.Tag);
            }
        }

        private void ApplyPreset()
        {
            var item = listPresets.SelectedItems[0];
            if (item == null) return;
            try
            {
                PresetController.ApplyPreset(SelectedPreset);
            }
            catch (DataException ex)
            {
                MessageBox.Show($"Erro ao aplicar predefinição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region UI events

        private void listPresets_SelectedIndexChanged(object sender, EventArgs e) => UpdateButtonsState();

        private void UpdateButtonsState()
        {
            int count = listPresets.SelectedItems.Count;
            bool single = count == 1;
            bool any = count > 0;
            bool none = count == 0;

            btnApplyPreset.Enabled = single;
            itemViewMode.Visible = none;
            itemSelectAll.Visible = none;
            itemApplyPreset.Visible = single;

            btnDeletePreset.Enabled = any;
            itemDeletePreset.Visible = any;
            itemDeletePreset.Enabled = any;

            itemRenamePreset.Visible = single;
            itemRenamePreset.Enabled = single;

            itemHotkey.Visible = single;
            sprItem2.Visible = any;
        }

        private void SelectListViewMode(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int selectedViewMode = int.Parse(item.Tag.ToString());
            ChangeListViewMode(selectedViewMode);
            Settings.Default.ListViewMode = selectedViewMode;
        }

        private void ChangeListViewMode(int selectedView)
        {
            foreach (ToolStripMenuItem item in itemViewMode.DropDownItems)
            {
                item.Checked = false;
                if (int.Parse(item.Tag.ToString()) == selectedView)
                {
                    item.CheckState = CheckState.Indeterminate;
                    item.Checked = true;
                }
            }

            listPresets.View = (View)selectedView;
        }

        private void NewPreset_Click(object sender, EventArgs e) => NewPreset();

        private void ApplyPreset_Click(object sender, EventArgs e) => ApplyPreset();

        private void DeletePreset_Click(object sender, EventArgs e) => DeletePreset();

        private void RenamePreset_Click(object sender, EventArgs e) => RenamePreset();

        private void listPresets_ItemActivate(object sender, EventArgs e) => ApplyPreset();

        private void SetHotkey_Click(object sender, EventArgs e)
        {
            var preset = SelectedPreset;
            var hotkeyReader = new HotkeyReader();
            hotkeyReader.ShowDialog();
            if (hotkeyReader.DialogResult == DialogResult.OK)
            {
                PresetController.SetPresetHotkey(preset, hotkeyReader.Hotkey);
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listPresets.Items)
            {
                item.Selected = true;
            }
        }

        private void listPresets_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            var preset = (Preset)listPresets.Items[e.Item].Tag;
            preset.Name = e.Label;
            PresetController.UpdatePreset(preset);
        }

        #endregion

        #region ListView helpers

        private ListViewItem FindItemPreset(int presetId) =>
            listPresets.Items.Cast<ListViewItem>().FirstOrDefault(i => ((Preset)i.Tag).Id == presetId);

        private void itemReadOnly_Click(object sender, EventArgs e)
        {

        }

        private Preset SelectedPreset => (Preset)listPresets.SelectedItems[0].Tag;

        #endregion
    }
}
