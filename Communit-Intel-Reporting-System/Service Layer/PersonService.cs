
using Community_Intel_Reporting_System.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_LayerQL
{

    internal class PersonService
    {
       
        public Dictionary<string, object> AddPersonByDetails(string firstName, string lastName, string secretCode)
        {
            

            Person newPerson = new Person(firstName, lastName, secretCode);
            DalPerson.AddPerson(newPerson);
          
            return DalPerson.GetPersonByDetails(firstName, lastName, secretCode);
        }

        public void AddPerson(Person person)
        {
            DalPerson.AddPerson(person);
        }


        public Dictionary<string, object> GetPersonById(int id)
        {
            return DalPerson.GetPersonById(id);
        }

   
        public Dictionary<string, object> GetPersonByDetails(string firstName, string lastName, string secretCode)
        {
            return DalPerson.GetPersonByDetails(firstName, lastName, secretCode);
        }
        public void IncrementCounters(int reporterId, int targetId)
        {
            DalPerson.IncrementNumReports(reporterId);
            DalPerson.IncrementNumMentions(targetId);
        }


        public void UpdateUserType(int personId, string newType)
        {
            DalPerson.UpdateUserType(personId, newType);
        }

        public int GetReportCount(int personId)
        {
           return DalPerson.GetReportCount(personId);
        }




        public void getAllPersons()
        {
            DalPerson.GetAllPersons();
        }
        public void DeletePerson(int personID)
        {
            DalPerson.DeletePerson(personID);
        }



    }


}

