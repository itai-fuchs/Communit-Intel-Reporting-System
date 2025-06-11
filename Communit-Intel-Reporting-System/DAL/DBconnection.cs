using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace Community_Intel_Reporting_System.Service_LayerQL
{
    /// <summary>
    /// Static class responsible for managing MySQL database connections and executing SQL commands.
    /// </summary>
    internal static class DBConnection
    {
        // Connection string to the MySQL database
        private const string ConnectionString = "server=localhost;port=3306;user=root;password=;database=malshinon;";

        /// <summary>
        /// Opens and returns a new MySqlConnection using the connection string.
        /// </summary>
        public static MySqlConnection Connect()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB ERROR] Failed to connect: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Closes the given MySqlConnection.
        /// </summary>
        public static void Disconnect(MySqlConnection conn)
        {
            conn.Close();
            Console.WriteLine("connection closed");
        }
        /// <summary>
        /// Parses the MySqlDataReader into a list of dictionaries.
        /// Each dictionary contains column-value pairs for a single row.
        /// </summary>
        private static List<Dictionary<string, object>> Parse(MySqlDataReader rdr)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            using (rdr)
            {
                while (rdr.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>(rdr.FieldCount);
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string columnName = rdr.GetName(i);
                        object value = rdr.IsDBNull(i) ? null : rdr.GetValue(i);
                        row[columnName] = value;
                    }
                    rows.Add(row);
                }
            }

            return rows;
        }

        /// <summary>
        /// Executes a SQL query that returns rows (e.g. SELECT) and parses the result into a list of dictionaries.
        /// Each dictionary represents a row with column name as key and column value as value.
        /// </summary>
        public static List<Dictionary<string, object>> Execute(string sql, MySqlConnection conn)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
          
            return Parse(rdr);
        }

        /// <summary>
        /// Executes a SQL non-query command (e.g. INSERT, UPDATE, DELETE).
        /// Returns the number of rows affected.
        /// </summary>
        public static int ExecuteNonQuery(string sql, MySqlConnection conn)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
         
            return cmd.ExecuteNonQuery();
        }

     
        /// <summary>
        /// Maps a list of dictionaries to a list of strongly-typed objects of type T.
        /// Properties are matched by name and converted to the appropriate type.
        /// </summary>


        /// <summary>
        /// Prints the query results (list of dictionaries) to the console in a readable format.
        /// </summary>
        public static void PrintResult(List<Dictionary<string, object>> keyValuePairs)
        {
            if (keyValuePairs.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            foreach (Dictionary<string, object> row in keyValuePairs)
            {
                foreach (KeyValuePair<string, object> kv in row)
                {
                    Console.WriteLine($"{kv.Key}: {kv.Value}");
                }
                Console.WriteLine("---");
            }
        }
    }
}
