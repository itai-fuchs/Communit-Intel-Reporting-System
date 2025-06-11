using System;
using System.IO;

namespace Community_Intel_Reporting_System
{
    
public static class Logger
    {
        private static readonly string logFilePath = Path.GetFullPath(@"..\..\system_log.txt");


        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public static void Warning(string message)
        {
            WriteLog("WARNING", message);
        }

        public static void Error(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOGGER ERROR] Failed to write to log: {ex.Message}");
            }
        }
    }

}

