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
    public class UpdateStatusServiceVariableCommand : IRequest<AlertsServiceVariablesDTO>
    {
        public AlertsServiceVariablesDTO alertsServiceVariablesDTO { get; set; }
    }

    internal class UpdateStatusServiceVariableHandler : IRequestHandler<UpdateStatusServiceVariableCommand, AlertsServiceVariablesDTO>
    {
        protected readonly IAlertsServiceVariables _service;
        public UpdateStatusServiceVariableHandler(IAlertsServiceVariables service)
        {
            _service = service;
        }

        public async Task<AlertsServiceVariablesDTO> Handle(UpdateStatusServiceVariableCommand request, CancellationToken cancellationToken)
        {
            return await _service.ServiceVariableStatusUpdate(request.alertsServiceVariablesDTO);
        }

    }
}
