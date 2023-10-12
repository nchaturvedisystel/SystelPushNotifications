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
    public class ServiceSchedularCommand : IRequest<ServiceSchedularList>
    {
        public ServiceSchedularDTO serviceSchedularDTO { get; set; }
    }
    internal class ServiceSchedularHandler : IRequestHandler<ServiceSchedularCommand, ServiceSchedularList>
    {
        protected readonly IServiceSchedular _schedule;
        public ServiceSchedularHandler(IServiceSchedular schedule)
        {
            _schedule = schedule;
        }

        public async Task<ServiceSchedularList> Handle(ServiceSchedularCommand request, CancellationToken cancellationToken)
        {
            return await _schedule.GetServiceschedularList(request.serviceSchedularDTO);
        }



    }



}
