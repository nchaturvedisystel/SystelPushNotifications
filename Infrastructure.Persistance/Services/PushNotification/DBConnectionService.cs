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
using Application.Common;

namespace Infrastructure.Persistance.Services.PushNotification
{
    public class DBConnectionService : DABase, IDBConnection
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private const string SP_DBConnectionMaster_CRUD = "DBConnectionMaster_CRUD";
        private const string SP_DBConnectionMaster_StatusUpdate = "DBConnectionMaster_StatusUpdate";
        private ILogger<DBConnectionService> _logger;
        public DBConnectionService(IOptions<ConnectionSettings> connectionSettings, ILogger<DBConnectionService> logger, IOptions<APISettings> settings) : base(connectionSettings.Value.DBCONN)
        {
            _logger = logger;
        }

        public async Task<DBConnectionList> GetDBConnectionList(DBConnectionDTO dbConnectionDTO)
        {
            DBConnectionList response = new DBConnectionList();

            _logger.LogInformation($" Manage DB Connection Credentials ");

            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
                response.DBConnList = await connection.QueryAsync<DBConnectionDTO>(SP_DBConnectionMaster_CRUD, new
                {

                    DBConnId = dbConnectionDTO.DBConnId,
                    ConnName = dbConnectionDTO.ConnName,
                    ServerName = dbConnectionDTO.ServerName,
                    UserName = dbConnectionDTO.UserName,
                    Passwrd = encryptDecryptService.EncryptValue(dbConnectionDTO.Passwrd),
                    DBName = dbConnectionDTO.DBName,
                    IsActive = dbConnectionDTO.IsActive,
                    IsDeleted = dbConnectionDTO.IsDeleted,
                    ActionUser = dbConnectionDTO.ActionUser,

                }, commandType: CommandType.StoredProcedure);

            }
            return response;
        }

        public async Task<DBConnectionDTO> DBConnStatusUpdate(DBConnectionDTO dBConnectionDTO)
        {
            DBConnectionDTO response = new DBConnectionDTO();

            _logger.LogInformation($"Updating User Status as Active or Inactive for user: {dBConnectionDTO.ActionUser}");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                response = await connection.QueryFirstOrDefaultAsync<DBConnectionDTO>(SP_DBConnectionMaster_StatusUpdate, new
                {
                    ActionUser = dBConnectionDTO.ActionUser,
                    IsActive = dBConnectionDTO.IsActive,
                    DBConnId = dBConnectionDTO.DBConnId
                }, commandType: CommandType.StoredProcedure);
            }
            return response;
        }
    }
}
