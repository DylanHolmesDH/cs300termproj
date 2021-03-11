using System;
using System.Collections.Generic;

using ChocAnDatabase;
using ChocAnDatabase.records;

namespace ProviderTerminal {
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
            String flag = null;
            int login_success = 0;
            db = new Database();

            //repeats until user either quits or gets the right login
            do {
                login_success = Login();
            } while (login_success == 2);

            //only enters loop if the login was successful
            do {
                PrintOptions();
                flag = Console.ReadLine();
                switch (flag) {
                    //checks member number, goes to member sub menu
                    case "1":
                        VerifyAndSetActiveMember();
                        break;
                    //generates entire provider directory in alphabetical order
                    case "2":
                        GenerateProviderDirectory();
                        break;
                    //for provider to create a record by filling out a form
                    case "3":
                        CreateConsulationRecord();
                        break;
                    //checks if record exists in database, and if it does, displays it
                    case "4":
                        VerificationOfBilling();
                        break;
                    //to exit program
                    case "5":
                        break;
                    //otherwise input is bad
                    default:
                        Console.WriteLine("Please select a valid option!");
                        Console.ReadLine();
                        break;
                }
            } while (flag != "5");

            db.Save();
        }

        private int Login() {
            String input = null;
            int provider_num = 0;

            try {
                //clear the screen of text and display a login "menu"
                //get the provider number
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Please enter your ID number. To exit this program, type 'quit' instead.\n\n");
                input = GetInput("ID Number: ");
                provider_num = Convert.ToInt32(input);

                if (input == "quit" || input == "'quit'" || input == "q")
                    return 3;

                var record = db.FetchProvider(provider_num);
                providerName = record.Name;

                //check if the provider number exists in the database 
                if (record != null) {
                    //provider num exists in database
                    return 1;
                }

                Console.WriteLine("ID number does not exist. Please try again.");
                return 2;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return 2;
            }
		}

        void VerificationOfBilling() {
        }

        void PrintOptions() {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(providerName + "\n");
            Console.WriteLine("\t[1] Verify a Member");
            Console.WriteLine("\t[2] Check the Provider Directory");
            Console.WriteLine("\t[3] Create Consultation Record");
            Console.WriteLine("\t[4] Verify a Bill");
            Console.WriteLine("\t[5] Exit\n");
            Console.WriteLine("Please enter a number: ");
		}

        bool VerifyAndSetActiveMember() {
            activeMember = GetInputInt("Please enter the member id: ");
            // WIP
            bool memberIsValid = true;
            if (memberIsValid) {
                Console.WriteLine("Member '" + activeMember + "' is valid.");
            } else {
                Console.WriteLine("Member '" + activeMember + "' is not valid.");
            }
            return memberIsValid;
        }
        
        void BillMember() {
            if (!VerifyAndSetActiveMember()) {
                return;
            }
            Dictionary<String, object> newRecordData = new Dictionary<String, object>();

            //public ConsultationRecord(Dictionary<String, object> keyValues) : base(keyValues) {
            //    this.recordDate = (DateTime)Get("record_date");
            //    this.serviceDate = (DateTime)Get("service_date");
            //    this.memberNumber = GetInteger("member_number");
            //    this.serviceNumber = GetInteger("service_number");
            //    this.providerNumber = GetInteger("provider_number");
            //    this.comments = GetString("comments");
            //}

            newRecordData.Add("record_date", GetInputDate("Record date: "));
            newRecordData.Add("service_date", GetInputDate("Service date: "));
            while (!VerifyAndSetActiveMember()) {
                Console.WriteLine("Please try again.");
            }
            newRecordData.Add("record_date", activeMember);

            //    this.serviceNumber = GetInteger("service_number");
            //    this.providerNumber = GetInteger("provider_number");


            // Provider directory lookup
            // Key in service code

            newRecordData.Add("comments", GetInputString("Any extra comments? "));
            db.InsertConsultation(new ConsultationRecord(newRecordData));
            // Write record to database
            // Display fee


            // WIP
        }

        int GetServiceIDFromUser() {
            // WIP
            // Will be an interactive communication with the user to select a service
            return 598470;
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












        // Start of the Provider Terminal
        /*public void Startt() {
            // Initialize variables & connections
            //  db = new Database(...);

            //if (!db.IsConnected()) {
            //  ...
            //return;
            //}
            db = new Database();

            CreateConsulationRecord();

            db.Save();
            // Continously update UI Thread until break has been requested.
            while (!breakRequested) {
                UILoop();
            }
        }*/

        // The Main UI Thread loop.
        public void UILoop() {
            //...
        }

        // Sets the active Member
        public void SetActiveMember(int memberId) {
            this.activeMember = memberId;
        }

        // Get the Active Member
        public int GetActiveMember() {
            return this.activeMember;
        }

        // Checks the Provided member id
        public String CheckinMember(int memberId) {

            return "Validated";
        }
        // Attempt to Login the Active Provider.
        public String LoginProvider() {
            //String id = GetMultiInput()[0];

            return "...";
        }

        // Creates a ConsultationRecord for the active Member.
        public String CreateConsulationRecord() {
            // dummy method body to represent how a consultation record might be created
            var data = new Dictionary<String, object>();

            data.Add("current_date", DateTime.Now);
            data.Add("service_date", DateTime.Now);

            data.Add("provider_number", 1);
            data.Add("member_number", 333);

            data.Add("comments", "None");
            data.Add("service_number", 1);

            db.InsertConsultation(new ConsultationRecord(data));
            return "...";
        }


        // Requests the database for a directory of services (AKA ProviderDirectory)
        // Returns it as a Record, then converts the record into a string.
        public String GenerateProviderDirectory() {

            int id = this.providerId;


            return "...";
        }

        // Writes a message to the terminal.
        public void WriteMessage(String message) {
            Console.WriteLine("[Terminal] " + message);
        }





    }
}
