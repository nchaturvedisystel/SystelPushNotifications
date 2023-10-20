using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushNotification.Model
{
    public class ServiceSchedularDTO
    {
        public string SchedularName { get; set; }
        public string SchedularCode { get; set; }
        public string SchedularDesc { get; set; }
        public SchedularType SchedularType { get; set; }
        public string FrequencyInMins { get; set; }

    }
    public enum SchedularType
    {
        Recurring,
        SingleTime
    }
}
