using MediatR;
using Systel.Notification;
using Systel.Notification.BAL;
using Systel.Notification.Common;
using Systel.Notification.Interface;
using Systel.Notification.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        WorkerOptions options = configuration.GetSection("AppKeys").Get<WorkerOptions>();

        options.DBConn = DBConn.GetConnectionString(options.AppKeyPath);

        services.AddSingleton(options);

        services.AddHostedService<Worker>();

        services.AddTransient<PushNotification>();
        services.AddTransient<NotificationMaster>();

        services.AddTransient<IPushNotification, PushNotificationService>();
        services.AddTransient<INotificationMaster, NotificationMasterService>();
        services.AddTransient<IEmailConfiguration, EmailConfigurationService>();
    })
    .Build();

host.Run();
