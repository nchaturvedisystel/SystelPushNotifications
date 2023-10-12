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
    public class AlertsServiceVariablesService : DABase, IAlertsServiceVariables
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_AlertsServiceVariables_CRUD = "AlertsServiceVariables_CRUD";
        private ILogger<AlertsServiceVariablesService> _logger;

        public AlertsServiceVariablesService(IOptions<ConnectionSettings> connectionSettings, ILogger<AlertsServiceVariablesService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }

        public async Task<AlertsServiceVariablesList> alertsServiceVariablesList(AlertsServiceVariablesDTO alertsServiceVariablesDTO)
        {
            AlertsServiceVariablesList response = new AlertsServiceVariablesList();

            _logger.LogInformation($" Manage Service Master Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.AlertsserviceVariablesList = await connection.QueryAsync<AlertsServiceVariablesDTO>(SP_AlertsServiceVariables_CRUD, new
                {
                    VariableId = alertsServiceVariablesDTO.VariableId,
                    ServiceId = alertsServiceVariablesDTO.ServiceId,
                    VarInstance = alertsServiceVariablesDTO.VarInstance,
                    VarValue = alertsServiceVariablesDTO.VarValue,
                    VarType = alertsServiceVariablesDTO.VarType,

                }, commandType: CommandType.StoredProcedure); ;

            }
            return response;
        }

      }
}
