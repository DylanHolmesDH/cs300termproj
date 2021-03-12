using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator;
using ReportGenerator.Factory;
using System;
using System.Collections.Generic;

namespace ManagerTerminal {
    class Terminal {
        static void Main(string[] args) {
            bool end = false;

            var serviceFactory = new ServicesFactory();
            var reportFactory = new ReportFactory();

            var reportServices = serviceFactory.CreateReportServices();
            var crudServices = serviceFactory.CreateCrudServices();
            var database = serviceFactory.CreateDatabaseWrapper();
            int id = 0;
            TypeOfReport typeOfReport = 0;
            TypeOfCrudAction typeOfCrudAction = 0;
            int nextIdAvailable = 300000000;
            UserInterfaceRecord userInterfaceRecord = new UserInterfaceRecord();

            Console.Write("Hello! ");

            do {
                try {
                    var optionNumber = DisplayOptions();

                    if (optionNumber == 1 || optionNumber == 2)
                    {
                        var records = GetMemberOrProviderRecord(optionNumber, database);
                        var stringId = DisplayRecords(records);
                        id = ReturnIdFromStringId(stringId);
                    }

                    if (optionNumber == 1 || optionNumber == 2 || optionNumber == 3) {
                        typeOfReport = reportServices.DetermineTypeOfReport(optionNumber);

                        var result = reportServices.CreateReport(database, reportFactory, typeOfReport, id);

                        DisplayWhetherValid(result);
                    }
                    else if (optionNumber == 4 || optionNumber == 5) {
                        GetNameAndAddress(userInterfaceRecord);

                        typeOfCrudAction = DoAction(serviceFactory, crudServices, database, nextIdAvailable, userInterfaceRecord, optionNumber);

                        nextIdAvailable++;
                    }
                    else if (optionNumber == 6 || optionNumber == 7) {
                        bool successful = false;

                        while (!successful) {
                            Console.Write("Enter in the number: ");
                            string number = Console.ReadLine();

                            int realNumber = 0;

                            successful = Int32.TryParse(number, out realNumber);

                            if (successful)
                                userInterfaceRecord.Number = realNumber;
                        }

                        GetNameAndAddress(userInterfaceRecord);

                        typeOfCrudAction = DoAction(serviceFactory, crudServices, database, nextIdAvailable, userInterfaceRecord, optionNumber);

                        nextIdAvailable++;
                    }
                    else if (optionNumber == 8 || optionNumber == 9) {
                        bool successful = false;

                        while (!successful) {
                            Console.Write("Enter in the number: ");
                            string number = Console.ReadLine();

                            int realNumber = 0;

                            successful = Int32.TryParse(number, out realNumber);

                            if (successful)
                                userInterfaceRecord.Number = realNumber;
                        }

                        typeOfCrudAction = DoAction(serviceFactory, crudServices, database, nextIdAvailable, userInterfaceRecord, optionNumber);

                        nextIdAvailable++;
                    }
                    else {
                        DisplayPartingMessage();
                        end = true;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Unsuccessful! See message below:");
                    Console.WriteLine("\t" + "\"" + e.Message + "\"");
                    Console.WriteLine();
                }
            } while (!end);
        }

        private static TypeOfCrudAction DoAction(ServicesFactory serviceFactory, ICrudServices crudServices, IDatabaseWrapper database, int nextIdAvailable, UserInterfaceRecord userInterfaceRecord, int optionNumber) {
            TypeOfCrudAction typeOfCrudAction = crudServices.DetermineTypeOfCrudAction(optionNumber);
            var result = crudServices.DoCrudAction(database, typeOfCrudAction, userInterfaceRecord, serviceFactory, nextIdAvailable);

            DisplayWhetherValid(result);
            return typeOfCrudAction;
        }

        private static void GetNameAndAddress(UserInterfaceRecord userInterfaceRecord) {
            Console.Write("Enter in the Name: ");
            userInterfaceRecord.Name = Console.ReadLine();
            Console.Write("Enter in the Street address: ");
            userInterfaceRecord.Address = Console.ReadLine();
            Console.Write("Enter in the City: ");
            userInterfaceRecord.City = Console.ReadLine();
            Console.Write("Enter in the State: ");
            userInterfaceRecord.State = Console.ReadLine();

            bool successful = false;

            while (!successful) {
                Console.Write("Enter in the Zip code: ");
                string zip = Console.ReadLine();

                int realZip = 0;

                successful = Int32.TryParse(zip, out realZip);

                if (successful)
                    userInterfaceRecord.Zip = realZip;
            }
        }

        private static bool ProceedWithGeneratingReport(string proceed) {
            if (proceed == null)
                return false;

            if (proceed.StartsWith("Y") || proceed.StartsWith("y"))
                return true;

            return false;
        }

        private static int DisplayOptions() {
            bool invalid = false;
            int optionNumber = 0;

            do {
                Console.WriteLine("Would you like to: ");
                Console.WriteLine("\t1) Generate a member report");
                Console.WriteLine("\t2) Generate a provider report");
                Console.WriteLine("\t3) Generate a summary report");
                Console.WriteLine("\t4) Add a member");
                Console.WriteLine("\t5) Add a provider");
                Console.WriteLine("\t6) Update a member");
                Console.WriteLine("\t7) Update a provider");
                Console.WriteLine("\t8) Remove a member");
                Console.WriteLine("\t9) Remove a provider");
                Console.WriteLine("\t0) Quit");
                Console.Write("Please enter the option number (0-9) before each action as shown above: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option) {
                    case "1":
                    case "Generate member report":
                    case "generate member report":
                        invalid = false;
                        optionNumber = 1;
                        break;
                    case "2":
                    case "Generate provider report":
                    case "generate provider report":
                        invalid = false;
                        optionNumber = 2;
                        break;
                    case "3":
                    case "Generate summary report":
                    case "generate summary report":
                        invalid = false;
                        optionNumber = 3;
                        break;
                    case "4":
                    case "Add member":
                    case "add member":
                        optionNumber = 4;
                        invalid = false;
                        break;
                    case "5":
                    case "Add provider":
                    case "add provider":
                        optionNumber = 5;
                        invalid = false;
                        break;
                    case "6":
                    case "Update member":
                    case "update member":
                        optionNumber = 6;
                        invalid = false;
                        break;
                    case "7":
                    case "Update provider":
                    case "update provider":
                        invalid = false;
                        optionNumber = 7;
                        break;
                    case "8":
                    case "Remove member":
                    case "remove member":
                        optionNumber = 8;
                        invalid = false;
                        break;
                    case "9":
                    case "Remove provider":
                    case "remove provider":
                        invalid = false;
                        optionNumber = 9;
                        break;
                    case "0":
                    case "Quit":
                    case "quit":
                    case "Exit":
                    case "exit":
                        invalid = false;
                        optionNumber = 0;
                        break;
                    default:
                        invalid = true;
                        break;
                }
            } while (invalid);
            
            return optionNumber;
        }

        private static List<Record> GetMemberOrProviderRecord(int optionNumber, IDatabaseWrapper database) {
            List<Record> records = null;

            if (database == null)
                return records;

            if (optionNumber == 1)
                records = database.FetchMembers();
            else if (optionNumber == 2)
                records = database.FetchProviders();

            return records;
        }

        private static string DisplayRecords(List<Record> records) {
            if (records == null)
                return string.Empty;

            int id = 0;
            string name = string.Empty;

            foreach (var record in records) {
                id = (int)record.Get("number");
                name = (string) record.Get("name");

                Console.WriteLine(id.ToString().PadLeft(9, '0') + "\t\t" + name);
            }

            Console.Write("Enter the 9 digit id of the person that you want to generate a report for (Ex: 999999999): ");
            string stringId = Console.ReadLine();

            return stringId;
        }

        private static int ReturnIdFromStringId(string stringId) {
            if (stringId == null)
                throw new NullReferenceException();

            if (stringId.Length < 9)
                stringId = stringId.PadLeft(9, '0');

            if (stringId.Length < 9 || stringId.Length > 9)
                throw new ApplicationException("The id needs to be of length 9!");

            int id = 0;
            int exponent = 9;

            foreach (var character in stringId) {
                if (character >= '0' && character <= '9')
                    id = (character - '0') * (int)Math.Pow(10, exponent - 1) + id;

                exponent--;
            }

            return id;
        }

        private static void DisplayWhetherValid((bool successful, string errorMessage) result) {
            if (!result.successful) {
                if (!string.IsNullOrWhiteSpace(result.errorMessage)) {
                    Console.WriteLine("Unsuccessful! See message below:");
                    Console.WriteLine("\t" + "\"" + result.errorMessage + "\"");
                }
            }  
            else
                Console.WriteLine("Successful!");

            Console.WriteLine();
        }

        private static void DisplayPartingMessage() {
            Console.WriteLine("Have a nice day...");
            Console.WriteLine();
        }
    }
}
