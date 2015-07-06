using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QCmd.Commands;

namespace QCmd.Cef
{
    class QCmdCallback
    {
        private readonly QCmdHandler _handler;

        public QCmdCallback(QCmdHandler handler)
        {
            _handler = handler;
        }

        public void Execute(string command)
        {
            _handler.HandleCommand(command);
        }
    }
}
