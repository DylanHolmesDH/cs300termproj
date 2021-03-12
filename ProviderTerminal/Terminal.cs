using System;
using System.Collections.Generic;

using ChocAnDatabase.records;
using ChocAnDatabase;

namespace ProviderTerminal {
    class Terminal {
        Database db;
        ProviderRecord provider;
        MemberRecord member;
        List<String> exitWords = new List<String>{ "e", "E", "exit", "Exit", "q", "Q", "quit", "Quit", };
        
        static void Main(String[] args) {
            Terminal term = new Terminal();
            term.MainMenu();
        }


        //////////////// Main Menu ////////////////

        public void MainMenu() {
            db = new Database("./database.db");

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
                Console.WriteLine("Hello, " + provider.Name + ".\n");
                Console.WriteLine("\t[1] Member Menu");
                Console.WriteLine("\t[2] Check the Provider Directory");
                Console.WriteLine("\t[3] Verify a Bill");
                Console.WriteLine("\t[0] Exit\n");
                while (true) {
                    String input = GetInputString("Please enter an option: ");
                    if (input == "1") {
                        MemberMenu();
                    } else if (input == "2") {
                        GenerateProviderDirectory();
                    } else if (input == "3") {
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


        //////////////// Member Menu ////////////////

        void MemberMenu() {
            // Get a valid member from user
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(provider.Name + "\n\n\n\n\n\n\n");
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
                break;
            }

            // Member menu
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Hello " + provider.Name + ", you're now serving " + member.Name + ".\n");
            Console.WriteLine("\t[1] Create Consultation Record");
            Console.WriteLine("\t[0] Main menu\n");
            while (true) {
                String option = GetInputString("Please enter an option: ");
                if (option == "1") {
                    CreateConsulationRecord();
                //} else if (option == "2") {
                //    Console.WriteLine("Yay! Option 2!");
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

        void CreateConsulationRecord() {
            GenerateProviderDirectory();
            Dictionary<String, object> newRecordData = new Dictionary<String, object>();
            newRecordData.Add("record_date", GetInputDate("Record date: "));
            newRecordData.Add("service_date", GetInputDate("Service date: "));
            newRecordData.Add("member_number", member.Number);
            newRecordData.Add("provider_number", provider.Number);
            newRecordData.Add("service_number", GetInputInt("Service code: "));
            newRecordData.Add("comments", GetInputString("Any extra comments? "));
            db.InsertConsultation(new ConsultationRecord(newRecordData));
            // Display fee
        }


        //////////////// Tools ////////////////

        void GenerateProviderDirectory() {
            List<Record> services = db.FetchServices();
            foreach (Record service in services) {
                Console.WriteLine(service.ToString());
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
