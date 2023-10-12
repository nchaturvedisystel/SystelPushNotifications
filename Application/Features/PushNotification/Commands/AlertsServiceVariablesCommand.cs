using Application.DTOs.PushNotification;
using Application.Interfaces.PushNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PushNotification.Commands
{
    public class AlertsServiceVariablesCommand : IRequest<AlertsServiceVariablesList>
    {
        public AlertsServiceVariablesDTO alertsServiceVariablesDTO { get; set; }
    }
    internal class AlertsServiceVariablesHandler : IRequestHandler<AlertsServiceVariablesCommand, AlertsServiceVariablesList>
    {
        protected readonly IAlertsServiceVariables _service;
        public AlertsServiceVariablesHandler(IAlertsServiceVariables service)
        {
            _service = service;
        }

        public async Task<AlertsServiceVariablesList> Handle(AlertsServiceVariablesCommand request, CancellationToken cancellationToken)
        {
            return await _service.alertsServiceVariablesList(request.alertsServiceVariablesDTO);
        }



    }
}
