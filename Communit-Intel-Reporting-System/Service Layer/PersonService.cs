using Community_Intel_Reporting_System.DAL;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_LayerQL
{

    internal static class PersonService
    {

        public static void AddPersonByDetails(string firstName, string lastName, string secretCode)
        {
            Person newPerson = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                SecretCode = secretCode,
                type = "user",
                NumReports = 0,
                NumMentions = 0
            };

                AddPerson(newPerson);
 
        }


        public static void AddPerson(Person person)
        {
            DalPerson.AddPerson(person);
        }


        public static Person GetPersonById(int id)
        {
            return DalPerson.GetPersonById(id);
        }

        public static Person GetPersonBySecretCode(string secretCode)
        {
            return DalPerson.GetPersonBySecretCode(secretCode);
        }
        public static Person GetPersonByDetails(string firstName, string lastName, string secretCode)
        {
            return DalPerson.GetPersonByDetails(firstName, lastName, secretCode);
        }


        public static void IncrementCounters(int reporterId, int targetId)
        {
            DalPerson.IncrementNumReports(reporterId);
            DalPerson.IncrementNumMentions(targetId);
        }


        public static void UpdateUserType(int personId, string newType)
        {
            DalPerson.UpdateUserType(personId, newType);
        }


        public static int GetReportCount(int personId)
        {
           return DalPerson.GetReportsCount(personId);
        }



        public static List<Person> GetAllPersons()
        {
            return DalPerson.GetAllPersons();
        }

        public static void DeletePerson(int personID)
        {
            DalPerson.DeletePerson(personID);
        }



    }


}

