using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community_Intel_Reporting_System.models
{
    public class Report
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int TargetId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; private set;}

        public Report(int reporterId, int targetId, string text)
        {
            ReporterId = reporterId;
            TargetId = targetId;
            Text = text;
            Timestamp = DateTime.Now;
        }
    }

}
