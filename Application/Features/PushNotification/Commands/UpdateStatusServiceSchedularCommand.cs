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
    public class UpdateStatusServiceSchedularCommand : IRequest<ServiceSchedularDTO>
    {
        public ServiceSchedularDTO serviceSchedularDTO { get; set; }
    }

    internal class UpdateStatusServiceSchedularHandler : IRequestHandler<UpdateStatusServiceSchedularCommand, ServiceSchedularDTO>
    {
        protected readonly IServiceSchedular _service;
        public UpdateStatusServiceSchedularHandler(IServiceSchedular service)
        {
            _service = service;
        }

        public async Task<ServiceSchedularDTO> Handle(UpdateStatusServiceSchedularCommand request, CancellationToken cancellationToken)
        {
            return await _service.ServiceSchedularStatusUpdate(request.serviceSchedularDTO);
        }

    }


}
