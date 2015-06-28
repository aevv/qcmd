using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QCmd
{
    public class QCmdMain
    {
        public static void Main(string[] args)
        {
            var config = QcmdConfig.Create("config.json");
            if (config == null)
                throw new Exception("Config null - create a config.json file");

            var form = new QCmdHostForm(config);
            Application.Run(form);
        }
    }
}
