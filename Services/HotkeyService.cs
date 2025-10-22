using System.ComponentModel;
using System.Runtime.InteropServices;
using WebcamController.Models;

namespace WebcamController.Services
{
    public class HotkeyService : IDisposable
    {
        private HotkeyWindow _hotkeyWindow;
        private readonly Dictionary<int, Hotkey> _registeredHotkeys = new();

        public event Action<int> HotkeyPressed;
        private enum Modifiers
        {
            None = 0x000,
            Control = 0x002,
            Shift = 0x004,
            Alt = 0x008,
            NoRepeat = 0x4000
        }

        public HotkeyService()
        {
            _hotkeyWindow = new HotkeyWindow(this);
        }

        public void Register(Hotkey hotkey, int id)
        {
            if (id == null) id = hotkey.Id;

            var (modifiers, key) = HotkeyConverter(hotkey);
            if (hotkey.NoRepeat) modifiers |= (uint)Modifiers.NoRepeat;

            if (_registeredHotkeys.ContainsKey(id)) Unregister(id);

            bool result = NativeMethods.RegisterHotKey(_hotkeyWindow.Handle, id, modifiers, key);
            if (!result)
            {
                Win32Exception ex = new(Marshal.GetLastWin32Error());
                throw new HotkeyException(HotkeyException.HotkeyExceptionMessages(ex), ex);
            }
            else
            {
                _registeredHotkeys.Add(id, hotkey);
            }
        }

        public void Unregister(int id)
        {
            if (!_registeredHotkeys.ContainsKey(id)) return;

            bool result = NativeMethods.UnregisterHotKey(_hotkeyWindow.Handle, id);
            if (!result)
            {
                Win32Exception ex = new(Marshal.GetLastWin32Error());
                throw new HotkeyException(HotkeyException.HotkeyExceptionMessages(ex), ex);
            }
            else
            {
                _registeredHotkeys.Remove(id);
            }
        }

        public void Dispose()
        {
            foreach (int id in _registeredHotkeys.Keys) Unregister(id);
            _hotkeyWindow?.Dispose();
        }

        private (uint, uint) HotkeyConverter(Hotkey hotkey)
        {
            Modifiers modifiers = Modifiers.None;

            if ((hotkey.KeyData & Keys.Shift) == Keys.Shift)
            {
                modifiers |= Modifiers.Shift;
            }

            if ((hotkey.KeyData & Keys.Control) == Keys.Control)
            {
                modifiers |= Modifiers.Control;
            }

            if ((hotkey.KeyData & Keys.Menu) == Keys.Menu)
            {
                modifiers |= Modifiers.Alt;
            }

            Keys key = hotkey.KeyData & Keys.KeyCode;

            return ((uint)modifiers, (uint)key);
        }

        private class HotkeyWindow : NativeWindow, IDisposable
        {
            private readonly HotkeyService _service;
            public HotkeyWindow(HotkeyService service)
            {
                _service = service;
                CreateParams cp = new CreateParams();
                cp.Caption = $"{Application.ProductName} (Hotkey Listener)";
                cp.Style = 0;
                cp.ExStyle = 0;

                CreateHandle(cp);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WM_HOTKEY)
                {
                    int hotkeyId = m.WParam.ToInt32();
                    _service.HotkeyPressed?.Invoke(hotkeyId);
                }
                base.WndProc(ref m);
            }

            public void Dispose()
            {
                DestroyHandle();
            }
        }
    }

    internal static class NativeMethods
    {
        internal const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    public class HotkeyException(string message, Exception innerException) : Exception(message, innerException)
    {
        public static string HotkeyExceptionMessages(Win32Exception ex)
        {
            return ex.NativeErrorCode switch
            {
                1409 => "A combinação já está sendo usada por outro programa.",
                87 => "Parâmetro inválido ao registrar o atalho.",
                6 => "O handle da janela é inválido.",
                5 => "Acesso negado ao registrar o atalho.",
                _ => $"Erro desconhecido ao registrar o atalho. Código: {ex.NativeErrorCode}",
            };
        }
    }
}
