using Application.DTOs.PushNotification;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PushNotification
{
    public interface IDBConnection
    {
    
        public Task<DBConnectionList> GetDBConnectionList(DBConnectionDTO dbConnectionDTO);
        public Task<DBConnectionDTO> DBConnStatusUpdate(DBConnectionDTO dbConnectionDTO);
    }
}
