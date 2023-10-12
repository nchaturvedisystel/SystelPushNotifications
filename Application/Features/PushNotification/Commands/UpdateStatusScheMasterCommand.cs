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
    public class UpdateStatusScheMasterCommand : IRequest<SchedularMasterDTO>
    {
        public SchedularMasterDTO schedularMasterDTO { get; set; }
    }
    internal class UpdateStatusSchedularMasterHandler : IRequestHandler<UpdateStatusScheMasterCommand, SchedularMasterDTO>
    {
        protected readonly ISchedularMaster _schedule;
        public UpdateStatusSchedularMasterHandler(ISchedularMaster schedule)
        {
            _schedule = schedule;
        }

        public async Task<SchedularMasterDTO> Handle(UpdateStatusScheMasterCommand request, CancellationToken cancellationToken)
        {
            return await _schedule.SchedularMasterStatusUpdate(request.schedularMasterDTO);
        }

    }
}
