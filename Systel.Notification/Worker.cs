using Systel.Notification.Common;
using Systel.Notification.BAL;
using Systel.Notification.Interface;

namespace Systel.Notification
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions options;
        private readonly PushNotification pushNotification;
        private readonly NotificationMaster notificationMaster;
        public Worker(ILogger<Worker> logger, WorkerOptions options, PushNotification pushNotification, NotificationMaster notificationMaster)
        {
            this._logger = logger;
            this.options = options;
            this.pushNotification = pushNotification;
            this.notificationMaster = notificationMaster;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int SchedularTimer = (options.ServiceFrequencyInMins * 60 * 1000);

                notificationMaster.ExecutionServicemaster();

                //PushNotification pushNotification = new PushNotification();
                pushNotification.ProcessNotification();


                await Task.Delay(SchedularTimer, stoppingToken);
            }
        }

    }
}