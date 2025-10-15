using WebcamController.Models;

namespace WebcamController.Forms
{
    public partial class HotkeyReader : Form
    {
        public Hotkey Hotkey { get; set; }

        public HotkeyReader()
        {
            InitializeComponent();
        }

        private void HotkeyReader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
            {
                InvalidHotkey();
                return;
            }

            Hotkey = new Hotkey();
            Hotkey.KeyData = e.KeyData;
            KeysConverter _converter = new KeysConverter();
            Hotkey.DisplayString = _converter.ConvertToString(e.KeyData);
            label1.Text = Hotkey.DisplayString;

            e.Handled = true;
            btnOk.Enabled = true;
        }

        private void RemoveHotkey()
        {
            label1.Text = "(nenhum atalho)";
            btnOk.Enabled = true;
            Hotkey = null;
        }

        private void InvalidHotkey()
        {
            label1.Text = "Pressione qualquer combinação de teclas...";
            btnOk.Enabled = false;
            return;
        }

        private void linkRemoveHotkey_Click(object sender, EventArgs e) => RemoveHotkey();
    }
}
