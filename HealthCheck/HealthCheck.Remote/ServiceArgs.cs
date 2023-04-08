using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Remote
{
    public class ServiceArgs
    {
        public string Key { get; set; }
        public ServiceArgs(string key) { Key = key; }
    }
}
