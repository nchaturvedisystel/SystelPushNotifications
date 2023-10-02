using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systel.Notification.Common
{
    public class WorkerOptions
    {
        public int ServiceFrequencyInMins { get; set; }
        public string AppKeyPath { get; set; }
        public string DBConn { get; set; }
    }
}
