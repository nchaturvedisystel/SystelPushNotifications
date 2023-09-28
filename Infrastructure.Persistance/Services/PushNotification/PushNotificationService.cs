using Application.DTOs.PushNotification;
using Application.Interfaces.PushNotification;
using Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class PushNotificationService : DABase, IPushNotification
    {
        private const string SP_GetPushNotifications = "GetPushNotifications";
        private const string SP_NotificationMaster_UpdateStatus = "NotificationMaster_UpdateStatus";
        private const string ConnectionString = "Data Source=tcp:INMUM-GP-005,49172;Initial Catalog=PushNotification;Persist Security Info=True;User ID=full;Password=P@ssw0rd#1";

        private ILogger<PushNotificationService> _logger;

        //public Task<PushNotificationList> GetNotificationList(PushNotificationDTO pushNotification)
        //{
        //    throw new NotImplementedException();
        //}
        public PushNotificationService(IOptions<ConnectionSettings> connectionSettings, ILogger<PushNotificationService> logger) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }
        public PushNotificationService()
        {

        }

        public async Task<PushNotificationList> GetNotificationList(PushNotificationDTO pushNotification)
        {
            PushNotificationList response = new PushNotificationList();
            _logger.LogInformation($"Started fetching suggestion for Searchbar text  ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                response.NotificationList = await connection.QueryAsync<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public PushNotificationList GetPushNotifications()
        {
            PushNotificationList response = new PushNotificationList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response.NotificationList = connection.Query<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public void UpdatePushNotifications(DataTable pushNotificationList)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query<PushNotificationDTO>(SP_NotificationMaster_UpdateStatus, new
                {
                    typNotificationMaster = pushNotificationList.AsTableValuedParameter("dbo.typNotificationMaster")
                },
                     commandType: CommandType.StoredProcedure);

            }
        }
    }
}
