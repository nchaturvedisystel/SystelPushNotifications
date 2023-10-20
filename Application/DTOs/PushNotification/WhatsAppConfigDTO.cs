using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PushNotification
{
    public class WhatsAppConfigDTO
    {

        public int WAConfigId { get; set; }
        public string IName { get; set; }
        public string IDesc { get; set; }
        public string WAUrl { get; set; }
        public string AccessToken { get; set; }
        public string MProduct { get; set; }
        public string IType { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ActionUser { get; set; }
    }

    public class WhatsAppConfigurationList
    {
        public IEnumerable<WhatsAppConfigDTO> WhatsAppConfigList { get; set; }
    }
}
