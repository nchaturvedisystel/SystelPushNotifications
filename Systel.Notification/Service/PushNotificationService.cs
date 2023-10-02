using Domain.Settings;
using Infrastructure;
using Microsoft.Extensions.Options;
using Systel.Notification.Interface;
using Systel.Notification.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Systel.Notification.Common;

namespace Systel.Notification.Service
{
    public class PushNotificationService : DABase, IPushNotification
    {
        private const string SP_GetPushNotifications = "GetPushNotifications";
        private const string SP_NotificationMaster_UpdateStatus = "NotificationMaster_UpdateStatus";

        private ILogger<PushNotificationService> _logger;
        private WorkerOptions options;
        public PushNotificationService(ILogger<PushNotificationService> logger, WorkerOptions options)
        {
            _logger = logger;
            this.options = options;
        }

        public async Task<PushNotificationList> GetNotificationList(PushNotificationDTO pushNotification)
        {
            PushNotificationList response = new PushNotificationList();
            _logger.LogInformation($"Started fetching suggestion for Searchbar text  ");

            using (SqlConnection connection = new SqlConnection(options.DBConn))
            {
                response.NotificationList = await connection.QueryAsync<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public PushNotificationList GetPushNotifications()
        {
            PushNotificationList response = new PushNotificationList();

            using (SqlConnection connection = new SqlConnection(options.DBConn))
            {
                response.NotificationList = connection.Query<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public void UpdatePushNotifications(DataTable pushNotificationList)
        {

            using (SqlConnection connection = new SqlConnection(options.DBConn))
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
