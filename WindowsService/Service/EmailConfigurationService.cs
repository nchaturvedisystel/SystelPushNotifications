using Systel.Notification.Interface;
using Systel.Notification.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MyFirstService;

namespace Systel.Notification.Service
{
    public class EmailConfigurationService : IEmailConfiguration
    {
        private const string SP_GetEmailConfigDetails = "GetEmailConfigDetails";

        public EmailConfigurationService()
        {
        }

        public EmailConfigurationList GetEmailConfigDetails()
        {
            EmailConfigurationList response = new EmailConfigurationList();

            using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
            {
                response.EmailConfigList = connection.Query<EmailConfigurationDTO>(SP_GetEmailConfigDetails, commandType: CommandType.StoredProcedure);

            }
            return response;
        }
    }
}
