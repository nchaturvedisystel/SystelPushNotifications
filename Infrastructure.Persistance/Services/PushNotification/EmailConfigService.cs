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
    public class EmailConfigService : DABase, IEmailConfig
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_EmailConfig_CRUD = "EmailConfig_CRUD";
        private const string SP_EmailConfig_StatusUpdate = "EmailConfig_StatusUpdate";
        private ILogger<EmailConfigService> _logger;
        public EmailConfigService(IOptions<ConnectionSettings> connectionSettings, ILogger<EmailConfigService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }

        public async Task<EmailConfigurationList> GetEmailConfigurationList(EmailConfigDTO emailConfigDTO)
        {
            EmailConfigurationList response = new EmailConfigurationList();

            _logger.LogInformation($" Manage Email Configuration Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.EmailConfigList = await connection.QueryAsync<EmailConfigDTO>(SP_EmailConfig_CRUD, new
                {

                    EmailConfigId = emailConfigDTO.EmailConfigId,
                    IName = emailConfigDTO.IName,
                    IDesc = emailConfigDTO.IDesc,
                    IHost = emailConfigDTO.IHost,
                    IPort = emailConfigDTO.IPort,
                    IFrom = emailConfigDTO.IFrom,
                    IPassword = encryptDecryptService.EncryptValue(emailConfigDTO.IPassword),
                    IEnableSsl = emailConfigDTO.IEnableSsl,
                    IsBodyHtml = emailConfigDTO.IsBodyHtml,
                    IsActive = emailConfigDTO.IsActive,
                    IsDeleted = emailConfigDTO.IsDeleted,
                    ActionUser = emailConfigDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public async Task<EmailConfigDTO> EmailConfigurationListStatusUpdate(EmailConfigDTO emailConfigDTO)
        {
            EmailConfigDTO response = new EmailConfigDTO();

            _logger.LogInformation($"Updating User Status as Active or Inactive for user: {emailConfigDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<EmailConfigDTO>(SP_EmailConfig_StatusUpdate, new
                {
                    ActionUser = emailConfigDTO.ActionUser,
                    IsActive = emailConfigDTO.IsActive,
                    EmailConfigId = emailConfigDTO.EmailConfigId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }
    }
}
