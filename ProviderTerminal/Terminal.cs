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

        static void Main(string[] args) {
            Terminal term = new Terminal();
            term.Start();
        }

        void Start() {
            db = new Database();
            Login();
            while (true) {
                PrintOptions();
                int input = GetInputOption();
                switch (input) {
                    case 1:
                        VerifyMember();
                        return;
                    case 2:
                        BillMember();
                        return;
                }
            }
            db.Save();
        }

        void Login() {
            // WIP
            providerId = 1;
		}

        void PrintOptions() {
            // WIP
		}

        int GetInputOption() {
            // WIP
            return 1;
		}

        bool VerifyMember() {
            int memberId = GetInputInt("Please enter the member id: ");
            bool memberIsValid = db.FetchMember(memberId) != null;
            if (memberIsValid) {
                Console.WriteLine("Member '" + memberId + "' is valid.");
            } else {
                Console.WriteLine("Member '" + memberId + "' is not valid.");
            }
            return memberIsValid;
        }
        
        void BillMember() {
            if (!VerifyMember()) {
                return;
            }
            // WIP
		}

        int GetServiceIDFromUser() {
            // WIP
            // Will be an interactive communication with the user to select a service
            return 598470;
        }







        string GetInput(string message) {
            Console.WriteLine(message);
            return Console.ReadLine();

        }

        int GetInputInt(string message) {
            Console.WriteLine(message);
            //return (int)Console.ReadLine();
            return 1;
        }












        // Start of the Provider Terminal
        public void Startt() {
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
        }

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
            String id = GetMultiInput()[0];

            return "...";
        }

        // Creates a ConsultationRecord for the active Member.
        public String CreateConsulationRecord() {
            // dummy method body to represent how a consultation record might be created
            var data = new Dictionary<string, object>();

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

        // Get a Single Line of Input
        public String GetInput(String message) {
            return Console.ReadLine();
        }

        // Gets a Multi-Line of Input terminated when an empty line is entered.
        public List<String> GetMultiInput() {
            List<String> inputs = new List<String>();
            String input = GetInput("");
            while ((input = GetInput("")) != "") {
                inputs.Add(input);
            }

            return inputs;
        }



    }
}
