using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using System;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal static class AlertService
    {


        

        
        public static void CheckAndCreateAlertIfNeeded(int targetId)
        {
            var person =PersonService.GetPersonById(targetId);
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
                PersonService.UpdateUserType(targetId, "suspect");
            }
          
        }

    public static void GetAllAlerts()
{
    DalAlert.GetAllAlerts();
}

public static void GetAlertById(int id)
{
    DalAlert.GetAlertById(id);
}

        public static void CloseAlert(int alertId)
        {
            Console.WriteLine("Enter a reason for closing the alert:");
            string closeReason = Console.ReadLine();
            

           DalAlert.EndAlert(alertId, closeReason);
        }


        public static void DeleteAlert(int id)
{
    DalAlert.DeleteAlert(id);
}
    }
}

