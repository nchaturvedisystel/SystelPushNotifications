using Application.DTOs.PushNotification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PushNotification
{
    public interface IPushNotification
    {
        public Task<PushNotificationList> GetNotificationList(PushNotificationDTO pushNotification);
        public PushNotificationList GetPushNotifications();
        public void UpdatePushNotifications(DataTable pushNotificationList);

    }
}
