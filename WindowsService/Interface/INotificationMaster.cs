using Systel.Notification.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systel.Notification.Interface
{
    public interface INotificationMaster
    {
        ServiceMasterList GetExecutionServicemaster();
        DataSet GetServiceMasterDataByQuery(string connectionString, string queryString);
    }
}
