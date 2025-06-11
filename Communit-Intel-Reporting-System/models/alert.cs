using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community_Intel_Reporting_System.models
{
    public class Alert
    {
        public int Id { get; set; }
        public int TargetId { get; set; }
        public DateTime StartTime { get; private set; }
        public string AlertReason { get; set; }
        public int IsActive { get; set; }

        public DateTime EndTime { get; set; }
        public string CloseReason { get; set; }


        public Alert(int targetId)
        {
            TargetId = targetId;
            IsActive = 1;
            StartTime = DateTime.Now;
        }
    }


}
