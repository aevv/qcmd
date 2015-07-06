using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;
using QCmd.Commands;
using QCmd.Scripts;

namespace QCmd.Cef
{
    class BrowserWrapper
    {
        private readonly ChromiumWebBrowser _browser;
        public ChromiumWebBrowser Control { get { return _browser; } }
        private readonly QCmdHandler _handler;

        public BrowserWrapper(string url)
        {
            var factory = new LocalSchemeHandlerFactory();
            var settings = new CefSettings();

            settings.RegisterScheme(new CefCustomScheme { SchemeName = "local", SchemeHandlerFactory = factory });
            CefSharp.Cef.Initialize(settings);

            _browser = new ChromiumWebBrowser(url);

            _browser.BrowserSettings = new BrowserSettings()
            {
                FileAccessFromFileUrlsAllowed = true,
                UniversalAccessFromFileUrlsAllowed = true,
                TabToLinksDisabled = true
            };

            _handler = new QCmdHandler(new CommandStore(), new EngineWrapper());

            _browser.RegisterJsObject("qcmd", new QCmdCallback(_handler), false);
        }

        public void Focus()
        {
            _browser.ExecuteScriptAsync("focusQcmd();");
            _browser.Focus();
        }
    }
}
