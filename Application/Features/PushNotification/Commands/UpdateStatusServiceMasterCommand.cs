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
    public class UpdateStatusServiceMasterCommand : IRequest<ServiceMasterDTO>
    {
        public ServiceMasterDTO serviceMasterDTO { get; set; }
    }

    internal class UpdateStatusServiceMasterHandler : IRequestHandler<UpdateStatusServiceMasterCommand, ServiceMasterDTO>
    {
        protected readonly IServiceMaster _service;
        public UpdateStatusServiceMasterHandler(IServiceMaster service)
        {
            _service = service;
        }

        public async Task<ServiceMasterDTO> Handle(UpdateStatusServiceMasterCommand request, CancellationToken cancellationToken)
        {
            return await _service.ServiceMasterStatusUpdate(request.serviceMasterDTO);
        }

    }
}
