using Application.Common;
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
    public  class ServiceSchedularService : DABase, IServiceSchedular
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_AlertsServiceSchedular_CRUD = "AlertsServiceSchedular_CRUD";
        private const string SP_ServiceSchedular_StatusUpdate = "AlertsServiceSchedular_StatusUpdate";

        private ILogger<ServiceSchedularService> _logger;



        public ServiceSchedularService(IOptions<ConnectionSettings> connectionSettings, ILogger<ServiceSchedularService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }

        public async Task<ServiceSchedularList> GetServiceschedularList(ServiceSchedularDTO serviceSchedularDTO)
        {
            ServiceSchedularList response = new ServiceSchedularList();

            _logger.LogInformation($" Manage Schedular Master Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.ServiceschedularList = await connection.QueryAsync<ServiceSchedularDTO>(SP_AlertsServiceSchedular_CRUD, new
                {
                    MappperId = serviceSchedularDTO.MappperId,
                    SchedularId = serviceSchedularDTO.SchedularId,
                    ServiceId = serviceSchedularDTO.ServiceId,
                    LastExecutionTime = serviceSchedularDTO.LastExecutionTime,
                    NextExecutionTime = serviceSchedularDTO.NextExecutionTime,
                    StartsFrom = serviceSchedularDTO.StartsFrom,
                    EndsOn = serviceSchedularDTO.EndsOn,
                    DailyStartOn = serviceSchedularDTO.DailyStartOn,
                    DailyEndsOn = serviceSchedularDTO.DailyEndsOn,
                    IsActive = serviceSchedularDTO.IsActive,
                    IsDeleted = serviceSchedularDTO.IsDeleted,
                    ActionUser = serviceSchedularDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);


            }
            return response;
        }

        public async Task<ServiceSchedularDTO> ServiceSchedularStatusUpdate(ServiceSchedularDTO serviceSchedularDTO)
        {
            ServiceSchedularDTO response = new ServiceSchedularDTO();

            _logger.LogInformation($"Updating User Status as Active or Inactive for user: {serviceSchedularDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<ServiceSchedularDTO>(SP_ServiceSchedular_StatusUpdate, new
                {
                    ActionUser = serviceSchedularDTO.ActionUser,
                    IsActive = serviceSchedularDTO.IsActive,
                    MappperId = serviceSchedularDTO.MappperId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }

        
    }
}
