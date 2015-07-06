using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;

namespace QCmd.Scripts
{
    class EngineWrapper
    {
        private readonly Engine _engine;

        public EngineWrapper()
        {
            _engine = new Engine(cfg => cfg.AllowClr());

            _engine.SetValue("log", new Action<object>(Console.WriteLine));
            _engine.SetValue("StartProcess", new Action<string, string>((proc, arg) => Process.Start(proc, arg)));
        }

        public void Execute(string script)
        {
            _engine.Execute(script);
        }

        public void Invoke(string func, string args)
        {
            _engine.Invoke(func, args);
        }
    }
}
