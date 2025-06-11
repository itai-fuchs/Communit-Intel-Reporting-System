using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal class AlertService
    {


        private readonly PersonService personService=  new PersonService();

        
        public void CheckAndCreateAlertIfNeeded(int targetId)
        {
            var person = new PersonService().GetPersonById(targetId);
            if (person == null)
            {
                Console.WriteLine("Person not found.");
                return;
            }

            int mentions = Convert.ToInt32(person["num_mentions"]);

            if (mentions >= 20)
            {
                Alert alert = new Alert(targetId)
                {
                    AlertReason = $"Mentioned {mentions} times",
                    CloseReason = null
                };

                DalAlert.AddAlert(alert);
                Console.WriteLine($"New alert created for person with ID {targetId}");
                personService.UpdateUserType(targetId, "suspect");
            }
          
        }

    public void GetAllAlerts()
{
    DalAlert.GetAllAlerts();
}

public void GetAlertById(int id)
{
    DalAlert.GetAlertById(id);
}

        public void CloseAlert(int alertId)
        {
            Console.WriteLine("Enter a reason for closing the alert:");
            string closeReason = Console.ReadLine();
            

           DalAlert.EndAlert(alertId, closeReason);
        }


        public void DeleteAlert(int id)
{
    DalAlert.DeleteAlert(id);
}
    }
}

