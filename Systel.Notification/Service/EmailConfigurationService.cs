using Infrastructure;
using Systel.Notification.Interface;
using Systel.Notification.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Systel.Notification.Common;

namespace Systel.Notification.Service
{
    public class EmailConfigurationService : DABase, IEmailConfiguration
    {
        private const string SP_GetEmailConfigDetails = "GetEmailConfigDetails";

        private ILogger<EmailConfigurationService> _logger;
        private WorkerOptions options;
        public EmailConfigurationService(ILogger<EmailConfigurationService> logger, WorkerOptions options)
        {
            _logger = logger;
            this.options = options;
        }

        public EmailConfigurationList GetEmailConfigDetails()
        {
            EmailConfigurationList response = new EmailConfigurationList();

            using (SqlConnection connection = new SqlConnection(options.DBConn))
            {
                response.EmailConfigList = connection.Query<EmailConfigurationDTO>(SP_GetEmailConfigDetails, commandType: CommandType.StoredProcedure);

            }
            return response;
        }
    }
}
