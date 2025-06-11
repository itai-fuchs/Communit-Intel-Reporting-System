using Community_Intel_Reporting_System.Service_LayerQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.DAL
{
    internal static class DalPerson
    {
        public static void AddPerson(Person person)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"INSERT INTO persons (first_name, last_name, secret_code, type, num_reports, num_mentions)" +
                        $"VALUES ('{person.FirstName}', '{person.LastName}', '{person.SecretCode}','{person.type}', '{person.NumReports}', '{person.NumMentions}')";

                    DBConnection.ExecuteNonQuery(sql, conn);
                }

                Logger.Info("Person added successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error($"[ADD ERROR] {ex.Message}");
            }
        }

        public static Dictionary<string, object> GetPersonById(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM persons WHERE id = {id}";
                    var rows = DBConnection.Execute(sql, conn);
                    return rows.Count > 0 ? rows[0] : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[SELECT BY ID ERROR] {ex.Message}");
                return null;
            }
        }

        public static Dictionary<string, object> GetPersonByDetails(string firstName, string lastName, string secretCode)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"SELECT * FROM persons " +
                        $" WHERE first_name = '{firstName}' " +
                        $"AND last_name = '{lastName}' " +
                        $" AND secret_code = '{secretCode}'";

                    var rows = DBConnection.Execute(sql, conn);
                    return rows.Count > 0 ? rows[0] : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[DETAILS ERROR] {ex.Message}");
                return null;
            }
        }
        public static void IncrementNumReports(int personId)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"UPDATE persons SET num_reports = num_reports + 1 WHERE id = {personId}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[UPDATE ERROR] Failed to increment NumReports: {ex.Message}");
            }
        }

        public static void IncrementNumMentions(int personId)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"UPDATE persons SET num_mentions = num_mentions + 1 WHERE id = {personId}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[UPDATE ERROR] Failed to increment NumMentions: {ex.Message}");
            }
        }



        public static void UpdateUserType(int personId, string newType)
        {
            var allowedTypes = new HashSet<string> { "user", "agent", "suspect" };
            
            if (!allowedTypes.Contains(newType.ToLower()))
            {
                Logger.Error($"[UPDATE TYPE ERROR] Invalid user type: '{newType}'");
                return;
            }

            try
            {
                string sql = $"UPDATE persons SET type = '{newType}' WHERE id = {personId}";

                using (var conn = DBConnection.Connect())
                {
                    DBConnection.Execute(sql, conn);
                }

                Logger.Info($"User type for person with ID {personId} updated to '{newType}'.");
            }
            catch (Exception ex)
            {
                Logger.Error($"[UPDATE TYPE ERROR] Failed to update user type: {ex.Message}");
            }
        }



        public static int GetReportCount(int personId)
        {
            string sql = $"SELECT COUNT(*) FROM reports WHERE reporter_id = {personId}";

            using (var conn = DBConnection.Connect())
            {
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }



        public static List<Dictionary<string, object>> GetAllPersons()
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = "SELECT * FROM persons";
                    return DBConnection.Execute(sql, conn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[SELECT ALL ERROR] {ex.Message}");
                return new List<Dictionary<string, object>>();
            }
        }
        public static void DeletePerson(int id)
        {
            try
            {
                using (MySqlConnection conn = DBConnection.Connect())
                {
                    string sql = $"DELETE FROM persons WHERE id = {id}";
                    DBConnection.ExecuteNonQuery(sql, conn);
                }

                Logger.Info("Person deleted.");
            }
            catch (Exception ex)
            {
                Logger.Error($"[DELETE ERROR] {ex.Message}");
            }
        }

    }


}
