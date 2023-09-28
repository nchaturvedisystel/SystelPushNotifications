using Application.DTOs.PushNotification;
using Application.Interfaces;
using Application.Interfaces.PushNotification;
using Infrastructure.Persistance.Services.PushNotification;
using Systel.Notification.Common;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using Infrastructure;
using Dapper;
using System.Text.RegularExpressions;
using System.IO;

namespace Systel.Notification
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int SchedularTimer = 5000;

                PushNotification pushNotification = new PushNotification();
                pushNotification.ProcessNotification();


                await Task.Delay(SchedularTimer, stoppingToken);
            }
        }

    }
}