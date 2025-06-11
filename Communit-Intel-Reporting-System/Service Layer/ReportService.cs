
using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal class ReportService
    {
       
        private readonly PersonService personService = new PersonService();
        private readonly AlertService alertService = new AlertService();


        public void SubmitReport(int reporterId, string targetFirstName, string targetLastName, string targetSecretCode, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Cannot submit empty report.");
                return;
            }


            Dictionary<string, object> target = personService.GetPersonByDetails(targetFirstName, targetLastName, targetSecretCode);

   
            if (target == null)
            {
                target = personService.AddPersonByDetails(targetFirstName, targetLastName, targetSecretCode);
            }

            int targetId = Convert.ToInt32(target["id"]);

         
            Report report = new Report(reporterId, targetId, text);
            DalReport.AddReport(report);

           
            personService.IncrementCounters(reporterId, targetId);

            alertService.CheckAndCreateAlertIfNeeded(targetId);
            int reporterReportCount = personService.GetReportCount(reporterId);
            if (reporterReportCount > 10)
            {
                personService.UpdateUserType(reporterId, "agent");
                Console.WriteLine($"Reporter with ID {reporterId} promoted to agent.");
            }





        }


       

        public void DeleteReport(int id)
        {
          DalReport.DeleteReport(id);
        }
    }
}
