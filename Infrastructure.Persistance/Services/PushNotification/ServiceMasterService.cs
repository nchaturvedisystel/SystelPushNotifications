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
using Application.DTOs.User;

namespace Infrastructure.Persistance.Services.PushNotification
{
    public class ServiceMasterService : DABase, IServiceMaster
    {
        APISettings _settings;
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_AlertsServiceMaster_CRUD = "AlertsServiceMaster_CRUD";
        private const string SP_AlertsServiceMaster_StatusUpdate = "AlertsServiceMaster_StatusUpdate";
        private ILogger<ServiceMasterService> _logger;

        public ServiceMasterService(IOptions<ConnectionSettings> connectionSettings, ILogger<ServiceMasterService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<ServiceMasterList> GetServiceMasterList(ServiceMasterDTO serviceMasterDTO)
        {
            ServiceMasterList response = new ServiceMasterList();

            var filename = (DateTime.Now).Ticks;
            var filepath = " ";
            //Save the file
            if (!System.String.IsNullOrEmpty(serviceMasterDTO.AttachmentBase64) && serviceMasterDTO.IsDeleted == 0 && serviceMasterDTO.AttachmentBase64.Trim() != "")
            {
                string filePath = $"{_settings.UploadPath}\\Alerts\\ReportFile\\{filename}";
                filepath = Utilities.SaveFileFromBase64(serviceMasterDTO.WebRootPath, filePath, serviceMasterDTO.AttachmentBase64);
            }
            else
            {
                filepath = serviceMasterDTO.AttachmentPath;
            }

            _logger.LogInformation($" Manage Service Master Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.ServicemasterList = await connection.QueryAsync<ServiceMasterDTO>(SP_AlertsServiceMaster_CRUD, new
                {
                    ServiceId = serviceMasterDTO.ServiceId,
                    Title = serviceMasterDTO.Title,
                    SDesc = serviceMasterDTO.SDesc,
                    AlertType = serviceMasterDTO.AlertType,
                    HasAttachment = serviceMasterDTO.HasAttachment,
                    AttachmentType = serviceMasterDTO.AttachmentType,
                    AttachmentPath = serviceMasterDTO.WebRootPath + "\\" + filepath,
                    AttachmentFileType = serviceMasterDTO.AttachmentFileType,
                    OutputFileName = serviceMasterDTO.OutputFileName,
                    DataSourceType = serviceMasterDTO.DataSourceType,
                    DataSourceDef = serviceMasterDTO.DataSourceDef,
                    PostSendDataSourceType = serviceMasterDTO.PostSendDataSourceType,
                    PostSendDataSourceDef = serviceMasterDTO.PostSendDataSourceDef,
                    EmailTo = serviceMasterDTO.EmailTo,
                    CCTo = serviceMasterDTO.CCTo,
                    BccTo = serviceMasterDTO.BccTo,
                    ASubject = serviceMasterDTO.ASubject,
                    ABody = serviceMasterDTO.ABody,
                    DBConnid = serviceMasterDTO.DBConnId,
                    AlertConfigId = serviceMasterDTO.AlertConfigId,
                    SchedularId = serviceMasterDTO.SchedularId,
                    LastExecutedOn = serviceMasterDTO.LastExecutedOn,
                    NextExecutionTime = serviceMasterDTO.NextExecutionTime,
                    IsActive = serviceMasterDTO.IsActive,
                    IsDeleted = serviceMasterDTO.IsDeleted,
                    ActionUser = serviceMasterDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);;

            }
            return response;
        }

        public async Task<ServiceMasterDTO> ServiceMasterStatusUpdate(ServiceMasterDTO serviceMasterDTO)
        {
            ServiceMasterDTO response = new ServiceMasterDTO();

            _logger.LogInformation($"Updating User Status as Active or Inactive for user: {serviceMasterDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<ServiceMasterDTO>(SP_AlertsServiceMaster_StatusUpdate, new
                {
                    ActionUser = serviceMasterDTO.ActionUser,
                    IsActive = serviceMasterDTO.IsActive,
                    ServiceId = serviceMasterDTO.ServiceId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }

    }
}
