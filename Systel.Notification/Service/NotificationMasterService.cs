using Microsoft.Extensions.Options;
using Systel.Notification.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systel.Notification.Common;
using Dapper;
using Systel.Notification.Interface;

namespace Systel.Notification.Service
{
    public class NotificationMasterService: INotificationMaster
    {
        private const string SP_GetExecutionServicemaster = "GetExecutionServicemaster";

        private ILogger<NotificationMasterService> _logger;
        private WorkerOptions options;
        public NotificationMasterService(ILogger<NotificationMasterService> logger, WorkerOptions options)
        {
            _logger = logger;
            this.options = options;
        }
        public ServiceMasterList GetExecutionServicemaster()
        {
            ServiceMasterList response = new ServiceMasterList();

            using (SqlConnection connection = new SqlConnection(options.DBConn))
            {
                response.ServicemasterList = connection.Query<ServiceMasterDTO>(SP_GetExecutionServicemaster, commandType: CommandType.StoredProcedure);

            }
            return response;
        }
        public DataSet GetServiceMasterDataByQuery(string connectionString, string queryString)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.Fill(dataset);
                return dataset;
            }
        }
    }
}
