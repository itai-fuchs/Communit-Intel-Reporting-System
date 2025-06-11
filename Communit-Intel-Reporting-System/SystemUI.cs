using Community_Intel_Reporting_System.Service_Layer;
using Community_Intel_Reporting_System.Service_LayerQL;
using System;
using System.Collections.Generic;

namespace Community_Intel_Reporting_System.UI
{
    internal class SystemUI
    {
     
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n--- Welcome to the System ---");
                Console.WriteLine("1. Login");
                Console.WriteLine("0. Exit");
                string input = Console.ReadLine();

                if (input == "1")
                    Login();
                else if (input == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                    Console.WriteLine("Invalid input.");
            }
        }

        private void Login()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Secret Code: ");
            string secretCode = Console.ReadLine();

            var person = PersonService.AddPersonByDetails(firstName, lastName, secretCode);

            if (person == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            string userType = person["type"].ToString().ToLower();
            int userId = Convert.ToInt32(person["id"]);

            switch (userType)
            {
                case "suspect":
                    Console.WriteLine("Access denied. You are marked as a suspect.");
                    break;
                case "user":
                    ShowUserMenu(userId);
                    break;
                case "agent":
                    ShowAgentMenu(userId);
                    break;
                default:
                    Console.WriteLine("Unknown user type.");
                    break;
            }
        }

        private void ShowUserMenu(int userId)
        {
            while (true)
            {
                Console.WriteLine("\n--- User Menu ---");
                Console.WriteLine("1. Submit Report");
                Console.WriteLine("0. Logout");
                Console.Write("Choice: ");
                string input = Console.ReadLine();

                if (input == "1")
                    UISubmitReport(userId);
                else if (input == "0")
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                    Console.WriteLine("Invalid input.");
            }
        }

   
        private void ShowAgentMenu(int userId)
        {
            while (true)
            {
                Console.WriteLine("\n--- Agent Menu ---");
                Console.WriteLine("1. Submit Report");
                Console.WriteLine("2. View All Reports");
                Console.WriteLine("3. View All Alerts");
                Console.WriteLine("4. Close Alert");
                Console.WriteLine("0. Logout");
                Console.Write("Choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        UISubmitReport(userId);
                        break;
                    case "2":
                        ViewAllReports();
                        break;
                    case "3":
                        ViewAllAlerts();
                        break;
                    case "4":
                        UICloseAlert();
                        break;
                    case "0":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }

      
        private void UISubmitReport(int reporterId)
        {
            Console.Write("Target First Name: ");
            string first = Console.ReadLine();

            Console.Write("Target Last Name: ");
            string last = Console.ReadLine();

            Console.Write("Target Secret Code: ");
            string code = Console.ReadLine();

            Console.Write("Report Text: ");
            string text = Console.ReadLine();

            ReportService.SubmitReport(reporterId, first, last, code, text);
        }

        private void ViewAllReports()
        {
            var reports = DalReport.GetAllReports();
            Console.WriteLine("\n--- Reports ---");
            foreach (var r in reports)
            {
                Console.WriteLine($"ID: {r["id"]}, Reporter: {r["reporter_id"]}, Target: {r["target_id"]}, Text: {r["text"]}");
            }
        }

    
        private void ViewAllAlerts()
        {
            var alerts = DalAlert.GetAllAlerts();
            Console.WriteLine("\n--- Alerts ---");
            foreach (var a in alerts)
            {
                Console.WriteLine($"ID: {a["id"]}, Target: {a["target_id"]}, Reason: {a["alert_reason"]}, Active: {a["is_active"]}");
            }
        }

       
        private void UICloseAlert()
        {
            Console.Write("Enter Alert ID: ");
            if (!int.TryParse(Console.ReadLine(), out int alertId))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Console.Write("Enter Close Reason: ");
            string reason = Console.ReadLine();

            AlertService.CloseAlert(alertId);
        }
    }
}
