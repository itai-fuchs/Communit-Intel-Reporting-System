using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal static class AlertService
    {
        public static void CheckAndCreateAlertIfNeed(int targetId)
        {
            Person person = PersonService.GetPersonById(targetId);
            if (person == null)
                return;

            int mentions = person.NumMentions;
            int recentReports = ReportService.GetRecentReportsCount(targetId, TimeSpan.FromMinutes(15));
            if (mentions >= 20 || recentReports >= 3)
            {
                Alert alert = new Alert()
                {
                    TargetId = targetId,
                    AlertReason = $"Mentioned {mentions} times, {recentReports} reports in last 15 minutes",
                    CloseReason = null
                };

                DalAlert.AddAlert(alert);
                Logger.Info($"Alert created for target {targetId}: {alert.AlertReason}");
                Console.WriteLine($"Alert created for target {targetId}: {alert.AlertReason}");

                PersonService.UpdateUserType(targetId, "suspect");
            }
        }





        public static List<Alert> GetAllAlertsById(int targetId)
        {
           return DalAlert.GetAllAlertsById(targetId);
        }

        public static Alert GetAlertById(int id)
        {
            return DalAlert.GetAlertById(id);
        }

        public static void CloseAlert(int alertId, string closeReason)
        {
            DalAlert.EndAlert(alertId,closeReason);
        }

        public static void DeleteAlert(int id)
        {
            DalAlert.DeleteAlert(id);
        }
    }
}

