using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communit_Intel_Reporting_System.models
{

    public enum PersonType
    {
        Reporter,
        Target,
        Both,
        PotentialAgent
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string SecretCode { get; private set; }
        public PersonType Type { get; private set; }
        public int NumReports { get; set; } = 0;
        public int NumMentions { get; set; } = 0;

        public Person(string firstName, string lastName, string secretCode, PersonType type)
        {
            FirstName = firstName;
            LastName = lastName;
            SecretCode = secretCode;
            Type = type;
        }
    }
}

