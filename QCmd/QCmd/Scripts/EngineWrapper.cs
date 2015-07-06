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
        private readonly Dictionary<string, Action> _hardCommands; 

        public EngineWrapper()
        {
            _hardCommands = new Dictionary<string, Action>();
            _engine = new Engine(cfg => cfg.AllowClr());

            _engine.SetValue("log", new Action<object>(Console.WriteLine));
            _engine.SetValue("StartProcess", new Action<string, string>((proc, arg) => Process.Start(proc, arg)));
            _engine.SetValue("KillProcess", new Action<string>(proc => Process.GetProcessesByName(proc).First().Kill()));
        }

        public void AddHardCommand(string command, Action action)
        {
            if (_hardCommands.ContainsKey(command))
                return;

            _hardCommands.Add(command, action);
        }

        public void Execute(string script)
        {
            _engine.Execute(script);
        }

        public void Invoke(string func, string args)
        {
            if (_hardCommands.ContainsKey(func))
            {
                _hardCommands[func]();
                return;
            }

            _engine.Invoke(func, args);
        }
    }
}
