
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
                    Console.WriteLine("Report added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ADD ERROR] Failed to add report: {ex.Message}");
            }
        }

        public static List<Dictionary<string, object>> GetAllReports()
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = "SELECT * FROM reports";
                    return DBConnection.Execute(sql, conn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GET ERROR] Failed to retrieve reports: {ex.Message}");
                return new List<Dictionary<string, object>>();
            }
        }

        public static Dictionary<string, object> GetReportById(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM reports WHERE id = {id}";
                    var rows = DBConnection.Execute(sql, conn);
                    return rows.Count > 0 ? rows[0] : null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GET ERROR] Failed to retrieve report by ID: {ex.Message}");
                return null;
            }
        }

        public static void DeleteReport(int id)
        {
            try

            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"DELETE FROM reports WHERE id = {id}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                    Console.WriteLine("Report deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DELETE ERROR] Failed to delete report: {ex.Message}");
            }
        }
    }
}
