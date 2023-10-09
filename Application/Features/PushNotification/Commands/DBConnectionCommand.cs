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
    public class DBConnectionCommand : IRequest<DBConnectionList>
    
    {
        public DBConnectionDTO dBConnectionDTO { get; set; }
    }

    internal class DBConnectionHandler : IRequestHandler<DBConnectionCommand, DBConnectionList>
    {
        protected readonly IDBConnection _dbconn;
        public DBConnectionHandler(IDBConnection dbconn)
        {
            _dbconn = dbconn;
        }

        public async Task<DBConnectionList> Handle(DBConnectionCommand request, CancellationToken cancellationToken)
        {
            return await _dbconn.GetDBConnectionList(request.dBConnectionDTO);
        }



    }
}
