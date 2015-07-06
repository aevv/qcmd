using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using QCmd.Scripts;

namespace QCmd.Commands
{
    class CommandStore
    {
        private IDictionary<string, string> _scripts;

        public CommandStore()
        {
            _scripts = new Dictionary<string, string>();
        }

        public void LoadScripts(EngineWrapper engine, string dir)
        {
            LoadDirectory(dir);
            foreach (var script in _scripts)
            {
                engine.Execute(File.ReadAllText(script.Value));
            }
        }

        public void LoadDirectory(string dir)
        {
            var dirInfo = new DirectoryInfo(dir);

            foreach (var directory in dirInfo.GetDirectories())
            {
                LoadDirectory(directory.FullName);
            }
                
            foreach (var file in dirInfo.GetFiles("*.js"))
            {
                _scripts.Add(file.Name.Substring(0, file.Name.Length - 3), file.FullName);
            }
        }

        public bool HasScript(string name)
        {
            return _scripts.ContainsKey(name);
        }

        public string GetScript(string name)
        {
            return File.ReadAllText(_scripts[name]);
        }
    }
}
