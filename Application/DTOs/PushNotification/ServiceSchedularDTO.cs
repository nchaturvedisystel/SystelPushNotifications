using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PushNotification
{
    public class ServiceSchedularDTO
    {
        public int MappperId { get; set; }
        public int ServiceId { get; set; }
        public int SchedularId { get; set; }
        public DateTime LastExecutionTime { get; set; }
        public DateTime NextExecutionTime { get; set; }
        public DateTime StartsFrom { get; set; }
        public DateTime EndsOn { get; set; }
        public string DailyStartOn { get; set; }
        public string DailyEndsOn { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ActionUser { get; set; }
        public string IName { get; set; }
        public string Title { get; set; }

    }

    public class ServiceSchedularList
    {
        public IEnumerable<ServiceSchedularDTO> ServiceschedularList { get; set; }
    }

}
