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
    public class WhatsAppConfigService : DABase, IWhatsAppConfig
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_WhatsAppConfig_CRUD = "WhatsAppConfig_CRUD";
        private const string SP_WhatsAppConfig_StatusUpdate = "WhatsAppConfig_StatusUpdate";
        private ILogger<WhatsAppConfigService> _logger;

        public WhatsAppConfigService(IOptions<ConnectionSettings> connectionSettings, ILogger<WhatsAppConfigService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }
        public async Task<WhatsAppConfigurationList> GetWhatsAppConfigurationList(WhatsAppConfigDTO whatsAppConfigDTO)
        {
            WhatsAppConfigurationList response = new WhatsAppConfigurationList();

            _logger.LogInformation($" Manage WhatsApp Configuration Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                response.WhatsAppConfigList = await connection.QueryAsync<WhatsAppConfigDTO>(SP_WhatsAppConfig_CRUD, new
                {

                    WAConfigId = whatsAppConfigDTO.WAConfigId,
                    IName = whatsAppConfigDTO.IName,
                    IDesc = whatsAppConfigDTO.IDesc,
                    WAUrl = whatsAppConfigDTO.WAUrl,
                    AccessToken = whatsAppConfigDTO.AccessToken,
                    MProduct = whatsAppConfigDTO.MProduct,
                    IType = whatsAppConfigDTO.IType,
                    IsActive = whatsAppConfigDTO.IsActive,
                    IsDeleted = whatsAppConfigDTO.IsDeleted,
                    ActionUser = whatsAppConfigDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public async Task<WhatsAppConfigDTO> WhatsAppConfigurationListStatusUpdate(WhatsAppConfigDTO whatsAppConfigDTO)
        {
            WhatsAppConfigDTO response = new WhatsAppConfigDTO();

            _logger.LogInformation($"Updating WhatsAppConfig Status as Active or Inactive for user: {whatsAppConfigDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<WhatsAppConfigDTO>(SP_WhatsAppConfig_StatusUpdate, new
                {
                    ActionUser = whatsAppConfigDTO.ActionUser,
                    IsActive = whatsAppConfigDTO.IsActive,
                    WAConfigId = whatsAppConfigDTO.WAConfigId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }


       
    }
}
