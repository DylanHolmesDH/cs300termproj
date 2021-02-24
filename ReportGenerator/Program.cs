using System;

namespace ReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter 1 to generate provider report, 2 for member, 3 for summary: ");
            int choice = Console.Read();

            try
            {
                int id = 3;

                Processor processor = new Processor();
                (bool created, string errorMessage) result = processor.GetReportGenerator(choice, id);

                if (!result.created)
                {
                    Console.WriteLine(result.errorMessage);
                }
                else
                {
                    Console.WriteLine("Successful");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
