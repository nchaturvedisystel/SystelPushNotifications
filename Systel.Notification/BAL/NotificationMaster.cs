using Amazon.Runtime.Internal.Transform;
using Systel.Notification.Common;
using Systel.Notification.Interface;
using Systel.Notification.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systel.Notification.BAL
{
    public class NotificationMaster
    {
        protected readonly EncryptDecryptService encryptDecryptService = new EncryptDecryptService();
        private readonly ILogger<NotificationMaster> _logger;
        private readonly WorkerOptions options;
        private readonly INotificationMaster notificationMaster;
        public NotificationMaster(ILogger<NotificationMaster> logger, WorkerOptions options, INotificationMaster notificationMaster)
        {
            _logger = logger;
            this.options = options;
            this.notificationMaster = notificationMaster;
        }

        public void ExecutionServicemaster()
        {
            ServiceMasterList serviceMasterList = new ServiceMasterList();
            serviceMasterList = notificationMaster.GetExecutionServicemaster();

            foreach (ServiceMasterDTO serviceMasterDTO in serviceMasterList.ServicemasterList)
            {
                serviceMasterDTO.Passwrd = encryptDecryptService.DecryptValue(serviceMasterDTO.Passwrd);
                DataTable serviceDataTable =  GetServiceData(serviceMasterDTO);
                Dictionary<string, dynamic> keyValuePairs = GetServiceVariables(serviceMasterDTO);

                if(serviceMasterDTO.AlertType == "Email")
                {
                    GenerateEmailSchedularData(serviceMasterDTO, serviceDataTable, keyValuePairs);
                }
            }
        }

        public DataTable GetServiceData(ServiceMasterDTO serviceMasterDTO)
        {
            DataSet ServiceDataSet = new DataSet();
            if (serviceMasterDTO.DataSourceType == "Query")
            {
                string DBConnString = $"Data Source={serviceMasterDTO.ServerName};Initial Catalog={serviceMasterDTO.DBName};Persist Security Info=True;User ID={serviceMasterDTO.UserName};Password={serviceMasterDTO.Passwrd}";
                ServiceDataSet = notificationMaster.GetServiceMasterDataByQuery(DBConnString, serviceMasterDTO.DataSourceDef);
            }
            return ServiceDataSet.Tables[0];
        }
        public Dictionary<string, dynamic> GetServiceVariables(ServiceMasterDTO serviceMasterDTO)
        {
            Dictionary<string, dynamic> keyValuePairs = new Dictionary<string, dynamic>();
            if(!string.IsNullOrEmpty(serviceMasterDTO.ServiceVariables))
            {
                string[] variablesList = serviceMasterDTO.ServiceVariables.Split(",");
                foreach (string variable in variablesList)
                {
                    string[] VariableKV = variable.Split(':');
                    dynamic KeyValue;
                    if(VariableKV[2] == "INT")
                    {
                        KeyValue = Convert.ToInt32(VariableKV[1]);
                    }
                    else
                    {
                        KeyValue = VariableKV[1];
                    }
                    keyValuePairs.Add(VariableKV[0].Trim(), KeyValue);
                }
            }
            return keyValuePairs;
        }
        public void GenerateEmailSchedularData(ServiceMasterDTO serviceMasterDTO, DataTable dataTable, Dictionary<string, dynamic> keyValuePairs)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                PushNotificationDTO pushNotificationDTO = new PushNotificationDTO();
                pushNotificationDTO.NType = "Email";
                pushNotificationDTO.NSubject = ReplaceVariables(serviceMasterDTO.ASubject, row, keyValuePairs);
                pushNotificationDTO.NContent = ReplaceVariables(serviceMasterDTO.ABody, row, keyValuePairs);
                pushNotificationDTO.NStatus = "Pending";
                pushNotificationDTO.NTo = ReplaceVariables(serviceMasterDTO.EmailTo, row, keyValuePairs);
                pushNotificationDTO.NCc = serviceMasterDTO.CCTo;
                pushNotificationDTO.NBcc = serviceMasterDTO.BccTo;
                pushNotificationDTO.RetryCount = 0;
                pushNotificationDTO.IsDeleted = 0;
                pushNotificationDTO.Remarks = "";
                pushNotificationDTO.ScheduledDate = DateTime.Now;
                pushNotificationDTO.AttachmentPath = "";

                //Insert into Push Notification
            }
        }
        public string ReplaceVariables(string Content, DataRow row, Dictionary<string, dynamic> keyValuePairs)
        {
            foreach(KeyValuePair<string, dynamic> keyValue in keyValuePairs)
            {
                string oldValue = keyValue.Key;
                string newValue = row[keyValue.Value].ToString();
                Content = Content.Replace(oldValue, newValue);
            }
            return Content;
        }
    }
}
