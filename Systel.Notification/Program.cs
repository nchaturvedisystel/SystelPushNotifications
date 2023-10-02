using Systel.Notification;
using Systel.Notification.Common;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        WorkerOptions options = configuration.GetSection("AppKeys").Get<WorkerOptions>();

        options.DBConn = DBConn.GetConnectionString(options.AppKeyPath);

        services.AddSingleton(options);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
