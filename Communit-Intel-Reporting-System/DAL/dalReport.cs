using Community_Intel_Reporting_System.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_LayerQL
{
    internal static class DalReport
    {
        public static void AddReport(Report report)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"INSERT INTO reports (reporter_id, target_id, text, timestamp)" +
                        $" VALUES ({report.ReporterId}, {report.TargetId}, '{report.Text}', '{report.Timestamp:yyyy-MM-dd HH:mm:ss}')";

                    DBConnection.ExecuteNonQuery(sql, conn);
                    Logger.Info("Report added successfully.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[ADD ERROR] Failed to add report: {ex.Message}");
            }
        }


        public static Report GetReportById(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM reports WHERE id = {id}";
                    var rows = DBConnection.Execute(sql, conn);

                    if (rows.Count > 0)
                    {
                        var row = rows[0];
                        return new Report
                        {
                            Id = Convert.ToInt32(row["id"]),
                            ReporterId = Convert.ToInt32(row["reporter_id"]),
                            TargetId = Convert.ToInt32(row["target_id"]),
                            Text = Convert.ToString(row["text"]),
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[GET ERROR] Failed to retrieve report by ID: {ex.Message}");
                return null;
            }
        }


        public static List<Report> GetAllReports()
        {
            List<Report> reports = new List<Report>();

            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = "SELECT * FROM reports";
                    var rows = DBConnection.Execute(sql, conn);

                    foreach (var row in rows)
                    {
                        Report report = new Report
                        {
                            Id = Convert.ToInt32(row["id"]),
                            ReporterId = Convert.ToInt32(row["reporter_id"]),
                            TargetId = Convert.ToInt32(row["target_id"]),
                            Text = row["text"].ToString(),
                          
                        };

                        reports.Add(report);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[GET ERROR] Failed to retrieve reports: {ex.Message}");
            }

            return reports;
        }


     


        public static void DeleteReport(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"DELETE FROM reports WHERE id = {id}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                    Logger.Info("Report deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[DELETE ERROR] Failed to delete report: {ex.Message}");
            }
        }
    }
}
