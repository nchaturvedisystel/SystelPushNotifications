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
    public class SchedularMasterCommand : IRequest<SchedularMasterList>
    {
        public SchedularMasterDTO schedularMasterDTO { get; set; }
    }
    internal class SchedularMasterHandler : IRequestHandler<SchedularMasterCommand, SchedularMasterList>
    {
        protected readonly ISchedularMaster _schedule;
        public SchedularMasterHandler(ISchedularMaster schedule)
        {
            _schedule = schedule;
        }

        public async Task<SchedularMasterList> Handle(SchedularMasterCommand request, CancellationToken cancellationToken)
        {
            return await _schedule.GetSchedularMasterList(request.schedularMasterDTO);
        }



    }
}
