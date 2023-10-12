using Application.DTOs.PushNotification;
using Application.DTOs.User;
using Application.Features.Users.Commands;
using Application.Interfaces.PushNotification;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PushNotification.Commands
{
    public class UpdateStatusDBConnCommand : IRequest<DBConnectionDTO>
    {
        public DBConnectionDTO dBConnectionDTO { get; set; }
    }

    internal class UpdateStatusDBConnHandler : IRequestHandler<UpdateStatusDBConnCommand, DBConnectionDTO>
    {
        protected readonly IDBConnection _dbconn;
        public UpdateStatusDBConnHandler(IDBConnection dbconn)
        {
            _dbconn = dbconn;
        }

        public async Task<DBConnectionDTO> Handle(UpdateStatusDBConnCommand request, CancellationToken cancellationToken)
        {
            return await _dbconn.DBConnStatusUpdate(request.dBConnectionDTO);
        }

    }
}
