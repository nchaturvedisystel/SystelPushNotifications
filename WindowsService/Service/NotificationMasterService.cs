using Systel.Notification.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Systel.Notification.Interface;
using MyFirstService;

namespace Systel.Notification.Service
{
    public class NotificationMasterService: INotificationMaster
    {
        private const string SP_GetExecutionServicemaster = "GetExecutionServicemaster";

        public NotificationMasterService()
        {
        }
        public ServiceMasterList GetExecutionServicemaster()
        {
            ServiceMasterList response = new ServiceMasterList();

            using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
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
