using System;
using System.Collections.Generic;

using ChocAnDatabase.records;
using ChocAnDatabase;

namespace ProviderTerminal {
    class Terminal {

        private int providerId, activeMember;
        private List<int> members;
        //private Database db;
        private bool breakRequested;
        private Database db;

        // Start of the Provider Terminal
        public void Start() {
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

            data.Add("record_date", DateTime.Now);
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



        static void Main(string[] args) {
            Terminal p = new Terminal();

            p.Start();
        }
    }
}
