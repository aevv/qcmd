using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using QCmd.Cef;
using QCmd.Input;

namespace QCmd
{
    public partial class QCmdHostForm : Form
    {
        private readonly BrowserWrapper _browser;
        private readonly KeyboardHook _hook;
        private readonly QcmdConfig _config;

        public QCmdHostForm(QcmdConfig config)
        {
            _config = config;

            InitializeComponent();

            _hook = new KeyboardHook();
            _hook.KeyPressed += KeyPressed;

            _hook.RegisterHotKey(KeyboardHook.ModifierKeys.Alt | KeyboardHook.ModifierKeys.Control, Keys.NumPad7);
            
            _browser = new BrowserWrapper(config.StartPage);
            Controls.Add(_browser.Control);

            Closed += (o, s) => CefSharp.Cef.Shutdown();

            CenterToScreen();
            Location = new Point(Location.X, 0);
        }

        public void KeyPressed(object sender, KeyboardHook.KeyPressedEventArgs args)
        {
            ToggleVisibility();
            FocusInput();
        }

        private void ToggleVisibility()
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void FocusInput()
        {
            Focus();
            _browser.Focus();
        }
    }
}
