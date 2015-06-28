using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;

namespace QCmd.Cef
{
    class BrowserWrapper
    {
        private readonly ChromiumWebBrowser _browser;
        public ChromiumWebBrowser Control { get { return _browser; } }

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

            _browser.RegisterJsObject("qcmd", new QCmdCallback(), false);
        }

        public void Focus()
        {
            _browser.ExecuteScriptAsync("focusQcmd();");
            _browser.Focus();
        }
    }
}
