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
    public class ServiceMasterCommand : IRequest<ServiceMasterList>
    {
        public ServiceMasterDTO serviceMasterDTO { get; set; }
    }

    internal class ServiceMasterHandler : IRequestHandler<ServiceMasterCommand, ServiceMasterList>
    {
        protected readonly IServiceMaster _service;
        public ServiceMasterHandler(IServiceMaster service)
        {
            _service = service;
        }

        public async Task<ServiceMasterList> Handle(ServiceMasterCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetServiceMasterList(request.serviceMasterDTO);
        }



    }
}
