
using Community_Intel_Reporting_System.DAL;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_LayerQL
{

    internal static class PersonService
    {
       
        public static Dictionary<string, object> AddPersonByDetails(string firstName, string lastName, string secretCode)
        {
            

            Person newPerson = new Person(firstName, lastName, secretCode);
            DalPerson.AddPerson(newPerson);
          
            return DalPerson.GetPersonByDetails(firstName, lastName, secretCode);
        }

        public static void AddPerson(Person person)
        {
            DalPerson.AddPerson(person);
        }


        public static Dictionary<string, object> GetPersonById(int id)
        {
            return DalPerson.GetPersonById(id);
        }

   
        public static Dictionary<string, object> GetPersonByDetails(string firstName, string lastName, string secretCode)
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
           return DalPerson.GetReportCount(personId);
        }




        public static void GetAllPersons()
        {
            DalPerson.GetAllPersons();
        }
        public static void DeletePerson(int personID)
        {
            DalPerson.DeletePerson(personID);
        }



    }


}

