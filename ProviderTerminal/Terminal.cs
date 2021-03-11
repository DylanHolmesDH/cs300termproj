using System;
using System.Collections.Generic;

using ChocAnDatabase;
using ChocAnDatabase.records;

namespace ProviderTerminal {

    //possible options. includes success and failure for verfication purposes
    enum option
    {
        member = 1,
        providerDir = 2,
        consultRec = 3,
        veriBill = 4,
        quit = 5,
        success = 6,
        fail = 7
    }

    class Terminal {
        private int providerId, activeMember;
        private List<int> members;
        private bool breakRequested;
        private Database db;
        private String providerName;

        static void Main(String[] args) {
            Terminal term = new Terminal();
            term.Start();
        }

        //Start of the Provider Terminal
        public void Start() {
            option flag;
            option login_success;
            db = new Database();

            //repeats until user either quits or gets the right login
            do {
                login_success = Login();
            } while (login_success == option.fail);

            //only enters loop if the login was successful
            do {
                PrintOptions();
                flag = GetInputOption("Please enter a number: ");
                switch (flag) {
                    //checks member number, goes to member sub menu
                    case option.member:
                        VerifyAndSetActiveMember();
                        break;
                    //generates entire provider directory in alphabetical order
                    case option.providerDir:
                        GenerateProviderDirectory();
                        break;
                    //for provider to create a record by filling out a form
                    case option.consultRec:
                        CreateConsulationRecord();
                        break;
                    //checks if record exists in database, and if it does, displays it
                    case option.veriBill:
                        VerificationOfBilling();
                        break;
                    //to exit program
                    case option.quit:
                        break;
                    //otherwise input is bad
                    default:
                        Console.WriteLine("Please select a valid option!");
                        Console.ReadLine();
                        break;
                }
            } while (flag != option.quit);

            db.Save();
        }

        private option Login() {
            String input = null;

            try {
                //clear the screen of text and display a login "menu"
                //get the provider number
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Please enter your ID number. To exit this program, type 'quit' instead.\n\n");
                input = GetInputString("ID Number: ");
                providerId = Convert.ToInt32(input);

                if (input == "quit" || input == "'quit'" || input == "q")
                    return option.quit;

                var record = db.FetchProvider(providerId);
                providerName = record.Name;

                //check if the provider number exists in the database 
                if (record != null) {
                    //provider num exists in database
                    return option.success;
                }

                Console.WriteLine("ID number does not exist. Please try again.");
                return option.fail;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return option.fail;
            }
        }

        void PrintOptions() {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(providerName + "\n");
            Console.WriteLine("\t[" + (int)option.member + "] Verify a Member");
            Console.WriteLine("\t[" + (int)option.providerDir + "] Check the Provider Directory");
            Console.WriteLine("\t[" + (int)option.consultRec + "] Create Consultation Record");
            Console.WriteLine("\t[" + (int)option.veriBill + "] Verify a Bill");
            Console.WriteLine("\t[" + (int)option.quit + "] Exit\n");
		}

        //checks if a member number is valid, if their membership is suspended, or if they were never a member
        bool VerifyAndSetActiveMember() {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(providerName + "\n\n\n\n\n\n\n");
            activeMember = GetInputInt("Please enter the member id: ");

            bool memberIsValid = true;
            if (memberIsValid) {
                Console.WriteLine("Member '" + activeMember + "' is valid.");
            } else {
                Console.WriteLine("Member '" + activeMember + "' is not valid.");
            }
            return memberIsValid;
        }

        void GenerateProviderDirectory() {
            List<Record> services = db.FetchServices();
            foreach (Record service in services) {
                Console.WriteLine(service.ToString());
            }
        }

        void CreateConsulationRecord() {
            if (!VerifyAndSetActiveMember()) {
                return;
            }
            Dictionary<String, object> newRecordData = new Dictionary<String, object>();
            newRecordData.Add("record_date", GetInputDate("Record date: "));
            newRecordData.Add("service_date", GetInputDate("Service date: "));
            while (!VerifyAndSetActiveMember()) {
                Console.WriteLine("Please try again.");
            }
            newRecordData.Add("member_number", activeMember);
            newRecordData.Add("provider_number", providerId);
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
                    providerId,
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

        option GetInputOption(String message = "")
        {
            Console.WriteLine(message);
            return option.(Console.ReadLine());
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
