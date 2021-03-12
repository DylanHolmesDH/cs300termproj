using System;
using System.Collections.Generic;

using ChocAnDatabase;
using ChocAnDatabase.records;

namespace ProviderTerminal {
    class Terminal {
        Database db;
        ProviderRecord provider;
        MemberRecord member;
        List<String> exitWords = new List<String>{ "e", "E", "exit", "Exit", "q", "Q", "quit", "Quit", };
        
        static void Main(String[] args) {
            Terminal term = new Terminal();
            term.Start();
        }

        public void Start() {
            db = new Database();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            provider = new ProviderRecord(temp);
            provider.Name = "Emerson";
            provider.Number = 69;
            provider.State = "OR";
            provider.Zip = 97531;
            provider.Address = "SrawberryLn";
            provider.City = "Portland";
            db.InsertProvider(provider);
            db.Save();
            return;

            // Login
            Console.Clear();
            Console.WriteLine("LOGIN");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Please enter your ID number. To exit this program, type 'quit' instead.\n\n");
            while (true) {
                String input = GetInputString("ID Number: ");
                if (input == "" || exitWords.Contains(input)) {
                    return;
                }
				try {
                    provider = db.FetchProvider(Convert.ToInt32(input));
				} catch (FormatException) {
					Console.WriteLine("Please enter a valid number (or 'quit')");
                    continue;
				}
				if (provider == null) {
                    Console.WriteLine("ID number does not exist. Please try again.");
                    continue;
                }
                break;
            }

            // Main menu
            bool exit = false;
			while (!exit) {
                Console.Clear();
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Welcome, " + provider.Name + ".\n");
                Console.WriteLine("\t[1] Check-in Member");              // Checks member number, goes to member sub menu
                Console.WriteLine("\t[2] Check the Provider Directory"); // Generates entire provider directory in alphabetical order
                Console.WriteLine("\t[3] Create Consultation Record");   // For provider to create a record by filling out a form
                Console.WriteLine("\t[4] Verify a Bill");                // Checks if record exists in database, and if it does, displays it
                Console.WriteLine("\t[0] Exit\n");
                while (true) {
                    String input = GetInputString("Please enter an option: ");
                    if (input == "1") {
                        CheckinMember();
                    } else if (input == "2") {
                        GenerateProviderDirectory();
                    } else if (input == "3") {
                        CreateConsulationRecord();
                    } else if (input == "4") {
                        VerificationOfBilling();
                    } else if (input == "0" || input == "" || exitWords.Contains(input)) {
                        exit = true;
                        Console.WriteLine("See ya!");
                    } else {
                        Console.WriteLine("Sorry, that's not a valid option.");
                        continue;
                    }
                    break;
                }
            }
            db.Save();
        }

        // Checks if a member number is valid, if their membership is suspended, or if they were never a member
        void CheckinMember() {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(provider.Name + "\n\n\n\n\n\n\n");

            // Get a valid member from user
            while (true) {
                String input = GetInputString("Please enter the member id (swipe card): ");
                if (input == "" || exitWords.Contains(input)) {
                    return;
                }
                try {
                    member = db.FetchMember(Convert.ToInt32(input));
                } catch (FormatException) {
                    Console.WriteLine("Please enter a valid number (or 'quit')");
                    continue;
                }
                if (member == null) {
                    Console.WriteLine("Sorry, that's not an existing member.");
                    continue;
                }
                if (!member.Validated) {
                    Console.WriteLine("Sorry, member '" + member.Name + "' is not a valid member (anymore).");
                    return;
                }
                Console.WriteLine("Member '" + member.Name + "' is now checked in.");
                break;
            }

            // Member menu
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(provider.Name + "; now serving " + member.Name + ".\n");
            Console.WriteLine("\t[1] ");
            Console.WriteLine("\t[0] Main menu\n");
            while (true) {
                String option = GetInputString("Please enter an option: ");
                if (option == "1") {

                } else if (option == "2") {

                } else if (option == "0" || option == "" || exitWords.Contains(option)) {
                    Console.WriteLine("Going back to main menu.");
                    return;
                } else {
                    Console.WriteLine("Sorry, that's not a valid option.");
                    continue;
                }
                break;
            }
        }

        void GenerateProviderDirectory() {
            List<Record> services = db.FetchServices();
            foreach (Record service in services) {
                Console.WriteLine(service.ToString());
            }
        }

        void CreateConsulationRecord() {
            //if (!CheckinMember()) {
            //    return;
            //}
            Dictionary<String, object> newRecordData = new Dictionary<String, object>();
            newRecordData.Add("record_date", GetInputDate("Record date: "));
            newRecordData.Add("service_date", GetInputDate("Service date: "));
            //while (!CheckinMember()) {
            //    Console.WriteLine("Please try again.");
            //}
            newRecordData.Add("member_number", member);
            newRecordData.Add("provider_number", provider.Number);
            GenerateProviderDirectory();
            newRecordData.Add("service_number", GetInputInt("Service code: "));
            newRecordData.Add("comments", GetInputString("Any extra comments? "));
            db.InsertConsultation(new ConsultationRecord(newRecordData));
            // Display fee
        }

        void VerificationOfBilling() {
            GenerateProviderDirectory();
            if (db.FetchConsultation(
                    GetInputInt("Member ID: "),
                    GetInputInt("Service code: "),
                    provider.Number,
                    GetInputDate("Record date: "),
                    GetInputDate("Service date: ")) != null) {
                Console.WriteLine("The info entered pertains to an existing record in the database.");
            }
        }

        String GetInputString(String message = "") {
            Console.WriteLine(message);
            return Console.ReadLine();

        }

        int GetInputInt(String message = "") {
            Console.WriteLine(message);
            return Convert.ToInt32(Console.ReadLine());
        }

        DateTime GetInputDate(String message = "") {
            Console.WriteLine(message);
            return DateTime.Parse(Console.ReadLine());
        }

        List<String> GetInputMultiLine(String message = "") {
            Console.WriteLine(message);
            List<String> inputs = new List<String>();
            String input;
            while ((input = GetInputString()) != "") {
                inputs.Add(input);
            }
            return inputs;
        }
    }
}
