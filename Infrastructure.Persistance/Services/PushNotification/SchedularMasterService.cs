using Application.Common;
using Application.DTOs.PushNotification;
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
using Application.Interfaces.PushNotification;
using Dapper;

namespace Infrastructure.Persistance.Services.PushNotification
{
    public class SchedularMasterService : DABase, ISchedularMaster
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_AlertsSchedular_CRUD = "AlertsSchedular_CRUD";
        private const string SP_AlertsSchedular_StatusUpdate = "AlertsSchedular_StatusUpdate";
        private ILogger<SchedularMasterService> _logger;

        public SchedularMasterService(IOptions<ConnectionSettings> connectionSettings, ILogger<SchedularMasterService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }

        public async Task<SchedularMasterList> GetSchedularMasterList(SchedularMasterDTO schedularMasterDTO)
        {
            SchedularMasterList response = new SchedularMasterList();

            _logger.LogInformation($" Manage Schedular Master Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.SchedularmasterList = await connection.QueryAsync<SchedularMasterDTO>(SP_AlertsSchedular_CRUD, new
                {
                    SchedularId  = schedularMasterDTO.SchedularId,
                    IName = schedularMasterDTO.IName,
                    ICode = schedularMasterDTO.ICode,
                    IDesc = schedularMasterDTO.IDesc,
                    FrequencyInMinutes = schedularMasterDTO.FrequencyInMinutes,
                    SchedularType = schedularMasterDTO.SchedularType,
                    IsActive = schedularMasterDTO.IsActive,
                    IsDeleted = schedularMasterDTO.IsDeleted,
                    ActionUser = schedularMasterDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public async Task<SchedularMasterDTO> SchedularMasterStatusUpdate(SchedularMasterDTO schedularMasterDTO)
        {
            SchedularMasterDTO response = new SchedularMasterDTO();

            _logger.LogInformation($"Updating User Status as Active or Inactive for user: {schedularMasterDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<SchedularMasterDTO>(SP_AlertsSchedular_StatusUpdate, new
                {
                    ActionUser = schedularMasterDTO.ActionUser,
                    IsActive = schedularMasterDTO.IsActive,
                    SchedularId = schedularMasterDTO.SchedularId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }

        
    }
}
