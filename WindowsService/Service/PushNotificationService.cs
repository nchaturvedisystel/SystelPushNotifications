﻿using Systel.Notification.Interface;
using Systel.Notification.Model;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyFirstService;
using MyFirstService.Common;
using System;

namespace Systel.Notification.Service
{
    public class PushNotificationService : IPushNotification
    {
        private const string SP_GetPushNotifications = "GetPushNotifications";
        private const string SP_NotificationMaster_UpdateStatus = "NotificationMaster_UpdateStatus";
        private const string SP_NotificationMaster_Insert = "NotificationMaster_Insert";
        protected readonly Logging logging = new Logging();
        public PushNotificationService()
        {
        }

        public async Task<PushNotificationList> GetNotificationList(PushNotificationDTO pushNotification)
        {
            PushNotificationList response = new PushNotificationList();
            try
            {
                using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
                {
                    response.NotificationList = await connection.QueryAsync<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                logging.LogError("Systel.Notification.Service.PushNotificationService/GetNotificationList : " + ex.Message);
                throw ex;
            }
            return response;
        }

        public PushNotificationList GetPushNotifications()
        {
            PushNotificationList response = new PushNotificationList();
            try
            {
                using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
                {
                    response.NotificationList = connection.Query<PushNotificationDTO>(SP_GetPushNotifications, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                logging.LogError("Systel.Notification.Service.PushNotificationService/GetPushNotifications : " + ex.Message);
                throw ex;
            }
            return response;
        }

        public void UpdatePushNotifications(DataTable pushNotificationList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
                {
                    connection.Query<PushNotificationDTO>(SP_NotificationMaster_UpdateStatus, new
                    {
                        typNotificationMaster = pushNotificationList.AsTableValuedParameter("dbo.typNotificationMaster")
                    },
                         commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                logging.LogError("Systel.Notification.Service.PushNotificationService/UpdatePushNotifications : " + ex.Message);
                throw ex;
            }
        }
        public void InsertPushNotifications(DataTable pushNotificationList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SessionObject.DBConn))
                {
                    connection.Query<PushNotificationDTO>(SP_NotificationMaster_Insert, new
                    {
                        typNotificationMaster = pushNotificationList.AsTableValuedParameter("dbo.typNotificationMaster")
                    },
                         commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                logging.LogError("Systel.Notification.Service.PushNotificationService/InsertPushNotifications : " + ex.Message);
                throw ex;
            }
        }

    }
}