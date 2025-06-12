using Community_Intel_Reporting_System.Service_Layer;
using Community_Intel_Reporting_System.Service_LayerQL;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Community_Intel_Reporting_System.UI
{
    internal class SystemUI
    {

        public void Start()
        {
            while (true)
            {
               
                Console.WriteLine("1. Login");
                Console.WriteLine("2. register");
                Console.WriteLine("0. Exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Login();
                        break;

                    case "2":
                        register();
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                       return;
                        

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }

            }
        }


        private void register()
        {
            Console.WriteLine("first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("secret code: ");
            string secretCode = Console.ReadLine();

            var existingPerson = PersonService.GetPersonByDetails(firstName, lastName, secretCode);
            if (existingPerson != null)
            {
                Console.WriteLine("This person already exists in the system.");
                return;  
            }

            PersonService.AddPersonByDetails(firstName, lastName, secretCode);
            Console.WriteLine("Registration successful.");
        }


        private void Login()
        {

            Console.Write("Secret Code: ");
            string secretCode = Console.ReadLine();

            var person = PersonService.GetPersonBySecretCode(secretCode);

            if (person == null)
            {
                Console.WriteLine("need to register first");
                return;
            }

            string userType = person.type.ToString().ToLower();
            int userId = Convert.ToInt32(person.Id);

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
                Console.WriteLine("2. View All ReportsB by id");
                Console.WriteLine("3. View All Alerts");
                Console.WriteLine("4. Close Alert");
                Console.WriteLine("0. Logout");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        UISubmitReport(userId);
                        break;
                    case "2":
                        Console.WriteLine("target id: ");
                        int targetId =int.Parse(Console.ReadLine());
                        UIViewAllReports(targetId);
                        break;
                    case "3":
                        Console.WriteLine("target id: ");
                        targetId = int.Parse(Console.ReadLine());
                        UIViewAllAlertsbYId(targetId);
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
            Console.Write("Target secretCode: ");
            string targetSecretCode = Console.ReadLine();

            var target = PersonService.GetPersonBySecretCode(targetSecretCode);

            if (target == null)
            {
                Console.WriteLine("Target not found. Please enter target details.");

                Console.Write("Target first Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Target last Name: ");
                string lastName = Console.ReadLine();

                PersonService.AddPersonByDetails(firstName, lastName, targetSecretCode);

               Logger.Info("[INFO]New target added");

               
                target = PersonService.GetPersonBySecretCode(targetSecretCode);
            }

            Console.Write("Report Text: ");
            string text = Console.ReadLine();

            try
            {
                ReportService.SubmitReport(reporterId, Convert.ToInt32(target.Id), text);
                Console.WriteLine("Report submitted successfully.");
                Logger.Info("[INFO]Report submitted successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UIViewAllReports(int targetid)
        {
            var reports = DalReport.GetAllReportsByID(targetid);
            Console.WriteLine("\n--- Reports ---");
            foreach (var r in reports)
            {
                Console.WriteLine($"ID: {r.Id}, Reporter: {r.ReporterId}, Target: {r.TargetId}, Text: {r.Text}");
            }
        }

    
        private void UIViewAllAlertsbYId(int targetId)
        {
            var alerts = DalAlert.GetAllAlertsById(targetId);
            Console.WriteLine("\n--- Alerts ---");
            foreach (var a in alerts)
            {
                Console.WriteLine($"ID: {a.Id}, Target: {a.TargetId}, Reason: {a.AlertReason}, Active: {a.IsActive}");
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

            AlertService.CloseAlert(alertId,reason);
        }
    }
}
