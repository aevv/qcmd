using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCmd.Scripts;

namespace QCmd.Commands
{
    class QCmdHandler
    {
        private readonly CommandStore _store;
        private readonly EngineWrapper _wrapper;
        public Action HideCallback { get; set; }

        public QCmdHandler(CommandStore store, EngineWrapper wrapper)
        {
            _store = store;
            _wrapper = wrapper;

            _store.LoadScripts(_wrapper, "Commands/Default");
        }

        public void HandleCommand(string command)
        {
            var parser = new CommandParser();
            var qcmds = parser.Parse(command).ToList();

            if (!qcmds.Any())
            {
                HideCallback();
                return;
            }

            foreach (var cmd in qcmds)
            {
                if (cmd.HideOnFinish)
                    if (HideCallback != null)
                        HideCallback();

                if (!_store.HasScript(cmd.Command))
                    continue;

                _wrapper.Invoke(cmd.Command, string.Join(" ", cmd.Parameters));
            }
        }
    }
}
