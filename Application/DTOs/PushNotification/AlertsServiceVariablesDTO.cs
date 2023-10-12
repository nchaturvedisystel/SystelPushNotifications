using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PushNotification
{
    public class AlertsServiceVariablesDTO
    {
        public int VariableId { get; set; }
        public int ServiceId { get; set; }
        public string VarInstance { get; set; }
        public string VarValue { get; set; }
        public string VarType { get; set; }
        public string Title { get; set; }
    }

    public class AlertsServiceVariablesList
    {
        public IEnumerable<AlertsServiceVariablesDTO> AlertsserviceVariablesList { get; set; }
    }
}
