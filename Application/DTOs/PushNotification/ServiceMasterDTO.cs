using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PushNotification
{
    public class ServiceMasterDTO
    {

        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string SDesc { get; set; }
        public string AlertType { get; set; }
        public int HasAttachment { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentFileType { get; set; }
        public string OutputFileName { get; set; }
        public string DataSourceType { get; set; }
        public string DataSourceDef { get; set; }
        public string PostSendDataSourceType { get; set; }
        public string PostSendDataSourceDef { get; set; }
        public string EmailTo { get; set; }
        public string CCTo { get; set; }
        public string BccTo { get; set; }
        public string ASubject { get; set; }
        public string ABody { get; set; }
        public int DBConnId { get; set; }
        public int AlertConfigId { get; set; }
        public int SchedularId { get; set; }
        public DateTime LastExecutedOn { get; set; }
        public DateTime NextExecutionTime { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ActionUser { get; set; }
        public string SchedularName { get; set; }
        public string ConnName { get; set; }
        public string EmailConfigName { get; set; }
        
    }

    public class ServiceMasterList
    {
        public IEnumerable<ServiceMasterDTO> ServicemasterList { get; set; }
    }

}
