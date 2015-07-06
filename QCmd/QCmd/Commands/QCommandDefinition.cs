using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCmd.Commands
{
    class QCommandDefinition
    {
        public string Command { get; set; }
        public IList<string> Parameters { get; set; }

        public QCommandDefinition()
        {
            Parameters = new List<string>();
        }
    }
}
