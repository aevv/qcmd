using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QCmd
{
    public class QcmdConfig
    {
        [JsonProperty("start")]
        public string StartPage { get; set; }

        public static QcmdConfig Create(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            return JsonConvert.DeserializeObject<QcmdConfig>(File.ReadAllText(fileName));
        }
    }
}
