using Systel.Notification.Common;
using Systel.Notification.BAL;

namespace Systel.Notification
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions options;
        private readonly PushNotification pushNotification;
        public Worker(ILogger<Worker> logger, WorkerOptions options, PushNotification pushNotification)
        {
            this._logger = logger;
            this.options = options;
            this.pushNotification = pushNotification;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int SchedularTimer = (options.ServiceFrequencyInMins * 60 * 1000);

                //PushNotification pushNotification = new PushNotification();
                pushNotification.ProcessNotification();


                await Task.Delay(SchedularTimer, stoppingToken);
            }
        }

    }
}