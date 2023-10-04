using Amazon.Runtime.Internal.Transform;
using CrystalDecisions.CrystalReports.Engine;
using Systel.Notification.Common;
using Systel.Notification.Interface;
using Systel.Notification.Model;
using System.Data;

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
                if(serviceMasterDTO.HasAttachment == 1)
                {
                    if(serviceMasterDTO.AttachmentType.Trim() == "CrystalReport")
                    {
                        pushNotificationDTO.AttachmentPath = GenerateAttachmentGetPath(serviceMasterDTO);
                    }
                }

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
        public string GenerateAttachmentGetPath(ServiceMasterDTO serviceMasterDTO)
        {
            string AttachmentPath = "";
            if(serviceMasterDTO.AttachmentFileType == "PDF")
            {
                AttachmentPath = GenratePdfFromCrystalReport(serviceMasterDTO);
            }
            return AttachmentPath;
        }
        public string GenratePdfFromCrystalReport(ServiceMasterDTO serviceMasterDTO)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(serviceMasterDTO.AttachmentPath);

            DataSet dataSet = new DataSet();
            rpt.SetDataSource(dataSet);
            // Assign Paramters after set datasource
            rpt.SetParameterValue("DocKey", "60241");

            //ExportOptions rptExportOption;
            //DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
            //string reportFileName = options.ServiceDocPath+ serviceMasterDTO.ServiceId + @"\SampleReport.pdf";
            //rptFileDestOption.DiskFileName = reportFileName;
            //rptExportOption = rpt.ExportOptions;
            //{
            //    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
            //    //if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
            //    //if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
            //    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    rptExportOption.ExportDestinationOptions = rptFileDestOption;
            //    rptExportOption.ExportFormatOptions = rptFormatOption;
            //}

            rpt.Export();

            return "";
        }
    }
}
