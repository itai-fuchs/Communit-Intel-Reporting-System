using Community_Intel_Reporting_System.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.Service_LayerQL
{
    internal static class DalAlert
    {
        
        public static void AddAlert(Alert alert)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"INSERT INTO alerts (target_id, start_time, alert_reason, is_active, end_time, close_reason) " +
                        $"VALUES ('{alert.TargetId}', '{alert.StartTime:yyyy-MM-dd HH:mm:ss}', '{alert.AlertReason}', '{alert.IsActive}', '{alert.EndTime:yyyy-MM-dd HH:mm:ss}', '{alert.CloseReason}')";

                    DBConnection.ExecuteNonQuery(sql, conn);
                    Logger.Info($"New alert created for person with ID {alert.TargetId}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[ADD ERROR] Failed to add alert: {ex.Message}");
            }
        }

        
        public static void EndAlert(int alertId, string closeReason)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"UPDATE alerts SET end_time = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'," +
                                 $" is_active = 0, close_reason = '{MySqlHelper.EscapeString(closeReason)}' WHERE id = {alertId}";

                    int rowsAffected = DBConnection.ExecuteNonQuery(sql, conn);

                    if (rowsAffected > 0)
                    {
                        Logger.Info("Alert ended successfully.");
                    }
                    else
                    {
                        Logger.Info($"No alert found with ID {alertId}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[END ALERT ERROR] Failed to end alert: {ex.Message}");
            }
        }

        public static Alert GetAlertById(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM alerts WHERE id = {id}";
                    var rows = DBConnection.Execute(sql, conn);

                    if (rows.Count > 0)
                    {
                        var row = rows[0];

                        Alert alert = new Alert
                        {
                            Id = Convert.ToInt32(row["id"]),
                            TargetId = Convert.ToInt32(row["target_id"]),
                            AlertReason = row["alert_reason"].ToString(),
                            EndTime = row["end_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["end_time"]),
                            CloseReason = row["close_reason"]?.ToString()
                        };

                        return alert;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[GET ERROR] Failed to retrieve alert by ID: {ex.Message}");
                return null;
            }
        }

        public static List<Alert> GetAllAlertsById(int targetId)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM alerts WHERE target_id ={targetId}" ;
                    var rows = DBConnection.Execute(sql, conn);

                    List<Alert> alerts = new List<Alert>();

                    foreach (var row in rows)
                    {
                        Alert alert = new Alert
                        {
                            Id = Convert.ToInt32(row["id"]),
                            TargetId = Convert.ToInt32(row["target_id"]),
                            AlertReason = row["alert_reason"].ToString(),
                            EndTime = row["end_time"] == DBNull.Value ? default(DateTime?) : Convert.ToDateTime(row["end_time"]),

                            CloseReason = row["close_reason"]?.ToString()
                        };

                        alerts.Add(alert);
                    }

                    Logger.Info("[INFO] GET ALL ALERT");
                    return alerts;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[GET ERROR] Failed to retrieve alerts: {ex.Message}");
                return new List<Alert>();
            }
        }


        
       
       
        public static void DeleteAlert(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"DELETE FROM alerts WHERE id = {id}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                    Console.WriteLine("Alert deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DELETE ERROR] Failed to delete alert: {ex.Message}");
            }
        }
    }
}
