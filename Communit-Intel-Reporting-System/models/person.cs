using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community_Intel_Reporting_System.Service_LayerQL
{



    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get;  set; }
        public string SecretCode { get; set;}

        public string type { get; set; }

        public int NumReports { get; set; } = 0;
        public int NumMentions { get; set; } = 0;

        public Person()
        {
           
            type = "user";
        }
    }
}

