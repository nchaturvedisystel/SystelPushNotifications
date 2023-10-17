using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushNotification.Model;

namespace PushNotification.Interface
{
    public interface IDBConfig
    {
        List<DBConfigConnection> GetAllDBConfigConnection();

        DBConfigConnection GetDBConfigConnection();
    }
}