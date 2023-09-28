using Application.DTOs.PushNotification;
using Application.Interfaces.PushNotification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Persistance.Services.PushNotification
{
    public class EmailConfigurationService : DABase, IEmailConfiguration
    {
        private const string SP_GetEmailConfigDetails = "GetEmailConfigDetails";
        private const string ConnectionString = "Data Source=tcp:INMUM-GP-005,49172;Initial Catalog=PushNotification;Persist Security Info=True;User ID=full;Password=P@ssw0rd#1";

        private ILogger<PushNotificationService> _logger;

        public EmailConfigurationList GetEmailConfigDetails()
        {
            EmailConfigurationList response = new EmailConfigurationList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response.EmailConfigList = connection.Query<EmailConfigurationDTO>(SP_GetEmailConfigDetails, commandType: CommandType.StoredProcedure);

            }
            return response;
        }
    }
}
