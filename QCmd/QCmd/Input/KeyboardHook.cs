using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QCmd.Input
{
    public class KeyboardHook : IDisposable
    {
        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--)
                UnregisterHotKey(_window.Handle, i);

            _window.Dispose();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hwnd, int id);

        [Flags]
        public enum ModifierKeys : uint
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        class Window : NativeWindow, IDisposable
        {
            private const int WM_HOTKEY = 0x0312;

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                DestroyHandle();
            }

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
                    var modifier = (ModifierKeys) ((int) m.LParam & 0xFFFF);

                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }
        }

        private Window _window;
        private int _currentId;

        public EventHandler<KeyPressedEventArgs> KeyPressed;

        public KeyboardHook()
        {
            _window = new Window();
            _window.KeyPressed += (o, a) =>
                                  {
                                      if (KeyPressed != null)
                                          KeyPressed(this, a);
                                  };
        }

        public int RegisterHotKey(ModifierKeys mods, Keys key)
        {
            if (!RegisterHotKey(_window.Handle, ++_currentId, (uint) mods, (uint) key))
                throw new Exception("Key register failed");

            return _currentId;
        }

        public class KeyPressedEventArgs : EventArgs
        {
            private readonly ModifierKeys _mod;
            private readonly Keys _key;

            public ModifierKeys Mods { get { return _mod; } }
            public Keys Key { get { return _key; } }

            internal KeyPressedEventArgs(ModifierKeys mod, Keys key)
            {
                _mod = mod;
                _key = key;
            }
        }
    }
}
