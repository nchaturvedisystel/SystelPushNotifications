using Application.DTOs.PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PushNotification
{
    public interface IAlertsServiceVariables
    {
        public Task<AlertsServiceVariablesList> alertsServiceVariablesList(AlertsServiceVariablesDTO alertsServiceVariablesDTO);

        public Task<AlertsServiceVariablesDTO> ServiceVariableStatusUpdate(AlertsServiceVariablesDTO alertsServiceVariablesDTO);
    }
}
