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
        public DateTime StartTime { get; private set; } =  DateTime.Now;
        public string AlertReason { get; set; }
        public int IsActive { get; set; } = 1;

        public DateTime? EndTime { get; set; }
        public string CloseReason { get; set; }

    }


}
