using Community_Intel_Reporting_System.models;
using Community_Intel_Reporting_System.Service_LayerQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_Layer
{
    internal static class ReportService
    {


        public static void SubmitReport(int reporterId, int targetId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                
                throw new ArgumentException("Cannot submit empty report.");
            }

            Report report = new Report()
            {
                ReporterId = reporterId,
                TargetId = targetId,
                Text = text
            };

            DalReport.AddReport(report);

            PersonService.IncrementCounters(reporterId, targetId);

            AlertService.CheckAndCreateAlertIfNeed(targetId);

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


        public static int GetRecentReportsCount(int targetId, TimeSpan timeSpan)
        {
            try
            {
                using (var conn = DBConnection.Connect())
                {
                    string sql = $"SELECT COUNT(*) FROM reports WHERE target_id = {targetId} AND timestamp >= DATE_SUB(NOW(), INTERVAL {timeSpan.TotalMinutes} MINUTE)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[GetRecentReportsCount ERROR] {ex.Message}");
                return 0;
            }
        }
    }
}
