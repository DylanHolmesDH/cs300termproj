using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Factory;
using System;
using System.Collections.Generic;

namespace ManagerTerminal {
    class Terminal {
        static void Main(string[] args) {
            int reportToGenerate = 0;
            bool end = false;

            var serviceFactory = new ServicesFactory();
            var reportServices = serviceFactory.CreateReportServices();
            var database = serviceFactory.CreateDatabaseWrapper();
            var reportFactory = new ReportFactory();

            Console.Write("Hello! ");

            do {
                Console.Write("Would you like to generate a report (yes/no): ");
                string proceed = Console.ReadLine();

                if (ProceedWithGeneratingReport(proceed)) {
                    try {
                        reportToGenerate = DisplayReportOptions();

                        var records = GetMemberOrProviderRecord(reportToGenerate, database);
                        var stringId = DisplayRecords(records);

                    
                        int id = ReturnIdFromStringId(stringId);

                        var result = reportServices.CreateReport(database, reportFactory, reportToGenerate, id);

                        DisplayWhetherReportGenerated(result);
                    }
                    catch (Exception e) {
                        Console.WriteLine("Unsuccessful! See message below:");
                        Console.WriteLine("\t" + "\"" + e.Message + "\"");
                        Console.WriteLine();
                    }
                }
                else {
                    DisplayPartingMessage();
                    end = true;
                }
            } while (!end);
        }

        private static bool ProceedWithGeneratingReport(string proceed) {
            if (proceed == null)
                return false;

            if (proceed.StartsWith("Y") || proceed.StartsWith("y"))
                return true;

            return false;
        }

        private static int DisplayReportOptions() {
            bool invalid = false;
            int reportOptionNumber = 0;

            do {
                Console.WriteLine("Your options are listed below: ");
                Console.WriteLine("\t1) Generate a member report");
                Console.WriteLine("\t2) Generate a provider report");
                Console.WriteLine("\t3) Generate a summary report");
                string reportToPrint = Console.ReadLine();
                Console.WriteLine();

                if (reportToPrint.StartsWith("M") || reportToPrint.StartsWith("m") || reportToPrint == "1") {
                    reportOptionNumber = 1;
                    invalid = false;
                }
                else if (reportToPrint.StartsWith("P") || reportToPrint.StartsWith("p") || reportToPrint == "2") {
                    reportOptionNumber = 2;
                    invalid = false;
                }
                else if (reportToPrint.StartsWith("S") || reportToPrint.StartsWith("s") || reportToPrint == "3") {
                    reportOptionNumber = 3;
                    invalid = false;
                }
                else
                    invalid = true;
            } while (invalid);
            
            return reportOptionNumber;
        }

        private static List<Record> GetMemberOrProviderRecord(int reportToGenerate, IDatabaseWrapper database) {
            List<Record> records = null;

            if (database == null)
                return records;

            if (reportToGenerate == 1)
                records = database.FetchMembers();
            else if (reportToGenerate == 2)
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

                Console.WriteLine(id + "\t\t" + name);
            }

            Console.Write("Enter the 9 digit id of the person that you want to generate a report for (Ex: 999999999): ");
            string stringId = Console.ReadLine();

            return stringId;
        }

        private static int ReturnIdFromStringId(string stringId) {
            if (stringId == null)
                throw new NullReferenceException();

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

        private static void DisplayWhetherReportGenerated((bool created, string errorMessage) result) {
            if (!result.created) {
                if (!string.IsNullOrWhiteSpace(result.errorMessage))
                    Console.WriteLine(result.errorMessage);
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
