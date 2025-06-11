using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal static class ReportService
    {
        public static void SubmitReport(int reporterId, string targetFirstName, string targetLastName, string targetSecretCode, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Cannot submit empty report.");
                return;
            }

            Dictionary<string, object> target = PersonService.GetPersonByDetails(targetFirstName, targetLastName, targetSecretCode);

            if (target == null)
            {
                target = PersonService.AddPersonByDetails(targetFirstName, targetLastName, targetSecretCode);
            }

            int targetId = Convert.ToInt32(target["id"]);

            Report report = new Report(reporterId, targetId, text);
            DalReport.AddReport(report);
 
            PersonService.IncrementCounters(reporterId, targetId);

            AlertService.CheckAndCreateAlertIfNeeded(targetId);

            int reporterReportCount = PersonService.GetReportCount(reporterId);
            if (reporterReportCount > 10)
            {
                PersonService.UpdateUserType(reporterId, "agent");
            }
        }

        public static void DeleteReport(int id)
        {
            DalReport.DeleteReport(id);
        }
    }
}
